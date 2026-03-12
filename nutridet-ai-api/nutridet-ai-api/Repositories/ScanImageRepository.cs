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

        public async Task<ScanImage?> GetInvokeAsync(int scanImageId)
        {
            return await _context.ScanImages.AsNoTracking()
                                            .Where(s => s.ScanImageId == scanImageId)
                                            .Select(s => new ScanImage
                                            {
                                                ScanImageId = s.ScanImageId,
                                                ImageUrl = s.ImageUrl,
                                                CreatedAt = s.CreatedAt,
                                                User = s.User,
                                                OutputNutrition = new OutputNutrition
                                                {
                                                    OutputNutritionVisuals = s.OutputNutrition.OutputNutritionVisuals,
                                                    OutputNutritionExcercises = s.OutputNutrition.OutputNutritionExcercises
                                                }
                                            })
                                            .FirstOrDefaultAsync();
        }

        public async Task<ScanImage> SaveScanResultAsync(string imageBase64, string aiResult, int userId, string aiProvider)
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
                return scanImage;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> ChangeDeleteAsync(int scanImageId)
        {
            var scanImage = await _context.ScanImages.FirstOrDefaultAsync(s => s.ScanImageId == scanImageId);
            if (scanImage == null)
            {
                return false;
            }
            scanImage.IsDelete = false;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

