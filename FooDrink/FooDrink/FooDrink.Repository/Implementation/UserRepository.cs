using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.DTO.Request;
using FooDrink.DTO.Request.User;
using FooDrink.DTO.Response.User;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FooDrink.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly FooDrinkDbContext _context;

        public UserRepository(FooDrinkDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// AddAsync 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<User> AddAsync(User entity)
        {
            if (_context.Users == null)
            {
                throw new NullReferenceException("User database context is null");
            }

            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<User> addedUser = await _context.Users.AddAsync(entity);
            _ = await _context.SaveChangesAsync();

            return addedUser.Entity;
        }

        /// <summary>
        /// DeleteByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (_context.Users == null)
            {
                throw new NullReferenceException("Restaurant database context is null");
            }
            User? entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            entity.Status = false;
            _ = await _context.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// EditAsync
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        /// <exception cref="Exception"></exception>
        public async Task<bool> EditAsync(User entity)
        {
            try
            {
                if (_context.Users == null)
                {
                    throw new NullReferenceException("User database context is null");
                }
                _ = _context.Users.Update(entity);
                _ = await _context.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new Exception("Concurrency conflict occurred while updating user.");
            }
        }

        /// <summary>
        /// GetAll
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<IEnumerable<User>> GetAll()
        {
            return _context.Users == null
                ? throw new NullReferenceException("User database context is null")
                : (IEnumerable<User>)await _context.Users.ToListAsync();
        }

        /// <summary>
        /// GetByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public async Task<User?> GetByIdAsync(Guid id)
        {
            return _context.Users == null
                ? throw new NullReferenceException("User database context is null")
                : await _context.Users.FindAsync(id);
        }

        /// <summary>
        /// GetUsersAsync
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IEnumerable<UserGetListResponse>> GetUsersAsync(UserGetListRequest request)
        {
            try
            {
                if (_context.Users == null)
                {
                    throw new NullReferenceException("User database context is null");
                }
                IQueryable<User> query = _context.Users.AsQueryable();
                if (!string.IsNullOrEmpty(request.SearchString))
                {
                    query = query.Where(r =>
                        r.FullName.Contains(request.SearchString) ||
                        r.Email.Contains(request.SearchString) ||
                        r.PhoneNumber.Contains(request.SearchString));
                }
                query = query.OrderBy(r => r.FullName)
                             .Skip((request.PageIndex - 1) * request.PageSize)
                             .Take(request.PageSize);
                List<UserGetListResponse> responseList = await query.Select(r => new UserGetListResponse
                {
                    PageSize = request.PageSize,
                    PageIndex = request.PageIndex,
                    SearchString = request.SearchString,
                    Data = new List<UserResponse>
                    {
                        new UserResponse
                        {
                            Id = r.Id,
                            Username = r.Username,
                            Password = "*********",
                            Email = r.Email,
                            FullName = r.FullName,
                            PhoneNumber = r.PhoneNumber,
                            Address = r.Address,
                            FavoritedList = r.FavoritedList,
                            Status = r.Status,
                        }
                    }

                }).ToListAsync();

                return responseList;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching User.", ex);
            }
        }

        /// <summary>
        /// GetWithPaging
        /// </summary>
        /// <param name="pagingRequest"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<User> GetWithPaging(IPagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }
    }
}
