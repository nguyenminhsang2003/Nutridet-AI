using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class ScanImageService : IScanImageService
    {
        private readonly IGeminiService _geminiService;
        private readonly IScanImageRepository _scanImageRepository;

        public ScanImageService(IGeminiService geminiService, IScanImageRepository scanImageRepository)
        {
            _geminiService = geminiService;
            _scanImageRepository = scanImageRepository;
        }

        public async Task<string> ScanImageAsync(string imageBase64, int userId)
        {
            var aiResult = await _geminiService.GenerateAsync(imageBase64);
            await _scanImageRepository.SaveScanResultAsync("URL", aiResult, userId, "Gemini");
            return aiResult;
        }
    }
}
