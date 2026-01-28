using Microsoft.AspNetCore.Mvc;
using RestaurantDashboard.Api.DTOs.Staff;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/staff")]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _service;
        public StaffController(IStaffService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] bool? active)
            => Ok(await _service.GetAsync(active));

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStaffDto dto)
        {
            var staff = new Staff { Name = dto.Name, Role = dto.Role, Active = dto.Active };
            var created = await _service.CreateAsync(staff);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateStaffDto dto)
        {
            var updated = new Staff { Name = dto.Name, Role = dto.Role, Active = dto.Active };
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
