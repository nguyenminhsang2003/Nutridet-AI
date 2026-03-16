using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Controllers
{
    [ApiController]
    [Route("api/excercise")]
    public class ExcerciseController : ControllerBase
    {
        private readonly IOutputNutritionExcerciseService _outputNutritionExcerciseService;
        private readonly IOutputNutritionRepository _outputNutritionRepository;
        public ExcerciseController(IOutputNutritionExcerciseService outputNutritionExcerciseService,
                                    IOutputNutritionRepository outputNutritionRepository)
        {
            _outputNutritionExcerciseService = outputNutritionExcerciseService;
            _outputNutritionRepository = outputNutritionRepository;
        }
        [Authorize]
        [HttpPost("create-excercise")]
        public async Task<IActionResult> CreateExcercise([FromQuery] int scanImageId, [FromQuery] int OutputNutritionId)
        {
            if (scanImageId <=0 || OutputNutritionId <= 0)
            {
                return BadRequest(new { message = "OutputNutritionId and scanImageId not valid." });
            }

            var outputNutrition = await _outputNutritionRepository.GetOutputNutritionsByIdAsync(OutputNutritionId);
            if (outputNutrition == null)
            {
                return BadRequest(new { message = "outputNutrition not exit." });
            }

            var listExcercise = await _outputNutritionExcerciseService.CreateExercisesAsync(scanImageId, OutputNutritionId);
            return Ok(listExcercise);
        }
        [Authorize]
        [HttpPatch("update-isDone")]
        public async Task<IActionResult> ChangeIsDone([FromQuery] int outputNutritionExcerciseId)
        {
            if(outputNutritionExcerciseId <= 0)
            {
                return BadRequest(new { message = "OutputNutritionId and scanImageId not valid." });
            }
            if (!await _outputNutritionExcerciseService.ChangeIsDoneAsync(outputNutritionExcerciseId))
            {
                return BadRequest(new { message = "Can not update." });
            }
            else
            {
                return Ok(new { message = "Update successfull." });
            }
        }
    }
}
