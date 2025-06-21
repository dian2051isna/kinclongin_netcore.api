namespace KinclongIN.Models
{
    public class OrderDetail
    {
        public int IdDetail { get; set; }
        public int JumlahJasa { get; set; }
        public decimal HargaSatuan { get; set; }
        public decimal Subtotal { get; set; }

        public int IdOrder { get; set; }
        public Orders Orders { get; set; }

        public int IdJasa { get; set; }
        public Services Services { get; set; }
    }
}
