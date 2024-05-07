using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class RestaurantConfig : IEntityTypeConfiguration<Restaurant>
    {
        public void Configure(EntityTypeBuilder<Restaurant> builder)
        {
            builder.HasMany(res => res.Users)
                   .WithMany(user => user.Restaurants);

            builder.HasOne(res => res.Menu)
                   .WithOne(menu => menu.Restaurant);

            builder.HasMany(res => res.Reviews)
                   .WithOne(r => r.Restaurant);

            builder.HasMany(res => res.Orders)
                   .WithOne(o => o.Restaurant);
        }
    }
}
