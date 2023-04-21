using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
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
            ////System.Web.HttpContext.Current.RewritePath("KeepAlive.ashx");
            ////System.Web.HttpContext.Current.Server.Transfer("KeepAlive.ashx");
            //////Server.Transfer("KeepAlive.ashx");
            //var ViewDataTempData = TempData["ViewDataTempData"];

            //var stringBuilder = new StringBuilder();
            //var strLogs = "";
            //var PathFileLog = "";
            //CUtils.StrAppend(stringBuilder, "Màn hình Dashboard:");
            //CUtils.StrAppend(stringBuilder, "Method: Get");
            //if (!CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["PathFileLog"]))
            //{
            //    PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
            //    var iindex = PathFileLog.IndexOf("File_Dashboard");
            //    if (iindex < 0)
            //    {
            //        var iCount = ReturnCount();
            //        CreateFileWriteLog("File_Dashboard", Today, iCount);
            //    }
            //}
            //else
            //{
            //    var iCount = ReturnCount();
            //    CreateFileWriteLog("File_Dashboard", Today, iCount);
            //}

            //var dateStart = DateTime.Now;

            //var userState = this.UserState;
            //Session["UserState"] = userState;
            //var addressAPIs = userState.AddressAPIs;
            //var waUserCode = userState.SysUser.UserCode;
            //var waUserPassword = userState.SysUser.UserPassword;
            //var waMST = userState.SysUser.MST;

            //var datetime = DateTime.Today.ToString("yyyy-MM-dd");
            //var convertdatetimeFrom = "";
            //if (!CUtils.IsNullOrEmpty(datetime))
            //{
            //    convertdatetimeFrom = datetime + " 00:00:00";
            //}

            //var convertdatetimeTo = "";
            //if (!CUtils.IsNullOrEmpty(datetime))
            //{
            //    convertdatetimeTo = datetime + " 23:59:59";
            //}

            //var lstRpt_InvoiceForDashboard = new List<Rpt_InvoiceForDashboard>();

            //var rqRpt_InvoiceForDashboard = new RQ_Rpt_InvoiceForDashboard
            //{
            //    Tid = GetNextTId(),
            //    WAUserCode = waUserCode,
            //    WAUserPassword = waUserPassword,
            //    GwUserCode = GwUserCode,
            //    GwPassword = GwPassword,
            //    Ft_RecordStart = Ft_RecordStart,
            //    Ft_RecordCount = Ft_RecordCount,
            //    ReportDTimeFrom = convertdatetimeFrom,
            //    ReportDTimeTo = convertdatetimeTo,
            //};

            //var dateEndTapHopDuLieu = DateTime.Now;
            //TimeSpan timeSpanEndTapHopDuLieu = dateEndTapHopDuLieu - dateStart;

            //var dateStartWA_Rpt_InvoiceForDashboard = DateTime.Now;
            //var rtRpt_InvoiceForDashboard = Rpt_InvoiceForDashboardService.Instance.WA_Rpt_InvoiceForDashboard(rqRpt_InvoiceForDashboard, addressAPIs);
            //var dateEndWA_Rpt_InvoiceForDashboard = DateTime.Now;

            //TimeSpan timeSpanWA_Rpt_InvoiceForDashboard = dateEndWA_Rpt_InvoiceForDashboard - dateStartWA_Rpt_InvoiceForDashboard;

            //if (rtRpt_InvoiceForDashboard.Lst_Rpt_InvoiceForDashboard != null && rtRpt_InvoiceForDashboard.Lst_Rpt_InvoiceForDashboard.Count > 0)
            //{
            //    lstRpt_InvoiceForDashboard = rtRpt_InvoiceForDashboard.Lst_Rpt_InvoiceForDashboard;
            //}

            //var invoice_license = new List<Invoice_license>();
            //if (rtRpt_InvoiceForDashboard.Lst_Invoice_license != null)
            //{
            //    invoice_license = rtRpt_InvoiceForDashboard.Lst_Invoice_license;
            //}

            //ViewBag.Invoice_license = invoice_license;
            //ViewBag.Datetime = datetime;
            //ViewBag.MST = waMST;

            //var dateEnd = DateTime.Now;
            //TimeSpan timeSpan = dateEnd - dateStart;
            //CUtils.StrAppend(stringBuilder, "Tổng hợp dữ liệu: " + timeSpanEndTapHopDuLieu.TotalMilliseconds);
            //CUtils.StrAppend(stringBuilder, "WA_Rpt_InvoiceForDashboard: " + timeSpanWA_Rpt_InvoiceForDashboard.TotalMilliseconds);
            //CUtils.StrAppend(stringBuilder, "Tổng thời gian: " + timeSpan.TotalMilliseconds);
            //PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
            //strLogs = stringBuilder.ToString();
            //System.IO.File.AppendAllText(PathFileLog, strLogs, Encoding.UTF8);
            //stringBuilder.Clear();
            //strLogs = "";
            //return View(lstRpt_InvoiceForDashboard);
            return View();
        }
    }
}