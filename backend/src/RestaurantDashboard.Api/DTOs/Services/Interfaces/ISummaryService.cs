using RestaurantDashboard.Api.DTOs.Summary;

namespace RestaurantDashboard.Api.DTOs.Services.Interfaces
{
    public interface ISummaryService
    {
        Task<WeeklySummaryDto> GetWeeklySummaryAsync(DateTime from, DateTime to);
    }
}
