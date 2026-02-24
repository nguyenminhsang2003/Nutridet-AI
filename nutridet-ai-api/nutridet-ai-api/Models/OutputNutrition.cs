using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.Models
{
    public class OutputNutrition
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AiRawId { get; set; }

        public int ScanImageId { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? EnergyKcal { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? CarbohydrateG { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? SugarG { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? ProteinG { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? FatG { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? SaturatedFatG { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? FiberG { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? SodiumMg { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? CholesterolMg { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ScanImage ScanImage { get; set; }
    }
}

