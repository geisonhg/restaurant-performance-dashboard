using Microsoft.AspNetCore.Mvc;
using RestaurantDashboard.Api.DTOs.Sales;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISalesService _service;

        public SalesController(ISalesService service) => _service = service;

        // GET /api/sales?from=2025-01-01&to=2025-01-31&serviceType=dine-in&section=Floor
        [HttpGet]
        public async Task<IActionResult> Get(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? serviceType,
            [FromQuery] string? section)
            => Ok(await _service.GetAsync(from, to, serviceType, section));

        // GET /api/sales/summary
        [HttpGet("summary")]
        public async Task<IActionResult> Summary(
            [FromQuery] DateTime? from,
            [FromQuery] DateTime? to,
            [FromQuery] string? serviceType,
            [FromQuery] string? section)
            => Ok(await _service.GetSummaryAsync(from, to, serviceType, section));


        // GET /api/sales/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        // POST /api/sales
        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleDto dto)
        {
            var sale = new Sale
            {
                Date = dto.Date,
                Section = dto.Section,
                Covers = dto.Covers,
                Amount = dto.NetAmount,
                Tax = dto.Tax,
                ServiceType = dto.ServiceType
            };

            var created = await _service.CreateAsync(sale);

            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT /api/sales/{id}
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateSaleDto dto)
        {
            var updated = new Sale
            {
                Date = dto.Date,
                Section = dto.Section,
                Covers = dto.Covers,
                Amount = dto.NetAmount,
                Tax = dto.Tax,
                ServiceType = dto.ServiceType
            };

            var ok = await _service.UpdateAsync(id, updated);
            return ok ? NoContent() : NotFound();
        }

        // DELETE /api/sales/{id}
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var ok = await _service.DeleteAsync(id);
            return ok ? NoContent() : NotFound();
        }
    }
}
