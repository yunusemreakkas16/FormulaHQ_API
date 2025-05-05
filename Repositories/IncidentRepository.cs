using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class IncidentRepository : IIncidentRepository
    {
        private SqlConnection connection;

        public IncidentRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        public static Incident MapToIncident(SqlDataReader reader)
        {
            return new Incident
            {
                IncidentID = (Guid)reader["IncidentID"],
                RaceID = (Guid)reader["RaceID"],
                DriverID = (Guid)reader["DriverID"],
                Type = (string)reader["IncidentType"],
                Description = (string)reader["IncidentDescription"],
                Time = (DateTime)reader["IncidentTime"],
                isDeleted = (bool)reader["IncidentDeletedStatus"],

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



        public async Task<IncidentResponseModel> AddIncidentAsync(Incident incident)
        {
            var response = new IncidentResponseModel 
            {
                MessageID = 0,
                Message = string.Empty,
                Incident = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateIncident", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@RaceID", incident.RaceID);
                    command.Parameters.AddWithValue("@DriverID", incident.DriverID);
                    command.Parameters.AddWithValue("@Type", incident.Type);
                    command.Parameters.AddWithValue("@Description", incident.Description);
                    command.Parameters.AddWithValue("@Timestamp", incident.Time);

                    // Output parameter
                    var MessageIDParameter = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    var MessageParameter = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParameter);
                    command.Parameters.Add(MessageParameter);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            response.Incident = MapToIncident(reader);
                        }
                    }
                    response.MessageID = (int)MessageIDParameter.Value;
                    response.Message = (string)MessageParameter.Value;
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

        public async Task<IncidentListResponseModel> GetAllIncidentsAsync()
        {
            var response = new IncidentListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Incidents = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllIncidents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Output parameter
                    var MessageIDParameter = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParameter = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParameter);
                    command.Parameters.Add(MessageParameter);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var incidents = new List<Incident>();
                        while (await reader.ReadAsync())
                        {
                            incidents.Add(MapToIncident(reader));
                        }
                        response.Incidents = incidents;
                    }
                    response.MessageID = (int)MessageIDParameter.Value;
                    response.Message = (string)MessageParameter.Value;
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

        public async Task<IncidentResponseModel> GetIncidentByIDAsync(IncidentParamModel incidentParamModel)
        {
            var response = new IncidentResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Incident = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetIncidentByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IncidentID", incidentParamModel.IncidentID);

                    // Output parameter
                    var MessageIDParameter = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParameter = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParameter);
                    command.Parameters.Add(MessageParameter);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Incident = MapToIncident(reader);
                        }
                    }
                    response.MessageID = (int)MessageIDParameter.Value;
                    response.Message = (string)MessageParameter.Value;
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

        public async Task<IncidentResponseModel> UpdateIncidentAsync(Incident incident)
        {
            var response = new IncidentResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Incident = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateIncident", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@IncidentID", incident.IncidentID);
                    command.Parameters.AddWithValue("@RaceID", incident.RaceID);
                    command.Parameters.AddWithValue("@DriverID", incident.DriverID);
                    command.Parameters.AddWithValue("@Type", incident.Type);
                    command.Parameters.AddWithValue("@Description", incident.Description);
                    command.Parameters.AddWithValue("@Timestamp", incident.Time);
                    command.Parameters.AddWithValue("@isDeleted", incident.isDeleted);

                    // Output parameter
                    var MessageIDParameter = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var MessageParameter = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(MessageIDParameter);
                    command.Parameters.Add(MessageParameter);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Incident = MapToIncident(reader);
                        }
                    }
                    response.MessageID = (int)MessageIDParameter.Value;
                    response.Message = (string)MessageParameter.Value;
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
