using System.ComponentModel.DataAnnotations;
using EngContent;

namespace NewsPortal.Models
{
    public class RegisterViewModel
    {
        public int Id { get; set; }

        [RegularExpression(@"[A-Za-zА-Яа-яЁё]+(\s+[A-Za-zА-Яа-яЁё]+)?", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Name")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Name")]
        public string Name { get; set; }

        [RegularExpression(@"[A-Za-zА-Яа-яЁё]+(\s+[A-Za-zА-Яа-яЁё]+)?", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Surname")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Surname")]
        public string Surname { get; set; }

        [EmailAddress(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Email")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Email")]
        public string Email { get; set; }

        [StringLength(maximumLength: 20, ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Login_Length")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_RegisterView_Login_Required")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [RegularExpression(@"[A-Za-z0-9]{6,12}", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_Password")]
        [Required(ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Compare("Password", ErrorMessageResourceType = typeof(ContentEng), ErrorMessageResourceName = "Model_Password_Confirm")]
        public string ConfirmPassword { get; set; }
    }
}