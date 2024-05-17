using FooDrink.API.Configuration;
using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Review;
using FooDrink.DTO.Response.Review;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        private readonly ApppSettingConfig _appSettingConfig;

        public ReviewController(IReviewService reviewService, ApppSettingConfig appSettingConfig)
        {
            _reviewService = reviewService;
            _appSettingConfig = appSettingConfig;
        }

        /// <summary>
        /// AddReviewAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddReviewAsync(ReviewAddRequest request)
        {
            try
            {
                ReviewAddResponse response = await _reviewService.AddReviewAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// UpdateReviewAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateReviewAsync(ReviewUpdateRequest request)
        {
            try
            {
                ReviewUpdateResponse response = await _reviewService.UpdateReviewAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// DeleteReviewAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteReviewAsync(ReviewDeleteRequest request)
        {
            try
            {
                ReviewDeleteResponse response = await _reviewService.DeleteReviewAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetReviewByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetReviewByIdAsync([FromQuery] Guid id)
        {
            try
            {
                ReviewGetByIdRequest request = new() { Id = id };
                ReviewGetByIdResponse response = await _reviewService.GetReviewByIdAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetReviewsByUserIdAsync
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("get-by-user/{userId}")]
        public async Task<IActionResult> GetReviewsByUserIdAsync([FromQuery] Guid userId)
        {
            try
            {
                ReviewGetByUserIdRequest request = new() { UserId = userId };
                ReviewGetByUserIdResponse response = await _reviewService.GetReviewsByUserIdAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetReviewsByRestaurantIdAsync
        /// </summary>
        /// <param name="restaurantId"></param>
        /// <returns></returns>
        [HttpGet("get-by-restaurant/{restaurantId}")]
        public async Task<IActionResult> GetReviewsByRestaurantIdAsync([FromQuery] Guid restaurantId)
        {
            try
            {
                ReviewGetByRestaurantIdRequest request = new() { RestaurantId = restaurantId };
                ReviewGetByRestaurantIdResponse response = await _reviewService.GetReviewsByRestaurantIdAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// GetReviewsAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-list")]
        public async Task<IActionResult> GetReviewsAsync([FromQuery] ReviewGetListRequest request)
        {
            try
            {
                ReviewGetListResponse response = await _reviewService.GetReviewsAsync(request);
                AddDomainToImageUrls(response.Data);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// AddDomainToImageUrls
        /// </summary>
        /// <param name="reviewResponses"></param>
        private void AddDomainToImageUrls(List<ReviewResponse> reviewResponses)
        {
            foreach (ReviewResponse reviewResponse in reviewResponses)
            {
                reviewResponse.ImageList = reviewResponse.ImageList.Select(img => _appSettingConfig.Domain + img).ToList();
            }
        }

        [HttpPost("toggle-reaction")]
        public async Task<IActionResult> ToggleReactionAsync([FromBody] ToggleReactionRequest request)
        {
            try
            {
                await _reviewService.ToggleReactionAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
