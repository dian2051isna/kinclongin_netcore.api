﻿using System.ComponentModel.DataAnnotations;

namespace KinclongIN.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string NamaRole { get; set; }
        public string DeskripsiRole { get; set; }

        public ICollection<Users> Users { get; set; }
    }
}
