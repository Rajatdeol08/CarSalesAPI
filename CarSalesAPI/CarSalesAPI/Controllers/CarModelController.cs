using Microsoft.AspNetCore.Mvc;
using CarSalesAPI.Models;

[Route("api/[controller]")]
[ApiController]
public class CarModelController : ControllerBase
{
    private readonly ICarModelRepository _carModelRepository;

    public CarModelController(ICarModelRepository carModelRepository)
    {
        _carModelRepository = carModelRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllModels()
    {
        var models = await _carModelRepository.GetAllCarModelsAsync();
        return Ok(models);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetModelById(int id)
    {
        var model = await _carModelRepository.GetCarModelByIdAsync(id);
        if (model == null) return NotFound("Car model not found.");
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddCarModel([FromBody] CarModel model)
    {
        bool isAdded = await _carModelRepository.AddCarModelAsync(model);
        return isAdded ? Ok("Car model added successfully") : StatusCode(500, "Error adding car model.");
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCarModel([FromBody] CarModel model)
    {
        bool isUpdated = await _carModelRepository.UpdateCarModelAsync(model);
        return isUpdated ? Ok("Car model updated successfully") : NotFound("Car model not found.");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCarModel(int id)
    {
        bool isDeleted = await _carModelRepository.DeleteCarModelAsync(id);
        return isDeleted ? Ok("Car model deleted successfully") : NotFound("Car model not found.");
    }
}
