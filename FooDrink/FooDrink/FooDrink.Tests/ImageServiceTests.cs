using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.DTO.Request.Image;
using FooDrink.DTO.Response.Image;
using FooDrink.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FooDrink.Tests
{
    public class ImageServiceTests
    {
        private readonly Mock<IHandleImageRepository> _handleImageRepositoryMock;
        private readonly IImageService _imageService;

        public ImageServiceTests()
        {
            _handleImageRepositoryMock = new Mock<IHandleImageRepository>();
            _imageService = new ImageService(_handleImageRepositoryMock.Object);
        }

        [Fact]
        public async Task UploadImagesAsync_ShouldReturnUploadImageResponse_WhenImagesAreUploaded()
        {
            // Arrange
            Guid entityId = Guid.NewGuid();
            string entityType = "Restaurant";
            string webRootPath = "wwwroot/images";

            // Mock IFormFile list
            Mock<IFormFile> fileMock1 = new();
            _ = fileMock1.Setup(_ => _.FileName).Returns("image1.png");
            Mock<IFormFile> fileMock2 = new();
            _ = fileMock2.Setup(_ => _.FileName).Returns("image2.png");
            List<IFormFile> files = new() { fileMock1.Object, fileMock2.Object };

            UploadImageRequest request = new()
            {
                EntityId = entityId,
                EntityType = entityType,
                Images = files
            };

            UploadImageResponse expectedResponse = new()
            {
                Success = true,
                ImageUrls = new List<string> { "url1", "url2" }
            };

            _ = _handleImageRepositoryMock
                .Setup(repo => repo.UploadImagesAsync(request, webRootPath))
                .ReturnsAsync(expectedResponse);

            // Act
            UploadImageResponse result = await _imageService.UploadImagesAsync(request, webRootPath);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(2, result.ImageUrls.Count);
            Assert.Equal("url1", result.ImageUrls[0]);
            Assert.Equal("url2", result.ImageUrls[1]);
        }

        [Fact]
        public async Task GetEntityImageListAsync_ShouldReturnImageList_WhenImagesExist()
        {
            // Arrange
            string rootPath = "wwwroot/images";
            string entityType = "Restaurant";
            Guid entityId = Guid.NewGuid();

            List<string> expectedImageList = new() { "image1.png", "image2.png" };

            _ = _handleImageRepositoryMock
                .Setup(repo => repo.GetEntityImageListAsync(rootPath, entityType, entityId))
                .ReturnsAsync(expectedImageList);

            // Act
            List<string> result = await _imageService.GetEntityImageListAsync(rootPath, entityType, entityId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("image1.png", result[0]);
            Assert.Equal("image2.png", result[1]);
        }
    }
}
