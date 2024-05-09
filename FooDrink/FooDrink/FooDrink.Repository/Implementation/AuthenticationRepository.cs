using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FooDrink.Repository.Implementation
{
    public class AuthenticationRepository : RepositoryGeneric<User>, IAuthenticationRepository
    {
        private readonly DbContextOptions<FooDrinkDbContext> _contextOptions;

        public AuthenticationRepository(DbContextOptions<FooDrinkDbContext> contextOptions) : base(contextOptions)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));

        }

        public async Task<User?> GetByUsernameAndPassword(string username, string password)
        {
            using FooDrinkDbContext context = new(_contextOptions);
            User? entity = await context.Set<User>().FirstOrDefaultAsync(a => a.Username == username && a.Password == password && a.Status == true);
            return entity;
        }

        public async Task<User?> GetByUsername(string username)
        {
            using FooDrinkDbContext context = new(_contextOptions);
            User? entity = await context.Set<User>().FirstOrDefaultAsync(a => a.Username == username);
            return entity;
        }
    }
}
