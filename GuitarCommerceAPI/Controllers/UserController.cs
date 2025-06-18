using GuitarCommerceAPI.Models;
using GuitarCommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GuitarCommerceAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IIdentityService identityService;

        public UserController(IUserService userService, IIdentityService identityService)
        {
            this.userService = userService;
            this.identityService = identityService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }
            var result = await userService.Register(request.Username, request.Password);
            if (!result)
            {
                return BadRequest("User registration failed. Username may already be taken.");
            }
            return Ok();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            var user = await userService.Login(request.Username, request.Password);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            string token = identityService.GenerateToken(user);
            return Ok(new LoginResponse { Token = token });
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> GetUserData()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            User? user = await userService.GetUserData(userId);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(new UserDataResponse { UserId = user.Id, Username = user.Name });
        }
    }
}
