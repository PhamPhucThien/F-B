using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.User;
using FooDrink.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] UserAddRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserAddResponse response = await _userService.AddUserAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while adding the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Block user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete/{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                bool result = await _userService.DeleteUserIdAsync(id);
                return result ? Ok(new { message = "User successfully deleted." }) : NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get/{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                UserGetByIdRequest request = new() { Id = id };
                UserGetByIdResponse response = await _userService.GetUserByIdAsync(request);

                return response.Data.Count > 0 ? Ok(response) : NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("getAll")]
        public async Task<IActionResult> GetUsers([FromQuery] UserGetListRequest request)
        {
            try
            {
                UserGetListResponse response = await _userService.GetUsersAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users.", error = ex.Message });
            }
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                UserUpdateResponse response = await _userService.UpdateUserAsync(request);

                return response.Data.Count > 0 ? Ok(response) : NotFound(new { message = "User not found." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user.", error = ex.Message });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-user-by-jwt")]
        [Authorize]
        public async Task<IActionResult> GetByJwt()
        {
            try
            {
                Guid userId = Guid.Parse(HttpContext.GetName());

                UserGetByIdRequest request = new() { Id = userId };
                var response = await _userService.GetUserByIdAsync(request);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
