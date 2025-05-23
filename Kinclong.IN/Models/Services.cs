namespace Kinclong.IN.Models
{
    public class Services
    {
        public int id_service { get; set; }
        public string service_name { get; set; }
        public string service_category { get; set; }
        public string service_price { get; set; }
        public string service_photo { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }


        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
