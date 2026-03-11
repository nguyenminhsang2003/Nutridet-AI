using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class OutputNutritionVisualReponsitory : IOutputNutritionVisualReponsitory
    {
        private readonly NutridetAiDbContext _context;
        public OutputNutritionVisualReponsitory(NutridetAiDbContext context)
        {
            _context = context;
        }
        public async Task<OutputNutritionVisual> SaveOutputNutritionVisualAsync(OutputNutritionVisual outputNutritionVisual)
        {
            if (outputNutritionVisual == null) throw new Exception("OutputNutritionVisual is null"); 
            _context.OutputNutritionVisuals.Add(outputNutritionVisual);
            await _context.SaveChangesAsync();
            return outputNutritionVisual;
        }
    }
}
