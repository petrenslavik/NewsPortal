using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using EngContent;

namespace NewsPortal.Models
{
    public class ResetPasswordViewModel
    {
        public string Token { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [RegularExpression(@"[A-Za-z0-9]{6,12}", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_Password")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_Password_Confirm")]
        public string ConfirmPassword { get; set; }
    }
}