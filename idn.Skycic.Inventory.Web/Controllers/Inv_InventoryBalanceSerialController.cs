using idn.Skycic.Inventory.Common.ModelsUI;
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
using System.Configuration;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Inv_InventoryBalanceSerialController : AdminBaseController
    {
        // GET: Inv_InventoryBalanceSerial
        public ActionResult Index(string agentcode = "", string partcode = "", string serialno = "", string canno = "",
            string boxno = "", string status = "", string pkdtfrom = "", string pkdtto = "", string odtfrom = "",
            string odtto = "", string init = "init", int page = 0, int recordcount = 10)
        {
            ViewBag.PageCur = page.ToString();

            //init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;

            ViewBag.AddressAPIs = UserState.AddressAPIs;
            ViewBag.Offset = Offset;
            var pkdttoCur = pkdtto;
            var odttoCur = odtto;

            var listDataCur = new List<Inv_InventoryBalanceSerial>();
            var pageInfo = new PageInfo<Inv_InventoryBalanceSerial>(0, recordcount)
            {
                DataList = new List<Inv_InventoryBalanceSerial>()
            };
            var itemCount = 0;
            var pageCount = 0;

            #region ["Mst_Part"]
            var objMst_Part = new Mst_Part()
            {
                FlagActive = FlagActive,
            };
            var strWhereClause_Mst_Part = strWhereClause_MstPart(objMst_Part);
            var objRQ_Mst_Part = new RQ_Mst_Part()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                Ft_WhereClause = strWhereClause_Mst_Part,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_Agent
                Rt_Cols_Mst_Part = "*",
            };
            var objRT_Mst_Part = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);

            ViewBag.ListMst_Part = objRT_Mst_Part.Lst_Mst_Part;
            #endregion

            #region ["Đại lý"]
            var objMst_NNT = new Mst_NNT()
            {
                FlagActive = FlagActive,
            };
            var strWhereClause_Mst_NNT = strWhereClause_MstNNT(objMst_NNT);
            var objRQ_Mst_NNT = new RQ_Mst_NNT()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                Ft_WhereClause = strWhereClause_Mst_NNT,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_Agent
                Rt_Cols_Mst_NNT = "*",
            };
            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            ViewBag.ListMst_NNT = objRT_Mst_NNT.Lst_Mst_NNT;
            #endregion

            if (init != "init")
            {
                var objInv_InventoryBalanceSerial = new Inv_InventoryBalanceSerialUI()
                {
                    MST = agentcode,
                    PartCode = partcode,
                    QR_SerialNo = serialno,
                    QR_CanNo = canno,
                    QR_BoxNo = boxno,
                    PackageDateFrom = pkdtfrom,
                    PackageDateTo = pkdtto,
                    OutDTimeFrom = odtfrom,
                    OutDTimeTo = odtto,
                    FlagSales = status,
                };
                var strWhereClause = strWhereClause_Inv_InventoryBalanceSerial(objInv_InventoryBalanceSerial);
                var objRQ_Inv_InventoryBalanceSerial = new RQ_Inv_InventoryBalanceSerial()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClause,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Agent
                    Rt_Cols_Inv_InventoryBalanceSerial = "*",
                };

                var objRT_Inv_InventoryBalanceSerial = Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(objRQ_Inv_InventoryBalanceSerial, addressAPIs);
                if (objRT_Inv_InventoryBalanceSerial.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Inv_InventoryBalanceSerial.MySummaryTable.MyCount);
                }
                if (objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
            }
            else
            {
                init = "search";
                pkdtfrom = DateToSearch();
                odtfrom = DateToSearch();
                pkdttoCur = pkdtfrom;
                odttoCur = pkdtfrom;
            }

            #region ["ViewBag"]
            ViewBag.AgentCode = agentcode;
            ViewBag.PartCode = partcode;
            ViewBag.SerialNo = serialno;
            ViewBag.CanNo = canno;
            ViewBag.BoxNo = boxno;
            ViewBag.Status = status;
            ViewBag.PackageDateFrom = pkdtfrom;
            ViewBag.PackageDateTo = pkdtto;
            ViewBag.OutDTimeFrom = odtfrom;
            ViewBag.OutDTimeTo = odtto;
            #endregion

            #region ["PageInfo"]
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            #endregion

            return View(pageInfo);
        }

        #region ["Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file, string invcode = "")
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                if (!CUtils.IsNullOrEmpty(invcode))
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                    var exitsData = "";
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
                    var list = new List<Inv_InventoryBalanceSerial>();
                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {

                        if (table.Columns.Count != 10)
                        {
                            exitsData = "File excel import không hợp lệ!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region ["Add columns"]
                            if (!table.Columns.Contains("InvCode"))
                            {
                                table.Columns.AddRange(new DataColumn[] { new DataColumn("InvCode", typeof(string)) });
                            }
                            if (!table.Columns.Contains("MST"))
                            {
                                table.Columns.AddRange(new DataColumn[] { new DataColumn("MST", typeof(string)) });
                            }
                            #endregion

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

                                var obj = new Inv_InventoryBalanceSerial();
                                var partCode = "";
                                var serialNo = "";
                                var packageDate = "";
                                var printDate = "";
                                var agentcode = "";
                                var partlotno = "";
                                var shiftincode = "";
                                var productiondate = "";
                                var canno = "";
                                var boxno = "";
                                var productUrl = ConfigurationManager.AppSettings["Product"].ToString();
                                var boxUrl = ConfigurationManager.AppSettings["Box"].ToString(); 
                                var canUrl = ConfigurationManager.AppSettings["Carton"].ToString();

                                #region ["SerialNo"]
                                if (CUtils.IsNullOrEmpty(dr["SerialNo"]))
                                {
                                    exitsData = "Cột SerialNo không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    dr["SerialNo"] = CUtils.StrReplaceUrl(dr["SerialNo"].ToString(), productUrl," ");
                                }
                                #endregion

                                #region ["PartCode"]
                                if (CUtils.IsNullOrEmpty(dr["PartCode"]))
                                {
                                    exitsData = "Mã sản phẩm không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    partCode = dr["PartCode"].ToString().Trim();
                                }
                                #endregion

                                #region ["CartonNo"]
                                if (!CUtils.IsNullOrEmpty(dr["CanNo"]))
                                {
                                    dr["CanNo"] = CUtils.StrReplaceUrl(dr["CanNo"].ToString(), canUrl, " ");
                                }
                                #endregion

                                #region ["BoxNo"]
                                if (!CUtils.IsNullOrEmpty(dr["BoxNo"]))
                                {
                                    dr["BoxNo"] = CUtils.StrReplaceUrl(dr["BoxNo"].ToString(), boxUrl, " ");
                                }
                                #endregion

                                if (!CUtils.IsNullOrEmpty(dr["PackageDate"]))
                                {
                                    packageDate = dr["PackageDate"].ToString().Trim();
                                    if (!CUtils.IsDateTime(packageDate))
                                    {
                                        exitsData = "Ngày đóng gói không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        packageDate = CUtils.FormatDate(packageDate, "yyyy-MM-dd");
                                    }
                                }
                                else
                                {
                                    packageDate = DateTime.Now.ToString("yyyy-MM-dd");
                                    dr["PackageDate"] = packageDate;
                                }


                                if (!CUtils.IsNullOrEmpty(dr["PrintDate"]))
                                {
                                    printDate = dr["PrintDate"].ToString().Trim();
                                    if (!CUtils.IsDateTime(printDate))
                                    {
                                        exitsData = "Ngày in không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        printDate = CUtils.FormatDate(printDate, "yyyy-MM-dd");
                                    }
                                }

                                if (!CUtils.IsNullOrEmpty(dr["ProductionDate"]))
                                {
                                    productiondate = dr["ProductionDate"].ToString().Trim();
                                    if (!CUtils.IsDateTime(productiondate))
                                    {
                                        exitsData = "Ngày sản xuất không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                    else
                                    {
                                        productiondate = CUtils.FormatDate(productiondate, "yyyy-MM-dd");
                                        dr["ProductionDate"] = productiondate;
                                    }

                                }

                                if (!CUtils.IsNullOrEmpty(dr["AgentCode"]))
                                {
                                    agentcode = dr["AgentCode"].ToString().Trim();
                                }

                                if (!CUtils.IsNullOrEmpty(dr["PartLotNo"]))
                                {
                                    partlotno = dr["PartLotNo"].ToString().Trim();
                                }

                                if (!CUtils.IsNullOrEmpty(dr["ShiftInCode"]))
                                {
                                    shiftincode = dr["ShiftInCode"].ToString().Trim();
                                }

                                dr["MST"] = userState.MST;
                                dr["InvCode"] = invcode;
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

                                var serialno = table.Rows[i]["SerialNo"].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    jRows = 2;
                                    jRows = (jRows + j + 1);
                                    if (i != j)
                                    {
                                        var _serialno = table.Rows[j]["SerialNo"].ToString().Trim();
                                        if (serialno == _serialno)
                                        {
                                            strRows += (" - " + jRows);
                                            exitsData = "SerialNo '" + serialno + "' bị lặp trong file excel ở dòng thứ" + strRows + "!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion
                            
                        }
                        list = DataTableCmUtils.ToListof<Inv_InventoryBalanceSerial>(table);

                        var objRQ_Inv_InventoryBalanceSerial = new RQ_Inv_InventoryBalanceSerial()
                        {
                            // WARQBase
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            FuncType = null,
                            Ft_RecordStart = Ft_RecordStartExportExcel,
                            Ft_RecordCount = RowsWorksheets.ToString(),
                            Ft_Cols_Upd = null,
                            Ft_WhereClause = null,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            UtcOffset = userState.UtcOffset,
                            // RQ_Mst_Agent
                            Rt_Cols_Inv_InventoryBalanceSerial = "*",
                            Lst_Inv_InventoryBalanceSerial = list,
                        };
                        Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Map(objRQ_Inv_InventoryBalanceSerial, addressAPIs);

                        resultEntry.Success = true;
                        exitsData = "Cập nhật thông tin xác thực thành công!";
                        resultEntry.AddMessage(exitsData);
                    }
                    else
                    {
                        exitsData = "File excel import không có dữ liệu!";
                        resultEntry.AddMessage(exitsData);
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã kho trống");
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
        public ActionResult Export(string agentcode = "", string partcode = "", string serialno = "", string canno = "",
            string boxno = "", string status = "", string pkdtfrom = "", string pkdtto = "", string odtfrom = "",
            string odtto = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs = userState.AddressAPIs;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                var listData = new List<Inv_InventoryBalanceSerial>();
                var itemCount = 0;
                var pageCount = 0;
                var url = "";

                #region["Search"]
                var objInv_InventoryBalanceSerial = new Inv_InventoryBalanceSerialUI()
                {
                    AgentCode = agentcode,
                    PartCode = partcode,
                    QR_SerialNo = serialno,
                    QR_CanNo = canno,
                    QR_BoxNo = boxno,
                    PackageDateFrom = pkdtfrom,
                    PackageDateTo = pkdtto,
                    OutDTimeFrom = odtfrom,
                    OutDTimeTo = odtto,
                    FlagSales = status,
                };
                var strWhereClause = strWhereClause_Inv_InventoryBalanceSerial(objInv_InventoryBalanceSerial);
                var objRQ_Inv_InventoryBalanceSerial = new RQ_Inv_InventoryBalanceSerial()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClause,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Agent
                    Rt_Cols_Inv_InventoryBalanceSerial = "*",
                };

                var objRT_Inv_InventoryBalanceSerial = Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(objRQ_Inv_InventoryBalanceSerial, addressAPIs);
                if (objRT_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial != null)
                {
                    if (objRT_Inv_InventoryBalanceSerial.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Inv_InventoryBalanceSerial.MySummaryTable.MyCount);
                    }
                    if (objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listData.AddRange(objRT_Inv_InventoryBalanceSerial.Lst_Inv_InventoryBalanceSerial);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBalanceSerial).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Inv_InventoryBalanceSerial"));


                    #region["Export các trang còn lại"]
                    listData.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Inv_InventoryBalanceSerial.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Inv_InventoryBalanceSerialCur = Inv_InventoryBalanceSerialService.Instance.WA_Inv_InventoryBalanceSerial_Get(objRQ_Inv_InventoryBalanceSerial, addressAPIs);
                            if (objRT_Inv_InventoryBalanceSerialCur != null && objRT_Inv_InventoryBalanceSerialCur.Lst_Inv_InventoryBalanceSerial != null && objRT_Inv_InventoryBalanceSerialCur.Lst_Inv_InventoryBalanceSerial.Count > 0)
                            {
                                listData.AddRange(objRT_Inv_InventoryBalanceSerialCur.Lst_Inv_InventoryBalanceSerial);
                                ExcelExport.ExportToExcelFromList(listData, dicColNames, filePath, string.Format("Inv_InventoryBalanceSerial_" + i));
                                listData.Clear();
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
            catch (Exception e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(new { Success = false, Detail = resultEntry.Detail });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var list = new List<Inv_InventoryBalanceSerial>();
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Inv_InventoryBalanceSerial).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Inv_InventoryBalanceSerial"));

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

        #region["Show import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowPopupImportExcel()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region ["Kho"]
                var objMst_Inventory = new Mst_Inventory()
                {
                    FlagActive = FlagActive,
                };
                var strWhereClause_Mst_Inventory = strWhereClause_MstInventory(objMst_Inventory);
                var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClause_Mst_Inventory,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Agent
                    Rt_Cols_Mst_Inventory = "*",
                };
                var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
                ViewBag.ListMst_Inventory = objRT_Mst_Inventory.Lst_Mst_Inventory;
                #endregion

                return JsonView("_ImportExcel", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_ImportExcel", null, resultEntry);
        }
        #endregion

        #region ["GetDicColumns"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"QR_SerialNo","SerialNo"},
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên sản phẩm"},
                 {"QR_BoxNo","Mã hộp"},
                 {"QR_CanNo","Mã thùng"},
                 {"AgentCode","Mã tổ chức"},
                 {"AgentName","Tên tổ chức"},
                 {"WarrantyDateStart","Ngày bắt đầu bảo hành"},
                 {"PackageDate","Ngày đóng gói"},
                 {"UserCan","Người đóng thùng"},
                 {"UserBox","Người đóng hộp"},
                 {"UserCheckPart","Người kiểm tra sản phẩm"},
                 {"UserKCS","KSC"},
                 {"InvCode","Mã kho"},
                 {"OutDTime","Ngày xuất kho"},
                 {"IF_InvOutFGNo","Số phiếu xuất"},
                 {"FlagSales","Trạng thái xuất"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblInv_InventoryBalanceSerial.SerialNo, "Mã SerialNo (*)"},
                {TblInv_InventoryBalanceSerial.PartCode, "Mã sản phẩm (*)"},
                {TblInv_InventoryBalanceSerial.CanNo, "Mã thùng"},
                {TblInv_InventoryBalanceSerial.BoxNo, "Mã hộp"},
                {TblInv_InventoryBalanceSerial.AgentCode, "Mã tổ chức"},
                {TblInv_InventoryBalanceSerial.PartLotNo, "Mã lô thành phẩm"},
                {TblInv_InventoryBalanceSerial.ShiftInCode, "Ca sản xuất"},
                {TblInv_InventoryBalanceSerial.ProductionDate, "Ngày sản xuất"},
                {TblInv_InventoryBalanceSerial.PackageDate, "Ngày đóng gói"},
                {TblInv_InventoryBalanceSerial.PrintDate, "Ngày bắt đầu in"},
            };
        }
        #endregion

        #region ["strWhereClause"]
        private string strWhereClause_Inv_InventoryBalanceSerial(Inv_InventoryBalanceSerialUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Inv_InventoryBalanceSerial = TableName.Inv_InventoryBalanceSerial + ".";
            var Tbl_Inv_InventorySecret = TableName.Inv_InventorySecret + ".";
            var Tbl_Inv_InventoryBox = TableName.Inv_InventoryBox + ".";
            var Tbl_Inv_InventoryCarton = TableName.Inv_InventoryCarton + ".";

            if (!CUtils.IsNullOrEmpty(data.AgentCode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.AgentCode, data.AgentCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.MST, data.MST, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.QR_SerialNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventorySecret + TblInv_InventorySecret.QR_SerialNo, data.QR_SerialNo , "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.PartCode, data.PartCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.QR_CanNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryCarton + TblInv_InventoryCarton.QR_CanNo, data.QR_CanNo, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.QR_BoxNo))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBox + TblInv_InventoryBox.QR_BoxNo, data.QR_BoxNo, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagSales))
            {
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.FlagSales, data.FlagSales, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PackageDateFrom))
            {
                data.PackageDate = data.PackageDateFrom.ToString().Trim();
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.PackageDate, data.PackageDate, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.PackageDateTo))
            {
                data.PackageDate = data.PackageDateTo.ToString().Trim();
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.PackageDate, data.PackageDate, "<=");
            }

            if (!CUtils.IsNullOrEmpty(data.OutDTimeFrom))
            {
                data.OutDTime = data.OutDTimeFrom.ToString().Trim() + " 00:00:00";
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.OutDTime, data.OutDTime, ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.OutDTimeTo))
            {
                data.OutDTime = data.OutDTimeTo.ToString().Trim() + " 23:59:59";
                sbSql.AddWhereClause(Tbl_Inv_InventoryBalanceSerial + TblInv_InventoryBalanceSerial.OutDTime, data.OutDTime, "<=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_MstPart(Mst_Part data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Part = TableName.Mst_Part + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.FlagActive, data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.PartCode, data.PartCode, "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_MstNNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, data.MST, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_MstInventory(Mst_Inventory data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Agent = TableName.Mst_Inventory + ".";

            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Agent + TblMst_Inventory.FlagActive, data.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(data.InvCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Agent + TblMst_Inventory.InvCode, data.InvCode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}