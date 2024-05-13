using System;
using System.Threading.Tasks;
using FooDrink.BussinessService.Service;
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

        /// <summary>
        /// Get list of users.
        /// </summary>
        [HttpGet("GetListUser")]
        public async Task<IActionResult> GetListUserAsync([FromQuery] UserGetListRequest request)
        {
            try
            {
                var userListResponse = await _userService.GetUsersAsync(request);
                return Ok(userListResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Get a user by their ID.
        /// </summary>
        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUserByIdAsync(Guid id)
        {
            try
            {
                var userGetByIdRequest = new UserGetByIdRequest { Id = id };
                var userGetByIdResponse = await _userService.GetUserByIdAsync(userGetByIdRequest);
                return Ok(userGetByIdResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUserAsync([FromBody] UserAddRequest request)
        {
            try
            {
                var userAddResponse = await _userService.AddUserAsync(request);
                return Ok(userAddResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Delete a user by their ID.
        /// </summary>
        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            try
            {
                var isDeleted = await _userService.DeleteUserIdAsync(id);
                return Ok(isDeleted);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        /// <summary>
        /// Update a user.
        /// </summary>
        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync([FromBody] UserUpdateRequest request)
        {
            try
            {
                var userUpdateResponse = await _userService.UpdateUserAsync(request);
                return Ok(userUpdateResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
