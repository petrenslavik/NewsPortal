using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Data_Access_Layer.Entities;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using NewsPortal.Managers;
using NewsPortal.Models;
using Microsoft.AspNet.Identity;
using System.Net.Mail;
using System.Net;
using System;
using System.Threading;
using EngContent;

namespace NewsPortal.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public AuthenticationController(IUnitOfWork unitOfWork,IUserRepository userRepository)
        {
            _uow = unitOfWork;
            this._userRepository = userRepository;
        }

        private SignInManager SignInManager => HttpContext.GetOwinContext().Get<SignInManager>();

        private UserManager UserManager => HttpContext.GetOwinContext().GetUserManager<UserManager>();

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Logout()
        {
            SignInManager.SignOut();
            return RedirectToAction("Index", "News");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new User
            {
                UserName = model.Login,
                Email = model.Email,
                Name = model.Name,
                PasswordHash = model.Password,
                Surname = model.Surname,
                ConfirmEmail = false
            };

                var result = UserManager.Create(user, model.Password);
                _uow.Commit();

                if (result.Succeeded)
                {
                    var code = UserManager.GenerateEmailConfirmationToken(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Authentication", new { userId = user.Id, email = user.Email, token = code }, protocol: Request.Url.Scheme);
                    UserManager.SendEmail(user.Id, ContentEng.AuthentificationController_Register_EmailSubject, ContentEng.AuthentificationController_Register_EmailContent+"<a href=\"" + callbackUrl + "\">"+ContentEng.AuthentificationController_Register_EmailLinkContent+"</a>");
                    return RedirectToAction("PageReportsAbout", new { content = ContentEng.AuthentificationController_Register_EmailInstructionToComplete + user.Email });
                }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PageReportsAbout(string content)
        {
            ViewBag.Content = content;
            return View("PageReportsAbout");
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(int userId, string email, string token)
        {
            var user = _userRepository.Get(userId);
            if (user == null)
                return RedirectToAction("PageReportsAbout", new { content = ContentEng.AuthentificationController_ConfirmEmail_MessageAboutUnexistingMail + user.Email });
            if (user.Email != email)
                    return RedirectToAction("PageReportsAbout", new { content = ContentEng.AuthentificationController_ConfirmEmail_MessageAboutRegistrarionInstruction + user.Email });
            UserManager.ConfirmEmail(userId, token);
            _uow.Commit();
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", ContentEng.AuthentificationController_Login_MessageAboutIncorrectUserData);
                return View(model);
            }

            var user = _userRepository.Get(model.Login);

            if (user == null)
            {
                ModelState.AddModelError("", ContentEng.AuthentificationController_Login_MessageAboutIncorrectName);
                return View(model);
            }

            if (!user.ConfirmEmail)
            {
                ModelState.AddModelError("", ContentEng.AccountAuthentificationController_MessageAboutUnconfirmedMail);
                return View(model);
            }

            var result = await SignInManager.PasswordSignInAsync(model.Login, model.Password, false);
            if (result == SignInStatus.Success)
                return RedirectToAction("Index", "News");
            ModelState.AddModelError("", ContentEng.AuthentificationController_Login_MessageAboutIncorrectUserData);
            return View(model);
        }
    }
}