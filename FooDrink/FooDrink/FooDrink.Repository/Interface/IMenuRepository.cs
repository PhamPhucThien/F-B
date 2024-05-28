using FooDrink.Database.Models;

namespace FooDrink.Repository.Interface
{
    public interface IMenuRepository : IRepository<Menu>
    {
        Task<List<Menu>?> GetInRangeWithPaging(Guid id, int page, int size);
        Task<bool> RemoveById(Guid managerId, Guid menuId);
    }
}
