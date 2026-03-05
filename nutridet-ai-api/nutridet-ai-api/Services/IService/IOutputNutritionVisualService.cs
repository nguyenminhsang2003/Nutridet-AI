using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;

namespace nutridet_ai_api.Services.IService
{
    public interface IOutputNutritionVisualService
    {
        Task<List<OutputNutritionVisualDto>> ConvertNutritionAsync(int outputNutritionId, OutputNutritionDto outputNutritionDto);
    }
}
