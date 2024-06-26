﻿namespace FooDrink.DTO.Response.User
{
    /// <summary>
    /// Get user by Id
    /// </summary>
    public class UserGetByIdResponse
    {
        public List<UserResponse> Data { get; set; } = new List<UserResponse>();
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;
    }
}
