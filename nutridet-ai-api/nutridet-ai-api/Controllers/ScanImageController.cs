using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.Services;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Controllers
{
    [ApiController]
    [Route("api/scan-image")]
    public class ScanImageController : ControllerBase
    {
        private readonly IScanImageService _scanImageService;

        public ScanImageController(IScanImageService scanImageService)
        {
            _scanImageService = scanImageService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> Chat([FromBody] string message)
        {
            var result = await _scanImageService.ScanImageAsync(message);
            return Ok(result);
        }
    }
}
