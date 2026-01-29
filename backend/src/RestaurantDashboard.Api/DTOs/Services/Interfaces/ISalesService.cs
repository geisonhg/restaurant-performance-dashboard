using RestaurantDashboard.Api.DTOs.Sales;
using RestaurantDashboard.Domain.Entities;

public interface ISalesService
{
    Task<List<Sale>> GetAsync(
        DateTime? from,
        DateTime? to,
        string? serviceType,
        string? section
    );

    Task<Sale?> GetByIdAsync(Guid id);
    Task<Sale> CreateAsync(Sale sale);
    Task<bool> UpdateAsync(Guid id, Sale sale);
    Task<bool> DeleteAsync(Guid id);

    Task<SalesSummaryDto> GetSummaryAsync(
    DateTime? from,
    DateTime? to,
    string? serviceType,
    string? section
);
}
