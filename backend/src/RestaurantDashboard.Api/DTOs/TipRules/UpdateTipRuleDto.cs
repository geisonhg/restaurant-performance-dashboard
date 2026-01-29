namespace RestaurantDashboard.Api.DTOs.TipRules
{
    public class UpdateTipRuleDto
    {
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal FOHPercent { get; set; }
        public decimal BOHPercent { get; set; }
    }
}
