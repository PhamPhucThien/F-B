using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.User;
using FooDrink.Repository.Interface;

namespace FooDrink.BussinessService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Add new User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserAddResponse> AddUserAsync(UserAddRequest request)
        {
            User user = new()
            {
                Id = Guid.NewGuid(),
                Username = request.Username,
                Password = request.Password,
                Email = request.Email,
                FullName = request.FullName,
                PhoneNumber = request.PhoneNumber,
                Address = request.Address,
                FavoritedList = " ",
                Role = "Customer",
                RestaurantId = Guid.NewGuid(),
                Image = "",
                Status = true,
                CreatedAt = DateTime.Now,
                CreatedBy = request.Username,
                UpdatedAt = DateTime.Now,
                UpdatedBy = request.Username,
            };

            User addedUser = await _userRepository.AddAsync(user);

            UserAddResponse response = new()
            {
                Data = new List<UserResponse>
        {
            new UserResponse
            {
                Id = addedUser.Id,
                Username = addedUser.Username,
                Password = "*********",
                Email = addedUser.Email,
                FullName = addedUser.FullName,
                PhoneNumber = addedUser.PhoneNumber,
                Address = addedUser.Address,
                FavoritedList = addedUser.FavoritedList,
                Status = addedUser.Status
            }
        }
            };

            return response;
        }

        /// <summary>
        /// Block User
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteUserIdAsync(Guid id)
        {
            return await _userRepository.DeleteByIdAsync(id);
        }

        /// <summary>
        /// Get User by Id
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserGetByIdResponse> GetUserByIdAsync(UserGetByIdRequest request)
        {
            UserGetByIdResponse response = new();

            try
            {
                User? user = await _userRepository.GetByIdAsync(request.Id);

                if (user != null)
                {
                    UserResponse userResponse = new()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = "*********",
                        Email = user.Email,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        FavoritedList = user.FavoritedList,
                        Status = user.Status
                    };

                    response.Data.Add(userResponse);
                }
                else
                {
                    response.Data = new List<UserResponse>();
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while fetching user.";
                response.ErrorDetails = ex.ToString();
            }

            return response;
        }

        /// <summary>
        /// Get all User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<UserGetListResponse> GetUsersAsync(UserGetListRequest request)
        {
            UserGetListResponse response = new();

            try
            {
                IEnumerable<UserGetListResponse> userResponses = await _userRepository.GetUsersAsync(request);

                List<UserResponse> users = userResponses
                    .SelectMany(userResponse => userResponse.Data)
                    .ToList();

                response.PageSize = request.PageSize;
                response.PageIndex = request.PageIndex;
                response.SearchString = request.SearchString;
                response.Data = users;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching users.", ex);
            }

            return response;
        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<UserUpdateResponse> UpdateUserAsync(UserUpdateRequest request)
        {
            UserUpdateResponse response = new();

            try
            {
                // Retrieve the user from the database based on the provided user ID
                User? user = await _userRepository.GetByIdAsync(request.Id);

                if (user != null)
                {
                    // Update the user entity with the data from the request
                    user.Username = request.Username;
                    user.Password = request.Password;
                    user.Email = request.Email;
                    user.FullName = request.FullName;
                    user.PhoneNumber = request.PhoneNumber;
                    user.Address = request.Address;
                    user.FavoritedList = request.FavoritedList;

                    _ = await _userRepository.EditAsync(user);

                    UserResponse updatedUserResponse = new()
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Password = "*********",
                        Email = user.Email,
                        FullName = user.FullName,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        FavoritedList = user.FavoritedList,
                        Status = user.Status
                    };

                    response.Data.Add(updatedUserResponse);
                }
                else
                {
                    response.Data = new List<UserResponse>();
                }
            }
            catch (Exception ex)
            {
                response.ErrorMessage = "An error occurred while updating user.";
                response.ErrorDetails = ex.ToString();
            }

            return response;
        }
    }
}
