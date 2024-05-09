using FooDrink.BusinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.BussinessService.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<IEnumerable<RestaurantGetListResponse>> GetRestaurantsAsync(RestaurantGetListRequest request)
        {
            var restaurants = await _restaurantRepository.GetRestaurantsAsync(request);

            int pageSize = request.PageSize > 0 ? request.PageSize : restaurants.Count();
            int totalPages = (int)Math.Ceiling((double)restaurants.Count() / pageSize);
            int pageIndex = request.PageIndex > 0 ? request.PageIndex : 1;
            if (pageIndex > totalPages) pageIndex = totalPages;

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                restaurants = restaurants.Where(r =>
                    r.RestaurantName.ToLower().Contains(request.SearchString.ToLower()) ||
                    r.City.ToLower().Contains(request.SearchString.ToLower()) ||
                    r.Country.ToLower().Contains(request.SearchString.ToLower()));
            }

            var pagedRestaurants = restaurants.Skip((pageIndex - 1) * pageSize).Take(pageSize);

            var responseList = new List<RestaurantGetListResponse>();

            foreach (var restaurant in pagedRestaurants)
            {
                responseList.Add(new RestaurantGetListResponse
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
                });
            }

            return responseList;
        }

        public async Task<RestaurantGetByIdResponse> GetRestaurantByIdAsync(RestaurantGetByIdRequest request)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);
            if (restaurant == null)
            {
                return null;
            }
            return new RestaurantGetByIdResponse
            {
                RestaurantName = restaurant.RestaurantName,
                Latitude = restaurant.Latitude,
                Longitude = restaurant.Longitude,
                Address = restaurant.Address,
                City = restaurant.City,
                Country = restaurant.Country,
                Hotline = restaurant.Hotline,
                AverageRating = restaurant.AverageRating
            };
        }

        public async Task<bool> DeleteRestaurantByIdAsync(Guid id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return false;
            }
            restaurant.Status = true;
            var result = await _restaurantRepository.EditAsync(restaurant);
            return result;
        }

        public async Task<RestaurantAddResponse> AddRestaurantAsync(RestaurantAddRequest request)
        {
            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(request.RestaurantName) || string.IsNullOrEmpty(request.Latitude) ||
                string.IsNullOrEmpty(request.Longitude) || string.IsNullOrEmpty(request.Address) ||
                string.IsNullOrEmpty(request.City) || string.IsNullOrEmpty(request.Country) ||
                string.IsNullOrEmpty(request.Hotline))
            {
                // Nếu dữ liệu đầu vào không hợp lệ, trả về BadRequest hoặc thông báo lỗi phù hợp
                throw new ArgumentException("Invalid input data. All required fields must be provided.");
            }

            try
            {
                var restaurant = new Restaurant
                {
                    Id = Guid.NewGuid(),
                    RestaurantName = request.RestaurantName,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Address = request.Address,
                    City = request.City,
                    Country = request.Country,
                    Hotline = request.Hotline,
                    Status = false,
                    CreatedBy = "",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = "",
                    UpdatedAt = DateTime.UtcNow,
                    AverageRating = 0.0f, // Giá trị mặc định cho AverageRating
                    ImageList = "", // Giá trị mặc định cho ImageList
                    TotalRevenue = "", // Giá trị mặc định cho TotalRevenue
                    DailyRevenue = "", // Giá trị mặc định cho DailyRevenue
                    MonthlyRevenue = "" // Giá trị mặc định cho MonthlyRevenue
                };

                var addedRestaurant = await _restaurantRepository.AddAsync(restaurant);

                // Chuyển đổi đối tượng thêm mới thành đối tượng phản hồi
                var response = new RestaurantAddResponse
                {
                    Id = addedRestaurant.Id.ToString(),
                    RestaurantName = addedRestaurant.RestaurantName,
                    Latitude = addedRestaurant.Latitude,
                    Longitude = addedRestaurant.Longitude,
                    Address = addedRestaurant.Address,
                    City = addedRestaurant.City,
                    Country = addedRestaurant.Country,
                    Hotline = addedRestaurant.Hotline,
                    AverageRating = addedRestaurant.AverageRating,
                    ImageList = addedRestaurant.ImageList,
                    TotalRevenue = addedRestaurant.TotalRevenue,
                    DailyRevenue = addedRestaurant.DailyRevenue,
                    MonthlyRevenue = addedRestaurant.MonthlyRevenue
                };

                return response;
            }
            catch (Exception ex)
            {
                // Log lỗi
                Console.WriteLine($"An error occurred while adding restaurant: {ex.Message}");
                // Trả về lỗi server internal
                throw new Exception("An error occurred while saving the entity changes.");
            }
        }

        public async Task<RestaurantUpdateResponse> UpdateRestaurantAsync(RestaurantUpdateRequest request)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(request.Id);
            if (existingRestaurant == null)
            {
                return null;
            }

            existingRestaurant.RestaurantName = request.RestaurantName;
            existingRestaurant.Latitude = request.Latitude;
            existingRestaurant.Longitude = request.Longitude;
            existingRestaurant.Address = request.Address;
            existingRestaurant.City = request.City;
            existingRestaurant.Country = request.Country;
            existingRestaurant.Hotline = request.Hotline;

            var isEdited = await _restaurantRepository.EditAsync(existingRestaurant);
            if (!isEdited)
            {
                return null;
            }

            return new RestaurantUpdateResponse
            {
                Id = existingRestaurant.Id,
                RestaurantName = existingRestaurant.RestaurantName,
                Latitude = existingRestaurant.Latitude,
                Longitude = existingRestaurant.Longitude,
                Address = existingRestaurant.Address,
                City = existingRestaurant.City,
                Country = existingRestaurant.Country,
                Hotline = existingRestaurant.Hotline
            };
        }
    }
}
