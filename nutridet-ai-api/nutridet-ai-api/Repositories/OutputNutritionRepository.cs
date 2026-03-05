using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;
using System.Text.Json;
using nutridet_ai_api.DTO;

namespace nutridet_ai_api.Repositories
{
    public class OutputNutritionRepository : IOutputNutritionRepository
    {
        private readonly NutridetAiDbContext _context;

        public OutputNutritionRepository(NutridetAiDbContext context)
        {
            _context = context;
        }

        public async Task<List<OutputNutrition>> GetAllOutputNutritionsAsync(int scanImageId)
        {
            return await _context.OutputNutritions.Where(o => o.ScanImageId == scanImageId).ToListAsync();
        }

        public async Task<OutputNutrition> SaveOutputNutritionAsync(int scanImageId, string? aiResult)
        {
            if (string.IsNullOrEmpty(aiResult))
                throw new Exception("AI result is null");

            var nutrition = JsonSerializer.Deserialize<OutputNutritionDto>(aiResult);
            var outputNutrition = new OutputNutrition
            {
                ScanImageId = scanImageId,
                EnergyKcal = nutrition?.energyKcal,
                CarbohydrateG = nutrition?.carbohydrateG,
                SugarG = nutrition?.sugarG,
                ProteinG = nutrition?.proteinG,
                FatG = nutrition?.fatG,
                SaturatedFatG = nutrition?.saturatedFatG,
                FiberG = nutrition?.fiberG,
                SodiumMg = nutrition?.sodiumMg,
                CholesterolMg = nutrition?.cholesterolMg,
                CreatedAt = DateTime.UtcNow
            };

            _context.OutputNutritions.Add(outputNutrition);
            await _context.SaveChangesAsync();
            return outputNutrition;
        }
    }
}

