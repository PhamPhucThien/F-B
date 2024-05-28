using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Review;
using FooDrink.DTO.Response.Review;
using FooDrink.Repository.Interface;
using Moq;

namespace FooDrink.Tests
{
    public class ReviewServiceTests
    {
        private readonly Mock<IUserReviewReactionRepository> _userReviewReactionRepositoryMock;
        private readonly Mock<IReviewRepository> _reviewRepositoryMock;
        private readonly IReviewService _reviewService;

        public ReviewServiceTests()
        {
            _userReviewReactionRepositoryMock = new Mock<IUserReviewReactionRepository>();
            _reviewRepositoryMock = new Mock<IReviewRepository>();
            _reviewService = new ReviewService(_userReviewReactionRepositoryMock.Object, _reviewRepositoryMock.Object);
        }

        [Fact]
        public async Task AddReviewAsync_ShouldReturnReviewAddResponse_WhenReviewIsAdded()
        {
            // Arrange
            ReviewAddRequest request = new()
            {
                UserId = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                Content = "Great food!",
                Rating = 5,
                Reaction = 0
            };

            Review addedReview = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                RestaurantId = request.RestaurantId,
                Content = request.Content,
                Rating = request.Rating,
                Reaction = request.Reaction,
                Status = true,
                CreatedBy = request.UserId.ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.AddAsync(It.IsAny<Review>()))
                .ReturnsAsync(addedReview);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            // Act
            ReviewAddResponse result = await _reviewService.AddReviewAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal(addedReview.Id, result.Data[0].Id);
            Assert.Equal("Great food!", result.Data[0].Content);
            Assert.Equal(5, result.Data[0].Rating);
            Assert.Equal(0, result.Data[0].Reaction);
        }

        [Fact]
        public async Task UpdateReviewAsync_ShouldReturnReviewUpdateResponse_WhenReviewIsUpdated()
        {
            // Arrange
            ReviewUpdateRequest request = new()
            {
                Id = Guid.NewGuid(),
                Content = "Updated review",
                Rating = 4,
                Reaction = 10
            };

            Review existingReview = new()
            {
                Id = request.Id,
                UserId = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                Content = "Great food!",
                Rating = 5,
                Reaction = 0,
                Status = true,
                CreatedBy = "User1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(existingReview);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.EditAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            // Act
            ReviewUpdateResponse result = await _reviewService.UpdateReviewAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal(existingReview.Id, result.Data[0].Id);
            Assert.Equal("Updated review", result.Data[0].Content);
            Assert.Equal(4, result.Data[0].Rating);
            Assert.Equal(10, result.Data[0].Reaction);
        }


        [Fact]
        public async Task DeleteReviewAsync_ShouldReturnReviewDeleteResponse_WhenReviewIsDeleted()
        {
            // Arrange
            Guid reviewId = Guid.NewGuid();
            Review existingReview = new()
            {
                Id = reviewId,
                UserId = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                Content = "Great food!",
                Rating = 5,
                Reaction = 0,
                Status = true,
                CreatedBy = "User1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByIdAsync(reviewId))
                .ReturnsAsync(existingReview);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.EditAsync(existingReview))
                .ReturnsAsync(true);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(reviewId))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            ReviewDeleteRequest request = new() { Id = reviewId };

            // Act
            ReviewDeleteResponse result = await _reviewService.DeleteReviewAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.False(result.Data[0].Status);
        }


