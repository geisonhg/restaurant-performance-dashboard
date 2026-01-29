namespace RestaurantDashboard.Api.DTOs.Tips
{
    public class TipsSummaryDto
    {
        public int Count { get; set; }
        public decimal TotalTips { get; set; }
        public decimal AvgTip { get; set; }
    }
}
