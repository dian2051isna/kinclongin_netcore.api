using System.Data;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KinclongIN.Models;

namespace KinclongIN.Models
{
    public class Users
    {
        [Key]
        public int IdUser { get; set; }
        public string Nama { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string NomorTelepon { get; set; }
        public string Alamat { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Orders> Orders { get; set; }
        public ICollection<Bookings> Bookings { get; set; }
        public Role Role { get; set; }
    }
}
