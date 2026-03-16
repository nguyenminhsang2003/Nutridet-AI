using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> ChangeIsDoneAsync(int OutputNutritionExcerciseId)
        {
            var exitExcercise = await GetOutputNutritionExcerciseByIdAsync(OutputNutritionExcerciseId);
            if(exitExcercise == null) return false;
            exitExcercise.IsDone = !exitExcercise.IsDone;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<OutputNutritionExcercise> GetOutputNutritionExcerciseByIdAsync(int id)
        {
            return await _context.OutputNutritionExcercises.FirstOrDefaultAsync(o => o.Id == id);
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
