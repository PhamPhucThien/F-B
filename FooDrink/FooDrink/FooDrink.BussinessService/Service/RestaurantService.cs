using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.Repository.Interface;

namespace FooDrink.BussinessService.Service
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Restaurant Service
        /// </summary>
        /// <param name="restaurantRepository"></param>
        /// <param name="userRepository"></param>
        public RestaurantService(IRestaurantRepository restaurantRepository, IUserRepository userRepository)
        {
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Get list Restaurant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RestaurantGetListResponse> GetRestaurantsAsync(RestaurantGetListRequest request)
        {
            try
            {
                IEnumerable<RestaurantGetListResponse> restaurantResponses = await _restaurantRepository.GetRestaurantsAsync(request);

                List<RestaurantResponse> restaurants = restaurantResponses
                    .SelectMany(response => response.Data)
                    .ToList();

                RestaurantGetListResponse response = new()
                {
                    PageSize = request.PageSize,
                    PageIndex = request.PageIndex,
                    SearchString = request.SearchString,
                    Data = restaurants
                };

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants.", ex);
            }
        }

        /// <summary>
        /// Get Restaurant by location (Lat, Long)
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<RestaurantGetByLocationResponse> GetRestaurantsByLocationAsync(RestaurantGetByLocationRequest request)
        {
            try
            {
                RestaurantGetByLocationResponse response = await _restaurantRepository.GetRestaurantsByLocationAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants by location.", ex);
            }
        }

        /// <summary>
        /// Get Restaurant by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<RestaurantGetByIdResponse> GetRestaurantByIdAsync(RestaurantGetByIdRequest request)
        {
            try
            {
                Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

                if (restaurant == null)
                {
                    throw new ArgumentException("Restaurant not found");
                }

                List<string> imageList = await _restaurantRepository.GetRestaurantImageUrlsAsync(request.Id);

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
                    ImageList = imageList,
                    TotalRevenue = restaurant.TotalRevenue,
                    DailyRevenue = restaurant.DailyRevenue,
                    MonthlyRevenue = restaurant.MonthlyRevenue,
                    IsRegistration = restaurant.IsRegistration,
                    Status = restaurant.Status
                };

                return new RestaurantGetByIdResponse
                {
                    Data = restaurantResponse
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the restaurant by id.", ex);
            }
        }

        /// <summary>
        /// Block Restaurant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRestaurantByIdAsync(Guid id)
        {
            Restaurant? restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return false;
            }
            restaurant.Status = true;
            bool result = await _restaurantRepository.EditAsync(restaurant);
            return result;
        }

        /// <summary>
        /// Add Retaurant contemporaneous create Add account witl Role: Manager
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<RestaurantAddResponse> AddRestaurantAsync(RestaurantAddRequest request)
        {
            if (string.IsNullOrEmpty(request.RestaurantName) || string.IsNullOrEmpty(request.Latitude) ||
                string.IsNullOrEmpty(request.Longitude) || string.IsNullOrEmpty(request.Address) ||
                string.IsNullOrEmpty(request.City) || string.IsNullOrEmpty(request.Country) ||
                string.IsNullOrEmpty(request.Hotline) || string.IsNullOrEmpty(request.Username) ||
                string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Email))
            {
                throw new ArgumentException("Invalid input data. All required fields must be provided.");
            }

            try
            {
                User newUser = new()
                {
                    Id = Guid.NewGuid(),
                    Username = request.Username,
                    Password = request.Password,
                    Email = request.Email,
                    FullName = request.RestaurantName,
                    Role = "Manager",
                    Address = request.Address,
                    PhoneNumber = request.Hotline,
                    FavoritedList = "",
                    Image = "",
                    CreatedBy = request.Username,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = request.Username,
                    UpdatedAt = DateTime.UtcNow,

                };

                User addedUser = await _userRepository.AddAsync(newUser);

                Restaurant restaurant = new()
                {
                    Id = Guid.NewGuid(),
                    RestaurantName = request.RestaurantName,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    Address = request.Address,
                    City = request.City,
                    Country = request.Country,
                    Hotline = request.Hotline,
                    Status = true,
                    IsRegistration = false,
                    CreatedBy = request.Username,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = request.Username,
                    UpdatedAt = DateTime.UtcNow,
                    AverageRating = 0.0f,
                    ImageList = "",
                    TotalRevenue = "",
                    DailyRevenue = "",
                    MonthlyRevenue = "",

                };

                Restaurant addedRestaurant = await _restaurantRepository.AddAsync(restaurant);

                addedUser.RestaurantId = addedRestaurant.Id;
                _ = await _userRepository.EditAsync(addedUser);

                RestaurantAddResponse response = new()
                {
                    Data = new List<RestaurantResponse>
            {
                new RestaurantResponse
                {
                    Id = addedRestaurant.Id,
                    RestaurantName = addedRestaurant.RestaurantName,
                    Latitude = addedRestaurant.Latitude,
                    Longitude = addedRestaurant.Longitude,
                    Address = addedRestaurant.Address,
                    City = addedRestaurant.City,
                    Country = addedRestaurant.Country,
                    Hotline = addedRestaurant.Hotline,
                    AverageRating = addedRestaurant.AverageRating,
                    ImageList = new List<string>(),
                    TotalRevenue = addedRestaurant.TotalRevenue,
                    DailyRevenue = addedRestaurant.DailyRevenue,
                    MonthlyRevenue = addedRestaurant.MonthlyRevenue,
                    IsRegistration = addedRestaurant.IsRegistration,
                    Status = addedRestaurant.Status,
                }
            }
                };

                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding restaurant: {ex.Message}");
                throw new Exception("An error occurred while saving the entity changes.");
            }
        }

        /// <summary>
        /// Update Restaurant
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<RestaurantUpdateResponse> UpdateRestaurantAsync(RestaurantUpdateRequest request)
        {
            Restaurant? existingRestaurant = await _restaurantRepository.GetByIdAsync(request.Id);
            if (existingRestaurant == null)
            {
                throw new InvalidOperationException("Failed to update restaurant: restaurant not found.");
            }

            existingRestaurant.RestaurantName = request.RestaurantName;
            existingRestaurant.Latitude = request.Latitude;
            existingRestaurant.Longitude = request.Longitude;
            existingRestaurant.Address = request.Address;
            existingRestaurant.City = request.City;
            existingRestaurant.Country = request.Country;
            existingRestaurant.Hotline = request.Hotline;
            existingRestaurant.IsRegistration = request.IsRegistration;

            bool isEdited = await _restaurantRepository.EditAsync(existingRestaurant);
            return !isEdited
                ? throw new InvalidOperationException("Failed to update restaurant.")
                : new RestaurantUpdateResponse
                {
                    Data = new List<RestaurantResponse>
        {
            new RestaurantResponse
            {
                Id = existingRestaurant.Id,
                RestaurantName = existingRestaurant.RestaurantName,
                Latitude = existingRestaurant.Latitude,
                Longitude = existingRestaurant.Longitude,
                Address = existingRestaurant.Address,
                City = existingRestaurant.City,
                Country = existingRestaurant.Country,
                Hotline = existingRestaurant.Hotline,
                AverageRating = existingRestaurant.AverageRating,
                DailyRevenue = existingRestaurant.DailyRevenue,
                MonthlyRevenue = existingRestaurant.MonthlyRevenue,
                TotalRevenue = existingRestaurant.TotalRevenue,
                IsRegistration = existingRestaurant.IsRegistration,
                Status = existingRestaurant.Status

            }
        }
                };
        }

        /// <summary>
        /// Approve Restaurant partner by Admin
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ApproveRestaurantPartnerResponse> ApproveRestaurantPartnerAsync(ApproveRestaurantPartnerRequest request)
        {
            try
            {
                ApproveRestaurantPartnerResponse response = await _restaurantRepository.ApproveRestaurantPartnerAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while approving restaurant partner.", ex);
            }
        }
    }
}
