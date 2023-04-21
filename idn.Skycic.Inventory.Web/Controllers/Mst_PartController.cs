using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;
using idn.Skycic.Inventory.Web.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
namespace idn.Skycic.Inventory.Web.Controllers
{
    public class Mst_PartController : AdminBaseController
    {
        // GET: Mst_Part
        public ActionResult Index(string partcode = "", string partname = "", string status = "", string init = "init", int recordcount = 10, int page = 0)
        {
            init = "search";
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var pageInfo = new PageInfo<Mst_Part>(0, recordcount)
            {
                DataList = new List<Mst_Part>()
            };
            var itemCount = 0;
            var pageCount = 0;

            #region ["Danh sách đơn vị"]
            var objMst_PartUnit = new Mst_PartUnit()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);
            var listMst_PartUnit = List_Mst_PartUnit(strWhereClause_MstPartUnit);
            ViewBag.ListMst_PartUnit = listMst_PartUnit;
            #endregion

            #region ["Danh sách nhóm sản phẩm"]
            var objMst_PartMaterialType = new Mst_PartMaterialType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClause_MstPartMaterialType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_PartMaterialType
                Rt_Cols_Mst_PartMaterialType = "*",
                Mst_PartMaterialType = null,
            };

            var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
            ViewBag.ListMst_PartMaterialType = objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType;
            #endregion

