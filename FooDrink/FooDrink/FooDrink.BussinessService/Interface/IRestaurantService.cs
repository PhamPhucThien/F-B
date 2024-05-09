using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;

namespace FooDrink.BussinessService.Interface
{
    public interface IRestaurantService
    {
        Task<RestaurantGetListResponse> GetRestaurantsAsync(RestaurantGetListRequest request);
        Task<RestaurantGetByIdResponse> GetRestaurantByIdAsync(RestaurantGetByIdRequest request);
        Task<bool> DeleteRestaurantByIdAsync(Guid id);
        Task<RestaurantAddResponse> AddRestaurantAsync(RestaurantAddRequest request);
        Task<RestaurantUpdateResponse> UpdateRestaurantAsync(RestaurantUpdateRequest request);
    }
}
