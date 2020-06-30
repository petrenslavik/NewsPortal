using System.Web;
using System.Web.Mvc;
using Data_Access_Layer.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using NewsPortal.Managers;
using NewsPortal.Models;
using EngContent;

namespace NewsPortal.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _uow;

        public AccountController(IUnitOfWork unitOfWork,IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _uow = unitOfWork;
        }

        public UserManager UserManager => HttpContext.GetOwinContext().GetUserManager<UserManager>();

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var user = _userRepository.GetByEmail(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("", ContentEng.AccountController_ForgotPassword_ModelError);
                return View(model);
            }

            if (user.ConfirmEmail)
            {
                ModelState.AddModelError("", ContentEng.AccountAuthentificationController_MessageAboutUnconfirmedMail);
                return View(model);
            }

            var code = UserManager.GeneratePasswordResetToken(user.Id);
            var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, email = user.Email, token = code }, protocol: Request.Url.Scheme);
            UserManager.SendEmail(user.Id, ContentEng.AccountController_ForgotPassword_SendEmail_ResetPassword, ContentEng.AccountController_ForgotPassword_ResetLinkMessage + "<a href=\"" + callbackUrl + "\">" + ContentEng.AccountController_ForgotPassword_SendEmail_ResetPassword + "</a>");
            return RedirectToAction("PageReportsAbout", "Authentication", new { content = ContentEng.AccountController_ForgotPassword_ResetInstruction_Content + model.Email });
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string email, string token)
        {
            var resetPassword = new ResetPasswordViewModel
            {
                Email = email,
                Token = token
            };
            return View(resetPassword);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
                return View();

            var user = _userRepository.GetByEmail(model.Email);

            if (user == null)
            {
                ModelState.AddModelError("",ContentEng.AccountController_ResetPassword_MessaheAboutBadOccure);
                return View(model);
            }
            UserManager.ResetPassword(user.Id, model.Token, model.Password);
            _uow.Commit();
            return RedirectToAction("PageReportsAbout", "Authentication", new { content = ContentEng.AccountController_ResetPassword_Report_Content });
        }

    }
}