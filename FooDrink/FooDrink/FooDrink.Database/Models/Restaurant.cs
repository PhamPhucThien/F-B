namespace FooDrink.Database.Models
{
    public class Restaurant : BaseEntity
    {
        public string RestaurantName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Hotline { get; set; } = string.Empty;
        public float AverageRating { get; set; }
        public string ImageList { get; set; } = string.Empty;
        public string TotalRevenue { get; set; } = string.Empty;
        public string DailyRevenue { get; set; } = string.Empty;
        public string MonthlyRevenue { get; set; } = string.Empty;
        public ICollection<User>? Users { get; set; }
        public Menu? Menu { get; set; }
        public ICollection<Review>? Reviews { get; set; }
        public ICollection<Order>? Orders { get; set;}
    }
}
