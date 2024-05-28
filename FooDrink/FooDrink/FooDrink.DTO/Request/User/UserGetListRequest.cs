namespace FooDrink.DTO.Request.User
{
    public class UserGetListRequest
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
