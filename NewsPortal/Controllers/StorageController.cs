using NewsPortal.Managers;
using NewsPortal.Managers.Ninject;
using NewsPortal.Models;
using System.Web;
using System.Web.Mvc;

namespace NewsPortal.Controllers
{
    public class StorageController : Controller
    {
        public RedirectToRouteResult Toggle(ToggleStorageInputModel model)
        {
            NinjectConfiguration.Configure(model.Storage);
            return RedirectToAction("Index", "News");
        }
    }
}