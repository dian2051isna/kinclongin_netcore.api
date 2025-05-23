namespace KinclongIN.DTO
{
    public class BookingResponseDTO
    {
        public int id_booking { get; set; }
        public DateTime booking_date { get; set; }
        public string booking_status { get; set; }
        public int idUser { get; set; }
    }

}
