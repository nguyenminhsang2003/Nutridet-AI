using nutridet_ai_api.Models;

namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IScanImageRepository
    {
        Task<ScanImage> SaveScanResultAsync(string imageBase64, string aiResult, int userId, string aiProvider);
        Task<ScanImage?> GetInvokeAsync(int scanImageId);
        Task<bool> ChangeDeleteAsync(int scanImageId);
    }
}

