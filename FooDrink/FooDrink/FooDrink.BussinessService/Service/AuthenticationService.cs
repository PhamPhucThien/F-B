using FooDrink.BussinessService.Interface;
using FooDrink.Database.Models;
using FooDrink.DTO.Request.Authentication;
using FooDrink.DTO.Response.Authentication;
using FooDrink.Repository.Interface;

namespace FooDrink.BussinessService.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IAuthenticationRepository _authenticationRepository;
        public AuthenticationService(IAuthenticationRepository authenticationRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            _authenticationRepository = authenticationRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<AuthenticationResponse> Login(LoginRequest request)
        {
            AuthenticationResponse response = new();

            User? user = await _authenticationRepository.GetByUsernameAndPassword(request.Username, request.Password);

            if (user != null)
            {
                response.Token = _jwtTokenGenerator.GenerateToken(user.Id, user.Role);
                response.Role = user.Role;
            }
            else
            {
                response.Message = "Wrong username or password";
            }

            return response;

        }

        public async Task<AuthenticationResponse> Register(RegisterRequest request)
        {
            AuthenticationResponse response = new();

            User? user = await _authenticationRepository.GetByUsername(request.Username);

            if (user == null)
            {
                User newUser = new()
                {
                    Username = request.Username,
                    Password = request.Password,
                    FullName = request.Fullname,
                    Email = request.Email,
                    Address = request.Address,
                    Role = "Customer",
                    Status = true
                };
                _ = await _authenticationRepository.AddAsync(newUser);
                response.Token = _jwtTokenGenerator.GenerateToken(newUser.Id, newUser.Role);
                response.Role= newUser.Role;
            }
            else
            {
                response.Message = "Username has already existed";
            }

            return response;
        }
    }
}
