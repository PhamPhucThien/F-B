using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.User;

namespace FooDrink.BussinessService.Interface
{
    /// <summary>
    /// Interface for handling user-related operations.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get a list of users based on the provided criteria.
        /// </summary>
        Task<UserGetListResponse> GetUsersAsync(UserGetListRequest request);

        /// <summary>
        /// Get user details by ID.
        /// </summary>
        Task<UserGetByIdResponse> GetUserByIdAsync(UserGetByIdRequest request);

        /// <summary>
        /// Delete a user by ID.
        /// </summary>
        Task<bool> DeleteUserIdAsync(Guid id);

        /// <summary>
        /// Add a new user.
        /// </summary>
        Task<UserAddResponse> AddUserAsync(UserAddRequest request);

        /// <summary>
        /// Update an existing user.
        /// </summary>
        Task<UserUpdateResponse> UpdateUserAsync(UserUpdateRequest request);
    }
}
