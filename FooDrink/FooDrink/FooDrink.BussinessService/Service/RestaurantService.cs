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

        public RestaurantService(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

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

        public async Task<RestaurantGetByLocationResponse> GetRestaurantsByLocationAsync(RestaurantGetByLocationRequest request)
        {
            try
            {
                var response = await _restaurantRepository.GetRestaurantsByLocationAsync(request);
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching restaurants by location.", ex);
            }
        }

        public async Task<RestaurantGetByIdResponse?> GetRestaurantByIdAsync(RestaurantGetByIdRequest request)
        {
            try
            {
                var restaurant = await _restaurantRepository.GetByIdAsync(request.Id);

                if (restaurant == null)
                {
                    return null;
                }

                return new RestaurantGetByIdResponse
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
                    IsRegistration = restaurant.IsRegistration,
                }
            }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the restaurant by id.", ex);
            }
        }

        public async Task<bool> DeleteRestaurantByIdAsync(Guid id)
        {
            var restaurant = await _restaurantRepository.GetByIdAsync(id);
            if (restaurant == null)
            {
                return false;
            }
            restaurant.Status = true;
            bool result = await _restaurantRepository.EditAsync(restaurant);
            return result;
        }

        public async Task<RestaurantAddResponse> AddRestaurantAsync(RestaurantAddRequest request)
        {
            if (string.IsNullOrEmpty(request.RestaurantName) || string.IsNullOrEmpty(request.Latitude) ||
                string.IsNullOrEmpty(request.Longitude) || string.IsNullOrEmpty(request.Address) ||
                string.IsNullOrEmpty(request.City) || string.IsNullOrEmpty(request.Country) ||
                string.IsNullOrEmpty(request.Hotline))
            {
                throw new ArgumentException("Invalid input data. All required fields must be provided.");
            }

            try
            {
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
                    Status = false,
                    IsRegistration = false,
                    CreatedBy = "",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedBy = "",
                    UpdatedAt = DateTime.UtcNow,
                    AverageRating = 0.0f,
                    ImageList = "",
                    TotalRevenue = "",
                    DailyRevenue = "",
                    MonthlyRevenue = ""
                };

                Restaurant addedRestaurant = await _restaurantRepository.AddAsync(restaurant);

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
                            ImageList = addedRestaurant.ImageList,
                            TotalRevenue = addedRestaurant.TotalRevenue,
                            DailyRevenue = addedRestaurant.DailyRevenue,
                            MonthlyRevenue = addedRestaurant.MonthlyRevenue,
                            IsRegistration = addedRestaurant.IsRegistration,

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

        public async Task<RestaurantUpdateResponse> UpdateRestaurantAsync(RestaurantUpdateRequest request)
        {
            var existingRestaurant = await _restaurantRepository.GetByIdAsync(request.Id);
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
            if (!isEdited)
            {
                throw new InvalidOperationException("Failed to update restaurant.");
            }

            return new RestaurantUpdateResponse
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
                IsRegistration = existingRestaurant.IsRegistration
            }
        }
            };
        }

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
