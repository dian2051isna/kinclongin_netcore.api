namespace KinclongIN.Models
{
    public class StatusOrder
    {
        public int IdStatusOrder { get; set; }
        public string NamaStatus { get; set; }

        public ICollection<Orders> Orders { get; set; }
    }
}
