namespace KinclongIN.Models
{
    public class Review
    {
        public int IdReview { get; set; }
        public int IdUser { get; set; }
        public int IdOrder { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