            #region ["Danh sách loại sản phẩm"]
            var objMst_PartType = new Mst_PartType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartType = strWhereClause_Mst_PartType(objMst_PartType);
            var listMst_PartType = List_Mst_PartType(strWhereClause_MstPartType);
            ViewBag.ListMst_PartType = listMst_PartType;
            #endregion
            if (init != "init")
            {
                #region["Search"]
                var objMst_Part = new Mst_Part()
                {
                    PartCode = partcode,
                    PartName = partname,
                    FlagActive = status,
                };
                var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
                var objRQ_Mst_Part = new RQ_Mst_Part()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_Cols_Upd = null,
                    Ft_WhereClause = strWhereClause_MstPart,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Part
                    Rt_Cols_Mst_Part = "*",
                };
                var objRT_Mst_Part = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPI);
                if (objRT_Mst_Part.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Mst_Part.MySummaryTable.MyCount);
                }
                if (objRT_Mst_Part != null && objRT_Mst_Part.Lst_Mst_Part != null && objRT_Mst_Part.Lst_Mst_Part.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Mst_Part.Lst_Mst_Part);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }
                #endregion
            }
            else
            {
                init = "search";
            }
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            ViewBag.PageCur = page.ToString();
            ViewBag.PartCode = partcode;
            ViewBag.PartName = partname;
            ViewBag.Status = status;

            return View(pageInfo);
        }

        #region ["Tạo mới"]
        [HttpGet]
        public ActionResult Create()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region ["Danh sách đơn vị"]
            var objMst_PartUnit = new Mst_PartUnit()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);
            var listMst_PartUnit = List_Mst_PartUnit(strWhereClause_MstPartUnit);
            ViewBag.ListMst_PartUnit = listMst_PartUnit;
            #endregion

            #region ["Danh sách nhóm sản phẩm"]
            var objMst_PartMaterialType = new Mst_PartMaterialType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClause_MstPartMaterialType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_PartMaterialType
                Rt_Cols_Mst_PartMaterialType = "*",
                Mst_PartMaterialType = null,
            };

            var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
            ViewBag.ListMst_PartMaterialType = objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType;
            #endregion

            #region ["Danh sách loại sản phẩm"]
            var objMst_PartType = new Mst_PartType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartType = strWhereClause_Mst_PartType(objMst_PartType);
            var listMst_PartType = List_Mst_PartType(strWhereClause_MstPartType);
            ViewBag.ListMst_PartType = listMst_PartType;
            #endregion

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            try
            {
                var objMst_Part = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Part>(model);

                var objRQ_Mst_Part = new RQ_Mst_Part()
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
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_Part = "*",
                    Mst_Part = objMst_Part,
                };
                Mst_PartService.Instance.WA_Mst_Part_Create(objRQ_Mst_Part, addressAPI);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới hàng hóa thành công!");
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

        #region ["Sửa"]
        [HttpGet]
        public ActionResult Update(string partcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region ["Danh sách đơn vị"]
            var objMst_PartUnit = new Mst_PartUnit()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);
            var listMst_PartUnit = List_Mst_PartUnit(strWhereClause_MstPartUnit);
            ViewBag.ListMst_PartUnit = listMst_PartUnit;
            #endregion

            #region ["Danh sách nhóm sản phẩm"]
            var objMst_PartMaterialType = new Mst_PartMaterialType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClause_MstPartMaterialType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_PartMaterialType
                Rt_Cols_Mst_PartMaterialType = "*",
                Mst_PartMaterialType = null,
            };

            var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
            ViewBag.ListMst_PartMaterialType = objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType;
            #endregion

            #region ["Danh sách loại sản phẩm"]
            var objMst_PartType = new Mst_PartType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartType = strWhereClause_Mst_PartType(objMst_PartType);
            var listMst_PartType = List_Mst_PartType(strWhereClause_MstPartType);
            ViewBag.ListMst_PartType = listMst_PartType;
            #endregion

            if (!CUtils.IsNullOrEmpty(partcode))
            {
                var objMst_Part = new Mst_Part()
                {
                    PartCode = partcode,
                };
                var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
                var objRQ_Mst_Part = new RQ_Mst_Part()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClause_MstPart,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_Part = "*",
                };
                var objRT_Mst_Part = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPI);
                if (objRT_Mst_Part.Lst_Mst_Part != null && objRT_Mst_Part.Lst_Mst_Part.Count > 0)
                {
                    return View(objRT_Mst_Part.Lst_Mst_Part[0]);
                }
            }

            return View(new Mst_Part());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            if (!CUtils.IsNullOrEmpty(model))
            {
                var objMst_PartInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Part>(model);
                var objMst_Part = new Mst_Part()
                {
                    PartCode = objMst_PartInput.PartCode,
                };

                var strWhereClauseMst_Part = strWhereClause_Mst_Part(objMst_Part);

                var objRQ_Mst_Part = new RQ_Mst_Part()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Part,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_TaxType
                    Rt_Cols_Mst_Part = "*",
                    Mst_Part = null,
                };

                var objRT_Mst_PartCur = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
                if (objRT_Mst_PartCur.Lst_Mst_Part != null && objRT_Mst_PartCur.Lst_Mst_Part.Count > 0)
                {
                    #region ["Update fields"]

                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartName = objMst_PartInput.PartName;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartNameFS = objMst_PartInput.PartNameFS;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].QtyEffSt = objMst_PartInput.QtyEffSt;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartDesc = objMst_PartInput.PartDesc;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].QtyMaxSt = objMst_PartInput.QtyMaxSt;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].QtyMinSt = objMst_PartInput.QtyMinSt;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].QtyEffMonth = objMst_PartInput.QtyEffMonth;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].RemarkForEffUsed = objMst_PartInput.RemarkForEffUsed;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].UPIn = objMst_PartInput.UPIn;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].UPOut = objMst_PartInput.UPOut;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].FilePath = objMst_PartInput.FilePath;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].ImagePath = objMst_PartInput.ImagePath;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartOrigin = objMst_PartInput.PartOrigin;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartComponents = objMst_PartInput.PartComponents;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].InstructionForUse = objMst_PartInput.InstructionForUse;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartStorage = objMst_PartInput.PartStorage;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].UrlMnfSequence = objMst_PartInput.UrlMnfSequence;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].MnfStandard = objMst_PartInput.MnfStandard;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartStyle = objMst_PartInput.PartStyle;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].PartIntroduction = objMst_PartInput.PartIntroduction;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].FlagBOM = objMst_PartInput.FlagBOM;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].FlagVirtual = objMst_PartInput.FlagVirtual;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].FlagInputLot = objMst_PartInput.FlagInputLot;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].FlagInputSerial = objMst_PartInput.FlagInputSerial;
                    objRT_Mst_PartCur.Lst_Mst_Part[0].FlagActive = objMst_PartInput.FlagActive;

                    #endregion

                    #region ["strFt_Cols_Upd"]

                    var strFt_Cols_Upd = "";
                    var Tbl_Mst_Part = TableName.Mst_Part + ".";
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartNameFS);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.QtyEffSt);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartDesc);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.QtyMaxSt);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.QtyMinSt);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.QtyEffMonth);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.RemarkForEffUsed);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.UPIn);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.UPOut);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.FilePath);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.ImagePath);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartOrigin);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartComponents);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.InstructionForUse);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartStorage);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.UrlMnfSequence);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.UrlMnfSequence);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.MnfStandard);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartStyle);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.PartIntroduction);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.FlagBOM);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.FlagVirtual);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.FlagInputLot);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.FlagInputSerial);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Part, TblMst_Part.FlagActive);

                    #endregion

                    objRQ_Mst_Part.Ft_WhereClause = null;
                    objRQ_Mst_Part.Ft_Cols_Upd = strFt_Cols_Upd;
                    objRQ_Mst_Part.Rt_Cols_Mst_Part = null;
                    objRQ_Mst_Part.Mst_Part = objRT_Mst_PartCur.Lst_Mst_Part[0];

                    Mst_PartService.Instance.WA_Mst_Part_Update(objRQ_Mst_Part, addressAPIs);

                    resultEntry.Success = true;
                    resultEntry.AddMessage("Sửa sản phẩm thành công!");
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã sản phẩm '" + objMst_PartInput.PartCode + "' không có trong hệ thống!");
                }
            }
            else
            {
                resultEntry.Success = true;
                resultEntry.AddMessage("Mã sản phẩm trống!");
            }
            return Json(resultEntry);
        }
        #endregion

        #region ["Chi tiết - Xóa"]
        [HttpGet]
        public ActionResult Detail(string partcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPI = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region ["Danh sách đơn vị"]
            var objMst_PartUnit = new Mst_PartUnit()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartUnit = strWhereClause_Mst_PartUnit(objMst_PartUnit);
            var listMst_PartUnit = List_Mst_PartUnit(strWhereClause_MstPartUnit);
            ViewBag.ListMst_PartUnit = listMst_PartUnit;
            #endregion

            #region ["Danh sách nhóm sản phẩm"]
            var objMst_PartMaterialType = new Mst_PartMaterialType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartMaterialType = strWhereClause_Mst_PartMaterialType(objMst_PartMaterialType);
            var objRQ_Mst_PartMaterialType = new RQ_Mst_PartMaterialType()
            {
                // WARQBase
                Tid = GetNextTId(),
                GwUserCode = GwUserCode,
                GwPassword = GwPassword,
                FuncType = null,
                Ft_RecordStart = Ft_RecordStartExportExcel,
                Ft_RecordCount = RowsWorksheets.ToString(),
                Ft_WhereClause = strWhereClause_MstPartMaterialType,
                Ft_Cols_Upd = null,
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_PartMaterialType
                Rt_Cols_Mst_PartMaterialType = "*",
                Mst_PartMaterialType = null,
            };

            var objRT_Mst_PartMaterialTypeCur = Mst_PartMaterialTypeService.Instance.WA_Mst_PartMaterialType_Get(objRQ_Mst_PartMaterialType, userState.AddressAPIs);
            ViewBag.ListMst_PartMaterialType = objRT_Mst_PartMaterialTypeCur.Lst_Mst_PartMaterialType;
            #endregion

            #region ["Danh sách loại sản phẩm"]
            var objMst_PartType = new Mst_PartType()
            {
                FlagActive = "1",
            };
            var strWhereClause_MstPartType = strWhereClause_Mst_PartType(objMst_PartType);
            var listMst_PartType = List_Mst_PartType(strWhereClause_MstPartType);
            ViewBag.ListMst_PartType = listMst_PartType;
            #endregion

            if (!CUtils.IsNullOrEmpty(partcode))
            {
                var objMst_Part = new Mst_Part()
                {
                    PartCode = partcode,
                };
                var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
                var objRQ_Mst_Part = new RQ_Mst_Part()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClause_MstPart,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_PartMaterialType
                    Rt_Cols_Mst_Part = "*",
                };
                var objRT_Mst_Part = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPI);
                if (objRT_Mst_Part.Lst_Mst_Part != null && objRT_Mst_Part.Lst_Mst_Part.Count > 0)
                {
                    return View(objRT_Mst_Part.Lst_Mst_Part[0]);
                }
            }

            return View(new Mst_Part());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string partcode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };

            try
            {
                if (!CUtils.IsNullOrEmpty(partcode))
                {
                    var strWhereClause = "";
                    strWhereClause = strWhereClause_Mst_Part(new Mst_Part() { PartCode = partcode.Trim().ToString() });

                    var objRQ_Mst_Part = new RQ_Mst_Part()
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
                        // RQ_Mst_Part
                        Rt_Cols_Mst_Part = "*",
                        Mst_Part = null,
                    };
                    var objRT_Mst_Part_Cur = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
                    if (objRT_Mst_Part_Cur != null && objRT_Mst_Part_Cur.Lst_Mst_Part != null && objRT_Mst_Part_Cur.Lst_Mst_Part.Count > 0)
                    {
                        objRQ_Mst_Part.Mst_Part = objRT_Mst_Part_Cur.Lst_Mst_Part[0];

                        Mst_PartService.Instance.WA_Mst_Part_Delete(objRQ_Mst_Part, addressAPIs);

                        resultEntry.Success = true;
                        resultEntry.AddMessage("Xóa sản phẩm thành công!");
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã sản phẩm '" + partcode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã sản phẩm trống!");
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

        #region ["Excel"]
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
            var list = new List<Mst_Part>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Part).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Part"));

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
        public ActionResult Export(string partcode = "", string partname = "", string status = "", string init = "init", int recordcount = 10, int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listMst_Part = new List<Mst_Part>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;


            try
            {
                #region["Search"]
                var objMst_Part = new Mst_Part()
                {
                    PartCode = partcode,
                    PartName = partname,
                    FlagActive = status,
                };
                var strWhereClause_MstPart = strWhereClause_Mst_Part(objMst_Part);
                var objRQ_Mst_Part = new RQ_Mst_Part()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStartExportExcel,
                    Ft_RecordCount = RowsWorksheets.ToString(),
                    Ft_WhereClause = strWhereClause_MstPart,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Mst_Part
                    Rt_Cols_Mst_Part = "*",
                    Mst_Part = null,
                };

                var objRT_Mst_PartCur = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
                if (objRT_Mst_PartCur != null && objRT_Mst_PartCur.Lst_Mst_Part != null)
                {
                    if (objRT_Mst_PartCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Mst_PartCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Mst_PartCur.Lst_Mst_Part != null && objRT_Mst_PartCur.Lst_Mst_Part.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listMst_Part.AddRange(objRT_Mst_PartCur.Lst_Mst_Part);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Part).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listMst_Part, dicColNames, filePath, string.Format("Mst_Part"));


                    #region["Export các trang còn lại"]
                    listMst_Part.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Mst_Part.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Mst_PartExportCur = Mst_PartService.Instance.WA_Mst_Part_Get(objRQ_Mst_Part, addressAPIs);
                            if (objRT_Mst_PartExportCur != null && objRT_Mst_PartExportCur.Lst_Mst_Part != null && objRT_Mst_PartExportCur.Lst_Mst_Part.Count > 0)
                            {
                                listMst_Part.AddRange(objRT_Mst_PartExportCur.Lst_Mst_Part);
                                ExcelExport.ExportToExcelFromList(listMst_Part, dicColNames, filePath, string.Format("Mst_Part" + i));
                                listMst_Part.Clear();
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
                var listMst_Part = new List<Mst_Part>();
                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 24)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region ["add columns"]

                        if (!table.Columns.Contains("FlagBOM"))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                                   new DataColumn("FlagBOM", typeof (string))
                                               });
                        }
                        if (!table.Columns.Contains("FlagVirtual"))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                                   new DataColumn("FlagVirtual", typeof (string))
                                               });
                        }
                        if (!table.Columns.Contains("FlagInputLot"))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                                   new DataColumn("FlagInputLot", typeof (string))
                                               });
                        }
                        if (!table.Columns.Contains("FlagInputSerial"))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                                   new DataColumn("FlagInputSerial", typeof (string))
                                               });
                        }
                        if (!table.Columns.Contains("PartBarCode"))
                        {
                            table.Columns.AddRange(new DataColumn[]{
                                                   new DataColumn("PartBarCode", typeof (string))
                                               });
                        }
                        #endregion

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

                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartCode]))
                            {
                                exitsData = "Mã sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartName]))
                            {
                                exitsData = "Tên sản phẩm tiếng việt không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            //if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartBarCode]))
                            //{
                            //    exitsData = "Mã vạch không được trống!" + strRows;
                            //    resultEntry.AddMessage(exitsData);
                            //    return Json(resultEntry);
                            //}
                            dr[TblMst_Part.PartBarCode] = dr[TblMst_Part.PartCode];
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartNameFS]))
                            {
                                exitsData = "Tên sản phẩm tiếng anh không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartType]))
                            {
                                exitsData = "Loại sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PMType]))
                            {
                                exitsData = "Nhóm sản không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartUnitCodeStd]))
                            {
                                exitsData = "Đơn vị chuẩn không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.PartUnitCodeDefault]))
                            {
                                exitsData = "Đơn vị thường dùng không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.QtyMinSt]))
                            {
                                exitsData = "Số lượng tối thiểu không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.QtyEffSt]))
                            {
                                exitsData = "Số lượng tối ưu không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.QtyMaxSt]))
                            {
                                exitsData = "Số lượng tối đa không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.UPIn]))
                            {
                                exitsData = "Giá nhập không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.UPOut]))
                            {
                                exitsData = "Giá bán không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblMst_Part.QtyEffMonth]))
                            {
                                exitsData = "Thời hạn sử dụng không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            dr[TblMst_Part.FlagBOM] = "1";
                            dr[TblMst_Part.FlagInputLot] = "0";
                            dr[TblMst_Part.FlagInputSerial] = "1";
                            dr[TblMst_Part.FlagVirtual] = "1";
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
                            var Mst_PartCur = table.Rows[i][TblMst_Part.PartCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _Mst_PartCur = table.Rows[j][TblMst_Part.PartCode].ToString().Trim();
                                    if (Mst_PartCur.Equals(_Mst_PartCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã sản phẩm '" + Mst_PartCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listMst_Part = DataTableCmUtils.ToListof<Mst_Part>(table);
                    // Gọi hàm save data
                    if (listMst_Part != null && listMst_Part.Count > 0)
                    {
                        foreach (var item in listMst_Part)
                        {
                            var objRQ_Mst_Part = new RQ_Mst_Part()
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
                                // RQ_Mst_Part
                                Rt_Cols_Mst_Part = null,
                                Mst_Part = item,
                            };

                            Mst_PartService.Instance.WA_Mst_Part_Create(objRQ_Mst_Part, addressAPIs);
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

        #region ["strWhereClause"]
        private string strWhereClause_Mst_Part(Mst_Part model)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Part = TableName.Mst_Part + ".";
            if (!CUtils.IsNullOrEmpty(model.PartCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.PartCode, "%" + model.PartCode + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(model.PartName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.PartName, "%" + model.PartName + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Part + TblMst_Part.FlagActive, model.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_PartUnit(Mst_PartUnit model)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartUnit = TableName.Mst_PartUnit + ".";
            if (!CUtils.IsNullOrEmpty(model.PartUnitCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartUnit + TblMst_PartUnit.PartUnitCode, model.PartUnitCode, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.PartUnitName))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartUnit + TblMst_PartUnit.PartUnitName, model.PartUnitName, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartUnit + TblMst_PartUnit.FlagActive, model.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_PartMaterialType(Mst_PartMaterialType model)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartMaterialType = TableName.Mst_PartMaterialType + ".";
            if (!CUtils.IsNullOrEmpty(model.PMType))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.PMType, model.PMType, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.PMTypeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.PMTypeName, model.PMTypeName, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartMaterialType + TblMst_PartMaterialType.FlagActive, model.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_PartType(Mst_PartType model)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_PartType = TableName.Mst_PartType + ".";
            if (!CUtils.IsNullOrEmpty(model.PartType))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartType + TblMst_PartType.PartType, model.PartType, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.PartTypeName))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartType + TblMst_PartType.PartTypeName, model.PartTypeName, "=");
            }
            if (!CUtils.IsNullOrEmpty(model.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_PartType + TblMst_PartType.FlagActive, model.FlagActive, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion

        #region ["GetDicColumns"]
        private Dictionary<string, string> GetImportDicColums()
        {
            return new Dictionary<string, string>()
            {
                 {"PartCode","Mã sản phẩm"},
                 {"PartName","Tên tiếng anh"},
                 {"PartNameFS","Tên tiếng việt"},
                 {"PartType","Mã loại sản phẩm"},
                 {"PMType","Mã nhóm sản phẩm"},
                 {"PartUnitCodeStd","Đơn vị chuẩn"},
                 {"PartUnitCodeDefault","Đơn vị thường dùng"},
                 {"QtyMinSt","Số lượng tối thiểu"},
                 {"QtyEffSt","Số lượng tối ưu"},
                 {"QtyMaxSt","Số lượng tối đa"},
                 {"UPIn","Giá nhập"},
                 {"UPOut","Giá bán"},
                 {"FilePath","File đính kèm"},
                 {"ImagePath","Ảnh đại diện"},
                 {"QtyEffMonth","Thời hạn sử dụng"},
                 {"PartOrigin","Xuất xứ"}, // Nguồn gốc
                 {"PartDesc","Chi tiết SP"}, // Mô tả
                 {"PartComponents","Hình ảnh SP"}, // Thành phần
                 {"InstructionForUse","HD lắp đặt"}, // Cách dùng
                 {"PartStorage","Bản vẽ"}, // Bảo quản
                 {"UrlMnfSequence","Link quy trình sản xuất"},
                 {"PartIntroduction","Giới thiệu sản phẩm"},
                 {"MnfStandard","Tiêu chuẩn"},
                 {"PartStyle","Quy cách"},
                 {"FlagActive","Trạng thái"},
            };
        }
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>()
            {
                 {"PartCode","Mã sản phẩm"},
                 //{"PartBarCode","Mã vạch"},
                 {"PartName","Tên tiếng anh"},
                 {"PartNameFS","Tên tiếng việt"},
                 {"PartType","Mã loại sản phẩm"},
                 {"PMType","Mã nhóm sản phẩm"},
                 {"PartUnitCodeStd","Đơn vị chuẩn"},
                 {"PartUnitCodeDefault","Đơn vị thường dùng"},
                 {"QtyMinSt","Số lượng tối thiểu"},
                 {"QtyEffSt","Số lượng tối ưu"},
                 {"QtyMaxSt","Số lượng tối đa"},
                 {"UPIn","Giá nhập"},
                 {"UPOut","Giá bán"},
                 {"QtyEffMonth","Thời hạn sử dụng"},
                 {"FilePath","File đính kèm"},
                 {"ImagePath","Ảnh đại diện"},
                 {"PartOrigin","Xuất xứ"}, // Nguồn gốc
                 {"PartDesc","Chi tiết SP"}, // Mô tả
                 {"PartComponents","Hình ảnh SP"}, // Thành phần
                 {"InstructionForUse","HD lắp đặt"}, // Cách dùng
                 {"PartStorage","Bản vẽ"}, // Bảo quản
                 {"UrlMnfSequence","Link quy trình sản xuất"},
                 {"PartIntroduction","Giới thiệu sản phẩm"},
                 {"MnfStandard","Tiêu chuẩn"},
                 {"PartStyle","Quy cách"},
            };
        }
        #endregion
    }
}