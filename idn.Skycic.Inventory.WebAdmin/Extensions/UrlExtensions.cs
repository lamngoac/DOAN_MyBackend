using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.WebAdmin.Extensions
{
    public static class UrlExtensions
    {
        public static string LocalizedUrl(this string url)
        {
            return url;

            //var networdid = System.Web.HttpContext.Current.Session["networdid"];

            //if (url.StartsWith("/" + networdid)) return url;

            //return string.Format("/{0}{1}", networdid, url);
        }
        public static string ActionLocalized(this UrlHelper url, string actionName)
        {

            return url.Action(actionName.ToLower()).LocalizedUrl();

        }
        public static string ActionLocalized(this UrlHelper url, string actionName, string controllerName)
        {

            return url.Action(actionName.ToLower(), controllerName.ToLower()).LocalizedUrl();

        }
        public static string ActionLocalized(this UrlHelper url, string actionName, string controllerName, object routeValues)
        {

            return url.Action(actionName.ToLower(), controllerName.ToLower(), routeValues).LocalizedUrl();

        }
    }
}