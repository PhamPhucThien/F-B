using FooDrink.BussinessService.Interface;
using FooDrink.BussinessService.Service;
using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Response.Authentication;
using FooDrink.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FooDrink.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService) 
        {
            _authenticationService = authenticationService ?? throw new ArgumentNullException(nameof(authenticationService));
        }

        [HttpPost("Login")]
        public Task<AuthenticationResponse> Login(LoginRequest request)
        {
            return _authenticationService.Login(request);
        }

        [HttpPost("Register")]
        public Task<AuthenticationResponse> Register(RegisterRequest request)
        {
            return _authenticationService.Register(request);
        }
    }
}
