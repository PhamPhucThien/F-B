using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class ReviewConfig : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            _ = builder.HasOne(r => r.User)
                   .WithMany(user => user.Reviews);

            _ = builder.HasOne(r => r.Restaurant)
                   .WithMany(res => res.Reviews);

            _ = builder.HasMany(r => r.UserReviewReactions)
                   .WithOne(urr => urr.Review)
                   .HasForeignKey(urr => urr.ReviewId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
