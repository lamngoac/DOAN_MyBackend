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
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class MstBrandController : AdminBaseController
    {
        // GET: MstBrand
        public ActionResult Index(string init = "init", int page = 0, int recordcount = 10)
        {
            GetUrlProductSolution();
            var itemCount = 0;
            var pageCount = 0;
            var addressAPIs = UserState.AddressAPIs;
            var userState = this.UserState;
            var strWhereClause_Mst_Brand = strWhereClauseMst_Brand(new Mst_Brand()
            {
                //FlagActive = "1",
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
            });
            var objRQ_Mst_Brand = new RQ_Mst_Brand()
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
                Ft_WhereClause = strWhereClause_Mst_Brand,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_Brand = "*",
                Mst_Brand = null,
            };
            var objRT_Mst_Brand = Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs);

            var pageInfo = new PageInfo<Mst_Brand>(0, recordcount)
            {
                DataList = new List<Mst_Brand>()
            };
            var lstMstBrand = new List<Mst_Brand>();
            if (objRT_Mst_Brand != null && objRT_Mst_Brand.Lst_Mst_Brand.Count != 0)
            {
                lstMstBrand = objRT_Mst_Brand.Lst_Mst_Brand;
            }
            pageInfo.DataList = lstMstBrand;
            itemCount = objRT_Mst_Brand.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Brand.MySummaryTable.MyCount);
            pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            ViewBag.RecordCount = recordcount;
            ViewBag.Offset = 7;
            ViewBag.UserState = userState;
            ViewBag.PageCur = page;
            if (init == "search")
            {
                return JsonView("BindDataMstBrand", pageInfo);
            }
            else
            {
                return View(pageInfo);
            }
        }

        public ActionResult Save(string BrandCode, string BrandName, string FlagActive)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
                var objMst_Brand = new Mst_Brand()
                {
                    BrandCode = BrandCode,
                    BrandName = BrandName,
                    FlagActive = FlagActive,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                };

                var objRQ_Mst_Brand = new RQ_Mst_Brand()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                    GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_Cols_Upd = null,
                    Mst_Brand = objMst_Brand
                };
                Mst_BrandService.Instance.WA_Mst_Brand_Create(objRQ_Mst_Brand, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới nhãn hiệu thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        public ActionResult Update(string BrandCode, string BrandName, string FlagActive)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
                var objMst_Brand = new Mst_Brand()
                {
                    BrandCode = BrandCode,
                    BrandName = BrandName,
                    FlagActive = FlagActive,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                };

                var objRQ_Mst_Brand = new RQ_Mst_Brand()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                    GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    FuncType = null,
                    Ft_Cols_Upd = "Mst_Brand.BrandName, Mst_Brand.FlagActive",
                    Mst_Brand = objMst_Brand
                };
                Mst_BrandService.Instance.WA_Mst_Brand_Update(objRQ_Mst_Brand, addressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật nhãn hiệu thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        public ActionResult Detail(string brandCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var addressAPIs = UserState.AddressAPIs;
                var objMst_Brand = new Mst_Brand()
                {
                    BrandCode = brandCode,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                };
                var strWhereClause = strWhereClauseMst_Brand(objMst_Brand);
                var objRQ_Mst_Brand = new RQ_Mst_Brand()
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
                    Ft_Cols_Upd = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClause,
                    Rt_Cols_Mst_Brand = "*"
                };
                var rtMst_Brand = Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs);
                if (rtMst_Brand != null && rtMst_Brand.Lst_Mst_Brand != null && rtMst_Brand.Lst_Mst_Brand.Count > 0)
                {
                    objMst_Brand = rtMst_Brand.Lst_Mst_Brand[0];
                }
                return JsonView("ModalUpdate", objMst_Brand);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        public ActionResult DeleteMulti(string lstBrandCode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
            #endregion
            try
            {
                var listBrandCode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(lstBrandCode);
                if (listBrandCode == null || listBrandCode.Count == 0)
                {
                    resultEntry.Success = false;
                    resultEntry.AddMessage("Không tồn tại danh sách cần xóa !");
                    resultEntry.RedirectUrl = Url.Action("Index");
                    return Json(resultEntry);
                }
                foreach (var brandcode in listBrandCode)
                {
                    var objMst_Brand = new Mst_Brand()
                    {
                        BrandCode = brandcode,
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                    };
                    var objRQ_Mst_Brand = new RQ_Mst_Brand()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                        GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                        AccessToken = CUtils.StrValue(UserState.AccessToken),
                        WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                        WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        FuncType = null,
                        Ft_Cols_Upd = null,
                        Mst_Brand = objMst_Brand
                    };
                    Mst_BrandService.Instance.WA_Mst_Brand_Delete(objRQ_Mst_Brand, addressAPIs);
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa nhãn hiệu thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
                return Json(resultEntry);
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }
        private string strWhereClauseMst_Brand(Mst_Brand data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Brand = "Mst_Brand.";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "FlagActive", data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "OrgID", data.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "BrandCode", data.BrandCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.BrandName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + "BrandName", data.BrandName, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        public ActionResult Export()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressAPIs = UserState.AddressAPIs;
            var strWhereClause_Mst_Brand = strWhereClauseMst_Brand(new Mst_Brand()
            {
                //FlagActive = "1",
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
            });
            var objRQ_Mst_Brand = new RQ_Mst_Brand()
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
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause_Mst_Brand,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_Brand = "*",
                Mst_Brand = null,
            };
            try
            {
                var objRT_Mst_Brand = Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs);
                var lstMstBrand = new List<Mst_Brand>();
                if (objRT_Mst_Brand != null && objRT_Mst_Brand.Lst_Mst_Brand != null)
                {
                    lstMstBrand = objRT_Mst_Brand.Lst_Mst_Brand;
                }
                if (lstMstBrand != null && lstMstBrand.Count > 0)
                {
                    Dictionary<string, string> dicColNames = GetExportDicColums();

                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Brand).Name), ref url);
                    ExcelExport.ExportToExcelFromList(lstMstBrand, dicColNames, filePath, string.Format("MstBrand"));

                    return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = Nonsense.MESS_CHECK_FILE_NULL, CheckSuccess = "1" });
                }

            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return View(resultEntry);
        }

        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_Brand = new List<Mst_Brand>();
                Dictionary<string, string> dicColNames = GetImportDicColums();

                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Brand).Name), ref url);
                ExcelExport.ExportToExcelFromList(listMst_Brand, dicColNames, filePath, string.Format("Mst_Brand"));
                return Json(new { Success = true, Title = Nonsense.MESS_EXPORT_EXCEL_SUCCESS, CheckSuccess = "1", strUrl = url }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Messages }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = ServiceAddress.BaseAPIProductCenter;//CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            if (ModelState.IsValid)
            {
                try
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    if (ext != ".xlsx" && ext != ".xls")
                    {
                        resultEntry.Detail = Nonsense.MESS_CHECK_FILEIMPORT;
                        return Json(resultEntry);
                    }
                    if (ext.Equals(".xlsx") || ext.Equals(".xls"))
                    {
                        FileUtils.SaveTempFile(file, fileId, ext);
                    }
                    var lstMstBrand = new List<Mst_Brand>();
                    string filePath = FileUtils.GetTempFilePhysicalPath(fileId, ext);

                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        #region["Check null"]

                        foreach (DataRow dr in table.Rows)
                        {
                            if (dr["BrandCode"] == null || dr["BrandCode"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Mã nhãn hiệu không được để trống!";
                                resultEntry.AddDetail(exitsData);
                                return Json(resultEntry);
                            }

                            if (dr["BrandName"] == null || dr["BrandName"].ToString().Trim().Length == 0)
                            {
                                exitsData = "Tên nhãn hiệu không được để trống!";
                                resultEntry.AddDetail(exitsData);
                                return Json(resultEntry);
                            }
                        }
                        #endregion

                        var listImport = DataTableCmUtils.ToListof<Mst_Brand>(table);
                        if (listImport != null)
                        {
                            lstMstBrand = listImport;
                        }

                        foreach (var it in lstMstBrand)
                        {
                            var objMst_Brand = new Mst_Brand()
                            {
                                BrandCode = it.BrandCode,
                                BrandName = it.BrandName,
                                FlagActive = FlagActive,
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                            };

                            var objRQ_Mst_Brand = new RQ_Mst_Brand()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_MasterServer_GwUserCode,
                                GwPassword = OS_ProductCentrer_MasterServer_GwPassword,
                                AccessToken = CUtils.StrValue(UserState.AccessToken),
                                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                                FuncType = null,
                                Ft_Cols_Upd = null,
                                Mst_Brand = objMst_Brand
                            };
                            Mst_BrandService.Instance.WA_Mst_Brand_Create(objRQ_Mst_Brand, addressAPIs);
                        }
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Import nhãn hiệu thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        exitsData = Nonsense.MESS_CHECK_FILE_NULL;
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                }
                catch (ClientException ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
                catch (Exception ex)
                {
                    resultEntry.SetFailed().AddException(ex);
                }
            }
            return Json(resultEntry);
        }

        private Dictionary<string, string> GetExportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"BrandCode","Mã nhãn hiệu"},
                 {"BrandName","Tên nhãn hiệu"},
                 {"FlagActive","Trạng thái"}
            };
        }
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"BrandCode","Mã nhãn hiệu"},
                 {"BrandName","Tên nhãn hiệu"},
                 {"FlagActive","Trạng thái"}
            };
        }
    }
}