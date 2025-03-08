using CarSalesAPI.Models;

namespace CarSalesAPI.Interfaces
{
    public interface ISalesManRepository
    {
        Task<IEnumerable<Salesman>> GetAllSalesmenAsync();
        Task<Salesman> GetSalesmanByIdAsync(int id);
        Task<bool> AddSalesmanAsync(Salesman salesman);
        Task<bool> UpdateSalesmanAsync(Salesman salesman);
        Task<bool> DeleteSalesmanAsync(int id);
    }
}
