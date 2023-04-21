using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Filters;
using System;
using System.Text;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading.Tasks;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Extensions;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Website.State;
using System.Threading.Tasks;
//using idn.Skycic.Inventory.Common.Extensions;
using idn.Skycic.Inventory.Common.ModelsUI;
using System.Text;
using idn.Skycic.Inventory.Common.Extensions;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.ClientService;

namespace idn.Skycic.Inventory.Website.Controllers
{
    [CustomErrorHandler]
    public class InitController : Controller
    {
        #region["Variables"]
        public string GwUserCode = "";
        public string GwPassword = "";
        public string WAUserCode_RptSV = "";
        public string WAUserPassword_RptSV = "";
        public string WAUserCode_MstSV = "";
        public string WAUserPassword_MstSV = "";
        public string WAUserCode_Network = "";
        public string WAUserPassword_Network = "";
        public string GwUserCode_PayGate = "";
        public string GwPassword_PayGate = "";
        public string WAUserCode_PayGate = "";
        public string WAPassword_PayGate = "";

        public string OS_ProductCentrer_GwUserCode = "";
        public string OS_ProductCentrer_GwPassword = "";

        public string OS_ProductCentrer_MasterServer_GwUserCode = "";
        public string OS_ProductCentrer_MasterServer_GwPassword = "";
        public string OS_ProductCentrer_MasterServer_WAUserCode = "";
        public string OS_ProductCentrer_MasterServer_WAUserPassword = "";


        public string OS_Veloca_MasterServer_GwUserCode = "";
        public string OS_Veloca_MasterServer_GwPassword = "";
        public string OS_Veloca_MasterServer_WAUserCode = "";
        public string OS_Veloca_MasterServer_WAUserPassword = "";
        public string OS_Veloca_GwUserCode = "";
        public string OS_Veloca_GwPassword = "";
        public string OS_Veloca_WAUserCode = "";
        public string OS_Veloca_WAUserPassword = "";

        public string OS_DMS_MasterServer_GwUserCode = "";
        public string OS_DMS_MasterServer_GwPassword = "";
        public string OS_DMS_MasterServer_WAUserCode = "";
        public string OS_DMS_MasterServer_WAUserPassword = "";
        public string OS_DMS_GwUserCode = "";
        public string OS_DMS_GwPassword = "";

        public string OS_LQDMS_MasterServer_GwUserCode = "";
        public string OS_LQDMS_MasterServer_GwPassword = "";
        public string OS_LQDMS_MasterServer_WAUserCode = "";
        public string OS_LQDMS_MasterServer_WAUserPassword = "";
        public string OS_LQDMS_GwUserCode = "";
        public string OS_LQDMS_GwPassword = "";
        public string BaseMasterServerLQDMSAPIAddress = "";

        public string GwUserCodeLQDMS = "";
        public string GwPasswordLQDMS = "";
        public string WAUserCode_LQDMS_BG = "";
        public string WAUserPassword_LQDMS_BG = "";
        public string LQDMS_OrgID = "";
        public string LQDMS_NetworkID = "";
        public string LQDMS_UtcOffset = "";




        public string FuncType = "";
        public string Ft_RecordStart = "0";
        public string Ft_RecordStartExportExcel = "0";
        public string Ft_RecordCount = "123456000";
        public string Ft_WhereClause = "";
        public string FlagActive = "1";

        public string NetworkID = "";
        public string TokenIDFix = "";
        public string Hethong = "";
        public int PageSizeConfig = 10;
        public int RowsWorksheets = 1048570; // If you are working on Excel 2007 or any of the latest versions - there are 1048576 rows and 16384 columns. 
        public string Uploads = "Uploads";
        public string TempFiles = "TempFiles";
        public double FileUploadSize = 26214400; // 25MB
        public double FileImageSize = 26214400; // 25MB
        public string RootFolder = "";
        public string ReportBro_ServerUrl = "";
        public string ReportBro_Key = "";

        public string SolutionCodeSendMail = "";
        public string SolutionName = "";

        //
        public string APIsSendMail = "";
        public string ApiKeySendMail = "";
        public string DisplayNameMailFrom = "";
        public string MailFrom = "";
        public string From = "";
        public string MailgateDomain = "";

        public string UserInos = "";
        public string UserInosPass = "";
        public string HeThongInos = "";

