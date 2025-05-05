using FormulaHQ.API.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class FIARegulationRepository : IFIARegulationRepository
    {
        private SqlConnection connection;

        public FIARegulationRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        private FIARegulation MapFIARegulationResponse(SqlDataReader reader)
        {
            return new FIARegulation
            {
                RegulationID = (Guid)reader["FIARegulationID"],
                Name = (string)reader["FIARegulationName"],
                Description = (string)reader["FIARegulationDescription"],
                AffectedTable = (string)reader["FIARegulationAffectedTable"]
            };
        }
        public async Task<FIARegulationResponseModel> AddFIARegulationAsync(FIARegulation regulation)
        {
            var response = new FIARegulationResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Regulation = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateFIARegulation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", regulation.Name);
                    command.Parameters.AddWithValue("@Description", regulation.Description);
                    command.Parameters.AddWithValue("@AffectedTable", regulation.AffectedTable);

                    // Output parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Regulation = MapFIARegulationResponse(reader);
                        }
                    }
                    response.MessageID = (int)outputMessageID.Value;
                    response.Message = (string)outputMessage.Value;

                }
            }
            catch (SqlException sqlEx)
            {
                response.MessageID = -99;
                response.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.MessageID = -100;
                response.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return response;
        }

        public async Task<FIARegulationListResponseModel> GetAllFIARegulationsAsync()
        {
            var response = new FIARegulationListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Regulations = new List<FIARegulation>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllFIARegulations", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Output parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Regulations.Add(MapFIARegulationResponse(reader));
                        }
                    }
                    response.MessageID = (int)outputMessageID.Value;
                    response.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                response.MessageID = -99;
                response.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.MessageID = -100;
                response.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return response;
        }

        public async Task<FIARegulationResponseModel> GetFIARegulationByIDAsync(FIARegulationParamModel regulationParam)
        {
            var response = new FIARegulationResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Regulation = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetFIARegulationByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegulationID", regulationParam.RegulationID);
                    // Output parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Regulation = MapFIARegulationResponse(reader);
                        }
                    }
                    response.MessageID = (int)outputMessageID.Value;
                    response.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                response.MessageID = -99;
                response.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.MessageID = -100;
                response.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return response;
        }

        public async Task<FIARegulationResponseModel> UpdateFIARegulationAsync(FIARegulation regulation)
        {
            var response = new FIARegulationResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Regulation = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateFIARegulation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegulationID", regulation.RegulationID);
                    command.Parameters.AddWithValue("@Name", regulation.Name);
                    command.Parameters.AddWithValue("@Description", regulation.Description);
                    command.Parameters.AddWithValue("@AffectedTable", regulation.AffectedTable);
                    // Output parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Regulation = MapFIARegulationResponse(reader);
                        }
                    }
                    response.MessageID = (int)outputMessageID.Value;
                    response.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                response.MessageID = -99;
                response.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.MessageID = -100;
                response.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return response;
        }

        public async Task<FIARegulationResponseModel> DeleteFIARegulationAsync(Guid FIARegulationID)
        {
            var response = new FIARegulationResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Regulation = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_DeleteFIARegulation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RegulationID", FIARegulationID);

                    // Output parameters
                    SqlParameter outputMessageID = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter outputMessage = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(outputMessageID);
                    command.Parameters.Add(outputMessage);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                    response.MessageID = (int)outputMessageID.Value;
                    response.Message = (string)outputMessage.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                response.MessageID = -99;
                response.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                response.MessageID = -100;
                response.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return response;

        }
    }
}
