using System.ComponentModel.DataAnnotations;

namespace KinclongIN.Models
{
    public class TrackingKurir
    {
        [Key]
        public int IdTracking { get; set; }
        public string StatusTracking { get; set; }
    }
}
