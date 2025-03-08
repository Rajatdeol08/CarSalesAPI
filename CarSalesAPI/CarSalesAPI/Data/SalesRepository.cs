using CarSalesAPI.Interfaces;
using CarSalesAPI.Models;
using Microsoft.Data.SqlClient;

namespace CarSalesAPI.Data
{
    public class SalesRepository : ISalesRepository
    {
        private readonly string _connectionString;

        public SalesRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Sales>> GetAllSalesAsync()
        {
            var salesList = new List<Sales>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Sales";
                SqlCommand cmd = new SqlCommand(query, con);
                await con.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    salesList.Add(new Sales
                    {
                        Id = reader.GetInt32(0),
                        Salesman = reader.GetString(1),
                        Class = reader.GetString(2),
                        Brand = reader.GetString(3),
                        CarsSold = reader.GetInt32(4),
                        TotalSaleAmount = reader.GetDecimal(5)
                    });
                }
            }
            return salesList;
        }

        public async Task<Sales> GetSalesByIdAsync(int id)
        {
            Sales sale = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Sales WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                await con.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    sale = new Sales
                    {
                        Id = reader.GetInt32(0),
                        Salesman = reader.GetString(1),
                        Class = reader.GetString(2),
                        Brand = reader.GetString(3),
                        CarsSold = reader.GetInt32(4),
                        TotalSaleAmount = reader.GetDecimal(5)
                    };
                }
            }
            return sale;
        }

        public async Task<bool> AddSalesAsync(Sales sale)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Sales (Salesman, Class, Brand, CarsSold, TotalSaleAmount) VALUES (@Salesman, @Class, @Brand, @CarsSold, @TotalSaleAmount)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Salesman", sale.Salesman);
                cmd.Parameters.AddWithValue("@Class", sale.Class);
                cmd.Parameters.AddWithValue("@Brand", sale.Brand);
                cmd.Parameters.AddWithValue("@CarsSold", sale.CarsSold);
                cmd.Parameters.AddWithValue("@TotalSaleAmount", sale.TotalSaleAmount);

                await con.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> UpdateSalesAsync(Sales sale)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Sales SET Salesman = @Salesman, Class = @Class, Brand = @Brand, CarsSold = @CarsSold, TotalSaleAmount = @TotalSaleAmount WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", sale.Id);
                cmd.Parameters.AddWithValue("@Salesman", sale.Salesman);
                cmd.Parameters.AddWithValue("@Class", sale.Class);
                cmd.Parameters.AddWithValue("@Brand", sale.Brand);
                cmd.Parameters.AddWithValue("@CarsSold", sale.CarsSold);
                cmd.Parameters.AddWithValue("@TotalSaleAmount", sale.TotalSaleAmount);

                await con.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteSalesAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Sales WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                await con.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
