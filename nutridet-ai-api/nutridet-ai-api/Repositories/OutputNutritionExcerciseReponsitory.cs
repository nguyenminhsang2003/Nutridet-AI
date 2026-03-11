using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class OutputNutritionExcerciseReponsitory : IOutputNutritionExcerciseReponsitory
    {
        private readonly NutridetAiDbContext _context;

        public OutputNutritionExcerciseReponsitory(NutridetAiDbContext context)
        {
            _context = context;
        }

        public async Task<OutputNutritionExcercise> SaveOutputNutritionExcerciseAsync(OutputNutritionExcercise outputNutritionExcercise)
        {
            if (outputNutritionExcercise == null) throw new Exception("OutputNutritionExcercise is null");
            _context.OutputNutritionExcercises.Add(outputNutritionExcercise);
            await _context.SaveChangesAsync();
            return outputNutritionExcercise;
        }
    }
}
