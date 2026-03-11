using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.Models
{
    public class NutritionExcerciseRule
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Nutrient { get; set; }

        [Column(TypeName = "numeric(10,2)")]
        public decimal ReferenceAmount { get; set; }

        [StringLength(50)]
        public string Excercise { get; set; }

        [StringLength(50)]
        public string Unit { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
