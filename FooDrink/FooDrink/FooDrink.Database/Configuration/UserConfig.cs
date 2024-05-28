using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            _ = builder.HasMany(user => user.Restaurants)
                   .WithMany(res => res.Users);

            _ = builder.HasMany(user => user.Reviews)
                   .WithOne(r => r.User);
        }
    }
}
