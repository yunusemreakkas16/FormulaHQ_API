using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class SponsorRepository : ISponsorRepository
    {
        private SqlConnection connection;

        public SponsorRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        public static Sponsor MapToSponsor(SqlDataReader reader)
        {
            return new Sponsor
            {
                SponsorID = (Guid)reader["SponsorID"],
                Name = (string)reader["SponsorName"],
                ContributionAmount = (double)reader["SponsorContribution"],
                isDeleted = (bool)reader["SponsorDeletedStatus"],

                Team = new Team
                {
                    TeamID = (Guid)reader["SponsorTeamID"],
                    TeamName = (string)reader["TeamName"],
                    Country = (string)reader["TeamCountry"],
                    EstablishmentYear = (int)reader["TeamYear"],
                    isDeleted = (bool)reader["TeamDeletedStatus"]
                }
            };
        }

        public async Task<SponsorResponseModel> AddSponsor(Sponsor sponsor)
        {
            var responseModel = new SponsorResponseModel 
            {
                MessageID = 0,
                Message = string.Empty,
                Sponsor = null
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateSponsor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("Name", sponsor.Name);
                    command.Parameters.AddWithValue("@ContributionAmount", sponsor.ContributionAmount);
                    command.Parameters.AddWithValue("@TeamID", sponsor.TeamID);

                    //Output parameter
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            responseModel.Sponsor = MapToSponsor(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIDParam.Value;
                    responseModel.Message = (string)messageParam.Value;
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

        public async Task<SponsorListResponseModel> GetAllSponsors()
        {
            var responseModel = new SponsorListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Sponsors = new List<Sponsor>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllSponsor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    //Output parameter
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            responseModel.Sponsors.Add(MapToSponsor(reader));
                        }
                    }
                    responseModel.MessageID = (int)messageIDParam.Value;
                    responseModel.Message = (string)messageParam.Value;
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

        public async Task<SponsorResponseModel> GetSponsorByID(Guid sponsorID)
        {
            var responseModel = new SponsorResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Sponsor = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetSponsorbyID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SponsorID", sponsorID);

                    //Output parameter
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.Sponsor = MapToSponsor(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIDParam.Value;
                    responseModel.Message = (string)messageParam.Value;
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

        public async Task<SponsorResponseModel> UpdateSponsor(Sponsor sponsor)
        {
            var responseModel = new SponsorResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Sponsor = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateSponsor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SponsorID", sponsor.SponsorID);
                    command.Parameters.AddWithValue("@Name", sponsor.Name);
                    command.Parameters.AddWithValue("@ContributionAmount", sponsor.ContributionAmount);
                    command.Parameters.AddWithValue("@TeamID", sponsor.TeamID);
                    command.Parameters.AddWithValue("@isDeleted", sponsor.isDeleted);

                    //Output parameter

                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 100) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.Sponsor = MapToSponsor(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIDParam.Value;
                    responseModel.Message = (string)messageParam.Value;
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
