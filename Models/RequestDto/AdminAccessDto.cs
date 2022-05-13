using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class AdminAccessDto
    {
        [JsonProperty("userId")]
        public int UserId { get; set; }

        [JsonProperty("password")]
        [Required(ErrorMessage = "New Password required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

        [JsonProperty("email")]
        [Required(ErrorMessage = "Email address required")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
    }
}