        [Fact]
        public async Task GetReviewByIdAsync_ShouldReturnReviewGetByIdResponse_WhenReviewIsFound()
        {
            // Arrange
            ReviewGetByIdRequest request = new()
            {
                Id = Guid.NewGuid()
            };

            Review existingReview = new()
            {
                Id = request.Id,
                UserId = Guid.NewGuid(),
                RestaurantId = Guid.NewGuid(),
                Content = "Great food!",
                Rating = 5,
                Reaction = 0,
                Status = true,
                CreatedBy = "User1",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.Id))
                .ReturnsAsync(existingReview);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            // Act
            ReviewGetByIdResponse result = await _reviewService.GetReviewByIdAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal(existingReview.Id, result.Data[0].Id);
            Assert.Equal("Great food!", result.Data[0].Content);
        }

        [Fact]
        public async Task GetReviewsByUserIdAsync_ShouldReturnReviewGetByUserIdResponse_WhenReviewsAreFound()
        {
            // Arrange
            ReviewGetByUserIdRequest request = new()
            {
                UserId = Guid.NewGuid()
            };

            List<Review> reviews = new()
            {
                new Review
                {
                    Id = Guid.NewGuid(),
                    UserId = request.UserId,
                    RestaurantId = Guid.NewGuid(),
                    Content = "Great food!",
                    Rating = 5,
                    Reaction = 0,
                    Status = true,
                    CreatedBy = "User1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByUserIdAsync(request.UserId))
                .ReturnsAsync(reviews);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            // Act
            ReviewGetByUserIdResponse result = await _reviewService.GetReviewsByUserIdAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal(reviews[0].Id, result.Data[0].Id);
        }

        [Fact]
        public async Task GetReviewsByRestaurantIdAsync_ShouldReturnReviewGetByRestaurantIdResponse_WhenReviewsAreFound()
        {
            // Arrange
            ReviewGetByRestaurantIdRequest request = new()
            {
                RestaurantId = Guid.NewGuid()
            };

            List<Review> reviews = new()
            {
                new Review
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    RestaurantId = request.RestaurantId,
                    Content = "Great food!",
                    Rating = 5,
                    Reaction = 0,
                    Status = true,
                    CreatedBy = "User1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByRestaurantIdAsync(request.RestaurantId))
                .ReturnsAsync(reviews);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            // Act
            ReviewGetByRestaurantIdResponse result = await _reviewService.GetReviewsByRestaurantIdAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal(reviews[0].Id, result.Data[0].Id);
        }

        [Fact]
        public async Task GetReviewsAsync_ShouldReturnReviewGetListResponse_WhenReviewsAreFound()
        {
            // Arrange
            ReviewGetListRequest request = new()
            {
                PageSize = 10,
                PageIndex = 1,
                SearchString = ""
            };

            List<Review> reviews = new()
            {
                new Review
                {
                    Id = Guid.NewGuid(),
                    UserId = Guid.NewGuid(),
                    RestaurantId = Guid.NewGuid(),
                    Content = "Great food!",
                    Rating = 5,
                    Reaction = 0,
                    Status = true,
                    CreatedBy = "User1",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }
            };

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetWithPaging(It.IsAny<ReviewGetListRequest>()))
                .Returns(reviews);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetReviewImageUrlsAsync(It.IsAny<Guid>()))
                .ReturnsAsync(new List<string> { "image1.png", "image2.png" });

            // Act
            ReviewGetListResponse result = await _reviewService.GetReviewsAsync(request);

            // Assert
            Assert.NotNull(result);
            _ = Assert.Single(result.Data);
            Assert.Equal(reviews[0].Id, result.Data[0].Id);
        }

        [Fact]
        public async Task ToggleReactionAsync_ShouldAddReaction_WhenReactionDoesNotExist()
        {
            // Arrange
            ToggleReactionRequest request = new()
            {
                UserId = Guid.NewGuid(),
                ReviewId = Guid.NewGuid()
            };

            _ = _userReviewReactionRepositoryMock
                .Setup(repo => repo.GetUserReviewReactionAsync(request))
                .ReturnsAsync((UserReviewReaction)null);

            _ = _userReviewReactionRepositoryMock
                .Setup(repo => repo.AddUserReviewReactionAsync(It.IsAny<UserReviewReaction>()))
                .Returns(Task.CompletedTask);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.ReviewId))
                .ReturnsAsync(new Review
                {
                    Id = request.ReviewId,
                    Reaction = 0
                });

            _ = _userReviewReactionRepositoryMock
                .Setup(repo => repo.GetReactionCountAsync(request.ReviewId))
                .ReturnsAsync(1);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.EditAsync(It.IsAny<Review>()))
                .ReturnsAsync(true);

            // Act
            await _reviewService.ToggleReactionAsync(request);

            // Assert
            _userReviewReactionRepositoryMock.Verify(repo => repo.AddUserReviewReactionAsync(It.IsAny<UserReviewReaction>()), Times.Once);
        }


        [Fact]
        public async Task ToggleReactionAsync_ShouldRemoveReaction_WhenReactionExists()
        {
            // Arrange
            ToggleReactionRequest request = new()
            {
                UserId = Guid.NewGuid(),
                ReviewId = Guid.NewGuid()
            };

            UserReviewReaction existingReaction = new()
            {
                UserId = request.UserId,
                ReviewId = request.ReviewId,
                IsLiked = true
            };

            _ = _userReviewReactionRepositoryMock
                .Setup(repo => repo.GetUserReviewReactionAsync(request))
                .ReturnsAsync(existingReaction);

            _ = _userReviewReactionRepositoryMock
                .Setup(repo => repo.RemoveUserReviewReactionAsync(It.IsAny<UserReviewReaction>()))
                .Returns(Task.CompletedTask);

            _ = _reviewRepositoryMock
                .Setup(repo => repo.GetByIdAsync(request.ReviewId))
                .ReturnsAsync(new Review
                {
                    Id = request.ReviewId,
                    Reaction = 1
                });

            _ = _userReviewReactionRepositoryMock
                .Setup(repo => repo.GetReactionCountAsync(request.ReviewId))
                .ReturnsAsync(0);

            _ = _reviewRepositoryMock
            .Setup(repo => repo.EditAsync(It.IsAny<Review>()))
            .Returns(Task.FromResult(true));

            // Act
            await _reviewService.ToggleReactionAsync(request);

            // Assert
            _userReviewReactionRepositoryMock.Verify(repo => repo.RemoveUserReviewReactionAsync(existingReaction), Times.Once);
            _userReviewReactionRepositoryMock.Verify(repo => repo.AddUserReviewReactionAsync(It.IsAny<UserReviewReaction>()), Times.Never);
        }

    }
}
