using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.State;
using inos.common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace idn.Skycic.Inventory.Website.Handlers
{
    /// <summary>
    /// Summary description for KeepAlive
    /// </summary>
    public class KeepAlive : IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        string GwUserCode = ConfigurationManager.AppSettings["GwUserCode"];
        string GwPassword = ConfigurationManager.AppSettings["GwPassword"];
        public int PageSizeConfig = 10;

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.AddHeader("Cache-Control", "no-cache");
            context.Response.AddHeader("Pragma", "no-cache");

            string json = new StreamReader(context.Request.InputStream).ReadToEnd();
            string myName = context.Request.Form["model"];
            var objInosNotificationResult = new InosNotificationResult()
            {
                List = new List<InosNotification>(),
                PageIndex = 0,
                PageSize = 0,
                UnreadCount = 0,
                PageCount = 0,
                ItemCount = 0,
            };
            var listInosNotification_UnRead = new List<InosNotification>();
            //var objInosNotification_UnRead = new InosNotification();
            inos.common.Service.NotificationService notificationService;

            var Success = false;
            var ErrorDetail = "";
            var serverBaseAddress = "";
            try
            {
                if (context.Session != null)
                {
                    var userState = context.Session["UserState"] as UserState;
                    if (userState != null)
                    {
                        if (userState.HttpSessionStateBase != null)
                        {

                            #region["Init SSO"]
                            serverBaseAddress = inos.common.Paths.AuthorizationServerBaseAddress;
                            var iindex = serverBaseAddress.IndexOf("localhost");
                            if (iindex >= 0)
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

                            #endregion

                            notificationService = new inos.common.Service.NotificationService(userState.HttpSessionStateBase);
                            //notificationService = new inos.common.Service.NotificationService(null);
                            #region["Lấy all Notify"]
                            Int64 iNetworkId = 0;
                            if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
                            {
                                iNetworkId = Convert.ToInt64(userState.Mst_NNT.NetworkID);
                            }
                            objInosNotificationResult = notificationService.GetNotifications(iNetworkId, "", inos.common.Model.NotificationStatuses.ALL, 0, PageSizeConfig);
                            if (objInosNotificationResult != null)
                            {
                                if (objInosNotificationResult.List != null && objInosNotificationResult.List.Count > 0)
                                {
                                    var objInosNotification_UnRead = objInosNotificationResult.List.Where(it => it.Status.Equals(NotificationStatuses.UNREAD)).FirstOrDefault();
                                    if (objInosNotification_UnRead != null)
                                    {
                                        listInosNotification_UnRead.Add(objInosNotification_UnRead);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                Success = true;
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    ErrorDetail = "ServerBaseAddress: " + serverBaseAddress + "<br />" + "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail;

                }
                else
                {
                    ErrorDetail = "ServerBaseAddress: " + serverBaseAddress + "<br />" + ex.Message + ex.StackTrace;
                }
            }



            var objNotificationResult = new
            {
                Success = Success,
                ErrorDetail = ErrorDetail,
                //InosNotificationResult = objInosNotificationResult,
                InosNotificationResult = new
                {
                    PageIndex = objInosNotificationResult.PageIndex,
                    PageSize = objInosNotificationResult.PageSize,
                    UnreadCount = objInosNotificationResult.UnreadCount,
                    PageCount = objInosNotificationResult.PageCount,
                    ItemCount = objInosNotificationResult.ItemCount,
                    List = objInosNotificationResult.List.Select(i => new {
                        Id = i.Id,
                        NetworkId = i.NetworkId,
                        SolutionCode = i.SolutionCode,
                        TypeCode = i.TypeCode,
                        SubType = i.SubType,
                        UserId = i.UserId,
                        Detail = !CUtils.IsNullOrEmpty(i.Detail) ? i.Detail.Replace("\n", "<br />") : "",
                        DetailShort = !CUtils.IsNullOrEmpty(i.Detail) ? CUtils.CatChuoi(i.Detail.Replace("\n", "<br />"), 100) : "",
                        DetailRemoveTagsHtml = !CUtils.IsNullOrEmpty(i.Detail) ? CUtils.CatChuoi(CUtils.StripTagsRegexCompiled(i.Detail.Replace("\n", "<br />")), 100) : "",
                        SendUserId = i.SendUserId,
                        Params = i.Params,
                        Status = i.Status,
                        EmailStatus = i.EmailStatus,
                        FirebaseStatus = i.FirebaseStatus,
                        CreateDTime = i.CreateDTime.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT),
                        InosUser = i.InosUser,
                    }),

                },
                ListInosNotification_UnRead = listInosNotification_UnRead,
                DateTime = DateTime.Now.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT),
            };
            context.Response.Write(JsonConvert.SerializeObject(objNotificationResult));

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #region ["strWhereClause"]

        #endregion
    }
}