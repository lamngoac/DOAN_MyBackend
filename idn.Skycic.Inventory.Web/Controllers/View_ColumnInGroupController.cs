using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Utils;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Constants;
using System.Data;
using System.Text;
using idn.Skycic.Inventory.Common.ModelsUI;
using System.Diagnostics;

namespace idn.Skycic.Inventory.Web.Controllers
{
    public class View_ColumnInGroupController : AdminBaseController
    {
        // GET: View_ColumnInGroup
        public ActionResult Index(string role)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var listColumnHienThi = new List<View_ColumnView>();
            var listColumnKhongHienThi = new List<View_ColumnView>();


            var objView_GroupView = new View_GroupView()
            {
                FlagActive = FlagActive,
            };
            var strWhereClause_ViewGroupView = strWhereClause_View_GroupView(objView_GroupView);
            var objRQ_View_GroupView = new RQ_View_GroupView()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                Ft_WhereClause = strWhereClause_ViewGroupView,
                // RQ_View_ColumnView
                Rt_Cols_View_GroupView = "*",
            };
            var listViewGroupView = View_GroupViewService.Instance.WA_View_GroupView_Get(objRQ_View_GroupView, UserState.AddressAPIs);

            if (!CUtils.IsNullOrEmpty(role))
            {
                var roleCur = role.ToUpper().Trim();
                var objView_ColumnView = new View_ColumnView()
                {
                    FlagActive = FlagActive,
                };
                var strWhereClause_View_ColumnView = strWhereClause_ViewColumnView(objView_ColumnView);
                var objRQ_View_ColumnView = new RQ_View_ColumnView()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause_View_ColumnView,
                    // RQ_View_ColumnView
                    Rt_Cols_View_ColumnView = "*",
                };
                var listViewColumnView = View_ColumnViewService.Instance.WA_View_ColumnView_Get(objRQ_View_ColumnView, UserState.AddressAPIs);

                if (listViewColumnView.Lst_View_ColumnView != null && listViewColumnView.Lst_View_ColumnView.Count > 0)
                {
                    var objView_ColumnInGroup = new View_ColumnInGroup()
                    {
                        GroupViewCode = role,
                        FlagActive = FlagActive,
                    };
                    var strWhereClause_View_ColumnInGroup = strWhereClause_ViewColumnInGroup(objView_ColumnInGroup);
                    var objRQ_View_ColumnInGroup = new RQ_View_ColumnInGroup()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStartExportExcel,
                        Ft_RecordCount = RowsWorksheets.ToString(),
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        Ft_WhereClause = strWhereClause_View_ColumnInGroup,
                        // RQ_View_ColumnInGroup
                        Rt_Cols_View_ColumnInGroup = "*",
                    };
                    var listViewColumnInGroup = View_ColumnInGroupService.Instance.WA_View_ColumnInGroup_Get(objRQ_View_ColumnInGroup, UserState.AddressAPIs);

