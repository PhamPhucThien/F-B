namespace FooDrink.Database.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
