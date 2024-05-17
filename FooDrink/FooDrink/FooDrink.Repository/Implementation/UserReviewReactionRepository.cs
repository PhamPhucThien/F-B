using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Review;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;

public class UserReviewReactionRepository : IUserReviewReactionRepository
{
    private readonly FooDrinkDbContext _context;

    public UserReviewReactionRepository(FooDrinkDbContext context)
    {
        _context = context;
    }

    public async Task<UserReviewReaction?> GetUserReviewReactionAsync(ToggleReactionRequest request)
    {
        return await _context.UserReviewReactions
                             .FirstOrDefaultAsync(urr => urr.UserId == request.UserId && urr.ReviewId == request.ReviewId);
    }

    public async Task AddUserReviewReactionAsync(UserReviewReaction reaction)
    {
        _ = await _context.UserReviewReactions.AddAsync(reaction);
        _ = await _context.SaveChangesAsync();
    }

    public async Task RemoveUserReviewReactionAsync(UserReviewReaction reaction)
    {
        _ = _context.UserReviewReactions.Remove(reaction);
        _ = await _context.SaveChangesAsync();
    }

    public async Task<int> GetReactionCountAsync(Guid reviewId)
    {
        return await _context.UserReviewReactions.CountAsync(urr => urr.ReviewId == reviewId);
    }
}
