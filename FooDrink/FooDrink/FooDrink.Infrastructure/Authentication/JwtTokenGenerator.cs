﻿using FooDrink.Repository.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FooDrink.Infrastructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;

        public JwtTokenGenerator()
        {
            ConfigurationBuilder configurationBuilder = new();

            _ = configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfiguration configuration = configurationBuilder.Build();
            _configuration = configuration;
        }
        public string GenerateToken(Guid id, string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtSettings:Key").Value));

            SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken token = new(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            string jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
