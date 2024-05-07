namespace FooDrink.Database.Models
{
    public class Review : BaseEntity
    {
        public Guid UserId { get; set; }

        public Guid RestaurantId { get; set; }

        public string ImageList { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int Rating { get; set; }
        public User? User { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}
