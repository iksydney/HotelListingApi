using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace HotelListing.Services
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;
        public AuthManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var signInCredentials = GetSignInCredentials();
            var claims = await GetClaims();
            var token = GenerateTokenOptions(signInCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signInCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var expiration = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings.GetSection("lifetime").Value));

            var token = new JwtSecurityToken(
                issuer: jwtSettings.GetSection("validIssuer").Value,
                claims: claims,
                expires: expiration,
                signingCredentials: signInCredentials);

            return token;
        }

        private async Task<List<Claim>> GetClaims()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, _user.UserName)
            };
            var roles = await _userManager.GetRolesAsync(_user);

            foreach(var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSignInCredentials()
        {
            var key = Environment.GetEnvironmentVariable("key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        public async Task<bool> ValidateUser(LoginUserDTO userDTO)
        {
            _user = await _userManager.FindByNameAsync(userDTO.Email);
            return (_user != null && await _userManager.CheckPasswordAsync(_user, userDTO.Password));
        }
    }
}
