using RestaurantDashboard.Api.DTOs.Dashboard;

namespace RestaurantDashboard.Api.DTOs.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardSummaryDto> GetSummaryAsync(DateTime? from, DateTime? to);
        Task<List<DashboardDailyDto>> GetDailyAsync(DateTime? from, DateTime? to);
    }
}
