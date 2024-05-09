using FooDrink.BusinessService.Interface;
using FooDrink.DTO.Request.Restaurant;
using FooDrink.DTO.Response.Restaurant;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooDrink.Controllers
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
        public async Task<ActionResult<IEnumerable<RestaurantGetListResponse>>> GetRestaurantsAsync([FromQuery] RestaurantGetListRequest request)
        {
            try
            {
                var restaurants = await _restaurantService.GetRestaurantsAsync(request);
                return Ok(restaurants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantGetByIdResponse>> GetRestaurantByIdAsync(Guid id)
        {
            try
            {
                var request = new RestaurantGetByIdRequest { Id = id };
                var restaurant = await _restaurantService.GetRestaurantByIdAsync(request);
                if (restaurant == null)
                {
                    return NotFound();
                }
                return Ok(restaurant);
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
                var addedRestaurant = await _restaurantService.AddRestaurantAsync(request);
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

                var updatedRestaurant = await _restaurantService.UpdateRestaurantAsync(request);
                if (updatedRestaurant == null)
                {
                    return NotFound();
                }
                return Ok(updatedRestaurant);
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
                var result = await _restaurantService.DeleteRestaurantByIdAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
