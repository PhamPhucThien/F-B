namespace FooDrink.Database.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string CategoryList { get; set; } = string.Empty;
        public string ImageList { get; set; } = string.Empty;
        public Guid MenuId { get; set; }
        public Menu? Menu { get; set; }
    }
}
