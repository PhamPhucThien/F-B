using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Response.Restaurant
{
    public class RestaurantGetByLocationResponse
    {
        public string Location { get; set; } = string.Empty;
        public List<RestaurantResponse> Data { get; set; } = new List<RestaurantResponse>();
    }
}
