namespace FooDrink.DTO.Request.Restaurant
{
    public class RestaurantGetListRequest
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
    }
}
