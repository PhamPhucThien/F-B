using FooDrink.Database.Models;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;

namespace FooDrink.Repository.Interface
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<IEnumerable<RestaurantGetListResponse>> GetRestaurantsAsync(RestaurantGetListRequest request);
    }
}
