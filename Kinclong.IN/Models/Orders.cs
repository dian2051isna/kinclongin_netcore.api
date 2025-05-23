using System.ComponentModel.DataAnnotations;

namespace Kinclong.IN.Models
{
    public class Orders
    {
        [Key]
        public int IdOrder { get; set; }
        public DateTime TanggalOrder { get; set; }
        public decimal TotalHarga { get; set; }
        public string AlamatPickup { get; set; }
        public string AlamatDelivery { get; set; }
        public string Photo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int IdUser { get; set; }
        public Users User { get; set; }

        public int IdStatusOrder { get; set; }
        public StatusOrder StatusOrder { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<TransaksiPembayaran> TransaksiPembayarans { get; set; }
    }
}
