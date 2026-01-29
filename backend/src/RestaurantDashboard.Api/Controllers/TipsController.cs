using Microsoft.AspNetCore.Mvc;
using RestaurantDashboard.Api.DTOs.Tips;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/tips")]
    public class TipsController : ControllerBase
    {
        private readonly ITipsService _service;
        public TipsController(ITipsService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] Guid? staffId)
            => Ok(await _service.GetAsync(from, to, staffId));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTipDto dto)
        {
            var tip = new Tip { Date = dto.Date, StaffId = dto.StaffId, Amount = dto.Amount};
            var created = await _service.CreateAsync(tip);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // GET /api/tips/summary
        [HttpGet("summary")]
        public async Task<IActionResult> Summary(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] Guid? staffId)
            => Ok(await _service.GetSummaryAsync(from, to, staffId));

        // GET /api/tips/distribution
        [HttpGet("distribution")]
        public async Task<IActionResult> Distribution(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to)
            => Ok(await _service.GetDistributionAsync(from, to));


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateTipDto dto)
        {
            var updated = new Tip { Date = dto.Date, StaffId = dto.StaffId, Amount = dto.Amount};
            var ok = await _service.UpdateAsync(id, updated);
            return ok ? NoContent() : NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
