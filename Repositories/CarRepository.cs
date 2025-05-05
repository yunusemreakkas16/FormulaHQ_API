using FormulaHQ.API.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace FormulaHQ.API.Repositories
{
    public class CarRepository : ICarRepository
    {
        private SqlConnection connection;

        public CarRepository(IConfiguration configuration)
        {
            connection = new SqlConnection(configuration.GetConnectionString("FormulaHQConnectionString"));
        }

        public static class CarMapper
        {
            public static Car MapToCar(SqlDataReader reader)
            {
                return new Car
                {
                    CarID = (Guid)reader["CarID"],
                    Model = (string)reader["CarModel"],
                    TechnicalDetails = (string)reader["CarTechnicalInfo"],
                    isDeleted = (bool)reader["CarDeletedStatus"],

                    Team = new Team
                    {
                        TeamID = (Guid)reader["CarTeamID"],
                        TeamName = (string)reader["TeamName"],
                        Country = (string)reader["TeamCountry"],
                        EstablishmentYear = (int)reader["TeamYear"],
                        isDeleted = (bool)reader["TeamDeletedStatus"]
                    },

                    Season = new Season
                    {
                        SeasonID = (Guid)reader["CarSeasonID"],
                        Name = (string)reader["SeasonName"],
                        Year = (int)reader["SeasonYear"]
                    }
                };
            }
        }

        public async Task<CarResponseModel> AddCarAsync(Car car)
        {
            var response = new CarResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Car = null,
            };

            try
            {
                using (SqlCommand command = new SqlCommand("usp_CreateCar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@TeamID", car.TeamID);
                    command.Parameters.AddWithValue("@SeasonID", car.SeasonID);
                    command.Parameters.AddWithValue("@TechnicalDetails", car.TechnicalDetails);

                    // Output Parameters
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int){Direction = ParameterDirection.Output};
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            response.Car = CarMapper.MapToCar(reader);
                        }
                    }
                    response.MessageID = (int)messageIDParam.Value;
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

        public async Task<CarListResponseModel> GetAllCarsAsync()
        {
            var response = new CarListResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Cars = new List<Car>()
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetAllCar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    // Output Parameters
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            response.Cars.Add(CarMapper.MapToCar(reader));
                        }
                    }
                    response.MessageID = (int)messageIDParam.Value;
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

        public async Task<CarResponseModel> GetCarByIDAsync(Guid carID)
        {
            var response = new CarResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Car = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_GetCarByID", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarID", carID);
                    // Output Parameters
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Car = CarMapper.MapToCar(reader);
                        }
                    }
                    response.MessageID = (int)messageIDParam.Value;
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

        public async Task<CarResponseModel> UpdateCarAsync(Car car)
        {
            var response = new CarResponseModel
            {
                MessageID = 0,
                Message = string.Empty,
                Car = null
            };
            try
            {
                using (SqlCommand command = new SqlCommand("usp_UpdateCar", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CarID", car.CarID);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@TeamID", car.TeamID);
                    command.Parameters.AddWithValue("@SeasonID", car.SeasonID);
                    command.Parameters.AddWithValue("@TechnicalDetails", car.TechnicalDetails);
                    command.Parameters.AddWithValue("@isDeleted", car.isDeleted);

                    // Output Parameters
                    SqlParameter messageIDParam = new SqlParameter("@MessageID", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    SqlParameter messageParam = new SqlParameter("@MessageDescription", SqlDbType.NVarChar, 255) { Direction = ParameterDirection.Output };

                    command.Parameters.Add(messageIDParam);
                    command.Parameters.Add(messageParam);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            response.Car = CarMapper.MapToCar(reader);
                        }
                    }
                    response.MessageID = (int)messageIDParam.Value;
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
