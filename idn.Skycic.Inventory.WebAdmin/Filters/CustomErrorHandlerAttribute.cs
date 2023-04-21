using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.WebAdmin.Extensions;

namespace idn.Skycic.Inventory.WebAdmin.Filters
{
    public class CustomErrorHandlerAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {


            var ex = filterContext.Exception;



            //var viewData = new ViewDataDictionary()
            //    {
            //        {"Exception", new ServiceException(ex) },

            //    };

            var viewData = new ViewDataDictionary();
            if (ex is ClientException)
            {
                var cex = ex as ClientException;
                viewData.Add(new KeyValuePair<string, object>("Exception", cex));
            }
            else
            {
                viewData.Add(new KeyValuePair<string, object>("Exception", ex));
            }


            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "LogOn" }));

            var request = filterContext.RequestContext.HttpContext.Request;
            var rw = request.Headers.Get("X-Requested-With");
            filterContext.ExceptionHandled = true;


            var response = filterContext.RequestContext.HttpContext.Response;
            string message = "";
            //ajax
            if (!string.IsNullOrEmpty(rw) && rw.Equals("XMLHttpRequest", StringComparison.InvariantCultureIgnoreCase))
            {
                //message = HtmlExtensions.RenderPartialViewToString(filterContext.Controller.ControllerContext, "FilteredErrorAjax", null, viewData);
                message = HtmlExtensions.RenderPartialViewToString(filterContext.Controller.ControllerContext, "ServiceException", null, viewData);
                response.StatusCode = 999;

            }
            //not ajax
            else
                //post to frame
                if (filterContext.RequestContext.HttpContext.Request.HttpMethod.Equals("POST", StringComparison.InvariantCultureIgnoreCase)
                    && (request["insideframe"] ?? "").Equals("true", StringComparison.InvariantCultureIgnoreCase)

                    )
            {
                //message = HtmlExtensions.RenderPartialViewToString(filterContext.Controller.ControllerContext, "FilteredErrorPost", null, viewData);
                message = HtmlExtensions.RenderPartialViewToString(filterContext.Controller.ControllerContext, "ServiceException", null, viewData);
                response.StatusCode = 999;
            }

            //get
            else
            {

                message = HtmlExtensions.RenderPartialViewToString(filterContext.Controller.ControllerContext, "ServiceException", null, viewData);
                //response.StatusCode = 400;
                response.StatusCode = 200;
            }
            response.Write(message);
            response.StatusDescription = filterContext.Exception.Message;
            response.End();

        }
    }
}