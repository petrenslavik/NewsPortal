using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using Data_Access_Layer.Entities;

namespace NewsPortal.Managers
{
    public class UserManager : UserManager<User, int>
    {
        public UserManager(IUserStore<User, int> store) : base(store)
        {
            UserValidator = new UserValidator<User, int>(this)
            {
                RequireUniqueEmail = true,
                AllowOnlyAlphanumericUserNames = true
            };
            PasswordValidator = new PasswordValidator()
            {
                RequireDigit = true,
                RequiredLength = 6,
                RequireLowercase = true,
                RequireUppercase = true
            };
            EmailService = new EmailService();
            var provider = new DpapiDataProtectionProvider("NewsPortal");
            UserTokenProvider = new DataProtectorTokenProvider<User, int>(provider.Create("ResetPassword"));
        }
    }
}