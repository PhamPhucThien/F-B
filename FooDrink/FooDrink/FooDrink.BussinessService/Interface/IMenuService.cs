using FooDrink.DTO.Response.Menu;

namespace FooDrink.BussinessService.Interface
{
    public interface IMenuService
    {
        Task<MenuGetResponse> Get(Guid id, int page, int size);
        Task<bool> Add();
        Task<bool> RemoveById(Guid managerId, Guid menuId);
        Task<bool> UpdateById(Guid managerId);
    }
}
