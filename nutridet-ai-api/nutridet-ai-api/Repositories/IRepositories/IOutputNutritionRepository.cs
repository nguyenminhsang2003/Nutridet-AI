namespace nutridet_ai_api.Repositories.IRepositories
{
    public interface IOutputNutritionRepository
    {
        Task SaveOutputNutritionAsync(int scanImageId);
        Task SaveOutputNutritionAsync(int scanImageId, decimal? energyKcal, decimal? carbohydrateG, decimal? sugarG, decimal? proteinG, decimal? fatG, decimal? saturatedFatG, decimal? fiberG, decimal? sodiumMg, decimal? cholesterolMg);
    }
}

