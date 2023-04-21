using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class ReportController : BaseController
    {
        // GET: Report
        #region Báo cáo tình hình sử dụng hóa đơn
        public ActionResult RptSv_Rpt_InvoiceSummary_01(string fromdate, string todate, string init = "init")
        {            
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            ViewBag.Fromdate = fromdate;
            ViewBag.Todate = todate;
            
            if (init == "init")
            {
                return View();
            }
            var lstRQ_RptSv_Rpt_InvoiceSummary_01 = new List<RptSv_Rpt_InvoiceSummary_01>(); 
            
            var strWhere = strWhereClause_RptSv_Rpt_InvoiceSummary_01(fromdate, todate);

            var rq = new RQ_RptSv_Rpt_InvoiceSummary_01()
            {
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhere,
                /**/
                Rt_Cols_RptSv_Rpt_InvoiceSummary_01 = "*",
            };
            
            var rt = RptSv_Rpt_InvoiceSummary_01Service.Instance.WA_RptSv_Rpt_InvoiceSummary_01_Get(rq, addressAPIs);
            if(rt != null) if(rt.Lst_RptSv_Rpt_InvoiceSummary_01 != null)
                {
                    lstRQ_RptSv_Rpt_InvoiceSummary_01 = rt.Lst_RptSv_Rpt_InvoiceSummary_01;
                }
            return View(lstRQ_RptSv_Rpt_InvoiceSummary_01);
        }

        #endregion

        #region Common
        private string strWhereClause_RptSv_Rpt_InvoiceSummary_01(string fromdate, string todate)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Rpt_InvoiceSummary_01 = TableName.RptSv_Rpt_InvoiceSummary_01 + ".";
            if (!CUtils.IsNullOrEmpty(fromdate))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Rpt_InvoiceSummary_01 + TblRptSv_Rpt_InvoiceSummary_01.Month, CUtils.StrValue(fromdate), ">=");
            }
            if (!CUtils.IsNullOrEmpty(todate))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Rpt_InvoiceSummary_01 + TblRptSv_Rpt_InvoiceSummary_01.Month, CUtils.StrValue(todate), "<=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        
        #endregion
    }
}