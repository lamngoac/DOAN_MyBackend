using idn.InBrand.Cloud.ClientService.Services;
using idn.InBrand.Cloud.Common.Models;
using idn.InBrand.Cloud.Constants;
using idn.InBrand.Cloud.Utils;
using idn.InBrand.Cloud.Web.Controllers;
using idn.InBrand.Cloud.Web.Models;
using idn.InBrand.Cloud.Web.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace idn.InBrand.Cloud.Web.Controllers
{
    public class Mst_AgentController : AdminBaseController
    {
        // GET: Mst_Agent
        public ActionResult Index(string init = "init", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var pageInfo = new PageInfo<Mst_Agent>(0, PageSizeConfig)
            {
                DataList = new List<Mst_Agent>()
            };
            var itemCount = 0;
            var pageCount = 0;
            // (không có nút tìm kiếm => load data luôn)
            init = String.Format("{0}", "search");
            if (init != "init")
            {
                #region["Search"]
                var objRQ_Mst_Agent = new RQ_Mst_Agent()
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
                    // RQ_Mst_Agent
                    Rt_Cols_Mst_Agent = "*",
                    Mst_Agent = null,
                };

                var objRT_Mst_AgentCur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);

                if (objRT_Mst_AgentCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_AgentCur.MySummaryTable.MyCount);
                }
                if (objRT_Mst_AgentCur != null && objRT_Mst_AgentCur.Lst_Mst_Agent != null && objRT_Mst_AgentCur.Lst_Mst_Agent.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_AgentCur.Lst_Mst_Agent);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "init";
            }

            pageInfo.PageSize = PageSizeConfig;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * PageSizeConfig).ToString();

            return View(pageInfo);
        }

        #region ["Tao moi"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;

            #region["Tỉnh / Thành phố"]
            var objMst_Province = new Mst_Province()
            {
                FlagActive = FlagActive,
            };
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
            var listMst_Province = List_Mst_Province(strWhereClauseMst_Province);
            ViewBag.ListMst_Province = listMst_Province;
            #endregion

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

            try
            {
                var objMst_AgentInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Agent>(model);
                //var title = "";
                var objRQ_Mst_Agent = new RQ_Mst_Agent
                {
                    FlagIsDelete = null,
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
                    // RQ_Mst_Agent
                    Rt_Cols_Mst_Agent = null,
                    Mst_Agent = objMst_AgentInput
                };
                Mst_AgentService.Instance.WA_Mst_Agent_Create(objRQ_Mst_Agent, addressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới danh mục đại lý thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
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

        #region ["Sua"]
        [HttpGet]
        public ActionResult Update(string agentcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(agentcode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_Agent(new Mst_Agent() { AgentCode = agentcode.Trim().ToString() });

                var objRQ_Mst_Agent = new RQ_Mst_Agent()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause,
                    // RQ_Mst_Agent
                    Rt_Cols_Mst_Agent = "*",
                    Mst_Agent = null,
                };
                var objRT_Mst_Agent_Cur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);
                if (objRT_Mst_Agent_Cur != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent.Count > 0)
                {
                    #region["Tỉnh / Thành phố"]
                    var objMst_Province = new Mst_Province()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                    var listMst_Province = List_Mst_Province(strWhereClauseMst_Province);
                    ViewBag.ListMst_Province = listMst_Province;
                    #endregion

                    if (!CUtils.IsNullOrEmpty(objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].AgentCode))
                    {
                        var provinceCode = CUtils.StrValueOrNull(objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].ProvinceCode);
                        var strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District()
                        {
                            ProvinceCode = provinceCode,
                            FlagActive = FlagActive,
                        });
                        var listMst_District = List_Mst_District(strWhereClauseMst_District);
                        ViewBag.ListMst_District = listMst_District;
                    }
                    return View(objRT_Mst_Agent_Cur.Lst_Mst_Agent[0]);
                }
            }

            return View(new Mst_Agent());
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

            try
            {
                if (!CUtils.IsNullOrEmpty(model))
                {
                    var objMst_AgentInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Agent>(model);

                    var objMst_Agent = new Mst_Agent()
                    {
                        AgentCode = objMst_AgentInput.AgentCode,
                    };

                    var strWhereClauseMst_Agent = strWhereClause_Mst_Agent(objMst_Agent);

                    var objRQ_Mst_Agent = new RQ_Mst_Agent
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = strWhereClauseMst_Agent,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        // RQ_Mst_CustomerNNT
                        Rt_Cols_Mst_Agent = "*",
                        Mst_Agent = null,
                    };
                    var objRT_Mst_Agent_Cur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);
                    if (objRT_Mst_Agent_Cur.Lst_Mst_Agent != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent.Count > 0)
                    {
                        objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].AgentName = objMst_AgentInput.AgentName;
                        objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].AgentAddress = objMst_AgentInput.AgentAddress;
                        objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].FlagActive = objMst_AgentInput.FlagActive;

                        #region ["strFt_Cols_Upd"]

                        var strFt_Cols_Upd = "";
                        var Tbl_Mst_Agent = TableName.Mst_Agent + ".";
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Agent, TblMst_Agent.AgentName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Agent, TblMst_Agent.AgentAddress);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Agent, TblMst_Agent.FlagActive);

                        #endregion

                        objRQ_Mst_Agent.Ft_WhereClause = null;
                        objRQ_Mst_Agent.Ft_Cols_Upd = strFt_Cols_Upd;
                        objRQ_Mst_Agent.Rt_Cols_Mst_Agent = null;
                        objRQ_Mst_Agent.Mst_Agent = objRT_Mst_Agent_Cur.Lst_Mst_Agent[0];

                        Mst_AgentService.Instance.WA_Mst_Agent_Update(objRQ_Mst_Agent, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa đại lý thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                        return Json(resultEntry);
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đại lý '" + objMst_AgentInput.AgentCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đại lý trống!");
                }

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

        #region ["Chi tiet - Xoa"]
        [HttpGet]
        public ActionResult Detail(string agentcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            if (!CUtils.IsNullOrEmpty(agentcode))
            {
                var strWhereClause = "";
                strWhereClause = strWhereClause_Mst_Agent(new Mst_Agent() { AgentCode = agentcode.Trim().ToString() });

                var objRQ_Mst_Agent = new RQ_Mst_Agent()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    Ft_WhereClause = strWhereClause,
                    // RQ_Mst_Agent
                    Rt_Cols_Mst_Agent = "*",
                    Mst_Agent = null,
                };
                var objRT_Mst_Agent_Cur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);
                if (objRT_Mst_Agent_Cur != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent.Count > 0)
                {
                    #region["Tỉnh / Thành phố"]
                    var objMst_Province = new Mst_Province()
                    {
                        FlagActive = FlagActive,
                    };
                    var strWhereClauseMst_Province = strWhereClause_Mst_Province(objMst_Province);
                    var listMst_Province = List_Mst_Province(strWhereClauseMst_Province);
                    ViewBag.ListMst_Province = listMst_Province;
                    #endregion

                    if (!CUtils.IsNullOrEmpty(objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].AgentCode))
                    {
                        var provinceCode = CUtils.StrValueOrNull(objRT_Mst_Agent_Cur.Lst_Mst_Agent[0].ProvinceCode);
                        var strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District()
                        {
                            ProvinceCode = provinceCode,
                            FlagActive = FlagActive,
                        });
                        var listMst_District = List_Mst_District(strWhereClauseMst_District);
                        ViewBag.ListMst_District = listMst_District;
                    }
                    return View(objRT_Mst_Agent_Cur.Lst_Mst_Agent[0]);
                }
            }

            return View(new Mst_Agent());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string agentcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                if (!CUtils.IsNullOrEmpty(agentcode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_Mst_Agent(new Mst_Agent() { AgentCode = agentcode.Trim().ToString() });

                    var objRQ_Mst_Agent = new RQ_Mst_Agent()
                    {
                        // WARQBase
                        Tid = GetNextTId(),
                        GwUserCode = GwUserCode,
                        GwPassword = GwPassword,
                        FuncType = null,
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_Cols_Upd = null,
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        UtcOffset = userState.UtcOffset,
                        Ft_WhereClause = strWhereClause,
                        // RQ_Mst_Agent
                        Rt_Cols_Mst_Agent = "*",
                        Mst_Agent = null,
                    };
                    var objRT_Mst_Agent_Cur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);
                    if (objRT_Mst_Agent_Cur != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent != null && objRT_Mst_Agent_Cur.Lst_Mst_Agent.Count > 0)
                    {
                        objRQ_Mst_Agent.Mst_Agent = objRT_Mst_Agent_Cur.Lst_Mst_Agent[0];

                        Mst_AgentService.Instance.WA_Mst_Agent_Delete(objRQ_Mst_Agent, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa đại lý thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã đại lý '" + agentcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã đại lý trống!");
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

        #region ["strWhereClause"]
        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.ProvinceCode, CUtils.StrValueOrNull(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Agent(Mst_Agent data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Agent = TableName.Mst_Agent + ".";
            if (!CUtils.IsNullOrEmpty(data.AgentCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Agent + TblMst_Agent.AgentCode, CUtils.StrValueOrNull(data.AgentCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Agent + TblMst_Agent.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
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
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        #region["Import excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var exitsData = "";
            try
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
                var listMst_Agent = new List<Mst_Agent>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 5)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region["Check null"]
                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";
                        foreach (DataRow dr in table.Rows)
                        {
                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Agent.AgentCode]))
                            {
                                exitsData = "Mã đại lý không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Agent.AgentName]))
                            {
                                exitsData = "Tên đại lý không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Agent.ProvinceCode]))
                            {
                                exitsData = "Mã Tỉnh/Thành không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Agent.DistrictCode]))
                            {
                                exitsData = "Mã Quận/Huyện không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            idx++;
                        }
                        #endregion

                        #region["Check duplicate"]
                        iRows = 2;
                        strRows = " - Dòng ";
                        var jRows = 2;
                        for (var i = 0; i < table.Rows.Count; i++)
                        {
                            iRows = 2;
                            iRows = (iRows + i + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;
                            var Mst_AgentCur = table.Rows[i][TblMst_Agent.AgentCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_AgentCur = table.Rows[j][TblMst_Agent.AgentCode].ToString().Trim();
                                    if (Mst_AgentCur.Equals(_Mst_AgentCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã đại lý '" + Mst_AgentCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Agent = DataTableCmUtils.ToListof<Mst_Agent>(table);
                    // Gọi hàm save data
                    if (listMst_Agent != null && listMst_Agent.Count > 0)
                    {
                        foreach (var item in listMst_Agent)
                        {
                            var objRQ_Mst_Agent = new RQ_Mst_Agent()
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
                                // RQ_Mst_Agent
                                Rt_Cols_Mst_Agent = null,
                                Mst_Agent = item,
                            };

                            Mst_AgentService.Instance.WA_Mst_Agent_Create(objRQ_Mst_Agent, addressAPIs);
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
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_Agent>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Agent).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Agent"));

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
        public ActionResult Export()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Agent = new List<Mst_Agent>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objRQ_Mst_Agent = new RQ_Mst_Agent()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Agent
                    Rt_Cols_Mst_Agent = "*",
                    Mst_Agent = null,
                };

                var objRT_Mst_AgentCur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);
                if (objRT_Mst_AgentCur != null && objRT_Mst_AgentCur.Lst_Mst_Agent != null)
                {
                    if (objRT_Mst_AgentCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_AgentCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_AgentCur.Lst_Mst_Agent != null && objRT_Mst_AgentCur.Lst_Mst_Agent.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Agent.AddRange(objRT_Mst_AgentCur.Lst_Mst_Agent);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Agent).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Agent, dicColNames, filePath, string.Format("Mst_Agent"));


                    #region["Export các trang còn lại"]
                    listMst_Agent.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Agent.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_AgentExportCur = Mst_AgentService.Instance.WA_Mst_Agent_Get(objRQ_Mst_Agent, addressAPIs);
                            if (objRT_Mst_AgentExportCur != null && objRT_Mst_AgentExportCur.Lst_Mst_Agent != null && objRT_Mst_AgentExportCur.Lst_Mst_Agent.Count > 0)
                            {
                                listMst_Agent.AddRange(objRT_Mst_AgentExportCur.Lst_Mst_Agent);
                                ExcelExport.ExportToExcelFromList(listMst_Agent, dicColNames, filePath, string.Format("Mst_Agent" + i));
                                listMst_Agent.Clear();
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

        #region ["GetDicColumns"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Agent.AgentCode,"Mã đại lý (*)"},
                {TblMst_Agent.AgentName,"Tên đại lý (*)"},
                {TblMst_Agent.ProvinceCode,"Mã Tỉnh/Thành (*)"},
                {TblMst_Agent.DistrictCode,"Mã Quận/Huyện (*)"},
                {TblMst_Agent.AgentAddress,"Địa chỉ đại lý"},
            };
        }

        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                {TblMst_Agent.AgentCode,"Mã đại lý (*)"},
                {TblMst_Agent.AgentName,"Tên đại lý (*)"},
                {TblMst_Agent.ProvinceCode,"Mã Tỉnh/Thành (*)"},
                {TblMst_Agent.DistrictCode,"Mã Quận/Huyện (*)"},
                {TblMst_Agent.AgentAddress,"Địa chỉ đại lý"},
                {TblMst_Agent.FlagActive,"Trạng thái"},
            };
        }
        #endregion
    }
}