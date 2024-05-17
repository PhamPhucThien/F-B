namespace FooDrink.DTO.Request.Review
{
    public class ReviewGetListRequest : IPagingRequest
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public string SearchString { get; set; } = string.Empty;
    }
}
