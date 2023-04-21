using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Controllers;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Mst_DepartmentController : AdminBaseController
    {
        // GET: Mst_Department
        [HttpGet]
        public ActionResult Index(string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_Department>(0, PageSizeConfig)
            {
                DataList = new List<Mst_Department>()
            };
            var itemCount = 0;
            var pageCount = 0;
            
                #region["Search"]

                var objRQ_Mst_Department = new RQ_Mst_Department()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Department
                    Rt_Cols_Mst_Department = "*"
                };
                var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);

                if (objRT_Mst_DepartmentCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_DepartmentCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_DepartmentCur != null && objRT_Mst_DepartmentCur.Lst_Mst_Department != null && objRT_Mst_DepartmentCur.Lst_Mst_Department.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_DepartmentCur.Lst_Mst_Department);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
            #endregion
            ViewBag.PageCur = page.ToString();
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới bộ phận/phòng ban"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            #region["Bộ phận phòng ban"]
            var objMst_Department = new Mst_Department
            {
                FlagActive = FlagActive
            };
            var listMst_DepartmentUI = new List<Mst_DepartmentUI>();
            var strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);
            listMst_DepartmentUI = ListDepartmentUI(strWhereClauseMst_Department);
            ViewBag.ListMst_DepartmentUI = listMst_DepartmentUI;
            #endregion
            #region["Mã số thuế"]
            var objMst_NNT = new Mst_NNT
            {
                FlagActive = FlagActive
            };
            var listMst_NNT = new List<Mst_NNT>();
            var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);
            listMst_NNT.AddRange(List_Mst_NNT(strWhereClauseMst_NNT));
            ViewBag.ListMst_NNT = listMst_NNT;
            #endregion
            var objRQ_Mst_Department = new RQ_Mst_Department()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_Department
                Rt_Cols_Mst_Department = "*",
                Mst_Department = null
            };
            var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_DepartmentInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Department>(model);
                //var title = "";
                var objRQ_Mst_Department = new RQ_Mst_Department
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
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Department = null,
                    Mst_Department = objMst_DepartmentInput
                };
                Mst_DepartmentService.Instance.WA_Mst_Department_Create(objRQ_Mst_Department, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới bộ phận phòng ban thành công!");
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

        #region["Sửa bộ phận phòng ban"]
        [HttpGet]
        public ActionResult Update(string departmentcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(departmentcode))
            {
                var objMst_Department = new Mst_Department()
                {
                    DepartmentCode = departmentcode
                };

                var strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);

                var objRQ_Mst_Department = new RQ_Mst_Department()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Department,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Department
                    Rt_Cols_Mst_Department = "*"
                };
                var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
                if (objRT_Mst_DepartmentCur.Lst_Mst_Department != null && objRT_Mst_DepartmentCur.Lst_Mst_Department.Count > 0)
                {
                    #region["Bộ phận phòng ban"]
                    objMst_Department = new Mst_Department
                    {
                        FlagActive = FlagActive
                    };
                    var listMst_DepartmentUI = new List<Mst_DepartmentUI>();
                    strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);
                    listMst_DepartmentUI = ListDepartmentUI(strWhereClauseMst_Department);
                    ViewBag.ListMst_DepartmentUI = listMst_DepartmentUI;
                    #endregion
                    #region["Mã số thuế"]
                    var objMst_NNT = new Mst_NNT
                    {
                        FlagActive = FlagActive
                    };
                    var listMst_NNT = new List<Mst_NNT>();
                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);
                    listMst_NNT.AddRange(List_Mst_NNT(strWhereClauseMst_NNT));
                    ViewBag.ListMst_NNT = listMst_NNT;
                    #endregion
                    return View(objRT_Mst_DepartmentCur.Lst_Mst_Department[0]);
                }
            }
            return View(new Mst_Department());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            var addressAPIs = userState.AddressAPIs;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_DepartmentInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Department>(model);
                var objMst_Department = new Mst_Department()
                {
                    DepartmentCode = objMst_DepartmentInput.DepartmentCode
                };

                var strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);

                var objRQ_Mst_Department = new RQ_Mst_Department()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Department,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Department
                    Rt_Cols_Mst_Department = "*",
                    Mst_Department = null,
                };
                var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
                if (objRT_Mst_DepartmentCur.Lst_Mst_Department != null && objRT_Mst_DepartmentCur.Lst_Mst_Department.Count > 0)
                {
                    //objRT_Mst_DepartmentCur.Lst_Mst_Department[0].TenantId = objMst_DepartmentInput.TenantId;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].DepartmentCodeParent = objMst_DepartmentInput.DepartmentCodeParent;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].DepartmentBUCode = objMst_DepartmentInput.DepartmentBUCode;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].DepartmentBUPattern = objMst_DepartmentInput.DepartmentBUPattern;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].DepartmentLevel = objMst_DepartmentInput.DepartmentLevel;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].MST = objMst_DepartmentInput.MST;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].DepartmentName = objMst_DepartmentInput.DepartmentName;
                    objRT_Mst_DepartmentCur.Lst_Mst_Department[0].FlagActive = objMst_DepartmentInput.FlagActive;

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_Department = TableName.Mst_Department + ".";
                    //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.TenantId);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.DepartmentCodeParent);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.DepartmentBUCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.DepartmentBUPattern);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.DepartmentLevel);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.MST);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.DepartmentName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Department, TblMst_Department.FlagActive);

                    objRQ_Mst_Department.Ft_WhereClause = null;
                    objRQ_Mst_Department.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_Department.Rt_Cols_Mst_Department = null;
                    objRQ_Mst_Department.Mst_Department = objRT_Mst_DepartmentCur.Lst_Mst_Department[0];
                    Mst_DepartmentService.Instance.WA_Mst_Department_Update(objRQ_Mst_Department, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa bộ phận phòng ban thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã bộ phận phòng ban '" + objMst_DepartmentInput.DepartmentCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Bộ phận phòng ban trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region["Chi tiết - Xóa bộ phận phòng ban"]
        [HttpGet]
        public ActionResult Detail(string departmentcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(departmentcode))
            {
                var objMst_Department = new Mst_Department()
                {
                    DepartmentCode = departmentcode
                };

                var strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);

                var objRQ_Mst_Department = new RQ_Mst_Department()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Department,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Department
                    Rt_Cols_Mst_Department = "*",
                    Mst_Department = null,
                };

                var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
                if (objRT_Mst_DepartmentCur.Lst_Mst_Department != null && objRT_Mst_DepartmentCur.Lst_Mst_Department.Count > 0)
                {
                    #region["Bộ phận phòng ban"]
                    objMst_Department = new Mst_Department
                    {
                        FlagActive = FlagActive
                    };
                    var listMst_DepartmentUI = new List<Mst_DepartmentUI>();
                    strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);
                    listMst_DepartmentUI = ListDepartmentUI(strWhereClauseMst_Department);
                    ViewBag.ListMst_DepartmentUI = listMst_DepartmentUI;
                    #endregion
                    #region["Mã số thuế"]
                    var objMst_NNT = new Mst_NNT
                    {
                        FlagActive = FlagActive
                    };
                    var listMst_NNT = new List<Mst_NNT>();
                    var strWhereClauseMst_NNT = strWhereClause_Mst_NNT(objMst_NNT);
                    listMst_NNT.AddRange(List_Mst_NNT(strWhereClauseMst_NNT));
                    ViewBag.ListMst_NNT = listMst_NNT;
                    #endregion
                    return View(objRT_Mst_DepartmentCur.Lst_Mst_Department[0]);
                }
            }
            return View(new Mst_Department());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string departmentcode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(departmentcode))
            {
                var objMst_Department = new Mst_Department()
                {
                    DepartmentCode = departmentcode
                };

                var strWhereClauseMst_Department = strWhereClause_Mst_Department(objMst_Department);

                var objRQ_Mst_Department = new RQ_Mst_Department()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Department,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Department
                    Rt_Cols_Mst_Department = "*",
                    Mst_Department = null,
                };

                var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
                if (objRT_Mst_DepartmentCur.Lst_Mst_Department != null && objRT_Mst_DepartmentCur.Lst_Mst_Department.Count > 0)
                {
                    objRQ_Mst_Department.Mst_Department = objRT_Mst_DepartmentCur.Lst_Mst_Department[0];

                    Mst_DepartmentService.Instance.WA_Mst_Department_Delete(objRQ_Mst_Department, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Xóa bộ phận phòng ban thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã bộ phận phong ban'" + departmentcode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Bộ phận phòng ban trống!");
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<Mst_Department>();
                var objRQ_Mst_Department = new RQ_Mst_Department()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Department
                    Rt_Cols_Mst_Department = "*"
                };

                var objRT_Mst_DepartmentCur = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
                if (objRT_Mst_DepartmentCur.Lst_Mst_Department != null && objRT_Mst_DepartmentCur.Lst_Mst_Department.Count > 0)
                {
                    list.AddRange(objRT_Mst_DepartmentCur.Lst_Mst_Department);

                    
                    Dictionary<string, string> dicColNames = GetImportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Department).Name), ref url);

                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Department"));

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Không có dữ liệu!", CheckSuccess = "1" });
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
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<Mst_Department>();

                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Department).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Department"));

                return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        #endregion

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var networkID = userState.SysUser.NetworkID;
            var mst = userState.SysUser.MST;
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
                var list = new List<Mst_Department>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 3)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        foreach (DataRow dr in table.Rows)
                        {
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Department.DepartmentCode]))
                            {
                                exitsData = "Mã bộ phận không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Department.DepartmentName]))
                            {
                                exitsData = "Tên bộ phận không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Department.DepartmentCodeParent]))
                            {
                                exitsData = "Bộ phận quản lý không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Department.MST]))
                            //{
                            //    exitsData = "Mã đơn vị không được trống!";
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            
                        }
                        #endregion

                        #region["Check duplicate"]
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            var departmentCodeCur = table.Rows[i][TblMst_Department.DepartmentCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                if (i != j)
                                {
                                    var _departmentCodeCur = table.Rows[j][TblMst_Department.DepartmentCode].ToString().Trim();
                                    if (departmentCodeCur.Equals(_departmentCodeCur))
                                    {
                                        exitsData = "Mã bộ phận '" + departmentCodeCur + "' bị lặp trong file excel!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    list = DataTableCmUtils.ToListof<Mst_Department>(table); ;
                    // Gọi hàm save data
                    if (list != null && list.Count > 0)
                    {
                        
                        foreach (var item in list)
                        {
                            item.FlagActive = "1";
                            item.NetworkID = networkID;
                            item.MST = mst;
                            var objRQ_Mst_Department = new RQ_Mst_Department
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
                                // RQ_Mst_Province
                                Rt_Cols_Mst_Department = null,
                                Mst_Department = item,
                            };
                            Mst_DepartmentService.Instance.WA_Mst_Department_Create(objRQ_Mst_Department, addressAPIs);

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

        #region[""]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Department.DepartmentCode,"Mã bộ phận (*)"},
                {TblMst_Department.DepartmentName,"Tên bộ phận (*)"},
                {TblMst_Department.DepartmentCodeParent,"Bộ phận quản lý (*)"},
                {TblMst_Department.MST,"Mã đơn vị (*)"},
                {TblMst_Department.FlagActive,"Trạng thái"}
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {TblMst_Department.DepartmentCode,"Mã bộ phận (*)"},
                 {TblMst_Department.DepartmentName,"Tên bộ phận (*)"},
                 {TblMst_Department.DepartmentCodeParent,"Bộ phận quản lý (*)"},
                 //{TblMst_Department.MST,"Mã đơn vị (*)"},
                 //{"FlagActive","Trạng thái"}
            };
        }

        #endregion

        #region[""]
        private string strWhereClause_Mst_Department(Mst_Department data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Department = TableName.Mst_Department + ".";
            if (!CUtils.IsNullOrEmpty(data.DepartmentCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.DepartmentCode, CUtils.StrValueOrNull(data.DepartmentCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DepartmentName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.DepartmentName, "%" + CUtils.StrValueOrNull(data.DepartmentName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive,CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        
        #endregion
    }
}