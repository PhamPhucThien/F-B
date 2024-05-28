namespace FooDrink.DTO.Request.Restaurant
{
    public class RestaurantGetByLocationRequest
    {
        public string Latitude { get; set; } = string.Empty;
        public string Longitude { get; set; } = string.Empty;
    }
}
