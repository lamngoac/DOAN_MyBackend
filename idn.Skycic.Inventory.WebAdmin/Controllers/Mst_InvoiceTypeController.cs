using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class Mst_InvoiceTypeController : BaseController
    {
        // GET: Mst_InvoiceType
        public ActionResult Index()
        {
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            List<Mst_InvoiceType> lst = new List<Mst_InvoiceType>();

            RQ_Mst_InvoiceType rq = new RQ_Mst_InvoiceType()
            {
                WAUserCode = userState.RptSv_Sys_User.UserCode,
                WAUserPassword = userState.RptSv_Sys_User.UserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Tid = GetNextTId(),
                Rt_Cols_Mst_InvoiceType = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = "Mst_InvoiceType.FlagActive = 1"
            };
            var rt = Mst_InvoiceTypeService.Instance.WA_Mst_InvoiceType_Get(rq, addressAPIs);
            if (rt != null) if (rt.Lst_Mst_InvoiceType != null) lst = rt.Lst_Mst_InvoiceType;
            return View(lst);
        }
    }
}