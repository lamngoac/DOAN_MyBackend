using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class Mst_PaymentMethodsController : BaseController
    {
        // GET: Mst_PaymentMethods
        public ActionResult Index()
        {
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            List<Mst_PaymentMethods> lst = new List<Mst_PaymentMethods>();
            RQ_Mst_PaymentMethods rq = new RQ_Mst_PaymentMethods()
            {
                WAUserCode = userState.RptSv_Sys_User.UserCode,
                WAUserPassword = userState.RptSv_Sys_User.UserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Tid = GetNextTId(),
                Rt_Cols_Mst_PaymentMethods = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = ""
            };
            var rt = Mst_PaymentMethodsService.Instance.WA_Mst_PaymentMethods_Get(rq, addressAPIs);
            if (rt != null) if (rt.Lst_Mst_PaymentMethods != null) lst = rt.Lst_Mst_PaymentMethods;
            return View(lst);
        }
    }
}