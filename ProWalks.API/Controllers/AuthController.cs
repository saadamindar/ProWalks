using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProWalks.API.Models.DTO;
using System.Runtime.CompilerServices;

namespace ProWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public UserManager<IdentityUser> _userManager { get; }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto )
        {
            var user = new IdentityUser()
            {
                UserName = registerUserDto.Username,
                Email = registerUserDto.Username
            };

            var identityResult = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (identityResult.Succeeded)
            {
                if (registerUserDto.Roles != null && registerUserDto.Roles.Any())
                {
                    identityResult = await _userManager.AddToRolesAsync(user, registerUserDto.Roles);
                    if (identityResult.Succeeded)
                        return Ok("User registered successfully");
                }
            }
            return BadRequest(identityResult.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var user = await _userManager.FindByEmailAsync(loginUserDto.Username);

            if (user != null)
            { 
                var checkPassword = await _userManager.CheckPasswordAsync(user, loginUserDto.Password);
                if (checkPassword)
                {
                    //Create JWT Token
                    return Ok("User logged in successfully");
                }
            }

            return Unauthorized();
        }
    }
}
