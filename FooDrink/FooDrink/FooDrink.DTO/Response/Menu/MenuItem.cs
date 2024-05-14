using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Response.Menu
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string CategoryList { get; set; } = string.Empty;
        public string ImageList { get; set; } = string.Empty;

        public void Mapping(FooDrink.Database.Models.Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Description = product.Description;
            this.Price = product.Price;
            this.CategoryList = product.CategoryList;
            this.ImageList = product.ImageList;
        }
    }
}
