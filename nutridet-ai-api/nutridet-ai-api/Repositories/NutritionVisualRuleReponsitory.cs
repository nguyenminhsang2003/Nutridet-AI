using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class NutritionVisualRuleReponsitory : INutritionVisualRuleReponsitory
    {
        private readonly NutridetAiDbContext _context;

        public NutritionVisualRuleReponsitory(NutridetAiDbContext context)
        {
            _context = context;
        }
        public async Task<List<NutritionVisualRule>> GetAllNutritionVisualRuleAsync()
        {
            return await _context.NutritionVisualRules.ToListAsync();
        }
    }
}
