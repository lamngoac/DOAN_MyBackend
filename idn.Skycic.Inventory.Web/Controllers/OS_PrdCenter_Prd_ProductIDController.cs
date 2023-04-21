using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using ProductCentrer = idn.Skycic.Inventory.Common.Models.ProductCentrer;
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
    public class OS_PrdCenter_Prd_ProductIDController : AdminBaseController
    {
        // GET: OS_PrdCenter_PrdProductID
        public ActionResult Index(int page = 0, int recordcount = 10, string init = "init", string ProductID = "", string SpecCode = "", string ms_SpecName = "", string Buyer = "", string Brand = "", string ModelCode = "", string OrgID = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var userState = this.UserState;
                Session["UserState"] = userState;
                var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
                var waUserCode = userState.SysUser.UserCode;
                var waUserPassword = userState.SysUser.UserPassword;

                var waOrgID = userState.Mst_NNT.OrgID;
                var waNetworkID = Session["networkid"];
                ViewBag.WaOrgID = waOrgID;

                ViewBag.lstNNT = get_MstNNT();

                var isparent = false;
                if (waNetworkID.ToString() == waOrgID.ToString())
                {
                    isparent = true;
                }

                ViewBag.IsParent = isparent;

                #region CustomField
                var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
                var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
                {
                    Tid = GetNextTId(),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    Ft_RecordStart = Ft_RecordStart,//Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,//Ft_RecordCount,
                    Rt_Cols_Prd_PrdIDCustomField = "*"
                };
                var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
                if (objRT_Prd_PrdIDCustomField != null)
                    if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null)
                    {
                        listPrd_PrdIDCustomField = objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField;
                    }
                ViewBag.lstPrdIDCustomField = listPrd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
                #endregion

                var pageInfo = new PageInfo<ProductCentrer.Prd_ProductID>(0, recordcount)
                {
                    DataList = new List<ProductCentrer.Prd_ProductID>()
                };
                var itemCount = 0;
                var pageCount = 0;

                var objPrd_ProductID = new ProductCentrer.Prd_ProductID()
                {
                    OrgID = OrgID,
                    ProductID = ProductID,
                    SpecCode = SpecCode,
                    ms_SpecName = ms_SpecName,
                    Buyer = Buyer,
                    mb_BrandCode = Brand,
                    mm_ModelCode = ModelCode,
                };

                var strWhereClauseOS_PrdCenter_Mst_Spec = strWhereClauseProductID(objPrd_ProductID);

                var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_Spec,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Prd_ProductID
                    Rt_Cols_Prd_ProductID = "*",
                    Prd_ProductID = null
                };

                var objRT_Prd_ProductIDCur = OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Get(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
                if (objRT_Prd_ProductIDCur.MySummaryTable != null)
                {
                    itemCount = Convert.ToInt32(objRT_Prd_ProductIDCur.MySummaryTable.MyCount);
                }
                if (objRT_Prd_ProductIDCur != null && objRT_Prd_ProductIDCur.Lst_Prd_ProductID != null && objRT_Prd_ProductIDCur.Lst_Prd_ProductID.Count > 0)
                {
                    pageInfo.DataList.AddRange(objRT_Prd_ProductIDCur.Lst_Prd_ProductID);
                    pageCount = (itemCount % PageSizeConfig == 0) ? itemCount / PageSizeConfig : itemCount / PageSizeConfig + 1;
                }

                // Các thông tin tìm kiếm
                //
                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();
                return View(pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
                return Json(resultEntry, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult Create()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            try
            {
                #region CustomField

                var objPrd_PrdIDCustomField = new ProductCentrer.Prd_PrdIDCustomField()
                {
                    FlagActive = FlagActive,
                    //OrgID = userState.Mst_NNT.OrgID,
                };
                var strWhereClausePrd_PrdIDCustomField = strWhereClause_Prd_PrdIDCustomField(objPrd_PrdIDCustomField);
                var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
                var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
                {
                    Tid = GetNextTId(),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClausePrd_PrdIDCustomField,
                    Rt_Cols_Prd_PrdIDCustomField = "*"
                };
                var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
                if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null &&
                    objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Count > 0)
                {
                    listPrd_PrdIDCustomField = objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField;
                }
                ViewBag.lstPrdIDCustomField = listPrd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
                #endregion
                #region Spec
                var lst_MstSpec = new List<ProductCentrer.Mst_Spec>();

                var strWhere = strWhereRQ_Mst_Spec(new ProductCentrer.Mst_Spec() { FlagActive = FlagActive, OrgID = waOrgID }, null);
                var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                {
                    Tid = GetNextTId(),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    Ft_WhereClause = strWhere,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Rt_Cols_Mst_Spec = "*"
                };
                var objRT_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                if (objRT_Mst_Spec.Lst_Mst_Spec != null && objRT_Mst_Spec.Lst_Mst_Spec.Count > 0)
                {
                    lst_MstSpec = objRT_Mst_Spec.Lst_Mst_Spec;
                }
                ViewBag.lst_MstSpec = lst_MstSpec;
                #endregion

                ViewBag.OrgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
                return View();
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }

        [HttpPost]
        public ActionResult Create(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            var prd_ProductID = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Prd_ProductID>(model);

            if (isparent == false)
            {
                prd_ProductID.NetworkProductIDCode = null;
            }

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var rq = new ProductCentrer.RQ_Prd_ProductID()
                {
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    Prd_ProductID = prd_ProductID
                };
                OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Create(rq, addressAPIs_ProductCentrer);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo sản phẩm thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }
        public ActionResult Detail(string productID, string specCode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }
            ViewBag.UserCode = userState.SysUser.UserCode;
            ViewBag.IsParent = isparent;

            #region CustomField
            var objPrd_PrdIDCustomField = new ProductCentrer.Prd_PrdIDCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereOS_PrdCenter_Prd_PrdIDCustomField = strWhere_Prd_PrdIDCustomField(objPrd_PrdIDCustomField);

            var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
            var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_WhereClause = strWhereOS_PrdCenter_Prd_PrdIDCustomField,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Prd_PrdIDCustomField = "*"
            };
            var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
            if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null &&
                objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Count > 0)
            {
                listPrd_PrdIDCustomField = objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField;
            }
            ViewBag.lstPrdIDCustomField = listPrd_PrdIDCustomField;
            #endregion
            #region Spec
            var lst_MstSpec = new List<ProductCentrer.Mst_Spec>();
            var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Mst_Spec = "*"
            };
            var objRT_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
            if (objRT_Mst_Spec.Lst_Mst_Spec != null && objRT_Mst_Spec.Lst_Mst_Spec.Count > 0)
            {
                lst_MstSpec = objRT_Mst_Spec.Lst_Mst_Spec;
            }
            ViewBag.lst_MstSpec = lst_MstSpec;
            #endregion
            var prd_ProductID = new ProductCentrer.Prd_ProductID();
            var strWhere = strWhereClauseProductID(new ProductCentrer.Prd_ProductID() { ProductID = productID, SpecCode = specCode });
            var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
            {
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhere,
                Rt_Cols_Prd_ProductID = "*"
            };
            var objRT_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Get(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
            if (objRT_Prd_ProductID.Lst_Prd_ProductID != null && objRT_Prd_ProductID.Lst_Prd_ProductID.Count > 0)
            {
                prd_ProductID = objRT_Prd_ProductID.Lst_Prd_ProductID[0];
            }
            var lstImg = new List<ProductCentrer.Mst_SpecImage>();
            var strWhereSpec = strWhereRQ_Mst_Spec(new ProductCentrer.Mst_Spec() { SpecCode = specCode }, null);
            var rqImg = new ProductCentrer.RQ_Mst_Spec()
            {
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Mst_SpecImage = "*",
                Ft_WhereClause = strWhereSpec
            };
            var rtImg = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(rqImg, addressAPIs_ProductCentrer);
            if (rtImg != null) if (rtImg.Lst_Mst_SpecImage != null) lstImg = rtImg.Lst_Mst_SpecImage;
            
            //            
            var host = WA_OS_GetFileFromPath(GetNextTId(), waUserCode, waUserPassword);
            // Gán đường dẫn tuyệt đối
            foreach (var it in lstImg)
            {
                it.SpecImagePath = host + "/" + it.SpecImagePath;
            }
            //

            ViewBag.lstSpecImage = lstImg;
            return View(prd_ProductID);
        }
        [HttpGet]
        public ActionResult Update(string productID, string specCode)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            ViewBag.WaOrgID = waOrgID;

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            ViewBag.IsParent = isparent;

            ViewBag.UserCode = userState.SysUser.UserCode;

            #region CustomField
            var objPrd_PrdIDCustomField = new ProductCentrer.Prd_PrdIDCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereOS_PrdCenter_Prd_PrdIDCustomField = strWhere_Prd_PrdIDCustomField(objPrd_PrdIDCustomField);

            var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
            var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_WhereClause = strWhereOS_PrdCenter_Prd_PrdIDCustomField,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Prd_PrdIDCustomField = "*"
            };
            var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
            if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null &&
                objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Count > 0)
            {
                listPrd_PrdIDCustomField = objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField;
            }
            ViewBag.lstPrdIDCustomField = listPrd_PrdIDCustomField;// listPrd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
            #endregion
            #region Spec
            var lst_MstSpec = new List<ProductCentrer.Mst_Spec>();
            var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Mst_Spec = "*"
            };
            var objRT_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
            if (objRT_Mst_Spec.Lst_Mst_Spec != null && objRT_Mst_Spec.Lst_Mst_Spec.Count > 0)
            {
                lst_MstSpec = objRT_Mst_Spec.Lst_Mst_Spec;
            }
            ViewBag.lst_MstSpec = lst_MstSpec;
            #endregion
            var prd_ProductID = new ProductCentrer.Prd_ProductID();
            var strWhere = strWhereClauseProductID(new ProductCentrer.Prd_ProductID() { ProductID = productID, SpecCode = specCode });
            var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
            {
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhere,
                Rt_Cols_Prd_ProductID = "*"
            };
            var objRT_Prd_ProductID = OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Get(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
            if (objRT_Prd_ProductID.Lst_Prd_ProductID != null && objRT_Prd_ProductID.Lst_Prd_ProductID.Count > 0)
            {
                prd_ProductID = objRT_Prd_ProductID.Lst_Prd_ProductID[0];
            }
            return View(prd_ProductID);
        }
        [HttpPost]
        public ActionResult Update(string model)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductCentrer.Prd_ProductID>(model);
                var strFt_Cols_Upd = "";
                var Tbl_Prd_ProductID = TableName.Prd_ProductID + ".";
                
                if (isparent && ! waOrgID.Equals(data.OrgID))
                {
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.NetworkProductIDCode);
                }
                else
                {
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.SpecCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.ms_SpecName);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.PrdCustomFieldCode);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.ProductionDate);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.LOTNo);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.BuyDate);

                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.SecretNo);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.WarrantyStartDate);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.WarrantyExpiredDate);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.WarrantyDuration);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.RefNo1);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.RefBiz1);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.RefNo2);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.RefBiz2);

                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.RefNo3);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.RefBiz3);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.Buyer);
                    strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.ProductIDStatus);

                    #region["Prd_PrdIDCustomField"]
                    var objPrd_PrdIDCustomField = new ProductCentrer.Prd_PrdIDCustomField()
                    {
                        FlagActive = FlagActive,
                    };

                    var strWhereOS_PrdCenter_Prd_PrdIDCustomField = strWhere_Prd_PrdIDCustomField(objPrd_PrdIDCustomField);

                    var list_Prd_PrdIDCustomField = List_Prd_PrdIDCustomField(strWhereOS_PrdCenter_Prd_PrdIDCustomField);
                    if (list_Prd_PrdIDCustomField != null &&
                        list_Prd_PrdIDCustomField.Count > 0)
                    {
                        foreach (var it in list_Prd_PrdIDCustomField)
                        {
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, it.PrdCustomFieldCode);
                        }
                    }
                    #endregion
                }

                var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
                {
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    Prd_ProductID = data,
                    Ft_Cols_Upd = strFt_Cols_Upd
                };
                OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Update(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
                resultEntry.Success = true;
                resultEntry.AddMessage("Cập nhật sản phẩm thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }

        public ActionResult Delete(string productID, string specCode, string orgID)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
                {
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    Prd_ProductID = new ProductCentrer.Prd_ProductID()
                    {
                        ProductID = productID,
                        SpecCode = specCode,
                        OrgID = orgID
                    }
                    //,Ft_Cols_Upd = 
                };
                OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Delete(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa sản phẩm thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            return Json(resultEntry);
        }

        private List<Mst_NNT> get_MstNNT()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            List<Mst_NNT> lstMst_NNT = new List<Mst_NNT>();
            var org = userState.Mst_NNT.OrgID;
            Mst_NNT objNNT = new Mst_NNT()
            {                
                OrgID = org               
            };
            var strmst_NNT = strWhereClause_Mst_NNT(objNNT);           
            var rqNNT = new RQ_Mst_NNT
            {
                Tid = GetNextTId(),
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = "Mst_NNT.FlagActive = 1 AND Mst_NNT.OrgID != 0",
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                UtcOffset = userState.UtcOffset,
                // RQ_Mst_NNT
                Rt_Cols_Mst_NNT = "*",
                Mst_NNT = null,
            };
            //var rtNNT = Mst_NNTService.Instance.WA_Mst_NNT_Get(rqNNT, addressAPIs);
            //if (rtNNT != null) if (rtNNT.Lst_Mst_NNT != null && rtNNT.Lst_Mst_NNT.Count != 0)
            //    {
            //        lstMst_NNT = rtNNT.Lst_Mst_NNT;
            //    }
            return lstMst_NNT;
        }

        #region["Import excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            var exitsData = "";

            #region ["CustomField"]
            var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
            var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Prd_PrdIDCustomField = "*"
            };
            var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
            if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null &&
                objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Count > 0)
            {
                listPrd_PrdIDCustomField = objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
            }
            int count = listPrd_PrdIDCustomField.Count + 15;
            #endregion

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
                var listProductID = new List<ProductCentrer.Prd_ProductID>();
                var table = ExcelImportNew.Query(filePath, "A2");


                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != count)
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

                            if (CUtils.IsNullOrEmpty(dr[TblPrd_ProductID.ProductID]))
                            {
                                exitsData = "Mã sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblPrd_ProductID.SpecCode]))
                            {
                                exitsData = "Mã Spec không được trống!" + strRows;
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
                            var mst_ProductIDCur = table.Rows[i][TblPrd_ProductID.ProductID].ToString().Trim();
                            var mst_SpecCodeCur = table.Rows[i][TblPrd_ProductID.SpecCode].ToString().Trim();
                            for (var j = 0; j < table.Rows.Count; j++)
                            {
                                jRows = 2;
                                jRows = (jRows + j + 1);
                                if (i != j)
                                {
                                    var _mst_ProductIDCur = table.Rows[j][TblPrd_ProductID.ProductID].ToString().Trim();
                                    var _mst_SpecCodeCur = table.Rows[j][TblPrd_ProductID.SpecCode].ToString().Trim();
                                    if (mst_ProductIDCur.Equals(_mst_ProductIDCur) && mst_SpecCodeCur.Equals(_mst_SpecCodeCur))
                                    {
                                        strRows += (" - " + jRows);
                                        exitsData = "Mã sản phẩm '" + mst_ProductIDCur + "' - mã Spec '" + mst_SpecCodeCur + "' bị lặp trong file excel!" + strRows;
                                        resultEntry.AddMessage(exitsData);
                                        return Json(resultEntry);
                                    }
                                }
                            }
                        }
                        #endregion
                    }
                    listProductID = DataTableCmUtils.ToListof<ProductCentrer.Prd_ProductID>(table);

                    // Lấy danh sách Spec
                    var listSpec = new List<ProductCentrer.Mst_Spec>();
                    var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                    {
                        Tid = GetNextTId(),
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = "",
                        Rt_Cols_Mst_Spec = "*"
                    };
                    var objRT_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                    if (objRT_Mst_Spec.Lst_Mst_Spec != null && objRT_Mst_Spec.Lst_Mst_Spec.Count > 0)
                    {
                        listSpec = objRT_Mst_Spec.Lst_Mst_Spec;
                    }
                    //

                    // Gọi hàm save data
                    if (listProductID != null && listProductID.Count > 0)
                    {
                        foreach (var item in listProductID)
                        {
                            // Lấy thêm tên spec
                            var itSpec = listSpec.Where(x => x.SpecCode.Equals(item.SpecCode)).FirstOrDefault();
                            if (itSpec != null)
                            {
                                item.ms_SpecName = itSpec.SpecName;
                            }
                            //
                            // Mặc định trạng thái
                            item.ProductIDStatus = "OK";
                            //

                            // Lấy thời hạn bảo hành
                            var ngaybdBH = Convert.ToDateTime(item.WarrantyStartDate);
                            var ngayktBH = Convert.ToDateTime(item.WarrantyExpiredDate);
                            var thoigian = ngayktBH - ngaybdBH;
                            item.WarrantyDuration = Math.Round((thoigian.TotalDays) / 30);
                            //
                            item.OrgID = orgID;
                            var objRQ_ProductID = new ProductCentrer.RQ_Prd_ProductID()
                            {
                                // WARQBase
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                FuncType = null,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = null,
                                Ft_Cols_Upd = null,
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                Prd_ProductID = item
                            };
                            OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Create(objRQ_ProductID, addressAPIs_ProductCentrer);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ImportMapping(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var orgID = userState.Mst_NNT.OrgID == null ? "" : userState.Mst_NNT.OrgID;
            var exitsData = "";

            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];

            var isparent = false;
            if (waNetworkID.ToString() == waOrgID.ToString())
            {
                isparent = true;
            }

            #region CustomField
            //var listPrd_PrdIDCustomField = new List<OS_PrdCenter_Prd_PrdIDCustomField>();
            //var rq = new ProductCentrer.RQ_Prd_PrdIDCustomField()
            //{
            //    Tid = GetNextTId(),
            //    WAUserCode = waUserCode,
            //    WAUserPassword = waUserPassword,
            //    GwUserCode = GwUserCode,
            //    GwPassword = GwPassword,
            //    Ft_RecordStart = Ft_RecordStart,
            //    Ft_RecordCount = Ft_RecordCount,
            //    Rt_Cols_Prd_PrdIDCustomField = "*"
            //};
            //var rt = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(rq);
            //if (rt != null) if (rt.Lst_Prd_PrdIDCustomField != null) listPrd_PrdIDCustomField = rt.Lst_Prd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
            //int count = listPrd_PrdIDCustomField.Count + 17;
            #endregion

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
                var listProductID = new List<ProductCentrer.Prd_ProductID>();
                var table = ExcelImportNew.Query(filePath, "A2");

                var count = 4;
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != count)
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

                            if (CUtils.IsNullOrEmpty(dr[TblPrd_ProductID.ProductID]))
                            {
                                exitsData = "Mã sản phẩm không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblPrd_ProductID.SpecCode]))
                            {
                                exitsData = "Mã spec không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr[TblPrd_ProductID.OrgID]))
                            {
                                exitsData = "Mã tổ chức không được trống!" + strRows;
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            else
                            {
                                var orgId = CUtils.StrValueOrNull(dr[TblPrd_ProductID.OrgID]);
                                if (orgId.Equals(waNetworkID))
                                {
                                    exitsData = "Không được mapping mã sản phẩm chung cho đơn vị tổng!" + strRows;
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            idx++;
                        }
                        #endregion
                    }
                    listProductID = DataTableCmUtils.ToListof<ProductCentrer.Prd_ProductID>(table);

                    // Lấy danh sách Spec
                    var listSpec = new List<ProductCentrer.Mst_Spec>();
                    var objRQ_Mst_Spec = new ProductCentrer.RQ_Mst_Spec()
                    {
                        Tid = GetNextTId(),
                        WAUserCode = waUserCode,
                        WAUserPassword = waUserPassword,
                        GwUserCode = OS_ProductCentrer_GwUserCode,
                        GwPassword = OS_ProductCentrer_GwPassword,
                        OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                        Ft_RecordStart = Ft_RecordStart,
                        Ft_RecordCount = Ft_RecordCount,
                        Ft_WhereClause = "",
                        Rt_Cols_Mst_Spec = "*"
                    };
                    var objRT_Mst_Spec = OS_PrdCenter_Mst_SpecService.Instance.WA_Mst_Spec_Get(objRQ_Mst_Spec, addressAPIs_ProductCentrer);
                    if (objRT_Mst_Spec.Lst_Mst_Spec != null && objRT_Mst_Spec.Lst_Mst_Spec.Count > 0)
                    {
                        listSpec = objRT_Mst_Spec.Lst_Mst_Spec;
                    }
                    //

                    // Gọi hàm save data
                    if (listProductID != null && listProductID.Count > 0)
                    {
                        foreach (var item in listProductID)
                        {
                            var strFt_Cols_Upd = "";
                            var Tbl_Prd_ProductID = TableName.Prd_ProductID + ".";
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.OrgID);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.ProductID);
                            strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.SpecCode);
                            if (isparent)
                            {
                                strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Prd_ProductID, TblPrd_ProductID.NetworkProductIDCode);
                            }


                            var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
                            {
                                Tid = GetNextTId(),
                                GwUserCode = OS_ProductCentrer_GwUserCode,
                                GwPassword = OS_ProductCentrer_GwPassword,
                                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                                WAUserCode = waUserCode,
                                WAUserPassword = waUserPassword,
                                Ft_RecordStart = Ft_RecordStart,
                                Ft_RecordCount = Ft_RecordCount,
                                Ft_WhereClause = "",
                                Prd_ProductID = item,
                                Ft_Cols_Upd = strFt_Cols_Upd
                            };
                            OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Update(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
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

        #region["DicColums"]
        private Dictionary<string, string> GetImportDicColums()
        {
            var userState = this.UserState;
            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region CustomField
            #region["Prd_PrdIDCustomField"]
            var objPrd_PrdIDCustomField = new ProductCentrer.Prd_PrdIDCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereOS_PrdCenter_Prd_PrdIDCustomField = strWhere_Prd_PrdIDCustomField(objPrd_PrdIDCustomField);

            var list_Prd_PrdIDCustomField = List_Prd_PrdIDCustomField(strWhereOS_PrdCenter_Prd_PrdIDCustomField);
            ViewBag.List_Prd_PrdIDCustomField = list_Prd_PrdIDCustomField;
            #endregion
            #endregion

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {TblPrd_ProductID.OrgID,"Mã tổ chức (*)"},
                {TblPrd_ProductID.ProductID,"Mã sản phẩm (*)"},
                {TblPrd_ProductID.SpecCode,"Mã Spec (*)"},
            };
            if (waOrgID.Equals(waNetworkID))
            {
                dictionary.Add(TblPrd_ProductID.NetworkProductIDCode, "Mã sản phẩm chung");
            }
            dictionary.Add(TblPrd_ProductID.ProductionDate, "Ngày sản xuất");
            dictionary.Add(TblPrd_ProductID.LOTNo, "Số Lot");
            dictionary.Add(TblPrd_ProductID.BuyDate, "Ngày mua hàng");
            dictionary.Add(TblPrd_ProductID.SecretNo, "Mã bí mật");
            dictionary.Add(TblPrd_ProductID.WarrantyStartDate, "Ngày bắt đầu BH");
            dictionary.Add(TblPrd_ProductID.WarrantyExpiredDate, "Ngày kết thúc BH");
            dictionary.Add(TblPrd_ProductID.RefNo1, "Số tham chiếu 1");
            dictionary.Add(TblPrd_ProductID.RefBiz1, "Loại tham chiếu 1");
            dictionary.Add(TblPrd_ProductID.RefNo2, "Số tham chiếu 2");
            dictionary.Add(TblPrd_ProductID.RefBiz2, "Loại tham chiếu 2");
            dictionary.Add(TblPrd_ProductID.RefNo3, "Số tham chiếu 3");
            dictionary.Add(TblPrd_ProductID.RefBiz3, "Loại tham chiếu 3");
            dictionary.Add(TblPrd_ProductID.Buyer, "Khách hàng");

            foreach (var item in list_Prd_PrdIDCustomField)
            {
                dictionary.Add(item.PrdCustomFieldCode.ToString(), item.PrdCustomFieldName.ToString());
            }

            return dictionary;
        }

        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;

            #region CustomField
            var listPrd_PrdIDCustomField = new List<ProductCentrer.Prd_PrdIDCustomField>();
            var objRQ_Prd_PrdIDCustomField = new ProductCentrer.RQ_Prd_PrdIDCustomField()
            {
                Tid = GetNextTId(),
                WAUserCode = waUserCode,
                WAUserPassword = waUserPassword,
                GwUserCode = OS_ProductCentrer_GwUserCode,
                GwPassword = OS_ProductCentrer_GwPassword,
                OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Rt_Cols_Prd_PrdIDCustomField = "*"
            };
            var objRT_Prd_PrdIDCustomField = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(objRQ_Prd_PrdIDCustomField, addressAPIs_ProductCentrer);
            if (objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField != null &&
                objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Count > 0)
            {
                listPrd_PrdIDCustomField = objRT_Prd_PrdIDCustomField.Lst_Prd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
            }
            #endregion

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {TblPrd_ProductID.ProductID,"Mã sản phẩm (*)"},
                {TblPrd_ProductID.SpecCode,"Mã Spec (*)"},
                //{TblPrd_ProductID.ms_SpecName,"Tên Spec"},
                //{TblPrd_ProductID.PrdCustomFieldCode, "Trường tùy chỉnh sản phẩm" },
                {TblPrd_ProductID.ProductionDate, "Ngày sản xuất"},
                {TblPrd_ProductID.LOTNo , "Số Lot"},
                {TblPrd_ProductID.BuyDate , "Ngày mua hàng"},
                {TblPrd_ProductID.SecretNo , "Mã bí mật"},
                {TblPrd_ProductID.WarrantyStartDate , "Ngày bắt đầu BH"},
                {TblPrd_ProductID.WarrantyExpiredDate , "Ngày kết thúc BH"},
                //{TblPrd_ProductID.WarrantyDuration , "Thời hạn BH"},
                {TblPrd_ProductID.RefNo1 , "Số tham chiếu 1"},
                {TblPrd_ProductID.RefBiz1 , "Loại tham chiếu 1"},
                {TblPrd_ProductID.RefNo2 , "Số tham chiếu 2"},
                {TblPrd_ProductID.RefBiz2 , "Loại tham chiếu 2"},
                {TblPrd_ProductID.RefNo3 , "Số tham chiếu 3"},
                {TblPrd_ProductID.RefBiz3 , "Loại tham chiếu 3"},
                {TblPrd_ProductID.Buyer , "Khách hàng"}
            };

            foreach (var item in listPrd_PrdIDCustomField)
            {
                dictionary.Add(item.PrdCustomFieldCode.ToString(), item.PrdCustomFieldName.ToString());
            }

            return dictionary;
        }

        private Dictionary<string, string> GetImportDicColumsTemplateMapping()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var waOrgID = userState.Mst_NNT.OrgID;
            var waNetworkID = Session["networkid"];
            #region CustomField


            //var listPrd_PrdIDCustomField = new List<OS_PrdCenter_Prd_PrdIDCustomField>();
            //var rq = new ProductCentrer.RQ_Prd_PrdIDCustomField()
            //{
            //    Tid = GetNextTId(),
            //    WAUserCode = waUserCode,
            //    WAUserPassword = waUserPassword,
            //    GwUserCode = GwUserCode,
            //    GwPassword = GwPassword,
            //    Ft_RecordStart = Ft_RecordStart,
            //    Ft_RecordCount = Ft_RecordCount,
            //    Rt_Cols_Prd_PrdIDCustomField = "*"
            //};
            //var rt = OS_PrdCenter_Prd_PrdIDCustomFieldService.Instance.WA_Prd_PrdIDCustomField_Get(rq);
            //if (rt != null) if (rt.Lst_Prd_PrdIDCustomField != null) listPrd_PrdIDCustomField = rt.Lst_Prd_PrdIDCustomField.Where(x => x.FlagActive.ToString() != FlagInActive).ToList();
            #endregion

            //Dictionary<string, string> dictionary = new Dictionary<string, string>()
            //{
            //    {TblPrd_ProductID.OrgID,"Mã tổ chức (*)"},
            //    {TblPrd_ProductID.ProductID,"Mã sản phẩm (*)"}
            //{TblPrd_ProductID.NetworkProductIDCode,"Mã sản phẩm chung"},
            //{TblPrd_ProductID.SpecCode,"Mã Spec (*)"},
            ////{TblPrd_ProductID.ms_SpecName,"Tên Spec"},
            ////{TblPrd_ProductID.PrdCustomFieldCode, "Trường tùy chỉnh sản phẩm" },
            //{TblPrd_ProductID.ProductionDate, "Ngày sản xuất"},
            //{TblPrd_ProductID.LOTNo , "Số Lot"},
            //{TblPrd_ProductID.BuyDate , "Ngày mua hàng"},
            //{TblPrd_ProductID.SecretNo , "Mã bí mật"},
            //{TblPrd_ProductID.WarrantyStartDate , "Ngày bắt đầu BH"},
            //{TblPrd_ProductID.WarrantyExpiredDate , "Ngày kết thúc BH"},
            ////{TblPrd_ProductID.WarrantyDuration , "Thời hạn BH"},
            //{TblPrd_ProductID.RefNo1 , "Số tham chiếu 1"},
            //{TblPrd_ProductID.RefBiz1 , "Loại tham chiếu 1"},
            //{TblPrd_ProductID.RefNo2 , "Số tham chiếu 2"},
            //{TblPrd_ProductID.RefBiz2 , "Loại tham chiếu 2"},
            //{TblPrd_ProductID.RefNo3 , "Số tham chiếu 3"},
            //{TblPrd_ProductID.RefBiz3 , "Loại tham chiếu 3"},
            //{TblPrd_ProductID.Buyer , "Khách hàng"}
            //};

            //foreach (var item in listPrd_PrdIDCustomField)
            //{
            //    dictionary.Add(item.PrdCustomFieldCode.ToString(), item.PrdCustomFieldName.ToString());
            //}

            Dictionary<string, string> dictionary = new Dictionary<string, string>()
            {
                {TblPrd_ProductID.OrgID,"Mã tổ chức (*)"},
                {TblPrd_ProductID.ProductID,"Mã sản phẩm (*)"},
                {TblPrd_ProductID.NetworkProductIDCode,"Mã sản phẩm chung"},
                {TblPrd_ProductID.SpecCode,"Mã spec"},
            };
            //if(waOrgID.Equals(waNetworkID))
            //{
            //    dictionary.Add(TblPrd_ProductID.NetworkProductIDCode, "Mã sản phẩm chung");
            //}
            return dictionary;
        }
        #endregion

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_Country>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Prd_ProductID).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Prd_ProductID"));

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
        public ActionResult ExportTemplateMapping()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var list = new List<Mst_Country>();
            try
            {
                Dictionary<string, string> dicColNames = GetImportDicColumsTemplateMapping();
                string url = "";
                string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Prd_ProductID).Name), ref url);
                ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Prd_ProductID"));

                return Json(new { Success = true, Title = "Xuất file excel template mapping thành công!", CheckSuccess = "1", strUrl = url });
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
        public ActionResult Export(string orgid = "", string productid = "", string speccode = "", string specname = "", string buyer = "", string brand = "", string modelcode = "", int page = 0)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs_ProductCentrer = userState.AddressAPIs_ProductCentrer;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var listobjRT_Prd_ProductIDCur = new List<ProductCentrer.Prd_ProductID>();
            string url = "";
            var itemCount = 0;
            var pageCount = 0;
            #region["Prd_PrdIDCustomField"]
            var objPrd_PrdIDCustomField = new ProductCentrer.Prd_PrdIDCustomField()
            {
                FlagActive = FlagActive,
            };

            var strWhereOS_PrdCenter_Prd_PrdIDCustomField = strWhere_Prd_PrdIDCustomField(objPrd_PrdIDCustomField);

            var list_Prd_PrdIDCustomField = List_Prd_PrdIDCustomField(strWhereOS_PrdCenter_Prd_PrdIDCustomField);
            ViewBag.List_Prd_PrdIDCustomField = list_Prd_PrdIDCustomField;
            #endregion

            try
            {
                #region["Search"]

                var objPrd_ProductID = new ProductCentrer.Prd_ProductID()
                {
                    OrgID = orgid,
                    ProductID = productid,
                    SpecCode = speccode,
                    ms_SpecName = specname,
                    Buyer = buyer,
                    mb_BrandCode = brand,
                    mm_ModelCode = modelcode,
                };

                var strWhereClauseOS_PrdCenter_Mst_Spec = strWhereClauseProductID(objPrd_ProductID);

                var objRQ_Prd_ProductID = new ProductCentrer.RQ_Prd_ProductID()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = OS_ProductCentrer_GwUserCode,
                    GwPassword = OS_ProductCentrer_GwPassword,
                    OrgID = CUtils.StrValue(userState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseOS_PrdCenter_Mst_Spec,
                    Ft_Cols_Upd = null,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    UtcOffset = userState.UtcOffset,
                    // RQ_Prd_ProductID
                    Rt_Cols_Prd_ProductID = "*",
                    Prd_ProductID = null
                };

                var objRT_Prd_ProductIDCur = OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Get(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
                if (objRT_Prd_ProductIDCur != null && objRT_Prd_ProductIDCur.Lst_Prd_ProductID != null)
                {
                    if (objRT_Prd_ProductIDCur.MySummaryTable != null)
                    {
                        itemCount = Convert.ToInt32(objRT_Prd_ProductIDCur.MySummaryTable.MyCount);
                    }
                    if (objRT_Prd_ProductIDCur.Lst_Prd_ProductID != null && objRT_Prd_ProductIDCur.Lst_Prd_ProductID.Count > 0)
                    {
                        pageCount = (itemCount % RowsWorksheets == 0) ? itemCount / RowsWorksheets : itemCount / RowsWorksheets + 1;
                    }

                    listobjRT_Prd_ProductIDCur.AddRange(objRT_Prd_ProductIDCur.Lst_Prd_ProductID);

                    Dictionary<string, string> dicColNames = GetImportDicColums();
                    string filePath = GenExcelExportFilePath(string.Format(typeof(ProductCentrer.Prd_ProductID).Name), ref url);
                    ExcelExport.ExportToExcelFromList(listobjRT_Prd_ProductIDCur, dicColNames, filePath, string.Format("Prd_ProductID"));


                    #region["Export các trang còn lại"]
                    listobjRT_Prd_ProductIDCur.Clear();
                    if (pageCount > 1)
                    {
                        for (var i = 1; i <= pageCount; i++)
                        {
                            objRQ_Prd_ProductID.Ft_RecordStart = (i * RowsWorksheets).ToString();

                            var objRT_Prd_ProductIDExportCur = OS_PrdCenter_Prd_ProductIDService.Instance.WA_Prd_ProductID_Get(objRQ_Prd_ProductID, addressAPIs_ProductCentrer);
                            if (objRT_Prd_ProductIDExportCur != null && objRT_Prd_ProductIDExportCur.Lst_Prd_ProductID != null && objRT_Prd_ProductIDExportCur.Lst_Prd_ProductID.Count > 0)
                            {
                                listobjRT_Prd_ProductIDCur.AddRange(objRT_Prd_ProductIDExportCur.Lst_Prd_ProductID);
                                ExcelExport.ExportToExcelFromList(listobjRT_Prd_ProductIDCur, dicColNames, filePath, string.Format("Prd_ProductID_" + i));
                                listobjRT_Prd_ProductIDCur.Clear();
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

        #region ["strWhereClause"]
        private string strWhereClauseProductID(ProductCentrer.Prd_ProductID data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Prd_ProductID = TableName.Prd_ProductID + ".";
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";
            var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
            var Tbl_Mst_Model = TableName.Mst_Model + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.ProductID))
            {
                sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.ProductID, "%" + CUtils.StrValueOrNull(data.ProductID) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.SpecCode, "%" + CUtils.StrValueOrNull(data.SpecCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ms_SpecName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecName, "%" + CUtils.StrValueOrNull(data.ms_SpecName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.Buyer))
            {
                sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.Buyer, "%" + CUtils.StrValueOrNull(data.Buyer) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.mb_BrandCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandCode, "%" + CUtils.StrValueOrNull(data.mb_BrandCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.mb_BrandName))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandName, "%" + CUtils.StrValueOrNull(data.mb_BrandName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.mm_ModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Model + TblMst_Model.ModelCode, "%" + CUtils.StrValueOrNull(data.mm_ModelCode) + "%", "like");
            }

            //if (!CUtils.IsNullOrEmpty(data.ProductID))
            //{
            //    sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.ProductID, CUtils.StrValueOrNull(data.ProductID), "=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.SpecCode))
            //{
            //    sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.SpecCode, CUtils.StrValueOrNull(data.SpecCode), "=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.ms_SpecName))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecName, CUtils.StrValueOrNull(data.ms_SpecName), "=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.Buyer))
            //{
            //    sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.Buyer, CUtils.StrValueOrNull(data.Buyer), "=");
            //}
            //if (!CUtils.IsNullOrEmpty(data.OrgID))
            //{
            //    sbSql.AddWhereClause(Tbl_Prd_ProductID + TblPrd_ProductID.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            //}
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereRQ_Mst_Spec(ProductCentrer.Mst_Spec data, string brand)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Spec = TableName.Mst_Spec + ".";
            var Tbl_Mst_Brand = TableName.Mst_Brand + ".";
            //var Tbl_Mst_Model = TableName.Mst_Model + ".";           
            if (!CUtils.IsNullOrEmpty(data.SpecCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.SpecCode, "%" + CUtils.StrValueOrNull(data.SpecCode) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ModelCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.ModelCode, CUtils.StrValueOrNull(data.ModelCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Spec + TblMst_Spec.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(brand))
            {
                sbSql.AddWhereClause(Tbl_Mst_Brand + TblMst_Brand.BrandCode, CUtils.StrValueOrNull(brand), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhere_Prd_PrdIDCustomField(ProductCentrer.Prd_PrdIDCustomField data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Prd_PrdIDCustomField = TableName.Prd_PrdIDCustomField + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Prd_PrdIDCustomField + TblPrd_PrdIDCustomField.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Prd_PrdIDCustomField(ProductCentrer.Prd_PrdIDCustomField data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Prd_PrdIDCustomField = TableName.Prd_PrdIDCustomField + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Prd_PrdIDCustomField + TblPrd_PrdIDCustomField.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Prd_PrdIDCustomField + TblPrd_PrdIDCustomField.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        private string strWhereClause_Mst_NNT(Mst_NNT data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_NNT = TableName.Mst_NNT + ".";
            //if (!CUtils.IsNullOrEmpty(data.NNTType))
            //{
            //    sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTType, CUtils.StrValueOrNull(data.NNTType), "=");
            //}
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MST, CUtils.StrValueOrNull(data.MST), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.MSTParent))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.MSTParent, CUtils.StrValueOrNull(data.MSTParent), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NNTFullName))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.NNTFullName, "%" + CUtils.StrValueOrNull(data.NNTFullName) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.ContactEmail, "%" + CUtils.StrValueOrNull(data.ContactEmail) + "%", "like");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.FlagActive, CUtils.StrValueOrNull(data.FlagActive), "=");
            }

            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_NNT + TblMst_NNT.OrgID, CUtils.StrValueOrNull(data.OrgID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}