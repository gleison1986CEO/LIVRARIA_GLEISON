﻿using System.ComponentModel.DataAnnotations;

namespace sistema.Models
{
    public class Login
    {

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }
       
        public string? auth2fa { get; set; }
        
        public string? token{ get; set; }  

        public string? secret{ get; set; }

        public bool  Remember { get; set; }
    }
}