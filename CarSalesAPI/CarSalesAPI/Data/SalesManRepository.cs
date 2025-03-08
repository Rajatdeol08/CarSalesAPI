using CarSalesAPI.Interfaces;
using CarSalesAPI.Models;
using Microsoft.Data.SqlClient;

namespace CarSalesAPI.Data
{
    public class SalesManRepository : ISalesManRepository
    {
        private readonly string _connectionString;

        public SalesManRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // Get all salesmen
        public async Task<IEnumerable<Salesman>> GetAllSalesmenAsync()
        {
            var salesmenList = new List<Salesman>();
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salesman";
                SqlCommand cmd = new SqlCommand(query, con);
                await con.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (await reader.ReadAsync())
                {
                    salesmenList.Add(new Salesman
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LastYearTotalSales = reader.GetDecimal(2)
                    });
                }
            }
            return salesmenList;
        }

        // Get salesman by ID
        public async Task<Salesman> GetSalesmanByIdAsync(int id)
        {
            Salesman salesman = null;
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "SELECT * FROM Salesman WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);
                await con.OpenAsync();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (await reader.ReadAsync())
                {
                    salesman = new Salesman
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        LastYearTotalSales = reader.GetDecimal(2)
                    };
                }
            }
            return salesman;
        }

        // Add a new salesman
        public async Task<bool> AddSalesmanAsync(Salesman salesman)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Salesman (Name, LastYearTotalSales) VALUES (@Name, @LastYearTotalSales)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Name", salesman.Name);
                cmd.Parameters.AddWithValue("@LastYearTotalSales", salesman.LastYearTotalSales);

                await con.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        // Update an existing salesman
        public async Task<bool> UpdateSalesmanAsync(Salesman salesman)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "UPDATE Salesman SET Name = @Name, LastYearTotalSales = @LastYearTotalSales WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", salesman.Id);
                cmd.Parameters.AddWithValue("@Name", salesman.Name);
                cmd.Parameters.AddWithValue("@LastYearTotalSales", salesman.LastYearTotalSales);

                await con.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        // Delete a salesman
        public async Task<bool> DeleteSalesmanAsync(int id)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "DELETE FROM Salesman WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", id);

                await con.OpenAsync();
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
