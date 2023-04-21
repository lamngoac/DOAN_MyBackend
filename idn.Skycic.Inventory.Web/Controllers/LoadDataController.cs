using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using Newtonsoft.Json.Linq;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class LoadDataController : idnBaseController
    {
        #region["LoadDataReportServer"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_District(string provincecode)
        {
            var waUserCode = WAUserCode_RptSV;
            var waUserPassword = WAUserPassword_RptSV;
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;

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
                        // RQ_Mst_District
                        Rt_Cols_Mst_District = "*",
                        Mst_District = null,
                    };
                    var objRT_Mst_District = Mst_DistrictService.Instance.WA_RptSv_Mst_District_Get(objRQ_Mst_District, addressReportAPIs);
                    if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                    {
                        listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                    }
                }
                return JsonView("LoadMst_District", listMst_District);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
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
            var waUserCode = WAUserCode_RptSV;
            var waUserPassword = WAUserPassword_RptSV;
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;

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
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };
                    var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressReportAPIs);
                    if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
                    {
                        listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
                    }
                }
                return JsonView("LoadMst_GovTaxID", listMst_GovTaxID);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_GovTaxID", null, resultEntry);
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
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_GovTaxID(Mst_GovTaxID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";
            if (!CUtils.IsNullOrEmpty(data.GovTaxID))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.GovTaxID, data.GovTaxID.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}