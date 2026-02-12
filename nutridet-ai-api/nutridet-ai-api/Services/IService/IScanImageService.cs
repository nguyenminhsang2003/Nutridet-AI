namespace nutridet_ai_api.Services.IService
{
    public interface IScanImageService
    {
        Task<string> ScanImageAsync(string imageBase64, int userId);
    }
}
