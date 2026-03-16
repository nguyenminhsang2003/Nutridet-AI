using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.DTO;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Controllers
{
    [ApiController]
    [Route("api/invoke")]
    public class InvokeController : ControllerBase
    {
        private readonly IScanImageService _scanImageService;
        public InvokeController(IScanImageService scanImageService)
        {
            _scanImageService = scanImageService;
        }
        [Authorize]
        [HttpPost("get-invoke")]
        public async Task<IActionResult> GetInvoke([FromQuery] int scanImageId)
        {
            if(scanImageId <= 0) return BadRequest( new {message = "scanImageId is null" } );
            var invoke = await _scanImageService.GetInvokeAsync(scanImageId);
            if (invoke == null) return BadRequest(new { message = "invoke is null" });
            return Ok(invoke);
        }
        [Authorize]
        [HttpPost("get-all-invoke")]
        public async Task<IActionResult> GetAllInvoke([FromQuery] FilterInvoke filterInvoke)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (!int.TryParse(userIdClaim, out int userId) || userId <= 0)
            {
                return Unauthorized(new { message = "userId is invalid" });
            }

            if (filterInvoke.startDate != null && filterInvoke.endDate != null && filterInvoke.startDate > filterInvoke.endDate)
            {
                return BadRequest(new { message = "startDate is bigger than endDate" });
            } 
            var listInvoke = await _scanImageService.GetAllInvokeAsync(userId, filterInvoke.startDate, filterInvoke.endDate, filterInvoke.page, filterInvoke.pageSize);
            if (listInvoke == null) return BadRequest( new { message = "listInvoke is null" });
            return Ok(listInvoke);
        }
    }
}
