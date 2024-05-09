using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;

        public RestaurantController(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet("GetListRestaurant")]
        public async Task<IActionResult> GetRestaurants([FromQuery] RestaurantGetListRequest request)
        {
            try
            {
                RestaurantGetListResponse response = await _restaurantService.GetRestaurantsAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while fetching restaurants: {ex.Message}");
            }
        }


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

        [HttpPut("UpdateRestaurant{id}")]
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
    }
}
