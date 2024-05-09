namespace FooDrink.DTO.Response.Restaurant
{
    public class RestaurantGetListResponse
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
    }
}
