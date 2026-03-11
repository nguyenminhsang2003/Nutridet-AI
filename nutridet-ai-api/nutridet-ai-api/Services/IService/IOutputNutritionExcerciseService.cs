using nutridet_ai_api.DTO;

namespace nutridet_ai_api.Services.IService
{
    public interface IOutputNutritionExcerciseService
    {
        Task<List<OutputNutritionExcerciseDto>> CreateExercisesAsync(int scanImageId, int OutputNutritionId);
    }
}
