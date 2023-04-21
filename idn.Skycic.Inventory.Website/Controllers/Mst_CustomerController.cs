using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Constants;
using Newtonsoft.Json;
using idn.Skycic.Inventory.Common.Extensions;
using idn.Skycic.Inventory.Website.Utils;
using System.Data;
using System.Text;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class Mst_CustomerController : AdminBaseController
    {
        // GET: Mst_Customer
        public ActionResult Index(
            string customerName = "",
            string[] customerType = null,
            string[] customerSourceCode = null,
            string[] customerGrpCode = null,
            string[] customerMobilePhone = null,
            string[] contactEmail = null,
            string init = "init",
            int page = 0,
            int recordcount = 10
            )
        {



            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_KHACH_HANG");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }


            ViewBag.PageCur = page.ToString();
           
            var itemCount = 0;
            var pageCount = 0;
            ViewBag.Recordcount = recordcount;

            ViewBag.UserState = UserState;
            var pageInfo = new PageInfo<Mst_Customer>(0, recordcount)
            {
                DataList = new List<Mst_Customer>()
            };

            #region ["Nhóm khách hàng"]
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
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_CustomerGroup = "*",
            };
            var objRT_Mst_CustomerGroup = Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup, CUtils.StrValue(UserState.AddressAPIs));
            if (objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup != null && objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup.Count > 0)
            {
                listMst_CustomerGroup.AddRange(objRT_Mst_CustomerGroup.Lst_Mst_CustomerGroup);
            }
            ViewBag.listMst_CustomerGroup = listMst_CustomerGroup;
            #endregion
            #region ["Loại khách hàng"]
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
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_CustomerType = "*",
            };
            var objRT_Mst_CustomerType = Mst_CustomerTypeService.Instance.WA_Mst_CustomerType_Get(objRQ_Mst_CustomerType, CUtils.StrValue(UserState.AddressAPIs));
            if (objRT_Mst_CustomerType.Lst_Mst_CustomerType != null && objRT_Mst_CustomerType.Lst_Mst_CustomerType.Count > 0)
            {
                listMst_CustomerType.AddRange(objRT_Mst_CustomerType.Lst_Mst_CustomerType);
            }
            ViewBag.listMst_CustomerType = listMst_CustomerType;
            #endregion
            #region ["Nguồn khách"]
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
                Ft_WhereClause = "Mst_CustomerSource.OrgID = '"+ CUtils.StrValue(UserState.Mst_NNT.OrgID) +"'",
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_CustomerSource = "*",
            };
            var objRT_Mst_CustomerSource = Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource, CUtils.StrValue(UserState.AddressAPIs));
            if (objRT_Mst_CustomerSource.Lst_Mst_CustomerSource != null && objRT_Mst_CustomerSource.Lst_Mst_CustomerSource.Count > 0)
            {
                listMst_CustomerSource.AddRange(objRT_Mst_CustomerSource.Lst_Mst_CustomerSource);
            }
            ViewBag.listMst_CustomerSource = listMst_CustomerSource;
            #endregion


            #region["Khu vực"]

            var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area() { FlagActive = Client_Flag.Active });
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
                Ft_WhereClause = strWhereClauseMst_Area,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Area = "*",
                Mst_Area = null,
            };
            var list_Mst_Area = WA_Mst_Area_Get(objRQ_Mst_Area);
            ViewBag.List_Mst_Area = list_Mst_Area;



            #endregion


            #region["Khách hàng"]
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
                FuncType = null,
                Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                Ft_RecordCount = recordcount.ToString(),
                Ft_WhereClause = "",
                Ft_Cols_Upd = null,
                // RQ_Mst_ProductGroup
                Rt_Cols_Mst_Customer = "*",
                Rt_Cols_Mst_CustomerInCustomerGroup = "*",
                Rt_Cols_UserOwner_Customer = "*"
            };
            #endregion




            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, CUtils.StrValue(UserState.AddressAPIs));
            if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
            {
                pageInfo.DataList = objRT_Mst_Customer.Lst_Mst_Customer;
                itemCount = objRT_Mst_Customer.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Customer.MySummaryTable.MyCount);
                pageCount = (itemCount % recordcount == 0) ? (itemCount / recordcount) : (itemCount / recordcount + 1);

                // Nhóm khách hàng
                if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup != null && objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup.Count > 0)
                {
                    foreach (var cus in objRT_Mst_Customer.Lst_Mst_Customer)
                    {
                        var listCusGrpCode = objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup
                            .Where(c => c.CustomerCodeSys.Equals(cus.CustomerCodeSys))
                            .Select(grp => grp.CustomerGrpCode)
                            .Distinct()
                            .ToList();
                        var listCusGrpName = objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup
                            .Where(c => c.CustomerCodeSys.Equals(cus.CustomerCodeSys))
                            .Select(grp => grp.mcg_CustomerGrpName)
                            .Distinct()
                            .ToList();
                        cus.CustomerGrpCode = string.Join(",", listCusGrpCode);
                        cus.mcg_CustomerGrpName = string.Join(",", listCusGrpName);
                    }
                }

                // khu vực
                if(objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_CustomerInArea != null && objRT_Mst_Customer.Lst_Mst_CustomerInArea.Count > 0)
                {
                    foreach (var cus in objRT_Mst_Customer.Lst_Mst_Customer)
                    {
                        var lstAreaName = new List<object>();
                        var lstCusAreaName = objRT_Mst_Customer.Lst_Mst_CustomerInArea.Where(are => are.CustomerCodeSys.Equals(cus.CustomerCodeSys)).Select(areGroup => areGroup.ma_AreaName).ToList();
                        if (lstCusAreaName != null && lstCusAreaName.Count > 0)
                        {
                            lstAreaName.AddRange(lstCusAreaName);
                        }
                        cus.ma_AreaName = string.Join("; ", lstAreaName.ToArray());

                    }
                }
            }
            pageInfo.PageSize = recordcount;
            pageInfo.PageIndex = page;
            pageInfo.ItemCount = itemCount;
            pageInfo.PageCount = pageCount;
            ViewBag.StartCount = (page * recordcount).ToString();
            return View(pageInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoSearch(
            string customerName = "",
            string[] customerType = null,
            string[] customerSourceCode = null,
            string customerMobilePhone = "",
            string[] customerGrpCode = null,
            string contactEmail = "",
            string flagDealer = "",
            string flagSupplier = "",
            string flagShipper = "",
            string flagEndUser = "",
            string flagBank = "",
            string flagInsurrance = "",
            string init = "init",
            int page = 0,
            int recordcount = 10)
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                ViewBag.init = "search";
                ViewBag.PageCur = page.ToString();
                ViewBag.RecordCount = recordcount.ToString();
                var itemCount = 0;
                var pageCount = 0;

                var pageInfo = new PageInfo<Mst_Customer>(0, recordcount)
                {
                    DataList = new List<Mst_Customer>()
                };

                var strWhereClauseMst_Customer =
                    StrWhereClauseMst_CustomerSearch(
                        customerName,
                        customerGrpCode,
                        customerType,
                        customerSourceCode,
                        customerMobilePhone,
                        contactEmail,
                        flagDealer,
                        flagSupplier,
                        flagShipper,
                        flagEndUser,
                        flagBank,
                        flagInsurrance
                        );
                var listCustomer = new List<Mst_Customer>();
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
                    FuncType = null,
                    Ft_RecordStart = (Convert.ToInt32(page) * recordcount).ToString(),
                    Ft_RecordCount = recordcount.ToString(),
                    Ft_WhereClause = strWhereClauseMst_Customer,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Rt_Cols_Mst_Customer = "*",
                    Rt_Cols_UserOwner_Customer = "*",
                    Rt_Cols_Mst_CustomerInCustomerGroup = "*",
                    Rt_Cols_Mst_CustomerInArea = "*"
                };
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Customer);
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, CUtils.StrValue(UserState.AddressAPIs));

                if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
                {
                    if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup != null && objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup.Count > 0)
                    {
                        foreach (var cus in objRT_Mst_Customer.Lst_Mst_Customer)
                        {
                            var lstGrpName = new List<object>();
                            var lstCusGrpName = objRT_Mst_Customer.Lst_Mst_CustomerInCustomerGroup
                                .Where(grp => grp.CustomerCodeSys.Equals(cus.CustomerCodeSys))
                                .Select(cusGrp => cusGrp.mcg_CustomerGrpName)
                                .ToList();
                            if (lstCusGrpName != null && lstCusGrpName.Count > 0)
                            {
                                lstGrpName.AddRange(lstCusGrpName);
                            }
                            cus.mcg_CustomerGrpName = string.Join(",", lstGrpName.ToArray());
                        }
                    }


                    // khu vực

                    if (objRT_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_CustomerInArea != null && objRT_Mst_Customer.Lst_Mst_CustomerInArea.Count > 0)
                    {
                        foreach (var cus in objRT_Mst_Customer.Lst_Mst_Customer)
                        {
                            var lstAreaName = new List<object>();
                            var lstCusAreaName = objRT_Mst_Customer.Lst_Mst_CustomerInArea.Where(are => are.CustomerCodeSys.Equals(cus.CustomerCodeSys)).Select(areGroup => areGroup.ma_AreaName).ToList();
                            if (lstCusAreaName != null && lstCusAreaName.Count > 0)
                            {
                                lstAreaName.AddRange(lstCusAreaName);
                            }
                            cus.ma_AreaName = string.Join(", ", lstAreaName.ToArray());

                        }
                    }

                    pageInfo.DataList = objRT_Mst_Customer.Lst_Mst_Customer;
                    itemCount = objRT_Mst_Customer.MySummaryTable == null ? 0 : Convert.ToInt32(objRT_Mst_Customer.MySummaryTable.MyCount);
                }

                pageCount = (itemCount % recordcount == 0) ? itemCount / recordcount : itemCount / recordcount + 1;

                pageInfo.PageSize = recordcount;
                pageInfo.PageIndex = page;
                pageInfo.ItemCount = itemCount;
                pageInfo.PageCount = pageCount;
                ViewBag.StartCount = (page * recordcount).ToString();

                return JsonView("BindDataMst_Customer", pageInfo);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        public string StrWhereClauseMst_CustomerSearch(
            string customerName,
            string[] customerGrpCode,
            string[] customerType,
            string[] customerSourceCode,
            string customerMobilePhone,
            string contactEmail,
            string flagDealer,
            string flagSupplier,
            string flagShipper,
            string flagEndUser,
            string flagBank,
            string flagInsurrance
            )
        {
            var strWhereClause = "";
            var Tbl_Mst_Customer = TableName.Mst_Customer + ".";
            var sbSql = new StringBuilder();

            var sbSqlCusGrpCode = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(customerGrpCode) && customerGrpCode.Length > 0)
            {
                foreach (var cusGrpCode in customerGrpCode)
                {
                    sbSqlCusGrpCode.AddWhereClause("Mst_CustomerInCustomerGroup." + "CustomerGrpCode", cusGrpCode, "=", "OR");
                }
                if (sbSqlCusGrpCode.Length > 0)
                {
                    sbSql.Append(string.Format("({0})", sbSqlCusGrpCode));
                }
            }
            var sbSqlCustomerType = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(customerType) && customerType.Length > 0)
            {
                foreach (var cusType in customerType)
                {
                    sbSqlCustomerType.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerType, cusType, "=", "OR");
                }
                if (sbSqlCustomerType.Length > 0)
                {
                    var and = sbSql.Length > 0 ? "AND" : "";
                    sbSql.Append(string.Format(" {0} ({1})", and, sbSqlCustomerType));
                }
            }
            var sbSqlCusSourceCode = new StringBuilder();
            if (!CUtils.IsNullOrEmpty(customerSourceCode) && customerSourceCode.Length > 0)
            {
                foreach (var cusSourceCode in customerSourceCode)
                {
                    sbSqlCusSourceCode.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerSourceCode, cusSourceCode, "=", "OR");
                }
                if (sbSqlCusSourceCode.Length > 0)
                {
                    var and = sbSql.Length > 0 ? "AND" : "";
                    sbSql.Append(string.Format(" {0} ({1})", and, sbSqlCusSourceCode));
                }
            }

            if (!CUtils.IsNullOrEmpty(customerName) && customerName.Length > 0)
            {
                var sbSqlCustomer = new StringBuilder();
                sbSqlCustomer.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerName, string.Format("%{0}%", customerName), "LIKE");
                sbSqlCustomer.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerCode, string.Format("%{0}%", customerName), "LIKE", "OR");
                if (sbSqlCustomer.Length > 0)
                {
                    var and = sbSql.Length > 0 ? "AND" : "";
                    sbSql.Append(string.Format(" {0} ({1})", and, sbSqlCustomer));
                }
            }

            if (!CUtils.IsNullOrEmpty(customerMobilePhone) && customerMobilePhone.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerMobilePhone, "%" + customerMobilePhone + "%", "LIKE");
            }
            if (!CUtils.IsNullOrEmpty(contactEmail) && contactEmail.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.ContactEmail, "%" + contactEmail + "%", "LIKE");
            }
            if (!CUtils.IsNullOrEmpty(flagDealer) && flagDealer.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagDealer, flagDealer, "=");
            }
            if (!CUtils.IsNullOrEmpty(flagSupplier) && flagSupplier.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagSupplier, flagSupplier, "=");
            }
            if (!CUtils.IsNullOrEmpty(flagShipper) && flagShipper.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagShipper, flagShipper, "=");
            }
            if (!CUtils.IsNullOrEmpty(flagEndUser) && flagEndUser.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagEndUser, flagEndUser, "=");
            }
            if (!CUtils.IsNullOrEmpty(flagBank) && flagBank.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagBank, flagBank, "=");
            }
            if (!CUtils.IsNullOrEmpty(flagInsurrance) && flagInsurrance.Length > 0)
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagInsurrance, flagInsurrance, "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }

        #region ["Export Excel"]
        #region ["Export Excel"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Export(
            string customerName = "",
            string[] customerType = null,
            string[] customerSourceCode = null,
            string customerMobilePhone = "",
            string[] customerGrpCode = null,
            string contactEmail = "",
            string flagDealer = "",
            string flagSupplier = "",
            string flagShipper = "",
            string flagEndUser = "",
            string flagBank = "",
            string flagInsurrance = "",
            int page = 0,
            int recordcount = 10)
        {
            var list = new List<Mst_Customer>();
            var strWhereClauseMst_Customer =
                    StrWhereClauseMst_CustomerSearch(
                        customerName,
                        customerGrpCode,
                        customerType,
                        customerSourceCode,
                        customerMobilePhone,
                        contactEmail,
                        flagDealer,
                        flagSupplier,
                        flagShipper,
                        flagEndUser,
                        flagBank,
                        flagInsurrance
                        );
            var listCustomer = new List<Mst_Customer>();
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
                FuncType = null,
                Ft_RecordStart = Ft_RecordStart,
                Ft_RecordCount = Ft_RecordCount,
                Ft_WhereClause = strWhereClauseMst_Customer,
                Ft_Cols_Upd = null,
                // RQ_Mst_Product
                Rt_Cols_Mst_Customer = "*",
                Rt_Cols_UserOwner_Customer = "*",
                Rt_Cols_Mst_CustomerInCustomerGroup = "*"
            };
            var addressAPIs = CUtils.StrValue(UserState.AddressAPIs);
            var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Get(objRQ_Mst_Customer, CUtils.StrValue(UserState.AddressAPIs));
            if (objRT_Mst_Customer.Lst_Mst_Customer != null && objRT_Mst_Customer.Lst_Mst_Customer.Count > 0)
            {
                list.AddRange(objRT_Mst_Customer.Lst_Mst_Customer);
            }

            // Dynamic Field
            var listParams = new List<CustomerDynamicFieldParamType>();
            var listCustomer_DynamicField = new List<Customer_DynamicField>();
            var strWhereClauseCustomer_DynamicField = strWhereClause_Customer_DynamicField(new Customer_DynamicField()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FlagActive = Client_Flag.Active
            });
            var objRQ_Customer_DynamicField = new RQ_Customer_DynamicField
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
                Ft_WhereClause = strWhereClauseCustomer_DynamicField,
                Ft_Cols_Upd = null,
                // RQ_Customer_DynamicField
                Rt_Cols_Customer_DynamicField = "*",
                Customer_DynamicField = null
            };
            listCustomer_DynamicField = WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField);
            if (listCustomer_DynamicField != null && listCustomer_DynamicField.Count > 0)
            {
                if (listCustomer_DynamicField[0].ParamTypes != null && listCustomer_DynamicField[0].ParamTypes.Count > 0)
                {
                    listParams.AddRange(listCustomer_DynamicField[0].ParamTypes);
                }
            }
            foreach (var cus in list)
            {
                // Truong dong 
                var ListCusDynamicFieldValue = new List<string>();
                if (cus.CustomDataDict != null && cus.CustomDataDict.Count > 0)
                {
                    foreach (var pair in cus.CustomDataDict)
                    {
                        var paramTitle = listParams.FirstOrDefault(param => param.Code.Equals(pair.Key));
                        if (paramTitle != null)
                        {
                            var fieldValue = string.Format("{0}:{1}", paramTitle.Title, pair.Value);
                            ListCusDynamicFieldValue.Add(fieldValue);
                        }
                    }
                    cus.ListOfCustDynamicFieldValue = string.Join("|", ListCusDynamicFieldValue.ToArray());
                }

            }

            Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
            string url = "";
            string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Customer).Name), ref url);
            ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Customer"));

            return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
        }
        #endregion

        #region ["Export Template"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ExportTemplate()
        {
            var list = new List<Mst_Product>();

            Dictionary<string, string> dicColNames = GetImportDicColumsTemplate();
            string url = "";
            string filePath = GenExcelExportFilePath(string.Format(typeof(Mst_Customer).Name), ref url);
            ExcelExport.ExportToExcelFromList(list, dicColNames, filePath, string.Format("Mst_Customer"));

            return Json(new { Success = true, Title = "Đã xuất excel thành công!", CheckSuccess = "1", strUrl = url });
        }
        #endregion

        #endregion

        #region ["Import Excel"]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Import(HttpPostedFileWrapper file)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            
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
                var list = new List<Mst_Customer>();

                #region ["Get Customer async"]
                var listCustomer_Cur = new List<Mst_Customer>();
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
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = "",
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_Customer = "*",
                    Rt_Cols_UserOwner_Customer = "*",
                    Mst_Customer = null,
                    Lst_Mst_Customer = null,
                    Lst_UserOwner_Customer = null

                };
                var objRT_Mst_Customer_Async = RT_Mst_Customer_Async(objRQ_Mst_Customer);
                if (objRT_Mst_Customer_Async != null && objRT_Mst_Customer_Async.Result != null && objRT_Mst_Customer_Async.Result.Lst_Mst_Customer.Count > 0)
                {
                    listCustomer_Cur.AddRange(objRT_Mst_Customer_Async.Result.Lst_Mst_Customer);
                }
                #endregion

                #region ["List Dynamic Field Current"]
                var listCustomer_DynamicField = new List<Customer_DynamicField>();
                var paramTypes = new List<CustomerDynamicFieldParamType>();
                var strWhereClauseCustomer_DynamicField = strWhereClause_Customer_DynamicField(new Customer_DynamicField()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
                var objRQ_Customer_DynamicField = new RQ_Customer_DynamicField
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
                    Ft_WhereClause = strWhereClauseCustomer_DynamicField,
                    Ft_Cols_Upd = null,
                    // RQ_Customer_DynamicField
                    Rt_Cols_Customer_DynamicField = "*",
                    Customer_DynamicField = null
                };
                listCustomer_DynamicField = WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField);
                if (listCustomer_DynamicField != null && listCustomer_DynamicField.Count > 0)
                {
                    paramTypes.AddRange(listCustomer_DynamicField[0].ParamTypes);
                }
                #endregion

                var table = ExcelImportNew.Query(filePath, "A2");
                if (table != null && table.Rows.Count > 0)
                {
                    if (table.Columns.Count != 39)
                    {
                        exitsData = "File excel import không hợp lệ!";
                        resultEntry.AddMessage(exitsData);
                        return Json(resultEntry);
                    }
                    else
                    {
                        #region ["Nguồn KH, Nhóm KH"]
                        var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
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
                            Ft_WhereClause = "",
                            Ft_Cols_Upd = null,
                            // RQ_Mst_CustomerSource
                            Rt_Cols_Mst_CustomerSource = "*"
                        };
                        var list_Mst_CustomerSource = WA_Mst_CustomerSource_Get(objRQ_Mst_CustomerSource);

                        var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
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
                            Ft_WhereClause = "",
                            Ft_Cols_Upd = null,
                            // RQ_Mst_CustomerGroup
                            Rt_Cols_Mst_CustomerGroup = "*"
                        };
                        var list_Mst_CustomerGroup = WA_Mst_CustomerGroup_Get(objRQ_Mst_CustomerGroup);
                        #endregion

                        #region ["Thêm col"]
                        if (!table.Columns.Contains(TblMst_Customer.FlagCustomerAvatarPath))
                        {
                            table.Columns.AddRange(
                                new DataColumn[] { new DataColumn(TblMst_Customer.FlagCustomerAvatarPath, typeof(string)) }
                            );
                        }
                        if (!table.Columns.Contains(TblMst_Customer.CustomerSourceCode))
                        {
                            table.Columns.AddRange(
                                new DataColumn[] { new DataColumn(TblMst_Customer.CustomerSourceCode, typeof(string)) }
                            );
                        }
                        if (!table.Columns.Contains(TblMst_Customer.CustomerGrpCode))
                        {
                            table.Columns.AddRange(
                                new DataColumn[] { new DataColumn(TblMst_Customer.CustomerGrpCode, typeof(string)) }
                            );
                        }
                        if (!table.Columns.Contains(TblMst_Customer.CustomerType))
                        {
                            table.Columns.AddRange(
                                new DataColumn[] { new DataColumn(TblMst_Customer.CustomerType, typeof(string)) }
                            );
                        }

                        #endregion

                        var idx = 0;
                        var iRows = 2;
                        var strRows = " - Dòng ";

                        foreach (DataRow dr in table.Rows)
                        {
                            dr["FlagCustomerAvatarPath"] = 1;

                            #region["Check null"]
                            if (CUtils.IsNullOrEmpty(dr["CustomerCode"]))
                            {
                                exitsData = "Mã khách hàng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr["CustomerName"]))
                            {
                                exitsData = "Tên khách hàng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            if (CUtils.IsNullOrEmpty(dr["mct_CustomerTypeName"]))
                            {
                                exitsData = "Tên loại khách hàng không được trống!";
                                resultEntry.AddMessage(exitsData);
                                return Json(resultEntry);
                            }
                            #endregion

                            #region["Check duplicate"]
                            for (var i = 0; i < table.Rows.Count; i++)
                            {
                                var customerCur = table.Rows[i]["CustomerCode"].ToString().Trim();
                                for (var j = 0; j < table.Rows.Count; j++)
                                {
                                    if (i != j)
                                    {
                                        var _customerCur = table.Rows[j]["CustomerCode"].ToString().Trim();
                                        if (customerCur.Equals(_customerCur))
                                        {
                                            exitsData = "Mã khách hàng '" + customerCur + "' bị lặp trong file excel!";
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                            }
                            #endregion

                            iRows = 2;
                            iRows = (iRows + idx + 1);
                            strRows = " - Dòng ";
                            strRows += iRows;
                            idx++;

                            var customerCodeCur = CUtils.StrValue(dr[TblMst_Customer.CustomerCode]);

                            #region ["Check loại KH"]
                            if (!CUtils.IsNullOrEmpty(dr["mct_CustomerTypeName"]))
                            {
                                var cusTypeName = CUtils.StrValue(dr["mct_CustomerTypeName"]);
                                if (!cusTypeName.ToString().ToLower().Equals("cá nhân") && !cusTypeName.ToString().ToLower().Equals("tổ chức"))
                                {
                                    exitsData = "Loại khách hàng của mã khách " + customerCodeCur + " chỉ có thể nhập 'Cá nhân' hoặc 'Tổ chức'" + strRows + "!";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                                else
                                {
                                    dr["CustomerType"] = cusTypeName.ToString().ToLower().Equals("cá nhân") ? "CANHAN" : "TOCHUC";
                                }
                            }
                            #endregion

                            #region ["Dynamic Field"]
                            var listOfCustDynamicFieldValue = CUtils.StrValue(dr[TblMst_Customer.ListOfCustDynamicFieldValue]);
                            if (!CUtils.IsNullOrEmpty(listOfCustDynamicFieldValue))
                            {
                                var listCustomer_DynamicParamTypesExcel = new List<CustomerDynamicFieldParamType>();
                                var arrDynamicField = listOfCustDynamicFieldValue.Split('|');
                                if (arrDynamicField != null && arrDynamicField.Length > 0)
                                {
                                    foreach (var subArr in arrDynamicField)
                                    {
                                        var itemArr = subArr.Split(':');
                                        if (itemArr != null && itemArr.Length == 2)
                                        {

                                            listCustomer_DynamicParamTypesExcel.Add(new CustomerDynamicFieldParamType
                                            {
                                                Title = itemArr[0].Trim(),
                                                DefaultValue = itemArr[1].Trim(),
                                            });
                                        }
                                        else
                                        {
                                            exitsData = "Trường động mã khách hàng'" + customerCodeCur + "' không hợp lệ" + strRows;
                                            resultEntry.AddMessage(exitsData);
                                            return Json(resultEntry);
                                        }
                                    }
                                }
                                var listCusDynamicFieldParamsNotExist = new List<CustomerDynamicFieldParamType>();
                                var listCusDynamicFieldParamsInput = new List<CustomerDynamicFieldParamType>();
                                if (listCustomer_DynamicParamTypesExcel.Count > 0)
                                {
                                    foreach (var item in listCustomer_DynamicParamTypesExcel)
                                    {
                                        var key = paramTypes.FirstOrDefault(param => param.Title.Equals(item.Title));
                                        if (key == null)
                                        {
                                            item.Code = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss.fff");
                                            item.DataType = PrdDynamicFieldDataTypes.String;
                                            item.ControlType = ControlTypes.Default;
                                            listCusDynamicFieldParamsNotExist.Add(item);
                                        }
                                        else
                                        {
                                            item.Code = key.Code;
                                            item.DataType = key.DataType;
                                            item.ControlType = ControlTypes.Default;
                                        }
                                        listCusDynamicFieldParamsInput.Add(item);
                                    }
                                }

                                // Them item trong list not exist
                                if (listCusDynamicFieldParamsNotExist.Count > 0)
                                {
                                    paramTypes.AddRange(listCusDynamicFieldParamsNotExist);

                                    #region["Xóa DynamicField cũ"]
                                    var customer_DynamicFieldDel = new Customer_DynamicField()
                                    {
                                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                                    };
                                    var objRQ_Customer_DynamicFieldDel = new RQ_Customer_DynamicField()
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
                                        // RQ_Customer_DynamicField
                                        Customer_DynamicField = customer_DynamicFieldDel,
                                    };
                                    Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Delete(objRQ_Customer_DynamicFieldDel, CUtils.StrValue(UserState.AddressAPIs));
                                    #endregion

                                    #region["Tạo DynamicField mới có add thêm objParamType"]
                                    var customer_DynamicFieldCreate = new Customer_DynamicField()
                                    {
                                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                        Detail = "",
                                        ParamTypes = paramTypes
                                    };
                                    var objRQ_Customer_DynamicFieldCreate = new RQ_Customer_DynamicField()
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
                                        // RQ_Customer_DynamicField
                                        Customer_DynamicField = customer_DynamicFieldCreate,
                                    };
                                    Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Create(objRQ_Customer_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                                    #endregion
                                }

                                var listArr = new List<string>();
                                foreach (var item in listCusDynamicFieldParamsInput)
                                {
                                    var itemDynamicField = "";
                                    itemDynamicField = string.Format("\"{0}\":\"{1}\"", item.Code, item.DefaultValue);
                                    listArr.Add(itemDynamicField);
                                }
                                listOfCustDynamicFieldValue = string.Join(",", listArr);
                                dr[TblMst_Customer.ListOfCustDynamicFieldValue] = "{" + listOfCustDynamicFieldValue + "}";
                            }
                            #endregion

                            #region ["Thêm nhóm và nguồn KH nếu chưa tồn tại"]
                            if (!CUtils.IsNullOrEmpty(dr["mcg_CustomerGrpName"]))
                            {
                                var lstCusGrpName = new List<string>();
                                var cusGrpName = CUtils.StrValue(dr["mcg_CustomerGrpName"]);
                                lstCusGrpName = cusGrpName.Split(',').ToList();
                                var lstCusGrpCode = new List<string>();
                                foreach (var grpName in lstCusGrpName)
                                {
                                    var existGrpName = list_Mst_CustomerGroup.FirstOrDefault(grp => grp.CustomerGrpName.Equals(grpName));
                                    if (existGrpName == null)
                                    {
                                        var objMst_CustomerGroup = new Mst_CustomerGroup()
                                        {
                                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                            CustomerGrpCode = WA_Seq_GenCommonCode_Get(SeqType.CustomerGrpCode),
                                            CustomerGrpName = cusGrpName,
                                            CustomerGrpDesc = "Create by excel",
                                        };

                                        lstCusGrpCode.Add(objMst_CustomerGroup.CustomerGrpCode.ToString());

                                        var lst_Mst_CustomerGroupImages = new List<Mst_CustomerGroupImages>();
                                        var objRQ_Mst_CustomerGroupCreate = new RQ_Mst_CustomerGroup()
                                        {
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
                                            Ft_WhereClause = "",
                                            Ft_Cols_Upd = null,
                                            // RQ_Mst_CustomerSource
                                            Mst_CustomerGroup = objMst_CustomerGroup,
                                            Lst_Mst_CustomerGroupImages = lst_Mst_CustomerGroupImages
                                        };
                                        Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Create(objRQ_Mst_CustomerGroupCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));

                                    }
                                    else
                                    {
                                        lstCusGrpCode.Add(existGrpName.CustomerGrpCode.ToString());
                                    }
                                }
                                dr["CustomerGrpCode"] = string.Join(",", lstCusGrpCode.ToArray());
                            }

                            if (!CUtils.IsNullOrEmpty(dr["mcs_CustomerSourceName"]))
                            {
                                var cusSrcName = CUtils.StrValue(dr["mcs_CustomerSourceName"]);
                                var existSrcName = list_Mst_CustomerSource.FirstOrDefault(grp => grp.CustomerSourceName.Equals(cusSrcName));
                                if (existSrcName == null)
                                {
                                    var objMst_CustomerSource = new Mst_CustomerSource()
                                    {
                                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                        CustomerSourceCode = WA_Seq_GenCommonCode_Get(SeqType.CustomerSourceCode),
                                        CustomerSourceName = cusSrcName,
                                        CustomerSourceDesc = "Create by excel"
                                    };
                                    var lst_Mst_CustomerSourceImages = new List<Mst_CustomerSourceImages>();
                                    var objRQ_Mst_CustomerSourceCreate = new RQ_Mst_CustomerSource()
                                    {
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
                                        Ft_WhereClause = "",
                                        Ft_Cols_Upd = null,
                                        // RQ_Mst_CustomerSource
                                        Mst_CustomerSource = objMst_CustomerSource,
                                        Lst_Mst_CustomerSourceImages = lst_Mst_CustomerSourceImages
                                    };
                                    Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Create(objRQ_Mst_CustomerSourceCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                                    dr["CustomerSourceCode"] = objMst_CustomerSource.CustomerSourceCode;
                                }
                                else
                                {
                                    dr["CustomerSourceCode"] = existSrcName.CustomerSourceCode;
                                }
                            }
                            #endregion

                            #region ["Check trùng số ĐT & email & MST"]
                            if (!CUtils.IsNullOrEmpty(dr["CustomerMobilePhone"]))
                            {
                                var cusMobilePhone = dr["CustomerMobilePhone"];
                                var existCusPhoneNo = listCustomer_Cur.FirstOrDefault(cus => !CUtils.IsNullOrEmpty(cus.CustomerMobilePhone) && cus.CustomerMobilePhone.Equals(cusMobilePhone));
                                if (existCusPhoneNo != null)
                                {
                                    exitsData = "Số điện thoại " + dr["CustomerMobilePhone"] + strRows + " đã tồn tại trong hệ thống'";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            if (!CUtils.IsNullOrEmpty(dr["CustomerEmail"]))
                            {
                                var cusEmail = dr["CustomerEmail"];
                                var existCusEmail = listCustomer_Cur.FirstOrDefault(cus => !CUtils.IsNullOrEmpty(cus.CustomerEmail) && cus.CustomerEmail.Equals(cusEmail));
                                if (existCusEmail != null)
                                {
                                    exitsData = "Email " + dr["CustomerEmail"] + strRows + " đã tồn tại trong hệ thống'";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }

                            if (!CUtils.IsNullOrEmpty(dr["MST"]))
                            {
                                var cusMST = dr["MST"];
                                var existCusMST = listCustomer_Cur.FirstOrDefault(cus => !CUtils.IsNullOrEmpty(cus.MST) && cus.MST.Equals(cusMST));
                                if (existCusMST != null)
                                {
                                    exitsData = "Mã số thuế " + dr["MST"] + strRows + " đã tồn tại trong hệ thống'";
                                    resultEntry.AddMessage(exitsData);
                                    return Json(resultEntry);
                                }
                            }
                            #endregion

                        }
                    }
                    list = DataTableCmUtils.ToListof<Mst_Customer>(table);
                    // Gọi hàm save data
                    if (list != null && list.Count > 0)
                    {
                        foreach (var item in list)
                        {
                            item.FlagActive = "1";
                            item.CustomerCodeSys = WA_Seq_GenCommonCode_Get(SeqType.CustomerCodeSys);
                            item.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                            item.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                        }

                        var objRQ_Mst_Customer_Create = new RQ_Mst_Customer()
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
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            Ft_WhereClause = null,
                            Ft_Cols_Upd = null,
                            // RQ_Sys_User
                            Lst_Mst_Customer = list,
                        };

                        var LstMst_CustomerInCustomerGroup = new List<Mst_CustomerInCustomerGroup>();
                        foreach (var customer in objRQ_Mst_Customer_Create.Lst_Mst_Customer)
                        {
                            if (customer.CustomerGrpCode != null && customer.CustomerGrpCode.ToString().Length > 0)
                            {
                                var lstCusGrpCode = customer.CustomerGrpCode.ToString().Split(',').ToList();
                                foreach (var grpCode in lstCusGrpCode)
                                {
                                    var objMst_CustomerInCustomerGroup = new Mst_CustomerInCustomerGroup
                                    {
                                        CustomerCodeSys = customer.CustomerCodeSys,
                                        CustomerGrpCode = grpCode,
                                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                                    };
                                    LstMst_CustomerInCustomerGroup.Add(objMst_CustomerInCustomerGroup);
                                    // SQL truncate avoid
                                    customer.CustomerGrpCode = "";
                                }
                            }
                        }
                        objRQ_Mst_Customer_Create.Lst_Mst_CustomerInCustomerGroup = LstMst_CustomerInCustomerGroup;
                        Mst_CustomerService.Instance.WA_Mst_Customer_Create(objRQ_Mst_Customer_Create, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                        resultEntry.Success = true;
                        exitsData = "Đã nhập dữ liệu excel thành công!";
                        resultEntry.AddMessage(exitsData);
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                    else
                    {
                        exitsData = "File excel import không có dữ liệu!";
                        resultEntry.AddMessage(exitsData);
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                resultEntry.IsServiceException = true;
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return Json(resultEntry);
        }
        #endregion

        #region ["GetDicCloumns"]
        private Dictionary<string, string> GetImportDicColumsTemplate()
        {
            return new Dictionary<string, string>
            {
                {TblMst_Customer.CustomerCode, "Mã khách hàng(*)"},
                {TblMst_Customer.CustomerName, "Tên khách hàng(*)"},
                {"mct_CustomerTypeName", "Tên loại khách hàng(*)"},
                {"mcs_CustomerSourceName", "Tên nguồn khách hàng"},
                {"mcg_CustomerGrpName", "Tên nhóm khách hàng"},
                {TblMst_Customer.CustomerPhoneNo, "Điện thoại cố định"},
                {TblMst_Customer.CustomerMobilePhone, "Điện thoại di động"},
                {TblMst_Customer.CustomerAvatarPath, "Đường dẫn ảnh avatar"},
                {TblMst_Customer.CustomerGender, "Giới tính"},
                {TblMst_Customer.CustomerEmail, "Email khách hàng"},
                {TblMst_Customer.CustomerAddress, "Địa chỉ"},
                {TblMst_Customer.ProvinceCode, "Tỉnh thành"},
                {TblMst_Customer.DistrictCode, "Quận huyện"},
                {TblMst_Customer.WardCode, "Phường xã"},
                {TblMst_Customer.AreaCode, "Khu vực"},
                {TblMst_Customer.FlagDealer, "Là đại lý (1)"},
                {TblMst_Customer.FlagSupplier, "Là nhà cung cấp (1)"},
                {TblMst_Customer.FlagShipper, "Là đơn vị giao hàng (1)"},
                {TblMst_Customer.FlagEndUser, "Là khách hàng cuối (1)"},
                {TblMst_Customer.FlagBank, "Là ngân hàng (1)"},
                {TblMst_Customer.FlagInsurrance, "Bảo hiểm (1)"},
                {TblMst_Customer.RepresentName, "Người đại diện"},
                {TblMst_Customer.RepresentPosition, "Chức vụ"},
                {TblMst_Customer.GovIDCardNo, "Số giấy tờ"},
                {TblMst_Customer.GovIDType, "Loại giấy tờ"},
                {TblMst_Customer.BankAccountNo, "Số tài khoản"},
                {TblMst_Customer.BankName, "Tên ngân hàng"},
                {TblMst_Customer.ContactName, "Người liên hệ"},
                {TblMst_Customer.ContactPhone, "Điện thoại liên hệ"},
                {TblMst_Customer.ContactEmail, "Email liên hệ"},
                {TblMst_Customer.Fax, "Fax"},
                {TblMst_Customer.CustomerDateOfBirth, "Ngày sinh/ Ngày thành lập"},
                {TblMst_Customer.Facebook, "Facebook"},
                {TblMst_Customer.InvoiceCustomerName, "Người mua hàng"},
                {TblMst_Customer.InvoiceOrgName, "Tên tổ chức"},
                {TblMst_Customer.MST, "Mã số thuế"},
                {TblMst_Customer.InvoiceCustomerAddress, "Địa chỉ hợp đồng"},
                {TblMst_Customer.InvoiceEmailSend, "Email nhận hợp đồng"},
                {TblMst_Customer.ListOfCustDynamicFieldValue, "Thuộc tính thêm"},
            };
        }
        #endregion

        #region["Xóa khách hàng"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string listCustomers)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objListMst_CustomerInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_Customer>>(listCustomers);

                var objRQ_Mst_Customer = new RQ_Mst_Customer()
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Product
                    Lst_Mst_Customer = objListMst_CustomerInput,
                };
                var objRT_Mst_Customer = Mst_CustomerService.Instance.WA_Mst_Customer_Delete(objRQ_Mst_Customer, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                // Lưu tại network
                objRQ_Mst_Customer.GwUserCode = GwUserCode;
                objRQ_Mst_Customer.GwPassword = GwPassword;
                objRQ_Mst_Customer.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_Mst_Customer.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_Mst_Customer.FlagIsDelete = "1";
                Mst_CustomerService.Instance.WA_Mst_Customer_Save(objRQ_Mst_Customer, UserState.AddressAPIs);

                resultEntry.Success = true;
                resultEntry.AddMessage("Xóa khách hàng thành công!");
                resultEntry.RedirectUrl = Url.Action("Index");
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }

            return Json(resultEntry);
        }
        #endregion

        #region["Tạo mới khách hàng"]
        [HttpGet]
        public async Task<ActionResult> Create()
        {

            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_KHACH_HANG_TAO_MOI");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }

            #region["Common"]
            #region["Loại khách hàng"]
            //var strWhereClauseMst_CustomerType = strWhereClause_Mst_CustomerType(new Common.Models.Mst_CustomerType()
            //{
            //    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
            //    FlagActive = Client_Flag.Active
            //});
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
                Ft_WhereClause = null,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerType 
                Rt_Cols_Mst_CustomerType = "*",
                Mst_CustomerType = null,
            };
            var list_Mst_CustomerType_Async = List_Mst_CustomerType_Async(objRQ_Mst_CustomerType);
            #endregion
            #region["Nhóm khách hàng"]
            var strWhereClauseMst_CustomerGroup = strWhereClause_Mst_CustomerGroup(new Mst_CustomerGroup()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FlagActive = Client_Flag.Active
            });
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
                Ft_WhereClause = strWhereClauseMst_CustomerGroup,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerGroup
                Rt_Cols_Mst_CustomerGroup = "*",
                Rt_Cols_Mst_CustomerInCustomerGroup = "*",
                Mst_CustomerGroup = null,
            };
            var list_Mst_CustomerGroupExt_Async = List_Mst_CustomerGroup_Async(objRQ_Mst_CustomerGroup);
            #endregion
            #region["Nguồn khách hàng"]
            var strWhereClauseMst_CustomerSource = strWhereClause_Mst_CustomerSource(new Mst_CustomerSource()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FlagActive = Client_Flag.Active
            });
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
                Ft_WhereClause = strWhereClauseMst_CustomerSource,
                Ft_Cols_Upd = null,
                // RQ_Mst_CustomerSource
                Rt_Cols_Mst_CustomerSource = "*",
                Mst_CustomerSource = null,
            };
            var list_Mst_CustomerSourceExt_Async = List_Mst_CustomerSource_Async(objRQ_Mst_CustomerSource);
            #endregion
            #region["Tỉnh/ thành phố"]
            var strWhereClauseMst_Province = strWhereClause_Mst_Province(new Mst_Province() { FlagActive = Client_Flag.Active });
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
                Ft_WhereClause = strWhereClauseMst_Province,
                Ft_Cols_Upd = null,
                // RQ_Mst_Province
                Rt_Cols_Mst_Province = "*",
                Mst_Province = null,
            };
            var list_Mst_Province_Async = List_Mst_Province_Async(objRQ_Mst_Province);
            #endregion
            #region["Quận/ huyện"]
            var strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District() { FlagActive = Client_Flag.Active });
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
                Ft_WhereClause = strWhereClauseMst_District,
                Ft_Cols_Upd = null,
                // RQ_Mst_District
                Rt_Cols_Mst_District = "*",
                Mst_District = null,
            };
            var list_Mst_District_Async = List_Mst_District_Async(objRQ_Mst_District);
            #endregion
            #region["Phường/ xã"]
            var strWhereClauseMst_Ward = strWhereClause_Mst_Ward(new Mst_Ward() { FlagActive = Client_Flag.Active });
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
                Ft_WhereClause = strWhereClauseMst_Ward,
                Ft_Cols_Upd = null,
                // RQ_Mst_Ward
                Rt_Cols_Mst_Ward = "*",
                Mst_Ward = null,
            };
            var list_Mst_Ward_Async = List_Mst_Ward_Async(objRQ_Mst_Ward);
            #endregion
            #region["Khu vực"]
            var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FlagActive = Client_Flag.Active
            });
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
                Ft_WhereClause = strWhereClauseMst_Area,
                Ft_Cols_Upd = null,
                // RQ_Mst_Area
                Rt_Cols_Mst_Area = "*",
                Mst_Area = null,
            };
            var list_Mst_AreaExt_Async = List_Mst_AreaExt_Async(objRQ_Mst_Area);

            #endregion
            #region["Loại giấy tờ"]
            var strWhereClauseMst_GovIDType = strWhereClause_Mst_GovIDType(new Mst_GovIDType()
            {
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FlagActive = Client_Flag.Active
            });
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
                Ft_WhereClause = strWhereClauseMst_GovIDType,
                Ft_Cols_Upd = null,
                // RQ_Mst_GovIDType
                Rt_Cols_Mst_GovIDType = "*",
                Mst_GovIDType = null,
            };
            var list_Mst_GovIDType_Async = List_Mst_GovIDType_Async(objRQ_Mst_GovIDType);
            #endregion
            #region["Thông tin bổ sung"]
            var strWhereClauseCustomer_DynamicField = strWhereClause_Customer_DynamicField(new Customer_DynamicField()
            {
                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                FlagActive = Client_Flag.Active
            });
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
                Ft_WhereClause = strWhereClauseCustomer_DynamicField,
                Ft_Cols_Upd = null,
                // RQ_Customer_DynamicField
                Rt_Cols_Customer_DynamicField = "*",
                Customer_DynamicField = null,
            };
            var list_Customer_DynamicField_Async = List_Customer_DynamicField_Async(objRQ_Customer_DynamicField);
            #endregion
            await Task.WhenAll(list_Mst_CustomerType_Async, list_Mst_CustomerGroupExt_Async, list_Mst_CustomerSourceExt_Async, list_Mst_Province_Async, list_Mst_AreaExt_Async, list_Mst_GovIDType_Async, list_Customer_DynamicField_Async);
            var list_Mst_CustomerType = new List<Common.Models.Mst_CustomerType>();
            var listMst_CustomerGroup = new List<Mst_CustomerGroup>();
            var listMst_CustomerSource = new List<Mst_CustomerSource>();
            var listMst_Province = new List<Mst_Province>();
            var listMst_District = new List<Mst_District>();
            var listMst_Ward = new List<Mst_Ward>();
            var listMst_AreaExt = new List<Mst_AreaExt>();
            var listMst_GovIDType = new List<Mst_GovIDType>();
            var listCustomer_DynamicField = new List<Customer_DynamicField>();
            if (list_Mst_CustomerType_Async != null && list_Mst_CustomerType_Async.Result != null &&
                list_Mst_CustomerType_Async.Result.Count > 0)
            {
                list_Mst_CustomerType.AddRange(list_Mst_CustomerType_Async.Result);
            }
            if (list_Mst_CustomerGroupExt_Async != null && list_Mst_CustomerGroupExt_Async.Result != null &&
                list_Mst_CustomerGroupExt_Async.Result.Count > 0)
            {
                listMst_CustomerGroup.AddRange(list_Mst_CustomerGroupExt_Async.Result);
            }
            if (list_Mst_CustomerSourceExt_Async != null && list_Mst_CustomerSourceExt_Async.Result != null &&
                list_Mst_CustomerSourceExt_Async.Result.Count > 0)
            {
                listMst_CustomerSource.AddRange(list_Mst_CustomerSourceExt_Async.Result);
            }
            if (list_Mst_Province_Async != null && list_Mst_Province_Async.Result != null &&
                list_Mst_Province_Async.Result.Count > 0)
            {
                listMst_Province.AddRange(list_Mst_Province_Async.Result);
            }
            if (list_Mst_District_Async != null && list_Mst_District_Async.Result != null &&
                list_Mst_District_Async.Result.Count > 0)
            {
                listMst_District.AddRange(list_Mst_District_Async.Result);
            }
            //if (list_Mst_Ward_Async != null && list_Mst_Ward_Async.Result != null &&
            //    list_Mst_Ward_Async.Result.Count > 0)
            //{
            //    listMst_Ward.AddRange(list_Mst_Ward_Async.Result);
            //}
            if (list_Mst_AreaExt_Async != null && list_Mst_AreaExt_Async.Result != null &&
                list_Mst_AreaExt_Async.Result.Count > 0)
            {
                listMst_AreaExt.AddRange(list_Mst_AreaExt_Async.Result);
            }
            if (list_Mst_GovIDType_Async != null && list_Mst_GovIDType_Async.Result != null &&
                list_Mst_GovIDType_Async.Result.Count > 0)
            {
                listMst_GovIDType.AddRange(list_Mst_GovIDType_Async.Result);
            }
            if (list_Customer_DynamicField_Async != null && list_Customer_DynamicField_Async.Result != null &&
                list_Customer_DynamicField_Async.Result.Count > 0)
            {
                listCustomer_DynamicField.AddRange(list_Customer_DynamicField_Async.Result);
            }

            ViewBag.List_Mst_CustomerType = list_Mst_CustomerType;
            ViewBag.listMst_CustomerGroup = listMst_CustomerGroup;
            ViewBag.listMst_CustomerSource = listMst_CustomerSource;
            ViewBag.ListMst_Province = listMst_Province;
            ViewBag.ListMst_District = listMst_District;
            ViewBag.ListMst_Ward = listMst_Ward;
            ViewBag.ListMst_AreaExt = listMst_AreaExt;
            ViewBag.ListMst_GovIDType = listMst_GovIDType;
            ViewBag.ListCustomer_DynamicField = listCustomer_DynamicField;
            #endregion


            return View(new Mst_Customer());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_CustomerUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerUI>(model);

                var objMst_CustomerInput = new Mst_Customer()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    CustomerType = objMst_CustomerUIInput.CustomerType,
                    CustomerCode = objMst_CustomerUIInput.CustomerCode,
                    CustomerName = objMst_CustomerUIInput.CustomerName,
                    CustomerNameEN = objMst_CustomerUIInput.CustomerName,
                    //CustomerGrpCode = objMst_CustomerUIInput.CustomerGrpCode,
                    CustomerSourceCode = objMst_CustomerUIInput.CustomerSourceCode,
                    CustomerPhoneNo = objMst_CustomerUIInput.CustomerPhoneNo,
                    CustomerMobilePhone = objMst_CustomerUIInput.CustomerMobilePhone,
                    CustomerAddress = objMst_CustomerUIInput.CustomerAddress,
                    ProvinceCode = objMst_CustomerUIInput.ProvinceCode,
                    DistrictCode = objMst_CustomerUIInput.DistrictCode,
                    WardCode = objMst_CustomerUIInput.WardCode,
                    AreaCode = objMst_CustomerUIInput.AreaCode,
                    RepresentName = objMst_CustomerUIInput.RepresentName,
                    RepresentPosition = objMst_CustomerUIInput.RepresentPosition,
                    GovIDCardNo = objMst_CustomerUIInput.GovIDCardNo,
                    GovIDType = objMst_CustomerUIInput.GovIDType,
                    BankAccountNo = objMst_CustomerUIInput.BankAccountNo,
                    BankName = objMst_CustomerUIInput.BankName,
                    BankCode = objMst_CustomerUIInput.BankName,
                    ContactName = objMst_CustomerUIInput.ContactName,
                    ContactPhone = objMst_CustomerUIInput.ContactPhone,
                    ContactEmail = objMst_CustomerUIInput.ContactEmail,
                    Fax = objMst_CustomerUIInput.Fax,
                    CustomerDateOfBirth = objMst_CustomerUIInput.CustomerDateOfBirth,
                    Facebook = objMst_CustomerUIInput.Facebook,
                    InvoiceCustomerName = objMst_CustomerUIInput.InvoiceCustomerName,
                    InvoiceOrgName = objMst_CustomerUIInput.InvoiceOrgName,
                    MST = objMst_CustomerUIInput.MST,
                    CustomerGender = objMst_CustomerUIInput.CustomerGender,
                    InvoiceCustomerAddress = objMst_CustomerUIInput.InvoiceCustomerAddress,
                    InvoiceEmailSend = objMst_CustomerUIInput.InvoiceEmailSend,
                    CustomerAvatarPath = objMst_CustomerUIInput.CustomerAvatarPath,
                    CustomerAvatarSpec = objMst_CustomerUIInput.CustomerAvatarSpec,
                    CustomerAvatarName = objMst_CustomerUIInput.CustomerAvatarName,
                    FlagCustomerAvatarPath = objMst_CustomerUIInput.FlagCustomerAvatarPath,
                    FlagDealer = objMst_CustomerUIInput.FlagDealer,
                    FlagSupplier = objMst_CustomerUIInput.FlagSupplier,
                    FlagShipper = objMst_CustomerUIInput.FlagShipper,
                    FlagEndUser = objMst_CustomerUIInput.FlagEndUser,
                    FlagBank = objMst_CustomerUIInput.FlagBank,
                    FlagInsurrance = objMst_CustomerUIInput.FlagInsurrance,
                    ListOfCustDynamicFieldValue = objMst_CustomerUIInput.ListOfCustDynamicFieldValue,
                    Remark = objMst_CustomerUIInput.Remark,
                };
                var customerCodeSys = WA_Seq_GenCommonCode_Get(SeqType.CustomerCodeSys);
                objMst_CustomerInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objMst_CustomerInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objMst_CustomerInput.CustomerCodeSys = customerCodeSys;
                objMst_CustomerInput.FlagActive = "1";

                var listCustomerGrpCode = objMst_CustomerUIInput.CustomerGrpCode.ToString().Split(',');
                var LstMst_CustomerInCustomerGroup = new List<Mst_CustomerInCustomerGroup>();
                //2021-04-27: Check nhóm khách hàng nếu có dữ liệu thì for
                if (listCustomerGrpCode != null && listCustomerGrpCode.Length > 0)
                {
                    foreach (var grpcode in listCustomerGrpCode)
                    {
                        var objMst_CustomerInCustomerGroup = new Mst_CustomerInCustomerGroup
                        {
                            CustomerCodeSys = customerCodeSys,
                            CustomerGrpCode = grpcode,
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                        };
                        LstMst_CustomerInCustomerGroup.Add(objMst_CustomerInCustomerGroup);
                    }
                }
                objMst_CustomerUIInput.CustomerGrpCode = "";

                var listUserOwner_Customer = new List<UserOwner_Customer>();
                var objUserOwner_Customer = new UserOwner_Customer()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    CustomerCodeSys = customerCodeSys,
                    UserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                };
                listUserOwner_Customer.Add(objUserOwner_Customer);


                #region["Vùng thị truòng"]
                var listAreaCode = objMst_CustomerUIInput.AreaCode?.ToString().Split(',');
                var Lst_Mst_CustomerInArea = new List<Mst_CustomerInArea>();
                if (listAreaCode != null && listAreaCode.Length > 0)
                {
                    foreach (var areacode in listAreaCode)
                    {
                        var objMst_CustomerInArea = new Mst_CustomerInArea
                        {
                            CustomerCodeSys = customerCodeSys,
                            AreaCode = areacode,
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID)
                        };
                        Lst_Mst_CustomerInArea.Add(objMst_CustomerInArea);
                    }
                }
                objMst_CustomerInput.AreaCode = "";
                #endregion
                var objRQ_Mst_Customer = new RQ_Mst_Customer()
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_Customer = null,
                    Rt_Cols_UserOwner_Customer = null,
                    Mst_Customer = null,
                    Lst_Mst_Customer = new List<Mst_Customer>(),
                    Lst_UserOwner_Customer = new List<UserOwner_Customer>(),
                };
                objRQ_Mst_Customer.Lst_Mst_Customer.Add(objMst_CustomerInput);
                objRQ_Mst_Customer.Lst_UserOwner_Customer.AddRange(listUserOwner_Customer);
                objRQ_Mst_Customer.Lst_Mst_CustomerInCustomerGroup = LstMst_CustomerInCustomerGroup;
                objRQ_Mst_Customer.Lst_Mst_CustomerInArea = Lst_Mst_CustomerInArea; // vùng thị trường
                //var x = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Customer);
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Customer);
                Mst_CustomerService.Instance.WA_Mst_Customer_Create(objRQ_Mst_Customer, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                // Lưu tại network
                objRQ_Mst_Customer.GwUserCode = GwUserCode;
                objRQ_Mst_Customer.GwPassword = GwPassword;
                objRQ_Mst_Customer.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_Mst_Customer.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_Mst_Customer.FlagIsDelete = "0";
                Mst_CustomerService.Instance.WA_Mst_Customer_Save(objRQ_Mst_Customer, UserState.AddressAPIs);
                // Lưu tại network
                objRQ_Mst_Customer.GwUserCode = GwUserCode;
                objRQ_Mst_Customer.GwPassword = GwPassword;
                objRQ_Mst_Customer.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                objRQ_Mst_Customer.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                objRQ_Mst_Customer.FlagIsDelete = "0";
                Mst_CustomerService.Instance.WA_Mst_Customer_Save(objRQ_Mst_Customer, UserState.AddressAPIs);
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới khách hàng thành công!");
                var flagRedirect = CUtils.StrValue(objMst_CustomerUIInput.FlagRedirect);
                if (flagRedirect.Equals("1"))
                {
                    resultEntry.RedirectUrl = Url.Action("Create");
                }
                else
                {
                    resultEntry.RedirectUrl = Url.Action("Index");
                }
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Customer_Check(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            var objMst_Customer_Check = new Mst_Customer_Check()
            {
                Success = true,
            };
            try
            {
                var objMst_CustomerUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerUI>(model);
                #region["Hàng hóa"]
                var objMst_CustomerSearch = new Mst_Customer()
                {
                    //CustomerCode = CUtils.StrValue(objMst_CustomerUIInput.CustomerCode),
                    //OrgID = orgID,
                    //NetworkID = networkID
                    CustomerMobilePhone = CUtils.StrValue(objMst_CustomerUIInput.CustomerMobilePhone),
                    ContactEmail = CUtils.StrValue(objMst_CustomerUIInput.ContactEmail),
                    MST = CUtils.StrValue(objMst_CustomerUIInput.MST),
                };
                var strWhereClauseMst_Customer = strWhereClause_Mst_Customer_By_Or(objMst_CustomerSearch);
                #endregion
                if (!CUtils.IsNullOrEmpty(strWhereClauseMst_Customer))
                {
                    var objRQ_Mst_CustomerSearch = new RQ_Mst_Customer()
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
                        Ft_WhereClause = strWhereClauseMst_Customer,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Customer
                        Rt_Cols_Mst_Customer = "*",
                        Rt_Cols_UserOwner_Customer = "*",
                        Mst_Customer = null,
                        Lst_Mst_Customer = null,
                        Lst_UserOwner_Customer = null

                    };
                    var objRT_Mst_CustomerSearch = RT_Mst_Customer_Get(objRQ_Mst_CustomerSearch);

                    resultEntry.Success = true;

                    if (objRT_Mst_CustomerSearch.Lst_Mst_Customer != null &&
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer.Count > 0)
                    {

                        objMst_Customer_Check.Action = "1"; // hiển thị cảnh báo
                        var messages = "";
                        var customerMobilePhone = CUtils.StrValue(objMst_CustomerUIInput.CustomerMobilePhone);
                        var contactEmail = CUtils.StrValue(objMst_CustomerUIInput.ContactEmail);
                        var mst = CUtils.StrValue(objMst_CustomerUIInput.MST);
                        foreach (var item in objRT_Mst_CustomerSearch.Lst_Mst_Customer)
                        {
                            if (!CUtils.IsNullOrEmpty(item.CustomerCodeSys) && (!CUtils.IsNullOrEmpty(objMst_CustomerUIInput.CustomerCodeSys)))
                            {
                                if (item.CustomerCodeSys.Equals(objMst_CustomerUIInput.CustomerCodeSys))
                                {
                                    objMst_Customer_Check = new Mst_Customer_Check { Success = true, Action = "0" };
                                    continue;
                                }
                            }
                            var customerMobilePhoneCur = CUtils.StrValue(item.CustomerMobilePhone);
                            var contactEmailCur = CUtils.StrValue(item.ContactEmail);
                            var mstCur = CUtils.StrValue(item.MST);
                            if (!CUtils.IsNullOrEmpty(customerMobilePhone) && !CUtils.IsNullOrEmpty(customerMobilePhoneCur))
                            {
                                if (customerMobilePhoneCur.Equals(customerMobilePhone))
                                {
                                    messages = "Khách hàng có số điện thoại '" + customerMobilePhone + "' đã tồn tại. Bạn có muốn lấy thông tin KH hiện tại không?";
                                    break;
                                }
                            }
                            if (!CUtils.IsNullOrEmpty(contactEmail) && !CUtils.IsNullOrEmpty(contactEmailCur))
                            {
                                if (contactEmailCur.Equals(contactEmail))
                                {
                                    messages = "Khách hàng có email '" + contactEmail + "' đã tồn tại. Bạn có muốn lấy thông tin KH hiện tại không?";
                                    break;
                                }
                            }
                            if (!CUtils.IsNullOrEmpty(mst) && !CUtils.IsNullOrEmpty(mstCur))
                            {
                                if (mstCur.Equals(mst))
                                {
                                    messages = "Khách hàng có MST '" + mst + "' đã tồn tại. Bạn có muốn lấy thông tin KH hiện tại không?";
                                    break;
                                }
                            }
                        }
                        objMst_Customer_Check.CustomerCode = CUtils.StrValue(objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerCode);
                        objMst_Customer_Check.Messages = messages;
                    }
                    else
                    {
                        objMst_Customer_Check.Action = "0"; // gọi hàm lưu luôn
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    objMst_Customer_Check.Action = "0"; // gọi hàm lưu luôn
                }


                return Json(objMst_Customer_Check);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }
        #endregion
        #region["Sửa thông tin khách hàng"]
        [HttpGet]
        public async Task<ActionResult> Update(string customercode, string customercodesys, string flagcustomerexsist)
        {
            var checkDeny = SysAccessCheckDeny(UserState, "MENU_QL_KHACH_HANG_SUA");
            if (!checkDeny)
            {
                return RedirectToAction("Index", "AccessDeny");
            }
            var objMst_Customer = new Mst_Customer();
            var listUserOwner_Customer = new List<UserOwner_Customer>();
            ViewBag.FlagCustomerExsist = flagcustomerexsist;
            if (!CUtils.IsNullOrEmpty(customercode) && !CUtils.IsNullOrEmpty(customercode))
            {
                #region["Hàng hóa"]
                var objMst_CustomerSearch = new Mst_Customer()
                {
                    //CustomerCodeSys = customercodesys,
                    CustomerCode = customercode,
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    //FlagActive = Client_Flag.Active
                };
                var strWhereClauseMst_Customer = strWhereClause_Mst_Customer(objMst_CustomerSearch);
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
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_Customer,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Customer
                    Rt_Cols_Mst_Customer = "*",
                    Rt_Cols_UserOwner_Customer = "*",
                    Rt_Cols_Mst_CustomerInCustomerGroup = "*",
                    Rt_Cols_Mst_CustomerInArea = "*",
                    Mst_Customer = null,
                    Lst_Mst_Customer = null,
                    Lst_UserOwner_Customer = null
                };

                //var json = new JavaScriptSerializer().Serialize(objRQ_Mst_Customer);
                var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Customer);
                var objRT_Mst_Customer_Async = RT_Mst_Customer_Async(objRQ_Mst_Customer);
                #endregion
                #region["Common"]
                #region["Loại khách hàng"]
                var strWhereClauseMst_CustomerType = strWhereClause_Mst_CustomerType(new Common.Models.Mst_CustomerType()
                {
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
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
                    Ft_WhereClause = strWhereClauseMst_CustomerType,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerType 
                    Rt_Cols_Mst_CustomerType = "*",
                    Mst_CustomerType = null,
                };
                var list_Mst_CustomerType_Async = List_Mst_CustomerType_Async(objRQ_Mst_CustomerType);
                #endregion
                #region["Nhóm khách hàng"]
                var strWhereClauseMst_CustomerGroup = strWhereClause_Mst_CustomerGroup(new Mst_CustomerGroup()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
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
                    Ft_WhereClause = strWhereClauseMst_CustomerGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Mst_CustomerGroup = null,
                };
                var list_Mst_CustomerGroupExt_Async = List_Mst_CustomerGroup_Async(objRQ_Mst_CustomerGroup);
                #endregion
                #region["Nguồn khách hàng"]
                var strWhereClauseMst_CustomerSource = strWhereClause_Mst_CustomerSource(new Mst_CustomerSource()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
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
                    Ft_WhereClause = strWhereClauseMst_CustomerSource,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Mst_CustomerSource = null,
                };
                var list_Mst_CustomerSourceExt_Async = List_Mst_CustomerSource_Async(objRQ_Mst_CustomerSource);
                #endregion
                #region["Tỉnh/ thành phố"]
                var strWhereClauseMst_Province = strWhereClause_Mst_Province(new Mst_Province() { FlagActive = Client_Flag.Active });
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
                    Ft_WhereClause = strWhereClauseMst_Province,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Province
                    Rt_Cols_Mst_Province = "*",
                    Mst_Province = null,
                };
                var list_Mst_Province_Async = List_Mst_Province_Async(objRQ_Mst_Province);
                #endregion

                #region["Khu vực"]
                var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area() {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
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
                    Ft_WhereClause = strWhereClauseMst_Area,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = "*",
                    Mst_Area = null,
                };
                var list_Mst_AreaExt_Async = List_Mst_AreaExt_Async(objRQ_Mst_Area);

                #endregion
                #region["Loại giấy tờ"]
                var strWhereClauseMst_GovIDType = strWhereClause_Mst_GovIDType(new Mst_GovIDType()
                {
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
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
                    Ft_WhereClause = strWhereClauseMst_GovIDType,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_GovIDType
                    Rt_Cols_Mst_GovIDType = "*",
                    Mst_GovIDType = null,
                };
                var list_Mst_GovIDType_Async = List_Mst_GovIDType_Async(objRQ_Mst_GovIDType);
                #endregion
                #region["Thông tin bổ sung"]
                var strWhereClauseCustomer_DynamicField = strWhereClause_Customer_DynamicField(new Customer_DynamicField()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
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
                    Ft_WhereClause = strWhereClauseCustomer_DynamicField,
                    Ft_Cols_Upd = null,
                    // RQ_Customer_DynamicField
                    Rt_Cols_Customer_DynamicField = "*",
                    Customer_DynamicField = null,
                };
                var list_Customer_DynamicField_Async = List_Customer_DynamicField_Async(objRQ_Customer_DynamicField);
                #endregion
                await Task.WhenAll(objRT_Mst_Customer_Async, list_Mst_CustomerType_Async, list_Mst_CustomerGroupExt_Async, list_Mst_CustomerSourceExt_Async, list_Mst_Province_Async, list_Mst_AreaExt_Async, list_Mst_GovIDType_Async, list_Customer_DynamicField_Async);
                var list_Mst_CustomerType = new List<Common.Models.Mst_CustomerType>();
                var listMst_CustomerGroup = new List<Mst_CustomerGroup>();
                var listMst_CustomerSource = new List<Mst_CustomerSource>();
                var listMst_Province = new List<Mst_Province>();
                var listMst_District = new List<Mst_District>();
                var listMst_Ward = new List<Mst_Ward>();
                var listMst_AreaExt = new List<Mst_AreaExt>();
                var listMst_GovIDType = new List<Mst_GovIDType>();
                var listCustomer_DynamicField = new List<Customer_DynamicField>();
                if (list_Mst_CustomerType_Async != null && list_Mst_CustomerType_Async.Result != null &&
                    list_Mst_CustomerType_Async.Result.Count > 0)
                {
                    list_Mst_CustomerType.AddRange(list_Mst_CustomerType_Async.Result);
                }
                if (list_Mst_CustomerGroupExt_Async != null && list_Mst_CustomerGroupExt_Async.Result != null &&
                    list_Mst_CustomerGroupExt_Async.Result.Count > 0)
                {
                    listMst_CustomerGroup.AddRange(list_Mst_CustomerGroupExt_Async.Result);
                }
                if (list_Mst_CustomerSourceExt_Async != null && list_Mst_CustomerSourceExt_Async.Result != null &&
                    list_Mst_CustomerSourceExt_Async.Result.Count > 0)
                {
                    listMst_CustomerSource.AddRange(list_Mst_CustomerSourceExt_Async.Result);
                }
                if (list_Mst_Province_Async != null && list_Mst_Province_Async.Result != null &&
                    list_Mst_Province_Async.Result.Count > 0)
                {
                    listMst_Province.AddRange(list_Mst_Province_Async.Result);
                }

                if (list_Mst_AreaExt_Async != null && list_Mst_AreaExt_Async.Result != null &&
                    list_Mst_AreaExt_Async.Result.Count > 0)
                {
                    listMst_AreaExt.AddRange(list_Mst_AreaExt_Async.Result);
                }
                if (list_Mst_GovIDType_Async != null && list_Mst_GovIDType_Async.Result != null &&
                    list_Mst_GovIDType_Async.Result.Count > 0)
                {
                    listMst_GovIDType.AddRange(list_Mst_GovIDType_Async.Result);
                }
                if (list_Customer_DynamicField_Async != null && list_Customer_DynamicField_Async.Result != null &&
                    list_Customer_DynamicField_Async.Result.Count > 0)
                {
                    listCustomer_DynamicField.AddRange(list_Customer_DynamicField_Async.Result);
                }
                ViewBag.List_Mst_CustomerType = list_Mst_CustomerType;
                ViewBag.listMst_CustomerGroup = listMst_CustomerGroup;
                ViewBag.listMst_CustomerSource = listMst_CustomerSource;
                ViewBag.ListMst_Province = listMst_Province;

                ViewBag.ListMst_AreaExt = listMst_AreaExt;
                ViewBag.ListMst_GovIDType = listMst_GovIDType;
                ViewBag.ListCustomer_DynamicField = listCustomer_DynamicField;
                #endregion

                if (objRT_Mst_Customer_Async != null && objRT_Mst_Customer_Async.Result != null)
                {
                    if (objRT_Mst_Customer_Async.Result.Lst_Mst_Customer != null &&
                        objRT_Mst_Customer_Async.Result.Lst_Mst_Customer.Count > 0)
                    {
                        objMst_Customer = objRT_Mst_Customer_Async.Result.Lst_Mst_Customer[0];
                        if (objRT_Mst_Customer_Async.Result.Lst_Mst_CustomerInCustomerGroup != null && objRT_Mst_Customer_Async.Result.Lst_Mst_CustomerInCustomerGroup.Count > 0)
                        {
                            var lstCusGrp = objRT_Mst_Customer_Async.Result.Lst_Mst_CustomerInCustomerGroup
                                .Where(grp => grp.CustomerCodeSys.Equals(objMst_Customer.CustomerCodeSys))
                                .Select(cusgrp => cusgrp.CustomerGrpCode)
                                .ToList();
                            if (lstCusGrp != null && lstCusGrp.Count > 0)
                            {
                                objMst_Customer.CustomerGrpCode = string.Join(",", lstCusGrp.ToArray());
                            }
                        }

                        // Ảnh
                        if (objMst_Customer.CustomerAvatarPath != null)
                        {
                            objMst_Customer.CustomerAvatarPath = objMst_Customer.CustomerAvatarPath.ToString().Replace("\\", "/");
                            if (!Regex.IsMatch(objMst_Customer.CustomerAvatarPath.ToString(), "^https?://.*"))
                            {
                                var addressAPIsImg =
                                objMst_Customer.CustomerAvatarPath = "http://14.232.244.217:12088/idocNet.Test.ProductCenter.V10.Local.WA/api/file/".Replace("Local", CUtils.StrValue(UserState.Mst_NNT.OrgID)) + objMst_Customer.CustomerAvatarPath;
                            }
                        }
                        ViewBag.ProvinceCode = CUtils.StrValue(objMst_Customer.ProvinceCode);
                        ViewBag.DistrictCode = CUtils.StrValue(objMst_Customer.DistrictCode);
                        ViewBag.WardCode = CUtils.StrValue(objMst_Customer.WardCode);
                    }
                    if (objRT_Mst_Customer_Async.Result.Lst_UserOwner_Customer != null &&
                        objRT_Mst_Customer_Async.Result.Lst_UserOwner_Customer.Count > 0)
                    {
                        listUserOwner_Customer.AddRange(objRT_Mst_Customer_Async.Result.Lst_UserOwner_Customer);
                    }
                    var lst_Mst_CustomerInArea = new List<Mst_CustomerInArea>();
                    if (objRT_Mst_Customer_Async.Result.Lst_Mst_CustomerInArea != null &&
                        objRT_Mst_Customer_Async.Result.Lst_Mst_CustomerInArea.Count > 0)
                    {
                        lst_Mst_CustomerInArea.AddRange(objRT_Mst_Customer_Async.Result.Lst_Mst_CustomerInArea);
                    }

                    #region["Quận/ huyện"]
                    var strWhereClauseMst_District = strWhereClause_Mst_District(new Mst_District() { ProvinceCode = CUtils.StrValue(objRT_Mst_Customer_Async.Result.Lst_Mst_Customer[0].ProvinceCode), FlagActive = Client_Flag.Active });
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
                        Ft_WhereClause = strWhereClauseMst_District,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_District
                        Rt_Cols_Mst_District = "*",
                        Mst_District = null,
                    };
                    var list_Mst_District_Async = List_Mst_District_Async(objRQ_Mst_District);
                    #endregion
                    #region["Phường/ xã"]
                    var strWhereClauseMst_Ward = strWhereClause_Mst_Ward(new Mst_Ward()
                    {
                        ProvinceCode = CUtils.StrValue(objRT_Mst_Customer_Async.Result.Lst_Mst_Customer[0].ProvinceCode),
                        DistrictCode = CUtils.StrValue(objRT_Mst_Customer_Async.Result.Lst_Mst_Customer[0].DistrictCode),
                        FlagActive = Client_Flag.Active
                    });
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
                        Ft_WhereClause = strWhereClauseMst_Ward,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Ward
                        Rt_Cols_Mst_Ward = "*",
                        Mst_Ward = null,
                    };
                    var list_Mst_Ward_Async = List_Mst_Ward_Async(objRQ_Mst_Ward);
                    #endregion
                    await Task.WhenAll(list_Mst_District_Async, list_Mst_Ward_Async); 
                    if (list_Mst_District_Async != null && list_Mst_District_Async.Result != null &&
                        list_Mst_District_Async.Result.Count > 0)
                    {
                        listMst_District.AddRange(list_Mst_District_Async.Result);
                    }
                    if (list_Mst_Ward_Async != null && list_Mst_Ward_Async.Result != null &&
                        list_Mst_Ward_Async.Result.Count > 0)
                    {
                        listMst_Ward.AddRange(list_Mst_Ward_Async.Result);
                    }
                    ViewBag.ListMst_District = listMst_District;
                    ViewBag.ListMst_Ward = listMst_Ward;
                    ViewBag.Lst_Mst_CustomerInArea = lst_Mst_CustomerInArea;
                }
            }
            return View(objMst_Customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(string model)
        {
           var resultEntry = new JsonResultEntry() { Success = false };
            var strFt_Cols_Upd = "";
            var Tbl_Mst_Customer = TableName.Mst_Customer + ".";
            try
            {
                var objMst_CustomerUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerUI>(model);
                if (objMst_CustomerUIInput != null && !CUtils.IsNullOrEmpty(objMst_CustomerUIInput.CustomerCodeSys))
                {
                    #region["Hàng hóa"]
                    var objMst_CustomerSearch = new Mst_Customer()
                    {
                        CustomerCodeSys = CUtils.StrValue(objMst_CustomerUIInput.CustomerCodeSys),
                        CustomerCode = CUtils.StrValue(objMst_CustomerUIInput.CustomerCode),
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID)
                    };
                    var strWhereClauseMst_Customer = strWhereClause_Mst_Customer(objMst_CustomerSearch);
                    var objRQ_Mst_CustomerSearch = new RQ_Mst_Customer()
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
                        Ft_WhereClause = strWhereClauseMst_Customer,
                        Ft_Cols_Upd = null,
                        // RQ_Mst_Customer
                        Rt_Cols_Mst_Customer = "*",
                        Rt_Cols_UserOwner_Customer = "*",
                        Mst_Customer = null,
                        Lst_Mst_Customer = null,
                        Lst_UserOwner_Customer = null

                    };
                    var objRT_Mst_CustomerSearch = RT_Mst_Customer_Get(objRQ_Mst_CustomerSearch);
                    #endregion
                    var listUserOwner_Customer = new List<UserOwner_Customer>();
                    if (objRT_Mst_CustomerSearch.Lst_Mst_Customer != null &&
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer.Count == 1)
                    {
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerType = objMst_CustomerUIInput.CustomerType;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerName = objMst_CustomerUIInput.CustomerName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerNameEN = objMst_CustomerUIInput.CustomerName;
                        //objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerGrpCode = objMst_CustomerUIInput.CustomerGrpCode;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerSourceCode = objMst_CustomerUIInput.CustomerSourceCode;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerPhoneNo = objMst_CustomerUIInput.CustomerPhoneNo;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerMobilePhone = objMst_CustomerUIInput.CustomerMobilePhone;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAddress = objMst_CustomerUIInput.CustomerAddress;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].ProvinceCode = objMst_CustomerUIInput.ProvinceCode;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].DistrictCode = objMst_CustomerUIInput.DistrictCode;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].WardCode = objMst_CustomerUIInput.WardCode;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].AreaCode = objMst_CustomerUIInput.AreaCode;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].Remark = objMst_CustomerUIInput.Remark;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].RepresentName = objMst_CustomerUIInput.RepresentName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].RepresentPosition = objMst_CustomerUIInput.RepresentPosition;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].GovIDCardNo = objMst_CustomerUIInput.GovIDCardNo;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].GovIDType = objMst_CustomerUIInput.GovIDType;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].BankAccountNo = objMst_CustomerUIInput.BankAccountNo;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].BankName = objMst_CustomerUIInput.BankName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].BankCode = objMst_CustomerUIInput.BankName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].ContactName = objMst_CustomerUIInput.ContactName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].ContactPhone = objMst_CustomerUIInput.ContactPhone;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].ContactEmail = objMst_CustomerUIInput.ContactEmail;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].Fax = objMst_CustomerUIInput.Fax;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerDateOfBirth = objMst_CustomerUIInput.CustomerDateOfBirth;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].Facebook = objMst_CustomerUIInput.Facebook;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].InvoiceCustomerName = objMst_CustomerUIInput.InvoiceCustomerName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].InvoiceOrgName = objMst_CustomerUIInput.InvoiceOrgName;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].MST = objMst_CustomerUIInput.MST;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerGender = objMst_CustomerUIInput.CustomerGender;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].InvoiceCustomerAddress = objMst_CustomerUIInput.InvoiceCustomerAddress;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].InvoiceEmailSend = objMst_CustomerUIInput.InvoiceEmailSend;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagDealer = objMst_CustomerUIInput.FlagDealer;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagBank = objMst_CustomerUIInput.FlagBank;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagEndUser = objMst_CustomerUIInput.FlagEndUser;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagInsurrance = objMst_CustomerUIInput.FlagInsurrance;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagSupplier = objMst_CustomerUIInput.FlagSupplier;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagShipper = objMst_CustomerUIInput.FlagShipper;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagActive = objMst_CustomerUIInput.FlagActive;
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].ListOfCustDynamicFieldValue = objMst_CustomerUIInput.ListOfCustDynamicFieldValue;
                        if (CUtils.IsNullOrEmpty(objMst_CustomerUIInput.CustomerAvatarSpec))
                        {
                            objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagCustomerAvatarPath = "1";
                            // Nếu xoá ảnh -> null
                            if (objMst_CustomerUIInput.CustomerAvatarSpec == null)
                            {
                                objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAvatarSpec = null;
                                objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAvatarPath = null;
                                objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAvatarName = null;
                            }
                        }
                        else
                        {
                            objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].FlagCustomerAvatarPath = "0";
                            objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAvatarPath = objMst_CustomerUIInput.CustomerAvatarPath;
                            objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAvatarSpec = objMst_CustomerUIInput.CustomerAvatarSpec;
                            objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].CustomerAvatarName = objMst_CustomerUIInput.CustomerAvatarName;
                        }

                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerType);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerName);
                        //strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerGrpCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerNameEN);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.Remark);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerSourceCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerPhoneNo);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerMobilePhone);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerAddress);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.ProvinceCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.DistrictCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.WardCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.AreaCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.RepresentName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.RepresentPosition);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.GovIDCardNo);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.GovIDType);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.BankAccountNo);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.BankName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.BankCode);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.ContactName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.ContactPhone);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.ContactEmail);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.Fax);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerDateOfBirth);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.Facebook);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.InvoiceCustomerName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.InvoiceOrgName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.MST);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.CustomerGender);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.InvoiceCustomerAddress);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.InvoiceEmailSend);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.InvoiceOrgName);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.FlagDealer);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.FlagEndUser);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.FlagBank);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.FlagSupplier);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.FlagShipper);
                        strFt_Cols_Upd += string.Format("{0}{1},", Tbl_Mst_Customer, TblMst_Customer.FlagInsurrance);
                        strFt_Cols_Upd += string.Format("{0}{1}", Tbl_Mst_Customer, TblMst_Customer.FlagActive);
                        strFt_Cols_Upd += string.Format("{0}{1}", Tbl_Mst_Customer, TblMst_Customer.ListOfCustDynamicFieldValue);

                        if (objRT_Mst_CustomerSearch.Lst_UserOwner_Customer != null &&
                            objRT_Mst_CustomerSearch.Lst_UserOwner_Customer.Count > 0)
                        {
                            listUserOwner_Customer.AddRange(objRT_Mst_CustomerSearch.Lst_UserOwner_Customer);
                        }

                        var listCustomerGrpCode = objMst_CustomerUIInput.CustomerGrpCode?.ToString().Split(',');
                        var LstMst_CustomerInCustomerGroup = new List<Mst_CustomerInCustomerGroup>();
                        //2021-04-27: Check nhóm khách hàng nếu có dữ liệu thì for
                        if(listCustomerGrpCode!=null && listCustomerGrpCode.Length > 0)
                        {
                            foreach (var grpcode in listCustomerGrpCode)
                            {
                                var objMst_CustomerInCustomerGroup = new Mst_CustomerInCustomerGroup
                                {
                                    CustomerCodeSys = objMst_CustomerUIInput.CustomerCodeSys,
                                    CustomerGrpCode = grpcode,
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                                };
                                LstMst_CustomerInCustomerGroup.Add(objMst_CustomerInCustomerGroup);
                            }
                        }


                        #region["Vùng thị trường"]
                        var listAreaCode = objMst_CustomerUIInput.AreaCode?.ToString().Split(',');
                        var Lst_Mst_CustomerInArea = new List<Mst_CustomerInArea>();
                        if (listAreaCode != null && listAreaCode.Length > 0)
                        {
                            foreach (var areacode in listAreaCode)
                            {
                                var objMst_CustomerInArea = new Mst_CustomerInArea
                                {
                                    CustomerCodeSys = CUtils.StrValue(objMst_CustomerUIInput.CustomerCodeSys),
                                    AreaCode = areacode,
                                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID)
                                };
                                Lst_Mst_CustomerInArea.Add(objMst_CustomerInArea);
                            }
                        }
                        objMst_CustomerUIInput.AreaCode = "";



                        #endregion

                        var objRQ_Mst_Customer = new RQ_Mst_Customer()
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
                            //Ft_WhereClause = strFt_Cols_Upd,
                            Ft_Cols_Upd = strFt_Cols_Upd,
                            // RQ_Mst_Customer
                            Rt_Cols_Mst_Customer = null,
                            Rt_Cols_UserOwner_Customer = null,
                            Mst_Customer = null,
                            Lst_Mst_Customer = new List<Mst_Customer>(),
                            Lst_UserOwner_Customer = new List<UserOwner_Customer>(),
                        };
                        objRT_Mst_CustomerSearch.Lst_Mst_Customer[0].AreaCode = "";
                        objRQ_Mst_Customer.Lst_Mst_Customer.Add(objRT_Mst_CustomerSearch.Lst_Mst_Customer[0]);
                        objRQ_Mst_Customer.Lst_UserOwner_Customer.AddRange(listUserOwner_Customer);
                        objRQ_Mst_Customer.Lst_Mst_CustomerInCustomerGroup = LstMst_CustomerInCustomerGroup;
                        objRQ_Mst_Customer.Lst_Mst_CustomerInArea = Lst_Mst_CustomerInArea; //vùng thị trường
                        var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_Customer);
                        Mst_CustomerService.Instance.WA_Mst_Customer_Update(objRQ_Mst_Customer, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                        // Lưu tại network
                        objRQ_Mst_Customer.GwUserCode = GwUserCode;
                        objRQ_Mst_Customer.GwPassword = GwPassword;
                        objRQ_Mst_Customer.WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode);
                        objRQ_Mst_Customer.WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword);
                        objRQ_Mst_Customer.FlagIsDelete = "0";
                        Mst_CustomerService.Instance.WA_Mst_Customer_Save(objRQ_Mst_Customer, UserState.AddressAPIs);
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Sửa thông tin khách hàng thành công!");
                        var flagRedirect = CUtils.StrValue(objMst_CustomerUIInput.FlagRedirect);
                        if (flagRedirect.Equals("1"))
                        {
                            resultEntry.RedirectUrl = Url.Action("Update", "Mst_Customer", new { customercode = CUtils.StrValue(objMst_CustomerUIInput.CustomerCode) });
                        }
                        else
                        {
                            resultEntry.RedirectUrl = Url.Action("Index");
                        }
                    }
                    else
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("Mã khách hàng '" + objMst_CustomerUIInput.CustomerCode + "' không có trong hệ thống!");
                    }
                }
                else
                {
                    resultEntry.Success = true;
                    resultEntry.AddMessage("Mã khách hàng không hợp lệ!");
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
        #region["Customer_DynamicField"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Customer_DynamicField_Create(string model)
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objParamType = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerDynamicFieldParamType>(model);
                var listCustomer_DynamicField = new List<Customer_DynamicField>();


                var objCustomer_DynamicFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer_DynamicField>(model);
                var strWhereClauseCustomer_DynamicField = strWhereClause_Customer_DynamicField(new Customer_DynamicField()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active,
                });
                var objRQ_Customer_DynamicField = new RQ_Customer_DynamicField
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
                    Ft_WhereClause = strWhereClauseCustomer_DynamicField,
                    Ft_Cols_Upd = null,
                    // RQ_Customer_DynamicField
                    Rt_Cols_Customer_DynamicField = "*",
                    Customer_DynamicField = null
                };
                listCustomer_DynamicField = WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField);
                if (listCustomer_DynamicField != null && listCustomer_DynamicField.Count > 0)
                {
                    if (listCustomer_DynamicField[0].ParamTypes != null && listCustomer_DynamicField[0].ParamTypes.Count > 0)
                    {
                        var paramTypes = new List<CustomerDynamicFieldParamType>();
                        paramTypes.AddRange(listCustomer_DynamicField[0].ParamTypes);
                        paramTypes.Add(objParamType);

                        if (listCustomer_DynamicField[0].ParamTypes.Where(x => x.Code.Equals(objParamType.Code)).ToList().Count == 0)
                        {
                            #region["Xóa DynamicField cũ"]
                            var customer_DynamicFieldDel = new Customer_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                            };
                            var objRQ_Customer_DynamicFieldDel = new RQ_Customer_DynamicField()
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
                                // RQ_Customer_DynamicField
                                Customer_DynamicField = customer_DynamicFieldDel,
                            };
                            Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Delete(objRQ_Customer_DynamicFieldDel, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion

                            #region["Tạo DynamicField mới có add thêm objParamType"]
                            var customer_DynamicFieldCreate = new Customer_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                                Detail = "",
                                ParamTypes = paramTypes
                            };
                            var objRQ_Customer_DynamicFieldCreate = new RQ_Customer_DynamicField()
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
                                // RQ_Customer_DynamicField
                                Customer_DynamicField = customer_DynamicFieldCreate,
                            };
                            Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Create(objRQ_Customer_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion
                            resultEntry.Success = true;
                        }
                        else
                        {
                            resultEntry.Success = false;
                            resultEntry.AddMessage("Mã thông tin đã tồn tại!");
                        }
                    }
                }
                else
                {
                    #region["Tạo DynamicField mới có add thêm objParamType"]
                    var customer_DynamicFieldCreate = new Customer_DynamicField()
                    {
                        OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                        Detail = "",
                        ParamTypes = new List<CustomerDynamicFieldParamType>(),
                    };
                    customer_DynamicFieldCreate.ParamTypes.Add(objParamType);
                    var objRQ_Customer_DynamicFieldCreate = new RQ_Customer_DynamicField()
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
                        // RQ_Customer_DynamicField
                        Customer_DynamicField = customer_DynamicFieldCreate,
                    };
                    Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Create(objRQ_Customer_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                    #endregion
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới thông tin bổ sung thành công!");
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Customer_DynamicField_Update(string model)
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objParamType = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerDynamicFieldParamType>(model);
                var listCustomer_DynamicField = new List<Customer_DynamicField>();


                var objCustomer_DynamicFieldInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Customer_DynamicField>(model);
                var strWhereClauseCustomer_DynamicField = strWhereClause_Customer_DynamicField(new Customer_DynamicField()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active,
                });
                var objRQ_Customer_DynamicField = new RQ_Customer_DynamicField
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
                    Ft_WhereClause = strWhereClauseCustomer_DynamicField,
                    Ft_Cols_Upd = null,
                    // RQ_Customer_DynamicField
                    Rt_Cols_Customer_DynamicField = "*",
                    Customer_DynamicField = null
                };
                listCustomer_DynamicField = WA_Customer_DynamicField_Get(objRQ_Customer_DynamicField);
                if (listCustomer_DynamicField != null && listCustomer_DynamicField.Count > 0)
                {
                    if (listCustomer_DynamicField[0].ParamTypes != null && listCustomer_DynamicField[0].ParamTypes.Count > 0)
                    {
                        var paramTypes = new List<CustomerDynamicFieldParamType>();
                        foreach (var item in listCustomer_DynamicField[0].ParamTypes)
                        {
                            if (!item.Code.Equals(objParamType.Code))
                            {
                                paramTypes.Add(item);
                            }
                            else
                            {
                                paramTypes.Add(objParamType);
                            }
                        }

                        if (listCustomer_DynamicField[0].ParamTypes.Where(x => x.Code.Equals(objParamType.Code)).ToList().Count > 0)
                        {
                            #region["Xóa DynamicField cũ"]
                            var customer_DynamicFieldDel = new Customer_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID)
                            };
                            var objRQ_Customer_DynamicFieldDel = new RQ_Customer_DynamicField()
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
                                // RQ_Customer_DynamicField
                                Customer_DynamicField = customer_DynamicFieldDel,
                            };
                            Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Delete(objRQ_Customer_DynamicFieldDel, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion

                            #region["Tạo DynamicField mới có add thêm objParamType"]
                            var customer_DynamicFieldCreate = new Customer_DynamicField()
                            {
                                OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                                Detail = "",
                                ParamTypes = paramTypes
                            };
                            var objRQ_Customer_DynamicFieldCreate = new RQ_Customer_DynamicField()
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
                                // RQ_Customer_DynamicField
                                Customer_DynamicField = customer_DynamicFieldCreate,
                            };
                            Customer_DynamicFieldService.Instance.WA_Customer_DynamicField_Create(objRQ_Customer_DynamicFieldCreate, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                            #endregion
                            resultEntry.Success = true;
                        }
                        else
                        {
                            resultEntry.Success = false;
                            resultEntry.AddMessage("Mã thông tin đã tồn tại!");
                        }
                    }
                }
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới thông tin bổ sung thành công!");
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
        #region["Common"]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomerGroup(string model, string modelimages)
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_CustomerGroupInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerGroup>(model);
                var listMst_CustomerGroupImagesInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_CustomerGroupImages>>(modelimages);
                var customerGrpCode = WA_Seq_GenCommonCode_Get(SeqType.CustomerGrpCode);
                objMst_CustomerGroupInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objMst_CustomerGroupInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objMst_CustomerGroupInput.CustomerGrpCode = customerGrpCode;
                if (CUtils.IsNullOrEmpty(objMst_CustomerGroupInput.CustomerGrpCodeParent))
                {
                    objMst_CustomerGroupInput.CustomerGrpCodeParent = "ALL";
                }
                var objRQ_Mst_CustomerGroup = new RQ_Mst_CustomerGroup()
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = null,
                    Rt_Cols_Mst_CustomerGroupImages = null,
                    Mst_CustomerGroup = objMst_CustomerGroupInput,
                    Lst_Mst_CustomerGroupImages = new List<Mst_CustomerGroupImages>(),
                };
                if (listMst_CustomerGroupImagesInput != null && listMst_CustomerGroupImagesInput.Count > 0)
                {
                    var i = 0;
                    foreach (var item in listMst_CustomerGroupImagesInput)
                    {
                        item.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                        item.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                        item.CustomerGrpCode = customerGrpCode;
                        item.Idx = i.ToString();
                        if (i == 0)
                        {
                            item.FlagPrimaryImage = "1"; // tạm thời set là ảnh đầu tiên hiển thị
                        }

                        i++;
                    }
                    objRQ_Mst_CustomerGroup.Lst_Mst_CustomerGroupImages.AddRange(listMst_CustomerGroupImagesInput);
                }
                Mst_CustomerGroupService.Instance.WA_Mst_CustomerGroup_Create(objRQ_Mst_CustomerGroup, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                #region["Nhóm khách hàng"]
                var strWhereClauseMst_CustomerGroup = strWhereClause_Mst_CustomerGroup(new Mst_CustomerGroup()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
                var objRQ_Mst_CustomerGroupSearch = new RQ_Mst_CustomerGroup()
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
                    Ft_WhereClause = strWhereClauseMst_CustomerGroup,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerGroup
                    Rt_Cols_Mst_CustomerGroup = "*",
                    Rt_Cols_Mst_CustomerGroupImages = "*",
                    Mst_CustomerGroup = null,
                    Lst_Mst_CustomerGroupImages = null,
                };
                var list_Mst_CustomerGroupExt_Async = List_Mst_CustomerGroup_Async(objRQ_Mst_CustomerGroupSearch);
                #endregion
                var listMst_CustomerGroup = new List<Mst_CustomerGroup>();
                if (list_Mst_CustomerGroupExt_Async != null && list_Mst_CustomerGroupExt_Async.Result != null &&
                    list_Mst_CustomerGroupExt_Async.Result.Count > 0)
                {
                    listMst_CustomerGroup.AddRange(list_Mst_CustomerGroupExt_Async.Result);
                }
                ViewBag.listMst_CustomerGroup = listMst_CustomerGroup;
                ViewBag.CustomerGrpCode = customerGrpCode;
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới nhóm khách hàng thành công!");
                return JsonView("~/Views/LoadData/LoadMst_CustomerGroup.cshtml", listMst_CustomerGroup);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomerSource(string model, string modelimages)
        {
            
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_CustomerSourceInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_CustomerSource>(model);
                var listMst_CustomerSourceImagesInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Mst_CustomerSourceImages>>(modelimages);
                var customerSourceCode = WA_Seq_GenCommonCode_Get(SeqType.CustomerSourceCode);
                objMst_CustomerSourceInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objMst_CustomerSourceInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objMst_CustomerSourceInput.CustomerSourceCode = customerSourceCode;

                if (CUtils.IsNullOrEmpty(objMst_CustomerSourceInput.CustomerSourceCodeParent))
                {
                    objMst_CustomerSourceInput.CustomerSourceCodeParent = "ALL";
                }
                var objRQ_Mst_CustomerSource = new RQ_Mst_CustomerSource()
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = null,
                    Rt_Cols_Mst_CustomerSourceImages = null,
                    Mst_CustomerSource = objMst_CustomerSourceInput,
                    Lst_Mst_CustomerSourceImages = new List<Mst_CustomerSourceImages>(),
                };
                if (listMst_CustomerSourceImagesInput != null && listMst_CustomerSourceImagesInput.Count > 0)
                {
                    var i = 0;
                    foreach (var item in listMst_CustomerSourceImagesInput)
                    {
                        item.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                        item.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                        item.CustomerSourceCode = customerSourceCode;
                        item.Idx = i.ToString();
                        if (i == 0)
                        {
                            item.FlagPrimaryImage = "1"; // tạm thời set là ảnh đầu tiên hiển thị
                        }

                        i++;
                    }
                    objRQ_Mst_CustomerSource.Lst_Mst_CustomerSourceImages.AddRange(listMst_CustomerSourceImagesInput);
                }
                Mst_CustomerSourceService.Instance.WA_Mst_CustomerSource_Create(objRQ_Mst_CustomerSource, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));
                #region["Nhóm khách hàng"]
                var strWhereClauseMst_CustomerSource = strWhereClause_Mst_CustomerSource(new Mst_CustomerSource()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
                var objRQ_Mst_CustomerSourceSearch = new RQ_Mst_CustomerSource()
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
                    Ft_WhereClause = strWhereClauseMst_CustomerSource,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_CustomerSource
                    Rt_Cols_Mst_CustomerSource = "*",
                    Rt_Cols_Mst_CustomerSourceImages = "*",
                    Mst_CustomerSource = null,
                    Lst_Mst_CustomerSourceImages = null,
                };
                var list_Mst_CustomerSourceExt_Async = List_Mst_CustomerSource_Async(objRQ_Mst_CustomerSourceSearch);
                #endregion
                var listMst_CustomerSource = new List<Mst_CustomerSource>();
                if (list_Mst_CustomerSourceExt_Async != null && list_Mst_CustomerSourceExt_Async.Result != null &&
                    list_Mst_CustomerSourceExt_Async.Result.Count > 0)
                {
                    listMst_CustomerSource.AddRange(list_Mst_CustomerSourceExt_Async.Result);
                }
                ViewBag.listMst_CustomerSource = listMst_CustomerSource;
                ViewBag.CustomerSourceCode = customerSourceCode;
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới nguồn khách hàng thành công!");
                return JsonView("~/Views/LoadData/LoadMst_CustomerSource.cshtml", listMst_CustomerSource);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddArea(string model)
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var objMst_AreaInput = Newtonsoft.Json.JsonConvert.DeserializeObject<Mst_Area>(model);
                var areaCode = WA_Seq_GenCommonCode_Get(SeqType.AreaCode);
                objMst_AreaInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objMst_AreaInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objMst_AreaInput.AreaCode = areaCode;
                if (CUtils.IsNullOrEmpty(objMst_AreaInput.AreaCodeParent))
                {
                    objMst_AreaInput.AreaCodeParent = "ALL";
                }
                var objRQ_Mst_Area = new RQ_Mst_Area()
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
                    Ft_WhereClause = null,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = null,
                    Mst_Area = objMst_AreaInput,
                };

                Mst_AreaService.Instance.WA_Mst_Area_Create(objRQ_Mst_Area, CUtils.StrValue(UserState.AddressAPIs_Product_Customer_Centrer));

                #region["Xử lý để đợi đồng bộ khu vực"]


                System.Threading.Thread.Sleep(5000);
                //await Task.Delay(5000);
                #endregion


                #region["Nhóm khách hàng"] 
                var strWhereClauseMst_Area = strWhereClause_Mst_Area(new Mst_Area()
                {
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    FlagActive = Client_Flag.Active
                });
                var objRQ_Mst_AreaSearch = new RQ_Mst_Area()
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
                    Ft_WhereClause = strWhereClauseMst_Area,
                    Ft_Cols_Upd = null,
                    // RQ_Mst_Area
                    Rt_Cols_Mst_Area = "*",
                    Mst_Area = null,
                };
                var list_Mst_AreaExt_Async = List_Mst_AreaExt_Async(objRQ_Mst_AreaSearch);
                #endregion
                var listMst_AreaExt = new List<Mst_AreaExt>();
                if (list_Mst_AreaExt_Async != null && list_Mst_AreaExt_Async.Result != null &&
                    list_Mst_AreaExt_Async.Result.Count > 0)
                {
                    listMst_AreaExt.AddRange(list_Mst_AreaExt_Async.Result);
                }
                ViewBag.ListMst_AreaExt = listMst_AreaExt;
                ViewBag.AreaCode = areaCode;
                resultEntry.Success = true;
                resultEntry.AddMessage("Tạo mới khu vực thành công!");
                return JsonView("~/Views/LoadData/LoadMst_Area.cshtml", listMst_AreaExt);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            if (resultEntry.Detail != null && !CUtils.IsNullOrEmpty(resultEntry.Detail))
            {
                resultEntry.Detail = resultEntry.Detail;
            }
            return JsonViewError("Index", null, resultEntry);
        }
        #endregion
        #region[""]
        private string strWhereClause_Mst_Customer_By_Or(Mst_Customer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Customer = TableName.Mst_Customer + ".";
            var abc = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.CustomerMobilePhone))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerMobilePhone, CUtils.StrValue(data.CustomerMobilePhone), "=", "or");
            }
            abc = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.ContactEmail))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.ContactEmail, CUtils.StrValue(data.ContactEmail), "=", "or");
            }
            abc = sbSql.ToString();
            if (!CUtils.IsNullOrEmpty(data.MST))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.MST, CUtils.StrValue(data.MST), "=", "or");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Customer(Mst_Customer data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Customer = TableName.Mst_Customer + ".";
            if (!CUtils.IsNullOrEmpty(data.CustomerCodeSys))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerCodeSys, CUtils.StrValue(data.CustomerCodeSys), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.CustomerCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.CustomerCode, CUtils.StrValue(data.CustomerCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Customer + TblMst_Customer.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_CustomerGroup(Mst_CustomerGroup data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CustomerGroup = TableName.Mst_CustomerGroup + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerGroup + TblMst_CustomerGroup.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerGroup + TblMst_CustomerGroup.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerGroup + TblMst_CustomerGroup.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_CustomerType(Common.Models.Mst_CustomerType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CustomerType = TableName.Mst_CustomerType + ".";
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerType + TblMst_CustomerType.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerType + TblMst_CustomerType.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_CustomerSource(Mst_CustomerSource data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_CustomerSource = TableName.Mst_CustomerSource + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerSource + TblMst_CustomerSource.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerSource + TblMst_CustomerSource.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_CustomerSource + TblMst_CustomerSource.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Customer_DynamicField(Customer_DynamicField data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Customer_DynamicField = TableName.Customer_DynamicField + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Customer_DynamicField + TblCustomer_DynamicField.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Customer_DynamicField + TblCustomer_DynamicField.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Customer_DynamicField + TblCustomer_DynamicField.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }

            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_GovIDType(Mst_GovIDType data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_GovIDType = TableName.Mst_GovIDType + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovIDType + TblMst_GovIDType.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_GovIDType + TblMst_GovIDType.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Province(Mst_Province data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Province = TableName.Mst_Province + ".";
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Province + TblMst_Province.FlagActive, CUtils.StrValue(data.FlagActive), "=");
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
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_District + TblMst_District.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Ward(Mst_Ward data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Ward = TableName.Mst_Ward + ".";
            if (!CUtils.IsNullOrEmpty(data.ProvinceCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.ProvinceCode, CUtils.StrValue(data.ProvinceCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.DistrictCode))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.DistrictCode, CUtils.StrValue(data.DistrictCode), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Ward + TblMst_Ward.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        private string strWhereClause_Mst_Area(Mst_Area data)
        {
            var strWhereClause = "";
            var sbSql = new StringBuilder();
            var Tbl_Mst_Area = TableName.Mst_Area + ".";
            if (!CUtils.IsNullOrEmpty(data.OrgID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.OrgID, CUtils.StrValue(data.OrgID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.NetworkID))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.NetworkID, CUtils.StrValue(data.NetworkID), "=");
            }
            if (!CUtils.IsNullOrEmpty(data.FlagActive))
            {
                sbSql.AddWhereClause(Tbl_Mst_Area + TblMst_Area.FlagActive, CUtils.StrValue(data.FlagActive), "=");
            }
            strWhereClause = sbSql.ToString();
            return strWhereClause;
        }
        #endregion
    }
}