                    if (listViewColumnInGroup.Lst_View_ColumnInGroup != null && listViewColumnInGroup.Lst_View_ColumnInGroup.Count > 0)
                    {
                        foreach (var item in listViewColumnView.Lst_View_ColumnView)
                        {
                            var checkExists = false;
                            var columnViewCode = item.ColumnViewCode.ToString().ToUpper().Trim();
                            #region[""]
                            foreach (var it in listViewColumnInGroup.Lst_View_ColumnInGroup)
                            {
                                var columnViewCodeCur = it.ColumnViewCode.ToString().ToUpper().Trim();
                                var groupViewCodeCur = it.GroupViewCode.ToString().ToUpper().Trim();
                                if (columnViewCode.Equals(columnViewCodeCur) && roleCur.Equals(groupViewCodeCur))
                                {
                                    checkExists = true;
                                    listColumnHienThi.Add(item);
                                    break;
                                }
                            }
                            #endregion
                            if (!checkExists)
                            {
                                listColumnKhongHienThi.Add(item);
                            }
                        }
                    }
                    else
                    {
                        listColumnKhongHienThi.AddRange(listViewColumnView.Lst_View_ColumnView);
                    }
                }

            }
            ViewBag.ListViewGroupView = listViewGroupView.Lst_View_GroupView;
            ViewBag.ListColumnHienThi = listColumnHienThi;
            ViewBag.ListColumnKhongHienThi = listColumnKhongHienThi;
            ViewBag.GroupViewCode = role;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult View_ColumnInGroupSave(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_View_ColumnInGroup = Newtonsoft.Json.JsonConvert.DeserializeObject<RQ_View_ColumnInGroup>(model);
                if (objRQ_View_ColumnInGroup.View_ColumnInGroup != null && !CUtils.IsNullOrEmpty(objRQ_View_ColumnInGroup.View_ColumnInGroup.GroupViewCode))
                {
                    var groupcode = objRQ_View_ColumnInGroup.View_ColumnInGroup.GroupViewCode;
                    var title = "";
                    
                    if(objRQ_View_ColumnInGroup.Lst_View_ColumnInGroup!=null && objRQ_View_ColumnInGroup.Lst_View_ColumnInGroup.Count > 0)
                    {
                        title = "Gán cột vào nhóm " + groupcode + " thành công!";
                    }
                    else
                    {
                        title = "Xóa cột khỏi nhóm " + groupcode + "thành công!";
                    }
                    // WARQBase
                    objRQ_View_ColumnInGroup.Tid = GetNextTId();
                    objRQ_View_ColumnInGroup.GwUserCode = GwUserCode;
                    objRQ_View_ColumnInGroup.GwPassword = GwPassword;
                    objRQ_View_ColumnInGroup.FuncType = null;
                    objRQ_View_ColumnInGroup.Ft_RecordStart = Ft_RecordStartExportExcel;
                    objRQ_View_ColumnInGroup.Ft_RecordCount = RowsWorksheets.ToString();
                    objRQ_View_ColumnInGroup.Ft_Cols_Upd = null;
                    objRQ_View_ColumnInGroup.WAUserCode = waUserCode;
                    objRQ_View_ColumnInGroup.WAUserPassword = waUserPassword;
                    objRQ_View_ColumnInGroup.UtcOffset = userState.UtcOffset;
                    objRQ_View_ColumnInGroup.Ft_WhereClause = null;
                    // RQ_View_ColumnView
                    objRQ_View_ColumnInGroup.Rt_Cols_View_ColumnInGroup = "*";

                    View_ColumnInGroupService.Instance.WA_View_ColumnInGroup_Save(objRQ_View_ColumnInGroup, addressAPIs);
                    //resultEntry.RedirectUrl = Url.Action("Index");
                    resultEntry.AddMessage(title);
                    resultEntry.Success = true;
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Chưa chọn nhóm Role!");
                }

                return Json(resultEntry);
            }
            catch (ServiceException e)
            {
                resultEntry.SetFailed().AddException(this, e);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region ["strWhereClause"]
        private string strWhereClause_ViewColumnView(View_ColumnView model)
        {
            var sbSql = new StringBuilder();
            string str = "";
            var Tbl_View_ColunmView = TableName.View_ColumnView + ".";

            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_View_ColunmView + TblView_ColumnView.FlagActive, model.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ColumnViewCode))
            {
                sbSql.AddWhereClause(Tbl_View_ColunmView + TblView_ColumnView.ColumnViewCode, model.ColumnViewCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ColumnViewName))
            {
                sbSql.AddWhereClause(Tbl_View_ColunmView + TblView_ColumnView.ColumnViewName, model.ColumnViewName, "=");
            }
            str = sbSql.ToString();
            return str;
        }
        private string strWhereClause_View_GroupView(View_GroupView model)
        {
            var sbSql = new StringBuilder();
            string str = "";
            var Tbl_View_GroupView = TableName.View_GroupView + ".";

            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_View_GroupView + TblView_GroupView.FlagActive, model.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.GroupViewCode))
            {
                sbSql.AddWhereClause(Tbl_View_GroupView + TblView_GroupView.GroupViewCode, model.GroupViewCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.GroupViewName))
            {
                sbSql.AddWhereClause(Tbl_View_GroupView + TblView_GroupView.GroupViewName, model.GroupViewName, "=");
            }
            str = sbSql.ToString();
            return str;
        }
        private string strWhereClause_ViewColumnInGroup(View_ColumnInGroup model)
        {
            var sbSql = new StringBuilder();
            string str = "";
            var Tbl_View_ColumnInGroup = TableName.View_ColumnInGroup + ".";

            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_View_ColumnInGroup + TblView_ColumnInGroup.FlagActive, model.FlagActive, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.ColumnViewCode))
            {
                sbSql.AddWhereClause(Tbl_View_ColumnInGroup + TblView_ColumnInGroup.ColumnViewCode, model.ColumnViewCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.GroupViewCode))
            {
                sbSql.AddWhereClause(Tbl_View_ColumnInGroup + TblView_ColumnInGroup.GroupViewCode, model.GroupViewCode, "=");
            }
            str = sbSql.ToString();
            return str;
        }
        #endregion
    }

}