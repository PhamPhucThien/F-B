using FooDrink.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FooDrink.Database.Configuration
{
    public class UserReviewConfig : IEntityTypeConfiguration<UserReviewReaction>
    {
        public void Configure(EntityTypeBuilder<UserReviewReaction> builder)
        {

            _ = builder.HasKey(urr => new { urr.UserId, urr.ReviewId });

            _ = builder.HasOne(urr => urr.User)
                   .WithMany(u => u.UserReviewReactions)
                   .HasForeignKey(urr => urr.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            _ = builder.HasOne(urr => urr.Review)
                   .WithMany(r => r.UserReviewReactions)
                   .HasForeignKey(urr => urr.ReviewId)
                   .OnDelete(DeleteBehavior.Cascade);

            _ = builder.Property(urr => urr.IsLiked)
                   .HasDefaultValue(true);
        }
    }
}
