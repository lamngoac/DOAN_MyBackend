using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.Utils;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class RptSv_Sys_UserController : BaseController
    {
        // GET: RptSv_Sys_User
        public ActionResult Index(string username, string init = "init", int recordCount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var pageInfo = new PageInfo<RptSv_Sys_User>(0, recordCount)
            {
                DataList = new List<RptSv_Sys_User>()
            };
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if(true)
            {
                var itemCount = 0;
                var pageCount = 0;

                if (init != "init")
                {
                    #region["Search"]
                    var objSys_User = new RptSv_Sys_User();

                    if (!userState.RptSv_Sys_User.DLCode.Equals(Mst_Dealer_Client.IDOCNET))
                    {
                        objSys_User.DLCode = userState.RptSv_Sys_User.DLCode;
                    }

                    objSys_User.UserName = username;

                    var strWhereClause = "";
                    strWhereClause = strWhereClause_RptSv_Sys_UserName(objSys_User);
                    var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordCount = recordCount.ToString(),
                        Ft_RecordStart = (Convert.ToInt32(page) * recordCount).ToString(),
                        Ft_WhereClause = strWhereClause,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        // RQ_RptSv_Sys_User
                        Rt_Cols_RptSv_Sys_User = "*",
                        Rt_Cols_RptSv_Sys_UserInGroup = null,
                        RptSv_Sys_User = null,
                    };

                    var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);

                    if (objRT_RptSv_Sys_UserCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_RptSv_Sys_UserCur.MySummaryTable.MyCount);
                    }
                    if (objRT_RptSv_Sys_UserCur != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                    {
                        pageInfo.DataList.AddRange(objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User);
                        pageCount = (itemCount % recordCount == 0) ? itemCount / recordCount : itemCount / recordCount + 1;
                    }
                    #endregion
                }
                else
                {
                    init = "search";
                }
                ViewBag.MyUserName = username;

                pageInfo.PageSize = recordCount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordCount).ToString();
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }

            return View(pageInfo);
        }

        #region["Tạo mới người dùng"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                #region["Danh sách đại lý"]
                var listMst_Dealer = new List<Mst_Dealer>();
                var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(new Mst_Dealer() {FlagActive = FlagActive});
                listMst_Dealer = List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer);
                #endregion
                ViewBag.ListMst_Dealer = listMst_Dealer;
                return View(new RptSv_Sys_User());
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var objRptSv_Sys_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<RptSv_Sys_User>(model);
                    
                    if (!CUtils.IsNullOrEmpty(objRptSv_Sys_UserInput.UserPassword))
                    {
                        objRptSv_Sys_UserInput.UserPassword = CUtils.GetEncodedHash(objRptSv_Sys_UserInput.UserPassword);
                    }
                    var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User();
                    {
                        // WARQBase
                        objRQ_RptSv_Sys_User.Tid = GetNextTId();
                        objRQ_RptSv_Sys_User.GwUserCode = GwUserCode;
                        objRQ_RptSv_Sys_User.GwPassword = GwPassword;
                        objRQ_RptSv_Sys_User.FuncType = null;
                        objRQ_RptSv_Sys_User.Ft_RecordStart = Ft_RecordStart;
                        objRQ_RptSv_Sys_User.Ft_RecordCount = Ft_RecordCount;
                        objRQ_RptSv_Sys_User.Ft_WhereClause = null;
                        objRQ_RptSv_Sys_User.Ft_Cols_Upd = null;
                        objRQ_RptSv_Sys_User.WAUserCode = waUserCode;
                        objRQ_RptSv_Sys_User.WAUserPassword = waUserPassword;
                        // RQ_RptSv_Sys_User
                        objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User = null;
                        objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_UserInGroup = null;
                        objRQ_RptSv_Sys_User.RptSv_Sys_User = objRptSv_Sys_UserInput;
                    };
                    RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Create(objRQ_RptSv_Sys_User, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Tạo người dùng thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index", "RptSv_Sys_User");
                    return Json(resultEntry);
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion

        #region["Sửa thông tin người dùng"]
        [HttpGet]
        public ActionResult Update(string usercode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                if (!CUtils.IsNullOrEmpty(usercode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_RptSv_Sys_UserName(new RptSv_Sys_User(){UserCode = usercode});

                    var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                        // RQ_RptSv_Sys_User
                        Rt_Cols_RptSv_Sys_User = "*",
                        Rt_Cols_RptSv_Sys_UserInGroup = null,
                        RptSv_Sys_User = null,
                    };

                    var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);

                    if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                    {
                        #region["Danh sách đại lý"]
                        var listMst_Dealer = new List<Mst_Dealer>();
                        var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(new Mst_Dealer() { FlagActive = FlagActive });
                        listMst_Dealer = List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer);
                        #endregion
                        ViewBag.ListMst_Dealer = listMst_Dealer;
                        return View(objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0]);
                    }
                }
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            return View(new RptSv_Sys_User());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var objRptSv_Sys_UserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<RptSv_Sys_User>(model);
                    if (objRptSv_Sys_UserInput != null && !CUtils.IsNullOrEmpty(objRptSv_Sys_UserInput.UserCode))
                    {
                        var strWhereClause = "";
                        strWhereClause = strWhereClause_RptSv_Sys_UserName(new RptSv_Sys_User() { UserCode = objRptSv_Sys_UserInput.UserCode });
                        var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                            // RQ_RptSv_Sys_User
                            Rt_Cols_RptSv_Sys_User = "*",
                            Rt_Cols_RptSv_Sys_UserInGroup = null,
                            RptSv_Sys_User = null,
                        };
                        var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);
                        if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                        {
                            objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].UserName = objRptSv_Sys_UserInput.UserName;
                            objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].PhoneNo = objRptSv_Sys_UserInput.PhoneNo;
                            objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].DLCode = objRptSv_Sys_UserInput.DLCode;
                            //objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].FlagRanking = objRptSv_Sys_UserInput.FlagRanking;
                            objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].FlagSysAdmin = objRptSv_Sys_UserInput.FlagSysAdmin;
                            objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].FlagActive = objRptSv_Sys_UserInput.FlagActive;

                            var strFt_Cols_Upd = "";
                            var Tbl_RptSv_Sys_User = TableName.RptSv_Sys_User + ".";
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.UserName);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.PhoneNo);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.DLCode);
                            //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.FlagRanking);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.FlagSysAdmin);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.FlagActive);

                            objRQ_RptSv_Sys_User.Ft_WhereClause = null;
                            objRQ_RptSv_Sys_User.Ft_Cols_Upd = strFt_Cols_Upd;
                            objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User = null;
                            objRQ_RptSv_Sys_User.RptSv_Sys_User = objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0];

                            RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Update(objRQ_RptSv_Sys_User, addressAPIs);

                            resultEntry.Success = true;
                            resultEntry.AddMessage("Sửa người dùng thành công!");
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                        else
                        {
                            resultEntry.Success = true;
                            resultEntry.AddMessage("Mã người dùng '" + objRptSv_Sys_UserInput.UserCode + "' không có trong hệ thống!");
                        }
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã người dùng trống!");
                    }

                    return Json(resultEntry);
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Update", null, resultEntry); 
        }
        #endregion

        #region["Xem chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string usercode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                if (!CUtils.IsNullOrEmpty(usercode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_RptSv_Sys_UserName(new RptSv_Sys_User() { UserCode = usercode });

                    var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                        // RQ_RptSv_Sys_User
                        Rt_Cols_RptSv_Sys_User = "*",
                        Rt_Cols_RptSv_Sys_UserInGroup = null,
                        RptSv_Sys_User = null,
                    };

                    var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);
                    if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                    {
                        #region["Danh sách đại lý"]
                        var listMst_Dealer = new List<Mst_Dealer>();
                        var strWhereClauseMst_Dealer = strWhereClause_Mst_Dealer(new Mst_Dealer() { FlagActive = FlagActive });
                        listMst_Dealer = List_RptSv_Mst_Dealer(strWhereClauseMst_Dealer);
                        #endregion
                        ViewBag.ListMst_Dealer = listMst_Dealer;
                        return View(objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0]);
                    }
                }
                return View(new RptSv_Sys_User());
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string usercode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            ViewBag.UserState = userState;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
            if (true)
            {
                if (!CUtils.IsNullOrEmpty(usercode))
                {
                    if (!CUtils.IsNullOrEmpty(usercode))
                    {
                        var strWhereClause = "";
                        strWhereClause = strWhereClause_RptSv_Sys_UserName(new RptSv_Sys_User() { UserCode = usercode });

                        var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                            // RQ_RptSv_Sys_User
                            Rt_Cols_RptSv_Sys_User = "*",
                            Rt_Cols_RptSv_Sys_UserInGroup = null,
                            RptSv_Sys_User = null,
                        };

                        var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);
                        //Sys_User User = new Sys_User();
                        if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count != 0)
                        {
                            objRQ_RptSv_Sys_User.RptSv_Sys_User = objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0];
                            RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Delete(objRQ_RptSv_Sys_User, addressAPIs);
                            resultEntry.Success = true;
                            resultEntry.AddMessage("Xóa người dùng thành công!");
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã người dùng '" + usercode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã người dùng trống!");
                }

                return Json(resultEntry);
            }
            else
            {
                return Redirect(RedirectAccessDeny());
            }
            
        }
        #endregion

        #region["Change Password"]
        [HttpPost]
        public ActionResult ShowPopupChangePassword(string usercode)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.UserCode = usercode;
                return JsonView("ShowPopupChangePassword", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("ShowPopupChangePassword", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string usercode, string passnew, string confpass)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            try
            {
                var title = "";
                if (!CUtils.IsNullOrEmpty(usercode))
                {
                    if (!CUtils.IsNullOrEmpty(passnew))
                    {
                        passnew = passnew.Trim();
                        if (!CUtils.IsNullOrEmpty(confpass))
                        {
                            confpass = confpass.Trim();
                            if (passnew.Equals(confpass))
                            {
                                var strWhereClause = "";
                                strWhereClause = strWhereClause_RptSv_Sys_UserName(new RptSv_Sys_User() { UserCode = usercode });

                                var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                                    // RQ_RptSv_Sys_User
                                    Rt_Cols_RptSv_Sys_User = "*",
                                    Rt_Cols_RptSv_Sys_UserInGroup = null,
                                    RptSv_Sys_User = null,
                                };
                                var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);

                                if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                                {
                                    passnew = CUtils.GetEncodedHash(passnew);

                                    objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0].UserPassword = passnew;
                                    var strFt_Cols_Upd = "";
                                    var Tbl_RptSv_Sys_User = TableName.RptSv_Sys_User + ".";
                                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_RptSv_Sys_User, TblRptSv_Sys_User.UserPassword);
                                    objRQ_RptSv_Sys_User.Rt_Cols_RptSv_Sys_User = null;
                                    objRQ_RptSv_Sys_User.RptSv_Sys_User = objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User[0];
                                    objRQ_RptSv_Sys_User.Ft_Cols_Upd = strFt_Cols_Upd;
                                    RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Update(objRQ_RptSv_Sys_User, addressAPIs);
                                    title = "Thay đổi mật khẩu thành công!";
                                    if (objRQ_RptSv_Sys_User.RptSv_Sys_User.UserCode.Equals(userState.RptSv_Sys_User.UserCode))
                                    {
                                        resultEntry.RedirectUrl = Url.Action("LogOff", "Account");
                                    }

                                }
                                else
                                {
                                    title = "Mã người dùng '" + usercode + "' không có trong hệ thống!";
                                }
                            }
                            else
                            {
                                title = "Nhập lại mật khẩu mới không đúng, Vui lòng nhập lại!";
                            }
                        }
                        else
                        {
                            title = "Nhập lại khẩu mới trống!";
                        }
                    }
                    else
                    {
                        title = "Mật khẩu mới trống!";
                    }
                }
                else
                {
                    title = "Mã người dùng trống!";
                }

                resultEntry.Success = true;
                resultEntry.AddMessage(title);

                //return Json(resultEntry);

                //return Json(new { Success = true, Title = title });

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            //return Json(new { Success = false, Detail = resultEntry.Detail });
            return Json(resultEntry);

        }
        #endregion

        #region["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var list = new List<RptSv_Sys_User>();
                    var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                        // RQ_RptSv_Sys_User
                        Rt_Cols_RptSv_Sys_User = "*",
                        Rt_Cols_RptSv_Sys_UserInGroup = null,
                        RptSv_Sys_User = null,
                    };

                    var objRT_RptSv_Sys_UserCur = RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Get(objRQ_RptSv_Sys_User, addressAPIs);
                    if (objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User != null && objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User.Count > 0)
                    {
                        list.AddRange(objRT_RptSv_Sys_UserCur.Lst_RptSv_Sys_User);

                        Dictionary<string, string> dicColNames = GetImportDicColums();

                        string url = "";
                        string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_User).Name), ref url);

                        ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_User"));

                        return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
                    }
                    else
                    {
                        return Json(new { Success = true, Title = "Không có dữ liệu!", CheckSuccess = "1" });
                    }
                }
                else
                {
                    return Redirect(RedirectAccessDeny());
                }
                
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
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    var list = new List<Sys_User>();

                    Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                    string url = "";
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Sys_User).Name), ref url);
                    ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Sys_User"));

                    return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });

                }
                else
                {
                    return Redirect(RedirectAccessDeny());
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

        #region["Import Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.RptSv_Sys_User.UserCode;
            var waUserPassword = userState.RptSv_Sys_User.UserPassword;

            var exitsData = "";
            try
            {
                //if (userState.RptSv_Sys_User.FlagSysAdmin.Equals("1"))
                if (true)
                {
                    string fileId = Guid.NewGuid().ToString("D");
                    string oldFileName = file.FileName;
                    var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
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
                    var list = new List<RptSv_Sys_User>();
                    var table = ExcelImportNew.Query(filePath, "A2");
                    if (table != null && table.Rows.Count > 0)
                    {
                        if (table.Columns.Count != 6)
                        {
                            exitsData = "File excel import không hợp lệ!";
                            resultEntry.AddMessage(exitsData);
                            return Json(resultEntry);
                        }
                        else
                        {
                            #region["Check null"]
                            foreach (DataRow dr in table.Rows)
                            {
                                if (CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.UserCode]))
                                {
                                    exitsData = "Mã người dùng không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.UserName]))
                                {
                                    exitsData = "Tên người dùng không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.DLCode]))
                                {
                                    exitsData = "Mã đại lý không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                if (CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.UserPassword]))
                                {
                                    exitsData = "Mật khẩu không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    var password = dr[TblRptSv_Sys_User.UserPassword].ToString().Trim();
                                    if (password.Length < 6)
                                    {
                                        exitsData = "Mật khẩu > 5 ký tự!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                if (!CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.PhoneNo]))
                                {
                                    var phone = dr[TblRptSv_Sys_User.PhoneNo].ToString().Trim();
                                    if (phone.Length <= 10)
                                    {
                                        if (CUtils.IsNumber(phone))
                                        {
                                            var index = phone.IndexOf(".");
                                            if (index >= 0)
                                            {
                                                exitsData = "Điện thoại không hợp lệ!";
                                                resultEntry.AddMessage(exitsData);
                                                return Json(resultEntry);
                                            }
                                        }
                                        else
                                        {
                                            exitsData = "Điện thoại không hợp lệ!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                    else
                                    {
                                        exitsData = "Điện thoại không hợp lệ!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                                //if (CUtils.IsNullOrEmpty(dr["EMail"]))
                                //{
                                //    exitsData = "Email không được trống!";
                                //    resultEntry.AddMessage(exitsData);
                                //    return Json(resultEntry);
                                //}
                                //else
                                //{
                                //    var email = dr["EMail"].ToString().Trim();
                                //    if (!CUtils.IsValidEmail(email))
                                //    {
                                //        exitsData = "Email không hợp lệ!";
                                //        resultEntry.AddMessage(exitsData);
                                //        return Json(resultEntry);
                                //    }
                                //}
                                //if (CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.FlagRanking]))
                                //{
                                //    exitsData = "Ranking không được trống!";
                                //    resultEntry.AddMessage(exitsData);
                                //    return Json(resultEntry);
                                //}
                                //else
                                //{
                                //    var ranking = dr[TblRptSv_Sys_User.FlagRanking].ToString().Trim();
                                //    if (!ranking.Equals("0") && !ranking.Equals("1"))
                                //    {
                                //        exitsData = "Ranking nhập '0' hoặc '1'!";
                                //        resultEntry.AddMessage(exitsData);
                                //        return Json(resultEntry);
                                //    }
                                //}
                                if (CUtils.IsNullOrEmpty(dr[TblRptSv_Sys_User.FlagSysAdmin]))
                                {
                                    exitsData = "Cờ quản trị hệ thống không được trống!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    var sysAdmin = dr[TblRptSv_Sys_User.FlagSysAdmin].ToString().Trim();
                                    if (!sysAdmin.Equals("0") && !sysAdmin.Equals("1"))
                                    {
                                        exitsData = "Cờ quản trị hệ thống nhập '0' hoặc '1'!";
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                            #endregion

                            #region["Check duplicate"]
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                var userCodeCur = table.Rows[i][TblRptSv_Sys_User.UserCode].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        var _userCodeCur = table.Rows[j][TblRptSv_Sys_User.UserCode].ToString().Trim();
                                        if (userCodeCur.Equals(_userCodeCur))
                                        {
                                            exitsData = "Mã người dùng '" + userCodeCur + "' bị lặp trong file excel!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                        list = DataTableCmUtils.ToListof<RptSv_Sys_User>(table); ;
                        // Gọi hàm save data
                        if (list != null && list.Count > 0)
                        {
                            foreach (var item in list)
                            {
                                //item.FlagActive = "1";
                                var objRQ_RptSv_Sys_User = new RQ_RptSv_Sys_User()
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
                                    // RQ_RptSv_Sys_User
                                    Rt_Cols_RptSv_Sys_User = null,
                                    Rt_Cols_RptSv_Sys_UserInGroup = null,
                                    RptSv_Sys_User = item,
                                };

                                RptSv_Sys_UserService.Instance.WA_RptSv_Sys_User_Create(objRQ_RptSv_Sys_User, addressAPIs);
                            }
                        }

                        resultEntry.Success = true;
                        exitsData = "Đã nhập dữ liệu excel thành công!";
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
                    return Redirect(RedirectAccessDeny());
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

        #region
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {TblRptSv_Sys_User.UserCode,"Mã người dùng"},                 
                 //{"UserPassword","Mật khẩu"},
                 {TblRptSv_Sys_User.UserName,"Tên người dùng"},
                 {TblRptSv_Sys_User.DLCode,"Mã đại lý"},
                 {TblRptSv_Sys_User.PhoneNo,"Điện thoại"},
                 //{TblRptSv_Sys_User.FlagRanking,"Ranking"},
                 {TblRptSv_Sys_User.FlagSysAdmin,"Cờ Quản trị HT"},
                 {TblRptSv_Sys_User.FlagActive,"Trạng thái"}
            };
        }

        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblRptSv_Sys_User.UserCode,"Mã người dùng (*)"},                 
                {TblRptSv_Sys_User.UserPassword,"Mật khẩu (*)"},
                {TblRptSv_Sys_User.UserName,"Tên người dùng (*)"},
                {TblRptSv_Sys_User.DLCode,"Mã đại lý (*)"},
                {TblRptSv_Sys_User.PhoneNo,"Điện thoại"},
                //{TblRptSv_Sys_User.FlagRanking,"Ranking (*)"},
                {TblRptSv_Sys_User.FlagSysAdmin,"Cờ Quản trị HT"},
            };
        }

        #endregion

        #region["strWhereClause"]
        private string strWhereClause_RptSv_Sys_UserName(RptSv_Sys_User data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_RptSv_Sys_User = TableName.RptSv_Sys_User + ".";
            if (!CUtils.IsNullOrEmpty(data.UserCode))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_User + TblRptSv_Sys_User.UserCode, data.UserCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.UserName))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_User + TblRptSv_Sys_User.UserName, "%" + data.UserName.ToString().Trim() + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.DLCode))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_User + TblRptSv_Sys_User.DLCode, data.DLCode.ToString().Trim(), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_RptSv_Sys_User + TblRptSv_Sys_User.FlagActive, data.FlagActive.ToString().Trim(), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }


        private string strWhereClause_Mst_Dealer(Mst_Dealer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Dealer = TableName.Mst_Dealer + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Dealer + TblMst_Dealer.FlagActive, data.FlagActive.ToString().Trim(), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}