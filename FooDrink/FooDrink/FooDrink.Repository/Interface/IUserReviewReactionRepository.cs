using FooDrink.Database.Models;
using FooDrink.DTO.Request.Review;

namespace FooDrink.Repository.Interface
{
    public interface IUserReviewReactionRepository
    {
        Task<UserReviewReaction?> GetUserReviewReactionAsync(ToggleReactionRequest request);
        Task AddUserReviewReactionAsync(UserReviewReaction reaction);
        Task RemoveUserReviewReactionAsync(UserReviewReaction reaction);
        Task<int> GetReactionCountAsync(Guid reviewId);
    }
}
