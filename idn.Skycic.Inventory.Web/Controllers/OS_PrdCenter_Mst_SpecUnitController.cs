using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class OS_PrdCenter_Mst_SpecUnitController : AdminBaseController
    {
        // GET: OS_PrdCenter_Mst_SpecUnit
        public ActionResult Index(string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<ProductCentrer.Mst_SpecUnit>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_SpecUnit>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var strWhereClauseMst_SpecUnit = "";

                var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Ft_RecordStart = (page * PageSizeConfig).ToString(),
                    //Ft_RecordCount = PageSizeConfig.ToString(),
                    Ft_WhereClause = strWhereClauseMst_SpecUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecUnit
                    Rt_Cols_Mst_SpecUnit = "*",
                    Mst_SpecUnit = null,
                };

                var objRT_Mst_SpecUnitCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);

                if (objRT_Mst_SpecUnitCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_SpecUnitCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_SpecUnitCur != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới Map Spec - Unit"]

        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            #region["Mst_Spec"]

            var objMst_Spec = new ProductCentrer.Mst_Spec()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_Spec);
            var listMst_Spec = List_Mst_Spec(strWhereClauseMst_Spec);
            ViewBag.ListOS_PrdCenter_Mst_Spec = listMst_Spec;
            #endregion

            #region["Mst_Unit - Đơn vị"]

            var objMst_Unit = new ProductCentrer.Mst_Unit()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
            var listMst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
            ViewBag.ListOS_PrdCenter_Mst_Unit = listMst_Unit;
            #endregion

            ViewBag.OrgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_SpecUnitInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_SpecUnit>(model);
                //var title = "";
                var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit
                {
                    FlagIsDelete = null,
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecUnit
                    Rt_Cols_Mst_SpecUnit = null,
                    Mst_SpecUnit = objMst_SpecUnitInput
                };
                OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Create(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới Map Spec - Unit thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        #region["Sửa loại tiền"]
        [HttpGet]
        public ActionResult Update(string speccode, string unitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(speccode) && !CUtils.IsNullOrEmpty(unitcode))
            {
                var objMst_SpecUnit = new ProductCentrer.Mst_SpecUnit()
                {
                    SpecCode = speccode,
                    UnitCode = unitcode,
                };

                var strWhereClauseMst_SpecUnit = strWhereClause_Mst_SpecUnit(objMst_SpecUnit);

                var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_SpecUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecUnit
                    Rt_Cols_Mst_SpecUnit = "*",
                    Mst_SpecUnit = null,
                };

                var objRT_Mst_SpecUnitCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit.Count > 0)
                {
                    #region["Mst_Spec"]

                    var objMst_Spec = new ProductCentrer.Mst_Spec()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_Spec);
                    var listMst_Spec = List_Mst_Spec(strWhereClauseMst_Spec);
                    ViewBag.ListOS_PrdCenter_Mst_Spec = listMst_Spec;
                    #endregion

                    #region["Mst_Unit - Đơn vị"]

                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
                    var listMst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
                    ViewBag.ListOS_PrdCenter_Mst_Unit = listMst_Unit;
                    #endregion
                    return View(objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0]);
                }
            }
            return View(new ProductCentrer.Mst_SpecUnit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_SpecUnitInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_SpecUnit>(model);
                    var objMst_SpecUnit = new ProductCentrer.Mst_SpecUnit()
                    {
                        SpecCode = objMst_SpecUnitInput.SpecCode,
                        UnitCode = objMst_SpecUnitInput.UnitCode,
                    };

                    var strWhereClauseMst_SpecUnit = strWhereClause_Mst_SpecUnit(objMst_SpecUnit);

                    var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_SpecUnit,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_SpecUnit
                        Rt_Cols_Mst_SpecUnit = "*",
                        Mst_SpecUnit = null,
                    };

                    var objRT_Mst_SpecUnitCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit.Count > 0)
                    {
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].StandardUnitCode = objMst_SpecUnitInput.StandardUnitCode;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].SpecUnitDesc = objMst_SpecUnitInput.SpecUnitDesc;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Qty = objMst_SpecUnitInput.Qty;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Length = objMst_SpecUnitInput.Length;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Width = objMst_SpecUnitInput.Width;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Height = objMst_SpecUnitInput.Height;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Volume = objMst_SpecUnitInput.Volume;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Weight = objMst_SpecUnitInput.Weight;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].Remark = objMst_SpecUnitInput.Remark;
                        objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0].FlagActive = objMst_SpecUnitInput.FlagActive;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_SpecUnit = TableName.Mst_SpecUnit + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.StandardUnitCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.SpecUnitDesc);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Qty);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Length);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Width);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Height);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Volume);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Weight);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.Remark);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecUnit, TblMst_SpecUnit.FlagActive);

                        objRQ_Mst_SpecUnit.Ft_WhereClause = null;
                        objRQ_Mst_SpecUnit.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_SpecUnit.Rt_Cols_Mst_SpecUnit = null;
                        objRQ_Mst_SpecUnit.Mst_SpecUnit = objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0];

                        OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Update(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa Map Spec - Unit thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Spec '" + objMst_SpecUnitInput.SpecCode + "' hoặc mã đơn vị '" + objMst_SpecUnitInput.UnitCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Spec hoặc mã đơn vị trống!");
                }

                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa loại tiền"]
        [HttpGet]
        public ActionResult Detail(string speccode, string unitcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(speccode) && !CUtils.IsNullOrEmpty(unitcode))
            {
                var objOS_PrdCenter_Mst_Brand = new ProductCentrer.Mst_SpecUnit()
                {
                    SpecCode = speccode,
                    UnitCode = unitcode,
                };

                var strWhereClauseMst_SpecUnit = strWhereClause_Mst_SpecUnit(objOS_PrdCenter_Mst_Brand);

                var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_SpecUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecUnit
                    Rt_Cols_Mst_SpecUnit = "*",
                    Mst_SpecUnit = null,
                };

                var objRT_Mst_SpecUnitCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit.Count > 0)
                {
                    #region["Mst_Spec"]

                    var objMst_Spec = new ProductCentrer.Mst_Spec()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Spec = strWhereClause_Mst_Spec(objMst_Spec);
                    var listMst_Spec = List_Mst_Spec(strWhereClauseMst_Spec);
                    ViewBag.ListOS_PrdCenter_Mst_Spec = listMst_Spec;
                    #endregion

                    #region["Mst_Unit - Đơn vị"]

                    var objMst_Unit = new ProductCentrer.Mst_Unit()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Unit = strWhereClause_Mst_Unit(objMst_Unit);
                    var listMst_Unit = List_Mst_Unit(strWhereClauseMst_Unit);
                    ViewBag.ListOS_PrdCenter_Mst_Unit = listMst_Unit;
                    #endregion
                    return View(objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0]);
                }
            }
            return View(new ProductCentrer.Mst_SpecUnit());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string speccode, string unitcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(speccode) && !CUtils.IsNullOrEmpty(unitcode))
                {
                    var objOS_PrdCenter_Mst_Brand = new ProductCentrer.Mst_SpecUnit()
                    {
                        SpecCode = speccode,
                        UnitCode = unitcode,
                    };

                    var strWhereClauseMst_SpecUnit = strWhereClause_Mst_SpecUnit(objOS_PrdCenter_Mst_Brand);

                    var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_SpecUnit,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_SpecUnit
                        Rt_Cols_Mst_SpecUnit = "*",
                        Mst_SpecUnit = null,
                    };

                    var objRT_Mst_SpecUnitCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit.Count > 0)
                    {
                        objRQ_Mst_SpecUnit.Mst_SpecUnit = objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit[0];

                        OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Delete(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa Map Spec - Unit thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Spec '" + speccode + "' hoặc mã đơn vị '" + unitcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Spec hoặc mã đơn vị trống!");
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
            //return Json(new { Success = false, Detail = resultEntry.Detail });
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
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            var exitsData = "";
            try
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
                var listMst_SpecUnit = new List<ProductCentrer.Mst_SpecUnit>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 10)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecUnit.SpecCode]))
                            {
                                exitsData = "Mã Spec không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecUnit.UnitCode]))
                            {
                                exitsData = "Mã đơn vị không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
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
                            var specCodeCur = table.Rows[i][TblMst_SpecUnit.SpecCode].ToString().Trim();
                            var unitCodeCur = table.Rows[i][TblMst_SpecUnit.UnitCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _brandCodeCur = table.Rows[j][TblMst_SpecUnit.SpecCode].ToString().Trim();
                                    var _unitCodeCur = table.Rows[j][TblMst_SpecUnit.UnitCode].ToString().Trim();
                                    if (specCodeCur.Equals(_brandCodeCur) && unitCodeCur.Equals(_unitCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã Spec '" + specCodeCur + "' - mã đơn vị '" + unitCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_SpecUnit = DataTableCmUtils.ToListof<ProductCentrer.Mst_SpecUnit>(table);
                    // Gọi hàm save data
                    if (listMst_SpecUnit != null && listMst_SpecUnit.Count > 0)
                    {

                        foreach (var item in listMst_SpecUnit)
                        {
                            item.OrgID = orgID;
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit
                            {
                                FlagIsDelete = null,
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_Mst_SpecUnit
                                Rt_Cols_Mst_SpecUnit = null,
                                Mst_SpecUnit = item,
                            };
                            OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Create(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);

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
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<ProductCentrer.Mst_SpecUnit>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_SpecUnit).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_SpecUnit"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
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
        //public ActionResult Export(string brandcode = "", string brandname = "", string init = "init", int page = 0)
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_SpecUnit = new List<ProductCentrer.Mst_SpecUnit>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;

            try
            {
                #region["Search"]

                var strWhereClauseMst_SpecUnit = "";

                var objRQ_Mst_SpecUnit = new ProductCentrer.RQ_Mst_SpecUnit()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Ft_RecordStart = (page * PageSizeConfig).ToString(),
                    //Ft_RecordCount = PageSizeConfig.ToString(),
                    Ft_WhereClause = strWhereClauseMst_SpecUnit,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecUnit
                    Rt_Cols_Mst_SpecUnit = "*",
                    Mst_SpecUnit = null,
                };

                var objRT_Mst_SpecUnitCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);

                if (objRT_Mst_SpecUnitCur != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null)
                {
                    if (objRT_Mst_SpecUnitCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_SpecUnitCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_SpecUnit.AddRange(objRT_Mst_SpecUnitCur.Lst_Mst_SpecUnit);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_SpecUnit).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_SpecUnit, dicColNames, filePath, string.Format("Mst_SpecUnit"));


                    #region["Export các trang còn lại"]
                    listMst_SpecUnit.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_SpecUnit.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_SpecUnitExportCur = OS_PrdCenter_Mst_SpecUnitService.Instance.WA_Mst_SpecUnit_Get(objRQ_Mst_SpecUnit, addressAPIs_ProductCentrer);
                            if (objRT_Mst_SpecUnitExportCur != null && objRT_Mst_SpecUnitExportCur.Lst_Mst_SpecUnit != null && objRT_Mst_SpecUnitExportCur.Lst_Mst_SpecUnit.Count > 0)
                            {
                                listMst_SpecUnit.AddRange(objRT_Mst_SpecUnitExportCur.Lst_Mst_SpecUnit);
                                ExcelExport.ExportToExcelFromList(listMst_SpecUnit, dicColNames, filePath, string.Format("Mst_SpecUnit_" + i));
                                listMst_SpecUnit.Clear();
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
                {TblMst_SpecUnit.SpecCode,"Mã Spec (*)"},
                {TblMst_SpecUnit.UnitCode,"Mã đơn vị (*)"},
                {TblMst_SpecUnit.StandardUnitCode,"Mã đơn vị chuẩn (*)"},
                {TblMst_SpecUnit.SpecUnitDesc,"Mô tả"},
                {TblMst_SpecUnit.Qty,"Số lượng (*)"},
                {TblMst_SpecUnit.Length,"Dài"},
                {TblMst_SpecUnit.Width,"Rộng"},
                {TblMst_SpecUnit.Height,"Cao"},
                {TblMst_SpecUnit.Volume,"Thể tích"},
                {TblMst_SpecUnit.Weight,"Khối lượng"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_SpecUnit.OrgID,"Mã tổ chức (*)"},
                {TblMst_SpecUnit.SpecCode,"Mã Spec (*)"},
                {TblMst_SpecUnit.UnitCode,"Mã đơn vị (*)"},
                {TblMst_SpecUnit.StandardUnitCode,"Mã đơn vị chuẩn (*)"},
                {TblMst_SpecUnit.SpecUnitDesc,"Mô tả"},
                {TblMst_SpecUnit.Qty,"Số lượng (*)"},
                {TblMst_SpecUnit.Length,"Dài"},
                {TblMst_SpecUnit.Width,"Rộng"},
                {TblMst_SpecUnit.Height,"Cao"},
                {TblMst_SpecUnit.Volume,"Thể tích"},
                {TblMst_SpecUnit.Weight,"Khối lượng"},
                {TblMst_SpecUnit.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_Unit(ProductCentrer.Mst_Unit data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Unit = TableName.Mst_Unit + ".";
            
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Unit + TblMst_Unit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Spec(ProductCentrer.Mst_Spec data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_SpecUnit(ProductCentrer.Mst_SpecUnit data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_SpecUnit = TableName.Mst_SpecUnit + ".";
            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecUnit + TblMst_SpecUnit.SpecCode, CUtils.StrValueOrNull(data.SpecCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.UnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecUnit + TblMst_SpecUnit.UnitCode, CUtils.StrValueOrNull(data.UnitCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecUnit + TblMst_SpecUnit.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}