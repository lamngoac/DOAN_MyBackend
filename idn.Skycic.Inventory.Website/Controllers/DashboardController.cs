using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class DashboardController : AdminBaseController
    {
        // GET: Dashboard
        public ActionResult Index(string networkid, string parentid)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var mst = userState.MST;
            ViewBag.MST = mst;
            var today = Today;
            ViewBag.Today = today;

            return View();
        }
    }
}