using nutridet_ai_api.Models;

namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IOutputNutritionExcerciseReponsitory
    {
        public Task<OutputNutritionExcercise> SaveOutputNutritionExcerciseAsync(OutputNutritionExcercise outputNutritionExcercise);
        public Task<OutputNutritionExcercise> GetOutputNutritionExcerciseByIdAsync(int id);
        public Task<bool> ChangeIsDoneAsync(int id);
    }
}
