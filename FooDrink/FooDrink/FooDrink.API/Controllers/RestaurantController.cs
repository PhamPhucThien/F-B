using FooDrink.API.Configuration;
using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
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
        public async Task<ActionResult<RestaurantGetByIdResponse>> GetRestaurantByIdAsync(Guid id)
        {
            try
            {
                RestaurantGetByIdRequest request = new() { Id = id };
                RestaurantGetByIdResponse restaurant = await _restaurantService.GetRestaurantByIdAsync(request);
                return restaurant == null ? (ActionResult<RestaurantGetByIdResponse>)NotFound() : (ActionResult<RestaurantGetByIdResponse>)Ok(restaurant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
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
        public async Task<ActionResult<RestaurantUpdateResponse>> UpdateRestaurantAsync(Guid id, RestaurantUpdateRequest request)
        {
            try
            {
                if (id != request.Id)
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
        public async Task<ActionResult> DeleteRestaurantByIdAsync(Guid id)
        {
            try
            {
                bool result = await _restaurantService.DeleteRestaurantByIdAsync(id);
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
        public async Task<IActionResult> ApproveRestaurantPartner(Guid id, [FromBody] ApproveRestaurantPartnerRequest request)
        {
            try
            {
                request.Id = id;
                ApproveRestaurantPartnerResponse response = await _restaurantService.ApproveRestaurantPartnerAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
