namespace FooDrink.DTO.Request.Product
{
    public class ProductGetListRequest : IPagingRequest
    {
        /// <summary>
        /// Page size
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; } = 1;

        public string SearchString { get; set; } = string.Empty;

    }
}