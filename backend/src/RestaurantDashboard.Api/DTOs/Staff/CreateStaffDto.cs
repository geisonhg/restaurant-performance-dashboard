namespace RestaurantDashboard.Api.DTOs.Staff
{
    public class CreateStaffDto
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "FOH"; // FOH/BOH
        public bool Active { get; set; } = true;
    }
}
