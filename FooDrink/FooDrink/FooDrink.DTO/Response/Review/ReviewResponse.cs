namespace FooDrink.DTO.Response.Review
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid RestaurantId { get; set; }

        public string ImageList { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int Rating { get; set; }

        public int Reaction { get; set; }

        public bool Status { get; set; }

        public string CreatedBy { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public string? UpdatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
