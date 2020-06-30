using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using Data_Access_Layer.Entities;

namespace NewsPortal.Managers
{
    public class SignInManager : SignInManager<User, int>
    {
        public SignInManager(UserManager<User, int> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) { }

        public void SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent)
        {
            if (UserManager == null)
            {
                return SignInStatus.Failure;
            }

            var user = await UserManager.FindByNameAsync(userName);

            if (user == null)
            {
                return SignInStatus.Failure;
            }

            if (await UserManager.CheckPasswordAsync(user, password))
            {
                await SignInAsync(user, isPersistent, false);
                return SignInStatus.Success;
            }

            return SignInStatus.Failure;
        }
    }
}