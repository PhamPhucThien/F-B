using FooDrink.DTO.Response.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Response.User
{
    public class UserAddResponse
    {
        public List<UserResponse> Data { get; set; } = new List<UserResponse>();
    }
}
