using FooDrink.Database.Models;
using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.Authentication;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.DTO.Response.User;

namespace FooDrink.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        Task<IEnumerable<UserGetListResponse>> GetUsersAsync(UserGetListRequest request);
    }
}
