using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using Newtonsoft.Json.Linq;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class LicenseController : AdminBaseController
    {
        // GET: License
        public ActionResult Index(string fromdate = "", string todate = "", string licstatus = "", string init = "init", int RecordCount = 10, int page = 0)
        {
            ViewBag.PageCur = page.ToString();

            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            ViewBag.Offset = Offset;

            var pageInfo = new PageInfo<Invoice_licenseCreHist>(0, PageSizeConfig)
            {
                DataList = new List<Invoice_licenseCreHist>()
            };
            var itemCount = 0;
            var pageCount = 0;

            #region Thông tin số lượng hóa đơn
            var listInvoice_license = WA_Invoice_license_Get_By_MST();
            if (listInvoice_license.Count > 0)
            {
                ViewBag.Invoice_license = listInvoice_license[0];
            }
            else
            {
                ViewBag.Invoice_license = new Invoice_license();
            }

            #endregion

            if (init != "init")
            {
                #region["Search"]
                var convertfromdate = "";
                if (!CUtils.IsNullOrEmpty(fromdate))
                {
                    convertfromdate = fromdate + " 00:00:00";
                }

                var converttodate = "";
                if (!CUtils.IsNullOrEmpty(todate))
                {
                    converttodate = todate + " 00:00:00";
                }

                var objOS_Inos_OrgLicense = new OS_Inos_OrgLicenseUI()
                {
                    FromDate = convertfromdate,
                    ToDate = converttodate,
                    LicStatus = licstatus,
                };

                var strWhereClauseOS_Inos_OrgLicense = strWhereClause_OS_Inos_OrgLicenseMng(objOS_Inos_OrgLicense);

                var objRQ_Invoice_licenseCreHist = new RQ_Invoice_licenseCreHist()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseOS_Inos_OrgLicense,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    AccessToken = userState.TokenID.ToString().Trim(),
                    // RQ_Mst_GovTaxID
                    Rt_Cols_Invoice_licenseCreHist = "*",
                };

                var objRT_Invoice_licenseCreHistCur = Invoice_licenseService.Instance.WA_Invoice_licenseCreHist_Get(objRQ_Invoice_licenseCreHist, userState.AddressAPIs);

                if (objRT_Invoice_licenseCreHistCur != null && objRT_Invoice_licenseCreHistCur.Lst_Invoice_licenseCreHist != null && objRT_Invoice_licenseCreHistCur.Lst_Invoice_licenseCreHist.Count > 0)
                {
                    itemCount = objRT_Invoice_licenseCreHistCur.Lst_Invoice_licenseCreHist.Count;
                    pageInfo.DataList.AddRange(objRT_Invoice_licenseCreHistCur.Lst_Invoice_licenseCreHist);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
                fromdate = DateToSearch();
                todate = DateTimeNow();
            }

            ViewBag.FromDate = fromdate;
            ViewBag.ToDate = todate;
            ViewBag.LicStatus = licstatus;

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region["Sinh số"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SinhSo(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objInv_GenTimesInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Inv_GenTimes>(model);
                var genTimesNo = SeqNo(new Seq_Common()
                {
                    SequenceType = Client_SequenceType.GENTIMESNO,
                    Param_Postfix = "",
                    Param_Prefix = ""
                });
                var objInv_GenTimes = new Inv_GenTimes()
                {
                    GenTimesNo = genTimesNo,
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    MST = CUtils.StrValue(userState.Mst_NNT.MST),
                    Qty = objInv_GenTimesInput.Qty,
                };
                var objRQ_Inv_GenTimes = new RQ_Inv_GenTimes
                {
                    FlagIsDelete = null,
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
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
                    // RQ_Inv_GenTimes
                    Rt_Cols_Inv_GenTimes = null,
                    Inv_GenTimes = objInv_GenTimes
                };
                Inv_GenTimesService.Instance.WA_Inv_GenTimes_Add(objRQ_Inv_GenTimes, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Sinh số thành công!");
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
        #endregion

        #region["Mua License"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ShowPopupLicense()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {

                #region Thông tin package
                var listOS_Inos_Package = new List<OS_Inos_Package>();
                var objRQ_OS_Inos_Package = new RQ_OS_Inos_Package()
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
                    // RQ_OS_Inos_Package
                    Rt_Cols_OS_Inos_Package = "*",
                    OS_Inos_Package = null
                };
                var objRT_OS_Inos_Package = OS_InosService.Instance.WA_OS_Inos_Package_Get(objRQ_OS_Inos_Package, userState.AddressAPIs);
                if (objRT_OS_Inos_Package.Lst_OS_Inos_Package != null &&
                    objRT_OS_Inos_Package.Lst_OS_Inos_Package.Count > 0)
                {
                    listOS_Inos_Package.AddRange(objRT_OS_Inos_Package.Lst_OS_Inos_Package);
                }

                ViewBag.ListOS_Inos_Package = listOS_Inos_Package;

                ViewBag.OrgId = userState.Mst_NNT.OrgID;
                #endregion

                return JsonView("ShowPopupLicense", listOS_Inos_Package);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupLicense", null, resultEntry);
        }


        #region["Gọi hàm get mã giảm giá tại network"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OS_Inos_OrderService_GetDiscountCode(string discountcode, string dealercode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waMST = userState.SysUser.MST;
            var resultEntry = new JsonResultEntry() { Success = false };

            var exitsData = "";
            try
            {
                if (!CUtils.IsNullOrEmpty(discountcode))
                {
                    var strWhereClause = "";
                    if (discountcode != null && discountcode != "")
                    {
                        strWhereClause = "Map_DealerDiscount.DiscountCode = '" + discountcode + "'";
                    }
                    if (dealercode != null && dealercode != "")
                    {
                        if (strWhereClause == "")
                        {
                            strWhereClause += "Map_DealerDiscount.DLCode = '" + dealercode + "'";
                        }
                        else
                        {
                            strWhereClause += " and Map_DealerDiscount.DLCode = '" + dealercode + "'";
                        }
                    }
                    var objRQ_Map_DealerDiscount = new RQ_Map_DealerDiscount
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_Cols_Upd = null,
                        Rt_Cols_Map_DealerDiscount = "*",
                        Ft_WhereClause = strWhereClause
                    };
                    var objRT_Map_DealerDiscount = Map_DealerDiscountService.Instance.WA_Map_DealerDiscount_Get(objRQ_Map_DealerDiscount, addressAPIs);
                    if (objRT_Map_DealerDiscount.Lst_Map_DealerDiscount != null && objRT_Map_DealerDiscount.Lst_Map_DealerDiscount.Count > 0)
                    {
                        var objDiscount = new Map_DealerDiscount();
                        if (objRT_Map_DealerDiscount.Lst_Map_DealerDiscount.Count > 0)
                        {
                            objDiscount = objRT_Map_DealerDiscount.Lst_Map_DealerDiscount[0];
                            if (objDiscount.FlagActive.ToString() == "1")
                            {
                                var objRQ_OS_Inos_LicOrder = new RQ_OS_Inos_LicOrder()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = waUserCode,
                                    WAUserPassword = waUserPassword,
                                    FuncType = null,
                                    Ft_RecordStart = Ft_RecordStart,
                                    Ft_RecordCount = Ft_RecordCount,
                                    Ft_WhereClause = null,
                                    Ft_Cols_Upd = null,
                                    // RQ_OS_Inos_LicOrder
                                    Inos_LicOrder = new Inos_LicOrder()
                                    {
                                        DiscountCode = discountcode,
                                    },
                                };
                                var objRT_OS_Inos_LicOrder = OS_InosService.Instance.WA_OS_Inos_OrderService_GetDiscountCode(objRQ_OS_Inos_LicOrder, addressAPIs);
                                if (objRT_OS_Inos_LicOrder != null && objRT_OS_Inos_LicOrder.Inos_DiscountCode != null)
                                {
                                    if (objRT_OS_Inos_LicOrder.Inos_DiscountCode.Enabled)
                                    {
                                        if (Convert.ToInt32(objRT_OS_Inos_LicOrder.Inos_DiscountCode.RemainQty) <= 0)
                                        {
                                            exitsData = "Mã giảm giá đã hết lượt sử dụng!";
                                        }
                                        else
                                        {
                                            var today = DateTime.Now.Date;
                                            var fromdate = objRT_OS_Inos_LicOrder.Inos_DiscountCode.EffectDateFrom;
                                            var todate = objRT_OS_Inos_LicOrder.Inos_DiscountCode.EffectDateTo;
                                            if (!(today >= fromdate && today <= todate))
                                            {
                                                if (today <= fromdate)
                                                {
                                                    exitsData = "Mã giảm giá chưa có hiệu lực sử dụng!";
                                                }
                                                else if (today >= todate)
                                                {
                                                    exitsData = "Mã giảm giá đã hết hạn sử dụng!";
                                                }
                                            }
                                            else
                                            {
                                                var discountType = objRT_OS_Inos_LicOrder.Inos_DiscountCode.DiscountType.ToString();
                                                if (discountType == "Dispose")
                                                {
                                                    discountcode = objRT_OS_Inos_LicOrder.Inos_DiscountCode.DiscountAmount.ToString();
                                                }
                                                return Json(new { Success = true, OS_Inos_LicOrder = objRT_OS_Inos_LicOrder.Inos_DiscountCode });
                                            }
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Mã giảm giá hết hiệu lực!";
                                    }
                                }
                                else
                                {
                                    exitsData = "Mã giảm giá không hợp lệ!";
                                }
                            }
                        }
                        return Json(new { Success = false, Messages = "Mã giảm giá sai." });
                    }
                    else
                    {
                        exitsData = "Mã giảm giá không hợp lệ!";
                    }

                }
                else
                {
                    exitsData = "Mã giảm giá trống!";
                }
                resultEntry.AddMessage(exitsData);
                return Json(new { Success = false, Messages = exitsData });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);

            return Json(resultEntry);
        }
        #endregion
        #endregion

        #region Payment VNPAY
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePaymentLiceseAdd(string model, string language, string bankcode, string ordertype)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            Inos_LicOrderUI objInos_LicOrder = new Inos_LicOrderUI();
            try
            {
                if (model != null)
                {
                    objInos_LicOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrderUI>(model);
                }
                System.Web.HttpContext.Current.Session["orderId"] = objInos_LicOrder.Id;
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"];
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
                string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_ReturnurlLiceseAdd"];
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
                VnPayLibrary vnpay = new VnPayLibrary();
                vnpay.AddRequestData("vnp_Version", "2.0.0");
                vnpay.AddRequestData("vnp_Command", "pay");
                vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);

                string locale = language;
                if (!string.IsNullOrEmpty(locale))
                {
                    vnpay.AddRequestData("vnp_Locale", locale);
                }
                else
                {
                    vnpay.AddRequestData("vnp_Locale", "vn");
                }
                vnpay.AddRequestData("vnp_CurrCode", "VND");
                //vnpay.AddRequestData("vnp_TxnRef", objInos_LicOrder.Id.ToString());
                vnpay.AddRequestData("vnp_TxnRef", objInos_LicOrder.PaymentCode.ToString());
                vnpay.AddRequestData("vnp_OrderInfo", objInos_LicOrder.Remark.ToString());
                vnpay.AddRequestData("vnp_OrderType", ordertype);
                var totalCost = objInos_LicOrder.TotalCost == null ? 0 : Convert.ToDouble(objInos_LicOrder.TotalCost);
                vnpay.AddRequestData("vnp_Amount", (totalCost * 100).ToString());

                vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                vnpay.AddRequestData("vnp_IpAddr", VnPayLibrary.Utils.GetIpAddress());
                vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                vnpay.AddRequestData("vnp_BankCode", bankcode);
                //Calculate data for sign

                string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
                if (Request.IsAjaxRequest())
                {
                    var jsonData = new { code = "00", message = "success", data = paymentUrl };
                    return Json(jsonData, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Redirect(paymentUrl);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Detail = ex.ToString() });
            }
        }

        public ActionResult VnPayReturnLiceseAdd()
        {
            ViewBag.NetworkID = UserState.SysUser.NetworkID;
            var vnpayData = Request.QueryString;

            var vnp_Params = new Dictionary<string, string>();
            if (vnpayData.Count > 0)
            {
                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    vnp_Params.Add(s, vnpayData[s]);
                }
            }
            //Remove some keys
            if (vnp_Params.ContainsKey("vnp_SecureHashType"))
            {
                vnp_Params.Remove("vnp_SecureHashType");
            }
            string vnp_SecureHash = "";
            if (vnp_Params.ContainsKey("vnp_SecureHash"))
            {
                vnp_SecureHash = vnp_Params["vnp_SecureHash"];
                vnp_Params.Remove("vnp_SecureHash");
            }
            vnp_Params = vnp_Params.OrderBy(o => o.Key, new VnPayLibrary.VnPayCompare()).ToDictionary(k => k.Key, v => v.Value);
            string singData = string.Join("&",
                vnp_Params.Where(x => !string.IsNullOrEmpty(x.Value))
                    .Select(k => (HttpUtility.UrlDecode(k.Key) + "=" + HttpUtility.UrlDecode(k.Value))));

            string rspCode = vnp_Params["vnp_ResponseCode"];
            var objInos_LicOrderCur = GetOrderStatus();

            if (objInos_LicOrderCur != null)
            {
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];

                string secureHash = VnPayLibrary.Utils.Md5(vnp_HashSecret + singData);
                if (secureHash.Equals(vnp_SecureHash))
                {
                    if (rspCode == "00")
                    {
                        ViewBag.msg = "<p style= 'color:green'>Giao dịch được thực hiện thành công. Cảm ơn quý khách đã sử dụng dịch vụ</p>";
                    }
                    else
                    {

                        ViewBag.msg = "<p style='color:red'>Giao dịch bị lỗi trong quá trình xử lý</p>";
                    }

                }
                else
                {
                    ViewBag.msg = "<p style='color:red'>Giao dịch không thành công do sai chữ ký</p>";
                }
            }
            else
            {
                ViewBag.msg = "<p style='color:red'>Không tìm thấy giao dịch</p>";
            }

            return View();
        }

        public Inos_LicOrder GetOrderStatus()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waTokenID = userState.TokenID;
            
            var objInos_LicOrder = new Inos_LicOrder();
            var orderId = CUtils.StrValue(System.Web.HttpContext.Current.Session["orderId"]);

            var rqInos_LicOrder = new Inos_LicOrder()
            {
                Id = orderId
            };
            var rq = new RQ_OS_Inos_LicOrder()
            {
                Inos_LicOrder = rqInos_LicOrder,
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = waTokenID,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount
            };

            var rt = OS_InosService.Instance.WA_OS_Inos_OrderService_CheckOrderStatus(rq, addressAPIs);
            if (rt != null && rt.Inos_LicOrder != null)
            {
                objInos_LicOrder = rt.Inos_LicOrder;
            }
            return objInos_LicOrder;
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderInos(string inoslicorder)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var resultEntry = new JsonResultEntry
            {
                Success = false,
                RedirectUrl = null
            };
            try
            {
                var objInos_LicOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrder>(inoslicorder); // truyền cả orgid
                #region["5. Tạo đơn hàng"]
                var objRQ_OS_Inos_LicOrder = new RQ_OS_Inos_LicOrder()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Inos_LicOrder = objInos_LicOrder,
                };
                var objRT_OS_Inos_LicOrder = OS_InosService.Instance.WA_OS_Inos_OrderService_CreateOrder(objRQ_OS_Inos_LicOrder, addressAPIs);
                var objInos_LicOrderUI = new Inos_LicOrderUI();
                if (objRT_OS_Inos_LicOrder != null && objRT_OS_Inos_LicOrder.Inos_LicOrder != null)
                {
                    objInos_LicOrderUI.Id = objRT_OS_Inos_LicOrder.Inos_LicOrder.Id;
                    objInos_LicOrderUI.OrgId = objRT_OS_Inos_LicOrder.Inos_LicOrder.OrgId;
                    objInos_LicOrderUI.PaymentCode = objRT_OS_Inos_LicOrder.Inos_LicOrder.PaymentCode;
                    objInos_LicOrderUI.TotalCost = objRT_OS_Inos_LicOrder.Inos_LicOrder.TotalCost;
                    objInos_LicOrderUI.StrCreateDTime = objRT_OS_Inos_LicOrder.Inos_LicOrder.CreateDTime.ToString("yyyyMMddHHmmss");
                    objInos_LicOrderUI.Remark = objRT_OS_Inos_LicOrder.Inos_LicOrder.Remark;
                    objInos_LicOrderUI.Inos_DetailList = new List<Inos_LicOrderDetail>();
                    if (objRT_OS_Inos_LicOrder.Inos_LicOrder.Inos_DetailList != null && objRT_OS_Inos_LicOrder.Inos_LicOrder.Inos_DetailList.Count > 0)
                    {
                        objInos_LicOrderUI.Inos_DetailList.AddRange(objRT_OS_Inos_LicOrder.Inos_LicOrder.Inos_DetailList);
                    }
                    return Json(new { Success = true, Inos_LicOrder = objInos_LicOrderUI });
                }
                #endregion
            }
            catch (Exception ex)
            {
                resultEntry.AddMessage(ex.StackTrace.ToString());
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("BuyPackage", null, resultEntry);
        }

        [HttpPost]
        public ActionResult CheckOrderStatus()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objInos_LicOrderCur = GetOrderStatus();
                var status = objInos_LicOrderCur.Status.ToString().ToUpper().Trim();
                if (status.Equals("APPROVED"))
                {
                    resultEntry.RedirectUrl = Url.Action("Index", "License");
                }
                resultEntry.Success = true;
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("CheckOrderStatus", null, resultEntry);
        }
        private string strWhereClause_OS_Inos_OrgLicenseMng(OS_Inos_OrgLicenseUI data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_OS_Inos_OrgLicense = TableName.OS_Inos_OrgLicense + ".";

            if (!CUtils.IsNullOrEmpty(data.FromDate))
            {
                sbSql.AddWhereClause(Tbl_OS_Inos_OrgLicense + TblOS_Inos_OrgLicense.StartDate, CUtils.StrValue(data.FromDate), ">=");
            }
            if (!CUtils.IsNullOrEmpty(data.ToDate))
            {
                sbSql.AddWhereClause(Tbl_OS_Inos_OrgLicense + TblOS_Inos_OrgLicense.StartDate, CUtils.StrValue(data.ToDate), "<=");
            }
            if (!CUtils.IsNullOrEmpty(data.LicStatus))
            {
                sbSql.AddWhereClause(Tbl_OS_Inos_OrgLicense + TblOS_Inos_OrgLicense.LicStatus, CUtils.StrValue(data.LicStatus), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
    }
}