using nutridet_ai_api.Models;

namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IOutputNutritionExcerciseReponsitory
    {
        public Task<OutputNutritionExcercise> SaveOutputNutritionExcerciseAsync(OutputNutritionExcercise outputNutritionExcercise);
    }
}
