using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace idn.Skycic.Inventory.Web.Extensions.Pager
{
    public static class PagerExtensions
    {
        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list)
        {
            return htmlHelper.Pager(list, null, null);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list, string labelPrevious, string labelNext)
        {
            return htmlHelper.Pager(list.PageSize, list.PageIndex, list.TotalItemCount, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string labelPrevious, string labelNext)
        {
            return htmlHelper.Pager(pageSize, currentPage, totalItemCount, null, null, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, string labelPrevious, string labelNext)
        {
            return htmlHelper.Pager(pageSize, currentPage, totalItemCount, actionName, null, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext)
        {
            return htmlHelper.Pager(pageSize, currentPage, totalItemCount, null, valuesDictionary, labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, object values, string labelPrevious, string labelNext)
        {
            return htmlHelper.Pager(pageSize, currentPage, totalItemCount, actionName, new RouteValueDictionary(values), labelPrevious, labelNext);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            return htmlHelper.Pager(pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values), labelPrevious, labelNext, labelFirst, labelLast);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            if (actionName != null)
            {
                if (valuesDictionary.ContainsKey("action"))
                {
                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
                }
                valuesDictionary.Add("action", actionName);
            }
            Pager pager = new Pager(htmlHelper, pageSize, currentPage, totalItemCount, valuesDictionary, labelPrevious, labelNext, labelFirst, labelLast);
            return pager.RenderHtml();
        }

        public static MvcHtmlString PagerBootstrap(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, object values, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            var strHtml = htmlHelper.PagerBootstrap(pageSize, currentPage, totalItemCount, null, new RouteValueDictionary(values), labelPrevious, labelNext, labelFirst, labelLast);
            return strHtml;
        }

        public static MvcHtmlString PagerBootstrap(this HtmlHelper htmlHelper, int pageSize, int currentPage, int totalItemCount, string actionName, RouteValueDictionary valuesDictionary, string labelPrevious, string labelNext, string labelFirst, string labelLast)
        {
            if (valuesDictionary == null)
            {
                valuesDictionary = new RouteValueDictionary();
            }
            if (actionName != null)
            {
                if (valuesDictionary.ContainsKey("action"))
                {
                    throw new ArgumentException("The valuesDictionary already contains an action.", "actionName");
                }
                valuesDictionary.Add("action", actionName);
            }
            Pager pager = new Pager(htmlHelper, pageSize, currentPage, totalItemCount, valuesDictionary, labelPrevious, labelNext, labelFirst, labelLast);
            return pager.RenderHtmlBootstrap();
        }
    }
}