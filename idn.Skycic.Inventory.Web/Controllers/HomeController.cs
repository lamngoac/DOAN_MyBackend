using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.Diagnostics;
using idn.Skycic.Inventory.Web.Extensions;
using idn.Skycic.Inventory.Web.State;
using idn.Skycic.Inventory.Common.ModelsUI;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class HomeController : idnBaseController
    {
        public ActionResult Index()
        {
            var totalElapsedMilliseconds = 0.0;
            var stringBuilder = new StringBuilder();
            var strLogs = "";
            var PathFileLog = "";
            CUtils.StrAppend(stringBuilder, "Màn hình danh sách Org:");
            CUtils.StrAppend(stringBuilder, "Method: Get");
            if (!CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["PathFileLog"]))
            {
                PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
                var iindex = PathFileLog.IndexOf("File_Home");
                if (iindex < 0)
                {
                    var iCount = ReturnCount();
                    CreateFileWriteLog("File_Home", Today, iCount);
                }
            }
            else
            {
                var iCount = ReturnCount();
                CreateFileWriteLog("File_Home", Today, iCount);
            }

            var dateStart = DateTime.Now;

            var waUserCode_MstSV = WAUserCode_MstSV;
            var waUserPassword_MstSV = WAUserPassword_MstSV;
            var addressMasterServerAPIs = ServiceAddress.BaseMasterServerAPIAddress;
            var tokenID = System.Web.HttpContext.Current.Session["TokenID"];
            var usercode = System.Web.HttpContext.Current.Session["UserCodeLogin"];
            var password = System.Web.HttpContext.Current.Session["PasswordLogin"];
            if (CUtils.IsNullOrEmpty(tokenID))
            {
                if (Request.Cookies["tokenid"] != null)
                {
                    HttpCookie ck_tokenid = Request.Cookies["tokenid"];
                    if (ck_tokenid != null)
                    {
                        var cktokenid = ck_tokenid.Value;
                        if (!CUtils.IsNullOrEmpty(cktokenid))
                        {
                            tokenID = cktokenid;
                        }
                    }
                }
            }

            var dateEndGetToken = DateTime.Now;
            TimeSpan timeSpanGetToken = dateEndGetToken - dateStart;
            CUtils.StrAppend(stringBuilder, "Get token: " + timeSpanGetToken.TotalMilliseconds);
            #region["Comment"]

            //if (CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["UserCodeLogin"]))
            //{
            //    if (Request.Cookies["_ibcckuc"] != null)
            //    {
            //        HttpCookie ck_usercode = Request.Cookies["_ibcckuc"];
            //        if (ck_usercode != null)
            //        {
            //            var ckusercode = ck_usercode.Value;
            //            if (!CUtils.IsNullOrEmpty(ckusercode))
            //            {
            //                // giải mã
            //                var usercodeDecrypt = CUtils.Decrypt(ckusercode, true);
            //                // đảo ngược chuỗi
            //                var usercodeReverse = CUtils.ReverseString(usercodeDecrypt);
            //                usercode = usercodeReverse;
            //                System.Web.HttpContext.Current.Session["UserCodeLogin"] = usercode;
            //            }
            //        }
            //    }
            //}

            //if (CUtils.IsNullOrEmpty(System.Web.HttpContext.Current.Session["PasswordLogin"]))
            //{
            //    if (Request.Cookies["_ibcckup"] != null)
            //    {
            //        HttpCookie ck_password = Request.Cookies["_ibcckup"];
            //        if (ck_password != null)
            //        {
            //            var ckpassword = ck_password.Value;
            //            if (!CUtils.IsNullOrEmpty(ckpassword))
            //            {
            //                // giải mã 
            //                var passwordDecrypt = CUtils.Decrypt(ckpassword, true);
            //                // đảo ngược chuỗi
            //                var passwordReverse = CUtils.ReverseString(passwordDecrypt);
            //                // cắt chuỗi
            //                var passwordSubstring = passwordReverse.Substring(23);
            //                password = passwordSubstring;
            //                System.Web.HttpContext.Current.Session["PasswordLogin"] = password;
            //            }
            //        }
            //    }
            //}

            #endregion

            var listOS_Inos_Org = new List<OS_Inos_Org>();
            if (!CUtils.IsNullOrEmpty(tokenID))
            {
                System.Web.HttpContext.Current.Session["TokenID"] = tokenID;
                var rawUrl = Request.RawUrl.ToLower().Trim();
                if (!rawUrl.Equals("/"))
                {
                    var url = "/";
                    return Redirect(url);
                }
                var objRQ_OS_Inos_Org = new RQ_OS_Inos_Org()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = tokenID.ToString().Trim(),
                    WAUserCode = waUserCode_MstSV,
                    WAUserPassword = waUserPassword_MstSV,
                    // RQ_OS_Inos_Org
                    Rt_Cols_OS_Inos_Org = "*",
                };

                var dateStartWA_OS_Inos_Org_GetMyOrgList = DateTime.Now;
                var objRT_OS_Inos_Org = OS_Inos_OrgService.Instance.WA_OS_Inos_Org_GetMyOrgList(objRQ_OS_Inos_Org, addressMasterServerAPIs);
                var dateEndtWA_OS_Inos_Org_GetMyOrgList = DateTime.Now;
                TimeSpan timeSpanWA_OS_Inos_Org_GetMyOrgList = dateEndtWA_OS_Inos_Org_GetMyOrgList - dateStartWA_OS_Inos_Org_GetMyOrgList;
                CUtils.StrAppend(stringBuilder, "Khối get danh sách Org:");
                CUtils.StrAppend(stringBuilder, "WA_OS_Inos_Org_GetMyOrgList: " + timeSpanWA_OS_Inos_Org_GetMyOrgList.TotalMilliseconds);

                if (objRT_OS_Inos_Org.Lst_OS_Inos_Org != null && objRT_OS_Inos_Org.Lst_OS_Inos_Org.Count > 0)
                {
                    var dateStartListOS_Inos_Org = DateTime.Now;
                    listOS_Inos_Org = (from item in objRT_OS_Inos_Org.Lst_OS_Inos_Org where (!CUtils.IsNullOrEmpty(item.ParentId) && item.ParentId.ToString().Trim().Equals("0")) select item).ToList();

                    var listOS_Inos_OrgSub = (from item in objRT_OS_Inos_Org.Lst_OS_Inos_Org where (!CUtils.IsNullOrEmpty(item.ParentId) && !item.ParentId.ToString().Trim().Equals("0")) select item).ToList();

                    if (listOS_Inos_OrgSub != null && listOS_Inos_OrgSub.Count > 0)
                    {
                        foreach (var item in listOS_Inos_OrgSub)
                        {
                            var objOS_Inos_Org = listOS_Inos_Org.FirstOrDefault(it => (!CUtils.IsNullOrEmpty(it.Id) && !CUtils.IsNullOrEmpty(item.ParentId) && it.Id.ToString().Trim().Equals(item.ParentId.ToString().Trim())));
                            if (objOS_Inos_Org == null)
                            {
                                listOS_Inos_Org.Add(objOS_Inos_Org);
                            }
                        }
                        
                    }

                    var dateEndListOS_Inos_Org = DateTime.Now;

                    TimeSpan timeSpanListOS_Inos_Org = dateEndListOS_Inos_Org - dateStartListOS_Inos_Org;
                    CUtils.StrAppend(stringBuilder, "Lọc danh sách Org hiển thị giao diện: " + timeSpanListOS_Inos_Org.TotalMilliseconds);


                    #region["Comment"]

                    //foreach (var item in objRT_OS_Inos_Org.Lst_OS_Inos_Org)
                    //{
                    //    if (!CUtils.IsNullOrEmpty(item.ParentId))
                    //    {
                    //        if (item.ParentId.ToString().Trim().Equals("0"))
                    //        {
                    //            var objOS_Inos_Org = listOS_Inos_OrgUI.FirstOrDefault(it => (!CUtils.IsNullOrEmpty(it.ParentIdTem) && !CUtils.IsNullOrEmpty(item.Id) && it.ParentIdTem.ToString().Trim().Equals(item.Id.ToString().Trim())));
                    //            if (objOS_Inos_Org == null)
                    //            {
                    //                listOS_Inos_OrgUI.Add(new OS_Inos_OrgUI()
                    //                {
                    //                    Id = item.Id,
                    //                    ParentId = item.ParentId,
                    //                    Name = item.Name,
                    //                    BizType = item.BizType,
                    //                    BizField = item.BizField,
                    //                    OrgSize = item.OrgSize,
                    //                    ContactName = item.ContactName,
                    //                    Email = item.Email,
                    //                    PhoneNo = item.PhoneNo,
                    //                    Description = item.Description,
                    //                    Enable = item.Enable,
                    //                    CurrentUserRole = item.CurrentUserRole,
                    //                    FlagActive = item.FlagActive,
                    //                    LogLUDTimeUTC = item.LogLUDTimeUTC,
                    //                    LogLUBy = item.LogLUBy,
                    //                    ParentIdTem = item.Id.ToString().Trim(), // ParentId = 0 => OrgId chính là Network
                    //                });
                    //            }
                    //        }
                    //        else
                    //        {
                    //            var objOS_Inos_Org = listOS_Inos_OrgUI.FirstOrDefault(it => (!CUtils.IsNullOrEmpty(it.ParentIdTem) && !CUtils.IsNullOrEmpty(item.ParentId) && it.ParentIdTem.ToString().Trim().Equals(item.ParentId.ToString().Trim())));
                    //            if (objOS_Inos_Org == null)
                    //            {
                    //                listOS_Inos_OrgUI.Add(new OS_Inos_OrgUI()
                    //                {
                    //                    Id = item.Id,
                    //                    ParentId = item.ParentId,
                    //                    Name = item.Name,
                    //                    BizType = item.BizType,
                    //                    BizField = item.BizField,
                    //                    OrgSize = item.OrgSize,
                    //                    ContactName = item.ContactName,
                    //                    Email = item.Email,
                    //                    PhoneNo = item.PhoneNo,
                    //                    Description = item.Description,
                    //                    Enable = item.Enable,
                    //                    CurrentUserRole = item.CurrentUserRole,
                    //                    FlagActive = item.FlagActive,
                    //                    LogLUDTimeUTC = item.LogLUDTimeUTC,
                    //                    LogLUBy = item.LogLUBy,
                    //                    ParentIdTem = item.Id.ToString().Trim(), // ParentId = 0 => OrgId chính là Network
                    //                });
                    //            }
                    //        }
                    //    }
                    //}

                    #endregion
                }
            }
            else
            {
                var urlLogin = RedirectActionToLogin();
                var dateEndGetToken_Null = DateTime.Now;
                TimeSpan timeSpanGetToken_Null = dateEndGetToken_Null - dateStart;
                CUtils.StrAppend(stringBuilder, "Tổng thời gian (Token null): " + timeSpanGetToken_Null.TotalMilliseconds);
                PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
                strLogs = stringBuilder.ToString();
                System.IO.File.AppendAllText(PathFileLog, strLogs, Encoding.UTF8);
                stringBuilder.Clear();
                strLogs = "";

                return Redirect(urlLogin);
            }

            var dateEnd = DateTime.Now;
            TimeSpan timeSpan = dateEnd - dateStart;
            CUtils.StrAppend(stringBuilder, "Tổng thời gian:" + timeSpan.TotalMilliseconds);
            PathFileLog = System.Web.HttpContext.Current.Session["PathFileLog"] as string;
            strLogs = stringBuilder.ToString();
            System.IO.File.AppendAllText(PathFileLog, strLogs, Encoding.UTF8);
            stringBuilder.Clear();
            strLogs = "";

            return View(listOS_Inos_Org);
            
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult License()
        {
            var pageInfo = new PageInfo<Invoice_TempInvoice>(0, 10)
            {
                DataList = new List<Invoice_TempInvoice>()
            };
            return View(pageInfo);
        }

        #region["strWhereClause"]
        private string strWhereClause_SysUser(string usercode)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Sys_User = TableName.Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(usercode))
            {
                sbSql.AddWhereClause(Tbl_Sys_User + TblSys_User.UserCode, usercode, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, data.MST.ToString().Trim(), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}