namespace FooDrink.Database.Models
{
    public class UserReviewReaction
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
        public bool IsLiked { get; set; } = true;

        public User? User { get; set; }
        public Review? Review { get; set; }
    }
}
