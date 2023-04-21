using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_InventoryController : AdminBaseController
    {
        // GET: Mst_Inventory
        public ActionResult Index(int page = 0, int recordcount = 10, string init = "init")
        {


            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QUAN_LY_KHO");
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
            var pageInfo = new PageInfo<Mst_Inventory>(0, recordcount)
            {
                DataList = new List<Mst_Inventory>()
            };

            try
            {
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    Ft_WhereClause = "",
                    Ft_Cols_Upd = null,
                    // RQ_Mst_ProductGroup
                    Rt_Cols_Mst_Inventory = "*",
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Inventory);
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    pageInfo.DataList = objRT_Mst_Inventory.Lst_Mst_Inventory;
                    itemCount = objRT_Mst_Inventory.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Inventory.MySummaryTable.MyCount);
                    pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);
                }

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                if (!init.Equals("init"))
                {
                    return JsonView("BindDataMst_Inventory", pageInfo);
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
        public async Task<ActionResult> Create()
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry { Success = false };

            #region ["Mst_Inventory + Mst_InventoryType + Mst_InventoryLevelType"]
            var strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";
            var strWhere_Mst_InventoryType = "Mst_InventoryType.FlagActive = '1'";
            var strWhere_Mst_InventoryLevelType = "Mst_InventoryLevelType.FlagActive = '1'";
            var Mst_Inventory_Get_Task = Mst_Inventory_Get_Async(strWhere_Mst_Inventory);
            var Mst_InventoryType_Get_Task = Mst_InventoryType_Get_Async(strWhere_Mst_InventoryType);
            var Mst_InventoryLevelType_Get_Task = Mst_InventoryLevelType_Get_Async(strWhere_Mst_InventoryLevelType);
            #endregion

            await Task.WhenAll(Mst_Inventory_Get_Task, Mst_InventoryType_Get_Task, Mst_InventoryLevelType_Get_Task);
            var list_Mst_Inventory = new List<Mst_Inventory>();
            var list_Mst_InventoryType = new List<Mst_InventoryType>();
            var list_Mst_InventoryLevelType = new List<Mst_InventoryLevelType>();

            if (Mst_Inventory_Get_Task != null && Mst_Inventory_Get_Task.Result != null && Mst_Inventory_Get_Task.Result.Count > 0)
            {
                list_Mst_Inventory.AddRange(Mst_Inventory_Get_Task.Result);
            }
            if (Mst_InventoryType_Get_Task != null && Mst_InventoryType_Get_Task.Result != null && Mst_InventoryType_Get_Task.Result.Count > 0)
            {
                list_Mst_InventoryType.AddRange(Mst_InventoryType_Get_Task.Result);
            }
            if (Mst_InventoryLevelType_Get_Task != null && Mst_InventoryLevelType_Get_Task.Result != null && Mst_InventoryLevelType_Get_Task.Result.Count > 0)
            {
                list_Mst_InventoryLevelType.AddRange(Mst_InventoryLevelType_Get_Task.Result);
            }

            ViewBag.Lst_Mst_Inventory = list_Mst_Inventory;
            ViewBag.Lst_Mst_InventoryType = list_Mst_InventoryType;
            ViewBag.Lst_Mst_InventoryLevelType = list_Mst_InventoryLevelType;

            resultEntry.Success = true;

            return JsonView("Create", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                var objMst_Inventory = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Inventory>(model);
                objMst_Inventory.OrgID = orgID;
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    // RQ_
                    Mst_Inventory = objMst_Inventory
                };
                Mst_InventoryService.Instance.WA_Mst_Inventory_Create(objRQ_Mst_Inventory, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới kho thành công!");
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

        public async Task<ActionResult> Update(string InvCode)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            #region ["Mst_Inventory + Mst_InventoryType + Mst_InventoryLevelType"]
            var strWhere_Mst_InventoryType = "Mst_InventoryType.FlagActive = '1'";
            var strWhere_Mst_InventoryLevelType = "Mst_InventoryLevelType.FlagActive = '1'";
            var Mst_InventoryType_Get_Task = Mst_InventoryType_Get_Async(strWhere_Mst_InventoryType);
            var Mst_InventoryLevelType_Get_Task = Mst_InventoryLevelType_Get_Async(strWhere_Mst_InventoryLevelType);

            await Task.WhenAll(Mst_InventoryType_Get_Task, Mst_InventoryLevelType_Get_Task);
            var list_Mst_InventoryType = new List<Mst_InventoryType>();
            var list_Mst_InventoryLevelType = new List<Mst_InventoryLevelType>();

            if (Mst_InventoryType_Get_Task != null && Mst_InventoryType_Get_Task.Result != null && Mst_InventoryType_Get_Task.Result.Count > 0)
            {
                list_Mst_InventoryType.AddRange(Mst_InventoryType_Get_Task.Result);
            }
            if (Mst_InventoryLevelType_Get_Task != null && Mst_InventoryLevelType_Get_Task.Result != null && Mst_InventoryLevelType_Get_Task.Result.Count > 0)
            {
                list_Mst_InventoryLevelType.AddRange(Mst_InventoryLevelType_Get_Task.Result);
            }

            ViewBag.Lst_Mst_InventoryType = list_Mst_InventoryType;
            ViewBag.Lst_Mst_InventoryLevelType = list_Mst_InventoryLevelType;
            #endregion

            string strWhere_Mst_Inventory = string.Format("Mst_Inventory.InvCode = '{0}'", InvCode);

            var List_Mst_Inventory = new List<Mst_Inventory>();
            try
            {
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    Ft_WhereClause = strWhere_Mst_Inventory,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_Inventory = "*"
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    resultEntry.Success = true;
                    return JsonView("Update", objRT_Mst_Inventory.Lst_Mst_Inventory[0]);
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
        [ValidateAntiForgeryToken]
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
                var objMst_Inventory = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Inventory>(model);
                objMst_Inventory.OrgID = orgID;
                var strFt_Cols_Upd = "";
                var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvName);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvAddress);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvLevelType);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvType);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvContactName);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvContactEmail);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.InvContactPhone);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.FlagActive);
                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Inventory, TblMst_Inventory.FlagIn_Out);
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    Mst_Inventory = objMst_Inventory
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Inventory);
                Mst_InventoryService.Instance.WA_Mst_Inventory_Update(objRQ_Mst_Inventory, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật kho thành công!");
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

        public ActionResult Detail(string invCode)
        {
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgID = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            var resultEntry = new JsonResultEntry() { Success = false };

            if (!CUtils.IsNullOrEmpty(invCode))
            {
                string strWhere_Mst_Inventory = string.Format("Mst_Inventory.InvCode = '{0}'", invCode);

                var List_Mst_Inventory = new List<Mst_Inventory>();
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    Ft_WhereClause = strWhere_Mst_Inventory,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_Inventory = "*"
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    return JsonView("Detail", objRT_Mst_Inventory.Lst_Mst_Inventory[0]);
                }
            }
            return View(new Mst_Inventory());
        }

        [HttpPost]
        public ActionResult Delete(string invCode)
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
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                objRQ_Mst_Inventory.Mst_Inventory = new Mst_Inventory()
                {
                    OrgID = orgID,
                    InvCode = invCode
                };
                Mst_InventoryService.Instance.WA_Mst_Inventory_Delete(objRQ_Mst_Inventory, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Xoá kho thành công!");
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
                    var list = new List<Mst_Inventory>();


                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        list = DataTableCmUtils.ToListof<Mst_Inventory>(table);
                        if (table.Columns.Count != 11)
                        {
                            var exitsData = Nonsense.MESS_CHECK_FILEIMPORT;
                            resultEntry.Detail = exitsData;
                            return Json(resultEntry);
                        }
                        else
                        {
                            string strWhere_Mst_Inventory = "Mst_Inventory.FlagActive = '1'";
                            var listCur = new List<Mst_Inventory>();
                            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                                Ft_WhereClause = strWhere_Mst_Inventory,
                                Ft_Cols_Upd = null,
                                // RQ_Mst_Customer
                                Rt_Cols_Mst_Inventory = "*"
                            };
                            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                            {
                                listCur.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                            }

                            foreach (DataRow dr in table.Rows)
                            {
                                if (dr["InvCode"] == null || dr["InvCode"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã kho không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                                if (dr["InvCodeParent"] == null || dr["InvCodeParent"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Mã kho cha không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                                if (dr["InvName"] == null || dr["InvName"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Tên kho " + "'" + dr["InvCode"].ToString().ToUpper() + "'" + " không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }

                                
                                if (dr["InvLevelType"] == null || dr["InvLevelType"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Cấp kho " + "'" + dr["InvLevelType"].ToString().ToUpper() + "'" + " không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                                if (dr["InvType"] == null || dr["InvType"].ToString().Trim().Length == 0)
                                {
                                    var exitsData = "Loại kho " + "'" + dr["InvType"].ToString().ToUpper() + "'" + " không được trống!";
                                    resultEntry.Detail = exitsData;
                                    return Json(resultEntry);
                                }
                            }
                            if (list != null && list.Count > 0)
                            {
                                var objRQ_Mst_Inventory_Input = new RQ_Mst_Inventory()
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
                                    // 
                                };
                                foreach (var item in list)
                                {
                                    if (listCur != null && listCur.Count > 0 &&
                                        listCur.Any(m => m.InvCode.ToString().ToUpper() == item.InvCode.ToString().ToUpper()))
                                    {
                                        var exitsData = "Mã kho: " + "'" + item.InvCode.ToString().ToUpper() + "'" + " đã tồn tại trong hệ thống. Vui lòng kiểm tra lại!";
                                        resultEntry.Detail = exitsData;
                                        return Json(resultEntry);
                                    }
                                    if (
                                        list.Where(m => m != item)
                                            .Any(m => m.InvCode.ToString().ToUpper() == item.InvCode.ToString().ToUpper()))
                                    {
                                        var exitsData = "Trùng mã kho: " + "'" + item.InvCode.ToString().ToUpper() + "'" + ". Vui lòng kiểm tra lại!";
                                        resultEntry.Detail = exitsData;
                                        return Json(resultEntry);
                                    }

                                    item.InvCode = item.InvCode.ToString().ToUpper();
                                    item.InvCodeParent = item.InvCodeParent.ToString().ToUpper();
                                    item.InvLevelType = item.InvLevelType.ToString().ToUpper();
                                    item.InvType = item.InvType.ToString().ToUpper();
                                    item.OrgID = orgID;
                                    item.FlagActive = "1";
                                    objRQ_Mst_Inventory_Input.Mst_Inventory = item;
                                    Mst_InventoryService.Instance.WA_Mst_Inventory_Create(objRQ_Mst_Inventory_Input, addressAPIs);
                                }
                            }
                            resultEntry.Success = true;
                            resultEntry.AddMessage(Nonsense.MESS_IMPORT_EXCEL_SUCCESS);
                            return Json(resultEntry);
                        }
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
                var list_Mst_Inventory = new List<Mst_Inventory>();
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                    Rt_Cols_Mst_Inventory = "*"
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
                {
                    list_Mst_Inventory.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
                }
                if (list_Mst_Inventory != null && list_Mst_Inventory.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Inventory).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list_Mst_Inventory, dicColNames, filePath, string.Format("Mst_Inventory"));

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
                var listInv = new List<Mst_Inventory>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();


                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Inventory).Name), ref url);
                ExcelExport.ExportToExcelFromList(listInv, dicColNames, filePath, string.Format("Mst_Inventory"));

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
                 {"InvCode","Mã kho(*)"},
                 {"InvName","Tên kho(*)"},
                 {"InvCodeParent","Mã kho cha(*)"},
                 {"InvAddress","Địa chỉ"},
                 {"InvLevelType","Cấp kho(*)"},
                 {"InvType","Loại kho(*)"},
                 {"InvContactName","Người quản lý"},
                 {"InvContactEmail","Email"},
                 {"InvContactPhone","Điện thoại"},
                 {"FlagActive","Trạng thái"},
                 {"FlagIn_Out","Flag kho nhập/xuất"},
                 {"InvBUPattern", "BU(%)" }
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"InvCode","Mã kho(*)"},
                 {"InvName","Tên kho(*)"},
                 {"InvCodeParent","Mã kho cha(*)"},
                 {"InvAddress","Địa chỉ"},
                 {"InvLevelType","Cấp kho(*)"},
                 {"InvType","Loại kho(*)"},
                 {"InvContactName","Người quản lý"},
                 {"InvContactEmail","Email"},
                 {"InvContactPhone","Điện thoại"},
                 {"FlagIn_Out","Flag kho nhập/xuất"},
                  {"FlagActive","Trạng thái"},
            };
        }
        #endregion

    }
}