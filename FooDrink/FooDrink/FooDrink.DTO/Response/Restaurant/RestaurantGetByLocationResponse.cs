namespace FooDrink.DTO.Response.Restaurant
{
    public class RestaurantGetByLocationResponse
    {
        public string Location { get; set; } = string.Empty;
        public List<RestaurantResponse> Data { get; set; } = new List<RestaurantResponse>();
    }
}
