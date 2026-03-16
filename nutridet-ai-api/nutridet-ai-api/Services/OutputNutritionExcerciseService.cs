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
        private readonly IScanImageRepository _scanImageRepository;


        public OutputNutritionExcerciseService(NutridetAiDbContext context, 
                                            INutritionExcerciseRuleReponsitory nutritionExcerciseRuleReponsitory,
                                            IOutputNutritionRepository outputNutritionRepository,
                                            IOutputNutritionExcerciseReponsitory outputNutritionExcerciseReponsitory,
                                            IScanImageRepository scanImageRepository)
        {
            _context = context;
            _nutritionExcerciseRuleReponsitory = nutritionExcerciseRuleReponsitory;
            _outputNutritionRepository = outputNutritionRepository;
            _outputNutritionExcerciseReponsitory = outputNutritionExcerciseReponsitory;
            _scanImageRepository = scanImageRepository;
        }

        public async Task<List<OutputNutritionExcerciseDto>> CreateExercisesAsync(int scanImageId, int outputNutritionId)
        {
            if (scanImageId <= 0  && outputNutritionId <= 0)
            {
                throw new NotImplementedException();
            }
            if (! await _scanImageRepository.ChangeDeleteAsync(scanImageId))
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

                var outputNutritionExcercise = new OutputNutritionExcercise();
                outputNutritionExcercise.OutputNutritionId = outputNutritionId;
                outputNutritionExcercise.Nutrient = item.Name;
                outputNutritionExcercise.OriginalValue = item.Value as decimal?;

                if (outputNutritionExcercise.OriginalValue == null || outputNutritionExcercise.OriginalValue <= 0) continue;

                outputNutritionExcercise.Excercise = nutritionVisualRule.Excercise;
                outputNutritionExcercise.ExcerciseValue = outputNutritionExcercise.OriginalValue / nutritionVisualRule.ReferenceAmount;
                if(outputNutritionExcercise.ExcerciseValue == null || outputNutritionExcercise.ExcerciseValue <= 0) continue;

                var itemResult = await _outputNutritionExcerciseReponsitory.SaveOutputNutritionExcerciseAsync(outputNutritionExcercise);
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
        public async Task<bool> ChangeIsDoneAsync(int id)
        {
            return await _outputNutritionExcerciseReponsitory.ChangeIsDoneAsync(id);
        }
    }
}
