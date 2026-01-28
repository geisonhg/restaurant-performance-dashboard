using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Services.Interfaces
{
    public interface ITipsService
    {
        Task<List<Tip>> GetAsync(DateTime? from, DateTime? to, Guid? staffId);
        Task<Tip?> GetByIdAsync(Guid id);
        Task<Tip> CreateAsync(Tip tip);
        Task<bool> UpdateAsync(Guid id, Tip updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
