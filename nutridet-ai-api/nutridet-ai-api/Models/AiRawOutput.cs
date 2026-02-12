using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nutridet_ai_api.Models
{
    public class AiRawOutput
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AiRawId { get; set; }

        public int ScanImageId { get; set; }

        [Column(TypeName = "text")]
        public string? RawTextResponse { get; set; }

        [Column(TypeName = "text")]
        public string? RawJsonResponse { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ScanImage ScanImage { get; set; }
    }
}
