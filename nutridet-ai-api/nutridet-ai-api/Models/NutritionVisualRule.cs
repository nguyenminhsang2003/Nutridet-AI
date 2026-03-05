using System.ComponentModel.DataAnnotations;

namespace nutridet_ai_api.Models
{
    public class NutritionVisualRule
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nutrient { get; set; }

        public decimal ReferenceAmount { get; set; }

        public string VisualName { get; set; }
    }
}
