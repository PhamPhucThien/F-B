namespace FooDrink.Database.Models
{
    public class Menu : BaseEntity
    {
        public string Description { get; set; } = string.Empty;
        public Guid RestaurantId { get; set; }
        public Restaurant Restaurant { get; set; } = new Restaurant();
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
