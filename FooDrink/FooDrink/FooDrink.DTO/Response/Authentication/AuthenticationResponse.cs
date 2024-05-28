namespace FooDrink.DTO.Response.Authentication
{
    public class AuthenticationResponse
    {
        public string Message { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
    }
}
