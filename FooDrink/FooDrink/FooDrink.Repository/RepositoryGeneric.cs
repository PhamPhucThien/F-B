using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FooDrink.Database;
using FooDrink.DTO.Request;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace FooDrink.Repository
{
    public class RepositoryGeneric<T> : IRepository<T> where T : class
    {
        private readonly DbContextOptions<FooDrinkDbContext> _contextOptions;

        public RepositoryGeneric(DbContextOptions<FooDrinkDbContext> contextOptions)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var context = new FooDrinkDbContext(_contextOptions))
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            using var context = new FooDrinkDbContext(_contextOptions);
            var entity = await context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> EditAsync(T entity)
        {
            using var context = new FooDrinkDbContext(_contextOptions);
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using var context = new FooDrinkDbContext(_contextOptions);
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            using var context = new FooDrinkDbContext(_contextOptions);
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                Console.WriteLine($"Entity with ID {id} not found.");
            }
            return entity;
        }

        public IEnumerable<T> GetWithPaging(IPagingRequest pagingRequest)
        {
            if (pagingRequest == null)
            {
                throw new ArgumentNullException(nameof(pagingRequest));
            }

            using var context = new FooDrinkDbContext(_contextOptions);
            var query = context.Set<T>().AsQueryable();

            query = query.Skip(pagingRequest.PageSize * (pagingRequest.PageIndex - 1))
                         .Take(pagingRequest.PageSize);

            return query.ToList();
        }
    }
}
