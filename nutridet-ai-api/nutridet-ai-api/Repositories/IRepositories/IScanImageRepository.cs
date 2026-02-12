namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IScanImageRepository
    {
        Task SaveScanResultAsync(string imageBase64, string aiResult, int userId, string aiProvider);
    }
}

