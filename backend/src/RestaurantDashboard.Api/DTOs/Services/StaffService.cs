using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;
using RestaurantDashboard.Infrastructure.Data;

namespace RestaurantDashboard.Api.Services
{
    public class StaffService : IStaffService
    {
        private readonly DashboardDbContext _db;
        public StaffService(DashboardDbContext db) => _db = db;

        public async Task<List<Staff>> GetAsync(bool? active)
        {
            var q = _db.Staff.AsQueryable();
            if (active.HasValue) q = q.Where(s => s.Active == active.Value);
            return await q.OrderBy(s => s.Name).ToListAsync();
        }

        public Task<Staff?> GetByIdAsync(Guid id) =>
            _db.Staff.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Staff> CreateAsync(Staff staff)
        {
            staff.Id = Guid.NewGuid();
            _db.Staff.Add(staff);
            await _db.SaveChangesAsync();
            return staff;
        }

        public async Task<bool> UpdateAsync(Guid id, Staff updated)
        {
            var existing = await _db.Staff.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            existing.Name = updated.Name;
            existing.Role = updated.Role;
            existing.Active = updated.Active;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _db.Staff.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            _db.Staff.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
