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
    public class Mst_DealerController : AdminBaseController
    {
        // GET: Mst_Dealer
        public ActionResult Index(string init = "init", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var pageInfo = new PageInfo<Mst_Dealer>(0, PageSizeConfig)
            {
                DataList = new List<Mst_Dealer>()
            };
            var itemCount = 0;
            var pageCount = 0;
            // (không có nút tìm kiếm => load data luôn)
            init = String.Format("{0}", "search");
            if (init != "init")
            {
                #region["Search"]
                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };

                var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);

                if (objRT_Mst_DealerCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_DealerCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_DealerCur != null && objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_DealerCur.Lst_Mst_Dealer);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "init";
            }

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region ["Tạo mới"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region["Kho"]
            var objMst_Inventory = new Mst_Inventory()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClauseMst_Inventory,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_Inventory = "*"
            };
            var objRTMst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            
            ViewBag.ListMst_Inventory = objRTMst_Inventory.Lst_Mst_Inventory;
            #endregion

            #region ["Nhóm sản phẩm"]
            var objMst_PartMaterialType = new Mst_PartMaterialType()
            {
                FlagActive = FlagActive
            };
            var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
            var listMst_PartMaterialType = List_Mst_PartMaterialType(strWhereClauseMst_PartMaterialType);
            ViewBag.ListMst_PartMaterialType = listMst_PartMaterialType;
            #endregion

            #region ["Mã đơn vị cha"]
            var objMst_DealerParent = new Mst_Dealer()
            {
                FlagActive = FlagActive,
                DLLevel = "1",
            };
            var strWhereClauseMst_DealerParent = strWhereClause_Mst_Dealer(objMst_DealerParent);
            var objRQ_Mst_DealerParent = new RQ_Mst_Dealer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                Ft_WhereClause = strWhereClauseMst_DealerParent,
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };
            var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_DealerParent, addressAPIs);
            ViewBag.ListMst_Dealer = objRT_Mst_Dealer.Lst_Mst_Dealer;
            #endregion

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
                var objMst_DealerInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Dealer>(model);
                //var title = "";
                var objRQ_Mst_Dealer = new RQ_Mst_Dealer
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
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = null,
                    Mst_Dealer = objMst_DealerInput
                };
                Mst_DealerService.Instance.WA_Mst_Dealer_Create(objRQ_Mst_Dealer, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới đơn vị thành công!");
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
        public ActionResult Update(string dlcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(dlcode))
            {

                #region["Kho"]
                var objMst_Inventory = new Mst_Inventory()
                {
                    FlagActive = FlagActive,
                };
                var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Inventory,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_Inventory = "*"
                };
                var objRTMst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);

                ViewBag.ListMst_Inventory = objRTMst_Inventory.Lst_Mst_Inventory;
                #endregion

                #region ["Nhóm sản phẩm"]
                var objMst_PartMaterialType = new Mst_PartMaterialType()
                {
                    FlagActive = FlagActive
                };
                var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
                var listMst_PartMaterialType = List_Mst_PartMaterialType(strWhereClauseMst_PartMaterialType);
                ViewBag.ListMst_PartMaterialType = listMst_PartMaterialType;
                #endregion

                #region ["Mã đơn vị cha"]
                var objMst_DealerParent = new Mst_Dealer()
                {
                    FlagActive = FlagActive,
                    DLLevel = "1",
                };
                var strWhereClauseMst_DealerParent = strWhereClause_Mst_Dealer(objMst_DealerParent);
                var objRQ_Mst_DealerParent = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClauseMst_DealerParent,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };
                var listMst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_DealerParent, addressAPIs);
                ViewBag.ListMst_Dealer = listMst_Dealer;
                #endregion

                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_Dealer(new Mst_Dealer() { DLCode = dlcode.Trim().ToString() });

                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };
                var objRT_Mst_Dealer_Cur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                if (objRT_Mst_Dealer_Cur != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer.Count > 0)
                {                             
                    return View(objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0]);
                }
            }

            return View(new Mst_Dealer());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_DealerInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Dealer>(model);

                    var objMst_Dealer = new Mst_Dealer()
                    {
                        DLCode = objMst_DealerInput.DLCode,
                    };

                    var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(objMst_Dealer);

                    var objRQ_Mst_Dealer = new RQ_Mst_Dealer
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Dealer,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_CustomerNNT
                        Rt_Cols_Mst_Dealer = "*",
                        Mst_Dealer = null,
                    };
                    var objRT_Mst_Dealer_Cur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                    if (objRT_Mst_Dealer_Cur.Lst_Mst_Dealer != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer.Count > 0)
                    {
                        objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0].DLName = objMst_DealerInput.DLName;
                        objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0].Remark = objMst_DealerInput.Remark;
                        objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0].FlagActive = objMst_DealerInput.FlagActive;

                        #region ["strFt_Cols_Upd"]

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.DLName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.Remark);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Dealer, TblMst_Dealer.FlagActive);

                        #endregion

                        objRQ_Mst_Dealer.Ft_WhereClause = null;
                        objRQ_Mst_Dealer.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_Dealer.Rt_Cols_Mst_Dealer = null;
                        objRQ_Mst_Dealer.Mst_Dealer = objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0];

                        Mst_DealerService.Instance.WA_Mst_Dealer_Update(objRQ_Mst_Dealer, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa đơn vị thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                        return Json(resultEntry);
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đơn vị '" + objMst_DealerInput.DLCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đơn vị trống!");
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

        #region ["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string dlcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(dlcode))
            {
                #region["Kho"]
                var objMst_Inventory = new Mst_Inventory()
                {
                    FlagActive = FlagActive,
                };
                var strWhereClauseMst_Inventory = strWhereClause_Mst_Inventory(objMst_Inventory);
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Inventory,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_District
                    Rt_Cols_Mst_Inventory = "*"
                };
                var objRTMst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);

                ViewBag.ListMst_Inventory = objRTMst_Inventory.Lst_Mst_Inventory;
                #endregion

                #region ["Nhóm sản phẩm"]
                var objMst_PartMaterialType = new Mst_PartMaterialType()
                {
                    FlagActive = FlagActive
                };
                var strWhereClauseMst_PartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
                var listMst_PartMaterialType = List_Mst_PartMaterialType(strWhereClauseMst_PartMaterialType);
                ViewBag.ListMst_PartMaterialType = listMst_PartMaterialType;
                #endregion

                #region ["Mã đơn vị cha"]
                var objMst_DealerParent = new Mst_Dealer()
                {
                    FlagActive = FlagActive,
                    DLLevel = "1",
                };
                var strWhereClauseMst_DealerParent = strWhereClause_Mst_Dealer(objMst_DealerParent);
                var objRQ_Mst_DealerParent = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClauseMst_DealerParent,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };
                var listMst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_DealerParent, addressAPIs);
                ViewBag.ListMst_Dealer = listMst_Dealer;
                #endregion

                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_Dealer(new Mst_Dealer() { DLCode = dlcode.Trim().ToString() });

                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };
                var objRT_Mst_Dealer_Cur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                if (objRT_Mst_Dealer_Cur != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer.Count > 0)
                {
                    return View(objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0]);
                }
            }

            return View(new Mst_Dealer());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string dlcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                if (!CUtils.IsNullOrEmpty(dlcode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_Mst_Dealer(new Mst_Dealer() { DLCode = dlcode.Trim().ToString() });

                    var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        Ft_WhereClause = strWhereClause,
                        // RQ_Mst_Dealer
                        Rt_Cols_Mst_Dealer = "*",
                        Mst_Dealer = null,
                    };
                    var objRT_Mst_Dealer_Cur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                    if (objRT_Mst_Dealer_Cur != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer != null && objRT_Mst_Dealer_Cur.Lst_Mst_Dealer.Count > 0)
                    {
                        objRQ_Mst_Dealer.Mst_Dealer = objRT_Mst_Dealer_Cur.Lst_Mst_Dealer[0];

                        Mst_DealerService.Instance.WA_Mst_Dealer_Delete(objRQ_Mst_Dealer, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa đơn vị thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đơn vị '" + dlcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đơn vị trống!");
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


        #region ["strWhereClause"]
        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.ProvinceCode, CUtils.StrValueOrNull(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Inventory(Mst_Inventory data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Inventory = TableName.Mst_Inventory + ".";
            if (!CUtils.IsNullOrEmpty(data.InvCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.InvCode, CUtils.StrValueOrNull(data.InvCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Inventory + TblMst_Inventory.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_PartMaterialType(Mst_PartMaterialType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartMaterialType = TableName.Mst_PartMaterialType + ".";
            if (!CUtils.IsNullOrEmpty(data.PMType))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.PMType, CUtils.StrValueOrNull(data.PMType), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Dealer(Mst_Dealer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
            if (!CUtils.IsNullOrEmpty(data.DLCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLCode, CUtils.StrValueOrNull(data.DLCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DLCodeParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLCodeParent, CUtils.StrValueOrNull(data.DLCodeParent), "=");
            }
            //if (!CUtils.IsNullOrEmpty(data.InvCode))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.InvCode, CUtils.StrValueOrNull(data.InvCode), "=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.PMType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.PMType, CUtils.StrValueOrNull(data.PMType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DLLevel))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.DLLevel, CUtils.StrValueOrNull(data.DLLevel), "=");
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
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, data.ProvinceCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
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
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

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
                var listMst_Dealer = new List<Mst_Dealer>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 6)
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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLCode]))
                            {
                                exitsData = "Mã đơn vị không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.DLName]))
                            {
                                exitsData = "Tên đơn vị không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }                            
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.InvCode]))
                            {
                                exitsData = "Mã kho không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Dealer.PMType]))
                            {
                                exitsData = "Nhóm sản phẩm không được trống!" + strRows;
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
                            var Mst_DealerCur = table.Rows[i][TblMst_Dealer.DLCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_DealerCur = table.Rows[j][TblMst_Dealer.DLCode].ToString().Trim();
                                    if (Mst_DealerCur.Equals(_Mst_DealerCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã đơn vị '" + Mst_DealerCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Dealer = DataTableCmUtils.ToListof<Mst_Dealer>(table);
                    // Gọi hàm save data
                    if (listMst_Dealer != null && listMst_Dealer.Count > 0)
                    {
                        foreach (var item in listMst_Dealer)
                        {
                            var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
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
                                // RQ_Mst_Dealer
                                Rt_Cols_Mst_Dealer = null,
                                Mst_Dealer = item,
                            };

                            Mst_DealerService.Instance.WA_Mst_Dealer_Create(objRQ_Mst_Dealer, addressAPIs);
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
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_Dealer>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Dealer).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Dealer"));

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
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Dealer = new List<Mst_Dealer>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Dealer
                    Rt_Cols_Mst_Dealer = "*",
                    Mst_Dealer = null,
                };

                var objRT_Mst_DealerCur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                if (objRT_Mst_DealerCur != null && objRT_Mst_DealerCur.Lst_Mst_Dealer != null)
                {
                    if (objRT_Mst_DealerCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_DealerCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_DealerCur.Lst_Mst_Dealer != null && objRT_Mst_DealerCur.Lst_Mst_Dealer.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Dealer.AddRange(objRT_Mst_DealerCur.Lst_Mst_Dealer);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Dealer).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Dealer, dicColNames, filePath, string.Format("Mst_Dealer"));


                    #region["Export các trang còn lại"]
                    listMst_Dealer.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Dealer.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_DealerExportCur = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
                            if (objRT_Mst_DealerExportCur != null && objRT_Mst_DealerExportCur.Lst_Mst_Dealer != null && objRT_Mst_DealerExportCur.Lst_Mst_Dealer.Count > 0)
                            {
                                listMst_Dealer.AddRange(objRT_Mst_DealerExportCur.Lst_Mst_Dealer);
                                ExcelExport.ExportToExcelFromList(listMst_Dealer, dicColNames, filePath, string.Format("Mst_Dealer" + i));
                                listMst_Dealer.Clear();
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

        #region ["GetDicColumns"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Dealer.DLCode,"Mã đơn vị (*)"},
                {TblMst_Dealer.DLName,"Tên đơn vị (*)"},
                {TblMst_Dealer.DLCodeParent,"Mã đơn vị cha (*)"},
                {TblMst_Dealer.InvCode,"Mã kho (*)"},
                {TblMst_Dealer.PMType,"Nhóm sản phẩm (*)"},
                {TblMst_Dealer.Remark,"Ghi chú"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Dealer.DLCode,"Mã đơn vị (*)"},
                {TblMst_Dealer.DLName,"Tên đơn vị (*)"},
                {TblMst_Dealer.DLCodeParent,"Mã đơn vị cha (*)"},
                {TblMst_Dealer.InvCode,"Mã kho (*)"},
                {TblMst_Dealer.PMType,"Nhóm sản phẩm (*)"},
                {TblMst_Dealer.Remark,"Ghi chú"},
                {TblMst_Dealer.FlagActive,"Trạng thái"},
            };
        }
        #endregion
    }
}