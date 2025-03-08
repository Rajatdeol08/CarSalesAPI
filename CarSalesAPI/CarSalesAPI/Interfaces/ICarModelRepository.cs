using CarSalesAPI.Models;

public interface ICarModelRepository
{
    Task<IEnumerable<CarModel>> GetAllCarModelsAsync();
    Task<CarModel> GetCarModelByIdAsync(int id);
    Task<bool> AddCarModelAsync(CarModel model);
    Task<bool> UpdateCarModelAsync(CarModel model);
    Task<bool> DeleteCarModelAsync(int id);
}
