namespace RestaurantDashboard.Api.DTOs.Staff
{
    public class UpdateStaffDto
    {
        public string Name { get; set; } = string.Empty;
        public string Role { get; set; } = "FOH";
        public bool Active { get; set; } = true;
    }
}
