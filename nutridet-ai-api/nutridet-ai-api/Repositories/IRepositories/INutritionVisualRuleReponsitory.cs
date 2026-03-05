using nutridet_ai_api.Models;

namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface INutritionVisualRuleReponsitory
    {
        Task<List<NutritionVisualRule>> GetAllNutritionVisualRuleAsync();
    }
}
