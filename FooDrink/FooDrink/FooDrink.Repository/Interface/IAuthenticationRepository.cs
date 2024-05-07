using FooDrink.Database.Models;
using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Response.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.Repository.Interface
{
    public interface IAuthenticationRepository : IRepository<User>
    {
        Task<User?> GetByUsernameAndPassword(string username, string password);
        Task<User?> GetByUsername(string username);
    }
}
