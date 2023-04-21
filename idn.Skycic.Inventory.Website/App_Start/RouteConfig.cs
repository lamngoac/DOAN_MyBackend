using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace idn.Skycic.Inventory.Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            var networkid = "";
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session["networkid"] != null)
                {
                    networkid = System.Web.HttpContext.Current.Session["networkid"].ToString().Trim();
                }
            }

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "logoff",
            //    "account/logoff",
            //    new { controller = "Account", action = "LogOff" },
            //    new[] { "idn.Skycic.Inventory.Website.Controllers" }
            //);
            //routes.MapRoute(
            //    "signout",
            //    "account/signout",
            //    new { controller = "Account", action = "SignOut" },
            //    new[] { "idn.Skycic.Inventory.Website.Controllers" }
            //);
            //routes.MapRoute(
            //    "login-callback",
            //    "account/logincallback",
            //    new { controller = "Account", action = "LoginCallback", id = UrlParameter.Optional },
            //    new[] { "idn.Skycic.Inventory.Website.Controllers" }
            //);
            routes.MapRoute(
                "logoff",
                "account/logoff",
                new { controller = "Account", action = "LogOff" },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );
            routes.MapRoute(
                "signout",
                "account/signout",
                new { controller = "Account", action = "SignOut" },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );
            routes.MapRoute(
                "login-callback",
                "account/logincallback",
                new { controller = "Account", action = "LoginCallback", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );

            routes.MapRoute(
                "login-rpt-callback",
                "reportacc/logincallback",
                new { controller = "ReportAcc", action = "LoginCallback", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );
            routes.MapRoute(
                "rpt-login",
                "rptlogin",
                new { controller = "ReportAcc", action = "Login", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );
            routes.MapRoute(
                "dashboard-rpt-invbalance",
                "dashboardrpt/inventorybalance",
                new { controller = "DashboardRpt", action = "WG_Rpt_Inv_InventoryBalance", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );

            routes.MapRoute(
                "dashboard-rpt-summary-in-pivot",
                "dashboardrpt/summaryinpivot",
                new { controller = "DashboardRpt", action = "WG_Rpt_Summary_In_Pivot", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );
            routes.MapRoute(
               "dashboard-rpt-summary-out-pivot",
               "dashboardrpt/summaryoutpivot",
               new { controller = "DashboardRpt", action = "WG_Rpt_Summary_Out_Pivot", id = UrlParameter.Optional },
               new[] { "idn.Skycic.Inventory.Website.Controllers" }
           );

            routes.MapRoute(
                "dang-nhap",
                "login",
                new { controller = "Account", action = "Login" },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );

            routes.MapRoute(
                "dang-nhap-os",
                "loginos",
                new { controller = "Account", action = "LoginOS" },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );

            routes.MapRoute(
                "Default",
                "{networkid}/{controller}/{action}/{id}",
                new { networkid = networkid, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Website.Controllers" }
            );

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
