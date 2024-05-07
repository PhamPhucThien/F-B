using System;
using System.Threading.Tasks;
using FooDrink.BussinessService.Service;
using FooDrink.DTO.Request.Product;
using FooDrink.DTO.Request.User;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        }
        [HttpGet("GetListUser")]
        public IActionResult GetListUser([FromQuery] UserGetListRequest request)
        {
            try
            {
                var productList = _userService.GetAllUsers(request);
                return Ok(productList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
