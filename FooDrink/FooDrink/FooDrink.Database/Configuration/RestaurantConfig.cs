using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            _ = builder.HasMany(res => res.Users)
                   .WithMany(user => user.Restaurants);

            _ = builder.HasOne(res => res.Menu)
                   .WithOne(menu => menu.Restaurant);

            _ = builder.HasMany(res => res.Reviews)
                   .WithOne(r => r.Restaurant);

            _ = builder.HasMany(res => res.Orders)
                   .WithOne(o => o.Restaurant);
        }
    }
}
