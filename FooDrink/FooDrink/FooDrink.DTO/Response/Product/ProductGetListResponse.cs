namespace FooDrink.DTO.Response.Product
{
    public class ProductGetListResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string CategoryList { get; set; } = string.Empty;
        public Guid MenuId { get; set; }
    }
}
