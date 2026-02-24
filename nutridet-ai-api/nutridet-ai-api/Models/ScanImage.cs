using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.Models
{
    public class ScanImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScanImageId { get; set; }

        public int UserId { get; set; }

        [MaxLength(500)]
        public string? ImageUrl { get; set; }

        [MaxLength(50)]
        public string? AiProvider { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string? RawTextResponse { get; set; }

        [Column(TypeName = "numeric(4,2)")]
        public decimal? AiConfidence { get; set; }

        [MaxLength(10)]
        public string? ConfidenceStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public bool IsDelete { get; set; } = false;

        public User User { get; set; }

        public OutputNutrition OutputNutrition { get; set; }
    }
}
