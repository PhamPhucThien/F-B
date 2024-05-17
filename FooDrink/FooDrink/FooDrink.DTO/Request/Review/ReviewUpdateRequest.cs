namespace FooDrink.DTO.Request.Review
{
    public class ReviewUpdateRequest
    {
        public Guid Id { get; set; }

        public string Content { get; set; } = string.Empty;

        public int Rating { get; set; }

        public int Reaction { get; set; }
    }
}
