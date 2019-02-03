﻿using System.ComponentModel.DataAnnotations;

namespace Measuring_equipment.Models.ViewModels
{
    public class LoginModel2
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [UIHint("password")]
        public string Password { get; set; }

        public string ReturnUrl { get; set; } = "/";
    }
}