using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace idn.Skycic.Inventory.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region["Comment"]
            //routes.MapRoute(
            //    "dang-ky",
            //    "agent/register",
            //    new { controller = "Register", action = "Create" },
            //    new[] { "idn.Skycic.Inventory.Web.Controllers" }
            //);

            //routes.MapRoute(
            //    "Default",
            //    "{networkid}/{controller}/{action}/{id}",
            //    new { networkid = "n0", controller = "Home", action = "Index", id = UrlParameter.Optional },
            //    new[] { "idn.Skycic.Inventory.Web.Controllers" }
            //);

            ////routes.MapRoute(
            ////    name: "Default",
            ////    url: "{controller}/{action}/{id}",
            ////    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            ////);
            #endregion

            #region[""]
            var networkid = "";
            if (System.Web.HttpContext.Current.Session != null)
            {
                if (System.Web.HttpContext.Current.Session["networkid"] != null)
                {
                    networkid = System.Web.HttpContext.Current.Session["networkid"].ToString().Trim();
                }
            }

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "dang-ky",
                "agent/register",
                new { controller = "Register", action = "Create" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dang-ky-network",
                "agent/registernetwork",
                new { controller = "Register", action = "CreateNetwork" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dang-ky-network-khong-tao-org",
                "agent/registernetworknocreateorg",
                new { controller = "Register", action = "CreateNetworkNoCreateOrg" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dang-nhap",
                "login",
                new { controller = "Account", action = "Login" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dang-nhap-os",
                "loginos",
                new { controller = "Account", action = "LoginOS" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "active",
                "register/active",
                new { controller = "Register", action = "Active", mst = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "active-acccount",
                "register/activeacc",
                new { controller = "Register", action = "ActiveAccount", uuid = UrlParameter.Optional, networkid = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "forget-password",
                "forget-password",
                new { controller = "Register", action = "ForgetPassword", id = UrlParameter.Optional},
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "reset-password",
                "reset-password/{code}",
                new { controller = "Register", action = "ResetPassword", key = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "recaptcha",
                "recaptcha",
                new { controller = "Register", action = "ConfirmCode"/*, usercode = UrlParameter.Optional, grecaptcharesponse = UrlParameter.Optional */},
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "send-email-verification-email",
                "send-email-verification-email",
                new { controller = "Register", action = "SendEmailVerificationEmail", email = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "verify-email",
                "verify-email",
                new { controller = "Register", action = "VerifyEmail" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "order-service-get-discount-code",
                "order-service-get-discount-code",
                new { controller = "Register", action = "OS_Inos_OrderService_GetDiscountCode" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "create-payment",
                "create-payment",
                new { controller = "Register", action = "CreatePayment" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "show-popup-gen-network",
                "show-popup-gen-network",
                new { controller = "Register", action = "ShowPopupGenNetwork" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "check-order-status",
                "check-order-status",
                new { controller = "Register", action = "CheckOrderStatus" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dashboard",
                "{networkid}/dashboard",
                new { networkid = networkid, controller = "Dashboard", action = "Index" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );

            routes.MapRoute(
                "load-trattin",
                "load-trattin",
                new { controller = "LoadData", action = "GetInfo", mst = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );

            routes.MapRoute(
                "load-district",
                "load-district",
                new { controller = "LoadData", action = "LoadMst_District", provincecode = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );

            routes.MapRoute(
                "load-govtaxid",
                "load-govtaxid",
                new { controller = "LoadData", action = "LoadMst_GovTaxID", districtcode = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            //
            routes.MapRoute(
                "dms-invoice",
                "dms-invoice",
                new { controller = "DMS_Invoice", action = "Index", xosdmsrefidx = UrlParameter.Optional, xnkx = UrlParameter.Optional, xux = UrlParameter.Optional, xpx = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dms-create-invoice",
                "dms-create-invoice",
                new { controller = "DMS_Invoice", action = "CreateInvoice"},
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dms-delete-invoice",
                "dms-delete-invoice",
                new { controller = "DMS_Invoice", action = "DeleteInvoice" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dms-save-document-send-mail",
                "dms-save-document-send-mail",
                new { controller = "DMS_Invoice", action = "SaveDocumentAndSendMail" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "dms-update-multi-sign-status",
                "dms-update-multi-sign-status",
                new { controller = "DMS_Invoice", action = "OS_Invoice_InvoiceTemp_UpdMultiSignStatus" },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );
            routes.MapRoute(
                "Default",
                "{networkid}/{controller}/{action}/{id}",
                new { networkid = networkid, controller = "Home", action = "Index", id = UrlParameter.Optional },
                new[] { "idn.Skycic.Inventory.Web.Controllers" }
            );

            //routes.MapRoute(
            //    "Default",
            //    "{networkid}/{controller}/{action}/{id}",
            //    new { networkid = networkid, controller = "Account", action = "Login", id = UrlParameter.Optional },
            //    new[] { "idn.Skycic.Inventory.Web.Controllers" }
            //);

            //routes.MapRoute(
            //    "Default",
            //    "{controller}/{action}",
            //    new { networkid = networkid, controller = "Account", action = "Login" },
            //    new[] { "idn.Skycic.Inventory.Web.Controllers" }
            //);
            #endregion

        }
    }
}
