using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.DTO;
using nutridet_ai_api.Services.IService;
using System.Security.Claims;
using System.Threading.Tasks;

namespace nutridet_ai_api.Controllers
{
    [ApiController]
    [Route("api")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public AuthController(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var userCheck = await _userService.LoginAsync(request.Email, request.Password);
            if (userCheck.Result)
            {
                var token = _jwtService.GenerateToken(userCheck.User.UserId, userCheck.User.Role ?? "user");

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
