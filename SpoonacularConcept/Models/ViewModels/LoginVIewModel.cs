using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpoonacularConcept.Models.ViewModels
{
    public class LoginVIewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public int UserId { get; set; }

        public AuthStatus authStatus;
    }
}