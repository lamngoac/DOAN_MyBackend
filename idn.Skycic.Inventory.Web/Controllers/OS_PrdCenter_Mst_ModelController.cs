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
    public class OS_PrdCenter_Mst_ModelController : AdminBaseController
    {
        // Chú ý: Xử lý không sử dụng OS_PrdCenter_Mst_Model.ModelCode mà sử dụng: Mst_Model.ModelCode
        // GET: OS_PrdCenter_Mst_Model
        public ActionResult Index(string brandcode = "", string modelcode = "", string modelname = "", string orgmodelcode = "", string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            var pageInfo = new PageInfo<ProductCentrer.Mst_Model>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_Model>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_Model = new ProductCentrer.Mst_Model()
                {
                    BrandCode = brandcode,
                    ModelCode = modelcode,
                    ModelName = modelname,
                    OrgModelCode = orgmodelcode,
                };

                var strWhereClauseMst_Model = strWhereClause_Mst_Model(objMst_Model);

                var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                    Ft_WhereClause = strWhereClauseMst_Model,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Model
                    Rt_Cols_Mst_Model = "*",
                    Mst_Model = null,
                };

                var objRT_Mst_ModelCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);

                if (objRT_Mst_ModelCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_ModelCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_ModelCur != null && objRT_Mst_ModelCur.Lst_Mst_Model != null && objRT_Mst_ModelCur.Lst_Mst_Model.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_ModelCur.Lst_Mst_Model);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.BrandCode = brandcode;
            ViewBag.ModelCode = modelcode;
            ViewBag.ModelName = modelname;
            ViewBag.OrgModelCode = orgmodelcode;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới Model"]

        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            #region["Brand"]
            var objMst_Brand = new ProductCentrer.Mst_Brand()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseOS_PrdCenter_Mst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

            var listMst_Brand = List_Mst_Brand(strWhereClauseOS_PrdCenter_Mst_Brand);
            ViewBag.ListOS_PrdCenter_Mst_Brand = listMst_Brand;
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

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }
            

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_ModelInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_Model>(model);
                //var title = "";
                if (isparent)
                {
                    //objOS_PrdCenter_Mst_ModelInput.NetworkModelCode = objOS_PrdCenter_Mst_ModelInput.ModelCode;
                }

                var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model
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
                    // RQ_Mst_Model
                    Rt_Cols_Mst_Model = null,
                    Mst_Model = objMst_ModelInput
                };
                OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Create(objRQ_Mst_Model, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới Model thành công!");
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

        #region["Sửa Model"]
        [HttpGet]
        public ActionResult Update(string modelcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            if (!CUtils.IsNullOrEmpty(modelcode))
            {
                var objMst_Model = new ProductCentrer.Mst_Model()
                {
                    ModelCode = modelcode,
                };

                var strWhereClauseMst_Model = strWhereClause_Mst_Model(objMst_Model);

                var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                    Ft_WhereClause = strWhereClauseMst_Model,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Model
                    Rt_Cols_Mst_Model = "*",
                    Mst_Model = null,
                };

                var objRT_Mst_ModelCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);
                if (objRT_Mst_ModelCur.Lst_Mst_Model != null && objRT_Mst_ModelCur.Lst_Mst_Model.Count > 0)
                {
                    #region["Brand"]
                    var objMst_Brand = new ProductCentrer.Mst_Brand()
                    {
                        FlagActive = FlagActive,
                    };
                    if (!isparent)
                    {
                        objMst_Brand.OrgID = waOrgID;
                    }
                    var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                    var listMst_Brand = List_Mst_Brand(strWhereClauseMst_Brand);
                    ViewBag.ListOS_PrdCenter_Mst_Brand = listMst_Brand;
                    #endregion
                    return View(objRT_Mst_ModelCur.Lst_Mst_Model[0]);
                }
            }
            return View(new ProductCentrer.Mst_Model());
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

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_ModelInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_Model>(model);
                    var objMst_Model = new ProductCentrer.Mst_Model()
                    {
                        OrgID = objMst_ModelInput.OrgID,
                        ModelCode = objMst_ModelInput.ModelCode,
                    };

                    var strWhereClauseMst_Model = strWhereClause_Mst_Model(objMst_Model);

                    var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                        Ft_WhereClause = strWhereClauseMst_Model,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Model
                        Rt_Cols_Mst_Model = "*",
                        Mst_Model = null,
                    };

                    var objRT_Mst_ModelCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);
                    if (objRT_Mst_ModelCur.Lst_Mst_Model != null && objRT_Mst_ModelCur.Lst_Mst_Model.Count > 0)
                    {
                        var orgID = objRT_Mst_ModelCur.Lst_Mst_Model[0].OrgID;

                        objRT_Mst_ModelCur.Lst_Mst_Model[0].ModelName = objMst_ModelInput.ModelName;
                        objRT_Mst_ModelCur.Lst_Mst_Model[0].BrandCode = objMst_ModelInput.BrandCode;
                        objRT_Mst_ModelCur.Lst_Mst_Model[0].OrgModelCode = objMst_ModelInput.OrgModelCode;
                        objRT_Mst_ModelCur.Lst_Mst_Model[0].Remark = objMst_ModelInput.Remark;
                        objRT_Mst_ModelCur.Lst_Mst_Model[0].FlagActive = objMst_ModelInput.FlagActive;
                        objRT_Mst_ModelCur.Lst_Mst_Model[0].NetworkModelCode = objMst_ModelInput.NetworkModelCode;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Model = TableName.Mst_Model + ".";
                        
                        if (isparent && !waOrgID.Equals(orgID))
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.NetworkModelCode);
                        }
                        else
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.ModelName);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.BrandCode);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.OrgModelCode);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.Remark);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.FlagActive);
                        }
                        objRQ_Mst_Model.Ft_WhereClause = null;
                        objRQ_Mst_Model.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_Model.Rt_Cols_Mst_Model = null;
                        objRQ_Mst_Model.Mst_Model = objRT_Mst_ModelCur.Lst_Mst_Model[0];

                        OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Update(objRQ_Mst_Model, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa Model thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Model '" + objMst_ModelInput.BrandCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Model trống!");
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

        #region["Chi tiết - Xóa Brand"]
        [HttpGet]
        public ActionResult Detail(string modelcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;
            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            if (!CUtils.IsNullOrEmpty(modelcode))
            {
                var objMst_Model = new ProductCentrer.Mst_Model()
                {
                    ModelCode = modelcode,
                };

                var strWhereClauseMst_Model = strWhereClause_Mst_Model(objMst_Model);

                var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                    Ft_WhereClause = strWhereClauseMst_Model,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Model
                    Rt_Cols_Mst_Model = "*",
                    Mst_Model = null,
                };

                var objRT_Mst_ModelCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);
                if (objRT_Mst_ModelCur.Lst_Mst_Model != null && objRT_Mst_ModelCur.Lst_Mst_Model.Count > 0)
                {
                    #region["Brand"]
                    var objMst_Brand = new ProductCentrer.Mst_Brand()
                    {
                        FlagActive = FlagActive,
                    };
                    if (!isparent)
                    {
                        objMst_Brand.OrgID = waOrgID;
                    }
                    var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                    var listMst_Brand = List_Mst_Brand(strWhereClauseMst_Brand);
                    ViewBag.ListOS_PrdCenter_Mst_Brand = listMst_Brand;
                    #endregion
                    return View(objRT_Mst_ModelCur.Lst_Mst_Model[0]);
                }
            }
            return View(new ProductCentrer.Mst_Model());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string modelcode, string orgID)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(modelcode))
                {
                    var objMst_Model = new ProductCentrer.Mst_Model()
                    {
                        ModelCode = modelcode,
                        OrgID = orgID
                    };

                    var strWhereClauseMst_Model = strWhereClause_Mst_Model(objMst_Model);

                    var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                        Ft_WhereClause = strWhereClauseMst_Model,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Model
                        Rt_Cols_Mst_Model = "*",
                        Mst_Model = null,
                    };

                    var objRT_Mst_ModelCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);
                    if (objRT_Mst_ModelCur.Lst_Mst_Model != null && objRT_Mst_ModelCur.Lst_Mst_Model.Count > 0)
                    {
                        objRQ_Mst_Model.Mst_Model = objRT_Mst_ModelCur.Lst_Mst_Model[0];

                        OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Delete(objRQ_Mst_Model, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa Model thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Model '" + modelcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Model trống!");
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
                var listMst_Model = new List<ProductCentrer.Mst_Model>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 5)
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

                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Model.OrgID]))
                            //{
                            //    exitsData = "Mã Tổ chức không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Model.ModelCode]))
                            {
                                exitsData = "Mã Model không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Model.ModelName]))
                            {
                                exitsData = "Tên Model không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Model.BrandCode]))
                            {
                                exitsData = "Mã Brand không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (!CUtils.IsNullOrEmpty(dr[TblMst_Model.Remark]))
                            {
                                var remark = CUtils.StrValueOrNull(dr[TblMst_Model.Remark]);
                                if (remark.Length > Nonsense.RemarkLength)
                                {
                                    exitsData = "Mô tả > " + Nonsense.RemarkLength + " ký tự!" + strRows;
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
                            var modelCodeCur = table.Rows[i][TblMst_Model.ModelCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _modelCodeCur = table.Rows[j][TblMst_Model.ModelCode].ToString().Trim();
                                    if (modelCodeCur.Equals(_modelCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã Model '" + modelCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Model = DataTableCmUtils.ToListof<ProductCentrer.Mst_Model>(table);
                    // Gọi hàm save data
                    if (listMst_Model != null && listMst_Model.Count > 0)
                    {

                        foreach (var item in listMst_Model)
                        {
                            item.OrgID = orgID;
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model
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
                                // RQ_Mst_Model
                                Rt_Cols_Mst_Model = null,
                                Mst_Model = item,
                            };
                            OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Create(objRQ_Mst_Model, addressAPIs_ProductCentrer);

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportMapping(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            var exitsData = "";

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

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
                var listMst_Model = new List<ProductCentrer.Mst_Model>();
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
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Model.OrgID]))
                            {
                                exitsData = "Mã Tổ chức không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var orgId = CUtils.StrValueOrNull(dr[TblMst_Model.OrgID]);
                                if (orgId.Equals(waNetworkID))
                                {
                                    exitsData = "Không được mapping mã model chung cho đơn vị tổng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Model.ModelCode]))
                            {
                                exitsData = "Mã Model không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Model.NetworkModelCode]))
                            //{
                            //    exitsData = "Mã Model chung không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            idx++;
                        }
                        #endregion
                    }
                    listMst_Model = DataTableCmUtils.ToListof<ProductCentrer.Mst_Model>(table);
                    // Gọi hàm save data
                    if (listMst_Model != null && listMst_Model.Count > 0)
                    {
                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Model = TableName.Mst_Model + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.OrgID);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.ModelCode);
                        if (isparent)
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Model, TblMst_Model.NetworkModelCode);
                        }
                        foreach (var item in listMst_Model)
                        {
                            var objMst_Model = new ProductCentrer.Mst_Model()
                            {
                                OrgID = item.OrgID,
                                ModelCode = item.ModelCode,
                                NetworkModelCode = item.NetworkModelCode,
                            };

                            
                            var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = strFt_Cols_Upd,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                UtcOffset = userState.UtcOffset,
                                // RQ_Mst_Model
                                Rt_Cols_Mst_Model = null,
                                Mst_Model = objMst_Model,
                            };

                            OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Update(objRQ_Mst_Model, addressAPIs_ProductCentrer);

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
            var list = new List<ProductCentrer.Mst_Model>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Model).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Model"));

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
        public ActionResult ExportTemplateMapping()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<ProductCentrer.Mst_Model>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateMapping();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Model).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Model"));

                return Json(new { Success = true, Title = "Xuất file excel template mapping thành công!", CheckSuccess = "1", strUrl = url });
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
            var listMst_Model = new List<ProductCentrer.Mst_Model>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                //var objOS_PrdCenter_Mst_Model = new OS_PrdCenter_Mst_Model()
                //{
                //    BrandCode = "",
                //    BrandName = "",
                //    FlagActive = "",
                //};

                //var strWhereClauseOS_PrdCenter_Mst_Model = strWhereClause_Mst_Model(objOS_PrdCenter_Mst_Model);

                var strWhereClauseMst_Model = "";

                var objRQ_Mst_Model = new ProductCentrer.RQ_Mst_Model()
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
                    Ft_WhereClause = strWhereClauseMst_Model,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Model
                    Rt_Cols_Mst_Model = "*",
                    Mst_Model = null,
                };

                var objRT_Mst_ModelCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);

                if (objRT_Mst_ModelCur != null && objRT_Mst_ModelCur.Lst_Mst_Model != null)
                {
                    if (objRT_Mst_ModelCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_ModelCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_ModelCur.Lst_Mst_Model != null && objRT_Mst_ModelCur.Lst_Mst_Model.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Model.AddRange(objRT_Mst_ModelCur.Lst_Mst_Model);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Model).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Model, dicColNames, filePath, string.Format("Mst_Model"));


                    #region["Export các trang còn lại"]
                    listMst_Model.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Model.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_ModelExportCur = OS_PrdCenter_Mst_ModelService.Instance.WA_Mst_Model_Get(objRQ_Mst_Model, addressAPIs_ProductCentrer);
                            if (objRT_Mst_ModelExportCur != null && objRT_Mst_ModelExportCur.Lst_Mst_Model != null && objRT_Mst_ModelExportCur.Lst_Mst_Model.Count > 0)
                            {
                                listMst_Model.AddRange(objRT_Mst_ModelExportCur.Lst_Mst_Model);
                                ExcelExport.ExportToExcelFromList(listMst_Model, dicColNames, filePath, string.Format("Mst_Model_" + i));
                                listMst_Model.Clear();
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
                //{TblMst_Model.OrgID,"Mã tổ chức (*)"},
                {TblMst_Model.ModelCode,"Mã Model (*)"},
                {TblMst_Model.ModelName,"Tên Model (*)"},
                {TblMst_Model.OrgModelCode,"Mã OrgModel"},
                {TblMst_Model.BrandCode,"Mã Brand (*)"},
                {TblMst_Model.Remark,"Mô tả"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplateMapping()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Model.OrgID,"Mã tổ chức (*)"},
                {TblMst_Model.ModelCode,"Mã Model (*)"},
                {TblMst_Model.NetworkModelCode,"Mã Model Chung"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            var userState = this.UserState;
            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {TblMst_Model.OrgID,"Mã tổ chức (*)"},
                {TblMst_Model.ModelCode,"Mã Model (*)"},
                //{TblMst_Model.NetworkModelCode,"Mã Model Chung"},
            };
            if (waOrgID.Equals(waNetworkID))
            {
                dictionary.Add(TblMst_Brand.NetworkBrandCode, "Mã Brand chung");
            }

            dictionary.Add(TblMst_Model.ModelName, "Tên Model (*)");
            dictionary.Add(TblMst_Model.OrgModelCode, "Mã OrgModel");
            dictionary.Add(TblMst_Model.BrandCode, "Mã Brand (*)");
            dictionary.Add(TblMst_Model.Remark, "Mô tả");
            dictionary.Add(TblMst_Model.FlagActive, "Trạng thái");
            return dictionary;
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_Model(ProductCentrer.Mst_Model data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Model = TableName.Mst_Model + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.ModelCode, CUtils.StrValueOrNull(data.ModelCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ModelName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.ModelName, "%" + CUtils.StrValueOrNull(data.ModelName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.BrandCode, CUtils.StrValueOrNull(data.BrandCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.OrgModelCode, CUtils.StrValueOrNull(data.OrgModelCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_Brand(ProductCentrer.Mst_Brand data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandCode, CUtils.StrValueOrNull(data.BrandCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandName, "%" + CUtils.StrValueOrNull(data.BrandName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}