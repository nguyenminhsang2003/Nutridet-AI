namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IOutputNutritionRepository
    {
        Task SaveOutputNutritionAsync(int scanImageId, string? aiResult);
    }
}

