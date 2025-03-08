using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SalesReportController : ControllerBase
{
    private readonly ISalesReportRepository _salesRepository;

    public SalesReportController(ISalesReportRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    [HttpGet("commission-report")]
    public async Task<IActionResult> GetSalesCommissionReport()
    {
        var report = await _salesRepository.GetSalesCommissionReportAsync();
        return Ok(report);
    }
}
