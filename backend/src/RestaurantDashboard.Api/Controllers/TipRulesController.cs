using Microsoft.AspNetCore.Mvc;
using RestaurantDashboard.Api.DTOs.TipRules;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Controllers
{
    [ApiController]
    [Route("api/tiprules")]
    public class TipRulesController : ControllerBase
    {
        private readonly ITipRulesService _service;
        public TipRulesController(ITipRulesService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAsync());

        [HttpGet("active")]
        public async Task<IActionResult> GetActive()
        {
            var rule = await _service.GetActiveAsync();
            return rule is null ? NotFound() : Ok(rule);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetByIdAsync(id);
            return item is null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTipRuleDto dto)
        {
            var rule = new TipRule
            {
                ValidFrom = dto.ValidFrom,
                FOHPercent = dto.FOHPercent,
                BOHPercent = dto.BOHPercent,
                ValidTo = null
            };


            var created = await _service.CreateAsync(rule);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateTipRuleDto dto)
        {
            var updated = new TipRule
            {
                ValidFrom = dto.ValidFrom,
                ValidTo = dto.ValidTo,
                FOHPercent = dto.FOHPercent,
                BOHPercent = dto.BOHPercent
            };

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
