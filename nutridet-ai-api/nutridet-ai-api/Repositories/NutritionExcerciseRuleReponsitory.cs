using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class NutritionExcerciseRuleReponsitory : INutritionExcerciseRuleReponsitory
    {
        private readonly NutridetAiDbContext _context;

        public NutritionExcerciseRuleReponsitory(NutridetAiDbContext context)
        {
            _context = context;
        }

        public async Task<List<NutritionExcerciseRule>> GetAllNutritionExcerciseRuleAsync()
        {
            return await _context.NutritionExcerciseRules.ToListAsync();
        }
    }
}
