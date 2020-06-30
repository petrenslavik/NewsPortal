using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NewsPortal.BusinessEntities
{
    public class Users
    {
        public virtual int Id { get; set; }

        [RegularExpression(@"[A-ZА-ЯЁ][a-zа-яё]+", ErrorMessage = "You specify your name wrong")]
        [Required(ErrorMessage = "You must specify your name")]
        public virtual string Name { get; set; }

        [RegularExpression(@"[A-ZА-ЯЁ][a-zа-яё]+", ErrorMessage = "You must specify your surname wrong")]
        [Required(ErrorMessage = "You must specify your surname")]
        public virtual string Surname { get; set; }

        [RegularExpression(@"^([a-z0-9_\.-]+)@([a-z0-9_\.-]+)\.([a-z\.]{2,6})$", ErrorMessage = "You specify your Email wrong")]
        [Required(ErrorMessage = "You must specify your Email")]
        public virtual string Email { get; set; }

        [Required(ErrorMessage = "You must specify your login")]
        public virtual string Login { get; set; }

        [RegularExpression(@"[A-Za-z0-9]{6,12}", ErrorMessage = "You specify your password wrong(must be 6 - 12 signs)")]
        [Required(ErrorMessage = "You must specify your password")]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

        [Required(ErrorMessage = "You must specify your password one more time")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public virtual string ConfirmPassword { get; set; }
    }
}