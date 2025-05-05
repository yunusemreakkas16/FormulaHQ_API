using Azure;
using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class DriverRepository : IDriverRepository
    {
        private SqlConnection connection;

        public DriverRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        public static class DriverMapper
        {
            public static Driver MapToDriver(SqlDataReader reader)
            {
                return new Driver
                {
                    DriverID = (Guid)reader["DriverID"],
                    FirstName = (string)reader["DriverFirstName"],
                    LastName = (string)reader["DriverLastName"],
                    BirthDate = (DateTime)reader["BirthDate"],
                    InternationalCode = (string)reader["DriverCode"],
                    TeamID = (Guid)reader["DriverTeamID"],
                    isDeleted = (bool)reader["DriverDeletedStatus"],
                    Team = new Team
                    {
                        TeamID = (Guid)reader["TeamUniqueID"],
                        TeamName = (string)reader["TeamTitle"],
                        EstablishmentYear = (int)reader["EstablishmentYear"],
                        Country = (string)reader["TeamCountry"],
                        isDeleted = (bool)reader["TeamDeletedStatus"],
                    }
                };
            }
        }

        public async Task<DriverResponseModel> AddDriverAsync(Driver driver)
        {
            var responseModel = new DriverResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Driver = null,
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateDriver", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", driver.FirstName);
                    command.Parameters.AddWithValue("@LastName", driver.LastName);
                    command.Parameters.AddWithValue("@BirthDate", driver.BirthDate.Date);
                    command.Parameters.AddWithValue("@InternationalCode", driver.InternationalCode);
                    command.Parameters.AddWithValue("@TeamID", driver.TeamID);

                    // Output Parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", System.Data.SqlDbType.Int){Direction = System.Data.ParameterDirection.Output};
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", System.Data.SqlDbType.NVarChar, 500) { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            responseModel.Driver = DriverMapper.MapToDriver(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
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

        public async  Task<DriverListResponseModel> GetAllDriversAsync()
        {
            var responseModel = new DriverListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Drivers = new List<Driver>(),
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllDrivers", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    // Output Parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", System.Data.SqlDbType.NVarChar, 500) { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            responseModel.Drivers.Add(DriverMapper.MapToDriver(reader));
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
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

        public async Task<DriverResponseModel> GetDriverByIdAsync(Guid driverId)
        {
            var responseModel = new DriverResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Driver = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetDriverById", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", driverId);

                    // Output Parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", System.Data.SqlDbType.NVarChar, 500) { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.Driver = DriverMapper.MapToDriver(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
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

        public async Task<DriverResponseModel> UpdateDriverAsync(Driver driver)
        {
            var responseModel = new DriverResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Driver = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateDriver", connection))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@DriverID", driver.DriverID);
                    command.Parameters.AddWithValue("@FirstName", driver.FirstName);
                    command.Parameters.AddWithValue("@LastName", driver.LastName);
                    command.Parameters.AddWithValue("@BirthDate", driver.BirthDate.Date);
                    command.Parameters.AddWithValue("@InternationalCode", driver.InternationalCode);
                    command.Parameters.AddWithValue("@TeamID", driver.TeamID);
                    command.Parameters.AddWithValue("@isDeleted", driver.isDeleted);

                    // Output Parameters
                    SqlParameter messageIdParam = new SqlParameter("@MessageID", System.Data.SqlDbType.Int) { Direction = System.Data.ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", System.Data.SqlDbType.NVarChar, 500) { Direction = System.Data.ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.Driver = DriverMapper.MapToDriver(reader);
                        }
                    }
                    responseModel.MessageID = (int)messageIdParam.Value;
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
