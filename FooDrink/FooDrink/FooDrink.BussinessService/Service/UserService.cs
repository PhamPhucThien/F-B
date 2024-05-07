using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.User;
using FooDrink.Repository;
using FooDrink.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FooDrink.BussinessService.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUser(User user)
        {
            // Add user to the repository
            return await _userRepository.AddAsync(user);
        }

        public async Task<bool> DeleteUser(Guid userId)
        {
            // Delete user from the repository
            return await _userRepository.DeleteByIdAsync(userId);
        }

        public async Task<bool> UpdateUser(User user)
        {
            // Update user in the repository
            return await _userRepository.EditAsync(user);
        }

        public async Task<IEnumerable<User>> GetAllUsers(UserGetListRequest request)
        {
            // Get all users from the repository
            return await _userRepository.GetAll();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            // Get user by ID from the repository
            return await _userRepository.GetByIdAsync(id);
        }

        public Task<UserGetListResponse> GetApplicationUserListAsync(UserGetListRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
