using Microsoft.EntityFrameworkCore;
using nutridet_ai_api.Models;
using nutridet_ai_api.Repositories.IRepositories;

namespace nutridet_ai_api.Repositories
{
    public class AiRawOutputRepository : IAiRawOutputRepository
    {
        private readonly NutridetAiDbContext _context;

        public AiRawOutputRepository(NutridetAiDbContext context)
        {
            _context = context;
        }

        public async Task SaveAiRawOutputAsync(int scanImageId, string rawTextResponse)
        {
            var aiRawOutput = new AiRawOutput
            {
                ScanImageId = scanImageId,
                RawTextResponse = rawTextResponse,
                CreatedAt = DateTime.UtcNow
            };

            _context.AiRawOutputs.Add(aiRawOutput);
            await _context.SaveChangesAsync();
        }
    }
}

