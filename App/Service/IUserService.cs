using App.Controllers;
using App.Shared;
using App.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


namespace App.Service
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> RegisterAdminAsync(RegisterViewModel model);
        Task<UserManagerResponse> RegisterBusinessAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
        Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token);
        Task<IdentityUser> GetUserbyId(string userId);

        Task<List<IdentityUser>> GetAllUsers(); 



    }

    public class UserService : IUserService
    {
        private UserManager<IdentityUser> _userManger;
        private IConfiguration _configuration;
        private IMailService _mailService;
        private RoleManager<IdentityRole> _roleManager;
        private IJWTService _jwtService;

        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration, IMailService mailService, RoleManager<IdentityRole> roleManager, IJWTService jwtService)
        {
            _userManger = userManager;
            _configuration = configuration;
            _mailService = mailService;
            _roleManager = roleManager;
            _jwtService = jwtService;
        }

        // Register User
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Token = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var FullName = model.UserName;
            

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = FullName.Substring(0, FullName.IndexOf(" "))
        };

            var result = await _userManger.CreateAsync(identityUser, model.Password);

            


            if (result.Succeeded)
            {
                // generate token for verify email address
                var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                 var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                 var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                 string url = $"{_configuration["AppUrl"]}api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                  $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

                // jwt

                string tokenString = await _jwtService.RegisterJWT(model);


                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _userManger.AddToRoleAsync(identityUser, UserRoles.User);
                    
                }


                return new UserManagerResponse
                {

                    Token = tokenString,
                    IsSuccess = true,
                    
                };
            }

            return new UserManagerResponse
            {
                Token = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }

        //Register Business User

        public async Task<UserManagerResponse> RegisterBusinessAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Token = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var FullName = model.UserName;

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = FullName.Substring(0, FullName.IndexOf(" "))
            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
               // generate token for verify email address
                var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                string url = $"{_configuration["AppUrl"]}api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                 $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

               

                if (!await _roleManager.RoleExistsAsync(UserRoles.Business))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Business));
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Business))
                {
                    await _userManger.AddToRoleAsync(identityUser, UserRoles.Business);

                }


                return new UserManagerResponse
                {
                    Token = "Business User created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Token = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }


        // Register Admin

        public async Task<UserManagerResponse> RegisterAdminAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new NullReferenceException("Reigster Model is null");

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Token = "Confirm password doesn't match the password",
                    IsSuccess = false,
                };

            var FullName = model.UserName;

            var identityUser = new IdentityUser
            {
                Email = model.Email,
                UserName = FullName.Substring(0, FullName.IndexOf(" "))
            };

            var result = await _userManger.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                // generate token for verify email address
                //var confirmEmailToken = await _userManger.GenerateEmailConfirmationTokenAsync(identityUser);

                //var encodedEmailToken = Encoding.UTF8.GetBytes(confirmEmailToken);
                //var validEmailToken = WebEncoders.Base64UrlEncode(encodedEmailToken);

                //string url = $"{_configuration["AppUrl"]}/api/auth/confirmemail?userid={identityUser.Id}&token={validEmailToken}";

                //await _mailService.SendEmailAsync(identityUser.Email, "Confirm your email", $"<h1>Welcome to Auth Demo</h1>" +
                //  $"<p>Please confirm your email by <a href='{url}'>Clicking here</a></p>");

                if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }

                if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }

                if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await _userManger.AddToRoleAsync(identityUser, UserRoles.Admin);
                    Console.WriteLine("admin created");
                }


                return new UserManagerResponse
                {
                    Token = "Admin created successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Token = "User did not create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };

        }


        // Login User
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManger.FindByEmailAsync(model.Email);

            if (user == null)
            {
                return new UserManagerResponse
                {
                    Token = "There is no user with that Email address",
                    IsSuccess = false,
                };
            }

            var result = await _userManger.CheckPasswordAsync(user, model.Password);
            var userRoles = await _userManger.GetRolesAsync(user);

            if (!result)
                return new UserManagerResponse
                {
                    Token = "Invalid password",
                    IsSuccess = false,
                };

            string tokenAsString = await _jwtService.LoginJWT(model);

            return new UserManagerResponse
            {
                Token = tokenAsString,
                IsSuccess = true,
                
               
            };

           
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token) {
          
            var user = await _userManger.FindByIdAsync(userId);
            
            if (user == null)
                return new UserManagerResponse
                {
                    IsSuccess = false,
                    Token = "User not found"
                };

            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);

            var result = await _userManger.ConfirmEmailAsync(user, normalToken);

            if (result.Succeeded)
                return new UserManagerResponse
                {
                    Token = "Email confirmed successfully!",
                    IsSuccess = true,
                };

            return new UserManagerResponse
            {
                IsSuccess = false,
                Token = "Email did not confirm",
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        // get user by id
        public async Task<IdentityUser> GetUserbyId(string userId)
        {
            var user = await _userManger.FindByIdAsync(userId);
            return user;
            }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            var user = await _userManger.Users.ToListAsync();
            return user;
        }
    }
 }


