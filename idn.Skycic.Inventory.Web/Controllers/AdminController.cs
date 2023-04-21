using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult Index()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var userName = "";
            if (userState != null && !CUtils.IsNullOrEmpty(userState.UserName))
            {
                userName = userState.UserName.Trim();
            }
            ViewBag.UserName = userName;
            return View();
        }

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowPopupImportExcel()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                return JsonView("_ImportExcel", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_ImportExcel", null, resultEntry);
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDatetimeServer()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var datetime = DateTimeNow();
                return Json(new { Success = true, DateTime = datetime });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
    }
}