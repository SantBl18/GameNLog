using System.ComponentModel.DataAnnotations;

namespace GameNLog.Models
{
    public class Cover
    {
        [Key]
        public int CoverID { get; set; }
        public int ImageID { get; set; }
    }
}
