using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Review;
using FooDrink.DTO.Response.Review;
using FooDrink.Repository.Interface;

namespace FooDrink.BussinessService.Service
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        /// <summary>
        /// AddReviewAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReviewAddResponse> AddReviewAsync(ReviewAddRequest request)
        {
            Review newReview = new()
            {
                UserId = request.UserId,
                RestaurantId = request.RestaurantId,
                Content = request.Content,
                Rating = request.Rating,
                Reaction = request.Reaction,
                Status = true,
                CreatedBy = request.UserId.ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            Review addedReview = await _reviewRepository.AddAsync(newReview);

            List<string> imageUrls = await GetReviewImageUrlsAsync(addedReview.Id);

            ReviewAddResponse response = new()
            {
                Data = new List<ReviewResponse>
        {
            new ReviewResponse
            {
                Id = addedReview.Id,
                UserId = addedReview.UserId,
                RestaurantId = addedReview.RestaurantId,
                ImageList = imageUrls,
                Content = addedReview.Content,
                Rating = addedReview.Rating,
                Reaction = addedReview.Reaction,
                Status = addedReview.Status,
                CreatedBy = addedReview.CreatedBy,
                CreatedAt = addedReview.CreatedAt,
                UpdatedBy = addedReview.UpdatedBy,
                UpdatedAt = addedReview.UpdatedAt
            }
        }
            };

            return response;
        }

        /// <summary>
        /// UpdateReviewAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ReviewUpdateResponse> UpdateReviewAsync(ReviewUpdateRequest request)
        {
            Review? existingReview = await _reviewRepository.GetByIdAsync(request.Id);

            if (existingReview == null)
            {
                throw new Exception("Review not found.");
            }

            existingReview.Content = request.Content;
            existingReview.Rating = request.Rating;
            existingReview.Reaction = request.Reaction;
            existingReview.UpdatedAt = DateTime.UtcNow;

            _ = await _reviewRepository.EditAsync(existingReview);

            List<string> imageUrls = await GetReviewImageUrlsAsync(existingReview.Id);

            ReviewUpdateResponse response = new()
            {
                Data = new List<ReviewResponse>
        {
            new ReviewResponse
            {
                Id = existingReview.Id,
                UserId = existingReview.UserId,
                RestaurantId = existingReview.RestaurantId,
                ImageList = imageUrls,
                Content = existingReview.Content,
                Rating = existingReview.Rating,
                Reaction = existingReview.Reaction,
                Status = existingReview.Status,
                CreatedBy = existingReview.CreatedBy,
                CreatedAt = existingReview.CreatedAt,
                UpdatedBy = existingReview.UpdatedBy,
                UpdatedAt = existingReview.UpdatedAt
            }
        }
            };

            return response;
        }

        /// <summary>
        /// DeleteReviewAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ReviewDeleteResponse> DeleteReviewAsync(ReviewDeleteRequest request)
        {
            Review? existingReview = await _reviewRepository.GetByIdAsync(request.Id);

            if (existingReview == null)
            {
                throw new Exception("Review not found.");
            }

            existingReview.Status = false;
            _ = await _reviewRepository.EditAsync(existingReview);

            List<string> imageUrls = await GetReviewImageUrlsAsync(existingReview.Id);

            ReviewDeleteResponse response = new()
            {
                Data = new List<ReviewResponse>
        {
            new ReviewResponse
            {
                Id = existingReview.Id,
                UserId = existingReview.UserId,
                RestaurantId = existingReview.RestaurantId,
                ImageList = imageUrls,
                Content = existingReview.Content,
                Rating = existingReview.Rating,
                Reaction = existingReview.Reaction,
                Status = existingReview.Status,
                CreatedBy = existingReview.CreatedBy,
                CreatedAt = existingReview.CreatedAt,
                UpdatedBy = existingReview.UpdatedBy,
                UpdatedAt = existingReview.UpdatedAt
            }
        }
            };

            return response;
        }

        /// <summary>
        /// GetReviewByIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ReviewGetByIdResponse> GetReviewByIdAsync(ReviewGetByIdRequest request)
        {
            Review? review = await _reviewRepository.GetByIdAsync(request.Id);

            if (review == null)
            {
                throw new Exception("Review not found.");
            }

            List<string> imageUrls = await GetReviewImageUrlsAsync(review.Id);

            ReviewGetByIdResponse response = new()
            {
                Data = new List<ReviewResponse>
        {
            new ReviewResponse
            {
                Id = review.Id,
                UserId = review.UserId,
                RestaurantId = review.RestaurantId,
                ImageList = imageUrls,
                Content = review.Content,
                Rating = review.Rating,
                Reaction = review.Reaction,
                Status = review.Status,
                CreatedBy = review.CreatedBy,
                CreatedAt = review.CreatedAt,
                UpdatedBy = review.UpdatedBy,
                UpdatedAt = review.UpdatedAt
            }
        }
            };

            return response;
        }

        /// <summary>
        /// GetReviewsByUserIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReviewGetByUserIdResponse> GetReviewsByUserIdAsync(ReviewGetByUserIdRequest request)
        {
            IEnumerable<Review> reviews = await _reviewRepository.GetByUserIdAsync(request.UserId);

            List<ReviewResponse> reviewResponses = new();
            foreach (Review review in reviews)
            {
                List<string> imageUrls = await GetReviewImageUrlsAsync(review.Id);

                reviewResponses.Add(new ReviewResponse
                {
                    Id = review.Id,
                    UserId = review.UserId,
                    RestaurantId = review.RestaurantId,
                    ImageList = imageUrls,
                    Content = review.Content,
                    Rating = review.Rating,
                    Reaction = review.Reaction,
                    Status = review.Status,
                    CreatedBy = review.CreatedBy,
                    CreatedAt = review.CreatedAt,
                    UpdatedBy = review.UpdatedBy,
                    UpdatedAt = review.UpdatedAt
                });
            }

            ReviewGetByUserIdResponse response = new()
            {
                Data = reviewResponses
            };

            return response;
        }

        /// <summary>
        /// GetReviewsByRestaurantIdAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReviewGetByRestaurantIdResponse> GetReviewsByRestaurantIdAsync(ReviewGetByRestaurantIdRequest request)
        {
            IEnumerable<Review> reviews = await _reviewRepository.GetByRestaurantIdAsync(request.RestaurantId);

            ReviewGetByRestaurantIdResponse response = new()
            {
                Data = reviews.Select(async review =>
                {
                    List<string> imageUrls = await GetReviewImageUrlsAsync(review.Id);

                    return new ReviewResponse
                    {
                        Id = review.Id,
                        UserId = review.UserId,
                        RestaurantId = review.RestaurantId,
                        ImageList = imageUrls,
                        Content = review.Content,
                        Rating = review.Rating,
                        Reaction = review.Reaction,
                        Status = review.Status,
                        CreatedBy = review.CreatedBy,
                        CreatedAt = review.CreatedAt,
                        UpdatedBy = review.UpdatedBy,
                        UpdatedAt = review.UpdatedAt
                    };
                }).Select(task => task.Result).ToList()
            };

            return response;
        }

        /// <summary>
        /// GetReviewsAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ReviewGetListResponse> GetReviewsAsync(ReviewGetListRequest request)
        {
            IEnumerable<Review> reviews = _reviewRepository.GetWithPaging(request);

            ReviewGetListResponse response = new()
            {
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                SearchString = request.SearchString,
                Data = reviews.Select(async review =>
                {
                    List<string> imageUrls = await GetReviewImageUrlsAsync(review.Id);

                    return new ReviewResponse
                    {
                        Id = review.Id,
                        UserId = review.UserId,
                        RestaurantId = review.RestaurantId,
                        ImageList = imageUrls,
                        Content = review.Content,
                        Rating = review.Rating,
                        Reaction = review.Reaction,
                        Status = review.Status,
                        CreatedBy = review.CreatedBy,
                        CreatedAt = review.CreatedAt,
                        UpdatedBy = review.UpdatedBy,
                        UpdatedAt = review.UpdatedAt
                    };
                }).Select(task => task.Result).ToList()
            };

            return await Task.FromResult(response);
        }

        /// <summary>
        /// GetReviewImageUrlsAsync
        /// </summary>
        /// <param name="reviewId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetReviewImageUrlsAsync(Guid reviewId)
        {
            return await _reviewRepository.GetReviewImageUrlsAsync(reviewId);
        }
    }
}
