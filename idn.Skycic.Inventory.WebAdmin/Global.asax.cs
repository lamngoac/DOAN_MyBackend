using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace idn.Skycic.Inventory.WebAdmin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 1: Loại bỏ thông tin X - AspNetMvc - Version ở header
            MvcHandler.DisableMvcResponseHeader = true;
        }

        protected void Application_PreSendRequestHeaders()
        {
            //2.1: Remove Server Header
            Response.Headers.Remove("Server");
            //2.2: Remove X-AspNet-Version Header
            Response.Headers.Remove("X-AspNet-Version");
        }
        // 3: Loại bỏ X-Powered-By (webconfig)

        // Auto Redirect http:// => https://
        protected void Application_BeginRequest()
        {
            //if (FormsAuthentication.RequireSSL && !Request.IsSecureConnection)
            //{
            //    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            //}

            //if (!Request.IsLocal)
            //    EnsureSslAndDomain(HttpContext.Current, true, true);

        }


        public static void EnsureSslAndDomain(HttpContext context, bool redirectWww, bool redirectSsl)
        {
            if (context == null)
                return;

            var redirect = false;
            var uri = context.Request.Url;
            var uriredirect = "";
            var scheme = uri.GetComponents(UriComponents.Scheme, UriFormat.Unescaped);

            if (redirectSsl && !scheme.Equals("https"))
            {
                uriredirect = uri.ToString().Replace("http", "https");
                redirect = true;
            }

            if (redirect)
            {
                context.Response.Redirect(uriredirect);
            }
        }
    }
}
