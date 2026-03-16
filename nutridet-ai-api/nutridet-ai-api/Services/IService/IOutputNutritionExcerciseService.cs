using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;

namespace nutridet_ai_api.Services.IService
{
    public interface IOutputNutritionExcerciseService
    {
        Task<List<OutputNutritionExcerciseDto>> CreateExercisesAsync(int scanImageId, int OutputNutritionId);
        Task<bool> ChangeIsDoneAsync(int id);
    }
}
