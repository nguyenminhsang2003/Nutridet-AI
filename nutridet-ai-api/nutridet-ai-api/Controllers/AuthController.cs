using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.DTO;
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
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "admin" && request.Password == "123")
            {
                var token = _jwtService.GenerateToken(1, "admin");

                return Ok(new
                {
                    token = token
                });
            }

            return Unauthorized("Username hoặc Password sai");
        }
        [Authorize]
        [HttpGet("profile")]
        public IActionResult GetProfile()
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (!int.TryParse(userIdClaim, out int userId) || userId <= 0)
            {
                return Unauthorized(new { message = "userId is invalid" });
            }

            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            return Ok(new
            {
                userId = userId,
                role = role
            });
        }
    }
}
