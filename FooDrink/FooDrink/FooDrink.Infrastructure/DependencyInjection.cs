using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.Database;
using FooDrink.Infrastructure.Authentication;
using FooDrink.Repository;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FooDrink.Infrastructure
{
    public static class DependencyInjection
    {
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            _ = services.AddHttpContextAccessor();
            _ = services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();
            _ = services.AddScoped<IAuthenticationService, AuthenticationService>();
            _ = services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<FooDrinkDbContext>(opts =>
                opts.UseSqlServer(connectionString), ServiceLifetime.Transient); 
            
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FooDrinkDbContext>();
/*            dbContext.Database.Migrate();
*/        }
    }
}
