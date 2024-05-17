namespace FooDrink.DTO.Response.Review
{
    public class ReviewGetByUserIdResponse
    {
        public List<ReviewResponse> Data { get; set; } = new List<ReviewResponse>();
    }
}
