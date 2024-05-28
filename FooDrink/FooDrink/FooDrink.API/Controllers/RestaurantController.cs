using FooDrink.API.Configuration;
using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using FooDrink.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    /// <summary>
    /// Controller for handling restaurant-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly ApppSettingConfig _appSettingConfig;

        public RestaurantController(IRestaurantService restaurantService, ApppSettingConfig apppSettingConfig)
        {
            _restaurantService = restaurantService;
            _appSettingConfig = apppSettingConfig;
        }

        /// <summary>
        /// Get a list of restaurants based on the provided criteria.
        /// </summary>
        [HttpGet("GetListRestaurant")]
        public async Task<IActionResult> GetRestaurants([FromQuery] RestaurantGetListRequest request)
        {
            try
            {
                RestaurantGetListResponse response = await _restaurantService.GetRestaurantsAsync(request);
                List<RestaurantResponse> listData = response.Data;

                foreach (RestaurantResponse item in listData)
                {
                    item.ImageList = item.ImageList.Select(img => _appSettingConfig.Domain + img).ToList();
                }
                response.Data = listData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching restaurants: {ex.Message}");
            }
        }

        /// <summary>
        /// Search restaurants by location coordinates.
        /// </summary>
        [HttpGet("SearchByLocation")]
        public async Task<ActionResult<RestaurantGetByLocationResponse>> SearchByLocationAsync([FromQuery] RestaurantGetByLocationRequest request)
        {
            try
            {
                RestaurantGetByLocationResponse response = await _restaurantService.GetRestaurantsByLocationAsync(request);
                List<RestaurantResponse> listData = response.Data;

                foreach (RestaurantResponse item in listData)
                {
                    item.ImageList = item.ImageList.Select(img => _appSettingConfig.Domain + img).ToList();
                }
                response.Data = listData;
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get restaurant details by ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRestaurantByIdAsync(Guid id)
        {
            try
            {
                RestaurantGetByIdRequest request = new() { Id = id };
                RestaurantGetByIdResponse response = await _restaurantService.GetRestaurantByIdAsync(request);
                response.Data.ImageList = response.Data.ImageList
                .Select(img => _appSettingConfig.Domain + img)
                .ToList();
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// Add a new restaurant.
        /// </summary>
        [HttpPost("AddRestaurant")]
        public async Task<ActionResult<RestaurantAddResponse>> AddRestaurantAsync(RestaurantAddRequest request)
        {
            try
            {
                RestaurantAddResponse addedRestaurant = await _restaurantService.AddRestaurantAsync(request);
                return addedRestaurant;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update an existing restaurant.
        /// </summary>
        [HttpPut("UpdateRestaurant")]
        public async Task<ActionResult<RestaurantUpdateResponse>> UpdateRestaurantAsync(string id, RestaurantUpdateRequest request)
        {
            try
            {
                if (Guid.Parse(id) != request.Id)
                {
                    return BadRequest("Id mismatch between request parameter and request body.");
                }
                RestaurantUpdateResponse updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(request);
                return updatedRestaurant == null ? (ActionResult<RestaurantUpdateResponse>)NotFound() : (ActionResult<RestaurantUpdateResponse>)Ok(updatedRestaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a restaurant by ID.
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRestaurantByIdAsync(string id)
        {
            try
            {
                bool result = await _restaurantService.DeleteRestaurantByIdAsync(Guid.Parse(id));
                return !result ? NotFound() : NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Approve or disapprove a restaurant partner.
        /// </summary>
        [HttpPut("approve")]
        public async Task<IActionResult> ApproveRestaurantPartner(string id, [FromBody] ApproveRestaurantPartnerRequest request)
        {
            try
            {
                request.Id = Guid.Parse(id);
                ApproveRestaurantPartnerResponse response = await _restaurantService.ApproveRestaurantPartnerAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// get restaurant with jwt token
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-restaurant-by-jwt")]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> GetByJwt()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                var response = await _restaurantService.GetByJwt(userId);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
