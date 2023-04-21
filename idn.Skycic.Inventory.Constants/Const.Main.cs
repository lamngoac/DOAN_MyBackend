
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace idn.Skycic.Inventory.Constants
{
    public static class Logs
    {
        public static string PathFolderLogs = "";
    }
    public static class ServiceAddress
    {
        public static string BaseServiceAddress = "";
        public static string BaseMasterServerAPIAddress = "";
        public static string BaseReportServerAPIAddress = "";
        public static string BaseTCTServerAPIAddress = "";
        public static string BasePayGateAddress = "";
        public static string BaseINOSAddress = "";
        public static string BaseAPIInbrand = "";

        public static string BaseAPIInbrandSolution = "";
        public static string BaseAPIDMSSolution = "";
        public static string BaseMasterServerProduct_Customer_CenterAPIAddress = "";
        public static string BaseMasterServerVelocaAPIAddress = "";
        public static string BaseMasterServerDMSAPIAddress = "";
        public static string BaseMasterServerLQDMSAPIAddress = "";

        public static string BaseAPIProductCenter = "";
    }
    public static class BizMasterServerPrdCenterAPIAddress
    {
        public static string BaseBizMasterServerAPIAddress = "";
    }
    public static class BizProductCenterAPIAddress
    {
        public static string BaseBizProductCenterAPIAddress = "";
    }
    public static class BizMasterServerSolutionAPIAddress
    {
        public static string BaseBizMasterServerSolutionAPIAddress = "";
    }

    public static class FontPath
    {
        public static string FontTimeNewRomanPath = "";
        public static string FontArialPath = "";
        public static string FontChakraPetchBold = "";
    }
    public class Client_Flag
    {
        public const string Active = "1";
        public const string Inactive = "0";
    }
    public static class ClientMix
    {
        public static string MSTRoot = "ALL";
    }

    public static class InvoiceTemplateType
    {
        public static string InvoiceTemType = "FREE";
    }


    public class CLientTypeData_NameVN
    {
        public const string DATETIME = "Thời gian";
        public const string DATE = "Ngày";
        public const string REALNUMBER = "Số thực";
        public const string INTEGERNUMBER = "Số nguyên";
        public const string TEXT = "Xâu ký tự";
        public const string NUMBER = "Số";
    }

    public class CLientTypeData
    {
        public const string DATETIME = "DATETIME";
        public const string DATE = "DATE";
        public const string REALNUMBER = "REALNUMBER";
        public const string INTEGERNUMBER = "INTEGERNUMBER";
        public const string TEXT = "TEXT";
        public const string NUMBER = "NUMBER";
    }

    public class SignConfig
    {
        public const string linkgetCertificationServer = "http://14.232.244.217:12888/api/Signature/GetCertificateInfo";
        public const string linkSignatureServer = "http://14.232.244.217:12888/api/Signature/SignData";
        public const string linkgetCertificationLocal = "http://localhost:12888/api/Signature/GetCertificateInfo";
        public const string linkSignatureLocal = "http://localhost:12888/api/Signature/SignData";
    }

    public class APIAddressType
    {
        public const string MASTERSERVER = "MASTERSERVER";
    }
    public class TCFType
    {
        public const string MASTER = "MASTER";
        public const string DETAIL = "DETAIL";
    }

    public class ClientInvoiceStatus_NameVN
    {
        public const string PENDING = "Chờ ký";
        public const string APPROVED = "Đã ký";
        public const string ISSUED = "Đã phát hành";
        public const string CANCELED = "Đã hủy";
        public const string DELETED = "Đã xóa bỏ";
    }
    public class Client_CommissionStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }
    public class ClientSourceInvoiceCode_NameVN
    {
        public const string INVOICEROOT = "Hóa đơn gốc";
        public const string INVOICEREPLACE = "Hóa đơn thay thế";
        public const string INVOICEADJ = "Hóa đơn điều chỉnh";
    }

    public class ClientInvoiceAdjType_NameVN
    {
        public const string NORMAL = "Bình thường";
        public const string ADJINCREASE = "Tăng";
        public const string ADJDESCREASE = "Giảm";
    }

    public class ClientTypeData_NameVN
    {
        public const string DATETIME = "Thời gian";
        public const string DATE = "Ngày";
        public const string REALNUMBER = "Số thực";
        public const string INTEGERNUMBER = "Số nguyên";
        public const string TEXT = "Sâu ký tự";
    }

    public class ClientTypeData
    {
        public const string DATETIME = "DATETIME";
        public const string DATE = "DATE";
        public const string REALNUMBER = "REALNUMBER";
        public const string INTEGERNUMBER = "INTEGERNUMBER";
        public const string TEXT = "TEXT";
    }
    public class Client_Status
    {
        public const string PENDING = "PENDING";
        public const string APPROVE = "APPROVE";
        public const string CANCEL = "CANCEL";
        public const string FINISH = "FINISH";
    }
    public class SolutionNameCloud
    {
        public const string HDDT = "Hóa đơn";
        public const string QINVOICE = "Hóa đơn";
        public const string INBRAND = "Tem thông minh";
        public const string DMSPLUS = "Kênh phân phối";
        public const string QCONTRACT = "Hợp đồng";
        public const string INVENTORY = "Kho";
        public const string SKYCIC = "Skycic";
        public const string PRODUCTCENTER = "Hàng hóa";
        public const string CUSTOMERCENTER = "Đối tác";
        public const string PRDCENTER = "Hàng hóa";
        public const string CUSCENTER = "Đối tác";
        public const string ICIC = "Skycic";
        public const string VELOCA = "Veloca";
        public const string TRACEINFO = "Traceinfo";
        public const string DASHBOARD = "Dashboard";
        public const string HTKK = "Kê khai thuế";
        public const string REPORTSERVICE = "Report";
        public const string QSERVICE = "Service";
        public const string LOGISTIC = "Logistic";
        public const string XNK = "XNK";
        public const string LQDMS = "LQ DMS";
        public const string ETEMNN = "eTem nông nghiệp";
        public const string FINAS = "Finas";
    }

    public class SolutionCodeCloud
    {
        public const string HDDT = "HDDT";
        public const string QINVOICE = "QINVOICE";
        public const string INBRAND = "INBRAND";
        public const string DMSPLUS = "DMSPLUS";
        public const string QCONTRACT = "QCONTRACT";
        public const string INVENTORY = "INVENTORY";
        public const string SKYCIC = "SKYCIC";
        public const string PRODUCTCENTER = "PRODUCTCENTER";
        public const string CUSTOMERCENTER = "CUSTOMERCENTER";
        public const string PRDCENTER = "PRDCENTER";
        public const string CUSCENTER = "CUSCENTER";
        public const string ICIC = "ICIC";
        public const string VELOCA = "VELOCA";
        public const string TRACEINFO = "TRACEINFO";
        public const string DASHBOARD = "DASHBOARD";
        public const string HTKK = "HTKK";
        public const string REPORTSERVICE = "REPORTSERVICE";
        public const string QSERVICE = "QSERVICE";
        public const string LOGISTIC = "LOGISTIC";
        public const string XNK = "XNK";
        public const string LQDMS = "LQDMS";
        public const string ETEMNN = "ETEMNN";
        public const string FINAS = "FINAS";
    }

    public class SolutionImageNavbar
    {
        public const string HDDT = "/images/SolutionImage/logo_Qinvoice.svg";
        public const string QINVOICE = "/images/SolutionImage/logo_Qinvoice.svg";
        public const string INBRAND = "/images/SolutionImage/logo_eTem.svg";
        public const string DMSPLUS = "/images/SolutionImage/logo_DMS.svg";
        public const string QCONTRACT = "/images/SolutionImage/logo_Qcontract.svg";
        public const string INVENTORY = "/images/SolutionImage/logo_inventory.svg";
        public const string SKYCIC = "/images/SolutionImage/logo_skycic.svg";
        public const string PRODUCTCENTER = "/images/SolutionImage/logo_product.svg";
        public const string CUSTOMERCENTER = "/images/SolutionImage/logo_Customer.svg";
        public const string PRDCENTER = "/images/SolutionImage/logo_product.svg";
        public const string CUSCENTER = "/images/SolutionImage/logo_Customer.svg";
        public const string ICIC = "/images/SolutionImage/logo_skycic.svg";
        public const string VELOCA = "/images/SolutionImage/logo_veloca.svg";
        public const string TRACEINFO = "/images/SolutionImage/logo_traceinfo.svg";
        public const string DASHBOARD = "/images/SolutionImage/logo_ecore.svg";
        public const string HTKK = "/images/SolutionImage/logo_htkk.svg";
        public const string REPORTSERVICE = "/images/SolutionImage/logo_reportservice.svg";
        public const string QSERVICE = "/images/SolutionImage/logo_qservice.svg";
        public const string LOGISTIC = "/images/SolutionImage/logo_logistic.svg";
        public const string XNK = "/images/SolutionImage/logo_importexport.svg";
        public const string LQDMS = "/images/SolutionImage/logo_dmslq.svg";
        public const string ETEMNN = "/images/SolutionImage/logo_etemnn.svg";
        public const string FINAS = "/images/SolutionImage/logo_finas.svg";
    }

    public class SolutionImageSlidebar
    {
        public const string HDDT = "/images/SolutionImage/logo_Qinvoice.svg";
        public const string QINVOICE = "/images/SolutionImage/logo_Qinvoice.svg";
        public const string INBRAND = "/images/SolutionImage/logo_eTem.svg";
        public const string DMSPLUS = "/images/SolutionImage/logo_DMS.svg";
        public const string QCONTRACT = "/images/SolutionImage/logo_Qcontract.svg";
        public const string INVENTORY = "/images/SolutionImage/logo_inventory.svg";
        public const string SKYCIC = "/images/SolutionImage/logo_skycic.svg";
        public const string PRODUCTCENTER = "/images/SolutionImage/logo_product.svg";
        public const string CUSTOMERCENTER = "/images/SolutionImage/logo_Customer.svg";
        public const string PRDCENTER = "/images/SolutionImage/logo_product.svg";
        public const string CUSCENTER = "/images/SolutionImage/logo_Customer.svg";
        public const string ICIC = "/images/SolutionImage/logo_skycic.svg";
        public const string VELOCA = "/images/SolutionImage/logo_veloca.svg";
        public const string TRACEINFO = "/images/SolutionImage/logo_traceinfo.svg";
        public const string DASHBOARD = "/images/SolutionImage/logo_ecore.svg";
        public const string HTKK = "/images/SolutionImage/logo_htkk.svg";
        public const string REPORTSERVICE = "/images/SolutionImage/logo_reportservice.svg";
        public const string QSERVICE = "/images/SolutionImage/logo_qservice.svg";
        public const string LOGISTIC = "/images/SolutionImage/logo_logistic.svg";
        public const string XNK = "/images/SolutionImage/logo_importexport.svg";
        public const string LQDMS = "/images/SolutionImage/logo_dmslq.svg";
        public const string ETEMNN = "/images/SolutionImage/logo_etemnn.svg";
        public const string FINAS = "/images/SolutionImage/logo_finas.svg";
    }

    public class TableName
    {
        public const string Sys_User = "Sys_User";
        public const string RptSv_Sys_Group = "RptSv_Sys_Group";
        public const string Product_CustomField = "Product_CustomField";
        public const string Sys_Group = "Sys_Group";
        public const string Sys_Access = "Sys_Access";
        public const string RptSv_Sys_Access = "RptSv_Sys_Access";
        public const string Sys_Object = "Sys_Object";
        public const string RptSv_Sys_Object = "RptSv_Sys_Object";
        public const string Sys_UserInGroup = "Sys_UserInGroup";
        public const string Mst_TaxType = "Mst_TaxType";
        public const string Mst_Tax = "Mst_Tax";
        public const string Mst_NNTType = "Mst_NNTType";
        public const string Invoice_TempInvoice = "Invoice_TempInvoice";
        public const string Mst_GovTaxID = "Mst_GovTaxID";
        public const string Tax_Appendix = "Tax_Appendix";
        public const string Mst_District = "Mst_District";
        public const string Mst_Country = "Mst_Country";
        public const string Mst_Province = "Mst_Province";
        public const string Mst_Department = "Mst_Department";
        public const string Mst_NNT = "Mst_NNT";
        public const string Mst_Inventory = "Mst_Inventory";
        public const string Map_TaxInKyKeKhai = "Map_TaxInKyKeKhai";
        public const string Mst_KyKeKhai = "Mst_KyKeKhai";
        public const string Sys_Modules = "Sys_Modules";
        public const string Sys_ObjectInModules = "Sys_ObjectInModules";
        public const string Sys_Solution = "Sys_Solution";
        public const string Rpt_SearchTK = "Rpt_SearchTK";
        public const string Rpt_SearchTBT = "Rpt_SearchTBT";
        public const string Rpt_SearchGD = "Rpt_SearchGD";
        public const string Mst_TransactionType = "Mst_TransactionType";
        public const string Tax_RegBalance = "Tax_RegBalance";
        public const string Tax_Submit = "Tax_Submit";
        public const string Mst_Dealer = "Mst_Dealer";
        public const string Mst_CustomerNNTType = "Mst_CustomerNNTType";
        public const string Mst_CustomerNNT = "Mst_CustomerNNT";
        public const string iNOS_Mst_BizType = "iNOS_Mst_BizType";
        public const string iNOS_Mst_BizField = "iNOS_Mst_BizField";
        public const string iNOS_Mst_BizSize = "iNOS_Mst_BizSize";
        public const string Mst_GovIDType = "Mst_GovIDType";
        public const string Invoice_Invoice = "Invoice_Invoice";
        public const string Invoice_InvoiceInput = "Invoice_InvoiceInput";
        //public const string OS_PrdCenter_Mst_Brand = "OS_PrdCenter_Mst_Brand";
        public const string Mst_Area = "Mst_Area";
        public const string Mst_Brand = "Mst_Brand";
        public const string Mst_Model = "Mst_Model";
        public const string Mst_Unit = "Mst_Unit";
        public const string Mst_Spec = "Mst_Spec";
        public const string Mst_SpecImage = "Mst_SpecImage";
        public const string Mst_SpecFiles = "Mst_SpecFiles";
        public const string Mst_SpecCustomField = "Mst_SpecCustomField";
        public const string Prd_PrdIDCustomField = "Prd_PrdIDCustomField";
        public const string Mst_SpecPrice = "Mst_SpecPrice";
        public const string Mst_SpecUnit = "Mst_SpecUnit";
        public const string Mst_SpecType1 = "Mst_SpecType1"; // Đối tượng OS_PrdCenter_Mst_SpecType1
        public const string Mst_SpecType2 = "Mst_SpecType2"; // Đối tượng OS_PrdCenter_Mst_SpecType2
        public const string Mst_VATRate = "Mst_VATRate"; // Đối tượng OS_PrdCenter_Mst_VATRate
        public const string Prd_ProductID = "Prd_ProductID"; // Đối tượng OS_PrdCenter_Prd_ProductID
        public const string Mst_CurrencyEx = "Mst_CurrencyEx";
        public const string Invoice_TempGroup = "Invoice_TempGroup";
        public const string OS_Inos_OrgLicense = "OS_Inos_OrgLicense";
        public const string Invoice_CustomField = "Invoice_CustomField";
        public const string Invoice_DtlCustomField = "Invoice_DtlCustomField";
        public const string InvF_InventoryInFG = "InvF_InventoryInFG";
        public const string InvF_InventoryInFGDtl = "InvF_InventoryInFGDtl";
        public const string Inv_InventoryBox = "Inv_InventoryBox";
        public const string Inv_InventoryCarton = "Inv_InventoryCarton";
        public const string Inv_GenTimesCarton = "Inv_GenTimesCarton";
        public const string Inv_GenTimesBox = "Inv_GenTimesBox";
        public const string Inv_InventoryBalance = "Inv_InventoryBalance";
        public const string InvF_InventoryOutFGInstSerial = "InvF_InventoryOutFGInstSerial";
        public const string InvF_InventoryInFGInstSerial = "InvF_InventoryInFGInstSerial";
        public const string Mst_InventoryType = "Mst_InventoryType";
        public const string Mst_InvInType = "Mst_InvInType";
        public const string Mst_InvOutType = "Mst_InvOutType";
        public const string Mst_Product = "Mst_Product";
        public const string Prd_Attribute = "Prd_Attribute";
        public const string InvF_MoveOrd = "InvF_MoveOrd";
        public const string InvF_InventoryOut = "InvF_InventoryOut";
        public const string InvF_InventoryIn = "InvF_InventoryIn";
        public const string InvF_InvAudit = "InvF_InvAudit";
        public const string InvF_InventoryInDtl = "InvF_InventoryInDtl";
        public const string InvF_InventoryCusReturn = "InvF_InventoryCusReturn";
        public const string InvF_InventoryCusReturnDtl = "InvF_InventoryCusReturnDtl";
        public const string InvF_InventoryReturnSup = "InvF_InventoryReturnSup";
        public const string Ord_OrderPD = "Ord_OrderPD";


        public const string Mst_CustomerGroup = "Mst_CustomerGroup";
        public const string Mst_Ward = "Mst_Ward";
        public const string Customer_DynamicField = "Customer_DynamicField";
        public const string Mst_CustomerSource = "Mst_CustomerSource";
        public const string Mst_CustomerType = "Mst_CustomerType";
        public const string Prd_DynamicField = "Prd_DynamicField";
        public const string Mst_Attribute = "Mst_Attribute";
        public const string Prd_BOM = "Prd_BOM";

        // DMS
        public const string OS_Invoice_InvoiceTemp = "OS_Invoice_InvoiceTemp";
        public const string OS_Invoice_InvoiceTempDtl = "OS_Invoice_InvoiceTempDtl";
        public const string Mst_Sys_Config = "Mst_Sys_Config";

        //LQDMS
        public const string Mst_PrintOrder = "Mst_PrintOrder";

        //Veloca
        public const string Ser_RO = "Ser_RO";
        public const string Ser_ROProductPart = "Ser_ROProductPart";

        //public const string OS_PrdCenter_Mst_SpecPrice = "OS_PrdCenter_Mst_SpecPrice";
        //public const string OS_PrdCenter_Mst_SpecUnit = "OS_PrdCenter_Mst_SpecUnit";
        //public const string OS_PrdCenter_Mst_CurrencyEx = "OS_PrdCenter_Mst_CurrencyEx";

        #region["Report Server"]

        public const string RptSv_Sys_User = "RptSv_Sys_User";
        public const string RptSv_Rpt_InvoiceSummary_01 = "RptSv_Rpt_InvoiceSummary_01";

        #endregion

        // Import Excel
        public const string Invoice_ImportExcel = "Invoice_ImportExcel";

        #region["InBrand Cloud"]
        #region ["2019-12-04"]
        public const string Mst_Agent = "Mst_Agent";
        public const string Mst_PartMaterialType = "Mst_PartMaterialType";
        #endregion
        public const string Temp_PrintTemp = "Temp_PrintTemp";
        public const string View_ColumnInGroup = "View_ColumnInGroup";
        public const string View_GroupView = "View_GroupView";
        public const string View_ColumnView = "View_ColumnView";
        public const string Inv_InventorySecret = "Inv_InventorySecret";
        public const string Inv_GenTimes = "Inv_GenTimes";
        public const string Inv_InventoryBalanceSerial = "Inv_InventoryBalanceSerial";
        public const string Mst_Part = "Mst_Part";
        public const string InvF_InventoryOutFG = "InvF_InventoryOutFG";
        public const string InvF_InventoryOutFGDtl = "InvF_InventoryOutFGDtl";

        public const string Mst_PartUnit = "Mst_PartUnit";
        public const string Mst_PartType = "Mst_PartType";
        public const string Rpt_InvFInventoryInFGSum = "Rpt_InvFInventoryInFGSum";
        public const string Rpt_InvFInventoryOutFGSum = "Rpt_InvFInventoryOutFGSum";
        public const string Rpt_InvInventoryBalanceMonth = "Rpt_InvInventoryBalanceMonth";
        #endregion

        #region ["Inventory"]
        public const string Mst_InventoryLevelType = "Mst_InventoryLevelType";
        public const string Mst_Customer = "Mst_Customer";
        public const string Mst_ColumnConfigGroup = "Mst_ColumnConfigGroup";
        public const string Mst_ColumnConfig = "Mst_ColumnConfig";
        public const string InvF_InventoryCusReturnCover = "InvF_InventoryCusReturnCover";
        public const string InvF_MoveOrdDtl = "InvF_MoveOrdDtl";
        public const string InvF_InvAuditDtl = "InvF_InvAuditDtl";
        public const string InvF_InventoryReturnSupDtl = "InvF_InventoryReturnSupDtl";
        public const string Rpt_Inv_InventoryBalance = "Rpt_Inv_InventoryBalance";
        public const string Rpt_InvF_WarehouseCard = "Rpt_InvF_WarehouseCard";
        public const string Rpt_Inventory_In_Out_Inv = "Rpt_Inventory_In_Out_Inv";
        public const string Rpt_Inv_InventoryBalance_StorageTime = "Rpt_Inv_InventoryBalance_StorageTime";
        public const string Rpt_Inv_InventoryBalance_Minimum = "Rpt_Inv_InventoryBalance_Minimum";

        #endregion
    }

    #region["TblTable"]
    public class TblSys_User
    {
        public const string OrgID = "OrgID";
        public const string CustomerCodeSys = "CustomerCodeSys";
        public const string CustomerCode = "CustomerCode";
        public const string UserCode = "UserCode";
        public const string UserName = "UserName";
        public const string UserPassword = "UserPassword";
        public const string UserPasswordNew = "UserPasswordNew";
        public const string PhoneNo = "PhoneNo";
        public const string MST = "MST";
        public const string EMail = "EMail";
        public const string OrganCode = "OrganCode";
        public const string DepartmentCode = "DepartmentCode";
        public const string Position = "Position";
        public const string FlagSysAdmin = "FlagSysAdmin";
        public const string FlagDLAdmin = "FlagDLAdmin";
        public const string FlagNNTAdmin = "FlagNNTAdmin";
        public const string FlagActive = "FlagActive";
        public const string ACLanguage = "ACLanguage";
        public const string ACTimeZone = "ACTimeZone";

        public const string mdept_DepartmentName = "mdept_DepartmentName";
    }
    public class TblSys_Group
    {
        public const string GroupCode = "GroupCode";
        public const string GroupName = "GroupName";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_Sys_Config
    {
        public const string NetworkID = "NetworkID";
        public const string FlagCurrency = "FlagCurrency";
        public const string FlagSupportUPDiffer = "FlagSupportUPDiffer";
        public const string FlagUPVAT = "FlagUPVAT";
        public const string FlagColmnConfig = "FlagColmnConfig";
        public const string FlagOrderInvBalanceZero = "FlagOrderInvBalanceZero";
    }
    public class TblRptSv_Sys_Group
    {
        public const string GroupCode = "GroupCode";
        public const string GroupName = "GroupName";
        public const string FlagActive = "FlagActive";
    }
    public class TblSys_Object
    {
        public const string ObjectCode = "ObjectCode";
        public const string ObjectName = "ObjectName";
        public const string ServiceCode = "ServiceCode";
        public const string ObjectType = "ObjectType";
        public const string FlagExecModal = "FlagExecModal";
        public const string FlagActive = "FlagActive";
    }
    public class TblRptSv_Sys_Object
    {
        public const string ObjectCode = "ObjectCode";
        public const string ObjectName = "ObjectName";
        public const string ServiceCode = "ServiceCode";
        public const string ObjectType = "ObjectType";
        public const string FlagExecModal = "FlagExecModal";
        public const string FlagActive = "FlagActive";
    }
    public class TblSys_Access
    {
        public const string GroupCode = "GroupCode";
        public const string ObjectCode = "ObjectCode";
    }

    public class TblRptSv_Sys_Access
    {
        public const string GroupCode = "GroupCode";
        public const string ObjectCode = "ObjectCode";
    }

    public class TblProduct_CustomField
    {
        public const string OrgID = "OrgID";
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_TaxType
    {
        public const string TaxType = "TaxType";
        public const string TaxTypeName = "TaxTypeName";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_Tax
    {
        public const string TaxId = "TaxId";
        public const string TaxType = "TaxType";
        public const string TaxName = "TaxName";
        public const string TaxTemplate = "TaxTemplate";
        public const string TaxVerXMLB = "TaxVerXMLB";
        public const string TaxVerXMLC = "TaxVerXMLC";
        public const string FlagHasAppendix = "FlagHasAppendix";
        public const string EffDateStart = "EffDateStart";
        public const string EffDateEnd = "EffDateEnd";
        public const string FlagActive = "FlagActive";

        //Mst_TaxType
        public const string mtt_TaxTypeName = "mtt_TaxTypeName";
    }
    public class TblMst_NNTType
    {
        public const string NNTType = "NNTType";
        public const string NNTTypeName = "NNTTypeName";
        public const string FlagActive = "FlagActive";
    }
    public class TblInvoice_TempInvoice
    {
        public const string DLCode = "DLCode";
        public const string ProvinceCode = "ProvinceCode";
        public const string DLName = "DLName";
        public const string DLAddress = "DLAddress";
        public const string DLPresentBy = "DLPresentBy";
        public const string DLGovIDNumber = "DLGovIDNumber";
        public const string DLEmail = "DLEmail";
        public const string DLPhoneNo = "DLPhoneNo";
        public const string FlagActive = "FlagActive";
        public const string TInvoiceCode = "TInvoiceCode";
        public const string InvoiceCode = "InvoiceCode";
        public const string InvoiceType = "InvoiceType";
        public const string TInvoiceName = "TInvoiceName";
        public const string FormNo = "FormNo";
        public const string Sign = "Sign";
        public const string TInvoiceStatus = "TInvoiceStatus";
        public const string MST = "MST";
        public const string NetworkID = "NetworkID";
    }

    public class TblInvoice_Invoice
    {
        public const string InvoiceCode = "InvoiceCode";
        public const string CustomerNNTBuyerName = "CustomerNNTBuyerName";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string InvoiceDateUTC = "InvoiceDateUTC";
        public const string InvoiceAdjType = "InvoiceAdjType";
        public const string MST = "MST";
        public const string InvoiceStatus = "InvoiceStatus";
        public const string InvoiceNo = "InvoiceNo";
        public const string SourceInvoiceCode = "SourceInvoiceCode";
        public const string mcnnt_CustomerMST = "mcnnt_CustomerMST";
        public const string TInvoiceCode = "TInvoiceCode";
        public const string iti_Sign = "iti_Sign";
        public const string FormNo = "FormNo";
        public const string Sign = "Sign";
        public const string CustomerNNTCode = "CustomerNNTCode";
        public const string CustomerNNTName = "CustomerNNTName";
        public const string CustomerMST = "CustomerMST";
        public const string CustomerNNTAddress = "CustomerNNTAddress";
        public const string CustomerNNTPhone = "CustomerNNTPhone";
        public const string CustomerNNTBankName = "CustomerNNTBankName";
        public const string CustomerNNTEmail = "CustomerNNTEmail";
        public const string CustomerNNTAccNo = "CustomerNNTAccNo";
        public const string EmailSend = "EmailSend";
        public const string MailSentDateTime = "MailSentDateTime";
        public const string TotalValInvoice = "TotalValInvoice";
        public const string TotalValVAT = "TotalValVAT";
        public const string TotalValPmt = "TotalValPmt";
        public const string ValGoodsNotTaxable = "ValGoodsNotTaxable";
        public const string ValGoodsNotChargeTax = "ValGoodsNotChargeTax";
        public const string ValGoodsVAT5 = "ValGoodsVAT5";
        public const string ValVAT5 = "ValVAT5";
        public const string ValGoodsVAT10 = "ValGoodsVAT10";
        public const string ValVAT10 = "ValVAT10";
        public const string PaymentMethodCode = "PaymentMethodCode";
        public const string Remark = "Remark";
        public const string DeleteReason = "DeleteReason";
        public const string AttachedDelFilePath = "AttachedDelFilePath";
        public const string RefNo = "RefNo";
        public const string InvoiceType2 = "InvoiceType2";
        public const string InvoiceCF1 = "InvoiceCF1";
        public const string InvoiceCF2 = "InvoiceCF2";
        public const string InvoiceCF3 = "InvoiceCF3";
        public const string InvoiceCF4 = "InvoiceCF4";
        public const string InvoiceCF5 = "InvoiceCF5";
        public const string InvoiceCF6 = "InvoiceCF6";
        public const string InvoiceCF7 = "InvoiceCF7";
        public const string InvoiceCF8 = "InvoiceCF8";
        public const string InvoiceCF9 = "InvoiceCF9";
        public const string InvoiceCF10 = "InvoiceCF10";


    }


    public class TblMst_ColumnConfigGroup
    {
        public const string ColumnConfigGrpCode = "ColumnConfigGrpCode";
        public const string ColumnGrpName = "ColumnGrpName";
        public const string ColumnGrpFormat = "ColumnGrpFormat";
        public const string ColumnGrpDesc = "ColumnGrpDesc";
        public const string FlagActive = "FlagActive";
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
    }


    public class TblMst_ColumnConfig
    {
        public const string AutoId = "AutoId";
        public const string TableName = "TableName";
        public const string ColumnName = "ColumnName";
        public const string ColumnFormat = "ColumnFormat";
        public const string ColumnDesc = "ColumnDesc";
        public const string FlagActive = "FlagActive";
        public const string OrgId = "OrgId";
        public const string NetworkID = "NetworkID";
    }


    public class TblInvoice_InvoiceDtl
    {
        public const string InvoiceCode = "InvoiceCode";
        public const string Idx = "Idx";
        public const string NetworkID = "NetworkID";
        public const string SpecCode = "SpecCode";
        public const string SpecName = "SpecName";
        public const string ProductID = "ProductID";
        public const string ProductName = "ProductName";
        public const string VATRateCode = "VATRateCode";
        public const string VATRate = "VATRate";
        public const string UnitCode = "UnitCode";
        public const string UnitName = "UnitName";
        public const string UnitPrice = "UnitPrice";
        public const string Qty = "Qty";
        public const string ValInvoice = "ValInvoice";
        public const string ValTax = "ValTax";
        public const string InventoryCode = "InventoryCode";
        public const string DiscountRate = "DiscountRate";
        public const string ValDiscount = "ValDiscount";
        public const string InvoiceDtlStatus = "InvoiceDtlStatus";
        public const string RemarkDtl = "RemarkDtl";
        public const string InvoiceDCF1 = "InvoiceDCF1";
        public const string InvoiceDCF2 = "InvoiceDCF2";
        public const string InvoiceDCF3 = "InvoiceDCF3";
        public const string InvoiceDCF4 = "InvoiceDCF4";
        public const string InvoiceDCF5 = "InvoiceDCF5";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblPrd_ProductID
    {
        public const string OrgID = "OrgID";
        public const string ProductID = "ProductID";
        public const string NetworkProductIDCode = "NetworkProductIDCode";
        public const string SpecCode = "SpecCode";
        public const string ms_SpecName = "ms_SpecName";
        public const string PrdCustomFieldCode = "PrdCustomFieldCode";
        public const string ProductionDate = "ProductionDate";
        public const string LOTNo = "LOTNo";
        public const string BuyDate = "BuyDate";
        public const string SecretNo = "SecretNo";
        public const string WarrantyStartDate = "WarrantyStartDate";
        public const string WarrantyExpiredDate = "WarrantyExpiredDate";
        public const string WarrantyDuration = "WarrantyDuration";
        public const string RefNo1 = "RefNo1";
        public const string RefBiz1 = "RefBiz1";
        public const string RefNo2 = "RefNo2";
        public const string RefBiz2 = "RefBiz2";
        public const string RefNo3 = "RefNo3";
        public const string RefBiz3 = "RefBiz3";
        public const string Buyer = "Buyer";
        public const string ProductIDStatus = "ProductIDStatus";
        public const string CustomField1 = "CustomField1";
        public const string CustomField2 = "CustomField2";
        public const string CustomField3 = "CustomField3";
        public const string CustomField4 = "CustomField4";
        public const string CustomField5 = "CustomField5";
    }
    public class TblMst_GovTaxID
    {
        public const string GovTaxID = "GovTaxID";
        public const string NetworkID = "NetworkID";
        public const string GovTaxIDParent = "GovTaxIDParent";
        public const string GovTaxIDBUCode = "GovTaxIDBUCode";
        public const string GovTaxIDBUPattern = "GovTaxIDBUPattern";
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictCode = "DistrictCode";
        public const string GovTaxIDLevel = "GovTaxIDLevel";
        public const string GovTaxName = "GovTaxName";
        public const string Level = "Level";
        public const string Address = "Address";
        public const string ContactEmail = "ContactEmail";
        public const string ContactPhone = "ContactPhone";
        public const string FlagActive = "FlagActive";
    }
    public class TblRptSv_Rpt_InvoiceSummary_01
    {
        public const string Month = "Month";
    }
    public class TblTax_Appendix
    {
        public const string AppendixId = "AppendixId";
        public const string TaxId = "TaxId";
        public const string AppendixName = "AppendixName";
        public const string AppendixTemplate = "AppendixTemplate";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_District
    {
        public const string DistrictCode = "DistrictCode";
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictName = "DistrictName";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_Country
    {
        public const string CountryCode = "CountryCode";
        public const string CountryName = "CountryName";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_Province
    {
        public const string ProvinceCode = "ProvinceCode";
        public const string ProvinceName = "ProvinceName";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_Department
    {
        public const string DepartmentCode = "DepartmentCode";
        public const string NetworkID = "NetworkID";
        public const string DepartmentCodeParent = "DepartmentCodeParent";
        public const string DepartmentBUCode = "DepartmentBUCode";
        public const string DepartmentBUPattern = "DepartmentBUPattern";
        public const string DepartmentLevel = "DepartmentLevel";
        public const string MST = "MST";
        public const string DepartmentName = "DepartmentName";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_NNT
    {
        public const string MST = "MST";
        public const string NNTFullName = "NNTFullName";
        public const string NetworkID = "NetworkID";
        public const string MSTParent = "MSTParent";
        public const string MSTBUCode = "MSTBUCode";
        public const string MSTBUPattern = "MSTBUPattern";
        public const string MSTLevel = "MSTLevel";
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictCode = "DistrictCode";
        public const string NNTType = "NNTType";
        public const string DLCode = "DLCode";
        //public const string FullName = "FullName";
        public const string NNTAddress = "NNTAddress";
        public const string NNTMobile = "NNTMobile";
        public const string NNTPhone = "NNTPhone";
        public const string NNTFax = "NNTFax";
        public const string PresentBy = "PresentBy";
        public const string BusinessRegNo = "BusinessRegNo";
        public const string NNTPosition = "NNTPosition";
        public const string PresentIDNo = "PresentIDNo";
        public const string PresentIDType = "PresentIDType";
        public const string GovTaxID = "GovTaxID";
        public const string ContactName = "ContactName";
        public const string ContactPhone = "ContactPhone";
        public const string ContactEmail = "ContactEmail";
        public const string Website = "Website";
        public const string BizType = "BizType";
        public const string BizFieldCode = "BizFieldCode";
        public const string BizSizeCode = "BizSizeCode";
        public const string CANumber = "CANumber";
        public const string CAOrg = "CAOrg";
        public const string CAEffDTimeUTCStart = "CAEffDTimeUTCStart";
        public const string CAEffDTimeUTCEnd = "CAEffDTimeUTCEnd";
        public const string PackageCode = "PackageCode";
        public const string CreatedDate = "CreatedDate";
        public const string AccNo = "AccNo";
        public const string AccHolder = "AccHolder";
        public const string BankName = "BankName";
        public const string FlagActive = "FlagActive";
        public const string TCTStatus = "TCTStatus";
        public const string RegisterStatus = "RegisterStatus";
        public const string Remark = "Remark";
        public const string OrgID = "OrgID";
        public const string DealerType = "DealerType";
        public const string AreaCode = "AreaCode";

    }
    public class TblMap_TaxInKyKeKhai
    {
        public const string TaxId = "TaxId";
        public const string KKKCode = "KKKCode";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblMst_KyKeKhai
    {
        public const string KKKCode = "KKKCode";
        public const string KKKName = "KKKName";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblSys_Modules
    {
        public const string ModuleCode = "ModuleCode";
        public const string ModuleName = "ModuleName";
        public const string SolutionCode = "SolutionCode";
        public const string Description = "Description";
        public const string QtyInvoice = "QtyInvoice";
        public const string ValCapacity = "ValCapacity";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblSys_ObjectInModules
    {
        public const string ModuleCode = "ModuleCode";
        public const string ObjectCode = "ObjectCode";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblSys_Solution
    {
        public const string SolutionCode = "SolutionCode";
        public const string SolutionName = "SolutionName";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblRpt_SearchTK
    {
        public const string SubNo = "SubNo";
        public const string MST = "MST";
        public const string TaxId = "TaxId";
        public const string TaxName = "TaxName";
        public const string TaxDate = "TaxDate";
        public const string SendNo = "SendNo";
        public const string SendDateTime = "SendDateTime";
        public const string GovTaxID = "GovTaxID";
        public const string GovTaxName = "GovTaxName";
        public const string FlagHasAppendix = "FlagHasAppendix";
        public const string FilePath = "FilePath";
        public const string TCTStatus = "TCTStatus";
        public const string TaxType = "TaxType";
    }
    public class TblRpt_SearchTBT
    {
        public const string MST = "MST";
        public const string TranStatus = "TranStatus";
        public const string TCTTran_maGDich = "TCTTran_maGDich";
        public const string TCTTran_ndungGDich = "TCTTran_ndungGDich";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string TCTTran_FilePath = "TCTTran_FilePath";
    }
    public class TblRpt_SearchGD
    {
        public const string MST = "MST";
        public const string TCTTran_maGDich = "TCTTran_maGDich";
        public const string TCTTran_ndungGDich = "TCTTran_ndungGDich";
        public const string TaxDate = "TaxDate";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string TranStatus = "TranStatus";
        public const string FilePath = "FilePath";
        public const string TCTTran_FilePath = "TCTTran_FilePath";
    }

    public class TblMst_TransactionType
    {
        public const string TranType = "TranType";
        public const string TranTypeName = "TranTypeName";
        public const string TCTTranType = "TCTTranType";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblTax_RegBalance
    {
        public const string TaxId = "TaxId";
        //public const string NetworkID = "NetworkID";
        public const string KKKCode = "KKKCode";
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblTax_Submit
    {
        public const string SubNo = "SubNo";
        //public const string NetworkID = "NetworkID";
        public const string TaxId = "TaxId";
        public const string MST = "MST";
        public const string TaxSubType = "TaxSubType";
        public const string TaxDate = "TaxDate";
        public const string SendNo = "SendNo";
        public const string SendDateTime = "SendDateTime";
        public const string CreateDTime = "CreateDTime";
        public const string CreateBy = "CreateBy";
        public const string FilePath = "FilePath";
        public const string TCTFilePath = "TCTFilePath";
        public const string LUDTime = "LUDTime";
        public const string LUBy = "LUBy";
        public const string FlagSigned = "FlagSigned";
        public const string FlagSent = "FlagSent";
        public const string FlagIsAppendix = "FlagIsAppendix";
        public const string InsTaxIDRefNo = "InsTaxIDRefNo";
        public const string UploadedDateTime = "UploadedDateTime";
        public const string TCTStatus = "TCTStatus";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblSubmitType
    {
        public const string C = "C";
        public const string B = "B";
    }

    //public class TblLogin
    //{
    //    //Dùng tại hàm đăng ký NTT (API report server)
    //    public const string WAUserCode = "SYSADMIN";
    //    //public const string WAUserPassword = "123456"; 
    //    public const string WAUserPassword = "xIqkU16EqzfPnlxkr3PbKA";
    //    //public const string Tid = "20181012.103518.721831";
    //    //public const string GwUserCode = "idocNet.idn.Skycic.Inventory.Sv";
    //    //public const string GwPassword = "idocNet.idn.Skycic.Inventory.Sv";
    //    //public const string Ft_RecordStart = "0";
    //    //public const string Ft_RecordCount = "12345600";
    //}

    public class Mst_TransactionType_Client
    {
        public const string TransactionType0001 = "0001";
        public const string TransactionType0002 = "0002";
        public const string TransactionType0003 = "0003";
        public const string TransactionType0004 = "0004";
        public const string TransactionType0005 = "0005";
        public const string TransactionType0006 = "0006";
        public const string TransactionType0007 = "0007";
    }
    public class TblMst_Dealer
    {
        public const string DLCode = "DLCode";
        public const string DLCodeParent = "DLCodeParent";
        public const string ProvinceCode = "ProvinceCode";
        public const string DLBUCode = "DLBUCode";
        public const string DLName = "DLName";
        public const string DLType = "DLType";
        public const string DLAddress = "DLAddress";
        public const string DLPresentBy = "DLPresentBy";
        public const string DLGovIDNumber = "DLGovIDNumber";
        public const string DLEmail = "DLEmail";
        public const string DLPhoneNo = "DLPhoneNo";
        public const string FlagActive = "FlagActive";

        //2019-1205
        public const string InvCode = "InvCode";
        public const string PMType = "PMType";
        public const string NetworkID = "NetworkID";
        public const string DLBUPattern = "DLBUPattern";
        public const string DLLevel = "DLLevel";
        public const string FlagRoot = "FlagRoot";
        public const string Remark = "Remark";

    }

    public class Client_Mst_CustomerType
    {
        public const string CANHAN = "CANHAN";
        public const string TOCHUC = "TOCHUC";
    }

    public class Mst_Dealer_Client
    {
        public const string IDOCNET = "IDOCNET";
    }
    public class TblMst_Customer
    {
        public const string NetworkID = "NetworkID";
        public const string CustomerCodeSys = "CustomerCodeSys";
        public const string CustomerCode = "CustomerCode";
        public const string CustomerName = "CustomerName";
        public const string FlagActive = "FlagActive";
        public const string OrgID = "OrgID";
        public const string FlagDealer = "FlagDealer" ;
        public const string CustomerType = "CustomerType";
        public const string CustomerSourceCode = "CustomerSourceCode";
        public const string CustomerMobilePhone = "CustomerMobilePhone";
        public const string ContactEmail = "ContactEmail";
        public const string FlagSupplier = "FlagSupplier";
        public const string FlagShipper = "FlagShipper";
        public const string FlagEndUser = "FlagEndUser";
        public const string FlagBank = "FlagBank";
        public const string FlagInsurrance = "FlagInsurrance";
        public const string FlagCustomerAvatarPath = "FlagCustomerAvatarPath";
        public const string CustomerGrpCode = "CustomerGrpCode";
        public const string ListOfCustDynamicFieldValue = "ListOfCustDynamicFieldValue";
        public const string CustomerPhoneNo = "CustomerPhoneNo";
        public const string CustomerAvatarPath = "CustomerAvatarPath";
        public const string CustomerGender = "CustomerGender";
        public const string CustomerEmail = "CustomerEmail";
        public const string CustomerAddress = "CustomerAddress";
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictCode = "DistrictCode";
        public const string WardCode = "WardCode";
        public const string AreaCode = "AreaCode";
        public const string RepresentName = "RepresentName";
        public const string RepresentPosition = "RepresentPosition";
        public const string GovIDCardNo = "GovIDCardNo";
        public const string GovIDType = "GovIDType";
        public const string BankAccountNo = "BankAccountNo";
        public const string BankName = "BankName";
        public const string ContactName = "ContactName";
        public const string ContactPhone = "ContactPhone";
        public const string Fax = "Fax";
        public const string CustomerDateOfBirth = "CustomerDateOfBirth";
        public const string Facebook = "Facebook";
        public const string InvoiceCustomerName = "InvoiceCustomerName";
        public const string InvoiceOrgName = "InvoiceOrgName";
        public const string MST = "MST";
        public const string InvoiceCustomerAddress = "InvoiceCustomerAddress";
        public const string InvoiceEmailSend = "InvoiceEmailSend";
        public const string CustomerNameEN = "CustomerNameEN";
        public const string Remark = "Remark";
        public const string BankCode = "BankCode";
    }
    public class TblMst_CustomerGroup
    {
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string FlagActive = "FlagActive";
        public const string CustomerGrpCode = "CustomerGrpCode";
        public const string CustomerGrpName = "CustomerGrpName";
        public const string CustomerGrpDesc = "CustomerGrpDesc";
    }

    public class TblMst_CustomerType
    {
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_CustomerSource
    {
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string FlagActive = "FlagActive";
        public const string CustomerSourceName = "CustomerSourceName"; 
        public const string CustomerSourceDesc = "CustomerSourceDesc";
        public const string CustomerSourceCode = "CustomerSourceCode";
    }

    public class TblCustomer_DynamicField
    {
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_Area
    {
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string FlagActive = "FlagActive";
        public const string AreaCode = "AreaCode";
    }

    public class TblMst_Ward
    {
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictCode = "DistrictCode";
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_CustomerNNT
    {
        public const string CustomerNNTCode = "CustomerNNTCode";
        public const string MST = "MST";
        //public const string CustomerMST = "CustomerMST";
        public const string NetworkID = "NetworkID";
        public const string AccCenterCode = "AccCenterCode";
        public const string CustomerNNTName = "CustomerNNTName";
        public const string CustomerNNTType = "CustomerNNTType";
        public const string CustomerNNTAddress = "CustomerNNTAddress";
        public const string CustomerNNTEmail = "CustomerNNTEmail";
        public const string CustomerNNTPhone = "CustomerNNTPhone";
        public const string CustomerNNTFax = "CustomerNNTFax";
        public const string ContactName = "ContactName";
        public const string ContactPhone = "ContactPhone";
        public const string ContactEmail = "ContactEmail";
        public const string CustomerNNTDOB = "CustomerNNTDOB";
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictCode = "DistrictCode";
        public const string AccNo = "AccNo";
        public const string BankName = "BankName";
        public const string GovIDType = "GovIDType";
        public const string GovID = "GovID";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";

        public const string mp_ProvinceName = "mp_ProvinceName";
        public const string CustomerMST = "CustomerMST";

    }

    public class TblMst_CustomerNNTType
    {
        public const string CustomerNNTType = "CustomerNNTType";
        public const string NetworkID = "NetworkID";
        public const string CustomerNNTTypeName = "CustomerNNTTypeName";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_Brand
    {
        public const string OrgID = "OrgID";
        public const string BrandCode = "BrandCode";
        public const string NetworkBrandCode = "NetworkBrandCode";
        public const string NetworkID = "NetworkID";
        public const string BrandName = "BrandName";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_Model
    {
        public const string OrgID = "OrgID";
        public const string ModelCode = "ModelCode";
        public const string NetworkModelCode = "NetworkModelCode";
        public const string NetworkID = "NetworkID";
        public const string ModelName = "ModelName";
        public const string OrgModelCode = "OrgModelCode";
        public const string BrandCode = "BrandCode";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
        public const string mb_BrandCode = "mb_BrandCode";
        public const string mb_BrandName = "mb_BrandName";
    }

    public class TblMst_Unit
    {
        public const string UnitCode = "UnitCode";
        public const string NetworkID = "NetworkID";
        public const string UnitName = "UnitName";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_Spec
    {
        public const string OrgID = "OrgID";
        public const string SpecCode = "SpecCode";
        public const string NetworkSpecCode = "NetworkSpecCode";
        public const string NetworkID = "NetworkID";
        public const string SpecName = "SpecName";
        public const string SpecDesc = "SpecDesc";
        public const string ModelCode = "ModelCode";
        public const string SpecType1 = "SpecType1";
        public const string SpecType2 = "SpecType2";
        public const string Color = "Color";
        public const string FlagHasSerial = "FlagHasSerial";
        public const string FlagHasLOT = "FlagHasLOT";
        public const string DefaultUnitCode = "DefaultUnitCode";
        public const string StandardUnitCode = "StandardUnitCode";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string CustomField1 = "CustomField1";
        public const string CustomField2 = "CustomField2";
        public const string CustomField3 = "CustomField3";
        public const string CustomField4 = "CustomField4";
        public const string CustomField5 = "CustomField5";
        public const string CustomField6 = "CustomField6";
        public const string CustomField7 = "CustomField7";
        public const string CustomField8 = "CustomField8";
        public const string CustomField9 = "CustomField9";
        public const string CustomField10 = "CustomField10";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_SpecImage
    {
        public const string AutoID = "AutoID";
        public const string NetworkID = "NetworkID";
        public const string SpecCode = "SpecCode";
        public const string SpecImagePath = "SpecImagePath";
        public const string SpecImageName = "SpecImageName";
        public const string SpecImageDesc = "SpecImageDesc";
        public const string FlagPrimaryImage = "FlagPrimaryImage";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_SpecFiles
    {
        public const string AutoID = "AutoID";
        public const string NetworkID = "NetworkID";
        public const string SpecCode = "SpecCode";
        public const string SpecFilePath = "SpecFilePath";
        public const string SpecFileName = "SpecFileName";
        public const string SpecFileDesc = "SpecFileDesc";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_SpecCustomField
    {
        public const string SpecCustomFieldCode = "SpecCustomFieldCode";
        public const string NetworkID = "NetworkID";
        public const string SpecCustomFieldName = "SpecCustomFieldName";
        public const string DBPhysicalType = "DBPhysicalType";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblPrd_PrdIDCustomField
    {
        public const string OrgID = "OrgID";
        public const string PrdCustomFieldCode = "PrdCustomFieldCode";
        public const string NetworkID = "NetworkID";
        public const string PrdCustomFieldName = "PrdCustomFieldName";
        public const string DBPhysicalType = "DBPhysicalType";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblInvoice_CustomField
    {
        public const string InvoiceCustomFieldCode = "InvoiceCustomFieldCode";
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string InvoiceCustomFieldName = "InvoiceCustomFieldName";
        public const string DBPhysicalType = "DBPhysicalType";
        public const string FlagActive = "FlagActive";
    }

    public class TblInvoice_DtlCustomField
    {
        public const string InvoiceDtlCustomFieldCode = "InvoiceDtlCustomFieldCode";
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string InvoiceDtlCustomFieldName = "InvoiceDtlCustomFieldName";
        public const string DBPhysicalType = "DBPhysicalType";
        public const string FlagActive = "FlagActive";
    }

    public class TblInvoice_TempGroup
    {
        public const string InvoiceTGroupCode = "InvoiceTGroupCode";
        //public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string MST = "MST";
        public const string VATType = "VATType";
        public const string InvoiceTGroupName = "InvoiceTGroupName";
        public const string InvoiceTGroupBody = "InvoiceTGroupBody";
        public const string FilePathThumbnail = "FilePathThumbnail";
        public const string Spec_Prd_Type = "Spec_Prd_Type";
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_SpecPrice
    {
        public const string OrgID = "OrgID";
        public const string SpecCode = "SpecCode";
        public const string UnitCode = "UnitCode";
        public const string NetworkID = "NetworkID";
        public const string BuyPrice = "BuyPrice";
        public const string SellPrice = "SellPrice";
        public const string CurrencyCode = "CurrencyCode";
        public const string DiscountVND = "DiscountVND";
        public const string VATRateCode = "VATRateCode";
        public const string EffectDTimeStart = "EffectDTimeStart";
        public const string EffectDTimeEnd = "EffectDTimeEnd";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_SpecUnit
    {
        public const string OrgID = "OrgID";
        public const string SpecCode = "SpecCode";
        public const string UnitCode = "UnitCode";
        public const string NetworkID = "NetworkID";
        public const string StandardUnitCode = "StandardUnitCode";
        public const string SpecUnitDesc = "SpecUnitDesc";
        public const string Qty = "Qty";
        public const string Length = "Length";
        public const string Width = "Width";
        public const string Height = "Height";
        public const string Volume = "Volume";
        public const string Weight = "Weight";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_SpecType1
    {
        public const string OrgID = "OrgID";
        public const string SpecType1 = "SpecType1";
        public const string NetworkID = "NetworkID";
        public const string SpecType1Name = "SpecType1Name";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_SpecType2
    {
        public const string OrgID = "OrgID";
        public const string SpecType2 = "SpecType2";
        public const string NetworkID = "NetworkID";
        public const string SpecType2Name = "SpecType2Name";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_VATRate
    {
        public const string VATRateCode = "VATRateCode";
        public const string NetworkID = "NetworkID";
        public const string VATRate = "VATRate";
        public const string VATDesc = "VATDesc";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_CurrencyEx
    {
        public const string CurrencyCode = "CurrencyCode";
        public const string NetworkID = "NetworkID";
        public const string CurrencyName = "CurrencyName";
        public const string BaseCurrencyCode = "BaseCurrencyCode";
        public const string BuyRate = "BuyRate";
        public const string SellRate = "SellRate";
        public const string UpdatedTime = "UpdatedTime";
        public const string InterEx = "InterEx";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    //public class TblOS_PrdCenter_Mst_SpecUnit
    //{
    //    public const string SpecCode = "SpecCode";
    //    public const string UnitCode = "UnitCode";
    //    public const string NetworkID = "NetworkID";
    //    public const string StandardUnitCode = "StandardUnitCode";
    //    public const string SpecUnitDesc = "SpecUnitDesc";
    //    public const string Qty = "Qty";
    //    public const string Length = "Length";
    //    public const string Width = "Width";
    //    public const string Height = "Height";
    //    public const string Volume = "Volume";
    //    public const string Weight = "Weight";
    //    public const string Remark = "Remark";
    //    public const string FlagActive = "FlagActive";
    //    public const string LogLUDTimeUTC = "LogLUDTimeUTC";
    //    public const string LogLUBy = "LogLUBy";
    //}

    //public class TblOS_PrdCenter_Mst_SpecPrice
    //{
    //    public const string SpecCode = "SpecCode";
    //    public const string UnitCode = "UnitCode";
    //    public const string NetworkID = "NetworkID";
    //    public const string BuyPrice = "BuyPrice";
    //    public const string SellPrice = "SellPrice";
    //    public const string CurrencyCode = "CurrencyCode";
    //    public const string DiscountVND = "DiscountVND";
    //    public const string VATRateCode = "VATRateCode";
    //    public const string EffectDTimeStart = "EffectDTimeStart";
    //    public const string EffectDTimeEnd = "EffectDTimeEnd";
    //    public const string Remark = "Remark";
    //    public const string FlagActive = "FlagActive";
    //    public const string LogLUDTimeUTC = "LogLUDTimeUTC";
    //    public const string LogLUBy = "LogLUBy";
    //}

    public class TbliNOS_Mst_BizType
    {
        public const string FlagActive = "FlagActive";
    }

    public class TbliNOS_Mst_BizField
    {
        public const string FlagActive = "FlagActive";
    }

    public class TbliNOS_Mst_BizSize
    {
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_GovIDType
    {
        public const string FlagActive = "FlagActive";
        public const string NetworkID = "NetworkID";
    }


    public class Client_InvoiceTGroupCode
    {
        public const string MAUX20 = "MAUX20";
        public const string MAU1VAT = "MAU1VAT";
        public const string MAUNVAT = "MAUNVAT";
        public const string MAUHTC = "MAUHTC";
        public const string MAUHTCQR = "MAUHTCQR";
        public const string MAU1VATQR = "MAU1VATQR";
        public const string MAUNVATQR = "MAUNVATQR";
    }

    public class Client_PaymentMethodCode
    {
        public const string TMCK = "TMCK";
        public const string CK = "CK";
        public const string TM = "TM";
        public const string TTD = "TTD";
        public const string DTCN = "DTCN";
    }

    public class Client_Mst_VATRate
    {
        public const string VAT0 = "VAT0";
        public const string VAT10 = "VAT10";
        public const string VAT5 = "VAT5";
        public const string VATnull = "VATnull";
    }

    public class Client_SequenceType
    {
        public const string PRTCODE = "PRTCODE";
        public const string GENTIMESNO = "GENTIMESNO";
        public const string IFTEMPPRINTNO = "IFTEMPPRINTNO";
    }

    public class TblOS_Inos_OrgLicense
    {
        public const string StartDate = "StartDate";
        public const string EndDate = "EndDate";
        public const string LicStatus = "LicStatus";
    }

    #region["DMS"]

    public class TblOS_Invoice_InvoiceTemp
    {
        public const string InvoiceCode = "InvoiceCode";
        public const string MST = "MST";
        public const string NetworkID = "NetworkID";
        public const string RefNo = "RefNo";
        public const string FormNo = "FormNo";
        public const string Sign = "Sign";
        public const string SourceInvoiceCode = "SourceInvoiceCode";
        public const string InvoiceAdjType = "InvoiceAdjType";
        public const string PaymentMethodCode = "PaymentMethodCode";
        public const string InvoiceType2 = "InvoiceType2";
        public const string InvoiceDateUTC = "InvoiceDateUTC";
        public const string CustomerNNTCode = "CustomerNNTCode";
        public const string CustomerNNTName = "CustomerNNTName";
        public const string CustomerNNTAddress = "CustomerNNTAddress";
        public const string CustomerNNTPhone = "CustomerNNTPhone";
        public const string CustomerNNTBankName = "CustomerNNTBankName";
        public const string CustomerNNTEmail = "CustomerNNTEmail";
        public const string CustomerNNTAccNo = "CustomerNNTAccNo";
        public const string CustomerNNTBuyerName = "CustomerNNTBuyerName";
        public const string CustomerMST = "CustomerMST";
        public const string TInvoiceCode = "TInvoiceCode";
        public const string InvoiceNo = "InvoiceNo";
        public const string EmailSend = "EmailSend";
        public const string InvoiceFileSpec = "InvoiceFileSpec";
        public const string InvoiceFilePath = "InvoiceFilePath";
        public const string InvoicePDFFilePath = "InvoicePDFFilePath";
        public const string TotalValInvoice = "TotalValInvoice";
        public const string TotalValVAT = "TotalValVAT";
        public const string TotalValPmt = "TotalValPmt";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string InvoiceNoDTimeUTC = "InvoiceNoDTimeUTC";
        public const string InvoiceNoBy = "InvoiceNoBy";
        public const string SignDTimeUTC = "SignDTimeUTC";
        public const string SignBy = "SignBy";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string ApprBy = "ApprBy";
        public const string CancelDTimeUTC = "CancelDTimeUTC";
        public const string CancelBy = "CancelBy";
        public const string SendEmailDTimeUTC = "SendEmailDTimeUTC";
        public const string SendEmailBy = "SendEmailBy";
        public const string IssuedDTimeUTC = "IssuedDTimeUTC";
        public const string IssuedBy = "IssuedBy";
        public const string AttachedDelFilePath = "AttachedDelFilePath";
        public const string DeleteReason = "DeleteReason";
        public const string DeleteDTimeUTC = "DeleteDTimeUTC";
        public const string DeleteBy = "DeleteBy";
        public const string ChangeDTimeUTC = "ChangeDTimeUTC";
        public const string ChangeBy = "ChangeBy";
        public const string InvoiceVerifyCQTCode = "InvoiceVerifyCQTCode";
        public const string CurrencyCode = "CurrencyCode";
        public const string CurrencyRate = "CurrencyRate";
        public const string ValGoodsNotTaxable = "ValGoodsNotTaxable";
        public const string ValGoodsNotChargeTax = "ValGoodsNotChargeTax";
        public const string ValGoodsVAT5 = "ValGoodsVAT5";
        public const string ValVAT5 = "ValVAT5";
        public const string ValGoodsVAT10 = "ValGoodsVAT10";
        public const string ValVAT10 = "ValVAT10";
        public const string NNTFullName = "NNTFullName";
        public const string NNTFullAdress = "NNTFullAdress";
        public const string NNTPhone = "NNTPhone";
        public const string NNTFax = "NNTFax";
        public const string NNTEmail = "NNTEmail";
        public const string NNTWebsite = "NNTWebsite";
        public const string NNTAccNo = "NNTAccNo";
        public const string NNTBankName = "NNTBankName";
        public const string LUDTimeUTC = "LUDTimeUTC";
        public const string LUBy = "LUBy";
        public const string Remark = "Remark";
        public const string InvoiceCF1 = "InvoiceCF1";
        public const string InvoiceCF2 = "InvoiceCF2";
        public const string InvoiceCF3 = "InvoiceCF3";
        public const string InvoiceCF4 = "InvoiceCF4";
        public const string InvoiceCF5 = "InvoiceCF5";
        public const string InvoiceCF6 = "InvoiceCF6";
        public const string InvoiceCF7 = "InvoiceCF7";
        public const string InvoiceCF8 = "InvoiceCF8";
        public const string InvoiceCF9 = "InvoiceCF9";
        public const string InvoiceCF10 = "InvoiceCF10";
        public const string InvoiceStatus = "InvoiceStatus";
        public const string FlagChange = "FlagChange";
        public const string FlagPushOutSite = "FlagPushOutSite";
        public const string FlagDeleteOutSite = "FlagDeleteOutSite";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
        public const string OS_DMS_RefNo = "OS_DMS_RefNo";
        public const string OS_DMS_RefID = "OS_DMS_RefID";
        public const string mpm_PaymentMethodCode = "mpm_PaymentMethodCode";
        public const string mpm_PaymentMethodName = "mpm_PaymentMethodName";

        public const string SignStatus = "SignStatus";
    }

    public class TblOS_Invoice_InvoiceTempDtl
    {
        public const string InvoiceCode = "InvoiceCode";
        public const string Idx = "Idx";
        public const string NetworkID = "NetworkID";
        public const string SpecCode = "SpecCode";
        public const string SpecName = "SpecName";
        public const string ProductID = "ProductID";
        public const string ProductName = "ProductName";
        public const string VATRateCode = "VATRateCode";
        public const string VATRate = "VATRate";
        public const string UnitCode = "UnitCode";
        public const string UnitName = "UnitName";
        public const string UnitPrice = "UnitPrice";
        public const string Qty = "Qty";
        public const string ValInvoice = "ValInvoice";
        public const string ValTax = "ValTax";
        public const string InventoryCode = "InventoryCode";
        public const string DiscountRate = "DiscountRate";
        public const string ValDiscount = "ValDiscount";
        public const string InvoiceDtlStatus = "InvoiceDtlStatus";
        public const string Remark = "Remark";
        public const string InvoiceDCF1 = "InvoiceDCF1";
        public const string InvoiceDCF2 = "InvoiceDCF2";
        public const string InvoiceDCF3 = "InvoiceDCF3";
        public const string InvoiceDCF4 = "InvoiceDCF4";
        public const string InvoiceDCF5 = "InvoiceDCF5";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    #endregion

    #region["Report Server"]

    public class TblRptSv_Sys_User
    {
        public const string UserCode = "UserCode";
        public const string NetworkID = "NetworkID";
        public const string DLCode = "DLCode";
        public const string UserName = "UserName";
        public const string UserPassword = "UserPassword";
        public const string UserPasswordNew = "UserPasswordNew";
        public const string PhoneNo = "PhoneNo";
        public const string UserID = "UserID";
        public const string FlagSysAdmin = "FlagSysAdmin";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    #endregion

    #region["Import Excel Invoice"]

    public class TblInvoice_ImportExcel
    {
        // Master
        public const string InvoiceCode = "InvoiceCode";
        public const string InvoiceNo = "InvoiceNo";
        public const string FormNo = "FormNo";
        public const string Sign = "Sign";
        public const string Idx = "Idx";
        public const string InvoiceDateUTC = "InvoiceDateUTC";
        public const string InvoiceStatus = "InvoiceStatus";
        public const string CustomerNNTCode = "CustomerNNTCode";
        public const string CustomerNNTName = "CustomerNNTName";
        public const string CustomerNNTAddress = "CustomerNNTAddress";
        public const string CustomerNNTBuyerName = "CustomerNNTBuyerName";
        public const string CustomerMST = "CustomerMST";
        public const string CustomerNNTPhone = "CustomerNNTPhone";
        public const string CustomerNNTAccNo = "CustomerNNTAccNo";
        public const string EmailSend = "EmailSend";
        public const string MailSentDateTime = "MailSentDateTime";
        public const string PaymentMethodCode = "PaymentMethodCode";
        public const string Remark = "Remark";
        public const string InvoiceCF1 = "InvoiceCF1";
        public const string InvoiceCF2 = "InvoiceCF2";
        public const string InvoiceCF3 = "InvoiceCF3";
        public const string InvoiceCF4 = "InvoiceCF4";
        public const string InvoiceCF5 = "InvoiceCF5";
        public const string InvoiceCF6 = "InvoiceCF6";
        public const string InvoiceCF7 = "InvoiceCF7";
        public const string InvoiceCF8 = "InvoiceCF8";
        public const string InvoiceCF9 = "InvoiceCF9";
        public const string InvoiceCF10 = "InvoiceCF10";
        // Detail
        public const string SpecCode = "SpecCode";
        public const string SpecName = "SpecName";
        public const string VATRateCode = "VATRateCode";
        public const string VATRate = "VATRate";
        public const string UnitCode = "UnitCode";
        public const string UnitName = "UnitName";
        public const string UnitPrice = "UnitPrice";
        public const string Qty = "Qty";
        public const string ValInvoice = "ValInvoice";
        public const string ValTax = "ValTax";
        public const string InventoryCode = "InventoryCode";
        public const string DiscountRate = "DiscountRate";
        public const string ValDiscount = "ValDiscount";
        public const string RemarkDtl = "RemarkDtl";
        public const string InvoiceDCF1 = "InvoiceDCF1";
        public const string InvoiceDCF2 = "InvoiceDCF2";
        public const string InvoiceDCF3 = "InvoiceDCF3";
        public const string InvoiceDCF4 = "InvoiceDCF4";
        public const string InvoiceDCF5 = "InvoiceDCF5";
        public const string ImportResult = "ImportResult";
    }
    #endregion

    #region["InBrand Cloud"]

    #region["2019-12-04"]
    public class TblMst_Inventory
    {
        public const string InvCode = "InvCode";
        public const string InvName = "InvName";
        public const string InvCodeParent = "InvCodeParent";
        public const string InvLevelType = "InvLevelType";
        public const string InvType = "InvType";
        public const string FlagActive = "FlagActive";
        public const string FlagIn_Out = "FlagIn_Out";
        public const string InvAddress = "InvAddress";
        public const string InvContactName = "InvContactName";
        public const string InvContactEmail = "InvContactEmail";
        public const string InvContactPhone = "InvContactPhone";
        public const string Remark = "Remark";
        public const string InvBUPattern = "InvBUPattern";
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string InvBUCode = "InvBUCode";
    }
    public class TblMst_Agent
    {
        public const string AgentCode = "AgentCode";
        public const string NetworkID = "NetworkID";
        public const string ProvinceCode = "ProvinceCode";
        public const string DistrictCode = "DistrictCode";
        public const string AgentName = "AgentName";
        public const string AgentAddress = "AgentAddress";
        public const string FlagActive = "FlagActive";
    }
    public class TblMst_PartMaterialType
    {
        public const string PMType = "PMType";
        public const string PMTypeName = "PMTypeName";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
    }
    public class TblInvF_InventoryInFG
    {
        public const string IF_InvInFGNo = "IF_InvInFGNo";
        public const string FormInType = "FormInType";
        public const string InvInType = "InvInType";
        public const string DLCode = "DLCode";
        public const string MST = "MST";
        public const string InvCode = "InvCode";
        public const string PMType = "PMType";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string LUDTimeUTC = "LUDTimeUTC";
        public const string LUBy = "LUBy";
        public const string ApprDTime = "ApprDTime";
        public const string ApprBy = "ApprBy";
        public const string IF_InvInFGStatus = "IF_InvInFGStatus";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
        public const string ApprDTimeUTC = "ApprDTimeUTC";

    }
    public class TblInvF_InventoryInFGDtl
    {
        public const string IF_InvInFGNo = "IF_InvInFGNo";
        public const string PartCode = "PartCode";
        public const string Qty = "Qty";
        public const string ProductionDate = "ProductionDate";
        public const string IF_InvInFGStatusDtl = "IF_InvInFGStatusDtl";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    #endregion

    public class TblTemp_PrintTemp
    {
        public const string PrintTempCode = "PrintTempCode";
        public const string NetworkID = "NetworkID";
        public const string DLCode = "DLCode";
        public const string OrgID = "OrgID";
        public const string PrintTempDesc = "PrintTempDesc";
        public const string PrintTempBody = "PrintTempBody";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string LUDTimeUTC = "LUDTimeUTC";
        public const string LUBy = "LUBy";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string ApprBy = "ApprBy";
        public const string CancelDTimeUTC = "CancelDTimeUTC";
        public const string Cancel = "Cancel";
        public const string PrintTempStatus = "PrintTempStatus";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblView_ColumnInGroup
    {
        public const string GroupViewCode = "GroupViewCode";
        public const string NetworkID = "NetworkID";
        public const string ColumnViewCode = "ColumnViewCode";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
    }

    public class TblView_GroupView
    {
        public const string GroupViewCode = "GroupViewCode";
        public const string NetworkID = "NetworkID";
        public const string GroupViewName = "GroupViewName";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
    }
    public class TblView_ColumnView
    {
        public const string ColumnViewCode = "ColumnViewCode";
        public const string NetworkID = "NetworkID";
        public const string ColumnViewName = "ColumnViewName";
        public const string Remark = "Remark";
        public const string FlagActive = "FlagActive";
    }

    public class TblInv_GenTimes
    {
        public const string GenTimesNo = "GenTimesNo";
        public const string NetworkID = "NetworkID";
        public const string MST = "MST";
        public const string Qty = "Qty";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblInv_InventorySecret
    {
        public const string SerialNo = "SerialNo";
        public const string QR_SerialNo = "QR_SerialNo";
        public const string NetworkID = "NetworkID";
        public const string MST = "MST";
        public const string GenTimesNo = "GenTimesNo";
        public const string SecretNo = "SecretNo";
        public const string FlagMap = "FlagMap";
        public const string FlagUsed = "FlagUsed";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";

        public const string Idx = "Idx";
    }
    public class TblInv_InventoryBalanceSerial
    {
        public const string InvCode = "InvCode";
        public const string PartCode = "PartCode";
        public const string SerialNo = "SerialNo";
        public const string QR_SerialNo = "QR_SerialNo";
        public const string PMType = "PMType";
        public const string MST = "MST";
        public const string PartLotNo = "PartLotNo";
        public const string BoxNo = "BoxNo";
        public const string QR_BoxNo = "QR_BoxNo";
        public const string CanNo = "CanNo";
        public const string QR_CanNo = "QR_CanNo";
        public const string AgentCode = "AgentCode";
        public const string SecretNo = "SecretNo";
        public const string WarrantyDateStart = "WarrantyDateStart";
        public const string PackageDate = "PackageDate";
        public const string ProductionDate = "ProductionDate";
        public const string UserBox = "UserBox";
        public const string UserCan = "UserCan";
        public const string UserKCS = "UserKCS";
        public const string UserCheckPart = "UserCheckPart";
        public const string BlockStatus = "BlockStatus";
        public const string FlagNG = "FlagNG";
        public const string FlagMap = "FlagMap";
        public const string FlagUI = "FlagUI";
        public const string FlagSales = "FlagSales";
        public const string FlagBox = "FlagBox";
        public const string FlagCan = "FlagCan";
        public const string FormInType = "FormInType";
        public const string IF_InvInFGNo = "IF_InvInFGNo";
        public const string FormOutType = "FormOutType";
        public const string IF_InvOutFGNo = "IF_InvOutFGNo";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string InvDTime = "InvDTime";
        public const string InvBy = "InvBy";
        public const string BoxDTime = "BoxDTime";
        public const string BoxBy = "BoxBy";
        public const string CanDTime = "CanDTime";
        public const string CanBy = "CanBy";
        public const string OutDTime = "OutDTime";
        public const string OutBy = "OutBy";
        public const string ShiftInCode = "ShiftInCode";
        public const string PrintDate = "PrintDate";
    }
    public class TblMst_Part
    {
        public const string PartCode = "PartCode";
        public const string PartBarCode = "PartBarCode";
        public const string PartName = "PartName";
        public const string PartNameFS = "PartNameFS";
        public const string PartDesc = "PartDesc";
        public const string PartType = "PartType";
        public const string SerialNo = "SerialNo";
        public const string PMType = "PMType";
        public const string PartUnitCodeStd = "PartUnitCodeStd";
        public const string PartUnitCodeDefault = "PartUnitCodeDefault";
        public const string QtyMaxSt = "QtyMaxSt";
        public const string QtyMinSt = "QtyMinSt";
        public const string QtyEffSt = "QtyEffSt";
        public const string UPIn = "UPIn";
        public const string UPOut = "UPOut";
        public const string FilePath = "FilePath";
        public const string ImagePath = "ImagePath";
        public const string QtyEffMonth = "QtyEffMonth";
        public const string PartOrigin = "PartOrigin";
        public const string PartComponents = "PartComponents";
        public const string InstructionForUse = "InstructionForUse";
        public const string PartStorage = "PartStorage";
        public const string UrlMnfSequence = "UrlMnfSequence";
        public const string MnfStandard = "MnfStandard";
        public const string PartStyle = "PartStyle";
        public const string PartIntroduction = "PartIntroduction";
        public const string FlagBOM = "FlagBOM";
        public const string FlagVirtual = "FlagVirtual";
        public const string FlagInputLot = "FlagInputLot";
        public const string FlagInputSerial = "FlagInputSerial";
        public const string FlagActive = "FlagActive";
        public const string RemarkForEffUsed = "RemarkForEffUsed";
    }
    public class TblInvF_InventoryOutFG
    {
        public const string IF_InvOutFGNo = "IF_InvOutFGNo";
        public const string FormOutType = "FormOutType";
        public const string InvOutType = "InvOutType";
        public const string InvCode = "InvCode";
        public const string DLCode = "DLCode";
        public const string PMType = "PMType";
        public const string CreateDTimeSv = "CreateDTimeSv";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBySv = "CreateBySv";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string ApprBy = "ApprBy";
        public const string IF_InvOutFGStatus = "IF_InvOutFGStatus";
        public const string Remark = "Remark";
        public const string MST = "MST";
        public const string InvFOutType = "InvFOutType";
        public const string PlateNo = "PlateNo";
        public const string MoocNo = "MoocNo";
        public const string DriverName = "DriverName";
        public const string DriverPhoneNo = "DriverPhoneNo";
        public const string AgentCode = "AgentCode";
        public const string CustomerName = "CustomerName";
    }
    public class TblInvF_InventoryOutFGDtl
    {
        public const string IF_InvOutFGNo = "IF_InvOutFGNo";
        public const string PartCode = "PartCode";
        public const string Qty = "Qty";
        public const string IF_InvOutFGStatusDtl = "IF_InvOutFGStatusDtl";
        public const string Remark = "Remark";
    }
    public class TblMst_PartUnit
    {
        public const string PartUnitCode = "PartUnitCode";
        public const string PartUnitName = "PartUnitName";
        public const string FlagActive = "FlagActive";
        public const string FlagUnitStd = "FlagUnitStd";
        public const string Remark = "Remark";
    }
    public class TblMst_PartType
    {
        public const string PartType = "PartType";
        public const string PartTypeName = "PartTypeName";
        public const string FlagActive = "FlagActive";
        public const string Remark = "Remark";
    }
    public class TblInv_GenTimesBox
    {
        public const string GenTimesBoxNo = "GenTimesBoxNo";
        public const string NetworkID = "NetworkID";
        public const string OrgID = "OrgID";
        public const string Qty = "Qty";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";

    }
    public class TblInv_GenTimesCarton
    {
        public const string GenTimesCartonNo = "GenTimesCartonNo";
        public const string NetworkID = "NetworkID";
        public const string OrgID = "OrgID";
        public const string Qty = "Qty";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblInv_InventoryBox
    {
        public const string BoxNo = "BoxNo";
        public const string NetworkID = "NetworkID";
        public const string OrgID = "OrgID";
        public const string GenTimesBoxNo = "GenTimesBoxNo";
        public const string SecretNo = "SecretNo";
        public const string QR_BoxNo = "QR_BoxNo";
        public const string FlagMap = "FlagMap";
        public const string FlagUsed = "FlagUsed";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblInv_InventoryCarton
    {
        public const string CanNo = "CanNo";
        public const string NetworkID = "NetworkID";
        public const string OrgID = "OrgID";
        public const string MST = "MST";
        public const string GenTimesCartonNo = "GenTimesCartonNo";
        public const string QR_CanNo = "QR_CanNo";
        public const string FlagMap = "FlagMap";
        public const string FlagUsed = "FlagUsed";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblRpt_InvFInventoryInFGSum
    {
        public const string InvCode = "InvCode";
        public const string DLCode = "DLCode";
        public const string PartCode = "PartCode";
        public const string PartName = "PartName";
        public const string TotalQtyIn = "TotalQtyIn";
    }
    public class TblRpt_InvFInventoryOutFGSum
    {
        public const string InvCode = "InvCode";
        public const string DLCode = "DLCode";
        public const string AgentCode = "AgentCode";
        public const string AgentName = "AgentName";
        public const string PartCode = "PartCode";
        public const string PartName = "PartName";
        public const string TotalQtyOut = "TotalQtyOut";
    }
    public class TblRpt_InvInventoryBalanceMonth
    {
        public const string InvCode = "InvCode";
        public const string PartCode = "PartCode";
        public const string PartName = "PartName";
        public const string PartType = "PartType";
        public const string PartTypeName = "PartTypeName";
        public const string TotalQtyInvBegin = "TotalQtyInvBegin";
        public const string TotalQtyIn = "TotalQtyIn";
        public const string TotalQtyOut = "TotalQtyOut";
        public const string TotalQtyInvEnd = "TotalQtyInvEnd";
    }
    public class TblInv_InventoryBalance
    {
        public const string InvCode = "InvCode";
        public const string PartCode = "PartCode";
        public const string NetworkID = "NetworkID";
        public const string MST = "MST";
        public const string QtyTotalOK = "QtyTotalOK";
        public const string QtyBlockOK = "QtyBlockOK";
        public const string QtyAvailOK = "QtyAvailOK";
        public const string QtyTotalNG = "QtyTotalNG";
        public const string QtyBlockNG = "QtyBlockNG";
        public const string QtyAvailNG = "QtyAvailNG";
        public const string QtyPlanTotal = "QtyPlanTotal";
        public const string QtyPlanBlock = "QtyPlanBlock";
        public const string QtyPlanAvail = "QtyPlanAvail";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
        public const string ProductCode = "ProductCode";
    }
    public class TblInvF_InventoryOutFGInstSerial
    {
        public const string IF_InvOutFGNo = "IF_InvOutFGNo";
        public const string PartCode = "PartCode";
        public const string SerialNo = "SerialNo";
        public const string IF_InvOutFGISStatus = "IF_InvOutFGISStatus";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }
    public class TblInvF_InventoryInFGInstSerial
    {
        public const string IF_InvInFGNo = "IF_InvInFGNo";
        public const string PartCode = "PartCode";
        public const string SerialNo = "SerialNo";
        public const string IF_InvInFGISStatus = "IF_InvInFGISStatus";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public class TblMst_InventoryType
    {
        public const string InvType = "InvType";
        public const string InvTypeName = "InvTypeName";
        public const string FlagActive = "FlagActive";
        public const string LogLUDTime = "LogLUDTime";
        public const string LogLUBy = "LogLUBy";
    }
    #endregion

    #region ["Inventory"]
    public class TblMst_InventoryLevelType
    {
        public const string OrgID = "OrgID";
        public const string InvLevelType = "InvLevelType";
        public const string InvLevelTypeName = "InvLevelTypeName";
        public const string FlagActive = "FlagActive";
    }

    public class TblMst_InvInType
    {
        public const string InvInType = "InvCode";
        public const string InvInTypeName = "InvInTypeName";
        public const string FlagActive = "FlagActive";
        public const string FlagStatistic = "FlagStatistic";
    }

    public class TblMst_InvOutType
    {
        public const string InvOutType = "InvOutType";
        public const string InvOutTypeName = "InvOutTypeName";
        public const string FlagActive = "FlagActive";
        public const string FlagStatistic = "FlagStatistic";
    }

    public class tblMst_Inventory
    {
        public const string InvCode = "InvCode";
        public const string InvName = "InvName";
        public const string FlagActive = "FlagActive";
        public const string FlagIn_Out = "FlagIn_Out";        
    }

    public class TblInvF_MoveOrd
    {
        public const string IF_MONo = "IF_MONo";
        public const string MoveOrdType = "MoveOrdType";
        public const string InvCodeOut = "InvCodeOut";
        public const string InvCodeIn = "InvCodeIn";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string IF_MOStatus = "IF_MOStatus";
        public const string Remark = "Remark";
    }


    public class TblInvF_InventoryOut
    {
        public const string IF_InvOutNo = "IF_InvOutNo";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string InvCodeIn = "InvCodeIn";
        public const string InvOutType = "InvOutType";
        public const string CustomerCode = "CustomerCode";
        public const string InvCodeOut = "InvCodeOut";
        public const string IF_InvOutStatus = "IF_InvOutStatus";
        public const string OrgID = "OrgID";
        public const string RefNo = "RefNo";
        public const string ProfileStatus = "ProfileStatus";
    }

    public class TblInvF_InventoryReturnSup
    {
        public const string IF_InvReturnSupNo = "IF_InvReturnSupNo";
        public const string InvCodeOut = "InvCodeOut";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string CustomerCode = "CustomerCode";
        public const string IF_InvInNo = "IF_InvInNo";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string OrgID = "OrgID";
        public const string IF_ReturnSupStatus = "IF_ReturnSupStatus";
    }

    public class Tbl_InvF_InvAudit
    {
        public const string IF_InvAudNo = "IF_InvAudNo";
        public const string InvCodeAudit = "InvCodeAudit";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string IF_InvAuditStatus = "IF_InvAuditStatus";
        public const string OrgID = "OrgID";
    }

    public class Tbl_InvF_InvAuditDtl
    {
        public const string ProductCode = "ProductCode";
    }

    public class TblInvF_InventoryIn
    {
        public const string IF_InvInNo = "IF_InvInNo";
        public const string InvInType = "InvInType";
        public const string InvCodeIn = "InvCodeIn";
        public const string CustomerCode = "CustomerCode";
        public const string OrgID = "OrgID";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string ApprBy = "ApprBy";
        public const string IF_InvInStatus = "IF_InvInStatus";
        public const string IF_InvAudNo = "IF_InvAudNo";
        public const string InvoiceNo = "InvoiceNo";
        public const string OrderNo = "OrderNo";
        public const string UserDeliver = "UserDeliver";
        public const string FlagQR = "FlagQR";
        public const string Remark = "Remark";
        public const string RefNo = "RefNo";
    }

    public class TblInvF_InventoryInDtl
    {
        public const string IF_InvInNo = "IF_InvInNo";
        public const string InvCodeInActual = "InvCodeInActual";
        public const string ProductCode = "ProductCode";
        public const string NetworkID = "NetworkID";
        public const string Qty = "Qty";
        public const string UPIn = "UPIn";
        public const string UPInDesc = "UPInDesc";
        public const string ValInvIn = "ValInvIn";
        public const string ValInDesc = "ValInDesc";
        public const string ValInAfterDesc = "ValInAfterDesc";
        public const string UnitCode = "UnitCode";
        public const string IF_InvInStatusDtl = "IF_InvInStatusDtl";
        public const string Remark = "Remark";
    }

    public class TblInvF_InventoryCusReturn
    {
        public const string IF_InvCusReturnNo = "IF_InvCusReturnNo";
        public const string NetworkID = "NetworkID";
        public const string OrgID = "OrgID";
        public const string InvInType = "InvInType";
        public const string InvCodeIn = "InvCodeIn";
        public const string CustomerCode = "CustomerCode";
        public const string OrderNo = "OrderNo";
        public const string InvoiceNo = "InvoiceNo";
        public const string OrderType = "OrderType";
        public const string TotalValCusReturn = "TotalValCusReturn";
        public const string TotalValCusReturnDesc = "TotalValCusReturnDesc";
        public const string TotalValCusReturnAfterDesc = "TotalValCusReturnAfterDesc";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string ApprBy = "ApprBy";
        public const string CancelDTimeUTC = "CancelDTimeUTC";
        public const string CancelBy = "CancelBy";
        public const string IF_InvCusReturnStatus = "IF_InvCusReturnStatus";
        public const string Remark = "Remark";
        public const string RefNo = "RefNo";
    }

    public class TblInvF_InventoryCusReturnDtl
    {
        public const string IF_InvCusReturnNo = "IF_InvCusReturnNo";
        public const string InvCodeInActual = "InvCodeInActual";
        public const string ProductCodeRoot = "ProductCodeRoot";
        public const string ProductCode = "ProductCode";
        public const string NetworkID = "NetworkID";
        public const string Qty = "Qty";
        public const string UnitCode = "UnitCode";
        public const string IF_InvCusReturnStatusDtl = "IF_InvCusReturnStatusDtl";
        public const string Remark = "Remark";
    }

    public class TblPrd_BOM
    {
        public const string ProductCode = "ProductCode";
        public const string ProductCodeParent = "ProductCodeParent";
    }


    public class TblOrd_OrderPD
    {
        public const string OrderPDNoSys = "OrderPDNoSys";
        public const string NetworkID = "NetworkID";
        public const string OrgID = "OrgID";
        public const string OrderType = "OrderType";
        public const string OrderPDNo = "OrderPDNo";
        public const string CustomerCodeSys = "CustomerCodeSys";
        public const string CustomerCode = "CustomerCode";
        public const string CustomerName = "CustomerName";
        public const string EstimatedDeliverDate = "EstimatedDeliverDate";
        public const string ShipperAddress = "ShipperAddress";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string ApprBy = "ApprBy";
        public const string ApprDTimeUTC = "ApprDTimeUTC";
        public const string CancelDTimeUTC = "CancelDTimeUTC";
        public const string CancelBy = "CancelBy";
        public const string OrderStatus = "OrderStatus";
        public const string Json = "Json";
        public const string Remark = "Remark";
        public const string LogLUDTimeUTC = "LogLUDTimeUTC";
        public const string LogLUBy = "LogLUBy";
    }

    public static class Breadcrumb_Code
    {
        public const string Dashboard = "Dashboard";
        public const string InvFInventoryReturnSup = "InvFInventoryReturnSup";
        public const string InvF_InventoryCusReturn = "InvF_InventoryCusReturn";
        public const string InvF_InventoryIn = "InvF_InventoryIn";
        public const string InvF_InventoryOut = "InvF_InventoryOut";

        // 
        public const string InvF_MoveOrd = "InvF_MoveOrd";
        public const string InvFInvAudit = "InvFInvAudit";

        //
        public const string Mst_Customer = "Mst_Customer ";
        public const string Mst_Product = "Mst_Product";

        // Common
        public const string Create = "Create";
        public const string Update = "Update";
        public const string Detail = "Detail";

        // Vùng thị trường
        public const string Mst_Area = "Mst_Area";
        public const string Mst_Area_Create = "Create";
        public const string Mst_Area_Update = "Update";
        public const string Mst_Area_Detail = "Detail";

        // Report
        public const string Report = "Report";
        public const string Rpt_Inv_InventoryBalance = "Rpt_Inv_InventoryBalance";
        public const string Rpt_Inv_InventoryBalance_Extend = "Rpt_Inv_InventoryBalance_Extend";
        public const string Rpt_InvF_WarehouseCard = "Rpt_InvF_WarehouseCard";
        public const string Rpt_Inventory_In_Out_Inv = "Rpt_Inventory_In_Out_Inv";
        public const string Rpt_Summary_In_Out = "Rpt_Summary_In_Out";
        public const string Rpt_Summary_QtyInvByPeriod = "Rpt_Summary_QtyInvByPeriod";
        public const string Rpt_Summary_InAndReturnSup = "Rpt_Summary_InAndReturnSup";
        public const string Rpt_Summary_In_Pivot = "Rpt_Summary_In_Pivot";
        public const string Rpt_Summary_In_Out_Sup_Pivot = "Rpt_Summary_In_Out_Sup_Pivot";
        public const string Rpt_InvBalLot_MaxExpiredDateByInv = "Rpt_InvBalLot_MaxExpiredDateByInv";
        public const string Rpt_MapDeliveryOrder_ByInvFIOut = "Rpt_MapDeliveryOrder_ByInvFIOut";
        public const string Rpt_Inv_InventoryBalance_StorageTime = "Rpt_Inv_InventoryBalance_StorageTime";
        public const string Rpt_Inv_InventoryBalance_Minimum = "Rpt_Inv_InventoryBalance_Minimum";

        // Ma trận phân quyền thông báo
        public const string Map_UserInNotifyType = "Map_UserInNotifyType";

        // Thông báo
        public const string Notification = "Notification";
        //master quản trị
        public const string Mst_Inventory = "Mst_Inventory";
        public const string Mst_InventoryLevelType = "Mst_InventoryLevelType";
        public const string Mst_InventoryType = "Mst_InventoryType";
        public const string Mst_InvInType = "Mst_InvInType";
        public const string Mst_InvOutType = "Mst_InvOutType";
        public const string Mst_UserMapInventory = "Mst_UserMapInventory";
        public const string InvFTempPrint = "InvFTempPrint";
        public const string Mst_Organization = "Mst_Organization";
        public const string Product_CustomField = "Product_CustomField";
        //người dùng
        public const string Sys_User = "Sys_User";
        public const string Sys_User_Create = "Create";
        public const string Sys_User_Update = "Update";
        public const string Sys_User_Detail = "Detail";
        //nhóm người dùng
        public const string Sys_Group = "Sys_Group";
        public const string Sys_Group_Create = "Create";
        public const string Sys_Group_Update = "Update";
        public const string Sys_Group_Detail = "Detail";



        //Hiển thị số thập phân
        public const string Mst_ColumnConfigGroup = "Mst_ColumnConfigGroup";
        public const string Mst_ColumnConfigGroup_Update = "Update";

        //Cấu hình hệ thống
        public const string Mst_Sys_Config = "Mst_Sys_Config";

        // Nhóm hàng
        public const string Mst_Productgroupsub = "Mst_Productgroupsub";

        // Nhãn hiệu
        public const string MstBrand = "MstBrand";

        //Quản lý nguồn khách hàng
        public const string Mst_Customersource = "Mst_Customersource";

        //Quản lý nhóm khách hàng
        public const string Mst_Customergroup = "Mst_Customergroup";
    }
    public static class Breadcrumb_Name
    {
        public const string Dashboard = "Dashboard";

        public const string InvF_InventoryCusReturn = "Khách hàng trả hàng";
        public const string InvFInventoryReturnSup = "Trả hàng NCC";
        public const string InvF_InventoryIn = "Nhập kho";
        public const string InvF_InventoryOut = "Xuất kho";
        // Khuyến mại
        public const string InvF_MoveOrd = "Điều chuyển";
        public const string InvFInvAudit = "Kiểm kê";

        public const string Mst_Customer = "Khách hàng";
        public const string Mst_Product = "Hàng hóa";

        //
        public const string Report = "Báo cáo";

        public const string Rpt_Inv_InventoryBalance = "Báo cáo tồn kho";
        public const string Rpt_Inv_InventoryBalance_Extend = "Báo cáo tồn kho mở rộng";
        public const string Rpt_InvF_WarehouseCard = "Thẻ kho";
        public const string Rpt_Inventory_In_Out_Inv = "Báo cáo xuất nhập tồn";
        public const string Rpt_Summary_In_Out = "Báo cáo nhập xuất theo kỳ";
        public const string Rpt_Summary_QtyInvByPeriod = "Báo cáo tồn kho theo kỳ";
        public const string Rpt_Summary_InAndReturnSup = "Báo cáo nhập kho theo NCC";
        public const string Rpt_Summary_In_Pivot = "Báo cáo tổng hợp nhập";
        public const string Rpt_Summary_In_Out_Sup_Pivot = "Báo cáo lịch sử giao dịch nhập xuất theo đối tác";
        public const string Rpt_InvBalLot_MaxExpiredDateByInv = "Báo cáo hạn sử dụng hàng hóa";
        public const string Rpt_MapDeliveryOrder_ByInvFIOut = "Báo cáo bản đồ lệnh giao hàng";
        public const string Rpt_Inv_InventoryBalance_StorageTime = "Báo cáo tuổi tồn kho";
        public const string Rpt_Inv_InventoryBalance_Minimum = "Báo cáo chạm tồn tối thiểu";
        //Ma trận phân quyền thông báo
        public const string Map_UserInNotifyType = "Ma trận phân quyền thông báo";

        // Thông báo
        public const string Notification = "Thông báo";
        public const string Notification_Setting = "Cài đặt thông báo";

        // Common
        public const string Create = "Tạo mới";
        public const string Update = "Cập nhật";
        public const string Detail = "Chi tiết";
        public const string ExportCross = "Phiếu xuất chéo";

        // Kiểm kê
        public const string Action = "Thực hiện phiếu kiểm kê";

        //master quản trị
        public const string Mst_Inventory = "Quản lý kho";
        public const string Mst_InventoryLevelType = "Quản lý cấp kho";
        public const string Mst_InventoryType = "Quản lý loại kho";
        public const string Mst_InvInType = "Quản lý loại nhập kho";
        public const string Mst_InvOutType = "Quản lý loại xuất kho";
        public const string Mst_UserMapInventory = "Phân quyền kho";
        public const string InvFTempPrint = "Quản lý mẫu in";
        public const string Product_CustomField = "Thiết lập trường động hàng hoá";


        public const string Mst_Organization = "Quản lý chi nhánh";
        //người dùng
        public const string Sys_User = "Quản lý người dùng";
        public const string Sys_User_Create = "Tạo mới";
        public const string Sys_User_Update = "Sửa";
        public const string Sys_User_Detail = "Chi tiết";
        //nhóm người dùng
        public const string Sys_Group = "Quản lý nhóm người dùng";
        public const string Sys_Group_Create = "Tạo mới";
        public const string Sys_Group_Update = "Sửa";
        public const string Sys_Group_Detail = "Chi tiết";


        //Vùng thị trường

        public const string Mst_Area = "Quản lý vùng thị trường";
        public const string Mst_Area_Create = "Tạo mới";
        public const string Mst_Area_Detail = "Chi tiết";
        public const string Mst_Area_Update = "Sửa";


        //Hiển thị số thập phân
        public const string Mst_ColumnConfigGroup = "Hiển thị số thập phân";
        public const string Mst_ColumnConfigGroup_Update = "Sửa";

        //Cấu hình hệ thống

        public const string Mst_Sys_Config = "Cấu hình hệ thống";

        //Quản lý nhóm hàng
        public const string Mst_Productgroupsub = "Quản lý nhóm hàng";

        //Quản lý nhãn hiệu
        public const string MstBrand = "Quản lý nhãn hiệu";

        //Quản lý nguồn khách hàng
        public const string Mst_Customersource = "Quản lý nguồn khách hàng";

        //Quản lý nhóm khách hàng
        public const string Mst_Customergroup = "Quản lý nhóm khách hàng";

    }
    #endregion

    #region ["ProductCenter"]

    #region ["Product Level System"]
    public class Client_ProductLevelSys
    {
        public const string RootPrd = "ROOTPRD";
        public const string BasePrd = "BASEPRD";
        public const string L2Prd = "L2PRD";
    }
    public class Client_DelProductLevelSys
    {
        public const string RootPrd = "ROOTPRD";
        public const string BasePrd = "BASEPRD";
        public const string L2Prd = "L2PRD";
        public const string RootBasePrd = "ROOTBASEPRD";
    }

    public class ProductLevelSys
    {
        public const string RootPrd = "ROOTPRD";
        public const string BasePrd = "BASEPRD";
        public const string L2Prd = "L2PRD";
    }
    #endregion

    public class TblMst_Product
    {
        public const string OrgID = "OrgID";
        public const string ProductCode = "ProductCode";
        public const string NetworkID = "NetworkID";
        public const string ProductLevelSys = "ProductLevelSys";
        public const string ProductCodeUser = "ProductCodeUser";
        public const string BrandCode = "BrandCode";
        public const string ProductType = "ProductType";
        public const string ProductGrpCode = "ProductGrpCode";
        public const string ProductName = "ProductName";
        public const string ProductNameEN = "ProductNameEN";
        public const string ProductBarCode = "ProductBarCode";
        public const string ProductCodeNetwork = "ProductCodeNetwork";
        public const string ProductCodeBase = "ProductCodeBase";
        public const string ProductCodeRoot = "ProductCodeRoot";
        public const string ProductImagePathList = "ProductImagePathList";
        public const string ProductFilePathList = "ProductFilePathList";
        public const string FlagSerial = "FlagSerial";
        public const string FlagLot = "FlagLot";
        public const string ValConvert = "ValConvert";
        public const string UnitCode = "UnitCode";
        public const string FlagSell = "FlagSell";
        public const string FlagBuy = "FlagBuy";
        public const string UPBuy = "UPBuy";
        public const string UPSell = "UPSell";
        public const string QtyMaxSt = "QtyMaxSt";
        public const string QtyMinSt = "QtyMinSt";
        public const string QtyEffSt = "QtyEffSt";
        public const string ListOfPrdDynamicFieldValue = "ListOfPrdDynamicFieldValue";
        public const string ProductStd = "ProductStd";
        public const string ProductExpiry = "ProductExpiry";
        public const string ProductQuyCach = "ProductQuyCach";
        public const string ProductMnfUrl = "ProductMnfUrl";
        public const string ProductIntro = "ProductIntro";
        public const string ProductUserGuide = "ProductUserGuide";
        public const string ProductDrawing = "ProductDrawing";
        public const string ProductOrigin = "ProductOrigin";
        public const string CreateDTimeUTC = "CreateDTimeUTC";
        public const string CreateBy = "CreateBy";
        public const string FlagActive = "FlagActive";
        public const string Remark = "Remark";
        public const string ProductDelType = "ProductDelType";
        public const string ListBOM = "ListBOM";
        public const string ListAttribute = "ListAttribute";
        public const string VATRateCode = "VATRateCode";
        public const string FlagFG = "FlagFG";
    }

    public class TblPrd_Attribute
    {
        public const string AttributeCode = "AttributeCode";
        public const string AttributeValue = "AttributeValue";
    }
    #endregion

    #region ["Veloca"]
    public class TblSer_RO
    {
        public const string RONoSys = "RONoSys";
        public const string OrgID = "OrgID";
        public const string NetworkID = "NetworkID";
        public const string RONo = "RONo";
        public const string CustomerCodeSys = "CustomerCodeSys";
        public const string CustomerCode = "CustomerCode";
        public const string CustomerName = "CustomerName";
        public const string CustomerNameEN = "CustomerNameEN";
        public const string CustomerAddress = "CustomerAddress";
        public const string CustomerMobilePhone = "CustomerMobilePhone";
        public const string CustomerPhoneNo = "CustomerPhoneNo";
        public const string CustomerEmail = "CustomerEmail";
        public const string CustomerContactName = "CustomerContactName";
        public const string CustomerContactPhone = "CustomerContactPhone";
        public const string CustomerContactEmail = "CustomerContactEmail";
        public const string RequestCustomer = "RequestCustomer";
        public const string PlateNoSys = "PlateNoSys";
        public const string PlateNo = "PlateNo";
        public const string VIN = "VIN";
        public const string ColorCode = "ColorCode";
        public const string BrandCode = "BrandCode";
        public const string ProductGrpCode = "ProductGrpCode";
        public const string ProductGrpName = "ProductGrpName";
        public const string ProductCode = "ProductCode";
        public const string WarrantyDateUTC = "WarrantyDateUTC";
        public const string EngineNo = "EngineNo";
        public const string FlagPlateNo = "FlagPlateNo";
        public const string AppDTimeUTC = "AppDTimeUTC";
        public const string InsCodeSys = "InsCodeSys";
        public const string InsCode = "InsCode";
        public const string InGarageDTimeUTC = "InGarageDTimeUTC";
        public const string InGarageBy = "InGarageBy";
        public const string RepairedDTimeUTC = "RepairedDTimeUTC";
        public const string RepairedBy = "RepairedBy";
        public const string CheckInDTimeUTC = "CheckInDTimeUTC";
        public const string CheckInBy = "CheckInBy";
        public const string StartDTimeUTC = "StartDTimeUTC";
        public const string StartBy = "StartBy";
        public const string CheckEndDTimeUTC = "CheckEndDTimeUTC";
        public const string CheckEndBy = "CheckEndBy";
        public const string PaidDTimeUTC = "PaidDTimeUTC";
        public const string FinishDTimeUTC = "FinishDTimeUTC";
        public const string FinishBy = "FinishBy";
        public const string CancelDTimeUTC = "CancelDTimeUTC";
        public const string CancelBy = "CancelBy";
        public const string ROStatus = "ROStatus";
        public const string RepairStatus = "RepairStatus";
        public const string FlagStop = "FlagStop";
        public const string FlagService = "FlagService";
        public const string FlagServiceDtl = "FlagServiceDtl";
        public const string FlagRORepair = "FlagRORepair";
        public const string Remark = "Remark";
        public const string LogLUDTime = "LogLUDTime";
        public const string PlanedDeliveryDTimeUTC = "PlanedDeliveryDTimeUTC";
        public const string EngineerNo = "EngineerNo";
        public const string CreateBy = "CreateBy";
        public const string CreateName = "CreateName";
        public const string TermsOfRepair = "TermsOfRepair";
        public const string ActualDeliveryDTimeUTC = "ActualDeliveryDTimeUTC";
        public const string TotalValEnd = "TotalValEnd";
        public const string TotalValDisAfterVATRO = "TotalValDisAfterVATRO";

        public const string TotalValBeforeVATRO = "TotalValBeforeVATRO";
        public const string TotalValVATRO = "TotalValVATRO";
        public const string TotalValAfterVATRO = "TotalValAfterVATRO";
        public const string ReminderMaintanceKm = "ReminderMaintanceKm";        
    }

    public class TblSer_ROProductPart
    {
        public const string RONoSys = "RONoSys";
        public const string ProductCodePart = "ProductCodePart";
        public const string ProductCodeUserPart = "ProductCodeUserPart";
        public const string ProductNamePart = "ProductNamePart";
    }
    #endregion

    #region["LQDMS"]
    public class TblMst_PrintOrder
    {
        public const string PrintOrdNo = "PrintOrdNo";
    }
    #endregion
    #endregion
}





