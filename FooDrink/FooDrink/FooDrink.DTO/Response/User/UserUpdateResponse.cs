namespace FooDrink.DTO.Response.User
{
    public class UserUpdateResponse
    {
        public List<UserResponse> Data { get; set; } = new List<UserResponse>();
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;
    }
}
