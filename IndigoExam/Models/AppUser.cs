﻿using Microsoft.AspNetCore.Identity;

namespace IndigoExam.Models
{
    public class AppUser:IdentityUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Surname { get; set; }
    }
}
