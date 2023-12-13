﻿using System.ComponentModel.DataAnnotations;

namespace harjoitustyoBackend.Models
{
    public class User
    {
        //-------------------------------------------
        public long Id { get; set; }
        //-------------------------------------------
        [Required]          //nämä vaikuttavat alla olevaan muuttujaan.
        [MinLength(3)]
        [MaxLength(255)]
        public string UserName { get; set; }
        //-------------------------------------------
        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
        public byte[]? Salt { get; set; }
        //-------------------------------------------
        [MaxLength(255)]
        public string? FirstName { get; set; }
        //-------------------------------------------
        [MaxLength(255)]
        public string? LastName { get; set; }
        //-------------------------------------------
        public DateTime? JoinDate { get; set; }
        //-------------------------------------------
        public DateTime? LastLogin { get; set; }
    }

    public class UserDTO
    {
        //-------------------------------------------
       
        //-------------------------------------------
        [Required]          //nämä vaikuttavat alla olevaan muuttujaan.
        [MinLength(3)]
        [MaxLength(255)]
        public string UserName { get; set; }
        //-------------------------------------------
        
        //-------------------------------------------
        [MaxLength(255)]
        public string? FirstName { get; set; }
        //-------------------------------------------
        [MaxLength(255)]
        public string? LastName { get; set; }
        //-------------------------------------------
        public DateTime? JoinDate { get; set; }
        //-------------------------------------------
        public DateTime? LastLogin { get; set; }
    }

}