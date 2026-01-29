using Microsoft.AspNetCore.Mvc;
using RestaurantDashboard.Api.DTOs.Services.Interfaces;

namespace RestaurantDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService _service;

        public SummaryController(ISummaryService service) => _service = service;

        [HttpGet("weekly")]
        public async Task<IActionResult> Weekly([FromQuery] DateTime from, [FromQuery] DateTime to)
            => Ok(await _service.GetWeeklySummaryAsync(from, to));
    }
}
