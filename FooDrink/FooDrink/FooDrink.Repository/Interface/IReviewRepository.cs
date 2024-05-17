using FooDrink.Database.Models;

namespace FooDrink.Repository.Interface
{
    public interface IReviewRepository : IRepository<Review>
    {
        /// <summary>
        /// GetByUserIdAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId);

        /// <summary>
        /// GetByRestaurantIdAsync
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        Task<IEnumerable<Review>> GetByRestaurantIdAsync(Guid restaurantId);

        /// <summary>
        /// GetReviewImageUrlsAsync
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        Task<List<string>> GetReviewImageUrlsAsync(Guid reviewId);
    }
}
