using FooDrink.BussinessService.Interface;
using FooDrink.DTO.Response.Menu;
using FooDrink.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        /// <summary>
        /// Get list of menu with page and size value
        /// </summary>
        /// <param name="id"></param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet("GetInRangeWithPaging")]
        public async Task<IActionResult> Get([FromQuery] Guid id, [FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                MenuGetResponse response = await _service.Get(id, page, size);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove menu with menu's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("RemoveById")]
        public async Task<IActionResult> RemoveById([FromQuery] Guid id)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                bool response = await _service.RemoveById(userId, id);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("UpdateById")]
        public IActionResult UpdateById([FromBody] MenuUpdateByIdRequest request)
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                //var response = await _service.RemoveById(userId, id);

                return Ok(userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
