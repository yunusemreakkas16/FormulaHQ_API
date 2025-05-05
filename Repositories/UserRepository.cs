using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SqlConnection connection;
        public UserRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        private User MapUserResponse(SqlDataReader reader)
        {
            return new User
            {
                UserID = (Guid)reader["UserID"],
                FirstName = (string)reader["UserName"], 
                LastName = (string)reader["UserSurname"],
                Email = (string)reader["UserEmail"], 
                Password = (string)reader["UserPassword"],
                Role = (string)reader["UserRole"],
                RegistrationDate = (DateTime)reader["UserRegisteredDate"],
                isDeleted = (bool)reader["isDeleted"]
            };
        }

        public async Task<UserResponseModel> CreateUserAsync(User user)
        {
            var responseModel = new UserResponseModel
            {
                MessageId = 0,
                MessageDescription = string.Empty,
                User = null,
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    command.Parameters.AddWithValue("@Role", user.Role);

                    // Output Parameters

                    var messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            responseModel.User = MapUserResponse(reader);
                        }
                    }
                    responseModel.MessageId = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageId = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageId = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async Task<UserListResponseModel> GetAllUsersAsync()
        {
            var responseModel = new UserListResponseModel
            {
                MessageId = 0,
                MessageDescription = string.Empty,
                Users = null,
            };

            try 
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Output Parameters
                    var messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        var users = new List<User>();
                        while (await reader.ReadAsync())
                        {
                            var user = MapUserResponse(reader);
                            users.Add(user);
                        }
                        responseModel.Users = users;
                    }

                    responseModel.MessageId = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageId = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageId = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async Task<UserResponseModel> GetUserByIdAsync(Guid userId)
        {
            var responseModel = new UserResponseModel
            {
                MessageId = 0,
                MessageDescription = string.Empty,
                User = null,
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetUserByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@UserId", userId);

                    // Output Parameters

                    var messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.User = MapUserResponse(reader);
                        }
                    }
                    responseModel.MessageId = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageId = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageId = -100;
                responseModel.MessageDescription = $"Unexpected error: {ex.Message}";
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    await connection.CloseAsync();
            }
            return responseModel;
        }

        public async Task<UserResponseModel> UpdateUserAsync(User user)
        {
            var responseModel = new UserResponseModel
            {
                MessageId = 0,
                MessageDescription = string.Empty,
                User = null,
            };

            try 
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@UserId", user.UserID));
                    command.Parameters.Add(new SqlParameter("@FirstName", user.FirstName));
                    command.Parameters.Add(new SqlParameter("@LastName", user.LastName));
                    command.Parameters.Add(new SqlParameter("@Email", user.Email));
                    command.Parameters.Add(new SqlParameter("@Password", user.Password));
                    command.Parameters.Add(new SqlParameter("@Role", user.Role));
                    command.Parameters.Add(new SqlParameter("@isDeleted", user.isDeleted));

                    // Output Parameters
                    var messageIdParam = new SqlParameter("@MessageId", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    var messageDescriptionParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIdParam);
                    command.Parameters.Add(messageDescriptionParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            responseModel.User = MapUserResponse(reader);
                        }
                    }
                    responseModel.MessageId = (int)messageIdParam.Value;
                    responseModel.MessageDescription = (string)messageDescriptionParam.Value;
                }
            }
            catch (SqlException sqlEx)
            {
                responseModel.MessageId = -99;
                responseModel.MessageDescription = $"Database error: {sqlEx.Message}";
            }
            catch (Exception ex)
            {
                responseModel.MessageId = -100;
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
