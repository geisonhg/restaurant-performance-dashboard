using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Services.Interfaces
{
    public interface ITipRulesService
    {
        Task<List<TipRule>> GetAsync();
        Task<TipRule?> GetActiveAsync();
        Task<TipRule?> GetByIdAsync(Guid id);
        Task<TipRule> CreateAsync(TipRule rule);
        Task<bool> UpdateAsync(Guid id, TipRule updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
