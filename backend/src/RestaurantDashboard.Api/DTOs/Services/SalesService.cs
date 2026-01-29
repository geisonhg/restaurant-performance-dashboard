using Microsoft.EntityFrameworkCore;
using RestaurantDashboard.Api.DTOs.Sales;
using RestaurantDashboard.Api.Services.Interfaces;
using RestaurantDashboard.Domain.Entities;
using RestaurantDashboard.Infrastructure.Data;

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
            existing.Amount = updated.Amount;
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

        public async Task<List<Sale>> GetAsync(
            DateTime? from,
            DateTime? to,
            string? serviceType,
            string? section)
        {
            var query = _db.Sales.AsQueryable();

            if (from.HasValue)
                query = query.Where(s => s.Date >= from.Value);

            if (to.HasValue)
                query = query.Where(s => s.Date <= to.Value);

            if (!string.IsNullOrEmpty(serviceType))
                query = query.Where(s => s.ServiceType == serviceType);

            if (!string.IsNullOrEmpty(section))
                query = query.Where(s => s.Section == section);

            return await query
                .OrderByDescending(s => s.Date)
                .ToListAsync();
        }

        public async Task<SalesSummaryDto> GetSummaryAsync(
    DateTime? from,
    DateTime? to,
    string? serviceType,
    string? section)
        {
            var query = _db.Sales.AsQueryable();

            if (from.HasValue) query = query.Where(s => s.Date >= from.Value);
            if (to.HasValue) query = query.Where(s => s.Date <= to.Value);
            if (!string.IsNullOrEmpty(serviceType)) query = query.Where(s => s.ServiceType == serviceType);
            if (!string.IsNullOrEmpty(section)) query = query.Where(s => s.Section == section);

            var count = await query.CountAsync();
            var totalCovers = await query.SumAsync(s => (int?)s.Covers) ?? 0;
            var totalNet = await query.SumAsync(s => (decimal?)s.Amount) ?? 0m;
            var totalTax = await query.SumAsync(s => (decimal?)s.Tax) ?? 0m;

            var avgNetPerCover = totalCovers == 0 ? 0m : totalNet / totalCovers;

            return new SalesSummaryDto
            {
                Count = count,
                TotalCovers = totalCovers,
                TotalNet = totalNet,
                TotalTax = totalTax,
                TotalGross = totalNet + totalTax,
                AvgNetPerCover = avgNetPerCover
            };
        }

    }
}
