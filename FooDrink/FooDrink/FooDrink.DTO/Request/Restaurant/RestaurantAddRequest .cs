namespace FooDrink.DTO.Request.Restaurant
{
    public class RestaurantAddRequest
    {
        public string RestaurantName { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Hotline { get; set; } = string.Empty;

    }
}
