using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Extensions;
using idn.Skycic.Inventory.WebAdmin.Filters;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.State;
using inos.common.Model;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    [Authorize]
    [CustomErrorHandler]
    public class BaseController : Controller
    {
        public string GwUserCode = "";
        public string GwPassword = "";
        public string FuncType = "";
        public string Ft_RecordStart = "0";
        public string Ft_RecordCount = "123456000";
        public string Ft_WhereClause = "";
        public int PageSizeConfig = 10;
        public int RowsWorksheets = 1048570; // If you are working on Excel 2007 or any of the latest versions - there are 1048576 rows and 16384 columns. 
        public string Uploads = "Uploads";
        public string TempFiles = "TempFiles";
        public string Hethong = "";
        public string FolderUploadTest = "";
        public static string SubPath = "";
        public string TenantIdName = "idocNet";

        public string AccessToken = "";
        public string TokenID = "";
        public string WAUserCodeMSTSV = "";
        public string WAUserPasswordMSTSV = "";

        public double FileUploadSize = 26214400; // 25MB
        public double FileImageSize = 26214400; // 25MB
        public string FlagActive = "1";
        public string FlagInActive = "0";

        public string SolutionCode = "";
        public string SolutionName = "";

        public string APIsSendMail = "";
        public string ApiKeySendMail = "";
        public string DisplayNameMailFrom = "";
        public string MailFrom = "";

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

        #region["Get DateTime Server"]
        public string DateTimeNow()
        {
            var datetime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return datetime;
        }

        public string DateTimeServerBizUTC()
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
            var addressAPIs = UserState.AddressAPIs;
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
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = UserState.UtcOffset,
                // RQ_Common
            };
            var objRT_Common = CommonService.Instance.WA_Common_GetDTime(objRQ_Common, addressAPIs);

            if (objRT_Common != null && objRT_Common.c_K_DT_Sys != null && objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo != null && objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo.Count > 0)
            {
                var objc_K_DT_SysInfo = objRT_Common.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0];
                if (!CUtils.IsNullOrEmpty(objc_K_DT_SysInfo.Remark))
                {
                    datetimeUTC = objc_K_DT_SysInfo.Remark.Trim();
                }

                var convertingLocalTimeToUTC = CUtils.ConvertingLocalTimeToUTC(datetimeUTC);
                var convertingUTCToLocalTime = CUtils.ConvertingUTCToLocalTime(datetimeUTC);
            }

            return datetimeUTC;
        }

        public string Now
        {
            get { return DateTimeNow(); }
        }
        public string Today
        {
            get
            {
                if (CUtils.IsDateTime(DateTimeNow()))
                {
                    DateTime today = Convert.ToDateTime(DateTimeNow());
                    return today.ToString(Nonsense.DATE_TIME_FORMAT);
                }
                else
                {
                    return null;
                }
            }
        }
        public string Year
        {
            get
            {
                if (CUtils.IsDateTime(DateTimeNow()))
                {
                    DateTime today = Convert.ToDateTime(DateTimeNow());
                    return today.Year.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        public string MonthOfYear
        {
            get
            {
                if (CUtils.IsDateTime(DateTimeNow()))
                {
                    DateTime today = Convert.ToDateTime(DateTimeNow());
                    return today.ToString(Nonsense.MONTH_YEAR_FORMAT);
                }
                else
                {
                    return null;
                }
            }
        }
        public string DateToSearch()
        {
            var date = "";
            date = CUtils.GetDateToSearch(DateTimeNow());
            return date;
        }
        #endregion

        private int inext = 0;
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

        public BaseController()
        {
            if (CUtils.IsNullOrEmpty(GwUserCode))
            {
                GwUserCode = ConfigurationManager.AppSettings["GwUserCode"];
            }
            if (CUtils.IsNullOrEmpty(GwPassword))
            {
                GwPassword = ConfigurationManager.AppSettings["GwPassword"];
            }
            //if (CUtils.IsNullOrEmpty(AccessToken))
            //{
            //    AccessToken = ConfigurationManager.AppSettings["AccessToken"];
            //}
            if (CUtils.IsNullOrEmpty(TokenID))
            {
                TokenID = ConfigurationManager.AppSettings["TokenID"];
            }
            //if (CUtils.IsNullOrEmpty(WAUserCodeMSTSV))
            //{
            //    WAUserCodeMSTSV = ConfigurationManager.AppSettings["WAUserCodeMSTSV"];
            //}
            //if (CUtils.IsNullOrEmpty(WAUserPasswordMSTSV))
            //{
            //    WAUserPasswordMSTSV = ConfigurationManager.AppSettings["WAUserPasswordMSTSV"];
            //}

            #region["SendMail Config"]
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
            if (CUtils.IsNullOrEmpty(SolutionCode))
            {
                SolutionCode = ConfigurationManager.AppSettings["SolutionCode"];
            }
            #endregion

        }

        #region ["Init"]
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            int sessionTimeout = System.Web.HttpContext.Current.Session.Timeout;

            var networdid = (string)RouteData.Values["networdid"];
            var routeData = RouteData.Values;
        }
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            this.Init();
        }

        protected virtual void Init()
        {
            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseReportServerAPIAddress))
            {
                ServiceAddress.BaseReportServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseReportServerAPIAddress"];
            }

            if (CUtils.IsNullOrEmpty(ServiceAddress.BaseTCTServerAPIAddress))
            {
                ServiceAddress.BaseTCTServerAPIAddress = System.Configuration.ConfigurationManager.AppSettings["BaseTCTServerAPIAddress"];
            }

            if (UserState == null || UserState.RptSv_Sys_User == null)
            {
                FormsAuthentication.SignOut();
                var action = this.RouteData.Values["action"].ToString();
                if (!action.Equals("Login", StringComparison.InvariantCultureIgnoreCase))
                {
                    var requestRawUrl = Request.RawUrl;
                    var redirectUrl = Url.Action("Login", "Account", new { redirectUrl = requestRawUrl });
                    Response.Redirect(redirectUrl);
                }
            }
            else
            {
                var userName = "";
                var organName = "";
                if (!CUtils.IsNullOrEmpty(UserState.UserName))
                {
                    userName = UserState.UserName.Trim();
                }
                if (UserState.RptSv_Sys_User != null && !CUtils.IsNullOrEmpty(UserState.RptSv_Sys_User.DLCode))
                {
                    organName = UserState.RptSv_Sys_User.DLCode.Trim();
                }
                ViewBag.UserName = userName;
                ViewBag.OrganName = organName;
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
            if (string.IsNullOrEmpty(viewname))
                viewname = (string)ControllerContext.RouteData.Values["action"];

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
            if (string.IsNullOrEmpty(viewname))
                viewname = (string)ControllerContext.RouteData.Values["action"];

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
            if (string.IsNullOrEmpty(viewname))
                viewname = (string)ControllerContext.RouteData.Values["action"];
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

        #region["Upload File"]
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

        #region[""]
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
        #region["Quận/Huyện"]
        protected List<Mst_District> WA_Mst_District_Get(RQ_Mst_District objRQ_Mst_District)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_District = new List<Mst_District>();

            var objRT_Mst_District = Mst_DistrictService.Instance.WA_RptSv_Mst_District_Get(objRQ_Mst_District, addressAPIs);
            if (objRT_Mst_District.Lst_Mst_District != null && objRT_Mst_District.Lst_Mst_District.Count > 0)
            {
                listMst_District.AddRange(objRT_Mst_District.Lst_Mst_District);
            }
            return listMst_District;
        }
        protected List<Mst_District> List_Mst_District(string strWhereClause)
        {
            var userState = this.UserState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var listMst_District = new List<Mst_District>();

            var objRQ_Mst_District = new RQ_Mst_District()
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
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_District
                Rt_Cols_Mst_District = "*",
                Mst_District = null,
            };
            listMst_District = WA_Mst_District_Get(objRQ_Mst_District);
            return listMst_District;
        }

        #endregion
        #region["Tỉnh/Thành phố"]
        protected List<Mst_Province> WA_Mst_Province_Get(RQ_Mst_Province objRQ_Mst_Province)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Province = new List<Mst_Province>();

            var objRT_Mst_Province = Mst_ProvinceService.Instance.WA_RptSv_Mst_Province_Get(objRQ_Mst_Province, addressAPIs);
            if (objRT_Mst_Province.Lst_Mst_Province != null && objRT_Mst_Province.Lst_Mst_Province.Count > 0)
            {
                listMst_Province.AddRange(objRT_Mst_Province.Lst_Mst_Province);
            }
            return listMst_Province;
        }
        protected List<Mst_Province> List_Mst_Province(string strWhereClause)
        {
            var userState = this.UserState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var listMst_Province = new List<Mst_Province>();

            var objRQ_Mst_Province = new RQ_Mst_Province()
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
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            listMst_Province = WA_Mst_Province_Get(objRQ_Mst_Province);
            return listMst_Province;
        }

        #endregion
        #region["Quản lý đại lý"]
        //protected List<Mst_Dealer> WA_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
        //{
        //    var listMst_Dealer = new List<Mst_Dealer>();

        //    var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_Mst_Dealer_Get(objRQ_Mst_Dealer);
        //    if (objRT_Mst_Dealer.Lst_Mst_Dealer != null && objRT_Mst_Dealer.Lst_Mst_Dealer.Count > 0)
        //    {
        //        listMst_Dealer.AddRange(objRT_Mst_Dealer.Lst_Mst_Dealer);
        //    }
        //    return listMst_Dealer;
        //}

        //protected List<Mst_Dealer> List_Mst_Dealer(string strWhereClause)
        //{
        //    var listMst_Dealer = new List<Mst_Dealer>();

        //    var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
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
        //        UtcOffset = UserState.UtcOffset,
        //        // RQ_Mst_Dealer
        //        Rt_Cols_Mst_Dealer = "*",
        //        Mst_Dealer = null,
        //    };
        //    listMst_Dealer = WA_Mst_Dealer_Get(objRQ_Mst_Dealer);
        //    return listMst_Dealer;
        //}

        protected List<Mst_Dealer> WA_RptSv_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Mst_Dealer != null && objRT_Mst_Dealer.Lst_Mst_Dealer.Count > 0)
            {
                listMst_Dealer.AddRange(objRT_Mst_Dealer.Lst_Mst_Dealer);
            }
            return listMst_Dealer;
        }

        protected List<Mst_Dealer> WA_Rpt_Mst_Dealer_Get(RQ_Mst_Dealer objRQ_Mst_Dealer)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRT_Mst_Dealer = Mst_DealerService.Instance.WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Mst_Dealer != null && objRT_Mst_Dealer.Lst_Mst_Dealer.Count > 0)
            {
                listMst_Dealer.AddRange(objRT_Mst_Dealer.Lst_Mst_Dealer);
            }
            return listMst_Dealer;
        }


        protected List<Mst_Dealer> List_RptSv_Mst_Dealer(string strWhereClause)
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };
            listMst_Dealer = WA_RptSv_Mst_Dealer_Get(objRQ_Mst_Dealer);
            return listMst_Dealer;
        }

        protected List<Mst_Dealer> List_Rpt_Mst_Dealer(string strWhereClause)
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
            var listMst_Dealer = new List<Mst_Dealer>();

            var objRQ_Mst_Dealer = new RQ_Mst_Dealer()
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
                Ft_WhereClause = strWhereClause,
                Ft_Cols_Upd = null,
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_Dealer
                Rt_Cols_Mst_Dealer = "*",
                Mst_Dealer = null,
            };
            listMst_Dealer = WA_Rpt_Mst_Dealer_Get(objRQ_Mst_Dealer);
            return listMst_Dealer;
        }
        #endregion

        #region["Solution"]
        protected List<Sys_Solution> WA_RptSv_Sys_Solution_Get(RQ_Sys_Solution objRQ_Sys_Solution)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listSys_Solution = new List<Sys_Solution>();

            var objRT_Mst_Dealer = RptSv_Sys_SolutionService.Instance.WA_RptSv_Sys_Solution_Get(objRQ_Sys_Solution, addressAPIs);
            if (objRT_Mst_Dealer.Lst_Sys_Solution != null && objRT_Mst_Dealer.Lst_Sys_Solution.Count > 0)
            {
                listSys_Solution.AddRange(objRT_Mst_Dealer.Lst_Sys_Solution);
            }
            return listSys_Solution;
        }
        protected List<Sys_Solution> List_Sys_Solution(string strWhereClause)
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
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
                UtcOffset = UserState.UtcOffset,
                // RQ_Sys_Solution
                Rt_Cols_Sys_Solution = "*",
            };
            listSys_Solution = WA_RptSv_Sys_Solution_Get(objRQ_Sys_Solution);
            return listSys_Solution;
        }
        #endregion
        #region["Loại giấy tờ"]
        protected List<Mst_GovIDType> WA_Mst_GovIDType_Get(RQ_Mst_GovIDType objRQ_Mst_GovIDType)
        {
            var addressAPIs = UserState.AddressAPIs;
            var listMst_GovIDType = new List<Mst_GovIDType>();

            var objRT_Mst_GovIDType = Mst_GovIDTypeService.Instance.WA_RptSv_Mst_GovIDType_Get(objRQ_Mst_GovIDType, addressAPIs);
            if (objRT_Mst_GovIDType.Lst_Mst_GovIDType != null && objRT_Mst_GovIDType.Lst_Mst_GovIDType.Count > 0)
            {
                listMst_GovIDType.AddRange(objRT_Mst_GovIDType.Lst_Mst_GovIDType);
            }
            return listMst_GovIDType;
        }
        protected List<Mst_GovIDType> List_Mst_GovIDType(string strWhereClause)
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
            var listMst_GovIDType = new List<Mst_GovIDType>();

            var objRQ_Mst_GovIDType = new RQ_Mst_GovIDType()
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
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_GovIDType
                Rt_Cols_Mst_GovIDType = "*",
                Mst_GovIDType = null,
            };
            listMst_GovIDType = WA_Mst_GovIDType_Get(objRQ_Mst_GovIDType);
            return listMst_GovIDType;
        }
        #endregion
        #region["Chi cục thuế"]
        protected List<Mst_GovTaxID> List_Mst_GovTaxID(string strWhereClause)
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
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
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_GovTaxID
                Rt_Cols_Mst_GovTaxID = "*",
                Mst_GovTaxID = null,
            };
            var objRT_Mst_GovTaxID = Mst_GovTaxIDService.Instance.WA_RptSv_Mst_GovTaxID_Get(objRQ_Mst_GovTaxID, addressAPIs);
            if (objRT_Mst_GovTaxID.Lst_Mst_GovTaxID != null && objRT_Mst_GovTaxID.Lst_Mst_GovTaxID.Count > 0)
            {
                listMst_GovTaxID.AddRange(objRT_Mst_GovTaxID.Lst_Mst_GovTaxID);
            }
            return listMst_GovTaxID;
        }

        #endregion
        #region["Loại khách hàng"]
        protected List<Mst_NNTType> List_Mst_NNTType(string strWhereClause)
        {
            var waUserCode = UserState.RptSv_Sys_User.UserCode;
            var waUserPassword = UserState.RptSv_Sys_User.UserPassword;
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
                UtcOffset = UserState.UtcOffset,
                // RQ_Mst_NNTType
                Rt_Cols_Mst_NNTType = "*",
                Mst_NNTType = null,
            };
            var objRT_Mst_NNTType = Mst_NNTTypeService.Instance.WA_RptSv_Mst_NNTType_Get(objRQ_Mst_NNTType, addressAPIs);
            if (objRT_Mst_NNTType.Lst_Mst_NNTType != null && objRT_Mst_NNTType.Lst_Mst_NNTType.Count > 0)
            {
                listMst_NNTType.AddRange(objRT_Mst_NNTType.Lst_Mst_NNTType);
            }
            return listMst_NNTType;
        }

        #endregion
        #endregion

        #region[""]

        public string RedirectAccessDeny()
        {
            var redirectAccessDeny = Url.ActionLocalized("Index", "AccessDeny");
            return redirectAccessDeny;
        }
        #endregion

        #region["Return trạng thái đơn hàng - trạng thái duyệt HH"]
        public string GetInosLicOrderStatusName(string orderStatuses)
        {
            var orderStatusesName = "";
            switch (orderStatuses)
            {
                case "Pending":
                    orderStatusesName = "Mới tạo";
                    break;
                case "Approved":
                    orderStatusesName = "Đã duyệt";
                    break;
                case "Cancel":
                    orderStatusesName = "Đã hủy";
                    break;
                case "Processing":
                    orderStatusesName = "Đang thanh toán";
                    break;
                default:
                    orderStatusesName = "";
                    break;
            }
            return orderStatusesName;
        }

        public string GetCommissionStatusName(string commissionStatus)
        {
            var commissionStatusName = "";
            switch (commissionStatus)
            {
                case Client_CommissionStatus.Pending:
                    commissionStatusName = "Chưa duyệt";
                    break;
                case Client_CommissionStatus.Approve:
                    commissionStatusName = "Đã duyệt";
                    break;
                default:
                    commissionStatusName = "";
                    break;
            }
            return commissionStatusName;
        }
        #endregion
    }
}