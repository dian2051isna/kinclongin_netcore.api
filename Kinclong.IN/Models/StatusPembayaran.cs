using System.ComponentModel.DataAnnotations;

namespace KinclongIN.Models
{
    public class StatusPembayaran
    {
        [Key]
        public int IdStatusPembayaran { get; set; }
        public string NamaStatus { get; set; }
    }
}
