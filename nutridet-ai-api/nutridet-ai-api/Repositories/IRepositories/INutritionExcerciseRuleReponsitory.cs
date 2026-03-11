using nutridet_ai_api.Models;

namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface INutritionExcerciseRuleReponsitory
    {
        Task<List<NutritionExcerciseRule>> GetAllNutritionExcerciseRuleAsync();
    }
}
