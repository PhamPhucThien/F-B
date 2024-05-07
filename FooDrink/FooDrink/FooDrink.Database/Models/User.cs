namespace FooDrink.Database.Models
{
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string FavoritedList { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public Guid RestaurantId { get; set; }
        public ICollection<Restaurant>? Restaurants { get; set; }
        public ICollection<Review>? Reviews { get; set; }
    }
}
