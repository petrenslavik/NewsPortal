using System;
using System.Globalization;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Castle.Core.Internal;
using System.Collections.Specialized;

namespace NewsPortal.Attributes 
{
    public class CanonicalizeAttribute : ActionFilterAttribute
    {
        private NameValueCollection AppSettings
        {
            get { return WebConfigurationManager.AppSettings; }
        }

        public bool RedirectToWwwHost { get; set; }
        public bool ForceLowerCase { get; set; }
        public bool ForceHttps { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContextBase context = filterContext.HttpContext;
            

            if (context.Request.Url != null)
            {
                string path = context.Request.Url.AbsolutePath ?? "/";
                string query = context.Request.Url.Query ?? "";
                string host = context.Request.Url.Host;

                string redirectUrl = "";

                if (RedirectToWwwHost && (!IsAllowedHostName(host)))
                    redirectUrl = GenerateRedirect(AppSettings["preferredHostName"], path, query);

                if (ForceHttps && (!context.Request.IsSecureConnection))
                    redirectUrl = GenerateRedirect(GetRedirectHost(host), path, query);

                if (ForceLowerCase && (!RequestedPathIsLowerCase(path)))
                    redirectUrl = GenerateRedirect(GetRedirectHost(host), path, query);

                if (!redirectUrl.IsNullOrEmpty())
                {
                    filterContext.Result = new RedirectResult(redirectUrl, true);
                    return;
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private bool IsAllowedHostName(string host)
        {
            if (host.Equals(AppSettings["preferredHostName"], StringComparison.CurrentCultureIgnoreCase))
                return true;

            return false;
        }

        private bool RequestedPathIsLowerCase(string path)
        {
            string loweredPath = path.ToLower(CultureInfo.InvariantCulture);

            return path.Equals(loweredPath, StringComparison.InvariantCulture);
        }

        private string GetRedirectHost(string host)
        {
            if (IsAllowedHostName(host))
                return host.ToLower();

            return AppSettings["preferredHostName"];
        }

        private string GenerateRedirect(string host, string path, string query)
        {
            string newPath = path;
            string newQuery = query;

            if (!newPath.IsNullOrEmpty() && (!newPath.StartsWith("/", StringComparison.InvariantCultureIgnoreCase)))
                newPath = "/" + newPath;

            if (!newQuery.IsNullOrEmpty() && (!newQuery.StartsWith("?", StringComparison.CurrentCultureIgnoreCase)))
                newQuery = "?" + newQuery;

            return (ForceHttps ? "https://" : "http://") + host + newPath.ToLower() + newQuery;
        }
    }
}