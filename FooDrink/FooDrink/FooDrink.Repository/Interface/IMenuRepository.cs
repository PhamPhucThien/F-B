using FooDrink.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Interface
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<List<Menu>?> GetInRangeWithPaging(Guid id, int page, int size);
        Task<bool> RemoveById(Guid managerId, Guid menuId);
    }
}
