using CarSalesAPI.Interfaces;
using CarSalesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISalesRepository _salesRepository;

        public SalesController(ISalesRepository salesRepository)
        {
            _salesRepository = salesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSales()
        {
            var sales = await _salesRepository.GetAllSalesAsync();
            return Ok(sales);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            var sale = await _salesRepository.GetSalesByIdAsync(id);
            return sale == null ? NotFound() : Ok(sale);
        }

        [HttpPost]
        public async Task<IActionResult> AddSale([FromBody] Sales sale)
        {
            return await _salesRepository.AddSalesAsync(sale) ? Ok("Sale added") : StatusCode(500);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            return await _salesRepository.DeleteSalesAsync(id) ? Ok("Sale deleted") : NotFound();
        }
    }
}
