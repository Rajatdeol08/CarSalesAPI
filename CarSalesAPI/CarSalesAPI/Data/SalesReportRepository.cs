using Microsoft.Data.SqlClient;
using CarSalesAPI.DTOs;

public class SalesReportRepository : ISalesReportRepository
{
    private readonly string _connectionString;

    public SalesReportRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<SalesCommissionDTO>> GetSalesCommissionReportAsync()
    {
        var salesReport = new List<SalesCommissionDTO>();

        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            string query = @"
            SELECT 
                s.Salesman, 
                s.Class, 
                CAST(SUM(s.CarsSold * c.Price) AS DECIMAL(18,2)) AS TotalSales,
                CAST(CASE 
                    WHEN c.Brand = 'Audi' AND c.Price > 25000 THEN 800
                    WHEN c.Brand = 'Jaguar' AND c.Price > 35000 THEN 750
                    WHEN c.Brand = 'Land Rover' AND c.Price > 30000 THEN 850
                    WHEN c.Brand = 'Renault' AND c.Price > 20000 THEN 400
                    ELSE 0
                END AS DECIMAL(18,2)) AS FixedCommission,
                CAST(CASE 
                    WHEN s.Class = 'A' THEN c.Price * 0.08
                    WHEN s.Class = 'B' THEN c.Price * 0.06
                    WHEN s.Class = 'C' THEN c.Price * 0.04
                    ELSE 0
                END AS DECIMAL(18,2)) AS ClassCommission,
                (SELECT sm.LastYearTotalSales FROM Salesman sm WHERE sm.Name = s.Salesman) AS LastYearSales,
                CAST(CASE 
                    WHEN (SELECT sm.LastYearTotalSales FROM Salesman sm WHERE sm.Name = s.Salesman) > 500000 
                    AND s.Class = 'A' THEN c.Price * 0.02
                    ELSE 0
                END AS DECIMAL(18,2)) AS ExtraCommission
            FROM Sales s
            JOIN CarModels c ON s.Brand = c.Brand
            GROUP BY s.Salesman, s.Class, c.Price, c.Brand";

            SqlCommand cmd = new SqlCommand(query, con);
            await con.OpenAsync();
            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                salesReport.Add(new SalesCommissionDTO
                {
                    Salesman = reader.IsDBNull(0) ? "Unknown" : reader.GetString(0),
                    Class = reader.IsDBNull(1) ? "Unknown" : reader.GetString(1),
                    TotalSales = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2),
                    FixedCommission = reader.IsDBNull(3) ? 0 : reader.GetDecimal(3),
                    ClassCommission = reader.IsDBNull(4) ? 0 : reader.GetDecimal(4),
                    ExtraCommission = reader.IsDBNull(5) ? 0 : reader.GetDecimal(5)
                });
            }
        }

        return salesReport;
    }

}
