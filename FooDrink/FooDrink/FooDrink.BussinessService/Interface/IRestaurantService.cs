using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;

namespace FooDrink.BussinessService.Interface
{
    /// <summary>
    /// Interface for handling restaurant-related operations.
    /// </summary>
    public interface IRestaurantService
    {
        /// <summary>
        /// Get a list of restaurants based on the provided criteria.
        /// </summary>
        Task<RestaurantGetListResponse> GetRestaurantsAsync(RestaurantGetListRequest request);

        /// <summary>
        /// Get restaurant details by ID.
        /// </summary>
        Task<RestaurantGetByIdResponse> GetRestaurantByIdAsync(RestaurantGetByIdRequest request);

        /// <summary>
        /// Delete a restaurant by ID.
        /// </summary>
        Task<bool> DeleteRestaurantByIdAsync(Guid id);

        /// <summary>
        /// Add a new restaurant.
        /// </summary>
        Task<RestaurantAddResponse> AddRestaurantAsync(RestaurantAddRequest request);

        /// <summary>
        /// Update an existing restaurant.
        /// </summary>
        Task<RestaurantUpdateResponse> UpdateRestaurantAsync(RestaurantUpdateRequest request);

        /// <summary>
        /// Approve or disapprove a restaurant partner.
        /// </summary>
        Task<ApproveRestaurantPartnerResponse> ApproveRestaurantPartnerAsync(ApproveRestaurantPartnerRequest request);

        /// <summary>
        /// Search restaurants by location coordinates.
        /// </summary>
        Task<RestaurantGetByLocationResponse> GetRestaurantsByLocationAsync(RestaurantGetByLocationRequest request);
    }
}
