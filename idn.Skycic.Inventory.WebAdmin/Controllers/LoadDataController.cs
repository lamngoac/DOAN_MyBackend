using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Models;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class LoadDataController : BaseController
    {
        #region["LoadData"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_District(string provincecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_District = new List<Mst_District>();

                if (!CUtils.IsNullOrEmpty(provincecode))
                {
                    var strWhereClauseMst_District = "";
                    strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District() { ProvinceCode = provincecode, FlagActive = FlagActive });

                    var objRQ_Mst_District = new RQ_Mst_District()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_District,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_District
                        Rt_Cols_Mst_District = "*",
                        Mst_District = null,
                    };
                    var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, addressAPIs);
                    if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                    {
                        listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                    }
                }
                return JsonView("LoadMst_District", listMst_District);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_District", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_GovTaxID(string districtcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_GovTaxID = new List<Mst_GovTaxID>();

                if (!CUtils.IsNullOrEmpty(districtcode))
                {
                    var strWhereClauseMst_GovTaxID = "";
                    strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(new Mst_GovTaxID() { DistrictCode = districtcode, FlagActive = FlagActive });

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = UserState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };
                    var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
                    {
                        listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
                    }
                }
                return JsonView("LoadMst_GovTaxID", listMst_GovTaxID);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_GovTaxID", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadOrgID(string networkid)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            //var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            //var waUserCode = userState.RptSv_Sys_User.UserCode;
            //var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var waUserCode = System.Configuration.ConfigurationManager.AppSettings["WAUserCode_Network"];
            var waUserPassword = System.Configuration.ConfigurationManager.AppSettings["WAUserPassword_Network"];

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var addressAPIs = System.Configuration.ConfigurationManager.AppSettings["BaseNetworkAPIAddress"];
                addressAPIs = addressAPIs.Replace("NETWORKIDIDN", networkid);
                var listMst_NNT = new List<Mst_NNT>();

                if (!CUtils.IsNullOrEmpty(networkid))
                {
                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = UserState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_NNT = "*",
                        Mst_NNT = null,
                    };
                    var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
                    if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
                    {
                        listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
                    }
                }
                return JsonView("LoadMst_NNT", listMst_NNT);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_NNT", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_Ward(string provincecode, string districtcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_Ward = new List<Mst_Ward>();
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.RptSv_Sys_User.UserCode;
                var waUserPassword = userState.RptSv_Sys_User.UserPassword;
                if (!CUtils.IsNullOrEmpty(provincecode))
                {
                    var strWhereClauseMst_Ward = "";
                    strWhereClauseMst_Ward = strWhereClause_Mst_Ward(new Mst_Ward() { ProvinceCode = provincecode, DistrictCode = districtcode, FlagActive = Client_Flag.Active });

                    var objRQ_Mst_Ward = new RQ_Mst_Ward()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        //AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        //NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        //OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Ward,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Ward
                        Rt_Cols_Mst_Ward = "*",
                        Mst_Ward = null,
                    };
                    var objRT_Mst_Ward = Mst_WardService.Instance.WA_Mst_Ward_Get(objRQ_Mst_Ward, CUtils.StrValue(UserState.AddressAPIs));
                    if (objRT_Mst_Ward.Lst_Mst_Ward != null && objRT_Mst_Ward.Lst_Mst_Ward.Count > 0)
                    {
                        listMst_Ward.AddRange(objRT_Mst_Ward.Lst_Mst_Ward);
                    }
                }
                return JsonView("LoadMst_Ward", listMst_Ward);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_Ward", null, resultEntry);
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_District(Mst_District data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_District = TableName.Mst_District + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, data.ProvinceCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_GovTaxID(Mst_GovTaxID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.DistrictCode, data.DistrictCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, data.MST.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Ward(Mst_Ward data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Ward = TableName.Mst_Ward + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.DistrictCode, CUtils.StrValue(data.DistrictCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}