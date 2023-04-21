using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.WebAdmin.Extensions;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class AccessDenyController : BaseController
    {
        // GET: AccessDeny
        public ActionResult Index()
        {
            return View();
        }
    }
}