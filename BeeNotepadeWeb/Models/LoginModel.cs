﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BeeNotepadeWeb.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string TgID { get; set; }
    }
}
