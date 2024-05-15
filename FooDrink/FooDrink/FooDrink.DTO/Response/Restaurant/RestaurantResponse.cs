namespace FooDrink.DTO.Response.Restaurant
{
    public class RestaurantResponse
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Hotline { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        public List<string> ImageList { get; set; } = new List<string>();
        public string TotalRevenue { get; set; } = string.Empty;
        public string DailyRevenue { get; set; } = string.Empty;
        public string MonthlyRevenue { get; set; } = string.Empty;
        public bool IsRegistration { get; set; }
        public bool Status { get; set; }
    }
}
