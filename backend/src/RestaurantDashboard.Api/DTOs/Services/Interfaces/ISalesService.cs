using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Services.Interfaces
{
    public interface ISalesService
    {
        Task<List<Sale>> GetAsync(DateTime? from, DateTime? to);
        Task<Sale?> GetByIdAsync(Guid id);
        Task<Sale> CreateAsync(Sale sale);
        Task<bool> UpdateAsync(Guid id, Sale updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
