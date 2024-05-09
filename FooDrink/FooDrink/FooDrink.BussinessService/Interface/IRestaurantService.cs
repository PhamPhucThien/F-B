using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooDrink.BusinessService.Interface
{
    public interface IRestaurantService
    {
        Task<IEnumerable<RestaurantGetListResponse>> GetRestaurantsAsync(RestaurantGetListRequest request);
        Task<RestaurantGetByIdResponse> GetRestaurantByIdAsync(RestaurantGetByIdRequest request);
        Task<bool> DeleteRestaurantByIdAsync(Guid id);
        Task<RestaurantAddResponse> AddRestaurantAsync(RestaurantAddRequest request);
        Task<RestaurantUpdateResponse> UpdateRestaurantAsync(RestaurantUpdateRequest request);
    }
}
