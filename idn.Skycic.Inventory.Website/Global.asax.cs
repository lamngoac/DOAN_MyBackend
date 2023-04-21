using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace idn.Skycic.Inventory.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain
                    chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;

                };
            var inosBaseUrl = System.Configuration.ConfigurationManager.AppSettings["InosBaseUrl"];
            var ssoSecret = System.Configuration.ConfigurationManager.AppSettings["SSOSecret"];
            var solutionCode = System.Configuration.ConfigurationManager.AppSettings["SolutionCode"];
            inos.common.Service.InosClientServiceBase.Init(solutionCode, inosBaseUrl, ssoSecret);

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
            // Fix iframe
            Response.Headers.Remove("X-Frame-Options");
            Response.AddHeader("X-Frame-Options", "AllowAll");
        }
        // 3: Loại bỏ X-Powered-By (webconfig)

        // Auto Redirect http:// => https://
        protected void Application_BeginRequest()
        {
            ////if (FormsAuthentication.RequireSSL && !Request.IsSecureConnection)
            ////{
            ////    Response.Redirect(Request.Url.AbsoluteUri.Replace("http://", "https://"));
            ////}

            var hethong = System.Configuration.ConfigurationManager.AppSettings["HeThong"];
            if (!Request.IsLocal && hethong != "TEST")
                EnsureSslAndDomain(HttpContext.Current, true, true);
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
