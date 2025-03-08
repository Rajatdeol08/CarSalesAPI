using Microsoft.Data.SqlClient;
using CarSalesAPI.Models;

public class CarModelRepository : ICarModelRepository
{
    private readonly string _connectionString;

    public CarModelRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    // Get all car models
    public async Task<IEnumerable<CarModel>> GetAllCarModelsAsync()
    {
        var carModels = new List<CarModel>();
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM CarModels ORDER BY DateOfManufacturing DESC, SortOrder";
            SqlCommand cmd = new SqlCommand(query, con);
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                carModels.Add(new CarModel
                {
                    Id = reader.GetInt32(0),
                    Brand = reader.GetString(1),
                    Class = reader.GetString(2),
                    ModelName = reader.GetString(3),
                    ModelCode = reader.GetString(4),
                    Description = reader.GetString(5),
                    Features = reader.GetString(6),
                    Price = reader.GetDecimal(7),
                    DateOfManufacturing = reader.GetDateTime(8),
                    Active = reader.GetBoolean(9),
                    SortOrder = reader.GetInt32(10),
                    CreatedAt = reader.GetDateTime(11)
                });
            }
        }
        return carModels;
    }

    // Get car model by ID
    public async Task<CarModel> GetCarModelByIdAsync(int id)
    {
        CarModel model = null;
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "SELECT * FROM CarModels WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new CarModel
                {
                    Id = reader.GetInt32(0),
                    Brand = reader.GetString(1),
                    Class = reader.GetString(2),
                    ModelName = reader.GetString(3),
                    ModelCode = reader.GetString(4),
                    Description = reader.GetString(5),
                    Features = reader.GetString(6),
                    Price = reader.GetDecimal(7),
                    DateOfManufacturing = reader.GetDateTime(8),
                    Active = reader.GetBoolean(9),
                    SortOrder = reader.GetInt32(10),
                    CreatedAt = reader.GetDateTime(11)
                };
            }
        }
        return model;
    }

    // Add new car model
    public async Task<bool> AddCarModelAsync(CarModel model)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"INSERT INTO CarModels 
                            (Brand, Class, ModelName, ModelCode, Description, Features, Price, DateOfManufacturing, Active, SortOrder) 
                            VALUES (@Brand, @Class, @ModelName, @ModelCode, @Description, @Features, @Price, @DateOfManufacturing, @Active, @SortOrder)";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Brand", model.Brand);
            cmd.Parameters.AddWithValue("@Class", model.Class);
            cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
            cmd.Parameters.AddWithValue("@ModelCode", model.ModelCode);
            cmd.Parameters.AddWithValue("@Description", model.Description);
            cmd.Parameters.AddWithValue("@Features", model.Features);
            cmd.Parameters.AddWithValue("@Price", model.Price);
            cmd.Parameters.AddWithValue("@DateOfManufacturing", model.DateOfManufacturing);
            cmd.Parameters.AddWithValue("@Active", model.Active);
            cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);

            await con.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }

    // Update existing car model
    public async Task<bool> UpdateCarModelAsync(CarModel model)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"UPDATE CarModels 
                            SET Brand = @Brand, Class = @Class, ModelName = @ModelName, ModelCode = @ModelCode, 
                                Description = @Description, Features = @Features, Price = @Price, 
                                DateOfManufacturing = @DateOfManufacturing, Active = @Active, SortOrder = @SortOrder
                            WHERE Id = @Id";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", model.Id);
            cmd.Parameters.AddWithValue("@Brand", model.Brand);
            cmd.Parameters.AddWithValue("@Class", model.Class);
            cmd.Parameters.AddWithValue("@ModelName", model.ModelName);
            cmd.Parameters.AddWithValue("@ModelCode", model.ModelCode);
            cmd.Parameters.AddWithValue("@Description", model.Description);
            cmd.Parameters.AddWithValue("@Features", model.Features);
            cmd.Parameters.AddWithValue("@Price", model.Price);
            cmd.Parameters.AddWithValue("@DateOfManufacturing", model.DateOfManufacturing);
            cmd.Parameters.AddWithValue("@Active", model.Active);
            cmd.Parameters.AddWithValue("@SortOrder", model.SortOrder);

            await con.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }

    // Delete car model by ID
    public async Task<bool> DeleteCarModelAsync(int id)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = "DELETE FROM CarModels WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id", id);

            await con.OpenAsync();
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
    }
}
