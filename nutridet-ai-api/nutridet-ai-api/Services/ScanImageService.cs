using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories;
using nutridet_ai_api.Repositories.IRepositories;
using nutridet_ai_api.Services.IService;

namespace nutridet_ai_api.Services
{
    public class ScanImageService : IScanImageService
    {
        private readonly IGeminiService _geminiService;
        private readonly IScanImageRepository _scanImageRepository;
        private readonly IOutputNutritionRepository _outputNutritionRepository;
        private readonly IOutputNutritionVisualService _outputNutritionVisualService;

        public ScanImageService(IGeminiService geminiService, 
                                IScanImageRepository scanImageRepository, 
                                IOutputNutritionRepository outputNutritionRepository,
                                IOutputNutritionVisualService outputNutritionVisualService)
        {
            _geminiService = geminiService;
            _scanImageRepository = scanImageRepository;
            _outputNutritionRepository = outputNutritionRepository;
            _outputNutritionVisualService = outputNutritionVisualService;
        }

        public async Task<ScanImage?> GetInvokeAsync(int scanImageId)
        {
            return await _scanImageRepository.GetInvokeAsync(scanImageId);
        }

        public async Task<Object> ScanImageAsync(string imageBase64, int userId)
        {
            var aiResult = await _geminiService.GenerateAsync(imageBase64);
            var scanImage = await _scanImageRepository.SaveScanResultAsync("URL", aiResult, userId, "Gemini");
            var outputNutrition = await _outputNutritionRepository.SaveOutputNutritionAsync(scanImage.ScanImageId, aiResult);

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

            var convertNutrition = await _outputNutritionVisualService.ConvertNutritionAsync(outputNutrition.OutputNutritionId, outputNutritionDto);
            return new
            {
                scanImageId = scanImage.ScanImageId,
                outputNutritionId = outputNutrition.OutputNutritionId,
                convertNutrition = convertNutrition
            };
        }
    }
}
