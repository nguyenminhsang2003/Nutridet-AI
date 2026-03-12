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
            if(scanImageId <= 0) return BadRequest( new {message = "scanImageId is null" } );
            var invoke = await _scanImageService.GetInvokeAsync(scanImageId);
            if (invoke == null) return BadRequest(new { message = "invoke is null" });
            return Ok(invoke);
        }
        [HttpPost("get-all-invoke")]
        public async Task<IActionResult> GetAllInvoke([FromQuery] FilterInvoke filterInvoke)
        {
            if (filterInvoke.userId <= 0) return BadRequest( new {message = "userId is null" } );
            if(filterInvoke.startDate != null && filterInvoke.endDate != null && filterInvoke.startDate > filterInvoke.endDate)
            {
                return BadRequest(new { message = "startDate is bigger than endDate" });
            } 
            var listInvoke = await _scanImageService.GetAllInvokeAsync(filterInvoke.userId, filterInvoke.startDate, filterInvoke.endDate);
            if (listInvoke == null) return BadRequest( new { message = "listInvoke is null" });
            return Ok(listInvoke);
        }
    }
    public class FilterInvoke
    {
        public int userId { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? endDate { get; set; }
    }
}