        #endregion
        #region["Get DateTime Server Client"]
        public DateTime GetDateTimeServerClient()
        {
            var dateTime = DateTime.Now;
            return dateTime;
        }
        public string DateTimeNow()
        {
            var datetime = GetDateTimeServerClient().ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
            return datetime;
        }
        public string Today
        {
            get
            {
                var date = GetDateTimeServerClient().ToString(Nonsense.DATE_TIME_FORMAT);
                return date;
            }
        }
        public string Year
        {
            get
            {
                var year = GetDateTimeServerClient().Year.ToString();
                return year;
            }
        }
        public string MonthOfYear
        {
            get
            {
                var monthOfYear = GetDateTimeServerClient().ToString(Nonsense.MONTH_YEAR_FORMAT);
                return monthOfYear;
            }
        }
        public string DateToSearch()
        {
            var date = "";
            date = CUtils.GetDateToSearch(DateTimeNow());
            return date;
        }
        #endregion
        #region["Constructor"]
        public InitController()
        {
            #region[""]
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
            {
                ServiceAddress.BaseReportServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
            {
                ServiceAddress.BaseMasterServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseMasterServerAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerProduct_Customer_CenterAPIAddress))
            {
                ServiceAddress.BaseMasterServerProduct_Customer_CenterAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseMasterServerProduct_Customer_CenterAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerVelocaAPIAddress))
            {
                ServiceAddress.BaseMasterServerVelocaAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseMasterServerVelocaAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerDMSAPIAddress))
            {
                ServiceAddress.BaseMasterServerDMSAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseMasterServerDMSAPIAddress"];
            }
            #endregion
            #region["GwUserCode - GwPassword"]
            if (CUtils.IsNullOrEmpty(GwUserCode))
            {
                GwUserCode = ConfigurationManager.AppSettings["GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(GwPassword))
            {
                GwPassword = ConfigurationManager.AppSettings["GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_Network))
            {
                WAUserCode_Network = ConfigurationManager.AppSettings["WAUserCode_Network"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_Network))
            {
                WAUserPassword_Network = ConfigurationManager.AppSettings["WAUserPassword_Network"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_RptSV))
            {
                WAUserCode_RptSV = ConfigurationManager.AppSettings["WAUserCode_RptSV"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_RptSV))
            {
                WAUserPassword_RptSV = ConfigurationManager.AppSettings["WAUserPassword_RptSV"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_MstSV))
            {
                WAUserCode_MstSV = ConfigurationManager.AppSettings["WAUserCode_MstSV"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_MstSV))
            {
                WAUserPassword_MstSV = ConfigurationManager.AppSettings["WAUserPassword_MstSV"];
            }
            if (CUtils.IsNullOrEmpty(GwUserCode_PayGate))
            {
                GwUserCode_PayGate = ConfigurationManager.AppSettings["GwUserCode_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(GwPassword_PayGate))
            {
                GwPassword_PayGate = ConfigurationManager.AppSettings["GwPassword_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_PayGate))
            {
                WAUserCode_PayGate = ConfigurationManager.AppSettings["WAUserCode_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(WAPassword_PayGate))
            {
                WAPassword_PayGate = ConfigurationManager.AppSettings["WAPassword_PayGate"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_MasterServer_WAUserCode))
            {
                OS_ProductCentrer_MasterServer_WAUserCode = ConfigurationManager.AppSettings["OS_ProductCentrer_MasterServer_WAUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_MasterServer_WAUserPassword))
            {
                OS_ProductCentrer_MasterServer_WAUserPassword = ConfigurationManager.AppSettings["OS_ProductCentrer_MasterServer_WAUserPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_GwUserCode))
            {
                OS_ProductCentrer_GwUserCode = ConfigurationManager.AppSettings["OS_ProductCentrer_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_GwPassword))
            {
                OS_ProductCentrer_GwPassword = ConfigurationManager.AppSettings["OS_ProductCentrer_GwPassword"];
            }

            //
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_MasterServer_GwUserCode))
            {
                OS_ProductCentrer_MasterServer_GwUserCode = ConfigurationManager.AppSettings["OS_ProductCentrer_MasterServer_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_ProductCentrer_MasterServer_GwPassword))
            {
                OS_ProductCentrer_MasterServer_GwPassword = ConfigurationManager.AppSettings["OS_ProductCentrer_MasterServer_GwPassword"];
            }

            //if (CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerProduct_Customer_CenterAPIAddress))
            //{
            //    ServiceAddress.BaseReportServerProduct_Customer_CenterAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerProduct_Customer_CenterAPIAddress"];
            //}

            if (CUtils.IsNullOrEmpty(OS_Veloca_MasterServer_WAUserCode))
            {
                OS_Veloca_MasterServer_WAUserCode = ConfigurationManager.AppSettings["OS_Veloca_MasterServer_WAUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_MasterServer_WAUserPassword))
            {
                OS_Veloca_MasterServer_WAUserPassword = ConfigurationManager.AppSettings["OS_Veloca_MasterServer_WAUserPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_MasterServer_GwUserCode))
            {
                OS_Veloca_MasterServer_GwUserCode = ConfigurationManager.AppSettings["OS_Veloca_MasterServer_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_MasterServer_GwPassword))
            {
                OS_Veloca_MasterServer_GwPassword = ConfigurationManager.AppSettings["OS_Veloca_MasterServer_GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_GwUserCode))
            {
                OS_Veloca_GwUserCode = ConfigurationManager.AppSettings["OS_Veloca_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_GwPassword))
            {
                OS_Veloca_GwPassword = ConfigurationManager.AppSettings["OS_Veloca_GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_WAUserCode))
            {
                OS_Veloca_WAUserCode = ConfigurationManager.AppSettings["OS_Veloca_WAUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_Veloca_WAUserPassword))
            {
                OS_Veloca_WAUserPassword = ConfigurationManager.AppSettings["OS_Veloca_WAUserPassword"];
            }

            if (CUtils.IsNullOrEmpty(OS_DMS_MasterServer_WAUserCode))
            {
                OS_DMS_MasterServer_WAUserCode = ConfigurationManager.AppSettings["OS_DMS_MasterServer_WAUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_DMS_MasterServer_WAUserPassword))
            {
                OS_DMS_MasterServer_WAUserPassword = ConfigurationManager.AppSettings["OS_DMS_MasterServer_WAUserPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_DMS_MasterServer_GwUserCode))
            {
                OS_DMS_MasterServer_GwUserCode = ConfigurationManager.AppSettings["OS_DMS_MasterServer_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_DMS_MasterServer_GwPassword))
            {
                OS_DMS_MasterServer_GwPassword = ConfigurationManager.AppSettings["OS_DMS_MasterServer_GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(OS_DMS_GwUserCode))
            {
                OS_DMS_GwUserCode = ConfigurationManager.AppSettings["OS_DMS_GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(OS_DMS_GwPassword))
            {
                OS_DMS_GwPassword = ConfigurationManager.AppSettings["OS_DMS_GwPassword"];
            }
            if (CUtils.IsNullOrEmpty(GwUserCodeLQDMS))
            {
                GwUserCodeLQDMS = ConfigurationManager.AppSettings["GwUserCodeLQDMS"];
            }
            if (CUtils.IsNullOrEmpty(GwPasswordLQDMS))
            {
                GwPasswordLQDMS = ConfigurationManager.AppSettings["GwPasswordLQDMS"];
            }
            if (CUtils.IsNullOrEmpty(WAUserCode_LQDMS_BG))
            {
                WAUserCode_LQDMS_BG = ConfigurationManager.AppSettings["WAUserCode_LQDMS_BG"];
            }
            if (CUtils.IsNullOrEmpty(WAUserPassword_LQDMS_BG))
            {
                WAUserPassword_LQDMS_BG = ConfigurationManager.AppSettings["WAUserPassword_LQDMS_BG"];
            }
            if (CUtils.IsNullOrEmpty(LQDMS_OrgID))
            {
                LQDMS_OrgID = ConfigurationManager.AppSettings["LQDMS_OrgID"];
            }
            if (CUtils.IsNullOrEmpty(LQDMS_NetworkID))
            {
                LQDMS_NetworkID = ConfigurationManager.AppSettings["LQDMS_NetworkID"];
            }
            if (CUtils.IsNullOrEmpty(LQDMS_UtcOffset))
            {
                LQDMS_UtcOffset = ConfigurationManager.AppSettings["LQDMS_UtcOffset"];
            }
            if (CUtils.IsNullOrEmpty(BaseMasterServerLQDMSAPIAddress))
            {
                BaseMasterServerLQDMSAPIAddress = ConfigurationManager.AppSettings["BaseMasterServerLQDMSAPIAddress"];
            }
            #endregion
            #region["SendMail Config"]
            if (CUtils.IsNullOrEmpty(SolutionCodeSendMail))
            {
                SolutionCodeSendMail = ConfigurationManager.AppSettings["SolutionCodeSendMail"];
            }
            if (CUtils.IsNullOrEmpty(SolutionName))
            {
                SolutionName = ConfigurationManager.AppSettings["SolutionName"];
            }
            if (CUtils.IsNullOrEmpty(APIsSendMail))
            {
                APIsSendMail = ConfigurationManager.AppSettings["APIsSendMail"];
            }
            if (CUtils.IsNullOrEmpty(ApiKeySendMail))
            {
                ApiKeySendMail = ConfigurationManager.AppSettings["ApiKeySendMail"];
            }
            if (CUtils.IsNullOrEmpty(DisplayNameMailFrom))
            {
                DisplayNameMailFrom = ConfigurationManager.AppSettings["DisplayNameMailFrom"];
            }
            if (CUtils.IsNullOrEmpty(MailFrom))
            {
                MailFrom = ConfigurationManager.AppSettings["MailFrom"];
            }
            if (CUtils.IsNullOrEmpty(From))
            {
                From = ConfigurationManager.AppSettings["From"];
            }
            if (CUtils.IsNullOrEmpty(MailgateDomain))
            {
                MailgateDomain = ConfigurationManager.AppSettings["MailgateDomain"];
            }
            #endregion
            #region ["User Backen - Inos"]
            if (CUtils.IsNullOrEmpty(UserInos))
            {
                UserInos = ConfigurationManager.AppSettings["UserInos"];
            }
            if (CUtils.IsNullOrEmpty(UserInosPass))
            {
                UserInosPass = ConfigurationManager.AppSettings["UserInosPass"];
            }
            if (CUtils.IsNullOrEmpty(HeThongInos))
            {
                HeThongInos = ConfigurationManager.AppSettings["HeThongInos"];
            }
            #endregion
        }
        #endregion
        #region ["State management"]
        private SessionWrapper _session;
        public SessionWrapper SessionWrapper
        {
            get
            {
                if (_session == null)
                {
                    var sessionCur = ControllerContext.HttpContext.Session;
                    _session = new SessionWrapper(ControllerContext.HttpContext.Session);
                }
                return _session;
            }
            set
            {
                _session = value;
            }
        }
        #endregion
        #region["RenderRazorViewToString"]
        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        public string RenderRazorViewToString(string viewName, string masterName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindView(ControllerContext, viewName, masterName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        #endregion
        #region["JsonView"]
        protected override JsonResult Json(object data, string contentType, System.Text.Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonResult()
            {
                Data = data,
                ContentType = contentType,
                ContentEncoding = contentEncoding,
                JsonRequestBehavior = behavior,
                MaxJsonLength = Int32.MaxValue // Use this value to set your maximum size for all of your Requests
            };
        }
        public ActionResult JsonView(object model = null)
        {
            return JsonView("", model);
        }

        public ActionResult JsonView(string viewname, object model)
        {
            if (CUtils.IsNullOrEmpty(viewname))
            {
                viewname = (string)ControllerContext.RouteData.Values["action"];
            }
            try
            {
                var html = RenderRazorViewToString(viewname, model);
                var data = new
                {
                    Success = true,
                    Html = html
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var data = new
                {
                    Success = false,
                    Message = e.Message,
                    Detail = e.StackTrace
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult JsonView(string viewname, string masterName, object model)
        {
            if (CUtils.IsNullOrEmpty(viewname))
            {
                viewname = (string)ControllerContext.RouteData.Values["action"];
            }
            try
            {
                var html = RenderRazorViewToString(viewname, masterName, model);
                var data = new
                {
                    Success = true,
                    Html = html
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                var data = new
                {
                    Success = false,
                    Message = e.Message,
                    Detail = e.StackTrace
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult JsonViewError(string viewname, object model, JsonResultEntry resultEntry)
        {
            if (CUtils.IsNullOrEmpty(viewname))
            {
                viewname = (string)ControllerContext.RouteData.Values["action"];
            }
            string html = "";
            try
            {
                html = RenderRazorViewToString(viewname, model);
                if (resultEntry.Success)
                {
                    var data = new
                    {
                        Success = true,
                        Html = html,
                        Title = resultEntry.Messages
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Html = html,
                        Title = resultEntry.Messages,
                        Detail = resultEntry.Detail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                var data = new
                {
                    Success = false,
                    Message = e.Message,
                    Detail = resultEntry.Detail
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region["File - Folder"]
        public string CheckFolderExists(string path = "")
        {
            var strFolder = "";
            if (!string.IsNullOrEmpty(path))
            {
                var strPath = Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            else
            {
                path = "/Uploads";
                var strPath = Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            return strFolder;
        }

        public string CheckFolderCurrentExists(string path = "")
        {
            var strFolder = "";
            if (!string.IsNullOrEmpty(path))
            {
                var strPath = System.Web.HttpContext.Current.Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            else
            {
                path = "/Uploads";
                var strPath = System.Web.HttpContext.Current.Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            return strFolder;
            //return System.Web.HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", GetTempDir(), fileId, extension));
        }
        #endregion
        #region["GenFilePath"]
        public string GenExcelExportFilePath(string prefix, ref string virualPath)
        {
            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {
                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }

        public string GenExcelExportFilePath(string ext, string prefix, ref string virualPath)
        {
            if (string.IsNullOrEmpty(ext))
            {
                ext = ".xls";
            }
            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}{2}", subpath, filename, ext);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {

                virualPath = string.Format("{0}/{1}_{2}{3}", subpath, filename, ++i, ext);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }

        public string GenExcelExportReportFilePath(string prefix, ref string virualPath)
        {
            String subpath = string.Format("/Reports/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {
                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }
        #endregion
        #region["Commons"]
        private static int inext = 0;
        public string GetNextTId()
        {
            var strDateTimeNow = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff").Trim();
            var strNextTId = string.Format("{0}.{1}", strDateTimeNow, inext++);
            return strNextTId;
        }
        public string GetNextDateTId()
        {
            var strNextTId = string.Format("{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            return strNextTId;
        }
        public string GetRandomId(string pre, int length = 8)
        {
            string ran = Guid.NewGuid().ToString("N").Substring(0, length).ToUpper();
            return string.Format("{0}{1}", pre, ran);
        }
        protected string RedirectActionToLogin()
        {
            FormsAuthentication.SignOut();
            var requestRawUrl = "/";
            var action = CUtils.StrValue(this.RouteData.Values["action"].ToString()).ToLower();
            if (!action.Equals("Login".ToLower(), StringComparison.InvariantCultureIgnoreCase))
            {
                requestRawUrl = Request.RawUrl;
            }
            var redirectUrl = Url.Action("Login", "Account", new { redirectUrl = requestRawUrl });
            return redirectUrl;
        }

        protected string RedirectActionToReportLogin()
        {
            FormsAuthentication.SignOut();
            var requestRawUrl = "/";
            var action = CUtils.StrValue(this.RouteData.Values["action"].ToString()).ToLower();
            if (!action.Equals("Login".ToLower(), StringComparison.InvariantCultureIgnoreCase))
            {
                requestRawUrl = Request.RawUrl;
            }
            var redirectUrl = Url.Action("Login", "ReportAcc", new { redirecturl = requestRawUrl });
            return redirectUrl;
        }

        protected string Reports_RedirectUrl()
        {
            var requestRawUrl = "/";
            var action = CUtils.StrValue(this.RouteData.Values["action"].ToString()).ToLower();
            if (!action.Equals("Login".ToLower(), StringComparison.InvariantCultureIgnoreCase))
            {
                var a1 = Request.Url.AbsoluteUri;
                var a12 = Request.Url;
                requestRawUrl = a1;
            }
            var redirectUrl = Url.Action("Login", "ReportAcc", new { redirecturl = requestRawUrl });
            return redirectUrl;
        }
        public string GetRequestBaseAddress()
        {
            int defaultPort = Request.IsSecureConnection ? 443 : 80;
            string ret = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host
                         + (Request.Url.Port != defaultPort ? ":" + Request.Url.Port : "");

            return ret;
        }
        #endregion

        #region["SSO"]
        public void InitSSO()
        {
            var serverBaseAddress = inos.common.Paths.AuthorizationServerBaseAddress;
            if (CUtils.IsNullOrEmpty(serverBaseAddress) || serverBaseAddress.IndexOf("localhost") >= 0)
            {
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object sender, X509Certificate certificate, X509Chain
                    chain, SslPolicyErrors sslPolicyErrors)
                {
                    return true;

                };
                var inosBaseUrl = System.Configuration.ConfigurationManager.AppSettings["InosBaseUrl"];
                var ssoSecret = System.Configuration.ConfigurationManager.AppSettings["SSOSecret"];
                var solutionCode = System.Configuration.ConfigurationManager.AppSettings["SolutionCode"];
                inos.common.Service.InosClientServiceBase.Init(solutionCode, inosBaseUrl, ssoSecret);
            }

        }
        #endregion
    }
    public class RptInitController : InitController
    {
        #region["UserState"]
        public UserReportState UserReportState
        {

            get
            {
                return SessionWrapper.UserReportState;
            }

            set
            {
                SessionWrapper.UserReportState = value;
            }
        }
        #endregion

    }
    [Authorize]
    public class ReportsBaseController : RptInitController
    {
        

        public ReportsBaseController()
        {

        }

        #region ["Init"]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.Init();
        }
        protected virtual void Init()
        {

            if (User.Identity.IsAuthenticated && UserReportState != null)
            {
                var userName = "";
                if (!CUtils.IsNullOrEmpty(UserReportState.UserName))
                {
                    userName = UserReportState.UserName.Trim();
                }

                ViewBag.UserName = userName;
                ViewBag.UtcOffset = UserReportState.UtcOffset;
            }
            else
            {
                var redirectUrl = RedirectActionToReportLogin();
                Response.Redirect(redirectUrl);
            }
        }
        #endregion
    }

    public class BaseController : InitController
    {
        #region ["UserState"]
        public UserState UserState
        {

            get
            {
                return SessionWrapper.UserState;
            }

            set
            {
                SessionWrapper.UserState = value;
            }
        }
        #endregion
        #region["Login hệ thống"]
        public async Task<List<Mst_NNT>> List_Mst_NNT_Login_Async(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            Task<List<Mst_NNT>> list_Mst_NNT_Task = List_Mst_NNT_Login_Task(objRQ_Mst_NNT, addressAPIs);
            List<Mst_NNT> list_Mst_NNT = await list_Mst_NNT_Task;
            return list_Mst_NNT;
        }
        public Task<List<Mst_NNT>> List_Mst_NNT_Login_Task(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            TaskCompletionSource<List<Mst_NNT>> tcs = new TaskCompletionSource<List<Mst_NNT>>();
            var listMst_NNT = new List<Mst_NNT>();
            listMst_NNT = WA_Mst_NNT_Login_Get(objRQ_Mst_NNT, addressAPIs);
            tcs.TrySetResult(listMst_NNT);
            return tcs.Task;
        }
        protected List<Mst_NNT> WA_Mst_NNT_Login_Get(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var listMst_NNT = new List<Mst_NNT>();
            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
            {
                listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
            }
            return listMst_NNT;
        }

        public async Task<RT_Mst_Sys_Config> RT_Mst_Sys_Config_Login_Async(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        {
            Task<RT_Mst_Sys_Config> objRT_Mst_Sys_Config_Task = List_Mst_Sys_Config_Login_Task(objRQ_Mst_Sys_Config, addressAPIs);
            RT_Mst_Sys_Config objRT_Mst_Sys_Config = await objRT_Mst_Sys_Config_Task;
            return objRT_Mst_Sys_Config;
        }
        public Task<RT_Mst_Sys_Config> List_Mst_Sys_Config_Login_Task(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        {
            TaskCompletionSource<RT_Mst_Sys_Config> tcs = new TaskCompletionSource<RT_Mst_Sys_Config>();
            var objRT_Mst_Sys_Config = WA_Mst_Sys_Config_Login_Get(objRQ_Mst_Sys_Config, addressAPIs);
            tcs.TrySetResult(objRT_Mst_Sys_Config);
            return tcs.Task;
        }
        protected RT_Mst_Sys_Config WA_Mst_Sys_Config_Login_Get(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        {
            var listMst_Sys_Config = new List<Mst_Sys_Config>();
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Sys_Config);
            var objRT_Mst_Sys_Config = Mst_Sys_ConfigService.Instance.WA_Mst_Sys_Config_Get(objRQ_Mst_Sys_Config, addressAPIs);
            return objRT_Mst_Sys_Config;
        }

        public async Task<string> ReturnAPIMasterServer_Async(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            Task<string> objRT_Mst_Sys_Config_Task = ReturnAPIMasterServer_Task(objRQ_MstSv_Sys_User, addressAPIs);
            string objRT_Mst_Sys_Config = await objRT_Mst_Sys_Config_Task;
            return objRT_Mst_Sys_Config;
        }
        public Task<string> ReturnAPIMasterServer_Task(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            var aPIMasterServerVeloca = ReturnAPIMasterServer(objRQ_MstSv_Sys_User, addressAPIs);
            tcs.TrySetResult(aPIMasterServerVeloca);
            return tcs.Task;
        }
        protected string ReturnAPIMasterServer(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            var baseServiceAddress = "";
            var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User, addressAPIs);
            if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                {
                    baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                }
            }
            return baseServiceAddress;
        }

        public async Task<string> ReturnAPIMasterServerProduct_Customer_Center_Async(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            Task<string> objRT_Mst_Sys_Config_Task = ReturnAPIMasterServerProduct_Customer_Center_Task(objRQ_MstSv_Sys_User, addressAPIs);
            string objRT_Mst_Sys_Config = await objRT_Mst_Sys_Config_Task;
            return objRT_Mst_Sys_Config;
        }
        public Task<string> ReturnAPIMasterServerProduct_Customer_Center_Task(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            var aPIMasterServerInventory = ReturnAPIMasterServerProduct_Customer_Center(objRQ_MstSv_Sys_User, addressAPIs);
            tcs.TrySetResult(aPIMasterServerInventory);
            return tcs.Task;
        }
        protected string ReturnAPIMasterServerProduct_Customer_Center(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            var baseServiceAddress = "";
            var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User, addressAPIs);
            if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                {
                    baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                }
            }
            return baseServiceAddress;
        }
        #endregion
        
    }


    [Authorize]
    public class AdminBaseController : BaseController
    {
        private inos.common.Service.NotificationService notificationService;
        protected inos.common.Service.NotificationService NotificationService
        {
            get
            {

                if (notificationService == null)
                    notificationService = new inos.common.Service.NotificationService(Session);

                return notificationService;
            }
        }

        private inos.common.Service.AccountService accountService;
        protected inos.common.Service.AccountService AccountService
        {
            get
            {

                if (accountService == null)
                    accountService = new inos.common.Service.AccountService(Session);

                return accountService;
            }
        }

        #region["DateTimeUTC ServerBiz"]
        public string DateTimeServerBizUTC()
        {
            var datetimeUTC = "";
            var objRQ_Common = new RQ_Common()
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
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
            };
            var objRT_Common = CommonService.Instance.WA_Common_GetDTime(objRQ_Common, ServiceAddress.BaseReportServerAPIAddress);
            if (objRT_Common != null && objRT_Common.c_K_DT_Sys != null && objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null && objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var objc_K_DT_SysInfo = objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(objc_K_DT_SysInfo.Remark))
                {
                    datetimeUTC = objc_K_DT_SysInfo.Remark.Trim();
                }
                //var convertingLocalTimeToUTC = CUtils.ConvertingLocalTimeToUTC(datetimeUTC);
                //var convertingUTCToLocalTime = CUtils.ConvertingUTCToLocalTime(datetimeUTC);
            }
            return datetimeUTC;
        }
        #endregion
        #region["Login hệ thống"]
        public async Task<List<Mst_NNT>> List_Mst_NNT_Login_Async(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            Task<List<Mst_NNT>> list_Mst_NNT_Task = List_Mst_NNT_Login_Task(objRQ_Mst_NNT, addressAPIs);
            List<Mst_NNT> list_Mst_NNT = await list_Mst_NNT_Task;
            return list_Mst_NNT;
        }
        public Task<List<Mst_NNT>> List_Mst_NNT_Login_Task(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            TaskCompletionSource<List<Mst_NNT>> tcs = new TaskCompletionSource<List<Mst_NNT>>();
            var listMst_NNT = new List<Mst_NNT>();
            listMst_NNT = WA_Mst_NNT_Login_Get(objRQ_Mst_NNT, addressAPIs);
            tcs.TrySetResult(listMst_NNT);
            return tcs.Task;
        }
        protected List<Mst_NNT> WA_Mst_NNT_Login_Get(RQ_Mst_NNT objRQ_Mst_NNT, string addressAPIs)
        {
            var listMst_NNT = new List<Mst_NNT>();
            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
            {
                listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
            }
            return listMst_NNT;
        }

        //public async Task<RT_Mst_Sys_Config> RT_Mst_Sys_Config_Login_Async(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        //{
        //    Task<RT_Mst_Sys_Config> objRT_Mst_Sys_Config_Task = List_Mst_Sys_Config_Login_Task(objRQ_Mst_Sys_Config, addressAPIs);
        //    RT_Mst_Sys_Config objRT_Mst_Sys_Config = await objRT_Mst_Sys_Config_Task;
        //    return objRT_Mst_Sys_Config;
        //}
        //public Task<RT_Mst_Sys_Config> List_Mst_Sys_Config_Login_Task(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        //{
        //    TaskCompletionSource<RT_Mst_Sys_Config> tcs = new TaskCompletionSource<RT_Mst_Sys_Config>();
        //    var objRT_Mst_Sys_Config = WA_Mst_Sys_Config_Login_Get(objRQ_Mst_Sys_Config, addressAPIs);
        //    tcs.TrySetResult(objRT_Mst_Sys_Config);
        //    return tcs.Task;
        //}
        //protected RT_Mst_Sys_Config WA_Mst_Sys_Config_Login_Get(RQ_Mst_Sys_Config objRQ_Mst_Sys_Config, string addressAPIs)
        //{
        //    var listMst_Sys_Config = new List<Mst_Sys_Config>();
        //    var objRT_Mst_Sys_Config = Mst_Sys_ConfigService.Instance.WA_Mst_Sys_Config_Get(objRQ_Mst_Sys_Config, addressAPIs);
        //    return objRT_Mst_Sys_Config;
        //}

        public async Task<string> ReturnAPIMasterServerInventory_Async(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            Task<string> objRT_Mst_Sys_Config_Task = ReturnAPIMasterServerInventory_Task(objRQ_MstSv_Sys_User, addressAPIs);
            string objRT_Mst_Sys_Config = await objRT_Mst_Sys_Config_Task;
            return objRT_Mst_Sys_Config;
        }
        public Task<string> ReturnAPIMasterServerInventory_Task(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            var aPIMasterServerInventory = ReturnAPIMasterServerInventory(objRQ_MstSv_Sys_User, addressAPIs);
            tcs.TrySetResult(aPIMasterServerInventory);
            return tcs.Task;
        }
        protected string ReturnAPIMasterServerInventory(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            var baseServiceAddress = "";
            var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User, addressAPIs);
            if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                {
                    baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                }
            }
            return baseServiceAddress;
        }

        public async Task<string> ReturnAPIMasterServerProduct_Customer_Center_Async(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            Task<string> objRT_Mst_Sys_Config_Task = ReturnAPIMasterServerProduct_Customer_Center_Task(objRQ_MstSv_Sys_User, addressAPIs);
            string objRT_Mst_Sys_Config = await objRT_Mst_Sys_Config_Task;
            return objRT_Mst_Sys_Config;
        }
        public Task<string> ReturnAPIMasterServerProduct_Customer_Center_Task(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            var aPIMasterServerInventory = ReturnAPIMasterServerProduct_Customer_Center(objRQ_MstSv_Sys_User, addressAPIs);
            tcs.TrySetResult(aPIMasterServerInventory);
            return tcs.Task;
        }
        protected string ReturnAPIMasterServerProduct_Customer_Center(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string addressAPIs)
        {
            var baseServiceAddress = "";
            var objRT_MstSv_Sys_UserCur = MasterServerService.Instance.WA_MstSv_Sys_User_Login(objRQ_MstSv_Sys_User, addressAPIs);
            if (objRT_MstSv_Sys_UserCur.c_K_DT_Sys != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null &&
                objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var c_K_DT_SysInfoCur = objRT_MstSv_Sys_UserCur.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(c_K_DT_SysInfoCur.Remark))
                {
                    baseServiceAddress = CUtils.StrValueOrNull(c_K_DT_SysInfoCur.Remark);
                }
            }
            return baseServiceAddress;
        }
        #endregion

        public void CheckApi_Url(string hethong)
        {
            if (hethong.Equals("TEST"))
            {
                // APIs phải là địa chỉ IP
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
                {
                    var api_Uri_ReportServer = new Uri(ServiceAddress.BaseReportServerAPIAddress);
                    var hostReportServer = api_Uri_ReportServer.Host;
                    if (!CUtils.IsIPAddress(hostReportServer))
                    {
                        RedirectActionToLogin();
                    }
                }
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
                {
                    var api_Uri_MasterServer = new Uri(ServiceAddress.BaseMasterServerAPIAddress);
                    var hostMasterServer = api_Uri_MasterServer.Host;
                    if (!CUtils.IsIPAddress(hostMasterServer))
                    {
                        RedirectActionToLogin();
                    }
                }
            }
            else if (hethong.Equals("REAL"))
            {
                // APIs phải có domain
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
                {
                    var api_Uri_ReportServer = new Uri(ServiceAddress.BaseReportServerAPIAddress);
                    var hostReportServer = api_Uri_ReportServer.Host;
                    if (!CUtils.IsIPAddress(hostReportServer))
                    {
                        RedirectActionToLogin();
                    }
                }
                if (!CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
                {
                    var api_Uri_MasterServer = new Uri(ServiceAddress.BaseMasterServerAPIAddress);
                    var hostMasterServer = api_Uri_MasterServer.Host;
                    if (!CUtils.IsIPAddress(hostMasterServer))
                    {
                        RedirectActionToLogin();
                    }
                }
            }
            else
            {
                RedirectActionToLogin();
            }
        }
        public AdminBaseController()
        {
            #region["AdminBaseController"]
            if (CUtils.IsNullOrEmpty(Hethong))
            {
                Hethong = ConfigurationManager.AppSettings["HeThong"];
            }

            //CheckApi_Url(Hethong);

            if (CUtils.IsNullOrEmpty(RootFolder))
            {
                string DiskCode = CUtils.StrValue(ConfigurationManager.AppSettings["DiskCode"]);
                string FolderInBrandCloud = CUtils.StrValue(ConfigurationManager.AppSettings["FolderInBrandCloud"]);
                RootFolder = DiskCode + @":\" + FolderInBrandCloud;
            }

            if (CUtils.IsNullOrEmpty(ReportBro_Key))
            {
                ReportBro_Key = ConfigurationManager.AppSettings["ReportBro_Key"];
            }
            if (CUtils.IsNullOrEmpty(ReportBro_ServerUrl))
            {
                ReportBro_ServerUrl = ConfigurationManager.AppSettings["ReportBro_ServerUrl"];
            }

            #endregion

            #region["InBrand Cloud Config"]
            var rowsWorksheets = ConfigurationManager.AppSettings["RowsWorksheets"];
            if (!CUtils.IsNullOrEmpty(rowsWorksheets))
            {
                RowsWorksheets = Convert.ToInt32(rowsWorksheets);
            }
            if (CUtils.IsNullOrEmpty(NetworkID))
            {
                NetworkID = ConfigurationManager.AppSettings["NetworkID"];
            }
            if (CUtils.IsNullOrEmpty(TokenIDFix))
            {
                TokenIDFix = ConfigurationManager.AppSettings["TokenID"];
            }
            #endregion

            #region["APIs"]
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
            {
                ServiceAddress.BaseReportServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseMasterServerAPIAddress))
            {
                ServiceAddress.BaseMasterServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseMasterServerAPIAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BasePayGateAddress))
            {
                ServiceAddress.BasePayGateAddress = System.Configuration.ConfigurationManager.AppSettings["BasePayGateAddress"];
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseINOSAddress))
            {
                ServiceAddress.BaseINOSAddress = System.Configuration.ConfigurationManager.AppSettings["BaseINOSAddress"];
                inos.common.Paths.SetInosServerBaseAddress(ServiceAddress.BaseINOSAddress);
            }
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseAPIInbrand))
            {
                ServiceAddress.BaseAPIInbrand = System.Configuration.ConfigurationManager.AppSettings["BaseAPIInbrand"];
            }
            #endregion
        }
        #region ["Init"]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            //int sessionTimeout = System.Web.HttpContext.Current.Session.Timeout;

            //var networkid = (string)RouteData.Values["networkid"];
            //var routeData = RouteData.Values;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.Init();
        }
        protected virtual void Init()
        {

            if (User.Identity.IsAuthenticated)
            {
                #region["Init"]
                #region["Fonts"]
                if (CUtils.IsNullOrEmpty(FontPath.FontTimeNewRomanPath))
                {
                    var pathFolderFonts = Server.MapPath(@"~\fonts\");
                    FontPath.FontTimeNewRomanPath = Path.Combine(pathFolderFonts, "times_0.ttf");
                }
                if (CUtils.IsNullOrEmpty(FontPath.FontArialPath))
                {
                    var pathFolderFonts = Server.MapPath(@"~\fonts\");
                    FontPath.FontArialPath = Path.Combine(pathFolderFonts, "arial.ttf");
                }
                if (CUtils.IsNullOrEmpty(FontPath.FontChakraPetchBold))
                {
                    var pathFolderFonts = Server.MapPath(@"~\fonts\");
                    FontPath.FontChakraPetchBold = Path.Combine(pathFolderFonts, "ChakraPetch-Light.ttf");
                }
                #endregion

                if (UserState == null || UserState.SysUser == null)
                {
                    FormsAuthentication.SignOut();
                    var redirectUrl = RedirectActionToLogin();
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    var userName = "";
                    var organName = "";
                    if (!CUtils.IsNullOrEmpty(UserState.UserName))
                    {
                        userName = UserState.UserName.Trim();
                    }
                    if (UserState.SysUser != null && !CUtils.IsNullOrEmpty(UserState.SysUser.mo_OrganName))
                    {
                        organName = UserState.SysUser.mo_OrganName.Trim();
                    }
                    ViewBag.UserName = userName;
                    ViewBag.OrganName = organName;
                    Session_List_Mst_ColumnConfig_Get();
                    UserState.HttpSessionStateBase = SessionWrapper.Session;
                }
                #endregion
            }
            else
            {
                FormsAuthentication.SignOut();
                var redirectUrl = RedirectActionToLogin();
                Response.Redirect(redirectUrl);
            }
        }
        #endregion
        #region["File - Folder"]
        public string CheckFolderExists(string path = "")
        {
            var strFolder = "";
            if (!string.IsNullOrEmpty(path))
            {
                var strPath = Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            else
            {
                path = "/Uploads";
                var strPath = Server.MapPath(path);
                bool exists = System.IO.Directory.Exists(strPath);
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(strPath);
                }
                strFolder = strPath.Trim();
            }
            return strFolder;
        }
        //public string CheckFolderCurrentExists(string path = "")
        //{
        //    var strFolder = "";
        //    if (!string.IsNullOrEmpty(path))
        //    {
        //        var strPath = System.Web.HttpContext.Current.Server.MapPath(path);
        //        bool exists = System.IO.Directory.Exists(strPath);
        //        if (!exists)
        //        {
        //            System.IO.Directory.CreateDirectory(strPath);
        //        }
        //        strFolder = strPath.Trim();
        //    }
        //    else
        //    {
        //        path = "/Uploads";
        //        var strPath = System.Web.HttpContext.Current.Server.MapPath(path);
        //        bool exists = System.IO.Directory.Exists(strPath);
        //        if (!exists)
        //        {
        //            System.IO.Directory.CreateDirectory(strPath);
        //        }
        //        strFolder = strPath.Trim();
        //    }
        //    return strFolder;
        //    //return System.Web.HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", GetTempDir(), fileId, extension));
        //}
        #endregion
        #region["GenFilePath"]
        public string GenExcelExportFilePath(string prefix, ref string virualPath)
        {
            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {
                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }

        public string GenExcelExportFilePath(string ext, string prefix, ref string virualPath)
        {
            if (string.IsNullOrEmpty(ext))
            {
                ext = ".xls";
            }
            String subpath = string.Format("/TempFiles/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}{2}", subpath, filename, ext);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {

                virualPath = string.Format("{0}/{1}_{2}{3}", subpath, filename, ++i, ext);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }

        public string GenExcelExportReportFilePath(string prefix, ref string virualPath)
        {
            String subpath = string.Format("/Reports/{0}", DateTime.Now.ToString("yyyy-MM-dd"));
            string filename = string.Format("{0}_{1}", prefix, DateTime.Now.ToString("yyMMddHHmmss"));
            string subpathPhys = Server.MapPath(subpath);
            if (!Directory.Exists(subpathPhys))
            {
                Directory.CreateDirectory(subpathPhys);
            }
            virualPath = string.Format("{0}/{1}.xlsx", subpath, filename);
            string filePath = Server.MapPath(virualPath);
            int i = 0;
            while (System.IO.File.Exists(filePath))
            {
                virualPath = string.Format("{0}/{1}_{2}.xlsx", subpath, filename, ++i);
                filePath = Server.MapPath(virualPath);
            }
            return filePath;
        }
        #endregion
        #region["SysObject Setting"]
        static System.Threading.ReaderWriterLockSlim locker = new System.Threading.ReaderWriterLockSlim();
        public SysObjectSetting GetSysObjectSetting()
        {
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data"), "SysObjectSetting.config");
            if (!System.IO.File.Exists(filePath)) return null;
            SysObjectSetting ret = null;
            locker.EnterReadLock();
            try
            {
                ret = Serialization.Deserialize<SysObjectSetting>(filePath);
            }
            finally
            {
                locker.ExitReadLock();
            }
            return ret;
        }
        public void SaveSysObjectSetting(SysObjectSetting item)
        {
            string filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/App_Data"), "SysObjectSetting.config");
            locker.EnterWriteLock();
            try
            {
                Serialization.SerializeSettings<SysObjectSetting>(item, filePath);
            }
            finally
            {
                locker.ExitWriteLock();
            }
        }
        public void UpdateSysObjectFunctions(string objectCode, List<string> functions)
        {
            if (string.IsNullOrEmpty(objectCode)) return;
            SysObjectSetting setting = GetSysObjectSetting();
            if (setting == null)
            {
                setting = new SysObjectSetting();
            }
            if (setting.List == null) setting.List = new List<SysObjectFunction>();
            int idx = -1;
            for (int i = 0; i < setting.List.Count; ++i)
            {
                var f = setting.List[i];
                if (f.ObjectCode.Equals(objectCode, StringComparison.InvariantCultureIgnoreCase))
                {
                    idx = i;
                    break;
                }
            }
            if (idx >= 0)
            {
                setting.List[idx].FunctionCodes = functions;
            }
            else
            {
                SysObjectFunction o = new SysObjectFunction()
                {
                    ObjectCode = objectCode.ToUpper().Trim(),
                    FunctionCodes = functions,
                };
                setting.List.Add(o);
            }
            SaveSysObjectSetting(setting);
        }
        public List<string> ResolveObjects(List<string> objectCodes)
        {
            List<string> list = new List<string>();
            var listAccess = new List<Sys_Access>();
            SysObjectSetting setting = GetSysObjectSetting();

            Dictionary<string, bool> dic = new Dictionary<string, bool>();
            if (setting != null && setting.List != null)
            {
                foreach (var code in objectCodes)
                {
                    if (!dic.ContainsKey(code))
                    {
                        dic.Add(code, true);
                        list.Add(code);
                    }


                    foreach (var scode in setting.List)
                    {
                        if (scode.ObjectCode.Equals(code, StringComparison.InvariantCultureIgnoreCase))
                        {
                            foreach (var f in scode.FunctionCodes)
                            {
                                if (!string.IsNullOrEmpty(f))
                                {
                                    if (!dic.ContainsKey(f))
                                    {
                                        dic.Add(f, true);
                                        var arrCode = f.Split(',');
                                        if (arrCode != null && arrCode.Length > 0)
                                        {
                                            for (int i = 0; i < arrCode.Length; i++)
                                            {
                                                var strCode = arrCode[i];
                                                list.Add(strCode);
                                            }
                                        }
                                        //list.Add(f);
                                    }
                                }

                            }
                        }
                    }
                }
            }

            else list = objectCodes;

            return list;
        }
        #endregion
        #region["Common"]
        //#region["Bộ phận/Phòng ban"]
        //protected List<Mst_Department> WA_Mst_Department_Get(RQ_Mst_Department objRQ_Mst_Department)
        //{
        //    var addressAPIs = UserState.AddressAPIs;
        //    var listMst_Department = new List<Mst_Department>();

        //    var objRT_Mst_Department = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
        //    if (objRT_Mst_Department.Lst_Mst_Department != null && objRT_Mst_Department.Lst_Mst_Department.Count > 0)
        //    {
        //        listMst_Department.AddRange(objRT_Mst_Department.Lst_Mst_Department);
        //    }
        //    return listMst_Department;
        //}
        //protected List<Mst_Department> List_Mst_Department(string strWhereClause)
        //{
        //    var waUserCode = UserState.SysUser.UserCode;
        //    var waUserPassword = UserState.SysUser.UserPassword;
        //    var listMst_Department = new List<Mst_Department>();

        //    var objRQ_Mst_Department = new RQ_Mst_Department()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = GwUserCode,
        //        GwPassword = GwPassword,
        //        FuncType = null,
        //        Ft_RecordStart = Ft_RecordStart,
        //        Ft_RecordCount = Ft_RecordCount,
        //        Ft_WhereClause = strWhereClause,
        //        Ft_Cols_Upd = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        UtcOffset = UserState.UtcOffset,
        //        AccessToken = UserState.AccessToken.ToString().Trim(),
        //        NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
        //        OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
        //        // RQ_Mst_Department
        //        Rt_Cols_Mst_Department = "*",
        //        Mst_Department = null,
        //    };
        //    listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
        //    return listMst_Department;
        //}
        //private string strWhereClause_Department_Base(Mst_Department obj)
        //{
        //    var strWhereClause = "";
        //    var sbSql = new StringBuilder();
        //    var Tbl_Mst_Department = TableName.Mst_Department + ".";
        //    if (!CUtils.IsNullOrEmpty(obj.MST))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.MST, obj.MST, "=");
        //    }
        //    if (!CUtils.IsNullOrEmpty(obj.NetworkID))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.NetworkID, obj.NetworkID, "=");
        //    }
        //    if (!CUtils.IsNullOrEmpty(obj.FlagActive))
        //    {
        //        sbSql.AddWhereClause(Tbl_Mst_Department + TblMst_Department.FlagActive, obj.FlagActive, "=");
        //    }
        //    strWhereClause = sbSql.ToString();
        //    return strWhereClause;
        //}

        //protected List<Mst_DepartmentExt> ListDepartmentExt(string strWhereClause = "")
        //{
        //    var waUserCode = UserState.SysUser.UserCode;
        //    var waUserPassword = UserState.SysUser.UserPassword;
        //    var listMst_Department = new List<Mst_Department>();
        //    var listMst_DepartmentExt = new List<Mst_DepartmentExt>();
        //    var _listMst_DepartmentExt = new List<Mst_DepartmentExt>();
        //    var strWhereClauseMst_Department = "";
        //    if (CUtils.IsNullOrEmpty(strWhereClause))
        //    {
        //        var objMst_Department = new Mst_Department()
        //        {
        //            FlagActive = Client_Flag.Active,
        //        };
        //        strWhereClauseMst_Department = strWhereClause_Department_Base(objMst_Department);
        //    }
        //    else
        //    {
        //        strWhereClauseMst_Department = strWhereClause;
        //    }
        //    var objRQ_Mst_Department = new RQ_Mst_Department()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = GwUserCode,
        //        GwPassword = GwPassword,
        //        Ft_RecordStart = Ft_RecordStart,
        //        Ft_RecordCount = Ft_RecordCount,
        //        Ft_WhereClause = strWhereClauseMst_Department,
        //        Ft_Cols_Upd = null,
        //        FuncType = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        // RQ_Mst_PaymentPartnerType
        //        Rt_Cols_Mst_Department = "*"
        //    };
        //    listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
        //    if (listMst_Department != null && listMst_Department.Count > 0)
        //    {
        //        foreach (var item in listMst_Department)
        //        {
        //            var objMst_DepartmentExt = new Mst_DepartmentExt()
        //            {
        //                DepartmentCode = item.DepartmentCode,
        //                DepartmentCodeParent = item.DepartmentCodeParent,
        //                DepartmentBUCode = item.DepartmentBUCode,
        //                DepartmentBUPattern = item.DepartmentBUPattern,
        //                DepartmentLevel = item.DepartmentLevel,
        //                DepartmentName = item.DepartmentName,
        //                MST = item.MST,
        //                NetworkID = item.NetworkID,
        //                FlagActive = item.FlagActive,
        //                LogLUDTimeUTC = item.LogLUDTimeUTC,
        //                LogLUBy = item.LogLUBy,
        //            };
        //            _listMst_DepartmentExt.Add(objMst_DepartmentExt);
        //        }

        //        var getGroupBaseList = Mst_DepartmentExtension.GetGroupBaseList(_listMst_DepartmentExt);
        //        var toGroupBaseTree = Mst_DepartmentExtension.ToGroupBaseTree(getGroupBaseList);
        //        var toFlatGroupBaseTree = Mst_DepartmentExtension.ToFlatGroupBaseTree(toGroupBaseTree);

        //        if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
        //        {
        //            listMst_DepartmentExt.AddRange(toFlatGroupBaseTree);
        //        }
        //    }
        //    return listMst_DepartmentExt;
        //}
        //#endregion
        //#region["Quận/Huyện"]
        //protected List<Mst_District> WA_Mst_District_Get(RQ_Mst_District objRQ_Mst_District)
        //{
        //    var addressAPIs = UserState.AddressAPIs;
        //    var listMst_District = new List<Mst_District>();

        //    var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, addressAPIs);
        //    if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
        //    {
        //        listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
        //    }
        //    return listMst_District;
        //}
        //protected List<Mst_District> List_Mst_District(string strWhereClause)
        //{
        //    var waUserCode = UserState.SysUser.UserCode;
        //    var waUserPassword = UserState.SysUser.UserPassword;
        //    var listMst_District = new List<Mst_District>();

        //    var objRQ_Mst_District = new RQ_Mst_District()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = GwUserCode,
        //        GwPassword = GwPassword,
        //        FuncType = null,
        //        Ft_RecordStart = Ft_RecordStart,
        //        Ft_RecordCount = Ft_RecordCount,
        //        Ft_WhereClause = strWhereClause,
        //        Ft_Cols_Upd = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        UtcOffset = UserState.UtcOffset,
        //        // RQ_Mst_District
        //        Rt_Cols_Mst_District = "*",
        //        Mst_District = null,
        //    };
        //    listMst_District = WA_Mst_District_Get(objRQ_Mst_District);
        //    return listMst_District;
        //}

        //#endregion
        //#region["Tỉnh/Thành phố"]
        //protected List<Mst_Province> WA_Mst_Province_Get(RQ_Mst_Province objRQ_Mst_Province)
        //{
        //    var addressAPIs = UserState.AddressAPIs;
        //    var listMst_Province = new List<Mst_Province>();
        //    var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, addressAPIs);
        //    if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
        //    {
        //        listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
        //    }
        //    return listMst_Province;
        //}
        //protected List<Mst_Province> List_Mst_Province(string strWhereClause)
        //{
        //    var waUserCode = UserState.SysUser.UserCode;
        //    var waUserPassword = UserState.SysUser.UserPassword;
        //    var listMst_Province = new List<Mst_Province>();

        //    var objRQ_Mst_Province = new RQ_Mst_Province()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = GwUserCode,
        //        GwPassword = GwPassword,
        //        FuncType = null,
        //        Ft_RecordStart = Ft_RecordStart,
        //        Ft_RecordCount = Ft_RecordCount,
        //        Ft_WhereClause = strWhereClause,
        //        Ft_Cols_Upd = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        UtcOffset = UserState.UtcOffset,
        //        // RQ_Mst_Province
        //        Rt_Cols_Mst_Province = "*",
        //        Mst_Province = null,
        //    };
        //    listMst_Province = WA_Mst_Province_Get(objRQ_Mst_Province);
        //    return listMst_Province;
        //}

        //#endregion
        #region["Quản lý đại lý"]
        protected List<Mst_Dealer> WA_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Mst_Dealer != null && objRT_Mst_Dealer.Lst_Mst_Dealer.Count > 0)
            {
                listMst_Dealer.AddRange(objRT_Mst_Dealer.Lst_Mst_Dealer);
            }
            return listMst_Dealer;
        }
        protected List<Mst_Dealer> List_Mst_Dealer(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };
            listMst_Dealer = WA_Mst_Dealer_Get(objRQ_Mst_Dealer);
            return listMst_Dealer;
        }


        #endregion
        //#region["Loại giấy tờ"]
        //protected List<Mst_GovIDType> WA_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        //{
        //    var addressAPIs = UserState.AddressAPIs;
        //    var listMst_GovIDType = new List<Mst_GovIDType>();

        //    var objRT_Mst_GovIDType = Mst_GovIDTypeService.Instance.WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType, addressAPIs);
        //    if (objRT_Mst_GovIDType.Lst_Mst_GovIDType != null && objRT_Mst_GovIDType.Lst_Mst_GovIDType.Count > 0)
        //    {
        //        listMst_GovIDType.AddRange(objRT_Mst_GovIDType.Lst_Mst_GovIDType);
        //    }
        //    return listMst_GovIDType;
        //}
        //protected List<Mst_GovIDType> List_Mst_GovIDType(string strWhereClause)
        //{
        //    var waUserCode = UserState.SysUser.UserCode;
        //    var waUserPassword = UserState.SysUser.UserPassword;
        //    var listMst_GovIDType = new List<Mst_GovIDType>();

        //    var objRQ_Mst_GovIDType = new RQ_Mst_GovIDType()
        //    {
        //        // WARQBase
        //        Tid = GetNextTId(),
        //        GwUserCode = GwUserCode,
        //        GwPassword = GwPassword,
        //        FuncType = null,
        //        Ft_RecordStart = Ft_RecordStart,
        //        Ft_RecordCount = Ft_RecordCount,
        //        Ft_WhereClause = strWhereClause,
        //        Ft_Cols_Upd = null,
        //        WAUserCode = waUserCode,
        //        WAUserPassword = waUserPassword,
        //        UtcOffset = UserState.UtcOffset,
        //        // RQ_Mst_GovIDType
        //        Rt_Cols_Mst_GovIDType = "*",
        //        Mst_GovIDType = null,
        //    };
        //    listMst_GovIDType = WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType);
        //    return listMst_GovIDType;
        //}
        //#endregion
        #region["Chi cục thuế"]
        protected List<Mst_GovTaxID> List_Mst_GovTaxID(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_GovTaxID = new List<Mst_GovTaxID>();

            var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_GovTaxID
                Rt_Cols_Mst_GovTaxID = "*",
                Mst_GovTaxID = null,
            };
            var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
            if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
            {
                listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
            }
            return listMst_GovTaxID;
        }

        #endregion
        #region["Solution"]
        protected List<Sys_Solution> WA_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listSys_Solution = new List<Sys_Solution>();

            var objRT_Mst_Dealer = Sys_SolutionService.Instance.WA_Sys_Solution_Get(objRQ_Sys_Solution, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Sys_Solution != null && objRT_Mst_Dealer.Lst_Sys_Solution.Count > 0)
            {
                listSys_Solution.AddRange(objRT_Mst_Dealer.Lst_Sys_Solution);
            }
            return listSys_Solution;
        }
        protected List<Sys_Solution> List_Sys_Solution(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listSys_Solution = new List<Sys_Solution>();

            var objRQ_Sys_Solution = new RQ_Sys_Solution()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Sys_Solution
                Rt_Cols_Sys_Solution = "*",
            };
            listSys_Solution = WA_Sys_Solution_Get(objRQ_Sys_Solution);
            return listSys_Solution;
        }
        #endregion
        #region["Seq_Common"]
        protected string SeqNo(Seq_Common objSeq_Common)
        {
            var seqNo = "";
            var addressAPIs = UserState.AddressAPIs;
            var objRQ_Seq_Common = new RQ_Seq_Common()
            {
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
                Seq_Common = objSeq_Common,
            };
            var objRT_Seq_Common = Seq_CommonService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPIs);
            seqNo = objRT_Seq_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            return seqNo;
        }
        #endregion
        #region["LoadDataInSolution"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_DistrictInSolution(string provincecode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_District = new List<Mst_District>();

                if (!CUtils.IsNullOrEmpty(provincecode))
                {
                    var strWhereClauseMst_District = "";
                    strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District() { ProvinceCode = provincecode, FlagActive = Client_Flag.Active });

                    var objRQ_Mst_District = new RQ_Mst_District()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_District,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        // RQ_Mst_District
                        Rt_Cols_Mst_District = "*",
                        Mst_District = null,
                    };
                    var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, addressAPIs);
                    if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
                    {
                        listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
                    }
                }
                return JsonView("LoadMst_DistrictInSolution", listMst_District);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_District", null, resultEntry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoadMst_GovTaxIDInSolution(string districtcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var listMst_GovTaxID = new List<Mst_GovTaxID>();

                if (!CUtils.IsNullOrEmpty(districtcode))
                {
                    var strWhereClauseMst_GovTaxID = "";
                    strWhereClauseMst_GovTaxID = strWhereClause_Mst_GovTaxID(new Mst_GovTaxID() { DistrictCode = districtcode, FlagActive = Client_Flag.Active });

                    var objRQ_Mst_GovTaxID = new RQ_Mst_GovTaxID()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_GovTaxID,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                        // RQ_Mst_GovTaxID
                        Rt_Cols_Mst_GovTaxID = "*",
                        Mst_GovTaxID = null,
                    };
                    var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
                    if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
                    {
                        listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
                    }
                }
                return JsonView("LoadMst_GovTaxIDInSolution", listMst_GovTaxID);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("LoadMst_GovTaxID", null, resultEntry);
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
        private string strWhereClause_Mst_GovTaxID(Mst_GovTaxID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovTaxID = TableName.Mst_GovTaxID + ".";
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.DistrictCode, data.DistrictCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovTaxID + TblMst_GovTaxID.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
        #region["Upload File"]
        protected string[] WA_UploadFileNew(Common.Models.RQ_File objFile, string addressAPIs)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var objRQ_File = new Common.Models.RQ_File()
            {
                folderUpload = objFile.folderUpload ?? "UploadedFiles",
                fileName = objFile.fileName,
                uploadFileAsBase64String = objFile.uploadFileAsBase64String,
                Tid = GetNextDateTId(),
                GwPassword = GwPassword,
                GwUserCode = GwUserCode,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword
            };
            var result = new FileService().WA_UploadFileNew(objRQ_File, addressAPIs);
            return result;
        }
        protected string[] WA_MoveFileNew(Common.Models.RQ_File objFile, string addressAPIs)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var objRQ_File = new Common.Models.RQ_File()
            {
                folderUpload = objFile.folderUpload ?? "UploadedFiles",
                fileName = "",
                sourceFileName = objFile.sourceFileName,
                destFileName = objFile.destFileName,
                uploadFileAsBase64String = objFile.uploadFileAsBase64String,
                Tid = GetNextDateTId(),
                GwPassword = GwPassword,
                GwUserCode = GwUserCode,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword
            };
            var result = new FileService().WA_MoveFileNew(objRQ_File, addressAPIs);
            return result;
        }
        #endregion

        #region ["Nhóm sản phẩm"]
        protected List<Mst_PartMaterialType> WA_Mst_PartMaterialType_Get(RQ_Mst_PartMaterialType objRQ_Mst_PartMaterialType)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_PartMaterialType = new List<Mst_PartMaterialType>();

            var objRT_Mst_PartMaterialType = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, addressAPIs);
            if (objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType != null && objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType.Count > 0)
            {
                listMst_PartMaterialType.AddRange(objRT_Mst_PartMaterialType.Lst_Mst_PartMaterialType);
            }
            return listMst_PartMaterialType;
        }
        protected List<Mst_PartMaterialType> List_Mst_PartMaterialType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_PartMaterialType = new List<Mst_PartMaterialType>();

            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.AccessToken.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_Province
                Rt_Cols_Mst_PartMaterialType = "*",
                Mst_PartMaterialType = null,
            };
            listMst_PartMaterialType = WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType);
            return listMst_PartMaterialType;
        }
        #endregion
        #region["Loại khách hàng"]
        protected List<Mst_NNTType> List_Mst_NNTType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_NNTType = new List<Mst_NNTType>();

            var objRQ_Mst_NNTType = new RQ_Mst_NNTType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_NNTType
                Rt_Cols_Mst_NNTType = "*",
                Mst_NNTType = null,
            };
            var objRT_Mst_NNTType = Mst_NNTTypeService.Instance.WA_Mst_NNTType_Get(objRQ_Mst_NNTType, addressAPIs);
            if (objRT_Mst_NNTType.Lst_Mst_NNTType != null && objRT_Mst_NNTType.Lst_Mst_NNTType.Count > 0)
            {
                listMst_NNTType.AddRange(objRT_Mst_NNTType.Lst_Mst_NNTType);
            }
            return listMst_NNTType;
        }

        #endregion
        #region["Khách hàng"]
        protected List<Mst_NNT> List_Mst_NNT(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_NNT = new List<Mst_NNT>();

            var objRQ_Mst_NNT = new RQ_Mst_NNT()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.AccessToken.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_NNT
                Rt_Cols_Mst_NNT = "*",
                Mst_NNT = null,
            };
            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
            {
                listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
            }
            return listMst_NNT;
        }
        #endregion
        #region ["Thông tin số lượng hóa đơn"]

        public List<Invoice_license> WA_Invoice_license_Get_By_MST()
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPI = UserState.AddressAPIs;
            var listInvoice_license = new List<Invoice_license>();
            var objRQ_Invoice_license = new RQ_Invoice_license
            {
                Rt_Cols_Invoice_license = "*",
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount
            };
            var objRT_Invoice_license = Invoice_licenseService.Instance.WA_Invoice_license_Get(objRQ_Invoice_license, addressAPI);
            if (objRT_Invoice_license.Lst_Invoice_license != null && objRT_Invoice_license.Lst_Invoice_license.Count > 0)
            {
                var mst = CUtils.StrValue(UserState.Mst_NNT.MST);
                if (!CUtils.IsNullOrEmpty(mst))
                {
                    listInvoice_license = objRT_Invoice_license.Lst_Invoice_license.Where(x => x.MST.ToString() == mst.ToString()).ToList();
                }
            }

            return listInvoice_license;
        }
        #endregion

        #region ["Inbrand Clound"]
        protected List<Mst_Part> List_Mst_Part(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Part = new List<Mst_Part>();

            var objRQ_Mst_Part = new RQ_Mst_Part()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.AccessToken.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_NNT
                Rt_Cols_Mst_Part = "*",
                Mst_Part = null,
            };
            var objRT_Mst_Part = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
            if (objRT_Mst_Part.Lst_Mst_Part != null && objRT_Mst_Part.Lst_Mst_Part.Count > 0)
            {
                listMst_Part.AddRange(objRT_Mst_Part.Lst_Mst_Part);
            }
            return listMst_Part;
        }
        protected List<Mst_PartType> List_Mst_PartType(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_PartType = new List<Mst_PartType>();

            var objRQ_Mst_PartType = new RQ_Mst_PartType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.AccessToken.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_NNT
                Rt_Cols_Mst_PartType = "*",
                Mst_PartType = null,
            };
            var objRT_Mst_Part = Mst_PartTypeService.Instance.WA_Mst_PartType_Get(objRQ_Mst_PartType, addressAPIs);
            if (objRT_Mst_Part.Lst_Mst_PartType != null && objRT_Mst_Part.Lst_Mst_PartType.Count > 0)
            {
                listMst_PartType.AddRange(objRT_Mst_Part.Lst_Mst_PartType);
            }
            return listMst_PartType;
        }
        protected List<Mst_PartUnit> List_Mst_PartUnit(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
            var listMst_PartUnit = new List<Mst_PartUnit>();

            var objRQ_Mst_PartUnit = new RQ_Mst_PartUnit()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                AccessToken = UserState.AccessToken.ToString().Trim(),
                NetworkID = UserState.Mst_NNT.NetworkID.ToString().Trim(),
                OrgID = UserState.Mst_NNT.OrgID.ToString().Trim(),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                // RQ_Mst_NNT
                Rt_Cols_Mst_PartUnit = "*",
                Mst_PartUnit = null,
            };
            var objRT_Mst_PartUnit = Mst_PartUnitService.Instance.WA_Mst_PartUnit_Get(objRQ_Mst_PartUnit, addressAPIs);
            if (objRT_Mst_PartUnit.Lst_Mst_PartUnit != null && objRT_Mst_PartUnit.Lst_Mst_PartUnit.Count > 0)
            {
                listMst_PartUnit.AddRange(objRT_Mst_PartUnit.Lst_Mst_PartUnit);
            }
            return listMst_PartUnit;
        }
        #endregion


        #region["Trường động hàng hoá"]
        protected List<Product_CustomField> WA_Product_CustomField_Get(RQ_Product_CustomField objRQ_Product_CustomField)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listProduct_CustomField = new List<Product_CustomField>();
            var objRT_Product_CustomField = ProductCustomFieldService.Instance.WA_Product_CustomField_Get(objRQ_Product_CustomField, addressAPIs);
            if (objRT_Product_CustomField.Lst_Product_CustomField != null && objRT_Product_CustomField.Lst_Product_CustomField.Count > 0)
            {
                listProduct_CustomField.AddRange(objRT_Product_CustomField.Lst_Product_CustomField);
            }
            return listProduct_CustomField;
        }
        public async Task<List<Product_CustomField>> List_Product_CustomField_Async(RQ_Product_CustomField objRQ_Product_CustomField)
        {
            Task<List<Product_CustomField>> list_Product_CustomField_Task = List_Product_CustomField_Task(objRQ_Product_CustomField);
            List<Product_CustomField> list_Product_CustomField = await list_Product_CustomField_Task;
            return list_Product_CustomField;
        }
        public Task<List<Product_CustomField>> List_Product_CustomField_Task(RQ_Product_CustomField objRQ_Product_CustomField)
        {
            TaskCompletionSource<List<Product_CustomField>> tcs = new TaskCompletionSource<List<Product_CustomField>>();
            var listProduct_CustomField = new List<Product_CustomField>();
            listProduct_CustomField = WA_Product_CustomField_Get(objRQ_Product_CustomField);
            tcs.TrySetResult(listProduct_CustomField);
            return tcs.Task;
        }
        #endregion

        #region ["Mst_Inventory_Async"]
        public Task<List<Mst_Inventory>> Task_Mst_Inventory_Get(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Inventory>> tcs = new TaskCompletionSource<List<Mst_Inventory>>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Inventory = new List<Mst_Inventory>();
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Customer_DynamicField
                Rt_Cols_Mst_Inventory = "*",
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, UserState.AddressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                listMst_Inventory.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }
            tcs.TrySetResult(listMst_Inventory);
            return tcs.Task;
        }

        public async Task<List<Mst_Inventory>> Mst_Inventory_Get_Async(string strWhereClause)
        {
            Task<List<Mst_Inventory>> list_Mst_Inventory_Task = Task_Mst_Inventory_Get(strWhereClause);
            List<Mst_Inventory> list_Mst_Inventory = await list_Mst_Inventory_Task;
            return list_Mst_Inventory;
        }

        public List<Mst_Inventory> List_Mst_Inventory_Get(string strWhereClause)
        {
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_Inventory = new List<Mst_Inventory>();
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Customer_DynamicField
                Rt_Cols_Mst_Inventory = "*",
            };
            var objRT_Mst_Inventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, UserState.AddressAPIs);
            if (objRT_Mst_Inventory != null && objRT_Mst_Inventory.Lst_Mst_Inventory.Count > 0)
            {
                listMst_Inventory.AddRange(objRT_Mst_Inventory.Lst_Mst_Inventory);
            }

            return listMst_Inventory;
        }
        #endregion

        #region ["Mst_InventoryType_Async"]
        public Task<List<Mst_InventoryType>> Task_Mst_InventoryType_Get(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_InventoryType>> tcs = new TaskCompletionSource<List<Mst_InventoryType>>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_InventoryType = new List<Mst_InventoryType>();
            var objRQ_Mst_InventoryType = new RQ_Mst_InventoryType()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // Rt
                Rt_Cols_Mst_InventoryType = "*",
            };
            var objRT_Mst_InventoryType = Mst_InventoryTypeService.Instance.WA_Mst_InventoryType_Get(objRQ_Mst_InventoryType, UserState.AddressAPIs);
            if (objRT_Mst_InventoryType != null && objRT_Mst_InventoryType.Lst_Mst_InventoryType.Count > 0)
            {
                listMst_InventoryType.AddRange(objRT_Mst_InventoryType.Lst_Mst_InventoryType);
            }
            tcs.TrySetResult(listMst_InventoryType);
            return tcs.Task;
        }

        public async Task<List<Mst_InventoryType>> Mst_InventoryType_Get_Async(string strWhereClause)
        {
            Task<List<Mst_InventoryType>> list_Mst_InventoryType_Task = Task_Mst_InventoryType_Get(strWhereClause);
            List<Mst_InventoryType> list_Mst_InventoryType = await list_Mst_InventoryType_Task;
            return list_Mst_InventoryType;
        }
        #endregion

        #region ["Mst_InventoryLevelType_Async"]
        public Task<List<Mst_InventoryLevelType>> Task_Mst_InventoryLevelType_Get(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_InventoryLevelType>> tcs = new TaskCompletionSource<List<Mst_InventoryLevelType>>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_InventoryLevelType = new List<Mst_InventoryLevelType>();
            var objRQ_Mst_InventoryLevelType = new RQ_Mst_InventoryLevelType()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // Rt
                Rt_Cols_Mst_InventoryLevelType = "*",
            };
            var objRT_Mst_InventoryLevelType = Mst_InventoryLevelTypeService.Instance.WA_Mst_InventoryLevelType_Get(objRQ_Mst_InventoryLevelType, UserState.AddressAPIs);
            if (objRT_Mst_InventoryLevelType != null && objRT_Mst_InventoryLevelType.Lst_Mst_InventoryLevelType.Count > 0)
            {
                listMst_InventoryLevelType.AddRange(objRT_Mst_InventoryLevelType.Lst_Mst_InventoryLevelType);
            }
            tcs.TrySetResult(listMst_InventoryLevelType);
            return tcs.Task;
        }

        public async Task<List<Mst_InventoryLevelType>> Mst_InventoryLevelType_Get_Async(string strWhereClause)
        {
            Task<List<Mst_InventoryLevelType>> list_Mst_InventoryLevelType_Task = Task_Mst_InventoryLevelType_Get(strWhereClause);
            List<Mst_InventoryLevelType> list_Mst_InventoryLevelType = await list_Mst_InventoryLevelType_Task;
            return list_Mst_InventoryLevelType;
        }
        #endregion

        #region ["Mst_InvInType_Async"]
        public Task<List<Mst_InvInType>> Task_Mst_InvInType_Get(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_InvInType>> tcs = new TaskCompletionSource<List<Mst_InvInType>>();

            var listMst_InvInType = new List<Mst_InvInType>();
            var objRQ_Mst_InvInType = new RQ_Mst_InvInType()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Customer_DynamicField
                Rt_Cols_Mst_InvInType = "*",
            };
            var objRT_Mst_InvInType = Mst_InvInTypeService.Instance.WA_Mst_InvInType_Get(objRQ_Mst_InvInType, UserState.AddressAPIs);
            if (objRT_Mst_InvInType != null && objRT_Mst_InvInType.Lst_Mst_InvInType.Count > 0)
            {
                listMst_InvInType.AddRange(objRT_Mst_InvInType.Lst_Mst_InvInType);
            }
            tcs.TrySetResult(listMst_InvInType);
            return tcs.Task;
        }

        public async Task<List<Mst_InvInType>> Mst_InvInType_Get_Async(string strWhereClause)
        {
            Task<List<Mst_InvInType>> list_Mst_InvInType_Task = Task_Mst_InvInType_Get(strWhereClause);
            List<Mst_InvInType> list_Mst_InvInType = await list_Mst_InvInType_Task;
            return list_Mst_InvInType;
        }
        #endregion

        #region["Mst_Product"]
        public List<Mst_Product> WA_Mst_Product_Get(RQ_Mst_Product objRQ_Mst_Product)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Product = new List<Mst_Product>();
            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            if (objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
            {
                listMst_Product.AddRange(objRT_Mst_Product.Lst_Mst_Product);
            }
            return listMst_Product;
        }

        public RT_Mst_Product RT_Mst_Product_Get(RQ_Mst_Product objRQ_Mst_Product)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Product = new List<Mst_Product>();
            var objRT_Mst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            //if (objRT_Mst_Product.Lst_Mst_Product != null && objRT_Mst_Product.Lst_Mst_Product.Count > 0)
            //{
            //    listMst_Product.AddRange(objRT_Mst_Product.Lst_Mst_Product);
            //}
            return objRT_Mst_Product;
        }
        public async Task<RT_Mst_Product> List_Mst_Product_Async(RQ_Mst_Product objRQ_Mst_Product)
        {
            Task<RT_Mst_Product> list_Mst_Product_Task = List_Mst_Product_Task(objRQ_Mst_Product);
            var objRT_Mst_Product = await list_Mst_Product_Task;
            return objRT_Mst_Product;
        }
        public Task<RT_Mst_Product> List_Mst_Product_Task(RQ_Mst_Product objRQ_Mst_Product)
        {
            TaskCompletionSource<RT_Mst_Product> tcs = new TaskCompletionSource<RT_Mst_Product>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var objRT_Mst_Product = RT_Mst_Product_Get(objRQ_Mst_Product);
            tcs.TrySetResult(objRT_Mst_Product);
            return tcs.Task;
        }
        #endregion

        #region["Mst_Customer"]
        public List<Mst_Customer> WA_Mst_Customer_Get(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Customer = new List<Mst_Customer>();
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
            if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
            {
                listMst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
            }
            return listMst_Customer;
        }

        public RT_Mst_Customer RT_Mst_Customer_Get(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Customer = new List<Mst_Customer>();
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
            //if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
            //{
            //    listMst_Customer.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
            //}
            return objRT_Mst_Customer;
        }
        public async Task<RT_Mst_Customer> List_Mst_Customer_Async(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            Task<RT_Mst_Customer> list_Mst_Customer_Task = List_Mst_Customer_Task(objRQ_Mst_Customer);
            var objRT_Mst_Customer = await list_Mst_Customer_Task;
            return objRT_Mst_Customer;
        }
        public Task<RT_Mst_Customer> List_Mst_Customer_Task(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            TaskCompletionSource<RT_Mst_Customer> tcs = new TaskCompletionSource<RT_Mst_Customer>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var objRT_Mst_Customer = RT_Mst_Customer_Get(objRQ_Mst_Customer);
            tcs.TrySetResult(objRT_Mst_Customer);
            return tcs.Task;
        }
        #endregion

        #region Mst_InvOutType Active='1'
        public List<Mst_InvOutType> Mst_InvOutTypeGetAllActive(string addressAPIs)
        {
            var lst_Mst_InvOutType = new List<Mst_InvOutType>();
            var objRQ_Mst_InventoryOutType = new RQ_Mst_InvOutType()
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
                Rt_Cols_Mst_InvOutType = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = "Mst_InvOutType.FlagActive = 1"
            };
            var rt = MstInvOutTypeService.Instance.WA_Mst_InventoryOutType_Get(objRQ_Mst_InventoryOutType, addressAPIs);
            if (rt != null && rt.Lst_Mst_InvOutType != null)
            {
                lst_Mst_InvOutType = rt.Lst_Mst_InvOutType;
            }
            return lst_Mst_InvOutType;
        }
        #endregion

        #region GetMstCustomer Active
        public List<Mst_Customer> Mst_Customer_GetActive(string addressAPIs, string ftrecordcount)
        {
            var lstCustomer = new List<Mst_Customer>();
            var objRQ_Mst_Customer = new RQ_Mst_Customer()
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
                Rt_Cols_Mst_Customer = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = "Mst_Customer.FlagActive = 1"
            };
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, addressAPIs);
            if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer != null)
            {
                lstCustomer = objRT_Mst_Customer.Lst_Mst_Customer;
            }
            return lstCustomer;
        }
        #endregion

        public List<Mst_Inventory> Mst_Inventory_GetKhoCon(string addressAPIs, string InvBUPattern)
        {
            var lst_Mst_Inventory = new List<Mst_Inventory>();
            var strWhereClause = "Mst_Inventory.FlagActive = 1 and Mst_Inventory.FlagIn_Out = 1 and Mst_Inventory.InvBUCode like '"+ InvBUPattern+ "'";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Rt_Cols_Mst_Inventory = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause
            };
            var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
            {
                lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
            }
            return lst_Mst_Inventory;
        }

        #region Mst_Inventory_Get FlagInOut = '1'
        public List<Mst_Inventory> Mst_Inventory_Get_Inventory(string addressAPIs)
        {
            var lst_Mst_Inventory = new List<Mst_Inventory>();
            var strWhereClause = "Mst_Inventory.FlagActive = 1 and Mst_Inventory.FlagIn_Out = 1";
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Rt_Cols_Mst_Inventory = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause
            };
            var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
            {
                lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
            }
            return lst_Mst_Inventory;
        }

        public List<Mst_Inventory> Mst_Inventory_Get(string addressAPIs, string strWhereClause)
        {
            var lst_Mst_Inventory = new List<Mst_Inventory>();            
            var objRQ_Mst_Inventory = new RQ_Mst_Inventory()
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
                Rt_Cols_Mst_Inventory = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause
            };
            var rtInventory = Mst_InventoryService.Instance.WA_Mst_Inventory_Get(objRQ_Mst_Inventory, addressAPIs);
            if (rtInventory != null && rtInventory.Lst_Mst_Inventory != null)
            {
                lst_Mst_Inventory = rtInventory.Lst_Mst_Inventory;
            }
            return lst_Mst_Inventory;
        }
        #endregion

        #region Hàng hóa đc bán = 1, 
        public List<Mst_Product> GetProductProductSale(string addressAPIs, string ftRecordCount)
        {
            var lst_Mst_Product = new List<Mst_Product>();
            var objRQ_Mst_Product = new RQ_Mst_Product()
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
                Rt_Cols_Mst_Product = "*",
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = ftRecordCount,
                Ft_WhereClause = "Mst_Product.FlagActive = '1' AND Mst_Product.FlagSell = '1' AND Mst_Product.ProductType in 'PRODUCT,COMBO'"
            };
            var rtMst_Product = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            if (rtMst_Product != null && rtMst_Product.Lst_Mst_Product != null)
            {
                lst_Mst_Product = rtMst_Product.Lst_Mst_Product;
            }
            return lst_Mst_Product;
        }
        #endregion
        #endregion
        #region ["Logs"]
        public string pathFile = "";
        public string rootFolder = "";
        public string fileLogName = "";
        public string PathFileiCount = "";

        public int ReturnCount()
        {
            var iCount = 0;
            if (!CUtils.IsNullOrEmpty(PathFileiCount))
            {
                StreamReader streamReader = new StreamReader(PathFileiCount, System.Text.Encoding.UTF8);
                var strCount = streamReader.ReadLine();
                if (CUtils.IsNumeric(strCount))
                {
                    iCount = Convert.ToInt32(strCount);
                }
                streamReader.Close();

                System.IO.File.WriteAllText(PathFileiCount, (iCount + 1).ToString(), Encoding.UTF8);
            }

            return iCount;
        }
        public void CreateFileWriteLog(string view, string date, int icount)
        {
            var stringBuilder = new StringBuilder();
            var folder = "";
            var strLogs = "";

            var dateCur = DateTimeNow();
            folder = date;
            var datetimenow = dateCur;
            CUtils.StrAppend(stringBuilder, "-------------------------------");
            CUtils.StrAppend(stringBuilder, datetimenow);
            CUtils.StrAppend(stringBuilder, "Màn hình: " + view);
            CUtils.StrAppend(stringBuilder, "");


            #region["Tạo file ghi Log"]
            fileLogName = icount + "_" + view + ".txt";
            pathFile = CUtils.PathFile(rootFolder, folder, fileLogName);
            System.Web.HttpContext.Current.Session["PathFileLog"] = pathFile;
            strLogs = stringBuilder.ToString();
            System.IO.File.AppendAllText(pathFile, strLogs, Encoding.UTF8);
            stringBuilder.Clear();
            #endregion
        }

        
        public bool SysAccessCheckDeny(UserState userState, string functionNames)
        {

            if (userState != null)
            {
                if (userState.IsSysAdmin)
                {
                    return true;
                }
                else
                {
                    if (functionNames != null && functionNames.Trim().Length > 0)
                    {
                        if (userState.ListSysAccess != null)
                        {
                            return userState.ListSysAccess.Any(a => a.ObjectCode.Equals(functionNames.Trim(), StringComparison.InvariantCultureIgnoreCase));
                        }
                    }

                }
            }


            return false;
        }

        #endregion
        #region["Mst_Department - Bộ phận - Phòng ban"]
        protected List<Mst_Department> WA_Mst_Department_Get(RQ_Mst_Department objRQ_Mst_Department)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_Department = new List<Mst_Department>();
            var objRT_Mst_Department = Mst_DepartmentService.Instance.WA_Mst_Department_Get(objRQ_Mst_Department, addressAPIs);
            if (objRT_Mst_Department.Lst_Mst_Department != null && objRT_Mst_Department.Lst_Mst_Department.Count > 0)
            {
                listMst_Department.AddRange(objRT_Mst_Department.Lst_Mst_Department);
            }
            return listMst_Department;
        }
        public async Task<List<Mst_Department>> List_Mst_Department_Async(string strWhereClause)
        {
            Task<List<Mst_Department>> list_Mst_Department_Task = List_Mst_Department_Task(strWhereClause);
            List<Mst_Department> list_Mst_Department = await list_Mst_Department_Task;
            return list_Mst_Department;
        }
        public Task<List<Mst_Department>> List_Mst_Department_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Department>> tcs = new TaskCompletionSource<List<Mst_Department>>();
            var listMst_Department = new List<Mst_Department>();
            var objRQ_Mst_Department = new RQ_Mst_Department()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Department
                Rt_Cols_Mst_Department = "*",
                Mst_Department = null,
            };
            listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
            tcs.TrySetResult(listMst_Department);
            return tcs.Task;
        }
        public async Task<List<Mst_Department>> List_Mst_Department_Async(RQ_Mst_Department objRQ_Mst_Department)
        {
            Task<List<Mst_Department>> list_Mst_Department_Task = List_Mst_Department_Task(objRQ_Mst_Department);
            List<Mst_Department> list_Mst_Department = await list_Mst_Department_Task;
            return list_Mst_Department;
        }
        public async Task<List<Mst_DepartmentExt>> List_Mst_DepartmentExt_Async(RQ_Mst_Department objRQ_Mst_Department)
        {
            Task<List<Mst_Department>> list_Mst_Department_Task = List_Mst_Department_Task(objRQ_Mst_Department);
            List<Mst_Department> list_Mst_Department = await list_Mst_Department_Task;
            var list_Mst_DepartmentExt = new List<Mst_DepartmentExt>();
            var _list_Mst_DepartmentExt = new List<Mst_DepartmentExt>();
            if (list_Mst_Department != null && list_Mst_Department.Count > 0)
            {
                foreach (var item in list_Mst_Department)
                {
                    var objMst_DepartmentExt = new Mst_DepartmentExt()
                    {
                        DepartmentCode = CUtils.StrValue(item.DepartmentCode),
                        NetworkID = CUtils.StrValue(item.NetworkID),
                        DepartmentCodeParent = CUtils.StrValue(item.DepartmentCodeParent),
                        DepartmentBUCode = CUtils.StrValue(item.DepartmentBUCode),
                        DepartmentBUPattern = CUtils.StrValue(item.DepartmentBUPattern),
                        DepartmentLevel = CUtils.StrValue(item.DepartmentLevel),
                        DepartmentName = CUtils.StrValue(item.DepartmentName),
                        FlagActive = CUtils.StrValue(item.FlagActive),
                        LogLUDTimeUTC = CUtils.StrValue(item.LogLUDTimeUTC),
                        LogLUBy = CUtils.StrValue(item.LogLUBy),
                    };
                    _list_Mst_DepartmentExt.Add(objMst_DepartmentExt);
                }
                var toGroupBaseTree = Mst_DepartmentExtension.ToGroupBaseTree(Mst_DepartmentExtension.GetGroupBaseList(_list_Mst_DepartmentExt));
                var toFlatGroupBaseTree = Mst_DepartmentExtension.ToFlatGroupBaseTree(toGroupBaseTree);
                if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
                {
                    list_Mst_DepartmentExt.AddRange(toFlatGroupBaseTree);
                }
            }
            return list_Mst_DepartmentExt;
        }
        public Task<List<Mst_Department>> List_Mst_Department_Task(RQ_Mst_Department objRQ_Mst_Department)
        {
            TaskCompletionSource<List<Mst_Department>> tcs = new TaskCompletionSource<List<Mst_Department>>();
            var listMst_Department = new List<Mst_Department>();
            listMst_Department = WA_Mst_Department_Get(objRQ_Mst_Department);
            tcs.TrySetResult(listMst_Department);
            return tcs.Task;
        }
        #endregion
        #region ["Mst_NNT Chi nhánh (map 1-1 với master org)"]
        protected List<Mst_NNT> WA_Mst_NNT_Get(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_NNT = new List<Mst_NNT>();
            var objRT_Mst_NNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(objRQ_Mst_NNT, addressAPIs);
            if (objRT_Mst_NNT.Lst_Mst_NNT != null && objRT_Mst_NNT.Lst_Mst_NNT.Count > 0)
            {
                listMst_NNT.AddRange(objRT_Mst_NNT.Lst_Mst_NNT);
            }
            return listMst_NNT;
        }
        public async Task<List<Mst_NNT>> List_Mst_NNT_Async(string strWhereClause)
        {
            Task<List<Mst_NNT>> list_Mst_NNT_Task = List_Mst_NNT_Task(strWhereClause);
            List<Mst_NNT> list_Mst_NNT = await list_Mst_NNT_Task;
            return list_Mst_NNT;
        }
        public Task<List<Mst_NNT>> List_Mst_NNT_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_NNT>> tcs = new TaskCompletionSource<List<Mst_NNT>>();
            var listMst_NNT = new List<Mst_NNT>();
            var objRQ_Mst_NNT = new RQ_Mst_NNT()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_NNT
                Rt_Cols_Mst_NNT = "*",
                Mst_NNT = null,
            };
            listMst_NNT = WA_Mst_NNT_Get(objRQ_Mst_NNT);
            tcs.TrySetResult(listMst_NNT);
            return tcs.Task;
        }
        public async Task<List<Mst_NNT>> List_Mst_NNT_Async(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            Task<List<Mst_NNT>> list_Mst_NNT_Task = List_Mst_NNT_Task(objRQ_Mst_NNT);
            List<Mst_NNT> list_Mst_NNT = await list_Mst_NNT_Task;
            return list_Mst_NNT;
        }
        public Task<List<Mst_NNT>> List_Mst_NNT_Task(RQ_Mst_NNT objRQ_Mst_NNT)
        {
            TaskCompletionSource<List<Mst_NNT>> tcs = new TaskCompletionSource<List<Mst_NNT>>();
            var listMst_NNT = new List<Mst_NNT>();
            listMst_NNT = WA_Mst_NNT_Get(objRQ_Mst_NNT);
            tcs.TrySetResult(listMst_NNT);
            return tcs.Task;
        }
        #endregion
        #region["Mst_Province - Tỉnh/ thành phố"]
        protected List<Mst_Province> WA_Mst_Province_Get(RQ_Mst_Province objRQ_Mst_Province)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_Province = new List<Mst_Province>();
            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_Mst_Province_Get(objRQ_Mst_Province, addressAPIs);
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }
            return listMst_Province;
        }
        public async Task<List<Mst_Province>> List_Mst_Province_Async(string strWhereClause)
        {
            Task<List<Mst_Province>> list_Mst_Province_Task = List_Mst_Province_Task(strWhereClause);
            List<Mst_Province> list_Mst_Province = await list_Mst_Province_Task;
            return list_Mst_Province;
        }
        public Task<List<Mst_Province>> List_Mst_Province_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Province>> tcs = new TaskCompletionSource<List<Mst_Province>>();
            var listMst_Province = new List<Mst_Province>();
            var objRQ_Mst_Province = new RQ_Mst_Province()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            listMst_Province = WA_Mst_Province_Get(objRQ_Mst_Province);
            tcs.TrySetResult(listMst_Province);
            return tcs.Task;
        }
        public async Task<List<Mst_Province>> List_Mst_Province_Async(RQ_Mst_Province objRQ_Mst_Province)
        {
            Task<List<Mst_Province>> list_Mst_Province_Task = List_Mst_Province_Task(objRQ_Mst_Province);
            List<Mst_Province> list_Mst_Province = await list_Mst_Province_Task;
            return list_Mst_Province;
        }
        public Task<List<Mst_Province>> List_Mst_Province_Task(RQ_Mst_Province objRQ_Mst_Province)
        {
            TaskCompletionSource<List<Mst_Province>> tcs = new TaskCompletionSource<List<Mst_Province>>();
            var listMst_Province = new List<Mst_Province>();
            listMst_Province = WA_Mst_Province_Get(objRQ_Mst_Province);
            tcs.TrySetResult(listMst_Province);
            return tcs.Task;
        }
        #endregion
        #region["Mst_District - Quận/ huyện"]
        protected List<Mst_District> WA_Mst_District_Get(RQ_Mst_District objRQ_Mst_District)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_District = new List<Mst_District>();
            var objRT_Mst_District = Mst_DistrictService.Instance.WA_Mst_District_Get(objRQ_Mst_District, addressAPIs);
            if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
            {
                listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
            }
            return listMst_District;
        }
        public async Task<List<Mst_District>> List_Mst_District_Async(string strWhereClause)
        {
            Task<List<Mst_District>> list_Mst_District_Task = List_Mst_District_Task(strWhereClause);
            List<Mst_District> list_Mst_District = await list_Mst_District_Task;
            return list_Mst_District;
        }
        public Task<List<Mst_District>> List_Mst_District_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_District>> tcs = new TaskCompletionSource<List<Mst_District>>();
            var listMst_District = new List<Mst_District>();
            var objRQ_Mst_District = new RQ_Mst_District()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_District
                Rt_Cols_Mst_District = "*",
                Mst_District = null,
            };
            listMst_District = WA_Mst_District_Get(objRQ_Mst_District);
            tcs.TrySetResult(listMst_District);
            return tcs.Task;
        }
        public async Task<List<Mst_District>> List_Mst_District_Async(RQ_Mst_District objRQ_Mst_District)
        {
            Task<List<Mst_District>> list_Mst_District_Task = List_Mst_District_Task(objRQ_Mst_District);
            List<Mst_District> list_Mst_District = await list_Mst_District_Task;
            return list_Mst_District;
        }
        public Task<List<Mst_District>> List_Mst_District_Task(RQ_Mst_District objRQ_Mst_District)
        {
            TaskCompletionSource<List<Mst_District>> tcs = new TaskCompletionSource<List<Mst_District>>();
            var listMst_District = new List<Mst_District>();
            listMst_District = WA_Mst_District_Get(objRQ_Mst_District);
            tcs.TrySetResult(listMst_District);
            return tcs.Task;
        }
        #endregion
        #region["Mst_Ward - Phường/ xã"]
        protected List<Mst_Ward> WA_Mst_Ward_Get(RQ_Mst_Ward objRQ_Mst_Ward)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_Ward = new List<Mst_Ward>();
            var objRT_Mst_Ward = Mst_WardService.Instance.WA_Mst_Ward_Get(objRQ_Mst_Ward, addressAPIs);
            if (objRT_Mst_Ward.Lst_Mst_Ward != null && objRT_Mst_Ward.Lst_Mst_Ward.Count > 0)
            {
                listMst_Ward.AddRange(objRT_Mst_Ward.Lst_Mst_Ward);
            }
            return listMst_Ward;
        }
        public async Task<List<Mst_Ward>> List_Mst_Ward_Async(string strWhereClause)
        {
            Task<List<Mst_Ward>> list_Mst_Ward_Task = List_Mst_Ward_Task(strWhereClause);
            List<Mst_Ward> list_Mst_Ward = await list_Mst_Ward_Task;
            return list_Mst_Ward;
        }
        public Task<List<Mst_Ward>> List_Mst_Ward_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Ward>> tcs = new TaskCompletionSource<List<Mst_Ward>>();
            var listMst_Ward = new List<Mst_Ward>();
            var objRQ_Mst_Ward = new RQ_Mst_Ward()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Ward
                Rt_Cols_Mst_Ward = "*",
                Mst_Ward = null,
            };
            listMst_Ward = WA_Mst_Ward_Get(objRQ_Mst_Ward);
            tcs.TrySetResult(listMst_Ward);
            return tcs.Task;
        }
        public async Task<List<Mst_Ward>> List_Mst_Ward_Async(RQ_Mst_Ward objRQ_Mst_Ward)
        {
            Task<List<Mst_Ward>> list_Mst_Ward_Task = List_Mst_Ward_Task(objRQ_Mst_Ward);
            List<Mst_Ward> list_Mst_Ward = await list_Mst_Ward_Task;
            return list_Mst_Ward;
        }
        public Task<List<Mst_Ward>> List_Mst_Ward_Task(RQ_Mst_Ward objRQ_Mst_Ward)
        {
            TaskCompletionSource<List<Mst_Ward>> tcs = new TaskCompletionSource<List<Mst_Ward>>();
            var listMst_Ward = new List<Mst_Ward>();
            listMst_Ward = WA_Mst_Ward_Get(objRQ_Mst_Ward);
            tcs.TrySetResult(listMst_Ward);
            return tcs.Task;
        }
        #endregion
        #region["Mst_Area - Khu vực"]
        protected List<Mst_Area> WA_Mst_Area_Get(RQ_Mst_Area objRQ_Mst_Area)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_Area = new List<Mst_Area>();
            var objRT_Mst_Area = Mst_AreaService.Instance.WA_Mst_Area_Get(objRQ_Mst_Area, addressAPIs);
            if (objRT_Mst_Area.Lst_Mst_Area != null && objRT_Mst_Area.Lst_Mst_Area.Count > 0)
            {
                listMst_Area.AddRange(objRT_Mst_Area.Lst_Mst_Area);
            }
            return listMst_Area;
        }
        public async Task<List<Mst_Area>> List_Mst_Area_Async(string strWhereClause)
        {
            Task<List<Mst_Area>> list_Mst_Area_Task = List_Mst_Area_Task(strWhereClause);
            List<Mst_Area> list_Mst_Area = await list_Mst_Area_Task;
            return list_Mst_Area;
        }
        public Task<List<Mst_Area>> List_Mst_Area_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Area>> tcs = new TaskCompletionSource<List<Mst_Area>>();
            var listMst_Area = new List<Mst_Area>();
            var objRQ_Mst_Area = new RQ_Mst_Area()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Area = "*",
                Mst_Area = null,
            };
            listMst_Area = WA_Mst_Area_Get(objRQ_Mst_Area);
            tcs.TrySetResult(listMst_Area);
            return tcs.Task;
        }
        public async Task<List<Mst_Area>> List_Mst_Area_Async(RQ_Mst_Area objRQ_Mst_Area)
        {
            Task<List<Mst_Area>> list_Mst_Area_Task = List_Mst_Area_Task(objRQ_Mst_Area);
            List<Mst_Area> list_Mst_Area = await list_Mst_Area_Task;
            return list_Mst_Area;
        }
        public async Task<List<Mst_AreaExt>> List_Mst_AreaExt_Async(RQ_Mst_Area objRQ_Mst_Area)
        {
            Task<List<Mst_Area>> list_Mst_Area_Task = List_Mst_Area_Task(objRQ_Mst_Area);
            List<Mst_Area> list_Mst_Area = await list_Mst_Area_Task;
            var list_Mst_AreaExt = new List<Mst_AreaExt>();
            var _list_Mst_AreaExt = new List<Mst_AreaExt>();
            if (list_Mst_Area != null && list_Mst_Area.Count > 0)
            {
                foreach (var item in list_Mst_Area)
                {
                    var objMst_AreaExt = new Mst_AreaExt()
                    {
                        OrgID = CUtils.StrValue(item.OrgID),
                        AreaCode = CUtils.StrValue(item.AreaCode),
                        NetworkID = CUtils.StrValue(item.NetworkID),
                        AreaCodeParent = CUtils.StrValue(item.AreaCodeParent),
                        AreaBUCode = CUtils.StrValue(item.AreaBUCode),
                        AreaBUPattern = CUtils.StrValue(item.AreaBUPattern),
                        AreaLevel = CUtils.StrValue(item.AreaLevel),
                        AreaName = CUtils.StrValue(item.AreaName),
                        AreaDesc = CUtils.StrValue(item.AreaDesc),
                        FlagActive = CUtils.StrValue(item.FlagActive),
                        LogLUDTimeUTC = CUtils.StrValue(item.LogLUDTimeUTC),
                        LogLUBy = CUtils.StrValue(item.LogLUBy),
                    };
                    _list_Mst_AreaExt.Add(objMst_AreaExt);
                }
                var toGroupBaseTree = Mst_AreaExtension.ToGroupBaseTree(Mst_AreaExtension.GetGroupBaseList(_list_Mst_AreaExt));
                var toFlatGroupBaseTree = Mst_AreaExtension.ToFlatGroupBaseTree(toGroupBaseTree);
                if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
                {
                    list_Mst_AreaExt.AddRange(toFlatGroupBaseTree);
                }
            }
            return list_Mst_AreaExt;
        }
        public Task<List<Mst_Area>> List_Mst_Area_Task(RQ_Mst_Area objRQ_Mst_Area)
        {
            TaskCompletionSource<List<Mst_Area>> tcs = new TaskCompletionSource<List<Mst_Area>>();
            var listMst_Area = new List<Mst_Area>();
            listMst_Area = WA_Mst_Area_Get(objRQ_Mst_Area);
            tcs.TrySetResult(listMst_Area);
            return tcs.Task;
        }
        #endregion
        #region["Customer_DynamicField - Thông tin bổ sung"]
        protected List<Customer_DynamicField> WA_Customer_DynamicField_Get(RQ_Customer_DynamicField objRQ_Customer_DynamicField)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listCustomer_DynamicField = new List<Customer_DynamicField>();
            var objRT_Customer_DynamicField = Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField, addressAPIs);
            if (objRT_Customer_DynamicField.Lst_Customer_DynamicField != null && objRT_Customer_DynamicField.Lst_Customer_DynamicField.Count > 0)
            {
                listCustomer_DynamicField.AddRange(objRT_Customer_DynamicField.Lst_Customer_DynamicField);
            }
            return listCustomer_DynamicField;
        }
        public async Task<List<Customer_DynamicField>> List_Customer_DynamicField_Async(string strWhereClause)
        {
            Task<List<Customer_DynamicField>> list_Customer_DynamicField_Task = List_Customer_DynamicField_Task(strWhereClause);
            List<Customer_DynamicField> list_Customer_DynamicField = await list_Customer_DynamicField_Task;
            return list_Customer_DynamicField;
        }
        public Task<List<Customer_DynamicField>> List_Customer_DynamicField_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Customer_DynamicField>> tcs = new TaskCompletionSource<List<Customer_DynamicField>>();
            var listCustomer_DynamicField = new List<Customer_DynamicField>();
            var objRQ_Customer_DynamicField = new RQ_Customer_DynamicField()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Customer_DynamicField
                Rt_Cols_Customer_DynamicField = "*",
                Customer_DynamicField = null,
            };
            listCustomer_DynamicField = WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField);
            tcs.TrySetResult(listCustomer_DynamicField);
            return tcs.Task;
        }
        public async Task<List<Customer_DynamicField>> List_Customer_DynamicField_Async(RQ_Customer_DynamicField objRQ_Customer_DynamicField)
        {
            Task<List<Customer_DynamicField>> list_Customer_DynamicField_Task = List_Customer_DynamicField_Task(objRQ_Customer_DynamicField);
            List<Customer_DynamicField> list_Customer_DynamicField = await list_Customer_DynamicField_Task;
            return list_Customer_DynamicField;
        }
        public Task<List<Customer_DynamicField>> List_Customer_DynamicField_Task(RQ_Customer_DynamicField objRQ_Customer_DynamicField)
        {
            TaskCompletionSource<List<Customer_DynamicField>> tcs = new TaskCompletionSource<List<Customer_DynamicField>>();
            var listCustomer_DynamicField = new List<Customer_DynamicField>();
            listCustomer_DynamicField = WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField);
            tcs.TrySetResult(listCustomer_DynamicField);
            return tcs.Task;
        }

        #endregion
        #region["Mst_Customer - Đối tác (Phân biệt theo cờ)"]
        public async Task<RT_Mst_Customer> RT_Mst_Customer_Async(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            Task<RT_Mst_Customer> objRT_Mst_Customer_Task = RT_Mst_Customer_Task(objRQ_Mst_Customer);
            var objRT_Mst_Customer = await objRT_Mst_Customer_Task;
            return objRT_Mst_Customer;
        }
        public Task<RT_Mst_Customer> RT_Mst_Customer_Task(RQ_Mst_Customer objRQ_Mst_Customer)
        {
            TaskCompletionSource<RT_Mst_Customer> tcs = new TaskCompletionSource<RT_Mst_Customer>();
            var objRT_Mst_Product = RT_Mst_Customer_Get(objRQ_Mst_Customer);
            tcs.TrySetResult(objRT_Mst_Product);
            return tcs.Task;
        }
        #endregion
        #region["Mst_CustomerSource - Nguồn khách hàng"]
        protected List<Mst_CustomerSource> WA_Mst_CustomerSource_Get(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource)
        {
            var abc = new Mst_Customer();
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_CustomerSource = new List<Mst_CustomerSource>();
            var objRT_Mst_CustomerSource = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, addressAPIs);
            if (objRT_Mst_CustomerSource.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSource.Lst_Mst_CustomerSource.Count > 0)
            {
                listMst_CustomerSource.AddRange(objRT_Mst_CustomerSource.Lst_Mst_CustomerSource);
            }
            return listMst_CustomerSource;
        }
        public async Task<List<Mst_CustomerSource>> List_Mst_CustomerSource_Async(string strWhereClause)
        {
            Task<List<Mst_CustomerSource>> list_Mst_CustomerSource_Task = List_Mst_CustomerSource_Task(strWhereClause);
            List<Mst_CustomerSource> list_Mst_CustomerSource = await list_Mst_CustomerSource_Task;
            return list_Mst_CustomerSource;
        }
        public Task<List<Mst_CustomerSource>> List_Mst_CustomerSource_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_CustomerSource>> tcs = new TaskCompletionSource<List<Mst_CustomerSource>>();
            var listMst_CustomerSource = new List<Mst_CustomerSource>();
            var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerSource
                Rt_Cols_Mst_CustomerSource = "*",
                Mst_CustomerSource = null,
            };
            listMst_CustomerSource = WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource);
            tcs.TrySetResult(listMst_CustomerSource);
            return tcs.Task;
        }
        public async Task<List<Mst_CustomerSource>> List_Mst_CustomerSource_Async(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource)
        {
            Task<List<Mst_CustomerSource>> list_Mst_CustomerSource_Task = List_Mst_CustomerSource_Task(objRQ_Mst_CustomerSource);
            List<Mst_CustomerSource> list_Mst_CustomerSource = await list_Mst_CustomerSource_Task;
            return list_Mst_CustomerSource;
        }

        public Task<List<Mst_CustomerSource>> List_Mst_CustomerSource_Task(RQ_Mst_CustomerSource objRQ_Mst_CustomerSource)
        {
            TaskCompletionSource<List<Mst_CustomerSource>> tcs = new TaskCompletionSource<List<Mst_CustomerSource>>();
            var listMst_CustomerSource = new List<Mst_CustomerSource>();
            listMst_CustomerSource = WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource);
            tcs.TrySetResult(listMst_CustomerSource);
            return tcs.Task;
        }
        #endregion
        #region["Mst_CustomerGroup - Nhóm khách hàng"]
        protected List<Mst_CustomerGroup> WA_Mst_CustomerGroup_Get(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_CustomerGroup = new List<Mst_CustomerGroup>();
            var objRT_Mst_CustomerGroup = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, addressAPIs);
            if (objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup.Count > 0)
            {
                listMst_CustomerGroup.AddRange(objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup);
            }
            return listMst_CustomerGroup;
        }
        public async Task<List<Mst_CustomerGroup>> List_Mst_CustomerGroup_Async(string strWhereClause)
        {
            Task<List<Mst_CustomerGroup>> list_Mst_CustomerGroup_Task = List_Mst_CustomerGroup_Task(strWhereClause);
            List<Mst_CustomerGroup> list_Mst_CustomerGroup = await list_Mst_CustomerGroup_Task;
            return list_Mst_CustomerGroup;
        }
        public Task<List<Mst_CustomerGroup>> List_Mst_CustomerGroup_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_CustomerGroup>> tcs = new TaskCompletionSource<List<Mst_CustomerGroup>>();
            var listMst_CustomerGroup = new List<Mst_CustomerGroup>();
            var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_CustomerGroup = "*",
                Mst_CustomerGroup = null,
            };
            listMst_CustomerGroup = WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup);
            tcs.TrySetResult(listMst_CustomerGroup);
            return tcs.Task;
        }
        public async Task<List<Mst_CustomerGroup>> List_Mst_CustomerGroup_Async(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup)
        {
            Task<List<Mst_CustomerGroup>> list_Mst_CustomerGroup_Task = List_Mst_CustomerGroup_Task(objRQ_Mst_CustomerGroup);
            List<Mst_CustomerGroup> list_Mst_CustomerGroup = await list_Mst_CustomerGroup_Task;
            return list_Mst_CustomerGroup;
        }

        public Task<List<Mst_CustomerGroup>> List_Mst_CustomerGroup_Task(RQ_Mst_CustomerGroup objRQ_Mst_CustomerGroup)
        {
            TaskCompletionSource<List<Mst_CustomerGroup>> tcs = new TaskCompletionSource<List<Mst_CustomerGroup>>();
            var listMst_CustomerGroup = new List<Mst_CustomerGroup>();
            listMst_CustomerGroup = WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup);
            tcs.TrySetResult(listMst_CustomerGroup);
            return tcs.Task;
        }
        #endregion
        #region["Mst_CustomerType - Loại khách hàng"]
        protected List<Common.Models.Mst_CustomerType> WA_Mst_CustomerType_Get(RQ_Mst_CustomerType objRQ_Mst_CustomerType)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_CustomerType = new List<Common.Models.Mst_CustomerType>();
            var objRT_Mst_CustomerType = Mst_CustomerTypeService.Instance.WA_Mst_CustomerType_Get(objRQ_Mst_CustomerType, addressAPIs);
            if (objRT_Mst_CustomerType.Lst_Mst_CustomerType != null && objRT_Mst_CustomerType.Lst_Mst_CustomerType.Count > 0)
            {
                listMst_CustomerType.AddRange(objRT_Mst_CustomerType.Lst_Mst_CustomerType);
            }
            return listMst_CustomerType;
        }
        public async Task<List<Common.Models.Mst_CustomerType>> List_Mst_CustomerType_Async(string strWhereClause)
        {
            Task<List<Common.Models.Mst_CustomerType>> list_Mst_CustomerType_Task = List_Mst_CustomerType_Task(strWhereClause);
            List<Common.Models.Mst_CustomerType> list_Mst_CustomerType = await list_Mst_CustomerType_Task;
            return list_Mst_CustomerType;
        }
        public Task<List<Common.Models.Mst_CustomerType>> List_Mst_CustomerType_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Common.Models.Mst_CustomerType>> tcs = new TaskCompletionSource<List<Common.Models.Mst_CustomerType>>();
            var listMst_CustomerType = new List<Common.Models.Mst_CustomerType>();
            var objRQ_Mst_CustomerType = new RQ_Mst_CustomerType()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerType
                Rt_Cols_Mst_CustomerType = "*",
                Mst_CustomerType = null,
            };
            listMst_CustomerType = WA_Mst_CustomerType_Get(objRQ_Mst_CustomerType);
            tcs.TrySetResult(listMst_CustomerType);
            return tcs.Task;
        }
        public async Task<List<Common.Models.Mst_CustomerType>> List_Mst_CustomerType_Async(RQ_Mst_CustomerType objRQ_Mst_CustomerType)
        {
            Task<List<Common.Models.Mst_CustomerType>> list_Mst_CustomerType_Task = List_Mst_CustomerType_Task(objRQ_Mst_CustomerType);
            List<Common.Models.Mst_CustomerType> list_Mst_CustomerType = await list_Mst_CustomerType_Task;
            return list_Mst_CustomerType;
        }
        public Task<List<Common.Models.Mst_CustomerType>> List_Mst_CustomerType_Task(RQ_Mst_CustomerType objRQ_Mst_CustomerType)
        {
            TaskCompletionSource<List<Common.Models.Mst_CustomerType>> tcs = new TaskCompletionSource<List<Common.Models.Mst_CustomerType>>();
            var listMst_CustomerType = new List<Common.Models.Mst_CustomerType>();
            listMst_CustomerType = WA_Mst_CustomerType_Get(objRQ_Mst_CustomerType);
            tcs.TrySetResult(listMst_CustomerType);
            return tcs.Task;
        }
        #endregion
        #region["Mst_GovIDType - Loại giấy tờ"]
        protected List<Mst_GovIDType> WA_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_GovIDType = new List<Mst_GovIDType>();
            var objRT_Mst_GovIDType = Mst_GovIDTypeService.Instance.WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType, addressAPIs);
            if (objRT_Mst_GovIDType.Lst_Mst_GovIDType != null && objRT_Mst_GovIDType.Lst_Mst_GovIDType.Count > 0)
            {
                listMst_GovIDType.AddRange(objRT_Mst_GovIDType.Lst_Mst_GovIDType);
            }
            return listMst_GovIDType;
        }
        public async Task<List<Mst_GovIDType>> List_Mst_GovIDType_Async(string strWhereClause)
        {
            Task<List<Mst_GovIDType>> list_Mst_GovIDType_Task = List_Mst_GovIDType_Task(strWhereClause);
            List<Mst_GovIDType> list_Mst_GovIDType = await list_Mst_GovIDType_Task;
            return list_Mst_GovIDType;
        }
        public Task<List<Mst_GovIDType>> List_Mst_GovIDType_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_GovIDType>> tcs = new TaskCompletionSource<List<Mst_GovIDType>>();
            var listMst_GovIDType = new List<Mst_GovIDType>();
            var objRQ_Mst_GovIDType = new RQ_Mst_GovIDType()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_GovIDType
                Rt_Cols_Mst_GovIDType = "*",
                Mst_GovIDType = null,
            };
            listMst_GovIDType = WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType);
            tcs.TrySetResult(listMst_GovIDType);
            return tcs.Task;
        }
        public async Task<List<Mst_GovIDType>> List_Mst_GovIDType_Async(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            Task<List<Mst_GovIDType>> list_Mst_GovIDType_Task = List_Mst_GovIDType_Task(objRQ_Mst_GovIDType);
            List<Mst_GovIDType> list_Mst_GovIDType = await list_Mst_GovIDType_Task;
            return list_Mst_GovIDType;
        }
        public Task<List<Mst_GovIDType>> List_Mst_GovIDType_Task(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            TaskCompletionSource<List<Mst_GovIDType>> tcs = new TaskCompletionSource<List<Mst_GovIDType>>();
            var listMst_GovIDType = new List<Mst_GovIDType>();
            listMst_GovIDType = WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType);
            tcs.TrySetResult(listMst_GovIDType);
            return tcs.Task;
        }
        #endregion

        #region["Mst_ProductGroup - Nhóm hàng"]
        protected List<Mst_ProductGroup> WA_Mst_ProductGroup_Get(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_ProductGroup = new List<Mst_ProductGroup>();
            var objRT_Mst_ProductGroup = Mst_ProductGroupService.Instance.WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup, addressAPIs);
            if (objRT_Mst_ProductGroup.Lst_Mst_ProductGroup != null && objRT_Mst_ProductGroup.Lst_Mst_ProductGroup.Count > 0)
            {
                listMst_ProductGroup.AddRange(objRT_Mst_ProductGroup.Lst_Mst_ProductGroup);
            }
            return listMst_ProductGroup;
        }
        public async Task<List<Mst_ProductGroup>> List_Mst_ProductGroup_Async(string strWhereClause)
        {
            Task<List<Mst_ProductGroup>> list_Mst_ProductGroup_Task = List_Mst_ProductGroup_Task(strWhereClause);
            List<Mst_ProductGroup> list_Mst_ProductGroup = await list_Mst_ProductGroup_Task;
            return list_Mst_ProductGroup;
        }
        public Task<List<Mst_ProductGroup>> List_Mst_ProductGroup_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_ProductGroup>> tcs = new TaskCompletionSource<List<Mst_ProductGroup>>();
            var listMst_ProductGroup = new List<Mst_ProductGroup>();
            var objRQ_Mst_ProductGroup = new RQ_Mst_ProductGroup()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_ProductGroup = "*",
                Mst_ProductGroup = null,
            };
            listMst_ProductGroup = WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup);
            tcs.TrySetResult(listMst_ProductGroup);
            return tcs.Task;
        }
        public async Task<List<Mst_ProductGroup>> List_Mst_ProductGroup_Async(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup)
        {
            Task<List<Mst_ProductGroup>> list_Mst_ProductGroup_Task = List_Mst_ProductGroup_Task(objRQ_Mst_ProductGroup);
            List<Mst_ProductGroup> list_Mst_ProductGroup = await list_Mst_ProductGroup_Task;
            return list_Mst_ProductGroup;
        }
        public async Task<List<Mst_ProductGroupExt>> List_Mst_ProductGroupExt_Async(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup)
        {
            Task<List<Mst_ProductGroup>> list_Mst_ProductGroup_Task = List_Mst_ProductGroup_Task(objRQ_Mst_ProductGroup);
            List<Mst_ProductGroup> list_Mst_ProductGroup = await list_Mst_ProductGroup_Task;
            var list_Mst_ProductGroupExt = new List<Mst_ProductGroupExt>();
            var _list_Mst_ProductGroupExt = new List<Mst_ProductGroupExt>();
            if (list_Mst_ProductGroup != null && list_Mst_ProductGroup.Count > 0)
            {
                foreach (var item in list_Mst_ProductGroup)
                {
                    var objMst_ProductGroupExt = new Mst_ProductGroupExt()
                    {
                        OrgID = CUtils.StrValue(item.OrgID),
                        ProductGrpCode = CUtils.StrValue(item.ProductGrpCode),
                        NetworkID = CUtils.StrValue(item.NetworkID),
                        ProductGrpCodeParent = CUtils.StrValue(item.ProductGrpCodeParent),
                        ProductGrpBUCode = CUtils.StrValue(item.ProductGrpBUCode),
                        ProductGrpBUPattern = CUtils.StrValue(item.ProductGrpBUPattern),
                        ProductGrpLevel = CUtils.StrValue(item.ProductGrpLevel),
                        ProductGrpName = CUtils.StrValue(item.ProductGrpName),
                        FlagActive = CUtils.StrValue(item.FlagActive),
                        LogLUDTimeUTC = CUtils.StrValue(item.LogLUDTimeUTC),
                        LogLUBy = CUtils.StrValue(item.LogLUBy),
                    };
                    _list_Mst_ProductGroupExt.Add(objMst_ProductGroupExt);
                }
                var toGroupBaseTree = Mst_ProductGroupExtension.ToGroupBaseTree(Mst_ProductGroupExtension.GetGroupBaseList(_list_Mst_ProductGroupExt));
                var toFlatGroupBaseTree = Mst_ProductGroupExtension.ToFlatGroupBaseTree(toGroupBaseTree);
                if (toFlatGroupBaseTree != null && toFlatGroupBaseTree.Count > 0)
                {
                    list_Mst_ProductGroupExt.AddRange(toFlatGroupBaseTree);
                }
            }
            return list_Mst_ProductGroupExt;
        }
        public Task<List<Mst_ProductGroup>> List_Mst_ProductGroup_Task(RQ_Mst_ProductGroup objRQ_Mst_ProductGroup)
        {
            TaskCompletionSource<List<Mst_ProductGroup>> tcs = new TaskCompletionSource<List<Mst_ProductGroup>>();
            var listMst_ProductGroup = new List<Mst_ProductGroup>();
            listMst_ProductGroup = WA_Mst_ProductGroup_Get(objRQ_Mst_ProductGroup);
            tcs.TrySetResult(listMst_ProductGroup);
            return tcs.Task;
        }
        #endregion

        #region["Mst_Attribute - Thuộc tính"]
        protected List<Mst_Attribute> WA_Mst_Attribute_Get(RQ_Mst_Attribute objRQ_Mst_Attribute)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_Attribute = new List<Mst_Attribute>();
            var objRT_Mst_Attribute = Mst_AttributeService.Instance.WA_Mst_Attribute_Get(objRQ_Mst_Attribute, addressAPIs);
            if (objRT_Mst_Attribute.Lst_Mst_Attribute != null && objRT_Mst_Attribute.Lst_Mst_Attribute.Count > 0)
            {
                listMst_Attribute.AddRange(objRT_Mst_Attribute.Lst_Mst_Attribute);
            }
            return listMst_Attribute;
        }
        public async Task<List<Mst_Attribute>> List_Mst_Attribute_Async(string strWhereClause)
        {
            Task<List<Mst_Attribute>> list_Mst_Attribute_Task = List_Mst_Attribute_Task(strWhereClause);
            List<Mst_Attribute> list_Mst_Attribute = await list_Mst_Attribute_Task;
            return list_Mst_Attribute;
        }
        public Task<List<Mst_Attribute>> List_Mst_Attribute_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Attribute>> tcs = new TaskCompletionSource<List<Mst_Attribute>>();
            var listMst_Attribute = new List<Mst_Attribute>();
            var objRQ_Mst_Attribute = new RQ_Mst_Attribute()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_Attribute = "*",
                Mst_Attribute = null,
            };
            listMst_Attribute = WA_Mst_Attribute_Get(objRQ_Mst_Attribute);
            tcs.TrySetResult(listMst_Attribute);
            return tcs.Task;
        }
        public async Task<List<Mst_Attribute>> List_Mst_Attribute_Async(RQ_Mst_Attribute objRQ_Mst_Attribute)
        {
            Task<List<Mst_Attribute>> list_Mst_Attribute_Task = List_Mst_Attribute_Task(objRQ_Mst_Attribute);
            List<Mst_Attribute> list_Mst_Attribute = await list_Mst_Attribute_Task;
            return list_Mst_Attribute;
        }
        public Task<List<Mst_Attribute>> List_Mst_Attribute_Task(RQ_Mst_Attribute objRQ_Mst_Attribute)
        {
            TaskCompletionSource<List<Mst_Attribute>> tcs = new TaskCompletionSource<List<Mst_Attribute>>();
            var listMst_Attribute = new List<Mst_Attribute>();
            listMst_Attribute = WA_Mst_Attribute_Get(objRQ_Mst_Attribute);
            tcs.TrySetResult(listMst_Attribute);
            return tcs.Task;
        }
        #endregion
        #region["Loại hàng"]
        protected List<Mst_ProductType> WA_Mst_ProductType_Get(RQ_Mst_ProductType objRQ_Mst_ProductType)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_ProductType = new List<Mst_ProductType>();

            var objRT_Mst_ProductType = Mst_ProductTypeService.Instance.WA_Mst_ProductType_Get(objRQ_Mst_ProductType, addressAPIs);
            if (objRT_Mst_ProductType.Lst_Mst_ProductType != null && objRT_Mst_ProductType.Lst_Mst_ProductType.Count > 0)
            {
                listMst_ProductType.AddRange(objRT_Mst_ProductType.Lst_Mst_ProductType);
            }
            return listMst_ProductType;
        }
        public async Task<List<Mst_ProductType>> List_Mst_ProductType_Async(string strWhereClause)
        {
            Task<List<Mst_ProductType>> list_Mst_ProductType_Task = List_Mst_ProductType_Task(strWhereClause);
            List<Mst_ProductType> list_Mst_ProductType = await list_Mst_ProductType_Task;
            return list_Mst_ProductType;
        }
        public Task<List<Mst_ProductType>> List_Mst_ProductType_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_ProductType>> tcs = new TaskCompletionSource<List<Mst_ProductType>>();
            var listMst_ProductType = new List<Mst_ProductType>();
            var objRQ_Mst_ProductType = new RQ_Mst_ProductType()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductType
                Rt_Cols_Mst_ProductType = "*",
                Mst_ProductType = null,
            };
            listMst_ProductType = WA_Mst_ProductType_Get(objRQ_Mst_ProductType);
            tcs.TrySetResult(listMst_ProductType);
            return tcs.Task;
        }
        public async Task<List<Mst_ProductType>> List_Mst_ProductType_Async(RQ_Mst_ProductType objRQ_Mst_ProductType)
        {
            Task<List<Mst_ProductType>> list_Mst_ProductType_Task = List_Mst_ProductType_Task(objRQ_Mst_ProductType);
            List<Mst_ProductType> list_Mst_ProductType = await list_Mst_ProductType_Task;
            return list_Mst_ProductType;
        }
        public Task<List<Mst_ProductType>> List_Mst_ProductType_Task(RQ_Mst_ProductType objRQ_Mst_ProductType)
        {
            TaskCompletionSource<List<Mst_ProductType>> tcs = new TaskCompletionSource<List<Mst_ProductType>>();
            var listMst_ProductType = new List<Mst_ProductType>();
            listMst_ProductType = WA_Mst_ProductType_Get(objRQ_Mst_ProductType);
            tcs.TrySetResult(listMst_ProductType);
            return tcs.Task;
        }
        #endregion
        #region["Mst_Brand - Nhãn hiệu"]
        protected List<Mst_Brand> WA_Mst_Brand_Get(RQ_Mst_Brand objRQ_Mst_Brand)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listMst_Brand = new List<Mst_Brand>();
            var objRT_Mst_Brand = Mst_BrandService.Instance.WA_Mst_Brand_Get(objRQ_Mst_Brand, addressAPIs);
            if (objRT_Mst_Brand.Lst_Mst_Brand != null && objRT_Mst_Brand.Lst_Mst_Brand.Count > 0)
            {
                listMst_Brand.AddRange(objRT_Mst_Brand.Lst_Mst_Brand);
            }
            return listMst_Brand;
        }
        public async Task<List<Mst_Brand>> List_Mst_Brand_Async(string strWhereClause)
        {
            Task<List<Mst_Brand>> list_Mst_Brand_Task = List_Mst_Brand_Task(strWhereClause);
            List<Mst_Brand> list_Mst_Brand = await list_Mst_Brand_Task;
            return list_Mst_Brand;
        }
        public Task<List<Mst_Brand>> List_Mst_Brand_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_Brand>> tcs = new TaskCompletionSource<List<Mst_Brand>>();
            var listMst_Brand = new List<Mst_Brand>();
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Brand
                Rt_Cols_Mst_Brand = "*",
                Mst_Brand = null,
            };
            listMst_Brand = WA_Mst_Brand_Get(objRQ_Mst_Brand);
            tcs.TrySetResult(listMst_Brand);
            return tcs.Task;
        }
        public async Task<List<Mst_Brand>> List_Mst_Brand_Async(RQ_Mst_Brand objRQ_Mst_Brand)
        {
            Task<List<Mst_Brand>> list_Mst_Brand_Task = List_Mst_Brand_Task(objRQ_Mst_Brand);
            List<Mst_Brand> list_Mst_Brand = await list_Mst_Brand_Task;
            return list_Mst_Brand;
        }
        public Task<List<Mst_Brand>> List_Mst_Brand_Task(RQ_Mst_Brand objRQ_Mst_Brand)
        {
            TaskCompletionSource<List<Mst_Brand>> tcs = new TaskCompletionSource<List<Mst_Brand>>();
            var listMst_Brand = new List<Mst_Brand>();
            listMst_Brand = WA_Mst_Brand_Get(objRQ_Mst_Brand);
            tcs.TrySetResult(listMst_Brand);
            return tcs.Task;
        }
        #endregion
        #region["Prd_DynamicField - Thông tin bổ sung"]
        protected List<Prd_DynamicField> WA_Prd_DynamicField_Get(RQ_Prd_DynamicField objRQ_Prd_DynamicField)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listPrd_DynamicField = new List<Prd_DynamicField>();
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Prd_DynamicField);
            var objRT_Prd_DynamicField = Prd_DynamicFieldService.Instance.WA_Prd_DynamicField_Get(objRQ_Prd_DynamicField, addressAPIs);
            if (objRT_Prd_DynamicField.Lst_Prd_DynamicField != null && objRT_Prd_DynamicField.Lst_Prd_DynamicField.Count > 0)
            {
                listPrd_DynamicField.AddRange(objRT_Prd_DynamicField.Lst_Prd_DynamicField);
            }
            return listPrd_DynamicField;
        }
        public async Task<List<Prd_DynamicField>> List_Prd_DynamicField_Async(string strWhereClause)
        {
            Task<List<Prd_DynamicField>> list_Prd_DynamicField_Task = List_Prd_DynamicField_Task(strWhereClause);
            List<Prd_DynamicField> list_Prd_DynamicField = await list_Prd_DynamicField_Task;
            return list_Prd_DynamicField;
        }
        public Task<List<Prd_DynamicField>> List_Prd_DynamicField_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Prd_DynamicField>> tcs = new TaskCompletionSource<List<Prd_DynamicField>>();
            var listPrd_DynamicField = new List<Prd_DynamicField>();
            var objRQ_Prd_DynamicField = new RQ_Prd_DynamicField()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Prd_DynamicField
                Rt_Cols_Prd_DynamicField = "*",
                Prd_DynamicField = null,
            };
            listPrd_DynamicField = WA_Prd_DynamicField_Get(objRQ_Prd_DynamicField);
            tcs.TrySetResult(listPrd_DynamicField);
            return tcs.Task;
        }
        public async Task<List<Prd_DynamicField>> List_Prd_DynamicField_Async(RQ_Prd_DynamicField objRQ_Prd_DynamicField)
        {
            Task<List<Prd_DynamicField>> list_Prd_DynamicField_Task = List_Prd_DynamicField_Task(objRQ_Prd_DynamicField);
            List<Prd_DynamicField> list_Prd_DynamicField = await list_Prd_DynamicField_Task;
            return list_Prd_DynamicField;
        }
        public Task<List<Prd_DynamicField>> List_Prd_DynamicField_Task(RQ_Prd_DynamicField objRQ_Prd_DynamicField)
        {
            TaskCompletionSource<List<Prd_DynamicField>> tcs = new TaskCompletionSource<List<Prd_DynamicField>>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listPrd_DynamicField = new List<Prd_DynamicField>();
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Prd_DynamicField);
            listPrd_DynamicField = WA_Prd_DynamicField_Get(objRQ_Prd_DynamicField);
            tcs.TrySetResult(listPrd_DynamicField);
            return tcs.Task;
        }
        #endregion
        #region ["Mst_VATRate"]
        protected List<Mst_VATRate> WA_Mst_VATRate_Get(RQ_Mst_VATRate objRQ_Mst_VATRate)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_VATRate = new List<Mst_VATRate>();
            var objRT_Mst_VATRate = Mst_VATRateService.Instance.WA_Mst_VATRate_Get(objRQ_Mst_VATRate, addressAPIs);
            if (objRT_Mst_VATRate.Lst_Mst_VATRate != null && objRT_Mst_VATRate.Lst_Mst_VATRate.Count > 0)
            {
                listMst_VATRate.AddRange(objRT_Mst_VATRate.Lst_Mst_VATRate);
            }
            return listMst_VATRate;
        }
        public async Task<List<Mst_VATRate>> List_Mst_VATRate_Async(string strWhereClause)
        {
            Task<List<Mst_VATRate>> list_Mst_VATRate_Task = List_Mst_VATRate_Task(strWhereClause);
            List<Mst_VATRate> list_Mst_VATRate = await list_Mst_VATRate_Task;
            return list_Mst_VATRate;
        }
        public Task<List<Mst_VATRate>> List_Mst_VATRate_Task(string strWhereClause)
        {
            TaskCompletionSource<List<Mst_VATRate>> tcs = new TaskCompletionSource<List<Mst_VATRate>>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_VATRate = new List<Mst_VATRate>();
            var objRQ_Mst_VATRate = new RQ_Mst_VATRate()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                // RQ_Mst_Attribute
                Rt_Cols_Mst_VATRate = "*",
                Mst_VATRate = null,
            };
            listMst_VATRate = WA_Mst_VATRate_Get(objRQ_Mst_VATRate);
            tcs.TrySetResult(listMst_VATRate);
            return tcs.Task;
        }
        public async Task<List<Mst_VATRate>> List_Mst_VATRate_Async(RQ_Mst_VATRate objRQ_Mst_VATRate)
        {
            Task<List<Mst_VATRate>> list_Mst_VATRate_Task = List_Mst_VATRate_Task(objRQ_Mst_VATRate);
            List<Mst_VATRate> list_Mst_VATRate = await list_Mst_VATRate_Task;
            return list_Mst_VATRate;
        }
        public Task<List<Mst_VATRate>> List_Mst_VATRate_Task(RQ_Mst_VATRate objRQ_Mst_VATRate)
        {
            TaskCompletionSource<List<Mst_VATRate>> tcs = new TaskCompletionSource<List<Mst_VATRate>>();
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var listMst_VATRate = new List<Mst_VATRate>();
            listMst_VATRate = WA_Mst_VATRate_Get(objRQ_Mst_VATRate);
            tcs.TrySetResult(listMst_VATRate);
            return tcs.Task;
        }
        #endregion
        #region["Prd_BOM"]
        public List<Prd_BOM> WA_Prd_BOM_Get(RQ_Mst_Product objRQ_Mst_Product)
        {
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var listPrd_BOM = new List<Prd_BOM>();
            var objRT_Prd_BOM = Mst_ProductService.Instance.WA_Mst_Product_Get(objRQ_Mst_Product, addressAPIs);
            if (objRT_Prd_BOM.Lst_Prd_BOM != null && objRT_Prd_BOM.Lst_Prd_BOM.Count > 0)
            {
                listPrd_BOM.AddRange(objRT_Prd_BOM.Lst_Prd_BOM);
            }
            return listPrd_BOM;
        }
        #endregion

        #region ["In phiếu"]
        
        public List<InvF_TempPrint> ListInvF_TempPrint(string tempprinttype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);

            var listInvF_TempPrint = new List<InvF_TempPrint>();
            if (!CUtils.IsNullOrEmpty(tempprinttype))
            {
                var strWhereClauseInvF_TempPrint = "InvF_TempPrint.TempPrintType = '" + tempprinttype + "'";

                var objRQ_InvF_TempPrint = new RQ_InvF_TempPrint()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseInvF_TempPrint,
                    Ft_Cols_Upd = null,

                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    // RQ_InvF_TempPrint
                    Rt_Cols_InvF_TempPrint = "*",
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_TempPrint);
                var objRT_InvF_TempPrintCur = InvFTempPrintService.Instance.WA_InvF_TempPrint_Get(objRQ_InvF_TempPrint, addressAPI);
                if (objRT_InvF_TempPrintCur.Lst_InvF_TempPrint != null && objRT_InvF_TempPrintCur.Lst_InvF_TempPrint.Count > 0)
                {
                    listInvF_TempPrint.AddRange(objRT_InvF_TempPrintCur.Lst_InvF_TempPrint);
                }
            }
            return listInvF_TempPrint;
        }
        public string PostReport(dynamic data)
        {
            string url = ReportBro_ServerUrl + "rp/preview";
            string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Object myObject = new
            {
                json = serializedObject
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(myObject);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "Post";
            request.AllowWriteStreamBuffering = false;
            request.ContentType = "application/json";
            request.Accept = "Accept=application/json";
            request.SendChunked = true;
            request.ContentLength = serializedObject.Length;
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }
            var response = request.GetResponse() as HttpWebResponse;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Now you have your response.
                //or false depending on information in the response
                return responseText;
            }
            return null;
        }

        public string LinkFilePdf(string serverurl, string key, string outputFormat)
        {
            var linkPdf = String.Format("{0}{1}/{2}?key={3}&outputFormat={4}", serverurl, "rp", "preview", key, outputFormat);
            return linkPdf;
        }

        #endregion
        #region["Get DateTime Server Client"]
        public DateTime GetDateTimeServerClient()
        {
            var dateTime = DateTime.Now;
            return dateTime;
        }
        public string DateTimeNow()
        {
            var datetime = GetDateTimeServerClient().ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
            return datetime;
        }
        public string Today
        {
            get
            {
                var date = GetDateTimeServerClient().ToString(Nonsense.DATE_TIME_FORMAT);
                return date;
            }
        }
        public string Year
        {
            get
            {
                var year = GetDateTimeServerClient().Year.ToString();
                return year;
            }
        }
        public string MonthOfYear
        {
            get
            {
                var monthOfYear = GetDateTimeServerClient().ToString(Nonsense.MONTH_YEAR_FORMAT);
                return monthOfYear;
            }
        }
        public string DateToSearch()
        {
            var date = "";
            date = CUtils.GetDateToSearch(DateTimeNow());
            return date;
        }
        #endregion

        #region["Format number"]
        public string ReturnColumnFormatValue(string tableName, string columnName)
        {
            var columnFormat = "";
            tableName = CUtils.StrValueNew(tableName);
            columnName = CUtils.StrValueNew(columnName);
            var listMst_ColumnConfig = new List<Mst_ColumnConfig>();
            Session_List_Mst_ColumnConfig_Get();
            if (System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] != null)
            {
                var _listMst_ColumnConfig = System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] as List<Mst_ColumnConfig>;
                if (_listMst_ColumnConfig != null && _listMst_ColumnConfig.Count > 0)
                {
                    listMst_ColumnConfig.AddRange(_listMst_ColumnConfig);
                }
            }
            var objMst_ColumnConfig = listMst_ColumnConfig.FirstOrDefault(_it => !CUtils.IsNullOrEmpty(_it.ColumnName) && !CUtils.IsNullOrEmpty(_it.TableName) && _it.TableName.ToString().ToUpper().Trim().Equals(tableName.ToUpper()) && _it.ColumnName.ToString().ToUpper().Trim().Equals(columnName.ToUpper().Trim()));
            if (objMst_ColumnConfig != null && !CUtils.IsNullOrEmpty(objMst_ColumnConfig.ColumnFormat))
            {
                columnFormat = CUtils.StrValueNew(objMst_ColumnConfig.ColumnFormat);
            }

            return columnFormat;
        }
        public string NumericFormat(string tableName, string columnName, string numericFormatDefault)
        {
            var numericFormat = numericFormatDefault;
            var columnFormat = ReturnColumnFormatValue(tableName, columnName);
            if (!CUtils.IsNullOrEmpty(columnFormat))
            {
                numericFormat = CUtils.ReturnNumericFormat(columnFormat);
            }

            if (CUtils.IsNullOrEmpty(numericFormat))
            {
                //numericFormat = "{0:0,0.00}";
                numericFormat = "{0:0,0}";
            }
            return numericFormat;
        }
        #endregion

        #region["Mst_ColumnConfig"]

        //public string strFormatNumber()
        //{

        //}

        public void Session_List_Mst_ColumnConfig_Get()
        {
            //System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] = null;
            var listMst_ColumnConfig = new List<Mst_ColumnConfig>();
            if(System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] == null)
            {
                var strWhereClauseMst_ColumnConfig = strWhereClause_Mst_ColumnConfig_Base(new Mst_ColumnConfig()
                {
                    FlagActive = Client_Flag.Active,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                });

                listMst_ColumnConfig = List_Mst_ColumnConfig(strWhereClauseMst_ColumnConfig);

                if(listMst_ColumnConfig != null && listMst_ColumnConfig.Count > 0)
                {
                    System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] = listMst_ColumnConfig;
                }

            }
            else
            {
                listMst_ColumnConfig = System.Web.HttpContext.Current.Session["List_Mst_ColumnConfig"] as List<Mst_ColumnConfig>;
                if(listMst_ColumnConfig == null || listMst_ColumnConfig.Count == 0)
                {
                    listMst_ColumnConfig = new List<Mst_ColumnConfig>();
                }
            }
            ViewBag.ListMst_ColumnConfig = listMst_ColumnConfig;
        }


        protected List<Mst_ColumnConfig> WA_Mst_ColumnConfig_Get(RQ_Mst_ColumnConfig objRQ_Mst_ColumnConfig)
        {
            var listMst_ColumnConfig = new List<Mst_ColumnConfig>();
            var objRT_Mst_ColumnConfig = Mst_ColumnConfigService.Instance.WA_Mst_ColumnConfig_Get(objRQ_Mst_ColumnConfig, CUtils.StrValue(UserState.AddressAPIs));
            if (objRT_Mst_ColumnConfig.Lst_Mst_ColumnConfig != null && objRT_Mst_ColumnConfig.Lst_Mst_ColumnConfig.Count > 0)
            {
                listMst_ColumnConfig.AddRange(objRT_Mst_ColumnConfig.Lst_Mst_ColumnConfig);
            }
            return listMst_ColumnConfig;
        }
        protected List<Mst_ColumnConfig> List_Mst_ColumnConfig(string strWhereClause)
        {
            var listMst_ColumnConfig = new List<Mst_ColumnConfig>();
            var objRQ_Mst_ColumnConfig = new RQ_Mst_ColumnConfig()
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
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClause,
                // RQ_Mst_ColumnConfig
                Rt_Cols_Mst_ColumnConfig = "*",
                Mst_ColumnConfig = null,
            };
            var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_ColumnConfig);
            listMst_ColumnConfig = WA_Mst_ColumnConfig_Get(objRQ_Mst_ColumnConfig);
            return listMst_ColumnConfig;
        }


        private string strWhereClause_Mst_ColumnConfig_Base(Mst_ColumnConfig obj)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_ColumnConfig = TableName.Mst_ColumnConfig + ".";
            if (!CUtils.IsNullOrEmpty(obj.TableName))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.TableName, obj.TableName, "=");
            }
            if (!CUtils.IsNullOrEmpty(obj.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.NetworkID, obj.NetworkID, "=");
            }
            if (!CUtils.IsNullOrEmpty(obj.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.OrgId, obj.OrgID, "=");
            }
            if (!CUtils.IsNullOrEmpty(obj.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_ColumnConfig + TblMst_ColumnConfig.FlagActive, obj.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }


        #endregion

        public string WA_Seq_GenCommonCode_Get(string sequencetype)
        {
            var objRQ_Seq_Common = new RQ_Seq_Common()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                AccessToken = CUtils.StrValue(UserState.AccessToken),
                WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = ""
            };
            if (!CUtils.IsNullOrEmpty(sequencetype))
            {
                objRQ_Seq_Common.Seq_Common = new Seq_Common()
                {
                    SequenceType = sequencetype,
                    Param_Postfix = "",
                    Param_Prefix = ""
                };
            }
            var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
            var listSeq = SeqService.Instance.WA_Seq_Common_Get(objRQ_Seq_Common, addressAPIs);
            var seqCode = listSeq.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            return seqCode;
        }
        public List<string> WA_Seq_GenObjCode_Get(RQ_Seq_ObjCode objRQ_Seq_ObjCode)
        {
            var listSeq = new List<string>();
            var addressAPIs = UserState.AddressAPIs_Product_Customer_Centrer;
            var objRT_Seq_ObjCode = SeqService.Instance.WA_Seq_GenObjCode_Get(objRQ_Seq_ObjCode, addressAPIs);
            if (objRT_Seq_ObjCode.Lst_Seq_ObjCode != null && objRT_Seq_ObjCode.Lst_Seq_ObjCode.Count > 0)
            {
                listSeq.AddRange(objRT_Seq_ObjCode.Lst_Seq_ObjCode);
            }
            return listSeq;
        }

        public void GetUrlProductSolution()
        {
            #region Get link Solution
            var gwUserCode = OS_ProductCentrer_MasterServer_GwUserCode;
            var gwPassword = OS_ProductCentrer_MasterServer_GwPassword;
            if (ServiceAddress.BaseAPIProductCenter == null || ServiceAddress.BaseAPIProductCenter == "")
            {
                string strMstSvUrl_ProductCenter = ServiceAddress.BaseMasterServerProduct_Customer_CenterAPIAddress;
                MstSv_Sys_User objMstSv_Sys_User = new MstSv_Sys_User();

                /////
                RQ_MstSv_Sys_User objRQ_MstSv_Sys_User = new RQ_MstSv_Sys_User()
                {
                    Tid = GetNextTId(),
                    TokenID = strMstSvUrl_ProductCenter,
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    GwUserCode = gwUserCode,
                    GwPassword = gwPassword,
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword)
                };
                var strNetWorkUrl = ReturnAPIMasterServerInventory(objRQ_MstSv_Sys_User, strMstSvUrl_ProductCenter);
                if (strNetWorkUrl != null && strNetWorkUrl != "")
                {
                    ServiceAddress.BaseAPIProductCenter = strNetWorkUrl;
                }
            }
            #endregion
        }
    }
}