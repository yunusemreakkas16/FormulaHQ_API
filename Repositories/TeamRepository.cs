using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SqlConnection connection;

        public TeamRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        private Team MapTeamResponse(SqlDataReader reader)
        {
            return new Team
            {
                TeamID = (Guid)reader["TeamID"],
                TeamName = (string)reader["TeamName"], 
                EstablishmentYear = (int)reader["TeamEstablishmentYear"], 
                Country = (string)reader["TeamCountry"],
                isDeleted = (bool)reader["isDeleted"] 
            };
        }

        public async Task<TeamResponseModel> AddTeamAsync(Team team)
        {
            var teamResponseModel = new TeamResponseModel
            {
                MessageId = 0,
                Message = string.Empty,
                Team = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@TeamName", team.TeamName);
                    command.Parameters.AddWithValue("@EstablishmentYear", team.EstablishmentYear);
                    command.Parameters.AddWithValue("@Country", team.Country);

                    // Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int){Direction = ParameterDirection.Output};
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            teamResponseModel.Team = MapTeamResponse(reader);
                        }
                    }
                    teamResponseModel.MessageId = (int)messageIdParam.Value;
                    teamResponseModel.Message = (string)messageParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                teamResponseModel.MessageId = -99;
                teamResponseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                teamResponseModel.MessageId = -100;
                teamResponseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return teamResponseModel;
        }

        public async Task<TeamListResponseModel> GetAllTeamsAsync()
        {
            var teamListResponseModel = new TeamListResponseModel
            {
                MessageId = 0,
                Message = string.Empty,
                Teams = new List<Team>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllTeams", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            teamListResponseModel.Teams.Add(MapTeamResponse(reader));
                        }
                    }

                    teamListResponseModel.MessageId = (int)messageIdParam.Value;
                    teamListResponseModel.Message = (string)messageParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                teamListResponseModel.MessageId = -99;
                teamListResponseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                teamListResponseModel.MessageId = -100;
                teamListResponseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return teamListResponseModel;
        }

        public async Task<TeamResponseModel> GetTeamByIdAsync(Guid teamId)
        {
            var teamResponseModel = new TeamResponseModel
            {
                MessageId = 0,
                Message = string.Empty,
                Team = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetTeamByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamID", teamId);

                    // Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            teamResponseModel.Team = MapTeamResponse(reader);
                        }
                    }
                    teamResponseModel.MessageId = (int)messageIdParam.Value;
                    teamResponseModel.Message = (string)messageParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                teamResponseModel.MessageId = -99;
                teamResponseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                teamResponseModel.MessageId = -100;
                teamResponseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return teamResponseModel;
        }

        public async Task<TeamResponseModel> UpdateTeamAsync(Team team)
        {
            var teamResponseModel = new TeamResponseModel
            {
                MessageId = 0,
                Message = string.Empty,
                Team = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateTeam", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TeamID", team.TeamID);
                    command.Parameters.AddWithValue("@TeamName", team.TeamName);
                    command.Parameters.AddWithValue("@EstablishmentYear", team.EstablishmentYear);
                    command.Parameters.AddWithValue("@Country", team.Country);
                    command.Parameters.AddWithValue("@isDeleted", team.isDeleted);
                    // Output parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            teamResponseModel.Team = MapTeamResponse(reader);
                        }
                    }
                    teamResponseModel.MessageId = (int)messageIdParam.Value;
                    teamResponseModel.Message = (string)messageParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                teamResponseModel.MessageId = -99;
                teamResponseModel.Message = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                teamResponseModel.MessageId = -100;
                teamResponseModel.Message = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return teamResponseModel;
        }
    }
}
