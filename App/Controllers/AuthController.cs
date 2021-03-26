using App.Service;
using App.Shared;
using App.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private IUserService _userService;
        private IMailService _mailService;
        private IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(IUserService userService, IMailService mailService, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _mailService = mailService;
            _configuration = configuration;
            _userManager = userManager;
           
            
           
        }


        // /api/auth/register (register user)
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        // /api/auth/register (register Admin)
        [HttpPost()]
        [Route("register-admin")]
        public async Task<IActionResult> AdminRegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterAdminAsync(model);

                

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        // /api/auth/register-business (register Business User)
        [HttpPost()]
        [Route("register-business")]
        public async Task<IActionResult> BusinessRegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterBusinessAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        // /api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);

                if (result.IsSuccess)
                {
                    // await _mailService.SendEmailAsync(model.Email, "New login", "<h1>Hey!, new login to your account noticed</h1><p>New login to your account at " + DateTime.Now + "</p>");
                    return Ok(result);
                }

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid");
        }

        // /api/auth/confirmemail?userid&token
        [HttpGet("ConfirmEmail")]

        public async Task<IActionResult> ConfirmEmail(string userId, string token) {

            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"http://localhost:3000/index"); // redired page after confirm email address
            }

            return BadRequest(result);
        }

        // get user
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUser(string id) {
            var user = await _userService.GetUserbyId(id);
            var role = await _userManager.GetRolesAsync(user);
            var rolename = role.FirstOrDefault<String>();
            




            if (user == null) {
                return NotFound();
            }
            return Ok(new UserView
            {
                UserID = user.Id,
                Email = user.Email,
                UserName = user.UserName,
                UserRole = rolename

            }); 

        }

        // get all users
        //api/auth/users
        [HttpGet("users")]
        //[Authorize(Policy = UserRoles.Admin)]
        public async Task<IActionResult> GetAllUsers() {
            var users = await _userService.GetAllUsers();

            List<UserView> Alluser = new List<UserView>();

            // retuen all users with these attributes
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                var rolename = role.FirstOrDefault<String>();

                UserView member = new UserView {
                    UserID=user.Id,
                    Email = user.Email,
                    UserName = user.UserName,
                    UserRole = rolename
            };

                Alluser.Add(member);
            }

            if (Alluser == null)
            {
                return NotFound();
            }
            return Ok(Alluser);

        }



    }
}
