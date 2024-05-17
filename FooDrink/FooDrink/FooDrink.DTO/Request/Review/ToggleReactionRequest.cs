namespace FooDrink.DTO.Request.Review
{
    public class ToggleReactionRequest
    {
        public Guid UserId { get; set; }
        public Guid ReviewId { get; set; }
    }
}
