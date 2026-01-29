namespace RestaurantDashboard.Api.DTOs.Dashboard
{
    public class DashboardSummaryDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }

        public decimal TotalSales { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalTips { get; set; }

        // Simple profit definition: Sales - Expenses (tips are tracked separately)
        public decimal NetProfit { get; set; }

        // Tips / Sales * 100 (0 if sales is 0)
        public decimal TipsPercentOfSales { get; set; }
    }
}
