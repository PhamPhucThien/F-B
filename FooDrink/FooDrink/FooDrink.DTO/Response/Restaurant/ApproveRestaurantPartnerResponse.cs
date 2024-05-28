namespace FooDrink.DTO.Response.Restaurant
{
    public class ApproveRestaurantPartnerResponse
    {

        public List<RestaurantResponse> Data { get; set; } = new List<RestaurantResponse>();
        public string Message { get; set; } = string.Empty;
    }
}
