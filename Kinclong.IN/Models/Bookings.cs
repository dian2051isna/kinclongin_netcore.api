namespace Kinclong.IN.Models
{
    public class Bookings
    {
        public int id_booking { get; set; }
        public DateTime booking_date { get; set; }
        public string booking_status { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        public int IdUser { get; set; }
        public Users User { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
