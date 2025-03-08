using CarSalesAPI.Interfaces;
using CarSalesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesManController : Controller
    {
        private readonly ISalesManRepository _salesmanRepository;

        public SalesManController(ISalesManRepository salesmanRepository)
        {
            _salesmanRepository = salesmanRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSalesmen()
        {
            var salesmen = await _salesmanRepository.GetAllSalesmenAsync();
            return Ok(salesmen);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSalesmanById(int id)
        {
            var salesman = await _salesmanRepository.GetSalesmanByIdAsync(id);
            return salesman == null ? NotFound() : Ok(salesman);
        }

        [HttpPost]
        public async Task<IActionResult> AddSalesman([FromBody] Salesman salesman)
        {
            return await _salesmanRepository.AddSalesmanAsync(salesman) ? Ok("Salesman added") : StatusCode(500);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSalesman([FromBody] Salesman salesman)
        {
            return await _salesmanRepository.UpdateSalesmanAsync(salesman) ? Ok("Salesman updated") : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalesman(int id)
        {
            return await _salesmanRepository.DeleteSalesmanAsync(id) ? Ok("Salesman deleted") : NotFound();
        }
    }
}
