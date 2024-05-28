using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace FooDrink.Repository.Interface
{
    public interface IFooDrinkDbContext
    {
        public DbSet<User>? Users { get; set; }

        public DbSet<Restaurant>? Restaurants { get; set; }

        public DbSet<Menu>? Menus { get; set; }

        public DbSet<Order>? Orders { get; set; }

        public DbSet<Product>? Products { get; set; }

        public DbSet<Review>? Reviews { get; set; }
    }
}
