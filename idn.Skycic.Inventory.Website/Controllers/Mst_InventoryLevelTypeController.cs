using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Utils;
using System.Text;
using idn.Skycic.Inventory.Constants;
using System.Data;
using idn.Skycic.Inventory.Website.Utils;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_InventoryLevelTypeController : AdminBaseController
    {
        // GET: Mst_InventoryLevelType
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_CAP_KHO");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = this.UserState;
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_InventoryLevelType>(0, recordcount)
            {
                DataList = new List<Mst_InventoryLevelType>()
            };

            var strWhere = string.Format("Mst_InventoryLevelType.NetworkID = '{0}'", networkID);

            try
            {
                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhere,
                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = "*"
                };
                var objRT_Mst_InventoryLevelType = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, addressAPIs);
                if (objRT_Mst_InventoryLevelType.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_InventoryLevelType.MySummaryTable.MyCount);
                }
                if (objRT_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelType.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelType.Lst_Mst_InventoryLevelType.Count > 0)
                {
                    pageInfo.DataList = objRT_Mst_InventoryLevelType.Lst_Mst_InventoryLevelType;
                    itemCount = objRT_Mst_InventoryLevelType.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_InventoryLevelType.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }
                ViewBag.PageCur = page.ToString();
                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                if (!init.Equals("init"))
                {
                    return JsonView("BindDataMst_InventoryLevelType", pageInfo);
                }
                return View(pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region ["Tạo mới"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgId = UserState.Mst_NNT.OrgID;

            return JsonView("Create", null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var orgId = UserState.Mst_NNT.OrgID;

            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                var objMst_InventoryLevelTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_InventoryLevelType>(model);
                objMst_InventoryLevelTypeInput.OrgID = orgId;

                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = null,
                    Mst_InventoryLevelType = objMst_InventoryLevelTypeInput
                };
                Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Create(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới cấp kho thành công!");
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

        #region ["Sửa"]
        [HttpGet]
        public ActionResult Update(string invleveltype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var orgId = userState.OrgID;

            if (!CUtils.IsNullOrEmpty(invleveltype))
            {
                var objMst_InventoryLevelType = new Mst_InventoryLevelType()
                {
                    InvLevelType = invleveltype,
                };

                var strWhereClauseMst_InventoryLevelType = strWhereClause_Mst_InventoryLevelType(objMst_InventoryLevelType);

                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_InventoryLevelType,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = "*",
                    Mst_InventoryLevelType = null,
                };

                var objRT_Mst_InventoryLevelTypeCur = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);
                if (objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType.Count > 0)
                {
                    return JsonView("Update", objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType[0]);
                }
            }
            return JsonView("Update", new Mst_InventoryLevelType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgId = UserState.Mst_NNT.OrgID;

            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_InventoryLevelTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_InventoryLevelType>(model);
                var objMst_InventoryLevelType = new Mst_InventoryLevelType()
                {
                    InvLevelType = objMst_InventoryLevelTypeInput.InvLevelType,
                };

                var strWhereClauseMst_InventoryLevelType = strWhereClause_Mst_InventoryLevelType(objMst_InventoryLevelType);

                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_InventoryLevelType,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = "*",
                    Mst_InventoryLevelType = null,
                };

                var objRT_Mst_InventoryLevelTypeCur = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);
                if (objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType.Count > 0)
                {
                    objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType[0].InvLevelTypeName = objMst_InventoryLevelTypeInput.InvLevelTypeName;
                    objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType[0].FlagActive = objMst_InventoryLevelTypeInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_InventoryLevelType = TableName.Mst_InventoryLevelType + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InventoryLevelType, TblMst_InventoryLevelType.InvLevelTypeName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InventoryLevelType, TblMst_InventoryLevelType.FlagActive);

                    objRQ_Mst_InventoryLevelType.Ft_WhereClause = null;
                    objRQ_Mst_InventoryLevelType.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_InventoryLevelType.Rt_Cols_Mst_InventoryLevelType = null;
                    objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType = objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType[0];

                    Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Update(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa danh mục cấp kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã cấp kho '" + objMst_InventoryLevelTypeInput.InvLevelType + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã cấp kho trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region ["Xóa - Chi tiết"]
        [HttpGet]
        public ActionResult Detail(string invleveltype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var orgId = UserState.Mst_NNT.OrgID;

            if (!CUtils.IsNullOrEmpty(invleveltype))
            {
                var objMst_InventoryLevelType = new Mst_InventoryLevelType()
                {
                    InvLevelType = invleveltype,
                };

                var strWhereClauseMst_InventoryLevelType = strWhereClause_Mst_InventoryLevelType(objMst_InventoryLevelType);

                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_InventoryLevelType,
                    Ft_Cols_Upd = null,

                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = "*",
                    Mst_InventoryLevelType = null,
                };

                var objRT_Mst_InventoryLevelTypeCur = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);
                if (objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType.Count > 0)
                {
                    return JsonView("Detail", objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType[0]);
                }
            }

            return JsonView("Detail", new Mst_InventoryLevelType());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string invleveltype)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var orgId = UserState.Mst_NNT.OrgID;

            if (!CUtils.IsNullOrEmpty(invleveltype))
            {
                var objMst_InventoryLevelType = new Mst_InventoryLevelType()
                {
                    InvLevelType = invleveltype,
                };

                var strWhereClauseMst_InventoryLevelType = strWhereClause_Mst_InventoryLevelType(objMst_InventoryLevelType);

                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_InventoryLevelType,
                    Ft_Cols_Upd = null,

                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = "*",
                    Mst_InventoryLevelType = null,
                };

                var objRT_Mst_InventoryLevelTypeCur = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);
                if (objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType.Count > 0)
                {
                    objRQ_Mst_InventoryLevelType.Mst_InventoryLevelType = objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType[0];

                    Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Delete(objRQ_Mst_InventoryLevelType, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa danh mục cấp kho thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã cấp kho '" + invleveltype + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã cấp kho!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region ["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var orgId = UserState.Mst_NNT.OrgID;

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
                var listMst_InventoryLevelType = new List<Mst_InventoryLevelType>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != GetImportDicColums().Count)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_InventoryLevelType.InvLevelType]))
                            {
                                exitsData = "Mã cấp kho không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_InventoryLevelType.InvLevelTypeName]))
                            {
                                exitsData = "Tên cấp kho không được trống!" + strRows;
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
                            var Mst_InventoryLevelTypeCur = table.Rows[i][TblMst_InventoryLevelType.InvLevelType].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_InventoryLevelTypeCur = table.Rows[j][TblMst_InventoryLevelType.InvLevelType].ToString().Trim();
                                    if (Mst_InventoryLevelTypeCur.Equals(_Mst_InventoryLevelTypeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã cấp kho '" + Mst_InventoryLevelTypeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_InventoryLevelType = DataTableCmUtils.ToListof<Mst_InventoryLevelType>(table);
                    // Gọi hàm save data
                    if (listMst_InventoryLevelType != null && listMst_InventoryLevelType.Count > 0)
                    {
                        foreach (var item in listMst_InventoryLevelType)
                        {
                            item.FlagActive = "1";
                            item.OrgID = orgId;
                            var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType
                            {
                                Tid = GetNextTId(),
                                GwUserCode = GwUserCode,
                                GwPassword = GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,

                                // RQ_Mst_Province
                                Rt_Cols_Mst_InventoryLevelType = null,
                                Mst_InventoryLevelType = item,
                            };
                            Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Create(objRQ_Mst_InventoryLevelType, userState.AddressAPIs);
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

        #region ["Export excel"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(/*int recordcount = 10, int page = 0*/)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var orgId = UserState.Mst_NNT.OrgID;

            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_InventoryLevelType = new List<Mst_InventoryLevelType>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var strWhere = string.Format("Mst_InventoryLevelType.NetworkID = '{0}'", UserState.Mst_NNT.NetworkID);
                var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart/*(Convert.ToInt32(page) * recordcount).ToString()*/,
                    Ft_RecordCount = Ft_RecordCount/*recordcount.ToString()*/,
                    Ft_WhereClause = strWhere,
                    Ft_Cols_Upd = null,


                    // RQ_Mst_InventoryLevelType
                    Rt_Cols_Mst_InventoryLevelType = "*",
                    Mst_InventoryLevelType = null,
                };

                var objRT_Mst_InventoryLevelTypeCur = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, UserState.AddressAPIs);
                if (objRT_Mst_InventoryLevelTypeCur != null && objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType != null)
                {
                    if (objRT_Mst_InventoryLevelTypeCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_InventoryLevelTypeCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_InventoryLevelType.AddRange(objRT_Mst_InventoryLevelTypeCur.Lst_Mst_InventoryLevelType);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_InventoryLevelType).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_InventoryLevelType, dicColNames, filePath, string.Format("Mst_InventoryLevelType"));


                    #region["Export các trang còn lại"]
                    listMst_InventoryLevelType.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_InventoryLevelType.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_InventoryLevelTypeExportCur = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, UserState.AddressAPIs);
                            if (objRT_Mst_InventoryLevelTypeExportCur != null && objRT_Mst_InventoryLevelTypeExportCur.Lst_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelTypeExportCur.Lst_Mst_InventoryLevelType.Count > 0)
                            {
                                listMst_InventoryLevelType.AddRange(objRT_Mst_InventoryLevelTypeExportCur.Lst_Mst_InventoryLevelType);
                                ExcelExport.ExportToExcelFromList(listMst_InventoryLevelType, dicColNames, filePath, string.Format("Mst_InventoryLevelType" + i));
                                listMst_InventoryLevelType.Clear();
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

        #region ["Export excel template"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgId = UserState.Mst_NNT.OrgID;

            var resultEntry = new JsonResultEntry() { Success = false };

            var list = new List<Mst_InventoryLevelType>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_InventoryLevelType).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_InventoryLevelType"));

                return Json(new { Success = true, Title = "Xuất file excel template thành công!", CheckSuccess = "1", strUrl = url });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        #endregion

        #region ["strWhereClause"]
        private string strWhereClause_Mst_InventoryLevelType(Mst_InventoryLevelType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_InventoryLevelType = TableName.Mst_InventoryLevelType + ".";
            if (!CUtils.IsNullOrEmpty(data.InvLevelType))
            {
                sbSql.AddWhereClause(Tbl_Mst_InventoryLevelType + TblMst_InventoryLevelType.InvLevelType, CUtils.StrValueOrNull(data.InvLevelType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvLevelTypeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_InventoryLevelType + TblMst_InventoryLevelType.InvLevelTypeName, "%" + CUtils.StrValueOrNull(data.InvLevelTypeName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_InventoryLevelType + TblMst_InventoryLevelType.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        #region ["strColumns"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"InvLevelType","Mã cấp kho"},
                 {"InvLevelTypeName","Tên cấp kho"},
                 {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"InvLevelType","Mã cấp kho(*)"},
                 {"InvLevelTypeName","Tên cấp kho(*)"},
                 {"FlagActive","Trạng thái"}
            };
        }
        #endregion
    }
}