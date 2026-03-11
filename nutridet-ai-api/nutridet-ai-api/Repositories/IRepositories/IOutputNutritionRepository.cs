using nutridet_ai_api.Models;

namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IOutputNutritionRepository
    {
        Task<OutputNutrition> SaveOutputNutritionAsync(int scanImageId, string? aiResult);
        Task<OutputNutrition> GetAllOutputNutritionsAsync(int scanImageId);
        Task<OutputNutrition> GetOutputNutritionsByIdAsync(int OutputNutritionId);
    }
}

