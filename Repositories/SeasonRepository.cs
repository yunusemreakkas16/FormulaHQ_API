using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private SqlConnection connection;

        public SeasonRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        private Season MapSeasonResponse(SqlDataReader reader)
        {
            return new Season
            {
                SeasonID = (Guid)reader["SeasonId"],
                Name = (string)reader["SeasonName"],
                Year = (int)reader["SeasonYear"]
            };
        }

        public async Task<SeasonResponseModel> AddSeasonAsync(Season season)
        {
            var response = new SeasonResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Season = null,
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateSeason", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", season.Name);
                    command.Parameters.AddWithValue("@Year", season.Year);

                    // Add output parameter
                    var MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    var MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Season = MapSeasonResponse(reader);
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

        public async Task<SeasonListResponseModel> GetAllSeasonsAsync()
        {
            var response = new SeasonListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Seasons = new List<Season>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllSeason", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Output Parameter

                    var MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Seasons.Add(MapSeasonResponse(reader));
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

        public async Task<SeasonResponseModel> GetSeasonByIdAsync(Guid seasonId)
        {
            var response = new SeasonResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Season = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetSeasonByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SeasonID", seasonId);

                    // Output Parameter
                    var MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Season = MapSeasonResponse(reader);
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

        public async Task<SeasonResponseModel> UpdateSeasonAsync(Season season)
        {
            var response = new SeasonResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Season = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateSeason", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SeasonID", season.SeasonID);
                    command.Parameters.AddWithValue("@Name", season.Name);
                    command.Parameters.AddWithValue("@Year", season.Year);

                    // Output Parameter
                    var MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Season = MapSeasonResponse(reader);
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

        public async Task<SeasonResponseModel> DeleteSeasonAsync(Guid seasonId)
        {
            var response = new SeasonResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Season = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_DeleteSeason", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SeasonID", seasonId);

                    // Output Parameter
                    var MessageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParam);
                    command.Parameters.Add(MessageParam);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
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
