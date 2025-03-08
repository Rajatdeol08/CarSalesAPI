using CarSalesAPI.Models;

namespace CarSalesAPI.Interfaces
{
    public interface ISalesRepository
    {
        Task<IEnumerable<Sales>> GetAllSalesAsync();
        Task<Sales> GetSalesByIdAsync(int id);
        Task<bool> AddSalesAsync(Sales sale);
        Task<bool> UpdateSalesAsync(Sales sale);
        Task<bool> DeleteSalesAsync(int id);
    }
}
