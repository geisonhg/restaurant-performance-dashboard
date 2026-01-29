using System;



namespace RestaurantDashboard.Domain.Entities
{
    public class TipRule
    {
        public Guid Id { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public decimal FOHPercent { get; set; }
        public decimal BOHPercent { get; set; }
    }

}
