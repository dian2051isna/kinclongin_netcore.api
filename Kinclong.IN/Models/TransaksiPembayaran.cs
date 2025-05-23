using System.ComponentModel.DataAnnotations;

namespace Kinclong.IN.Models
{
    public class TransaksiPembayaran
    {
        [Key]
        public int IdTransaksi { get; set; }
        public DateTime TanggalBayar { get; set; }
        public decimal JumlahBayar { get; set; }

        public int IdOrder { get; set; }
        public Orders Orders { get; set; }

        public int MetodePembayaranId { get; set; }
        public MetodePembayaran MetodePembayaran { get; set; }

        public int StatusPembayaranId { get; set; }
        public StatusPembayaran StatusPembayaran { get; set; }
    }
}
