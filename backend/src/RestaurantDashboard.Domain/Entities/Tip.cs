using System;

namespace RestaurantDashboard.Domain.Entities
{
    public class Tip
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid StaffId { get; set; }
        public decimal Amount { get; set; }
        public string? Source { get; set; } // cash/card
    }


}





