using FooDrink.BussinessService.Service;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Response.Authentication;
using FooDrink.Repository.Interface;
using Moq;

namespace FooDrink.Tests
{
    public class AuthenticationServiceTests
    {
        private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
        private readonly Mock<IAuthenticationRepository> _authenticationRepositoryMock;
        private readonly AuthenticationService _authenticationService;

        public AuthenticationServiceTests()
        {
            _jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
            _authenticationRepositoryMock = new Mock<IAuthenticationRepository>();
            _authenticationService = new AuthenticationService(_authenticationRepositoryMock.Object, _jwtTokenGeneratorMock.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnToken_WhenUserExists()
        {
            // Arrange
            LoginRequest request = new() { Username = "existingUser", Password = "password123" };
            User existingUser = new() { Id = Guid.NewGuid(), Username = "existingUser", FullName = "Existing User" };
            const string expectedToken = "testToken123";

            _ = _authenticationRepositoryMock.Setup(repo => repo.GetByUsernameAndPassword(request.Username, request.Password)).ReturnsAsync(existingUser);
            _ = _jwtTokenGeneratorMock.Setup(generator => generator.GenerateToken(existingUser.Id, existingUser.FullName)).Returns(expectedToken);

            // Act
            AuthenticationResponse result = await _authenticationService.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual("", result.Token);
            Assert.Equal("", result.Message);
        }


        [Fact]
        public async Task Login_ShouldReturnErrorMessage_WhenUserDoesNotExist()
        {
            // Arrange
            LoginRequest request = new() { Username = "nonExistingUser", Password = "password123" };

            _ = _authenticationRepositoryMock.Setup(repo => repo.GetByUsernameAndPassword(request.Username, request.Password)).ReturnsAsync((User)null);

            // Act
            AuthenticationResponse result = await _authenticationService.Login(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("", result.Token);
            Assert.Equal("Wrong username or password", result.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnToken_WhenUsernameIsAvailable()
        {
            // Arrange
            RegisterRequest request = new()
            {
                Username = "newUser",
                Password = "password123",
                Fullname = "New User",
                Email = "newuser@example.com",
                Address = "123 Street, City"
            };
            string expectedToken = "testToken123";

            _ = _authenticationRepositoryMock.Setup(repo => repo.GetByUsername(request.Username)).ReturnsAsync((User)null);
            _ = _jwtTokenGeneratorMock.Setup(generator => generator.GenerateToken(It.IsAny<Guid>(), "Customer")).Returns(expectedToken);

            // Act
            AuthenticationResponse result = await _authenticationService.Register(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedToken, result.Token);
            Assert.Equal("", result.Message);
        }

        [Fact]
        public async Task Register_ShouldReturnErrorMessage_WhenUsernameIsNotAvailable()
        {
            // Arrange
            RegisterRequest request = new() { Username = "existingUser" };

            _ = _authenticationRepositoryMock.Setup(repo => repo.GetByUsername(request.Username)).ReturnsAsync(new User());

            // Act
            AuthenticationResponse result = await _authenticationService.Register(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("", result.Token);
            Assert.Equal("Username has already existed", result.Message);
        }

    }
}
