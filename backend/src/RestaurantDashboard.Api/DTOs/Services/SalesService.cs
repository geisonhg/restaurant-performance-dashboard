using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Domain.Entities;
using RestaurantDashboard.Infrastructure.Data;
using RestaurantDashboard.Api.Services.Interfaces;

namespace RestaurantDashboard.Api.Services
{
    public class SalesService : ISalesService
    {
        private readonly DashboardDbContext _db;

        public SalesService(DashboardDbContext db) => _db = db;

        public async Task<List<Sale>> GetAsync(DateTime? from, DateTime? to)
        {
            var q = _db.Sales.AsQueryable();

            if (from.HasValue) q = q.Where(s => s.Date >= from.Value);
            if (to.HasValue) q = q.Where(s => s.Date <= to.Value);

            return await q.OrderByDescending(s => s.Date).ToListAsync();
        }

        public Task<Sale?> GetByIdAsync(Guid id) =>
            _db.Sales.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Sale> CreateAsync(Sale sale)
        {
            sale.Id = Guid.NewGuid();
            _db.Sales.Add(sale);
            await _db.SaveChangesAsync();
            return sale;
        }

        public async Task<bool> UpdateAsync(Guid id, Sale updated)
        {
            var existing = await _db.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            existing.Date = updated.Date;
            existing.Section = updated.Section;
            existing.Covers = updated.Covers;
            existing.NetAmount = updated.NetAmount;
            existing.Tax = updated.Tax;
            existing.ServiceType = updated.ServiceType;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existing = await _db.Sales.FirstOrDefaultAsync(x => x.Id == id);
            if (existing is null) return false;

            _db.Sales.Remove(existing);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
