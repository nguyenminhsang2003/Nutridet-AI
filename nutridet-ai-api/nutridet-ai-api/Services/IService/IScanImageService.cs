using Microsoft.AspNetCore.Mvc;
using nutridet_ai_api.DTO;
using nutridet_ai_api.Models;

namespace nutridet_ai_api.Services.IService
{
    public interface IScanImageService
    {
        Task<Object> ScanImageAsync(string imageBase64, int userId);
        Task<ScanImage?> GetInvokeAsync(int scanImageId);
        Task<List<ScanImage>> GetAllInvokeAsync(int userId, DateTime? startDate, DateTime? endDate, int? page, int? pageSize);

    }
}
