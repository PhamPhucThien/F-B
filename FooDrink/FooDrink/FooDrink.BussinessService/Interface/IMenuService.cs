using FooDrink.DTO.Response.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
