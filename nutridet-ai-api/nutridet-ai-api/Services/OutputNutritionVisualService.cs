using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class OutputNutritionVisualService : IOutputNutritionVisualService
    {
        private readonly NutridetAiDbContext _context;
        private readonly INutritionVisualRuleReponsitory _nutritionVisualRuleReponsitory;
        private readonly IOutputNutritionVisualReponsitory _outputNutritionVisualReponsitory;
        public OutputNutritionVisualService(NutridetAiDbContext context, 
                                            INutritionVisualRuleReponsitory nutritionVisualRuleReponsitory,
                                            IOutputNutritionVisualReponsitory outputNutritionVisualReponsitory)
        {
            _context = context;
            _nutritionVisualRuleReponsitory = nutritionVisualRuleReponsitory;
            _outputNutritionVisualReponsitory = outputNutritionVisualReponsitory;
        }
        public async Task<List<OutputNutritionVisualDto>> ConvertNutritionAsync(int outputNutritionId, OutputNutritionDto outputNutritionDto)
        {
            if (outputNutritionId <= 0 || outputNutritionDto == null)
            {
                throw new NotImplementedException();
            }
            
            var listResults = new List<OutputNutritionVisualDto>();

            var listNutritionVisualRule = await _nutritionVisualRuleReponsitory.GetAllNutritionVisualRuleAsync();

            var result = typeof(OutputNutritionDto).GetProperties()
                        .Select(p => new
                        {
                            Name = p.Name,
                            Value = p.GetValue(outputNutritionDto)
                        }).ToList();
            foreach( var item in result )
            {
                var nutritionVisualRule = listNutritionVisualRule.FirstOrDefault(a => a.Nutrient == item.Name);
                if( nutritionVisualRule == null ) continue;

                var outputNutritionVisual = new OutputNutritionVisual();
                outputNutritionVisual.OutputNutritionId = outputNutritionId;
                outputNutritionVisual.Nutrient = item.Name;
                outputNutritionVisual.OriginalValue = item.Value as decimal?;
                outputNutritionVisual.VisualName = nutritionVisualRule.VisualName;
                outputNutritionVisual.VisualValue = outputNutritionVisual.OriginalValue / nutritionVisualRule.ReferenceAmount;

                var itemResult = await _outputNutritionVisualReponsitory.SaveOutputNutritionVisualAsync(outputNutritionVisual);
                listResults.Add(new OutputNutritionVisualDto()
                {
                    Nutrient = itemResult.Nutrient,
                    OriginalValue = itemResult.OriginalValue,
                    VisualName = itemResult.VisualName,
                    VisualAmount = itemResult.VisualValue,
                });
            }

            return listResults;
        }
    }
}
