using RestaurantDashboard.Domain.Entities;

namespace RestaurantDashboard.Api.Services.Interfaces
{
    public interface IStaffService
    {
        Task<List<Staff>> GetAsync(bool? active);
        Task<Staff?> GetByIdAsync(Guid id);
        Task<Staff> CreateAsync(Staff staff);
        Task<bool> UpdateAsync(Guid id, Staff updated);
        Task<bool> DeleteAsync(Guid id);
    }
}
