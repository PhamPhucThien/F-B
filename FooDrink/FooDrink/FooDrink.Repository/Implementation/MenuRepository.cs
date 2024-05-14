using FooDrink.Database;
using FooDrink.Database.Models;
using FooDrink.DTO.Request;
using FooDrink.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Implementation
{
    public class MenuRepository : RepositoryGeneric<Menu>, IMenuRepository
    {
        private readonly DbContextOptions<FooDrinkDbContext> _contextOptions;

        public MenuRepository(DbContextOptions<FooDrinkDbContext> contextOptions) : base(contextOptions)
        {
            _contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));
        }

        public async Task<List<Menu>?> GetInRangeWithPaging(Guid id, int page, int size)
        {
            using FooDrinkDbContext context = new(_contextOptions);
            List<Menu>? entity = await context.Set<Menu>().Where(a => a.RestaurantId == id).Skip(size * (page - 1)).OrderByDescending(a => a.CreatedAt).Take(size).Include(p => p.Products).ToListAsync();
            return entity;
        }

        public async Task<bool> RemoveById(Guid managerId, Guid menuId)
        {
            using var context = new FooDrinkDbContext(_contextOptions);

            User? updater = await context.Set<User>().Where(a => a.Id == managerId).FirstOrDefaultAsync();
            Menu? entity = await context.Set<Menu>().Where(a => a.Restaurant.Users.Any(f => f.Id == managerId) && a.Id == menuId).FirstOrDefaultAsync();

            if (updater != null && entity != null)
            {
                entity.Status = false;
                entity.UpdatedAt = DateTime.UtcNow;
                entity.UpdatedBy = updater.FullName;
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
