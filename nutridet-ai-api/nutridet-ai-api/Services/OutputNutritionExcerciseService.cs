using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class OutputNutritionExcerciseService : IOutputNutritionExcerciseService
    {
        private readonly NutridetAiDbContext _context;
        private readonly INutritionExcerciseRuleReponsitory _nutritionExcerciseRuleReponsitory;
        private readonly IOutputNutritionRepository _outputNutritionRepository;
        private readonly IOutputNutritionExcerciseReponsitory _outputNutritionExcerciseReponsitory;


        public OutputNutritionExcerciseService(NutridetAiDbContext context, 
                                            INutritionExcerciseRuleReponsitory nutritionExcerciseRuleReponsitory,
                                            IOutputNutritionRepository outputNutritionRepository,
                                            IOutputNutritionExcerciseReponsitory outputNutritionExcerciseReponsitory)
        {
            _context = context;
            _nutritionExcerciseRuleReponsitory = nutritionExcerciseRuleReponsitory;
            _outputNutritionRepository = outputNutritionRepository;
            _outputNutritionExcerciseReponsitory = outputNutritionExcerciseReponsitory;
        }

        public async Task<List<OutputNutritionExcerciseDto>> CreateExercisesAsync(int outputNutritionId)
        {
            if (outputNutritionId <= 0)
            {
                throw new NotImplementedException();
            }

            var listResults = new List<OutputNutritionExcerciseDto>();

            var listNutritionExcerciseRule = await _nutritionExcerciseRuleReponsitory.GetAllNutritionExcerciseRuleAsync();

            var outputNutrition = await _outputNutritionRepository.GetOutputNutritionsByIdAsync(outputNutritionId);
            var outputNutritionDto = new OutputNutritionDto()
            {
                energyKcal = outputNutrition.EnergyKcal,
                carbohydrateG = outputNutrition.CarbohydrateG,
                sugarG = outputNutrition.SugarG,
                proteinG = outputNutrition.ProteinG,
                fatG = outputNutrition.FatG,
                saturatedFatG = outputNutrition.SaturatedFatG,
                fiberG = outputNutrition.FiberG,
                sodiumMg = outputNutrition.SodiumMg,
                cholesterolMg = outputNutrition.CholesterolMg
            };
            var result = typeof(OutputNutritionDto).GetProperties()
                        .Select(p => new
                        {
                            Name = p.Name,
                            Value = p.GetValue(outputNutritionDto)
                        }).ToList();

            foreach (var item in result)
            {
                var nutritionVisualRule = listNutritionExcerciseRule.FirstOrDefault(a => a.Nutrient == item.Name);
                if (nutritionVisualRule == null) continue;

                var outputNutritionVisual = new OutputNutritionExcercise();
                outputNutritionVisual.OutputNutritionId = outputNutritionId;
                outputNutritionVisual.Nutrient = item.Name;
                outputNutritionVisual.OriginalValue = item.Value as decimal?;
                outputNutritionVisual.Excercise = nutritionVisualRule.Excercise;
                outputNutritionVisual.ExcerciseValue = outputNutritionVisual.OriginalValue / nutritionVisualRule.ReferenceAmount;

                var itemResult = await _outputNutritionExcerciseReponsitory.SaveOutputNutritionExcerciseAsync(outputNutritionVisual);
                listResults.Add(new OutputNutritionExcerciseDto()
                {
                    Nutrient = itemResult.Nutrient,
                    OriginalValue = itemResult.OriginalValue,
                    Excercise = itemResult.Excercise,
                    ExcerciseValue = itemResult.ExcerciseValue,
                });
            }

            return listResults;
        }
    }
}
