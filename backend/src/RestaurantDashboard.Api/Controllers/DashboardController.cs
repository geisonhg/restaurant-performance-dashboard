using Microsoft.AspNetCore.Mvc;
using RestaurantDashboard.Api.DTOs.Services.Interfaces;

namespace RestaurantDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }

        // GET: /api/dashboard/summary?from=2026-01-01&to=2026-01-31
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromQuery] DateTime? from, [FromQuery] DateTime? to)
            => Ok(await _service.GetSummaryAsync(from, to));

        // GET: /api/dashboard/daily?from=2026-01-01&to=2026-01-31
        [HttpGet("daily")]
        public async Task<IActionResult> GetDaily([FromQuery] DateTime? from, [FromQuery] DateTime? to)
            => Ok(await _service.GetDailyAsync(from, to));
    }
}
