namespace FooDrink.DTO.Response.Menu
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string CategoryList { get; set; } = string.Empty;
        public string ImageList { get; set; } = string.Empty;

        public void Mapping(FooDrink.Database.Models.Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Price = product.Price;
            CategoryList = product.CategoryList;
            ImageList = product.ImageList;
        }
    }
}
