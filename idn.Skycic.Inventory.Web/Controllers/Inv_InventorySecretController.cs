using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Utils;
using System.Text;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.ModelsUI;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Inv_InventorySecretController : AdminBaseController
    {
        // Quản lý cấp số in tem
        // GET: Inv_InventorySecret
        public ActionResult Index(string qty, string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.Offset = Offset;
            ViewBag.Qty = qty;
            var pageInfo = new PageInfo<Inv_InventorySecret>(0, recordcount)
            {
                DataList = new List<Inv_InventorySecret>()
            };
            var itemCount = 0;
            var pageCount = 0;
            var tongSoLuongConLai = 0.0;
            var totalQty = 0.0; // Tổng số lượng
            var totalQtyIssued = 0.0; // Tổng số lượng đã phát hành
            var totalQtyUsed = 0.0; // Tổng số lượng đã dùng
            #region ["Thông tin số lượng hóa đơn"]
            var listInvoice_license = WA_Invoice_license_Get_By_MST();
            if (listInvoice_license.Count != 0)
            {
                totalQty = Convert.ToDouble(listInvoice_license[0].TotalQty);
                totalQtyIssued = Convert.ToDouble(listInvoice_license[0].TotalQtyIssued); 
                totalQtyUsed = Convert.ToDouble(listInvoice_license[0].TotalQtyUsed);

                tongSoLuongConLai = totalQty - totalQtyIssued;
            }
            ViewBag.TotalQty = totalQty.ToString();
            ViewBag.TotalQtyIssued = totalQtyIssued.ToString();
            ViewBag.TotalQtyUsed = totalQtyUsed.ToString();
            ViewBag.TongSoLuongConLai = tongSoLuongConLai.ToString();
            #endregion
            if (init != "init")
            {
                #region["Search"]

                var iQty = Convert.ToInt32(qty);
                if (recordcount > iQty)
                {
                    recordcount = iQty;
                }
                // Tổng số trang
                var iPageCount = (iQty % recordcount == 0) ? iQty / recordcount : iQty / recordcount + 1;

                var objInv_InventorySecret = new Inv_InventorySecret()
                {
                    FlagUsed = FlagInActive, // 0-chưa dùng; 1-đã dùng
                };
                // Số bản ghi có thể truy vấn
                var _recordcount = recordcount;
                var iRecordCount = page * recordcount;
                if ((iQty - iRecordCount) > recordcount)
                {
                    _recordcount = recordcount;
                }
                else
                {
                    _recordcount = iQty - iRecordCount;
                }
                var strWhereClauseInv_InventorySecret = strWhereClause_Inv_InventorySecret(objInv_InventorySecret);

                var objRQ_Inv_InventorySecret = new RQ_Inv_InventorySecret()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = _recordcount.ToString(),
                    Ft_WhereClause = strWhereClauseInv_InventorySecret,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Inv_InventorySecret
                    Rt_Cols_Inv_InventorySecret = "*",
                };

                var objRT_Inv_InventorySecretCur = Inv_InventorySecretService.Instance.WA_Inv_InventorySecret_Get(objRQ_Inv_InventorySecret, addressAPI);
                if (objRT_Inv_InventorySecretCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Inv_InventorySecretCur.MySummaryTable.MyCount);
                    if (itemCount > iQty)
                    {
                        itemCount = iQty;
                    }
                }
                if (objRT_Inv_InventorySecretCur != null && objRT_Inv_InventorySecretCur.Lst_Inv_InventorySecret != null && objRT_Inv_InventorySecretCur.Lst_Inv_InventorySecret.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Inv_InventorySecretCur.Lst_Inv_InventorySecret);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;

                    if (pageCount > iPageCount)
                    {
                        pageCount = iPageCount;
                    }
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            
            return View(pageInfo);
        }

        #region["Export Excel"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(string qty)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listInv_InventorySecret_UpdateFlagUsed = new List<Inv_InventorySecret>();
            var listInv_InventorySecret = new List<Inv_InventorySecret>();
            var listInv_InventorySecretUI = new List<Inv_InventorySecretUI>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            var recordcount = RowsWorksheets;
            //var recordcount = 2;
            var page = 0;
            try
            {
                #region["Search"]
                var iQty = Convert.ToInt32(qty);
                if (recordcount > iQty)
                {
                    recordcount = iQty;
                }
                // Tổng số trang
                var iPageCount = (iQty % recordcount == 0) ? iQty / recordcount : iQty / recordcount + 1;

                var objInv_InventorySecret = new Inv_InventorySecret()
                {
                    FlagUsed = FlagInActive, // 0-chưa dùng; 1-đã dùng
                };
                // Số bản ghi có thể truy vấn
                var _recordcount = recordcount;
                var iRecordCount = page * recordcount;
                if ((iQty - iRecordCount) > recordcount)
                {
                    _recordcount = recordcount;
                }
                else
                {
                    _recordcount = iQty - iRecordCount;
                }
                var strWhereClauseInv_InventorySecret = strWhereClause_Inv_InventorySecret(objInv_InventorySecret);

                var objRQ_Inv_InventorySecret = new RQ_Inv_InventorySecret()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = _recordcount.ToString(),
                    Ft_WhereClause = strWhereClauseInv_InventorySecret,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Inv_InventorySecret
                    Rt_Cols_Inv_InventorySecret = "*",
                };

                var objRT_Inv_InventorySecretCur = Inv_InventorySecretService.Instance.WA_Inv_InventorySecret_Get(objRQ_Inv_InventorySecret, addressAPI);
                if (objRT_Inv_InventorySecretCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Inv_InventorySecretCur.MySummaryTable.MyCount);
                    if (itemCount > iQty)
                    {
                        itemCount = iQty;
                    }
                }
                if (objRT_Inv_InventorySecretCur != null && objRT_Inv_InventorySecretCur.Lst_Inv_InventorySecret != null && objRT_Inv_InventorySecretCur.Lst_Inv_InventorySecret.Count > 0)
                {
                    listInv_InventorySecret.AddRange(objRT_Inv_InventorySecretCur.Lst_Inv_InventorySecret);
                    listInv_InventorySecret_UpdateFlagUsed.AddRange(listInv_InventorySecret);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;

                    if (pageCount > iPageCount)
                    {
                        pageCount = iPageCount;
                    }

                    var startCount = page * recordcount;

                    #region["Update FlagUsed"]
                    var objRQ_Inv_InventorySecret_UpdateFlagUsed = new RQ_Inv_InventorySecret()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = null,
                        Ft_RecordCount = null,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Inv_InventorySecret
                        Rt_Cols_Inv_InventorySecret = null,
                        Lst_Inv_InventorySecret = new List<Inv_InventorySecret>(),
                    };
                    objRQ_Inv_InventorySecret_UpdateFlagUsed.Lst_Inv_InventorySecret.AddRange(listInv_InventorySecret);

                    Inv_InventorySecretService.Instance.WA_Inv_InventorySecret_UpdateFlagUsed(objRQ_Inv_InventorySecret_UpdateFlagUsed, addressAPI);
                    #endregion

                    var idx = 0;
                    foreach (var item in listInv_InventorySecret)
                    {
                        var iindex = startCount + (idx + 1);
                        var objInv_InventorySecretUI = new Inv_InventorySecretUI()
                        {
                            Idx = iindex.ToString(),
                            QR_SerialNo = CUtils.StrValue(item.QR_SerialNo),
                        };
                        listInv_InventorySecretUI.Add(objInv_InventorySecretUI);
                        idx++;
                    }
                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventorySecret).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listInv_InventorySecretUI, dicColNames, filePath, string.Format("Inv_InventorySecret"));

                    #region["Export các trang còn lại"]
                    listInv_InventorySecretUI.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Inv_InventorySecret.Ft_RecordStart = (i * recordcount).ToString();
                            _recordcount = recordcount;
                            iRecordCount = i * recordcount;
                            if ((iQty - iRecordCount) > recordcount)
                            {
                                _recordcount = recordcount;
                            }
                            else
                            {
                                _recordcount = iQty - iRecordCount;
                            }

                            objRQ_Inv_InventorySecret.Ft_RecordCount = _recordcount.ToString();
                            var objRT_Inv_InventorySecretExportCur = Inv_InventorySecretService.Instance.WA_Inv_InventorySecret_Get(objRQ_Inv_InventorySecret, addressAPI);
                            if (objRT_Inv_InventorySecretExportCur != null && objRT_Inv_InventorySecretExportCur.Lst_Inv_InventorySecret != null && objRT_Inv_InventorySecretExportCur.Lst_Inv_InventorySecret.Count > 0)
                            {
                                listInv_InventorySecret.Clear();
                                listInv_InventorySecret.AddRange(objRT_Inv_InventorySecretExportCur.Lst_Inv_InventorySecret);

                                #region["Update FlagUsed"]
                                objRQ_Inv_InventorySecret_UpdateFlagUsed.Tid = GetNextTId();
                                objRQ_Inv_InventorySecret_UpdateFlagUsed.Lst_Inv_InventorySecret = new List<Inv_InventorySecret>();
                                objRQ_Inv_InventorySecret_UpdateFlagUsed.Lst_Inv_InventorySecret.AddRange(listInv_InventorySecret);
                                Inv_InventorySecretService.Instance.WA_Inv_InventorySecret_UpdateFlagUsed(objRQ_Inv_InventorySecret_UpdateFlagUsed, addressAPI);
                                #endregion

                                startCount = i * recordcount;
                                var _idx = 0;
                                foreach (var item in listInv_InventorySecret)
                                {
                                    var iindex = startCount + (_idx + 1);
                                    var objInv_InventorySecretUI = new Inv_InventorySecretUI()
                                    {
                                        Idx = iindex.ToString(),
                                        QR_SerialNo = CUtils.StrValue(item.QR_SerialNo),
                                    };
                                    listInv_InventorySecretUI.Add(objInv_InventorySecretUI);
                                    _idx++;
                                }

                                ExcelExport.ExportToExcelFromList(listInv_InventorySecretUI, dicColNames, filePath, string.Format("Inv_InventorySecret_" + i));

                                listInv_InventorySecretUI.Clear();
                            }
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                }
                else
                {
                    return Json(new { Success = true, Title = "Dữ liệu trống!", CheckSuccess = "1" });
                }
                #endregion
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail, strUrl = url });
        }
        #endregion

        #region[""]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblInv_InventorySecret.Idx,"Số thứ tự"},
                {TblInv_InventorySecret.QR_SerialNo,"Số serials"},
            };
        }
        #endregion

        #region[""]

        private string strWhereClause_Inv_InventorySecret(Inv_InventorySecret data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventorySecret = TableName.Inv_InventorySecret + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagUsed))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventorySecret + TblInv_InventorySecret.FlagUsed, CUtils.StrValueOrNull(data.FlagUsed), "=");
            }
            
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}