using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.Services.IService;
using System.Security.Claims;

namespace nutridet_ai_api.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "123")
            {
                var token = _jwtService.GenerateToken(1, "admin");

                return Ok(new
                {
                    token = token
                });
            }

            return Unauthorized();
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userId = User.FindFirst("userId")?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                userId = userId,
                role = role
            });
        }
    }
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
