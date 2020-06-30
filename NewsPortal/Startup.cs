using System.Web.Mvc;
using Data_Access_Layer.Identity;
using Data_Access_Layer.Interfaces;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NewsPortal.Managers;

[assembly: OwinStartup(typeof(NewsPortal.Startup))]
namespace NewsPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(() => new UserManager(DependencyResolver.Current.GetService<IdentityStore>()));
            app.CreatePerOwinContext<SignInManager>((options, context) => new SignInManager(context.GetUserManager<UserManager>(), context.Authentication));
         
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Authentication/Login"),
                Provider = new CookieAuthenticationProvider()
            });
        }
    }
}
