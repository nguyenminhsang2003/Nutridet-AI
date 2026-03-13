using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Validate userId
            var userIdClaim = User.FindFirst("userId")?.Value;

            if (!int.TryParse(userIdClaim, out int userId) || userId <= 0)
            {
                return Unauthorized(new { message = "userId is invalid" });
            }

            // Validate file exists (basic check to avoid null reference)
            if (file == null || file.Length == 0)
            {
                return BadRequest(new { message = "No file uploaded." });
            }

            // Convert file to base64 string
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            var fileBytes = memoryStream.ToArray();
            var base64String = Convert.ToBase64String(fileBytes);
            var imageDataString = $"data:{file.ContentType};base64,{base64String}";

            // Pass base64 string and userId to service
            var result = await _scanImageService.ScanImageAsync(imageDataString, userId);
            return Ok(result);
        }
    }
}
