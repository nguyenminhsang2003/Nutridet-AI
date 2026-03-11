using Microsoft.AspNetCore.Mvc;
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
        [HttpPost("get-invoke")]
        public async Task<IActionResult> GetInvoke([FromQuery] int scanImageId)
        {
            if(scanImageId == 0) return BadRequest( new {message = "scanImageId is null" } );
            var invoke = await _scanImageService.GetInvokeAsync(scanImageId);
            if (invoke == null) return BadRequest(new { message = "invoke is null" });
            return Ok(invoke);
        }
    }
}
