using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using inos.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class NotificationController : AdminBaseController
    {
        // GET: Notification
        public ActionResult Index(int page = 0, int recordcount = 10, int totalitem = 0)
        {
            NotificationStatuses notificationStatuses = NotificationStatuses.ALL;
            var userState = SessionWrapper.UserState;
            Session["UserState"] = userState;
            var objInosNotificationResult = new InosNotificationResult()
            {
                List = new List<InosNotification>(),
                PageIndex = 0,
                PageSize = 0,
                UnreadCount = 0,
                PageCount = 0,
                ItemCount = 0,
            };
            var errorDetail = "";
            try
            {
                InitSSO();
                #region["Lấy all Notify"]
                Int64 iNetworkId = 0;
                if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
                {
                    iNetworkId = Convert.ToInt64(userState.Mst_NNT.NetworkID);
                }
                objInosNotificationResult = NotificationService.GetNotifications(iNetworkId, "", notificationStatuses, page, recordcount);
                //var data = new
                //{
                //    Success = true,
                //    //InosNotificationResult = objInosNotificationResult,
                //    InosNotificationResult = new
                //    {
                //        PageIndex = objInosNotificationResult.PageIndex,
                //        PageSize = objInosNotificationResult.PageSize,
                //        UnreadCount = objInosNotificationResult.UnreadCount,
                //        PageCount = objInosNotificationResult.PageCount,
                //        ItemCount = objInosNotificationResult.ItemCount,
                //        List = objInosNotificationResult.List.Select(i => new {
                //            Id = i.Id,
                //            NetworkId = i.NetworkId,
                //            SolutionCode = i.SolutionCode,
                //            TypeCode = i.TypeCode,
                //            SubType = i.SubType,
                //            UserId = i.UserId,
                //            Detail = !CUtils.IsNullOrEmpty(i.Detail) ? i.Detail.Replace("\n", "<br />") : "",
                //            DetailShort = !CUtils.IsNullOrEmpty(i.Detail) ? CUtils.CatChuoi(i.Detail.Replace("\n", "<br />"), 100) : "",
                //            DetailRemoveTagsHtml = !CUtils.IsNullOrEmpty(i.Detail) ? CUtils.CatChuoi(CUtils.StripTagsRegexCompiled(i.Detail.Replace("\n", "<br />")), 100) : "",
                //            SendUserId = i.SendUserId,
                //            Params = i.Params,
                //            Status = i.Status,
                //            EmailStatus = i.EmailStatus,
                //            FirebaseStatus = i.FirebaseStatus,
                //            CreateDTime = i.CreateDTime.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT),
                //            InosUser = i.InosUser,
                //        }),

                //    },
                //    DateTime = DateTime.Now.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT),
                //};
                #endregion
                ViewBag.DateTime = DateTime.Now.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT);
                //ViewBag.InosNotificationResult = data;
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {

                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    errorDetail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail;

                }
                else
                {
                    errorDetail = ex.Message + "<br />" + ex.StackTrace;
                }
            }
            ViewBag.ErrorDetail = errorDetail;
            return View(objInosNotificationResult);
        }

        /// <summary>
        /// public enum NotificationStatuses
        /// {
        /// UNREAD = 0,
        /// READ = 1,
        /// DONE = 2,
        /// DELETED = 10,
        /// NOT_DELETED = 98,
        /// ALL = 99
        /// }
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SearchIndex(string status = "99", int page = 0, int recordcount = 10, int totalitem = 0)
        {
            NotificationStatuses notificationStatuses = NotificationStatuses.ALL;
            switch (status)
            {
                case "0":
                    {
                        notificationStatuses = NotificationStatuses.UNREAD;
                        break;
                    }
                case "1":
                    {
                        notificationStatuses = NotificationStatuses.READ;
                        break;
                    }
                case "2":
                    {
                        notificationStatuses = NotificationStatuses.DONE;
                        break;
                    }
                case "10":
                    {
                        notificationStatuses = NotificationStatuses.DELETED;
                        break;
                    }
                case "98":
                    {
                        notificationStatuses = NotificationStatuses.NOT_DELETED;
                        break;
                    }
                case "99":
                    {
                        notificationStatuses = NotificationStatuses.ALL;
                        break;
                    }
                default:
                    {
                        notificationStatuses = NotificationStatuses.ALL;
                        break;
                    }
            }
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = SessionWrapper.UserState;
            Session["UserState"] = userState;
            var objInosNotificationResult = new InosNotificationResult()
            {
                List = new List<InosNotification>(),
                PageIndex = 0,
                PageSize = 0,
                UnreadCount = 0,
                PageCount = 0,
                ItemCount = 0,
            };

            try
            {
                InitSSO();
                #region["Lấy all Notify"]
                Int64 iNetworkId = 0;
                if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
                {
                    iNetworkId = Convert.ToInt64(userState.Mst_NNT.NetworkID);
                }
                objInosNotificationResult = NotificationService.GetNotifications(iNetworkId, "", notificationStatuses, page, recordcount);

                #endregion
                var data = new
                {
                    Success = true,
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
                    DateTime = DateTime.Now.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT),
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = cex.Message,
                        Detail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = ex.Message,
                        Detail = ex.Message + ex.StackTrace
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search(int page = 0, int recordcount = 10, int totalitem = 0)
        {
            NotificationStatuses notificationStatuses = NotificationStatuses.ALL;

            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = SessionWrapper.UserState;
            Session["UserState"] = userState;
            var objInosNotificationResult = new InosNotificationResult()
            {
                List = new List<InosNotification>(),
                PageIndex = 0,
                PageSize = 0,
                UnreadCount = 0,
                PageCount = 0,
                ItemCount = 0,
            };

            try
            {
                InitSSO();
                #region["Lấy all Notify"]
                Int64 iNetworkId = 0;
                if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
                {
                    iNetworkId = Convert.ToInt64(userState.Mst_NNT.NetworkID);
                }
                objInosNotificationResult = NotificationService.GetNotifications(iNetworkId, "", notificationStatuses, page, recordcount);

                #endregion
                var data = new
                {
                    Success = true,
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
                    DateTime = DateTime.Now.ToString(Nonsense.DATE_TIME_FULL_DB_FORMAT),
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = cex.Message,
                        Detail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = ex.Message,
                        Detail = ex.Message + ex.StackTrace
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
            }
        }

        [HttpGet]
        public ActionResult Setting()
        {
            var userState = this.UserState;
            ViewBag.UserState = this.UserState;
            var objNotificationSetting = new NotificationSetting()
            {
                Sound = true,
                Popup = true,
                UserId = 0,
                Items = new List<NotificationSettingItem>(),
            };
            var errorDetail = "";
            var listNotificationSettingItemDistinct = new List<NotificationSettingItem>();
            var listNotificationSettingItemSystem = new List<NotificationSettingItem>();
            var listNotificationSettingItemSolutionCode = new List<NotificationSettingItem>();
            try
            {
                InitSSO();

                //2020-12-14: Truyền thêm networkid
                Int64 iNetworkId = 0;
                if (!CUtils.IsNullOrEmpty(userState.Mst_NNT.NetworkID))
                {
                    iNetworkId = Convert.ToInt64(userState.Mst_NNT.NetworkID);
                }
                objNotificationSetting = NotificationService.GetUserNotificationSetting(iNetworkId);

                //var abc = CUtils.RemoveDuplicatesSet<NotificationSettingItem>(objNotificationSetting.Items);

                //var list = new List<NotificationSettingItem>()
                //{
                //    objNotificationSetting.Items[0],
                //    objNotificationSetting.Items[0],
                //};
                //var a1 = CUtils.RemoveDuplicatesSet<NotificationSettingItem>(list);
                if (objNotificationSetting != null)
                {
                    if (objNotificationSetting.Items != null && objNotificationSetting.Items.Count > 0)
                    {
                        listNotificationSettingItemDistinct = objNotificationSetting.Items.GroupBy(p => p.SolutionCode)
                          .Select(g => g.First())
                          .ToList();
                        listNotificationSettingItemDistinct = listNotificationSettingItemDistinct.Where(it => !CUtils.IsNullOrEmpty(it.SolutionCode) && (!CUtils.StrValue(it.SolutionCode).Equals("CUSTOMERCENTER") && !CUtils.StrValue(it.SolutionCode).Equals("CUSCENTER") && !CUtils.StrValue(it.SolutionCode).Equals("PRODUCTCENTER") && !CUtils.StrValue(it.SolutionCode).Equals("PRDCENTER"))).ToList();
                        if (listNotificationSettingItemDistinct != null && listNotificationSettingItemDistinct.Count > 0)
                        {
                            listNotificationSettingItemSystem = listNotificationSettingItemDistinct.Where(it => it.SolutionCode.Equals("System")).ToList();
                            listNotificationSettingItemSolutionCode = listNotificationSettingItemDistinct.Where(it => !it.SolutionCode.Equals("System")).ToList();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {

                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    errorDetail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail;

                }
                else
                {
                    errorDetail = ex.Message + "<br />" + ex.StackTrace;
                }
            }
            ViewBag.ErrorDetail = errorDetail;
            ViewBag.ListNotificationSettingItemDistinct = listNotificationSettingItemDistinct;
            ViewBag.ListNotificationSettingItemSystem = listNotificationSettingItemSystem;
            ViewBag.ListNotificationSettingItemSolutionCode = listNotificationSettingItemSolutionCode;
            return View(objNotificationSetting);
        }

        [HttpPost]
        public ActionResult Setting(string model)
        {
            try
            {
                InitSSO();
                var objNotificationSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<NotificationSetting>(model);
                NotificationService.SaveUserNotificationSetting(objNotificationSetting);
                var data = new
                {
                    Success = true,
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = cex.Message,
                        Detail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = ex.Message,
                        Detail = ex.Message + ex.StackTrace
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Notification_Change(string model)
        {
            var userState = SessionWrapper.UserState;
            Session["UserState"] = userState;

            try
            {
                InitSSO();
                var serverBaseAddress = inos.common.Paths.AuthorizationServerBaseAddress;
                var listInosNotification = Newtonsoft.Json.JsonConvert.DeserializeObject<List<InosNotification>>(model);
                NotificationService.UpdateNotificationStatus(listInosNotification);
                var data = new
                {
                    Success = true,
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = cex.Message,
                        Detail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = ex.Message,
                        Detail = ex.Message + ex.StackTrace
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewItem(string model)
        {
            var userState = SessionWrapper.UserState;
            Session["UserState"] = userState;

            try
            {
                InitSSO();
                var objInosNotification = Newtonsoft.Json.JsonConvert.DeserializeObject<InosNotification>(model);
                if (objInosNotification.Status.Equals(NotificationStatuses.UNREAD))
                {
                    objInosNotification.Status = NotificationStatuses.READ;
                    var listInosNotification = new List<InosNotification>()
                    {
                        objInosNotification,
                    };
                    NotificationService.UpdateNotificationStatus(listInosNotification);
                }

                var objRT_InosNotification = NotificationService.GetNotificationDetail(objInosNotification.Id);
                if (objRT_InosNotification != null)
                {
                    objRT_InosNotification.Detail = !CUtils.IsNullOrEmpty(objRT_InosNotification.Detail) ? objRT_InosNotification.Detail.Replace("\n", "<br />") : "";
                }

                var data = new
                {
                    Success = true,
                    InosNotification = objRT_InosNotification,
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = cex.Message,
                        Detail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = ex.Message,
                        Detail = ex.Message + ex.StackTrace
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TakeAction(string model)
        {
            var userState = SessionWrapper.UserState;
            Session["UserState"] = userState;

            try
            {
                InitSSO();
                //var objInosNotification = Newtonsoft.Json.JsonConvert.DeserializeObject<InosNotification>(model);
                //if (objInosNotification.Status.Equals(NotificationStatuses.UNREAD))
                //{
                //    objInosNotification.Status = NotificationStatuses.READ;
                //    var listInosNotification = new List<InosNotification>()
                //    {
                //        objInosNotification,
                //    };
                //    NotificationService.UpdateNotificationStatus(listInosNotification);
                //}

                //var objRT_InosNotification = NotificationService.GetNotificationDetail(objInosNotification.Id);
                //if (objRT_InosNotification != null)
                //{
                //    objRT_InosNotification.Detail = !CUtils.IsNullOrEmpty(objRT_InosNotification.Detail) ? objRT_InosNotification.Detail.Replace("\n", "<br />") : "";
                //}
                var data = new
                {
                    Success = true,
                    Message = "Chưa làm chức năng này",
                    //InosNotification = objRT_InosNotification,
                };
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                if (ex is mbiz.core.Exceptions.ServiceException)
                {
                    var cex = ex as mbiz.core.Exceptions.ServiceException;
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = cex.Message,
                        Detail = "ErrorCode: " + cex.ErrorCode + "<br />" + "ErrorMessage: " + cex.ErrorMessage + "<br />" + "ErrorDetail: " + cex.ErrorDetail
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var data = new
                    {
                        Success = false,
                        Status = 1,
                        Message = ex.Message,
                        Detail = ex.Message + ex.StackTrace
                    };
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

            }

        }
    }
}