namespace RestaurantDashboard.Api.DTOs.Tips
{
    public class TipsDistributionDto
    {
        public decimal TotalTips { get; set; }

        public decimal FOHPercent { get; set; }
        public decimal BOHPercent { get; set; }

        public decimal FOHAmount { get; set; }
        public decimal BOHAmount { get; set; }
    }
}
