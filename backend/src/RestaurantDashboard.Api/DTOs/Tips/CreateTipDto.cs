using System;

namespace RestaurantDashboard.Api.DTOs.Tips
{
    public class CreateTipDto
    {
        public DateTime Date { get; set; }
        public Guid StaffId { get; set; }
        public decimal Amount { get; set; }
    }
}
