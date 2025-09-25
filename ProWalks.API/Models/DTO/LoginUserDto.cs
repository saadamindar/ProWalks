﻿using System.ComponentModel.DataAnnotations;

namespace ProWalks.API.Models.DTO
{
    public class LoginUserDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
