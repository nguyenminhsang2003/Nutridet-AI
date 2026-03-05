using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.Models
{
    public class OutputNutritionVisual
    {
        [Key]
        public int Id { get; set; }

        public int OutputNutritionId { get; set; }

        [MaxLength(50)]
        public string Nutrient { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? OriginalValue { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? VisualAmount { get; set; }

        [MaxLength(100)]
        public string VisualName { get; set; }
        public OutputNutrition OutputNutrition { get; set; }

    }
}
