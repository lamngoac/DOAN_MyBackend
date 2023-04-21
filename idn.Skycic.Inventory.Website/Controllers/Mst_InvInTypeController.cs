using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.State;
using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_InvInTypeController : AdminBaseController
    {
        // GET: Mst_InvInType
        public ActionResult Index(int page = 0, int recordcount = 10, string init = "init")
        {


            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_LOAI_NHAP_KHO");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID.ToString();
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            ViewBag.UserState = this.UserState;
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            var itemCount = 0;
            var pageCount = 0;
            ViewBag.PageCur = page;
            ViewBag.Recordcount = recordcount;
            var pageInfo = new PageInfo<Mst_InvInType>(0, recordcount)
            {
                DataList = new List<Mst_InvInType>()
            };
            try
            {
                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_InvInType = "*",
                };
                var objRT_Mst_InvInType = Mst_InvInTypeService.Instance.WA_Mst_InvInType_Get(objRQ_Mst_InvInType, addressAPIs);
                if (objRT_Mst_InvInType != null && objRT_Mst_InvInType.Lst_Mst_InvInType.Count > 0)
                {
                    pageInfo.DataList = objRT_Mst_InvInType.Lst_Mst_InvInType;
                    itemCount = objRT_Mst_InvInType.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_InvInType.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                if (!init.Equals("init"))
                {
                    return JsonView("BindDataMst_InvInType", pageInfo);
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

        [HttpGet]
        public ActionResult Create()
        {
            return JsonView("Create");
        }

        [HttpPost]
        public ActionResult Create(string model)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_InvInType = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_InvInType>(model);
                objMst_InvInType.OrgID = orgID;
                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                    // RQ_Mst_Customer
                    Mst_InvInType = objMst_InvInType
                };
                Mst_InvInTypeService.Instance.WA_Mst_InvInType_Create(objRQ_Mst_InvInType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới loại nhập kho thành công!");
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

        [HttpGet]
        public ActionResult Update(string invInType)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            var List_Mst_InvInType = new List<Mst_InvInType>();
            try
            {
                string strWhere_Mst_InvInType = string.Format("Mst_InvInType.InvInType = '{0}'", invInType);

                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                    Ft_WhereClause = strWhere_Mst_InvInType,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_InvInType = "*"
                };
                var objRT_Mst_InvInType = Mst_InvInTypeService.Instance.WA_Mst_InvInType_Get(objRQ_Mst_InvInType, addressAPIs);
                if (objRT_Mst_InvInType != null && objRT_Mst_InvInType.Lst_Mst_InvInType.Count > 0)
                {
                    return JsonView("Update", objRT_Mst_InvInType.Lst_Mst_InvInType[0]);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }

        [HttpPost]
        public ActionResult Edit(string model)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_InvInType = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_InvInType>(model);
                objMst_InvInType.OrgID = orgID;
                var strFt_Cols_Upd = "";
                var Tbl_Mst_InvInType = TableName.Mst_InvInType + ".";
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InvInType, TblMst_InvInType.InvInTypeName);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InvInType, TblMst_InvInType.FlagActive);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_InvInType, TblMst_InvInType.FlagStatistic);
                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                    Ft_Cols_Upd = strFt_Cols_Upd,
                    // RQ_Mst_Customer
                    Mst_InvInType = objMst_InvInType
                };
                Mst_InvInTypeService.Instance.WA_Mst_InvInType_Update(objRQ_Mst_InvInType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật loại nhập kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry);
        }

        [HttpGet]
        public ActionResult Detail(string invInType)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            if (!CUtils.IsNullOrEmpty(invInType))
            {
                string strWhere_Mst_InvInType = string.Format("Mst_InvInType.InvInType = '{0}'", invInType);

                var List_Mst_InvInType = new List<Mst_InvInType>();
                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    NetworkID = networkID,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhere_Mst_InvInType,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_InvInType = "*"
                };
                var objRT_Mst_InvInType = Mst_InvInTypeService.Instance.WA_Mst_InvInType_Get(objRQ_Mst_InvInType, addressAPIs);
                if (objRT_Mst_InvInType != null && objRT_Mst_InvInType.Lst_Mst_InvInType.Count > 0)
                {
                    return JsonView("Detail", objRT_Mst_InvInType.Lst_Mst_InvInType[0]);
                }
            }
            return View(new Mst_Inventory());
        }

        [HttpPost]
        public ActionResult Delete(string invInType)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                    // RQ_Mst_Customer
                };
                objRQ_Mst_InvInType.Mst_InvInType = new Mst_InvInType()
                {
                    OrgID = orgID,
                    InvInType = invInType

                };
                Mst_InvInTypeService.Instance.WA_Mst_InvInType_Delete(objRQ_Mst_InvInType, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá loại nhập kho thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            if (ModelState.IsValid)
            {
                try
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage(Nonsense.MESS_CHECK_FILEIMPORT);
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    else
                    {
                        throw new Exception(Nonsense.MESS_CHECK_FILEIMPORT);
                    }

                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var list = new List<Mst_InvInType>();

                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        list = DataTableCmUtils.ToListof<Mst_InvInType>(table);
                        if (table.Columns.Count != 2)
                        {
                            var exitsData = Nonsense.MESS_CHECK_FILEIMPORT;
                            resultEntry.Detail = exitsData;
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region["check data"]
                            string strWhere_Mst_InvInType = "Mst_InvInType.FlagActive = '1'";
                            var listCur = new List<Mst_InvInType>();
                            var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                                Ft_WhereClause = strWhere_Mst_InvInType,
                                Ft_Cols_Upd = null,
                                // RQ_Mst_Customer
                                Rt_Cols_Mst_InvInType = "*"
                            };
                            var objRT_Mst_InvInType = Mst_InvInTypeService.Instance.WA_Mst_InvInType_Get(objRQ_Mst_InvInType, addressAPIs);
                            if (objRT_Mst_InvInType != null && objRT_Mst_InvInType.Lst_Mst_InvInType.Count > 0)
                            {
                                listCur.AddRange(objRT_Mst_InvInType.Lst_Mst_InvInType);
                            }

                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["InvInType"] == null || dr["InvInType"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã loại nhập kho không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                                if (dr["InvInTypeName"] == null || dr["InvInTypeName"].ToString().Trim().Length == 0)
                                {
                                    //var exitsData = "Tên loại nhập kho " + "'" + dr["SupCode"].ToString().ToUpper() + "'" + " không được trống!";
                                    //resultEntry.Detail = exitsData;
                                    //return Json(resultEntry);


                                    var exitsData = "Tên loại nhập kho không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                            }
                            if (list != null && list.Count > 0)
                            {
                                var objRQ_Mst_InvInType_Input = new RQ_Mst_InvInType()
                                {
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = waUserCode,
                                    WAUserPassword = waUserPassword,
                                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                    NetworkID = networkID,
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = null,
                                    Ft_Cols_Upd = null,
                                    // RQ_Mst_Customer
                                };
                                foreach (var item in list)
                                {
                                    if (listCur != null && listCur.Count > 0 &&
                                        listCur.Any(m => m.InvInType.ToString().ToUpper() == item.InvInType.ToString().ToUpper()))
                                    {
                                        var exitsData = "Mã loại nhập kho: " + "'" + item.InvInType.ToString().ToUpper() + "'" + " đã tồn tại trong hệ thống. Vui lòng kiểm tra lại!";
                                        resultEntry.Detail = exitsData;
                                        return Json(resultEntry);
                                    }
                                    if (
                                        list.Where(m => m != item)
                                            .Any(m => m.InvInType.ToString().ToUpper() == item.InvInType.ToString().ToUpper()))
                                    {
                                        var exitsData = "Trùng mã loại nhập kho: " + "'" + item.InvInType.ToString().ToUpper() + "'" + ". Vui lòng kiểm tra lại!";
                                        resultEntry.Detail = exitsData;
                                        return Json(resultEntry);
                                    }
                                    item.InvInType = item.InvInType.ToString().ToUpper();
                                    item.FlagActive = "1";
                                    item.OrgID = orgID;
                                    objRQ_Mst_InvInType_Input.Mst_InvInType = item;
                                    Mst_InvInTypeService.Instance.WA_Mst_InvInType_Create(objRQ_Mst_InvInType_Input, addressAPIs);
                                }
                            }

                            #endregion
                        }
                        resultEntry.Success = true;
                        resultEntry.AddMessage(Nonsense.MESS_IMPORT_EXCEL_SUCCESS);
                        return Json(resultEntry);
                    }
                    else
                    {
                        var exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.Detail = exitsData;
                        return Json(resultEntry);
                    }
                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
            }
            return Json(resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(int page = 0, int recordcount = 10)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list_Mst_InvInType = new List<Mst_InvInType>();
                var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_InvInType = "*"
                };
                var objRT_Mst_InvInType = Mst_InvInTypeService.Instance.WA_Mst_InvInType_Get(objRQ_Mst_InvInType, addressAPIs);
                if (objRT_Mst_InvInType != null && objRT_Mst_InvInType.Lst_Mst_InvInType.Count > 0)
                {
                    list_Mst_InvInType.AddRange(objRT_Mst_InvInType.Lst_Mst_InvInType);
                }

                if (list_Mst_InvInType != null && list_Mst_InvInType.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_InvInType).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list_Mst_InvInType, dicColNames, filePath, string.Format("Mst_InvInType"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listInv = new List<Mst_InvInType>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_InvInType).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv, dicColNames, filePath, string.Format("Mst_InvInType"));

                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages });
        }
        #endregion

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {"InvInType","Mã loại nhập kho"},
                {"InvInTypeName","Tên loại nhập kho"},
                {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {"InvInType","Mã loại nhập kho(*)"},
                {"InvInTypeName","Tên loại nhập kho(*)"},
            };
        }
        #endregion
    }
}