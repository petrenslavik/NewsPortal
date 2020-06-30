using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortal.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
    }
}