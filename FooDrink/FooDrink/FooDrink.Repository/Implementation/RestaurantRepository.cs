using FooDrink.Database;
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
        private readonly IHandleImageRepository _imageRepository;

        public RestaurantRepository(FooDrinkDbContext context, IHandleImageRepository imageRepository)
        {
            _context = context;
            _imageRepository = imageRepository;
        }

        /// <summary>
        /// GetRestaurantsAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<RestaurantGetListResponse>> GetRestaurantsAsync(RestaurantGetListRequest request)
        {
            try
            {
                if (_context.Restaurants == null)
                {
                    throw new NullReferenceException("Restaurant database context is null");
                }
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

                List<RestaurantGetListResponse> responseList = new();
                foreach (Restaurant? restaurant in await query.ToListAsync())
                {
                    List<string> imageUrls = await GetRestaurantImageUrlsAsync(restaurant.Id);

                    RestaurantResponse restaurantResponse = new()
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
                        ImageList = imageUrls,
                        TotalRevenue = restaurant.TotalRevenue,
                        DailyRevenue = restaurant.DailyRevenue,
                        MonthlyRevenue = restaurant.MonthlyRevenue,
                        IsRegistration = restaurant.IsRegistration,
                        Status = restaurant.Status,
                    };
                    RestaurantGetListResponse getListResponse = new()
                    {
                        PageSize = request.PageSize,
                        PageIndex = request.PageIndex,
                        SearchString = request.SearchString,
                        Data = new List<RestaurantResponse> { restaurantResponse }
                    };
                    responseList.Add(getListResponse);
                }

                return responseList;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants.", ex);
            }
        }

        /// <summary>
        /// GetRestaurantsByLocationAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<RestaurantGetByLocationResponse> GetRestaurantsByLocationAsync(RestaurantGetByLocationRequest request)
        {
            try
            {
                if (_context.Restaurants == null)
                {
                    throw new NullReferenceException("Restaurant database context is null");
                }

                List<Restaurant> restaurants = await _context.Restaurants
                    .Where(r => r.Latitude == request.Latitude && r.Longitude == request.Longitude)
                    .ToListAsync();

                RestaurantGetByLocationResponse response = new()
                {
                    Location = $"{request.Latitude},{request.Longitude}",
                    Data = new List<RestaurantResponse>()
                };

                foreach (Restaurant? restaurant in restaurants)
                {
                    List<string> imageUrls = await GetRestaurantImageUrlsAsync(restaurant.Id);
                    response.Data.Add(new RestaurantResponse
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
                        ImageList = imageUrls,
                        TotalRevenue = restaurant.TotalRevenue,
                        DailyRevenue = restaurant.DailyRevenue,
                        MonthlyRevenue = restaurant.MonthlyRevenue,
                        IsRegistration = restaurant.IsRegistration,
                        Status = restaurant.Status,
                    });
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants by location.", ex);
            }
        }

        /// <summary>
        /// AddAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<Restaurant> AddAsync(Restaurant entity)
        {
            if (_context.Restaurants == null)
            {
                throw new NullReferenceException("Restaurant database context is null");
            }

            _ = _context.Restaurants.Add(entity);
            _ = await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>      
        /// DeleteByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (_context.Restaurants == null)
            {
                throw new NullReferenceException("Restaurant database context is null");
            }

            Restaurant? entity = await _context.Restaurants.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.Status = false;
            _ = await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// EditAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync(Restaurant entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _ = await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<Restaurant>> GetAll()
        {
            DbSet<Restaurant>? restaurants = _context.Restaurants;
            return restaurants == null ? throw new Exception("Restaurant data not found in the database.") : (IEnumerable<Restaurant>)await restaurants.ToListAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Restaurant?> GetByIdAsync(Guid id)
        {
            return _context.Restaurants == null
                ? throw new NullReferenceException("Restaurant database context is null")
                : await _context.Restaurants.FindAsync(id);
        }

        /// <summary>
        /// ApproveRestaurantPartnerAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<ApproveRestaurantPartnerResponse> ApproveRestaurantPartnerAsync(ApproveRestaurantPartnerRequest request)
        {
            try
            {
                if (_context.Restaurants == null)
                {
                    throw new NullReferenceException("Restaurant database context is null");
                }

                Restaurant? restaurant = await _context.Restaurants.FindAsync(request.Id);

                if (restaurant == null)
                {
                    throw new ArgumentException("Restaurant not found");
                }

                restaurant.IsRegistration = request.IsRegistration;

                _ = await _context.SaveChangesAsync();

                ApproveRestaurantPartnerResponse response = new()
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
                    ImageList = await GetRestaurantImageUrlsAsync(restaurant.Id),
                    TotalRevenue = restaurant.TotalRevenue,
                    DailyRevenue = restaurant.DailyRevenue,
                    MonthlyRevenue = restaurant.MonthlyRevenue,
                    IsRegistration = restaurant.IsRegistration,
                    Status = restaurant.Status
                }
            },
                    Message = request.IsRegistration ? "Successfully approved the restaurant partner" : "Failed to approve the restaurant partner"
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while approving the restaurant partner.", ex);
            }
        }

        /// <summary>
        /// GetWithPaging
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public IEnumerable<Restaurant> GetWithPaging(IPagingRequest pagingRequest)
        {
            return pagingRequest == null
                ? throw new ArgumentNullException(nameof(pagingRequest))
                : _context.Restaurants == null
                ? throw new NullReferenceException("Restaurant database context is null")
                : (IEnumerable<Restaurant>)_context.Restaurants
                .Skip(pagingRequest.PageSize * (pagingRequest.PageIndex - 1))
                .Take(pagingRequest.PageSize)
                .ToList();
        }

        /// <summary>
        /// GetRestaurantImageListAsync
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        public async Task<List<string>> GetRestaurantImageUrlsAsync(Guid restaurantId)
        {
            if (_context.Restaurants == null)
            {
                throw new NullReferenceException("Restaurant database context is null");
            }
            Restaurant? restaurant = await _context.Restaurants.FindAsync(restaurantId);
            if (restaurant == null)
            {
                throw new ArgumentException("Restaurant not found");
            }
            List<string> imageList = restaurant.ImageList.Split(',')
                .Select(image => $"{image}")
                .Where(image => !string.IsNullOrWhiteSpace(image))
                .ToList();

            return imageList;
        }

    }
}
