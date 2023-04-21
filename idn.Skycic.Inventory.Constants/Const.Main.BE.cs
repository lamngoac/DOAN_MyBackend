using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace idn.Skycic.Inventory.Constants
{
    public class BizMix
	{
		public const string Default_PasswordMask = "*********";
		public const string Default_DBColType = "nvarchar(400)";
		public const string MyText_DBColType = "ntext";
		public const int Default_RootYear = 2010;
		public const string TVAN = "222";
		public const int Default_MaxCallWS = 3;
		public const int Default_MaxMinutesToCheckToken = 30;
		public const string DealerCodeRoot = "HTC";
		public const string BankCodeRoot = "ALL";
		public const string BankPrdCodeRoot = "ALL";
		public const string DBCodeRoot = "DVN";
		public const string AreaCodeRoot = "VN";
		public const string CostCenterRoot = "C";
		public const string AccountRoot = "A";
		public const string OrganRoot = "O";
		public const string OrgIDRoot = "0";
		public const string InvRoot = "I";
		public const string CurrencyCodeRoot = "VND";
		public const string RecordStart = "0";
		public const string RecordCount = "123456000";
        //public const double Default_Epsilon = 0.000001; // 20210405
        public const double Default_Epsilon = 0.0001; // 20210405
        public const double CB_Epsilon = 100;
		public const double Default_AuthMinutes = 10.0;
		public const string Max_DBCol = "ntext";
        public const string MSTALL = "ALL";
		public const string DLRoot = "VN";
        public const Int64 nQtyMaxGen = 100000;
        public const string CustomerCodeSysRoot = "ALL";
    }

	public class Mst_Param
	{
		public const string Param1 = "PARAM1";
		public const string Param2 = "PARAM2";
		public const string Param3 = "PARAM3";
		public const string PARAM_QINVOICEORDADMIN_USERCODE = "PARAM_QINVOICEORDADMIN_USERCODE";
		public const string PARAM_QINVOICEORDADMIN_USERPASSWORD = "PARAM_QINVOICEORDADMIN_USERPASSWORD";
		//public const string PARAM_OS_REPORTSERVER_SOLUTION_API_URL = "PARAM_OS_REPORTSERVER_SOLUTION_API_URL";
		public const string PARAM_OS_REPORTSERVER_BG_WAUSERCODE = "PARAM_OS_REPORTSERVER_BG_WAUSERCODE";
		public const string PARAM_OS_REPORTSERVER_BG_WAUSERPASSWORD = "PARAM_OS_REPORTSERVER_BG_WAUSERPASSWORD";
		public const string PARAM_UPLOADFILE = "PARAM_UPLOADFILE";
		public const string PARAM_SOLUTIONCODE = "PARAM_SOLUTIONCODE";
		public const string PARAM_APISSENDMAIL = "PARAM_APISSENDMAIL";
		public const string PARAM_APIKEYSENDMAIL = "PARAM_APIKEYSENDMAIL";
		public const string PARAM_DISPLAYNAMEMAILFROM = "PARAM_DISPLAYNAMEMAILFROM";
		public const string PARAM_MAILFROM = "PARAM_MAILFROM";
		public const string PARAM_SKYCICBASEURL = "PARAM_SKYCICBASEURL";
		public const string Param_Biz_DoubleCommit = "PARAM_BIZ_DOUBLECOMMIT";

		public const string INVENTORY_RptSvLocal_URL = "INVENTORY_RPTSVLOCAL_URL";
		public const string INVENTORY_RptSvLocal_GwUserCode = "INVENTORY_RPTSVLOCAL_GWUSERCODE";
		public const string INVENTORY_RptSvLocal_GwPassword = "INVENTORY_RPTSVLOCAL_GWPASSWORD";
		public const string INVENTORY_RptSvLocal_WAUserCode = "INVENTORY_RPTSVLOCAL_WAUSERCODE";
		public const string INVENTORY_RptSvLocal_WAUserPassword = "INVENTORY_RPTSVLOCAL_WAUSERPASSWORD";

		public const string INVENTORY_MstSv_URL = "INVENTORY_MSTSV_URL";
		public const string INVENTORY_MstSv_GwUserCode = "INVENTORY_MSTSV_GWUSERCODE";
		public const string INVENTORY_MstSv_GwPassword = "INVENTORY_MSTSV_GWPASSWORD";
		public const string INVENTORY_MstSv_WAUserCode = "INVENTORY_MSTSV_WAUSERCODE";
		public const string INVENTORY_MstSv_WAUserPassword = "INVENTORY_MSTSV_WAUSERPASSWORD";

		public const string INVENTORY_RPTSV_URL = "INVENTORY_RPTSV_URL";
		public const string INVENTORY_RPTSV_GWUSERCODE = "INVENTORY_RPTSV_GWUSERCODE";
		public const string INVENTORY_RPTSV_GWPASSWORD = "INVENTORY_RPTSV_GWPASSWORD";
		public const string INVENTORY_RPTSV_WAUSERCODE = "INVENTORY_RPTSV_WAUSERCODE";
		public const string INVENTORY_RPTSV_WAUSERPASSWORD = "INVENTORY_RPTSV_WAUSERPASSWORD";
		////

		public const string PRODUCTCENTER_MSTSV_URL = "PRODUCTCENTER_MSTSV_URL";
		public const string PRODUCTCENTER_MSTSV_GWUSERCODE = "PRODUCTCENTER_MSTSV_GWUSERCODE";
		public const string PRODUCTCENTER_MSTSV_GWPASSWORD = "PRODUCTCENTER_MSTSV_GWPASSWORD";
		public const string PRODUCTCENTER_MSTSV_WAUSERCODE = "PRODUCTCENTER_MSTSV_WAUSERCODE";
		public const string PRODUCTCENTER_MSTSV_WAUSERPASSWORD = "PRODUCTCENTER_MSTSV_WAUSERPASSWORD";
        
        public const string INVENTORY_LQDMS_PRODUCTCODEUSER = "INVENTORY_LQDMS_PRODUCTCODEUSER";
        public const string OS_LQDMS_API_URL = "OS_LQDMS_API_URL";
        public const string OS_LQDMS_TOKENID = "OS_LQDMS_TOKENID";
        public const string OS_LQDMS_GWUSERCODE = "OS_LQDMS_GWUSERCODE";
        public const string OS_LQDMS_GWPASSWORD = "OS_LQDMS_GWPASSWORD";
        public const string OS_LQDMS_BG_WAUSERCODE = "OS_LQDMS_BG_WAUSERCODE";
        public const string OS_LQDMS_BG_WAUSERPASSWORD = "OS_LQDMS_BG_WAUSERPASSWORD";

        public const string INVENTORY_LQDMS_NETWORKID = "INVENTORY_LQDMS_NETWORKID"; // 20210915. NetworkID check đơn hàng trên LQDMS, Network khác check đơn hàng trên DMS+

        public const string INVENTORY_NETWORKID_SYNCTOSSE = "INVENTORY_NETWORKID_SYNCTOSSE"; // 20230103. NetworkID Đồng bộ sang hệ thống kế toán SSE.
    }

	public class GenEngineSolution
	{
		public const string HDDT = "0";
		public const string INBRAND = "1";
		public const string INVENTORY = "2";
		public const string PRODUCTCENTER = "3";
		public const string DMSPLUS = "4";
		public const string QCONTRACT = "5";
	}

	public class GenEngineVersion
	{
		public const string V0 = "0";
	}

	public class InosMix
    {
        public const string Default_Password = "123456";
        public const string Default_Language = "vi";
        public const int Default_TimeZone = 7;
        public const int Default_Anonymous = -1;
        public const int Default_ParentId = 0;
		public const Int64 Default_PkgId = 3243782000;
	}

	public class DataRowMyState
	{
		public const string Added = "ADDED";
		public const string Deleted = "DELETED";
		public const string Detached = "DETACHED";
		public const string Modified = "MODIFIED";
		public const string Unchanged = "UNCHANGED";
	}

	public class Flag
	{
		public const string Active = "1";
		public const string Inactive = "0";
		public const string Auto = "A";
		public const string Manual = "M";
		public const string Yes = Active;
		public const string No = Inactive;
    }

	public class RefType
	{
		public const string OrderDL = "ORDERDL"; // Đơn hàng đại lý
        public const string OrderSO = "ORDERSO"; // Đơn hàng bán ngoài
        public const string OrderSR = "ORDERSR";
        public const string RO = "RO"; // Báo giá
        public const string InvOut = "INVOUT"; // Dùng cho nghiệp vụ xuất kho tích hợp với hệ thống kho khác (Lâm Thao)
        public const string PrinterOrder = "PRINTERORDER"; // Đơn hàng sản xuất
        public const string StockOut = "STOCKOUT"; // Phiếu xuất
    }

	public class List_InventoryAction
    {
        public const string zzzzz_List_InventoryAction_In = "'IN', 'AUDITIN', 'MOVEORDIN', 'MOVEAUDITIN', 'CUSRETURN'";
        public const string zzzzz_List_InventoryAction_Out = "'OUT', 'AUDITOUT', 'MOVEORDOUT', 'MOVEAUDITOUT', 'RETURNSUP'";
    }

    public class QRType
    {
        public const string Box = "BOX";
        public const string Can = "CARTON";
        public const string Tem = "TEM";
    }

    public class ProductType
    {
        public const string None = "NONE";
        public const string Serial = "SERIAL";
        public const string Lot = "LOT";
    }


    public class Spec_Prd_Type
    {
        public const string Spec = "SPEC";
        public const string ProductId = "PRODUCTID";
        public const string Product_Id = "PROCDUCT_ID";


    }

    public class MAUGroup
    {
        public const string Mau1VAT = "MAU1VAT";
        public const string MauNVAT = "MAUNVAT";
        public const string MauX20 = "MAUX20";
        public const string MauHTC = "MAUHTC";
    }

    public class UrlType
    {
        public const string UrlPrdCenter = "URLPRDCENTER";
        public const string UrlMstSvPrdCenter = "URLMSTSVPRDCENTER";
        public const string UrlSolution = "URLSOLUTION";
        public const string UrlMstSvSolution = "URLMSTSVSOLUTION";
    }

    public class Language
	{
		public const string VI_VN = "VI-VN";
		public const string EN_US = "EN-US";
	}

	public class SeqType
	{
		public const string Id = "ID";
		public const string InsuranceClaimNo = "INSURANCECLAIMNO";
		public const string InsuranceClaimDocCode = "INSURANCECLAIMDOCCODE";
		public const string WorkingRecordNo = "WORKINGRECORDNO";
		public const string LevelCode = "LEVELCODE";
		public const string CampaignCrAwardCode = "CAMPAIGNCRAWARDCODE";
		public const string CampaignCode = "CAMPAIGNCODE";
		public const string CICode = "CICODE";
		public const string InputCode = "INPUTCODE";
		public const string PrdStateNo = "PRDSTATENO";
		public const string ColReleaseNo = "COLRELEASENO";
		public const string MDCrtBatchNo = "MDCRTBATCHNO";
		public const string MDAutoBatchNo = "MDAUTOBATCHNO";
		public const string CBABatchNo = "CBABATCHNO";
		public const string TranNo = "TRANNO";
        //
        public const string TaxRegNo = "TAXREGNO";
        public const string TaxRegStopNo = "TAXREGSTOPNO";
        public const string TaxSubNo = "TAXSUBNO";
        public const string TInvoiceCode = "TINVOICECODE";
        public const string InvoiceCode = "INVOICECODE";
		public const string BatchNo = "BATCHNO";
        public const string PRTCode = "PRTCODE";
        public const string SerialNo = "SERIALNO";
        public const string GenTimesNo = "GENTIMESNO";
        public const string IFInvOutFGNo = "IFINVOUTFGNO";
        public const string IFInvInFGNo = "IFINVINFGNO";
        public const string IFInvOutNo = "IFINVOUTNO";
        public const string IFInvInNo = "IFINVINNO";
        public const string IFMONo = "IFMONO";
        public const string IFInvCusReturnNo = "IFINVCUSRETURNNO";
        public const string IFInvAudNo = "IFINVAUDNO";
        public const string IFInvReturnSupNo = "IFINVRETURNSUPNO";
        public const string IFTempPrintNo = "IFTEMPPRINTNO";
        public const string CustomerGrpCode = "CUSTOMERGRPCODE";
        public const string CustomerSourceCode = "CUSTOMERSOURCECODE";
        public const string CustomerCodeSys = "CUSTOMERCODESYS";
        public const string AreaCode = "AREACODE";
        public const string ProductGrpCode = "ProductGrpCode";
    }

	public class MstSvSeqType
	{
		public const string InvoiceCode = "INVOICECODE";
	}

	public class CalendarType
	{
		public const string WorkingDay = "WORKINGDAY";
		//public const string DepositDuty = "DEPOSITDUTY";

	}

	public class DateTimeSpecial
	{
		public const string DateMin = "1900-01-01";
		public const string DateMax = "2100-01-01";
	}

	public class ClientConstants
	{
		public const string DATE_FORMAT = "yyyy-MM-dd";
		public const string TIME_FORMAT = "HH:mm:ss";
	}

	public class AccType
	{
		public const string Common = "COMMON";
    }


    public class FolderUpload
    {
        public const string Temp_PrintTemp_Temp = @"\UploadedFiles\Temp_PrintTemp_Temp";
        public const string Temp_PrintTemp = @"\UploadedFiles\Temp_PrintTemp";
        public const string Mst_Temp = @"\UploadedFiles\Mst_Temp";
        public const string InvF_TempPrint = @"\UploadedFiles\InvF_TempPrint";
        public const string Mst_TempPrintGroup = @"\UploadedFiles\Mst_TempPrintGroup";
    }

    public class InventoryAction
    {
        public const string Normal = "NORMAL";
        public const string In = "IN";
        public const string Out = "OUT";
        public const string MoveOrd = "MOVEORD";
        public const string AuditIn = "AUDITIN";
        public const string AuditOut = "AUDITOUT";
        public const string MoveOrdIn = "MOVEORDIN";
        public const string MoveOrdOut = "MOVEORDOUT";
        public const string MoveAuditIn = "MOVEAUDITIN";
        public const string MoveAuditOut = "MOVEAUDITOUT";
        public const string CusReturn = "CUSRETURN";
        public const string ReturnSup = "RETURNSUP";
    }

    public class InvInType
    {
        public const string Virtual = "VIRTUAL";
        public const string Adjust = "ADJUST";
    }
    public class InvOutType
    {
        public const string Virtual = "VIRTUAL";
        public const string Adjust = "ADJUST";
    }
    public class FormInType
    {
        public const string MaVach = "MAVACH";
        public const string KhongMaVach = "KHONGMAVACH";
    }

    public class FormOutType
    {
        public const string MaVach = "MAVACH";
        public const string KhongMaVach = "KHONGMAVACH";
    }

    public class InvFOutType
    {
        public const string OutThuongMai = "OUTTHUONGMAI";
        public const string OutEndCus = "OUTENDCUS";
    }


    public class IF_InvInFGStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_MOStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_InvInStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_InvOutStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_InvAuditStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_ReturnSupStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_InvCusReturnStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }

    public class IF_InvOutFGStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }


    public class IF_InvOutHistStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
        public const string Finish = "FINISH";
        public const string Error = "ERROR";
    }
    public class InventoryTransactionAction
    {
        public const string Add = "ADD";
        public const string Update = "UPDATE";
        public const string Delete = "DELETE";
    }
    public class InvInOutAction
    {
        public const string In = "IN";
        public const string Out = "OUT";
    }
    public class CurrencyCode
	{
		public const string VND = "VND";
		public const string USD = "USD";
	}

	public class MoneyBoxCode
	{
		public const string NhanDauTu = "NHANDAUTU";
	}

	public class InputStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class RegisterStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string InsertMQ = "INSERTMQ";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class PaymentType
	{
		public const string LC = "LC";
		public const string TT = "TT";
		public const string UTC = "UTC";
		public const string Other = "OTHER";
	}

	public class LCType
	{
		public const string TN = "TN";
		public const string TC = "TC";
	}

	public class MDType
	{
		public const string BLDUTHAU = "BLDUTHAU";
		public const string BLHOPDONG = "BLHOPDONG";
		public const string BLHOANTAMUNG = "BLHOANTAMUNG";
		public const string BLBAOHANH = "BLBAOHANH";
		public const string BLTHANHTOAN = "BLTHANHTOAN";
		public const string BLNOPTHUE = "BLNOPTHUE";
	}

	public class PaymentPartnerType
	{
		public const string NH = "NH";
	}

	//public class Mst_CustomerType
	//{
	//	public const string KHTT = "KHTT";
	//	public const string DEALER = "DEALER";
	//	public const string EU = "EU";
	//}

	public class MoneyStockType
	{
		public const string QTM = "QTM";
		public const string TKNH = "TKNH";
	}

	public class AssetType
	{
		public const string HDTG = "HDTG";
		public const string BDS = "BDS";
		public const string HSXE = "HSXE";
		//public const string XE = "XE";
		public const string DA = "DA";
		public const string HDNGOAIXE = "HDNGOAIXE";
		public const string HDNOIXE = "HDNOIXE";
		public const string HDKHACXE = "HDKHACXE";
		public const string QDN = "QDN";
		public const string KHAC = "KHAC";
	}

	public class Mst_CollateralSpecInputType
	{
		public const string Rule0 = "RULE0";
		public const string Rule1 = "RULE1";
		public const string Rule2 = "RULE2";
		public const string Rule3 = "RULE3";
	}

	public class CTOStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class MDCrtBatchStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class MDAutoBatchStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class LDAutoBatchStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class ColAutoBatchStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class T24_PurposePayment
	{
		public const string VayttungtruocTTC = "Vay tt ung truoc TTC";
		public const string VayttsotienconlaiTCC= "Vay tt so tien con lai TTC";
		public const string PhatvaybatbuocBLTTTCC = "Phat vay bat buoc BLTT TTC";
	}

	public class LCStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class LDStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class ColStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class ColRlStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class MDStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class BGStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";
	}

	public class AssetStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class CrCtStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class RecpStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
    }

    public class TInvoiceStatus
    {
        public const string Pending = "PENDING";
        public const string Issued = "ISSUED";
    }

    public class PrintTempStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
    }

    public class FPmtStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class KUNNStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class CtrDpsStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class PrjStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class BPStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class CtrPcpStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class CtrStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class BPPStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class CtrCarStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
	}

	public class CreditContractType
	{
		public const string HDNGOAI = "HDNGOAI";
		public const string HDNOI = "HDNOI";
		public const string HDKHAC = "HDKHAC";
	}

	public class PhuongPhapTinhLaiType
	{
		public const string PPTL_360 = "360";
		public const string PPTL_365 = "365";
	}

	public class BizType
	{
		public const string Payment = "PAYMENT";
		public const string Receipt = "RECEIPT";
	}

	public class BizRefNo	
	{
		public const string HD = "HD";
		public const string HDTG = "HDTG";
		public const string KUNN = "KUNN";
		public const string LC = "LC";
		public const string MD = "MD";
	}

	public class ValNgayChotTinhLai
	{
		public const int DateEndOfMonth = 32;
	}

	public class HinhThucTraType
	{
		public const string TraLaiHangThangGocCuoiKi = "TRALAIHANGTHANGGOCCUOIKI";
		public const string TraLaiVaGocHangThang = "TRALAIVAGOCHANGTHANG";
		public const string TraLaiVaGocCuoiKi = "TraLaiVaGocCuoiKi";
	}

	public class PhuongPhapGiaiNganType
	{
		public const string ChuyenKhoan = "CHUYENKHOAN";
		public const string TienMat = "TIENMAT";
		public const string All = "ALL";
	}

	public class BorrowStatus
	{
		public const string Normal = "NORMAL";
		public const string Borrow = "BORROW";
	}

	public class T24_Status
	{
		public const string Cur = "CUR";
		public const string Liq = "LIQ";
	}

	public class BorrowStatusSpec
	{
		public const string NotReceive = "NOTRECEIVE";
		public const string Normal = "NORMAL";
		public const string Release = "RELEASE";
	}

	public class Mst_CollateralType
	{
		public const string ACLK = "ACLK";
		public const string LoHang = "LH";
		//public const string LoHangCon = "LHC";
		public const string DSXDL = "DSXDL";
		public const string Other = "OTHER";
		public const string QuyenDoiNo = "QDN";
		//public const string QuyenDoiNoVIN = "QDNVIN";
		public const string TSHTTL = "TSHTTL";
	}

	public class Mst_MDType
	{
		//public const string MDCCHT = "MDCCHT";
		//public const string MDCKHT = "MDCKHT";
		public const string MDC = "MDC";
		public const string MDT = "MDT";
	}

	public class RefNoType
	{
		public const string VIN = "VIN";
		public const string SubSpec = "SUBSPEC";
	}

	public class ConfigCode
	{
		public const string Common = "COMMON";
	}

	public class TEmailCode
	{
		public const string Inos_User_Active = "INOS_USER_ACTIVE";
		public const string Sys_User_Active = "SYS_USER_ACTIVE";
	}

	public class BatchStatus
	{
		public const string Pending = "PENDING";
		public const string Approve = "APPROVE";
		public const string Cancel = "CANCEL";
		public const string Finish = "FINISH";
		public const string Error = "ERROR";

		public const string Active = "1";
		public const string Inactive = "0";
    }

    public class BlockStatus
    {
        public const string No = "0";
        public const string SYS = "S";
        public const string User = "U";
    }

    public class TCTTranType
	{
		public const string KoHopLe = "####";
		public const string DKSuDungQuaDVTVAN = "0001";
		public const string DKHoSoKhaiThueNopQuaDVTVAN = "0002";
		public const string DKThayDoiCTSQuaDVTVAN = "0003";
		public const string DKHoSoKhaiThueNgungNopQuaDVTVAN = "0004";
		public const string DKNgungSuDungDVTVAN = "0005";
		public const string NopHoSoKhaiThueQuaDVTVAN = "0006";
		public const string NopHoSoDinhKemHoSoKhaiThueQuaDVTVAN = "0007";
		public const string DanhSachThongBaoTVAN = "0014";
	}

    public class BusinessType
    {
        public const string KoHopLe = "####";
        public const string DKLaiSuDungQuaDVTVAN = "0000"; // Đăng ký lại sử dụng qua dịch vụ VAN
        public const string DKSuDungQuaDVTVAN = "0001"; // Đăng ký sử dụng qua dịch vụ VAN
        public const string DKThayDoiCTSQuaDVTVAN = "0003"; // Đăng ký thay đổi chứng thư số qua dịch vụ VAN
        public const string DKNgungSuDungDVTVAN = "0005"; // Đăng ký ngừng sử dụng dịch vụ VAN

        public const string DKHoSoKhaiThueNopQuaDVTVAN = "0002"; // Đăng ký hồ sơ khai thuế nộp qua dịch vụ VAN
        public const string DKHoSoKhaiThueNgungNopQuaDVTVAN = "0004"; // Đăng ký hồ sơ khai thuế ngừng nộp qua dịch vụ VAN
        public const string NopHoSoKhaiThueQuaDVTVAN = "0006"; // Nộp hồ sơ khai thuế qua  dịch vụ VAN


    }

    public class BusinessTypeName
    {
        public const string KoHopLe = "Giao dịch không hợp lệ"; // ####
        public const string DKSuDungQuaDVTVAN = "Đăng ký sử dụng qua dịch vụ VAN"; // 0001
        public const string DKLaiSuDungQuaDVTVAN = "Đăng ký lại sử dụng qua dịch vụ VAN"; // 0000
        public const string DKNgungSuDungDVTVAN = "Đăng ký ngừng sử dụng dịch vụ VAN"; // 0005
        public const string DKThayDoiCTSQuaDVTVAN = "Đăng ký thay đổi chứng thư số qua dịch vụ VAN"; // 0003
        ////
        public const string DKHoSoKhaiThueNopQuaDVTVAN = "Đăng ký hồ sơ khai thuế nộp qua dịch vụ VAN"; // 0002
        public const string DKHoSoKhaiThueNgungNopQuaDVTVAN = "Đăng ký hồ sơ khai thuế ngừng nộp qua dịch vụ VAN"; // 0004
        public const string NopHoSoKhaiThueQuaDVTVAN = "Nộp hồ sơ khai thuế qua  dịch vụ VAN"; // 0006

    }

    public class TaxSubType
    {
        public const string NopThang = "1"; // Nộp thẳng
        public const string TrinhKy = "0"; // Trình ký
       
    }


    public class TranStatus
    {
        public const string None = "NONE";

        public const string S0 = "0";
		public const string S1 = "1";
		public const string S2 = "2";
		public const string S3 = "3";
		public const string S4 = "4";
		public const string S5 = "5";
	}
      

    public class MstNNT_TCTStatus // tổng cục thuế trả kết quả (NNT đăng kí/ngừng sử dụng dịch vụ TVAN)
    {
        public const string None = "0"; // chưa đăng ký
        public const string Reg = "1"; // TCT xác nhận đăng ký
        public const string Cancel = "2"; // TCT xác nhận ngừng đăng ký

        public const string Active = "1";
        public const string Inactive = "0";
    }

    public class MstNNT_RegStatus
    {
        public const string None = "NONE"; // chưa đăng ký
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";

        public const string Active = "1";
        public const string Inactive = "0";
    }

    public class MstNNT_NotRegStatus
    {
        public const string None = "NONE"; // chưa đăng ký
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";

        public const string Active = "1";
        public const string Inactive = "0";
    }


    public class TranType
    {
        public const string KoHopLe = "####";
        public const string DKSuDungQuaDVTVAN = "0001";
        public const string DKHoSoKhaiThueNopQuaDVTVAN = "0002";
        public const string DKThayDoiCTSQuaDVTVAN = "0003";
        public const string DKHoSoKhaiThueNgungNopQuaDVTVAN = "0004";
        public const string DKNgungSuDungDVTVAN = "0005";
        public const string NopHoSoKhaiThueQuaDVTVAN = "0006";
        public const string NopHoSoDinhKemHoSoKhaiThueQuaDVTVAN = "0007";
        public const string DanhSachThongBaoTVAN = "0014";
    }

	public class TCTTranErrCode
	{
		public const string Err_00 = "00";
		public const string Err_0000 = "####";
	}

	public class MapUserInOrganFunc
	{
		public const string Sys_User_Create = "Sys_User_Create";
		public const string Mst_Organ_CreateX = "Mst_Organ_CreateX";
	}

	//public class Mst_Param
	//{
	//	public const string Param1 = "PARAM1";
	//	public const string Param2 = "PARAM2";
	//	public const string Param3 = "PARAM3";
	//}

	public class FunctionActionType
	{
		public const string Add = "ADD";
		public const string Update = "UPDATE";
		public const string Delete = "DELETE";
	}

	public class ColReleaseType
	{
		public const string KHTT = "KHTT";
		public const string Dealer = "DEALER";		
	}

	public class CustomerAcc
	{
		public const string Cate_4030 = "4030";
		public const string Cate_4019 = "4019";
	}

	public class T24_Coll_Status
	{
		public const string Normal = "00- NORMAL";
		public const string Custody = "01- CUSTODY";
		public const string Release = "02- RELEASE";
		public const string Borrowed = "03- BORROWED";
	}

	public class T24RespErrCode
	{
		public const string OK = "0";
		public const string VPB_BERR_UNAUTHORISED_REQUEST = "1004";
		public const string VPB_BERR_INVALID_REQUEST_FORMAT = "2000";
		public const string VPB_BERR_SOURCE_ACCOUNT_INVALID = "2003";
		public const string VPB_BERR_SERVER_TIMEOUT = "4000";
		public const string VPB_BERR_RECORD_NOT_FOUND = "5000";
		public const string VPB_BERR_EXCEPTION = "9001";
	}

    public class InvoiceStatus
    {
        public const string Pending = "PENDING";
        public const string Approved = "APPROVED";
        public const string Issued = "ISSUED";
        public const string Canceled = "CANCELED";
        public const string Deleted = "DELETED";
    }

    public class SourceInvoiceCode
    {
        public const string InvoiceADJ = "INVOICEADJ";
        public const string InvoiceReplace = "INVOICEREPLACE";
        public const string InvoiceRoot = "INVOICEROOT";
    }

    public class InvoiceAdjType
    {
        public const string AdjInCrease = "ADJINCREASE";
        public const string AdjDescrease = "ADJDESCREASE";
        public const string Normal = "NORMAL";
    }

	public class SpecCustomFieldCode
	{
		public const string CustomField1 = "CUSTOMFIELD1";
		public const string CustomField2 = "CUSTOMFIELD2";
		public const string CustomField3 = "CUSTOMFIELD3";
		public const string CustomField4 = "CUSTOMFIELD4";
		public const string CustomField5 = "CUSTOMFIELD5";
		public const string CustomField6 = "CUSTOMFIELD6";
		public const string CustomField7 = "CUSTOMFIELD7";
		public const string CustomField8 = "CUSTOMFIELD8";
		public const string CustomField9 = "CUSTOMFIELD9";
		public const string CustomField10 = "CUSTOMFIELD10";
	}

    public class NotifyTitle
    {
        public const string TitleInventory = "QRBOX";
    }

    public class NotifyType
    {
        //public const string StockOut = "StockOut";
        //public const string StockIn = "StockIn";
        //public const string Others = "Others";

        public const string ApStockIn = "ApStockIn"; // Duyệt nhập kho 
        public const string ApStockOut = "ApStockOut"; // Duyệt xuất kho
        public const string StockOutDL = "StockOutDL"; // 
        public const string ApTransfer = "ApTransfer"; // Duyệt điều chuyển
        public const string FinishAudit = "FinishAudit"; // Duyệt điều chỉnh kiểm kê
        public const string ApReturnSup = "ApReturnSup"; // Duyệt trả hàng NCC
        public const string ApCusReturn = "ApCusReturn"; // Duyệt KH trả hàng
    }

    public class NotifySubType
    {
        // NotifySubType
        public const string INV_IN = "INV_IN";
        public const string INV_OUT = "INV_OUT";
        public const string INV_AUDIT = "INV_AUDIT";
        public const string INV_MOVE = "INV_MOVE";
    }

    public class OrderStatus
    {
        public const string Pending = "PENDING";
        public const string Approve = "APPROVE";
        public const string Cancel = "CANCEL";
    }
}
