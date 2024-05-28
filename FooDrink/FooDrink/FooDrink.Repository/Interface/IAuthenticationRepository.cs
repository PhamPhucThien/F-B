using FooDrink.Database.Models;

namespace FooDrink.Repository.Interface
{
    public interface IAuthenticationRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAndPassword(string username, string password);
        Task<User?> GetByUsername(string username);
    }
}
