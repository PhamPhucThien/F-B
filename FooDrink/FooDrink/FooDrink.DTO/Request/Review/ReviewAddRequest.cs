namespace FooDrink.DTO.Request.Review
{
    public class ReviewAddRequest
    {
        public Guid UserId { get; set; }

        public Guid RestaurantId { get; set; }

        public string Content { get; set; } = string.Empty;

        public int Rating { get; set; }

        public int Reaction { get; set; }
    }
}
