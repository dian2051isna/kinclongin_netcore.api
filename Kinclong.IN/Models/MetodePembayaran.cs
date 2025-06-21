using System.ComponentModel.DataAnnotations;

namespace KinclongIN.Models
{
    public class MetodePembayaran
    {
        [Key]
        public int IdMetode { get; set; }
        public string NamaMetodePembayaran { get; set; }
    }
}
