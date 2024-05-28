using FooDrink.BussinessService.Service;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.Repository.Interface;
using Moq;

namespace FooDrink.Tests
{
    public class RestaurantServiceTests
    {
        private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly RestaurantService _restaurantService;

        public RestaurantServiceTests()
        {
            _restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _restaurantService = new RestaurantService(_restaurantRepositoryMock.Object, _userRepositoryMock.Object);
        }

        [Fact]
        public async Task GetRestaurantsAsync_ShouldReturnRestaurants_WhenCalled()
        {
            // Arrange
            RestaurantGetListRequest request = new()
            {
                PageSize = 10,
                PageIndex = 1,
                SearchString = "Test"
            };

            RestaurantGetListResponse response = new()
            {
                PageSize = 10,
                PageIndex = 1,
                SearchString = "Test",
                Data = new List<RestaurantResponse>
                {
                    new RestaurantResponse { Id = Guid.NewGuid(), RestaurantName = "Test Restaurant" }
                }
            };

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetRestaurantsAsync(It.IsAny<RestaurantGetListRequest>()))
                .ReturnsAsync(new List<RestaurantGetListResponse> { response });

            // Act
            RestaurantGetListResponse result = await _restaurantService.GetRestaurantsAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("Test Restaurant", result.Data.First().RestaurantName);
        }

        [Fact]
        public async Task GetRestaurantsByLocationAsync_ShouldReturnRestaurantsByLocation_WhenCalled()
        {
            // Arrange
            RestaurantGetByLocationRequest request = new() { Latitude = "10.762622", Longitude = "106.660172" };

            RestaurantGetByLocationResponse response = new()
            {
                Data = new List<RestaurantResponse>
                {
                    new RestaurantResponse { Id = Guid.NewGuid(), RestaurantName = "Location Restaurant" }
                }
            };

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetRestaurantsByLocationAsync(It.IsAny<RestaurantGetByLocationRequest>()))
                .ReturnsAsync(response);

            // Act
            RestaurantGetByLocationResponse result = await _restaurantService.GetRestaurantsByLocationAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("Location Restaurant", result.Data.First().RestaurantName);
        }

        [Fact]
        public async Task GetRestaurantByIdAsync_ShouldReturnRestaurant_WhenIdExists()
        {
            // Arrange
            Guid restaurantId = Guid.NewGuid();
            RestaurantGetByIdRequest request = new() { Id = restaurantId };

            Restaurant restaurant = new()
            {
                Id = restaurantId,
                RestaurantName = "Test Restaurant",
                Latitude = "0.0",
                Longitude = "0.0",
                Address = "123 Test St",
                City = "Test City",
                Country = "Test Country",
                Hotline = "123456789",
                AverageRating = 4.5f
            };

            List<string> imageUrls = new() { "url1", "url2" };

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(restaurant);

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetRestaurantImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(imageUrls);

            // Act
            RestaurantGetByIdResponse result = await _restaurantService.GetRestaurantByIdAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Restaurant", result.Data.RestaurantName);
            Assert.Equal(2, result.Data.ImageList.Count);
        }

        [Fact]
        public async Task DeleteRestaurantByIdAsync_ShouldReturnFalse_WhenRestaurantDoesNotExist()
        {
            // Arrange
            Guid restaurantId = Guid.NewGuid();

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((Restaurant)null);

            // Act
            bool result = await _restaurantService.DeleteRestaurantByIdAsync(restaurantId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task DeleteRestaurantByIdAsync_ShouldReturnTrue_WhenRestaurantExists()
        {
            // Arrange
            Guid restaurantId = Guid.NewGuid();
            Restaurant restaurant = new() { Id = restaurantId, Status = true };

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(restaurant);

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.EditAsync(It.IsAny<Restaurant>()))
                .ReturnsAsync(true);

            // Act
            bool result = await _restaurantService.DeleteRestaurantByIdAsync(restaurantId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddRestaurantAsync_ShouldAddRestaurantAndReturnResponse()
        {
            // Arrange
            RestaurantAddRequest request = new()
            {
                RestaurantName = "New Restaurant",
                Latitude = "0.0",
                Longitude = "0.0",
                Address = "123 New St",
                City = "New City",
                Country = "New Country",
                Hotline = "987654321",
                Username = "newuser",
                Password = "password",
                Email = "newuser@example.com"
            };

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
                CreatedBy = request.Username,
                CreatedAt = DateTime.UtcNow,
                UpdatedBy = request.Username,
                UpdatedAt = DateTime.UtcNow
            };

            Restaurant newRestaurant = new()
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
                AverageRating = 0.0f
            };

            _ = _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(newUser);

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Restaurant>()))
                .ReturnsAsync(newRestaurant);

            // Act
            RestaurantAddResponse result = await _restaurantService.AddRestaurantAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("New Restaurant", result.Data.First().RestaurantName);
        }

        [Fact]
        public async Task UpdateRestaurantAsync_ShouldUpdateRestaurantAndReturnResponse()
        {
            // Arrange
            Guid restaurantId = Guid.NewGuid();
            RestaurantUpdateRequest request = new()
            {
                Id = restaurantId,
                RestaurantName = "Updated Restaurant",
                Latitude = "0.1",
                Longitude = "0.1",
                Address = "123 Updated St",
                City = "Updated City",
                Country = "Updated Country",
                Hotline = "123123123",
                IsRegistration = true
            };

            Restaurant existingRestaurant = new()
            {
                Id = restaurantId,
                RestaurantName = "Old Restaurant",
                Latitude = "0.0",
                Longitude = "0.0",
                Address = "123 Old St",
                City = "Old City",
                Country = "Old Country",
                Hotline = "987654321",
                Status = true,
                IsRegistration = false,
                AverageRating = 4.5f,
                DailyRevenue = "100",
                MonthlyRevenue = "3000",
                TotalRevenue = "36000"
            };

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(existingRestaurant);

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.EditAsync(It.IsAny<Restaurant>()))
                .ReturnsAsync(true);

            // Act
            RestaurantUpdateResponse result = await _restaurantService.UpdateRestaurantAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("Updated Restaurant", result.Data.First().RestaurantName);
        }

        [Fact]
        public async Task ApproveRestaurantPartnerAsync_ShouldApproveRestaurantPartner_WhenCalled()
        {
            // Arrange
            ApproveRestaurantPartnerRequest request = new() { Id = Guid.NewGuid() };

            ApproveRestaurantPartnerResponse response = new()
            {
                Data = new List<RestaurantResponse>
        {
            new RestaurantResponse { Id = Guid.NewGuid(), RestaurantName = "Approved Restaurant" }
        },
                Message = "Approval successful"
            };

            _ = _restaurantRepositoryMock
                .Setup(repo => repo.ApproveRestaurantPartnerAsync(It.IsAny<ApproveRestaurantPartnerRequest>()))
                .ReturnsAsync(response);

            // Act
            ApproveRestaurantPartnerResponse result = await _restaurantService.ApproveRestaurantPartnerAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("Approved Restaurant", result.Data.First().RestaurantName);
            Assert.Equal("Approval successful", result.Message);
        }

    }
}
