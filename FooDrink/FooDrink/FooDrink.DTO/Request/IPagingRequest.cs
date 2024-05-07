namespace FooDrink.DTO.Request
{
    public interface IPagingRequest
    {
        /// <summary>
        /// Page Size
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Page Index
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// Search string
        /// </summary>
        public string SearchString { get; set; }

    }
}
