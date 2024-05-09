namespace FooDrink.DTO.Response.Restaurant
{
    public class RestaurantGetByIdResponse
    {
        public List<RestaurantResponse> Data { get; set; } = new List<RestaurantResponse>();
    }
}
