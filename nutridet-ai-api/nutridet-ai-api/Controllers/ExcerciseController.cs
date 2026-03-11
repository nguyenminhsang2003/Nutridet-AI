using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.Models;
using nutridet_ai_api.Services.IService;
using nutridet_ai_api.Repositories.IRepositories;

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
        [HttpPost("create-excercise")]
        public async Task<IActionResult> CreateExcercise([FromQuery] int OutputNutritionId)
        {
            if (OutputNutritionId <= 0)
            {
                return BadRequest(new { message = "OutputNutritionId not valid." });
            }

            var outputNutrition = await _outputNutritionRepository.GetOutputNutritionsByIdAsync(OutputNutritionId);
            if (outputNutrition == null)
            {
                return BadRequest(new { message = "outputNutrition not exit." });
            }

            var listExcercise = await _outputNutritionExcerciseService.CreateExercisesAsync(OutputNutritionId);
            return Ok(listExcercise);
        }
    }
}
