namespace FooDrink.DTO.Response.Menu
{
    public class MenuGetResponse
    {
        public List<MenuList> Items { get; set; } = new List<MenuList>();
    }

    public class MenuList
    {
        public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
        public string Description { get; set; } = string.Empty;
    }
}
