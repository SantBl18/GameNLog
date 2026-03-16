using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameNLog.Models
{
    public class Cover
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CoverID { get; set; }
        [Required]
        public required string ImageID { get; set; }
    }
}
