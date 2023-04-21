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
    public class OS_PrdCenter_Mst_BrandController : AdminBaseController
    {
        // Chú ý: Xử lý không sử dụng OS_PrdCenter_Mst_Brand.BrandCode mà sử dụng: Mst_Brand.BrandCode
        // GET: OS_PrdCenter_Mst_Brand
        public ActionResult Index(string brandcode = "", string brandname = "", string init = "init", int page = 0)
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

            var pageInfo = new PageInfo<ProductCentrer.Mst_Brand>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_Brand>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_Brand = new ProductCentrer.Mst_Brand()
                {
                    BrandCode = brandcode,
                    BrandName = brandname,
                };

                var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    //Ft_RecordStart = (page * PageSizeConfig).ToString(),
                    //Ft_RecordCount = PageSizeConfig.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Brand,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    // RQ_Mst_Brand
                    Rt_Cols_Mst_Brand = "*",
                    Mst_Brand = null,
                };

                var objRT_Mst_BrandCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

                if (objRT_Mst_BrandCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_BrandCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_BrandCur != null && objRT_Mst_BrandCur.Lst_Mst_Brand != null && objRT_Mst_BrandCur.Lst_Mst_Brand.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_BrandCur.Lst_Mst_Brand);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.BrandCode = brandcode;
            ViewBag.BrandName = brandname;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới Brand"]

        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            ViewBag.OrgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

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
                var objMst_BrandInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_Brand>(model);
                //var title = "";

                //Nếu là Org cha thì truyền BrandCode chung bằng BrandCode
                if (isparent)
                {
                    //objOS_PrdCenter_Mst_BrandInput.NetworkBrandCode = objOS_PrdCenter_Mst_BrandInput.BrandCode;
                }

                var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand
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
                    // RQ_Mst_Brand
                    Rt_Cols_Mst_Brand = null,
                    Mst_Brand = objMst_BrandInput
                };
                OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Create(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới Brand thành công!");
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

        #region["Sửa Brand"]
        [HttpGet]
        public ActionResult Update(string brandcode)
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
            var ischild = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            if (!CUtils.IsNullOrEmpty(brandcode))
            {
                var objMst_Brand = new ProductCentrer.Mst_Brand()
                {
                    
                    BrandCode = brandcode,
                };
                var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
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
                    Ft_WhereClause = strWhereClauseMst_Brand,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Brand
                    Rt_Cols_Mst_Brand = "*",
                    Mst_Brand = null,
                };

                var objRT_Mst_BrandCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);
                if (objRT_Mst_BrandCur.Lst_Mst_Brand != null && objRT_Mst_BrandCur.Lst_Mst_Brand.Count > 0)
                {
                    var orgbrand = objRT_Mst_BrandCur.Lst_Mst_Brand[0].OrgID;
                    if (isparent)
                    {
                        if (waOrgID.ToString() != orgbrand.ToString())
                        {
                            ischild = true;
                        }
                    }

                    ViewBag.IsChild = ischild;
                    return View(objRT_Mst_BrandCur.Lst_Mst_Brand[0]);
                }
            }

            ViewBag.IsChild = ischild;
            return View(new ProductCentrer.Mst_Brand());
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
                    var objMst_BrandInput = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_Brand>(model);
                    var objMst_Brand = new ProductCentrer.Mst_Brand()
                    {
                        BrandCode = objMst_BrandInput.BrandCode,
                        OrgID = objMst_BrandInput.OrgID
                    };

                    var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                    var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
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
                        Ft_WhereClause = strWhereClauseMst_Brand,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Brand
                        Rt_Cols_Mst_Brand = "*",
                        Mst_Brand = null,
                    };

                    var objRT_Mst_BrandCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);
                    if (objRT_Mst_BrandCur.Lst_Mst_Brand != null && objRT_Mst_BrandCur.Lst_Mst_Brand.Count > 0)
                    {
                        var orgID = objRT_Mst_BrandCur.Lst_Mst_Brand[0].OrgID;

                        objRT_Mst_BrandCur.Lst_Mst_Brand[0].BrandName = objMst_BrandInput.BrandName;
                        objRT_Mst_BrandCur.Lst_Mst_Brand[0].FlagActive = objMst_BrandInput.FlagActive;
                        objRT_Mst_BrandCur.Lst_Mst_Brand[0].NetworkBrandCode = objMst_BrandInput.NetworkBrandCode;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
                        
                        if (isparent && !waOrgID.Equals(orgID))
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Brand, TblMst_Brand.NetworkBrandCode);
                        }
                        else
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Brand, TblMst_Brand.BrandName);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Brand, TblMst_Brand.FlagActive);
                        }

                        objRQ_Mst_Brand.Ft_WhereClause = null;
                        objRQ_Mst_Brand.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_Brand.Rt_Cols_Mst_Brand = null;
                        objRQ_Mst_Brand.Mst_Brand = objRT_Mst_BrandCur.Lst_Mst_Brand[0];

                        OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Update(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa Brand thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Brand '" + objMst_BrandInput.BrandCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Brand trống!");
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
        public ActionResult Detail(string brandcode)
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

            if (!CUtils.IsNullOrEmpty(brandcode))
            {
                var objMst_Brand = new ProductCentrer.Mst_Brand()
                {
                    BrandCode = brandcode,
                };

                var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
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
                    Ft_WhereClause = strWhereClauseMst_Brand,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Brand
                    Rt_Cols_Mst_Brand = "*",
                    Mst_Brand = null,
                };

                var objRT_Mst_BrandCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);
                if (objRT_Mst_BrandCur.Lst_Mst_Brand != null && objRT_Mst_BrandCur.Lst_Mst_Brand.Count > 0)
                {
                    return View(objRT_Mst_BrandCur.Lst_Mst_Brand[0]);
                }
            }
            return View(new ProductCentrer.Mst_Brand());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string brandcode, string orgID)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(brandcode))
                {
                    var objMst_Brand = new ProductCentrer.Mst_Brand()
                    {
                        BrandCode = brandcode,
                        OrgID = orgID
                    };

                    var strWhereClauseMst_Brand = strWhereClause_Mst_Brand(objMst_Brand);

                    var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
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
                        Ft_WhereClause = strWhereClauseMst_Brand,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_Brand
                        Rt_Cols_Mst_Brand = "*",
                        Mst_Brand = null,
                    };

                    var objRT_Mst_BrandCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);
                    if (objRT_Mst_BrandCur.Lst_Mst_Brand != null && objRT_Mst_BrandCur.Lst_Mst_Brand.Count > 0)
                    {
                        objRQ_Mst_Brand.Mst_Brand = objRT_Mst_BrandCur.Lst_Mst_Brand[0];

                        OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Delete(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa Brand thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã Brand '" + brandcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã Brand trống!");
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
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID.ToString();
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
                var listMst_Brand = new List<ProductCentrer.Mst_Brand>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 2)
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

                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Brand.OrgID]))
                            //{
                            //    exitsData = "Mã Tổ chức không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Brand.BrandCode]))
                            {
                                exitsData = "Mã Brand không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Brand.BrandName]))
                            {
                                exitsData = "Tên Brand không được trống!" + strRows;
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
                            var brandCodeCur = table.Rows[i][TblMst_Brand.BrandCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _brandCodeCur = table.Rows[j][TblMst_Brand.BrandCode].ToString().Trim();
                                    if (brandCodeCur.Equals(_brandCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã Brand '" + brandCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Brand = DataTableCmUtils.ToListof<ProductCentrer.Mst_Brand>(table);
                    // Gọi hàm save data
                    if (listMst_Brand != null && listMst_Brand.Count > 0)
                    {

                        foreach (var item in listMst_Brand)
                        {
                            item.OrgID = orgID;
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand
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
                                // RQ_Mst_Brand
                                Rt_Cols_Mst_Brand = null,
                                Mst_Brand = item,
                            };
                            OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Create(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

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
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID.ToString();
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
                var listMst_Brand = new List<ProductCentrer.Mst_Brand>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Brand.OrgID]))
                            {
                                exitsData = "Mã Tổ chức không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var orgId = CUtils.StrValueOrNull(dr[TblMst_Brand.OrgID]);
                                if (orgId.Equals(waNetworkID))
                                {
                                    exitsData = "Không được mapping mã brand chung cho đơn vị tổng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Brand.BrandCode]))
                            {
                                exitsData = "Mã Brand không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Brand.NetworkBrandCode]))
                            //{
                            //    exitsData = "Mã Brand chung không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            idx++;
                        }
                        #endregion
                    }
                    listMst_Brand = DataTableCmUtils.ToListof<ProductCentrer.Mst_Brand>(table);
                    // Gọi hàm save data
                    if (listMst_Brand != null && listMst_Brand.Count > 0)
                    {
                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Brand, TblMst_Brand.BrandCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Brand, TblMst_Brand.OrgID);
                        if (isparent)
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Brand, TblMst_Brand.NetworkBrandCode);
                        }
                        foreach (var item in listMst_Brand)
                        {
                            var objOS_PrdCenter_Mst_Brand = new ProductCentrer.Mst_Brand()
                            {
                                BrandCode = item.BrandCode,
                                OrgID = item.OrgID,
                                NetworkBrandCode = item.NetworkBrandCode,
                            };

                            var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
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
                                // RQ_Mst_Brand
                                Rt_Cols_Mst_Brand = null,
                                Mst_Brand = objOS_PrdCenter_Mst_Brand,
                            };

                            OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Update(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<ProductCentrer.Mst_Brand>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Brand).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Brand"));

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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<ProductCentrer.Mst_Brand>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateMapping();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Brand).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Brand"));

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
            var listMst_Brand = new List<ProductCentrer.Mst_Brand>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                //var objOS_PrdCenter_Mst_Brand = new OS_PrdCenter_Mst_Brand()
                //{
                //    BrandCode = "",
                //    BrandName = "",
                //    FlagActive = "",
                //};

                //var strWhereClauseOS_PrdCenter_Mst_Brand = strWhereClause_Mst_Brand(objOS_PrdCenter_Mst_Brand);

                var strWhereClauseOS_PrdCenter_Mst_Brand = "";

                var objRQ_Mst_Brand = new ProductCentrer.RQ_Mst_Brand()
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
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_Brand,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Brand
                    Rt_Cols_Mst_Brand = "*",
                    Mst_Brand = null,
                };

                var objRT_Mst_BrandCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);

                if (objRT_Mst_BrandCur != null && objRT_Mst_BrandCur.Lst_Mst_Brand != null)
                {
                    if (objRT_Mst_BrandCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_BrandCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_BrandCur.Lst_Mst_Brand != null && objRT_Mst_BrandCur.Lst_Mst_Brand.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Brand.AddRange(objRT_Mst_BrandCur.Lst_Mst_Brand);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_Brand).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Brand, dicColNames, filePath, string.Format("Mst_Brand"));


                    #region["Export các trang còn lại"]
                    listMst_Brand.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Brand.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_BrandExportCur = OS_PrdCenter_Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs_ProductCentrer);
                            if (objRT_Mst_BrandExportCur != null && objRT_Mst_BrandExportCur.Lst_Mst_Brand != null && objRT_Mst_BrandExportCur.Lst_Mst_Brand.Count > 0)
                            {
                                listMst_Brand.AddRange(objRT_Mst_BrandExportCur.Lst_Mst_Brand);
                                ExcelExport.ExportToExcelFromList(listMst_Brand, dicColNames, filePath, string.Format("Mst_Brand_" + i));
                                listMst_Brand.Clear();
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
                //{TblMst_Brand.OrgID,"Mã tổ chức (*)"},
                {TblMst_Brand.BrandCode,"Mã Brand (*)"},
                {TblMst_Brand.BrandName,"Tên Brand (*)"},
                //{TblMst_CustomerNNTType.Remark,"Mô tả"},
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplateMapping()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Brand.OrgID,"Mã tổ chức (*)"},
                {TblMst_Brand.BrandCode,"Mã Brand (*)"},
                {TblMst_Brand.NetworkBrandCode,"Mã Brand chung"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            var userState = this.UserState;
            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {TblMst_Brand.OrgID,"Mã tổ chức (*)"},
                {TblMst_Brand.BrandCode,"Mã Brand (*)"},
            };
            if (waOrgID.Equals(waNetworkID))
            {
                dictionary.Add(TblMst_Brand.NetworkBrandCode, "Mã Brand chung");
            }
            dictionary.Add(TblMst_Brand.BrandName, "Tên Brand (*)");
            dictionary.Add(TblMst_Brand.FlagActive, "Trạng thái");
            return dictionary;
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_Brand(ProductCentrer.Mst_Brand data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
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