using GuitarCommerceAPI.Models;
using GuitarCommerceAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GuitarCommerceAPI.Controllers
{
    [Route("api/v1/user")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }
            var user = await userService.Register(request.Username, request.Password);
            if (user == null)
            {
                return BadRequest("User registration failed. Username may already be taken.");
            }
            return Ok(new LoginResponse(user.Id, user.Name));
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
            return Ok(new LoginResponse(user.Id, user.Name));
        }

    }
}
