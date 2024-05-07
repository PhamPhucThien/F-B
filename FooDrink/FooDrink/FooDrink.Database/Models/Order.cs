namespace FooDrink.Database.Models
{
    public class Order : BaseEntity
    {
        public string Details { get; set; } = string.Empty;
        public string TotalePrice { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public Restaurant? Restaurant { get; set; }
    }
}
