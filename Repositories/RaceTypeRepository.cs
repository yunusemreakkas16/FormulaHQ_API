using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class RaceTypeRepository : IRaceTypeRepository
    {
        private SqlConnection connection;

        public RaceTypeRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        private RaceType MapRaceTypeResponse(SqlDataReader reader)
        {
            return new RaceType
            {
                TypeID= (Guid)reader["RaceTypeID"],
                Name = (string)reader["RaceTypeName"],
                Description = (string)reader["RaceTypeDescription"],
                isDeleted = (bool)reader["isDeleted"]
            };
        }


        public async Task<RaceTypeResponseModel> CreateRaceTypeAsync(RaceType raceType)
        {
            var responseModel = new RaceTypeResponseModel
            {
                MessageID = 0,
                MessageDescription = string.Empty,
                RaceType = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateRaceType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", raceType.Name);
                    command.Parameters.AddWithValue("@Description", raceType.Description);

                    //Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    SqlParameter messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255){Direction = ParameterDirection.Output};

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            responseModel.RaceType = MapRaceTypeResponse(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;

                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageID = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageID = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async Task<RacetypeListResponseModel> GetAllRaceTypesAsync()
        {
            var responseModel = new RacetypeListResponseModel
            {
                MessageID = 0,
                MessageDescription = string.Empty,
                RaceTypes = new List<RaceType>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllRaceType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            responseModel.RaceTypes.Add(MapRaceTypeResponse(reader));
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageID = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageID = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async  Task<RaceTypeResponseModel> GetRaceTypeByIdAsync(Guid typeId)
        {
            var responseModel = new RaceTypeResponseModel
            {
                MessageID = 0,
                MessageDescription = string.Empty,
                RaceType = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetRaceTypeByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceTypeID", typeId);
                    //Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.RaceType = MapRaceTypeResponse(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageID = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageID = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async Task<RaceTypeResponseModel> UpdateRaceTypeAsync(RaceType raceType)
        {
            var responseModel = new RaceTypeResponseModel
            {
                MessageID = 0,
                MessageDescription = string.Empty,
                RaceType = null
            };
            try
            {
                using(SqlCommand command = new SqlCommand("usp_UpdateRaceType", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceTypeID", raceType.TypeID);
                    command.Parameters.AddWithValue("@Name", raceType.Name);
                    command.Parameters.AddWithValue("@Description", raceType.Description);
                    command.Parameters.AddWithValue("@isDeleted", raceType.isDeleted);

                    //Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);

                    await connection.OpenAsync();
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.RaceType = MapRaceTypeResponse(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageID = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageID = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
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
