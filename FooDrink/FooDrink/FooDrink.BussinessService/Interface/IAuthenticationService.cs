using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Response.Authentication;

namespace FooDrink.BussinessService.Interface
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginRequest request);
        Task<AuthenticationResponse> Register(RegisterRequest request);
    }
}
