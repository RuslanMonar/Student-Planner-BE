﻿using System.ComponentModel.DataAnnotations;

namespace Student_Planner.Dto
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string UserName { get; set; }

    }
}
