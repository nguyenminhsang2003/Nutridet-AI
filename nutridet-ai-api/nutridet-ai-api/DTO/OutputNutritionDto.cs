namespace nutridet_ai_api.DTO
{
    public class OutputNutritionDto
    {
        public decimal? energyKcal { get; set; }
        public decimal? carbohydrateG { get; set; }
        public decimal? sugarG { get; set; }
        public decimal? proteinG { get; set; }
        public decimal? fatG { get; set; }
        public decimal? saturatedFatG { get; set; }
        public decimal? fiberG { get; set; }
        public decimal? sodiumMg { get; set; }
        public decimal? cholesterolMg { get; set; }
    }
}
