namespace FooDrink.DTO.Response.Menu
{
    public class MenuGetByIdResponse
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public string Description { get; set; } = string.Empty;
    }
}
