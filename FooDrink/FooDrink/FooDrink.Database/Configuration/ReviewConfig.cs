using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasOne(r => r.User)
                   .WithMany(user => user.Reviews);

            builder.HasOne(r => r.Restaurant)
                   .WithMany(res => res.Reviews);
        }
    }
}
