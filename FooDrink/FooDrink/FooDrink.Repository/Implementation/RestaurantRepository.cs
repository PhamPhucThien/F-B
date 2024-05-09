using FooDrink.Database.Models;
using FooDrink.Database;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Request;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Implementation
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly FooDrinkDbContext _context;

        public RestaurantRepository(FooDrinkDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RestaurantGetListResponse>> GetRestaurantsAsync(RestaurantGetListRequest request)
        {
            IQueryable<Restaurant> query = _context.Restaurants;

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                query = query.Where(r =>
                    r.RestaurantName.ToLower().Contains(request.SearchString.ToLower()) ||
                    r.City.ToLower().Contains(request.SearchString.ToLower()) ||
                    r.Country.ToLower().Contains(request.SearchString.ToLower()));
            }

            var pagedRestaurants = await query
                .Skip(request.PageSize * (request.PageIndex - 1))
                .Take(request.PageSize)
                .ToListAsync();

            var responseList = pagedRestaurants.Select(r => new RestaurantGetListResponse
            {
                RestaurantName = r.RestaurantName,
                Latitude = r.Latitude,
                Longitude = r.Longitude,
                Address = r.Address,
                City = r.City,
                Country = r.Country,
                Hotline = r.Hotline,
                AverageRating = r.AverageRating
            }).ToList();

            return responseList;
        }

        public async Task<Restaurant> AddAsync(Restaurant entity)
        {
            _context.Restaurants.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var entity = await _context.Restaurants.FindAsync(id);
            if (entity == null)
                return false;
            entity.Status = true; // Đánh dấu là đã xóa
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(Restaurant entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            return await _context.Restaurants.ToListAsync();
        }

        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            return await _context.Restaurants.FindAsync(id);
        }

        public IEnumerable<Restaurant> GetWithPaging(IPagingRequest pagingRequest)
        {
            if (pagingRequest == null)
                throw new ArgumentNullException(nameof(pagingRequest));

            return _context.Restaurants
                .Skip(pagingRequest.PageSize * (pagingRequest.PageIndex - 1))
                .Take(pagingRequest.PageSize)
                .ToList();
        }
    }
}
