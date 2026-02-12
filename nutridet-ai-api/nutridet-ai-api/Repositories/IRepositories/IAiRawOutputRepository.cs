namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IAiRawOutputRepository
    {
        Task SaveAiRawOutputAsync(int scanImageId, string rawTextResponse);
    }
}

