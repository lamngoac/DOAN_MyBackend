using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using idn.Skycic.Inventory.Web.State;

namespace idn.Skycic.Inventory.Web.Filters
{
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        public string Modules { get; set; }
        public string Functions { get; set; }


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
            {
                return false;
            }

            var session = new SessionWrapper(httpContext.Session);

            //var sysSession = session.SysSession;
            //return sysSession != null;

            return session != null;

            #region["comment"]
            // comment 2017-01-09: đã check phân quyền menu nên ko cần( check phân quyền hàm nếu có biz hỗ trợ check)
            //if (sysSession == null) return false;
            //if (sysSession.IsSysAdmin) return true;



            //if (!string.IsNullOrEmpty(Modules))
            //{
            //    var mlist = Modules.Split(',').Select(m => m.Trim()).ToList();
            //    if (mlist.Exists(m => sysSession.HasModule(m))) return true;
            //}


            //if (!string.IsNullOrEmpty(Functions))
            //{
            //    var flist = Functions.Split(',').Select(m => m.Trim()).ToList();
            //    if (flist.Exists(m => sysSession.HasFunction(m))) return true;
            //}

            //return false;
            #endregion
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Account",
                        action = "Login"
                    })
            );
        }
    }
}