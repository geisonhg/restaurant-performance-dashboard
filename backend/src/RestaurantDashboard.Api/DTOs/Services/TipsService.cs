using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;
using RestaurantDashboard.Infrastructure.Data;

namespace RestaurantDashboard.Api.Services
{
    public class TipsService : ITipsService
    {
        private readonly DashboardDbContext _db;
        public TipsService(DashboardDbContext db) => _db = db;

        public async Task<List<Tip>> GetAsync(DateTime? from, DateTime? to, Guid? staffId)
        {
            var q = _db.Tips.AsQueryable();

            if (from.HasValue) q = q.Where(t => t.Date >= from.Value);
            if (to.HasValue) q = q.Where(t => t.Date <= to.Value);
            if (staffId.HasValue) q = q.Where(t => t.StaffId == staffId.Value);

            return await q.OrderByDescending(t => t.Date).ToListAsync();
        }

        public Task<Tip?> GetByIdAsync(Guid id) =>
            _db.Tips.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Tip> CreateAsync(Tip tip)
        {
            tip.Id = Guid.NewGuid();
            _db.Tips.Add(tip);
            await _db.SaveChangesAsync();
            return tip;
        }

        public async Task<bool> UpdateAsync(Guid id, Tip updated)
        {
            var existing = await _db.Tips.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            existing.Date = updated.Date;
            existing.StaffId = updated.StaffId;
            existing.Amount = updated.Amount;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _db.Tips.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            _db.Tips.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
