using FooDrink.Database.Models;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Interface
{
    public interface IRestaurantRepository : IRepository<Restaurant>
    {
        Task<IEnumerable<RestaurantGetListResponse>> GetRestaurantsAsync(RestaurantGetListRequest request);
    }
}
