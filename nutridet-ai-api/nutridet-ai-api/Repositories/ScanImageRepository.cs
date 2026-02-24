using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class ScanImageRepository : IScanImageRepository
    {
        private readonly NutridetAiDbContext _context;
        private readonly IOutputNutritionRepository _outputNutritionRepository;

        public ScanImageRepository(NutridetAiDbContext context, IOutputNutritionRepository outputNutritionRepository)
        {
            _context = context;
            _outputNutritionRepository = outputNutritionRepository;
        }

        public async Task SaveScanResultAsync(string imageBase64, string aiResult, int userId, string aiProvider)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var imageUrl = imageBase64.Length > 500 ? null : imageBase64;

                var scanImage = new ScanImage
                {
                    UserId = userId,
                    ImageUrl = imageUrl,
                    AiProvider = aiProvider,
                    RawTextResponse = aiResult,
                    CreatedAt = DateTime.UtcNow,
                    IsDelete = false
                };

                _context.ScanImages.Add(scanImage);
                await _context.SaveChangesAsync();
                
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}

