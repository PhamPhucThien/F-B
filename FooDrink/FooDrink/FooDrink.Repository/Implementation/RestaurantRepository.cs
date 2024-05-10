﻿using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.DTO.Request;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;

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
            try
            {
                IQueryable<Restaurant> query = _context.Restaurants.AsQueryable();

                if (!string.IsNullOrEmpty(request.SearchString))
                {
                    query = query.Where(r =>
                        r.RestaurantName.Contains(request.SearchString) ||
                        r.City.Contains(request.SearchString) ||
                        r.Latitude.Contains(request.SearchString) ||
                        r.Longitude.Contains(request.SearchString) ||
                        r.Address.Contains(request.SearchString) ||
                        r.Country.Contains(request.SearchString));
                }

                query = query.OrderBy(r => r.RestaurantName)
                             .Skip((request.PageIndex - 1) * request.PageSize)
                             .Take(request.PageSize);

                List<RestaurantGetListResponse> responseList = await query.Select(r => new RestaurantGetListResponse
                {
                    PageSize = request.PageSize,
                    PageIndex = request.PageIndex,
                    SearchString = request.SearchString,
                    Data = new List<RestaurantResponse>
                    {
                        new RestaurantResponse
                        {
                            Id = r.Id,
                            RestaurantName = r.RestaurantName,
                            Latitude = r.Latitude,
                            Longitude = r.Longitude,
                            Address = r.Address,
                            City = r.City,
                            Country = r.Country,
                            Hotline = r.Hotline,
                            AverageRating = r.AverageRating,
                            ImageList = r.ImageList,
                            TotalRevenue = r.TotalRevenue,
                            DailyRevenue = r.DailyRevenue,
                            MonthlyRevenue = r.MonthlyRevenue

                        }
                    }
                }).ToListAsync();

                return responseList;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants.", ex);
            }
        }

        public async Task<RestaurantGetByLocationResponse> GetRestaurantsByLocationAsync(RestaurantGetByLocationRequest request)
        {
            try
            {
                var restaurants = await _context.Restaurants
                    .Where(r => r.Latitude == request.Latitude && r.Longitude == request.Longitude)
                    .ToListAsync();

                var response = new RestaurantGetByLocationResponse
                {
                    Location = $"{request.Latitude},{request.Longitude}",
                    Data = restaurants.Select(r => new RestaurantResponse
                    {
                        Id = r.Id,
                        RestaurantName = r.RestaurantName,
                        Latitude = r.Latitude,
                        Longitude = r.Longitude,
                        Address = r.Address,
                        City = r.City,
                        Country = r.Country,
                        Hotline = r.Hotline,
                        AverageRating = r.AverageRating,
                        ImageList = r.ImageList,
                        TotalRevenue = r.TotalRevenue,
                        DailyRevenue = r.DailyRevenue,
                        MonthlyRevenue = r.MonthlyRevenue
                    }).ToList()
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants by location.", ex);
            }
        }

        public async Task<Restaurant> AddAsync(Restaurant entity)
        {
            _ = _context.Restaurants.Add(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            Restaurant? entity = await _context.Restaurants.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.Status = true;
            _ = await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditAsync(Restaurant entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _ = await _context.SaveChangesAsync();
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

        public async Task<ApproveRestaurantPartnerResponse> ApproveRestaurantPartnerAsync(ApproveRestaurantPartnerRequest request)
        {
            try
            {
                var restaurant = await _context.Restaurants.FindAsync(request.Id);

                if (restaurant == null)
                {
                    throw new ArgumentException("Restaurant not found");
                }

                restaurant.IsRegistration = request.IsRegistration;

                await _context.SaveChangesAsync();

                var response = new ApproveRestaurantPartnerResponse
                {
                    Data = new List<RestaurantResponse>
            {
                new RestaurantResponse
                {
                    Id = restaurant.Id,
                    RestaurantName = restaurant.RestaurantName,
                    Latitude = restaurant.Latitude,
                    Longitude = restaurant.Longitude,
                    Address = restaurant.Address,
                    City = restaurant.City,
                    Country = restaurant.Country,
                    Hotline = restaurant.Hotline,
                    AverageRating = restaurant.AverageRating,
                    ImageList = restaurant.ImageList,
                    TotalRevenue = restaurant.TotalRevenue,
                    DailyRevenue = restaurant.DailyRevenue,
                    MonthlyRevenue = restaurant.MonthlyRevenue,
                    IsRegistration = restaurant.IsRegistration,
                    Status = restaurant.Status
                }
            }
                };

                response.Message = request.IsRegistration ? "Successfully approved the restaurant partner" : "Failed to approve the restaurant partner";

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while approving the restaurant partner.", ex);
            }
        }


        public IEnumerable<Restaurant> GetWithPaging(IPagingRequest pagingRequest)
        {
            return pagingRequest == null
                ? throw new ArgumentNullException(nameof(pagingRequest))
                : (IEnumerable<Restaurant>)_context.Restaurants
                .Skip(pagingRequest.PageSize * (pagingRequest.PageIndex - 1))
                .Take(pagingRequest.PageSize)
                .ToList();
        }

    }
}
