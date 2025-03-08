using CarSalesAPI.DTOs;

public interface ISalesReportRepository
{
    Task<IEnumerable<SalesCommissionDTO>> GetSalesCommissionReportAsync();
}
