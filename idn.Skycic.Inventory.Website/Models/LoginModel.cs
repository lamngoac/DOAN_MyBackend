using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Website.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "User code")]
        public string UserCodeLogin { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string PasswordLogin { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public string RedirectUrl { get; set; }
    }
}