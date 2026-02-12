using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class ScanImageService : IScanImageService
    {
        private readonly IGeminiService _geminiService;
        public ScanImageService(IGeminiService geminiService)
        {
            _geminiService = geminiService;
        }

        public async Task<string> ScanImageAsync(string imageString)
        {
            var result = await _geminiService.GenerateAsync(imageString);
            return result;
        }
    }
}
