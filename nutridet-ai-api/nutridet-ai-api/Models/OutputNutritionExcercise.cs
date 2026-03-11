using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.Models
{
    public class OutputNutritionExcercise
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int OutputNutritionId { get; set; }

        [MaxLength(50)]
        public string Nutrient { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? OriginalValue { get; set; }

        [MaxLength(100)]
        public string Excercise { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal? ExcerciseValue { get; set; }

        public OutputNutrition OutputNutrition { get; set; }
    }
}
