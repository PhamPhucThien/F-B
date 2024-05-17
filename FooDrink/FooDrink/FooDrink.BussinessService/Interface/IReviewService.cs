using FooDrink.DTO.Request.Review;
using FooDrink.DTO.Response.Review;

namespace FooDrink.BussinessService.Interface
{
    public interface IReviewService
    {
        Task<ReviewAddResponse> AddReviewAsync(ReviewAddRequest request);
        Task<ReviewUpdateResponse> UpdateReviewAsync(ReviewUpdateRequest request);
        Task<ReviewDeleteResponse> DeleteReviewAsync(ReviewDeleteRequest request);
        Task<ReviewGetByIdResponse> GetReviewByIdAsync(ReviewGetByIdRequest request);
        Task<ReviewGetByUserIdResponse> GetReviewsByUserIdAsync(ReviewGetByUserIdRequest request);
        Task<ReviewGetByRestaurantIdResponse> GetReviewsByRestaurantIdAsync(ReviewGetByRestaurantIdRequest request);
        Task<ReviewGetListResponse> GetReviewsAsync(ReviewGetListRequest request);
        Task<List<string>> GetReviewImageUrlsAsync(Guid reviewId);
    }
}
