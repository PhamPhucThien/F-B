namespace FooDrink.DTO.Response.Restaurant
{
    public class RestaurantGetListResponse
    {
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Search string
        /// </summary>
        public string SearchString { get; set; } = string.Empty;

        /// <summary>
        /// List of restaurants
        /// </summary>
        public List<RestaurantResponse> Data { get; set; } = new List<RestaurantResponse>();
    }
}
