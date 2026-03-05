using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;

namespace nutridet_ai_api.Services.IService
{
    public interface IScanImageService
    {
        Task<List<OutputNutritionVisualDto>> ScanImageAsync(string imageBase64, int userId);
    }
}
