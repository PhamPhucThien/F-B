using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Response.Menu;
using FooDrink.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.BussinessService.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repository;

        public MenuService(IMenuRepository repository)
        {
            _repository = repository;
        }
        public Task<bool> Add()
        {
            throw new NotImplementedException();
        }

        public async Task<MenuGetResponse> Get(Guid id, int page, int size)
        {
            MenuGetResponse response = new();
            List<Menu>? datas = await _repository.GetInRangeWithPaging(id, page, size);

            if (datas != null)
            {
                foreach (Menu data in datas)
                {
                    MenuList menu = new();
                    if (data.Products != null)
                    {
                        foreach (Product prod in data.Products)
                        {
                            MenuItem item = new();
                            item.Mapping(prod);
                            menu.MenuItems.Add(item);
                        }
                    }
                    response.Items.Add(menu);
                }
            }

            return response;
        }

        public async Task<bool> RemoveById(Guid managerId, Guid menuId)
        {
            bool response = await _repository.RemoveById(managerId, menuId);

            return response;
        }

        public Task<bool> UpdateById(Guid managerId)
        {
            throw new NotImplementedException();
        }
    }
}
