using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace xtbdEcommerceAdminPanel.Models.RequestDto
{
    public class PasswordChangeDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }

        [Required(ErrorMessage = "New Password required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("Password", ErrorMessage = "Password doesn't match.")]
        public string ConfirmPassword { get; set; }

    }
}