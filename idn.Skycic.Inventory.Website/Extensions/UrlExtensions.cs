using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Extensions
{
    public static class UrlExtensions
    {
        //public static string LocalizedUrl(this string url)
        //{
        //    //return url;
        //    //var urlCur = "";
        //    //if (url.Equals("/"))
        //    //{
        //    //    return url;
        //    //}
        //    //else
        //    //{
        //    //    if (url.IndexOf("//", StringComparison.Ordinal) == 0)
        //    //    {
        //    //        urlCur = url.Replace("//", "/");
        //    //    }
        //    //}
        //    //var networkid = System.Web.HttpContext.Current.Session["networkid"];

        //    //if (urlCur.StartsWith("/" + networkid)) return urlCur;

        //    //return string.Format("/{0}{1}", networkid, urlCur);




        //    var networkid = System.Web.HttpContext.Current.Session["networkid"];

        //    if (url.StartsWith("/" + networkid)) return url;

        //    return string.Format("/{0}{1}", networkid, url);

        //}

        public static string LocalizedUrl(this string url)
        {
            //return url;
            var urlCur = "";
            if (url.Equals("/"))
            {
                return url;
            }
            else
            {
                urlCur = url.IndexOf("//", StringComparison.Ordinal) == 0 ? url.Replace("//", "/") : url;
            }
            var networkid = System.Web.HttpContext.Current.Session["networkid"];

            if (urlCur.StartsWith("/" + networkid)) return urlCur;

            return string.Format("/{0}{1}", networkid, urlCur);
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