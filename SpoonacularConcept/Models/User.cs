﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpoonacularConcept.Models
{
    public class User
    {
        public string UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
    public enum AuthStatus
    {
        UserNotFound=1,
        WrongPassword=2,
        Authenticated=3
    }
}