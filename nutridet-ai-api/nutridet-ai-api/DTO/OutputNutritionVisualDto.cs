using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.DTO
{
    public class OutputNutritionVisualDto
    {

        public string Nutrient { get; set; }

        public decimal? OriginalValue { get; set; }

        public decimal? VisualAmount { get; set; }

        public string VisualName { get; set; }
    }
}
