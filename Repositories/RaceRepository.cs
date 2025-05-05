using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace FormulaHQ.API.Repositories
{
    public class RaceRepository : IRaceRepository
    {
        private SqlConnection connection;

        public RaceRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        public static Race MapToRace(SqlDataReader reader)
        {
            return new Race
            {
                RaceID = (Guid)reader["RaceID"],
                Name = (string)reader["RaceName"],
                Date = (DateTime)reader["RaceDate"],
                isDeleted = (bool)reader["RaceDeletedStatus"],

                Circuit = new Circuit
                {
                    CircuitID = (Guid)reader["CircuitID"],
                    Name = (string)reader["CircuitName"],
                    Location = (string)reader["Location"],
                    Length = (double)reader["Length"],
                    MaxSpeed = (double)reader["MaxSpeed"],
                    isDeleted = (bool)reader["CircuitDeletedStatus"]
                },

                RaceType = new RaceType
                {
                    TypeID = (Guid)reader["RaceTypeID"],
                    Name = (string)reader["RaceTypeName"],
                    Description = (string)reader["RaceTypeDescription"],
                    isDeleted = (bool)reader["RaceTypeDeletedStatus"]
                }
            };
        }


        public async Task<RaceResponseModel> AddRaceAsync(Race race)
        {
            var response = new RaceResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Race = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateRace", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", race.Name);
                    command.Parameters.AddWithValue("@Date", race.Date);
                    command.Parameters.AddWithValue("@CircuitID", race.CircuitID);
                    command.Parameters.AddWithValue("@TypeID", race.TypeID);

                    //Output parameter
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            response.Race = MapToRace(reader);
                        }
                    }
                    response.MessageID = (int)messageIdParam.Value;
                    response.Message = (string)messageParam.Value;
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

        public async  Task<RaceResponseListModel> GetAllRaceAsync()
        {
            var response = new RaceResponseListModel
            {
                MessageID = 0,
                Message = string.Empty,
                Races = new List<Race>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllRaces",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Output parameter
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while(await reader.ReadAsync())
                        {
                            response.Races.Add(MapToRace(reader));
                        }
                    }
                    response.MessageID = (int)messageIdParam.Value;
                    response.Message = (string)messageParam.Value;
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

        public async Task<RaceResponseModel> GetRaceByIdAsync(Guid raceID)
        {
            var response = new RaceResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Race = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetRacebyID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", raceID);

                    //Output parameter
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Race = MapToRace(reader);
                        }
                    }
                    response.MessageID = (int)messageIdParam.Value;
                    response.Message = (string)messageParam.Value;
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

        public async Task<RaceResponseModel> UpdateRaceAsync(Race race)
        {
            var response = new RaceResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Race = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateRace", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", race.RaceID);
                    command.Parameters.AddWithValue("@Name", race.Name);
                    command.Parameters.AddWithValue("@Date", race.Date);
                    command.Parameters.AddWithValue("@CircuitID", race.CircuitID);
                    command.Parameters.AddWithValue("@TypeID", race.TypeID);
                    command.Parameters.AddWithValue("@isDeleted", race.isDeleted);

                    //Output parameter
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Race = MapToRace(reader);
                        }
                    }
                    response.MessageID = (int)messageIdParam.Value;
                    response.Message = (string)messageParam.Value;
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
