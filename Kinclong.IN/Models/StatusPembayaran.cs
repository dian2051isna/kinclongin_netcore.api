using System.ComponentModel.DataAnnotations;

namespace Kinclong.IN.Models
{
    public class StatusPembayaran
    {
        [Key]
        public int IdStatusPembayaran { get; set; }
        public string NamaStatus { get; set; }
    }
}
