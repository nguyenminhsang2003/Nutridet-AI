using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file, [FromQuery] int? userId = null)
        {
            // Validate file exists (basic check to avoid null reference)
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded." });
            }

            // Validate userId
            if (!userId.HasValue || userId.Value <= 0)
            {
                return BadRequest(new { message = "UserId is required and must be greater than 0." });
            }

            // Convert file to base64 string
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(fileBytes);
            var imageDataString = $"data:{file.ContentType};base64,{base64String}";

            // Pass base64 string and userId to service
            var result = await _scanImageService.ScanImageAsync(imageDataString, userId.Value);
            return Ok(new { message = result });
        }
    }
}
