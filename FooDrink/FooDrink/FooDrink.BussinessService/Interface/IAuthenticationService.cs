using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Response.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.BussinessService.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginRequest request);
        Task<AuthenticationResponse> Register(RegisterRequest request);
    }
}
