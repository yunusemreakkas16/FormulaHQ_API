using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class RaceResultRepository : IRaceResultRepository
    {
        private SqlConnection connection;

        public RaceResultRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        public static RaceResult MapToRaceResult(SqlDataReader reader)
        {
            return new RaceResult
            {
                ResultID = (Guid)reader["ResultID"],
                Position = (int)reader["Position"],
                Points = (Double)reader["Points"],
                isDeleted = (bool)reader["RaceResultDeletedStatus"],

                Race = new Race
                {
                    RaceID = (Guid)reader["RaceID"],
                    Name = (string)reader["RaceName"],
                    Date = (DateTime)reader["RaceDate"],
                    CircuitID = (Guid)reader["CircuitID"],
                    TypeID = (Guid)reader["TypeID"],
                    isDeleted = (bool)reader["RaceDeletedStatus"]
                },

                Driver = new Driver
                {
                    DriverID = (Guid)reader["DriverID"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    BirthDate = (DateTime)reader["BirthDate"],
                    TeamID = (Guid)reader["TeamID"],
                    InternationalCode = (string)reader["InternationalCode"],
                    isDeleted = (bool)reader["DriverDeletedStatus"]
                }
            };
        }



        public async Task<RaceResultResponseModel> CreateRaceResultAsync(RaceResult raceResult)
        {
            var response = new RaceResultResponseModel 
            {
                RaceResult = null,
                MessageID = 0,
                Message = string.Empty,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateRaceResult",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", raceResult.RaceID);
                    command.Parameters.AddWithValue("@DriverID", raceResult.DriverID);
                    command.Parameters.AddWithValue("@Points", raceResult.Points);
                    command.Parameters.AddWithValue("@Position", raceResult.Position);

                    // Add output parameter
                    SqlParameter MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output};
                    SqlParameter MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            response.RaceResult = MapToRaceResult(reader);
                        }
                    }
                    response.MessageID = (int)MessageIDParam.Value;
                    response.Message = (string)MessageParam.Value;

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

        public async Task<RaceResultResponseModel> GetRaceResultbyIDAsync(Guid raceResultID)
        {
            var response = new RaceResultResponseModel
            {
                RaceResult = null,
                MessageID = 0,
                Message = string.Empty,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetRaceResultbyID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ResultID", raceResultID);

                    // Add output parameter
                    SqlParameter MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.RaceResult = MapToRaceResult(reader);
                        }
                    }
                    response.MessageID = (int)MessageIDParam.Value;
                    response.Message = (string)MessageParam.Value;
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

        public async Task<RaceResultListResponseModel> GetAllRaceResultsAsync()
        {
            var response = new RaceResultListResponseModel
            {
                RaceResults = new List<RaceResult>(),
                MessageID = 0,
                Message = string.Empty,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllRaceResult", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add output parameter
                    SqlParameter MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.RaceResults.Add(MapToRaceResult(reader));
                        }
                    }
                    response.MessageID = (int)MessageIDParam.Value;
                    response.Message = (string)MessageParam.Value;
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

        public async Task<RaceResultResponseModel> UpdateRaceResultAsync(RaceResult raceResult)
        {
            var response = new RaceResultResponseModel
            {
                RaceResult = null,
                MessageID = 0,
                Message = string.Empty,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateRaceResult", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceResultID", raceResult.ResultID);
                    command.Parameters.AddWithValue("@RaceID", raceResult.RaceID);
                    command.Parameters.AddWithValue("@DriverID", raceResult.DriverID);
                    command.Parameters.AddWithValue("@Points", raceResult.Points);
                    command.Parameters.AddWithValue("@Position", raceResult.Position);
                    command.Parameters.AddWithValue("@isDeleted", raceResult.isDeleted);

                    // Add output parameter
                    SqlParameter MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.RaceResult = MapToRaceResult(reader);
                        }
                    }
                    response.MessageID = (int)MessageIDParam.Value;
                    response.Message = (string)MessageParam.Value;
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
