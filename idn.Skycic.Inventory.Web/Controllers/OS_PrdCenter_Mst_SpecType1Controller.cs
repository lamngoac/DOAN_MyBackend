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
    public class OS_PrdCenter_Mst_SpecType1Controller : AdminBaseController
    {
        // Loại sản phẩm
        // GET: OS_PrdCenter_Mst_SpecType1
        public ActionResult Index(string spectype1 = "", string spectype1name = "", string init = "init", int page = 0)
        {
            init = "search"; // Không làm tìm kiếm
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<ProductCentrer.Mst_SpecType1>(0, PageSizeConfig)
            {
                DataList = new List<ProductCentrer.Mst_SpecType1>()
            };
            var itemCount = 0;
            var pageCount = 0;
            if (init != "init")
            {
                #region["Search"]
                var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                {
                    SpecType1 = spectype1,
                    SpecType1Name = spectype1name,
                };

                var strWhereClauseOS_PrdCenter_Mst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
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
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_SpecType1,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecType1
                    Rt_Cols_Mst_SpecType1 = "*",
                    Mst_SpecType1 = null,
                };

                var objRT_Mst_SpecType1Cur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);

                if (objRT_Mst_SpecType1Cur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_SpecType1Cur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_SpecType1Cur != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            ViewBag.SpecType1 = spectype1;
            ViewBag.SpecType1Name = spectype1name;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới loại sản phẩm"]

        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
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
                var objMst_SpecType1Input = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_SpecType1>(model);
                //var title = "";
                var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1
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
                    // RQ_Mst_SpecType1
                    Rt_Cols_Mst_SpecType1 = null,
                    Mst_SpecType1 = objMst_SpecType1Input
                };
                OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Create(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới loại sản phẩm thành công!");
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

        #region["Sửa loại sản phẩm"]
        [HttpGet]
        public ActionResult Update(string spectype1)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(spectype1))
            {
                var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                {
                    SpecType1 = spectype1,
                };

                var strWhereClauseOS_PrdCenter_Mst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
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
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_SpecType1,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecType1
                    Rt_Cols_Mst_SpecType1 = "*",
                    Mst_SpecType1 = null,
                };

                var objRT_Mst_SpecType1Cur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1.Count > 0)
                {
                    return View(objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0]);
                }
            }
            return View(new ProductCentrer.Mst_SpecType1());
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
                    var objMst_SpecType1Input = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Mst_SpecType1>(model);
                    var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                    {
                        SpecType1 = objMst_SpecType1Input.SpecType1,
                    };

                    var strWhereClauseOS_PrdCenter_Mst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                    var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
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
                        Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_SpecType1,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_SpecType1
                        Rt_Cols_Mst_SpecType1 = "*",
                        Mst_SpecType1 = null,
                    };

                    var objRT_Mst_SpecType1Cur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1.Count > 0)
                    {
                        objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0].SpecType1Name = objMst_SpecType1Input.SpecType1Name;
                        //objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0].Remark = objMst_SpecType1Input.Remark;
                        objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0].FlagActive = objMst_SpecType1Input.FlagActive;

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_SpecType1 = TableName.Mst_SpecType1 + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecType1, TblMst_SpecType1.SpecType1Name);
                        //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecType1, TblMst_SpecType1.Remark);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_SpecType1, TblMst_SpecType1.FlagActive);

                        objRQ_Mst_SpecType1.Ft_WhereClause = null;
                        objRQ_Mst_SpecType1.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_SpecType1.Rt_Cols_Mst_SpecType1 = null;
                        objRQ_Mst_SpecType1.Mst_SpecType1 = objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0];

                        OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Update(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa loại sản phẩm thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã loại sản phẩm '" + objMst_SpecType1Input.SpecType1 + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã loại sản phẩm trống!");
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

        #region["Chi tiết - Xóa loại sản phẩm"]
        [HttpGet]
        public ActionResult Detail(string spectype1)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(spectype1))
            {
                var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                {
                    SpecType1 = spectype1,
                };

                var strWhereClauseOS_PrdCenter_Mst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
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
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_SpecType1,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecType1
                    Rt_Cols_Mst_SpecType1 = "*",
                    Mst_SpecType1 = null,
                };

                var objRT_Mst_SpecType1Cur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);
                if (objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1.Count > 0)
                {
                    return View(objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0]);
                }
            }
            return View(new ProductCentrer.Mst_SpecType1());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string spectype1)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            try
            {
                if (!CUtils.IsNullOrEmpty(spectype1))
                {
                    var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                    {
                        SpecType1 = spectype1,
                    };

                    var strWhereClauseOS_PrdCenter_Mst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                    var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
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
                        Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_SpecType1,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_SpecType1
                        Rt_Cols_Mst_SpecType1 = "*",
                        Mst_SpecType1 = null,
                    };

                    var objRT_Mst_SpecType1Cur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);
                    if (objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1.Count > 0)
                    {
                        objRQ_Mst_SpecType1.Mst_SpecType1 = objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1[0];

                        OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Delete(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa loại sản phẩm thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã loại sản phẩm '" + spectype1 + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã loại sản phẩm trống!");
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
                var listMst_SpecType1 = new List<ProductCentrer.Mst_SpecType1>();
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecType1.SpecType1]))
                            {
                                exitsData = "Mã loại sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_SpecType1.SpecType1Name]))
                            {
                                exitsData = "Tên loại sản phẩm không được trống!" + strRows;
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
                            var specType1Cur = table.Rows[i][TblMst_SpecType1.SpecType1].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _specType1Cur = table.Rows[j][TblMst_SpecType1.SpecType1].ToString().Trim();
                                    if (specType1Cur.Equals(_specType1Cur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã loại sản phẩm '" + specType1Cur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_SpecType1 = DataTableCmUtils.ToListof<ProductCentrer.Mst_SpecType1>(table);
                    // Gọi hàm save data
                    if (listMst_SpecType1 != null && listMst_SpecType1.Count > 0)
                    {

                        foreach (var item in listMst_SpecType1)
                        {
                            item.OrgID = orgID;
                            //item.FlagActive = FlagActive;
                            var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1
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
                                // RQ_Mst_SpecType1
                                Rt_Cols_Mst_SpecType1 = null,
                                Mst_SpecType1 = item,
                            };
                            OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Create(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);

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
            var list = new List<ProductCentrer.Mst_SpecType1>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_SpecType1).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_SpecType1"));

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
        //public ActionResult Export(string spectype1 = "", string spectype1name = "", string init = "init", int page = 0)
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_SpecType1 = new List<ProductCentrer.Mst_SpecType1>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]

                //var objMst_SpecType1 = new ProductCentrer.Mst_SpecType1()
                //{
                //    SpecType1 = spectype1,
                //    SpecType1Name = spectype1name,
                //    FlagActive = "";
                //};

                //var strWhereClauseOS_PrdCenter_Mst_SpecType1 = strWhereClause_Mst_SpecType1(objMst_SpecType1);

                var strWhereClauseOS_PrdCenter_Mst_SpecType1 = "";

                var objRQ_Mst_SpecType1 = new ProductCentrer.RQ_Mst_SpecType1()
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
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_SpecType1,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_SpecType1
                    Rt_Cols_Mst_SpecType1 = "*",
                    Mst_SpecType1 = null,
                };

                var objRT_Mst_SpecType1Cur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);

                if (objRT_Mst_SpecType1Cur != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null)
                {
                    if (objRT_Mst_SpecType1Cur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_SpecType1Cur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_SpecType1.AddRange(objRT_Mst_SpecType1Cur.Lst_Mst_SpecType1);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Mst_SpecType1).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_SpecType1, dicColNames, filePath, string.Format("Mst_SpecType1"));


                    #region["Export các trang còn lại"]
                    listMst_SpecType1.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_SpecType1.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_SpecType1ExportCur = OS_PrdCenter_Mst_SpecType1Service.Instance.WA_Mst_SpecType1_Get(objRQ_Mst_SpecType1, addressAPIs_ProductCentrer);
                            if (objRT_Mst_SpecType1ExportCur != null && objRT_Mst_SpecType1ExportCur.Lst_Mst_SpecType1 != null && objRT_Mst_SpecType1ExportCur.Lst_Mst_SpecType1.Count > 0)
                            {
                                listMst_SpecType1.AddRange(objRT_Mst_SpecType1ExportCur.Lst_Mst_SpecType1);
                                ExcelExport.ExportToExcelFromList(listMst_SpecType1, dicColNames, filePath, string.Format("Mst_SpecType1_" + i));
                                listMst_SpecType1.Clear();
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
                {TblMst_SpecType1.SpecType1,"Mã loại sản phẩm (*)"},
                {TblMst_SpecType1.SpecType1Name,"Tên loại sản phẩm (*)"},
                //{TblMst_SpecType1.Remark,"Mô tả"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_SpecType1.OrgID,"Mã tổ chức (*)"},
                {TblMst_SpecType1.SpecType1,"Mã loại sản phẩm (*)"},
                {TblMst_SpecType1.SpecType1Name,"Tên loại sản phẩm (*)"},
                //{TblMst_SpecType1.Remark,"Mô tả"},
                {TblMst_SpecType1.FlagActive,"Trạng thái"},
            };
        }
        #endregion

        #region["strWhereClause"]
        private string strWhereClause_Mst_SpecType1(ProductCentrer.Mst_SpecType1 data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_SpecType1 = TableName.Mst_SpecType1 + ".";
            if (!CUtils.IsNullOrEmpty(data.SpecType1))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.SpecType1, CUtils.StrValueOrNull(data.SpecType1), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecType1Name))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.SpecType1Name, "%" + CUtils.StrValueOrNull(data.SpecType1Name) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_SpecType1 + TblMst_SpecType1.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}