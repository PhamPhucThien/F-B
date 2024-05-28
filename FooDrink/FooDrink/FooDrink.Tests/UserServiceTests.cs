using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.User;
using FooDrink.Repository.Interface;
using Moq;

namespace FooDrink.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task AddUserAsync_ShouldReturnUserAddResponse_WhenUserIsAdded()
        {
            // Arrange
            UserAddRequest request = new()
            {
                Username = "testuser",
                Password = "testpassword",
                Email = "test@example.com",
                FullName = "Test User",
                PhoneNumber = "1234567890",
                Address = "123 Test St"
            };

            User addedUser = new()
            {
                Id = Guid.NewGuid(),
                Username = "testuser",
                Password = "testpassword",
                Email = "test@example.com",
                FullName = "Test User",
                PhoneNumber = "1234567890",
                Address = "123 Test St",
                FavoritedList = " ",
                Role = "Customer",
                RestaurantId = Guid.NewGuid(),
                Image = "",
                Status = true,
                CreatedAt = DateTime.Now,
                CreatedBy = "testuser",
                UpdatedAt = DateTime.Now,
                UpdatedBy = "testuser"
            };

            _ = _userRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(addedUser);

            // Act
            UserAddResponse result = await _userService.AddUserAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("testuser", result.Data.First().Username);
        }

        [Fact]
        public async Task DeleteUserIdAsync_ShouldReturnTrue_WhenUserIsDeleted()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _ = _userRepositoryMock
                .Setup(repo => repo.DeleteByIdAsync(userId))
                .ReturnsAsync(true);

            // Act
            bool result = await _userService.DeleteUserIdAsync(userId);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeleteUserIdAsync_ShouldReturnFalse_WhenUserIsNotDeleted()
        {
            // Arrange
            Guid userId = Guid.NewGuid();

            _ = _userRepositoryMock
                .Setup(repo => repo.DeleteByIdAsync(userId))
                .ReturnsAsync(false);

            // Act
            bool result = await _userService.DeleteUserIdAsync(userId);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnUserGetByIdResponse_WhenUserIsFound()
        {
            // Arrange
            UserGetByIdRequest request = new() { Id = Guid.NewGuid() };

            User user = new()
            {
                Id = request.Id,
                Username = "testuser",
                Password = "testpassword",
                Email = "test@example.com",
                FullName = "Test User",
                PhoneNumber = "1234567890",
                Address = "123 Test St",
                FavoritedList = " ",
                Status = true
            };

            _ = _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(user);

            // Act
            UserGetByIdResponse result = await _userService.GetUserByIdAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("testuser", result.Data.First().Username);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnEmptyResponse_WhenUserIsNotFound()
        {
            // Arrange
            UserGetByIdRequest request = new() { Id = Guid.NewGuid() };

            _ = _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync((User)null);

            // Act
            UserGetByIdResponse result = await _userService.GetUserByIdAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result.Data);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnUserGetListResponse_WhenUsersAreFound()
        {
            // Arrange
            UserGetListRequest request = new() { PageSize = 10, PageIndex = 1, SearchString = "test" };

            List<UserGetListResponse> userResponses = new()
            {
                new UserGetListResponse
                {
                    Data = new List<UserResponse>
                    {
                        new UserResponse
                        {
                            Id = Guid.NewGuid(),
                            Username = "testuser",
                            Email = "test@example.com",
                            FullName = "Test User",
                            PhoneNumber = "1234567890",
                            Address = "123 Test St",
                            FavoritedList = " ",
                            Status = true
                        }
                    }
                }
            };

            _ = _userRepositoryMock
                .Setup(repo => repo.GetUsersAsync(request))
                .ReturnsAsync(userResponses);

            // Act
            UserGetListResponse result = await _userService.GetUsersAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("testuser", result.Data.First().Username);
        }

        [Fact]
        public async Task UpdateUserAsync_ShouldReturnUserUpdateResponse_WhenUserIsUpdated()
        {
            // Arrange
            UserUpdateRequest request = new()
            {
                Id = Guid.NewGuid(),
                Username = "updateduser",
                Password = "updatedpassword",
                Email = "updated@example.com",
                FullName = "Updated User",
                PhoneNumber = "0987654321",
                Address = "456 Updated St",
                FavoritedList = "updated"
            };

            User user = new()
            {
                Id = request.Id,
                Username = "testuser",
                Password = "testpassword",
                Email = "test@example.com",
                FullName = "Test User",
                PhoneNumber = "1234567890",
                Address = "123 Test St",
                FavoritedList = " ",
                Status = true
            };

            _ = _userRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(user);

            _ = _userRepositoryMock
                .Setup(repo => repo.EditAsync(It.IsAny<User>()))
                .ReturnsAsync(true);

            // Act
            UserUpdateResponse result = await _userService.UpdateUserAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal("updateduser", result.Data.First().Username);
        }
    }
}
