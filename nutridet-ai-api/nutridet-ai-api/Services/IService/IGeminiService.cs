namespace nutridet_ai_api.Services.IService
{
    public interface IGeminiService
    {
        Task<string> GenerateAsync(string prompt);
    }
}
