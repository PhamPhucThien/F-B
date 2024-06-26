﻿using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.Database;
using FooDrink.Infrastructure.Authentication;
using FooDrink.Repository.Implementation;
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
            _ = services.AddScoped<IRestaurantRepository, RestaurantRepository>();
            _ = services.AddScoped<IUserRepository, UserRepository>();
            _ = services.AddScoped<IRestaurantService, RestaurantService>();
            _ = services.AddScoped<IUserService, UserService>();
            _ = services.AddScoped<IHandleImageRepository, HandleImageRepository>();
            _ = services.AddScoped<IImageService, ImageService>();
            _ = services.AddScoped<IReviewRepository, ReviewRepository>();
            _ = services.AddScoped<IReviewService, ReviewService>();
            _ = services.AddScoped<IUserReviewReactionRepository, UserReviewReactionRepository>();
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            _ = services.AddDbContext<FooDrinkDbContext>(opts =>
                opts.UseSqlServer(connectionString), ServiceLifetime.Transient);

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            using IServiceScope scope = serviceProvider.CreateScope();
            FooDrinkDbContext dbContext = scope.ServiceProvider.GetRequiredService<FooDrinkDbContext>();
            /*            dbContext.Database.Migrate();
            */
        }
    }
}
