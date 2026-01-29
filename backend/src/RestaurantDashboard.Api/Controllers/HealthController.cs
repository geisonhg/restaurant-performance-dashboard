using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Infrastructure.Data;

namespace RestaurantDashboard.Api.Controllers;

[ApiController]
[Route("health")]
public class HealthController : ControllerBase
{
    private readonly DashboardDbContext _db;

    public HealthController(DashboardDbContext db) => _db = db;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            // Prueba DB rápida
            var canConnect = await _db.Database.CanConnectAsync();
            return Ok(new
            {
                status = "ok",
                db = canConnect ? "ok" : "down",
                timeUtc = DateTime.UtcNow
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new
            {
                status = "error",
                db = "error",
                message = ex.Message
            });
        }
    }
}
