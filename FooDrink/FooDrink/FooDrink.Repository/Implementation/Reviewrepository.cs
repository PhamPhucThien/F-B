using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.DTO.Request;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FooDrink.Repository.Implementation
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly FooDrinkDbContext _context;

        public ReviewRepository(FooDrinkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Review> AddAsync(Review entity)
        {
            if (_context.Reviews == null)
            {
                throw new InvalidOperationException("The Reviews DbSet is null.");
            }
            _ = await _context.Reviews.AddAsync(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// DeleteByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (_context.Reviews == null)
            {
                throw new InvalidOperationException("The Reviews DbSet is null.");
            }
            Review? entity = await _context.Reviews.FindAsync(id);
            if (entity != null)
            {
                entity.Status = false;
                _ = _context.Reviews.Update(entity);
                _ = await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// EditAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> EditAsync(Review entity)
        {
            if (_context.Reviews == null)
            {
                throw new InvalidOperationException("The Reviews DbSet is null.");
            }
            _ = _context.Reviews.Update(entity);
            _ = await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Review>> GetAll()
        {
            return _context.Reviews == null
                ? throw new InvalidOperationException("The Reviews DbSet is null.")
                : (IEnumerable<Review>)await _context.Reviews.ToListAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<Review?> GetByIdAsync(Guid id)
        {
            return _context.Reviews == null
                ? throw new InvalidOperationException("The Reviews DbSet is null.")
                : await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);
        }

        /// <summary>
        /// GetByUserIdAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Review>> GetByUserIdAsync(Guid userId)
        {
            return _context.Reviews == null
                ? throw new InvalidOperationException("The Reviews DbSet is null.")
                : (IEnumerable<Review>)await _context.Reviews.Where(r => r.UserId == userId).ToListAsync();
        }

        /// <summary>
        /// GetByRestaurantIdAsync
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<IEnumerable<Review>> GetByRestaurantIdAsync(Guid restaurantId)
        {
            return _context.Reviews == null
                ? throw new InvalidOperationException("The Reviews DbSet is null.")
                : (IEnumerable<Review>)await _context.Reviews.Where(r => r.RestaurantId == restaurantId).ToListAsync();
        }

        /// <summary>
        /// GetWithPaging
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IEnumerable<Review> GetWithPaging(IPagingRequest pagingRequest)
        {
            if (_context.Reviews == null)
            {
                throw new InvalidOperationException("The Reviews DbSet is null.");
            }
            IQueryable<Review> query = _context.Reviews.AsQueryable();

            if (!string.IsNullOrEmpty(pagingRequest.SearchString))
            {
                query = query.Where(r => r.Content.Contains(pagingRequest.SearchString));
            }

            return query.Skip(pagingRequest.PageSize * (pagingRequest.PageIndex - 1))
                        .Take(pagingRequest.PageSize)
                        .ToList();
        }

        /// <summary>
        /// GetReviewImageUrlsAsync
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<List<string>> GetReviewImageUrlsAsync(Guid reviewId)
        {
            if (_context.Reviews == null)
            {
                throw new InvalidOperationException("The Reviews DbSet is null.");
            }
            Review? review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                throw new ArgumentException("Review not found");
            }

            List<string> imageList = review.ImageList.Split(',')
                .Select(image => $"{image}")
                .Where(image => !string.IsNullOrWhiteSpace(image))
                .ToList();

            return imageList;
        }
    }
}
