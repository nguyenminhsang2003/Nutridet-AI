using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class OutputNutritionRepository : IOutputNutritionRepository
    {
        private readonly NutridetAiDbContext _context;

        public OutputNutritionRepository(NutridetAiDbContext context)
        {
            _context = context;
        }

        public async Task SaveOutputNutritionAsync(int scanImageId)
        {
            var outputNutrition = new OutputNutrition
            {
                ScanImageId = scanImageId,
                CreatedAt = DateTime.UtcNow
            };

            _context.OutputNutritions.Add(outputNutrition);
            await _context.SaveChangesAsync();
        }

        public async Task SaveOutputNutritionAsync(int scanImageId, decimal? energyKcal, decimal? carbohydrateG, decimal? sugarG, decimal? proteinG, decimal? fatG, decimal? saturatedFatG, decimal? fiberG, decimal? sodiumMg, decimal? cholesterolMg)
        {
            var outputNutrition = new OutputNutrition
            {
                ScanImageId = scanImageId,
                EnergyKcal = energyKcal,
                CarbohydrateG = carbohydrateG,
                SugarG = sugarG,
                ProteinG = proteinG,
                FatG = fatG,
                SaturatedFatG = saturatedFatG,
                FiberG = fiberG,
                SodiumMg = sodiumMg,
                CholesterolMg = cholesterolMg,
                CreatedAt = DateTime.UtcNow
            };

            _context.OutputNutritions.Add(outputNutrition);
            await _context.SaveChangesAsync();
        }
    }
}

