using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class CircuitRepository:ICircuitRepository
    {
        private SqlConnection connection;

        public CircuitRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        private Circuit MapCircuitResponse(SqlDataReader reader)
        {
            return new Circuit
            {
                CircuitID = (Guid)reader["CircuitID"],
                Name = (string)reader["CircuitName"],
                Location = (string)reader["CircuitLocation"],
                Length = (double)reader["CircuitLength"],
                MaxSpeed = (double)reader["CircuitMaxSpeed"],
                isDeleted = (bool)reader["isDeleted"]
            };
        }

        public async Task<CircuitResponseModel> AddCircuitAsync(Circuit circuit)
        {
            var responseModel = new CircuitResponseModel 
            {
                MessageID = 0,
                Message = string.Empty,
                Circuit = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateCircuit", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", circuit.Name);
                    command.Parameters.AddWithValue("@Location", circuit.Location);
                    command.Parameters.AddWithValue("@Length", circuit.Length);
                    command.Parameters.AddWithValue("@MaxSpeed", circuit.MaxSpeed);

                    // Output Parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);

                    await connection.OpenAsync();
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            responseModel.Circuit = MapCircuitResponse(reader);
                        }
                    }
                    responseModel.MessageID = (int)outputMessageID.Value;
                    responseModel.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageID = -99;
                responseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageID = -100;
                responseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async Task<CircuitListResponseModel> GetAllCircuitsAsync()
        {
            var circuitListReponseModel = new CircuitListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Circuits = new List<Circuit>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllCircuit", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Output Parameters

                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            circuitListReponseModel.Circuits.Add(MapCircuitResponse(reader));
                        }
                    }
                    circuitListReponseModel.MessageID = (int)outputMessageID.Value;
                    circuitListReponseModel.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                circuitListReponseModel.MessageID = -99;
                circuitListReponseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                circuitListReponseModel.MessageID = -100;
                circuitListReponseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return circuitListReponseModel;
        }

        public async Task<CircuitResponseModel> GetCircuitByIdAsync(Guid circuitID)
        {
            var circuitresponseModel = new CircuitResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Circuit = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetCircuitByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CircuitID", circuitID);

                    // Output Parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            circuitresponseModel.Circuit = MapCircuitResponse(reader);
                        }
                    }
                    circuitresponseModel.MessageID = (int)outputMessageID.Value;
                    circuitresponseModel.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                circuitresponseModel.MessageID = -99;
                circuitresponseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                circuitresponseModel.MessageID = -100;
                circuitresponseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return circuitresponseModel;
        }

        public async Task<CircuitResponseModel> UpdateCircuitAsync(Circuit circuit)
        {
            var responseModel = new CircuitResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Circuit = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateCircuit", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CircuitID", circuit.CircuitID);
                    command.Parameters.AddWithValue("@Name", circuit.Name);
                    command.Parameters.AddWithValue("@Location", circuit.Location);
                    command.Parameters.AddWithValue("@Length", circuit.Length);
                    command.Parameters.AddWithValue("@MaxSpeed", circuit.MaxSpeed);
                    command.Parameters.AddWithValue("@isDeleted", circuit.isDeleted);
                    // Output Parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.Circuit = MapCircuitResponse(reader);
                        }
                    }
                    responseModel.MessageID = (int)outputMessageID.Value;
                    responseModel.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageID = -99;
                responseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageID = -100;
                responseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }
    }
}
