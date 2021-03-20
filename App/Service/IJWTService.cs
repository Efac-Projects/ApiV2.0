using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using App.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace App.Service
{
    public interface IJWTService
    {
        Task<string> RegisterJWT(RegisterViewModel model);
        Task<string> LoginJWT(LoginViewModel model);
    }

    public class JWTService : IJWTService
    {
        private  UserManager<IdentityUser> _userManger;
        private  IConfiguration _configuration;

        public JWTService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManger = userManager;
            _configuration = configuration;
        }
        
        // generate JWT when log in
        public async Task<string> LoginJWT(LoginViewModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
            var userRoles = await _userManger.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim("userId", user.Id),
                new Claim("userName", user.UserName),
                new Claim("email", model.Email),
                
               // new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("userRole", userRole));
                Console.WriteLine(userRole.ToString());
            }


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }

        // generate JWT when sign in
        public async Task<string> RegisterJWT(RegisterViewModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);
            var userRoles = await _userManger.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim("userId", user.Id),
                new Claim("userName", user.UserName),
                new Claim("email", model.Email),
                
               // new Claim(ClaimTypes.NameIdentifier, user.Id),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim("userRole", userRole));
                Console.WriteLine(userRole.ToString());
            }


            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["AuthSettings:Issuer"],
                audience: _configuration["AuthSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenAsString;
        }
    }
}
