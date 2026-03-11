namespace nutridet_ai_api.DTO
{
    public class OutputNutritionExcerciseDto
    {
        public string Nutrient { get; set; }

        public decimal? OriginalValue { get; set; }

        public string Excercise { get; set; }

        public decimal? ExcerciseValue { get; set; }
    }
}
