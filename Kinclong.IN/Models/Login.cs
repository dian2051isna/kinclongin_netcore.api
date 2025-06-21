namespace KinclongIN.Models
{
    public class Login
    {
        public int id_person { get; set; }
        public string nama { get; set; }
        public string alamat { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public int id_peran { get; set; }
        public string nama_peran { get; set; }
        public string token { get; set; }
    }
}
