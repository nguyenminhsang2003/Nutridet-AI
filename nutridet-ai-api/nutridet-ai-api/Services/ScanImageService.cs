using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class ScanImageService : IScanImageService
    {
        private readonly IGeminiService _geminiService;
        private readonly IScanImageRepository _scanImageRepository;
        private readonly IOutputNutritionRepository _outputNutritionRepository;

        public ScanImageService(IGeminiService geminiService, IScanImageRepository scanImageRepository, IOutputNutritionRepository outputNutritionRepository)
        {
            _geminiService = geminiService;
            _scanImageRepository = scanImageRepository;
            _outputNutritionRepository = outputNutritionRepository;
        }

        public async Task<string> ScanImageAsync(string imageBase64, int userId)
        {
            var aiResult = await _geminiService.GenerateAsync(imageBase64);
            var scanImage = await _scanImageRepository.SaveScanResultAsync("URL", aiResult, userId, "Gemini");
            await _outputNutritionRepository.SaveOutputNutritionAsync(scanImage.ScanImageId, aiResult);
            return aiResult;
        }
    }
}
