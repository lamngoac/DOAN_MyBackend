using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.Utils;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class Mst_GovTaxIDController : BaseController
    {
        // Danh mục cơ quan thuế
        // GET: Mst_GovTaxID
        [HttpGet]
        public ActionResult Index(string govtaxid = "", string govtaxname = "", string districtcode = "", string flagactive = "", string init = "init", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var pageInfo = new PageInfo<Mst_GovTaxID>(0, PageSizeConfig)
            {
                DataList = new List<Mst_GovTaxID>()
            };
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                var itemCount = 0;
                var pageCount = 0;

                #region["Loại cơ quan thuế"]
                var objMst_District = new Mst_District()
                {
                    FlagActive = FlagActive,
                };
                var listMst_District = new List<Mst_District>();
                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
                listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
                ViewBag.ListMst_District = listMst_District;
                #endregion

                if (init == "init")
                {
                    #region["Search"]
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        GovTaxID = govtaxid,
                        GovTaxName = govtaxname,
                        DistrictCode = districtcode,
                        FlagActive = flagactive,
                    };

                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxIDMng(objMst_GovTaxID);

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };

                    var objRT_Mst_GovTaxIDCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);

                    if (objRT_Mst_GovTaxIDCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_GovTaxIDCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_GovTaxIDCur != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID.Count > 0)
                    {
                        pageInfo.DataList.AddRange(objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID);
                        pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                    }
                    #endregion
                }
                else
                {
                    init = "search";
                }
                ViewBag.GovTaxID = govtaxid;
                ViewBag.GovTaxName = govtaxname;
                ViewBag.DistrictCode = districtcode;
                ViewBag.FlagActive = flagactive;

                pageInfo.PageSize = PageSizeConfig;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * PageSizeConfig).ToString();

            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            
            return View(pageInfo);
        }

        #region["Tạo mới cơ quan thuế"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.UserState = userState;
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                #region["CQT cấp trên"]
                var objMst_GovTaxID = new Mst_GovTaxID()
                {
                    FlagActive = FlagActive,
                };
                var listMst_GovTaxID = new List<Mst_GovTaxID>();
                var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);
                listMst_GovTaxID.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxID));
                ViewBag.ListMst_GovTaxID = listMst_GovTaxID;
                #endregion
                #region["Tỉnh/Thành Phố"]
                var objMst_Province = new Mst_Province()
                {
                    FlagActive = FlagActive,
                };
                var listMst_Province = new List<Mst_Province>();
                var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                ViewBag.ListMst_Province = listMst_Province;
                #endregion
                #region["Quận/Huyện"]
                var objMst_District = new Mst_District()
                {
                    FlagActive = FlagActive,
                };
                var listMst_District = new List<Mst_District>();
                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
                listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
                ViewBag.ListMst_District = listMst_District;
                #endregion
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
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
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var objMst_GovTaxIDInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_GovTaxID>(model);
                    //var title = "";
                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID
                    {
                        FlagIsDelete = null,
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
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = null,
                        Mst_GovTaxID = objMst_GovTaxIDInput
                    };
                    Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Create(objRQ_Mst_GovTaxID, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Tạo mới cơ quan thuế thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
                
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        #region["Sửa cơ quan thuế"]
        [HttpGet]
        public ActionResult Update(string govtaxid)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                if (!CUtils.IsNullOrEmpty(govtaxid))
                {
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        GovTaxID = govtaxid,
                    };

                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };

                    var objRT_Mst_GovTaxIDCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID.Count > 0)
                    {
                        #region["Tỉnh/Thành Phố"]
                        var objMst_Province = new Mst_Province()
                        {
                            FlagActive = FlagActive,
                        };
                        var listMst_Province = new List<Mst_Province>();
                        var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                        listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                        ViewBag.ListMst_Province = listMst_Province;
                        #endregion

                        #region["Quận/Huyện"]
                        var objMst_District = new Mst_District()
                        {
                            ProvinceCode = objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].ProvinceCode,
                            FlagActive = FlagActive,
                        };
                        var listMst_District = new List<Mst_District>();
                        var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
                        listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
                        ViewBag.ListMst_District = listMst_District;
                        #endregion

                        #region["CQT cấp trên"]
                        var objMst_GovTaxIDparent = new Mst_GovTaxID()
                        {
                            FlagActive = FlagActive,
                        };
                        var listMst_GovTaxIDparrent = new List<Mst_GovTaxID>();
                        var strWhereClauseMst_GovTaxIDparrent = strWhereClause_Mst_GovTaxID(objMst_GovTaxIDparent);
                        listMst_GovTaxIDparrent.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxIDparrent));
                        ViewBag.ListMst_GovTaxIDparrent = listMst_GovTaxIDparrent;
                        #endregion
                        return View(objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0]);
                    }
                }
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }

            return View(new Mst_GovTaxID());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    if (!CUtils.IsNullOrEmpty(model))
                    {
                        var objMst_GovTaxIDInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_GovTaxID>(model);
                        var objMst_GovTaxID = new Mst_GovTaxID()
                        {
                            GovTaxID = objMst_GovTaxIDInput.GovTaxID,
                        };

                        var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);

                        var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = RowsWorksheets.ToString(),
                            Ft_WhereClause = strWhereClauseMst_GovTaxID,
                            Ft_Cols_Upd = null,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            UtcOffset = userState.UtcOffset,
                            // RQ_Mst_GovTaxID
                            Rt_Cols_Mst_GovTaxID = "*",
                            Mst_GovTaxID = null,
                        };

                        var objRT_Mst_GovTaxIDCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                        if (objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID.Count > 0)
                        {
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].GovTaxName = objMst_GovTaxIDInput.GovTaxName;
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].GovTaxIDParent = objMst_GovTaxIDInput.GovTaxIDParent;
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].Level = objMst_GovTaxIDInput.Level;
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].Address = objMst_GovTaxIDInput.Address;
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].ContactEmail = objMst_GovTaxIDInput.ContactEmail;
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].ContactPhone = objMst_GovTaxIDInput.ContactPhone;
                            objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].FlagActive = objMst_GovTaxIDInput.FlagActive;

                            var strFt_Cols_Upd = "";
                            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.GovTaxName);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.GovTaxIDParent);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.Level);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.Address);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.ContactEmail);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.ContactPhone);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_GovTaxID, TblMst_GovTaxID.FlagActive);

                            objRQ_Mst_GovTaxID.Ft_WhereClause = null;
                            objRQ_Mst_GovTaxID.Ft_Cols_Upd = strFt_Cols_Upd;
                            objRQ_Mst_GovTaxID.Rt_Cols_Mst_GovTaxID = null;
                            objRQ_Mst_GovTaxID.Mst_GovTaxID = objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0];

                            Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Update(objRQ_Mst_GovTaxID, addressAPIs);

                            resultEntry.Success = true;
                            resultEntry.AddMessage("Sửa cơ quan thuế thành công!");
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                        else
                        {
                            resultEntry.Success = true;
                            resultEntry.AddMessage("Mã cơ quan thuế '" + objMst_GovTaxIDInput.GovTaxID + "' không có trong hệ thống!");
                        }
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã cơ quan thuế trống!");
                    }
                    return Json(resultEntry);
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa cơ quan thuế"]
        [HttpGet]
        public ActionResult Detail(string govtaxid)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                if (!CUtils.IsNullOrEmpty(govtaxid))
                {
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        GovTaxID = govtaxid,
                    };

                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };

                    var objRT_Mst_GovTaxIDCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID.Count > 0)
                    {
                        #region["Tỉnh/Thành Phố"]
                        var objMst_Province = new Mst_Province()
                        {
                            FlagActive = FlagActive,
                        };
                        var listMst_Province = new List<Mst_Province>();
                        var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                        listMst_Province.AddRange(List_Mst_Province(strWhereClauseMst_Province));
                        ViewBag.ListMst_Province = listMst_Province;
                        #endregion

                        #region["Quận/Huyện"]
                        var objMst_District = new Mst_District()
                        {
                            ProvinceCode = objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0].ProvinceCode,
                            FlagActive = FlagActive,
                        };
                        var listMst_District = new List<Mst_District>();
                        var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);
                        listMst_District.AddRange(List_Mst_District(strWhereClauseMst_District));
                        ViewBag.ListMst_District = listMst_District;
                        #endregion

                        #region["CQT cấp trên"]
                        var objMst_GovTaxIDparent = new Mst_GovTaxID()
                        {
                            FlagActive = FlagActive,
                        };
                        var listMst_GovTaxIDparrent = new List<Mst_GovTaxID>();
                        var strWhereClauseMst_GovTaxIDparrent = strWhereClause_Mst_GovTaxID(objMst_GovTaxIDparent);
                        listMst_GovTaxIDparrent.AddRange(List_Mst_GovTaxID(strWhereClauseMst_GovTaxIDparrent));
                        ViewBag.ListMst_GovTaxIDparrent = listMst_GovTaxIDparrent;
                        #endregion
                        return View(objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0]);
                    }
                }
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }

            return View(new Mst_GovTaxID());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string govtaxid)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                if (!CUtils.IsNullOrEmpty(govtaxid))
                {
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        GovTaxID = govtaxid,
                    };

                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(objMst_GovTaxID);

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };

                    var objRT_Mst_GovTaxIDCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID.Count > 0)
                    {
                        objRQ_Mst_GovTaxID.Mst_GovTaxID = objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID[0];

                        Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Delete(objRQ_Mst_GovTaxID, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa cơ quan thuế thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã cơ quan thuế '" + govtaxid + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã cơ quan thuế trống!");
                }

                return Json(resultEntry);
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
        }
        #endregion

        #region["Import excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var exitsData = "";
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    else
                    {
                        throw new Exception("File excel import không hợp lệ!");
                    }
                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var listMst_GovTaxID = new List<Mst_GovTaxID>();
                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != 9)
                        {
                            exitsData = "File excel import không hợp lệ!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region["Check null"]
                            var idx = 0;
                            var iRows = 2;
                            var strRows = " - Dòng ";
                            foreach (DataRow dr in table.Rows)
                            {
                                iRows = 2;
                                iRows = (iRows + idx + 1);
                                strRows = " - Dòng ";
                                strRows += iRows;

                                if (CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.GovTaxID]))
                                {
                                    exitsData = "Mã cơ quan thuế không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.GovTaxName]))
                                {
                                    exitsData = "Tên cơ quan thuế không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.GovTaxIDParent]))
                                {
                                    exitsData = "Mã cơ quan thuế cấp trên không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.Level]))
                                {
                                    exitsData = "Cấp không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    //var level = dr[TblMst_GovTaxID.Level].ToString().Trim();
                                    //if (!level.Equals("0") && !level.Equals("1") && !level.Equals("2"))
                                    //{
                                    //    exitsData = "Cấp nhập '0' hoặc '1' hoặc '2'!" + strRows;
                                    //    resultEntry.AddMessage(exitsData);
                                    //    return Json(resultEntry);
                                    //}
                                }

                                if (CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.DistrictCode]))
                                {
                                    exitsData = "Mã quận/huyện không được trống!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }

                                if (!CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.ContactEmail]))
                                {
                                    var contactEmail = dr[TblMst_GovTaxID.ContactEmail].ToString().Trim();
                                    if (!CUtils.IsValidEmail(contactEmail))
                                    {
                                        exitsData = "Email không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }

                                if (!CUtils.IsNullOrEmpty(dr[TblMst_GovTaxID.ContactPhone]))
                                {
                                    var contactPhone = dr[TblMst_GovTaxID.ContactPhone].ToString().Trim();
                                    if (contactPhone.Length <= 10)
                                    {
                                        if (CUtils.IsNumber(contactPhone))
                                        {
                                            var index = contactPhone.IndexOf(".");
                                            if (index >= 0)
                                            {
                                                exitsData = "Điện thoại không hợp lệ!";
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        else
                                        {
                                            exitsData = "Điện thoại không hợp lệ!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Điện thoại không hợp lệ (10 số)!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                idx++;
                            }
                            #endregion

                            #region["Check duplicate"]
                            iRows = 2;
                            strRows = " - Dòng ";
                            var jRows = 2;
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                iRows = 2;
                                iRows = (iRows + i + 1);
                                strRows = " - Dòng ";
                                strRows += iRows;
                                var govTaxIDCur = table.Rows[i][TblMst_GovTaxID.GovTaxID].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    jRows = 2;
                                    jRows = (jRows + j + 1);
                                    if (i != j)
                                    {
                                        var _govTaxIDCur = table.Rows[j][TblMst_GovTaxID.GovTaxID].ToString().Trim();
                                        if (govTaxIDCur.Equals(_govTaxIDCur))
                                        {
                                            strRows += (" - " + jRows);
                                            exitsData = "Mã cơ quan thuế '" + govTaxIDCur + "' bị lặp trong file excel!" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        listMst_GovTaxID = DataTableCmUtils.ToListof<Mst_GovTaxID>(table); ;
                        // Gọi hàm save data
                        if (listMst_GovTaxID != null && listMst_GovTaxID.Count > 0)
                        {
                            foreach (var item in listMst_GovTaxID)
                            {
                                //item.FlagActive = "1";
                                var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
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
                                    // RQ_Mst_GovTaxID
                                    Rt_Cols_Mst_GovTaxID = null,
                                    Mst_GovTaxID = item,
                                };

                                Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Create(objRQ_Mst_GovTaxID, addressAPIs);
                            }
                        }

                        resultEntry.Success = true;
                        exitsData = "Đã nhập dữ liệu excel thành công!";
                        resultEntry.AddMessage(exitsData);
                    }
                    else
                    {
                        exitsData = "File excel import không có dữ liệu!";
                        resultEntry.AddMessage(exitsData);
                    }
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
                
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region["Export excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_GovTaxID>();
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_GovTaxID).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_GovTaxID"));

                    return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
                
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string govtaxid = "", string govtaxname = "", string districtcode = "", string flagactive = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_GovTaxID = new List<Mst_GovTaxID>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var objMst_GovTaxID = new Mst_GovTaxID()
                    {
                        GovTaxID = govtaxid,
                        GovTaxName = govtaxname,
                        DistrictCode = districtcode,
                        FlagActive = flagactive,
                    };

                    #region["Search"]
                    var strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxIDMng(objMst_GovTaxID);

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };

                    var objRT_Mst_GovTaxIDCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxIDCur != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null)
                    {
                        if (objRT_Mst_GovTaxIDCur.MySummaryTable != null)
                        {
                            itemCount = Convert.ToInt32(objRT_Mst_GovTaxIDCur.MySummaryTable.MyCount);
                        }
                        if (objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID.Count > 0)
                        {
                            pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                        }

                        listMst_GovTaxID.AddRange(objRT_Mst_GovTaxIDCur.Lst_Mst_GovTaxID);

                        Dictionary<string, string> dicColNames = GetImportDicColums();
                        string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_GovTaxID).Name), ref url);
                        ExcelExport.ExportToExcelFromList(listMst_GovTaxID, dicColNames, filePath, string.Format("Mst_GovTaxID"));


                        #region["Export các trang còn lại"]
                        listMst_GovTaxID.Clear();
                        if (pageCount > 1)
                        {
                            for (var i = 1; i <= pageCount; i++)
                            {
                                objRQ_Mst_GovTaxID.Ft_RecordStart = (i * RowsWorksheets).ToString();

                                var objRT_Mst_GovTaxIDExportCur = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                                if (objRT_Mst_GovTaxIDExportCur != null && objRT_Mst_GovTaxIDExportCur.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxIDExportCur.Lst_Mst_GovTaxID.Count > 0)
                                {
                                    listMst_GovTaxID.AddRange(objRT_Mst_GovTaxIDExportCur.Lst_Mst_GovTaxID);
                                    ExcelExport.ExportToExcelFromList(listMst_GovTaxID, dicColNames, filePath, string.Format("Mst_GovTaxID_" + i));
                                    listMst_GovTaxID.Clear();
                                }
                            }
                        }
                        #endregion

                        return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                    }
                    #endregion

                    else
                    {
                        return Json(new { Success = true, Title = "Dữ liệu trống!", CheckSuccess = "1" });
                    }
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
                
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_GovTaxID.GovTaxID,"Mã cơ quan thuế (*)"},
                {TblMst_GovTaxID.GovTaxName,"Tên cơ quan thuế (*)"},
                {TblMst_GovTaxID.GovTaxIDParent,"Mã cơ quan thuế cấp trên (*)"},
                {TblMst_GovTaxID.Level,"Cấp (*)"},
                {TblMst_GovTaxID.ProvinceCode,"Tỉnh/Thành phố (*)"},
                {TblMst_GovTaxID.DistrictCode,"Quận/huyện (*)"},
                {TblMst_GovTaxID.Address,"Địa chỉ"},
                {TblMst_GovTaxID.ContactEmail,"Email"},
                {TblMst_GovTaxID.ContactPhone,"Điện thoại"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_GovTaxID.GovTaxID,"Mã cơ quan thuế (*)"},
                {TblMst_GovTaxID.GovTaxName,"Tên cơ quan thuế (*)"},
                {TblMst_GovTaxID.GovTaxIDParent,"Mã cơ quan thuế cấp trên (*)"},
                {TblMst_GovTaxID.Level,"Cấp (*)"},
                {TblMst_GovTaxID.Address,"Địa chỉ"},
                {TblMst_GovTaxID.ProvinceCode,"Mã tỉnh/thành phố (*)"},
                {TblMst_GovTaxID.DistrictCode,"Mã quận/huyện (*)"},
                {TblMst_GovTaxID.ContactEmail,"Email"},
                {TblMst_GovTaxID.ContactPhone,"Điện thoại"},
                {TblMst_GovTaxID.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_GovTaxIDMng(Mst_GovTaxID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";

            if (!CUtils.IsNullOrEmpty(data.GovTaxID))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.GovTaxID, "%" + CUtils.StrValue(data.GovTaxID) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.GovTaxName))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.GovTaxName, "%" + CUtils.StrValue(data.GovTaxName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.DistrictCode, CUtils.StrValue(data.DistrictCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.FlagActive, CUtils.StrValue(data.FlagActive), "=");
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
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.GovTaxID, CUtils.StrValue(data.GovTaxID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_District(Mst_District data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_District = TableName.Mst_District + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}