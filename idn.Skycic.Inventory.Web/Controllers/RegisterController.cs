using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class RegisterController : BaseController
    {
        #region[""]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOrderInos(string inoslicorder)
        {
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

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
                    WAUserCode = waUserCode_RptSV,
                    WAUserPassword = waUserPassword_RptSV,
                    AccessToken = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Inos_LicOrder = objInos_LicOrder,
                };
                var objRT_OS_Inos_LicOrder = OS_InosService.Instance.WA_RptSv_OS_Inos_OrderService_CreateOrder(objRQ_OS_Inos_LicOrder, addressReportAPIs);
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
        #endregion

        #region["Confirm Code"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmCode(string usercode, string grecaptcharesponse)
        {
            var resultEntry = new JsonResultEntry() { Success = false };


            var exitsData = "";
            try
            {
                string secretKey = ReCaptchaPrivateKey;
                var client = new WebClient();
                //var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, grecaptcharesponse));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");
                if (status)
                {
                    resultEntry.Success = true;
                }
                else
                {
                    exitsData = "Mã captcha không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                //resultEntry.Success = true;
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);

            return Json(resultEntry);
        }
        #endregion

        #region ["Tạo mới"]
        [HttpGet]
        public ActionResult Create(string promote = "")
        {
            var cacheNNT = System.Web.HttpContext.Current.Session["Mst_NNT"] as Mst_NNT;
            ViewBag.CacheNNT = cacheNNT;
            var cacheInos_Org = System.Web.HttpContext.Current.Session["OS_Inos_Org"] as OS_Inos_Org;
            ViewBag.CacheInos_Org = cacheInos_Org;
            var cacheShortName = System.Web.HttpContext.Current.Session["ShortName"];
            ViewBag.CacheShortName = cacheShortName;

            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            #region["Tỉnh/Thành phố"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = "1",
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

            var objRQ_Mst_Province = new RQ_Mst_Province()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Province,
                Ft_Cols_Upd = null,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_RptSv_Mst_Province_Get(objRQ_Mst_Province, addressReportAPIs);
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }

            ViewBag.ListMst_Province = listMst_Province;
            #endregion

            #region["Quận/Huyện"]
            if (cacheNNT != null)
            {
                var objMst_District = new Mst_District()
                {
                    ProvinceCode = cacheNNT.ProvinceCode,
                    FlagActive = "1",
                };
                var listMst_District = new List<Mst_District>();
                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode_RptSV,
                    WAUserPassword = waUserPassword_RptSV,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };
                var objRT_Mst_District = Mst_DistrictService.Instance.WA_RptSv_Mst_District_Get(objRQ_Mst_District, addressReportAPIs);
                if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                {
                    listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                }

                ViewBag.ListMst_District = listMst_District;
            }
            #endregion


            #region["BizType"]
            var objiNOS_Mst_BizType = new iNOS_Mst_BizType()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
            var strWhereClauseiNOS_Mst_BizType = strWhereClause_iNOS_Mst_BizType(objiNOS_Mst_BizType);

            var objRQ_iNOS_Mst_BizType = new RQ_iNOS_Mst_BizType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                // RQ_iNOS_Mst_BizType
                Rt_Cols_iNOS_Mst_BizType = "*",
                iNOS_Mst_BizType = null,
            };
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_RptSv_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressReportAPIs);
            if (objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType != null && objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType.Count > 0)
            {
                listiNOS_Mst_BizType.AddRange(objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType);
            }

            ViewBag.ListiNOS_Mst_BizType = listiNOS_Mst_BizType;
            #endregion

            #region["BizField"]
            var objiNOS_Mst_BizField = new iNOS_Mst_BizField()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
            var strWhereClauseiNOS_Mst_BizField = strWhereClause_iNOS_Mst_BizField(objiNOS_Mst_BizField);

            var objRQ_iNOS_Mst_BizField = new RQ_iNOS_Mst_BizField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizField,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizField
                Rt_Cols_iNOS_Mst_BizField = "*",
                iNOS_Mst_BizField = null,
            };
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_RptSv_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressReportAPIs);

            if (objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField != null && objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField.Count > 0)
            {
                listiNOS_Mst_BizField.AddRange(objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField);
            }

            ViewBag.ListiNOS_Mst_BizField = listiNOS_Mst_BizField;
            #endregion

            #region["BizSize"]
            var objiNOS_Mst_BizSize = new iNOS_Mst_BizSize()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizSize = new List<iNOS_Mst_BizSize>();
            var strWhereClauseiNOS_Mst_BizSize = strWhereClause_iNOS_Mst_BizSize(objiNOS_Mst_BizSize);

            var objRQ_iNOS_Mst_BizSize = new RQ_iNOS_Mst_BizSize()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizSize,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizSize
                Rt_Cols_iNOS_Mst_BizSize = "*",
                iNOS_Mst_BizSize = null,
            };
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_RptSv_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressReportAPIs);
            if (objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize != null && objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize.Count > 0)
            {
                listiNOS_Mst_BizSize.AddRange(objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize);
            }

            ViewBag.ListiNOS_Mst_BizSize = listiNOS_Mst_BizSize;
            #endregion

            #region["OS_Inos_Package"]

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
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                // RQ_OS_Inos_Package
                Rt_Cols_OS_Inos_Package = "*",
                OS_Inos_Package = null
            };

            var objRT_OS_Inos_Package = OS_InosService.Instance.WA_RptSv_OS_Inos_Package_Get(objRQ_OS_Inos_Package, addressReportAPIs);
            if (objRT_OS_Inos_Package.Lst_OS_Inos_Package != null &&
                objRT_OS_Inos_Package.Lst_OS_Inos_Package.Count > 0)
            {
                listOS_Inos_Package.AddRange(objRT_OS_Inos_Package.Lst_OS_Inos_Package);
            }

            ViewBag.ListOS_Inos_Package = listOS_Inos_Package;
            #endregion

            #region ["Gen MST"]
            var mstnnt = "";
            var objSeq = new Seq_Common { SequenceType = "", Param_Postfix = "", Param_Prefix = "" };
            var objRQ_Seq_Common = new RQ_Seq_Common()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                Seq_Common = objSeq,
            };
            var seq_common = Seq_CommonService.Instance.WA_RptSv_Seq_MST_Get(objRQ_Seq_Common, addressReportAPIs);
            if (seq_common != null && seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null)
            {
                mstnnt = seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            }
            ViewBag.MST = mstnnt;
            #endregion
            
            ViewBag.Promote = promote;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string mstnnt, string osinosuser, string osinosorg, string inosmstbiztype, string inosmstbizfield, string inoslicorder)
        {
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;

            var resultEntry = new JsonResultEntry
            {
                Success = false,
                RedirectUrl = null
            };

            var exitsData = "";
            var iindex = 1;
            try
            {
                var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(mstnnt);
                var objOS_Inos_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_User>(osinosuser);
                var objOS_Inos_OrgInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_Org>(osinosorg);
                var objiNOS_Mst_BizTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<iNOS_Mst_BizType>(inosmstbiztype);
                var objiNOS_Mst_BizFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<iNOS_Mst_BizField>(inosmstbizfield);
                var objInos_LicOrderInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrder>(inoslicorder);
                

                // Lưu lại mst
                System.Web.HttpContext.Current.Session["MST"] = objMst_NNTInput.MST;
                // Lưu link report
                System.Web.HttpContext.Current.Session["AddressReportAPI"] = addressReportAPIs;
                //Lưu lại thông tin KH
                System.Web.HttpContext.Current.Session["Mst_NNT"] = objMst_NNTInput;
                System.Web.HttpContext.Current.Session["OS_Inos_Org"] = objOS_Inos_OrgInput;
                var objOS_Inos_OrgUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_OrgUI>(osinosorg);


                //var isValidate = Validate_MST_Client(objMst_NNTInput.MST, ref exitsData);
                //if (!isValidate)
                //{
                //    resultEntry.AddMessage(exitsData);
                //    resultEntry.Success = false;
                //    return Json(resultEntry);
                //}
                //else
                //{
                    #region["1. Tạo User Inos"]
                    // Thêm 1 số trường
                    objMst_NNTInput.TCTStatus = "1";
                    objMst_NNTInput.MSTParent = ClientMix.MSTRoot;
                    objMst_NNTInput.UserPassword = objOS_Inos_UserInput.Password;
                    objMst_NNTInput.UserPasswordRepeat = objOS_Inos_UserInput.Password;

                    objOS_Inos_OrgInput.ParentId = "0";
                    objOS_Inos_OrgInput.Id = "0";
                    objOS_Inos_OrgInput.Name = objMst_NNTInput.NNTFullName;
                    objOS_Inos_OrgInput.Description = null;
                    objOS_Inos_OrgInput.Enable = true;



                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = waUserCode_RptSV,
                        WAUserPassword = waUserPassword_RptSV,
                        // Tạo NNT (RptServer)
                        Mst_NNT = objMst_NNTInput,
                        // Tạo user (iNOS)
                        OS_Inos_User = new OS_Inos_User()
                        {
                            //Name = objSysUser.UserName,
                            Name = objOS_Inos_UserInput.Name,
                            Email = objOS_Inos_UserInput.Email,
                            Password = objOS_Inos_UserInput.Password,
                            VerificationCode = objOS_Inos_UserInput.VerificationCode,
                            //Language = "vi",
                            //TimeZone = "7",
                        },
                        // Tạo Network (iNOS)
                        OS_Inos_Org = objOS_Inos_OrgInput,
                        iNOS_Mst_BizType = objiNOS_Mst_BizTypeInput,
                        iNOS_Mst_BizField = objiNOS_Mst_BizFieldInput,

                        // Tạo đơn hàng
                        Inos_LicOrder = objInos_LicOrderInput,
                    };

                    // Gọi hàm tính toán thử

                    var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_NNT);
                    var objRT_Mst_NNT_Calc = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Calc(objRQ_Mst_NNT, addressReportAPIs);

                    var objRT_Mst_NNT_Add = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_Add(objRQ_Mst_NNT, addressReportAPIs);
                    #endregion

                    var objInos_LicOrderUI = new Inos_LicOrderUI();
                    #region["2. Login để lấy TokenId"]

                    var objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = TokenIDFix,
                        NetworkID = NetworkID,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        WAUserCode = objOS_Inos_UserInput.Email.ToString().Trim(),
                        WAUserPassword = objOS_Inos_UserInput.Password.ToString().Trim(),
                        // RQ_MstSv_Sys_User
                        Rt_Cols_MstSv_Sys_User = null,
                        MstSv_Sys_User = null,
                    };
                    var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_GetAccessToken(objRQ_MstSv_Sys_User, addressMasterServerAPIs);
                    if (objRT_MstSv_Sys_User != null)
                    {
                        if (objRT_MstSv_Sys_User.c_K_DT_Sys != null &&
                            objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                            objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            var c_K_DT_SysInfo = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                            if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                            {
                                System.Web.HttpContext.Current.Session["TokenID"] = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                            }
                        }
                    }

                    #endregion
                    if (objRT_Mst_NNT_Add != null && objRT_Mst_NNT_Add.Inos_LicOrder != null)
                    {
                        objInos_LicOrderUI.Id = objRT_Mst_NNT_Add.Inos_LicOrder.Id;
                        objInos_LicOrderUI.OrgId = objRT_Mst_NNT_Add.Inos_LicOrder.OrgId;
                        objInos_LicOrderUI.PaymentCode = objRT_Mst_NNT_Add.Inos_LicOrder.PaymentCode;
                        objInos_LicOrderUI.TotalCost = objRT_Mst_NNT_Add.Inos_LicOrder.TotalCost;
                        objInos_LicOrderUI.StrCreateDTime = objRT_Mst_NNT_Add.Inos_LicOrder.CreateDTime.ToString("yyyyMMddHHmmss");
                        objInos_LicOrderUI.Remark = objRT_Mst_NNT_Add.Inos_LicOrder.Remark;
                        objInos_LicOrderUI.Inos_DetailList = new List<Inos_LicOrderDetail>();
                        //System.Web.HttpContext.Current.Session["OrgID"] = objRT_OS_Inos_LicOrder.Inos_LicOrder.OrgId;
                        if (objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList != null && objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList.Count > 0)
                        {
                            objInos_LicOrderUI.Inos_DetailList.AddRange(objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList);
                        }
                    }

                    return Json(new { Success = true, Inos_LicOrder = objInos_LicOrderUI });

                //}

            }
            catch (Exception ex)
            {
                resultEntry.AddMessage(ex.StackTrace.ToString());
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #region ["Create network"]

        [HttpGet]
        public ActionResult CreateNetwork(string promote = "")
        {
            var userCodeLogin = System.Web.HttpContext.Current.Session["UserCodeLogin"] as string;
            ViewBag.UserCodeLogin = userCodeLogin;
            var cacheNNT = System.Web.HttpContext.Current.Session["Mst_NNT"] as Mst_NNT;
            ViewBag.CacheNNT = cacheNNT;
            var cacheInos_Org = System.Web.HttpContext.Current.Session["OS_Inos_Org"] as OS_Inos_Org;
            ViewBag.CacheInos_Org = cacheInos_Org;
            var cacheShortName = System.Web.HttpContext.Current.Session["ShortName"];
            ViewBag.CacheShortName = cacheShortName;

            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            #region["Tỉnh/Thành phố"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = "1",
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

            var objRQ_Mst_Province = new RQ_Mst_Province()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Province,
                Ft_Cols_Upd = null,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_RptSv_Mst_Province_Get(objRQ_Mst_Province, addressReportAPIs);
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }

            ViewBag.ListMst_Province = listMst_Province;
            #endregion

            #region["Quận/Huyện"]
            if (cacheNNT != null)
            {
                var objMst_District = new Mst_District()
                {
                    ProvinceCode = cacheNNT.ProvinceCode,
                    FlagActive = "1",
                };
                var listMst_District = new List<Mst_District>();
                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode_RptSV,
                    WAUserPassword = waUserPassword_RptSV,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };
                var objRT_Mst_District = Mst_DistrictService.Instance.WA_RptSv_Mst_District_Get(objRQ_Mst_District, addressReportAPIs);
                if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                {
                    listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                }

                ViewBag.ListMst_District = listMst_District;
            }
            #endregion


            #region["BizType"]
            var objiNOS_Mst_BizType = new iNOS_Mst_BizType()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
            var strWhereClauseiNOS_Mst_BizType = strWhereClause_iNOS_Mst_BizType(objiNOS_Mst_BizType);

            var objRQ_iNOS_Mst_BizType = new RQ_iNOS_Mst_BizType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                // RQ_iNOS_Mst_BizType
                Rt_Cols_iNOS_Mst_BizType = "*",
                iNOS_Mst_BizType = null,
            };
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_RptSv_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressReportAPIs);
            if (objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType != null && objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType.Count > 0)
            {
                listiNOS_Mst_BizType.AddRange(objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType);
            }

            ViewBag.ListiNOS_Mst_BizType = listiNOS_Mst_BizType;
            #endregion

            #region["BizField"]
            var objiNOS_Mst_BizField = new iNOS_Mst_BizField()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
            var strWhereClauseiNOS_Mst_BizField = strWhereClause_iNOS_Mst_BizField(objiNOS_Mst_BizField);

            var objRQ_iNOS_Mst_BizField = new RQ_iNOS_Mst_BizField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizField,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizField
                Rt_Cols_iNOS_Mst_BizField = "*",
                iNOS_Mst_BizField = null,
            };
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_RptSv_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressReportAPIs);

            if (objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField != null && objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField.Count > 0)
            {
                listiNOS_Mst_BizField.AddRange(objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField);
            }

            ViewBag.ListiNOS_Mst_BizField = listiNOS_Mst_BizField;
            #endregion

            #region["BizSize"]
            var objiNOS_Mst_BizSize = new iNOS_Mst_BizSize()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizSize = new List<iNOS_Mst_BizSize>();
            var strWhereClauseiNOS_Mst_BizSize = strWhereClause_iNOS_Mst_BizSize(objiNOS_Mst_BizSize);

            var objRQ_iNOS_Mst_BizSize = new RQ_iNOS_Mst_BizSize()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizSize,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizSize
                Rt_Cols_iNOS_Mst_BizSize = "*",
                iNOS_Mst_BizSize = null,
            };
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_RptSv_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressReportAPIs);
            if (objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize != null && objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize.Count > 0)
            {
                listiNOS_Mst_BizSize.AddRange(objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize);
            }

            ViewBag.ListiNOS_Mst_BizSize = listiNOS_Mst_BizSize;
            #endregion

            #region["OS_Inos_Package"]

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
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                // RQ_OS_Inos_Package
                Rt_Cols_OS_Inos_Package = "*",
                OS_Inos_Package = null
            };

            var objRT_OS_Inos_Package = OS_InosService.Instance.WA_RptSv_OS_Inos_Package_Get(objRQ_OS_Inos_Package, addressReportAPIs);
            if (objRT_OS_Inos_Package.Lst_OS_Inos_Package != null &&
                objRT_OS_Inos_Package.Lst_OS_Inos_Package.Count > 0)
            {
                listOS_Inos_Package.AddRange(objRT_OS_Inos_Package.Lst_OS_Inos_Package);
            }

            ViewBag.ListOS_Inos_Package = listOS_Inos_Package;
            #endregion

            #region ["Gen MST"]
            var mstnnt = "";
            var objSeq = new Seq_Common { SequenceType = "", Param_Postfix = "", Param_Prefix = "" };
            var objRQ_Seq_Common = new RQ_Seq_Common()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                Seq_Common = objSeq,
            };
            var seq_common = Seq_CommonService.Instance.WA_RptSv_Seq_MST_Get(objRQ_Seq_Common, addressReportAPIs);
            if (seq_common != null && seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null)
            {
                mstnnt = seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            }
            ViewBag.MST = mstnnt;
            #endregion

            ViewBag.Promote = promote;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNetwork(string mstnnt, string osinosorg, string inosmstbiztype, string inosmstbizfield, string inoslicorder)
        {
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            var waUserCode = System.Web.HttpContext.Current.Session["UserCodeLogin"] as string;
            var waUserPassword = System.Web.HttpContext.Current.Session["PasswordLogin"] as string;

            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;

            var resultEntry = new JsonResultEntry
            {
                Success = false,
                RedirectUrl = null
            };

            var exitsData = "";
            var iindex = 1;
            try
            {
                var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(mstnnt);
                var objOS_Inos_OrgInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_Org>(osinosorg);
                var objiNOS_Mst_BizTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<iNOS_Mst_BizType>(inosmstbiztype);
                var objiNOS_Mst_BizFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<iNOS_Mst_BizField>(inosmstbizfield);
                var objInos_LicOrderInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrder>(inoslicorder);


                // Lưu lại mst
                System.Web.HttpContext.Current.Session["MST"] = objMst_NNTInput.MST;
                // Lưu link report
                System.Web.HttpContext.Current.Session["AddressReportAPI"] = addressReportAPIs;
                //Lưu lại thông tin KH
                System.Web.HttpContext.Current.Session["Mst_NNT"] = objMst_NNTInput;
                System.Web.HttpContext.Current.Session["OS_Inos_Org"] = objOS_Inos_OrgInput;
                var objOS_Inos_OrgUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_OrgUI>(osinosorg);


                //var isValidate = Validate_MST_Client(objMst_NNTInput.MST, ref exitsData);
                //if (!isValidate)
                //{
                //    resultEntry.AddMessage(exitsData);
                //    resultEntry.Success = false;
                //    return Json(resultEntry);
                //}
                //else
                //{
                #region["1. Tính toán thử"]
                // Thêm 1 số trường
                objMst_NNTInput.TCTStatus = "1";
                objMst_NNTInput.MSTParent = ClientMix.MSTRoot;
                objMst_NNTInput.UserPassword = waUserPassword;
                objMst_NNTInput.UserPasswordRepeat = waUserPassword;

                objOS_Inos_OrgInput.ParentId = "0";
                objOS_Inos_OrgInput.Id = null;
                objOS_Inos_OrgInput.Name = objMst_NNTInput.NNTFullName;
                objOS_Inos_OrgInput.Description = null;
                objOS_Inos_OrgInput.Enable = true;



                var objRQ_Mst_NNT = new RQ_Mst_NNT()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode_RptSV,
                    WAUserPassword = waUserPassword_RptSV,
                    // Tạo NNT (RptServer)
                    Mst_NNT = objMst_NNTInput,
                    // Tạo user (iNOS)
                    //OS_Inos_User = new OS_Inos_User()
                    //{
                    //    Name = objMst_NNTInput.ContactName,
                    //    Email = objMst_NNTInput.ContactEmail,
                    //    Password = waUserPassword,
                    //    //VerificationCode = objOS_Inos_UserInput.VerificationCode,
                    //},
                    // Tạo Network (iNOS)
                    OS_Inos_Org = objOS_Inos_OrgInput,
                    iNOS_Mst_BizType = objiNOS_Mst_BizTypeInput,
                    iNOS_Mst_BizField = objiNOS_Mst_BizFieldInput,

                    // Tạo đơn hàng
                    Inos_LicOrder = objInos_LicOrderInput,
                };

                // Gọi hàm tính toán thử

                var objRT_Mst_NNT_Calc = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_CalcByUserExist(objRQ_Mst_NNT, addressReportAPIs);

                #endregion

                #region["2. Login để lấy TokenId"]
                var objInos_LicOrderUI = new Inos_LicOrderUI();

                var objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = TokenIDFix,
                    NetworkID = NetworkID,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    // RQ_MstSv_Sys_User
                    Rt_Cols_MstSv_Sys_User = null,
                    MstSv_Sys_User = null,
                };
                var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_GetAccessToken(objRQ_MstSv_Sys_User, addressMasterServerAPIs);
                if (objRT_MstSv_Sys_User != null)
                {
                    if (objRT_MstSv_Sys_User.c_K_DT_Sys != null &&
                        objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                        objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                    {
                        var c_K_DT_SysInfo = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                        if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                        {
                            System.Web.HttpContext.Current.Session["TokenID"] = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                            objRQ_Mst_NNT.AccessToken = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                        }
                    }
                }

                #endregion

                #region[3.Tạo NNT]

                var objRT_Mst_NNT_Add = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_AddByUserExist(objRQ_Mst_NNT, addressReportAPIs);
                if (objRT_Mst_NNT_Add != null && objRT_Mst_NNT_Add.Inos_LicOrder != null)
                {
                    objInos_LicOrderUI.Id = objRT_Mst_NNT_Add.Inos_LicOrder.Id;
                    objInos_LicOrderUI.OrgId = objRT_Mst_NNT_Add.Inos_LicOrder.OrgId;
                    objInos_LicOrderUI.PaymentCode = objRT_Mst_NNT_Add.Inos_LicOrder.PaymentCode;
                    objInos_LicOrderUI.TotalCost = objRT_Mst_NNT_Add.Inos_LicOrder.TotalCost;
                    objInos_LicOrderUI.StrCreateDTime = objRT_Mst_NNT_Add.Inos_LicOrder.CreateDTime.ToString("yyyyMMddHHmmss");
                    objInos_LicOrderUI.Remark = objRT_Mst_NNT_Add.Inos_LicOrder.Remark;
                    objInos_LicOrderUI.Inos_DetailList = new List<Inos_LicOrderDetail>();
                    //System.Web.HttpContext.Current.Session["OrgID"] = objRT_OS_Inos_LicOrder.Inos_LicOrder.OrgId;
                    if (objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList != null && objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList.Count > 0)
                    {
                        objInos_LicOrderUI.Inos_DetailList.AddRange(objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList);
                    }
                }
                #endregion

                return Json(new { Success = true, Inos_LicOrder = objInos_LicOrderUI });

                //}

            }
            catch (Exception ex)
            {
                resultEntry.AddMessage(ex.StackTrace.ToString());
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        #endregion

        #region ["Create network no create org"]
        [AllowAnonymous]
        [HttpGet]
        public ActionResult CreateNetworkNoCreateOrg(string inosNetwork, string promote = "")
        {
            var userCodeLogin = System.Web.HttpContext.Current.Session["UserCodeLogin"] as string;
            ViewBag.UserCodeLogin = userCodeLogin;
            var cacheNNT = System.Web.HttpContext.Current.Session["Mst_NNT"] as Mst_NNT;
            ViewBag.CacheNNT = cacheNNT;
            var cacheInos_Org = System.Web.HttpContext.Current.Session["OS_Inos_Org"] as OS_Inos_Org;
            ViewBag.CacheInos_Org = cacheInos_Org;
            var cacheShortName = System.Web.HttpContext.Current.Session["ShortName"];
            ViewBag.CacheShortName = cacheShortName;
            ViewBag.InosNetwork = inosNetwork;

            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            #region["Tỉnh/Thành phố"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = "1",
            };
            var listMst_Province = new List<Mst_Province>();
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);

            var objRQ_Mst_Province = new RQ_Mst_Province()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Province,
                Ft_Cols_Upd = null,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_RptSv_Mst_Province_Get(objRQ_Mst_Province, addressReportAPIs);
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }

            ViewBag.ListMst_Province = listMst_Province;
            #endregion

            #region["Quận/Huyện"]
            if (cacheNNT != null)
            {
                var objMst_District = new Mst_District()
                {
                    ProvinceCode = cacheNNT.ProvinceCode,
                    FlagActive = "1",
                };
                var listMst_District = new List<Mst_District>();
                var strWhereClauseMst_District = strWhereClause_Mst_District(objMst_District);

                var objRQ_Mst_District = new RQ_Mst_District()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode_RptSV,
                    WAUserPassword = waUserPassword_RptSV,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_District,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_District
                    Rt_Cols_Mst_District = "*",
                    Mst_District = null,
                };
                var objRT_Mst_District = Mst_DistrictService.Instance.WA_RptSv_Mst_District_Get(objRQ_Mst_District, addressReportAPIs);
                if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                {
                    listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                }

                ViewBag.ListMst_District = listMst_District;
            }
            #endregion

            #region["BizType"]
            var objiNOS_Mst_BizType = new iNOS_Mst_BizType()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizType = new List<iNOS_Mst_BizType>();
            var strWhereClauseiNOS_Mst_BizType = strWhereClause_iNOS_Mst_BizType(objiNOS_Mst_BizType);

            var objRQ_iNOS_Mst_BizType = new RQ_iNOS_Mst_BizType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                // RQ_iNOS_Mst_BizType
                Rt_Cols_iNOS_Mst_BizType = "*",
                iNOS_Mst_BizType = null,
            };
            var objRT_iNOS_Mst_BizType = Mst_BizTypeService.Instance.WA_RptSv_iNOS_Mst_BizType_Get(objRQ_iNOS_Mst_BizType, addressReportAPIs);
            if (objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType != null && objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType.Count > 0)
            {
                listiNOS_Mst_BizType.AddRange(objRT_iNOS_Mst_BizType.Lst_iNOS_Mst_BizType);
            }

            ViewBag.ListiNOS_Mst_BizType = listiNOS_Mst_BizType;
            #endregion

            #region["BizField"]
            var objiNOS_Mst_BizField = new iNOS_Mst_BizField()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizField = new List<iNOS_Mst_BizField>();
            var strWhereClauseiNOS_Mst_BizField = strWhereClause_iNOS_Mst_BizField(objiNOS_Mst_BizField);

            var objRQ_iNOS_Mst_BizField = new RQ_iNOS_Mst_BizField()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizField,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizField
                Rt_Cols_iNOS_Mst_BizField = "*",
                iNOS_Mst_BizField = null,
            };
            var objRT_iNOS_Mst_BizField = Mst_BizFieldService.Instance.WA_RptSv_iNOS_Mst_BizField_Get(objRQ_iNOS_Mst_BizField, addressReportAPIs);

            if (objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField != null && objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField.Count > 0)
            {
                listiNOS_Mst_BizField.AddRange(objRT_iNOS_Mst_BizField.Lst_iNOS_Mst_BizField);
            }

            ViewBag.ListiNOS_Mst_BizField = listiNOS_Mst_BizField;
            #endregion

            #region["BizSize"]
            var objiNOS_Mst_BizSize = new iNOS_Mst_BizSize()
            {
                FlagActive = "1",
            };
            var listiNOS_Mst_BizSize = new List<iNOS_Mst_BizSize>();
            var strWhereClauseiNOS_Mst_BizSize = strWhereClause_iNOS_Mst_BizSize(objiNOS_Mst_BizSize);

            var objRQ_iNOS_Mst_BizSize = new RQ_iNOS_Mst_BizSize()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseiNOS_Mst_BizSize,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                //UtcOffset = UserState.UtcOffset,
                // RQ_iNOS_Mst_BizSize
                Rt_Cols_iNOS_Mst_BizSize = "*",
                iNOS_Mst_BizSize = null,
            };
            var objRT_iNOS_Mst_BizSize = Mst_BizSizeService.Instance.WA_RptSv_iNOS_Mst_BizSize_Get(objRQ_iNOS_Mst_BizSize, addressReportAPIs);
            if (objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize != null && objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize.Count > 0)
            {
                listiNOS_Mst_BizSize.AddRange(objRT_iNOS_Mst_BizSize.Lst_iNOS_Mst_BizSize);
            }

            ViewBag.ListiNOS_Mst_BizSize = listiNOS_Mst_BizSize;
            #endregion

            #region["OS_Inos_Package"]

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
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                // RQ_OS_Inos_Package
                Rt_Cols_OS_Inos_Package = "*",
                OS_Inos_Package = null
            };

            var objRT_OS_Inos_Package = OS_InosService.Instance.WA_RptSv_OS_Inos_Package_Get(objRQ_OS_Inos_Package, addressReportAPIs);
            if (objRT_OS_Inos_Package.Lst_OS_Inos_Package != null &&
                objRT_OS_Inos_Package.Lst_OS_Inos_Package.Count > 0)
            {
                listOS_Inos_Package.AddRange(objRT_OS_Inos_Package.Lst_OS_Inos_Package);
            }

            ViewBag.ListOS_Inos_Package = listOS_Inos_Package;
            #endregion

            #region ["Gen MST"]
            var mstnnt = "";
            var objSeq = new Seq_Common { SequenceType = "", Param_Postfix = "", Param_Prefix = "" };
            var objRQ_Seq_Common = new RQ_Seq_Common()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode_RptSV,
                WAUserPassword = waUserPassword_RptSV,
                Seq_Common = objSeq,
            };
            var seq_common = Seq_CommonService.Instance.WA_RptSv_Seq_MST_Get(objRQ_Seq_Common, addressReportAPIs);
            if (seq_common != null && seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null)
            {
                mstnnt = seq_common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            }
            ViewBag.MST = mstnnt;
            #endregion

            ViewBag.Promote = promote;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNetworkNoCreateOrg(string mstnnt, string osinosorg, string inosmstbiztype, string inosmstbizfield, string inoslicorder, string inosNetwork)
        {
            var waUserCode = System.Web.HttpContext.Current.Session["UserCodeLogin"] as string;
            var waUserPassword = System.Web.HttpContext.Current.Session["PasswordLogin"] as string;


            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;

            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;

            var resultEntry = new JsonResultEntry
            {
                Success = false,
                RedirectUrl = null
            };

            var exitsData = "";
            var iindex = 1;
            try
            {
                var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(mstnnt);
                var objOS_Inos_OrgInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_Org>(osinosorg);
                var objiNOS_Mst_BizTypeInput = Newtonsoft.Json.JsonConvert.DeserializeObject<iNOS_Mst_BizType>(inosmstbiztype);
                var objiNOS_Mst_BizFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<iNOS_Mst_BizField>(inosmstbizfield);
                var objInos_LicOrderInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrder>(inoslicorder);

                // Lưu lại mst
                System.Web.HttpContext.Current.Session["MST"] = objMst_NNTInput.MST;
                // Lưu link report
                System.Web.HttpContext.Current.Session["AddressReportAPI"] = addressReportAPIs;
                //Lưu lại thông tin KH
                System.Web.HttpContext.Current.Session["Mst_NNT"] = objMst_NNTInput;
                System.Web.HttpContext.Current.Session["OS_Inos_Org"] = objOS_Inos_OrgInput;
                var objOS_Inos_OrgUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<OS_Inos_OrgUI>(osinosorg);

                //var isValidate = Validate_MST_Client(objMst_NNTInput.MST, ref exitsData);
                //if (!isValidate)
                //{
                //    resultEntry.AddMessage(exitsData);
                //    resultEntry.Success = false;
                //    return Json(resultEntry);
                //}
                //else
                //{
                    #region["1. Tính toán thử"]
                    // Thêm 1 số trường
                    objMst_NNTInput.TCTStatus = "1";
                    objMst_NNTInput.MSTParent = ClientMix.MSTRoot;
                    objMst_NNTInput.UserPassword = waUserPassword;
                    objMst_NNTInput.UserPasswordRepeat = waUserPassword;

                    objOS_Inos_OrgInput.ParentId = "0";
                    objOS_Inos_OrgInput.Id = inosNetwork;
                    objOS_Inos_OrgInput.Name = objMst_NNTInput.NNTFullName;
                    objOS_Inos_OrgInput.Description = null;
                    objOS_Inos_OrgInput.Enable = true;



                    var objRQ_Mst_NNT = new RQ_Mst_NNT()
                    {
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = waUserCode_RptSV,
                        WAUserPassword = waUserPassword_RptSV,
                        // Tạo NNT (RptServer)
                        Mst_NNT = objMst_NNTInput,
                        // Tạo user (iNOS)
                        //OS_Inos_User = new OS_Inos_User()
                        //{
                        //    Name = objMst_NNTInput.ContactName,
                        //    Email = objMst_NNTInput.ContactEmail,
                        //    Password = waUserPassword,
                        //    //VerificationCode = objOS_Inos_UserInput.VerificationCode,
                        //},
                        // Tạo Network (iNOS)
                        OS_Inos_Org = objOS_Inos_OrgInput,
                        iNOS_Mst_BizType = objiNOS_Mst_BizTypeInput,
                        iNOS_Mst_BizField = objiNOS_Mst_BizFieldInput,

                        // Tạo đơn hàng
                        Inos_LicOrder = objInos_LicOrderInput,
                    };

                    // Gọi hàm tính toán thử

                    var objRT_Mst_NNT_Calc = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_CalcByUserExist(objRQ_Mst_NNT, addressReportAPIs);

                    #endregion

                    #region["2. Login để lấy TokenId"]
                    var objInos_LicOrderUI = new Inos_LicOrderUI();

                    var objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        AccessToken = TokenIDFix,
                        NetworkID = NetworkID,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        // RQ_MstSv_Sys_User
                        Rt_Cols_MstSv_Sys_User = null,
                        MstSv_Sys_User = null,
                    };
                    var objRT_MstSv_Sys_User = MasterServerService.Instance.WA_MstSv_Sys_User_GetAccessToken(objRQ_MstSv_Sys_User, addressMasterServerAPIs);
                    if (objRT_MstSv_Sys_User != null)
                    {
                        if (objRT_MstSv_Sys_User.c_K_DT_Sys != null &&
                            objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                            objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                        {
                            var c_K_DT_SysInfo = objRT_MstSv_Sys_User.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                            if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                            {
                                System.Web.HttpContext.Current.Session["TokenID"] = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                                objRQ_Mst_NNT.AccessToken = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                            }
                        }
                    }

                    #endregion

                    #region[3.Tạo NNT]

                    var objRT_Mst_NNT_Add = Mst_NNTService.Instance.WA_RptSv_Mst_NNT_AddByUserExist(objRQ_Mst_NNT, addressReportAPIs);
                    if (objRT_Mst_NNT_Add != null && objRT_Mst_NNT_Add.Inos_LicOrder != null)
                    {
                        objInos_LicOrderUI.Id = objRT_Mst_NNT_Add.Inos_LicOrder.Id;
                        objInos_LicOrderUI.OrgId = objRT_Mst_NNT_Add.Inos_LicOrder.OrgId;
                        objInos_LicOrderUI.PaymentCode = objRT_Mst_NNT_Add.Inos_LicOrder.PaymentCode;
                        objInos_LicOrderUI.TotalCost = objRT_Mst_NNT_Add.Inos_LicOrder.TotalCost;
                        objInos_LicOrderUI.StrCreateDTime = objRT_Mst_NNT_Add.Inos_LicOrder.CreateDTime.ToString("yyyyMMddHHmmss");
                        objInos_LicOrderUI.Remark = objRT_Mst_NNT_Add.Inos_LicOrder.Remark;
                        objInos_LicOrderUI.Inos_DetailList = new List<Inos_LicOrderDetail>();
                        //System.Web.HttpContext.Current.Session["OrgID"] = objRT_OS_Inos_LicOrder.Inos_LicOrder.OrgId;
                        if (objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList != null && objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList.Count > 0)
                        {
                            objInos_LicOrderUI.Inos_DetailList.AddRange(objRT_Mst_NNT_Add.Inos_LicOrder.Inos_DetailList);
                        }
                    }
                    #endregion

                    return Json(new { Success = true, Inos_LicOrder = objInos_LicOrderUI });

                //}

            }
            catch (Exception ex)
            {
                resultEntry.AddMessage(ex.StackTrace.ToString());
                resultEntry.Success = false;
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(string model, string grecaptcharesponse)
        {
            var resultEntry = new JsonResultEntry() { Success = false };


            var exitsData = "";
            try
            {
                string secretKey = ReCaptchaPrivateKey;
                var client = new WebClient();
                var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, grecaptcharesponse));
                var obj = JObject.Parse(result);
                var status = (bool)obj.SelectToken("success");
                if (status)
                {
                    var objMst_NNTInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_NNT>(model);
                    #region["Kiểm tra mã số thuế"]
                    if (objMst_NNTInput != null)
                    {
                        exitsData = "";
                        var isValidate = Validate_MST_Client(objMst_NNTInput.MST, ref exitsData);
                        if (CUtils.IsNullOrEmpty(objMst_NNTInput.MST))
                        {
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            exitsData = "";
                            // gọi API check trạng thái hoạt động của mã số thuế
                            isValidate = Validate_MST_API(objMst_NNTInput.MST, ref exitsData);
                            if (!isValidate)
                            {
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }

                            resultEntry.Success = true;
                        }
                    }
                    else
                    {
                        exitsData = "Mã số thuế trống!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    #endregion
                }
                else
                {
                    exitsData = "Mã captcha không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);

            return Json(resultEntry);
        }

        #region["Gọi hàm gửi mail lấy mã xác thực"]

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SendEmailVerificationEmail(string email)
        {
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;
            var resultEntry = new JsonResultEntry() { Success = false };

            var exitsData = "";
            try
            {
                if (!CUtils.IsNullOrEmpty(email))
                {
                    var strEmailTemplate = BodyEMail.GetContentMailVerificationEmail(email);
                    var objRQ_VerificationEmail = new RQ_VerificationEmail()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = waUserCode_RptSV,
                        WAUserPassword = waUserPassword_RptSV,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        // RQ_VerificationEmail
                        Rt_Cols_VerificationEmail = null,
                        VerificationEmail = new VerificationEmail()
                        {
                            email = email,
                            emailSubject = "Xác thực địa chỉ email",
                            emailTemplate = strEmailTemplate,
                        },
                    };

                    var objRT_VerificationEmail = OS_InosService.Instance.WA_OS_Inos_SendEmailVerificationEmail(objRQ_VerificationEmail, addressReportAPIs);
                    if (objRT_VerificationEmail != null && objRT_VerificationEmail.c_K_DT_Sys != null &&
                        objRT_VerificationEmail.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                        objRT_VerificationEmail.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                    {
                        var remark = "";
                        var c_K_DT_SysInfo = objRT_VerificationEmail.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                        if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                        {
                            remark = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                        }

                        if (remark.ToLower().Trim().Equals("true"))
                        {
                            exitsData = "Kiểm tra email để lấy mã xác thực!";
                        }
                        else
                        {
                            exitsData = "Gửi mail không thành công!";
                        }
                    }

                    resultEntry.Success = true;

                }
                else
                {
                    exitsData = "Email liên hệ trống!";
                }
                resultEntry.AddMessage(exitsData);
                return Json(new { Success = true, Messages = exitsData, Email = email, VerificationEmailCode = "" });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (!CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                exitsData = "";
                var iindex = resultEntry.Detail.IndexOf("Email existed");
                if (iindex >= 0)
                {
                    exitsData = "Email '" + email + "' đã tồn tại. Vui lòng liên hệ idocNet để được hỗ trợ";
                }
                return Json(new { Success = false, Detail = exitsData, RedirectUrl = Url.Action("Login", "Account") });
            }
            else
            {
                return Json(new { Success = false, Detail = resultEntry.Detail });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VerifyEmail(string email, string code)
        {
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;
            var resultEntry = new JsonResultEntry() { Success = false };

            var exitsData = "";
            try
            {
                if (!CUtils.IsNullOrEmpty(email))
                {
                    //var strEmailTemplate = BodyEMail.GetContentMailVerificationEmail(email);
                    var objRQ_VerificationEmail = new RQ_VerificationEmail()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = waUserCode_RptSV,
                        WAUserPassword = waUserPassword_RptSV,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = null,
                        Ft_Cols_Upd = null,
                        // RQ_VerificationEmail
                        Rt_Cols_VerificationEmail = null,
                        VerificationEmail = new VerificationEmail()
                        {
                            email = email,
                            code = code,
                        },
                    };

                    var objRT_VerificationEmail = OS_InosService.Instance.WA_OS_Inos_VerifyEmail(objRQ_VerificationEmail, addressReportAPIs);
                    exitsData = "Đã có lỗi trong quá trình xác thực";
                    if (objRT_VerificationEmail != null && objRT_VerificationEmail.c_K_DT_Sys != null &&
                        objRT_VerificationEmail.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                        objRT_VerificationEmail.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                    {
                        var remark = "";
                        var c_K_DT_SysInfo = objRT_VerificationEmail.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                        if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfo.Remark))
                        {
                            remark = CUtils.StrValue(c_K_DT_SysInfo.Remark);
                        }

                        if (remark.ToLower().Trim().Equals("true"))
                        {
                            exitsData = "Email xác thực thành công!";
                        }
                        else
                        {
                            exitsData = "Đã có lỗi trong quá trình xác thực!";
                        }
                    }
                    resultEntry.Success = true;

                }
                else
                {
                    exitsData = "Email liên hệ trống!";
                }
                resultEntry.AddMessage(exitsData);
                return Json(new { Success = true, Messages = exitsData, Email = email, VerificationEmailCode = code });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);

            return Json(new { Success = false, Detail = resultEntry.Messages });
        }
        #endregion

        #endregion

        #region["Active Account"]
        [HttpGet]
        public ActionResult ActiveAccount(string uuid, string networkid)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var detailErr = "";
            try
            {
                var waUserCode_MstSV = WAUserCode_MstSV;
                var waUserPassword_MstSV = WAUserPassword_MstSV;
                var wAUserCode_Network = WAUserCode_Network;
                var wAUserPassword_Network = WAUserPassword_Network;

                var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
                var baseServiceAddress = "";
                #region["Gọi hàm WA_MstSv_Sys_User_Login để lấy APIs tương ứng"]
                var objRQ_MstSv_Sys_UserCur = new RQ_MstSv_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode_MstSV,
                    WAUserPassword = waUserPassword_MstSV,
                    AccessToken = null,
                    NetworkID = networkid,
                    OrgID = networkid,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    FuncType = null,
                };
                var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_UserCur, addressMasterServerAPIs);
                if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null &&
                    objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                    objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
                {
                    var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                    if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                    {
                        baseServiceAddress = CUtils.StrValue(c_K_DT_SysInfoCur.Remark);
                    }
                }
                #endregion
                #region["Gọi hàm WA_Sys_User_Activate để active user"]

                var objRQ_Sys_User = new RQ_Sys_User()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = wAUserCode_Network,
                    WAUserPassword = wAUserPassword_Network,
                    AccessToken = null,
                    NetworkID = networkid,
                    OrgID = networkid,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    FuncType = null,
                    //RQ_Sys_User
                    Rt_Cols_Sys_User = null,
                    Rt_Cols_Sys_UserInGroup = null,
                    Sys_User = new Sys_User() { UUID = uuid },
                };

                var objRT_Sys_User = Sys_UserService.Instance.WA_Sys_User_Activate(objRQ_Sys_User, baseServiceAddress);
                #endregion

                ViewBag.Domain = System.Configuration.ConfigurationManager.AppSettings["Domain"];
                ViewBag.Success = "1"; // true
            }
            catch (Exception ex)
            {
                ViewBag.Success = "0"; // false
                resultEntry.SetFailed().AddException(ex);
                detailErr = resultEntry.Detail;
            }

            ViewBag.DetailErr = detailErr;


            return View();
        }
        #endregion

        #region["Gọi hàm get mã giảm giá"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OS_Inos_OrderService_GetDiscountCode(string discountcode, string dealercode)
        {
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var waUserCode_RptSV = WAUserCode_RptSV;
            var waUserPassword_RptSV = WAUserPassword_RptSV;
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
                        WAUserCode = waUserCode_RptSV,
                        WAUserPassword = waUserPassword_RptSV,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_Cols_Upd = null,
                        Rt_Cols_Map_DealerDiscount = "*",
                        Ft_WhereClause = strWhereClause
                    };
                    var objRTRptSv_Map_DealerDiscount = Map_DealerDiscountService.Instance.WA_RptSv_Map_DealerDiscount_Get(objRQ_Map_DealerDiscount, addressReportAPIs);
                    if (objRTRptSv_Map_DealerDiscount != null && objRTRptSv_Map_DealerDiscount.Lst_Map_DealerDiscount != null)
                    {
                        var objDiscount = new Map_DealerDiscount();
                        if (objRTRptSv_Map_DealerDiscount.Lst_Map_DealerDiscount.Count > 0)
                        {
                            objDiscount = objRTRptSv_Map_DealerDiscount.Lst_Map_DealerDiscount[0];
                            if (objDiscount.FlagActive.ToString() == "1")
                            {
                                var objRQ_OS_Inos_LicOrder = new RQ_OS_Inos_LicOrder()
                                {
                                    // WARQBase
                                    Tid = GetNextTId(),
                                    GwUserCode = GwUserCode,
                                    GwPassword = GwPassword,
                                    WAUserCode = waUserCode_RptSV,
                                    WAUserPassword = waUserPassword_RptSV,
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
                                var objRT_OS_Inos_LicOrder = OS_InosService.Instance.WA_RptSv_OS_Inos_OrderService_GetDiscountCode(objRQ_OS_Inos_LicOrder, addressReportAPIs);
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

            return Json(new { Success = false, Detail = resultEntry.Detail });
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ShowPopupGenNetwork()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                return JsonView("ShowPopupGenNetwork");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupGenNetwork", null, resultEntry);
        }
        #endregion

        #region Payment
        [HttpPost]
        public ActionResult CreatePayment(string Inos_LicOrder, string language, string bankcode, string ordertype)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            Inos_LicOrderUI objInos_LicOrder = new Inos_LicOrderUI();
            try
            {
                if (Inos_LicOrder != null)
                {
                    objInos_LicOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrderUI>(Inos_LicOrder);
                }
                System.Web.HttpContext.Current.Session["orderId"] = objInos_LicOrder.Id;
                string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"];
                string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
                string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"];
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

        public ActionResult VnPayReturn()
        {
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

                //gen network
                //var status = objInos_LicOrderCur.Status.ToString().ToUpper();
                //while (status != "APPROVED")
                //{
                //    objInos_LicOrderCur = GetOrderStatus();
                //    System.Threading.Thread.Sleep(20000);
                //}

                //GenNetwork();
            }
            else
            {
                ViewBag.msg = "<p style='color:red'>Không tìm thấy giao dịch</p>";
            }

            return View();
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
                    var error = "";
                    var check = GenNetwork(ref error);
                    if (check)
                    {
                        resultEntry.RedirectUrl = Url.Action("Login", "Account");
                    }
                    else
                    {
                        resultEntry.Detail = error;
                    }

                }
                else
                {
                    resultEntry.RedirectUrl = "";
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

        public Inos_LicOrder GetOrderStatus()
        {
            var objInos_LicOrder = new Inos_LicOrder();
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
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
                WAUserCode = WAUserCode_RptSV,
                WAUserPassword = WAUserPassword_RptSV,
                AccessToken = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]),
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount
            };

            var rt = OS_InosService.Instance.WA_RptSv_OS_Inos_OrderService_CheckOrderStatus(rq, addressReportAPIs);
            if (rt != null && rt.Inos_LicOrder != null)
            {
                objInos_LicOrder = rt.Inos_LicOrder;
            }
            return objInos_LicOrder;
        }

        public bool GenNetwork(ref string error)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                #region Gen network
                var tokenID = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]);
                var mst = CUtils.StrValue(System.Web.HttpContext.Current.Session["MST"]);
                //var addressAPIs = CUtils.StrValue(System.Web.HttpContext.Current.Session["AddressReportAPI"]);
                var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
                var objRQ_MstSv_Mst_Network = new RQ_MstSv_Mst_Network()
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
                    WAUserCode = WAUserCode_RptSV,
                    WAUserPassword = WAUserPassword_RptSV,
                    AccessToken = tokenID,
                    // RQ_MstSv_Mst_Network
                    MstSv_Mst_Network = new MstSv_Mst_Network() { MST = mst }
                };

                MstSv_Mst_NetworkService.Instance.WA_MstSv_Mst_Network_Gen(objRQ_MstSv_Mst_Network, addressReportAPIs);
                return true;
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            error = resultEntry.Detail;
            return false;

            #endregion
        }

        public ActionResult VnPayIpn()
        {
            string returnContent = string.Empty;
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            if (Request.QueryString.Count > 0)
            {
                var vnpayData = Request.QueryString;
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                VnPayLibrary vnpay = new VnPayLibrary();
                if (vnpayData.Count > 0)
                {
                    foreach (string s in vnpayData)
                    {
                        //get all querystring data
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }

                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                int vnpayTranId = Convert.ToInt32(vnpay.GetResponseData("vnp_TransactionNo"));

                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                int vnpTranId = Convert.ToInt32(vnpay.GetResponseData("vnp_TransactionNo"));

                //vnp_SecureHash: MD5 cua du lieu tra ve
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    //var orgID = CUtils.StrValue(System.Web.HttpContext.Current.Session["OrgID"]);
                    var objInos_LicOrder = new Inos_LicOrder()
                    {
                        Id = vnpay.GetResponseData("vnp_TxnRef"),
                        //OrgId = orgID
                    };
                    var rq = new RQ_OS_Inos_LicOrder()
                    {
                        Inos_LicOrder = objInos_LicOrder,
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        WAUserCode = WAUserCode_RptSV,
                        WAUserPassword = WAUserPassword_RptSV,
                        AccessToken = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount
                    };

                    var rt = OS_InosService.Instance.WA_RptSv_OS_Inos_OrderService_CheckOrderStatus(rq, addressReportAPIs);
                    var order = new Inos_LicOrder();
                    if (rt != null && rt.Inos_LicOrder != null)
                    {
                        order = rt.Inos_LicOrder;
                    }
                    if (order != null)
                    {
                        if (order.Status == 0)
                        {
                            if (vnp_ResponseCode == "00")
                            {
                                returnContent = "{\"RspCode\":\"00\",\"Message\":\"Confirm Success\"}";
                            }
                            else
                            {
                                returnContent = "{\"RspCode\":\"00\",\"Message\":\"Confirm Success\"}";
                            }
                            #region Thêm code Thực hiện cập nhật vào Database     
                            var strRT_PmtOrd_PaymentOrderVnp = "";
                            try
                            {
                                JObject objPmtOrd_PaymentOrderVnp = new JObject();
                                objPmtOrd_PaymentOrderVnp["PmtOrdNo"] = order.PaymentCode == null ? "" : order.PaymentCode.ToString();
                                objPmtOrd_PaymentOrderVnp["Vnp_Amount"] = vnpayData["vnp_Amount"];
                                objPmtOrd_PaymentOrderVnp["Vnp_BankCode"] = vnpayData["vnp_BankCode"];
                                objPmtOrd_PaymentOrderVnp["Vnp_BankTranNo"] = vnpayData["vnp_BankTranNo"];
                                objPmtOrd_PaymentOrderVnp["Vnp_CardType"] = vnpayData["vnp_CardType"];
                                objPmtOrd_PaymentOrderVnp["Vnp_OrderInfo"] = vnpayData["vnp_OrderInfo"];
                                objPmtOrd_PaymentOrderVnp["Vnp_PayDate"] = vnpayData["vnp_PayDate"];
                                objPmtOrd_PaymentOrderVnp["Vnp_ResponseCode"] = vnpayData["vnp_ResponseCode"];
                                objPmtOrd_PaymentOrderVnp["Vnp_TmnCode"] = vnpayData["vnp_TmnCode"];
                                objPmtOrd_PaymentOrderVnp["Vnp_TransactionNo"] = vnpayData["vnp_TransactionNo"];
                                objPmtOrd_PaymentOrderVnp["Vnp_TxnRef"] = vnpayData["vnp_TxnRef"];
                                objPmtOrd_PaymentOrderVnp["Vnp_SecureHashType"] = vnpayData["vnp_SecureHashType"];
                                objPmtOrd_PaymentOrderVnp["Vnp_SecureHash"] = vnpayData["vnp_HashSecret"];

                                JObject RQ_PmtOrd_PaymentOrderVnp = new JObject();
                                RQ_PmtOrd_PaymentOrderVnp["Tid"] = GetNextTId();
                                RQ_PmtOrd_PaymentOrderVnp["NetworkID"] = "0";
                                RQ_PmtOrd_PaymentOrderVnp["GwUserCode"] = GwUserCode_PayGate;
                                RQ_PmtOrd_PaymentOrderVnp["GwPassword"] = GwPassword_PayGate;
                                RQ_PmtOrd_PaymentOrderVnp["WAUserCode"] = WAUserCode_PayGate;
                                RQ_PmtOrd_PaymentOrderVnp["WAUserPassword"] = WAPassword_PayGate;
                                RQ_PmtOrd_PaymentOrderVnp["PmtOrd_PaymentOrderVnp"] = objPmtOrd_PaymentOrderVnp;
                                var strRQ_PmtOrd_PaymentOrderVnp = RQ_PmtOrd_PaymentOrderVnp.ToString();
                                strRT_PmtOrd_PaymentOrderVnp = new PayGateService().WA_PmtOrd_PaymentOrderVnp_Add(strRQ_PmtOrd_PaymentOrderVnp);

                                #region Gen network
                                var tokenID = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]);
                                var mst = CUtils.StrValue(System.Web.HttpContext.Current.Session["MST"]);
                                var addressAPIs = CUtils.StrValue(System.Web.HttpContext.Current.Session["AddressReportAPI"]);
                                var objRQ_MstSv_Mst_Network = new RQ_MstSv_Mst_Network()
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
                                    WAUserCode = WAUserCode_RptSV,
                                    WAUserPassword = WAUserPassword_RptSV,
                                    AccessToken = tokenID,
                                    // RQ_MstSv_Mst_Network
                                    MstSv_Mst_Network = new MstSv_Mst_Network() { MST = mst }
                                };

                                MstSv_Mst_NetworkService.Instance.WA_MstSv_Mst_Network_Gen(objRQ_MstSv_Mst_Network, addressAPIs);
                                #endregion
                            }
                            catch (Exception ex)
                            {
                                ErrWriteLog(ex.ToString(), strRT_PmtOrd_PaymentOrderVnp, "WA_PmtOrd_PaymentOrderVnp_Add");
                            }
                            #endregion Update Database    
                        }
                        else
                        {
                            returnContent = "{\"RspCode\":\"02\",\"Message\":\"Order already confirmed\"}";
                        }
                    }
                    else
                    {
                        returnContent = "{\"RspCode\":\"01\",\"Message\":\"Order not found\"}";
                    }
                }
                else
                {
                    returnContent = "{\"RspCode\":\"97\",\"Message\":\"Invalid signature\"}";
                }
            }
            else
            {
                returnContent = "{\"RspCode\":\"99\",\"Message\":\"Input data required\"}";
            }
            return Content(returnContent);
        }
        
        public void ErrWriteLog(string msg, string stackTrace, string methodEx)
        {
            try
            {
                string startupPath = AppDomain.CurrentDomain.BaseDirectory;
                string fileName = startupPath + (startupPath.EndsWith(@"\") ? "" : @"\") + "ErrLog.txt";
                string strErr = "Method: " + methodEx;
                strErr += "\r\n" + "Message: " + msg;
                strErr += "\r\n" + "StackTrace: " + stackTrace;
                strErr += "\r\n" + "====================";
                if (System.IO.File.Exists(fileName) == true)
                    System.IO.File.AppendAllText(fileName, "\r\n" + strErr, System.Text.Encoding.UTF8);
                else
                    System.IO.File.WriteAllText(fileName, strErr, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
            }
        }

        public ActionResult CheckOrderStatus(string Inos_LicOrder)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var addressReportAPIs = ServiceAddress.BaseReportServerAPIAddress;
            var objInos_LicOrder = new Inos_LicOrder();
            try
            {
                if (Inos_LicOrder != null)
                {
                    objInos_LicOrder = Newtonsoft.Json.JsonConvert.DeserializeObject<Inos_LicOrder>(Inos_LicOrder);
                }

                //var objInos_LicOrder = new Inos_LicOrder()
                //{
                //    Id = orderId,
                //    OrgId = orgId,                    
                //    DiscountCode = discountCode,
                //    TotalCost = totalCost,
                //    Inos_DetailList = lstInos_LicOrderDetail
                //};               

                var rq = new RQ_OS_Inos_LicOrder()
                {
                    Inos_LicOrder = objInos_LicOrder,
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = WAUserCode_RptSV,
                    WAUserPassword = WAUserPassword_RptSV,
                    AccessToken = CUtils.StrValue(System.Web.HttpContext.Current.Session["TokenID"]),
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount
                };

                var rt = OS_InosService.Instance.WA_OS_Inos_OrderService_CheckOrderStatus(rq, addressReportAPIs);
                return Json(new { Success = true, Data = rt.Inos_LicOrder });
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);

            return Json(new { Success = false, Detail = resultEntry.Messages });

        }
        #endregion

        #region ["Validate MST"]
        public bool Validate_MST_API(object mst, ref string message)
        {
            var isValidate = true;
            message = "";
            if (CUtils.IsNullOrEmpty(mst))
            {
                message = "Mã số thuế trống!";
                isValidate = false;
            }
            else
            {
                var mstInput = mst.ToString().Trim();
            }
            return isValidate;
        }

        public bool Validate_MST_Client(object mst, ref string message)
        {
            var isValidate = true;
            message = "";
            if (CUtils.IsNullOrEmpty(mst))
            {
                message = "Mã số thuế trống!";
                isValidate = false;
            }
            else
            {
                var mstInput = mst.ToString().Trim();
                if (mstInput.Length < 10)
                {
                    message = "Mã số thuế không hợp lệ!";
                    isValidate = false;
                }
                else
                {
                    var mstsub = mstInput.Substring(0, 10);
                    if (CUtils.IsNumber(mstsub))
                    {
                        var s1 = Convert.ToInt32(mstsub.Substring(0, 1));
                        var s2 = Convert.ToInt32(mstsub.Substring(1, 1));
                        var s3 = Convert.ToInt32(mstsub.Substring(2, 1));
                        var s4 = Convert.ToInt32(mstsub.Substring(3, 1));
                        var s5 = Convert.ToInt32(mstsub.Substring(4, 1));
                        var s6 = Convert.ToInt32(mstsub.Substring(5, 1));
                        var s7 = Convert.ToInt32(mstsub.Substring(6, 1));
                        var s8 = Convert.ToInt32(mstsub.Substring(7, 1));
                        var s9 = Convert.ToInt32(mstsub.Substring(8, 1));
                        var s10 = Convert.ToInt32(mstsub.Substring(9, 1));
                        var sum = (s1 * 31 + s2 * 29 + s3 * 23 + s4 * 19 + s5 * 17 + s6 * 13 + s7 * 7 + s8 * 5 + s9 * 3);
                        var mod = CUtils.MathMod((10 - sum), 11);
                        if (mod != s10)
                        {
                            message = "Mã số thuế không hợp lệ!";
                            isValidate = false;
                        }
                        else
                        {
                            #region["Gọi service kiểm tra trạng thái hoạt động của mã số thuế"]
                            // mặc định đúng

                            #endregion
                        }
                    }
                    else
                    {
                        message = "Mã số thuế không hợp lệ!";
                        isValidate = false;
                    }
                }
            }
            return isValidate;
        }
        #endregion

        #region["Return View thông báo kết quả giao dịch - Mua License ở network"]

        public ActionResult VnPayReturnLiceseAdd()
        {
            var UserState = System.Web.HttpContext.Current.Session["UserState"];
            
            if (UserState != null)
            {

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(UserState);
                dynamic data = System.Web.Helpers.Json.Decode(json);
                var sysUser = data.SysUser;
                //ViewBag.NetworkID = UserState.SysUser.NetworkID;
                var vnpayData = Request.QueryString;
                ViewBag.NetworkID = sysUser.NetworkID;
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
                var objInos_LicOrderCur = GetOrderStatusNetwork();

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

            }
            return View();
        }

        public Inos_LicOrder GetOrderStatusNetwork()
        {
            var UserState = System.Web.HttpContext.Current.Session["UserState"];

            var objInos_LicOrder = new Inos_LicOrder();
            if (UserState != null)
            {
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(UserState);
                dynamic data = System.Web.Helpers.Json.Decode(json);
                var sysUser = data.SysUser;
                var tokenID = data.TokenID;
                var addressAPIs = data.AddressAPIs;
                var wAUserCode = sysUser.UserCode;
                var wAUserPassword = sysUser.UserPassword;
                var orderId = CUtils.StrValue(System.Web.HttpContext.Current.Session["orderId"]);

                var objInos_LicOrderInput = new Inos_LicOrder()
                {
                    Id = orderId
                };
                var objRQ_OS_Inos_LicOrder = new RQ_OS_Inos_LicOrder()
                {
                    Inos_LicOrder = objInos_LicOrderInput,
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = wAUserCode,
                    WAUserPassword = wAUserPassword,
                    AccessToken = tokenID,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount
                };

                var objRT_OS_Inos_LicOrder = OS_InosService.Instance.WA_OS_Inos_OrderService_CheckOrderStatus(objRQ_OS_Inos_LicOrder, addressAPIs);
                if (objRT_OS_Inos_LicOrder != null && objRT_OS_Inos_LicOrder.Inos_LicOrder != null)
                {
                    objInos_LicOrder = objRT_OS_Inos_LicOrder.Inos_LicOrder;
                }
            }
            
            return objInos_LicOrder;
        }

        #endregion


        #region ["strWhereClause"]
        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValue(data.FlagActive), "=");
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
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_iNOS_Mst_BizType(iNOS_Mst_BizType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_iNOS_Mst_BizType = TableName.iNOS_Mst_BizType + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_iNOS_Mst_BizType + TbliNOS_Mst_BizType.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_iNOS_Mst_BizSize(iNOS_Mst_BizSize data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_iNOS_Mst_BizSize = TableName.iNOS_Mst_BizSize + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_iNOS_Mst_BizSize + TbliNOS_Mst_BizSize.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_iNOS_Mst_BizField(iNOS_Mst_BizField data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_iNOS_Mst_BizField = TableName.iNOS_Mst_BizField + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_iNOS_Mst_BizField + TbliNOS_Mst_BizField.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}