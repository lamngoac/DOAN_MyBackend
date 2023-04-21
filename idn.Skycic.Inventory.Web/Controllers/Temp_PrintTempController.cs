using idn.Skycic.Inventory.Common.Models;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using idn.Skycic.Inventory.Common.ModelsUI;
using System.IO;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Temp_PrintTempController : AdminBaseController
    {
        // GET: Temp_PrintTemp
        public ActionResult Index(string printtempcode = "", string printtempdes = "", string fromdate = "",
            string todate = "", string printtempstatus = "", string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            ViewBag.Offset = Offset;
            var pageInfo = new PageInfo<Temp_PrintTemp>(0, recordcount)
            {
                DataList = new List<Temp_PrintTemp>()
            };
            var itemCount = 0;
            var pageCount = 0;

            if (init != "init")
            {
                #region["Search"]
                var objTemp_PrintTempUI = new Temp_PrintTempUI()
                {
                    PrintTempCode = printtempcode,
                    PrintTempDesc = printtempdes,
                    PrintTempStatus = printtempstatus,
                    FromDate = fromdate,
                    ToDate = todate,
                };

                var strWhereClauseTemp_PrintTemp = strWhereClause_Temp_PrintTemp(objTemp_PrintTempUI);

                var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_WhereClause = strWhereClauseTemp_PrintTemp,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Temp_PrintTemp
                    Rt_Cols_Temp_PrintTemp = "*",
                };

                var objRT_Temp_PrintTempCur = Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Get(objRQ_Temp_PrintTemp, addressAPI);
                if (objRT_Temp_PrintTempCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Temp_PrintTempCur.MySummaryTable.MyCount);
                }
                if (objRT_Temp_PrintTempCur != null && objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get != null && objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get);
                    pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
                fromdate = DateToSearch();
            }
            ViewBag.PageCur = page.ToString();
            ViewBag.PrintTempCode = printtempcode;
            ViewBag.PrintTempDes = printtempdes;
            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;
            ViewBag.PrintTempStatus = printtempstatus;

            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();

            return View(pageInfo);
        }

        #region["Tạo mới mẫu tem"]
        [HttpGet]
        public ActionResult Create()
        {
            var printTempCode = SeqNo(new Seq_Common()
            {
                SequenceType = Client_SequenceType.PRTCODE,
                Param_Postfix = "",
                Param_Prefix = ""
            });
            ViewBag.PrintTempCode = printTempCode;
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
            var exitsData = "";
            try
            {
                var objTemp_PrintTempUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Temp_PrintTempUI>(model);
                var filePath = CUtils.StrValue(objTemp_PrintTempUIInput.FilePath);
                if (!CUtils.IsNullOrEmpty(filePath))
                {
                    var filePathCur = RootFolder + filePath.Replace(@"/", @"\");
                    using (StreamReader r = new StreamReader(filePathCur))
                    {
                        string json = r.ReadToEnd();
                        var contentFileDecrypt = CUtils.Decrypt(json);
                        dynamic data = System.Web.Helpers.Json.Decode(contentFileDecrypt.ToString());
                        
                        var uiid = CUtils.StrValue(data.UserId);
                        var title = CUtils.StrValue(data.Title);
                        var imageUrl = "";
                        if (!CUtils.IsNullOrEmpty(data.ImageUrl))
                        {
                            imageUrl = ReportBro_ServerUrl + CUtils.StrValue(data.ImageUrl);
                        }
                        var detail = data.Detail;
                        var watermark = "";
                        var objJson = new
                        {
                            outputFormat = "pdf",
                            isTestData = false,
                            data = CreateDataObjectReportServer(new List<Mst_Part>(), ref watermark),
                            watermark = watermark,
                            report = detail,
                        };
                        string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objJson);
                        
                        //var objJsonNew = new
                        //{
                        //    outputFormat = "pdf",
                        //    isTestData = false,
                        //    data = CreateDataObjectReportServer(new List<Mst_Part>(), ref watermark),
                        //    watermark = watermark,
                        //    report = detail,
                        //};

                        ////var key = PostReport(objJsonNew);
                        //string serializedObject1 = Newtonsoft.Json.JsonConvert.SerializeObject(objJsonNew);

                        var objTemp_PrintTempInput = new Temp_PrintTemp()
                        {
                            PrintTempCode = CUtils.StrValue(objTemp_PrintTempUIInput.PrintTempCode),
                            PrintTempDesc = CUtils.StrValue(objTemp_PrintTempUIInput.PrintTempDesc),
                            PrintTempBody = serializedObject,
                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        };

                        var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
                        {
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            FlagIsDelete = "0",

                            // RQ_Temp_PrintTemp
                            Rt_Cols_Temp_PrintTemp = null,
                            Temp_PrintTemp = objTemp_PrintTempInput
                        };
                        Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Save(objRQ_Temp_PrintTemp, addressAPIs);
                        exitsData = "Tạo mẫu tem thành công!";
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                }
                else
                {
                    exitsData = "Đường dẫn file mẫu tem trống!";
                }
                resultEntry.Success = true;

                resultEntry.AddMessage(exitsData);
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

        #region["Sửa mẫu tem"]
        [HttpGet]
        public ActionResult Update(string printtempcode)
        {
            var serverUrl = ReportBro_ServerUrl;
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);
            ViewBag.Offset = Offset;
            if (!CUtils.IsNullOrEmpty(printtempcode))
            {
                var listTemp_PrintTemp = this.ListTemp_PrintTemp(printtempcode);
                
                if (listTemp_PrintTemp != null && listTemp_PrintTemp.Count > 0)
                {
                    var objTemp_PrintTemp = listTemp_PrintTemp[0];
                    var printTempBody = CUtils.StrValue(objTemp_PrintTemp.PrintTempBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";
                    var listMst_Part = new List<Mst_Part>();
                    for (int i = 0; i < 10; i++)
                    {
                        var objMst_Part = new Mst_Part()
                        {
                            PartCode = (i + 1).ToString(),
                            PartName = "Việt Nam " + (i + 1),
                            PartNameFS = "Viet Nam " + (i + 1),
                        };
                        listMst_Part.Add(objMst_Part);
                    }
                    data.data = CreateDataObjectReportServer(listMst_Part, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            ViewBag.LinkPDF = linkpdf + "#toolbar=0";
                        }
                    }

                    return View(listTemp_PrintTemp[0]);
                }
            }
            return View(new Temp_PrintTemp());
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
            var exitsData = "";
            try
            {
                var objTemp_PrintTempUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Temp_PrintTempUI>(model);
                if (objTemp_PrintTempUIInput != null && !CUtils.IsNullOrEmpty(objTemp_PrintTempUIInput.PrintTempCode))
                {
                    var printTempCode = CUtils.StrValue(objTemp_PrintTempUIInput.PrintTempCode);
                    var listTemp_PrintTemp = this.ListTemp_PrintTemp(printTempCode);
                    if (listTemp_PrintTemp != null && listTemp_PrintTemp.Count > 0)
                    {
                        var strFt_Cols_Upd = "";
                        var Tbl_Temp_PrintTemp = TableName.Temp_PrintTemp + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Temp_PrintTemp, TblTemp_PrintTemp.PrintTempDesc);
                        //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Temp_PrintTemp, TblTemp_PrintTemp.PrintTempStatus);
                        var filePath = CUtils.StrValue(objTemp_PrintTempUIInput.FilePath);
                        if (!CUtils.IsNullOrEmpty(filePath))
                        {
                            // trường hợp thay đổi mẫu tem
                            var filePathCur = RootFolder + filePath.Replace(@"/", @"\");
                            using (StreamReader r = new StreamReader(filePathCur))
                            {
                                string json = r.ReadToEnd();
                                var contentFileDecrypt = CUtils.Decrypt(json);
                                dynamic data = System.Web.Helpers.Json.Decode(contentFileDecrypt.ToString());

                                var uiid = CUtils.StrValue(data.UserId);
                                var title = CUtils.StrValue(data.Title);
                                var imageUrl = "";
                                if (!CUtils.IsNullOrEmpty(data.ImageUrl))
                                {
                                    imageUrl = ReportBro_ServerUrl + CUtils.StrValue(data.ImageUrl);
                                }
                                var detail = data.Detail;
                                var watermark = "";
                                var objJson = new
                                {
                                    outputFormat = "pdf",
                                    isTestData = false,
                                    data = CreateDataObjectReportServer(new List<Mst_Part>(), ref watermark),
                                    watermark = watermark,
                                    report = detail,
                                };
                                string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objJson);

                                listTemp_PrintTemp[0].PrintTempBody = serializedObject;
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Temp_PrintTemp, TblTemp_PrintTemp.PrintTempDesc);
                            }
                        }
                        listTemp_PrintTemp[0].PrintTempDesc = CUtils.StrValue(objTemp_PrintTempUIInput.PrintTempDesc);

                        var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
                        {
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            WAUserCode = waUserCode,
                            WAUserPassword = waUserPassword,
                            OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            FlagIsDelete = "0",
                            Ft_Cols_Upd = strFt_Cols_Upd,
                            // RQ_Temp_PrintTemp
                            Rt_Cols_Temp_PrintTemp = null,
                            Temp_PrintTemp = listTemp_PrintTemp[0],
                        };
                        Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Save(objRQ_Temp_PrintTemp, addressAPIs);

                        exitsData = "Sửa mẫu tem thành công!";
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        exitsData = "Mã mẫu tem '" + printTempCode + "' không có trong hệ thống!";
                    }
                }
                else
                {
                    exitsData = "Mã mẫu tem trống!";
                }
                resultEntry.Success = true;
                resultEntry.AddMessage(exitsData);
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

        #region["Chi tiết - Xóa - Duyệt - Hủy mẫu tem"]
        [HttpGet]
        public ActionResult Detail(string printtempcode)
        {
            var serverUrl = ReportBro_ServerUrl;
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);
            ViewBag.Offset = Offset;
            if (!CUtils.IsNullOrEmpty(printtempcode))
            {
                var listTemp_PrintTemp = this.ListTemp_PrintTemp(printtempcode);

                if (listTemp_PrintTemp != null && listTemp_PrintTemp.Count > 0)
                {
                    var objTemp_PrintTemp = listTemp_PrintTemp[0];
                    var printTempBody = CUtils.StrValue(objTemp_PrintTemp.PrintTempBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";
                    var listMst_Part = new List<Mst_Part>();
                    for (int i = 0; i < 10; i++)
                    {
                        var objMst_Part = new Mst_Part()
                        {
                            PartCode = (i + 1).ToString(),
                            PartName = "Việt Nam " + (i + 1),
                            PartNameFS = "Viet Nam " + (i + 1),
                        };
                        listMst_Part.Add(objMst_Part);
                    }
                    data.data = CreateDataObjectReportServer(listMst_Part, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            ViewBag.LinkPDF = linkpdf + "#toolbar=0";
                        }
                    }

                    return View(listTemp_PrintTemp[0]);
                }
            }
            return View(new Temp_PrintTemp());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPI = userState.AddressAPIs;
            var resultEntry = new JsonResultEntry() { Success = false };
            var message = "";
            try
            {
                var listPrintTempCode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(model);
                if (listPrintTempCode != null && listPrintTempCode.Count > 0)
                {
                    // Do xóa danh sách nên ko thực hiện truy vấn check mẫu tem có tồn tại trong DB => xóa luôn
                    foreach (var printTempCode in listPrintTempCode)
                    {
                        var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
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
                            UtcOffset = userState.UtcOffset,
                            FlagIsDelete = "1",
                            // RQ_Temp_PrintTemp
                            Rt_Cols_Temp_PrintTemp = "*",
                            Temp_PrintTemp = new Temp_PrintTemp()
                            {
                                PrintTempCode = CUtils.StrValue(printTempCode),
                            },
                        };
                        Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Save(objRQ_Temp_PrintTemp, addressAPI);
                    }

                    if (listPrintTempCode.Count == 1)
                    {
                        message = "Xóa mẫu tem '" + CUtils.StrValue(listPrintTempCode[0]) +"' thành công!";
                    }
                    else
                    {
                        message = "Xóa danh sách mẫu tem thành công!";
                    }
                }
                else
                {
                    message = "Mã mẫu tem trống!";
                }

                resultEntry.Success = true;
                resultEntry.AddMessage(message);
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Approved(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPI = userState.AddressAPIs;
            var resultEntry = new JsonResultEntry() { Success = false };
            var message = "";
            try
            {
                var listPrintTempCode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(model);
                if (listPrintTempCode != null && listPrintTempCode.Count > 0)
                {
                    // Do duyệt danh sách nên ko thực hiện truy vấn check mẫu tem có tồn tại trong DB => duyệt luôn
                    foreach (var printTempCode in listPrintTempCode)
                    {
                        var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
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
                            UtcOffset = userState.UtcOffset,
                            // RQ_Temp_PrintTemp
                            Rt_Cols_Temp_PrintTemp = "*",
                            Temp_PrintTemp = new Temp_PrintTemp()
                            {
                                PrintTempCode = CUtils.StrValue(printTempCode),
                            },
                        };
                        Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Approved(objRQ_Temp_PrintTemp, addressAPI);
                    }

                    if (listPrintTempCode.Count == 1)
                    {
                        message = "Duyệt mẫu tem '" + CUtils.StrValue(listPrintTempCode[0]) + "' thành công!";
                    }
                    else
                    {
                        message = "Duyệt danh sách mẫu tem thành công!";
                    }
                }
                else
                {
                    message = "Mã mẫu tem trống!";
                }

                resultEntry.Success = true;
                resultEntry.AddMessage(message);
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Cancel(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var addressAPI = userState.AddressAPIs;
            var resultEntry = new JsonResultEntry() { Success = false };
            var message = "";
            try
            {
                var listPrintTempCode = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(model);
                if (listPrintTempCode != null && listPrintTempCode.Count > 0)
                {
                    // Do hủy danh sách nên ko thực hiện truy vấn check mẫu tem có tồn tại trong DB => hủy luôn
                    foreach (var printTempCode in listPrintTempCode)
                    {
                        var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
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
                            UtcOffset = userState.UtcOffset,
                            // RQ_Temp_PrintTemp
                            Rt_Cols_Temp_PrintTemp = "*",
                            Temp_PrintTemp = new Temp_PrintTemp()
                            {
                                PrintTempCode = CUtils.StrValue(printTempCode),
                            },
                        };
                        Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Cancel(objRQ_Temp_PrintTemp, addressAPI);
                    }

                    if (listPrintTempCode.Count == 1)
                    {
                        message = "Hủy mẫu tem '" + CUtils.StrValue(listPrintTempCode[0]) + "' thành công!";
                    }
                    else
                    {
                        message = "Hủy danh sách mẫu tem thành công!";
                    }
                }
                else
                {
                    message = "Mã mẫu tem trống!";
                }

                resultEntry.Success = true;
                resultEntry.AddMessage(message);
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }
        #endregion

        #region[""]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowPopupImportFile()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                return JsonView("_ImportFile", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_ImportFile", null, resultEntry);
        }
        #endregion

        #region["Import"]

        private List<Temp_PrintTemp> ListTemp_PrintTemp(string printtempcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);

            var listTemp_PrintTemp = new List<Temp_PrintTemp>();
            if (!CUtils.IsNullOrEmpty(printtempcode))
            {
                var strWhereClauseTemp_PrintTemp = strWhereClause_Temp_PrintTemp(new Temp_PrintTempUI() { PrintTempCode = printtempcode });

                var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseTemp_PrintTemp,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Temp_PrintTemp
                    Rt_Cols_Temp_PrintTemp = "*",
                };

                var objRT_Temp_PrintTempCur = Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Get(objRQ_Temp_PrintTemp, addressAPI);
                if (objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get != null && objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get.Count > 0)
                {
                    listTemp_PrintTemp.AddRange(objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get);
                }
            }
            return listTemp_PrintTemp;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportFileTemp_PrintTemp(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var exitsData = "";
            var filePath = "";
            var temFilePath = "";
            var urlFilePdf = "";
            var serverUrl = ReportBro_ServerUrl;
            var outputFormat = "";
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext.Equals(".rtmpl"))
                {
                    filePath = FileUtils.SaveTempFile(file, fileId, ext);
                    string filePath1 = FileUtils.GetTempFilePhysicalPath(fileId, ext);
                    var iindex = filePath.IndexOf(@"\TempFiles\");
                    temFilePath = filePath.Substring(iindex);
                    if (!CUtils.IsNullOrEmpty(temFilePath))
                    {
                        temFilePath = temFilePath.Replace(@"\", "/");
                    }
                }
                else
                {
                    exitsData = "File import không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }

                var abc = CUtils.Encrypt(filePath);

                var abcd = CUtils.Decrypt(abc);
                dynamic data = null;
                using (StreamReader r = new StreamReader(file.InputStream))
                {
                    string json = r.ReadToEnd();
                    var contentFileDecrypt = CUtils.Decrypt(json);
                    data = System.Web.Helpers.Json.Decode(contentFileDecrypt);
                    var detail = data.Detail;
                    outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var watermark = "";
                    var objJsonNew = new
                    {
                        outputFormat = "pdf",
                        isTestData = false,
                        data = CreateDataObjectReportServer( new List<Mst_Part>(), ref watermark),
                        watermark = watermark,
                        report = detail,
                    };

                    //var key = PostReport(objJsonNew);
                    string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objJsonNew);
                    var content = PostReport(objJsonNew);
                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            //var linkpdf = String.Format("{0}{1}/{2}?key={3}&outputFormat={4}", serverUrl, "rp", "preview", key, outputFormat);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            ViewBag.linkpdf = linkpdf + "#toolbar=0";
                        }
                    }
                    var uiid = CUtils.StrValue(data.UserId);
                    var title = CUtils.StrValue(data.Title);
                    var imageUrl = "";
                    if (!CUtils.IsNullOrEmpty(data.ImageUrl))
                    {
                        imageUrl = ReportBro_ServerUrl + CUtils.StrValue(data.ImageUrl.ToString().Substring(1));
                    }
                    var vatType = "";
                    if (!CUtils.IsNullOrEmpty(data.VatType))
                    {
                        vatType = CUtils.StrValue(data.VatType).ToString().ToUpper();
                    }
                    urlFilePdf = @"https://services.qinvoice.vn/idocNet.Real.HDDT.V22.3344306000.WA/api/File/UploadedFiles/PdfFiles/3344306000/HOADT@IDOCNET.COM/2019-11-28/0005K3BS00H0W820191128_094050.pdf";
                    urlFilePdf = ViewBag.linkpdf;
                    Object objData = new {
                        //FilePathImport = filePath,
                        FilePathImport = temFilePath,
                        UrlFilePdf = urlFilePdf,

                    };

                    return Json(new { Success = true, ObjectData = objData });
                    //return JsonView("InvoiceTemGroup_Selected", null);
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
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Temp_PrintTemp>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Temp_PrintTemp).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Temp_PrintTemp"));

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
        public ActionResult Export(string printtempcode = "", string printtempdes = "", string fromdate = "",
            string todate = "", string printtempstatus = "", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listTemp_PrintTemp = new List<Temp_PrintTemp>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;

            try
            {
                #region["Search"]

                var objTemp_PrintTempUI = new Temp_PrintTempUI()
                {
                    PrintTempCode = printtempcode,
                    PrintTempDesc = printtempdes,
                    FromDate = fromdate,
                    ToDate = todate,
                    PrintTempStatus = printtempstatus,
                };

                var strWhereClauseTemp_PrintTemp = strWhereClause_Temp_PrintTemp(objTemp_PrintTempUI);

                var objRQ_Temp_PrintTemp = new RQ_Temp_PrintTemp()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseTemp_PrintTemp,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Temp_PrintTemp
                    Rt_Cols_Temp_PrintTemp = "*",
                };

                var objRT_Temp_PrintTempCur = Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Get(objRQ_Temp_PrintTemp, addressAPI);
                if (objRT_Temp_PrintTempCur != null && objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get != null)
                {
                    if (objRT_Temp_PrintTempCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Temp_PrintTempCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get != null && objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listTemp_PrintTemp.AddRange(objRT_Temp_PrintTempCur.Lst_Temp_PrintTemp_Get);
                    foreach (var item in listTemp_PrintTemp)
                    {
                        item.ApprDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.ApprDTimeUTC), Nonsense.DATE_TIME_FORMAT, Offset);
                        item.CreateDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, Offset);
                    }
                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Temp_PrintTemp).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listTemp_PrintTemp, dicColNames, filePath, string.Format("Temp_PrintTemp"));


                    #region["Export các trang còn lại"]
                    listTemp_PrintTemp.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Temp_PrintTemp.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Temp_PrintTempExportCur = Temp_PrintTempService.Instance.WA_Temp_PrintTemp_Get(objRQ_Temp_PrintTemp, addressAPI);
                            if (objRT_Temp_PrintTempExportCur != null && objRT_Temp_PrintTempExportCur.Lst_Temp_PrintTemp_Get != null && objRT_Temp_PrintTempExportCur.Lst_Temp_PrintTemp_Get.Count > 0)
                            {
                                listTemp_PrintTemp.AddRange(objRT_Temp_PrintTempExportCur.Lst_Temp_PrintTemp_Get);
                                foreach (var item in listTemp_PrintTemp)
                                {
                                    item.ApprDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.ApprDTimeUTC), Nonsense.DATE_TIME_FORMAT, Offset);
                                    item.CreateDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.CreateDTimeUTC), Nonsense.DATE_TIME_FORMAT, Offset);
                                }
                                ExcelExport.ExportToExcelFromList(listTemp_PrintTemp, dicColNames, filePath, string.Format("Temp_PrintTemp_" + i));
                                listTemp_PrintTemp.Clear();
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

        #region["strWhereClause"]
        private string strWhereClause_Temp_PrintTemp(Temp_PrintTempUI model)
        {
            var strWhereClause = "";
            var Tbl_Temp_PrintTemp = TableName.Temp_PrintTemp + ".";
            var sbSql = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(model.PrintTempCode))
            {
                sbSql.AddWhereClause(Tbl_Temp_PrintTemp + TblTemp_PrintTemp.PrintTempCode, "%" + model.PrintTempCode + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(model.PrintTempDesc))
            {
                sbSql.AddWhereClause(Tbl_Temp_PrintTemp + TblTemp_PrintTemp.PrintTempDesc, "%" + model.PrintTempDesc + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(model.PrintTempStatus))
            {
                sbSql.AddWhereClause(Tbl_Temp_PrintTemp + TblTemp_PrintTemp.PrintTempStatus, model.PrintTempStatus, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FromDate))
            {
                model.CreateDTimeUTC = model.FromDate.ToString().Trim() + " 00:00:00";
                //var fromdate = CUtils.ConvertingLocalTimeToUTC(model.CreateDTimeUTC).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Temp_PrintTemp + TblTemp_PrintTemp.CreateDTimeUTC, model.CreateDTimeUTC, ">=");
            }
            if (!CUtils.IsNullOrEmpty(model.ToDate))
            {
                model.CreateDTimeUTC = model.ToDate.ToString().Trim() + " 23:59:59";
                //var todate = CUtils.ConvertingLocalTimeToUTC(model.CreateDTimeUTC).ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                sbSql.AddWhereClause(Tbl_Temp_PrintTemp + TblTemp_PrintTemp.CreateDTimeUTC, model.CreateDTimeUTC, "<=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        #region["GetImportDicColums"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblTemp_PrintTemp.PrintTempCode,"Mã tem (*)"},
                {TblTemp_PrintTemp.PrintTempDesc,"Mô tả"},
                {TblTemp_PrintTemp.PrintTempBody,"Nội dung (*)"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblTemp_PrintTemp.PrintTempCode,"Mã tem (*)"},
                {TblTemp_PrintTemp.PrintTempDesc,"Mô tả"},
                {TblTemp_PrintTemp.PrintTempBody,"Nội dung (*)"},
                {TblTemp_PrintTemp.CreateDTimeUTC,"Ngày tạo"},
                {TblTemp_PrintTemp.ApprDTimeUTC,"Ngày duyệt"},
                {TblTemp_PrintTemp.PrintTempStatus,"Trạng thái"},
            };
        }
        #endregion
    }
}