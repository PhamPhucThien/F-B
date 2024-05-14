using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.DTO.Request;
using FooDrink.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Implementation
{
    public class Reviewrepository : IReviewRepository
    {
        private readonly FooDrinkDbContext _context;

        public Reviewrepository(FooDrinkDbContext context)
        {
            _context = context;
        }

        public Task<Review> AddAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EditAsync(Review entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Review>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Review?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Review> GetWithPaging(IPagingRequest pagingRequest)
        {
            throw new NotImplementedException();
        }
    }
}
