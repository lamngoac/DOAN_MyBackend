using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace idn.Skycic.Inventory.Errors
{
	public partial class ErridnInventory
	{
		#region // Common:
		// Common:
		public const string NoError = Error.NoError; //// Thực hiện thành công.
		public const string ErridnInventoryPrefix = "Erridn.Skycic.Inventory."; //// Lỗi idn.Skycic.Inventory.
		public const string CmSys_ServiceInit = "Erridn.Skycic.Inventory.CmSys_ServiceInit"; //// Lỗi Hệ thống, khi khởi tạo SystemService.
		public const string CmSys_InvalidTid = "Erridn.Skycic.Inventory.CmSys_InvalidTid"; //// Lỗi Hệ thống, Mã giao dịch không hợp lệ.
		public const string CmSys_GatewayAuthenticateFailed = "Erridn.Skycic.Inventory.CmSys_GatewayAuthenticateFailed"; //// Lỗi Hệ thống, khi truy nhập trái phép vào Cổng giao dịch.
		public const string CmSys_SessionPreInitFailed = "Erridn.Skycic.Inventory.CmSys_SessionPreInitFailed"; //// Lỗi Hệ thống, khi khởi tạo Phiên làm việc.
		public const string CmSys_SessionNotFound = "Erridn.Skycic.Inventory.CmSys_SessionNotFound"; //// Lỗi Hệ thống, Phiên làm việc không hợp lệ.
		public const string CmSys_SessionExpired = "Erridn.Skycic.Inventory.CmSys_SessionExpired"; //// Lỗi Hệ thống, Phiên làm việc đã hết hạn.
		public const string CmSys_InvalidServiceCode = "Erridn.Skycic.Inventory.CmSys_InvalidServiceCode"; //// //CmSys_InvalidServiceCode
		public const string CmSys_InvalidBizSpecialPw = "Erridn.Skycic.Inventory.CmSys_InvalidBizSpecialPw"; //// //CmSys_InvalidBizSpecialPw
		public const string CmSys_InvalidStdCode = "Erridn.Skycic.Inventory.CmSys_InvalidStdCode"; //// //CmSys_InvalidStdCode
        public const string CmSys_InvalidOutSite = "Erridn.Skycic.Inventory.CmSys_InvalidOutSite"; //// //CmSys_InvalidOutSite

        // CmApp_Mst_Common:
        public const string CmApp_Mst_Common_TableNotSupported = "Erridn.Skycic.Inventory.CmApp_Mst_Common_TableNotSupported"; //// //CmApp_Mst_Common_TableNotSupported

		#endregion

		#region // Common.WA:
		// WAS_Cm_GetDTime:
		public const string WAS_Cm_GetDTime = "Erridn.Skycic.Inventory.WAS_Cm_GetDTime"; //// //WAS_Cm_GetDTime
		#endregion

		#region // Map_DealerDiscount:
		// Map_DealerDiscount_Get:
		public const string Map_DealerDiscount_Get = "ErridnInventory.Map_DealerDiscount_Get"; //// // Map_DealerDiscount_Get

		// WAS_Map_DealerDiscount_Get:
		public const string WAS_Map_DealerDiscount_Get = "ErridnInventory.WAS_Map_DealerDiscount_Get"; //// // WAS_Map_DealerDiscount_Get
		#endregion

		#region // Mst_Org:
		// Mst_Org_Get:
		public const string Mst_Org_Get = "ErridnInventory.Mst_Org_Get"; //// // Mst_Org_Get

		// WAS_Mst_Org_Get:
		public const string WAS_Mst_Org_Get = "ErridnInventory.WAS_Mst_Org_Get"; //// // WAS_Mst_Org_Get
		#endregion

		#region // Mst_ParamPrivate:
		// Mst_MoneyStock_CheckDB:
		public const string Mst_ParamPrivate_CheckDB_ParamPrivateNotFound = "ErridnInventory.Mst_ParamPrivate_CheckDB_ParamPrivateNotFound"; //// // Mst_ParamPrivate_CheckDB_ParamPrivateNotFound
		public const string Mst_ParamPrivate_CheckDB_ParamPrivateExist = "ErridnInventory.Mst_ParamPrivate_CheckDB_ParamPrivateExist"; //// // Mst_ParamPrivate_CheckDB_ParamPrivateExist

		// Mst_ParamPrivate_Get:
		public const string Mst_ParamPrivate_Get = "ErridnInventory.Mst_ParamPrivate_Get"; //// // Mst_ParamPrivate_Get

		// WAS_Mst_ParamPrivate_Get:
		public const string WAS_Mst_ParamPrivate_Get = "ErridnInventory.WAS_Mst_ParamPrivate_Get"; //// // WAS_Mst_ParamPrivate_Get

		// Mst_ParamPrivate_Create:
		public const string Mst_ParamPrivate_Create = "ErridnInventory.Mst_ParamPrivate_Create"; //// // Mst_ParamPrivate_Create
		public const string Mst_ParamPrivate_Create_InvalidParamCode = "ErridnInventory.Mst_ParamPrivate_Create_InvalidParamCode"; //// // Mst_ParamPrivate_Create_InvalidParamCode
		public const string WAS_Mst_ParamPrivate_Create = "ErridnInventory.WAS_Mst_ParamPrivate_Create"; //// // WAS_Mst_ParamPrivate_Create

		// Mst_ParamPrivate_Update:
		public const string Mst_ParamPrivate_Update = "ErridnInventory.Mst_ParamPrivate_Update"; //// // Mst_ParamPrivate_Update

		// WAS_Mst_ParamPrivate_Update:
		public const string WAS_Mst_ParamPrivate_Update = "ErridnInventory.WAS_Mst_ParamPrivate_Update"; //// // WAS_Mst_ParamPrivate_Update

		// Mst_ParamPrivate_Delete:
		public const string Mst_ParamPrivate_Delete = "ErridnInventory.Mst_ParamPrivate_Delete"; //// // Mst_ParamPrivate_Delete

		// WAS_Mst_ParamPrivate_Delete:
		public const string WAS_Mst_ParamPrivate_Delete = "ErridnInventory.WAS_Mst_ParamPrivate_Delete"; //// // WAS_Mst_ParamPrivate_Delete
		#endregion

		#region // Mst_Param:
		// Mst_Param_CheckDB:
		public const string Mst_Param_CheckDB_ParamCodeNotFound = "ErridnInventory.Mst_Param_CheckDB_ParamCodeNotFound"; //// // Mst_Param_CheckDB_ParamCodeNotFound
		public const string Mst_Param_CheckDB_ParamCodeExist = "ErridnInventory.Mst_Param_CheckDB_ParamCodeExist"; //// // Mst_Param_CheckDB_ParamCodeExist

		// WAS_Mst_Param_Create:
		public const string WAS_Mst_Param_Create = "ErridnInventory.WAS_Mst_Param_Create"; //// // WAS_Mst_Param_Create

		// Mst_Param_Create:
		public const string Mst_Param_Create = "ErridnInventory.Mst_Param_Create"; //// // Mst_Param_Create
		public const string Mst_Param_Create_InvalidParamCode = "ErridnInventory.Mst_Param_Create_InvalidParamCode"; //// // Mst_Param_Create_InvalidParamCode

		// WAS_Mst_Param_Update:
		public const string WAS_Mst_Param_Update = "ErridnInventory.WAS_Mst_Param_Update"; //// // WAS_Mst_Param_Update

		// Mst_Param_Update:
		public const string Mst_Param_Update = "ErridnInventory.Mst_Param_Update"; //// // Mst_Param_Update

		// WAS_Mst_Param_Delete:
		public const string WAS_Mst_Param_Delete = "ErridnInventory.WAS_Mst_Param_Delete"; //// // WAS_Mst_Param_Delete

		// Mst_Param_Delete:
		public const string Mst_Param_Delete = "ErridnInventory.Mst_Param_Delete"; //// // Mst_Param_Delete
		#endregion

		#region // Mst_Supplier:
		// Mst_Supplier_CheckDB:
		public const string Mst_Supplier_CheckDB_SupCodeNotFound = "ErridnInventory.Mst_Supplier_CheckDB_SupCodeNotFound"; //// // Mst_Supplier_CheckDB_SupCodeNotFound
		public const string Mst_Supplier_CheckDB_SupCodeExist = "ErridnInventory.Mst_Supplier_CheckDB_SupCodeExist"; //// // Mst_Supplier_CheckDB_SupCodeExist
		public const string Mst_Supplier_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Supplier_CheckDB_FlagActiveNotMatched"; //// // Mst_Supplier_CheckDB_FlagActiveNotMatched

		// Mst_Supplier_Get:
		public const string Mst_Supplier_Get = "ErridnInventory.Mst_Supplier_Get"; //// // Mst_Supplier_Get

		// WAS_Mst_Supplier_Get:
		public const string WAS_Mst_Supplier_Get = "ErridnInventory.WAS_Mst_Supplier_Get"; //// // WAS_Mst_Supplier_Get

		// WAS_Mst_Supplier_Create:
		public const string WAS_Mst_Supplier_Create = "ErridnInventory.WAS_Mst_Supplier_Create"; //// // WAS_Mst_Supplier_Create

		// Mst_Supplier_Create:
		public const string Mst_Supplier_Create = "ErridnInventory.Mst_Supplier_Create"; //// // Mst_Supplier_Create
		public const string Mst_Supplier_Create_InvalidSupCode = "ErridnInventory.Mst_Supplier_Create_InvalidSupCode"; //// // Mst_Supplier_Create_InvalidSupCode
		public const string Mst_Supplier_Create_InvalidSupType = "ErridnInventory.Mst_Supplier_Create_InvalidSupType"; //// // Mst_Supplier_Create_InvalidSupType
		public const string Mst_Supplier_Create_InvalidSupName = "ErridnInventory.Mst_Supplier_Create_InvalidSupName"; //// // Mst_Supplier_Create_InvalidSupName

		// WAS_Mst_Supplier_Update:
		public const string WAS_Mst_Supplier_Update = "ErridnInventory.WAS_Mst_Supplier_Update"; //// // WAS_Mst_Supplier_Update

		// Mst_Supplier_Update:
		public const string Mst_Supplier_Update = "ErridnInventory.Mst_Supplier_Update"; //// // Mst_Supplier_Update
		public const string Mst_Supplier_Update_InvalidSupType = "ErridnInventory.Mst_Supplier_Update_InvalidSupType"; //// // Mst_Supplier_Update_InvalidSupType
		public const string Mst_Supplier_Update_InvalidSupName = "ErridnInventory.Mst_Supplier_Update_InvalidSupName"; //// // Mst_Supplier_Update_InvalidSupName

		// WAS_Mst_Supplier_Delete:
		public const string WAS_Mst_Supplier_Delete = "ErridnInventory.WAS_Mst_Supplier_Delete"; //// // WAS_Mst_Supplier_Delete

		// Mst_Supplier_Delete:
		public const string Mst_Supplier_Delete = "ErridnInventory.Mst_Supplier_Delete"; //// // Mst_Supplier_Delete
		#endregion

		#region // Mst_MoveOrdType:
		// Mst_MoveOrdType_CheckDB:
		public const string Mst_MoveOrdType_CheckDB_MoveOrdTypeNotFound = "ErridnInventory.Mst_MoveOrdType_CheckDB_MoveOrdTypeNotFound"; //// // Mst_MoveOrdType_CheckDB_MoveOrdTypeNotFound
		public const string Mst_MoveOrdType_CheckDB_MoveOrdTypeExist = "ErridnInventory.Mst_MoveOrdType_CheckDB_MoveOrdTypeExist"; //// // Mst_MoveOrdType_CheckDB_MoveOrdTypeExist
		public const string Mst_MoveOrdType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_MoveOrdType_CheckDB_FlagActiveNotMatched"; //// // Mst_MoveOrdType_CheckDB_FlagActiveNotMatched

		// Mst_MoveOrdType_Get:
		public const string Mst_MoveOrdType_Get = "ErridnInventory.Mst_MoveOrdType_Get"; //// // Mst_MoveOrdType_Get

		// WAS_Mst_MoveOrdType_Get:
		public const string WAS_Mst_MoveOrdType_Get = "ErridnInventory.WAS_Mst_MoveOrdType_Get"; //// // WAS_Mst_MoveOrdType_Get
		#endregion

		#region // MasterData:
		// Mst_Common_Get:
		public const string Mst_Common_Get = "Erridn.Skycic.Inventory.Mst_Common_Get"; //// //Mst_Common_Get
		public const string Mst_Common_Get_NotSupportTable = "Erridn.Skycic.Inventory.Mst_Common_Get_NotSupportTable"; //// //Mst_Common_Get_NotSupportTable

		// Cm_ExecSql:
		public const string Cm_ExecSql = "Erridn.Skycic.Inventory.Cm_ExecSql"; //// //Cm_ExecSql
		public const string Cm_ExecSql_ParamMissing = "Erridn.Skycic.Inventory.Cm_ExecSql_ParamMissing"; //// //Cm_ExecSql_ParamMissing

		#endregion

		#region // Share:
		// Share:
		public const string AccSplitRuleCouple_InvalidDm6 = "ErridnInventory.AccSplitRuleCouple_InvalidDm6"; //// AccSplitRuleCouple_InvalidDm6.
		public const string AccSplitRuleCouple_InvalidDm7 = "ErridnInventory.AccSplitRuleCouple_InvalidDm7"; //// AccSplitRuleCouple_InvalidDm7.
		public const string AccSplitRuleCouple_InvalidDm7Std = "ErridnInventory.AccSplitRuleCouple_InvalidDm7Std"; //// AccSplitRuleCouple_InvalidDm7Std.
		#endregion

		#region // WS_T24:
		// WS_T24_LD_Get:
		public const string WS_T24_LD_Get = "ErridnInventory.WS_T24_LD_Get"; //// // WS_T24_LD_Get

		// WS_T24_Collateral_Get:
		public const string WS_T24_Collateral_Get = "ErridnInventory.WS_T24_Collateral_Get"; //// // WS_T24_Collateral_Get

		// WS_T24_CollateralChange_Get:
		public const string WS_T24_CollateralChange_Get = "ErridnInventory.WS_T24_CollateralChange_Get"; //// // WS_T24_CollateralChange_Get

		// WS_T24_LDChange_Get:
		public const string WS_T24_LDChange_Get = "ErridnInventory.WS_T24_LDChange_Get"; //// // WS_T24_LDChange_Get

		// WS_T24_LDCreate_Get:
		public const string WS_T24_LDCreate_Get = "ErridnInventory.WS_T24_LDCreate_Get"; //// // WS_T24_LDCreate_Get

		// WS_T24_MD_Get:
		public const string WS_T24_MD_Get = "ErridnInventory.WS_T24_MD_Get"; //// // WS_T24_MD_Get

		// WS_T24_MDChange_Get:
		public const string WS_T24_MDChange_Get = "ErridnInventory.WS_T24_MDChange_Get"; //// // WS_T24_MDChange_Get

		//// WS_T24_MDChange_Get:
		//public const string WS_T24_MDChange_Get = "ErridnInventory.WS_T24_MDChange_Get"; //// // WS_T24_MDChange_Get

		// WS_T24_MDCreate_Get:
		public const string WS_T24_MDCreate_Get = "ErridnInventory.WS_T24_MDCreate_Get"; //// // WS_T24_MDCreate_Get
		#endregion

		#region // Mst_AreaMarket:
		// Mst_AreaMarket_CheckDB:
		public const string Mst_AreaMarket_CheckDB_AreaMarketNotFound = "ErridnInventory.Mst_AreaMarket_CheckDB_AreaMarketNotFound"; //// Không tìm thấy Vùng thị trường trong cơ sở dữ liệu ///
		public const string Mst_AreaMarket_CheckDB_AreaMarketExist = "ErridnInventory.Mst_AreaMarket_CheckDB_AreaMarketExist"; //// Vùng thị trường đã tồn tại ////
		public const string Mst_AreaMarket_CheckDB_AreaMarketStatusNotMatched = "ErridnInventory.Mst_AreaMarket_CheckDB_AreaMarketStatusNotMatched"; //// Trạng thái Vùng thị trường không hợp lệ ////
		public const string Mst_AreaMarket_CheckDB_FlagRootNotMatched = "ErridnInventory.Mst_AreaMarket_CheckDB_FlagRootNotMatched"; //// //Mst_AreaMarket_CheckDB_FlagRootNotMatched

		// Mst_AreaMarket_Get:
		public const string Mst_AreaMarket_Get = "ErridnInventory.Mst_AreaMarket_Get"; //// Mã lỗi: Mst_AreaMarket_Get ////

		// Mst_AreaMarket_Create:
		public const string Mst_AreaMarket_Create = "ErridnInventory.Mst_AreaMarket_Create"; //// Mã lỗi: Mst_AreaMarket_Create ////
		public const string Mst_AreaMarket_Create_InvalidAreaCode = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaCode"; //// Mã Vùng thị trường không hợp lệ ////
		public const string Mst_AreaMarket_Create_InvalidAreaCodeParent = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaCodeParent"; //// Mã Vùng thị trường cấp trên không hợp lệ ////
		public const string Mst_AreaMarket_Create_InvalidAreaBUCode = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaBUCode"; //// Mã lỗi: Mst_AreaMarket_Create_InvalidAreaBUCode ////
		public const string Mst_AreaMarket_Create_InvalidAreaBUPattern = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaBUPattern"; //// Mã lỗi: Mst_AreaMarket_Create_InvalidAreaBUPattern ////
		public const string Mst_AreaMarket_Create_InvalidAreaLevel = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaLevel"; //// Cấp độ Vùng thị trường không hợp lệ ////
		public const string Mst_AreaMarket_Create_InvalidAreaDesc = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaDesc"; //// Mã lỗi: Mst_AreaMarket_Create_InvalidAreaDesc ////
		public const string Mst_AreaMarket_Create_InvalidAreaStatus = "ErridnInventory.Mst_AreaMarket_Create_InvalidAreaStatus"; ////  Trạng thái Vùng thị trường không hợp lệ ////

		// Mst_AreaMarket_Update:
		public const string Mst_AreaMarket_Update = "ErridnInventory.Mst_AreaMarket_Update"; //// Mã lỗi: Mst_AreaMarket_Update ////
		public const string Mst_AreaMarket_Update_InvalidAreaCode = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaCode"; //// Mã Vùng thị trường không hợp lệ ////
		public const string Mst_AreaMarket_Update_InvalidAreaCodeParent = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaCodeParent"; //// Mã Nhà phân phối cấp trên không hợp lệ ////
		public const string Mst_AreaMarket_Update_InvalidAreaBUCode = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaBUCode"; //// Mã lỗi: Mst_AreaMarket_Update_InvalidAreaBUCode ////
		public const string Mst_AreaMarket_Update_InvalidAreaBUPattern = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaBUPattern"; //// Mã lỗi: Mst_AreaMarket_Update_InvalidAreaBUPattern ////
		public const string Mst_AreaMarket_Update_InvalidAreaLevel = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaLevel"; //// Cấp Vùng thị trường không hợp lệ ////
		public const string Mst_AreaMarket_Update_InvalidAreaDesc = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaDesc"; //// Mã lỗi: Mst_AreaMarket_Create_InvalidAreaDesc ////
		public const string Mst_AreaMarket_Update_InvalidAreaStatus = "ErridnInventory.Mst_AreaMarket_Update_InvalidAreaStatus"; ////  Trạng thái Vùng thị trường không hợp lệ ////
		public const string Mst_AreaMarket_Update_ExistAreaMarketChildActive = "ErridnInventory.Mst_AreaMarket_Update_ExistAreaMarketChildActive"; //// // Mst_AreaMarket_Update_ExistAreaMarketChildActive 
		public const string Mst_AreaMarket_Update_ExistDistributorActive = "ErridnInventory.Mst_AreaMarket_Update_ExistDistributorActive"; //// // Mst_AreaMarket_Update_ExistDistributorActive 
		public const string Mst_AreaMarket_Update_ExistOutLetActive = "ErridnInventory.Mst_AreaMarket_Update_ExistOutLetActive"; //// // Mst_AreaMarket_Update_ExistOutLetActive 

		// Mst_AreaMarket_Delete:
		public const string Mst_AreaMarket_Delete = "ErridnInventory.Mst_AreaMarket_Delete"; //// Mã lỗi: Mst_AreaMarket_Delete ////

		#endregion

		#region // Mst_Distributor:
		// Mst_Distributor_CheckDB:
		public const string Mst_Distributor_CheckDB_DBCodeNotFound = "ErridnInventory.Mst_Distributor_CheckDB_DBCodeNotFound"; //// Không tìm thấy Mã nhà phân phối trong cơ sở dữ liệu ////
		public const string Mst_Distributor_CheckDB_DBCodeExist = "ErridnInventory.Mst_Distributor_CheckDB_DBCodeExist"; //// Mã nhà phân phối đã tồn tại ////
		public const string Mst_Distributor_CheckDB_DBStatusNotMatched = "ErridnInventory.Mst_Distributor_CheckDB_DBStatusNotMatched"; ////  Trạng thái Nhà phân phối không hợp lệ ////

		// Mst_Distributor_Get:
		public const string Mst_Distributor_Get = "ErridnInventory.Mst_Distributor_Get"; //// Mã lỗi: Mst_Distributor_Get ////

		// Mst_Distributor_Create:
		public const string Mst_Distributor_Create = "ErridnInventory.Mst_Distributor_Create"; //// Mã lỗi: Mst_Distributor_Create ////
		public const string Mst_Distributor_Create_InvalidDBCode = "ErridnInventory.Mst_Distributor_Create_InvalidDBCode"; //// Mã Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBCodeParent = "ErridnInventory.Mst_Distributor_Create_InvalidDBCodeParent"; //// Mã Nhà phân phối cấp trên không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBBUCode = "ErridnInventory.Mst_Distributor_Create_InvalidDBBUCode"; //// Mã lỗi: lMst_Distributor_Create_InvalidDBBUCode ////
		public const string Mst_Distributor_Create_InvalidDBBUPattern = "ErridnInventory.Mst_Distributor_Create_InvalidDBBUPattern"; //// Mã lỗi: Mst_Distributor_Create_InvalidDBBUPattern ////
		public const string Mst_Distributor_Create_InvalidDBLevel = "ErridnInventory.Mst_Distributor_Create_InvalidDBLevel"; //// Cấp Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBName = "ErridnInventory.Mst_Distributor_Create_InvalidDBName"; //// Tên Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidAreaCode = "ErridnInventory.Mst_Distributor_Create_InvalidAreaCode"; //// Mã Vùng thị trương không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBAddress = "ErridnInventory.Mst_Distributor_Create_InvalidDBAddress"; //// Địa chỉ Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBContactName = "ErridnInventory.Mst_Distributor_Create_InvalidDBContactName"; //// Tên người liên lạc của Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBPhoneNo = "ErridnInventory.Mst_Distributor_Create_InvalidDBPhoneNo"; //// Số điện thoại của Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBFaxNo = "ErridnInventory.Mst_Distributor_Create_InvalidDBFaxNo"; //// Số fax của Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBMobilePhoneNo = "ErridnInventory.Mst_Distributor_Create_InvalidDBMobilePhoneNo"; //// Số điện thoại di động của Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBSMSPhoneNo = "ErridnInventory.Mst_Distributor_Create_InvalidDBSMSPhoneNo"; //// Số điện thoại di động để nhận tin nhắn SMS của Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBTaxCode = "ErridnInventory.Mst_Distributor_Create_InvalidDBTaxCode"; //// Mã số thuế của Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidRemark = "ErridnInventory.Mst_Distributor_Create_InvalidRemark"; //// Ghi chú không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidDBStatus = "ErridnInventory.Mst_Distributor_Create_InvalidDBStatus"; //// Trạng thái Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidCreateDate = "ErridnInventory.Mst_Distributor_Create_InvalidCreateDate"; //// Ngày tạo không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidCreateBy = "ErridnInventory.Mst_Distributor_Create_InvalidCreateBy"; //// Mã lỗi: Mst_Distributor_Create_InvalidCreateBy ////
		public const string Mst_Distributor_Create_InvalidCancelDate = "ErridnInventory.Mst_Distributor_Create_InvalidCancelDate"; //// Ngày hủy không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidCancelBy = "ErridnInventory.Mst_Distributor_Create_InvalidCancelBy"; //// Mã lỗi: Mst_Distributor_Create_InvalidCancelBy ////
		public const string Mst_Distributor_Create_InvalidDiscountPercent = "ErridnInventory.Mst_Distributor_Create_InvalidDiscountPercent"; //// Tỉ lệ chiết khấu cho Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Create_InvalidBalance = "ErridnInventory.Mst_Distributor_Create_InvalidBalance"; //// Mã lỗi: Mst_Distributor_Create_InvalidBalance ////
		public const string Mst_Distributor_Create_InvalidOverdraftThreshold = "ErridnInventory.Mst_Distributor_Create_InvalidOverdraftThreshold"; //// Mã lỗi: Mst_Distributor_Create_InvalidOverdraftThreshold ////


		// Mst_Distributor_Update:
		public const string Mst_Distributor_Update = "ErridnInventory.Mst_Distributor_Update"; //// Mã lỗi: Mst_Distributor_Update ////
		public const string Mst_Distributor_Update_InvalidDBCode = "ErridnInventory.Mst_Distributor_Update_InvalidDBCode"; //// Mã Nhà phân phối không hợp lệ ////
		public const string Mst_Distributor_Update_InvalidDBCodeParent = "ErridnInventory.Mst_Distributor_Update_InvalidDBCodeParent"; //// Mã Nhà phân phối cấp trên không hợp lệ ////
		public const string Mst_Distributor_Update_InvalidDBBUCode = "ErridnInventory.Mst_Distributor_Update_InvalidDBBUCode"; //// Mã lỗi: Mst_Distributor_Update_InvalidDBBUCode ////
		public const string Mst_Distributor_Update_InvalidDBBUPattern = "ErridnInventory.Mst_Distributor_Update_InvalidDBBUPattern"; //// Mã lỗi: Mst_Distributor_Update_InvalidDBBUPattern ////
		public const string Mst_Distributor_Update_InvalidDBLevel = "ErridnInventory.Mst_Distributor_Update_InvalidDBLevel"; //// Cấp Nhà phân phối không hợp lệ //// 
		public const string Mst_Distributor_Update_InvalidDBName = "ErridnInventory.Mst_Distributor_Update_InvalidDBName"; //// Tên Nhà phối phân phối không hợp lệ ////
		public const string Mst_Distributor_Update_InvalidAreaCode = "ErridnInventory.Mst_Distributor_Update_InvalidAreaCode"; //// //Mst_Distributor_Update_InvalidAreaCode
		public const string Mst_Distributor_Update_InvalidDBAddress = "ErridnInventory.Mst_Distributor_Update_InvalidDBAddress"; //// //Mst_Distributor_Update_InvalidDBAddress
		public const string Mst_Distributor_Update_InvalidDBContactName = "ErridnInventory.Mst_Distributor_Update_InvalidDBContactName"; //// //Mst_Distributor_Update_InvalidDBContactName
		public const string Mst_Distributor_Update_InvalidDBPhoneNo = "ErridnInventory.Mst_Distributor_Update_InvalidDBPhoneNo"; //// //Mst_Distributor_Update_InvalidDBPhoneNo
		public const string Mst_Distributor_Update_InvalidDBFaxNo = "ErridnInventory.Mst_Distributor_Update_InvalidDBFaxNo"; //// //Mst_Distributor_Update_InvalidDBFaxNo
		public const string Mst_Distributor_Update_InvalidDBMobilePhoneNo = "ErridnInventory.Mst_Distributor_Update_InvalidDBMobilePhoneNo"; //// //Mst_Distributor_Update_InvalidDBMobilePhoneNo
		public const string Mst_Distributor_Update_InvalidDBSMSPhoneNo = "ErridnInventory.Mst_Distributor_Update_InvalidDBSMSPhoneNo"; //// //Mst_Distributor_Update_InvalidDBSMSPhoneNo
		public const string Mst_Distributor_Update_InvalidDBTaxCode = "ErridnInventory.Mst_Distributor_Update_InvalidDBTaxCode"; //// //Mst_Distributor_Update_InvalidDBTaxCode
		public const string Mst_Distributor_Update_InvalidRemark = "ErridnInventory.Mst_Distributor_Update_InvalidRemark"; //// //Mst_Distributor_Update_InvalidRemark
		public const string Mst_Distributor_Update_InvalidDBStatus = "ErridnInventory.Mst_Distributor_Update_InvalidDBStatus"; //// //Mst_Distributor_Update_InvalidDBStatus
		public const string Mst_Distributor_Update_InvalidCreateDate = "ErridnInventory.Mst_Distributor_Update_InvalidCreateDate"; //// //Mst_Distributor_Update_InvalidCreateDate
		public const string Mst_Distributor_Update_InvalidCreateBy = "ErridnInventory.Mst_Distributor_Update_InvalidCreateBy"; //// //Mst_Distributor_Update_InvalidCreateBy
		public const string Mst_Distributor_Update_InvalidCancelDate = "ErridnInventory.Mst_Distributor_Update_InvalidCancelDate"; //// //Mst_Distributor_Update_InvalidCancelDate
		public const string Mst_Distributor_Update_InvalidCancelBy = "ErridnInventory.Mst_Distributor_Update_InvalidCancelBy"; //// //Mst_Distributor_Update_InvalidCancelBy
		public const string Mst_Distributor_Update_InvalidDiscountPercent = "ErridnInventory.Mst_Distributor_Update_InvalidDiscountPercent"; //// //Mst_Distributor_Update_InvalidDiscountPercent
		public const string Mst_Distributor_Update_InvalidBalance = "ErridnInventory.Mst_Distributor_Update_InvalidBalance"; //// //Mst_Distributor_Update_InvalidBalance
		public const string Mst_Distributor_Update_InvalidOverdraftThreshold = "ErridnInventory.Mst_Distributor_Update_InvalidOverdraftThreshold"; //// //Mst_Distributor_Update_InvalidOverdraftThreshold
		public const string Mst_Distributor_Update_ExistOutletActive = "ErridnInventory.Mst_Distributor_Update_ExistOutletActive"; //// // Mst_Distributor_Update_ExistOutletActive

		// Mst_Distributor_Delete:
		public const string Mst_Distributor_Delete = "ErridnInventory.Mst_Distributor_Delete"; //// //Mst_Distributor_Delete

		#endregion

		#region // Mst_Outlet:
		// Mst_Outlet_CheckDB:
		public const string Mst_Outlet_CheckDB_OutletNotFound = "ErridnInventory.Mst_Outlet_CheckDB_OutletNotFound"; //// //Mst_Outlet_CheckDB_OutletNotFound
		public const string Mst_Outlet_CheckDB_OutletExist = "ErridnInventory.Mst_Outlet_CheckDB_OutletExist"; //// //Mst_Outlet_CheckDB_OutletExist
		public const string Mst_Outlet_CheckDB_OutletStatusNotMatched = "ErridnInventory.Mst_Outlet_CheckDB_OutletStatusNotMatched"; //// //Mst_Outlet_CheckDB_OutletStatusNotMatched

		// Mst_Outlet_Get:
		public const string Mst_Outlet_Get = "ErridnInventory.Mst_Outlet_Get"; //// //Mst_Outlet_Get

		// Mst_Outlet_GetByRouting:
		public const string Mst_Outlet_GetByRouting = "ErridnInventory.Mst_Outlet_GetByRouting"; //// //Mst_Outlet_GetByRouting

		// Mst_Outlet_Create:
		public const string Mst_Outlet_Create = "ErridnInventory.Mst_Outlet_Create"; //// //Mst_Outlet_Create
		public const string Mst_Outlet_Create_InvalidOLCode = "ErridnInventory.Mst_Outlet_Create_InvalidOLCode"; //// //Mst_Outlet_Create_InvalidOLCode
		public const string Mst_Outlet_Create_InvalidDBCode = "ErridnInventory.Mst_Outlet_Create_InvalidDBCode"; //// //Mst_Outlet_Create_InvalidDBCode
		public const string Mst_Outlet_Create_InvalidOLName = "ErridnInventory.Mst_Outlet_Create_InvalidOLName"; //// //Mst_Outlet_Create_InvalidOLName
		public const string Mst_Outlet_Create_InvalidOLAddress = "ErridnInventory.Mst_Outlet_Create_InvalidOLAddress"; //// //Mst_Outlet_Create_InvalidOLAddress
		public const string Mst_Outlet_Create_InvalidOLContactName = "ErridnInventory.Mst_Outlet_Create_InvalidOLContactName"; //// //Mst_Outlet_Create_InvalidOLContactName
		public const string Mst_Outlet_Create_InvalidOLPhoneNo = "ErridnInventory.Mst_Outlet_Create_InvalidOLPhoneNo"; //// //Mst_Outlet_Create_InvalidOLPhoneNo
		public const string Mst_Outlet_Create_InvalidOLFaxNo = "ErridnInventory.Mst_Outlet_Create_InvalidOLFaxNo"; //// //Mst_Outlet_Create_InvalidOLFaxNo
		public const string Mst_Outlet_Create_InvalidOLMobilePhoneNo = "ErridnInventory.Mst_Outlet_Create_InvalidOLMobilePhoneNo"; //// //Mst_Outlet_Create_InvalidOLMobilePhoneNo
		public const string Mst_Outlet_Create_InvalidOLSMSPhoneNo = "ErridnInventory.Mst_Outlet_Create_InvalidOLSMSPhoneNo"; //// //Mst_Outlet_Create_InvalidOLSMSPhoneNo
		public const string Mst_Outlet_Create_InvalidOLTaxCode = "ErridnInventory.Mst_Outlet_Create_InvalidOLTaxCode"; //// //Mst_Outlet_Create_InvalidOLTaxCode
		public const string Mst_Outlet_Create_InvalidRemark = "ErridnInventory.Mst_Outlet_Create_InvalidRemark"; //// //Mst_Outlet_Create_InvalidRemark
		public const string Mst_Outlet_Create_InvalidOLStatus = "ErridnInventory.Mst_Outlet_Create_InvalidOLStatus"; //// //Mst_Outlet_Create_InvalidOLStatus
		public const string Mst_Outlet_Create_InvalidCreateDate = "ErridnInventory.Mst_Outlet_Create_InvalidCreateDate"; //// //Mst_Outlet_Create_InvalidCreateDate
		public const string Mst_Outlet_Create_InvalidCreateBy = "ErridnInventory.Mst_Outlet_Create_InvalidCreateBy"; //// //Mst_Outlet_Create_InvalidCreateBy
		public const string Mst_Outlet_Create_InvalidApproveDate = "ErridnInventory.Mst_Outlet_Create_InvalidApproveDate"; //// //Mst_Outlet_Create_InvalidApproveDate
		public const string Mst_Outlet_Create_InvalidApproveBy = "ErridnInventory.Mst_Outlet_Create_InvalidApproveBy"; //// //Mst_Outlet_Create_InvalidApproveBy
		public const string Mst_Outlet_Create_InvalidCancelDate = "ErridnInventory.Mst_Outlet_Create_InvalidCancelDate"; //// //Mst_Outlet_Create_InvalidCancelDate
		public const string Mst_Outlet_Create_InvalidCancelBy = "ErridnInventory.Mst_Outlet_Create_InvalidCancelBy"; //// //Mst_Outlet_Create_InvalidCancelBy
		public const string Mst_Outlet_Create_UserNotBeDBAdmin = "ErridnInventory.Mst_Outlet_Create_UserNotBeDBAdmin"; //// //Mst_Outlet_Create_UserNotBeDBAdmin

		public const string Mst_Outlet_Create_InvalidUserSaleMan = "ErridnInventory.Mst_Outlet_Create_InvalidUserSaleMan"; //// //Mst_Outlet_Create_InvalidUserSaleMan

		// Mst_Outlet_Update:
		public const string Mst_Outlet_Update = "ErridnInventory.Mst_Outlet_Update"; //// //Mst_Outlet_Update
		public const string Mst_Outlet_Update_InvalidOLCode = "ErridnInventory.Mst_Outlet_Update_InvalidOLCode"; //// //Mst_Outlet_Update_InvalidOLCode
		public const string Mst_Outlet_Update_InvalidDBCode = "ErridnInventory.Mst_Outlet_Update_InvalidDBCode"; //// //Mst_Outlet_Update_InvalidDBCode
		public const string Mst_Outlet_Update_InvalidOLName = "ErridnInventory.Mst_Outlet_Update_InvalidOLName"; //// //Mst_Outlet_Update_InvalidOLName
		public const string Mst_Outlet_Update_InvalidOLAddress = "ErridnInventory.Mst_Outlet_Update_InvalidOLAddress"; //// //Mst_Outlet_Update_InvalidOLAddress
		public const string Mst_Outlet_Update_InvalidOLContactName = "ErridnInventory.Mst_Outlet_Update_InvalidOLContactName"; //// //Mst_Outlet_Update_InvalidOLContactName
		public const string Mst_Outlet_Update_InvalidOLPhoneNo = "ErridnInventory.Mst_Outlet_Update_InvalidOLPhoneNo"; //// //Mst_Outlet_Update_InvalidOLPhoneNo
		public const string Mst_Outlet_Update_InvalidOLFaxNo = "ErridnInventory.Mst_Outlet_Update_InvalidOLFaxNo"; //// //Mst_Outlet_Update_InvalidOLFaxNo
		public const string Mst_Outlet_Update_InvalidOLMobilePhoneNo = "ErridnInventory.Mst_Outlet_Update_InvalidOLMobilePhoneNo"; //// //Mst_Outlet_Update_InvalidOLMobilePhoneNo
		public const string Mst_Outlet_Update_InvalidOLSMSPhoneNo = "ErridnInventory.Mst_Outlet_Update_InvalidOLSMSPhoneNo"; //// //Mst_Outlet_Update_InvalidOLSMSPhoneNo
		public const string Mst_Outlet_Update_InvalidOLTaxCode = "ErridnInventory.Mst_Outlet_Update_InvalidOLTaxCode"; //// //Mst_Outlet_Update_InvalidOLTaxCode
		public const string Mst_Outlet_Update_InvalidRemark = "ErridnInventory.Mst_Outlet_Update_InvalidRemark"; //// //Mst_Outlet_Update_InvalidRemark
		public const string Mst_Outlet_Update_InvalidOLStatus = "ErridnInventory.Mst_Outlet_Update_InvalidOLStatus"; //// //Mst_Outlet_Update_InvalidOLStatus
		public const string Mst_Outlet_Update_InvalidCreateDate = "ErridnInventory.Mst_Outlet_Update_InvalidCreateDate"; //// //Mst_Outlet_Update_InvalidCreateDate
		public const string Mst_Outlet_Update_InvalidCreateBy = "ErridnInventory.Mst_Outlet_Update_InvalidCreateBy"; //// //Mst_Outlet_Update_InvalidCreateBy
		public const string Mst_Outlet_Update_InvalidApproveDate = "ErridnInventory.Mst_Outlet_Update_InvalidApproveDate"; //// //Mst_Outlet_Update_InvalidApproveDate
		public const string Mst_Outlet_Update_InvalidApproveBy = "ErridnInventory.Mst_Outlet_Update_InvalidApproveBy"; //// //Mst_Outlet_Update_InvalidApproveBy
		public const string Mst_Outlet_Update_InvalidCancelDate = "ErridnInventory.Mst_Outlet_Update_InvalidCancelDate"; //// //Mst_Outlet_Update_InvalidCancelDate
		public const string Mst_Outlet_Update_InvalidCancelBy = "ErridnInventory.Mst_Outlet_Update_InvalidCancelBy"; //// //Mst_Outlet_Update_InvalidCancelBy

		// Mst_Outlet_Delete:
		public const string Mst_Outlet_Delete = "ErridnInventory.Mst_Outlet_Delete"; //// //Mst_Outlet_Delete

		#endregion

		#region // Mst_Param:
		// Mst_Param_Get:
		public const string Mst_Param_Get = "ErridnInventory.Mst_Param_Get"; //// //Mst_Param_Get

		// WAS_Mst_Param_Get:
		public const string WAS_Mst_Param_Get = "ErridnInventory.WAS_Mst_Param_Get"; //// //WAS_Mst_Param_Get
		#endregion

		#region // Mst_Bank:
		// Mst_Bank_CheckDB:
		public const string Mst_Bank_CheckDB_BankNotFound = "ErridnInventory.Mst_Bank_CheckDB_BankNotFound"; //// //Mst_Bank_CheckDB_BankNotFound
		public const string Mst_Bank_CheckDB_BankExist = "ErridnInventory.Mst_Bank_CheckDB_BankExist"; //// //Mst_Bank_CheckDB_BankExist
		public const string Mst_Bank_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Bank_CheckDB_FlagActiveNotMatched"; //// //Mst_Bank_CheckDB_FlagActiveNotMatched

		// Mst_Bank_Get:
		public const string Mst_Bank_Get = "ErridnInventory.Mst_Bank_Get"; //// //Mst_Bank_Get

		// Mst_Bank_Create:
		public const string Mst_Bank_Create = "ErridnInventory.Mst_Bank_Create"; //// //Mst_Bank_Create
		public const string Mst_Bank_Create_InvalidBankCode = "ErridnInventory.Mst_Bank_Create_InvalidBankCode"; //// //Mst_Bank_Create_InvalidBankCode
		public const string Mst_Bank_Create_InvalidBankName = "ErridnInventory.Mst_Bank_Create_InvalidBankName"; //// //Mst_Bank_Create_InvalidBankName

		// Mst_Bank_Update:
		public const string Mst_Bank_Update = "ErridnInventory.Mst_Bank_Update"; //// //Mst_Bank_Update
		public const string Mst_Bank_Update_InvalidBankName = "ErridnInventory.Mst_Bank_Update_InvalidBankName"; //// //Mst_Bank_Update_InvalidBankName

		// Mst_Bank_Delete:
		public const string Mst_Bank_Delete = "ErridnInventory.Mst_Bank_Delete"; //// //Mst_Bank_Delete
		#endregion

		#region // Mst_BankProduct:
		// Mst_BankProduct_CheckDB:
		public const string Mst_BankProduct_CheckDB_BankProductNotFound = "ErridnInventory.Mst_BankProduct_CheckDB_BankProductNotFound"; //// //Mst_BankProduct_CheckDB_BankProductNotFound
		public const string Mst_BankProduct_CheckDB_BankProductExist = "ErridnInventory.Mst_BankProduct_CheckDB_BankProductExist"; //// //Mst_BankProduct_CheckDB_BankProductExist
		public const string Mst_BankProduct_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_BankProduct_CheckDB_FlagActiveNotMatched"; //// //Mst_BankProduct_CheckDB_FlagActiveNotMatched

		// Mst_BankProduct_Get:
		public const string Mst_BankProduct_Get = "ErridnInventory.Mst_BankProduct_Get"; //// //Mst_BankProduct_Get

		// Mst_BankProduct_Create:
		public const string Mst_BankProduct_Create = "ErridnInventory.Mst_BankProduct_Create"; //// //Mst_BankProduct_Create
		public const string Mst_BankProduct_Create_InvalidBankPrdCode = "ErridnInventory.Mst_BankProduct_Create_InvalidBankPrdCode"; //// //Mst_BankProduct_Create_InvalidBankPrdCode
		public const string Mst_BankProduct_Create_InvalidBankPrdName = "ErridnInventory.Mst_BankProduct_Create_InvalidBankPrdName"; //// //Mst_BankProduct_Create_InvalidBankPrdName
		public const string Mst_BankProduct_Create_InvalidEffDateStart = "ErridnInventory.Mst_BankProduct_Create_InvalidEffDateStart"; //// //Mst_BankProduct_Create_InvalidEffDateStart
		public const string Mst_BankProduct_Create_InvalidEffDateStartAfterSysDate = "ErridnInventory.Mst_BankProduct_Create_InvalidEffDateStartAfterSysDate"; //// //Mst_BankProduct_Create_InvalidEffDateStartAfterSysDate
		public const string Mst_BankProduct_Create_InvalidEffDateStartAfterEffDateStartParent = "ErridnInventory.Mst_BankProduct_Create_InvalidEffDateStartAfterEffDateStartParent"; //// //Mst_BankProduct_Create_InvalidEffDateStartAfterEffDateStartParent

		// Mst_BankProduct_Update:
		public const string Mst_BankProduct_Update = "ErridnInventory.Mst_BankProduct_Update"; //// //Mst_BankProduct_Update
		public const string Mst_BankProduct_Update_InvalidBankPrdName = "ErridnInventory.Mst_BankProduct_Update_InvalidBankPrdName"; //// //Mst_BankProduct_Update_InvalidBankPrdName

		// Mst_BankProduct_Delete:
		public const string Mst_BankProduct_Delete = "ErridnInventory.Mst_BankProduct_Delete"; //// //Mst_BankProduct_Delete
		#endregion

		#region // Mst_CustomerType:
		// Mst_CustomerType_CheckDB:
		public const string Mst_CustomerType_CheckDB_CtmTypeNotFound = "ErridnInventory.Mst_CustomerType_CheckDB_CtmTypeNotFound"; //// //Mst_CustomerType_CheckDB_CtmTypeNotFound
		public const string Mst_CustomerType_CheckDB_CtmTypeExist = "ErridnInventory.Mst_CustomerType_CheckDB_CtmTypeExist"; //// //Mst_CustomerType_CheckDB_CtmTypeExist
		public const string Mst_CustomerType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CustomerType_CheckDB_FlagActiveNotMatched"; //// //Mst_CustomerType_CheckDB_FlagActiveNotMatched

		// Mst_CustomerType_Get:
		public const string Mst_CustomerType_Get = "ErridnInventory.Mst_CustomerType_Get"; //// //Mst_CustomerType_Get
		#endregion

		#region // Mst_Customer:
		// Mst_Customer_CheckDB:
		public const string Mst_Customer_CheckDB_CustomerNotFound = "ErridnInventory.Mst_Customer_CheckDB_CustomerNotFound"; //// //Mst_Customer_CheckDB_CustomerNotFound
		public const string Mst_Customer_CheckDB_CustomerExist = "ErridnInventory.Mst_Customer_CheckDB_CustomerExist"; //// //Mst_Customer_CheckDB_CustomerExist
		public const string Mst_Customer_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Customer_CheckDB_FlagActiveNotMatched"; //// //Mst_Customer_CheckDB_FlagActiveNotMatched

		// Mst_Customer_CheckBizCtmType:
		public const string Mst_Customer_CheckBiz_InvalidCtmType = "ErridnInventory.Mst_Customer_CheckBiz_InvalidCtmType"; //// //Mst_Customer_CheckBiz_InvalidCtmType

		// Mst_Customer_Get:
		public const string Mst_Customer_Get = "ErridnInventory.Mst_Customer_Get"; //// //Mst_Customer_Get

		// Mst_Customer_Create:
		public const string Mst_Customer_Create = "ErridnInventory.Mst_Customer_Create"; //// //Mst_Customer_Create
		public const string Mst_Customer_Create_InvalidCtmCode = "ErridnInventory.Mst_Customer_Create_InvalidCtmCode"; //// //Mst_Customer_Create_InvalidCtmCode
		public const string Mst_Customer_Create_InvalidCtmType = "ErridnInventory.Mst_Customer_Create_InvalidCtmType"; //// //Mst_Customer_Create_InvalidCtmType
		public const string Mst_Customer_Create_InvalidCtmName = "ErridnInventory.Mst_Customer_Create_InvalidCtmName"; //// //Mst_Customer_Create_InvalidCtmName

		// Mst_Customer_Update:
		public const string Mst_Customer_Update = "ErridnInventory.Mst_Customer_Update"; //// //Mst_Customer_Update
		public const string Mst_Customer_Update_InvalidCtmName = "ErridnInventory.Mst_Customer_Update_InvalidCtmName"; //// //Mst_Customer_Update_InvalidCtmName

		// Mst_Customer_Delete:
		public const string Mst_Customer_Delete = "ErridnInventory.Mst_Customer_Delete"; //// //Mst_Customer_Delete
		#endregion

		#region // Ctm_RoomDL:
		// Ctm_RoomDL_CheckDB:
		public const string Ctm_RoomDL_CheckDB_RoomDLNotFound = "ErridnInventory.Ctm_RoomDL_CheckDB_RoomDLNotFound"; //// //Ctm_RoomDL_CheckDB_RoomDLNotFound
		public const string Ctm_RoomDL_CheckDB_RoomDLExist = "ErridnInventory.Ctm_RoomDL_CheckDB_RoomDLExist"; //// //Ctm_RoomDL_CheckDB_RoomDLExist
		public const string Ctm_RoomDL_CheckDB_FlagActiveNotMatched = "ErridnInventory.Ctm_RoomDL_CheckDB_FlagActiveNotMatched"; //// //Ctm_RoomDL_CheckDB_FlagActiveNotMatched

		// Ctm_RoomDL_Get:
		public const string Ctm_RoomDL_Get = "ErridnInventory.Ctm_RoomDL_Get"; //// //Ctm_RoomDL_Get

		// Ctm_RoomDL_Save:
		public const string Ctm_RoomDL_Save = "ErridnInventory.Ctm_RoomDL_Save"; //// //Ctm_RoomDL_Save
		public const string Ctm_RoomDL_Save_InvalidCtmType = "ErridnInventory.Ctm_RoomDL_Save_InvalidCtmType"; //// //Ctm_RoomDL_Save_InvalidCtmType
		public const string Ctm_RoomDL_Save_InputTblNotFound = "ErridnInventory.Ctm_RoomDL_Save_InputTblNotFound"; //// //Ctm_RoomDL_Save_InputTblNotFound
		public const string Ctm_RoomDL_Save_InvalidValue = "ErridnInventory.Ctm_RoomDL_Save_InvalidValue"; //// //Ctm_RoomDL_Save_InvalidValue
		public const string Ctm_RoomDL_Save_InvalidPaymentType = "ErridnInventory.Ctm_RoomDL_Save_InvalidPaymentType"; //// //Ctm_RoomDL_Save_InvalidPaymentType
		public const string Ctm_RoomDL_Save_InvalidRoomDL = "ErridnInventory.Ctm_RoomDL_Save_InvalidRoomDL"; //// //Ctm_RoomDL_Save_InvalidRoomDL

		// Ctm_RoomDL_Create:
		public const string Ctm_RoomDL_Create = "ErridnInventory.Ctm_RoomDL_Create"; //// //Ctm_RoomDL_Create
		public const string Ctm_RoomDL_Create_InvalidValue = "ErridnInventory.Ctm_RoomDL_Create_InvalidValue"; //// //Ctm_RoomDL_Create_InvalidValue

		// Ctm_RoomDL_Update:
		public const string Ctm_RoomDL_Update = "ErridnInventory.Ctm_RoomDL_Update"; //// //Ctm_RoomDL_Update
		public const string Ctm_RoomDL_Update_InvalidValue = "ErridnInventory.Ctm_RoomDL_Update_InvalidValue"; //// //Ctm_RoomDL_Update_InvalidValue

		// Ctm_RoomDL_Delete:
		public const string Ctm_RoomDL_Delete = "ErridnInventory.Ctm_RoomDL_Delete"; //// //Ctm_RoomDL_Delete
		#endregion

		#region // Mst_Currency:
		// Mst_Currency_CheckDB:
		public const string Mst_Currency_CheckDB_CurrencyNotFound = "ErridnInventory.Mst_Currency_CheckDB_CurrencyNotFound"; //// //Mst_Currency_CheckDB_CurrencyNotFound
		public const string Mst_Currency_CheckDB_CurrencyExist = "ErridnInventory.Mst_Currency_CheckDB_CurrencyExist"; //// //Mst_Currency_CheckDB_CurrencyExist
		public const string Mst_Currency_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Currency_CheckDB_FlagActiveNotMatched"; //// //Mst_Currency_CheckDB_FlagActiveNotMatched

		// Mst_Currency_Get:
		public const string Mst_Currency_Get = "ErridnInventory.Mst_Currency_Get"; //// //Mst_Currency_Get

		// WAS_Mst_Currency_Get:
		public const string WAS_Mst_Currency_Get = "ErridnInventory.WAS_Mst_Currency_Get"; //// //WAS_Mst_Currency_Get
		#endregion

		#region // Mst_CurrencyConvert:
		//// Mst_CurrencyConvert_Get:
		//public const string Mst_CurrencyConvert_Get = "ErridnInventory.Mst_CurrencyConvert_Get"; //// //Mst_CurrencyConvert_Get

		//// Mst_CurrencyConvert_Save:
		public const string Mst_CurrencyConvert_Save = "ErridnInventory.Mst_CurrencyConvert_Save"; //// //Mst_CurrencyConvert_Save
		public const string Mst_CurrencyConvert_Save_InputTblNotFound = "ErridnInventory.Mst_CurrencyConvert_Save_InputTblNotFound"; //// //Mst_CurrencyConvert_Save_InputTblNotFound
		public const string Mst_CurrencyConvert_Save_InvalidValue = "ErridnInventory.Mst_CurrencyConvert_Save_InvalidValue"; //// //Mst_CurrencyConvert_Save_InvalidValue
		#endregion

		#region // Mst_CollateralType:
		// Mst_CollateralType_CheckDB:
		public const string Mst_CollateralType_CheckDB_CollateralTypeNotFound = "ErridnInventory.Mst_CollateralType_CheckDB_CollateralTypeNotFound"; //// //Mst_CollateralType_CheckDB_CollateralTypeNotFound
		public const string Mst_CollateralType_CheckDB_CollateralTypeExist = "ErridnInventory.Mst_CollateralType_CheckDB_CollateralTypeExist"; //// //Mst_CollateralType_CheckDB_CollateralTypeExist
		public const string Mst_CollateralType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CollateralType_CheckDB_FlagActiveNotMatched"; //// //Mst_CollateralType_CheckDB_FlagActiveNotMatched

		// Mst_CollateralType_Get:
		public const string Mst_CollateralType_Get = "ErridnInventory.Mst_CollateralType_Get"; //// //Mst_CollateralType_Get
		#endregion

		#region // Mst_CollateralSpecInputType:
		// Mst_CollateralSpecInputType_CheckDB:
		public const string Mst_CollateralSpecInputType_CheckDB_CollateralSpecInputTypeNotFound = "ErridnInventory.Mst_CollateralSpecInputType_CheckDB_CollateralSpecInputTypeNotFound"; //// //Mst_CollateralSpecInputType_CheckDB_CollateralSpecInputTypeNotFound
		public const string Mst_CollateralSpecInputType_CheckDB_CollateralSpecInputTypeExist = "ErridnInventory.Mst_CollateralSpecInputType_CheckDB_CollateralSpecInputTypeExist"; //// //Mst_CollateralSpecInputType_CheckDB_CollateralSpecInputTypeExist
		public const string Mst_CollateralSpecInputType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CollateralSpecInputType_CheckDB_FlagActiveNotMatched"; //// //Mst_CollateralSpecInputType_CheckDB_FlagActiveNotMatched

		// Mst_CollateralSpecInputType_Get:
		public const string Mst_CollateralSpecInputType_Get = "ErridnInventory.Mst_CollateralSpecInputType_Get"; //// //Mst_CollateralSpecInputType_Get
		#endregion

		#region // Mst_MDType:
		// Mst_MDType_CheckDB:
		public const string Mst_MDType_CheckDB_MDTypeNotFound = "ErridnInventory.Mst_MDType_CheckDB_MDTypeNotFound"; //// //Mst_MDType_CheckDB_MDTypeNotFound
		public const string Mst_MDType_CheckDB_MDTypeExist = "ErridnInventory.Mst_MDType_CheckDB_MDTypeExist"; //// //Mst_MDType_CheckDB_MDTypeExist
		public const string Mst_MDType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_MDType_CheckDB_FlagActiveNotMatched"; //// //Mst_MDType_CheckDB_FlagActiveNotMatched
		#endregion

		#region // Mst_Chain:
		// Mst_Chain_CheckDB:
		public const string Mst_Chain_CheckDB_ChainNotFound = "ErridnInventory.Mst_Chain_CheckDB_ChainNotFound"; //// //Mst_Chain_CheckDB_ChainNotFound
		public const string Mst_Chain_CheckDB_ChainExist = "ErridnInventory.Mst_Chain_CheckDB_ChainExist"; //// //Mst_Chaint_CheckDB_ChainExist
		public const string Mst_Chain_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Chain_CheckDB_FlagActiveNotMatched"; //// //Mst_Chain_CheckDB_FlagActiveNotMatched

		// Mst_Chain_Get:
		public const string Mst_Chain_Get = "ErridnInventory.Mst_Chain_Get"; //// //Mst_Chain_Get

		// Mst_Chain_Create:
		public const string Mst_Chain_Create = "ErridnInventory.Mst_Chain_Create"; //// //Mst_Chain_Create
		public const string Mst_Chain_Create_InvalidChainCode = "ErridnInventory.Mst_Chain_Create_InvalidChainCode"; //// //Mst_Chain_Create_InvalidChainCode
		public const string Mst_Chain_Create_InvalidChainName = "ErridnInventory.Mst_Chain_Create_InvalidChainName"; //// //Mst_Chain_Create_InvalidChainName        

		// Mst_Chain_Update:
		public const string Mst_Chain_Update = "ErridnInventory.Mst_Chain_Update"; //// //Mst_Chain_Update                
		public const string Mst_Chain_Update_InvalidChainName = "ErridnInventory.Mst_Chain_Update_InvalidChainName"; //// //Mst_Chain_Update_InvalidChainName                       

		// Mst_Chain_Delete:
		public const string Mst_Chain_Delete = "ErridnInventory.Mst_Chain_Delete"; //// Mã lỗi: Mst_Chain_Delete ////

		// ChainBiz_UpdAuto_Run:
		public const string ChainBiz_UpdAuto_Run = "ErridnInventory.ChainBiz_UpdAuto_Run"; //// //ChainBiz_UpdAuto_Run

		// CB_Auto_Run:
		public const string CB_Auto_Run = "ErridnInventory.CB_Auto_Run"; //// //CB_Auto_Run
		public const string CB_Auto_Run_Invalid_T24_ChainCode = "ErridnInventory.CB_Auto_Run_Invalid_T24_ChainCode"; //// //CB_Auto_Run_Invalid_T24_ChainCode

		// ChainBiz_RefineAuto:
		public const string ChainBiz_RefineAuto = "ErridnInventory.ChainBiz_RefineAuto"; //// //ChainBiz_RefineAuto

		// ChainBiz_AutoUpd_ByCtm:
		public const string ChainBiz_AutoUpd_ByCtm = "ErridnInventory.ChainBiz_AutoUpd_ByCtm"; //// //ChainBiz_AutoUpd_ByCtm

		// ChainBiz_AutoSave_ByCtm:
		public const string ChainBiz_AutoSave_ByCtm = "ErridnInventory.ChainBiz_AutoSave_ByCtm"; //// //ChainBiz_AutoSave_ByCtm

		// MD_MD_Save_Auto_CheckDB_CollateralNotFound:
		public const string MD_MD_Save_Auto_CheckDB_CollateralNotFound = "ErridnInventory.MD_MD_Save_Auto_CheckDB_CollateralNotFound"; //// //MD_MD_Save_Auto_CheckDB_CollateralNotFound
		#endregion

		#region // ChainBiz:
		// CB_ColAndMD_Migration:
		public const string CB_ColAndMD_Migration = "ErridnInventory.CB_ColAndMD_Migration"; //// //CB_ColAndMD_Migration
		public const string CB_ColAndMD_Migration_Input_ColletaralTblNotFound = "ErridnInventory.CB_ColAndMD_Migration_Input_ColletaralTblNotFound"; //// //CB_ColAndMD_Migration_Input_ColletaralTblNotFound
		public const string CB_ColAndMD_Migration_Input_MDTblNotFound = "ErridnInventory.CB_ColAndMD_Migration_Input_MDTblNotFound"; //// //CB_ColAndMD_Migration_Input_MDTblNotFound
		public const string CB_ColAndMD_Migration_InvalidCollateralType = "ErridnInventory.CB_ColAndMD_Migration_InvalidCollateralType"; //// //CB_ColAndMD_Migration_InvalidCollateralType
		public const string CB_ColAndMD_Migration_InvalidMDType = "ErridnInventory.CB_ColAndMD_Migration_InvalidMDType"; //// //CB_ColAndMD_Migration_InvalidMDType
		#endregion

		#region // Ctm_LimitBankProduct:
		// Ctm_LimitBankProduct_Get:
		public const string Ctm_LimitBankProduct_Get = "ErridnInventory.Ctm_LimitBankProduct_Get"; //// //Ctm_LimitBankProduct_Get

		// Ctm_LimitBankProduct_Save:
		public const string Ctm_LimitBankProduct_Save = "ErridnInventory.Ctm_LimitBankProduct_Save"; //// //Ctm_LimitBankProduct_Save
		public const string Ctm_LimitBankProduct_Save_InputLimitBankProductNotFound = "ErridnInventory.Ctm_LimitBankProduct_Save_InputLimitBankProductNotFound"; //// //Ctm_LimitBankProduct_Save_InputLimitBankProductNotFound
		public const string Ctm_LimitBankProduct_Save_InvalidValue = "ErridnInventory.Ctm_LimitBankProduct_Save_InputLimitBankProductNotFound"; //// //Ctm_LimitBankProduct_Save_InputLimitBankProductNotFound
		#endregion

		#region // Mst_PropertyType:
		// Mst_PropertyType_CheckDB:
		public const string Mst_PropertyType_CheckDB_PropertyTypeNotFound = "ErridnInventory.Mst_PropertyType_CheckDB_PropertyTypeNotFound"; //// //Mst_PropertyType_CheckDB_PropertyTypeNotFound
		public const string Mst_PropertyType_CheckDB_PropertyTypeExist = "ErridnInventory.Mst_PropertyType_CheckDB_PropertyTypeExist"; //// //Mst_PropertyType_CheckDB_PropertyTypeExist
		public const string Mst_PropertyType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PropertyType_CheckDB_FlagActiveNotMatched"; //// //Mst_PropertyType_CheckDB_FlagActiveNotMatched

		// Mst_PropertyType_Get:
		public const string Mst_PropertyType_Get = "ErridnInventory.Mst_PropertyType_Get"; //// //Mst_PropertyType_Get		

		// Mst_PropertyType_Create:
		public const string Mst_PropertyType_Create = "ErridnInventory.Mst_PropertyType_Create"; //// //Mst_PropertyType_Create
		public const string Mst_PropertyType_Create_InvalidPropertyType = "ErridnInventory.Mst_PropertyType_Create_InvalidPropertyType"; //// //Mst_PropertyType_Create_InvalidPropertyType
		public const string Mst_PropertyType_Create_InvalidPropertyTypeName = "ErridnInventory.Mst_PropertyType_Create_InvalidPropertyTypeName"; //// //Mst_PropertyType_Create_InvalidPropertyTypeName

		// Mst_PropertyType_Update:
		public const string Mst_PropertyType_Update = "ErridnInventory.Mst_PropertyType_Update"; //// //Mst_PropertyType_Update
		public const string Mst_PropertyType_Update_InvalidPropertyTypeName = "ErridnInventory.Mst_PropertyType_Update_InvalidPropertyTypeName"; //// //Mst_PropertyType_Update_InvalidPropertyTypeName

		// Mst_PropertyType_Delete:
		public const string Mst_PropertyType_Delete = "ErridnInventory.Mst_PropertyType_Delete"; //// //Mst_PropertyType_Delete
		#endregion

		#region // Mst_CarColor:
		// Mst_CarColor_CheckDB:
		public const string Mst_CarColor_CheckDB_CarColorNotFound = "ErridnInventory.Mst_CarColor_CheckDB_CarColorNotFound"; //// //Mst_CarColor_CheckDB_CarColorNotFound
		public const string Mst_CarColor_CheckDB_CarColorExist = "ErridnInventory.Mst_CarColor_CheckDB_CarColorExist"; //// //Mst_CarColor_CheckDB_CarColorExist
		public const string Mst_CarColor_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CarColor_CheckDB_FlagActiveNotMatched"; //// //Mst_CarColor_CheckDB_FlagActiveNotMatched

		// Mst_CarColor_Get:
		public const string Mst_CarColor_Get = "ErridnInventory.Mst_CarColor_Get"; //// //Mst_CarColor_Get

		// Mst_CarColor_Create:
		public const string Mst_CarColor_Create = "ErridnInventory.Mst_CarColor_Create"; //// //Mst_CarColor_Create
		public const string Mst_CarColor_Create_InvalidColorCode = "ErridnInventory.Mst_CarColor_Create_InvalidColorCode"; //// //Mst_CarColor_Create_InvalidColorCode
		public const string Mst_CarColor_Create_InvalidColorName = "ErridnInventory.Mst_CarColor_Create_InvalidColorName"; //// //Mst_CarColor_Create_InvalidColorName

		// Mst_CarColor_Update:
		public const string Mst_CarColor_Update = "ErridnInventory.Mst_CarColor_Update"; //// //Mst_CarColor_Update
		public const string Mst_CarColor_Update_InvalidColorName = "ErridnInventory.Mst_CarColor_Update_InvalidColorName"; //// //Mst_CarColor_Update_InvalidColorName

		// Mst_CarColor_Delete:
		public const string Mst_CarColor_Delete = "ErridnInventory.Mst_CarColor_Delete"; //// //Mst_CarColor_Delete
		#endregion

		#region // Mst_CarModel:
		// Mst_CarModel_CheckDB:
		public const string Mst_CarModel_CheckDB_CarModelNotFound = "ErridnInventory.Mst_CarModel_CheckDB_CarModelNotFound"; //// //Mst_CarModel_CheckDB_CarModelNotFound
		public const string Mst_CarModel_CheckDB_CarModelExist = "ErridnInventory.Mst_CarModel_CheckDB_CarModelExist"; //// //Mst_CarModel_CheckDB_CarModelExist
		public const string Mst_CarModel_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CarModel_CheckDB_FlagActiveNotMatched"; //// //Mst_CarModel_CheckDB_FlagActiveNotMatched

		// Mst_CarModel_Get:
		public const string Mst_CarModel_Get = "ErridnInventory.Mst_CarModel_Get"; //// //Mst_CarModel_Get

		// Mst_CarModel_Create:
		public const string Mst_CarModel_Create = "ErridnInventory.Mst_CarModel_Create"; //// //Mst_CarModel_Create
		public const string Mst_CarModel_Create_InvalidModelCode = "ErridnInventory.Mst_CarModel_Create_InvalidModelCode"; //// //Mst_CarModel_Create_InvalidModelCode
		public const string Mst_CarModel_Create_InvalidModelName = "ErridnInventory.Mst_CarModel_Create_InvalidModelName"; //// //Mst_CarModel_Create_InvalidModelName

		// Mst_CarModel_Update:
		public const string Mst_CarModel_Update = "ErridnInventory.Mst_CarModel_Update"; //// //Mst_CarModel_Update
		public const string Mst_CarModel_Update_InvalidModelName = "ErridnInventory.Mst_CarModel_Update_InvalidModelName"; //// //Mst_CarModel_Update_InvalidModelName

		// Mst_CarModel_Delete:
		public const string Mst_CarModel_Delete = "ErridnInventory.Mst_CarModel_Delete"; //// //Mst_CarModel_Delete
		#endregion

		#region // Mst_CarSpec:
		// Mst_CarSpec_CheckDB:
		public const string Mst_CarSpec_CheckDB_CarSpecNotFound = "ErridnInventory.Mst_CarSpec_CheckDB_CarSpecNotFound"; //// //Mst_CarSpec_CheckDB_CarSpecNotFound
		public const string Mst_CarSpec_CheckDB_CarSpecExist = "ErridnInventory.Mst_CarSpec_CheckDB_CarSpecExist"; //// //Mst_CarSpec_CheckDB_CarSpecExist
		public const string Mst_CarSpec_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CarSpec_CheckDB_FlagActiveNotMatched"; //// //Mst_CarSpec_CheckDB_FlagActiveNotMatched

		// Mst_CarSpec_Get:
		public const string Mst_CarSpec_Get = "ErridnInventory.Mst_CarSpec_Get"; //// //Mst_CarSpec_Get

		// Mst_CarSpec_Create:
		public const string Mst_CarSpec_Create = "ErridnInventory.Mst_CarSpec_Create"; //// //Mst_CarSpec_Create
		public const string Mst_CarSpec_Create_InvalidSpecCode = "ErridnInventory.Mst_CarSpec_Create_InvalidSpecCode"; //// //Mst_CarSpec_Create_InvalidSpecCode
		public const string Mst_CarSpec_Create_InvalidSpecName = "ErridnInventory.Mst_CarSpec_Create_InvalidSpecName"; //// //Mst_CarSpec_Create_InvalidSpecName

		// Mst_CarSpec_Update:
		public const string Mst_CarSpec_Update = "ErridnInventory.Mst_CarSpec_Update"; //// //Mst_CarSpec_Update
		public const string Mst_CarSpec_Update_InvalidSpecName = "ErridnInventory.Mst_CarSpec_Update_InvalidSpecName"; //// //Mst_CarSpec_Update_InvalidSpecName

		// Mst_CarSpec_Delete:
		public const string Mst_CarSpec_Delete = "ErridnInventory.Mst_CarSpec_Delete"; //// //Mst_CarSpec_Delete
		#endregion

		#region // Mst_CarSubSpec:
		// Mst_CarSubSpec_CheckDB:
		public const string Mst_CarSubSpec_CheckDB_CarSubSpecNotFound = "ErridnInventory.Mst_CarSubSpec_CheckDB_CarSubSpecNotFound"; //// //Mst_CarSubSpec_CheckDB_CarSubSpecNotFound
		public const string Mst_CarSubSpec_CheckDB_CarSubSpecExist = "ErridnInventory.Mst_CarSubSpec_CheckDB_CarSubSpecExist"; //// //Mst_CarSubSpec_CheckDB_CarSubSpecExist
		public const string Mst_CarSubSpec_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CarSubSpec_CheckDB_FlagActiveNotMatched"; //// //Mst_CarSubSpec_CheckDB_FlagActiveNotMatched

		// Mst_CarSubSpec_Get:
		public const string Mst_CarSubSpec_Get = "ErridnInventory.Mst_CarSubSpec_Get"; //// //Mst_CarSubSpec_Get

		// Mst_CarSubSpec_Create:
		public const string Mst_CarSubSpec_Create = "ErridnInventory.Mst_CarSubSpec_Create"; //// //Mst_CarSubSpec_Create
		public const string Mst_CarSubSpec_Create_InvalidSpecCode = "ErridnInventory.Mst_CarSubSpec_Create_InvalidSpecCode"; //// //Mst_CarSubSpec_Create_InvalidSpecCode
		public const string Mst_CarSubSpec_Create_InvalidSubSpecDesc = "ErridnInventory.Mst_CarSubSpec_Create_InvalidSubSpecDesc"; //// //Mst_CarSubSpec_Create_InvalidSubSpecDesc

		// Mst_CarSubSpec_Update:
		public const string Mst_CarSubSpec_Update = "ErridnInventory.Mst_CarSubSpec_Update"; //// //Mst_CarSubSpec_Update
		public const string Mst_CarSubSpec_Update_InvalidSubSpecDesc = "ErridnInventory.Mst_CarSubSpec_Update_InvalidSubSpecDesc"; //// //Mst_CarSubSpec_Update_InvalidSubSpecDesc

		// Mst_CarSubSpec_Delete:
		public const string Mst_CarSubSpec_Delete = "ErridnInventory.Mst_CarSubSpec_Delete"; //// //Mst_CarSubSpec_Delete
		#endregion

		#region // Mst_CarSubSpecPrice:
		// Mst_CarSubSpecPrice_CheckDB:
		public const string Mst_CarSubSpecPrice_CheckDB_CarSubSpecPriceNotFound = "ErridnInventory.Mst_CarSubSpecPrice_CheckDB_CarSubSpecPriceNotFound"; //// //Mst_CarSubSpecPrice_CheckDB_CarSubSpecPriceNotFound
		public const string Mst_CarSubSpecPrice_CheckDB_CarSubSpecPriceExist = "ErridnInventory.Mst_CarSubSpecPrice_CheckDB_CarSubSpecPriceExist"; //// //Mst_CarSubSpecPrice_CheckDB_CarSubSpecPriceExist
		public const string Mst_CarSubSpecPrice_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CarSubSpecPrice_CheckDB_FlagActiveNotMatched"; //// //Mst_CarSubSpecPrice_CheckDB_FlagActiveNotMatched

		// Mst_CarSubSpecPrice_Get:
		public const string Mst_CarSubSpecPrice_Get = "ErridnInventory.Mst_CarSubSpecPrice_Get"; //// //Mst_CarSubSpecPrice_Get

		// Mst_CarSubSpecPrice_Save:
		public const string Mst_CarSubSpecPrice_Save = "ErridnInventory.Mst_CarSubSpecPrice_Save"; //// //Mst_CarSubSpecPrice_Save
		public const string Mst_CarSubSpecPrice_Save_InputTblNotFound = "ErridnInventory.Mst_CarSubSpecPrice_Save_InputTblNotFound"; //// //Mst_CarSubSpecPrice_Save_InputTblNotFound
		public const string Mst_CarSubSpecPrice_Save_InvalidValue = "ErridnInventory.Mst_CarSubSpecPrice_Save_InvalidValue"; //// //Mst_CarSubSpecPrice_Save_InvalidValue

		// Mst_CarSubSpecPrice_Create:
		public const string Mst_CarSubSpecPrice_Create = "ErridnInventory.Mst_CarSubSpecPrice_Create"; //// //Mst_CarSubSpecPrice_Create
		public const string Mst_CarSubSpecPrice_Create_InvalidValue = "ErridnInventory.Mst_CarSubSpecPrice_Create_InvalidValue"; //// //Mst_CarSubSpecPrice_Create_InvalidValue

		// Mst_CarSubSpecPrice_Update:
		public const string Mst_CarSubSpecPrice_Update = "ErridnInventory.Mst_CarSubSpecPrice_Update"; //// //Mst_CarSubSpecPrice_Update
		public const string Mst_CarSubSpecPrice_Update_InvalidValue = "ErridnInventory.Mst_CarSubSpecPrice_Update_InvalidValue"; //// //Mst_CarSubSpecPrice_Update_InvalidValue

		// Mst_CarSubSpecPrice_Delete:
		public const string Mst_CarSubSpecPrice_Delete = "ErridnInventory.Mst_CarSubSpecPrice_Delete"; //// //Mst_CarSubSpecPrice_Delete
		#endregion

		#region // Mst_Account:
		// Mst_Account_CheckDB:
		public const string Mst_Account_CheckDB_AccountNotFound = "ErridnInventory.Mst_Account_CheckDB_AccountNotFound"; //// //Mst_Account_CheckDB_AccountNotFound
		public const string Mst_Account_CheckDB_AccountExist = "ErridnInventory.Mst_Account_CheckDB_AccountExist"; //// //Mst_Account_CheckDB_AccountExist
		public const string Mst_Account_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Account_CheckDB_FlagActiveNotMatched"; //// //Mst_Account_CheckDB_FlagActiveNotMatched

		// Mst_Account_Get:
		public const string Mst_Account_Get = "ErridnInventory.Mst_Account_Get"; //// //Mst_Account_Get

		// Mst_Account_Create:
		public const string Mst_Account_Create = "ErridnInventory.Mst_Account_Create"; //// //Mst_Account_Create
		public const string Mst_Account_Create_InvalidAccCode = "ErridnInventory.Mst_Account_Create_InvalidAccCode"; //// //Mst_Account_Create_InvalidAccCode
		public const string Mst_Account_Create_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_Account_Create_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_Account_Create_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_Account_Create_InvalidAccName = "ErridnInventory.Mst_Account_Create_InvalidAccName"; //// //Mst_Account_Create_InvalidAccName
		public const string Mst_Account_Create_InvalidValue = "ErridnInventory.Mst_Account_Create_InvalidValue"; //// //Mst_Account_Create_InvalidValue

		// Mst_Account_CreateMulti:
		public const string Mst_Account_CreateMulti = "ErridnInventory.Mst_Account_CreateMulti"; //// //Mst_Account_CreateMulti
		public const string Mst_Account_CreateMulti_Input_AccountTblNotFound = "ErridnInventory.Mst_Account_CreateMulti_Input_AccountTblNotFound"; //// //Mst_Account_CreateMulti_Input_AccountTblNotFound
		public const string Mst_Account_CreateMulti_Input_AccountTblInvalid = "ErridnInventory.Mst_Account_CreateMulti_Input_AccountTblInvalid"; //// //Mst_Account_CreateMulti_Input_AccountTblInvalid
		public const string Mst_Account_CreateMulti_InvalidAccCode = "ErridnInventory.Mst_Account_CreateMulti_InvalidAccCode"; //// //Mst_Account_CreateMulti_InvalidAccCode
		public const string Mst_Account_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_Account_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_Account_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_Account_CreateMulti_InvalidAccName = "ErridnInventory.Mst_Account_CreateMulti_InvalidAccName"; //// //Mst_Account_CreateMulti_InvalidAccName
		public const string Mst_Account_CreateMulti_InvalidValue = "ErridnInventory.Mst_Account_CreateMulti_InvalidValue"; //// //Mst_Account_CreateMulti_InvalidValue

		// Mst_Account_Update:
		public const string Mst_Account_Update = "ErridnInventory.Mst_Account_Update"; //// //Mst_Account_Update
		public const string Mst_Account_Update_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_Account_Update_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_Account_Update_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_Account_Update_InvalidAccName = "ErridnInventory.Mst_Account_Update_InvalidAccName"; //// //Mst_Account_Update_InvalidAccName
		public const string Mst_Account_Update_InvalidActiveDateStartBeforeActiveDateStartChild = "ErridnInventory.Mst_Account_Update_InvalidActiveDateStartBeforeActiveDateStartChild"; //// //Mst_Account_Update_InvalidActiveDateStartBeforeActiveDateStartChild
		public const string Mst_Account_Update_ExistCostCenterChildActive = "ErridnInventory.Mst_Account_Update_ExistCostCenterChildActive"; //// //Mst_Account_Update_ExistCostCenterChildActive
		public const string Mst_Account_Update_InvalidValue = "ErridnInventory.Mst_Account_Update_InvalidValue"; //// //Mst_Account_Update_InvalidValue

		// Mst_Account_Delete:
		public const string Mst_Account_Delete = "ErridnInventory.Mst_Account_Delete"; //// //Mst_Account_Delete

		// WAS_Mst_Account_Get:
		public const string WAS_Mst_Account_Get = "ErridnInventory.WAS_Mst_Account_Get"; //// //WAS_Mst_Account_Get

		// WAS_Mst_Account_Create:
		public const string WAS_Mst_Account_Create = "ErridnInventory.WAS_Mst_Account_Create"; //// //WAS_Mst_Account_Create

		// WAS_Mst_Account_Update:
		public const string WAS_Mst_Account_Update = "ErridnInventory.WAS_Mst_Account_Update"; //// //WAS_Mst_Account_Update

		// WAS_Mst_Account_Delete:
		public const string WAS_Mst_Account_Delete = "ErridnInventory.WAS_Mst_Account_Delete"; //// //WAS_Mst_Account_Delete
		#endregion

		#region // Mst_PaymentType:
		// Mst_PaymentType_CheckDB:
		public const string Mst_PaymentType_CheckDB_PaymentTypeNotFound = "ErridnInventory.Mst_PaymentType_CheckDB_PaymentTypeNotFound"; //// //Mst_PaymentType_CheckDB_PaymentTypeNotFound
		public const string Mst_PaymentType_CheckDB_PaymentTypeExist = "ErridnInventory.Mst_PaymentType_CheckDB_PaymentTypeExist"; //// //Mst_PaymentType_CheckDB_PaymentTypeExist
		public const string Mst_PaymentType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PaymentType_CheckDB_FlagActiveNotMatched"; //// //Mst_PaymentType_CheckDB_FlagActiveNotMatched

		// Mst_PaymentType_Get:
		public const string Mst_PaymentType_Get = "ErridnInventory.Mst_PaymentType_Get"; //// //Mst_PaymentType_Get

		// Mst_PaymentType_Create:
		public const string Mst_PaymentType_Create = "ErridnInventory.Mst_PaymentType_Create"; //// //Mst_PaymentType_Create
		public const string Mst_PaymentType_Create_InvalidPaymentType = "ErridnInventory.Mst_PaymentType_Create_InvalidPaymentType"; //// //Mst_PaymentType_Create_InvalidPaymentType
		public const string Mst_PaymentType_Create_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_PaymentType_Create_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_PaymentType_Create_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_PaymentType_Create_InvalidPaymentTypeName = "ErridnInventory.Mst_PaymentType_Create_InvalidPaymentTypeName"; //// //Mst_PaymentType_Create_InvalidPaymentTypeName
		public const string Mst_PaymentType_Create_InvalidValue = "ErridnInventory.Mst_PaymentType_Create_InvalidValue"; //// //Mst_PaymentType_Create_InvalidValue

		// Mst_PaymentType_CreateMulti:
		public const string Mst_PaymentType_CreateMulti = "ErridnInventory.Mst_PaymentType_CreateMulti"; //// //Mst_PaymentType_CreateMulti
		public const string Mst_PaymentType_CreateMulti_Input_AccountTblNotFound = "ErridnInventory.Mst_PaymentType_CreateMulti_Input_AccountTblNotFound"; //// //Mst_PaymentType_CreateMulti_Input_AccountTblNotFound
		public const string Mst_PaymentType_CreateMulti_Input_AccountTblInvalid = "ErridnInventory.Mst_PaymentType_CreateMulti_Input_AccountTblInvalid"; //// //Mst_PaymentType_CreateMulti_Input_AccountTblInvalid
		public const string Mst_PaymentType_CreateMulti_InvalidPaymentType = "ErridnInventory.Mst_PaymentType_CreateMulti_InvalidPaymentType"; //// //Mst_PaymentType_CreateMulti_InvalidPaymentType
		public const string Mst_PaymentType_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_PaymentType_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_PaymentType_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_PaymentType_CreateMulti_InvalidPaymentTypeName = "ErridnInventory.Mst_PaymentType_CreateMulti_InvalidPaymentTypeName"; //// //Mst_PaymentType_CreateMulti_InvalidPaymentTypeName
		public const string Mst_PaymentType_CreateMulti_InvalidValue = "ErridnInventory.Mst_PaymentType_CreateMulti_InvalidValue"; //// //Mst_PaymentType_CreateMulti_InvalidValue

		// Mst_PaymentType_Update:
		public const string Mst_PaymentType_Update = "ErridnInventory.Mst_PaymentType_Update"; //// //Mst_PaymentType_Update
		public const string Mst_PaymentType_Update_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_PaymentType_Update_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_PaymentType_Update_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_PaymentType_Update_InvalidPaymentTypeName = "ErridnInventory.Mst_PaymentType_Update_InvalidPaymentTypeName"; //// //Mst_PaymentType_Update_InvalidPaymentTypeName
		public const string Mst_PaymentType_Update_InvalidActiveDateStartBeforeActiveDateStartChild = "ErridnInventory.Mst_PaymentType_Update_InvalidActiveDateStartBeforeActiveDateStartChild"; //// //Mst_PaymentType_Update_InvalidActiveDateStartBeforeActiveDateStartChild
		public const string Mst_PaymentType_Update_ExistCostCenterChildActive = "ErridnInventory.Mst_PaymentType_Update_ExistCostCenterChildActive"; //// //Mst_PaymentType_Update_ExistCostCenterChildActive
		public const string Mst_PaymentType_Update_InvalidValue = "ErridnInventory.Mst_PaymentType_Update_InvalidValue"; //// //Mst_PaymentType_Update_InvalidValue

		// Mst_PaymentType_Delete:
		public const string Mst_PaymentType_Delete = "ErridnInventory.Mst_PaymentType_Delete"; //// //Mst_PaymentType_Delete

		// WAS_Mst_PaymentType_Get:
		public const string WAS_Mst_PaymentType_Get = "ErridnInventory.WAS_Mst_PaymentType_Get"; //// //WAS_Mst_PaymentType_Get

		// WAS_Mst_PaymentType_Create:
		public const string WAS_Mst_PaymentType_Create = "ErridnInventory.WAS_Mst_PaymentType_Create"; //// //WAS_Mst_PaymentType_Create

		// WAS_Mst_PaymentType_Update:
		public const string WAS_Mst_PaymentType_Update = "ErridnInventory.WAS_Mst_PaymentType_Update"; //// //WAS_Mst_PaymentType_Update

		// WAS_Mst_PaymentType_Delete:
		public const string WAS_Mst_PaymentType_Delete = "ErridnInventory.WAS_Mst_PaymentType_Delete"; //// //WAS_Mst_PaymentType_Delete
		#endregion

		#region // Mst_MoneyStock:
		// Mst_MoneyStock_CheckDB:
		public const string Mst_MoneyStock_CheckDB_MoneyStockNotFound = "ErridnInventory.Mst_MoneyStock_CheckDB_MoneyStockNotFound"; //// //Mst_MoneyStock_CheckDB_MoneyStockNotFound
		public const string Mst_MoneyStock_CheckDB_MoneyStockExist = "ErridnInventory.Mst_MoneyStock_CheckDB_MoneyStockExist"; //// //Mst_MoneyStock_CheckDB_MoneyStockExist
		public const string Mst_MoneyStock_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_MoneyStock_CheckDB_FlagActiveNotMatched"; //// //Mst_MoneyStock_CheckDB_FlagActiveNotMatched

		// Mst_MoneyStock_Get:
		public const string Mst_MoneyStock_Get = "ErridnInventory.Mst_MoneyStock_Get"; //// //Mst_MoneyStock_Get

		// Mst_MoneyStock_Create:
		public const string Mst_MoneyStock_Create = "ErridnInventory.Mst_MoneyStock_Create"; //// //Mst_MoneyStock_Create
		public const string Mst_MoneyStock_Create_InvalidMoneyStockCode = "ErridnInventory.Mst_MoneyStock_Create_InvalidMoneyStockCode"; //// //Mst_MoneyStock_Create_InvalidMoneyStockCode
        public const string Mst_MoneyStock_Create_InvalidPaymentPartnerType = "ErridnInventory.Mst_MoneyStock_Create_InvalidPaymentPartnerType"; //// //Mst_MoneyStock_Create_InvalidPaymentPartnerType
        public const string Mst_MoneyStock_Create_InvalidPaymentPartnerCode = "ErridnInventory.Mst_MoneyStock_Create_InvalidPaymentPartnerCode"; //// //Mst_MoneyStock_Create_InvalidPaymentPartnerCode
        public const string Mst_MoneyStock_Create_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_MoneyStock_Create_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_MoneyStock_Create_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_MoneyStock_Create_InvalidMoneyStockName = "ErridnInventory.Mst_MoneyStock_Create_InvalidMoneyStockName"; //// //Mst_MoneyStock_Create_InvalidMoneyStockName
		public const string Mst_MoneyStock_Create_InvalidValue = "ErridnInventory.Mst_MoneyStock_Create_InvalidValue"; //// //Mst_MoneyStock_Create_InvalidValue

		// Mst_MoneyStock_CreateMulti:
		public const string Mst_MoneyStock_CreateMulti = "ErridnInventory.Mst_MoneyStock_CreateMulti"; //// //Mst_MoneyStock_CreateMulti
		public const string Mst_MoneyStock_CreateMulti_Input_AccountTblNotFound = "ErridnInventory.Mst_MoneyStock_CreateMulti_Input_AccountTblNotFound"; //// //Mst_MoneyStock_CreateMulti_Input_AccountTblNotFound
		public const string Mst_MoneyStock_CreateMulti_Input_AccountTblInvalid = "ErridnInventory.Mst_MoneyStock_CreateMulti_Input_AccountTblInvalid"; //// //Mst_MoneyStock_CreateMulti_Input_AccountTblInvalid
		public const string Mst_MoneyStock_CreateMulti_InvalidMoneyStockCode = "ErridnInventory.Mst_MoneyStock_CreateMulti_InvalidMoneyStockCode"; //// //Mst_MoneyStock_CreateMulti_InvalidMoneyStockCode
		public const string Mst_MoneyStock_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_MoneyStock_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_MoneyStock_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_MoneyStock_CreateMulti_InvalidMoneyStockName = "ErridnInventory.Mst_MoneyStock_CreateMulti_InvalidMoneyStockName"; //// //Mst_MoneyStock_CreateMulti_InvalidMoneyStockName
		public const string Mst_MoneyStock_CreateMulti_InvalidValue = "ErridnInventory.Mst_MoneyStock_CreateMulti_InvalidValue"; //// //Mst_MoneyStock_CreateMulti_InvalidValue

		// Mst_MoneyStock_Update:
		public const string Mst_MoneyStock_Update = "ErridnInventory.Mst_MoneyStock_Update"; //// //Mst_MoneyStock_Update
		public const string Mst_MoneyStock_Update_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_MoneyStock_Update_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_MoneyStock_Update_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_MoneyStock_Update_InvalidMoneyStockName = "ErridnInventory.Mst_MoneyStock_Update_InvalidMoneyStockName"; //// //Mst_MoneyStock_Update_InvalidMoneyStockName
		public const string Mst_MoneyStock_Update_InvalidActiveDateStartBeforeActiveDateStartChild = "ErridnInventory.Mst_MoneyStock_Update_InvalidActiveDateStartBeforeActiveDateStartChild"; //// //Mst_MoneyStock_Update_InvalidActiveDateStartBeforeActiveDateStartChild
		public const string Mst_MoneyStock_Update_ExistCostCenterChildActive = "ErridnInventory.Mst_MoneyStock_Update_ExistCostCenterChildActive"; //// //Mst_MoneyStock_Update_ExistCostCenterChildActive
		public const string Mst_MoneyStock_Update_InvalidValue = "ErridnInventory.Mst_MoneyStock_Update_InvalidValue"; //// //Mst_MoneyStock_Update_InvalidValue

		// Mst_MoneyStock_Delete:
		public const string Mst_MoneyStock_Delete = "ErridnInventory.Mst_MoneyStock_Delete"; //// //Mst_MoneyStock_Delete

		// WAS_Mst_MoneyStock_Get:
		public const string WAS_Mst_MoneyStock_Get = "ErridnInventory.WAS_Mst_MoneyStock_Get"; //// //WAS_Mst_MoneyStock_Get

		// WAS_Mst_MoneyStock_Create:
		public const string WAS_Mst_MoneyStock_Create = "ErridnInventory.WAS_Mst_MoneyStock_Create"; //// //WAS_Mst_MoneyStock_Create

		// WAS_Mst_MoneyStock_Update:
		public const string WAS_Mst_MoneyStock_Update = "ErridnInventory.WAS_Mst_MoneyStock_Update"; //// //WAS_Mst_MoneyStock_Update

		// WAS_Mst_MoneyStock_Delete:
		public const string WAS_Mst_MoneyStock_Delete = "ErridnInventory.WAS_Mst_MoneyStock_Delete"; //// //WAS_Mst_MoneyStock_Delete
		#endregion

		#region // Mst_CurrencyConvert:
		// Mst_CurrencyConvert_CheckDB:
		public const string Mst_CurrencyConvert_CheckDB_CurrencyConvertNotFound = "ErridnInventory.Mst_CurrencyConvert_CheckDB_CurrencyConvertNotFound"; //// //Mst_CurrencyConvert_CheckDB_CurrencyConvertNotFound
		public const string Mst_CurrencyConvert_CheckDB_CurrencyConvertExist = "ErridnInventory.Mst_CurrencyConvert_CheckDB_CurrencyConvertExist"; //// //Mst_CurrencyConvert_CheckDB_CurrencyConvertExist
		public const string Mst_CurrencyConvert_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CurrencyConvert_CheckDB_FlagActiveNotMatched"; //// //Mst_CurrencyConvert_CheckDB_FlagActiveNotMatched

		// Mst_CurrencyConvert_Get:
		public const string Mst_CurrencyConvert_Get = "ErridnInventory.Mst_CurrencyConvert_Get"; //// //Mst_CurrencyConvert_Get

		// Mst_CurrencyConvert_Create:
		public const string Mst_CurrencyConvert_Create = "ErridnInventory.Mst_CurrencyConvert_Create"; //// //Mst_CurrencyConvert_Create
		public const string Mst_CurrencyConvert_Create_InvalidMoneyStockCode = "ErridnInventory.Mst_CurrencyConvert_Create_InvalidMoneyStockCode"; //// //Mst_CurrencyConvert_Create_InvalidMoneyStockCode
		public const string Mst_CurrencyConvert_Create_InvalidPaymentPartnerType = "ErridnInventory.Mst_CurrencyConvert_Create_InvalidPaymentPartnerType"; //// //Mst_CurrencyConvert_Create_InvalidPaymentPartnerType
		public const string Mst_CurrencyConvert_Create_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_CurrencyConvert_Create_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_CurrencyConvert_Create_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_CurrencyConvert_Create_InvalidMoneyStockName = "ErridnInventory.Mst_CurrencyConvert_Create_InvalidMoneyStockName"; //// //Mst_CurrencyConvert_Create_InvalidMoneyStockName
		public const string Mst_CurrencyConvert_Create_InvalidValue = "ErridnInventory.Mst_CurrencyConvert_Create_InvalidValue"; //// //Mst_CurrencyConvert_Create_InvalidValue

		// Mst_CurrencyConvert_CreateMulti:
		public const string Mst_CurrencyConvert_CreateMulti = "ErridnInventory.Mst_CurrencyConvert_CreateMulti"; //// //Mst_CurrencyConvert_CreateMulti
		public const string Mst_CurrencyConvert_CreateMulti_Input_AccountTblNotFound = "ErridnInventory.Mst_CurrencyConvert_CreateMulti_Input_AccountTblNotFound"; //// //Mst_CurrencyConvert_CreateMulti_Input_AccountTblNotFound
		public const string Mst_CurrencyConvert_CreateMulti_Input_AccountTblInvalid = "ErridnInventory.Mst_CurrencyConvert_CreateMulti_Input_AccountTblInvalid"; //// //Mst_CurrencyConvert_CreateMulti_Input_AccountTblInvalid
		public const string Mst_CurrencyConvert_CreateMulti_InvalidMoneyStockCode = "ErridnInventory.Mst_CurrencyConvert_CreateMulti_InvalidMoneyStockCode"; //// //Mst_CurrencyConvert_CreateMulti_InvalidMoneyStockCode
		public const string Mst_CurrencyConvert_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_CurrencyConvert_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_CurrencyConvert_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_CurrencyConvert_CreateMulti_InvalidMoneyStockName = "ErridnInventory.Mst_CurrencyConvert_CreateMulti_InvalidMoneyStockName"; //// //Mst_CurrencyConvert_CreateMulti_InvalidMoneyStockName
		public const string Mst_CurrencyConvert_CreateMulti_InvalidValue = "ErridnInventory.Mst_CurrencyConvert_CreateMulti_InvalidValue"; //// //Mst_CurrencyConvert_CreateMulti_InvalidValue

		// Mst_CurrencyConvert_Update:
		public const string Mst_CurrencyConvert_Update = "ErridnInventory.Mst_CurrencyConvert_Update"; //// //Mst_CurrencyConvert_Update
		public const string Mst_CurrencyConvert_Update_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_CurrencyConvert_Update_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_CurrencyConvert_Update_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_CurrencyConvert_Update_InvalidMoneyStockName = "ErridnInventory.Mst_CurrencyConvert_Update_InvalidMoneyStockName"; //// //Mst_CurrencyConvert_Update_InvalidMoneyStockName
		public const string Mst_CurrencyConvert_Update_InvalidActiveDateStartBeforeActiveDateStartChild = "ErridnInventory.Mst_CurrencyConvert_Update_InvalidActiveDateStartBeforeActiveDateStartChild"; //// //Mst_CurrencyConvert_Update_InvalidActiveDateStartBeforeActiveDateStartChild
		public const string Mst_CurrencyConvert_Update_ExistCostCenterChildActive = "ErridnInventory.Mst_CurrencyConvert_Update_ExistCostCenterChildActive"; //// //Mst_CurrencyConvert_Update_ExistCostCenterChildActive
		public const string Mst_CurrencyConvert_Update_InvalidValue = "ErridnInventory.Mst_CurrencyConvert_Update_InvalidValue"; //// //Mst_CurrencyConvert_Update_InvalidValue

		// Mst_CurrencyConvert_Delete:
		public const string Mst_CurrencyConvert_Delete = "ErridnInventory.Mst_CurrencyConvert_Delete"; //// //Mst_CurrencyConvert_Delete

		// WAS_Mst_CurrencyConvert_Get:
		public const string WAS_Mst_CurrencyConvert_Get = "ErridnInventory.WAS_Mst_CurrencyConvert_Get"; //// //WAS_Mst_CurrencyConvert_Get

		// WAS_Mst_CurrencyConvert_Create:
		public const string WAS_Mst_CurrencyConvert_Create = "ErridnInventory.WAS_Mst_CurrencyConvert_Create"; //// //WAS_Mst_CurrencyConvert_Create

		// WAS_Mst_CurrencyConvert_Update:
		public const string WAS_Mst_CurrencyConvert_Update = "ErridnInventory.WAS_Mst_CurrencyConvert_Update"; //// //WAS_Mst_CurrencyConvert_Update

		// WAS_Mst_CurrencyConvert_Delete:
		public const string WAS_Mst_CurrencyConvert_Delete = "ErridnInventory.WAS_Mst_CurrencyConvert_Delete"; //// //WAS_Mst_CurrencyConvert_Delete
		#endregion

		#region // Map_UserInOrgan:
		// Map_UserInOrgan_Get:
		public const string Map_UserInOrgan_Get = "ErridnInventory.Map_UserInOrgan_Get"; //// //Map_UserInOrgan_Get

		// Map_UserInOrgan_Save:
		public const string Map_UserInOrgan_Save = "ErridnInventory.Map_UserInOrgan_Save"; //// //Map_UserInOrgan_Save
		public const string Map_UserInOrgan_Save_InputTblDtlNotFound = "ErridnInventory.Map_UserInOrgan_Save_InputTblDtlNotFound"; //// //Map_UserInOrgan_Save_InputTblDtlNotFound

		// WAS_Map_UserInOrgan_Get:
		public const string WAS_Map_UserInOrgan_Get = "ErridnInventory.WAS_Map_UserInOrgan_Get"; //// //WAS_Map_UserInOrgan_Get

		// WAS_Map_UserInOrgan_Save:
		public const string WAS_Map_UserInOrgan_Save = "ErridnInventory.WAS_Map_UserInOrgan_Save"; //// //WAS_Map_UserInOrgan_Save

		#endregion

		#region // Mst_AssetType:
		// Mst_AssetType_CheckDB:
		public const string Mst_AssetType_CheckDB_AssetTypeNotFound = "ErridnInventory.Mst_AssetType_CheckDB_AssetTypeNotFound"; //// //Mst_AssetType_CheckDB_AssetTypeNotFound
		public const string Mst_AssetType_CheckDB_AssetTypeExist = "ErridnInventory.Mst_AssetType_CheckDB_AssetTypeExist"; //// //Mst_AssetType_CheckDB_AssetTypeExist
		public const string Mst_AssetType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_AssetType_CheckDB_FlagActiveNotMatched"; //// //Mst_AssetType_CheckDB_FlagActiveNotMatched

		// Mst_AssetType_Get:
		public const string Mst_AssetType_Get = "ErridnInventory.Mst_AssetType_Get"; //// //Mst_AssetType_Get

		// WAS_Mst_AssetType_Get:
		public const string WAS_Mst_AssetType_Get = "ErridnInventory.WAS_Mst_AssetType_Get"; //// //WAS_Mst_AssetType_Get
		#endregion

		#region // Mst_CreditType:
		// Mst_CreditType_CheckDB:
		public const string Mst_CreditType_CheckDB_CreditTypeNotFound = "ErridnInventory.Mst_CreditType_CheckDB_CreditTypeNotFound"; //// //Mst_CreditType_CheckDB_CreditTypeNotFound
		public const string Mst_CreditType_CheckDB_CreditTypeExist = "ErridnInventory.Mst_CreditType_CheckDB_CreditTypeExist"; //// //Mst_CreditType_CheckDB_CreditTypeExist
		public const string Mst_CreditType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CreditType_CheckDB_FlagActiveNotMatched"; //// //Mst_CreditType_CheckDB_FlagActiveNotMatched

		// Mst_CreditType_Get:
		public const string Mst_CreditType_Get = "ErridnInventory.Mst_CreditType_Get"; //// //Mst_CreditType_Get

		// WAS_Mst_CreditType_Get:
		public const string WAS_Mst_CreditType_Get = "ErridnInventory.WAS_Mst_CreditType_Get"; //// //WAS_Mst_CreditType_Get
		#endregion

		#region // Mst_CreditContractType:
		// Mst_CreditContractType_CheckDB:
		public const string Mst_CreditContractType_CheckDB_CreditContractTypeNotFound = "ErridnInventory.Mst_CreditContractType_CheckDB_CreditContractTypeNotFound"; //// //Mst_CreditContractType_CheckDB_CreditContractTypeNotFound
		public const string Mst_CreditContractType_CheckDB_CreditContractTypeExist = "ErridnInventory.Mst_CreditContractType_CheckDB_CreditContractTypeExist"; //// //Mst_CreditContractType_CheckDB_CreditContractTypeExist
		public const string Mst_CreditContractType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CreditContractType_CheckDB_FlagActiveNotMatched"; //// //Mst_CreditContractType_CheckDB_FlagActiveNotMatched

		// Mst_CreditContractType_Get:
		public const string Mst_CreditContractType_Get = "ErridnInventory.Mst_CreditContractType_Get"; //// //Mst_CreditContractType_Get

		// WAS_Mst_CreditContractType_Get:
		public const string WAS_Mst_CreditContractType_Get = "ErridnInventory.WAS_Mst_CreditContractType_Get"; //// //WAS_Mst_CreditContractType_Get
		#endregion

		#region // Mst_LoanPurpose:
		// Mst_LoanPurpose_CheckDB:
		public const string Mst_LoanPurpose_CheckDB_LoanPurposeNotFound = "ErridnInventory.Mst_LoanPurpose_CheckDB_LoanPurposeNotFound"; //// //Mst_LoanPurpose_CheckDB_LoanPurposeNotFound
		public const string Mst_LoanPurpose_CheckDB_LoanPurposeExist = "ErridnInventory.Mst_LoanPurpose_CheckDB_LoanPurposeExist"; //// //Mst_LoanPurpose_CheckDB_LoanPurposeExist
		public const string Mst_LoanPurpose_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_LoanPurpose_CheckDB_FlagActiveNotMatched"; //// //Mst_LoanPurpose_CheckDB_FlagActiveNotMatched

		// Mst_LoanPurpose_Get:
		public const string Mst_LoanPurpose_Get = "ErridnInventory.Mst_LoanPurpose_Get"; //// //Mst_LoanPurpose_Get

		// WAS_Mst_LoanPurpose_Get:
		public const string WAS_Mst_LoanPurpose_Get = "ErridnInventory.WAS_Mst_LoanPurpose_Get"; //// //WAS_Mst_LoanPurpose_Get
		#endregion

		#region // Mst_SourceOfCapital:
		// Mst_SourceOfCapital_CheckDB:
		public const string Mst_SourceOfCapital_CheckDB_SourceOfCapitalNotFound = "ErridnInventory.Mst_SourceOfCapital_CheckDB_SourceOfCapitalNotFound"; //// //Mst_SourceOfCapital_CheckDB_SourceOfCapitalNotFound
		public const string Mst_SourceOfCapital_CheckDB_SourceOfCapitalExist = "ErridnInventory.Mst_SourceOfCapital_CheckDB_SourceOfCapitalExist"; //// //Mst_SourceOfCapital_CheckDB_SourceOfCapitalExist
		public const string Mst_SourceOfCapital_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_SourceOfCapital_CheckDB_FlagActiveNotMatched"; //// //Mst_SourceOfCapital_CheckDB_FlagActiveNotMatched

		// Mst_SourceOfCapital_Get:
		public const string Mst_SourceOfCapital_Get = "ErridnInventory.Mst_SourceOfCapital_Get"; //// //Mst_SourceOfCapital_Get

		// WAS_Mst_SourceOfCapital_Get:
		public const string WAS_Mst_SourceOfCapital_Get = "ErridnInventory.WAS_Mst_SourceOfCapital_Get"; //// //WAS_Mst_SourceOfCapital_Get
		#endregion

		#region // Mst_BizRef:
		// Mst_BizRef_CheckDB:
		public const string Mst_BizRef_CheckDB_BizRefNotFound = "ErridnInventory.Mst_BizRef_CheckDB_BizRefNotFound"; //// //Mst_BizRef_CheckDB_BizRefNotFound
		public const string Mst_BizRef_CheckDB_BizRefExist = "ErridnInventory.Mst_BizRef_CheckDB_BizRefExist"; //// //Mst_BizRef_CheckDB_BizRefExist
		public const string Mst_BizRef_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_BizRef_CheckDB_FlagActiveNotMatched"; //// //Mst_BizRef_CheckDB_FlagActiveNotMatched

		// Mst_BizRef_Get:
		public const string Mst_BizRef_Get = "ErridnInventory.Mst_BizRef_Get"; //// //Mst_BizRef_Get

		// WAS_Mst_BizRef_Get:
		public const string WAS_Mst_BizRef_Get = "ErridnInventory.WAS_Mst_BizRef_Get"; //// //WAS_Mst_BizRef_Get
		#endregion

		#region // Mst_PlanPaymentType:
		// Mst_PlanPaymentType_CheckDB:
		public const string Mst_PlanPaymentType_CheckDB_PlanPaymentTypeNotFound = "ErridnInventory.Mst_PlanPaymentType_CheckDB_PlanPaymentTypeNotFound"; //// //Mst_PlanPaymentType_CheckDB_PlanPaymentTypeNotFound
		public const string Mst_PlanPaymentType_CheckDB_PlanPaymentTypeExist = "ErridnInventory.Mst_PlanPaymentType_CheckDB_PlanPaymentTypeExist"; //// //Mst_PlanPaymentType_CheckDB_PlanPaymentTypeExist
		public const string Mst_PlanPaymentType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PlanPaymentType_CheckDB_FlagActiveNotMatched"; //// //Mst_PlanPaymentType_CheckDB_FlagActiveNotMatched

		// Mst_PlanPaymentType_Get:
		public const string Mst_PlanPaymentType_Get = "ErridnInventory.Mst_PlanPaymentType_Get"; //// //Mst_PlanPaymentType_Get

		// Mst_PlanPaymentType_Create:
		public const string Mst_PlanPaymentType_Create = "ErridnInventory.Mst_PlanPaymentType_Create"; //// //Mst_PlanPaymentType_Create
		public const string Mst_PlanPaymentType_Create_InvalidPlanPaymentType = "ErridnInventory.Mst_PlanPaymentType_Create_InvalidPlanPaymentType"; //// //Mst_PlanPaymentType_Create_InvalidPlanPaymentType
		public const string Mst_PlanPaymentType_Create_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_PlanPaymentType_Create_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_PlanPaymentType_Create_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_PlanPaymentType_Create_InvalidPlanPaymentTypeName = "ErridnInventory.Mst_PlanPaymentType_Create_InvalidPlanPaymentTypeName"; //// //Mst_PlanPaymentType_Create_InvalidPlanPaymentTypeName
		public const string Mst_PlanPaymentType_Create_InvalidValue = "ErridnInventory.Mst_PlanPaymentType_Create_InvalidValue"; //// //Mst_PlanPaymentType_Create_InvalidValue

		// Mst_PlanPaymentType_CreateMulti:
		public const string Mst_PlanPaymentType_CreateMulti = "ErridnInventory.Mst_PlanPaymentType_CreateMulti"; //// //Mst_PlanPaymentType_CreateMulti
		public const string Mst_PlanPaymentType_CreateMulti_Input_AccountTblNotFound = "ErridnInventory.Mst_PlanPaymentType_CreateMulti_Input_AccountTblNotFound"; //// //Mst_PlanPaymentType_CreateMulti_Input_AccountTblNotFound
		public const string Mst_PlanPaymentType_CreateMulti_Input_AccountTblInvalid = "ErridnInventory.Mst_PlanPaymentType_CreateMulti_Input_AccountTblInvalid"; //// //Mst_PlanPaymentType_CreateMulti_Input_AccountTblInvalid
		public const string Mst_PlanPaymentType_CreateMulti_InvalidPlanPaymentType = "ErridnInventory.Mst_PlanPaymentType_CreateMulti_InvalidPlanPaymentType"; //// //Mst_PlanPaymentType_CreateMulti_InvalidPlanPaymentType
		public const string Mst_PlanPaymentType_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_PlanPaymentType_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_PlanPaymentType_CreateMulti_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_PlanPaymentType_CreateMulti_InvalidPlanPaymentTypeName = "ErridnInventory.Mst_PlanPaymentType_CreateMulti_InvalidPlanPaymentTypeName"; //// //Mst_PlanPaymentType_CreateMulti_InvalidPlanPaymentTypeName
		public const string Mst_PlanPaymentType_CreateMulti_InvalidValue = "ErridnInventory.Mst_PlanPaymentType_CreateMulti_InvalidValue"; //// //Mst_PlanPaymentType_CreateMulti_InvalidValue

		// Mst_PlanPaymentType_Update:
		public const string Mst_PlanPaymentType_Update = "ErridnInventory.Mst_PlanPaymentType_Update"; //// //Mst_PlanPaymentType_Update
		public const string Mst_PlanPaymentType_Update_InvalidActiveDateStartAfterActiveDateStartParent = "ErridnInventory.Mst_PlanPaymentType_Update_InvalidActiveDateStartAfterActiveDateStartParent"; //// //Mst_PlanPaymentType_Update_InvalidActiveDateStartAfterActiveDateStartParent
		public const string Mst_PlanPaymentType_Update_InvalidPlanPaymentTypeName = "ErridnInventory.Mst_PlanPaymentType_Update_InvalidPlanPaymentTypeName"; //// //Mst_PlanPaymentType_Update_InvalidPlanPaymentTypeName
		public const string Mst_PlanPaymentType_Update_InvalidActiveDateStartBeforeActiveDateStartChild = "ErridnInventory.Mst_PlanPaymentType_Update_InvalidActiveDateStartBeforeActiveDateStartChild"; //// //Mst_PlanPaymentType_Update_InvalidActiveDateStartBeforeActiveDateStartChild
		public const string Mst_PlanPaymentType_Update_ExistCostCenterChildActive = "ErridnInventory.Mst_PlanPaymentType_Update_ExistCostCenterChildActive"; //// //Mst_PlanPaymentType_Update_ExistCostCenterChildActive
		public const string Mst_PlanPaymentType_Update_InvalidValue = "ErridnInventory.Mst_PlanPaymentType_Update_InvalidValue"; //// //Mst_PlanPaymentType_Update_InvalidValue

		// Mst_PlanPaymentType_Delete:
		public const string Mst_PlanPaymentType_Delete = "ErridnInventory.Mst_PlanPaymentType_Delete"; //// //Mst_PlanPaymentType_Delete

		// WAS_Mst_PlanPaymentType_Get:
		public const string WAS_Mst_PlanPaymentType_Get = "ErridnInventory.WAS_Mst_PlanPaymentType_Get"; //// //WAS_Mst_PlanPaymentType_Get

		// WAS_Mst_PlanPaymentType_Create:
		public const string WAS_Mst_PlanPaymentType_Create = "ErridnInventory.WAS_Mst_PlanPaymentType_Create"; //// //WAS_Mst_PlanPaymentType_Create

		// WAS_Mst_PlanPaymentType_Update:
		public const string WAS_Mst_PlanPaymentType_Update = "ErridnInventory.WAS_Mst_PlanPaymentType_Update"; //// //WAS_Mst_PlanPaymentType_Update

		// WAS_Mst_PlanPaymentType_Delete:
		public const string WAS_Mst_PlanPaymentType_Delete = "ErridnInventory.WAS_Mst_PlanPaymentType_Delete"; //// //WAS_Mst_PlanPaymentType_Delete
		#endregion

		#region // Mst_PaymentPurpose:
		// Mst_PaymentPurpose_CheckDB:
		public const string Mst_PaymentPurpose_CheckDB_PaymentPurposeNotFound = "ErridnInventory.Mst_PaymentPurpose_CheckDB_PaymentPurposeNotFound"; //// //Mst_PaymentPurpose_CheckDB_PaymentPurposeNotFound
		public const string Mst_PaymentPurpose_CheckDB_PaymentPurposeExist = "ErridnInventory.Mst_PaymentPurpose_CheckDB_PaymentPurposeExist"; //// //Mst_PaymentPurpose_CheckDB_PaymentPurposeExist
		public const string Mst_PaymentPurpose_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_PaymentPurpose_CheckDB_FlagActiveNotMatched"; //// //Mst_PaymentPurpose_CheckDB_FlagActiveNotMatched

		// Mst_PaymentPurpose_Get:
		public const string Mst_PaymentPurpose_Get = "ErridnInventory.Mst_PaymentPurpose_Get"; //// //Mst_PaymentPurpose_Get

		// WAS_Mst_PaymentPurpose_Get:
		public const string WAS_Mst_PaymentPurpose_Get = "ErridnInventory.WAS_Mst_PaymentPurpose_Get"; //// //WAS_Mst_PaymentPurpose_Get
		#endregion

		#region // Mst_Fund:
		// Mst_Fund_CheckDB:
		public const string Mst_Fund_CheckDB_FundNotFound = "ErridnInventory.Mst_Fund_CheckDB_FundNotFound"; //// //Mst_Fund_CheckDB_FundNotFound
		public const string Mst_Fund_CheckDB_FundExist = "ErridnInventory.Mst_Fund_CheckDB_FundExist"; //// //Mst_Fund_CheckDB_FundExist
		public const string Mst_Fund_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Fund_CheckDB_FlagActiveNotMatched"; //// //Mst_Fund_CheckDB_FlagActiveNotMatched

		// Mst_Fund_Get:
		public const string Mst_Fund_Get = "ErridnInventory.Mst_Fund_Get"; //// //Mst_Fund_Get

		// WAS_Mst_Fund_Get:
		public const string WAS_Mst_Fund_Get = "ErridnInventory.WAS_Mst_Fund_Get"; //// //WAS_Mst_Fund_Get
		#endregion

		#region // Mst_LoanDurationType:
		// Mst_LoanDurationType_CheckDB:
		public const string Mst_LoanDurationType_CheckDB_LDRTypeNotFound = "ErridnInventory.Mst_LoanDurationType_CheckDB_LDRTypeNotFound"; //// //Mst_LoanDurationType_CheckDB_LDRTypeNotFound
		public const string Mst_LoanDurationType_CheckDB_LDRTypeExist = "ErridnInventory.Mst_LoanDurationType_CheckDB_LDRTypeExist"; //// //Mst_LoanDurationType_CheckDB_LDRTypeExist
		public const string Mst_LoanDurationType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_LoanDurationType_CheckDB_FlagActiveNotMatched"; //// //Mst_LoanDurationType_CheckDB_FlagActiveNotMatched

		// Mst_LoanDurationType_Get:
		public const string Mst_LoanDurationType_Get = "ErridnInventory.Mst_LoanDurationType_Get"; //// //Mst_LoanDurationType_Get

		// WAS_Mst_LoanDurationType_Get:
		public const string WAS_Mst_LoanDurationType_Get = "ErridnInventory.WAS_Mst_LoanDurationType_Get"; //// //WAS_Mst_LoanDurationType_Get
		#endregion

		#region // Mst_ProjectPaymentType:
		// Mst_ProjectPaymentType_CheckDB:
		public const string Mst_ProjectPaymentType_CheckDB_PrjPmtTypeNotFound = "ErridnInventory.Mst_ProjectPaymentType_CheckDB_PrjPmtTypeNotFound"; //// //Mst_ProjectPaymentType_CheckDB_PrjPmtTypeNotFound
		public const string Mst_ProjectPaymentType_CheckDB_PrjPmtTypeExist = "ErridnInventory.Mst_ProjectPaymentType_CheckDB_PrjPmtTypeExist"; //// //Mst_ProjectPaymentType_CheckDB_PrjPmtTypeExist
		public const string Mst_ProjectPaymentType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_ProjectPaymentType_CheckDB_FlagActiveNotMatched"; //// //Mst_ProjectPaymentType_CheckDB_FlagActiveNotMatched

		// Mst_ProjectPaymentType_Get:
		public const string Mst_ProjectPaymentType_Get = "ErridnInventory.Mst_ProjectPaymentType_Get"; //// //Mst_ProjectPaymentType_Get

		// WAS_Mst_ProjectPaymentType_Get:
		public const string WAS_Mst_ProjectPaymentType_Get = "ErridnInventory.WAS_Mst_ProjectPaymentType_Get"; //// //WAS_Mst_ProjectPaymentType_Get
		#endregion

		#region // Mst_LoanDuration:
		// Mst_LoanDuration_CheckDB:
		public const string Mst_LoanDuration_CheckDB_LoanDurationNotFound = "ErridnInventory.Mst_LoanDuration_CheckDB_LoanDurationNotFound"; //// //Mst_LoanDuration_CheckDB_LoanDurationNotFound
		public const string Mst_LoanDuration_CheckDB_LoanDurationExist = "ErridnInventory.Mst_LoanDuration_CheckDB_LoanDurationExist"; //// //Mst_LoanDuration_CheckDB_LoanDurationExist
		public const string Mst_LoanDuration_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_LoanDuration_CheckDB_FlagActiveNotMatched"; //// //Mst_LoanDuration_CheckDB_FlagActiveNotMatched

		// Mst_LoanDuration_Get:
		public const string Mst_LoanDuration_Get = "ErridnInventory.Mst_LoanDuration_Get"; //// //Mst_LoanDuration_Get

		// WAS_Mst_LoanDuration_Get:
		public const string WAS_Mst_LoanDuration_Get = "ErridnInventory.WAS_Mst_LoanDuration_Get"; //// //WAS_Mst_LoanDuration_Get
        #endregion

        #region // Mst_Province:
        // Mst_Province_CheckDB:
        public const string Mst_Province_CheckDB_ProvinceNotFound = "ErridnInventory.Mst_Province_CheckDB_ProvinceNotFound"; //// //Mst_Province_CheckDB_ProvinceNotFound
        public const string Mst_Province_CheckDB_ProvinceExist = "ErridnInventory.Mst_Province_CheckDB_ProvinceExist"; //// //Mst_Province_CheckDB_ProvinceExist
        public const string Mst_Province_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Province_CheckDB_FlagActiveNotMatched"; //// //Mst_Province_CheckDB_FlagActiveNotMatched

        // RptSv_Mst_Province_Get:
        public const string RptSv_Mst_Province_Get = "ErridnInventory.RptSv_Mst_Province_Get"; //// //RptSv_Mst_Province_Get

        // WAS_RptSv_Mst_Province_Get:
        public const string WAS_RptSv_Mst_Province_Get = "ErridnInventory.WAS_RptSv_Mst_Province_Get"; //// //WAS_RptSv_Mst_Province_Get

        // Mst_Province_Get:
        public const string Mst_Province_Get = "ErridnInventory.Mst_Province_Get"; //// //Mst_Province_Get

        // WAS_Mst_Province_Get:
        public const string WAS_Mst_Province_Get = "ErridnInventory.WAS_Mst_Province_Get"; //// //WAS_Mst_Province_Get

        // Mst_Province_Create:
        public const string Mst_Province_Create = "ErridnInventory.Mst_Province_Create"; //// //Mst_Province_Create
        public const string Mst_Province_Create_InvalidProvinceCode = "ErridnInventory.Mst_Province_Create_InvalidProvinceCode"; //// //Mst_Province_Create_InvalidProvinceCode
        public const string Mst_Province_Create_InvalidProvinceName = "ErridnInventory.Mst_Province_Create_InvalidProvinceName"; //// //Mst_Province_Create_InvalidProvinceName        

        // WAS_Mst_Province_Create:
        public const string WAS_Mst_Province_Create = "ErridnInventory.WAS_Mst_Province_Create"; //// //WAS_Mst_Province_Create

        // Mst_Province_Update:
        public const string Mst_Province_Update = "ErridnInventory.Mst_Province_Update"; //// //Mst_Province_Update                
        public const string Mst_Province_Update_InvalidProvinceName = "ErridnInventory.Mst_Province_Update_InvalidProvinceName"; //// //Mst_Province_Update_InvalidProvinceName                       

        // WAS_Mst_Province_Update:
        public const string WAS_Mst_Province_Update = "ErridnInventory.WAS_Mst_Province_Update"; //// //WAS_Mst_Province_Update

        // Mst_Province_Delete:
        public const string Mst_Province_Delete = "ErridnInventory.Mst_Province_Delete"; //// Mã lỗi: Mst_Province_Delete ////

        // WAS_Mst_Province_Delete:
        public const string WAS_Mst_Province_Delete = "ErridnInventory.WAS_Mst_Province_Delete"; //// //WAS_Mst_Province_Delete

        //RptSv:
        public const string WAS_RptSv_Mst_Province_Create = "ErridnInventory.WAS_RptSv_Mst_Province_Create"; //// //WAS_RptSv_Mst_Province_Create
        public const string WAS_RptSv_Mst_Province_Update = "ErridnInventory.WAS_RptSv_Mst_Province_Update"; //// //WAS_RptSv_Mst_Province_Update
        public const string WAS_RptSv_Mst_Province_Delete = "ErridnInventory.WAS_RptSv_Mst_Province_Delete"; //// //WAS_RptSv_Mst_Province_Delete

        #endregion

        #region // Mst_District:
        // Mst_District_CheckDB:
        public const string Mst_District_CheckDB_DistrictNotFound = "ErridnInventory.Mst_District_CheckDB_DistrictNotFound"; //// //Mst_District_CheckDB_ProvinceNotFound
        public const string Mst_District_CheckDB_DistrictExist = "ErridnInventory.Mst_District_CheckDB_DistrictExist"; //// //Mst_District_CheckDB_ProvinceExist
        public const string Mst_District_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_District_CheckDB_FlagActiveNotMatched"; //// //Mst_District_CheckDB_FlagActiveNotMatched

        // RptSv_Mst_District_Get:
        public const string RptSv_Mst_District_Get = "ErridnInventory.RptSv_Mst_District_Get"; //// //RptSv_Mst_District_Get

        // WAS_RptSv_Mst_District_Get:
        public const string WAS_RptSv_Mst_District_Get = "ErridnInventory.WAS_RptSv_Mst_District_Get"; //// //WAS_RptSv_Mst_District_Get

        // Mst_District_Get:
        public const string Mst_District_Get = "ErridnInventory.Mst_District_Get"; //// //Mst_District_Get

        // WAS_Mst_District_Get:
        public const string WAS_Mst_District_Get = "ErridnInventory.WAS_Mst_District_Get"; //// //WAS_Mst_District_Get

        // Mst_District_Create:
        public const string Mst_District_Create = "ErridnInventory.Mst_District_Create"; //// //Mst_District_Create
        public const string Mst_District_Create_InvalidDistrictCode = "ErridnInventory.Mst_District_Create_InvalidDistrictCode"; //// //Mst_District_Create_InvalidDistrictCode
        public const string Mst_District_Create_InvalidDistrictName = "ErridnInventory.Mst_District_Create_InvalidDistrictName"; //// //Mst_District_Create_InvalidDistrictName        

        // WAS_Mst_District_Create:
        public const string WAS_Mst_District_Create = "ErridnInventory.WAS_Mst_District_Create"; //// //WAS_Mst_District_Create

        // Mst_District_Update:
        public const string Mst_District_Update = "ErridnInventory.Mst_District_Update"; //// //Mst_District_Update                
        public const string Mst_District_Update_InvalidDistrictName = "ErridnInventory.Mst_District_Update_InvalidDistrictName"; //// //Mst_District_Update_InvalidDistrictName                       

        // WAS_Mst_District_Update:
        public const string WAS_Mst_District_Update = "ErridnInventory.WAS_Mst_District_Update"; //// //WAS_Mst_District_Update

        // Mst_District_Delete:
        public const string Mst_District_Delete = "ErridnInventory.Mst_District_Delete"; //// Mã lỗi: Mst_District_Delete ////

        // WAS_Mst_District_Delete:
        public const string WAS_Mst_District_Delete = "ErridnInventory.WAS_Mst_District_Delete"; //// //WAS_Mst_District_Delete

        //RptSv:
        public const string WAS_RptSv_Mst_District_Create = "ErridnInventory.WAS_RptSv_Mst_District_Create"; //// //WAS_RptSv_Mst_District_Create
        public const string WAS_RptSv_Mst_District_Update = "ErridnInventory.WAS_RptSv_Mst_District_Update"; //// //WAS_RptSv_Mst_District_Update
        public const string WAS_RptSv_Mst_District_Delete = "ErridnInventory.WAS_RptSv_Mst_District_Delete"; //// //WAS_RptSv_Mst_District_Delete

        #endregion

        #region // Mst_Country:
        // Mst_Country_CheckDB:
        public const string Mst_Country_CheckDB_CountryNotFound = "ErridnInventory.Mst_Country_CheckDB_CountryNotFound"; //// //Mst_Country_CheckDB_CountryNotFound
        public const string Mst_Country_CheckDB_CountryExist = "ErridnInventory.Mst_Country_CheckDB_CountryExist"; //// //Mst_Country_CheckDB_CountryExist
        public const string Mst_Country_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Country_CheckDB_FlagActiveNotMatched"; //// //Mst_Country_CheckDB_FlagActiveNotMatched

        // Mst_Country_Get:
        public const string Mst_Country_Get = "ErridnInventory.Mst_Country_Get"; //// //Mst_Country_Get

        // WAS_Mst_Country_Get:
        public const string WAS_Mst_Country_Get = "ErridnInventory.WAS_Mst_Country_Get"; //// //WAS_Mst_Country_Get

        // Mst_Country_Create:
        public const string Mst_Country_Create = "ErridnInventory.Mst_Country_Create"; //// //Mst_Country_Create
        public const string Mst_Country_Create_InvalidCountryCode = "ErridnInventory.Mst_Country_Create_InvalidCountryCode"; //// //Mst_Country_Create_InvalidCountryCode
        public const string Mst_Country_Create_InvalidCountryName = "ErridnInventory.Mst_Country_Create_InvalidCountryName"; //// //Mst_Country_Create_InvalidCountryName        

        // WAS_Mst_Country_Create:
        public const string WAS_Mst_Country_Create = "ErridnInventory.WAS_Mst_Country_Create"; //// //WAS_Mst_Country_Create

        // Mst_Country_Update:
        public const string Mst_Country_Update = "ErridnInventory.Mst_Country_Update"; //// //Mst_Country_Update                
        public const string Mst_Country_Update_InvalidCountryName = "ErridnInventory.Mst_Country_Update_InvalidCountryName"; //// //Mst_Country_Update_InvalidCountryName                       

        // WAS_Mst_Country_Update:
        public const string WAS_Mst_Country_Update = "ErridnInventory.WAS_Mst_Country_Update"; //// //WAS_Mst_Country_Update

        // Mst_Country_Delete:
        public const string Mst_Country_Delete = "ErridnInventory.Mst_Country_Delete"; //// Mã lỗi: Mst_Country_Delete ////

        // WAS_Mst_Country_Delete:
        public const string WAS_Mst_Country_Delete = "ErridnInventory.WAS_Mst_Country_Delete"; //// //WAS_Mst_Country_Delete
        #endregion

        #region // Mst_TaxType:
        // Mst_TaxType_CheckDB:
        public const string Mst_TaxType_CheckDB_TaxTypeNotFound = "ErridnInventory.Mst_TaxType_CheckDB_TaxTypeNotFound"; //// //Mst_TaxType_CheckDB_TaxTypeNotFound
        public const string Mst_TaxType_CheckDB_TaxTypeExist = "ErridnInventory.Mst_TaxType_CheckDB_TaxTypeExist"; //// //Mst_TaxType_CheckDB_TaxTypeExist
        public const string Mst_TaxType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_TaxType_CheckDB_FlagActiveNotMatched"; //// //Mst_TaxType_CheckDB_FlagActiveNotMatched

        // Mst_TaxType_Get:
        public const string Mst_TaxType_Get = "ErridnInventory.Mst_TaxType_Get"; //// //Mst_TaxType_Get

        // WAS_Mst_TaxType_Get:
        public const string WAS_Mst_TaxType_Get = "ErridnInventory.WAS_Mst_TaxType_Get"; //// //WAS_Mst_TaxType_Get

        // Mst_TaxType_Create:
        public const string Mst_TaxType_Create = "ErridnInventory.Mst_TaxType_Create"; //// //Mst_TaxType_Create
        public const string Mst_TaxType_Create_InvalidTaxType = "ErridnInventory.Mst_TaxType_Create_InvalidTaxType"; //// //Mst_TaxType_Create_InvalidTaxTypeCode
        public const string Mst_TaxType_Create_InvalidTaxTypeName = "ErridnInventory.Mst_TaxType_Create_InvalidTaxTypeName"; //// //Mst_TaxType_Create_InvalidTaxTypeName        

        // WAS_Mst_TaxType_Create:
        public const string WAS_Mst_TaxType_Create = "ErridnInventory.WAS_Mst_TaxType_Create"; //// //WAS_Mst_TaxType_Create

        // Mst_TaxType_Update:
        public const string Mst_TaxType_Update = "ErridnInventory.Mst_TaxType_Update"; //// //Mst_TaxType_Update                
        public const string Mst_TaxType_Update_InvalidTaxTypeName = "ErridnInventory.Mst_TaxType_Update_InvalidTaxTypeName"; //// //Mst_TaxType_Update_InvalidTaxTypeName                       

        // WAS_Mst_TaxType_Update:
        public const string WAS_Mst_TaxType_Update = "ErridnInventory.WAS_Mst_TaxType_Update"; //// //WAS_Mst_TaxType_Update

        // Mst_TaxType_Delete:
        public const string Mst_TaxType_Delete = "ErridnInventory.Mst_TaxType_Delete"; //// Mã lỗi: Mst_TaxType_Delete ////

        // WAS_Mst_TaxType_Delete:
        public const string WAS_Mst_TaxType_Delete = "ErridnInventory.WAS_Mst_TaxType_Delete"; //// //WAS_Mst_TaxType_Delete
        #endregion

        #region // Mst_Tax:
        // Mst_Tax_CheckDB:
        public const string Mst_Tax_CheckDB_TaxNotFound = "ErridnInventory.Mst_Tax_CheckDB_TaxNotFound"; //// //Mst_Tax_CheckDB_TaxNotFound
        public const string Mst_Tax_CheckDB_TaxExist = "ErridnInventory.Mst_Tax_CheckDB_TaxExist"; //// //Mst_Tax_CheckDB_TaxExist
        public const string Mst_Tax_CheckDB_FlagHasAppendixNotMatched = "ErridnInventory.Mst_Tax_CheckDB_FlagHasAppendixNotMatched"; //// //Mst_Tax_CheckDB_FlagHasAppendixNotMatched
        public const string Mst_Tax_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Tax_CheckDB_FlagActiveNotMatched"; //// //Mst_Tax_CheckDB_FlagActiveNotMatched

        // Mst_Tax_Get:
        public const string Mst_Tax_Get = "ErridnInventory.Mst_Tax_Get"; //// //Mst_Tax_Get

        // WAS_Mst_Tax_Get:
        public const string WAS_Mst_Tax_Get = "ErridnInventory.WAS_Mst_Tax_Get"; //// //WAS_Mst_Tax_Get

        // Mst_Tax_Create:
        public const string Mst_Tax_Create = "ErridnInventory.Mst_Tax_Create"; //// //Mst_Tax_Create
        public const string Mst_Tax_Create_InvalidTaxId = "ErridnInventory.Mst_Tax_Create_InvalidTaxId"; //// //Mst_Tax_Create_InvalidTaxCode
        public const string Mst_Tax_Create_InvalidTaxName = "ErridnInventory.Mst_Tax_Create_InvalidTaxName"; //// //Mst_Tax_Create_InvalidTaxName        
        public const string Mst_Tax_Create_InvalidTaxTemplate = "ErridnInventory.Mst_Tax_Create_InvalidTaxTemplate"; //// //Mst_Tax_Create_InvalidTaxTemplate        

        // WAS_Mst_Tax_Create:
        public const string WAS_Mst_Tax_Create = "ErridnInventory.WAS_Mst_Tax_Create"; //// //WAS_Mst_Tax_Create

        // Mst_Tax_Update:
        public const string Mst_Tax_Update = "ErridnInventory.Mst_Tax_Update"; //// //Mst_Tax_Update                
        public const string Mst_Tax_Update_InvalidTaxName = "ErridnInventory.Mst_Tax_Update_InvalidTaxName"; //// //Mst_Tax_Update_InvalidTaxName                       
        public const string Mst_Tax_Update_InvalidTaxTemplate = "ErridnInventory.Mst_Tax_Update_InvalidTaxTemplate"; //// //Mst_Tax_Update_InvalidTaxTemplate
        // WAS_Mst_Tax_Update:
        public const string WAS_Mst_Tax_Update = "ErridnInventory.WAS_Mst_Tax_Update"; //// //WAS_Mst_Tax_Update

        // Mst_Tax_Delete:
        public const string Mst_Tax_Delete = "ErridnInventory.Mst_Tax_Delete"; //// Mã lỗi: Mst_Tax_Delete ////

        // WAS_Mst_Tax_Delete:
        public const string WAS_Mst_Tax_Delete = "ErridnInventory.WAS_Mst_Tax_Delete"; //// //WAS_Mst_Tax_Delete
        #endregion

        #region // Tax_Register:
        // Tax_Register_CheckDB:
        public const string Tax_Register_CheckDB_RegisterNotFound = "ErridnInventory.Tax_Register_CheckDB_RegisterNotFound"; //// //Tax_Register_CheckDB_RegisterNotFound
        public const string Tax_Register_CheckDB_RegisterExist = "ErridnInventory.Tax_Register_CheckDB_RegisterExist"; //// //Tax_Register_CheckDB_RegisterExist
        public const string Tax_Register_CheckDB_TCTStatusNotMatched = "ErridnInventory.Tax_Register_CheckDB_TCTStatusNotMatched"; //// //Tax_Register_CheckDB_TCTStatusNotMatched

        // Tax_Register_Get:
        public const string Tax_Register_Get = "ErridnInventory.Tax_Register_Get"; //// //Tax_Register_Get

        // WAS_Tax_Register_Get:
        public const string WAS_Tax_Register_Get = "ErridnInventory.WAS_Tax_Register_Get"; //// //WAS_Tax_Register_Get
        
        // Tax_Register_Add:
        public const string Tax_Register_Add = "ErridnInventory.Tax_Register_Add"; //// //Tax_Register_Add
        public const string Tax_Register_Add_Input_Tax_RegisterDtlTblNotFound = "ErridnInventory.Tax_Register_Add_Input_Tax_RegisterDtlTblNotFound"; //// //Tax_Register_Add_Input_Tax_RegisterDtlTblNotFound

        // WAS_Tax_Register_Add:
        public const string WAS_Tax_Register_Add = "ErridnInventory.WAS_Tax_Register_Add"; //// //WAS_Tax_Register_Add
        
        // Tax_Register_Upd:
        public const string Tax_Register_Upd = "ErridnInventory.Tax_Register_Upd"; //// //Tax_Register_Upd
        public const string Tax_Register_Upd_Input_RegisterDtlTblNotFound = "ErridnInventory.Tax_Register_Upd_Input_RegisterDtlTblNotFound"; //// //Tax_Register_Upd_Input_RegisterDtlTblNotFound

        // WAS_Tax_Register_Upd:
        public const string WAS_Tax_Register_Upd = "ErridnInventory.WAS_Tax_Register_Upd"; //// //WAS_Tax_Register_Upd

        #endregion

        #region // Tax_RegisterStop:
        // Tax_RegisterStop_CheckDB:
        public const string Tax_RegisterStop_CheckDB_RegisterStopNotFound = "ErridnInventory.Tax_RegisterStop_CheckDB_RegisterStopNotFound"; //// //Tax_RegisterStop_CheckDB_RegisterStopNotFound
        public const string Tax_RegisterStop_CheckDB_RegisterStopExist = "ErridnInventory.Tax_RegisterStop_CheckDB_RegisterStopExist"; //// //Tax_RegisterStop_CheckDB_RegisterStopExist
        public const string Tax_RegisterStop_CheckDB_TCTStatusNotMatched = "ErridnInventory.Tax_RegisterStop_CheckDB_TCTStatusNotMatched"; //// //Tax_RegisterStop_CheckDB_TCTStatusNotMatched

        // Tax_RegisterStop_Get:
        public const string Tax_RegisterStop_Get = "ErridnInventory.Tax_RegisterStop_Get"; //// //Tax_RegisterStop_Get

        // WAS_Tax_RegisterStop_Get:
        public const string WAS_Tax_RegisterStop_Get = "ErridnInventory.WAS_Tax_RegisterStop_Get"; //// //WAS_Tax_RegisterStop_Get

        // Tax_RegisterStop_Upd:
        public const string Tax_RegisterStop_Upd = "ErridnInventory.Tax_RegisterStop_Upd"; //// //Tax_RegisterStop_Upd
        public const string Tax_RegisterStop_Upd_Input_RegisterStopDtlTblNotFound = "ErridnInventory.Tax_RegisterStop_Upd_Input_RegisterStopDtlTblNotFound"; //// //Tax_RegisterStop_Upd_Input_RegisterStopDtlTblNotFound

        // WAS_Tax_RegisterStop_Upd:
        public const string WAS_Tax_RegisterStop_Upd = "ErridnInventory.WAS_Tax_RegisterStop_Upd"; //// //WAS_Tax_RegisterStop_Upd

        // Tax_RegisterStop_Add:
        public const string Tax_RegisterStop_Add = "ErridnInventory.Tax_RegisterStop_Add"; //// //Tax_RegisterStop_Add
        public const string Tax_RegisterStop_Add_Input_Tax_RegisterStopDtlTblNotFound = "ErridnInventory.Tax_RegisterStop_Add_Input_Tax_RegisterStopDtlTblNotFound"; //// //Tax_RegisterStop_Add_Input_Tax_RegisterStopDtlTblNotFound

        // WAS_Tax_RegisterStop_Add:
        public const string WAS_Tax_RegisterStop_Add = "ErridnInventory.WAS_Tax_RegisterStop_Add"; //// //WAS_Tax_RegisterStop_Add
        #endregion

        #region // Tax_Submit:
        // Tax_Submit_CheckDB:
        public const string Tax_Submit_CheckDB_SubmitNotFound = "ErridnInventory.Tax_Submit_CheckDB_SubmitNotFound"; //// //Tax_Submit_CheckDB_SubmitNotFound
        public const string Tax_Submit_CheckDB_SubmitExist = "ErridnInventory.Tax_Submit_CheckDB_SubmitExist"; //// //Tax_Submit_CheckDB_SubmitExist
        public const string Tax_Submit_CheckDB_FlagSignedNotMatched = "ErridnInventory.Tax_Submit_CheckDB_FlagSignedNotMatched"; //// //Tax_Submit_CheckDB_FlagSignedNotMatched
        public const string Tax_Submit_CheckDB_FlagSentNotMatched = "ErridnInventory.Tax_Submit_CheckDB_FlagSentNotMatched"; //// //Tax_Submit_CheckDB_FlagSentNotMatched
        public const string Tax_Submit_CheckDB_FlagIsAppendixNotMatched = "ErridnInventory.Tax_Submit_CheckDB_FlagIsAppendixNotMatched"; //// //Tax_Submit_CheckDB_FlagIsAppendixNotMatched
        public const string Tax_Submit_CheckDB_TCTStatusNotMatched = "ErridnInventory.Tax_Submit_CheckDB_TCTStatusNotMatched"; //// //Tax_Submit_CheckDB_TCTStatusNotMatched
        public const string Tax_Submit_CheckDB_FlagActiveNotMatched = "ErridnInventory.Tax_Submit_CheckDB_FlagActiveNotMatched"; //// //Tax_Submit_CheckDB_FlagActiveNotMatched

        // Tax_Submit_Get:
        public const string Tax_Submit_Get = "ErridnInventory.Tax_Submit_Get"; //// //Tax_Submit_Get

        // WAS_Tax_Submit_Get:
        public const string WAS_Tax_Submit_Get = "ErridnInventory.WAS_Tax_Submit_Get"; //// //WAS_Tax_Submit_Get

        // Tax_Submit_Add:
        public const string Tax_Submit_Add = "ErridnInventory.Tax_Submit_Add"; //// //Tax_Submit_Add
        public const string Tax_Submit_Add_InvalidSubNo = "ErridnInventory.Tax_Submit_Add_InvalidSubNo"; //// //Tax_Submit_Add_InvalidSubNo
        public const string Tax_Submit_Add_InvalidTaxId = "ErridnInventory.Tax_Submit_Add_InvalidTaxId"; //// //Tax_Submit_Add_InvalidTaxId
        public const string Tax_Submit_Add_InvalidMST = "ErridnInventory.Tax_Submit_Add_InvalidMST"; //// //Tax_Submit_Add_InvalidMST
        public const string Tax_Submit_Add_InvalidFilePath = "ErridnInventory.Tax_Submit_Add_InvalidFilePath"; //// //Tax_Submit_Add_InvalidFilePath

        // WAS_Tax_Submit_Add
        public const string WAS_Tax_Submit_Add = "ErridnInventory.WAS_Tax_Submit_Add"; //// //WAS_Tax_Submit_Add

        // Tax_Submit_AddTrinhKy:
        public const string Tax_Submit_AddTrinhKy = "ErridnInventory.Tax_Submit_AddTrinhKy"; //// //Tax_Submit_AddTrinhKy

        // WAS_Tax_Submit_AddTrinhKy:
        public const string WAS_Tax_Submit_AddTrinhKy = "ErridnInventory.WAS_Tax_Submit_AddTrinhKy"; //// //WAS_Tax_Submit_AddTrinhKy

        // Tax_Submit_NopTrinhKy:
        public const string Tax_Submit_NopTrinhKy = "ErridnInventory.Tax_Submit_NopTrinhKy"; //// //Tax_Submit_NopTrinhKy

        // WAS_Tax_Submit_NopTrinhKy:
        public const string WAS_Tax_Submit_NopTrinhKy = "ErridnInventory.WAS_Tax_Submit_NopTrinhKy"; //// //WAS_Tax_Submit_NopTrinhKy
        
        // Tax_Submit_NopPhuLuc:
        public const string Tax_Submit_NopPhuLuc = "ErridnInventory.Tax_Submit_NopPhuLuc"; //// //Tax_Submit_NopPhuLuc

        // WAS_Tax_Submit_NopPhuLuc:
        public const string WAS_Tax_Submit_NopPhuLuc = "ErridnInventory.WAS_Tax_Submit_NopPhuLuc"; //// //WAS_Tax_Submit_NopPhuLuc

        // Tax_Submit_Upd:
        public const string Tax_Submit_Upd = "ErridnInventory.Tax_Submit_Upd"; //// //Tax_Submit_Upd
        
        // WAS_Tax_Submit_Add
        public const string WAS_Tax_Submit_Update = "ErridnInventory.WAS_Tax_Submit_Update"; //// //WAS_Tax_Submit_Update
        
        // Tax_Submit_Delete:
        public const string Tax_Submit_Delete = "ErridnInventory.Tax_Submit_Delete"; //// //Tax_Submit_Delete

        // WAS_Tax_Submit_Delete
        public const string WAS_Tax_Submit_Delete = "ErridnInventory.WAS_Tax_Submit_Delete"; //// //WAS_Tax_Submit_Delete

        #endregion

        #region // Mst_KyKeKhai:
        // Mst_KyKeKhai_CheckDB:
        public const string Mst_KyKeKhai_CheckDB_KyKeKhaiNotFound = "ErridnInventory.Mst_KyKeKhai_CheckDB_KyKeKhaiNotFound"; //// //Mst_KyKeKhai_CheckDB_KyKeKhaiNotFound
        public const string Mst_KyKeKhai_CheckDB_KyKeKhaiExist = "ErridnInventory.Mst_KyKeKhai_CheckDB_KyKeKhaiExist"; //// //Mst_KyKeKhai_CheckDB_KyKeKhaiExist
        public const string Mst_KyKeKhai_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_KyKeKhai_CheckDB_FlagActiveNotMatched"; //// //Mst_KyKeKhai_CheckDB_FlagActiveNotMatched

        // Mst_KyKeKhai_Get:
        public const string Mst_KyKeKhai_Get = "ErridnInventory.Mst_KyKeKhai_Get"; //// //Mst_KyKeKhai_Get

        // WAS_Mst_KyKeKhai_Get:
        public const string WAS_Mst_KyKeKhai_Get = "ErridnInventory.WAS_Mst_KyKeKhai_Get"; //// //WAS_Mst_KyKeKhai_Get
        #endregion

        #region // Map_TaxInKyKeKhai:
        // Map_TaxInKyKeKhai_Get:
        public const string Map_TaxInKyKeKhai_Get = "ErridnInventory.Map_TaxInKyKeKhai_Get"; //// //Map_TaxInKyKeKhai_Get

        // Map_TaxInKyKeKhai_Save:
        public const string Map_TaxInKyKeKhai_Save = "ErridnInventory.Map_TaxInKyKeKhai_Save"; //// //Map_TaxInKyKeKhai_Save
        //public const string Map_TaxInKyKeKhai_Save_InputTblDtlNotFound = "ErridnInventory.Map_TaxInKyKeKhai_Save_InputTblDtlNotFound"; //// //Map_TaxInKyKeKhai_Save_InputTblDtlNotFound

        // WAS_Map_TaxInKyKeKhai_Get:
        public const string WAS_Map_TaxInKyKeKhai_Get = "ErridnInventory.WAS_Map_TaxInKyKeKhai_Get"; //// //WAS_Map_TaxInKyKeKhai_Get

        // WAS_Map_TaxInKyKeKhai_Save:
        public const string WAS_Map_TaxInKyKeKhai_Save = "ErridnInventory.WAS_Map_TaxInKyKeKhai_Save"; //// //WAS_Map_TaxInKyKeKhai_Save

        #endregion

        #region // Tax_Appendix:
        // Tax_Appendix_CheckDB:
        public const string Tax_Appendix_CheckDB_AppendixNotFound = "ErridnInventory.Tax_Appendix_CheckDB_TaxTypeNotFound"; //// //Tax_Appendix_CheckDB_TaxTypeNotFound
        public const string Tax_Appendix_CheckDB_AppendixExist = "ErridnInventory.Tax_Appendix_CheckDB_TaxTypeExist"; //// //Tax_Appendix_CheckDB_TaxTypeExist
        public const string Tax_Appendix_CheckDB_FlagActiveNotMatched = "ErridnInventory.Tax_Appendix_CheckDB_FlagActiveNotMatched"; //// //Tax_Appendix_CheckDB_FlagActiveNotMatched

        // Tax_Appendix_Get:
        public const string Tax_Appendix_Get = "ErridnInventory.Tax_Appendix_Get"; //// //Tax_Appendix_Get

        // WAS_Tax_Appendix_Get:
        public const string WAS_Tax_Appendix_Get = "ErridnInventory.WAS_Tax_Appendix_Get"; //// //WAS_Tax_Appendix_Get

        // Tax_Appendix_Create:
        public const string Tax_Appendix_Create = "ErridnInventory.Tax_Appendix_Create"; //// //Tax_Appendix_Create
        public const string Tax_Appendix_Create_InvalidAppendixId = "ErridnInventory.Tax_Appendix_Create_InvalidAppendixId"; //// //Tax_Appendix_Create_InvalidAppendixId
        public const string Tax_Appendix_Create_InvalidAppendixName = "ErridnInventory.Tax_Appendix_Create_InvalidAppendixName"; //// //Tax_Appendix_Create_InvalidAppendixName        
        public const string Tax_Appendix_Create_InvalidAppendixTemplate = "ErridnInventory.Tax_Appendix_Create_InvalidAppendixTemplate"; //// //Tax_Appendix_Create_InvalidAppendixTemplate        

        // WAS_Tax_Appendix_Create:
        public const string WAS_Tax_Appendix_Create = "ErridnInventory.WAS_Tax_Appendix_Create"; //// //WAS_Tax_Appendix_Create

        // Tax_Appendix_Update:
        public const string Tax_Appendix_Update = "ErridnInventory.Tax_Appendix_Update"; //// //Tax_Appendix_Update      
        public const string Tax_Appendix_Update_InvalidAppendixName = "ErridnInventory.Tax_Appendix_Update_InvalidTaxTypeName"; //// //Tax_Appendix_Update_InvalidTaxTypeName                       

        // WAS_Tax_Appendix_Update:
        public const string WAS_Tax_Appendix_Update = "ErridnInventory.WAS_Tax_Appendix_Update"; //// //WAS_Tax_Appendix_Update

        // Tax_Appendix_Delete:
        public const string Tax_Appendix_Delete = "ErridnInventory.Tax_Appendix_Delete"; //// Mã lỗi: Tax_Appendix_Delete ////

        // WAS_Tax_Appendix_Delete:
        public const string WAS_Tax_Appendix_Delete = "ErridnInventory.WAS_Tax_Appendix_Delete"; //// //WAS_Tax_Appendix_Delete
        #endregion

        #region // Mst_NNTType:
        // Mst_NNTType_CheckDB:
        public const string Mst_NNTType_CheckDB_NNTTypeNotFound = "ErridnInventory.Mst_NNTType_CheckDB_NNTTypeNotFound"; //// //Mst_NNTType_CheckDB_NNTTypeNotFound
        public const string Mst_NNTType_CheckDB_NNTTypeExist = "ErridnInventory.Mst_NNTType_CheckDB_NNTTypeExist"; //// //Mst_NNTType_CheckDB_NNTTypeExist
        public const string Mst_NNTType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_NNTType_CheckDB_FlagActiveNotMatched"; //// //Mst_NNTType_CheckDB_FlagActiveNotMatched

        // Mst_NNTType_Get:
        public const string Mst_NNTType_Get = "ErridnInventory.Mst_NNTType_Get"; //// //Mst_NNTType_Get

        // WAS_Mst_NNTType_Get:
        public const string WAS_Mst_NNTType_Get = "ErridnInventory.WAS_Mst_NNTType_Get"; //// //WAS_Mst_NNTType_Get

        // Mst_NNTType_Create:
        public const string Mst_NNTType_Create = "ErridnInventory.Mst_NNTType_Create"; //// //Mst_NNTType_Create
        public const string Mst_NNTType_Create_InvalidNNTType = "ErridnInventory.Mst_NNTType_Create_InvalidNNTType"; //// //Mst_NNTType_Create_InvalidNNTTypeCode
        public const string Mst_NNTType_Create_InvalidNNTTypeName = "ErridnInventory.Mst_NNTType_Create_InvalidNNTTypeName"; //// //Mst_NNTType_Create_InvalidNNTTypeName        

        // WAS_Mst_NNTType_Create:
        public const string WAS_Mst_NNTType_Create = "ErridnInventory.WAS_Mst_NNTType_Create"; //// //WAS_Mst_NNTType_Create

        // Mst_NNTType_Update:
        public const string Mst_NNTType_Update = "ErridnInventory.Mst_NNTType_Update"; //// //Mst_NNTType_Update                
        public const string Mst_NNTType_Update_InvalidNNTTypeName = "ErridnInventory.Mst_NNTType_Update_InvalidNNTTypeName"; //// //Mst_NNTType_Update_InvalidNNTTypeName                       

        // WAS_Mst_NNTType_Update:
        public const string WAS_Mst_NNTType_Update = "ErridnInventory.WAS_Mst_NNTType_Update"; //// //WAS_Mst_NNTType_Update

        // Mst_NNTType_Delete:
        public const string Mst_NNTType_Delete = "ErridnInventory.Mst_NNTType_Delete"; //// Mã lỗi: Mst_NNTType_Delete ////

        // WAS_Mst_NNTType_Delete:
        public const string WAS_Mst_NNTType_Delete = "ErridnInventory.WAS_Mst_NNTType_Delete"; //// //WAS_Mst_NNTType_Delete
        //RptSv:
        public const string WAS_RptSv_Mst_NNTType_Get = "ErridnInventory.WAS_RptSv_Mst_NNTType_Get"; //// //WAS_RptSv_Mst_NNTType_Get

        #endregion

        #region // Mst_CustomerNNTType:
        // Mst_CustomerNNTType_CheckDB:
        public const string Mst_CustomerNNTType_CheckDB_CustomerNNTTypeNotFound = "ErridnInventory.Mst_CustomerNNTType_CheckDB_CustomerNNTTypeNotFound"; //// //Mst_CustomerNNTType_CheckDB_CustomerNNTTypeNotFound
        public const string Mst_CustomerNNTType_CheckDB_CustomerNNTTypeExist = "ErridnInventory.Mst_CustomerNNTType_CheckDB_CustomerNNTTypeExist"; //// //Mst_CustomerNNTType_CheckDB_CustomerNNTTypeExist
        public const string Mst_CustomerNNTType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_CustomerNNTType_CheckDB_FlagActiveNotMatched"; //// //Mst_CustomerNNTType_CheckDB_FlagActiveNotMatched

        // Mst_CustomerNNTType_Get:
        public const string Mst_CustomerNNTType_Get = "ErridnInventory.Mst_CustomerNNTType_Get"; //// //Mst_CustomerNNTType_Get

        // WAS_Mst_CustomerNNTType_Get:
        public const string WAS_Mst_CustomerNNTType_Get = "ErridnInventory.WAS_Mst_CustomerNNTType_Get"; //// //WAS_Mst_CustomerNNTType_Get

        // Mst_CustomerNNTType_Create:
        public const string Mst_CustomerNNTType_Create = "ErridnInventory.Mst_CustomerNNTType_Create"; //// //Mst_CustomerNNTType_Create
        public const string Mst_CustomerNNTType_Create_InvalidCustomerNNTType = "ErridnInventory.Mst_CustomerNNTType_Create_InvalidCustomerNNTType"; //// //Mst_CustomerNNTType_Create_InvalidCustomerNNTTypeCode
        public const string Mst_CustomerNNTType_Create_InvalidCustomerNNTTypeName = "ErridnInventory.Mst_CustomerNNTType_Create_InvalidCustomerNNTTypeName"; //// //Mst_CustomerNNTType_Create_InvalidCustomerNNTTypeName        

        // WAS_Mst_CustomerNNTType_Create:
        public const string WAS_Mst_CustomerNNTType_Create = "ErridnInventory.WAS_Mst_CustomerNNTType_Create"; //// //WAS_Mst_CustomerNNTType_Create

        // Mst_CustomerNNTType_Update:
        public const string Mst_CustomerNNTType_Update = "ErridnInventory.Mst_CustomerNNTType_Update"; //// //Mst_CustomerNNTType_Update                
        public const string Mst_CustomerNNTType_Update_InvalidCustomerNNTTypeName = "ErridnInventory.Mst_CustomerNNTType_Update_InvalidCustomerNNTTypeName"; //// //Mst_CustomerNNTType_Update_InvalidCustomerNNTTypeName                       

        // WAS_Mst_CustomerNNTType_Update:
        public const string WAS_Mst_CustomerNNTType_Update = "ErridnInventory.WAS_Mst_CustomerNNTType_Update"; //// //WAS_Mst_CustomerNNTType_Update

        // Mst_CustomerNNTType_Delete:
        public const string Mst_CustomerNNTType_Delete = "ErridnInventory.Mst_CustomerNNTType_Delete"; //// Mã lỗi: Mst_CustomerNNTType_Delete ////

        // WAS_Mst_CustomerNNTType_Delete:
        public const string WAS_Mst_CustomerNNTType_Delete = "ErridnInventory.WAS_Mst_CustomerNNTType_Delete"; //// //WAS_Mst_CustomerNNTType_Delete
		#endregion

		#region // View_GroupView:
		// View_GroupView_CheckDB:
		public const string View_GroupView_CheckDB_GroupViewCodeNotFound = "ErridnInventory.View_GroupView_CheckDB_GroupViewCodeNotFound"; //// // View_GroupView_CheckDB_GroupViewCodeNotFound
		public const string View_GroupView_CheckDB_GroupViewCodeExist = "ErridnInventory.View_GroupView_CheckDB_GroupViewCodeExist"; //// // View_GroupView_CheckDB_GroupViewCodeExist
		public const string View_GroupView_CheckDB_FlagActiveNotMatched = "ErridnInventory.View_GroupView_CheckDB_FlagActiveNotMatched"; //// // View_GroupView_CheckDB_FlagActiveNotMatched

		// View_GroupView_Get:
		public const string View_GroupView_Get = "ErridnInventory.View_GroupView_Get"; //// // View_GroupView_Get

		// WAS_View_GroupView_Get:
		public const string WAS_View_GroupView_Get = "ErridnInventory.WAS_View_GroupView_Get"; //// // WAS_View_GroupView_Get

		// WAS_View_GroupView_Create:
		public const string WAS_View_GroupView_Create = "ErridnInventory.WAS_View_GroupView_Create"; //// // WAS_View_GroupView_Create

		// View_GroupView_Create:
		public const string View_GroupView_Create = "ErridnInventory.View_GroupView_Create"; //// // View_GroupView_Create
		public const string View_GroupView_Create_InvalidGroupViewCode = "ErridnInventory.View_GroupView_Create_InvalidGroupViewCode"; //// // View_GroupView_Create_InvalidGroupViewCode
		public const string View_GroupView_Create_InvalidGroupViewName = "ErridnInventory.View_GroupView_Create_InvalidGroupViewName"; //// // View_GroupView_Create_InvalidGroupViewName

		// WAS_View_GroupView_Update:
		public const string WAS_View_GroupView_Update = "ErridnInventory.WAS_View_GroupView_Update"; //// // WAS_View_GroupView_Update

		// View_GroupView_Update:
		public const string View_GroupView_Update = "ErridnInventory.View_GroupView_Update"; //// // View_GroupView_Update
		public const string View_GroupView_Update_InvalidGroupViewName = "ErridnInventory.View_GroupView_Update_InvalidGroupViewName"; //// // View_GroupView_Update_InvalidGroupViewName

		// WAS_View_GroupView_Delete:
		public const string WAS_View_GroupView_Delete = "ErridnInventory.WAS_View_GroupView_Delete"; //// // WAS_View_GroupView_Delete

		// View_GroupView_Delete:
		public const string View_GroupView_Delete = "ErridnInventory.View_GroupView_Delete"; //// // View_GroupView_Delete
		#endregion

		#region // View_ColumnView:
		// View_ColumnView_CheckDB:
		public const string View_ColumnView_CheckDB_ColumnViewCodeNotFound = "ErridnInventory.View_ColumnView_CheckDB_ColumnViewCodeNotFound"; //// // View_ColumnView_CheckDB_ColumnViewCodeNotFound
		public const string View_ColumnView_CheckDB_ColumnViewCodeExist = "ErridnInventory.View_ColumnView_CheckDB_ColumnViewCodeExist"; //// // View_ColumnView_CheckDB_ColumnViewCodeExist
		public const string View_ColumnView_CheckDB_FlagActiveNotMatched = "ErridnInventory.View_ColumnView_CheckDB_FlagActiveNotMatched"; //// // View_ColumnView_CheckDB_FlagActiveNotMatched

		// View_ColumnView_Get:
		public const string View_ColumnView_Get = "ErridnInventory.View_ColumnView_Get"; //// // View_ColumnView_Get

		// WAS_Mst_Agent_Get:
		public const string WAS_View_ColumnView_Get = "ErridnInventory.WAS_View_ColumnView_Get"; //// // WAS_View_ColumnView_Get

		// WAS_View_ColumnView_Create:
		public const string WAS_View_ColumnView_Create = "ErridnInventory.WAS_View_ColumnView_Create"; //// // WAS_View_ColumnView_Create

		// View_ColumnView_Create:
		public const string View_ColumnView_Create = "ErridnInventory.View_ColumnView_Create"; //// // View_ColumnView_Create
		public const string View_ColumnView_Create_InvalidColumnViewCode = "ErridnInventory.View_ColumnView_Create_InvalidColumnViewCode"; //// // Mst_View_ColumnView_Create_InvalidColumnViewCodeAgent_Create_InvalidAgentCode
		public const string View_ColumnView_Create_InvalidColumnViewName = "ErridnInventory.View_ColumnView_Create_InvalidColumnViewName"; //// // View_ColumnView_Create_InvalidColumnViewName

		// WAS_View_ColumnView_Update:
		public const string WAS_View_ColumnView_Update = "ErridnInventory.WAS_View_ColumnView_Update"; //// // WAS_View_ColumnView_Update

		// View_ColumnView_Update:
		public const string View_ColumnView_Update = "ErridnInventory.View_ColumnView_Update"; //// // View_ColumnView_Update
		public const string View_ColumnView_Update_InvalidColumnViewName = "ErridnInventory.View_ColumnView_Update_InvalidColumnViewName"; //// // View_ColumnView_Update_InvalidColumnViewName

		// WAS_View_ColumnView_Delete:
		public const string WAS_View_ColumnView_Delete = "ErridnInventory.WAS_View_ColumnView_Delete"; //// // WAS_View_ColumnView_Delete

		// View_ColumnView_Delete:
		public const string View_ColumnView_Delete = "ErridnInventory.View_ColumnView_Delete"; //// // View_ColumnView_Delete
		#endregion

		#region // View_ColumnInGroup:
		// View_ColumnInGroup_CheckDB:
		public const string View_ColumnInGroup_CheckDB_DistrictNotFound = "ErridnInventory.View_ColumnInGroup_CheckDB_DistrictNotFound"; //// // View_ColumnInGroup_CheckDB_DistrictNotFound
		public const string View_ColumnInGroup_CheckDB_DistrictExist = "ErridnInventory.View_ColumnInGroup_CheckDB_DistrictExist"; //// // View_ColumnInGroup_CheckDB_DistrictExist
		public const string View_ColumnInGroup_CheckDB_FlagActiveNotMatched = "ErridnInventory.View_ColumnInGroup_CheckDB_FlagActiveNotMatched"; //// // View_ColumnInGroup_CheckDB_FlagActiveNotMatched

		// View_ColumnInGroup_Get:
		public const string View_ColumnInGroup_Get = "ErridnInventory.View_ColumnInGroup_Get"; //// // View_ColumnInGroup_Get

		// WAS_View_ColumnInGroup_Get:
		public const string WAS_View_ColumnInGroup_Get = "ErridnInventory.WAS_View_ColumnInGroup_Get"; //// // WAS_View_ColumnInGroup_Get

		// WAS_View_ColumnInGroup_Create:
		public const string WAS_View_ColumnInGroup_Create = "ErridnInventory.WAS_View_ColumnInGroup_Create"; //// // WAS_View_ColumnInGroup_Create

		// View_ColumnInGroup_Create:
		public const string View_ColumnInGroup_Create = "ErridnInventory.View_ColumnInGroup_Create"; //// // View_ColumnInGroup_Create
		public const string View_ColumnInGroup_Create_InvalidGroupViewCode = "ErridnInventory.View_ColumnInGroup_Create_InvalidGroupViewCode"; //// // View_ColumnInGroup_Create_InvalidGroupViewCode
		public const string View_ColumnInGroup_Create_InvalidColumnViewCode = "ErridnInventory.View_ColumnInGroup_Create_InvalidColumnViewCode"; //// // View_ColumnInGroup_Create_InvalidColumnViewCode

		// WAS_View_ColumnInGroup_Update:
		public const string WAS_View_ColumnInGroup_Update = "ErridnInventory.WAS_View_ColumnInGroup_Update"; //// // WAS_View_ColumnInGroup_Update

		// View_ColumnInGroup_Update:
		public const string View_ColumnInGroup_Update = "ErridnInventory.View_ColumnInGroup_Update"; //// // View_ColumnInGroup_Update

		// WAS_View_ColumnInGroup_Delete:
		public const string WAS_View_ColumnInGroup_Delete = "ErridnInventory.WAS_View_ColumnInGroup_Delete"; //// // WAS_View_ColumnInGroup_Delete

		// View_ColumnInGroup_Delete:
		public const string View_ColumnInGroup_Delete = "ErridnInventory.View_ColumnInGroup_Delete"; //// // View_ColumnInGroup_Delete

		// View_ColumnInGroup_Save:
		public const string View_ColumnInGroup_Save_Input_View_ColumnInGroupTblNotFound = "ErridnInventory.View_ColumnInGroup_Save_Input_View_ColumnInGroupTblNotFound"; //// // View_ColumnInGroup_Save_Input_View_ColumnInGroupTblNotFound
		public const string View_ColumnInGroup_Save_Input_View_ColumnInGroupTblInvalid = "ErridnInventory.View_ColumnInGroup_Save_Input_View_ColumnInGroupTblInvalid"; //// // View_ColumnInGroup_Save_Input_View_ColumnInGroupTblInvalid

		// WAS_View_ColumnInGroup_Save:
		public const string WAS_View_ColumnInGroup_Save = "ErridnInventory.WAS_View_ColumnInGroup_Save"; //// // WAS_View_ColumnInGroup_Save

		// View_ColumnInGroup_Save:
		public const string View_ColumnInGroup_Save = "ErridnInventory.View_ColumnInGroup_Save"; //// // View_ColumnInGroup_Save

		#endregion

		#region // Mst_InventoryBlock:
		// Mst_InventoryBlock_CheckDB:
		public const string Mst_InventoryBlock_CheckDB_DistrictNotFound = "ErridnInventory.Mst_InventoryBlock_CheckDB_DistrictNotFound"; //// // Mst_InventoryBlock_CheckDB_DistrictNotFound
		public const string Mst_InventoryBlock_CheckDB_DistrictExist = "ErridnInventory.Mst_InventoryBlock_CheckDB_DistrictExist"; //// // Mst_InventoryBlock_CheckDB_DistrictExist
		public const string Mst_InventoryBlock_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_InventoryBlock_CheckDB_FlagActiveNotMatched"; //// // Mst_InventoryBlock_CheckDB_FlagActiveNotMatched

		// Mst_InventoryBlock_Get:
		public const string Mst_InventoryBlock_Get = "ErridnInventory.Mst_InventoryBlock_Get"; //// // Mst_InventoryBlock_Get

		// WAS_Mst_InventoryBlock_Get:
		public const string WAS_Mst_InventoryBlock_Get = "ErridnInventory.WAS_Mst_InventoryBlock_Get"; //// // WAS_Mst_InventoryBlock_Get

		// WAS_Mst_InventoryBlock_Create:
		public const string WAS_Mst_InventoryBlock_Create = "ErridnInventory.WAS_Mst_InventoryBlock_Create"; //// // WAS_Mst_InventoryBlock_Create

		// Mst_InventoryBlock_Create:
		public const string Mst_InventoryBlock_Create = "ErridnInventory.Mst_InventoryBlock_Create"; //// // Mst_InventoryBlock_Create
		public const string Mst_InventoryBlock_Create_InvalidInvBlockCode = "ErridnInventory.Mst_InventoryBlock_Create_InvalidInvBlockCode"; //// // Mst_InventoryBlock_Create_InvalidInvBlockCode
		public const string Mst_InventoryBlock_Create_InvalidShelfCode = "ErridnInventory.Mst_InventoryBlock_Create_InvalidShelfCode"; //// // Mst_InventoryBlock_Create_InvalidShelfCode
		public const string Mst_InventoryBlock_Create_InvalidValueLength = "ErridnInventory.Mst_InventoryBlock_Create_InvalidValueLength"; //// // Mst_InventoryBlock_Create_InvalidValueLength
		public const string Mst_InventoryBlock_Create_InvalidValueWidth = "ErridnInventory.Mst_InventoryBlock_Create_InvalidValueWidth"; //// // Mst_InventoryBlock_Create_InvalidValueWidth
		public const string Mst_InventoryBlock_Create_InvalidValueHeight = "ErridnInventory.Mst_InventoryBlock_Create_InvalidValueHeight"; //// // Mst_InventoryBlock_Create_InvalidValueHeight

		// WAS_Mst_InventoryBlock_Update:
		public const string WAS_Mst_InventoryBlock_Update = "ErridnInventory.WAS_Mst_InventoryBlock_Update"; //// // WAS_Mst_InventoryBlock_Update

		// Mst_InventoryBlock_Update:
		public const string Mst_InventoryBlock_Update = "ErridnInventory.Mst_InventoryBlock_Update"; //// // Mst_InventoryBlock_Update

		// WAS_Mst_InventoryBlock_Delete:
		public const string WAS_Mst_InventoryBlock_Delete = "ErridnInventory.WAS_Mst_InventoryBlock_Delete"; //// // WAS_Mst_InventoryBlock_Delete

		// Mst_InventoryBlock_Delete:
		public const string Mst_InventoryBlock_Delete = "ErridnInventory.Mst_InventoryBlock_Delete"; //// // Mst_InventoryBlock_Delete

		#endregion

		#region // Mst_Agent:
		// Mst_Agent_CheckDB:
		public const string Mst_Agent_CheckDB_AgentCodeNotFound = "ErridnInventory.Mst_Agent_CheckDB_AgentCodeNotFound"; //// // Mst_Agent_CheckDB_AgentCodeNotFound
		public const string Mst_Agent_CheckDB_AgentCodeExist = "ErridnInventory.Mst_Agent_CheckDB_AgentCodeExist"; //// // Mst_Agent_CheckDB_AgentCodeExist
		public const string Mst_Agent_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Agent_CheckDB_FlagActiveNotMatched"; //// // Mst_Agent_CheckDB_FlagActiveNotMatched

		// Mst_Agent_Get:
		public const string Mst_Agent_Get = "ErridnInventory.Mst_Agent_Get"; //// // Mst_Agent_Get

		// WAS_Mst_Agent_Get:
		public const string WAS_Mst_Agent_Get = "ErridnInventory.WAS_Mst_Agent_Get"; //// // WAS_Mst_Agent_Get

		// WAS_Mst_Agent_Create:
		public const string WAS_Mst_Agent_Create = "ErridnInventory.WAS_Mst_Agent_Create"; //// // WAS_Mst_Agent_Create

		// Mst_Agent_Create:
		public const string Mst_Agent_Create = "ErridnInventory.Mst_Agent_Create"; //// // Mst_Agent_Create
		public const string Mst_Agent_Create_InvalidAgentCode = "ErridnInventory.Mst_Agent_Create_InvalidAgentCode"; //// // Mst_Agent_Create_InvalidAgentCode
		public const string Mst_Agent_Create_InvalidAgentName = "ErridnInventory.Mst_Agent_Create_InvalidAgentName"; //// // Mst_Agent_Create_InvalidAgentName

		// WAS_Mst_Agent_Update:
		public const string WAS_Mst_Agent_Update = "ErridnInventory.WAS_Mst_Agent_Update"; //// // WAS_Mst_Agent_Update

		// Mst_Agent_Update:
		public const string Mst_Agent_Update = "ErridnInventory.Mst_Agent_Update"; //// // Mst_Agent_Update
		public const string Mst_Agent_Update_InvalidAgentName = "ErridnInventory.Mst_Agent_Update_InvalidAgentName"; //// // Mst_Agent_Update_InvalidAgentName

		// WAS_Mst_Agent_Delete:
		public const string WAS_Mst_Agent_Delete = "ErridnInventory.WAS_Mst_Agent_Delete"; //// // WAS_Mst_Agent_Delete

		// Mst_Agent_Delete:
		public const string Mst_Agent_Delete = "ErridnInventory.Mst_Agent_Delete"; //// // Mst_Agent_Delete

		#endregion

		#region // Mst_Dealer:
		// Mst_Dealer_CheckDB:
		public const string Mst_Dealer_CheckDB_DLCodeNotFound = "ErridnInventory.Mst_Dealer_CheckDB_DLCodeNotFound"; //// //Mst_Dealer_CheckDB_DLCodeNotFound
        public const string Mst_Dealer_CheckDB_DLCodeExist = "ErridnInventory.Mst_Dealer_CheckDB_DLCodeExist"; //// //Mst_Dealer_CheckDB_DLCodeExist
        public const string Mst_Dealer_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_Dealer_CheckDB_FlagActiveNotMatched"; //// //Mst_Dealer_CheckDB_FlagActiveNotMatched
		public const string Mst_Dealer_CheckDB_FlagRootNotMatched = "ErridnInventory.Mst_Dealer_CheckDB_FlagRootNotMatched"; //// //Mst_Dealer_CheckDB_FlagRootNotMatched

		// Mst_Dealer_Get:
		public const string Mst_Dealer_Get = "ErridnInventory.Mst_Dealer_Get"; //// //Mst_Dealer_Get

        // WAS_Mst_Dealer_Get:
        public const string WAS_Mst_Dealer_Get = "ErridnInventory.WAS_Mst_Dealer_Get"; //// //WAS_Mst_Dealer_Get

        // Mst_Dealer_Create:
        public const string Mst_Dealer_Create = "ErridnInventory.Mst_Dealer_Create"; //// //Mst_Dealer_Create
        public const string Mst_Dealer_Create_InvalidDLCode = "ErridnInventory.Mst_Dealer_Create_InvalidDLCode"; //// //Mst_Dealer_Create_InvalidDLCode
        public const string Mst_Dealer_Create_InvalidDLName = "ErridnInventory.Mst_Dealer_Create_InvalidDLName"; //// //Mst_Dealer_Create_InvalidDLName

        // WAS_Mst_Dealer_Create:
        public const string WAS_Mst_Dealer_Create = "ErridnInventory.WAS_Mst_Dealer_Create"; //// //WAS_Mst_Dealer_Create

        // Mst_Dealer_Update:
        public const string Mst_Dealer_Update = "ErridnInventory.Mst_Dealer_Update"; //// //Mst_Dealer_Update
        public const string Mst_Dealer_Update_InvalidDLName = "ErridnInventory.Mst_Dealer_Update_InvalidDLName"; //// //Mst_Dealer_Update_InvalidDLName

        // WAS_Mst_Dealer_Update:
        public const string WAS_Mst_Dealer_Update = "ErridnInventory.WAS_Mst_Dealer_Update"; //// //WAS_Mst_Dealer_Update

        // Mst_Dealer_Delete:
        public const string Mst_Dealer_Delete = "ErridnInventory.Mst_Dealer_Delete"; //// //Mst_Dealer_Delete

        // WAS_Mst_Dealer_Delete:
        public const string WAS_Mst_Dealer_Delete = "ErridnInventory.WAS_Mst_Dealer_Delete"; //// //WAS_Mst_Dealer_Delete

		// RptSv_Mst_Dealer_Get:
		public const string RptSv_Mst_Dealer_Get = "ErridnInventory.RptSv_Mst_Dealer_Get"; //// //RptSv_Mst_Dealer_Get

		// WAS_RptSv_Mst_Dealer_Get:
		public const string WAS_RptSv_Mst_Dealer_Get = "ErridnInventory.WAS_RptSv_Mst_Dealer_Get"; //// //WAS_RptSv_Mst_Dealer_Get

		// RptSv_Mst_Dealer_Create:
		public const string RptSv_Mst_Dealer_Create = "ErridnInventory.RptSv_Mst_Dealer_Create"; //// //RptSv_Mst_Dealer_Create

		// WAS_RptSv_Mst_Dealer_Create:
		public const string WAS_RptSv_Mst_Dealer_Create = "ErridnInventory.WAS_RptSv_Mst_Dealer_Create"; //// //WAS_RptSv_Mst_Dealer_Create

		// RptSv_Mst_Dealer_Update:
		public const string RptSv_Mst_Dealer_Update = "ErridnInventory.RptSv_Mst_Dealer_Update"; //// //RptSv_Mst_Dealer_Update

		// WAS_RptSv_Mst_Dealer_Update:
		public const string WAS_RptSv_Mst_Dealer_Update = "ErridnInventory.WAS_RptSv_Mst_Dealer_Update"; //// //WAS_RptSv_Mst_Dealer_Update

		// RptSv_Mst_Dealer_Delete:
		public const string RptSv_Mst_Dealer_Delete = "ErridnInventory.RptSv_Mst_Dealer_Delete"; //// //RptSv_Mst_Dealer_Delete

		// WAS_RptSv_Mst_Dealer_Delete:
		public const string WAS_RptSv_Mst_Dealer_Delete = "ErridnInventory.WAS_RptSv_Mst_Dealer_Delete"; //// //WAS_RptSv_Mst_Dealer_Delete
		#endregion

		#region // Mst_MapPartColor:
		// Mst_MapPartColor_CheckDB:
		public const string Mst_MapPartColor_CheckDB_MapPartColorNotFound = "ErridnInventory.Mst_MapPartColor_CheckDB_MapPartColorNotFound"; //// //Mst_MapPartColor_CheckDB_MapPartColorNotFound
		public const string Mst_MapPartColor_CheckDB_MapPartColorExist = "ErridnInventory.Mst_MapPartColor_CheckDB_MapPartColorExist"; //// //Mst_MapPartColor_CheckDB_MapPartColorExist
		public const string Mst_MapPartColor_CheckDB_FlagDefaultNotMatched = "ErridnInventory.Mst_MapPartColor_CheckDB_FlagDefaultNotMatched"; //// //Mst_MapPartColor_CheckDB_FlagDefaultNotMatched
		public const string Mst_MapPartColor_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_MapPartColor_CheckDB_FlagActiveNotMatched"; //// //Mst_MapPartColor_CheckDB_FlagActiveNotMatched

		// Mst_MapPartColor_Get:
		public const string Mst_MapPartColor_Get = "ErridnInventory.Mst_MapPartColor_Get"; //// //Mst_MapPartColor_Get

		// WAS_Mst_MapPartColor_Get:
		public const string WAS_Mst_MapPartColor_Get = "ErridnInventory.WAS_Mst_MapPartColor_Get"; //// //WAS_Mst_MapPartColor_Get

		// Mst_MapPartColor_Create:
		public const string Mst_MapPartColor_Create = "ErridnInventory.Mst_MapPartColor_Create"; //// //Mst_MapPartColor_Create
		public const string Mst_MapPartColor_Create_InvalidPartCode = "ErridnInventory.Mst_MapPartColor_Create_InvalidPartCode"; //// //Mst_MapPartColor_Create_InvalidPartCode
		public const string Mst_MapPartColor_Create_InvalidPartColorCode = "ErridnInventory.Mst_MapPartColor_Create_InvalidPartColorCode"; //// //Mst_MapPartColor_Create_InvalidPartColorCode

		// WAS_Mst_MapPartColor_Create:
		public const string WAS_Mst_MapPartColor_Create = "ErridnInventory.WAS_Mst_MapPartColor_Create"; //// //WAS_Mst_MapPartColor_Create

		// Mst_MapPartColor_Update:
		public const string Mst_MapPartColor_Update = "ErridnInventory.Mst_MapPartColor_Update"; //// //Mst_MapPartColor_Update

		// WAS_Mst_MapPartColor_Update:
		public const string WAS_Mst_MapPartColor_Update = "ErridnInventory.WAS_Mst_MapPartColor_Update"; //// //WAS_Mst_MapPartColor_Update

		// Mst_MapPartColor_Delete:
		public const string Mst_MapPartColor_Delete = "ErridnInventory.Mst_MapPartColor_Delete"; //// //Mst_MapPartColor_Delete

		// WAS_Mst_MapPartColor_Delete:
		public const string WAS_Mst_MapPartColor_Delete = "ErridnInventory.WAS_Mst_MapPartColor_Delete"; //// //WAS_Mst_MapPartColor_Delete
		#endregion

		#region // Mst_GovTaxID:
		// Mst_GovTaxID_CheckDB:
		public const string Mst_GovTaxID_CheckDB_GovTaxIDNotFound = "ErridnInventory.Mst_GovTaxID_CheckDB_GovTaxIDNotFound"; //// //Mst_GovTaxID_CheckDB_GovTaxIDNotFound
        public const string Mst_GovTaxID_CheckDB_GovTaxIDExist = "ErridnInventory.Mst_GovTaxID_CheckDB_GovTaxIDExist"; //// //Mst_GovTaxID_CheckDB_GovTaxIDExist
        public const string Mst_GovTaxID_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_GovTaxID_CheckDB_FlagActiveNotMatched"; //// //Mst_GovTaxID_CheckDB_FlagActiveNotMatched
        
        // RptSv_Mst_GovTaxID_Get:
        public const string RptSv_Mst_GovTaxID_Get = "ErridnInventory.RptSv_Mst_GovTaxID_Get"; //// //RptSv_Mst_GovTaxID_Get

        // WAS_RptSv_Mst_GovTaxID_Get:
        public const string WAS_RptSv_Mst_GovTaxID_Get = "ErridnInventory.WAS_RptSv_Mst_GovTaxID_Get"; //// //WAS_RptSv_Mst_GovTaxID_Get

        // Mst_GovTaxID_Get:
        public const string Mst_GovTaxID_Get = "ErridnInventory.Mst_GovTaxID_Get"; //// //Mst_GovTaxID_Get

        // WAS_Mst_GovTaxID_Get:
        public const string WAS_Mst_GovTaxID_Get = "ErridnInventory.WAS_Mst_GovTaxID_Get"; //// //WAS_Mst_GovTaxID_Get

        // Mst_GovTaxID_Create:
        public const string Mst_GovTaxID_Create = "ErridnInventory.Mst_GovTaxID_Create"; //// //Mst_GovTaxID_Create
        public const string Mst_GovTaxID_Create_InvalidGovTaxID = "ErridnInventory.Mst_GovTaxID_Create_InvalidGovTaxID"; //// //Mst_GovTaxID_Create_InvalidGovTaxID
        public const string Mst_GovTaxID_Create_InvalidGovTaxName = "ErridnInventory.Mst_GovTaxID_Create_InvalidGovTaxName"; //// //Mst_GovTaxID_Create_InvalidGovTaxName

        // WAS_Mst_GovTaxID_Create:
        public const string WAS_Mst_GovTaxID_Create = "ErridnInventory.WAS_Mst_GovTaxID_Create"; //// //WAS_Mst_GovTaxID_Create

        // Mst_GovTaxID_Update:
        public const string Mst_GovTaxID_Update = "ErridnInventory.Mst_GovTaxID_Update"; //// //Mst_GovTaxID_Update
        public const string Mst_GovTaxID_Update_InvalidGovTaxName = "ErridnInventory.Mst_GovTaxID_Update_InvalidGovTaxName"; //// //Mst_GovTaxID_Update_InvalidGovTaxName

        // WAS_Mst_GovTaxID_Update:
        public const string WAS_Mst_GovTaxID_Update = "ErridnInventory.WAS_Mst_GovTaxID_Update"; //// //WAS_Mst_GovTaxID_Update

        // Mst_GovTaxID_Delete:
        public const string Mst_GovTaxID_Delete = "ErridnInventory.Mst_GovTaxID_Delete"; //// //Mst_GovTaxID_Delete

        // WAS_Mst_GovTaxID_Delete:
        public const string WAS_Mst_GovTaxID_Delete = "ErridnInventory.WAS_Mst_GovTaxID_Delete"; //// //WAS_Mst_GovTaxID_Delete

        //RptSv:
        public const string WAS_RptSv_Mst_GovTaxID_Create = "ErridnInventory.WAS_RptSv_Mst_GovTaxID_Create"; //// //WAS_RptSv_Mst_GovTaxID_Create
        public const string RptSv_Mst_GovTaxID_Create = "ErridnInventory.RptSv_Mst_GovTaxID_Create"; //// //RptSv_Mst_GovTaxID_Create
        public const string WAS_RptSv_Mst_GovTaxID_Update = "ErridnInventory.WAS_RptSv_Mst_GovTaxID_Update"; //// //WAS_RptSv_Mst_GovTaxID_Update
        public const string RptSv_Mst_GovTaxID_Update = "ErridnInventory.RptSv_Mst_GovTaxID_Update"; //// //RptSv_Mst_GovTaxID_Update
        public const string WAS_RptSv_Mst_GovTaxID_Delete = "ErridnInventory.WAS_RptSv_Mst_GovTaxID_Delete"; //// //WAS_RptSv_Mst_GovTaxID_Delete
        public const string RptSv_Mst_GovTaxID_Delete = "ErridnInventory.RptSv_Mst_GovTaxID_Delete"; //// //RptSv_Mst_GovTaxID_Delete

        #endregion

        #region // Mst_NNT:
        // Mst_NNT_CheckDB:
        public const string Mst_NNT_CheckDB_NNTNotFound = "ErridnInventory.Mst_NNT_CheckDB_NNTNotFound"; //// //Mst_NNT_CheckDB_NNTNotFound
        public const string Mst_NNT_CheckDB_NNTExist = "ErridnInventory.Mst_NNT_CheckDB_NNTExist"; //// //Mst_NNT_CheckDB_NNTExist
        public const string Mst_NNT_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_NNT_CheckDB_FlagActiveNotMatched"; //// //Mst_NNT_CheckDB_FlagActiveNotMatched
        public const string Mst_NNT_CheckDB_StatusNotMatched = "ErridnInventory.Mst_NNT_CheckDB_StatusNotMatched"; //// //Mst_NNT_CheckDB_StatusNotMatched
        public const string Mst_NNT_CheckDB_RegisterStatusNotMatched = "ErridnInventory.Mst_NNT_CheckDB_RegisterStatusNotMatched"; //// //Mst_NNT_CheckDB_RegisterStatusNotMatched

        // WAS_OS_Mst_NNT_Get:
        public const string WAS_OS_Mst_NNT_Get = "ErridnInventory.WAS_OS_Mst_NNT_Get"; //// //WAS_OS_Mst_NNT_Get

		// Mst_NNT_Get:
		public const string Mst_NNT_Get = "ErridnInventory.Mst_NNT_Get"; //// //Mst_NNT_Get

		// OS_Mst_NNT_Get:
		public const string OS_Mst_NNT_Get = "ErridnInventory.OS_Mst_NNT_Get"; //// //OS_Mst_NNT_Get

		// WAS_Mst_NNT_Get:
		public const string WAS_Mst_NNT_Get = "ErridnInventory.WAS_Mst_NNT_Get"; //// //WAS_Mst_NNT_Get

        // Mst_NNT_CreateNNTAndDepartment:
        public const string Mst_NNT_CreateNNTAndDepartment = "ErridnInventory.Mst_NNT_CreateNNTAndDepartment"; //// //Mst_NNT_CreateNNTAndDepartment

        // Mst_NNT_Create:
        public const string Mst_NNT_Create = "ErridnInventory.Mst_NNT_Create"; //// //Mst_NNT_Create
        public const string Mst_NNT_Create_InvalidMST = "ErridnInventory.Mst_NNT_Create_InvalidNNTCode"; //// //Mst_NNT_Create_InvalidNNTCode
        public const string Mst_NNT_Create_InvalidNNTFullName = "ErridnInventory.Mst_NNT_Create_InvalidNNTFullName"; //// //Mst_NNT_Create_InvalidNNTFullName
        public const string Mst_NNT_Create_InvalidNNTAddress = "ErridnInventory.Mst_NNT_Create_InvalidNNTAddress"; //// //Mst_NNT_Create_InvalidNNTAddress
        public const string Mst_NNT_Create_InvalidPresentBy = "ErridnInventory.Mst_NNT_Create_InvalidPresentBy"; //// //Mst_NNT_Create_InvalidPresentBy
        public const string Mst_NNT_Create_InvalidNNTPosition = "ErridnInventory.Mst_NNT_Create_InvalidNNTPosition"; //// //Mst_NNT_Create_InvalidNNTPosition
        public const string Mst_NNT_Create_InvalidContactName = "ErridnInventory.Mst_NNT_Create_InvalidContactName"; //// //Mst_NNT_Create_InvalidContactName
        public const string Mst_NNT_Create_InvalidContactPhone = "ErridnInventory.Mst_NNT_Create_InvalidContactPhone"; //// //Mst_NNT_Create_InvalidContactPhone
        public const string Mst_NNT_Create_InvalidContactEmail = "ErridnInventory.Mst_NNT_CMst_NNT_Create_InvalidContactEmailreate_Invalid"; //// //Mst_NNT_Create_InvalidContactEmail
        public const string Mst_NNT_Create_InvalidTotalNetworkID = "ErridnInventory.Mst_NNT_Create_InvalidTotalNetworkID"; //// //Mst_NNT_Create_InvalidTotalNetworkID

        // WAS_Mst_NNT_Create:
        public const string WAS_Mst_NNT_Create = "ErridnInventory.WAS_Mst_NNT_Create"; //// //WAS_Mst_NNT_Create

        // WA_Mst_NNT_Registry:
        public const string WA_Mst_NNT_Registry = "ErridnInventory.WA_Mst_NNT_Registry"; //// //WA_Mst_NNT_Registry

        // Mst_NNT_Update:
        public const string Mst_NNT_Update = "ErridnInventory.Mst_NNT_Update"; //// //Mst_NNT_Update                
        public const string Mst_NNT_Update_InvalidMST = "ErridnInventory.Mst_NNT_Update_InvalidNNTCode"; //// //Mst_NNT_Update_InvalidNNTCode
        public const string Mst_NNT_Update_InvalidNNTFullName = "ErridnInventory.Mst_NNT_Update_InvalidNNTFullName"; //// //Mst_NNT_Update_InvalidNNTFullName
        public const string Mst_NNT_Update_InvalidNNTAddress = "ErridnInventory.Mst_NNT_Update_InvalidNNTAddress"; //// //Mst_NNT_Update_InvalidNNTAddress
        public const string Mst_NNT_Update_InvalidPresentBy = "ErridnInventory.Mst_NNT_Update_InvalidPresentBy"; //// //Mst_NNT_Update_InvalidPresentBy
        public const string Mst_NNT_Update_InvalidNNTPosition = "ErridnInventory.Mst_NNT_Update_InvalidNNTPosition"; //// //Mst_NNT_Update_InvalidNNTPosition
        public const string Mst_NNT_Update_InvalidContactName = "ErridnInventory.Mst_NNT_Update_InvalidContactName"; //// //Mst_NNT_Update_InvalidContactName
        public const string Mst_NNT_Update_InvalidContactPhone = "ErridnInventory.Mst_NNT_Update_InvalidContactPhone"; //// //Mst_NNT_Update_InvalidContactPhone
        public const string Mst_NNT_Update_InvalidContactEmail = "ErridnInventory.Mst_NNT_CMst_NNT_Update_InvalidContactEmailreate_Invalid"; //// //Mst_NNT_Update_InvalidContactEmail
        ////
        public const string Mst_NNT_Update_InvalidBizType = "ErridnInventory.Mst_NNT_Update_InvalidBizType"; //// //Mst_NNT_Update_InvalidBizType
        public const string Mst_NNT_Update_InvalidBizFieldCode = "ErridnInventory.Mst_NNT_Update_InvalidBizFieldCode"; //// //Mst_NNT_Update_InvalidBizFieldCode
        public const string Mst_NNT_Update_InvalidBizSizeCode = "ErridnInventory.Mst_NNT_Update_InvalidBizSizeCode"; //// //Mst_NNT_Update_InvalidBizSizeCode

        // Mst_NNT_UpdateRegisterStatus:
        public const string Mst_NNT_UpdateRegisterStatus = "ErridnInventory.Mst_NNT_UpdateRegisterStatus"; //// //Mst_NNT_UpdateRegisterStatus                

		// WAS_Mst_NNT_UpdateRegisterStatus:
		public const string WAS_Mst_NNT_UpdateRegisterStatus = "ErridnInventory.WAS_Mst_NNT_UpdateRegisterStatus"; //// //WAS_Mst_NNT_UpdateRegisterStatus                

		// Mst_NNT_RegisterStatusAppr:
		public const string Mst_NNT_RegisterStatusAppr = "ErridnInventory.Mst_NNT_RegisterStatusAppr"; //// //Mst_NNT_RegisterStatusAppr                

		// WAS_Mst_NNT_RegisterStatusAppr:
		public const string WAS_Mst_NNT_RegisterStatusAppr = "ErridnInventory.WAS_Mst_NNT_RegisterStatusAppr"; //// //WAS_Mst_NNT_RegisterStatusAppr                


		// WAS_Mst_NNT_Update:
		public const string WAS_Mst_NNT_Update = "ErridnInventory.WAS_Mst_NNT_Update"; //// //WAS_Mst_NNT_Update

        // Mst_NNT_Delete:
        public const string Mst_NNT_Delete = "ErridnInventory.Mst_NNT_Delete"; //// Mã lỗi: Mst_NNT_Delete ////
        public const string RptSv_Mst_NNT_Delete = "ErridnInventory.RptSv_Mst_NNT_Delete"; //// Mã lỗi: RptSv_Mst_NNT_Delete ////

        // WAS_Mst_NNT_Delete:
        public const string WAS_Mst_NNT_Delete = "ErridnInventory.WAS_Mst_NNT_Delete"; //// //WAS_Mst_NNT_Delete
        public const string WAS_RptSv_Mst_NNT_Delete = "ErridnInventory.WAS_RptSv_Mst_NNT_Delete"; //// //WAS_RptSv_Mst_NNT_Delete

        // Mst_NNT_UpdServiceStop:
        public const string Mst_NNT_UpdServiceStop = "ErridnInventory.Mst_NNT_UpdServiceStop"; //// //Mst_NNT_UpdServiceStop
        public const string Mst_NNT_UpdServiceStop_InvalidServiceStopStatus = "ErridnInventory.Mst_NNT_UpdServiceStop_InvalidServiceStopStatus"; //// //Mst_NNT_UpdServiceStop_InvalidServiceStopStatus
        public const string Mst_NNT_UpdServiceStop_InvalidReqStopDateBeforeSysDate = "ErridnInventory.Mst_NNT_UpdServiceStop_InvalidReqStopDateBeforeSysDate"; //// //Mst_NNT_UpdServiceStop_InvalidReqStopDateBeforeSysDate
        public const string Mst_NNT_UpdServiceStop_ExistRegServiceStop = "ErridnInventory.Mst_NNT_UpdServiceStop_ExistRegServiceStop"; //// //Mst_NNT_UpdServiceStop_ExistRegServiceStop

        // WAS_Mst_NNT_UpdServiceStop:
        public const string WAS_Mst_NNT_UpdServiceStop = "ErridnInventory.WAS_Mst_NNT_UpdServiceStop"; //// //WAS_Mst_NNT_UpdServiceStop

        // Mst_NNT_MSTChild_Registry:
        public const string Mst_NNT_MSTChild_Registry = "ErridnInventory.Mst_NNT_MSTChild_Registry"; //// //Mst_NNT_MSTChild_Registry
        public const string Mst_NNT_MSTChild_Registry_InvalidFlagSysAdmin_User = "ErridnInventory.Mst_NNT_MSTChild_Registry_InvalidFlagSysAdmin_User"; //// //Mst_NNT_MSTChild_Registry_InvalidFlagSysAdmin_User

        // WA_Mst_NNT_MSTChild_Registry:
        public const string WA_Mst_NNT_MSTChild_Registry = "ErridnInventory.WA_Mst_NNT_MSTChild_Registry"; //// //WA_Mst_NNT_MSTChild_Registry

        // RptSv:
        public const string RptSv_Mst_NNT_UpdateRegisterStatus = "ErridnInventory.RptSv_Mst_NNT_UpdateRegisterStatus"; //// //RptSv_Mst_NNT_UpdateRegisterStatus
        public const string WAS_RptSv_Mst_NNT_UpdateRegisterStatus = "ErridnInventory.WAS_RptSv_Mst_NNT_UpdateRegisterStatus"; //// //WAS_RptSv_Mst_NNT_UpdateRegisterStatus
        public const string RptSv_Mst_NNT_Update = "ErridnInventory.RptSv_Mst_NNT_Update"; //// //RptSv_Mst_NNT_Update
        public const string WAS_RptSv_Mst_NNT_Update = "ErridnInventory.WAS_RptSv_Mst_NNT_Update"; //// //WAS_RptSv_Mst_NNT_Update

        //Mst_NNT_CreateForNetwork
        public const string Mst_NNT_CreateForNetwork = "ErridnInventory.Mst_NNT_CreateForNetwork"; //// // Mst_NNT_CreateForNetwork

        // WAS_Mst_NNT_CreateForNetwork
        public const string WAS_Mst_NNT_CreateForNetwork = "ErridnInventory.WAS_Mst_NNT_CreateForNetwork"; //// //WAS_Mst_NNT_CreateForNetwork

		// WAS_RptSv_Mst_NNT_CalcByUserExist:
		public const string WAS_RptSv_Mst_NNT_CalcByUserExist = "ErridnInventory.WAS_RptSv_Mst_NNT_CalcByUserExist"; //// //WAS_RptSv_Mst_NNT_CalcByUserExist

		// WAS_RptSv_Mst_NNT_AddByUserExist:
		public const string WAS_RptSv_Mst_NNT_AddByUserExist = "ErridnInventory.WAS_RptSv_Mst_NNT_AddByUserExist"; //// //WAS_RptSv_Mst_NNT_AddByUserExist

		// WAS_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist:
		public const string WAS_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist = "ErridnInventory.WAS_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist"; //// //WAS_OS_MstSvCenter_RptSv_Mst_NNT_AddByUserExist

		// RptSv_Mst_NNT_CreateForNetwork:
		public const string RptSv_Mst_NNT_CreateForNetwork = "ErridnInventory.RptSv_Mst_NNT_CreateForNetwork"; //// //RptSv_Mst_NNT_CreateForNetwork

		// WAS_RptSv_Mst_NNT_CreateForNetwork:
		public const string WAS_RptSv_Mst_NNT_CreateForNetwork = "ErridnInventory.WAS_RptSv_Mst_NNT_CreateForNetwork"; //// //WAS_RptSv_Mst_NNT_CreateForNetwork

		// OS_RptSv_Mst_NNT_Create:
		public const string OS_RptSv_Mst_NNT_Create = "ErridnInventory.OS_RptSv_Mst_NNT_Create"; //// //OS_RptSv_Mst_NNT_Create

		#endregion

		#region // Mst_TransactionType:
		// Mst_TransactionType_CheckDB:
		public const string Mst_TransactionType_CheckDB_TransactionTypeNotFound = "ErridnInventory.Mst_TransactionType_CheckDB_TransactionTypeNotFound"; //// //Mst_TransactionType_CheckDB_TransactionTypeNotFound
        public const string Mst_TransactionType_CheckDB_TransactionTypeExist = "ErridnInventory.Mst_TransactionType_CheckDB_TransactionTypeExist"; //// //Mst_TransactionType_CheckDB_TransactionTypeExist
        public const string Mst_TransactionType_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_TransactionType_CheckDB_FlagActiveNotMatched"; //// //Mst_TransactionType_CheckDB_FlagActiveNotMatched

        // Mst_TransactionType_Get:
        public const string Mst_TransactionType_Get = "ErridnInventory.Mst_TransactionType_Get"; //// //Mst_TransactionType_Get

        // WAS_Mst_TransactionType_Get:
        public const string WAS_Mst_TransactionType_Get = "ErridnInventory.WAS_Mst_TransactionType_Get"; //// //WAS_Mst_TransactionType_Get
		#endregion

		#region // Transaction_KeKhaiThue:
		// Transaction_KeKhaiThue_CheckDB:
		public const string Transaction_KeKhaiThue_CheckDB_TransactionTypeNotFound = "ErridnInventory.Transaction_KeKhaiThue_CheckDB_TransactionTypeNotFound"; //// //Transaction_KeKhaiThue_CheckDB_TransactionTypeNotFound
		public const string Transaction_KeKhaiThue_CheckDB_TransactionTypeExist = "ErridnInventory.Transaction_KeKhaiThue_CheckDB_TransactionTypeExist"; //// //Transaction_KeKhaiThue_CheckDB_TransactionTypeExist
		public const string Transaction_KeKhaiThue_CheckDB_TranStatusNotMatched = "ErridnInventory.Transaction_KeKhaiThue_CheckDB_TranStatusNotMatched"; //// //Transaction_KeKhaiThue_CheckDB_TranStatusNotMatched
        public const string Transaction_KeKhaiThue_CheckDB_TransactionExist = "ErridnInventory.Transaction_KeKhaiThue_CheckDB_TransactionExist"; //// //Transaction_KeKhaiThue_CheckDB_TransactionExist
        public const string Transaction_KeKhaiThue_CheckDB_TransactionNotFound = "ErridnInventory.Transaction_KeKhaiThue_CheckDB_TransactionNotFound"; //// //Transaction_KeKhaiThue_CheckDB_TransactionNotFound

        // Transaction_KeKhaiThue_Get:
        public const string Transaction_KeKhaiThue_Get = "ErridnInventory.Transaction_KeKhaiThue_Get"; //// //Transaction_KeKhaiThue_Get

		// WAS_Transaction_KeKhaiThue_Get:
		public const string WAS_Transaction_KeKhaiThue_Get = "ErridnInventory.WAS_Transaction_KeKhaiThue_Get"; //// //WAS_Transaction_KeKhaiThue_Get

		// Transaction_KeKhaiThue_Add:
		public const string Transaction_KeKhaiThue_Add = "ErridnInventory.Transaction_KeKhaiThue_Add"; //// //Transaction_KeKhaiThue_Add
		public const string Transaction_KeKhaiThue_Add_Input_Transaction_KeKhaiThueTblNotFound = "ErridnInventory.Transaction_KeKhaiThue_Add_Input_Transaction_KeKhaiThueTblNotFound"; //// //Transaction_KeKhaiThue_Add_Input_Transaction_KeKhaiThueTblNotFound

		// WAS_Transaction_KeKhaiThue_Add:
		public const string WAS_Transaction_KeKhaiThue_Add = "ErridnInventory.WAS_Transaction_KeKhaiThue_Add"; //// //WAS_Transaction_KeKhaiThue_Add


		// Transaction_KeKhaiThue_Add:
		public const string Transaction_KeKhaiThue_AddMulti = "ErridnInventory.Transaction_KeKhaiThue_AddMulti"; //// //Transaction_KeKhaiThue_AddMulti
		public const string Transaction_KeKhaiThue_AddMulti_Input_Transaction_KeKhaiThueTblNotFound = "ErridnInventory.Transaction_KeKhaiThue_AddMulti_Input_Transaction_KeKhaiThueTblNotFound"; //// //Transaction_KeKhaiThue_AddMulti_Input_Transaction_KeKhaiThueTblNotFound

		// WAS_Transaction_KeKhaiThue_AddMulti:
		public const string WAS_Transaction_KeKhaiThue_AddMulti = "ErridnInventory.WAS_Transaction_KeKhaiThue_AddMulti"; //// //WAS_Transaction_KeKhaiThue_AddMulti

		// Transaction_KeKhaiThue_Update:
		public const string Transaction_KeKhaiThue_Update = "ErridnInventory.Transaction_KeKhaiThue_Update"; //// //Transaction_KeKhaiThue_Update
        public const string Transaction_KeKhaiThue_Update_InvalidStatus = "ErridnInventory.Transaction_KeKhaiThue_Update_InvalidStatus"; //// //Transaction_KeKhaiThue_Update_InvalidStatus
               
        // Transaction_KeKhaiThue_UpdateS1:
        public const string Transaction_KeKhaiThue_UpdateS1 = "ErridnInventory.Transaction_KeKhaiThue_UpdateS1"; //// //Transaction_KeKhaiThue_UpdateS1
        public const string Transaction_KeKhaiThue_UpdateS1_InvalidStatus = "ErridnInventory.Transaction_KeKhaiThue_UpdateS1_InvalidStatus"; //// //Transaction_KeKhaiThue_UpdateS1_InvalidStatus

        // Transaction_KeKhaiThue_UpdateS2:
        public const string Transaction_KeKhaiThue_UpdateS2 = "ErridnInventory.Transaction_KeKhaiThue_UpdateS2"; //// //Transaction_KeKhaiThue_UpdateS2
        public const string Transaction_KeKhaiThue_UpdateS2_InvalidStatus = "ErridnInventory.Transaction_KeKhaiThue_UpdateS2_InvalidStatus"; //// //Transaction_KeKhaiThue_UpdateS2_InvalidStatus

        // Transaction_KeKhaiThue_UpdateS3:
        public const string Transaction_KeKhaiThue_UpdateS3 = "ErridnInventory.Transaction_KeKhaiThue_UpdateS3"; //// //Transaction_KeKhaiThue_UpdateS3
        public const string Transaction_KeKhaiThue_UpdateS3_InvalidStatus = "ErridnInventory.Transaction_KeKhaiThue_UpdateS3_InvalidStatus"; //// //Transaction_KeKhaiThue_UpdateS3_InvalidStatus

        // Transaction_KeKhaiThue_UpdateS4:
        public const string Transaction_KeKhaiThue_UpdateS4 = "ErridnInventory.Transaction_KeKhaiThue_UpdateS4"; //// //Transaction_KeKhaiThue_UpdateS4
        public const string Transaction_KeKhaiThue_UpdateS4_InvalidStatus = "ErridnInventory.Transaction_KeKhaiThue_UpdateS4_InvalidStatus"; //// //Transaction_KeKhaiThue_UpdateS4_InvalidStatus

        // Transaction_KeKhaiThue_UpdateS5:
        public const string Transaction_KeKhaiThue_UpdateS5 = "ErridnInventory.Transaction_KeKhaiThue_UpdateS5"; //// //Transaction_KeKhaiThue_UpdateS5
        public const string Transaction_KeKhaiThue_UpdateS5_InvalidStatus = "ErridnInventory.Transaction_KeKhaiThue_UpdateS5_InvalidStatus"; //// //Transaction_KeKhaiThue_UpdateS5_InvalidStatus

        // Transaction_KeKhaiThue_GetForTraKQuaGDich:
        public const string Transaction_KeKhaiThue_GetForTraKQuaGDich = "ErridnInventory.Transaction_KeKhaiThue_GetForTraKQuaGDich"; //// //Transaction_KeKhaiThue_GetForTraKQuaGDich

        // WAS_Transaction_KeKhaiThue_GetForTraKQuaGDich:
        public const string WAS_Transaction_KeKhaiThue_GetForTraKQuaGDich = "ErridnInventory.WAS_Transaction_KeKhaiThue_GetForTraKQuaGDich"; //// //WAS_Transaction_KeKhaiThue_GetForTraKQuaGDich

        #endregion

        #region // Tax_RegBalance:
        // Tax_RegBalance_Get:
        public const string Tax_RegBalance_Get = "ErridnInventory.Tax_RegBalance_Get"; //// //Tax_RegBalance_Get

        // WAS_Tax_RegBalance_Get:
        public const string WAS_Tax_RegBalance_Get = "ErridnInventory.WAS_Tax_RegBalance_Get"; //// //WAS_Tax_RegBalance_Get

        // Tax_RegBalance_AddMulti:
        public const string Tax_RegBalance_AddMulti_Input_RegBalanceTblNotFound = "ErridnInventory.Tax_RegBalance_AddMulti_Input_RegBalanceTblNotFound"; //// //Tax_RegBalance_AddMulti_Input_RegBalanceTblNotFound

        // Tax_RegBalance_DelMulti:
        public const string Tax_RegBalance_DelMulti_Input_RegBalanceTblNotFound = "ErridnInventory.Tax_RegBalance_DelMulti_Input_RegBalanceTblNotFound"; //// //Tax_RegBalance_DelMulti_Input_RegBalanceTblNotFound

        #endregion

        #region // Budget_BudgetPlan:
        // Budget_BudgetPlan_CheckDB:
        public const string Budget_BudgetPlan_CheckDB_BudgetPlanNotFound = "ErridnInventory.Budget_BudgetPlan_CheckDB_BudgetPlanNotFound"; //// //Budget_BudgetPlan_CheckDB_BudgetPlanNotFound
		public const string Budget_BudgetPlan_CheckDB_BudgetPlanExist = "ErridnInventory.Budget_BudgetPlan_CheckDB_BudgetPlanExist"; //// //Budget_BudgetPlan_CheckDB_BudgetPlanExist
		public const string Budget_BudgetPlan_CheckDB_StatusNotMatched = "ErridnInventory.Budget_BudgetPlan_CheckDB_StatusNotMatched"; //// //Budget_BudgetPlan_CheckDB_StatusNotMatched

		// Budget_BudgetPlan_Get:
		public const string Budget_BudgetPlan_Get = "ErridnInventory.Budget_BudgetPlan_Get"; //// //Budget_BudgetPlan_Get

		// WAS_Budget_BudgetPlan_Get:
		public const string WAS_Budget_BudgetPlan_Get = "ErridnInventory.WAS_Budget_BudgetPlan_Get"; //// //WAS_Budget_BudgetPlan_Get

		// WAS_Budget_BudgetPlan_Save:
		public const string WAS_Budget_BudgetPlan_Save = "ErridnInventory.WAS_Budget_BudgetPlan_Save"; //// //WAS_Budget_BudgetPlan_Save


		// Budget_BudgetPlan_Save:
		public const string Budget_BudgetPlan_Save = "ErridnInventory.Budget_BudgetPlan_Save"; //// //Budget_BudgetPlan_Save
		public const string Budget_BudgetPlan_Save_InvalidBGStatus = "ErridnInventory.Budget_BudgetPlan_Save_InvalidBGStatus"; //// //Budget_BudgetPlan_Save_InvalidBGStatus
		public const string Budget_BudgetPlan_Save_InvalidValue = "ErridnInventory.Budget_BudgetPlan_Save_InvalidValue"; //// //Budget_BudgetPlan_Save_InvalidValue
		public const string Budget_BudgetPlan_Save_InvalidYear = "ErridnInventory.Budget_BudgetPlan_Save_InvalidYear"; //// //Budget_BudgetPlan_Save_InvalidYear
		public const string Budget_BudgetPlan_Save_InvalidSignedDate = "ErridnInventory.Budget_BudgetPlan_Save_InvalidSignedDate"; //// //Budget_BudgetPlan_Save_InvalidSignedDate
		public const string Budget_BudgetPlan_Save_InvalidSellerCode = "ErridnInventory.Budget_BudgetPlan_Save_InvalidSellerCode"; //// //Budget_BudgetPlan_Save_InvalidSellerCode
		public const string Budget_BudgetPlan_Save_Input_Budget_BudgetPlanDtlTblNotFound = "ErridnInventory.Budget_BudgetPlan_Save_Input_Budget_BudgetPlanDtlTblNotFound"; //// //Budget_BudgetPlan_Save_Input_Budget_BudgetPlanDtlTblNotFound		
		public const string Budget_BudgetPlan_Save_Input_Budget_BudgetPlanDtlTblInvalid = "ErridnInventory.Budget_BudgetPlan_Save_Input_Budget_BudgetPlanDtlTblInvalid"; //// //Budget_BudgetPlan_Save_Input_Budget_BudgetPlanDtlTblInvalid		
		public const string Budget_BudgetPlan_Save_Input_InvalidOrganCode = "ErridnInventory.Budget_BudgetPlan_Save_Input_InvalidOrganCode"; //// //Budget_BudgetPlan_Save_Input_InvalidOrganCode		

		// Budget_BudgetPlan_Approve:
		public const string Budget_BudgetPlan_Approve = "ErridnInventory.Budget_BudgetPlan_Approve"; //// //Budget_BudgetPlan_Approve
		public const string Budget_BudgetPlan_Approve_BudgetPlanDtlNotFound = "ErridnInventory.Budget_BudgetPlan_Approve_BudgetPlanDtlNotFound"; //// //Budget_BudgetPlan_Approve_BudgetPlanDtlNotFound

		// Budget_BudgetPlan_UpdAppr:
		public const string Budget_BudgetPlan_UpdAppr = "ErridnInventory.Budget_BudgetPlan_UpdAppr"; //// //Budget_BudgetPlan_UpdAppr
		public const string Budget_BudgetPlan_UpdAppr_InvalidBGStatus = "ErridnInventory.Budget_BudgetPlan_UpdAppr_InvalidBGStatus"; //// //Budget_BudgetPlan_UpdAppr_InvalidBGStatus
		public const string Budget_BudgetPlan_UpdAppr_InvalidValue = "ErridnInventory.Budget_BudgetPlan_UpdAppr_InvalidValue"; //// //Budget_BudgetPlan_UpdAppr_InvalidValue
		public const string Budget_BudgetPlan_UpdAppr_InvalidSignedDate = "ErridnInventory.Budget_BudgetPlan_UpdAppr_InvalidSignedDate"; //// //Budget_BudgetPlan_UpdAppr_InvalidSignedDate
		public const string Budget_BudgetPlan_UpdAppr_InvalidSellerCode = "ErridnInventory.Budget_BudgetPlan_UpdAppr_InvalidSellerCode"; //// //Budget_BudgetPlan_UpdAppr_InvalidSellerCode
		public const string Budget_BudgetPlan_UpdAppr_Input_Budget_BudgetPlanDtlTblNotFound = "ErridnInventory.Budget_BudgetPlan_UpdAppr_Input_Budget_BudgetPlanDtlTblNotFound"; //// //Budget_BudgetPlan_UpdAppr_Input_Budget_BudgetPlanDtlTblNotFound		
		public const string Budget_BudgetPlan_UpdAppr_Input_Budget_BudgetPlanDtlTblInvalid = "ErridnInventory.Budget_BudgetPlan_UpdAppr_Input_Budget_BudgetPlanDtlTblInvalid"; //// //Budget_BudgetPlan_UpdAppr_Input_Budget_BudgetPlanDtlTblInvalid		
		#endregion

		#region // Budget_BudgetPlanDtlDtl:
		// Budget_BudgetPlanDtl_CheckDB:
		public const string Budget_BudgetPlanDtl_CheckDB_BudgetPlanDtlNotFound = "ErridnInventory.Budget_BudgetPlanDtl_CheckDB_BudgetPlanDtlNotFound"; //// //Budget_BudgetPlanDtl_CheckDB_BudgetPlanDtlNotFound
		public const string Budget_BudgetPlanDtl_CheckDB_BudgetPlanDtlExist = "ErridnInventory.Budget_BudgetPlanDtl_CheckDB_BudgetPlanDtlExist"; //// //Budget_BudgetPlanDtl_CheckDB_BudgetPlanDtlExist
		public const string Budget_BudgetPlanDtl_CheckDB_StatusNotMatched = "ErridnInventory.Budget_BudgetPlanDtl_CheckDB_StatusNotMatched"; //// //Budget_BudgetPlanDtl_CheckDB_StatusNotMatched

		// Budget_BudgetPlanDtl_Get:
		public const string Budget_BudgetPlanDtl_Get = "ErridnInventory.Budget_BudgetPlanDtl_Get"; //// //Budget_BudgetPlanDtl_Get

		// WAS_Budget_BudgetPlanDtl_Get:
		public const string WAS_Budget_BudgetPlanDtl_Get = "ErridnInventory.WAS_Budget_BudgetPlanDtl_Get"; //// //WAS_Budget_BudgetPlanDtl_Get

		#endregion

		#region // Asset_Asset:
		// Asset_Asset_CheckDB:
		public const string Asset_Asset_CheckDB_AssetNotFound = "ErridnInventory.Asset_Asset_CheckDB_AssetNotFound"; //// //Asset_Asset_CheckDB_AssetNotFound
		public const string Asset_Asset_CheckDB_AssetExist = "ErridnInventory.Asset_Asset_CheckDB_AssetExist"; //// //Asset_Asset_CheckDB_AssetExist
		public const string Asset_Asset_CheckDB_StatusNotMatched = "ErridnInventory.Asset_Asset_CheckDB_StatusNotMatched"; //// //Asset_Asset_CheckDB_StatusNotMatched

		// Asset_Asset_Get:
		public const string Asset_Asset_Get = "ErridnInventory.Asset_Asset_Get"; //// //Asset_Asset_Get

		// Asset_Asset_Update:
		public const string Asset_Asset_Update = "ErridnInventory.Asset_Asset_Update"; //// //Asset_Asset_Update
		public const string Asset_Asset_Update_InvalidAssetName = "ErridnInventory.Asset_Asset_Update_InvalidAssetName"; //// //Asset_Asset_Update_InvalidAssetName
		public const string Asset_Asset_Update_InvalidValAsset = "ErridnInventory.Asset_Asset_Update_InvalidValAsset"; //// //Asset_Asset_Update_InvalidValAsset

		// Asset_Asset_Delete:
		public const string Asset_Asset_Delete = "ErridnInventory.Asset_Asset_Delete"; //// //Asset_Asset_Delete
		public const string Asset_Asset_Delete_InvalidAssetType = "ErridnInventory.Asset_Asset_Delete_InvalidAssetType"; //// //Asset_Asset_Delete_InvalidAssetType

		// Asset_Asset_InActive:
		public const string Asset_Asset_InActive = "ErridnInventory.Asset_Asset_InActive"; //// //Asset_Asset_InActive

		// WAS_Asset_Asset_Get:
		public const string WAS_Asset_Asset_Get = "ErridnInventory.WAS_Asset_Asset_Get"; //// //WAS_Asset_Asset_Get

		// WAS_Asset_Asset_Save:
		public const string WAS_Asset_Asset_Save = "ErridnInventory.WAS_Asset_Asset_Save"; //// //WAS_Asset_Asset_Save

		// WAS_Asset_Asset_InActive:
		public const string WAS_Asset_Asset_InActive = "ErridnInventory.WAS_Asset_Asset_InActive"; //// //WAS_Asset_Asset_InActive


		// Asset_Asset_Save:
		public const string Asset_Asset_Save = "ErridnInventory.Asset_Asset_Save"; //// //Asset_Asset_Save
		public const string Asset_Asset_Save_InvalidBGStatus = "ErridnInventory.Asset_Asset_Save_InvalidBGStatus"; //// //Asset_Asset_Save_InvalidBGStatus
		public const string Asset_Asset_Save_InvalidValue = "ErridnInventory.Asset_Asset_Save_InvalidValue"; //// //Asset_Asset_Save_InvalidValue
		public const string Asset_Asset_Save_InvalidYear = "ErridnInventory.Asset_Asset_Save_InvalidYear"; //// //Asset_Asset_Save_InvalidYear
		public const string Asset_Asset_Save_InvalidSignedDate = "ErridnInventory.Asset_Asset_Save_InvalidSignedDate"; //// //Asset_Asset_Save_InvalidSignedDate
		public const string Asset_Asset_Save_InvalidSellerCode = "ErridnInventory.Asset_Asset_Save_InvalidSellerCode"; //// //Asset_Asset_Save_InvalidSellerCode
		public const string Asset_Asset_Save_Input_Asset_AssetDtlTblNotFound = "ErridnInventory.Asset_Asset_Save_Input_Asset_AssetDtlTblNotFound"; //// //Asset_Asset_Save_Input_Asset_AssetDtlTblNotFound		
		public const string Asset_Asset_Save_Input_Asset_AssetDtlTblInvalid = "ErridnInventory.Asset_Asset_Save_Input_Asset_AssetDtlTblInvalid"; //// //Asset_Asset_Save_Input_Asset_AssetDtlTblInvalid		
		public const string Asset_Asset_Save_Input_InvalidOrganCode = "ErridnInventory.Asset_Asset_Save_Input_InvalidOrganCode"; //// //Asset_Asset_Save_Input_InvalidOrganCode		

		// Asset_Asset_Add:
		public const string Asset_Asset_Add = "ErridnInventory.Asset_Asset_Add"; //// //Asset_Asset_Add
		public const string Asset_Asset_Add_InvalidBGStatus = "ErridnInventory.Asset_Asset_Add_InvalidBGStatus"; //// //Asset_Asset_Add_InvalidBGStatus
		public const string Asset_Asset_Add_InvalidValue = "ErridnInventory.Asset_Asset_Add_InvalidValue"; //// //Asset_Asset_Add_InvalidValue
		public const string Asset_Asset_Add_InvalidYear = "ErridnInventory.Asset_Asset_Add_InvalidYear"; //// //Asset_Asset_Add_InvalidYear
		public const string Asset_Asset_Add_InvalidSignedDate = "ErridnInventory.Asset_Asset_Add_InvalidSignedDate"; //// //Asset_Asset_Add_InvalidSignedDate
		public const string Asset_Asset_Add_InvalidSellerCode = "ErridnInventory.Asset_Asset_Add_InvalidSellerCode"; //// //Asset_Asset_Add_InvalidSellerCode
		public const string Asset_Asset_Add_Input_Asset_AssetTblNotFound = "ErridnInventory.Asset_Asset_Add_Input_Asset_AssetTblNotFound"; //// //Asset_Asset_Add_Input_Asset_AssetTblNotFound		
		public const string Asset_Asset_Add_Input_Asset_AssetTblInvalid = "ErridnInventory.Asset_Asset_Add_Input_Asset_AssetTblInvalid"; //// //Asset_Asset_Add_Input_Asset_AssetTblInvalid		
		public const string Asset_Asset_Add_Input_InvalidAssetType = "ErridnInventory.Asset_Asset_Add_Input_InvalidAssetType"; //// //Asset_Asset_Add_Input_InvalidAssetType		
		public const string Asset_Asset_Add_Input_InvalidOrganCode = "ErridnInventory.Asset_Asset_Add_Input_InvalidOrganCode"; //// //Asset_Asset_Add_Input_InvalidOrganCode		

		// WAS_Asset_Asset_Add:
		public const string WAS_Asset_Asset_Add = "ErridnInventory.WAS_Asset_Asset_Add"; //// //WAS_Asset_Asset_Add

		// WAS_Asset_Asset_Update:
		public const string WAS_Asset_Asset_Update = "ErridnInventory.WAS_Asset_Asset_Update"; //// //WAS_Asset_Asset_Update

		// WAS_Asset_Asset_Delete:
		public const string WAS_Asset_Asset_Delete = "ErridnInventory.WAS_Asset_Asset_Delete"; //// //WAS_Asset_Asset_Delete

		// Asset_Asset_Approve:
		public const string Asset_Asset_Approve = "ErridnInventory.Asset_Asset_Approve"; //// //Asset_Asset_Approve
		public const string Asset_Asset_Approve_AssetDtlNotFound = "ErridnInventory.Asset_Asset_Approve_AssetDtlNotFound"; //// //Asset_Asset_Approve_AssetDtlNotFound

		// Asset_Asset_UpdAppr:
		public const string Asset_Asset_UpdAppr = "ErridnInventory.Asset_Asset_UpdAppr"; //// //Asset_Asset_UpdAppr
		public const string Asset_Asset_UpdAppr_InvalidBGStatus = "ErridnInventory.Asset_Asset_UpdAppr_InvalidBGStatus"; //// //Asset_Asset_UpdAppr_InvalidBGStatus
		public const string Asset_Asset_UpdAppr_InvalidValue = "ErridnInventory.Asset_Asset_UpdAppr_InvalidValue"; //// //Asset_Asset_UpdAppr_InvalidValue
		public const string Asset_Asset_UpdAppr_InvalidSignedDate = "ErridnInventory.Asset_Asset_UpdAppr_InvalidSignedDate"; //// //Asset_Asset_UpdAppr_InvalidSignedDate
		public const string Asset_Asset_UpdAppr_InvalidSellerCode = "ErridnInventory.Asset_Asset_UpdAppr_InvalidSellerCode"; //// //Asset_Asset_UpdAppr_InvalidSellerCode
		public const string Asset_Asset_UpdAppr_Input_Asset_AssetDtlTblNotFound = "ErridnInventory.Asset_Asset_UpdAppr_Input_Asset_AssetDtlTblNotFound"; //// //Asset_Asset_UpdAppr_Input_Asset_AssetDtlTblNotFound		
		public const string Asset_Asset_UpdAppr_Input_Asset_AssetDtlTblInvalid = "ErridnInventory.Asset_Asset_UpdAppr_Input_Asset_AssetDtlTblInvalid"; //// //Asset_Asset_UpdAppr_Input_Asset_AssetDtlTblInvalid		
		#endregion

		#region // Asset_CreditHist:
		// Asset_CreditHist_Get:
		public const string Asset_CreditHist_Get = "ErridnInventory.Asset_CreditHist_Get"; //// //Asset_CreditHist_Get

		// WAS_Asset_CreditHist_Get:
		public const string WAS_Asset_CreditHist_Get = "ErridnInventory.WAS_Asset_CreditHist_Get"; //// //WAS_Asset_CreditHist_Get

		#endregion

		#region // CrCt_CreditContractDtl:
		// CrCt_CreditContractDtl_Get:
		public const string CrCt_CreditContractDtl_Get = "ErridnInventory.CrCt_CreditContractDtl_Get"; //// //CrCt_CreditContractDtl_Get

		// WAS_CrCt_CreditContractDtl_Get:
		public const string WAS_CrCt_CreditContractDtl_Get = "ErridnInventory.WAS_CrCt_CreditContractDtl_Get"; //// //WAS_CrCt_CreditContractDtl_Get

		#endregion

		#region // CrCt_CreditContract:
		// CrCt_CreditContract_CheckDB:
		public const string CrCt_CreditContract_CheckDB_CreditContractNotFound = "ErridnInventory.CrCt_CreditContract_CheckDB_CreditContractNotFound"; //// //CrCt_CreditContract_CheckDB_CreditContractNotFound
		public const string CrCt_CreditContract_CheckDB_CreditContractExist = "ErridnInventory.CrCt_CreditContract_CheckDB_CreditContractExist"; //// //CrCt_CreditContract_CheckDB_CreditContractExist
		public const string CrCt_CreditContract_CheckDB_StatusNotMatched = "ErridnInventory.CrCt_CreditContract_CheckDB_StatusNotMatched"; //// //CrCt_CreditContract_CheckDB_StatusNotMatched

		// CrCt_CreditContract_Get:
		public const string CrCt_CreditContract_Get = "ErridnInventory.CrCt_CreditContract_Get"; //// //CrCt_CreditContract_Get

		// WAS_CrCt_CreditContract_Get:
		public const string WAS_CrCt_CreditContract_Get = "ErridnInventory.WAS_CrCt_CreditContract_Get"; //// //WAS_CrCt_CreditContract_Get

		// WAS_CrCt_CreditContract_Save:
		public const string WAS_CrCt_CreditContract_Save = "ErridnInventory.WAS_CrCt_CreditContract_Save"; //// //WAS_CrCt_CreditContract_Save

		// WAS_CrCt_CreditContract_Add:
		public const string WAS_CrCt_CreditContract_Add = "ErridnInventory.WAS_CrCt_CreditContract_Add"; //// //WAS_CrCt_CreditContract_Add

		// WAS_CrCt_CreditContract_Upd:
		public const string WAS_CrCt_CreditContract_Upd = "ErridnInventory.WAS_CrCt_CreditContract_Upd"; //// //WAS_CrCt_CreditContract_Upd

		// WAS_CrCt_CreditContract_Del:
		public const string WAS_CrCt_CreditContract_Del = "ErridnInventory.WAS_CrCt_CreditContract_Del"; //// //WAS_CrCt_CreditContract_Del

		// WAS_CrCt_CreditContract_Release:
		public const string WAS_CrCt_CreditContract_Release = "ErridnInventory.WAS_CrCt_CreditContract_Release"; //// //WAS_CrCt_CreditContract_Release

		// WAS_CrCt_CreditContract_Mortgage:
		public const string WAS_CrCt_CreditContract_Mortgage = "ErridnInventory.WAS_CrCt_CreditContract_Mortgage"; //// //WAS_CrCt_CreditContract_Mortgage

		// WAS_CrCt_CreditContract_Refinance:
		public const string WAS_CrCt_CreditContract_Refinance = "ErridnInventory.WAS_CrCt_CreditContract_Refinance"; //// //WAS_CrCt_CreditContract_Refinance

		// WAS_CrCt_CreditContract_Exchange:
		public const string WAS_CrCt_CreditContract_Exchange = "ErridnInventory.WAS_CrCt_CreditContract_Exchange"; //// //WAS_CrCt_CreditContract_Exchange

		// CrCt_CreditContract_Check:
		public const string CrCt_CreditContract_Check_InvalidOrganUserNotFound = "ErridnInventory.CrCt_CreditContract_Check_InvalidOrganUserNotFound"; //// //CrCt_CreditContract_Check_InvalidOrganUserNotFound

		// CrCt_CreditContract_Add:
		public const string CrCt_CreditContract_Add = "ErridnInventory.CrCt_CreditContract_Add"; //// //CrCt_CreditContract_Add
		public const string CrCt_CreditContract_Add_InvalidBGStatus = "ErridnInventory.CrCt_CreditContract_Add_InvalidBGStatus"; //// //CrCt_CreditContract_Add_InvalidBGStatus
		public const string CrCt_CreditContract_Add_InvalidValue = "ErridnInventory.CrCt_CreditContract_Add_InvalidValue"; //// //CrCt_CreditContract_Add_InvalidValue
		public const string CrCt_CreditContract_Add_InvalidYear = "ErridnInventory.CrCt_CreditContract_Add_InvalidYear"; //// //CrCt_CreditContract_Add_InvalidYear
		public const string CrCt_CreditContract_Add_InvalidEffDateStart = "ErridnInventory.CrCt_CreditContract_Add_InvalidEffDateStart"; //// //CrCt_CreditContract_Add_InvalidEffDateStart
		public const string CrCt_CreditContract_Add_InvalidEffDateEnd = "ErridnInventory.CrCt_CreditContract_Add_InvalidEffDateEnd"; //// //CrCt_CreditContract_Add_InvalidEffDateEnd
		public const string CrCt_CreditContract_Add_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.CrCt_CreditContract_Add_InvalidEffDateStartBeforeEffDateEnd"; //// //CrCt_CreditContract_Add_InvalidEffDateStartBeforeEffDateEnd
		public const string CrCt_CreditContract_Add_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.CrCt_CreditContract_Add_InvalidEffDateEndAfterEffSysDate"; //// //CrCt_CreditContract_Add_InvalidEffDateEndAfterEffSysDate
		public const string CrCt_CreditContract_Add_InvalidSignedDate = "ErridnInventory.CrCt_CreditContract_Add_InvalidSignedDate"; //// //CrCt_CreditContract_Add_InvalidSignedDate
		public const string CrCt_CreditContract_Add_InvalidSellerCode = "ErridnInventory.CrCt_CreditContract_Add_InvalidSellerCode"; //// //CrCt_CreditContract_Add_InvalidSellerCode
		public const string CrCt_CreditContract_Add_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_Add_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_Add_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_Add_Input_CrCt_CreditContractDtlTblInvalid		
		public const string CrCt_CreditContract_Add_Input_CrCt_OrganTblNotFound = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_OrganTblNotFound"; //// //CrCt_CreditContract_Add_Input_CrCt_OrganTblNotFound		
		public const string CrCt_CreditContract_Add_Input_CrCt_OrganTblInvalid = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_OrganTblInvalid"; //// //CrCt_CreditContract_Add_Input_CrCt_OrganTblInvalid		
		public const string CrCt_CreditContract_Add_Input_CrCt_CreditTypeTblNotFound = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_CreditTypeTblNotFound"; //// //CrCt_CreditContract_Add_Input_CrCt_CreditTypeTblNotFound		
		public const string CrCt_CreditContract_Add_Input_CrCt_CreditTypeTblInvalid = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_CreditTypeTblInvalid"; //// //CrCt_CreditContract_Add_Input_CrCt_CreditTypeTblInvalid		
		public const string CrCt_CreditContract_Add_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_LoanPurposeTblNotFound"; //// //CrCt_CreditContract_Add_Input_CrCt_LoanPurposeTblNotFound		
		public const string CrCt_CreditContract_Add_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_LoanPurposeTblInvalid"; //// //CrCt_CreditContract_Add_Input_CrCt_LoanPurposeTblInvalid		
		public const string CrCt_CreditContract_Add_Input_CrCt_FileUploadTblNotFound = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_FileUploadTblNotFound"; //// //CrCt_CreditContract_Add_Input_CrCt_FileUploadTblNotFound		
		public const string CrCt_CreditContract_Add_Input_CrCt_FileUploadTblInvalid = "ErridnInventory.CrCt_CreditContract_Add_Input_CrCt_FileUploadTblInvalid"; //// //CrCt_CreditContract_Add_Input_CrCt_FileUploadTblInvalid
		public const string CrCt_CreditContract_Add_Input_InvalidOrganCode = "ErridnInventory.CrCt_CreditContract_Add_Input_InvalidOrganCode"; //// //CrCt_CreditContract_Add_Input_InvalidOrganCode		

		// CrCt_CreditContract_Upd:
		public const string CrCt_CreditContract_Upd = "ErridnInventory.CrCt_CreditContract_Upd"; //// //CrCt_CreditContract_Upd
		public const string CrCt_CreditContract_Upd_InvalidBGStatus = "ErridnInventory.CrCt_CreditContract_Upd_InvalidBGStatus"; //// //CrCt_CreditContract_Upd_InvalidBGStatus
		public const string CrCt_CreditContract_Upd_InvalidValue = "ErridnInventory.CrCt_CreditContract_Upd_InvalidValue"; //// //CrCt_CreditContract_Upd_InvalidValue
		public const string CrCt_CreditContract_Upd_InvalidContractSigner = "ErridnInventory.CrCt_CreditContract_Upd_InvalidContractSigner"; //// //CrCt_CreditContract_Upd_InvalidContractSigner
		public const string CrCt_CreditContract_Upd_InvalidYear = "ErridnInventory.CrCt_CreditContract_Upd_InvalidYear"; //// //CrCt_CreditContract_Upd_InvalidYear
		public const string CrCt_CreditContract_Upd_InvalidEffDateStart = "ErridnInventory.CrCt_CreditContract_Upd_InvalidEffDateStart"; //// //CrCt_CreditContract_Upd_InvalidEffDateStart
		public const string CrCt_CreditContract_Upd_InvalidEffDateEnd = "ErridnInventory.CrCt_CreditContract_Upd_InvalidEffDateEnd"; //// //CrCt_CreditContract_Upd_InvalidEffDateEnd
		public const string CrCt_CreditContract_Upd_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.CrCt_CreditContract_Upd_InvalidEffDateStartBeforeEffDateEnd"; //// //CrCt_CreditContract_Upd_InvalidEffDateStartBeforeEffDateEnd
		public const string CrCt_CreditContract_Upd_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.CrCt_CreditContract_Upd_InvalidEffDateEndAfterEffSysDate"; //// //CrCt_CreditContract_Upd_InvalidEffDateEndAfterEffSysDate
		public const string CrCt_CreditContract_Upd_InvalidSignedDate = "ErridnInventory.CrCt_CreditContract_Upd_InvalidSignedDate"; //// //CrCt_CreditContract_Upd_InvalidSignedDate
		public const string CrCt_CreditContract_Upd_InvalidSellerCode = "ErridnInventory.CrCt_CreditContract_Upd_InvalidSellerCode"; //// //CrCt_CreditContract_Upd_InvalidSellerCode
		public const string CrCt_CreditContract_Upd_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_Upd_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_Upd_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_Upd_Input_CrCt_CreditContractDtlTblInvalid		
		public const string CrCt_CreditContract_Upd_Input_CrCt_OrganTblNotFound = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_OrganTblNotFound"; //// //CrCt_CreditContract_Upd_Input_CrCt_OrganTblNotFound		
		public const string CrCt_CreditContract_Upd_Input_CrCt_OrganTblInvalid = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_OrganTblInvalid"; //// //CrCt_CreditContract_Upd_Input_CrCt_OrganTblInvalid		
		public const string CrCt_CreditContract_Upd_Input_CrCt_CreditTypeTblNotFound = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_CreditTypeTblNotFound"; //// //CrCt_CreditContract_Upd_Input_CrCt_CreditTypeTblNotFound		
		public const string CrCt_CreditContract_Upd_Input_CrCt_CreditTypeTblInvalid = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_CreditTypeTblInvalid"; //// //CrCt_CreditContract_Upd_Input_CrCt_CreditTypeTblInvalid		
		public const string CrCt_CreditContract_Upd_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_LoanPurposeTblNotFound"; //// //CrCt_CreditContract_Upd_Input_CrCt_LoanPurposeTblNotFound		
		public const string CrCt_CreditContract_Upd_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_LoanPurposeTblInvalid"; //// //CrCt_CreditContract_Upd_Input_CrCt_LoanPurposeTblInvalid		
		public const string CrCt_CreditContract_Upd_Input_CrCt_FileUploadTblNotFound = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_FileUploadTblNotFound"; //// //CrCt_CreditContract_Upd_Input_CrCt_FileUploadTblNotFound		
		public const string CrCt_CreditContract_Upd_Input_CrCt_FileUploadTblInvalid = "ErridnInventory.CrCt_CreditContract_Upd_Input_CrCt_FileUploadTblInvalid"; //// //CrCt_CreditContract_Upd_Input_CrCt_FileUploadTblInvalid
		public const string CrCt_CreditContract_Upd_Input_InvalidOrganCode = "ErridnInventory.CrCt_CreditContract_Upd_Input_InvalidOrganCode"; //// //CrCt_CreditContract_Upd_Input_InvalidOrganCode		

		// CrCt_CreditContract_Release:
		public const string CrCt_CreditContract_Release = "ErridnInventory.CrCt_CreditContract_Release"; //// //CrCt_CreditContract_Release
		public const string CrCt_CreditContract_Release_InvalidReleaseDateAfterMortgageDate = "ErridnInventory.CrCt_CreditContract_Release_InvalidReleaseDateAfterMortgageDate"; //// //CrCt_CreditContract_Release_InvalidReleaseDateAfterMortgageDate
		public const string CrCt_CreditContract_Release_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_Release_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_Release_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_Release_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_Release_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_Release_Input_CrCt_CreditContractDtlTblInvalid		
		public const string CrCt_CreditContract_Release_Input_InvalidValue = "ErridnInventory.CrCt_CreditContract_Release_Input_InvalidValue"; //// //CrCt_CreditContract_Release_Input_InvalidValue
		public const string CrCt_CreditContract_Release_Input_InvalidOrganCode = "ErridnInventory.CrCt_CreditContract_Release_Input_InvalidOrganCode"; //// //CrCt_CreditContract_Release_Input_InvalidOrganCode
		public const string CrCt_CreditContract_Release_Input_InvalidPaymentPartnerCode = "ErridnInventory.CrCt_CreditContract_Release_Input_InvalidPaymentPartnerCode"; //// //CrCt_CreditContract_Release_Input_InvalidPaymentPartnerCode

		// CrCt_CreditContract_Mortgage:
		public const string CrCt_CreditContract_Mortgage = "ErridnInventory.CrCt_CreditContract_Mortgage"; //// //CrCt_CreditContract_Mortgage
		public const string CrCt_CreditContract_Mortgage_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_Mortgage_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_Mortgage_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_Mortgage_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_Mortgage_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_Mortgage_Input_CrCt_CreditContractDtlTblInvalid		
		public const string CrCt_CreditContract_Mortgage_Input_InvalidValue = "ErridnInventory.CrCt_CreditContract_Mortgage_Input_InvalidValue"; //// //CrCt_CreditContract_Mortgage_Input_InvalidValue
		public const string CrCt_CreditContract_Mortgage_Input_InvalidOrganCode = "ErridnInventory.CrCt_CreditContract_Mortgage_Input_InvalidOrganCode"; //// //CrCt_CreditContract_Mortgage_Input_InvalidOrganCode
		public const string CrCt_CreditContract_Mortgage_Input_InvalidPaymentPartnerCode = "ErridnInventory.CrCt_CreditContract_Mortgage_Input_InvalidPaymentPartnerCode"; //// //CrCt_CreditContract_Mortgage_Input_InvalidPaymentPartnerCode


		// CrCt_CreditContract_Del:
		public const string CrCt_CreditContract_Del = "ErridnInventory.CrCt_CreditContract_Del"; //// //CrCt_CreditContract_Del

		// CrCt_CreditContract_Exchange:
		public const string CrCt_CreditContract_Exchange = "ErridnInventory.CrCt_CreditContract_Exchange"; //// //CrCt_CreditContract_Exchange

		// CrCt_CreditContract_Refinance:
		public const string CrCt_CreditContract_Refinance = "ErridnInventory.CrCt_CreditContract_Refinance"; //// //CrCt_CreditContract_Refinance
		public const string CrCt_CreditContract_Refinance_InvalidBGStatus = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidBGStatus"; //// //CrCt_CreditContract_Refinance_InvalidBGStatus
		public const string CrCt_CreditContract_Refinance_InvalidValue = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidValue"; //// //CrCt_CreditContract_Refinance_InvalidValue
		public const string CrCt_CreditContract_Refinance_InvalidYear = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidYear"; //// //CrCt_CreditContract_Refinance_InvalidYear
		public const string CrCt_CreditContract_Refinance_InvalidEffDateStart = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidEffDateStart"; //// //CrCt_CreditContract_Refinance_InvalidEffDateStart
		public const string CrCt_CreditContract_Refinance_InvalidEffDateEnd = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidEffDateEnd"; //// //CrCt_CreditContract_Refinance_InvalidEffDateEnd
		public const string CrCt_CreditContract_Refinance_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidEffDateStartBeforeEffDateEnd"; //// //CrCt_CreditContract_Refinance_InvalidEffDateStartBeforeEffDateEnd
		public const string CrCt_CreditContract_Refinance_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidEffDateEndAfterEffSysDate"; //// //CrCt_CreditContract_Refinance_InvalidEffDateEndAfterEffSysDate
		public const string CrCt_CreditContract_Refinance_InvalidSignedDate = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidSignedDate"; //// //CrCt_CreditContract_Refinance_InvalidSignedDate
		public const string CrCt_CreditContract_Refinance_InvalidSellerCode = "ErridnInventory.CrCt_CreditContract_Refinance_InvalidSellerCode"; //// //CrCt_CreditContract_Refinance_InvalidSellerCode
		public const string CrCt_CreditContract_Refinance_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_Refinance_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_Refinance_Input_CrCt_CreditContractDtlTblInvalid		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_OrganTblNotFound = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_OrganTblNotFound"; //// //CrCt_CreditContract_Refinance_Input_CrCt_OrganTblNotFound		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_OrganTblInvalid = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_OrganTblInvalid"; //// //CrCt_CreditContract_Refinance_Input_CrCt_OrganTblInvalid		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_CreditTypeTblNotFound = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_CreditTypeTblNotFound"; //// //CrCt_CreditContract_Refinance_Input_CrCt_CreditTypeTblNotFound		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_CreditTypeTblInvalid = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_CreditTypeTblInvalid"; //// //CrCt_CreditContract_Refinance_Input_CrCt_CreditTypeTblInvalid		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_LoanPurposeTblNotFound"; //// //CrCt_CreditContract_Refinance_Input_CrCt_LoanPurposeTblNotFound		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_LoanPurposeTblInvalid"; //// //CrCt_CreditContract_Refinance_Input_CrCt_LoanPurposeTblInvalid		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_FileUploadTblNotFound = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_FileUploadTblNotFound"; //// //CrCt_CreditContract_Refinance_Input_CrCt_FileUploadTblNotFound		
		public const string CrCt_CreditContract_Refinance_Input_CrCt_FileUploadTblInvalid = "ErridnInventory.CrCt_CreditContract_Refinance_Input_CrCt_FileUploadTblInvalid"; //// //CrCt_CreditContract_Refinance_Input_CrCt_FileUploadTblInvalid
		public const string CrCt_CreditContract_Refinance_Input_InvalidOrganCode = "ErridnInventory.CrCt_CreditContract_Refinance_Input_InvalidOrganCode"; //// //CrCt_CreditContract_Refinance_Input_InvalidOrganCode		


		// CrCt_CreditContract_Save:
		public const string CrCt_CreditContract_Save = "ErridnInventory.CrCt_CreditContract_Save"; //// //CrCt_CreditContract_Save
		public const string CrCt_CreditContract_Save_InvalidBGStatus = "ErridnInventory.CrCt_CreditContract_Save_InvalidBGStatus"; //// //CrCt_CreditContract_Save_InvalidBGStatus
		public const string CrCt_CreditContract_Save_InvalidValue = "ErridnInventory.CrCt_CreditContract_Save_InvalidValue"; //// //CrCt_CreditContract_Save_InvalidValue
		public const string CrCt_CreditContract_Save_InvalidYear = "ErridnInventory.CrCt_CreditContract_Save_InvalidYear"; //// //CrCt_CreditContract_Save_InvalidYear
		public const string CrCt_CreditContract_Save_InvalidSignedDate = "ErridnInventory.CrCt_CreditContract_Save_InvalidSignedDate"; //// //CrCt_CreditContract_Save_InvalidSignedDate
		public const string CrCt_CreditContract_Save_InvalidSellerCode = "ErridnInventory.CrCt_CreditContract_Save_InvalidSellerCode"; //// //CrCt_CreditContract_Save_InvalidSellerCode
		public const string CrCt_CreditContract_Save_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_Save_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_Save_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_Save_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_Save_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_Save_Input_CrCt_CreditContractDtlTblInvalid		
		public const string CrCt_CreditContract_Save_Input_InvalidOrganCode = "ErridnInventory.CrCt_CreditContract_Save_Input_InvalidOrganCode"; //// //CrCt_CreditContract_Save_Input_InvalidOrganCode		

		// CrCt_CreditContract_Approve:
		public const string CrCt_CreditContract_Approve = "ErridnInventory.CrCt_CreditContract_Approve"; //// //CrCt_CreditContract_Approve
		public const string CrCt_CreditContract_Approve_CreditContractDtlNotFound = "ErridnInventory.CrCt_CreditContract_Approve_CreditContractDtlNotFound"; //// //CrCt_CreditContract_Approve_CreditContractDtlNotFound

		// CrCt_CreditContract_UpdAppr:
		public const string CrCt_CreditContract_UpdAppr = "ErridnInventory.CrCt_CreditContract_UpdAppr"; //// //CrCt_CreditContract_UpdAppr
		public const string CrCt_CreditContract_UpdAppr_InvalidBGStatus = "ErridnInventory.CrCt_CreditContract_UpdAppr_InvalidBGStatus"; //// //CrCt_CreditContract_UpdAppr_InvalidBGStatus
		public const string CrCt_CreditContract_UpdAppr_InvalidValue = "ErridnInventory.CrCt_CreditContract_UpdAppr_InvalidValue"; //// //CrCt_CreditContract_UpdAppr_InvalidValue
		public const string CrCt_CreditContract_UpdAppr_InvalidSignedDate = "ErridnInventory.CrCt_CreditContract_UpdAppr_InvalidSignedDate"; //// //CrCt_CreditContract_UpdAppr_InvalidSignedDate
		public const string CrCt_CreditContract_UpdAppr_InvalidSellerCode = "ErridnInventory.CrCt_CreditContract_UpdAppr_InvalidSellerCode"; //// //CrCt_CreditContract_UpdAppr_InvalidSellerCode
		public const string CrCt_CreditContract_UpdAppr_Input_CrCt_CreditContractDtlTblNotFound = "ErridnInventory.CrCt_CreditContract_UpdAppr_Input_CrCt_CreditContractDtlTblNotFound"; //// //CrCt_CreditContract_UpdAppr_Input_CrCt_CreditContractDtlTblNotFound		
		public const string CrCt_CreditContract_UpdAppr_Input_CrCt_CreditContractDtlTblInvalid = "ErridnInventory.CrCt_CreditContract_UpdAppr_Input_CrCt_CreditContractDtlTblInvalid"; //// //CrCt_CreditContract_UpdAppr_Input_CrCt_CreditContractDtlTblInvalid		
		#endregion

		#region // CrCt_CreditContractDtl:
		// CrCt_CreditContractDtl_CheckDB:
		public const string CrCt_CreditContractDtl_CheckDB_CreditContractDtlNotFound = "ErridnInventory.CrCt_CreditContractDtl_CheckDB_CreditContractDtlNotFound"; //// //CrCt_CreditContractDtl_CheckDB_CreditContractDtlNotFound
		public const string CrCt_CreditContractDtl_CheckDB_CreditContractDtlExist = "ErridnInventory.CrCt_CreditContractDtl_CheckDB_CreditContractDtlExist"; //// //CrCt_CreditContractDtl_CheckDB_CreditContractDtlExist
		public const string CrCt_CreditContractDtl_CheckDB_StatusNotMatched = "ErridnInventory.CrCt_CreditContractDtl_CheckDB_StatusNotMatched"; //// //CrCt_CreditContractDtl_CheckDB_StatusNotMatched

		#endregion

		#region // KUNN_KUNN:
		// KUNN_KUNN_CheckDB:
		public const string KUNN_KUNN_CheckDB_KUNNNotFound = "ErridnInventory.KUNN_KUNN_CheckDB_KUNNNotFound"; //// //KUNN_KUNN_CheckDB_KUNNNotFound
		public const string KUNN_KUNN_CheckDB_KUNNExist = "ErridnInventory.KUNN_KUNN_CheckDB_KUNNExist"; //// //KUNN_KUNN_CheckDB_KUNNExist
		public const string KUNN_KUNN_CheckDB_StatusNotMatched = "ErridnInventory.KUNN_KUNN_CheckDB_StatusNotMatched"; //// //KUNN_KUNN_CheckDB_StatusNotMatched

		// KUNN_KUNN_Get:
		public const string KUNN_KUNN_Get = "ErridnInventory.KUNN_KUNN_Get"; //// //KUNN_KUNN_Get

		// WAS_KUNN_KUNN_Get:
		public const string WAS_KUNN_KUNN_Get = "ErridnInventory.WAS_KUNN_KUNN_Get"; //// //WAS_KUNN_KUNN_Get

		// WAS_KUNN_KUNN_Save:
		public const string WAS_KUNN_KUNN_Save = "ErridnInventory.WAS_KUNN_KUNN_Save"; //// //WAS_KUNN_KUNN_Save

		// WAS_KUNN_KUNN_Add:
		public const string WAS_KUNN_KUNN_Add = "ErridnInventory.WAS_KUNN_KUNN_Add"; //// //WAS_KUNN_KUNN_Add

		// WAS_KUNN_KUNN_Upd:
		public const string WAS_KUNN_KUNN_Upd = "ErridnInventory.WAS_KUNN_KUNN_Upd"; //// //WAS_KUNN_KUNN_Upd

		// WAS_KUNN_KUNN_Del:
		public const string WAS_KUNN_KUNN_Del = "ErridnInventory.WAS_KUNN_KUNN_Del"; //// //WAS_KUNN_KUNN_Del

		// WAS_KUNN_KUNN_Release:
		public const string WAS_KUNN_KUNN_Release = "ErridnInventory.WAS_KUNN_KUNN_Release"; //// //WAS_KUNN_KUNN_Release

		// WAS_KUNN_KUNN_Mortgage:
		public const string WAS_KUNN_KUNN_Mortgage = "ErridnInventory.WAS_KUNN_KUNN_Mortgage"; //// //WAS_KUNN_KUNN_Mortgage

		// WAS_KUNN_KUNN_Refinance:
		public const string WAS_KUNN_KUNN_Refinance = "ErridnInventory.WAS_KUNN_KUNN_Refinance"; //// //WAS_KUNN_KUNN_Refinance

		public const string KUNN_KUNN_Check_InvalidKUNNStatusInActive = "ErridnInventory.KUNN_KUNN_Check_InvalidKUNNStatusInActive"; //// //KUNN_KUNN_Check_InvalidKUNNStatusInActive

		// KUNN_KUNN_Add:
		public const string KUNN_KUNN_Add = "ErridnInventory.KUNN_KUNN_Add"; //// //KUNN_KUNN_Add
		public const string KUNN_KUNN_Add_InvalidBGStatus = "ErridnInventory.KUNN_KUNN_Add_InvalidBGStatus"; //// //KUNN_KUNN_Add_InvalidBGStatus
		public const string KUNN_KUNN_Add_InvalidValue = "ErridnInventory.KUNN_KUNN_Add_InvalidValue"; //// //KUNN_KUNN_Add_InvalidValue
		public const string KUNN_KUNN_Add_InvalidYear = "ErridnInventory.KUNN_KUNN_Add_InvalidYear"; //// //KUNN_KUNN_Add_InvalidYear
		public const string KUNN_KUNN_Add_InvalidEffDateStart = "ErridnInventory.KUNN_KUNN_Add_InvalidEffDateStart"; //// //KUNN_KUNN_Add_InvalidEffDateStart
		public const string KUNN_KUNN_Add_InvalidEffDateEnd = "ErridnInventory.KUNN_KUNN_Add_InvalidEffDateEnd"; //// //KUNN_KUNN_Add_InvalidEffDateEnd
		public const string KUNN_KUNN_Add_InvalidKUNNSignDate = "ErridnInventory.KUNN_KUNN_Add_InvalidKUNNSignDate"; //// //KUNN_KUNN_Add_InvalidKUNNSignDate
		public const string KUNN_KUNN_Add_InvalidKUNNSignDateBeforeSysDate = "ErridnInventory.KUNN_KUNN_Add_InvalidKUNNSignDateBeforeSysDate"; //// //KUNN_KUNN_Add_InvalidKUNNSignDateBeforeSysDate
		public const string KUNN_KUNN_Add_InvalidNhanNoDateAfterSysDate = "ErridnInventory.KUNN_KUNN_Add_InvalidNhanNoDateAfterSysDate"; //// //KUNN_KUNN_Add_InvalidNhanNoDateAfterSysDate
		public const string KUNN_KUNN_Add_InvalidNhanNoDateAfterSignDate = "ErridnInventory.KUNN_KUNN_Add_InvalidNhanNoDateAfterSignDate"; //// //KUNN_KUNN_Add_InvalidNhanNoDateAfterSignDate
		public const string KUNN_KUNN_Add_InvalidDaoHanDateAfterSysDate = "ErridnInventory.KUNN_KUNN_Add_InvalidDaoHanDateAfterSysDate"; //// //KUNN_KUNN_Add_InvalidDaoHanDateAfterSysDate
		public const string KUNN_KUNN_Add_InvalidNhanNoDateBeforeDaoHanDate = "ErridnInventory.KUNN_KUNN_Add_InvalidNhanNoDateBeforeDaoHanDate"; //// //KUNN_KUNN_Add_InvalidNhanNoDateBeforeDaoHanDate
		public const string KUNN_KUNN_Add_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.KUNN_KUNN_Add_InvalidEffDateStartBeforeEffDateEnd"; //// //KUNN_KUNN_Add_InvalidEffDateStartBeforeEffDateEnd
		public const string KUNN_KUNN_Add_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.KUNN_KUNN_Add_InvalidEffDateEndAfterEffSysDate"; //// //KUNN_KUNN_Add_InvalidEffDateEndAfterEffSysDate
		public const string KUNN_KUNN_Add_InvalidSignedDate = "ErridnInventory.KUNN_KUNN_Add_InvalidSignedDate"; //// //KUNN_KUNN_Add_InvalidSignedDate
		public const string KUNN_KUNN_Add_InvalidSellerCode = "ErridnInventory.KUNN_KUNN_Add_InvalidSellerCode"; //// //KUNN_KUNN_Add_InvalidSellerCode
		public const string KUNN_KUNN_Add_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_Add_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_Add_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_Add_Input_KUNN_KUNNDtlTblInvalid		
		public const string KUNN_KUNN_Add_Input_CrCt_OrganTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_CrCt_OrganTblNotFound"; //// //KUNN_KUNN_Add_Input_CrCt_OrganTblNotFound		
		public const string KUNN_KUNN_Add_Input_CrCt_OrganTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_CrCt_OrganTblInvalid"; //// //KUNN_KUNN_Add_Input_CrCt_OrganTblInvalid		
		public const string KUNN_KUNN_Add_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //KUNN_KUNN_Add_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string KUNN_KUNN_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //KUNN_KUNN_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string KUNN_KUNN_Add_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_CrCt_LoanPurposeTblNotFound"; //// //KUNN_KUNN_Add_Input_CrCt_LoanPurposeTblNotFound		
		public const string KUNN_KUNN_Add_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_CrCt_LoanPurposeTblInvalid"; //// //KUNN_KUNN_Add_Input_CrCt_LoanPurposeTblInvalid		
		public const string KUNN_KUNN_Add_Input_KUNN_FileUploadTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_FileUploadTblNotFound"; //// //KUNN_KUNN_Add_Input_KUNN_FileUploadTblNotFound		
		public const string KUNN_KUNN_Add_Input_KUNN_FileUploadTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_FileUploadTblInvalid"; //// //KUNN_KUNN_Add_Input_KUNN_FileUploadTblInvalid
		public const string KUNN_KUNN_Add_Input_KUNN_LCTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_LCTblNotFound"; //// //KUNN_KUNN_Add_Input_KUNN_LCTblNotFound		
		public const string KUNN_KUNN_Add_Input_KUNN_LCTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_LCTblInvalid"; //// //KUNN_KUNN_Add_Input_KUNN_LCTblInvalid
		public const string KUNN_KUNN_Add_Input_KUNN_MDTblNotFound = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_MDTblNotFound"; //// //KUNN_KUNN_Add_Input_KUNN_MDTblNotFound		
		public const string KUNN_KUNN_Add_Input_KUNN_MDTblInvalid = "ErridnInventory.KUNN_KUNN_Add_Input_KUNN_MDTblInvalid"; //// //KUNN_KUNN_Add_Input_KUNN_MDTblInvalid
		public const string KUNN_KUNN_Add_Input_InvalidValue = "ErridnInventory.KUNN_KUNN_Add_Input_InvalidValue"; //// //KUNN_KUNN_Add_Input_InvalidValue
		public const string KUNN_KUNN_Add_Input_InvalidOrganCode = "ErridnInventory.KUNN_KUNN_Add_Input_InvalidOrganCode"; //// //KUNN_KUNN_Add_Input_InvalidOrganCode		

		// KUNN_KUNN_Upd:
		public const string KUNN_KUNN_Upd = "ErridnInventory.KUNN_KUNN_Upd"; //// //KUNN_KUNN_Upd
		public const string KUNN_KUNN_Upd_InvalidBGStatus = "ErridnInventory.KUNN_KUNN_Upd_InvalidBGStatus"; //// //KUNN_KUNN_Upd_InvalidBGStatus
		public const string KUNN_KUNN_Upd_InvalidValue = "ErridnInventory.KUNN_KUNN_Upd_InvalidValue"; //// //KUNN_KUNN_Upd_InvalidValue
		public const string KUNN_KUNN_Upd_InvalidTatToanDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidTatToanDate"; //// //KUNN_KUNN_Upd_InvalidTatToanDate
		public const string KUNN_KUNN_Upd_InvalidYear = "ErridnInventory.KUNN_KUNN_Upd_InvalidYear"; //// //KUNN_KUNN_Upd_InvalidYear
		public const string KUNN_KUNN_Upd_InvalidEffDateStart = "ErridnInventory.KUNN_KUNN_Upd_InvalidEffDateStart"; //// //KUNN_KUNN_Upd_InvalidEffDateStart
		public const string KUNN_KUNN_Upd_InvalidEffDateEnd = "ErridnInventory.KUNN_KUNN_Upd_InvalidEffDateEnd"; //// //KUNN_KUNN_Upd_InvalidEffDateEnd
		public const string KUNN_KUNN_Upd_InvalidKUNNSignDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidKUNNSignDate"; //// //KUNN_KUNN_Upd_InvalidKUNNSignDate
		public const string KUNN_KUNN_Upd_InvalidKUNNSignDateBeforeSysDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidKUNNSignDateBeforeSysDate"; //// //KUNN_KUNN_Upd_InvalidKUNNSignDateBeforeSysDate
		public const string KUNN_KUNN_Upd_InvalidNhanNoDateAfterSysDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidNhanNoDateAfterSysDate"; //// //KUNN_KUNN_Upd_InvalidNhanNoDateAfterSysDate
		public const string KUNN_KUNN_Upd_InvalidNhanNoDateAfterSignDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidNhanNoDateAfterSignDate"; //// //KUNN_KUNN_Upd_InvalidNhanNoDateAfterSignDate
		public const string KUNN_KUNN_Upd_InvalidDaoHanDateAfterSysDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidDaoHanDateAfterSysDate"; //// //KUNN_KUNN_Upd_InvalidDaoHanDateAfterSysDate
		public const string KUNN_KUNN_Upd_InvalidNhanNoDateBeforeDaoHanDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidNhanNoDateBeforeDaoHanDate"; //// //KUNN_KUNN_Upd_InvalidNhanNoDateBeforeDaoHanDate
		public const string KUNN_KUNN_Upd_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.KUNN_KUNN_Upd_InvalidEffDateStartBeforeEffDateEnd"; //// //KUNN_KUNN_Upd_InvalidEffDateStartBeforeEffDateEnd
		public const string KUNN_KUNN_Upd_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidEffDateEndAfterEffSysDate"; //// //KUNN_KUNN_Upd_InvalidEffDateEndAfterEffSysDate
		public const string KUNN_KUNN_Upd_InvalidSignedDate = "ErridnInventory.KUNN_KUNN_Upd_InvalidSignedDate"; //// //KUNN_KUNN_Upd_InvalidSignedDate
		public const string KUNN_KUNN_Upd_InvalidSellerCode = "ErridnInventory.KUNN_KUNN_Upd_InvalidSellerCode"; //// //KUNN_KUNN_Upd_InvalidSellerCode
		public const string KUNN_KUNN_Upd_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_Upd_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_Upd_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_Upd_Input_KUNN_KUNNDtlTblInvalid		
		public const string KUNN_KUNN_Upd_Input_CrCt_OrganTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_CrCt_OrganTblNotFound"; //// //KUNN_KUNN_Upd_Input_CrCt_OrganTblNotFound		
		public const string KUNN_KUNN_Upd_Input_CrCt_OrganTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_CrCt_OrganTblInvalid"; //// //KUNN_KUNN_Upd_Input_CrCt_OrganTblInvalid		
		public const string KUNN_KUNN_Upd_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //KUNN_KUNN_Upd_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string KUNN_KUNN_Upd_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //KUNN_KUNN_Upd_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string KUNN_KUNN_Upd_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_CrCt_LoanPurposeTblNotFound"; //// //KUNN_KUNN_Upd_Input_CrCt_LoanPurposeTblNotFound		
		public const string KUNN_KUNN_Upd_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_CrCt_LoanPurposeTblInvalid"; //// //KUNN_KUNN_Upd_Input_CrCt_LoanPurposeTblInvalid		
		public const string KUNN_KUNN_Upd_Input_KUNN_FileUploadTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_FileUploadTblNotFound"; //// //KUNN_KUNN_Upd_Input_KUNN_FileUploadTblNotFound		
		public const string KUNN_KUNN_Upd_Input_KUNN_FileUploadTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_FileUploadTblInvalid"; //// //KUNN_KUNN_Upd_Input_KUNN_FileUploadTblInvalid
		public const string KUNN_KUNN_Upd_Input_KUNN_LCTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_LCTblNotFound"; //// //KUNN_KUNN_Upd_Input_KUNN_LCTblNotFound		
		public const string KUNN_KUNN_Upd_Input_KUNN_LCTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_LCTblInvalid"; //// //KUNN_KUNN_Upd_Input_KUNN_LCTblInvalid
		public const string KUNN_KUNN_Upd_Input_KUNN_MDTblNotFound = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_MDTblNotFound"; //// //KUNN_KUNN_Upd_Input_KUNN_MDTblNotFound		
		public const string KUNN_KUNN_Upd_Input_KUNN_MDTblInvalid = "ErridnInventory.KUNN_KUNN_Upd_Input_KUNN_MDTblInvalid"; //// //KUNN_KUNN_Upd_Input_KUNN_MDTblInvalid
		public const string KUNN_KUNN_Upd_Input_InvalidValue = "ErridnInventory.KUNN_KUNN_Upd_Input_InvalidValue"; //// //KUNN_KUNN_Upd_Input_InvalidValue
		public const string KUNN_KUNN_Upd_Input_InvalidOrganCode = "ErridnInventory.KUNN_KUNN_Upd_Input_InvalidOrganCode"; //// //KUNN_KUNN_Upd_Input_InvalidOrganCode		

		// KUNN_KUNN_Release:
		public const string KUNN_KUNN_Release = "ErridnInventory.KUNN_KUNN_Release"; //// //KUNN_KUNN_Release
		public const string KUNN_KUNN_Release_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_Release_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_Release_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_Release_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_Release_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_Release_Input_KUNN_KUNNDtlTblInvalid		
		public const string KUNN_KUNN_Release_Input_InvalidValue = "ErridnInventory.KUNN_KUNN_Release_Input_InvalidValue"; //// //KUNN_KUNN_Release_Input_InvalidValue
		public const string KUNN_KUNN_Release_Input_InvalidOrganCode = "ErridnInventory.KUNN_KUNN_Release_Input_InvalidOrganCode"; //// //KUNN_KUNN_Release_Input_InvalidOrganCode
		public const string KUNN_KUNN_Release_Input_InvalidPaymentPartnerCode = "ErridnInventory.KUNN_KUNN_Release_Input_InvalidPaymentPartnerCode"; //// //KUNN_KUNN_Release_Input_InvalidPaymentPartnerCode

		// KUNN_KUNN_Mortgage:
		public const string KUNN_KUNN_Mortgage = "ErridnInventory.KUNN_KUNN_Mortgage"; //// //KUNN_KUNN_Mortgage
		public const string KUNN_KUNN_Mortgage_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_Mortgage_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_Mortgage_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_Mortgage_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_Mortgage_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_Mortgage_Input_KUNN_KUNNDtlTblInvalid		
		public const string KUNN_KUNN_Mortgage_Input_InvalidValue = "ErridnInventory.KUNN_KUNN_Mortgage_Input_InvalidValue"; //// //KUNN_KUNN_Mortgage_Input_InvalidValue
		public const string KUNN_KUNN_Mortgage_Input_InvalidOrganCode = "ErridnInventory.KUNN_KUNN_Mortgage_Input_InvalidOrganCode"; //// //KUNN_KUNN_Mortgage_Input_InvalidOrganCode
		public const string KUNN_KUNN_Mortgage_Input_InvalidPaymentPartnerCode = "ErridnInventory.KUNN_KUNN_Mortgage_Input_InvalidPaymentPartnerCode"; //// //KUNN_KUNN_Mortgage_Input_InvalidPaymentPartnerCode


		// KUNN_KUNN_Del:
		public const string KUNN_KUNN_Del = "ErridnInventory.KUNN_KUNN_Del"; //// //KUNN_KUNN_Del
		public const string KUNN_KUNN_Del_InvalidTatToanDate = "ErridnInventory.KUNN_KUNN_Del_InvalidTatToanDate"; //// //KUNN_KUNN_Del_InvalidTatToanDate

		// KUNN_KUNN_Refinance:
		public const string KUNN_KUNN_Refinance = "ErridnInventory.KUNN_KUNN_Refinance"; //// //KUNN_KUNN_Refinance
		public const string KUNN_KUNN_Refinance_InvalidBGStatus = "ErridnInventory.KUNN_KUNN_Refinance_InvalidBGStatus"; //// //KUNN_KUNN_Refinance_InvalidBGStatus
		public const string KUNN_KUNN_Refinance_InvalidValue = "ErridnInventory.KUNN_KUNN_Refinance_InvalidValue"; //// //KUNN_KUNN_Refinance_InvalidValue
		public const string KUNN_KUNN_Refinance_InvalidYear = "ErridnInventory.KUNN_KUNN_Refinance_InvalidYear"; //// //KUNN_KUNN_Refinance_InvalidYear
		public const string KUNN_KUNN_Refinance_InvalidEffDateStart = "ErridnInventory.KUNN_KUNN_Refinance_InvalidEffDateStart"; //// //KUNN_KUNN_Refinance_InvalidEffDateStart
		public const string KUNN_KUNN_Refinance_InvalidEffDateEnd = "ErridnInventory.KUNN_KUNN_Refinance_InvalidEffDateEnd"; //// //KUNN_KUNN_Refinance_InvalidEffDateEnd
		public const string KUNN_KUNN_Refinance_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.KUNN_KUNN_Refinance_InvalidEffDateStartBeforeEffDateEnd"; //// //KUNN_KUNN_Refinance_InvalidEffDateStartBeforeEffDateEnd
		public const string KUNN_KUNN_Refinance_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.KUNN_KUNN_Refinance_InvalidEffDateEndAfterEffSysDate"; //// //KUNN_KUNN_Refinance_InvalidEffDateEndAfterEffSysDate
		public const string KUNN_KUNN_Refinance_InvalidSignedDate = "ErridnInventory.KUNN_KUNN_Refinance_InvalidSignedDate"; //// //KUNN_KUNN_Refinance_InvalidSignedDate
		public const string KUNN_KUNN_Refinance_InvalidSellerCode = "ErridnInventory.KUNN_KUNN_Refinance_InvalidSellerCode"; //// //KUNN_KUNN_Refinance_InvalidSellerCode
		public const string KUNN_KUNN_Refinance_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_Refinance_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_Refinance_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_Refinance_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_Refinance_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_Refinance_Input_KUNN_KUNNDtlTblInvalid		
		public const string KUNN_KUNN_Refinance_Input_CrCt_OrganTblNotFound = "ErridnInventory.KUNN_KUNN_Refinance_Input_CrCt_OrganTblNotFound"; //// //KUNN_KUNN_Refinance_Input_CrCt_OrganTblNotFound		
		public const string KUNN_KUNN_Refinance_Input_CrCt_OrganTblInvalid = "ErridnInventory.KUNN_KUNN_Refinance_Input_CrCt_OrganTblInvalid"; //// //KUNN_KUNN_Refinance_Input_CrCt_OrganTblInvalid		
		public const string KUNN_KUNN_Refinance_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.KUNN_KUNN_Refinance_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //KUNN_KUNN_Refinance_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string KUNN_KUNN_Refinance_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.KUNN_KUNN_Refinance_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //KUNN_KUNN_Refinance_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string KUNN_KUNN_Refinance_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.KUNN_KUNN_Refinance_Input_CrCt_LoanPurposeTblNotFound"; //// //KUNN_KUNN_Refinance_Input_CrCt_LoanPurposeTblNotFound		
		public const string KUNN_KUNN_Refinance_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.KUNN_KUNN_Refinance_Input_CrCt_LoanPurposeTblInvalid"; //// //KUNN_KUNN_Refinance_Input_CrCt_LoanPurposeTblInvalid		
		public const string KUNN_KUNN_Refinance_Input_KUNN_FileUploadTblNotFound = "ErridnInventory.KUNN_KUNN_Refinance_Input_KUNN_FileUploadTblNotFound"; //// //KUNN_KUNN_Refinance_Input_KUNN_FileUploadTblNotFound		
		public const string KUNN_KUNN_Refinance_Input_KUNN_FileUploadTblInvalid = "ErridnInventory.KUNN_KUNN_Refinance_Input_KUNN_FileUploadTblInvalid"; //// //KUNN_KUNN_Refinance_Input_KUNN_FileUploadTblInvalid
		public const string KUNN_KUNN_Refinance_Input_InvalidOrganCode = "ErridnInventory.KUNN_KUNN_Refinance_Input_InvalidOrganCode"; //// //KUNN_KUNN_Refinance_Input_InvalidOrganCode		


		// KUNN_KUNN_Save:
		public const string KUNN_KUNN_Save = "ErridnInventory.KUNN_KUNN_Save"; //// //KUNN_KUNN_Save
		public const string KUNN_KUNN_Save_InvalidBGStatus = "ErridnInventory.KUNN_KUNN_Save_InvalidBGStatus"; //// //KUNN_KUNN_Save_InvalidBGStatus
		public const string KUNN_KUNN_Save_InvalidValue = "ErridnInventory.KUNN_KUNN_Save_InvalidValue"; //// //KUNN_KUNN_Save_InvalidValue
		public const string KUNN_KUNN_Save_InvalidYear = "ErridnInventory.KUNN_KUNN_Save_InvalidYear"; //// //KUNN_KUNN_Save_InvalidYear
		public const string KUNN_KUNN_Save_InvalidSignedDate = "ErridnInventory.KUNN_KUNN_Save_InvalidSignedDate"; //// //KUNN_KUNN_Save_InvalidSignedDate
		public const string KUNN_KUNN_Save_InvalidSellerCode = "ErridnInventory.KUNN_KUNN_Save_InvalidSellerCode"; //// //KUNN_KUNN_Save_InvalidSellerCode
		public const string KUNN_KUNN_Save_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_Save_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_Save_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_Save_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_Save_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_Save_Input_KUNN_KUNNDtlTblInvalid		
		public const string KUNN_KUNN_Save_Input_InvalidOrganCode = "ErridnInventory.KUNN_KUNN_Save_Input_InvalidOrganCode"; //// //KUNN_KUNN_Save_Input_InvalidOrganCode		

		// KUNN_KUNN_Approve:
		public const string KUNN_KUNN_Approve = "ErridnInventory.KUNN_KUNN_Approve"; //// //KUNN_KUNN_Approve
		public const string KUNN_KUNN_Approve_KUNNDtlNotFound = "ErridnInventory.KUNN_KUNN_Approve_KUNNDtlNotFound"; //// //KUNN_KUNN_Approve_KUNNDtlNotFound

		// KUNN_KUNN_UpdAppr:
		public const string KUNN_KUNN_UpdAppr = "ErridnInventory.KUNN_KUNN_UpdAppr"; //// //KUNN_KUNN_UpdAppr
		public const string KUNN_KUNN_UpdAppr_InvalidBGStatus = "ErridnInventory.KUNN_KUNN_UpdAppr_InvalidBGStatus"; //// //KUNN_KUNN_UpdAppr_InvalidBGStatus
		public const string KUNN_KUNN_UpdAppr_InvalidValue = "ErridnInventory.KUNN_KUNN_UpdAppr_InvalidValue"; //// //KUNN_KUNN_UpdAppr_InvalidValue
		public const string KUNN_KUNN_UpdAppr_InvalidSignedDate = "ErridnInventory.KUNN_KUNN_UpdAppr_InvalidSignedDate"; //// //KUNN_KUNN_UpdAppr_InvalidSignedDate
		public const string KUNN_KUNN_UpdAppr_InvalidSellerCode = "ErridnInventory.KUNN_KUNN_UpdAppr_InvalidSellerCode"; //// //KUNN_KUNN_UpdAppr_InvalidSellerCode
		public const string KUNN_KUNN_UpdAppr_Input_KUNN_KUNNDtlTblNotFound = "ErridnInventory.KUNN_KUNN_UpdAppr_Input_KUNN_KUNNDtlTblNotFound"; //// //KUNN_KUNN_UpdAppr_Input_KUNN_KUNNDtlTblNotFound		
		public const string KUNN_KUNN_UpdAppr_Input_KUNN_KUNNDtlTblInvalid = "ErridnInventory.KUNN_KUNN_UpdAppr_Input_KUNN_KUNNDtlTblInvalid"; //// //KUNN_KUNN_UpdAppr_Input_KUNN_KUNNDtlTblInvalid		
		#endregion

		#region // Prj_Project:
		// Prj_Project_CheckDB:
		public const string Prj_Project_CheckDB_ProjectNotFound = "ErridnInventory.Prj_Project_CheckDB_ProjectNotFound"; //// //Prj_Project_CheckDB_ProjectNotFound
		public const string Prj_Project_CheckDB_ProjectExist = "ErridnInventory.Prj_Project_CheckDB_ProjectExist"; //// //Prj_Project_CheckDB_ProjectExist
		public const string Prj_Project_CheckDB_StatusNotMatched = "ErridnInventory.Prj_Project_CheckDB_StatusNotMatched"; //// //Prj_Project_CheckDB_StatusNotMatched

		// Prj_Project_Get:
		public const string Prj_Project_Get = "ErridnInventory.Prj_Project_Get"; //// //Prj_Project_Get

		// Prj_Project_Del:
		public const string Prj_Project_Del = "ErridnInventory.Prj_Project_Del"; //// //Prj_Project_Del

		// WAS_Prj_Project_Get:
		public const string WAS_Prj_Project_Get = "ErridnInventory.WAS_Prj_Project_Get"; //// //WAS_Prj_Project_Get

		// WAS_Prj_Project_Save:
		public const string WAS_Prj_Project_Save = "ErridnInventory.WAS_Prj_Project_Save"; //// //WAS_Prj_Project_Save


		// Prj_Project_Save:
		public const string Prj_Project_Save = "ErridnInventory.Prj_Project_Save"; //// //Prj_Project_Save
		public const string Prj_Project_Save_InvalidBGStatus = "ErridnInventory.Prj_Project_Save_InvalidBGStatus"; //// //Prj_Project_Save_InvalidBGStatus
		public const string Prj_Project_Save_InvalidValue = "ErridnInventory.Prj_Project_Save_InvalidValue"; //// //Prj_Project_Save_InvalidValue
		public const string Prj_Project_Save_InvalidYear = "ErridnInventory.Prj_Project_Save_InvalidYear"; //// //Prj_Project_Save_InvalidYear
		public const string Prj_Project_Save_InvalidSignedDate = "ErridnInventory.Prj_Project_Save_InvalidSignedDate"; //// //Prj_Project_Save_InvalidSignedDate
		public const string Prj_Project_Save_InvalidSellerCode = "ErridnInventory.Prj_Project_Save_InvalidSellerCode"; //// //Prj_Project_Save_InvalidSellerCode
		public const string Prj_Project_Save_Input_Prj_ProjectDtlTblNotFound = "ErridnInventory.Prj_Project_Save_Input_Prj_ProjectDtlTblNotFound"; //// //Prj_Project_Save_Input_Prj_ProjectDtlTblNotFound		
		public const string Prj_Project_Save_Input_Prj_ProjectDtlTblInvalid = "ErridnInventory.Prj_Project_Save_Input_Prj_ProjectDtlTblInvalid"; //// //Prj_Project_Save_Input_Prj_ProjectDtlTblInvalid		
		public const string Prj_Project_Save_Input_InvalidOrganCode = "ErridnInventory.Prj_Project_Save_Input_InvalidOrganCode"; //// //Prj_Project_Save_Input_InvalidOrganCode		

		// Prj_Project_Add:
		public const string Prj_Project_Add = "ErridnInventory.Prj_Project_Add"; //// //Prj_Project_Add
		public const string Prj_Project_Add_InvalidBGStatus = "ErridnInventory.Prj_Project_Add_InvalidBGStatus"; //// //Prj_Project_Add_InvalidBGStatus
		public const string Prj_Project_Add_InvalidValue = "ErridnInventory.Prj_Project_Add_InvalidValue"; //// //Prj_Project_Add_InvalidValue
		public const string Prj_Project_Add_InvalidYear = "ErridnInventory.Prj_Project_Add_InvalidYear"; //// //Prj_Project_Add_InvalidYear
		public const string Prj_Project_Add_InvalidSignedDate = "ErridnInventory.Prj_Project_Add_InvalidSignedDate"; //// //Prj_Project_Add_InvalidSignedDate
		public const string Prj_Project_Add_InvalidSellerCode = "ErridnInventory.Prj_Project_Add_InvalidSellerCode"; //// //Prj_Project_Add_InvalidSellerCode
		public const string Prj_Project_Add_Input_Prj_ProjectTblNotFound = "ErridnInventory.Prj_Project_Add_Input_Prj_ProjectTblNotFound"; //// //Prj_Project_Add_Input_Prj_ProjectTblNotFound		
		public const string Prj_Project_Add_Input_Prj_ProjectTblInvalid = "ErridnInventory.Prj_Project_Add_Input_Prj_ProjectTblInvalid"; //// //Prj_Project_Add_Input_Prj_ProjectTblInvalid		
		public const string Prj_Project_Add_Input_InvalidAssetType = "ErridnInventory.Prj_Project_Add_Input_InvalidAssetType"; //// //Prj_Project_Add_Input_InvalidAssetType		
		public const string Prj_Project_Add_Input_InvalidOrganCode = "ErridnInventory.Prj_Project_Add_Input_InvalidOrganCode"; //// //Prj_Project_Add_Input_InvalidOrganCode		

		// Prj_Project_Upd:
		public const string Prj_Project_Upd = "ErridnInventory.Prj_Project_Upd"; //// //Prj_Project_Upd
		public const string Prj_Project_Upd_InvalidBGStatus = "ErridnInventory.Prj_Project_Upd_InvalidBGStatus"; //// //Prj_Project_Upd_InvalidBGStatus
		public const string Prj_Project_Upd_InvalidValue = "ErridnInventory.Prj_Project_Upd_InvalidValue"; //// //Prj_Project_Upd_InvalidValue
		public const string Prj_Project_Upd_InvalidBidPakageCodeStatus = "ErridnInventory.Prj_Project_Upd_InvalidBidPakageCodeStatus"; //// //Prj_Project_Upd_InvalidBidPakageCodeStatus
		public const string Prj_Project_Upd_InvalidYear = "ErridnInventory.Prj_Project_Upd_InvalidYear"; //// //Prj_Project_Upd_InvalidYear
		public const string Prj_Project_Upd_InvalidSignedDate = "ErridnInventory.Prj_Project_Upd_InvalidSignedDate"; //// //Prj_Project_Upd_InvalidSignedDate
		public const string Prj_Project_Upd_InvalidSellerCode = "ErridnInventory.Prj_Project_Upd_InvalidSellerCode"; //// //Prj_Project_Upd_InvalidSellerCode
		public const string Prj_Project_Upd_Input_Prj_ProjectTblNotFound = "ErridnInventory.Prj_Project_Upd_Input_Prj_ProjectTblNotFound"; //// //Prj_Project_Upd_Input_Prj_ProjectTblNotFound		
		public const string Prj_Project_Upd_Input_Prj_ProjectTblInvalid = "ErridnInventory.Prj_Project_Upd_Input_Prj_ProjectTblInvalid"; //// //Prj_Project_Upd_Input_Prj_ProjectTblInvalid		
		public const string Prj_Project_Upd_Input_InvalidAssetType = "ErridnInventory.Prj_Project_Upd_Input_InvalidAssetType"; //// //Prj_Project_Upd_Input_InvalidAssetType		
		public const string Prj_Project_Upd_Input_InvalidOrganCode = "ErridnInventory.Prj_Project_Upd_Input_InvalidOrganCode"; //// //Prj_Project_Upd_Input_InvalidOrganCode		

		// Prj_Project_InActive:
		public const string Prj_Project_InActive = "ErridnInventory.Prj_Project_InActive"; //// //Prj_Project_InActive
		public const string Prj_Project_InActive_InvalidBGStatus = "ErridnInventory.Prj_Project_InActive_InvalidBGStatus"; //// //Prj_Project_InActive_InvalidBGStatus
		public const string Prj_Project_InActive_InvalidValue = "ErridnInventory.Prj_Project_InActive_InvalidValue"; //// //Prj_Project_InActive_InvalidValue
		public const string Prj_Project_InActive_InvalidYear = "ErridnInventory.Prj_Project_InActive_InvalidYear"; //// //Prj_Project_InActive_InvalidYear
		public const string Prj_Project_InActive_InvalidSignedDate = "ErridnInventory.Prj_Project_InActive_InvalidSignedDate"; //// //Prj_Project_InActive_InvalidSignedDate
		public const string Prj_Project_InActive_InvalidSellerCode = "ErridnInventory.Prj_Project_InActive_InvalidSellerCode"; //// //Prj_Project_InActive_InvalidSellerCode
		public const string Prj_Project_InActive_Input_Prj_ProjectTblNotFound = "ErridnInventory.Prj_Project_InActive_Input_Prj_ProjectTblNotFound"; //// //Prj_Project_InActive_Input_Prj_ProjectTblNotFound		
		public const string Prj_Project_InActive_Input_Prj_ProjectTblInvalid = "ErridnInventory.Prj_Project_InActive_Input_Prj_ProjectTblInvalid"; //// //Prj_Project_InActive_Input_Prj_ProjectTblInvalid		
		public const string Prj_Project_InActive_Input_InvalidAssetType = "ErridnInventory.Prj_Project_InActive_Input_InvalidAssetType"; //// //Prj_Project_InActive_Input_InvalidAssetType		
		public const string Prj_Project_InActive_Input_InvalidOrganCode = "ErridnInventory.Prj_Project_InActive_Input_InvalidOrganCode"; //// //Prj_Project_InActive_Input_InvalidOrganCode		


		// WAS_Prj_Project_Add:
		public const string WAS_Prj_Project_Add = "ErridnInventory.WAS_Prj_Project_Add"; //// //WAS_Prj_Project_Add

		// WAS_Prj_Project_Upd:
		public const string WAS_Prj_Project_Upd = "ErridnInventory.WAS_Prj_Project_Upd"; //// //WAS_Prj_Project_Upd

		// WAS_Prj_Project_InActive:
		public const string WAS_Prj_Project_InActive = "ErridnInventory.WAS_Prj_Project_InActive"; //// //WAS_Prj_Project_InActive

		// WAS_Prj_Project_Delete:
		public const string WAS_Prj_Project_Delete = "ErridnInventory.WAS_Prj_Project_Delete"; //// //WAS_Prj_Project_Delete

		// Prj_Project_Approve:
		public const string Prj_Project_Approve = "ErridnInventory.Prj_Project_Approve"; //// //Prj_Project_Approve
		public const string Prj_Project_Approve_AssetDtlNotFound = "ErridnInventory.Prj_Project_Approve_AssetDtlNotFound"; //// //Prj_Project_Approve_AssetDtlNotFound

		// Prj_Project_UpdAppr:
		public const string Prj_Project_UpdAppr = "ErridnInventory.Prj_Project_UpdAppr"; //// //Prj_Project_UpdAppr
		public const string Prj_Project_UpdAppr_InvalidBGStatus = "ErridnInventory.Prj_Project_UpdAppr_InvalidBGStatus"; //// //Prj_Project_UpdAppr_InvalidBGStatus
		public const string Prj_Project_UpdAppr_InvalidValue = "ErridnInventory.Prj_Project_UpdAppr_InvalidValue"; //// //Prj_Project_UpdAppr_InvalidValue
		public const string Prj_Project_UpdAppr_InvalidSignedDate = "ErridnInventory.Prj_Project_UpdAppr_InvalidSignedDate"; //// //Prj_Project_UpdAppr_InvalidSignedDate
		public const string Prj_Project_UpdAppr_InvalidSellerCode = "ErridnInventory.Prj_Project_UpdAppr_InvalidSellerCode"; //// //Prj_Project_UpdAppr_InvalidSellerCode
		public const string Prj_Project_UpdAppr_Input_Prj_ProjectDtlTblNotFound = "ErridnInventory.Prj_Project_UpdAppr_Input_Prj_ProjectDtlTblNotFound"; //// //Prj_Project_UpdAppr_Input_Prj_ProjectDtlTblNotFound		
		public const string Prj_Project_UpdAppr_Input_Prj_ProjectDtlTblInvalid = "ErridnInventory.Prj_Project_UpdAppr_Input_Prj_ProjectDtlTblInvalid"; //// //Prj_Project_UpdAppr_Input_Prj_ProjectDtlTblInvalid		
		#endregion

		#region // Prj_BidPackage:
		// Prj_BidPackage_CheckDB:
		public const string Prj_BidPackage_CheckDB_BidPackageNotFound = "ErridnInventory.Prj_BidPackage_CheckDB_BidPackageNotFound"; //// //Prj_BidPackage_CheckDB_BidPackageNotFound
		public const string Prj_BidPackage_CheckDB_BidPackageExist = "ErridnInventory.Prj_BidPackage_CheckDB_BidPackageExist"; //// //Prj_BidPackage_CheckDB_BidPackageExist
		public const string Prj_BidPackage_CheckDB_StatusNotMatched = "ErridnInventory.Prj_BidPackage_CheckDB_StatusNotMatched"; //// //Prj_BidPackage_CheckDB_StatusNotMatched

		// Prj_BidPackage_Get:
		public const string Prj_BidPackage_Get = "ErridnInventory.Prj_BidPackage_Get"; //// //Prj_BidPackage_Get

		// Prj_BidPackage_Del:
		public const string Prj_BidPackage_Del = "ErridnInventory.Prj_BidPackage_Del"; //// //Prj_BidPackage_Del

		// WAS_Prj_BidPackage_Get:
		public const string WAS_Prj_BidPackage_Get = "ErridnInventory.WAS_Prj_BidPackage_Get"; //// //WAS_Prj_BidPackage_Get

		// WAS_Prj_BidPackage_Save:
		public const string WAS_Prj_BidPackage_Save = "ErridnInventory.WAS_Prj_BidPackage_Save"; //// //WAS_Prj_BidPackage_Save


		// Prj_BidPackage_Save:
		public const string Prj_BidPackage_Save = "ErridnInventory.Prj_BidPackage_Save"; //// //Prj_BidPackage_Save
		public const string Prj_BidPackage_Save_InvalidBGStatus = "ErridnInventory.Prj_BidPackage_Save_InvalidBGStatus"; //// //Prj_BidPackage_Save_InvalidBGStatus
		public const string Prj_BidPackage_Save_InvalidValue = "ErridnInventory.Prj_BidPackage_Save_InvalidValue"; //// //Prj_BidPackage_Save_InvalidValue
		public const string Prj_BidPackage_Save_InvalidYear = "ErridnInventory.Prj_BidPackage_Save_InvalidYear"; //// //Prj_BidPackage_Save_InvalidYear
		public const string Prj_BidPackage_Save_InvalidSignedDate = "ErridnInventory.Prj_BidPackage_Save_InvalidSignedDate"; //// //Prj_BidPackage_Save_InvalidSignedDate
		public const string Prj_BidPackage_Save_InvalidSellerCode = "ErridnInventory.Prj_BidPackage_Save_InvalidSellerCode"; //// //Prj_BidPackage_Save_InvalidSellerCode
		public const string Prj_BidPackage_Save_Input_Prj_BidPackageDtlTblNotFound = "ErridnInventory.Prj_BidPackage_Save_Input_Prj_BidPackageDtlTblNotFound"; //// //Prj_BidPackage_Save_Input_Prj_BidPackageDtlTblNotFound		
		public const string Prj_BidPackage_Save_Input_Prj_BidPackageDtlTblInvalid = "ErridnInventory.Prj_BidPackage_Save_Input_Prj_BidPackageDtlTblInvalid"; //// //Prj_BidPackage_Save_Input_Prj_BidPackageDtlTblInvalid		
		public const string Prj_BidPackage_Save_Input_InvalidOrganCode = "ErridnInventory.Prj_BidPackage_Save_Input_InvalidOrganCode"; //// //Prj_BidPackage_Save_Input_InvalidOrganCode		

		// Prj_BidPackage_Add:
		public const string Prj_BidPackage_Add = "ErridnInventory.Prj_BidPackage_Add"; //// //Prj_BidPackage_Add
		public const string Prj_BidPackage_Add_InvalidBGStatus = "ErridnInventory.Prj_BidPackage_Add_InvalidBGStatus"; //// //Prj_BidPackage_Add_InvalidBGStatus
		public const string Prj_BidPackage_Add_InvalidValue = "ErridnInventory.Prj_BidPackage_Add_InvalidValue"; //// //Prj_BidPackage_Add_InvalidValue
		public const string Prj_BidPackage_Add_InvalidYear = "ErridnInventory.Prj_BidPackage_Add_InvalidYear"; //// //Prj_BidPackage_Add_InvalidYear
		public const string Prj_BidPackage_Add_InvalidSignedDate = "ErridnInventory.Prj_BidPackage_Add_InvalidSignedDate"; //// //Prj_BidPackage_Add_InvalidSignedDate
		public const string Prj_BidPackage_Add_InvalidSellerCode = "ErridnInventory.Prj_BidPackage_Add_InvalidSellerCode"; //// //Prj_BidPackage_Add_InvalidSellerCode
		public const string Prj_BidPackage_Add_Input_Prj_BidPackageTblNotFound = "ErridnInventory.Prj_BidPackage_Add_Input_Prj_BidPackageTblNotFound"; //// //Prj_BidPackage_Add_Input_Prj_BidPackageTblNotFound		
		public const string Prj_BidPackage_Add_Input_Prj_BidPackageTblInvalid = "ErridnInventory.Prj_BidPackage_Add_Input_Prj_BidPackageTblInvalid"; //// //Prj_BidPackage_Add_Input_Prj_BidPackageTblInvalid		
		public const string Prj_BidPackage_Add_Input_InvalidAssetType = "ErridnInventory.Prj_BidPackage_Add_Input_InvalidAssetType"; //// //Prj_BidPackage_Add_Input_InvalidAssetType		
		public const string Prj_BidPackage_Add_Input_InvalidOrganCode = "ErridnInventory.Prj_BidPackage_Add_Input_InvalidOrganCode"; //// //Prj_BidPackage_Add_Input_InvalidOrganCode		

		// Prj_BidPackage_Upd:
		public const string Prj_BidPackage_Upd = "ErridnInventory.Prj_BidPackage_Upd"; //// //Prj_BidPackage_Upd
		public const string Prj_BidPackage_Upd_InvalidBGStatus = "ErridnInventory.Prj_BidPackage_Upd_InvalidBGStatus"; //// //Prj_BidPackage_Upd_InvalidBGStatus
		public const string Prj_BidPackage_Upd_InvalidValue = "ErridnInventory.Prj_BidPackage_Upd_InvalidValue"; //// //Prj_BidPackage_Upd_InvalidValue
		public const string Prj_BidPackage_Upd_InvalidYear = "ErridnInventory.Prj_BidPackage_Upd_InvalidYear"; //// //Prj_BidPackage_Upd_InvalidYear
		public const string Prj_BidPackage_Upd_InvalidSignedDate = "ErridnInventory.Prj_BidPackage_Upd_InvalidSignedDate"; //// //Prj_BidPackage_Upd_InvalidSignedDate
		public const string Prj_BidPackage_Upd_InvalidSellerCode = "ErridnInventory.Prj_BidPackage_Upd_InvalidSellerCode"; //// //Prj_BidPackage_Upd_InvalidSellerCode
		public const string Prj_BidPackage_Upd_Input_Prj_BidPackageTblNotFound = "ErridnInventory.Prj_BidPackage_Upd_Input_Prj_BidPackageTblNotFound"; //// //Prj_BidPackage_Upd_Input_Prj_BidPackageTblNotFound		
		public const string Prj_BidPackage_Upd_Input_Prj_BidPackageTblInvalid = "ErridnInventory.Prj_BidPackage_Upd_Input_Prj_BidPackageTblInvalid"; //// //Prj_BidPackage_Upd_Input_Prj_BidPackageTblInvalid		
		public const string Prj_BidPackage_Upd_Input_InvalidAssetType = "ErridnInventory.Prj_BidPackage_Upd_Input_InvalidAssetType"; //// //Prj_BidPackage_Upd_Input_InvalidAssetType		
		public const string Prj_BidPackage_Upd_Input_InvalidOrganCode = "ErridnInventory.Prj_BidPackage_Upd_Input_InvalidOrganCode"; //// //Prj_BidPackage_Upd_Input_InvalidOrganCode		

		// Prj_BidPackage_InActive:
		public const string Prj_BidPackage_InActive = "ErridnInventory.Prj_BidPackage_InActive"; //// //Prj_BidPackage_InActive
		public const string Prj_BidPackage_InActive_InvalidBGStatus = "ErridnInventory.Prj_BidPackage_InActive_InvalidBGStatus"; //// //Prj_BidPackage_InActive_InvalidBGStatus
		public const string Prj_BidPackage_InActive_InvalidValue = "ErridnInventory.Prj_BidPackage_InActive_InvalidValue"; //// //Prj_BidPackage_InActive_InvalidValue
		public const string Prj_BidPackage_InActive_InvalidYear = "ErridnInventory.Prj_BidPackage_InActive_InvalidYear"; //// //Prj_BidPackage_InActive_InvalidYear
		public const string Prj_BidPackage_InActive_InvalidSignedDate = "ErridnInventory.Prj_BidPackage_InActive_InvalidSignedDate"; //// //Prj_BidPackage_InActive_InvalidSignedDate
		public const string Prj_BidPackage_InActive_InvalidSellerCode = "ErridnInventory.Prj_BidPackage_InActive_InvalidSellerCode"; //// //Prj_BidPackage_InActive_InvalidSellerCode
		public const string Prj_BidPackage_InActive_Input_Prj_BidPackageTblNotFound = "ErridnInventory.Prj_BidPackage_InActive_Input_Prj_BidPackageTblNotFound"; //// //Prj_BidPackage_InActive_Input_Prj_BidPackageTblNotFound		
		public const string Prj_BidPackage_InActive_Input_Prj_BidPackageTblInvalid = "ErridnInventory.Prj_BidPackage_InActive_Input_Prj_BidPackageTblInvalid"; //// //Prj_BidPackage_InActive_Input_Prj_BidPackageTblInvalid		
		public const string Prj_BidPackage_InActive_Input_InvalidAssetType = "ErridnInventory.Prj_BidPackage_InActive_Input_InvalidAssetType"; //// //Prj_BidPackage_InActive_Input_InvalidAssetType		
		public const string Prj_BidPackage_InActive_Input_InvalidOrganCode = "ErridnInventory.Prj_BidPackage_InActive_Input_InvalidOrganCode"; //// //Prj_BidPackage_InActive_Input_InvalidOrganCode		


		// WAS_Prj_BidPackage_Add:
		public const string WAS_Prj_BidPackage_Add = "ErridnInventory.WAS_Prj_BidPackage_Add"; //// //WAS_Prj_BidPackage_Add

		// WAS_Prj_BidPackage_Upd:
		public const string WAS_Prj_BidPackage_Upd = "ErridnInventory.WAS_Prj_BidPackage_Upd"; //// //WAS_Prj_BidPackage_Upd

		// WAS_Prj_BidPackage_InActive:
		public const string WAS_Prj_BidPackage_InActive = "ErridnInventory.WAS_Prj_BidPackage_InActive"; //// //WAS_Prj_BidPackage_InActive

		// WAS_Prj_BidPackage_Delete:
		public const string WAS_Prj_BidPackage_Delete = "ErridnInventory.WAS_Prj_BidPackage_Delete"; //// //WAS_Prj_BidPackage_Delete

		// Prj_BidPackage_Approve:
		public const string Prj_BidPackage_Approve = "ErridnInventory.Prj_BidPackage_Approve"; //// //Prj_BidPackage_Approve
		public const string Prj_BidPackage_Approve_AssetDtlNotFound = "ErridnInventory.Prj_BidPackage_Approve_AssetDtlNotFound"; //// //Prj_BidPackage_Approve_AssetDtlNotFound

		// Prj_BidPackage_UpdAppr:
		public const string Prj_BidPackage_UpdAppr = "ErridnInventory.Prj_BidPackage_UpdAppr"; //// //Prj_BidPackage_UpdAppr
		public const string Prj_BidPackage_UpdAppr_InvalidBGStatus = "ErridnInventory.Prj_BidPackage_UpdAppr_InvalidBGStatus"; //// //Prj_BidPackage_UpdAppr_InvalidBGStatus
		public const string Prj_BidPackage_UpdAppr_InvalidValue = "ErridnInventory.Prj_BidPackage_UpdAppr_InvalidValue"; //// //Prj_BidPackage_UpdAppr_InvalidValue
		public const string Prj_BidPackage_UpdAppr_InvalidSignedDate = "ErridnInventory.Prj_BidPackage_UpdAppr_InvalidSignedDate"; //// //Prj_BidPackage_UpdAppr_InvalidSignedDate
		public const string Prj_BidPackage_UpdAppr_InvalidSellerCode = "ErridnInventory.Prj_BidPackage_UpdAppr_InvalidSellerCode"; //// //Prj_BidPackage_UpdAppr_InvalidSellerCode
		public const string Prj_BidPackage_UpdAppr_Input_Prj_BidPackageDtlTblNotFound = "ErridnInventory.Prj_BidPackage_UpdAppr_Input_Prj_BidPackageDtlTblNotFound"; //// //Prj_BidPackage_UpdAppr_Input_Prj_BidPackageDtlTblNotFound		
		public const string Prj_BidPackage_UpdAppr_Input_Prj_BidPackageDtlTblInvalid = "ErridnInventory.Prj_BidPackage_UpdAppr_Input_Prj_BidPackageDtlTblInvalid"; //// //Prj_BidPackage_UpdAppr_Input_Prj_BidPackageDtlTblInvalid		
		#endregion

		#region // Ctr_ContractPrincipal:
		// Ctr_ContractPrincipal_CheckDB:
		public const string Ctr_ContractPrincipal_CheckDB_ContractPrincipalNotFound = "ErridnInventory.Ctr_ContractPrincipal_CheckDB_ContractPrincipalNotFound"; //// //Ctr_ContractPrincipal_CheckDB_ContractPrincipalNotFound
		public const string Ctr_ContractPrincipal_CheckDB_ContractPrincipalExist = "ErridnInventory.Ctr_ContractPrincipal_CheckDB_ContractPrincipalExist"; //// //Ctr_ContractPrincipal_CheckDB_ContractPrincipalExist
		public const string Ctr_ContractPrincipal_CheckDB_StatusNotMatched = "ErridnInventory.Ctr_ContractPrincipal_CheckDB_StatusNotMatched"; //// //Ctr_ContractPrincipal_CheckDB_StatusNotMatched

		// Ctr_ContractPrincipal_Get:
		public const string Ctr_ContractPrincipal_Get = "ErridnInventory.Ctr_ContractPrincipal_Get"; //// //Ctr_ContractPrincipal_Get

		// Ctr_ContractPrincipal_Del:
		public const string Ctr_ContractPrincipal_Del = "ErridnInventory.Ctr_ContractPrincipal_Del"; //// //Ctr_ContractPrincipal_Del

		// WAS_Ctr_ContractPrincipal_Get:
		public const string WAS_Ctr_ContractPrincipal_Get = "ErridnInventory.WAS_Ctr_ContractPrincipal_Get"; //// //WAS_Ctr_ContractPrincipal_Get

		// WAS_Ctr_ContractPrincipal_Save:
		public const string WAS_Ctr_ContractPrincipal_Save = "ErridnInventory.WAS_Ctr_ContractPrincipal_Save"; //// //WAS_Ctr_ContractPrincipal_Save


		// Ctr_ContractPrincipal_Save:
		public const string Ctr_ContractPrincipal_Save = "ErridnInventory.Ctr_ContractPrincipal_Save"; //// //Ctr_ContractPrincipal_Save
		public const string Ctr_ContractPrincipal_Save_InvalidBGStatus = "ErridnInventory.Ctr_ContractPrincipal_Save_InvalidBGStatus"; //// //Ctr_ContractPrincipal_Save_InvalidBGStatus
		public const string Ctr_ContractPrincipal_Save_InvalidValue = "ErridnInventory.Ctr_ContractPrincipal_Save_InvalidValue"; //// //Ctr_ContractPrincipal_Save_InvalidValue
		public const string Ctr_ContractPrincipal_Save_InvalidYear = "ErridnInventory.Ctr_ContractPrincipal_Save_InvalidYear"; //// //Ctr_ContractPrincipal_Save_InvalidYear
		public const string Ctr_ContractPrincipal_Save_InvalidSignedDate = "ErridnInventory.Ctr_ContractPrincipal_Save_InvalidSignedDate"; //// //Ctr_ContractPrincipal_Save_InvalidSignedDate
		public const string Ctr_ContractPrincipal_Save_InvalidSellerCode = "ErridnInventory.Ctr_ContractPrincipal_Save_InvalidSellerCode"; //// //Ctr_ContractPrincipal_Save_InvalidSellerCode
		public const string Ctr_ContractPrincipal_Save_Input_Ctr_ContractPrincipalDtlTblNotFound = "ErridnInventory.Ctr_ContractPrincipal_Save_Input_Ctr_ContractPrincipalDtlTblNotFound"; //// //Ctr_ContractPrincipal_Save_Input_Ctr_ContractPrincipalDtlTblNotFound		
		public const string Ctr_ContractPrincipal_Save_Input_Ctr_ContractPrincipalDtlTblInvalid = "ErridnInventory.Ctr_ContractPrincipal_Save_Input_Ctr_ContractPrincipalDtlTblInvalid"; //// //Ctr_ContractPrincipal_Save_Input_Ctr_ContractPrincipalDtlTblInvalid		
		public const string Ctr_ContractPrincipal_Save_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractPrincipal_Save_Input_InvalidOrganCode"; //// //Ctr_ContractPrincipal_Save_Input_InvalidOrganCode		

		// Ctr_ContractPrincipal_Add:
		public const string Ctr_ContractPrincipal_Add = "ErridnInventory.Ctr_ContractPrincipal_Add"; //// //Ctr_ContractPrincipal_Add
		public const string Ctr_ContractPrincipal_Add_InvalidBGStatus = "ErridnInventory.Ctr_ContractPrincipal_Add_InvalidBGStatus"; //// //Ctr_ContractPrincipal_Add_InvalidBGStatus
		public const string Ctr_ContractPrincipal_Add_InvalidValue = "ErridnInventory.Ctr_ContractPrincipal_Add_InvalidValue"; //// //Ctr_ContractPrincipal_Add_InvalidValue
		public const string Ctr_ContractPrincipal_Add_InvalidYear = "ErridnInventory.Ctr_ContractPrincipal_Add_InvalidYear"; //// //Ctr_ContractPrincipal_Add_InvalidYear
		public const string Ctr_ContractPrincipal_Add_InvalidSignedDate = "ErridnInventory.Ctr_ContractPrincipal_Add_InvalidSignedDate"; //// //Ctr_ContractPrincipal_Add_InvalidSignedDate
		public const string Ctr_ContractPrincipal_Add_InvalidSellerCode = "ErridnInventory.Ctr_ContractPrincipal_Add_InvalidSellerCode"; //// //Ctr_ContractPrincipal_Add_InvalidSellerCode
		public const string Ctr_ContractPrincipal_Add_Input_Ctr_ContractPrincipalTblNotFound = "ErridnInventory.Ctr_ContractPrincipal_Add_Input_Ctr_ContractPrincipalTblNotFound"; //// //Ctr_ContractPrincipal_Add_Input_Ctr_ContractPrincipalTblNotFound		
		public const string Ctr_ContractPrincipal_Add_Input_Ctr_ContractPrincipalTblInvalid = "ErridnInventory.Ctr_ContractPrincipal_Add_Input_Ctr_ContractPrincipalTblInvalid"; //// //Ctr_ContractPrincipal_Add_Input_Ctr_ContractPrincipalTblInvalid		
		public const string Ctr_ContractPrincipal_Add_Input_InvalidAssetType = "ErridnInventory.Ctr_ContractPrincipal_Add_Input_InvalidAssetType"; //// //Ctr_ContractPrincipal_Add_Input_InvalidAssetType		
		public const string Ctr_ContractPrincipal_Add_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractPrincipal_Add_Input_InvalidOrganCode"; //// //Ctr_ContractPrincipal_Add_Input_InvalidOrganCode		

		// Ctr_ContractPrincipal_Upd:
		public const string Ctr_ContractPrincipal_Upd = "ErridnInventory.Ctr_ContractPrincipal_Upd"; //// //Ctr_ContractPrincipal_Upd
		public const string Ctr_ContractPrincipal_Upd_InvalidBGStatus = "ErridnInventory.Ctr_ContractPrincipal_Upd_InvalidBGStatus"; //// //Ctr_ContractPrincipal_Upd_InvalidBGStatus
		public const string Ctr_ContractPrincipal_Upd_InvalidValue = "ErridnInventory.Ctr_ContractPrincipal_Upd_InvalidValue"; //// //Ctr_ContractPrincipal_Upd_InvalidValue
		public const string Ctr_ContractPrincipal_Upd_InvalidYear = "ErridnInventory.Ctr_ContractPrincipal_Upd_InvalidYear"; //// //Ctr_ContractPrincipal_Upd_InvalidYear
		public const string Ctr_ContractPrincipal_Upd_InvalidSignedDate = "ErridnInventory.Ctr_ContractPrincipal_Upd_InvalidSignedDate"; //// //Ctr_ContractPrincipal_Upd_InvalidSignedDate
		public const string Ctr_ContractPrincipal_Upd_InvalidSellerCode = "ErridnInventory.Ctr_ContractPrincipal_Upd_InvalidSellerCode"; //// //Ctr_ContractPrincipal_Upd_InvalidSellerCode
		public const string Ctr_ContractPrincipal_Upd_Input_Ctr_ContractPrincipalTblNotFound = "ErridnInventory.Ctr_ContractPrincipal_Upd_Input_Ctr_ContractPrincipalTblNotFound"; //// //Ctr_ContractPrincipal_Upd_Input_Ctr_ContractPrincipalTblNotFound		
		public const string Ctr_ContractPrincipal_Upd_Input_Ctr_ContractPrincipalTblInvalid = "ErridnInventory.Ctr_ContractPrincipal_Upd_Input_Ctr_ContractPrincipalTblInvalid"; //// //Ctr_ContractPrincipal_Upd_Input_Ctr_ContractPrincipalTblInvalid		
		public const string Ctr_ContractPrincipal_Upd_Input_InvalidAssetType = "ErridnInventory.Ctr_ContractPrincipal_Upd_Input_InvalidAssetType"; //// //Ctr_ContractPrincipal_Upd_Input_InvalidAssetType		
		public const string Ctr_ContractPrincipal_Upd_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractPrincipal_Upd_Input_InvalidOrganCode"; //// //Ctr_ContractPrincipal_Upd_Input_InvalidOrganCode		

		// Ctr_ContractPrincipal_InActive:
		public const string Ctr_ContractPrincipal_InActive = "ErridnInventory.Ctr_ContractPrincipal_InActive"; //// //Ctr_ContractPrincipal_InActive
		public const string Ctr_ContractPrincipal_InActive_InvalidBGStatus = "ErridnInventory.Ctr_ContractPrincipal_InActive_InvalidBGStatus"; //// //Ctr_ContractPrincipal_InActive_InvalidBGStatus
		public const string Ctr_ContractPrincipal_InActive_InvalidValue = "ErridnInventory.Ctr_ContractPrincipal_InActive_InvalidValue"; //// //Ctr_ContractPrincipal_InActive_InvalidValue
		public const string Ctr_ContractPrincipal_InActive_InvalidYear = "ErridnInventory.Ctr_ContractPrincipal_InActive_InvalidYear"; //// //Ctr_ContractPrincipal_InActive_InvalidYear
		public const string Ctr_ContractPrincipal_InActive_InvalidSignedDate = "ErridnInventory.Ctr_ContractPrincipal_InActive_InvalidSignedDate"; //// //Ctr_ContractPrincipal_InActive_InvalidSignedDate
		public const string Ctr_ContractPrincipal_InActive_InvalidSellerCode = "ErridnInventory.Ctr_ContractPrincipal_InActive_InvalidSellerCode"; //// //Ctr_ContractPrincipal_InActive_InvalidSellerCode
		public const string Ctr_ContractPrincipal_InActive_Input_Ctr_ContractPrincipalTblNotFound = "ErridnInventory.Ctr_ContractPrincipal_InActive_Input_Ctr_ContractPrincipalTblNotFound"; //// //Ctr_ContractPrincipal_InActive_Input_Ctr_ContractPrincipalTblNotFound		
		public const string Ctr_ContractPrincipal_InActive_Input_Ctr_ContractPrincipalTblInvalid = "ErridnInventory.Ctr_ContractPrincipal_InActive_Input_Ctr_ContractPrincipalTblInvalid"; //// //Ctr_ContractPrincipal_InActive_Input_Ctr_ContractPrincipalTblInvalid		
		public const string Ctr_ContractPrincipal_InActive_Input_InvalidAssetType = "ErridnInventory.Ctr_ContractPrincipal_InActive_Input_InvalidAssetType"; //// //Ctr_ContractPrincipal_InActive_Input_InvalidAssetType		
		public const string Ctr_ContractPrincipal_InActive_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractPrincipal_InActive_Input_InvalidOrganCode"; //// //Ctr_ContractPrincipal_InActive_Input_InvalidOrganCode		


		// WAS_Ctr_ContractPrincipal_Add:
		public const string WAS_Ctr_ContractPrincipal_Add = "ErridnInventory.WAS_Ctr_ContractPrincipal_Add"; //// //WAS_Ctr_ContractPrincipal_Add

		// WAS_Ctr_ContractPrincipal_Upd:
		public const string WAS_Ctr_ContractPrincipal_Upd = "ErridnInventory.WAS_Ctr_ContractPrincipal_Upd"; //// //WAS_Ctr_ContractPrincipal_Upd

		// WAS_Ctr_ContractPrincipal_InActive:
		public const string WAS_Ctr_ContractPrincipal_InActive = "ErridnInventory.WAS_Ctr_ContractPrincipal_InActive"; //// //WAS_Ctr_ContractPrincipal_InActive

		// WAS_Ctr_ContractPrincipal_Delete:
		public const string WAS_Ctr_ContractPrincipal_Delete = "ErridnInventory.WAS_Ctr_ContractPrincipal_Delete"; //// //WAS_Ctr_ContractPrincipal_Delete

		// Ctr_ContractPrincipal_Approve:
		public const string Ctr_ContractPrincipal_Approve = "ErridnInventory.Ctr_ContractPrincipal_Approve"; //// //Ctr_ContractPrincipal_Approve
		public const string Ctr_ContractPrincipal_Approve_AssetDtlNotFound = "ErridnInventory.Ctr_ContractPrincipal_Approve_AssetDtlNotFound"; //// //Ctr_ContractPrincipal_Approve_AssetDtlNotFound

		// Ctr_ContractPrincipal_UpdAppr:
		public const string Ctr_ContractPrincipal_UpdAppr = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr"; //// //Ctr_ContractPrincipal_UpdAppr
		public const string Ctr_ContractPrincipal_UpdAppr_InvalidBGStatus = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr_InvalidBGStatus"; //// //Ctr_ContractPrincipal_UpdAppr_InvalidBGStatus
		public const string Ctr_ContractPrincipal_UpdAppr_InvalidValue = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr_InvalidValue"; //// //Ctr_ContractPrincipal_UpdAppr_InvalidValue
		public const string Ctr_ContractPrincipal_UpdAppr_InvalidSignedDate = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr_InvalidSignedDate"; //// //Ctr_ContractPrincipal_UpdAppr_InvalidSignedDate
		public const string Ctr_ContractPrincipal_UpdAppr_InvalidSellerCode = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr_InvalidSellerCode"; //// //Ctr_ContractPrincipal_UpdAppr_InvalidSellerCode
		public const string Ctr_ContractPrincipal_UpdAppr_Input_Ctr_ContractPrincipalDtlTblNotFound = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr_Input_Ctr_ContractPrincipalDtlTblNotFound"; //// //Ctr_ContractPrincipal_UpdAppr_Input_Ctr_ContractPrincipalDtlTblNotFound		
		public const string Ctr_ContractPrincipal_UpdAppr_Input_Ctr_ContractPrincipalDtlTblInvalid = "ErridnInventory.Ctr_ContractPrincipal_UpdAppr_Input_Ctr_ContractPrincipalDtlTblInvalid"; //// //Ctr_ContractPrincipal_UpdAppr_Input_Ctr_ContractPrincipalDtlTblInvalid		
		#endregion

		#region // Ctr_Contract:
		// Ctr_Contract_CheckDB:
		public const string Ctr_Contract_CheckDB_ContractNotFound = "ErridnInventory.Ctr_Contract_CheckDB_ContractNotFound"; //// //Ctr_Contract_CheckDB_ContractNotFound
		public const string Ctr_Contract_CheckDB_ContractExist = "ErridnInventory.Ctr_Contract_CheckDB_ContractExist"; //// //Ctr_Contract_CheckDB_ContractExist
		public const string Ctr_Contract_CheckDB_StatusNotMatched = "ErridnInventory.Ctr_Contract_CheckDB_StatusNotMatched"; //// //Ctr_Contract_CheckDB_StatusNotMatched

		// Ctr_Contract_Get:
		public const string Ctr_Contract_Get = "ErridnInventory.Ctr_Contract_Get"; //// //Ctr_Contract_Get

		// Ctr_Contract_Del:
		public const string Ctr_Contract_Del = "ErridnInventory.Ctr_Contract_Del"; //// //Ctr_Contract_Del

		// WAS_Ctr_Contract_Get:
		public const string WAS_Ctr_Contract_Get = "ErridnInventory.WAS_Ctr_Contract_Get"; //// //WAS_Ctr_Contract_Get

		// WAS_Ctr_Contract_Save:
		public const string WAS_Ctr_Contract_Save = "ErridnInventory.WAS_Ctr_Contract_Save"; //// //WAS_Ctr_Contract_Save


		// Ctr_Contract_Save:
		public const string Ctr_Contract_Save = "ErridnInventory.Ctr_Contract_Save"; //// //Ctr_Contract_Save
		public const string Ctr_Contract_Save_InvalidBGStatus = "ErridnInventory.Ctr_Contract_Save_InvalidBGStatus"; //// //Ctr_Contract_Save_InvalidBGStatus
		public const string Ctr_Contract_Save_InvalidValue = "ErridnInventory.Ctr_Contract_Save_InvalidValue"; //// //Ctr_Contract_Save_InvalidValue
		public const string Ctr_Contract_Save_InvalidYear = "ErridnInventory.Ctr_Contract_Save_InvalidYear"; //// //Ctr_Contract_Save_InvalidYear
		public const string Ctr_Contract_Save_InvalidSignedDate = "ErridnInventory.Ctr_Contract_Save_InvalidSignedDate"; //// //Ctr_Contract_Save_InvalidSignedDate
		public const string Ctr_Contract_Save_InvalidSellerCode = "ErridnInventory.Ctr_Contract_Save_InvalidSellerCode"; //// //Ctr_Contract_Save_InvalidSellerCode
		public const string Ctr_Contract_Save_Input_Ctr_ContractDtlTblNotFound = "ErridnInventory.Ctr_Contract_Save_Input_Ctr_ContractDtlTblNotFound"; //// //Ctr_Contract_Save_Input_Ctr_ContractDtlTblNotFound		
		public const string Ctr_Contract_Save_Input_Ctr_ContractDtlTblInvalid = "ErridnInventory.Ctr_Contract_Save_Input_Ctr_ContractDtlTblInvalid"; //// //Ctr_Contract_Save_Input_Ctr_ContractDtlTblInvalid		
		public const string Ctr_Contract_Save_Input_InvalidOrganCode = "ErridnInventory.Ctr_Contract_Save_Input_InvalidOrganCode"; //// //Ctr_Contract_Save_Input_InvalidOrganCode		

		// Ctr_Contract_Add:
		public const string Ctr_Contract_Add = "ErridnInventory.Ctr_Contract_Add"; //// //Ctr_Contract_Add
		public const string Ctr_Contract_Add_InvalidBGStatus = "ErridnInventory.Ctr_Contract_Add_InvalidBGStatus"; //// //Ctr_Contract_Add_InvalidBGStatus
		public const string Ctr_Contract_Add_InvalidValue = "ErridnInventory.Ctr_Contract_Add_InvalidValue"; //// //Ctr_Contract_Add_InvalidValue
		public const string Ctr_Contract_Add_InvalidYear = "ErridnInventory.Ctr_Contract_Add_InvalidYear"; //// //Ctr_Contract_Add_InvalidYear
		public const string Ctr_Contract_Add_InvalidSignedDate = "ErridnInventory.Ctr_Contract_Add_InvalidSignedDate"; //// //Ctr_Contract_Add_InvalidSignedDate
		public const string Ctr_Contract_Add_InvalidSellerCode = "ErridnInventory.Ctr_Contract_Add_InvalidSellerCode"; //// //Ctr_Contract_Add_InvalidSellerCode
		public const string Ctr_Contract_Add_Input_Ctr_ContractTblNotFound = "ErridnInventory.Ctr_Contract_Add_Input_Ctr_ContractTblNotFound"; //// //Ctr_Contract_Add_Input_Ctr_ContractTblNotFound		
		public const string Ctr_Contract_Add_Input_Ctr_ContractTblInvalid = "ErridnInventory.Ctr_Contract_Add_Input_Ctr_ContractTblInvalid"; //// //Ctr_Contract_Add_Input_Ctr_ContractTblInvalid		
		public const string Ctr_Contract_Add_Input_Ctr_ContractDtlTblNotFound = "ErridnInventory.Ctr_Contract_Add_Input_Ctr_ContractDtlTblNotFound"; //// //Ctr_Contract_Add_Input_Ctr_ContractDtlTblNotFound		
		public const string Ctr_Contract_Add_Input_Ctr_ContractDtlTblInvalid = "ErridnInventory.Ctr_Contract_Add_Input_Ctr_ContractDtlTblInvalid"; //// //Ctr_Contract_Add_Input_Ctr_ContractDtlTblInvalid		
		public const string Ctr_Contract_Add_Input_Ctr_ContractPrjPaymentTypeTblNotFound = "ErridnInventory.Ctr_Contract_Add_Input_Ctr_ContractPrjPaymentTypeTblNotFound"; //// //Ctr_Contract_Add_Input_Ctr_ContractPrjPaymentTypeTblNotFound		
		public const string Ctr_Contract_Add_Input_Ctr_ContractPrjPaymentTypeTblInvalid = "ErridnInventory.Ctr_Contract_Add_Input_Ctr_ContractPrjPaymentTypeTblInvalid"; //// //Ctr_Contract_Add_Input_Ctr_ContractPrjPaymentTypeTblInvalid		
		public const string Ctr_Contract_Add_Input_InvalidAssetType = "ErridnInventory.Ctr_Contract_Add_Input_InvalidAssetType"; //// //Ctr_Contract_Add_Input_InvalidAssetType		
		public const string Ctr_Contract_Add_Input_InvalidOrganCode = "ErridnInventory.Ctr_Contract_Add_Input_InvalidOrganCode"; //// //Ctr_Contract_Add_Input_InvalidOrganCode		
		public const string Ctr_Contract_Add_Input_InvalidValue = "ErridnInventory.Ctr_Contract_Add_Input_InvalidValue"; //// //Ctr_Contract_Add_Input_InvalidValue		

		// Ctr_Contract_Upd:
		public const string Ctr_Contract_Upd = "ErridnInventory.Ctr_Contract_Upd"; //// //Ctr_Contract_Upd
		public const string Ctr_Contract_Upd_InvalidBGStatus = "ErridnInventory.Ctr_Contract_Upd_InvalidBGStatus"; //// //Ctr_Contract_Upd_InvalidBGStatus
		public const string Ctr_Contract_Upd_InvalidValue = "ErridnInventory.Ctr_Contract_Upd_InvalidValue"; //// //Ctr_Contract_Upd_InvalidValue
		public const string Ctr_Contract_Upd_InvalidYear = "ErridnInventory.Ctr_Contract_Upd_InvalidYear"; //// //Ctr_Contract_Upd_InvalidYear
		public const string Ctr_Contract_Upd_InvalidSignedDate = "ErridnInventory.Ctr_Contract_Upd_InvalidSignedDate"; //// //Ctr_Contract_Upd_InvalidSignedDate
		public const string Ctr_Contract_Upd_InvalidSellerCode = "ErridnInventory.Ctr_Contract_Upd_InvalidSellerCode"; //// //Ctr_Contract_Upd_InvalidSellerCode
		public const string Ctr_Contract_Upd_Input_Ctr_ContractTblNotFound = "ErridnInventory.Ctr_Contract_Upd_Input_Ctr_ContractTblNotFound"; //// //Ctr_Contract_Upd_Input_Ctr_ContractTblNotFound		
		public const string Ctr_Contract_Upd_Input_Ctr_ContractTblInvalid = "ErridnInventory.Ctr_Contract_Upd_Input_Ctr_ContractTblInvalid"; //// //Ctr_Contract_Upd_Input_Ctr_ContractTblInvalid		
		public const string Ctr_Contract_Upd_Input_Ctr_ContractDtlTblNotFound = "ErridnInventory.Ctr_Contract_Upd_Input_Ctr_ContractDtlTblNotFound"; //// //Ctr_Contract_Upd_Input_Ctr_ContractDtlTblNotFound		
		public const string Ctr_Contract_Upd_Input_Ctr_ContractDtlTblInvalid = "ErridnInventory.Ctr_Contract_Upd_Input_Ctr_ContractDtlTblInvalid"; //// //Ctr_Contract_Upd_Input_Ctr_ContractDtlTblInvalid		
		public const string Ctr_Contract_Upd_Input_Ctr_ContractPrjPaymentTypeTblNotFound = "ErridnInventory.Ctr_Contract_Upd_Input_Ctr_ContractPrjPaymentTypeTblNotFound"; //// //Ctr_Contract_Upd_Input_Ctr_ContractPrjPaymentTypeTblNotFound		
		public const string Ctr_Contract_Upd_Input_Ctr_ContractPrjPaymentTypeTblInvalid = "ErridnInventory.Ctr_Contract_Upd_Input_Ctr_ContractPrjPaymentTypeTblInvalid"; //// //Ctr_Contract_Upd_Input_Ctr_ContractPrjPaymentTypeTblInvalid		
		public const string Ctr_Contract_Upd_Input_InvalidAssetType = "ErridnInventory.Ctr_Contract_Upd_Input_InvalidAssetType"; //// //Ctr_Contract_Upd_Input_InvalidAssetType		
		public const string Ctr_Contract_Upd_Input_InvalidOrganCode = "ErridnInventory.Ctr_Contract_Upd_Input_InvalidOrganCode"; //// //Ctr_Contract_Upd_Input_InvalidOrganCode		
		public const string Ctr_Contract_Upd_Input_InvalidValue = "ErridnInventory.Ctr_Contract_Upd_Input_InvalidValue"; //// //Ctr_Contract_Upd_Input_InvalidValue		

		// Ctr_Contract_InActive:
		public const string Ctr_Contract_InActive = "ErridnInventory.Ctr_Contract_InActive"; //// //Ctr_Contract_InActive
		public const string Ctr_Contract_InActive_InvalidBGStatus = "ErridnInventory.Ctr_Contract_InActive_InvalidBGStatus"; //// //Ctr_Contract_InActive_InvalidBGStatus
		public const string Ctr_Contract_InActive_InvalidValue = "ErridnInventory.Ctr_Contract_InActive_InvalidValue"; //// //Ctr_Contract_InActive_InvalidValue
		public const string Ctr_Contract_InActive_InvalidYear = "ErridnInventory.Ctr_Contract_InActive_InvalidYear"; //// //Ctr_Contract_InActive_InvalidYear
		public const string Ctr_Contract_InActive_InvalidSignedDate = "ErridnInventory.Ctr_Contract_InActive_InvalidSignedDate"; //// //Ctr_Contract_InActive_InvalidSignedDate
		public const string Ctr_Contract_InActive_InvalidSellerCode = "ErridnInventory.Ctr_Contract_InActive_InvalidSellerCode"; //// //Ctr_Contract_InActive_InvalidSellerCode
		public const string Ctr_Contract_InActive_Input_Ctr_ContractTblNotFound = "ErridnInventory.Ctr_Contract_InActive_Input_Ctr_ContractTblNotFound"; //// //Ctr_Contract_InActive_Input_Ctr_ContractTblNotFound		
		public const string Ctr_Contract_InActive_Input_Ctr_ContractTblInvalid = "ErridnInventory.Ctr_Contract_InActive_Input_Ctr_ContractTblInvalid"; //// //Ctr_Contract_InActive_Input_Ctr_ContractTblInvalid		
		public const string Ctr_Contract_InActive_Input_InvalidAssetType = "ErridnInventory.Ctr_Contract_InActive_Input_InvalidAssetType"; //// //Ctr_Contract_InActive_Input_InvalidAssetType		
		public const string Ctr_Contract_InActive_Input_InvalidOrganCode = "ErridnInventory.Ctr_Contract_InActive_Input_InvalidOrganCode"; //// //Ctr_Contract_InActive_Input_InvalidOrganCode		


		// WAS_Ctr_Contract_Add:
		public const string WAS_Ctr_Contract_Add = "ErridnInventory.WAS_Ctr_Contract_Add"; //// //WAS_Ctr_Contract_Add

		// WAS_Ctr_Contract_Upd:
		public const string WAS_Ctr_Contract_Upd = "ErridnInventory.WAS_Ctr_Contract_Upd"; //// //WAS_Ctr_Contract_Upd

		// WAS_Ctr_Contract_InActive:
		public const string WAS_Ctr_Contract_InActive = "ErridnInventory.WAS_Ctr_Contract_InActive"; //// //WAS_Ctr_Contract_InActive

		// WAS_Ctr_Contract_Delete:
		public const string WAS_Ctr_Contract_Delete = "ErridnInventory.WAS_Ctr_Contract_Delete"; //// //WAS_Ctr_Contract_Delete

		// Ctr_Contract_Approve:
		public const string Ctr_Contract_Approve = "ErridnInventory.Ctr_Contract_Approve"; //// //Ctr_Contract_Approve
		public const string Ctr_Contract_Approve_AssetDtlNotFound = "ErridnInventory.Ctr_Contract_Approve_AssetDtlNotFound"; //// //Ctr_Contract_Approve_AssetDtlNotFound

		// Ctr_Contract_UpdAppr:
		public const string Ctr_Contract_UpdAppr = "ErridnInventory.Ctr_Contract_UpdAppr"; //// //Ctr_Contract_UpdAppr
		public const string Ctr_Contract_UpdAppr_InvalidBGStatus = "ErridnInventory.Ctr_Contract_UpdAppr_InvalidBGStatus"; //// //Ctr_Contract_UpdAppr_InvalidBGStatus
		public const string Ctr_Contract_UpdAppr_InvalidValue = "ErridnInventory.Ctr_Contract_UpdAppr_InvalidValue"; //// //Ctr_Contract_UpdAppr_InvalidValue
		public const string Ctr_Contract_UpdAppr_InvalidSignedDate = "ErridnInventory.Ctr_Contract_UpdAppr_InvalidSignedDate"; //// //Ctr_Contract_UpdAppr_InvalidSignedDate
		public const string Ctr_Contract_UpdAppr_InvalidSellerCode = "ErridnInventory.Ctr_Contract_UpdAppr_InvalidSellerCode"; //// //Ctr_Contract_UpdAppr_InvalidSellerCode
		public const string Ctr_Contract_UpdAppr_Input_Ctr_ContractDtlTblNotFound = "ErridnInventory.Ctr_Contract_UpdAppr_Input_Ctr_ContractDtlTblNotFound"; //// //Ctr_Contract_UpdAppr_Input_Ctr_ContractDtlTblNotFound		
		public const string Ctr_Contract_UpdAppr_Input_Ctr_ContractDtlTblInvalid = "ErridnInventory.Ctr_Contract_UpdAppr_Input_Ctr_ContractDtlTblInvalid"; //// //Ctr_Contract_UpdAppr_Input_Ctr_ContractDtlTblInvalid		
		#endregion

		#region // Budget_ProjectPlan:
		// Budget_ProjectPlan_CheckDB:
		public const string Budget_ProjectPlan_CheckDB_ProjectPlanNotFound = "ErridnInventory.Budget_ProjectPlan_CheckDB_ProjectPlanNotFound"; //// //Budget_ProjectPlan_CheckDB_ProjectPlanNotFound
		public const string Budget_ProjectPlan_CheckDB_ProjectPlanExist = "ErridnInventory.Budget_ProjectPlan_CheckDB_ProjectPlanExist"; //// //Budget_ProjectPlan_CheckDB_ProjectPlanExist
		public const string Budget_ProjectPlan_CheckDB_StatusNotMatched = "ErridnInventory.Budget_ProjectPlan_CheckDB_StatusNotMatched"; //// //Budget_ProjectPlan_CheckDB_StatusNotMatched

		// Budget_ProjectPlan_Get:
		public const string Budget_ProjectPlan_Get = "ErridnInventory.Budget_ProjectPlan_Get"; //// //Budget_ProjectPlan_Get

		// Budget_ProjectPlan_Del:
		public const string Budget_ProjectPlan_Del = "ErridnInventory.Budget_ProjectPlan_Del"; //// //Budget_ProjectPlan_Del

		// WAS_Budget_ProjectPlan_Get:
		public const string WAS_Budget_ProjectPlan_Get = "ErridnInventory.WAS_Budget_ProjectPlan_Get"; //// //WAS_Budget_ProjectPlan_Get

		// WAS_Budget_ProjectPlan_Save:
		public const string WAS_Budget_ProjectPlan_Save = "ErridnInventory.WAS_Budget_ProjectPlan_Save"; //// //WAS_Budget_ProjectPlan_Save


		// Budget_ProjectPlan_Save:
		public const string Budget_ProjectPlan_Save = "ErridnInventory.Budget_ProjectPlan_Save"; //// //Budget_ProjectPlan_Save
		public const string Budget_ProjectPlan_Save_InvalidBGStatus = "ErridnInventory.Budget_ProjectPlan_Save_InvalidBGStatus"; //// //Budget_ProjectPlan_Save_InvalidBGStatus
		public const string Budget_ProjectPlan_Save_InvalidValue = "ErridnInventory.Budget_ProjectPlan_Save_InvalidValue"; //// //Budget_ProjectPlan_Save_InvalidValue
		public const string Budget_ProjectPlan_Save_InvalidYear = "ErridnInventory.Budget_ProjectPlan_Save_InvalidYear"; //// //Budget_ProjectPlan_Save_InvalidYear
		public const string Budget_ProjectPlan_Save_InvalidSignedDate = "ErridnInventory.Budget_ProjectPlan_Save_InvalidSignedDate"; //// //Budget_ProjectPlan_Save_InvalidSignedDate
		public const string Budget_ProjectPlan_Save_InvalidSellerCode = "ErridnInventory.Budget_ProjectPlan_Save_InvalidSellerCode"; //// //Budget_ProjectPlan_Save_InvalidSellerCode
		public const string Budget_ProjectPlan_Save_Input_Budget_ProjectPlanDtlTblNotFound = "ErridnInventory.Budget_ProjectPlan_Save_Input_Budget_ProjectPlanDtlTblNotFound"; //// //Budget_ProjectPlan_Save_Input_Budget_ProjectPlanDtlTblNotFound		
		public const string Budget_ProjectPlan_Save_Input_Budget_ProjectPlanDtlTblInvalid = "ErridnInventory.Budget_ProjectPlan_Save_Input_Budget_ProjectPlanDtlTblInvalid"; //// //Budget_ProjectPlan_Save_Input_Budget_ProjectPlanDtlTblInvalid		
		public const string Budget_ProjectPlan_Save_Input_InvalidOrganCode = "ErridnInventory.Budget_ProjectPlan_Save_Input_InvalidOrganCode"; //// //Budget_ProjectPlan_Save_Input_InvalidOrganCode		

		// Budget_ProjectPlan_Add:
		public const string Budget_ProjectPlan_Add = "ErridnInventory.Budget_ProjectPlan_Add"; //// //Budget_ProjectPlan_Add
		public const string Budget_ProjectPlan_Add_InvalidBGStatus = "ErridnInventory.Budget_ProjectPlan_Add_InvalidBGStatus"; //// //Budget_ProjectPlan_Add_InvalidBGStatus
		public const string Budget_ProjectPlan_Add_InvalidValue = "ErridnInventory.Budget_ProjectPlan_Add_InvalidValue"; //// //Budget_ProjectPlan_Add_InvalidValue
		public const string Budget_ProjectPlan_Add_InvalidYear = "ErridnInventory.Budget_ProjectPlan_Add_InvalidYear"; //// //Budget_ProjectPlan_Add_InvalidYear
		public const string Budget_ProjectPlan_Add_InvalidSignedDate = "ErridnInventory.Budget_ProjectPlan_Add_InvalidSignedDate"; //// //Budget_ProjectPlan_Add_InvalidSignedDate
		public const string Budget_ProjectPlan_Add_InvalidSellerCode = "ErridnInventory.Budget_ProjectPlan_Add_InvalidSellerCode"; //// //Budget_ProjectPlan_Add_InvalidSellerCode
		public const string Budget_ProjectPlan_Add_InvalidEffMonthStart = "ErridnInventory.Budget_ProjectPlan_Add_InvalidEffMonthStart"; //// //Budget_ProjectPlan_Add_InvalidEffMonthStart
		public const string Budget_ProjectPlan_Add_InvalidEffMonthEnd = "ErridnInventory.Budget_ProjectPlan_Add_InvalidEffMonthEnd"; //// //Budget_ProjectPlan_Add_InvalidEffMonthEnd
		public const string Budget_ProjectPlan_Add_InvalidEffMonthStartBeforeEffMonthEnd = "ErridnInventory.Budget_ProjectPlan_Add_InvalidEffMonthStartBeforeEffMonthEnd"; //// //Budget_ProjectPlan_Add_InvalidEffMonthStartBeforeEffMonthEnd
		public const string Budget_ProjectPlan_Add_InvalidEffMonthStartAfterEffMonthStartDB = "ErridnInventory.Budget_ProjectPlan_Add_InvalidEffMonthStartAfterEffMonthStartDB"; //// //Budget_ProjectPlan_Add_InvalidEffMonthStartAfterEffMonthStartDB
		public const string Budget_ProjectPlan_Add_InvalidEffMonthEndBeforeEffMonthEndDB = "ErridnInventory.Budget_ProjectPlan_Add_InvalidEffMonthEndBeforeEffMonthEndDB"; //// //Budget_ProjectPlan_Add_InvalidEffMonthEndBeforeEffMonthEndDB
		public const string Budget_ProjectPlan_Add_Input_Budget_ProjectPlanTblNotFound = "ErridnInventory.Budget_ProjectPlan_Add_Input_Budget_ProjectPlanTblNotFound"; //// //Budget_ProjectPlan_Add_Input_Budget_ProjectPlanTblNotFound		
		public const string Budget_ProjectPlan_Add_Input_Budget_ProjectPlanTblInvalid = "ErridnInventory.Budget_ProjectPlan_Add_Input_Budget_ProjectPlanTblInvalid"; //// //Budget_ProjectPlan_Add_Input_Budget_ProjectPlanTblInvalid		
		public const string Budget_ProjectPlan_Add_Input_Budget_ProjectPlanDtlTblNotFound = "ErridnInventory.Budget_ProjectPlan_Add_Input_Budget_ProjectPlanDtlTblNotFound"; //// //Budget_ProjectPlan_Add_Input_Budget_ProjectPlanDtlTblNotFound		
		public const string Budget_ProjectPlan_Add_Input_Budget_ProjectPlanDtlTblInvalid = "ErridnInventory.Budget_ProjectPlan_Add_Input_Budget_ProjectPlanDtlTblInvalid"; //// //Budget_ProjectPlan_Add_Input_Budget_ProjectPlanDtlTblInvalid		
		public const string Budget_ProjectPlan_Add_Input_Budget_ProjectPlanPrjPaymentTypeTblNotFound = "ErridnInventory.Budget_ProjectPlan_Add_Input_Budget_ProjectPlanPrjPaymentTypeTblNotFound"; //// //Budget_ProjectPlan_Add_Input_Budget_ProjectPlanPrjPaymentTypeTblNotFound		
		public const string Budget_ProjectPlan_Add_Input_Budget_ProjectPlanPrjPaymentTypeTblInvalid = "ErridnInventory.Budget_ProjectPlan_Add_Input_Budget_ProjectPlanPrjPaymentTypeTblInvalid"; //// //Budget_ProjectPlan_Add_Input_Budget_ProjectPlanPrjPaymentTypeTblInvalid		
		public const string Budget_ProjectPlan_Add_Input_InvalidAssetType = "ErridnInventory.Budget_ProjectPlan_Add_Input_InvalidAssetType"; //// //Budget_ProjectPlan_Add_Input_InvalidAssetType		
		public const string Budget_ProjectPlan_Add_Input_InvalidOrganCode = "ErridnInventory.Budget_ProjectPlan_Add_Input_InvalidOrganCode"; //// //Budget_ProjectPlan_Add_Input_InvalidOrganCode		
		public const string Budget_ProjectPlan_Add_Input_InvalidValue = "ErridnInventory.Budget_ProjectPlan_Add_Input_InvalidValue"; //// //Budget_ProjectPlan_Add_Input_InvalidValue		

		// Budget_ProjectPlan_Upd:
		public const string Budget_ProjectPlan_Upd = "ErridnInventory.Budget_ProjectPlan_Upd"; //// //Budget_ProjectPlan_Upd
		public const string Budget_ProjectPlan_Upd_InvalidBGStatus = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidBGStatus"; //// //Budget_ProjectPlan_Upd_InvalidBGStatus
		public const string Budget_ProjectPlan_Upd_InvalidValue = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidValue"; //// //Budget_ProjectPlan_Upd_InvalidValue
		public const string Budget_ProjectPlan_Upd_InvalidYear = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidYear"; //// //Budget_ProjectPlan_Upd_InvalidYear
		public const string Budget_ProjectPlan_Upd_InvalidSignedDate = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidSignedDate"; //// //Budget_ProjectPlan_Upd_InvalidSignedDate
		public const string Budget_ProjectPlan_Upd_InvalidSellerCode = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidSellerCode"; //// //Budget_ProjectPlan_Upd_InvalidSellerCode
		public const string Budget_ProjectPlan_Upd_InvalidEffMonthStart = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidEffMonthStart"; //// //Budget_ProjectPlan_Upd_InvalidEffMonthStart
		public const string Budget_ProjectPlan_Upd_InvalidEffMonthEnd = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidEffMonthEnd"; //// //Budget_ProjectPlan_Upd_InvalidEffMonthEnd
		public const string Budget_ProjectPlan_Upd_InvalidEffMonthStartBeforeEffMonthEnd = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidEffMonthStartBeforeEffMonthEnd"; //// //Budget_ProjectPlan_Upd_InvalidEffMonthStartBeforeEffMonthEnd
		public const string Budget_ProjectPlan_Upd_InvalidEffMonthStartAfterEffMonthStartDB = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidEffMonthStartAfterEffMonthStartDB"; //// //Budget_ProjectPlan_Upd_InvalidEffMonthStartAfterEffMonthStartDB
		public const string Budget_ProjectPlan_Upd_InvalidEffMonthEndBeforeEffMonthEndDB = "ErridnInventory.Budget_ProjectPlan_Upd_InvalidEffMonthEndBeforeEffMonthEndDB"; //// //Budget_ProjectPlan_Upd_InvalidEffMonthEndBeforeEffMonthEndDB
		public const string Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanTblNotFound = "ErridnInventory.Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanTblNotFound"; //// //Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanTblNotFound		
		public const string Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanTblInvalid = "ErridnInventory.Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanTblInvalid"; //// //Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanTblInvalid		
		public const string Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanDtlTblNotFound = "ErridnInventory.Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanDtlTblNotFound"; //// //Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanDtlTblNotFound		
		public const string Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanDtlTblInvalid = "ErridnInventory.Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanDtlTblInvalid"; //// //Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanDtlTblInvalid		
		public const string Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanPrjPaymentTypeTblNotFound = "ErridnInventory.Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanPrjPaymentTypeTblNotFound"; //// //Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanPrjPaymentTypeTblNotFound		
		public const string Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanPrjPaymentTypeTblInvalid = "ErridnInventory.Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanPrjPaymentTypeTblInvalid"; //// //Budget_ProjectPlan_Upd_Input_Budget_ProjectPlanPrjPaymentTypeTblInvalid		
		public const string Budget_ProjectPlan_Upd_Input_InvalidAssetType = "ErridnInventory.Budget_ProjectPlan_Upd_Input_InvalidAssetType"; //// //Budget_ProjectPlan_Upd_Input_InvalidAssetType		
		public const string Budget_ProjectPlan_Upd_Input_InvalidOrganCode = "ErridnInventory.Budget_ProjectPlan_Upd_Input_InvalidOrganCode"; //// //Budget_ProjectPlan_Upd_Input_InvalidOrganCode		
		public const string Budget_ProjectPlan_Upd_Input_InvalidValue = "ErridnInventory.Budget_ProjectPlan_Upd_Input_InvalidValue"; //// //Budget_ProjectPlan_Upd_Input_InvalidValue		

		// Budget_ProjectPlan_InActive:
		public const string Budget_ProjectPlan_InActive = "ErridnInventory.Budget_ProjectPlan_InActive"; //// //Budget_ProjectPlan_InActive
		public const string Budget_ProjectPlan_InActive_InvalidBGStatus = "ErridnInventory.Budget_ProjectPlan_InActive_InvalidBGStatus"; //// //Budget_ProjectPlan_InActive_InvalidBGStatus
		public const string Budget_ProjectPlan_InActive_InvalidValue = "ErridnInventory.Budget_ProjectPlan_InActive_InvalidValue"; //// //Budget_ProjectPlan_InActive_InvalidValue
		public const string Budget_ProjectPlan_InActive_InvalidYear = "ErridnInventory.Budget_ProjectPlan_InActive_InvalidYear"; //// //Budget_ProjectPlan_InActive_InvalidYear
		public const string Budget_ProjectPlan_InActive_InvalidSignedDate = "ErridnInventory.Budget_ProjectPlan_InActive_InvalidSignedDate"; //// //Budget_ProjectPlan_InActive_InvalidSignedDate
		public const string Budget_ProjectPlan_InActive_InvalidSellerCode = "ErridnInventory.Budget_ProjectPlan_InActive_InvalidSellerCode"; //// //Budget_ProjectPlan_InActive_InvalidSellerCode
		public const string Budget_ProjectPlan_InActive_Input_Budget_ProjectPlanTblNotFound = "ErridnInventory.Budget_ProjectPlan_InActive_Input_Budget_ProjectPlanTblNotFound"; //// //Budget_ProjectPlan_InActive_Input_Budget_ProjectPlanTblNotFound		
		public const string Budget_ProjectPlan_InActive_Input_Budget_ProjectPlanTblInvalid = "ErridnInventory.Budget_ProjectPlan_InActive_Input_Budget_ProjectPlanTblInvalid"; //// //Budget_ProjectPlan_InActive_Input_Budget_ProjectPlanTblInvalid		
		public const string Budget_ProjectPlan_InActive_Input_InvalidAssetType = "ErridnInventory.Budget_ProjectPlan_InActive_Input_InvalidAssetType"; //// //Budget_ProjectPlan_InActive_Input_InvalidAssetType		
		public const string Budget_ProjectPlan_InActive_Input_InvalidOrganCode = "ErridnInventory.Budget_ProjectPlan_InActive_Input_InvalidOrganCode"; //// //Budget_ProjectPlan_InActive_Input_InvalidOrganCode		


		// WAS_Budget_ProjectPlan_Add:
		public const string WAS_Budget_ProjectPlan_Add = "ErridnInventory.WAS_Budget_ProjectPlan_Add"; //// //WAS_Budget_ProjectPlan_Add

		// WAS_Budget_ProjectPlan_Upd:
		public const string WAS_Budget_ProjectPlan_Upd = "ErridnInventory.WAS_Budget_ProjectPlan_Upd"; //// //WAS_Budget_ProjectPlan_Upd

		// WAS_Budget_ProjectPlan_InActive:
		public const string WAS_Budget_ProjectPlan_InActive = "ErridnInventory.WAS_Budget_ProjectPlan_InActive"; //// //WAS_Budget_ProjectPlan_InActive

		// WAS_Budget_ProjectPlan_Delete:
		public const string WAS_Budget_ProjectPlan_Delete = "ErridnInventory.WAS_Budget_ProjectPlan_Delete"; //// //WAS_Budget_ProjectPlan_Delete

		// Budget_ProjectPlan_Approve:
		public const string Budget_ProjectPlan_Approve = "ErridnInventory.Budget_ProjectPlan_Approve"; //// //Budget_ProjectPlan_Approve
		public const string Budget_ProjectPlan_Approve_AssetDtlNotFound = "ErridnInventory.Budget_ProjectPlan_Approve_AssetDtlNotFound"; //// //Budget_ProjectPlan_Approve_AssetDtlNotFound

		// Budget_ProjectPlan_UpdAppr:
		public const string Budget_ProjectPlan_UpdAppr = "ErridnInventory.Budget_ProjectPlan_UpdAppr"; //// //Budget_ProjectPlan_UpdAppr
		public const string Budget_ProjectPlan_UpdAppr_InvalidBGStatus = "ErridnInventory.Budget_ProjectPlan_UpdAppr_InvalidBGStatus"; //// //Budget_ProjectPlan_UpdAppr_InvalidBGStatus
		public const string Budget_ProjectPlan_UpdAppr_InvalidValue = "ErridnInventory.Budget_ProjectPlan_UpdAppr_InvalidValue"; //// //Budget_ProjectPlan_UpdAppr_InvalidValue
		public const string Budget_ProjectPlan_UpdAppr_InvalidSignedDate = "ErridnInventory.Budget_ProjectPlan_UpdAppr_InvalidSignedDate"; //// //Budget_ProjectPlan_UpdAppr_InvalidSignedDate
		public const string Budget_ProjectPlan_UpdAppr_InvalidSellerCode = "ErridnInventory.Budget_ProjectPlan_UpdAppr_InvalidSellerCode"; //// //Budget_ProjectPlan_UpdAppr_InvalidSellerCode
		public const string Budget_ProjectPlan_UpdAppr_Input_Budget_ProjectPlanDtlTblNotFound = "ErridnInventory.Budget_ProjectPlan_UpdAppr_Input_Budget_ProjectPlanDtlTblNotFound"; //// //Budget_ProjectPlan_UpdAppr_Input_Budget_ProjectPlanDtlTblNotFound		
		public const string Budget_ProjectPlan_UpdAppr_Input_Budget_ProjectPlanDtlTblInvalid = "ErridnInventory.Budget_ProjectPlan_UpdAppr_Input_Budget_ProjectPlanDtlTblInvalid"; //// //Budget_ProjectPlan_UpdAppr_Input_Budget_ProjectPlanDtlTblInvalid		
		#endregion

		#region // Budget_ProjectPlanDtl:
		// Budget_ProjectPlanDtl_CheckDB:
		public const string Budget_ProjectPlanDtl_CheckDB_ProjectPlanDtlNotFound = "ErridnInventory.Budget_ProjectPlanDtl_CheckDB_ProjectPlanDtlNotFound"; //// //Budget_ProjectPlanDtl_CheckDB_ProjectPlanDtlNotFound
		public const string Budget_ProjectPlanDtl_CheckDB_ProjectPlanDtlExist = "ErridnInventory.Budget_ProjectPlanDtl_CheckDB_ProjectPlanDtlExist"; //// //Budget_ProjectPlanDtl_CheckDB_ProjectPlanDtlExist
		public const string Budget_ProjectPlanDtl_CheckDB_StatusNotMatched = "ErridnInventory.Budget_ProjectPlanDtl_CheckDB_StatusNotMatched"; //// //Budget_ProjectPlanDtl_CheckDB_StatusNotMatched

		// Budget_ProjectPlanDtl_Get:
		public const string Budget_ProjectPlanDtl_Get = "ErridnInventory.Budget_ProjectPlanDtl_Get"; //// //Budget_ProjectPlanDtl_Get

		// WAS_Budget_ProjectPlanDtl_Get:
		public const string WAS_Budget_ProjectPlanDtl_Get = "ErridnInventory.WAS_Budget_ProjectPlanDtl_Get"; //// //WAS_Budget_ProjectPlanDtl_Get

		#endregion

		#region // Budget_ProjectPlanRelease:
		// Budget_ProjectPlanRelease_Get:
		public const string Budget_ProjectPlanRelease_Get = "ErridnInventory.Budget_ProjectPlanRelease_Get"; //// //Budget_ProjectPlanRelease_Get

		// Budget_ProjectPlanRelease_Save:
		public const string Budget_ProjectPlanRelease_Save = "ErridnInventory.Budget_ProjectPlanRelease_Save"; //// //Budget_ProjectPlanRelease_Save
		public const string Budget_ProjectPlanRelease_Save_Input_Budget_ProjectPlanReleaseTblNotFound = "ErridnInventory.Budget_ProjectPlanRelease_Save_Input_Budget_ProjectPlanReleaseTblNotFound"; //// //Budget_ProjectPlanRelease_Save_Input_Budget_ProjectPlanReleaseTblNotFound
		public const string Budget_ProjectPlanRelease_Save_Input_InvalidValue = "ErridnInventory.Budget_ProjectPlanRelease_Save_Input_InvalidValue"; //// //Budget_ProjectPlanRelease_Save_Input_InvalidValue

		// WAS_Budget_ProjectPlanRelease_Get:
		public const string WAS_Budget_ProjectPlanRelease_Get = "ErridnInventory.WAS_Budget_ProjectPlanRelease_Get"; //// //WAS_Budget_ProjectPlanRelease_Get

		// WAS_Budget_ProjectPlanRelease_Save:
		public const string WAS_Budget_ProjectPlanRelease_Save = "ErridnInventory.WAS_Budget_ProjectPlanRelease_Save"; //// //WAS_Budget_ProjectPlanRelease_Save
		#endregion

		#region // Ctr_ContractCar:
		// Ctr_ContractCar_CheckDB:
		public const string Ctr_ContractCar_CheckDB_ContractCarNotFound = "ErridnInventory.Ctr_ContractCar_CheckDB_ContractCarNotFound"; //// //Ctr_ContractCar_CheckDB_ContractCarNotFound
		public const string Ctr_ContractCar_CheckDB_ContractCarExist = "ErridnInventory.Ctr_ContractCar_CheckDB_ContractCarExist"; //// //Ctr_ContractCar_CheckDB_ContractCarExist
		public const string Ctr_ContractCar_CheckDB_StatusNotMatched = "ErridnInventory.Ctr_ContractCar_CheckDB_StatusNotMatched"; //// //Ctr_ContractCar_CheckDB_StatusNotMatched

		// Ctr_ContractCar_Get:
		public const string Ctr_ContractCar_Get = "ErridnInventory.Ctr_ContractCar_Get"; //// //Ctr_ContractCar_Get

		// Ctr_ContractCar_Del:
		public const string Ctr_ContractCar_Del = "ErridnInventory.Ctr_ContractCar_Del"; //// //Ctr_ContractCar_Del

		// WAS_Ctr_ContractCar_Get:
		public const string WAS_Ctr_ContractCar_Get = "ErridnInventory.WAS_Ctr_ContractCar_Get"; //// //WAS_Ctr_ContractCar_Get

		// WAS_Ctr_ContractCar_Save:
		public const string WAS_Ctr_ContractCar_Save = "ErridnInventory.WAS_Ctr_ContractCar_Save"; //// //WAS_Ctr_ContractCar_Save


		// Ctr_ContractCar_Save:
		public const string Ctr_ContractCar_Save = "ErridnInventory.Ctr_ContractCar_Save"; //// //Ctr_ContractCar_Save
		public const string Ctr_ContractCar_Save_InvalidBGStatus = "ErridnInventory.Ctr_ContractCar_Save_InvalidBGStatus"; //// //Ctr_ContractCar_Save_InvalidBGStatus
		public const string Ctr_ContractCar_Save_InvalidValue = "ErridnInventory.Ctr_ContractCar_Save_InvalidValue"; //// //Ctr_ContractCar_Save_InvalidValue
		public const string Ctr_ContractCar_Save_InvalidYear = "ErridnInventory.Ctr_ContractCar_Save_InvalidYear"; //// //Ctr_ContractCar_Save_InvalidYear
		public const string Ctr_ContractCar_Save_InvalidSignedDate = "ErridnInventory.Ctr_ContractCar_Save_InvalidSignedDate"; //// //Ctr_ContractCar_Save_InvalidSignedDate
		public const string Ctr_ContractCar_Save_InvalidSellerCode = "ErridnInventory.Ctr_ContractCar_Save_InvalidSellerCode"; //// //Ctr_ContractCar_Save_InvalidSellerCode
		public const string Ctr_ContractCar_Save_Input_Ctr_ContractCarDtlTblNotFound = "ErridnInventory.Ctr_ContractCar_Save_Input_Ctr_ContractCarDtlTblNotFound"; //// //Ctr_ContractCar_Save_Input_Ctr_ContractCarDtlTblNotFound		
		public const string Ctr_ContractCar_Save_Input_Ctr_ContractCarDtlTblInvalid = "ErridnInventory.Ctr_ContractCar_Save_Input_Ctr_ContractCarDtlTblInvalid"; //// //Ctr_ContractCar_Save_Input_Ctr_ContractCarDtlTblInvalid		
		public const string Ctr_ContractCar_Save_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractCar_Save_Input_InvalidOrganCode"; //// //Ctr_ContractCar_Save_Input_InvalidOrganCode		

		// Ctr_ContractCar_Add:
		public const string Ctr_ContractCar_Add = "ErridnInventory.Ctr_ContractCar_Add"; //// //Ctr_ContractCar_Add
		public const string Ctr_ContractCar_Add_InvalidBGStatus = "ErridnInventory.Ctr_ContractCar_Add_InvalidBGStatus"; //// //Ctr_ContractCar_Add_InvalidBGStatus
		public const string Ctr_ContractCar_Add_InvalidValue = "ErridnInventory.Ctr_ContractCar_Add_InvalidValue"; //// //Ctr_ContractCar_Add_InvalidValue
		public const string Ctr_ContractCar_Add_InvalidFullValue = "ErridnInventory.Ctr_ContractCar_Add_InvalidFullValue"; //// //Ctr_ContractCar_Add_InvalidFullValue
		public const string Ctr_ContractCar_Add_InvalidYear = "ErridnInventory.Ctr_ContractCar_Add_InvalidYear"; //// //Ctr_ContractCar_Add_InvalidYear
		public const string Ctr_ContractCar_Add_InvalidSignedDate = "ErridnInventory.Ctr_ContractCar_Add_InvalidSignedDate"; //// //Ctr_ContractCar_Add_InvalidSignedDate
		public const string Ctr_ContractCar_Add_InvalidSellerCode = "ErridnInventory.Ctr_ContractCar_Add_InvalidSellerCode"; //// //Ctr_ContractCar_Add_InvalidSellerCode
		public const string Ctr_ContractCar_Add_Input_Ctr_ContractCarTblNotFound = "ErridnInventory.Ctr_ContractCar_Add_Input_Ctr_ContractCarTblNotFound"; //// //Ctr_ContractCar_Add_Input_Ctr_ContractCarTblNotFound		
		public const string Ctr_ContractCar_Add_Input_Ctr_ContractCarTblInvalid = "ErridnInventory.Ctr_ContractCar_Add_Input_Ctr_ContractCarTblInvalid"; //// //Ctr_ContractCar_Add_Input_Ctr_ContractCarTblInvalid		
		public const string Ctr_ContractCar_Add_Input_Ctr_ContractCarDtlTblNotFound = "ErridnInventory.Ctr_ContractCar_Add_Input_Ctr_ContractCarDtlTblNotFound"; //// //Ctr_ContractCar_Add_Input_Ctr_ContractCarDtlTblNotFound		
		public const string Ctr_ContractCar_Add_Input_Ctr_ContractCarDtlTblInvalid = "ErridnInventory.Ctr_ContractCar_Add_Input_Ctr_ContractCarDtlTblInvalid"; //// //Ctr_ContractCar_Add_Input_Ctr_ContractCarDtlTblInvalid		
		public const string Ctr_ContractCar_Add_Input_InvalidAssetType = "ErridnInventory.Ctr_ContractCar_Add_Input_InvalidAssetType"; //// //Ctr_ContractCar_Add_Input_InvalidAssetType		
		public const string Ctr_ContractCar_Add_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractCar_Add_Input_InvalidOrganCode"; //// //Ctr_ContractCar_Add_Input_InvalidOrganCode		

		// Ctr_ContractCar_Upd:
		public const string Ctr_ContractCar_Upd = "ErridnInventory.Ctr_ContractCar_Upd"; //// //Ctr_ContractCar_Upd
		public const string Ctr_ContractCar_Upd_InvalidBGStatus = "ErridnInventory.Ctr_ContractCar_Upd_InvalidBGStatus"; //// //Ctr_ContractCar_Upd_InvalidBGStatus
		public const string Ctr_ContractCar_Upd_InvalidValue = "ErridnInventory.Ctr_ContractCar_Upd_InvalidValue"; //// //Ctr_ContractCar_Upd_InvalidValue
		public const string Ctr_ContractCar_Upd_InvalidYear = "ErridnInventory.Ctr_ContractCar_Upd_InvalidYear"; //// //Ctr_ContractCar_Upd_InvalidYear
		public const string Ctr_ContractCar_Upd_InvalidSignedDate = "ErridnInventory.Ctr_ContractCar_Upd_InvalidSignedDate"; //// //Ctr_ContractCar_Upd_InvalidSignedDate
		public const string Ctr_ContractCar_Upd_InvalidSellerCode = "ErridnInventory.Ctr_ContractCar_Upd_InvalidSellerCode"; //// //Ctr_ContractCar_Upd_InvalidSellerCode
		public const string Ctr_ContractCar_Upd_Input_Ctr_ContractCarTblNotFound = "ErridnInventory.Ctr_ContractCar_Upd_Input_Ctr_ContractCarTblNotFound"; //// //Ctr_ContractCar_Upd_Input_Ctr_ContractCarTblNotFound		
		public const string Ctr_ContractCar_Upd_Input_Ctr_ContractCarTblInvalid = "ErridnInventory.Ctr_ContractCar_Upd_Input_Ctr_ContractCarTblInvalid"; //// //Ctr_ContractCar_Upd_Input_Ctr_ContractCarTblInvalid		
		public const string Ctr_ContractCar_Upd_Input_InvalidAssetType = "ErridnInventory.Ctr_ContractCar_Upd_Input_InvalidAssetType"; //// //Ctr_ContractCar_Upd_Input_InvalidAssetType		
		public const string Ctr_ContractCar_Upd_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractCar_Upd_Input_InvalidOrganCode"; //// //Ctr_ContractCar_Upd_Input_InvalidOrganCode		

		// Ctr_ContractCar_InActive:
		public const string Ctr_ContractCar_InActive = "ErridnInventory.Ctr_ContractCar_InActive"; //// //Ctr_ContractCar_InActive
		public const string Ctr_ContractCar_InActive_InvalidBGStatus = "ErridnInventory.Ctr_ContractCar_InActive_InvalidBGStatus"; //// //Ctr_ContractCar_InActive_InvalidBGStatus
		public const string Ctr_ContractCar_InActive_InvalidValue = "ErridnInventory.Ctr_ContractCar_InActive_InvalidValue"; //// //Ctr_ContractCar_InActive_InvalidValue
		public const string Ctr_ContractCar_InActive_InvalidYear = "ErridnInventory.Ctr_ContractCar_InActive_InvalidYear"; //// //Ctr_ContractCar_InActive_InvalidYear
		public const string Ctr_ContractCar_InActive_InvalidSignedDate = "ErridnInventory.Ctr_ContractCar_InActive_InvalidSignedDate"; //// //Ctr_ContractCar_InActive_InvalidSignedDate
		public const string Ctr_ContractCar_InActive_InvalidSellerCode = "ErridnInventory.Ctr_ContractCar_InActive_InvalidSellerCode"; //// //Ctr_ContractCar_InActive_InvalidSellerCode
		public const string Ctr_ContractCar_InActive_Input_Ctr_ContractCarTblNotFound = "ErridnInventory.Ctr_ContractCar_InActive_Input_Ctr_ContractCarTblNotFound"; //// //Ctr_ContractCar_InActive_Input_Ctr_ContractCarTblNotFound		
		public const string Ctr_ContractCar_InActive_Input_Ctr_ContractCarTblInvalid = "ErridnInventory.Ctr_ContractCar_InActive_Input_Ctr_ContractCarTblInvalid"; //// //Ctr_ContractCar_InActive_Input_Ctr_ContractCarTblInvalid		
		public const string Ctr_ContractCar_InActive_Input_InvalidAssetType = "ErridnInventory.Ctr_ContractCar_InActive_Input_InvalidAssetType"; //// //Ctr_ContractCar_InActive_Input_InvalidAssetType		
		public const string Ctr_ContractCar_InActive_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractCar_InActive_Input_InvalidOrganCode"; //// //Ctr_ContractCar_InActive_Input_InvalidOrganCode		


		// WAS_Ctr_ContractCar_Add:
		public const string WAS_Ctr_ContractCar_Add = "ErridnInventory.WAS_Ctr_ContractCar_Add"; //// //WAS_Ctr_ContractCar_Add

		// WAS_Ctr_ContractCar_Upd:
		public const string WAS_Ctr_ContractCar_Upd = "ErridnInventory.WAS_Ctr_ContractCar_Upd"; //// //WAS_Ctr_ContractCar_Upd

		// WAS_Ctr_ContractCar_InActive:
		public const string WAS_Ctr_ContractCar_InActive = "ErridnInventory.WAS_Ctr_ContractCar_InActive"; //// //WAS_Ctr_ContractCar_InActive

		// WAS_Ctr_ContractCar_Delete:
		public const string WAS_Ctr_ContractCar_Delete = "ErridnInventory.WAS_Ctr_ContractCar_Delete"; //// //WAS_Ctr_ContractCar_Delete

		// Ctr_ContractCar_Approve:
		public const string Ctr_ContractCar_Approve = "ErridnInventory.Ctr_ContractCar_Approve"; //// //Ctr_ContractCar_Approve
		public const string Ctr_ContractCar_Approve_AssetDtlNotFound = "ErridnInventory.Ctr_ContractCar_Approve_AssetDtlNotFound"; //// //Ctr_ContractCar_Approve_AssetDtlNotFound

		// Ctr_ContractCar_UpdAppr:
		public const string Ctr_ContractCar_UpdAppr = "ErridnInventory.Ctr_ContractCar_UpdAppr"; //// //Ctr_ContractCar_UpdAppr
		public const string Ctr_ContractCar_UpdAppr_InvalidBGStatus = "ErridnInventory.Ctr_ContractCar_UpdAppr_InvalidBGStatus"; //// //Ctr_ContractCar_UpdAppr_InvalidBGStatus
		public const string Ctr_ContractCar_UpdAppr_InvalidValue = "ErridnInventory.Ctr_ContractCar_UpdAppr_InvalidValue"; //// //Ctr_ContractCar_UpdAppr_InvalidValue
		public const string Ctr_ContractCar_UpdAppr_InvalidSignedDate = "ErridnInventory.Ctr_ContractCar_UpdAppr_InvalidSignedDate"; //// //Ctr_ContractCar_UpdAppr_InvalidSignedDate
		public const string Ctr_ContractCar_UpdAppr_InvalidSellerCode = "ErridnInventory.Ctr_ContractCar_UpdAppr_InvalidSellerCode"; //// //Ctr_ContractCar_UpdAppr_InvalidSellerCode
		public const string Ctr_ContractCar_UpdAppr_Input_Ctr_ContractCarDtlTblNotFound = "ErridnInventory.Ctr_ContractCar_UpdAppr_Input_Ctr_ContractCarDtlTblNotFound"; //// //Ctr_ContractCar_UpdAppr_Input_Ctr_ContractCarDtlTblNotFound		
		public const string Ctr_ContractCar_UpdAppr_Input_Ctr_ContractCarDtlTblInvalid = "ErridnInventory.Ctr_ContractCar_UpdAppr_Input_Ctr_ContractCarDtlTblInvalid"; //// //Ctr_ContractCar_UpdAppr_Input_Ctr_ContractCarDtlTblInvalid		
		#endregion

		#region // Ctr_ContractDeposit:
		// Ctr_ContractDeposit_CheckDB:
		public const string Ctr_ContractDeposit_CheckDB_ContractDepositNotFound = "ErridnInventory.Ctr_ContractDeposit_CheckDB_ContractDepositNotFound"; //// //Ctr_ContractDeposit_CheckDB_ContractDepositNotFound
		public const string Ctr_ContractDeposit_CheckDB_ContractDepositExist = "ErridnInventory.Ctr_ContractDeposit_CheckDB_ContractDepositExist"; //// //Ctr_ContractDeposit_CheckDB_ContractDepositExist
		public const string Ctr_ContractDeposit_CheckDB_StatusNotMatched = "ErridnInventory.Ctr_ContractDeposit_CheckDB_StatusNotMatched"; //// //Ctr_ContractDeposit_CheckDB_StatusNotMatched

		// Ctr_ContractDeposit_Get:
		public const string Ctr_ContractDeposit_Get = "ErridnInventory.Ctr_ContractDeposit_Get"; //// //Ctr_ContractDeposit_Get

		// WAS_Seq_Common_Get_CtrDpsCodeForTaiTuc:
		public const string WAS_Seq_Common_Get_CtrDpsCodeForTaiTuc = "ErridnInventory.WAS_Seq_Common_Get_CtrDpsCodeForTaiTuc"; //// //WAS_Seq_Common_Get_CtrDpsCodeForTaiTuc

		// WAS_Ctr_ContractDeposit_Get:
		public const string WAS_Ctr_ContractDeposit_Get = "ErridnInventory.WAS_Ctr_ContractDeposit_Get"; //// //WAS_Ctr_ContractDeposit_Get

		// WAS_Ctr_ContractDeposit_Save:
		public const string WAS_Ctr_ContractDeposit_Save = "ErridnInventory.WAS_Ctr_ContractDeposit_Save"; //// //WAS_Ctr_ContractDeposit_Save

		// WAS_Ctr_ContractDeposit_Add:
		public const string WAS_Ctr_ContractDeposit_Add = "ErridnInventory.WAS_Ctr_ContractDeposit_Add"; //// //WAS_Ctr_ContractDeposit_Add

		// WAS_Ctr_ContractDeposit_TaiTuc:
		public const string WAS_Ctr_ContractDeposit_TaiTuc = "ErridnInventory.WAS_Ctr_ContractDeposit_TaiTuc"; //// //WAS_Ctr_ContractDeposit_TaiTuc

		// WAS_Ctr_ContractDeposit_Upd:
		public const string WAS_Ctr_ContractDeposit_Upd = "ErridnInventory.WAS_Ctr_ContractDeposit_Upd"; //// //WAS_Ctr_ContractDeposit_Upd

		// WAS_Ctr_ContractDeposit_Del:
		public const string WAS_Ctr_ContractDeposit_Del = "ErridnInventory.WAS_Ctr_ContractDeposit_Del"; //// //WAS_Ctr_ContractDeposit_Del

		// WAS_Ctr_ContractDeposit_TatToan:
		public const string WAS_Ctr_ContractDeposit_TatToan = "ErridnInventory.WAS_Ctr_ContractDeposit_TatToan"; //// //WAS_Ctr_ContractDeposit_TatToan

		// WAS_Ctr_ContractDeposit_Release:
		public const string WAS_Ctr_ContractDeposit_Release = "ErridnInventory.WAS_Ctr_ContractDeposit_Release"; //// //WAS_Ctr_ContractDeposit_Release

		// WAS_Ctr_ContractDeposit_Mortgage:
		public const string WAS_Ctr_ContractDeposit_Mortgage = "ErridnInventory.WAS_Ctr_ContractDeposit_Mortgage"; //// //WAS_Ctr_ContractDeposit_Mortgage

		// WAS_Ctr_ContractDeposit_Refinance:
		public const string WAS_Ctr_ContractDeposit_Refinance = "ErridnInventory.WAS_Ctr_ContractDeposit_Refinance"; //// //WAS_Ctr_ContractDeposit_Refinance


		// Ctr_ContractDeposit_Add:
		public const string Ctr_ContractDeposit_Add = "ErridnInventory.Ctr_ContractDeposit_Add"; //// //Ctr_ContractDeposit_Add
		public const string Ctr_ContractDeposit_Add_InvalidBGStatus = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidBGStatus"; //// //Ctr_ContractDeposit_Add_InvalidBGStatus
		public const string Ctr_ContractDeposit_Add_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidValue"; //// //Ctr_ContractDeposit_Add_InvalidValue
		public const string Ctr_ContractDeposit_Add_InvalidYear = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidYear"; //// //Ctr_ContractDeposit_Add_InvalidYear
		public const string Ctr_ContractDeposit_Add_InvalidEffDateStart = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidEffDateStart"; //// //Ctr_ContractDeposit_Add_InvalidEffDateStart
		public const string Ctr_ContractDeposit_Add_InvalidEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidEffDateEnd"; //// //Ctr_ContractDeposit_Add_InvalidEffDateEnd
		public const string Ctr_ContractDeposit_Add_InvalidKUNNSignDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidKUNNSignDate"; //// //Ctr_ContractDeposit_Add_InvalidKUNNSignDate
		public const string Ctr_ContractDeposit_Add_InvalidKUNNSignDateBeforeSysDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidKUNNSignDateBeforeSysDate"; //// //Ctr_ContractDeposit_Add_InvalidKUNNSignDateBeforeSysDate
		public const string Ctr_ContractDeposit_Add_InvalidHieuLucDateAfterSysDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidHieuLucDateAfterSysDate"; //// //Ctr_ContractDeposit_Add_InvalidHieuLucDateAfterSysDate
		public const string Ctr_ContractDeposit_Add_InvalidHieuLucDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidHieuLucDate"; //// //Ctr_ContractDeposit_Add_InvalidHieuLucDate
		public const string Ctr_ContractDeposit_Add_InvalidDaoHanDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidDaoHanDate"; //// //Ctr_ContractDeposit_Add_InvalidDaoHanDate
		public const string Ctr_ContractDeposit_Add_InvalidHieuLucDateAfterSignDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidHieuLucDateAfterSignDate"; //// //Ctr_ContractDeposit_Add_InvalidHieuLucDateAfterSignDate
		public const string Ctr_ContractDeposit_Add_InvalidDaoHanDateAfterSysDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidDaoHanDateAfterSysDate"; //// //Ctr_ContractDeposit_Add_InvalidDaoHanDateAfterSysDate
		public const string Ctr_ContractDeposit_Add_InvalidHieuLucDateBeforeDaoHanDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidHieuLucDateBeforeDaoHanDate"; //// //Ctr_ContractDeposit_Add_InvalidHieuLucDateBeforeDaoHanDate
		public const string Ctr_ContractDeposit_Add_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidEffDateStartBeforeEffDateEnd"; //// //Ctr_ContractDeposit_Add_InvalidEffDateStartBeforeEffDateEnd
		public const string Ctr_ContractDeposit_Add_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidEffDateEndAfterEffSysDate"; //// //Ctr_ContractDeposit_Add_InvalidEffDateEndAfterEffSysDate
		public const string Ctr_ContractDeposit_Add_InvalidSignedDate = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidSignedDate"; //// //Ctr_ContractDeposit_Add_InvalidSignedDate
		public const string Ctr_ContractDeposit_Add_InvalidSellerCode = "ErridnInventory.Ctr_ContractDeposit_Add_InvalidSellerCode"; //// //Ctr_ContractDeposit_Add_InvalidSellerCode
		public const string Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_Add_Input_CrCt_OrganTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Add_Input_CrCt_OrganTblNotFound"; //// //Ctr_ContractDeposit_Add_Input_CrCt_OrganTblNotFound		
		public const string Ctr_ContractDeposit_Add_Input_CrCt_OrganTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Add_Input_CrCt_OrganTblInvalid"; //// //Ctr_ContractDeposit_Add_Input_CrCt_OrganTblInvalid		
		public const string Ctr_ContractDeposit_Add_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Add_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //Ctr_ContractDeposit_Add_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string Ctr_ContractDeposit_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //Ctr_ContractDeposit_Add_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string Ctr_ContractDeposit_Add_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Add_Input_CrCt_LoanPurposeTblNotFound"; //// //Ctr_ContractDeposit_Add_Input_CrCt_LoanPurposeTblNotFound		
		public const string Ctr_ContractDeposit_Add_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Add_Input_CrCt_LoanPurposeTblInvalid"; //// //Ctr_ContractDeposit_Add_Input_CrCt_LoanPurposeTblInvalid		
		public const string Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositFileUploadTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositFileUploadTblNotFound"; //// //Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositFileUploadTblNotFound		
		public const string Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositFileUploadTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositFileUploadTblInvalid"; //// //Ctr_ContractDeposit_Add_Input_Ctr_ContractDepositFileUploadTblInvalid
		public const string Ctr_ContractDeposit_Add_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_Add_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_Add_Input_InvalidOrganCode		

		// Ctr_ContractDeposit_Upd:
		public const string Ctr_ContractDeposit_Upd = "ErridnInventory.Ctr_ContractDeposit_Upd"; //// //Ctr_ContractDeposit_Upd
		public const string Ctr_ContractDeposit_Upd_InvalidBGStatus = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidBGStatus"; //// //Ctr_ContractDeposit_Upd_InvalidBGStatus
		public const string Ctr_ContractDeposit_Upd_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidValue"; //// //Ctr_ContractDeposit_Upd_InvalidValue
		public const string Ctr_ContractDeposit_Upd_InvalidTatToanDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidTatToanDate"; //// //Ctr_ContractDeposit_Upd_InvalidTatToanDate
		public const string Ctr_ContractDeposit_Upd_InvalidYear = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidYear"; //// //Ctr_ContractDeposit_Upd_InvalidYear
		public const string Ctr_ContractDeposit_Upd_InvalidEffDateStart = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidEffDateStart"; //// //Ctr_ContractDeposit_Upd_InvalidEffDateStart
		public const string Ctr_ContractDeposit_Upd_InvalidEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidEffDateEnd"; //// //Ctr_ContractDeposit_Upd_InvalidEffDateEnd
		public const string Ctr_ContractDeposit_Upd_InvalidKUNNSignDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidKUNNSignDate"; //// //Ctr_ContractDeposit_Upd_InvalidKUNNSignDate
		public const string Ctr_ContractDeposit_Upd_InvalidKUNNSignDateBeforeSysDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidKUNNSignDateBeforeSysDate"; //// //Ctr_ContractDeposit_Upd_InvalidKUNNSignDateBeforeSysDate
		public const string Ctr_ContractDeposit_Upd_InvalidHieuLucDateAfterSysDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidHieuLucDateAfterSysDate"; //// //Ctr_ContractDeposit_Upd_InvalidHieuLucDateAfterSysDate
		public const string Ctr_ContractDeposit_Upd_InvalidHieuLucDateAfterSignDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidHieuLucDateAfterSignDate"; //// //Ctr_ContractDeposit_Upd_InvalidHieuLucDateAfterSignDate
		public const string Ctr_ContractDeposit_Upd_InvalidDaoHanDateAfterSysDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidDaoHanDateAfterSysDate"; //// //Ctr_ContractDeposit_Upd_InvalidDaoHanDateAfterSysDate
		public const string Ctr_ContractDeposit_Upd_InvalidHieuLucDateBeforeDaoHanDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidHieuLucDateBeforeDaoHanDate"; //// //Ctr_ContractDeposit_Upd_InvalidHieuLucDateBeforeDaoHanDate
		public const string Ctr_ContractDeposit_Upd_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidEffDateStartBeforeEffDateEnd"; //// //Ctr_ContractDeposit_Upd_InvalidEffDateStartBeforeEffDateEnd
		public const string Ctr_ContractDeposit_Upd_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidEffDateEndAfterEffSysDate"; //// //Ctr_ContractDeposit_Upd_InvalidEffDateEndAfterEffSysDate
		public const string Ctr_ContractDeposit_Upd_InvalidSignedDate = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidSignedDate"; //// //Ctr_ContractDeposit_Upd_InvalidSignedDate
		public const string Ctr_ContractDeposit_Upd_InvalidSellerCode = "ErridnInventory.Ctr_ContractDeposit_Upd_InvalidSellerCode"; //// //Ctr_ContractDeposit_Upd_InvalidSellerCode
		public const string Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_Upd_Input_CrCt_OrganTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_CrCt_OrganTblNotFound"; //// //Ctr_ContractDeposit_Upd_Input_CrCt_OrganTblNotFound		
		public const string Ctr_ContractDeposit_Upd_Input_CrCt_OrganTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_CrCt_OrganTblInvalid"; //// //Ctr_ContractDeposit_Upd_Input_CrCt_OrganTblInvalid		
		public const string Ctr_ContractDeposit_Upd_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //Ctr_ContractDeposit_Upd_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string Ctr_ContractDeposit_Upd_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //Ctr_ContractDeposit_Upd_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string Ctr_ContractDeposit_Upd_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_CrCt_LoanPurposeTblNotFound"; //// //Ctr_ContractDeposit_Upd_Input_CrCt_LoanPurposeTblNotFound		
		public const string Ctr_ContractDeposit_Upd_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_CrCt_LoanPurposeTblInvalid"; //// //Ctr_ContractDeposit_Upd_Input_CrCt_LoanPurposeTblInvalid		
		public const string Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositFileUploadTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositFileUploadTblNotFound"; //// //Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositFileUploadTblNotFound		
		public const string Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositFileUploadTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositFileUploadTblInvalid"; //// //Ctr_ContractDeposit_Upd_Input_Ctr_ContractDepositFileUploadTblInvalid
		public const string Ctr_ContractDeposit_Upd_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_Upd_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_Upd_Input_InvalidOrganCode		

		// Ctr_ContractDeposit_TaiTuc:
		public const string Ctr_ContractDeposit_TaiTuc = "ErridnInventory.Ctr_ContractDeposit_TaiTuc"; //// //Ctr_ContractDeposit_TaiTuc
		public const string Ctr_ContractDeposit_TaiTuc_InvalidBGStatus = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidBGStatus"; //// //Ctr_ContractDeposit_TaiTuc_InvalidBGStatus
		public const string Ctr_ContractDeposit_TaiTuc_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidValue"; //// //Ctr_ContractDeposit_TaiTuc_InvalidValue
		public const string Ctr_ContractDeposit_TaiTuc_InvalidYear = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidYear"; //// //Ctr_ContractDeposit_TaiTuc_InvalidYear
		public const string Ctr_ContractDeposit_TaiTuc_InvalidEffDateStart = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidEffDateStart"; //// //Ctr_ContractDeposit_TaiTuc_InvalidEffDateStart
		public const string Ctr_ContractDeposit_TaiTuc_InvalidEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidEffDateEnd"; //// //Ctr_ContractDeposit_TaiTuc_InvalidEffDateEnd
		public const string Ctr_ContractDeposit_TaiTuc_InvalidKUNNSignDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidKUNNSignDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidKUNNSignDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidKUNNSignDateBeforeSysDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidKUNNSignDateBeforeSysDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidKUNNSignDateBeforeSysDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateAfterSysDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateAfterSysDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateAfterSysDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidDaoHanDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidDaoHanDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidDaoHanDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateAfterSignDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateAfterSignDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateAfterSignDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidDaoHanDateAfterSysDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidDaoHanDateAfterSysDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidDaoHanDateAfterSysDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateBeforeDaoHanDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateBeforeDaoHanDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidHieuLucDateBeforeDaoHanDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidEffDateStartBeforeEffDateEnd"; //// //Ctr_ContractDeposit_TaiTuc_InvalidEffDateStartBeforeEffDateEnd
		public const string Ctr_ContractDeposit_TaiTuc_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidEffDateEndAfterEffSysDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidEffDateEndAfterEffSysDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidSignedDate = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidSignedDate"; //// //Ctr_ContractDeposit_TaiTuc_InvalidSignedDate
		public const string Ctr_ContractDeposit_TaiTuc_InvalidSellerCode = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_InvalidSellerCode"; //// //Ctr_ContractDeposit_TaiTuc_InvalidSellerCode
		public const string Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_TaiTuc_Input_CrCt_OrganTblNotFound = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_CrCt_OrganTblNotFound"; //// //Ctr_ContractDeposit_TaiTuc_Input_CrCt_OrganTblNotFound		
		public const string Ctr_ContractDeposit_TaiTuc_Input_CrCt_OrganTblInvalid = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_CrCt_OrganTblInvalid"; //// //Ctr_ContractDeposit_TaiTuc_Input_CrCt_OrganTblInvalid		
		public const string Ctr_ContractDeposit_TaiTuc_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //Ctr_ContractDeposit_TaiTuc_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string Ctr_ContractDeposit_TaiTuc_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //Ctr_ContractDeposit_TaiTuc_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string Ctr_ContractDeposit_TaiTuc_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_CrCt_LoanPurposeTblNotFound"; //// //Ctr_ContractDeposit_TaiTuc_Input_CrCt_LoanPurposeTblNotFound		
		public const string Ctr_ContractDeposit_TaiTuc_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_CrCt_LoanPurposeTblInvalid"; //// //Ctr_ContractDeposit_TaiTuc_Input_CrCt_LoanPurposeTblInvalid		
		public const string Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositFileUploadTblNotFound = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositFileUploadTblNotFound"; //// //Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositFileUploadTblNotFound		
		public const string Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositFileUploadTblInvalid = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositFileUploadTblInvalid"; //// //Ctr_ContractDeposit_TaiTuc_Input_Ctr_ContractDepositFileUploadTblInvalid
		public const string Ctr_ContractDeposit_TaiTuc_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_TaiTuc_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_TaiTuc_Input_InvalidOrganCode		


		// Ctr_ContractDeposit_Release:
		public const string Ctr_ContractDeposit_Release = "ErridnInventory.Ctr_ContractDeposit_Release"; //// //Ctr_ContractDeposit_Release
		public const string Ctr_ContractDeposit_Release_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Release_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_Release_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_Release_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Release_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_Release_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_Release_Input_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_Release_Input_InvalidValue"; //// //Ctr_ContractDeposit_Release_Input_InvalidValue
		public const string Ctr_ContractDeposit_Release_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_Release_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_Release_Input_InvalidOrganCode
		public const string Ctr_ContractDeposit_Release_Input_InvalidPaymentPartnerCode = "ErridnInventory.Ctr_ContractDeposit_Release_Input_InvalidPaymentPartnerCode"; //// //Ctr_ContractDeposit_Release_Input_InvalidPaymentPartnerCode

		// Ctr_ContractDeposit_Mortgage:
		public const string Ctr_ContractDeposit_Mortgage = "ErridnInventory.Ctr_ContractDeposit_Mortgage"; //// //Ctr_ContractDeposit_Mortgage
		public const string Ctr_ContractDeposit_Mortgage_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Mortgage_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_Mortgage_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_Mortgage_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Mortgage_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_Mortgage_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_Mortgage_Input_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_Mortgage_Input_InvalidValue"; //// //Ctr_ContractDeposit_Mortgage_Input_InvalidValue
		public const string Ctr_ContractDeposit_Mortgage_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_Mortgage_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_Mortgage_Input_InvalidOrganCode
		public const string Ctr_ContractDeposit_Mortgage_Input_InvalidPaymentPartnerCode = "ErridnInventory.Ctr_ContractDeposit_Mortgage_Input_InvalidPaymentPartnerCode"; //// //Ctr_ContractDeposit_Mortgage_Input_InvalidPaymentPartnerCode


		// Ctr_ContractDeposit_Del:
		public const string Ctr_ContractDeposit_Del = "ErridnInventory.Ctr_ContractDeposit_Del"; //// //Ctr_ContractDeposit_Del
		public const string Ctr_ContractDeposit_Del_InvalidTatToanDate = "ErridnInventory.Ctr_ContractDeposit_Del_InvalidTatToanDate"; //// //Ctr_ContractDeposit_Del_InvalidTatToanDate

		// Ctr_ContractDeposit_TatToan:
		public const string Ctr_ContractDeposit_TatToan = "ErridnInventory.Ctr_ContractDeposit_TatToan"; //// //Ctr_ContractDeposit_TatToan
		public const string Ctr_ContractDeposit_TatToan_InvalidTatToanDate = "ErridnInventory.Ctr_ContractDeposit_TatToan_InvalidTatToanDate"; //// //Ctr_ContractDeposit_TatToan_InvalidTatToanDate
		public const string Ctr_ContractDeposit_TatToan_InvalidTatToanDateAfterHieuLucDate = "ErridnInventory.Ctr_ContractDeposit_TatToan_InvalidTatToanDateAfterHieuLucDate"; //// //Ctr_ContractDeposit_TatToan_InvalidTatToanDateAfterHieuLucDate
		public const string Ctr_ContractDeposit_TatToan_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_TatToan_InvalidValue"; //// //Ctr_ContractDeposit_TatToan_InvalidValue

		// Ctr_ContractDeposit_Refinance:
		public const string Ctr_ContractDeposit_Refinance = "ErridnInventory.Ctr_ContractDeposit_Refinance"; //// //Ctr_ContractDeposit_Refinance
		public const string Ctr_ContractDeposit_Refinance_InvalidBGStatus = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidBGStatus"; //// //Ctr_ContractDeposit_Refinance_InvalidBGStatus
		public const string Ctr_ContractDeposit_Refinance_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidValue"; //// //Ctr_ContractDeposit_Refinance_InvalidValue
		public const string Ctr_ContractDeposit_Refinance_InvalidYear = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidYear"; //// //Ctr_ContractDeposit_Refinance_InvalidYear
		public const string Ctr_ContractDeposit_Refinance_InvalidEffDateStart = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidEffDateStart"; //// //Ctr_ContractDeposit_Refinance_InvalidEffDateStart
		public const string Ctr_ContractDeposit_Refinance_InvalidEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidEffDateEnd"; //// //Ctr_ContractDeposit_Refinance_InvalidEffDateEnd
		public const string Ctr_ContractDeposit_Refinance_InvalidEffDateStartBeforeEffDateEnd = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidEffDateStartBeforeEffDateEnd"; //// //Ctr_ContractDeposit_Refinance_InvalidEffDateStartBeforeEffDateEnd
		public const string Ctr_ContractDeposit_Refinance_InvalidEffDateEndAfterEffSysDate = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidEffDateEndAfterEffSysDate"; //// //Ctr_ContractDeposit_Refinance_InvalidEffDateEndAfterEffSysDate
		public const string Ctr_ContractDeposit_Refinance_InvalidSignedDate = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidSignedDate"; //// //Ctr_ContractDeposit_Refinance_InvalidSignedDate
		public const string Ctr_ContractDeposit_Refinance_InvalidSellerCode = "ErridnInventory.Ctr_ContractDeposit_Refinance_InvalidSellerCode"; //// //Ctr_ContractDeposit_Refinance_InvalidSellerCode
		public const string Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_Refinance_Input_CrCt_OrganTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_CrCt_OrganTblNotFound"; //// //Ctr_ContractDeposit_Refinance_Input_CrCt_OrganTblNotFound		
		public const string Ctr_ContractDeposit_Refinance_Input_CrCt_OrganTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_CrCt_OrganTblInvalid"; //// //Ctr_ContractDeposit_Refinance_Input_CrCt_OrganTblInvalid		
		public const string Ctr_ContractDeposit_Refinance_Input_KUNN_ValLaiSuatChangeHistTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_KUNN_ValLaiSuatChangeHistTblNotFound"; //// //Ctr_ContractDeposit_Refinance_Input_KUNN_ValLaiSuatChangeHistTblNotFound		
		public const string Ctr_ContractDeposit_Refinance_Input_KUNN_ValLaiSuatChangeHistTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_KUNN_ValLaiSuatChangeHistTblInvalid"; //// //Ctr_ContractDeposit_Refinance_Input_KUNN_ValLaiSuatChangeHistTblInvalid		
		public const string Ctr_ContractDeposit_Refinance_Input_CrCt_LoanPurposeTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_CrCt_LoanPurposeTblNotFound"; //// //Ctr_ContractDeposit_Refinance_Input_CrCt_LoanPurposeTblNotFound		
		public const string Ctr_ContractDeposit_Refinance_Input_CrCt_LoanPurposeTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_CrCt_LoanPurposeTblInvalid"; //// //Ctr_ContractDeposit_Refinance_Input_CrCt_LoanPurposeTblInvalid		
		public const string Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositFileUploadTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositFileUploadTblNotFound"; //// //Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositFileUploadTblNotFound		
		public const string Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositFileUploadTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositFileUploadTblInvalid"; //// //Ctr_ContractDeposit_Refinance_Input_Ctr_ContractDepositFileUploadTblInvalid
		public const string Ctr_ContractDeposit_Refinance_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_Refinance_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_Refinance_Input_InvalidOrganCode		


		// Ctr_ContractDeposit_Save:
		public const string Ctr_ContractDeposit_Save = "ErridnInventory.Ctr_ContractDeposit_Save"; //// //Ctr_ContractDeposit_Save
		public const string Ctr_ContractDeposit_Save_InvalidBGStatus = "ErridnInventory.Ctr_ContractDeposit_Save_InvalidBGStatus"; //// //Ctr_ContractDeposit_Save_InvalidBGStatus
		public const string Ctr_ContractDeposit_Save_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_Save_InvalidValue"; //// //Ctr_ContractDeposit_Save_InvalidValue
		public const string Ctr_ContractDeposit_Save_InvalidYear = "ErridnInventory.Ctr_ContractDeposit_Save_InvalidYear"; //// //Ctr_ContractDeposit_Save_InvalidYear
		public const string Ctr_ContractDeposit_Save_InvalidSignedDate = "ErridnInventory.Ctr_ContractDeposit_Save_InvalidSignedDate"; //// //Ctr_ContractDeposit_Save_InvalidSignedDate
		public const string Ctr_ContractDeposit_Save_InvalidSellerCode = "ErridnInventory.Ctr_ContractDeposit_Save_InvalidSellerCode"; //// //Ctr_ContractDeposit_Save_InvalidSellerCode
		public const string Ctr_ContractDeposit_Save_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_Save_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_Save_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_Save_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_Save_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_Save_Input_Ctr_ContractDepositDtlTblInvalid		
		public const string Ctr_ContractDeposit_Save_Input_InvalidOrganCode = "ErridnInventory.Ctr_ContractDeposit_Save_Input_InvalidOrganCode"; //// //Ctr_ContractDeposit_Save_Input_InvalidOrganCode		

		// Ctr_ContractDeposit_Approve:
		public const string Ctr_ContractDeposit_Approve = "ErridnInventory.Ctr_ContractDeposit_Approve"; //// //Ctr_ContractDeposit_Approve
		public const string Ctr_ContractDeposit_Approve_KUNNDtlNotFound = "ErridnInventory.Ctr_ContractDeposit_Approve_KUNNDtlNotFound"; //// //Ctr_ContractDeposit_Approve_KUNNDtlNotFound

		// Ctr_ContractDeposit_UpdAppr:
		public const string Ctr_ContractDeposit_UpdAppr = "ErridnInventory.Ctr_ContractDeposit_UpdAppr"; //// //Ctr_ContractDeposit_UpdAppr
		public const string Ctr_ContractDeposit_UpdAppr_InvalidBGStatus = "ErridnInventory.Ctr_ContractDeposit_UpdAppr_InvalidBGStatus"; //// //Ctr_ContractDeposit_UpdAppr_InvalidBGStatus
		public const string Ctr_ContractDeposit_UpdAppr_InvalidValue = "ErridnInventory.Ctr_ContractDeposit_UpdAppr_InvalidValue"; //// //Ctr_ContractDeposit_UpdAppr_InvalidValue
		public const string Ctr_ContractDeposit_UpdAppr_InvalidSignedDate = "ErridnInventory.Ctr_ContractDeposit_UpdAppr_InvalidSignedDate"; //// //Ctr_ContractDeposit_UpdAppr_InvalidSignedDate
		public const string Ctr_ContractDeposit_UpdAppr_InvalidSellerCode = "ErridnInventory.Ctr_ContractDeposit_UpdAppr_InvalidSellerCode"; //// //Ctr_ContractDeposit_UpdAppr_InvalidSellerCode
		public const string Ctr_ContractDeposit_UpdAppr_Input_Ctr_ContractDepositDtlTblNotFound = "ErridnInventory.Ctr_ContractDeposit_UpdAppr_Input_Ctr_ContractDepositDtlTblNotFound"; //// //Ctr_ContractDeposit_UpdAppr_Input_Ctr_ContractDepositDtlTblNotFound		
		public const string Ctr_ContractDeposit_UpdAppr_Input_Ctr_ContractDepositDtlTblInvalid = "ErridnInventory.Ctr_ContractDeposit_UpdAppr_Input_Ctr_ContractDepositDtlTblInvalid"; //// //Ctr_ContractDeposit_UpdAppr_Input_Ctr_ContractDepositDtlTblInvalid		
		#endregion

		#region // Form_Receipt:
		// Form_Receipt_CheckDB:
		public const string Form_Receipt_CheckDB_ReceiptNotFound = "ErridnInventory.Form_Receipt_CheckDB_ReceiptNotFound"; //// //Form_Receipt_CheckDB_ReceiptNotFound
		public const string Form_Receipt_CheckDB_ReceiptExist = "ErridnInventory.Form_Receipt_CheckDB_ReceiptExist"; //// //Form_Receipt_CheckDB_ReceiptExist
		public const string Form_Receipt_CheckDB_StatusNotMatched = "ErridnInventory.Form_Receipt_CheckDB_StatusNotMatched"; //// //Form_Receipt_CheckDB_StatusNotMatched

		// Form_Receipt_CheckRefNo:
		public const string Form_Receipt_CheckRefNo_ReceiptNotFound = "ErridnInventory.Form_Receipt_CheckRefNo_ReceiptNotFound"; //// //Form_Receipt_CheckRefNo_ReceiptNotFound
		public const string Form_Receipt_CheckRefNo_ReceiptExist = "ErridnInventory.Form_Receipt_CheckRefNo_ReceiptExist"; //// //Form_Receipt_CheckRefNo_ReceiptExist
		public const string Form_Receipt_CheckRefNo_StatusNotMatched = "ErridnInventory.Form_Receipt_CheckRefNo_StatusNotMatched"; //// //Form_Receipt_CheckRefNo_StatusNotMatched

		// Form_Receipt_Get:
		public const string Form_Receipt_Get = "ErridnInventory.Form_Receipt_Get"; //// //Form_Receipt_Get

		// Form_Receipt_Del:
		public const string Form_Receipt_Del = "ErridnInventory.Form_Receipt_Del"; //// //Form_Receipt_Del

		// WAS_Form_Receipt_Get:
		public const string WAS_Form_Receipt_Get = "ErridnInventory.WAS_Form_Receipt_Get"; //// //WAS_Form_Receipt_Get

		// WAS_Form_Receipt_Save:
		public const string WAS_Form_Receipt_Save = "ErridnInventory.WAS_Form_Receipt_Save"; //// //WAS_Form_Receipt_Save


		public const string Form_Receipt_Check_InvalidTotalValReceipt = "ErridnInventory.Form_Receipt_Check_InvalidTotalValReceipt"; //// //Form_Receipt_Check_InvalidTotalValReceipt

		// Form_Receipt_Save:
		public const string Form_Receipt_Save = "ErridnInventory.Form_Receipt_Save"; //// //Form_Receipt_Save
		public const string Form_Receipt_Save_InvalidBGStatus = "ErridnInventory.Form_Receipt_Save_InvalidBGStatus"; //// //Form_Receipt_Save_InvalidBGStatus
		public const string Form_Receipt_Save_InvalidValue = "ErridnInventory.Form_Receipt_Save_InvalidValue"; //// //Form_Receipt_Save_InvalidValue
		public const string Form_Receipt_Save_InvalidYear = "ErridnInventory.Form_Receipt_Save_InvalidYear"; //// //Form_Receipt_Save_InvalidYear
		public const string Form_Receipt_Save_InvalidSignedDate = "ErridnInventory.Form_Receipt_Save_InvalidSignedDate"; //// //Form_Receipt_Save_InvalidSignedDate
		public const string Form_Receipt_Save_InvalidSellerCode = "ErridnInventory.Form_Receipt_Save_InvalidSellerCode"; //// //Form_Receipt_Save_InvalidSellerCode
		public const string Form_Receipt_Save_Input_Form_ReceiptDtlTblNotFound = "ErridnInventory.Form_Receipt_Save_Input_Form_ReceiptDtlTblNotFound"; //// //Form_Receipt_Save_Input_Form_ReceiptDtlTblNotFound		
		public const string Form_Receipt_Save_Input_Form_ReceiptDtlTblInvalid = "ErridnInventory.Form_Receipt_Save_Input_Form_ReceiptDtlTblInvalid"; //// //Form_Receipt_Save_Input_Form_ReceiptDtlTblInvalid		
		public const string Form_Receipt_Save_Input_InvalidOrganCode = "ErridnInventory.Form_Receipt_Save_Input_InvalidOrganCode"; //// //Form_Receipt_Save_Input_InvalidOrganCode		

		// Form_Receipt_Add:
		public const string Form_Receipt_Add = "ErridnInventory.Form_Receipt_Add"; //// //Form_Receipt_Add
		public const string Form_Receipt_Add_InvalidBGStatus = "ErridnInventory.Form_Receipt_Add_InvalidBGStatus"; //// //Form_Receipt_Add_InvalidBGStatus
		public const string Form_Receipt_Add_InvalidValue = "ErridnInventory.Form_Receipt_Add_InvalidValue"; //// //Form_Receipt_Add_InvalidValue
		public const string Form_Receipt_Add_InvalidYear = "ErridnInventory.Form_Receipt_Add_InvalidYear"; //// //Form_Receipt_Add_InvalidYear
		public const string Form_Receipt_Add_InvalidSignedDate = "ErridnInventory.Form_Receipt_Add_InvalidSignedDate"; //// //Form_Receipt_Add_InvalidSignedDate
		public const string Form_Receipt_Add_InvalidSellerCode = "ErridnInventory.Form_Receipt_Add_InvalidSellerCode"; //// //Form_Receipt_Add_InvalidSellerCode
		public const string Form_Receipt_Add_InvalidValReceipt = "ErridnInventory.Form_Receipt_Add_InvalidValReceipt"; //// //Form_Receipt_Add_InvalidValReceipt
		public const string Form_Receipt_Add_Input_Form_ReceiptTblNotFound = "ErridnInventory.Form_Receipt_Add_Input_Form_ReceiptTblNotFound"; //// //Form_Receipt_Add_Input_Form_ReceiptTblNotFound		
		public const string Form_Receipt_Add_Input_Form_ReceiptTblInvalid = "ErridnInventory.Form_Receipt_Add_Input_Form_ReceiptTblInvalid"; //// //Form_Receipt_Add_Input_Form_ReceiptTblInvalid		
		public const string Form_Receipt_Add_Input_Form_ReceiptDtlTblNotFound = "ErridnInventory.Form_Receipt_Add_Input_Form_ReceiptDtlTblNotFound"; //// //Form_Receipt_Add_Input_Form_ReceiptDtlTblNotFound		
		public const string Form_Receipt_Add_Input_Form_ReceiptDtlTblInvalid = "ErridnInventory.Form_Receipt_Add_Input_Form_ReceiptDtlTblInvalid"; //// //Form_Receipt_Add_Input_Form_ReceiptDtlTblInvalid		
		public const string Form_Receipt_Add_Input_InvalidOrgan = "ErridnInventory.Form_Receipt_Add_Input_InvalidOrgan"; //// //Form_Receipt_Add_Input_InvalidOrgan		
		public const string Form_Receipt_Add_Input_InvalidFlagGoc = "ErridnInventory.Form_Receipt_Add_Input_InvalidFlagGoc"; //// //Form_Receipt_Add_Input_InvalidFlagGoc		
		public const string Form_Receipt_Add_Input_InvalidFlagPay = "ErridnInventory.Form_Receipt_Add_Input_InvalidFlagPay"; //// //Form_Receipt_Add_Input_InvalidFlagPay		
		public const string Form_Receipt_Add_Input_ExistNullandNotNull = "ErridnInventory.Form_Receipt_Add_Input_ExistNullandNotNull"; //// //Form_Receipt_Add_Input_ExistNullandNotNull		
		public const string Form_Receipt_Add_Input_InvalidAssetType = "ErridnInventory.Form_Receipt_Add_Input_InvalidAssetType"; //// //Form_Receipt_Add_Input_InvalidAssetType		
		public const string Form_Receipt_Add_Input_InvalidOrganCode = "ErridnInventory.Form_Receipt_Add_Input_InvalidOrganCode"; //// //Form_Receipt_Add_Input_InvalidOrganCode		

		// Form_Receipt_Upd:
		public const string Form_Receipt_Upd = "ErridnInventory.Form_Receipt_Upd"; //// //Form_Receipt_Upd
		public const string Form_Receipt_Upd_InvalidBGStatus = "ErridnInventory.Form_Receipt_Upd_InvalidBGStatus"; //// //Form_Receipt_Upd_InvalidBGStatus
		public const string Form_Receipt_Upd_InvalidValue = "ErridnInventory.Form_Receipt_Upd_InvalidValue"; //// //Form_Receipt_Upd_InvalidValue
		public const string Form_Receipt_Upd_InvalidYear = "ErridnInventory.Form_Receipt_Upd_InvalidYear"; //// //Form_Receipt_Upd_InvalidYear
		public const string Form_Receipt_Upd_InvalidSignedDate = "ErridnInventory.Form_Receipt_Upd_InvalidSignedDate"; //// //Form_Receipt_Upd_InvalidSignedDate
		public const string Form_Receipt_Upd_InvalidSellerCode = "ErridnInventory.Form_Receipt_Upd_InvalidSellerCode"; //// //Form_Receipt_Upd_InvalidSellerCode
		public const string Form_Receipt_Upd_InvalidFlagGoc = "ErridnInventory.Form_Receipt_Upd_InvalidFlagGoc"; //// //Form_Receipt_Upd_InvalidFlagGoc		
		public const string Form_Receipt_Upd_Input_Form_ReceiptTblNotFound = "ErridnInventory.Form_Receipt_Upd_Input_Form_ReceiptTblNotFound"; //// //Form_Receipt_Upd_Input_Form_ReceiptTblNotFound		
		public const string Form_Receipt_Upd_Input_Form_ReceiptTblInvalid = "ErridnInventory.Form_Receipt_Upd_Input_Form_ReceiptTblInvalid"; //// //Form_Receipt_Upd_Input_Form_ReceiptTblInvalid		
		public const string Form_Receipt_Upd_Input_InvalidAssetType = "ErridnInventory.Form_Receipt_Upd_Input_InvalidAssetType"; //// //Form_Receipt_Upd_Input_InvalidAssetType		
		public const string Form_Receipt_Upd_Input_InvalidOrganCode = "ErridnInventory.Form_Receipt_Upd_Input_InvalidOrganCode"; //// //Form_Receipt_Upd_Input_InvalidOrganCode		

		// Form_Receipt_InActive:
		public const string Form_Receipt_InActive = "ErridnInventory.Form_Receipt_InActive"; //// //Form_Receipt_InActive
		public const string Form_Receipt_InActive_InvalidBGStatus = "ErridnInventory.Form_Receipt_InActive_InvalidBGStatus"; //// //Form_Receipt_InActive_InvalidBGStatus
		public const string Form_Receipt_InActive_InvalidValue = "ErridnInventory.Form_Receipt_InActive_InvalidValue"; //// //Form_Receipt_InActive_InvalidValue
		public const string Form_Receipt_InActive_InvalidYear = "ErridnInventory.Form_Receipt_InActive_InvalidYear"; //// //Form_Receipt_InActive_InvalidYear
		public const string Form_Receipt_InActive_InvalidSignedDate = "ErridnInventory.Form_Receipt_InActive_InvalidSignedDate"; //// //Form_Receipt_InActive_InvalidSignedDate
		public const string Form_Receipt_InActive_InvalidSellerCode = "ErridnInventory.Form_Receipt_InActive_InvalidSellerCode"; //// //Form_Receipt_InActive_InvalidSellerCode
		public const string Form_Receipt_InActive_Input_Form_ReceiptTblNotFound = "ErridnInventory.Form_Receipt_InActive_Input_Form_ReceiptTblNotFound"; //// //Form_Receipt_InActive_Input_Form_ReceiptTblNotFound		
		public const string Form_Receipt_InActive_Input_Form_ReceiptTblInvalid = "ErridnInventory.Form_Receipt_InActive_Input_Form_ReceiptTblInvalid"; //// //Form_Receipt_InActive_Input_Form_ReceiptTblInvalid		
		public const string Form_Receipt_InActive_Input_InvalidAssetType = "ErridnInventory.Form_Receipt_InActive_Input_InvalidAssetType"; //// //Form_Receipt_InActive_Input_InvalidAssetType		
		public const string Form_Receipt_InActive_Input_InvalidOrganCode = "ErridnInventory.Form_Receipt_InActive_Input_InvalidOrganCode"; //// //Form_Receipt_InActive_Input_InvalidOrganCode		


		// WAS_Form_Receipt_Add:
		public const string WAS_Form_Receipt_Add = "ErridnInventory.WAS_Form_Receipt_Add"; //// //WAS_Form_Receipt_Add

		// WAS_Form_Receipt_Upd:
		public const string WAS_Form_Receipt_Upd = "ErridnInventory.WAS_Form_Receipt_Upd"; //// //WAS_Form_Receipt_Upd

		// WAS_Form_Receipt_InActive:
		public const string WAS_Form_Receipt_InActive = "ErridnInventory.WAS_Form_Receipt_InActive"; //// //WAS_Form_Receipt_InActive

		// WAS_Form_Receipt_Delete:
		public const string WAS_Form_Receipt_Delete = "ErridnInventory.WAS_Form_Receipt_Delete"; //// //WAS_Form_Receipt_Delete

		// Form_Receipt_Approve:
		public const string Form_Receipt_Approve = "ErridnInventory.Form_Receipt_Approve"; //// //Form_Receipt_Approve
		public const string Form_Receipt_Approve_AssetDtlNotFound = "ErridnInventory.Form_Receipt_Approve_AssetDtlNotFound"; //// //Form_Receipt_Approve_AssetDtlNotFound

		// Form_Receipt_UpdAppr:
		public const string Form_Receipt_UpdAppr = "ErridnInventory.Form_Receipt_UpdAppr"; //// //Form_Receipt_UpdAppr
		public const string Form_Receipt_UpdAppr_InvalidBGStatus = "ErridnInventory.Form_Receipt_UpdAppr_InvalidBGStatus"; //// //Form_Receipt_UpdAppr_InvalidBGStatus
		public const string Form_Receipt_UpdAppr_InvalidValue = "ErridnInventory.Form_Receipt_UpdAppr_InvalidValue"; //// //Form_Receipt_UpdAppr_InvalidValue
		public const string Form_Receipt_UpdAppr_InvalidSignedDate = "ErridnInventory.Form_Receipt_UpdAppr_InvalidSignedDate"; //// //Form_Receipt_UpdAppr_InvalidSignedDate
		public const string Form_Receipt_UpdAppr_InvalidSellerCode = "ErridnInventory.Form_Receipt_UpdAppr_InvalidSellerCode"; //// //Form_Receipt_UpdAppr_InvalidSellerCode
		public const string Form_Receipt_UpdAppr_Input_Form_ReceiptDtlTblNotFound = "ErridnInventory.Form_Receipt_UpdAppr_Input_Form_ReceiptDtlTblNotFound"; //// //Form_Receipt_UpdAppr_Input_Form_ReceiptDtlTblNotFound		
		public const string Form_Receipt_UpdAppr_Input_Form_ReceiptDtlTblInvalid = "ErridnInventory.Form_Receipt_UpdAppr_Input_Form_ReceiptDtlTblInvalid"; //// //Form_Receipt_UpdAppr_Input_Form_ReceiptDtlTblInvalid		
		#endregion

		#region // Form_Payment:
		// Form_Payment_CheckDB:
		public const string Form_Payment_CheckDB_FrPmtNotFound = "ErridnInventory.Form_Payment_CheckDB_FrPmtNotFound"; //// //Form_Payment_CheckDB_FrPmtNotFound
		public const string Form_Payment_CheckDB_FrPmtExist = "ErridnInventory.Form_Payment_CheckDB_FrPmtExist"; //// //Form_Payment_CheckDB_FrPmtExist
		public const string Form_Payment_CheckDB_StatusNotMatched = "ErridnInventory.Form_Payment_CheckDB_StatusNotMatched"; //// //Form_Payment_CheckDB_StatusNotMatched

		// Form_Payment_CheckRefNo:
		public const string Form_Payment_CheckRefNo_FrPmtNotFound = "ErridnInventory.Form_Payment_CheckRefNo_FrPmtNotFound"; //// //Form_Payment_CheckRefNo_FrPmtNotFound
		public const string Form_Payment_CheckRefNo_FrPmtExist = "ErridnInventory.Form_Payment_CheckRefNo_FrPmtExist"; //// //Form_Payment_CheckRefNo_FrPmtExist
		public const string Form_Payment_CheckRefNo_StatusNotMatched = "ErridnInventory.Form_Payment_CheckRefNo_StatusNotMatched"; //// //Form_Payment_CheckRefNo_StatusNotMatched

		// Form_Payment_Get:
		public const string Form_Payment_Get = "ErridnInventory.Form_Payment_Get"; //// //Form_Payment_Get

		// Form_Payment_Del:
		public const string Form_Payment_Del = "ErridnInventory.Form_Payment_Del"; //// //Form_Payment_Del

		// WAS_Form_Payment_Get:
		public const string WAS_Form_Payment_Get = "ErridnInventory.WAS_Form_Payment_Get"; //// //WAS_Form_Payment_Get

		// WAS_Form_Payment_Save:
		public const string WAS_Form_Payment_Save = "ErridnInventory.WAS_Form_Payment_Save"; //// //WAS_Form_Payment_Save

		// Form_Payment_Check_InvalidTotalValFPmt:
		public const string Form_Payment_Check_InvalidTotalValFPmt = "ErridnInventory.Form_Payment_Check_InvalidTotalValFPmt"; //// //Form_Payment_Check_InvalidTotalValFPmt

		// Form_Payment_Save:
		public const string Form_Payment_Save = "ErridnInventory.Form_Payment_Save"; //// //Form_Payment_Save
		public const string Form_Payment_Save_InvalidBGStatus = "ErridnInventory.Form_Payment_Save_InvalidBGStatus"; //// //Form_Payment_Save_InvalidBGStatus
		public const string Form_Payment_Save_InvalidValue = "ErridnInventory.Form_Payment_Save_InvalidValue"; //// //Form_Payment_Save_InvalidValue
		public const string Form_Payment_Save_InvalidYear = "ErridnInventory.Form_Payment_Save_InvalidYear"; //// //Form_Payment_Save_InvalidYear
		public const string Form_Payment_Save_InvalidSignedDate = "ErridnInventory.Form_Payment_Save_InvalidSignedDate"; //// //Form_Payment_Save_InvalidSignedDate
		public const string Form_Payment_Save_InvalidSellerCode = "ErridnInventory.Form_Payment_Save_InvalidSellerCode"; //// //Form_Payment_Save_InvalidSellerCode
		public const string Form_Payment_Save_Input_Form_PaymentDtlTblNotFound = "ErridnInventory.Form_Payment_Save_Input_Form_PaymentDtlTblNotFound"; //// //Form_Payment_Save_Input_Form_PaymentDtlTblNotFound		
		public const string Form_Payment_Save_Input_Form_PaymentDtlTblInvalid = "ErridnInventory.Form_Payment_Save_Input_Form_PaymentDtlTblInvalid"; //// //Form_Payment_Save_Input_Form_PaymentDtlTblInvalid		
		public const string Form_Payment_Save_Input_InvalidOrganCode = "ErridnInventory.Form_Payment_Save_Input_InvalidOrganCode"; //// //Form_Payment_Save_Input_InvalidOrganCode		

		// Form_Payment_Add:
		public const string Form_Payment_Add = "ErridnInventory.Form_Payment_Add"; //// //Form_Payment_Add
		public const string Form_Payment_Add_InvalidBGStatus = "ErridnInventory.Form_Payment_Add_InvalidBGStatus"; //// //Form_Payment_Add_InvalidBGStatus
		public const string Form_Payment_Add_InvalidValue = "ErridnInventory.Form_Payment_Add_InvalidValue"; //// //Form_Payment_Add_InvalidValue
		public const string Form_Payment_Add_InvalidYear = "ErridnInventory.Form_Payment_Add_InvalidYear"; //// //Form_Payment_Add_InvalidYear
		public const string Form_Payment_Add_InvalidSignedDate = "ErridnInventory.Form_Payment_Add_InvalidSignedDate"; //// //Form_Payment_Add_InvalidSignedDate
		public const string Form_Payment_Add_InvalidSellerCode = "ErridnInventory.Form_Payment_Add_InvalidSellerCode"; //// //Form_Payment_Add_InvalidSellerCode
		public const string Form_Payment_Add_Input_Form_PaymentTblNotFound = "ErridnInventory.Form_Payment_Add_Input_Form_PaymentTblNotFound"; //// //Form_Payment_Add_Input_Form_PaymentTblNotFound		
		public const string Form_Payment_Add_Input_Form_PaymentTblInvalid = "ErridnInventory.Form_Payment_Add_Input_Form_PaymentTblInvalid"; //// //Form_Payment_Add_Input_Form_PaymentTblInvalid		
		public const string Form_Payment_Add_Input_Form_PaymentDtlTblNotFound = "ErridnInventory.Form_Payment_Add_Input_Form_PaymentDtlTblNotFound"; //// //Form_Payment_Add_Input_Form_PaymentDtlTblNotFound		
		public const string Form_Payment_Add_Input_Form_PaymentDtlTblInvalid = "ErridnInventory.Form_Payment_Add_Input_Form_PaymentDtlTblInvalid"; //// //Form_Payment_Add_Input_Form_PaymentDtlTblInvalid		
		public const string Form_Payment_Add_Input_InvalidOrgan = "ErridnInventory.Form_Payment_Add_Input_InvalidOrgan"; //// //Form_Payment_Add_Input_InvalidOrgan		
		public const string Form_Payment_Add_Input_InvalidFlagGoc = "ErridnInventory.Form_Payment_Add_Input_InvalidFlagGoc"; //// //Form_Payment_Add_Input_InvalidFlagGoc		
		public const string Form_Payment_Add_Input_InvalidFlagPay = "ErridnInventory.Form_Payment_Add_Input_InvalidFlagPay"; //// //Form_Payment_Add_Input_InvalidFlagPay		
		public const string Form_Payment_Add_Input_ExistNullandNotNull = "ErridnInventory.Form_Payment_Add_Input_ExistNullandNotNull"; //// //Form_Payment_Add_Input_ExistNullandNotNull		
		public const string Form_Payment_Add_Input_InvalidAssetType = "ErridnInventory.Form_Payment_Add_Input_InvalidAssetType"; //// //Form_Payment_Add_Input_InvalidAssetType		
		public const string Form_Payment_Add_Input_InvalidOrganCode = "ErridnInventory.Form_Payment_Add_Input_InvalidOrganCode"; //// //Form_Payment_Add_Input_InvalidOrganCode		

		// Form_Payment_Upd:
		public const string Form_Payment_Upd = "ErridnInventory.Form_Payment_Upd"; //// //Form_Payment_Upd
		public const string Form_Payment_Upd_InvalidBGStatus = "ErridnInventory.Form_Payment_Upd_InvalidBGStatus"; //// //Form_Payment_Upd_InvalidBGStatus
		public const string Form_Payment_Upd_InvalidValue = "ErridnInventory.Form_Payment_Upd_InvalidValue"; //// //Form_Payment_Upd_InvalidValue
		public const string Form_Payment_Upd_InvalidYear = "ErridnInventory.Form_Payment_Upd_InvalidYear"; //// //Form_Payment_Upd_InvalidYear
		public const string Form_Payment_Upd_InvalidSignedDate = "ErridnInventory.Form_Payment_Upd_InvalidSignedDate"; //// //Form_Payment_Upd_InvalidSignedDate
		public const string Form_Payment_Upd_InvalidSellerCode = "ErridnInventory.Form_Payment_Upd_InvalidSellerCode"; //// //Form_Payment_Upd_InvalidSellerCode
		public const string Form_Payment_Upd_InvalidFlagPay = "ErridnInventory.Form_Payment_Upd_InvalidFlagPay"; //// //Form_Payment_Upd_InvalidFlagPay
		public const string Form_Payment_Upd_InvalidOrgan = "ErridnInventory.Form_Payment_Upd_InvalidOrgan"; //// //Form_Payment_Upd_InvalidOrgan
		public const string Form_Payment_Upd_InvalidFlagGoc = "ErridnInventory.Form_Payment_Upd_InvalidFlagGoc"; //// //Form_Payment_Upd_InvalidFlagGoc
		public const string Form_Payment_Upd_Input_Form_PaymentTblNotFound = "ErridnInventory.Form_Payment_Upd_Input_Form_PaymentTblNotFound"; //// //Form_Payment_Upd_Input_Form_PaymentTblNotFound		
		public const string Form_Payment_Upd_Input_Form_PaymentTblInvalid = "ErridnInventory.Form_Payment_Upd_Input_Form_PaymentTblInvalid"; //// //Form_Payment_Upd_Input_Form_PaymentTblInvalid		
		public const string Form_Payment_Upd_Input_InvalidAssetType = "ErridnInventory.Form_Payment_Upd_Input_InvalidAssetType"; //// //Form_Payment_Upd_Input_InvalidAssetType		
		public const string Form_Payment_Upd_Input_InvalidOrganCode = "ErridnInventory.Form_Payment_Upd_Input_InvalidOrganCode"; //// //Form_Payment_Upd_Input_InvalidOrganCode		

		// Form_Payment_InActive:
		public const string Form_Payment_InActive = "ErridnInventory.Form_Payment_InActive"; //// //Form_Payment_InActive
		public const string Form_Payment_InActive_InvalidBGStatus = "ErridnInventory.Form_Payment_InActive_InvalidBGStatus"; //// //Form_Payment_InActive_InvalidBGStatus
		public const string Form_Payment_InActive_InvalidValue = "ErridnInventory.Form_Payment_InActive_InvalidValue"; //// //Form_Payment_InActive_InvalidValue
		public const string Form_Payment_InActive_InvalidYear = "ErridnInventory.Form_Payment_InActive_InvalidYear"; //// //Form_Payment_InActive_InvalidYear
		public const string Form_Payment_InActive_InvalidSignedDate = "ErridnInventory.Form_Payment_InActive_InvalidSignedDate"; //// //Form_Payment_InActive_InvalidSignedDate
		public const string Form_Payment_InActive_InvalidSellerCode = "ErridnInventory.Form_Payment_InActive_InvalidSellerCode"; //// //Form_Payment_InActive_InvalidSellerCode
		public const string Form_Payment_InActive_Input_Form_PaymentTblNotFound = "ErridnInventory.Form_Payment_InActive_Input_Form_PaymentTblNotFound"; //// //Form_Payment_InActive_Input_Form_PaymentTblNotFound		
		public const string Form_Payment_InActive_Input_Form_PaymentTblInvalid = "ErridnInventory.Form_Payment_InActive_Input_Form_PaymentTblInvalid"; //// //Form_Payment_InActive_Input_Form_PaymentTblInvalid		
		public const string Form_Payment_InActive_Input_InvalidAssetType = "ErridnInventory.Form_Payment_InActive_Input_InvalidAssetType"; //// //Form_Payment_InActive_Input_InvalidAssetType		
		public const string Form_Payment_InActive_Input_InvalidOrganCode = "ErridnInventory.Form_Payment_InActive_Input_InvalidOrganCode"; //// //Form_Payment_InActive_Input_InvalidOrganCode		


		// WAS_Form_Payment_Add:
		public const string WAS_Form_Payment_Add = "ErridnInventory.WAS_Form_Payment_Add"; //// //WAS_Form_Payment_Add

		// WAS_Form_Payment_Upd:
		public const string WAS_Form_Payment_Upd = "ErridnInventory.WAS_Form_Payment_Upd"; //// //WAS_Form_Payment_Upd

		// WAS_Form_Payment_InActive:
		public const string WAS_Form_Payment_InActive = "ErridnInventory.WAS_Form_Payment_InActive"; //// //WAS_Form_Payment_InActive

		// WAS_Form_Payment_Delete:
		public const string WAS_Form_Payment_Delete = "ErridnInventory.WAS_Form_Payment_Delete"; //// //WAS_Form_Payment_Delete

		// Form_Payment_Approve:
		public const string Form_Payment_Approve = "ErridnInventory.Form_Payment_Approve"; //// //Form_Payment_Approve
		public const string Form_Payment_Approve_AssetDtlNotFound = "ErridnInventory.Form_Payment_Approve_AssetDtlNotFound"; //// //Form_Payment_Approve_AssetDtlNotFound

		// Form_Payment_UpdAppr:
		public const string Form_Payment_UpdAppr = "ErridnInventory.Form_Payment_UpdAppr"; //// //Form_Payment_UpdAppr
		public const string Form_Payment_UpdAppr_InvalidBGStatus = "ErridnInventory.Form_Payment_UpdAppr_InvalidBGStatus"; //// //Form_Payment_UpdAppr_InvalidBGStatus
		public const string Form_Payment_UpdAppr_InvalidValue = "ErridnInventory.Form_Payment_UpdAppr_InvalidValue"; //// //Form_Payment_UpdAppr_InvalidValue
		public const string Form_Payment_UpdAppr_InvalidSignedDate = "ErridnInventory.Form_Payment_UpdAppr_InvalidSignedDate"; //// //Form_Payment_UpdAppr_InvalidSignedDate
		public const string Form_Payment_UpdAppr_InvalidSellerCode = "ErridnInventory.Form_Payment_UpdAppr_InvalidSellerCode"; //// //Form_Payment_UpdAppr_InvalidSellerCode
		public const string Form_Payment_UpdAppr_Input_Form_PaymentDtlTblNotFound = "ErridnInventory.Form_Payment_UpdAppr_Input_Form_PaymentDtlTblNotFound"; //// //Form_Payment_UpdAppr_Input_Form_PaymentDtlTblNotFound		
		public const string Form_Payment_UpdAppr_Input_Form_PaymentDtlTblInvalid = "ErridnInventory.Form_Payment_UpdAppr_Input_Form_PaymentDtlTblInvalid"; //// //Form_Payment_UpdAppr_Input_Form_PaymentDtlTblInvalid		
		#endregion

		#region // Cst_Input:
		// Cst_Input_CheckDB:
		public const string Cst_Input_CheckDB_InputNotFound = "ErridnInventory.Cst_Input_CheckDB_InputNotFound"; //// //Cst_Input_CheckDB_InputNotFound
		public const string Cst_Input_CheckDB_InputExist = "ErridnInventory.Cst_Input_CheckDB_InputExist"; //// //Cst_Input_CheckDB_InputExist
		public const string Cst_Input_CheckDB_StatusNotMatched = "ErridnInventory.Cst_Input_CheckDB_StatusNotMatched"; //// //Cst_Input_CheckDB_StatusNotMatched

		// Cst_Input_Get:
		public const string Cst_Input_Get = "ErridnInventory.Cst_Input_Get"; //// //Cst_Input_Get

		// Cst_Input_Del:
		public const string Cst_Input_Del = "ErridnInventory.Cst_Input_Del"; //// //Cst_Input_Del

		// WAS_Cst_Input_Get:
		public const string WAS_Cst_Input_Get = "ErridnInventory.WAS_Cst_Input_Get"; //// //WAS_Cst_Input_Get

		// WAS_Cst_Input_Save:
		public const string WAS_Cst_Input_Save = "ErridnInventory.WAS_Cst_Input_Save"; //// //WAS_Cst_Input_Save


		// Cst_Input_Save:
		public const string Cst_Input_Save = "ErridnInventory.Cst_Input_Save"; //// //Cst_Input_Save
		public const string Cst_Input_Save_InvalidBGStatus = "ErridnInventory.Cst_Input_Save_InvalidBGStatus"; //// //Cst_Input_Save_InvalidBGStatus
		public const string Cst_Input_Save_InvalidValue = "ErridnInventory.Cst_Input_Save_InvalidValue"; //// //Cst_Input_Save_InvalidValue
		public const string Cst_Input_Save_InvalidYear = "ErridnInventory.Cst_Input_Save_InvalidYear"; //// //Cst_Input_Save_InvalidYear
		public const string Cst_Input_Save_InvalidSignedDate = "ErridnInventory.Cst_Input_Save_InvalidSignedDate"; //// //Cst_Input_Save_InvalidSignedDate
		public const string Cst_Input_Save_InvalidSellerCode = "ErridnInventory.Cst_Input_Save_InvalidSellerCode"; //// //Cst_Input_Save_InvalidSellerCode
		public const string Cst_Input_Save_Input_Cst_InputDtlTblNotFound = "ErridnInventory.Cst_Input_Save_Input_Cst_InputDtlTblNotFound"; //// //Cst_Input_Save_Input_Cst_InputDtlTblNotFound		
		public const string Cst_Input_Save_Input_Cst_InputDtlTblInvalid = "ErridnInventory.Cst_Input_Save_Input_Cst_InputDtlTblInvalid"; //// //Cst_Input_Save_Input_Cst_InputDtlTblInvalid		
		public const string Cst_Input_Save_Input_InvalidOrganCode = "ErridnInventory.Cst_Input_Save_Input_InvalidOrganCode"; //// //Cst_Input_Save_Input_InvalidOrganCode		

		// Cst_Input_Add:
		public const string Cst_Input_Add = "ErridnInventory.Cst_Input_Add"; //// //Cst_Input_Add
		public const string Cst_Input_Add_InvalidBGStatus = "ErridnInventory.Cst_Input_Add_InvalidBGStatus"; //// //Cst_Input_Add_InvalidBGStatus
		public const string Cst_Input_Add_InvalidValue = "ErridnInventory.Cst_Input_Add_InvalidValue"; //// //Cst_Input_Add_InvalidValue
		public const string Cst_Input_Add_InvalidYear = "ErridnInventory.Cst_Input_Add_InvalidYear"; //// //Cst_Input_Add_InvalidYear
		public const string Cst_Input_Add_InvalidSignedDate = "ErridnInventory.Cst_Input_Add_InvalidSignedDate"; //// //Cst_Input_Add_InvalidSignedDate
		public const string Cst_Input_Add_InvalidSellerCode = "ErridnInventory.Cst_Input_Add_InvalidSellerCode"; //// //Cst_Input_Add_InvalidSellerCode
		public const string Cst_Input_Add_Input_Cst_InputTblNotFound = "ErridnInventory.Cst_Input_Add_Input_Cst_InputTblNotFound"; //// //Cst_Input_Add_Input_Cst_InputTblNotFound		
		public const string Cst_Input_Add_Input_Cst_InputTblInvalid = "ErridnInventory.Cst_Input_Add_Input_Cst_InputTblInvalid"; //// //Cst_Input_Add_Input_Cst_InputTblInvalid		
		public const string Cst_Input_Add_Input_Cst_InputDtlTblNotFound = "ErridnInventory.Cst_Input_Add_Input_Cst_InputDtlTblNotFound"; //// //Cst_Input_Add_Input_Cst_InputDtlTblNotFound		
		public const string Cst_Input_Add_Input_Cst_InputDtlTblInvalid = "ErridnInventory.Cst_Input_Add_Input_Cst_InputDtlTblInvalid"; //// //Cst_Input_Add_Input_Cst_InputDtlTblInvalid		
		public const string Cst_Input_Add_Input_InvalidAssetType = "ErridnInventory.Cst_Input_Add_Input_InvalidAssetType"; //// //Cst_Input_Add_Input_InvalidAssetType		
		public const string Cst_Input_Add_Input_InvalidOrganCode = "ErridnInventory.Cst_Input_Add_Input_InvalidOrganCode"; //// //Cst_Input_Add_Input_InvalidOrganCode		
		public const string Cst_Input_Add_Input_InvalidValue = "ErridnInventory.Cst_Input_Add_Input_InvalidValue"; //// //Cst_Input_Add_Input_InvalidValue		

		// Cst_Input_Upd:
		public const string Cst_Input_Upd = "ErridnInventory.Cst_Input_Upd"; //// //Cst_Input_Upd
		public const string Cst_Input_Upd_InvalidBGStatus = "ErridnInventory.Cst_Input_Upd_InvalidBGStatus"; //// //Cst_Input_Upd_InvalidBGStatus
		public const string Cst_Input_Upd_InvalidValue = "ErridnInventory.Cst_Input_Upd_InvalidValue"; //// //Cst_Input_Upd_InvalidValue
		public const string Cst_Input_Upd_InvalidYear = "ErridnInventory.Cst_Input_Upd_InvalidYear"; //// //Cst_Input_Upd_InvalidYear
		public const string Cst_Input_Upd_InvalidSignedDate = "ErridnInventory.Cst_Input_Upd_InvalidSignedDate"; //// //Cst_Input_Upd_InvalidSignedDate
		public const string Cst_Input_Upd_InvalidSellerCode = "ErridnInventory.Cst_Input_Upd_InvalidSellerCode"; //// //Cst_Input_Upd_InvalidSellerCode
		public const string Cst_Input_Upd_Input_Cst_InputTblNotFound = "ErridnInventory.Cst_Input_Upd_Input_Cst_InputTblNotFound"; //// //Cst_Input_Upd_Input_Cst_InputTblNotFound		
		public const string Cst_Input_Upd_Input_Cst_InputTblInvalid = "ErridnInventory.Cst_Input_Upd_Input_Cst_InputTblInvalid"; //// //Cst_Input_Upd_Input_Cst_InputTblInvalid		
		public const string Cst_Input_Upd_Input_Cst_InputDtlTblNotFound = "ErridnInventory.Cst_Input_Upd_Input_Cst_InputDtlTblNotFound"; //// //Cst_Input_Upd_Input_Cst_InputDtlTblNotFound		
		public const string Cst_Input_Upd_Input_Cst_InputDtlTblInvalid = "ErridnInventory.Cst_Input_Upd_Input_Cst_InputDtlTblInvalid"; //// //Cst_Input_Upd_Input_Cst_InputDtlTblInvalid		
		public const string Cst_Input_Upd_Input_InvalidAssetType = "ErridnInventory.Cst_Input_Upd_Input_InvalidAssetType"; //// //Cst_Input_Upd_Input_InvalidAssetType		
		public const string Cst_Input_Upd_Input_InvalidOrganCode = "ErridnInventory.Cst_Input_Upd_Input_InvalidOrganCode"; //// //Cst_Input_Upd_Input_InvalidOrganCode		
		public const string Cst_Input_Upd_Input_InvalidValue = "ErridnInventory.Cst_Input_Upd_Input_InvalidValue"; //// //Cst_Input_Upd_Input_InvalidValue		


		// Cst_Input_InActive:
		public const string Cst_Input_InActive = "ErridnInventory.Cst_Input_InActive"; //// //Cst_Input_InActive
		public const string Cst_Input_InActive_InvalidBGStatus = "ErridnInventory.Cst_Input_InActive_InvalidBGStatus"; //// //Cst_Input_InActive_InvalidBGStatus
		public const string Cst_Input_InActive_InvalidValue = "ErridnInventory.Cst_Input_InActive_InvalidValue"; //// //Cst_Input_InActive_InvalidValue
		public const string Cst_Input_InActive_InvalidYear = "ErridnInventory.Cst_Input_InActive_InvalidYear"; //// //Cst_Input_InActive_InvalidYear
		public const string Cst_Input_InActive_InvalidSignedDate = "ErridnInventory.Cst_Input_InActive_InvalidSignedDate"; //// //Cst_Input_InActive_InvalidSignedDate
		public const string Cst_Input_InActive_InvalidSellerCode = "ErridnInventory.Cst_Input_InActive_InvalidSellerCode"; //// //Cst_Input_InActive_InvalidSellerCode
		public const string Cst_Input_InActive_Input_Cst_InputTblNotFound = "ErridnInventory.Cst_Input_InActive_Input_Cst_InputTblNotFound"; //// //Cst_Input_InActive_Input_Cst_InputTblNotFound		
		public const string Cst_Input_InActive_Input_Cst_InputTblInvalid = "ErridnInventory.Cst_Input_InActive_Input_Cst_InputTblInvalid"; //// //Cst_Input_InActive_Input_Cst_InputTblInvalid		
		public const string Cst_Input_InActive_Input_InvalidAssetType = "ErridnInventory.Cst_Input_InActive_Input_InvalidAssetType"; //// //Cst_Input_InActive_Input_InvalidAssetType		
		public const string Cst_Input_InActive_Input_InvalidOrganCode = "ErridnInventory.Cst_Input_InActive_Input_InvalidOrganCode"; //// //Cst_Input_InActive_Input_InvalidOrganCode		


		// WAS_Cst_Input_Add:
		public const string WAS_Cst_Input_Add = "ErridnInventory.WAS_Cst_Input_Add"; //// //WAS_Cst_Input_Add

		// WAS_Cst_Input_Upd:
		public const string WAS_Cst_Input_Upd = "ErridnInventory.WAS_Cst_Input_Upd"; //// //WAS_Cst_Input_Upd

		// WAS_Cst_Input_InActive:
		public const string WAS_Cst_Input_InActive = "ErridnInventory.WAS_Cst_Input_InActive"; //// //WAS_Cst_Input_InActive

		// WAS_Cst_Input_Delete:
		public const string WAS_Cst_Input_Delete = "ErridnInventory.WAS_Cst_Input_Delete"; //// //WAS_Cst_Input_Delete

		// Cst_Input_Approve:
		public const string Cst_Input_Approve = "ErridnInventory.Cst_Input_Approve"; //// //Cst_Input_Approve
		public const string Cst_Input_Approve_AssetDtlNotFound = "ErridnInventory.Cst_Input_Approve_AssetDtlNotFound"; //// //Cst_Input_Approve_AssetDtlNotFound

		// Cst_Input_UpdAppr:
		public const string Cst_Input_UpdAppr = "ErridnInventory.Cst_Input_UpdAppr"; //// //Cst_Input_UpdAppr
		public const string Cst_Input_UpdAppr_InvalidBGStatus = "ErridnInventory.Cst_Input_UpdAppr_InvalidBGStatus"; //// //Cst_Input_UpdAppr_InvalidBGStatus
		public const string Cst_Input_UpdAppr_InvalidValue = "ErridnInventory.Cst_Input_UpdAppr_InvalidValue"; //// //Cst_Input_UpdAppr_InvalidValue
		public const string Cst_Input_UpdAppr_InvalidSignedDate = "ErridnInventory.Cst_Input_UpdAppr_InvalidSignedDate"; //// //Cst_Input_UpdAppr_InvalidSignedDate
		public const string Cst_Input_UpdAppr_InvalidSellerCode = "ErridnInventory.Cst_Input_UpdAppr_InvalidSellerCode"; //// //Cst_Input_UpdAppr_InvalidSellerCode
		public const string Cst_Input_UpdAppr_Input_Cst_InputDtlTblNotFound = "ErridnInventory.Cst_Input_UpdAppr_Input_Cst_InputDtlTblNotFound"; //// //Cst_Input_UpdAppr_Input_Cst_InputDtlTblNotFound		
		public const string Cst_Input_UpdAppr_Input_Cst_InputDtlTblInvalid = "ErridnInventory.Cst_Input_UpdAppr_Input_Cst_InputDtlTblInvalid"; //// //Cst_Input_UpdAppr_Input_Cst_InputDtlTblInvalid		
		#endregion

		#region // Cst_InputDtl:
		// Cst_InputDtl_CheckDB:
		public const string Cst_InputDtl_CheckDB_InputDtlNotFound = "ErridnInventory.Cst_InputDtl_CheckDB_InputDtlNotFound"; //// //Cst_InputDtl_CheckDB_InputDtlNotFound
		public const string Cst_InputDtl_CheckDB_InputDtlExist = "ErridnInventory.Cst_InputDtl_CheckDB_InputDtlExist"; //// //Cst_InputDtl_CheckDB_InputDtlExist
		public const string Cst_InputDtl_CheckDB_StatusNotMatched = "ErridnInventory.Cst_InputDtl_CheckDB_StatusNotMatched"; //// //Cst_InputDtl_CheckDB_StatusNotMatched

		// Cst_InputDtl_Get:
		public const string Cst_InputDtl_Get = "ErridnInventory.Cst_InputDtl_Get"; //// //Cst_InputDtl_Get

		// Cst_InputDtl_Upd:
		public const string Cst_InputDtl_Upd = "ErridnInventory.Cst_InputDtl_Upd"; //// //Cst_InputDtl_Upd
		public const string Cst_InputDtl_Upd_InvalidValue = "ErridnInventory.Cst_InputDtl_Upd_InvalidValue"; //// //Cst_InputDtl_Upd_InvalidValue
		public const string Cst_InputDtl_Upd_InvalidOrganCode = "ErridnInventory.Cst_InputDtl_Upd_InvalidOrganCode"; //// //Cst_InputDtl_Upd_InvalidOrganCode

		// Cst_InputDtl_Del:
		public const string Cst_InputDtl_Del = "ErridnInventory.Cst_InputDtl_Del"; //// //Cst_InputDtl_Del

		// WAS_Cst_InputDtl_Del:
		public const string WAS_Cst_InputDtl_Del = "ErridnInventory.WAS_Cst_InputDtl_Del"; //// //WAS_Cst_InputDtl_Del


		// WAS_Cst_InputDtl_Upd:
		public const string WAS_Cst_InputDtl_Upd = "ErridnInventory.WAS_Cst_InputDtl_Upd"; //// //WAS_Cst_InputDtl_Upd

		// WAS_Cst_InputDtl_Get:
		public const string WAS_Cst_InputDtl_Get = "ErridnInventory.WAS_Cst_InputDtl_Get"; //// //WAS_Cst_InputDtl_Get
		#endregion

		#region // LC_LC:
		// LC_LC_CheckDB:
		public const string LC_LC_CheckDB_LCNotFound = "ErridnInventory.LC_LC_CheckDB_LCNotFound"; //// //LC_LC_CheckDB_LCNotFound
		public const string LC_LC_CheckDB_LCExist = "ErridnInventory.LC_LC_CheckDB_LCExist"; //// //LC_LC_CheckDB_LCExist
		public const string LC_LC_CheckDB_StatusNotMatched = "ErridnInventory.LC_LC_CheckDB_StatusNotMatched"; //// //LC_LC_CheckDB_StatusNotMatched

		// LC_LC_Get:
		public const string LC_LC_Get = "ErridnInventory.LC_LC_Get"; //// //LC_LC_Get

		// LC_LC_Del:
		public const string LC_LC_Del = "ErridnInventory.LC_LC_Del"; //// //LC_LC_Del

		// WAS_LC_LC_Get:
		public const string WAS_LC_LC_Get = "ErridnInventory.WAS_LC_LC_Get"; //// //WAS_LC_LC_Get

		// WAS_LC_LC_Save:
		public const string WAS_LC_LC_Save = "ErridnInventory.WAS_LC_LC_Save"; //// //WAS_LC_LC_Save


		// LC_LC_Save:
		public const string LC_LC_Save = "ErridnInventory.LC_LC_Save"; //// //LC_LC_Save
		public const string LC_LC_Save_InvalidBGStatus = "ErridnInventory.LC_LC_Save_InvalidBGStatus"; //// //LC_LC_Save_InvalidBGStatus
		public const string LC_LC_Save_InvalidValue = "ErridnInventory.LC_LC_Save_InvalidValue"; //// //LC_LC_Save_InvalidValue
		public const string LC_LC_Save_InvalidYear = "ErridnInventory.LC_LC_Save_InvalidYear"; //// //LC_LC_Save_InvalidYear
		public const string LC_LC_Save_InvalidSignedDate = "ErridnInventory.LC_LC_Save_InvalidSignedDate"; //// //LC_LC_Save_InvalidSignedDate
		public const string LC_LC_Save_InvalidSellerCode = "ErridnInventory.LC_LC_Save_InvalidSellerCode"; //// //LC_LC_Save_InvalidSellerCode
		public const string LC_LC_Save_Input_LC_LCDtlTblNotFound = "ErridnInventory.LC_LC_Save_Input_LC_LCDtlTblNotFound"; //// //LC_LC_Save_Input_LC_LCDtlTblNotFound		
		public const string LC_LC_Save_Input_LC_LCDtlTblInvalid = "ErridnInventory.LC_LC_Save_Input_LC_LCDtlTblInvalid"; //// //LC_LC_Save_Input_LC_LCDtlTblInvalid		
		public const string LC_LC_Save_Input_InvalidOrganCode = "ErridnInventory.LC_LC_Save_Input_InvalidOrganCode"; //// //LC_LC_Save_Input_InvalidOrganCode		

		// LC_LC_Add:
		public const string LC_LC_Add = "ErridnInventory.LC_LC_Add"; //// //LC_LC_Add
		public const string LC_LC_Add_InvalidBGStatus = "ErridnInventory.LC_LC_Add_InvalidBGStatus"; //// //LC_LC_Add_InvalidBGStatus
		public const string LC_LC_Add_InvalidValue = "ErridnInventory.LC_LC_Add_InvalidValue"; //// //LC_LC_Add_InvalidValue
		public const string LC_LC_Add_InvalidYear = "ErridnInventory.LC_LC_Add_InvalidYear"; //// //LC_LC_Add_InvalidYear
		public const string LC_LC_Add_InvalidSignedDate = "ErridnInventory.LC_LC_Add_InvalidSignedDate"; //// //LC_LC_Add_InvalidSignedDate
		public const string LC_LC_Add_InvalidSellerCode = "ErridnInventory.LC_LC_Add_InvalidSellerCode"; //// //LC_LC_Add_InvalidSellerCode
		public const string LC_LC_Add_InvalidPhatHanhDate = "ErridnInventory.LC_LC_Add_InvalidPhatHanhDate"; //// //LC_LC_Add_InvalidPhatHanhDate
		public const string LC_LC_Add_InvalidHetHieuLucDate = "ErridnInventory.LC_LC_Add_InvalidHetHieuLucDate"; //// //LC_LC_Add_InvalidHetHieuLucDate
		public const string LC_LC_Add_InvalidPhatHanhDateBeforeHetHieuLucDate = "ErridnInventory.LC_LC_Add_InvalidPhatHanhDateBeforeHetHieuLucDate"; //// //LC_LC_Add_InvalidPhatHanhDateBeforeHetHieuLucDate
		public const string LC_LC_Add_InvalidPhatHanhDateBeforeEffSysDate = "ErridnInventory.LC_LC_Add_InvalidPhatHanhDateBeforeEffSysDate"; //// //LC_LC_Add_InvalidPhatHanhDateBeforeEffSysDate
		public const string LC_LC_Add_InvalidNhanNoDateAfterPhatHanhDate = "ErridnInventory.LC_LC_Add_InvalidNhanNoDateAfterPhatHanhDate"; //// //LC_LC_Add_InvalidNhanNoDateAfterPhatHanhDate
		public const string LC_LC_Add_Input_LC_LCTblNotFound = "ErridnInventory.LC_LC_Add_Input_LC_LCTblNotFound"; //// //LC_LC_Add_Input_LC_LCTblNotFound		
		public const string LC_LC_Add_Input_LC_LCTblInvalid = "ErridnInventory.LC_LC_Add_Input_LC_LCTblInvalid"; //// //LC_LC_Add_Input_LC_LCTblInvalid		
		public const string LC_LC_Add_Input_LC_LCDtlTblNotFound = "ErridnInventory.LC_LC_Add_Input_LC_LCDtlTblNotFound"; //// //LC_LC_Add_Input_LC_LCDtlTblNotFound		
		public const string LC_LC_Add_Input_LC_LCDtlTblInvalid = "ErridnInventory.LC_LC_Add_Input_LC_LCDtlTblInvalid"; //// //LC_LC_Add_Input_LC_LCDtlTblInvalid		
		public const string LC_LC_Add_Input_InvalidAssetType = "ErridnInventory.LC_LC_Add_Input_InvalidAssetType"; //// //LC_LC_Add_Input_InvalidAssetType		
		public const string LC_LC_Add_Input_InvalidOrganCode = "ErridnInventory.LC_LC_Add_Input_InvalidOrganCode"; //// //LC_LC_Add_Input_InvalidOrganCode		
		public const string LC_LC_Add_Input_InvalidValue = "ErridnInventory.LC_LC_Add_Input_InvalidValue"; //// //LC_LC_Add_Input_InvalidValue		

		public const string LC_LC_Check_InvalidTotalValNhanNo = "ErridnInventory.LC_LC_Check_InvalidTotalValNhanNo"; //// //LC_LC_Check_InvalidTotalValNhanNo		

		// LC_LC_Upd:
		public const string LC_LC_Upd = "ErridnInventory.LC_LC_Upd"; //// //LC_LC_Upd
		public const string LC_LC_Upd_InvalidBGStatus = "ErridnInventory.LC_LC_Upd_InvalidBGStatus"; //// //LC_LC_Upd_InvalidBGStatus
		public const string LC_LC_Upd_InvalidValue = "ErridnInventory.LC_LC_Upd_InvalidValue"; //// //LC_LC_Upd_InvalidValue
		public const string LC_LC_Upd_InvalidYear = "ErridnInventory.LC_LC_Upd_InvalidYear"; //// //LC_LC_Upd_InvalidYear
		public const string LC_LC_Upd_InvalidSignedDate = "ErridnInventory.LC_LC_Upd_InvalidSignedDate"; //// //LC_LC_Upd_InvalidSignedDate
		public const string LC_LC_Upd_InvalidSellerCode = "ErridnInventory.LC_LC_Upd_InvalidSellerCode"; //// //LC_LC_Upd_InvalidSellerCode
		public const string LC_LC_Upd_InvalidPhatHanhDate = "ErridnInventory.LC_LC_Upd_InvalidPhatHanhDate"; //// //LC_LC_Upd_InvalidPhatHanhDate
		public const string LC_LC_Upd_InvalidHetHieuLucDate = "ErridnInventory.LC_LC_Upd_InvalidHetHieuLucDate"; //// //LC_LC_Upd_InvalidHetHieuLucDate
		public const string LC_LC_Upd_InvalidPhatHanhDateBeforeHetHieuLucDate = "ErridnInventory.LC_LC_Upd_InvalidPhatHanhDateBeforeHetHieuLucDate"; //// //LC_LC_Upd_InvalidPhatHanhDateBeforeHetHieuLucDate
		public const string LC_LC_Upd_InvalidPhatHanhDateBeforeEffSysDate = "ErridnInventory.LC_LC_Upd_InvalidPhatHanhDateBeforeEffSysDate"; //// //LC_LC_Upd_InvalidPhatHanhDateBeforeEffSysDate
		public const string LC_LC_Upd_InvalidNhanNoDateAfterPhatHanhDate = "ErridnInventory.LC_LC_Upd_InvalidNhanNoDateAfterPhatHanhDate"; //// //LC_LC_Upd_InvalidNhanNoDateAfterPhatHanhDate
		public const string LC_LC_Upd_Input_LC_LCTblNotFound = "ErridnInventory.LC_LC_Upd_Input_LC_LCTblNotFound"; //// //LC_LC_Upd_Input_LC_LCTblNotFound		
		public const string LC_LC_Upd_Input_LC_LCTblInvalid = "ErridnInventory.LC_LC_Upd_Input_LC_LCTblInvalid"; //// //LC_LC_Upd_Input_LC_LCTblInvalid		
		public const string LC_LC_Upd_Input_LC_LCDtlTblNotFound = "ErridnInventory.LC_LC_Upd_Input_LC_LCDtlTblNotFound"; //// //LC_LC_Upd_Input_LC_LCDtlTblNotFound		
		public const string LC_LC_Upd_Input_LC_LCDtlTblInvalid = "ErridnInventory.LC_LC_Upd_Input_LC_LCDtlTblInvalid"; //// //LC_LC_Upd_Input_LC_LCDtlTblInvalid		
		public const string LC_LC_Upd_Input_InvalidAssetType = "ErridnInventory.LC_LC_Upd_Input_InvalidAssetType"; //// //LC_LC_Upd_Input_InvalidAssetType		
		public const string LC_LC_Upd_Input_InvalidOrganCode = "ErridnInventory.LC_LC_Upd_Input_InvalidOrganCode"; //// //LC_LC_Upd_Input_InvalidOrganCode		
		public const string LC_LC_Upd_Input_InvalidValue = "ErridnInventory.LC_LC_Upd_Input_InvalidValue"; //// //LC_LC_Upd_Input_InvalidValue		

		// LC_LC_InActive:
		public const string LC_LC_InActive = "ErridnInventory.LC_LC_InActive"; //// //LC_LC_InActive
		public const string LC_LC_InActive_InvalidBGStatus = "ErridnInventory.LC_LC_InActive_InvalidBGStatus"; //// //LC_LC_InActive_InvalidBGStatus
		public const string LC_LC_InActive_InvalidValue = "ErridnInventory.LC_LC_InActive_InvalidValue"; //// //LC_LC_InActive_InvalidValue
		public const string LC_LC_InActive_InvalidYear = "ErridnInventory.LC_LC_InActive_InvalidYear"; //// //LC_LC_InActive_InvalidYear
		public const string LC_LC_InActive_InvalidSignedDate = "ErridnInventory.LC_LC_InActive_InvalidSignedDate"; //// //LC_LC_InActive_InvalidSignedDate
		public const string LC_LC_InActive_InvalidSellerCode = "ErridnInventory.LC_LC_InActive_InvalidSellerCode"; //// //LC_LC_InActive_InvalidSellerCode
		public const string LC_LC_InActive_Input_LC_LCTblNotFound = "ErridnInventory.LC_LC_InActive_Input_LC_LCTblNotFound"; //// //LC_LC_InActive_Input_LC_LCTblNotFound		
		public const string LC_LC_InActive_Input_LC_LCTblInvalid = "ErridnInventory.LC_LC_InActive_Input_LC_LCTblInvalid"; //// //LC_LC_InActive_Input_LC_LCTblInvalid		
		public const string LC_LC_InActive_Input_InvalidAssetType = "ErridnInventory.LC_LC_InActive_Input_InvalidAssetType"; //// //LC_LC_InActive_Input_InvalidAssetType		
		public const string LC_LC_InActive_Input_InvalidOrganCode = "ErridnInventory.LC_LC_InActive_Input_InvalidOrganCode"; //// //LC_LC_InActive_Input_InvalidOrganCode		


		// WAS_LC_LC_Add:
		public const string WAS_LC_LC_Add = "ErridnInventory.WAS_LC_LC_Add"; //// //WAS_LC_LC_Add

		// WAS_LC_LC_Upd:
		public const string WAS_LC_LC_Upd = "ErridnInventory.WAS_LC_LC_Upd"; //// //WAS_LC_LC_Upd

		// WAS_LC_LC_InActive:
		public const string WAS_LC_LC_InActive = "ErridnInventory.WAS_LC_LC_InActive"; //// //WAS_LC_LC_InActive

		// WAS_LC_LC_Delete:
		public const string WAS_LC_LC_Delete = "ErridnInventory.WAS_LC_LC_Delete"; //// //WAS_LC_LC_Delete

		// LC_LC_Approve:
		public const string LC_LC_Approve = "ErridnInventory.LC_LC_Approve"; //// //LC_LC_Approve
		public const string LC_LC_Approve_AssetDtlNotFound = "ErridnInventory.LC_LC_Approve_AssetDtlNotFound"; //// //LC_LC_Approve_AssetDtlNotFound

		// LC_LC_UpdAppr:
		public const string LC_LC_UpdAppr = "ErridnInventory.LC_LC_UpdAppr"; //// //LC_LC_UpdAppr
		public const string LC_LC_UpdAppr_InvalidBGStatus = "ErridnInventory.LC_LC_UpdAppr_InvalidBGStatus"; //// //LC_LC_UpdAppr_InvalidBGStatus
		public const string LC_LC_UpdAppr_InvalidValue = "ErridnInventory.LC_LC_UpdAppr_InvalidValue"; //// //LC_LC_UpdAppr_InvalidValue
		public const string LC_LC_UpdAppr_InvalidSignedDate = "ErridnInventory.LC_LC_UpdAppr_InvalidSignedDate"; //// //LC_LC_UpdAppr_InvalidSignedDate
		public const string LC_LC_UpdAppr_InvalidSellerCode = "ErridnInventory.LC_LC_UpdAppr_InvalidSellerCode"; //// //LC_LC_UpdAppr_InvalidSellerCode
		public const string LC_LC_UpdAppr_Input_LC_LCDtlTblNotFound = "ErridnInventory.LC_LC_UpdAppr_Input_LC_LCDtlTblNotFound"; //// //LC_LC_UpdAppr_Input_LC_LCDtlTblNotFound		
		public const string LC_LC_UpdAppr_Input_LC_LCDtlTblInvalid = "ErridnInventory.LC_LC_UpdAppr_Input_LC_LCDtlTblInvalid"; //// //LC_LC_UpdAppr_Input_LC_LCDtlTblInvalid		
		#endregion

		#region // MD_MD:
		// MD_MD_CheckDB:
		public const string MD_MD_CheckDB_MDNotFound = "ErridnInventory.MD_MD_CheckDB_MDNotFound"; //// //MD_MD_CheckDB_MDNotFound
		public const string MD_MD_CheckDB_MDExist = "ErridnInventory.MD_MD_CheckDB_MDExist"; //// //MD_MD_CheckDB_MDExist
		public const string MD_MD_CheckDB_StatusNotMatched = "ErridnInventory.MD_MD_CheckDB_StatusNotMatched"; //// //MD_MD_CheckDB_StatusNotMatched

		// MD_MD_Get:
		public const string MD_MD_Get = "ErridnInventory.MD_MD_Get"; //// //MD_MD_Get

		// MD_MD_Del:
		public const string MD_MD_Del = "ErridnInventory.MD_MD_Del"; //// //MD_MD_Del

		// WAS_MD_MD_Get:
		public const string WAS_MD_MD_Get = "ErridnInventory.WAS_MD_MD_Get"; //// //WAS_MD_MD_Get

		// WAS_MD_MD_Save:
		public const string WAS_MD_MD_Save = "ErridnInventory.WAS_MD_MD_Save"; //// //WAS_MD_MD_Save


		// MD_MD_Save:
		public const string MD_MD_Save = "ErridnInventory.MD_MD_Save"; //// //MD_MD_Save
		public const string MD_MD_Save_InvalidBGStatus = "ErridnInventory.MD_MD_Save_InvalidBGStatus"; //// //MD_MD_Save_InvalidBGStatus
		public const string MD_MD_Save_InvalidValue = "ErridnInventory.MD_MD_Save_InvalidValue"; //// //MD_MD_Save_InvalidValue
		public const string MD_MD_Save_InvalidYear = "ErridnInventory.MD_MD_Save_InvalidYear"; //// //MD_MD_Save_InvalidYear
		public const string MD_MD_Save_InvalidSignedDate = "ErridnInventory.MD_MD_Save_InvalidSignedDate"; //// //MD_MD_Save_InvalidSignedDate
		public const string MD_MD_Save_InvalidSellerCode = "ErridnInventory.MD_MD_Save_InvalidSellerCode"; //// //MD_MD_Save_InvalidSellerCode
		public const string MD_MD_Save_Input_MD_MDDtlTblNotFound = "ErridnInventory.MD_MD_Save_Input_MD_MDDtlTblNotFound"; //// //MD_MD_Save_Input_MD_MDDtlTblNotFound		
		public const string MD_MD_Save_Input_MD_MDDtlTblInvalid = "ErridnInventory.MD_MD_Save_Input_MD_MDDtlTblInvalid"; //// //MD_MD_Save_Input_MD_MDDtlTblInvalid		
		public const string MD_MD_Save_Input_InvalidOrganCode = "ErridnInventory.MD_MD_Save_Input_InvalidOrganCode"; //// //MD_MD_Save_Input_InvalidOrganCode		

		// MD_MD_Add:
		public const string MD_MD_Add = "ErridnInventory.MD_MD_Add"; //// //MD_MD_Add
		public const string MD_MD_Add_InvalidBGStatus = "ErridnInventory.MD_MD_Add_InvalidBGStatus"; //// //MD_MD_Add_InvalidBGStatus
		public const string MD_MD_Add_InvalidValue = "ErridnInventory.MD_MD_Add_InvalidValue"; //// //MD_MD_Add_InvalidValue
		public const string MD_MD_Add_InvalidYear = "ErridnInventory.MD_MD_Add_InvalidYear"; //// //MD_MD_Add_InvalidYear
		public const string MD_MD_Add_InvalidSignedDate = "ErridnInventory.MD_MD_Add_InvalidSignedDate"; //// //MD_MD_Add_InvalidSignedDate
		public const string MD_MD_Add_InvalidSellerCode = "ErridnInventory.MD_MD_Add_InvalidSellerCode"; //// //MD_MD_Add_InvalidSellerCode
		public const string MD_MD_Add_InvalidPhatHanhDate = "ErridnInventory.MD_MD_Add_InvalidPhatHanhDate"; //// //MD_MD_Add_InvalidPhatHanhDate
		public const string MD_MD_Add_InvalidHetHieuLucDate = "ErridnInventory.MD_MD_Add_InvalidHetHieuLucDate"; //// //MD_MD_Add_InvalidHetHieuLucDate
		public const string MD_MD_Add_InvalidPhatHanhDateBeforeHetHieuLucDate = "ErridnInventory.MD_MD_Add_InvalidPhatHanhDateBeforeHetHieuLucDate"; //// //MD_MD_Add_InvalidPhatHanhDateBeforeHetHieuLucDate
		public const string MD_MD_Add_InvalidPhatHanhDateBeforeEffSysDate = "ErridnInventory.MD_MD_Add_InvalidPhatHanhDateBeforeEffSysDate"; //// //MD_MD_Add_InvalidPhatHanhDateBeforeEffSysDate
		public const string MD_MD_Add_InvalidNhanNoDateAfterPhatHanhDate = "ErridnInventory.MD_MD_Add_InvalidNhanNoDateAfterPhatHanhDate"; //// //MD_MD_Add_InvalidNhanNoDateAfterPhatHanhDate
		public const string MD_MD_Add_Input_MD_MDTblNotFound = "ErridnInventory.MD_MD_Add_Input_MD_MDTblNotFound"; //// //MD_MD_Add_Input_MD_MDTblNotFound		
		public const string MD_MD_Add_Input_MD_MDTblInvalid = "ErridnInventory.MD_MD_Add_Input_MD_MDTblInvalid"; //// //MD_MD_Add_Input_MD_MDTblInvalid		
		public const string MD_MD_Add_Input_MD_MDDtlTblNotFound = "ErridnInventory.MD_MD_Add_Input_MD_MDDtlTblNotFound"; //// //MD_MD_Add_Input_MD_MDDtlTblNotFound		
		public const string MD_MD_Add_Input_MD_MDDtlTblInvalid = "ErridnInventory.MD_MD_Add_Input_MD_MDDtlTblInvalid"; //// //MD_MD_Add_Input_MD_MDDtlTblInvalid		
		public const string MD_MD_Add_Input_InvalidAssetType = "ErridnInventory.MD_MD_Add_Input_InvalidAssetType"; //// //MD_MD_Add_Input_InvalidAssetType		
		public const string MD_MD_Add_Input_InvalidOrganCode = "ErridnInventory.MD_MD_Add_Input_InvalidOrganCode"; //// //MD_MD_Add_Input_InvalidOrganCode		
		public const string MD_MD_Add_Input_InvalidValue = "ErridnInventory.MD_MD_Add_Input_InvalidValue"; //// //MD_MD_Add_Input_InvalidValue		

		public const string MD_MD_Check_InvalidTotalValNhanNo = "ErridnInventory.MD_MD_Check_InvalidTotalValNhanNo"; //// //MD_MD_Check_InvalidTotalValNhanNo		

		// MD_MD_Upd:
		public const string MD_MD_Upd = "ErridnInventory.MD_MD_Upd"; //// //MD_MD_Upd
		public const string MD_MD_Upd_InvalidBGStatus = "ErridnInventory.MD_MD_Upd_InvalidBGStatus"; //// //MD_MD_Upd_InvalidBGStatus
		public const string MD_MD_Upd_InvalidValue = "ErridnInventory.MD_MD_Upd_InvalidValue"; //// //MD_MD_Upd_InvalidValue
		public const string MD_MD_Upd_InvalidYear = "ErridnInventory.MD_MD_Upd_InvalidYear"; //// //MD_MD_Upd_InvalidYear
		public const string MD_MD_Upd_InvalidSignedDate = "ErridnInventory.MD_MD_Upd_InvalidSignedDate"; //// //MD_MD_Upd_InvalidSignedDate
		public const string MD_MD_Upd_InvalidSellerCode = "ErridnInventory.MD_MD_Upd_InvalidSellerCode"; //// //MD_MD_Upd_InvalidSellerCode
		public const string MD_MD_Upd_InvalidPhatHanhDate = "ErridnInventory.MD_MD_Upd_InvalidPhatHanhDate"; //// //MD_MD_Upd_InvalidPhatHanhDate
		public const string MD_MD_Upd_InvalidHetHieuLucDate = "ErridnInventory.MD_MD_Upd_InvalidHetHieuLucDate"; //// //MD_MD_Upd_InvalidHetHieuLucDate
		public const string MD_MD_Upd_InvalidPhatHanhDateBeforeHetHieuLucDate = "ErridnInventory.MD_MD_Upd_InvalidPhatHanhDateBeforeHetHieuLucDate"; //// //MD_MD_Upd_InvalidPhatHanhDateBeforeHetHieuLucDate
		public const string MD_MD_Upd_InvalidPhatHanhDateBeforeEffSysDate = "ErridnInventory.MD_MD_Upd_InvalidPhatHanhDateBeforeEffSysDate"; //// //MD_MD_Upd_InvalidPhatHanhDateBeforeEffSysDate
		public const string MD_MD_Upd_InvalidNhanNoDateAfterPhatHanhDate = "ErridnInventory.MD_MD_Upd_InvalidNhanNoDateAfterPhatHanhDate"; //// //MD_MD_Upd_InvalidNhanNoDateAfterPhatHanhDate
		public const string MD_MD_Upd_Input_MD_MDTblNotFound = "ErridnInventory.MD_MD_Upd_Input_MD_MDTblNotFound"; //// //MD_MD_Upd_Input_MD_MDTblNotFound		
		public const string MD_MD_Upd_Input_MD_MDTblInvalid = "ErridnInventory.MD_MD_Upd_Input_MD_MDTblInvalid"; //// //MD_MD_Upd_Input_MD_MDTblInvalid		
		public const string MD_MD_Upd_Input_MD_MDDtlTblNotFound = "ErridnInventory.MD_MD_Upd_Input_MD_MDDtlTblNotFound"; //// //MD_MD_Upd_Input_MD_MDDtlTblNotFound		
		public const string MD_MD_Upd_Input_MD_MDDtlTblInvalid = "ErridnInventory.MD_MD_Upd_Input_MD_MDDtlTblInvalid"; //// //MD_MD_Upd_Input_MD_MDDtlTblInvalid		
		public const string MD_MD_Upd_Input_InvalidAssetType = "ErridnInventory.MD_MD_Upd_Input_InvalidAssetType"; //// //MD_MD_Upd_Input_InvalidAssetType		
		public const string MD_MD_Upd_Input_InvalidOrganCode = "ErridnInventory.MD_MD_Upd_Input_InvalidOrganCode"; //// //MD_MD_Upd_Input_InvalidOrganCode		
		public const string MD_MD_Upd_Input_InvalidValue = "ErridnInventory.MD_MD_Upd_Input_InvalidValue"; //// //MD_MD_Upd_Input_InvalidValue		

		// MD_MD_InActive:
		public const string MD_MD_InActive = "ErridnInventory.MD_MD_InActive"; //// //MD_MD_InActive
		public const string MD_MD_InActive_InvalidBGStatus = "ErridnInventory.MD_MD_InActive_InvalidBGStatus"; //// //MD_MD_InActive_InvalidBGStatus
		public const string MD_MD_InActive_InvalidValue = "ErridnInventory.MD_MD_InActive_InvalidValue"; //// //MD_MD_InActive_InvalidValue
		public const string MD_MD_InActive_InvalidYear = "ErridnInventory.MD_MD_InActive_InvalidYear"; //// //MD_MD_InActive_InvalidYear
		public const string MD_MD_InActive_InvalidSignedDate = "ErridnInventory.MD_MD_InActive_InvalidSignedDate"; //// //MD_MD_InActive_InvalidSignedDate
		public const string MD_MD_InActive_InvalidSellerCode = "ErridnInventory.MD_MD_InActive_InvalidSellerCode"; //// //MD_MD_InActive_InvalidSellerCode
		public const string MD_MD_InActive_Input_MD_MDTblNotFound = "ErridnInventory.MD_MD_InActive_Input_MD_MDTblNotFound"; //// //MD_MD_InActive_Input_MD_MDTblNotFound		
		public const string MD_MD_InActive_Input_MD_MDTblInvalid = "ErridnInventory.MD_MD_InActive_Input_MD_MDTblInvalid"; //// //MD_MD_InActive_Input_MD_MDTblInvalid		
		public const string MD_MD_InActive_Input_InvalidAssetType = "ErridnInventory.MD_MD_InActive_Input_InvalidAssetType"; //// //MD_MD_InActive_Input_InvalidAssetType		
		public const string MD_MD_InActive_Input_InvalidOrganCode = "ErridnInventory.MD_MD_InActive_Input_InvalidOrganCode"; //// //MD_MD_InActive_Input_InvalidOrganCode		


		// WAS_MD_MD_Add:
		public const string WAS_MD_MD_Add = "ErridnInventory.WAS_MD_MD_Add"; //// //WAS_MD_MD_Add

		// WAS_MD_MD_Upd:
		public const string WAS_MD_MD_Upd = "ErridnInventory.WAS_MD_MD_Upd"; //// //WAS_MD_MD_Upd

		// WAS_MD_MD_InActive:
		public const string WAS_MD_MD_InActive = "ErridnInventory.WAS_MD_MD_InActive"; //// //WAS_MD_MD_InActive

		// WAS_MD_MD_Delete:
		public const string WAS_MD_MD_Delete = "ErridnInventory.WAS_MD_MD_Delete"; //// //WAS_MD_MD_Delete

		// MD_MD_Approve:
		public const string MD_MD_Approve = "ErridnInventory.MD_MD_Approve"; //// //MD_MD_Approve
		public const string MD_MD_Approve_AssetDtlNotFound = "ErridnInventory.MD_MD_Approve_AssetDtlNotFound"; //// //MD_MD_Approve_AssetDtlNotFound

		// MD_MD_UpdAppr:
		public const string MD_MD_UpdAppr = "ErridnInventory.MD_MD_UpdAppr"; //// //MD_MD_UpdAppr
		public const string MD_MD_UpdAppr_InvalidBGStatus = "ErridnInventory.MD_MD_UpdAppr_InvalidBGStatus"; //// //MD_MD_UpdAppr_InvalidBGStatus
		public const string MD_MD_UpdAppr_InvalidValue = "ErridnInventory.MD_MD_UpdAppr_InvalidValue"; //// //MD_MD_UpdAppr_InvalidValue
		public const string MD_MD_UpdAppr_InvalidSignedDate = "ErridnInventory.MD_MD_UpdAppr_InvalidSignedDate"; //// //MD_MD_UpdAppr_InvalidSignedDate
		public const string MD_MD_UpdAppr_InvalidSellerCode = "ErridnInventory.MD_MD_UpdAppr_InvalidSellerCode"; //// //MD_MD_UpdAppr_InvalidSellerCode
		public const string MD_MD_UpdAppr_Input_MD_MDDtlTblNotFound = "ErridnInventory.MD_MD_UpdAppr_Input_MD_MDDtlTblNotFound"; //// //MD_MD_UpdAppr_Input_MD_MDDtlTblNotFound		
		public const string MD_MD_UpdAppr_Input_MD_MDDtlTblInvalid = "ErridnInventory.MD_MD_UpdAppr_Input_MD_MDDtlTblInvalid"; //// //MD_MD_UpdAppr_Input_MD_MDDtlTblInvalid		
		#endregion

		#region // Mst_BizRef:
		public const string Mst_BizRef_CheckBiz_InvalidBizType = "ErridnInventory.Mst_BizRef_CheckBiz_InvalidBizType"; //// //Mst_BizRef_CheckBiz_InvalidBizType

		public const string Mst_BizRef_CheckBiz_InvalidBizRefNo = "ErridnInventory.Mst_BizRef_CheckBiz_InvalidBizRefNo"; //// //Mst_BizRef_CheckBiz_InvalidBizRefNo

		public const string Mst_BizRef_CheckBiz_InvalidPrjCode = "ErridnInventory.Mst_BizRef_CheckBiz_InvalidPrjCode"; //// //Mst_BizRef_CheckBiz_InvalidPrjCode
		#endregion

		#region // MD_MDCreateBatch:
		// MD_MDCreateBatch_CheckDB:
		public const string MD_MDCreateBatch_CheckDB_MDCreateBatchNotFound = "ErridnInventory.MD_MDCreateBatch_CheckDB_MDCreateBatchNotFound"; //// //MD_MDCreateBatch_CheckDB_MDCreateBatchNotFound
		public const string MD_MDCreateBatch_CheckDB_MDCreateBatchExist = "ErridnInventory.MD_MDCreateBatch_CheckDB_MDCreateBatchExist"; //// //MD_MDCreateBatch_CheckDB_MDCreateBatchExist
		public const string MD_MDCreateBatch_CheckDB_StatusNotMatched = "ErridnInventory.MD_MDCreateBatch_CheckDB_StatusNotMatched"; //// //MD_MDCreateBatch_CheckDB_StatusNotMatched

		// MD_MDCreateBatch_Get:
		public const string MD_MDCreateBatch_Get = "ErridnInventory.MD_MDCreateBatch_Get"; //// //MD_MDCreateBatch_Get

		// MD_MDCreateBatch_Save:
		public const string MD_MDCreateBatch_Save = "ErridnInventory.MD_MDCreateBatch_Save"; //// //MD_MDCreateBatch_Save
		public const string MD_MDCreateBatch_Save_InvalidMDCrtBatchStatus = "ErridnInventory.MD_MDCreateBatch_Save_InvalidMDCrtBatchStatus"; //// //MD_MDCreateBatch_Save_InvalidMDCrtBatchStatus
		public const string MD_MDCreateBatch_Save_InvalidValue = "ErridnInventory.MD_MDCreateBatch_Save_InvalidValue"; //// //MD_MDCreateBatch_Save_InvalidValue
		public const string MD_MDCreateBatch_Save_InvalidSignedDate = "ErridnInventory.MD_MDCreateBatch_Save_InvalidSignedDate"; //// //MD_MDCreateBatch_Save_InvalidSignedDate
		public const string MD_MDCreateBatch_Save_InvalidSellerCode = "ErridnInventory.MD_MDCreateBatch_Save_InvalidSellerCode"; //// //MD_MDCreateBatch_Save_InvalidSellerCode
		public const string MD_MDCreateBatch_Save_Input_MD_MDCreateBatchDtlTblNotFound = "ErridnInventory.MD_MDCreateBatch_Save_Input_MD_MDCreateBatchDtlTblNotFound"; //// //MD_MDCreateBatch_Save_Input_MD_MDCreateBatchDtlTblNotFound		
		public const string MD_MDCreateBatch_Save_Input_MD_MDCreateBatchDtlTblInvalid = "ErridnInventory.MD_MDCreateBatch_Save_Input_MD_MDCreateBatchDtlTblInvalid"; //// //MD_MDCreateBatch_Save_Input_MD_MDCreateBatchDtlTblInvalid		

		// MD_MDCreateBatch_Approve:
		public const string MD_MDCreateBatch_Approve = "ErridnInventory.MD_MDCreateBatch_Approve"; //// //MD_MDCreateBatch_Approve
		public const string MD_MDCreateBatch_Approve_MDCreateBatchDtlNotFound = "ErridnInventory.MD_MDCreateBatch_Approve_MDCreateBatchDtlNotFound"; //// //MD_MDCreateBatch_Approve_MDCreateBatchDtlNotFound

		// MD_MDCreateBatch_UpdAppr:
		public const string MD_MDCreateBatch_UpdAppr = "ErridnInventory.MD_MDCreateBatch_UpdAppr"; //// //MD_MDCreateBatch_UpdAppr
		public const string MD_MDCreateBatch_UpdAppr_InvalidMDCrtBatchStatus = "ErridnInventory.MD_MDCreateBatch_UpdAppr_InvalidMDCrtBatchStatus"; //// //MD_MDCreateBatch_UpdAppr_InvalidMDCrtBatchStatus
		public const string MD_MDCreateBatch_UpdAppr_InvalidValue = "ErridnInventory.MD_MDCreateBatch_UpdAppr_InvalidValue"; //// //MD_MDCreateBatch_UpdAppr_InvalidValue
		public const string MD_MDCreateBatch_UpdAppr_InvalidSignedDate = "ErridnInventory.MD_MDCreateBatch_UpdAppr_InvalidSignedDate"; //// //MD_MDCreateBatch_UpdAppr_InvalidSignedDate
		public const string MD_MDCreateBatch_UpdAppr_InvalidSellerCode = "ErridnInventory.MD_MDCreateBatch_UpdAppr_InvalidSellerCode"; //// //MD_MDCreateBatch_UpdAppr_InvalidSellerCode
		public const string MD_MDCreateBatch_UpdAppr_Input_MD_MDCreateBatchDtlTblNotFound = "ErridnInventory.MD_MDCreateBatch_UpdAppr_Input_MD_MDCreateBatchDtlTblNotFound"; //// //MD_MDCreateBatch_UpdAppr_Input_MD_MDCreateBatchDtlTblNotFound		
		public const string MD_MDCreateBatch_UpdAppr_Input_MD_MDCreateBatchDtlTblInvalid = "ErridnInventory.MD_MDCreateBatch_UpdAppr_Input_MD_MDCreateBatchDtlTblInvalid"; //// //MD_MDCreateBatch_UpdAppr_Input_MD_MDCreateBatchDtlTblInvalid		
		#endregion

		#region // MD_MDAutoBatch:
		// MD_MDAutoBatch_CheckDB:
		public const string MD_MDAutoBatch_CheckDB_MDAutoBatchNotFound = "ErridnInventory.MD_MDAutoBatch_CheckDB_MDAutoBatchNotFound"; //// //MD_MDAutoBatch_CheckDB_MDAutoBatchNotFound
		public const string MD_MDAutoBatch_CheckDB_MDAutoBatchExist = "ErridnInventory.MD_MDAutoBatch_CheckDB_MDAutoBatchExist"; //// //MD_MDAutoBatch_CheckDB_MDAutoBatchExist
		public const string MD_MDAutoBatch_CheckDB_StatusNotMatched = "ErridnInventory.MD_MDAutoBatch_CheckDB_StatusNotMatched"; //// //MD_MDAutoBatch_CheckDB_StatusNotMatched

		// MD_MDAutoBatch_Get:
		public const string MD_MDAutoBatch_Get = "ErridnInventory.MD_MDAutoBatch_Get"; //// //MD_MDAutoBatch_Get

		// MD_MDAutoBatch_Save:
		public const string MD_MDAutoBatch_Save = "ErridnInventory.MD_MDAutoBatch_Save"; //// //MD_MDAutoBatch_Save
		public const string MD_MDAutoBatch_Save_InvalidMDAutoBatchStatus = "ErridnInventory.MD_MDAutoBatch_Save_InvalidMDAutoBatchStatus"; //// //MD_MDAutoBatch_Save_InvalidMDAutoBatchStatus
		public const string MD_MDAutoBatch_Save_InvalidValue = "ErridnInventory.MD_MDAutoBatch_Save_InvalidValue"; //// //MD_MDAutoBatch_Save_InvalidValue
		public const string MD_MDAutoBatch_Save_InvalidSignedDate = "ErridnInventory.MD_MDAutoBatch_Save_InvalidSignedDate"; //// //MD_MDAutoBatch_Save_InvalidSignedDate
		public const string MD_MDAutoBatch_Save_InvalidSellerCode = "ErridnInventory.MD_MDAutoBatch_Save_InvalidSellerCode"; //// //MD_MDAutoBatch_Save_InvalidSellerCode
		public const string MD_MDAutoBatch_Save_Input_MD_MDAutoBatchDtlTblNotFound = "ErridnInventory.MD_MDAutoBatch_Save_Input_MD_MDAutoBatchDtlTblNotFound"; //// //MD_MDAutoBatch_Save_Input_MD_MDAutoBatchDtlTblNotFound		
		public const string MD_MDAutoBatch_Save_Input_MD_MDAutoBatchDtlTblInvalid = "ErridnInventory.MD_MDAutoBatch_Save_Input_MD_MDAutoBatchDtlTblInvalid"; //// //MD_MDAutoBatch_Save_Input_MD_MDAutoBatchDtlTblInvalid		

		// MD_MDAutoBatch_Approve:
		public const string MD_MDAutoBatch_Approve = "ErridnInventory.MD_MDAutoBatch_Approve"; //// //MD_MDAutoBatch_Approve
		public const string MD_MDAutoBatch_Approve_MDCreateBatchDtlNotFound = "ErridnInventory.MD_MDAutoBatch_Approve_MDCreateBatchDtlNotFound"; //// //MD_MDAutoBatch_Approve_MDCreateBatchDtlNotFound

		// MD_MDAutoBatch_UpdAppr:
		public const string MD_MDAutoBatch_UpdAppr = "ErridnInventory.MD_MDAutoBatch_UpdAppr"; //// //MD_MDAutoBatch_UpdAppr
		public const string MD_MDAutoBatch_UpdAppr_InvalidMDCrtBatchStatus = "ErridnInventory.MD_MDAutoBatch_UpdAppr_InvalidMDCrtBatchStatus"; //// //MD_MDAutoBatch_UpdAppr_InvalidMDCrtBatchStatus
		public const string MD_MDAutoBatch_UpdAppr_InvalidValue = "ErridnInventory.MD_MDAutoBatch_UpdAppr_InvalidValue"; //// //MD_MDAutoBatch_UpdAppr_InvalidValue
		public const string MD_MDAutoBatch_UpdAppr_InvalidSignedDate = "ErridnInventory.MD_MDAutoBatch_UpdAppr_InvalidSignedDate"; //// //MD_MDAutoBatch_UpdAppr_InvalidSignedDate
		public const string MD_MDAutoBatch_UpdAppr_InvalidSellerCode = "ErridnInventory.MD_MDAutoBatch_UpdAppr_InvalidSellerCode"; //// //MD_MDAutoBatch_UpdAppr_InvalidSellerCode
		public const string MD_MDAutoBatch_UpdAppr_Input_MD_MDAutoBatchDtlTblNotFound = "ErridnInventory.MD_MDAutoBatch_UpdAppr_Input_MD_MDAutoBatchDtlTblNotFound"; //// //MD_MDAutoBatch_UpdAppr_Input_MD_MDAutoBatchDtlTblNotFound		
		public const string MD_MDAutoBatch_UpdAppr_Input_MD_MDAutoBatchDtlTblInvalid = "ErridnInventory.MD_MDAutoBatch_UpdAppr_Input_MD_MDAutoBatchDtlTblInvalid"; //// //MD_MDAutoBatch_UpdAppr_Input_MD_MDAutoBatchDtlTblInvalid		
		#endregion

		#region // License:
		// Lic_Session_Get:
		public const string Lic_Session_Get = "Erridn.Skycic.Inventory.Lic_Session_Get"; //// //Lic_Session_Get

		// Lic_Session_Del:
		public const string Lic_Session_Del = "Erridn.Skycic.Inventory.Lic_Session_Del"; //// //Lic_Session_Del

		#endregion

		#region // Seq_Common:
		// Seq_Common_MyGet:
		public const string Seq_Common_MyGet_InvalidSequenceType = "Erridn.Skycic.Inventory.Seq_Common_MyGet_InvalidSequenceType"; //// //Seq_Common_MyGet_InvalidSequenceType

		// Seq_Common_Get:
		public const string Seq_Common_Get = "Erridn.Skycic.Inventory.Seq_Common_Get"; //// //Seq_Common_Get

		// Seq_Common_Get_ForCtrDpsCode:
		public const string Seq_Common_Get_ForCtrDpsCode = "Erridn.Skycic.Inventory.Seq_Common_Get_ForCtrDpsCode"; //// //Seq_Common_Get_ForCtrDpsCode

		// Seq_Common_Get_TranNo:
		public const string Seq_Common_Get_TranNo = "Erridn.Skycic.Inventory.Seq_Common_Get_TranNo"; //// //Seq_Common_Get_TranNo

		// WAS_Seq_Common_Get_TranNo:
		public const string WAS_Seq_Common_Get_TranNo = "Erridn.Skycic.Inventory.WAS_Seq_Common_Get_TranNo"; //// //WAS_Seq_Common_Get_TranNo

		#endregion

		#region // Sys_Group:
		// Sys_Group_CheckDB:
		public const string Sys_Group_CheckDB_GroupCodeNotFound = "ErridnInventory.Sys_Group_CheckDB_GroupCodeNotFound"; //// //Sys_Group_CheckDB_GroupCodeNotFound
		public const string Sys_Group_CheckDB_GroupCodeExist = "ErridnInventory.Sys_Group_CheckDB_GroupCodeExist"; //// //Sys_Group_CheckDB_GroupCodeExist
		public const string Sys_Group_CheckDB_FlagActiveNotMatched = "ErridnInventory.Sys_Group_CheckDB_FlagActiveNotMatched"; //// //Sys_Group_CheckDB_FlagActiveNotMatched
		public const string Sys_Group_CheckDB_FlagPublicNotMatched = "ErridnInventory.Sys_Group_CheckDB_FlagPublicNotMatched"; //// //Sys_Group_CheckDB_FlagPublicNotMatched

		// Sys_Group_Get:
		public const string Sys_Group_Get = "ErridnInventory.Sys_Group_Get"; //// //Sys_Group_Get

		// WAS_Sys_Group_Get:
		public const string WAS_Sys_Group_Get = "ErridnInventory.WAS_Sys_Group_Get"; //// //WAS_Sys_Group_Get

		// Sys_Group_Create:
		public const string Sys_Group_Create = "ErridnInventory.Sys_Group_Create"; //// //Sys_Group_Create
		public const string Sys_Group_Create_InvalidGroupCode = "ErridnInventory.Sys_Group_Create_InvalidGroupCode"; //// //Sys_Group_Create_InvalidGroupCode
		public const string Sys_Group_Create_InvalidGroupName = "ErridnInventory.Sys_Group_Create_InvalidGroupName"; //// //Sys_Group_Create_InvalidGroupName

		// WAS_Sys_Group_Create:
		public const string WAS_Sys_Group_Create = "ErridnInventory.WAS_Sys_Group_Create"; //// //WAS_Sys_Group_Create

		// Sys_Group_Update:
		public const string Sys_Group_Update = "ErridnInventory.Sys_Group_Update"; //// //Sys_Group_Update
		public const string Sys_Group_Update_InvalidGroupCode = "ErridnInventory.Sys_Group_Update_InvalidGroupCode"; //// //Sys_Group_Update_InvalidGroupCode
		public const string Sys_Group_Update_InvalidGroupName = "ErridnInventory.Sys_Group_Update_InvalidGroupName"; //// //Sys_Group_Update_InvalidGroupName

		// WAS_Sys_Group_Update:
		public const string WAS_Sys_Group_Update = "ErridnInventory.WAS_Sys_Group_Update"; //// //WAS_Sys_Group_Update

		// Sys_Group_Delete:
		public const string Sys_Group_Delete = "ErridnInventory.Sys_Group_Delete"; //// //Sys_Group_Delete

		// WAS_Sys_Group_Delete:
		public const string WAS_Sys_Group_Delete = "ErridnInventory.WAS_Sys_Group_Delete"; //// //WAS_Sys_Group_Delete

		#endregion

		#region // Sys_User:
		// Sys_User_CheckDB:
		public const string Sys_User_CheckDB_UserCodeNotFound = "ErridnInventory.Sys_User_CheckDB_UserCodeNotFound"; //// //Sys_User_CheckDB_UserCodeNotFound
		public const string Sys_User_CheckDB_UserCodeExist = "ErridnInventory.Sys_User_CheckDB_UserCodeExist"; //// //Sys_User_CheckDB_UserCodeExist
		public const string Sys_User_CheckDB_FlagActiveNotMatched = "ErridnInventory.Sys_User_CheckDB_FlagActiveNotMatched"; //// //Sys_User_CheckDB_FlagActiveNotMatched
		public const string Sys_User_CheckDB_FlagSysAdminNotMatched = "ErridnInventory.Sys_User_CheckDB_FlagSysAdminNotMatched"; //// //Sys_User_CheckDB_FlagSysAdminNotMatched
		public const string Sys_User_CheckDB_BizInvalidUserAbility = "ErridnInventory.Sys_User_CheckDB_BizInvalidUserAbility"; //// //Sys_User_CheckDB_BizInvalidUserAbility

		// Sys_User_BizInvalidUserAbility:
		public const string Sys_User_BizInvalidUserAbility = "ErridnInventory.Sys_User_BizInvalidUserAbility"; //// //Sys_User_BizInvalidUserAbility

		// Sys_User_ChangePassword:
		public const string Sys_User_ChangePassword = "ErridnInventory.Sys_User_ChangePassword"; //// //Sys_User_ChangePassword
		public const string Sys_User_ChangePassword_InvalidPasswordOld = "ErridnInventory.Sys_User_ChangePassword_InvalidPasswordOld"; //// //Sys_User_ChangePassword_InvalidPasswordOld
		public const string Sys_User_ChangePassword_InvalidPasswordNew = "ErridnInventory.Sys_User_ChangePassword_InvalidPasswordNew"; //// //Sys_User_ChangePassword_InvalidPasswordNew

		// Sys_User_ResetPassword:
		public const string Sys_User_ResetPassword = "ErridnInventory.Sys_User_ResetPassword"; //// //Sys_User_ResetPassword

		// Sys_User_GetForCurrentUser:
		public const string Sys_User_GetForCurrentUser = "ErridnInventory.Sys_User_GetForCurrentUser"; //// //Sys_User_GetForCurrentUser

		// Sys_User_Login:
		public const string Sys_User_Login = "ErridnInventory.Sys_User_Login"; //// //Sys_User_Login
		public const string Sys_User_Login_InvalidLDAP = "ErridnInventory.Sys_User_Login_InvalidLDAP"; //// //Sys_User_Login_InvalidLDAP
		public const string Sys_User_Login_Checking = "ErridnInventory.Sys_User_Login_Checking"; //// //Sys_User_Login_Checking
		public const string Sys_User_Login_InvalidPassword = "ErridnInventory.Sys_User_Login_InvalidPassword"; //// //Sys_User_Login_InvalidPassword

		// Sys_User_CheckAuthentication:
		public const string Sys_User_CheckAuthentication = "ErridnInventory.Sys_User_CheckAuthentication"; //// //Sys_User_CheckAuthentication
		public const string Sys_User_CheckAuthentication_InvalidPassword = "ErridnInventory.Sys_User_CheckAuthentication_InvalidPassword"; //// //Sys_User_CheckAuthentication_InvalidPassword

		// Sys_User_Get:
		public const string Sys_User_Get = "ErridnInventory.Sys_User_Get"; //// //Sys_User_Get
		public const string Sys_User_Get_01 = "ErridnInventory.Sys_User_Get_01"; //// //Sys_User_Get_01

		// WA_Sys_User_Get:
		public const string WA_Sys_User_Get = "ErridnInventory.WA_Sys_User_Get"; //// //WA_Sys_User_Get

		// WAS_Sys_User_Get:
		public const string WAS_Sys_User_Get = "ErridnInventory.WAS_Sys_User_Get"; //// //WAS_Sys_User_Get

		// WAS_Sys_User_GetForCurrentUser:
		public const string WAS_Sys_User_GetForCurrentUser = "ErridnInventory.WAS_Sys_User_GetForCurrentUser"; //// //WAS_Sys_User_GetForCurrentUser

		// WAS_Sys_User_Create:
		public const string WAS_Sys_User_Create = "ErridnInventory.WAS_Sys_User_Create"; //// //WAS_Sys_User_Create

		// WAS_Sys_User_ChangePassword:
		public const string WAS_Sys_User_ChangePassword = "ErridnInventory.WAS_Sys_User_ChangePassword"; //// //WAS_Sys_User_ChangePassword

		// WAS_Sys_User_Update:
		public const string WAS_Sys_User_Update = "ErridnInventory.WAS_Sys_User_Update"; //// //WAS_Sys_User_Update

		// WAS_Sys_User_Delete:
		public const string WAS_Sys_User_Delete = "ErridnInventory.WAS_Sys_User_Delete"; //// //WAS_Sys_User_Delete

		// WAS_Sys_User_Login:
		public const string WAS_Sys_User_Login = "ErridnInventory.WAS_Sys_User_Login"; //// //WAS_Sys_User_Login

		// Sys_User_Logout:
		public const string Sys_User_Logout = "ErridnInventory.Sys_User_Logout"; //// //Sys_User_Logout

		// Sys_User_GetByDB:
		public const string Sys_User_GetByDB = "ErridnInventory.Sys_User_GetByDB"; //// //Sys_User_GetByDB

		// Sys_User_Activate:
		public const string Sys_User_Activate = "ErridnInventory.Sys_User_Activate"; //// //Sys_User_Activate

		// WAS_Sys_User_Activate:
		public const string WAS_Sys_User_Activate = "ErridnInventory.WAS_Sys_User_Activate"; //// //WAS_Sys_User_Activate

		// Sys_User_Create:
		public const string Sys_User_Create = "ErridnInventory.Sys_User_Create"; //// //Sys_User_Create
		public const string Sys_User_Create_InvalidUserCode = "ErridnInventory.Sys_User_Create_InvalidUserCode"; //// //Sys_User_Create_InvalidUserCode
		public const string Sys_User_Create_InvalidDBCode = "ErridnInventory.Sys_User_Create_InvalidDBCode"; //// //Sys_User_Create_InvalidDBCode
		public const string Sys_User_Create_InvalidAreaCode = "ErridnInventory.Sys_User_Create_InvalidAreaCode"; //// //Sys_User_Create_InvalidAreaCode
		public const string Sys_User_Create_InvalidUserNick = "ErridnInventory.Sys_User_Create_InvalidUserNick"; //// //Sys_User_Create_InvalidUserNick
		public const string Sys_User_Create_InvalidUserName = "ErridnInventory.Sys_User_Create_InvalidUserName"; //// //Sys_User_Create_InvalidUserName
		public const string Sys_User_Create_InvalidUserPassword = "ErridnInventory.Sys_User_Create_InvalidUserPassword"; //// //Sys_User_Create_InvalidUserPassword
        public const string Sys_User_Create_InvalidFlagDLAdmin = "ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin"; //// //Sys_User_Create_InvalidFlagDLAdmin
        public const string Sys_User_Create_InvalidFlagSysAdmin = "ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin"; //// //Sys_User_Create_InvalidFlagSysAdmin
		public const string Sys_User_Create_InvalidFlagDBAdmin = "ErridnInventory.Sys_User_Create_InvalidFlagDBAdmin"; //// //Sys_User_Create_InvalidFlagDBAdmin
        public const string Sys_User_Create_NNTNotAdmin = "ErridnInventory.Sys_User_Create_NNTNotAdmin"; //// // Sys_User_Create_NNTNotAdmin
		public const string Sys_User_Create_InosRole_Invalid = "ErridnInventory.Sys_User_Create_InosRole_Invalid"; //// // Sys_User_Create_InosRole_Invalid

		// Sys_User_Update:
		public const string Sys_User_Update = "ErridnInventory.Sys_User_Update"; //// //Sys_User_Update
		public const string Sys_User_Update_InvalidUserCode = "ErridnInventory.Sys_User_Update_InvalidUserCode"; //// //Sys_User_Update_InvalidUserCode
		public const string Sys_User_Update_InvalidDBCode = "ErridnInventory.Sys_User_Update_InvalidDBCode"; //// //Sys_User_Update_InvalidDBCode
		public const string Sys_User_Update_InvalidAreaCode = "ErridnInventory.Sys_User_Update_InvalidAreaCode"; //// //Sys_User_Update_InvalidAreaCode
		public const string Sys_User_Update_InvalidUserName = "ErridnInventory.Sys_User_Update_InvalidUserName"; //// //Sys_User_Update_InvalidUserName
		public const string Sys_User_Update_InvalidUserPassword = "ErridnInventory.Sys_User_Update_InvalidUserPassword"; //// //Sys_User_Update_InvalidUserPassword
		public const string Sys_User_Update_InvalidFlagSysAdmin = "ErridnInventory.Sys_User_Update_InvalidFlagSysAdmin"; //// //Sys_User_Update_InvalidFlagSysAdmin
		public const string Sys_User_Update_InvalidFlagDBAdmin = "ErridnInventory.Sys_User_Update_InvalidFlagDBAdmin"; //// //Sys_User_Update_InvalidFlagDBAdmin

		// Sys_User_Delete:
		public const string Sys_User_Delete = "ErridnInventory.Sys_User_Delete"; //// //Sys_User_Delete

		#endregion

		#region // Sys_UserInGroup:
		// Sys_UserInGroup_Save:
		public const string Sys_UserInGroup_Save = "ErridnInventory.Sys_UserInGroup_Save"; //// //Sys_UserInGroup_Save
		public const string Sys_UserInGroup_Save_InputTblDtlNotFound = "ErridnInventory.Sys_UserInGroup_Save_InputTblDtlNotFound"; //// //Sys_UserInGroup_Save_InputTblDtlNotFound

		// WAS_Sys_UserInGroup_Save:
		public const string WAS_Sys_UserInGroup_Save = "ErridnInventory.WAS_Sys_UserInGroup_Save"; //// //WAS_Sys_UserInGroup_Save

		#endregion

		#region // Acc_AccMapUser:
		// Acc_AccMapUser_Save:
		public const string Acc_AccMapUser_Save = "ErridnInventory.Acc_AccMapUser_Save"; //// //Acc_AccMapUser_Save
		#endregion

		#region // Sys_Access:
		// Sys_Access_CheckDeny:
		public const string Sys_Access_CheckDeny = "Sys_Access_CheckDeny"; //// //Sys_Access_CheckDeny
		// Sys_ViewAbility_Deny:
		public const string Sys_ViewAbility_Deny = "Sys_ViewAbility_Deny"; //// //Sys_ViewAbility_Deny
		public const string Sys_ViewAbility_NotExactUser = "Sys_ViewAbility_NotExactUser"; //// //Sys_ViewAbility_NotExactUser

		// Sys_Access_Get:
		public const string Sys_Access_Get = "ErridnInventory.Sys_Access_Get"; //// //Sys_Access_Get

		// WAS_Sys_Access_Get:
		public const string WAS_Sys_Access_Get = "ErridnInventory.WAS_Sys_Access_Get"; //// //WAS_Sys_Access_Get

		// Sys_Access_Save:
		public const string Sys_Access_Save = "ErridnInventory.Sys_Access_Save"; //// //Sys_Access_Save
		public const string Sys_Access_Save_InputTblDtlNotFound = "ErridnInventory.Sys_Access_Save_InputTblDtlNotFound"; //// //Sys_Access_Save_InputTblDtlNotFound

		// WAS_Sys_Access_Save:
		public const string WAS_Sys_Access_Save = "ErridnInventory.WAS_Sys_Access_Save"; //// //WAS_Sys_Access_Save
		#endregion

		#region // Sys_Object:
		// Sys_Object_CheckDB:
		public const string Sys_Object_CheckDB_ObjectCodeNotFound = "ErridnInventory.Sys_Object_CheckDB_ObjectCodeNotFound"; //// //Sys_Object_CheckDB_ObjectCodeNotFound
		public const string Sys_Object_CheckDB_ObjectCodeExist = "ErridnInventory.Sys_Object_CheckDB_ObjectCodeExist"; //// //Sys_Object_CheckDB_ObjectCodeExist
		public const string Sys_Object_CheckDB_FlagActiveNotMatched = "ErridnInventory.Sys_Object_CheckDB_FlagActiveNotMatched"; //// //Sys_Object_CheckDB_FlagActiveNotMatched

		// Sys_Object_Get:
		public const string Sys_Object_Get = "ErridnInventory.Sys_Object_Get"; //// //Sys_Object_Get

		// WAS_Sys_Object_Get:
		public const string WAS_Sys_Object_Get = "ErridnInventory.WAS_Sys_Object_Get"; //// //WAS_Sys_Object_Get
        #endregion

        #region // Mst_MoneyBox:
        // Mst_MoneyBox_CheckDB:
        public const string Mst_MoneyBox_CheckDB_MoneyBoxNotFound = "ErridnInventory.Mst_MoneyBox_CheckDB_MoneyBoxNotFound"; //// //Mst_MoneyBox_CheckDB_MoneyBoxNotFound
        public const string Mst_MoneyBox_CheckDB_MoneyBoxExist = "ErridnInventory.Mst_MoneyBox_CheckDB_MoneyBoxExist"; //// //Mst_MoneyBox_CheckDB_MoneyBoxExist
        public const string Mst_MoneyBox_CheckDB_FlagActiveNotMatched = "ErridnInventory.Mst_MoneyBox_CheckDB_FlagActiveNotMatched"; //// //Mst_MoneyBox_CheckDB_FlagActiveNotMatched

        // Mst_MoneyBox_Get:
        public const string Mst_MoneyBox_Get = "ErridnInventory.Mst_MoneyBox_Get"; //// //Mst_MoneyBox_Get

        // WAS_Mst_MoneyBox_Get:
        public const string WAS_Mst_MoneyBox_Get = "ErridnInventory.WAS_Mst_MoneyBox_Get"; //// //WAS_Mst_MoneyBox_Get

        // Mst_MoneyBox_Create:
        public const string Mst_MoneyBox_Create = "ErridnInventory.Mst_MoneyBox_Create"; //// //Mst_MoneyBox_Create
        public const string Mst_MoneyBox_Create_InvalidMoneyBoxCode = "ErridnInventory.Mst_MoneyBox_Create_InvalidMoneyBoxCode"; //// //Mst_MoneyBox_Create_InvalidMoneyBoxCode
        public const string Mst_MoneyBox_Create_InvalidMoneyBoxName = "ErridnInventory.Mst_MoneyBox_Create_InvalidMoneyBoxName"; //// //Mst_MoneyBox_Create_InvalidMoneyBoxName

        // WAS_Mst_MoneyBox_Create:
        public const string WAS_Mst_MoneyBox_Create = "ErridnInventory.WAS_Mst_MoneyBox_Create"; //// //WAS_Mst_MoneyBox_Create

        // Mst_MoneyBox_Update:
        public const string Mst_MoneyBox_Update = "ErridnInventory.Mst_MoneyBox_Update"; //// //Mst_MoneyBox_Update
        public const string Mst_MoneyBox_Update_InvalidMoneyBoxName = "ErridnInventory.Mst_MoneyBox_Update_InvalidMoneyBoxName"; //// //Mst_MoneyBox_Update_InvalidMoneyBoxName

        // WAS_Mst_MoneyBox_Update:
        public const string WAS_Mst_MoneyBox_Update = "ErridnInventory.WAS_Mst_MoneyBox_Update"; //// //WAS_Mst_MoneyBox_Update

        // Mst_MoneyBox_Delete:
        public const string Mst_MoneyBox_Delete = "ErridnInventory.Mst_MoneyBox_Delete"; //// //Mst_MoneyBox_Delete

        // WAS_Mst_MoneyBox_Delete:
        public const string WAS_Mst_MoneyBox_Delete = "ErridnInventory.WAS_Mst_MoneyBox_Delete"; //// //WAS_Mst_MoneyBox_Delete
        #endregion

        #region // Sys_Solution:
        // Sys_Solution_CheckDB:
        public const string Sys_Solution_CheckDB_SolutionNotFound = "ErridnInventory.Sys_Solution_CheckDB_SolutionNotFound"; //// //Sys_Solution_CheckDB_SolutionNotFound
        public const string Sys_Solution_CheckDB_SolutionExist = "ErridnInventory.Sys_Solution_CheckDB_SolutionExist"; //// //Sys_Solution_CheckDB_SolutionExist
        public const string Sys_Solution_CheckDB_FlagActiveNotMatched = "ErridnInventory.Sys_Solution_CheckDB_FlagActiveNotMatched"; //// //Sys_Solution_CheckDB_FlagActiveNotMatched

        // Sys_Solution_Get:
        public const string Sys_Solution_Get = "ErridnInventory.Sys_Solution_Get"; //// //Sys_Solution_Get

        // WAS_Sys_Solution_Get:
        public const string WAS_Sys_Solution_Get = "ErridnInventory.WAS_Sys_Solution_Get"; //// //WAS_Sys_Solution_Get
        #endregion

        #region // Sys_Modules:
        // Sys_Modules_CheckDB:
        public const string Sys_Modules_CheckDB_ModulesNotFound = "ErridnInventory.Sys_Modules_CheckDB_ModulesNotFound"; //// //Sys_Modules_CheckDB_ModulesNotFound
        public const string Sys_Modules_CheckDB_ModulesExist = "ErridnInventory.Sys_Modules_CheckDB_ModulesExist"; //// //Sys_Modules_CheckDB_ModulesExist
        public const string Sys_Modules_CheckDB_FlagActiveNotMatched = "ErridnInventory.Sys_Modules_CheckDB_FlagActiveNotMatched"; //// //Sys_Modules_CheckDB_FlagActiveNotMatched

        // Sys_Modules_Get:
        public const string Sys_Modules_Get = "ErridnInventory.Sys_Modules_Get"; //// //Sys_Modules_Get

        // WAS_Sys_Modules_Get:
        public const string WAS_Sys_Modules_Get = "ErridnInventory.WAS_Sys_Modules_Get"; //// //WAS_Sys_Modules_Get

        // Sys_Modules_Create:
        public const string Sys_Modules_Create = "ErridnInventory.Sys_Modules_Create"; //// //Sys_Modules_Create
        public const string Sys_Modules_Create_InvalidModuleCode = "ErridnInventory.Sys_Modules_Create_InvalidModuleCode"; //// //Sys_Modules_Create_InvalidModuleCode
        public const string Sys_Modules_Create_InvalidModuleName = "ErridnInventory.Sys_Modules_Create_InvalidModuleName"; //// //Sys_Modules_Create_InvalidModuleName

        // WAS_Sys_Modules_Create:
        public const string WAS_Sys_Modules_Create = "ErridnInventory.WAS_Sys_Modules_Create"; //// //WAS_Sys_Modules_Create

        // Sys_Modules_Update:
        public const string Sys_Modules_Update = "ErridnInventory.Sys_Modules_Update"; //// //Sys_Modules_Update                
        public const string Sys_Modules_Update_InvalidModuleName = "ErridnInventory.Sys_Modules_Update_InvalidModuleName"; //// //Sys_Modules_Update_InvalidModuleName                

        // WAS_Sys_Modules_Update:
        public const string WAS_Sys_Modules_Update = "ErridnInventory.WAS_Sys_Modules_Update"; //// //WAS_Sys_Modules_Update

        // Sys_Modules_Delete:
        public const string Sys_Modules_Delete = "ErridnInventory.Sys_Modules_Delete"; //// Mã lỗi: Sys_Modules_Delete ////

        // WAS_Sys_Modules_Delete:
        public const string WAS_Sys_Modules_Delete = "ErridnInventory.WAS_Sys_Modules_Delete"; //// //WAS_Sys_Modules_Delete

        //RptSv:
        public const string WAS_RptSv_Sys_Object_Get = "ErridnInventory.WAS_RptSv_Sys_Object_Get"; //// //WAS_RptSv_Sys_Object_Get
        public const string WAS_RptSv_Sys_Modules_Create = "ErridnInventory.WAS_RptSv_Sys_Modules_Create"; //// //WAS_RptSv_Sys_Modules_Create
        public const string WAS_RptSv_Sys_Modules_Update = "ErridnInventory.WAS_RptSv_Sys_Modules_Update"; //// //WAS_RptSv_Sys_Modules_Update
        public const string WAS_RptSv_Sys_Modules_Delete = "ErridnInventory.WAS_RptSv_Sys_Modules_Delete"; //// //WAS_RptSv_Sys_Modules_Delete
        
        #endregion

        #region // Sys_ObjectInModules:
        // Sys_ObjectInModules_Get:
        public const string Sys_ObjectInModules_Get = "ErridnInventory.Sys_ObjectInModules_Get"; //// //Sys_ObjectInModules_Get

        // WAS_Sys_ObjectInModules_Get:
        public const string WAS_Sys_ObjectInModules_Get = "ErridnInventory.WAS_Sys_ObjectInModules_Get"; //// //WAS_Sys_ObjectInModules_Get

        // Sys_ObjectInModules_Save:
        public const string Sys_ObjectInModules_Save = "ErridnInventory.Sys_ObjectInModules_Save"; //// //Sys_ObjectInModules_Save
        public const string Sys_ObjectInModules_Save_InputTblDtlNotFound = "ErridnInventory.Sys_ObjectInModules_Save_InputTblDtlNotFound"; //// //Sys_ObjectInModules_Save_InputTblDtlNotFound

        // WAS_Sys_ObjectInModules_Save:
        public const string WAS_Sys_ObjectInModules_Save = "ErridnInventory.WAS_Sys_UserInGroup_Save"; //// //WAS_Sys_UserInGroup_Save

        // RptSv:
        public const string WAS_RptSv_Sys_ObjectInModules_Get = "ErridnInventory.WAS_RptSv_Sys_ObjectInModules_Get"; //// //WAS_RptSv_Sys_ObjectInModules_Get
        public const string WAS_RptSv_Sys_ObjectInModules_Save = "ErridnInventory.WAS_RptSv_Sys_ObjectInModules_Save"; //// //WAS_RptSv_Sys_ObjectInModules_Save
        
        #endregion

        #region // Err_WebServicesOutSide:        
        public const string Err_WebServicesOutSide = "ErrWMS.Err_WebServicesOutSide"; //// //Err_WebServicesOutSide
		#endregion

		#region // Sys_UserLicense:
		// Sys_UserLicense_CheckDB:
		public const string Sys_UserLicense_CheckDB_UserLicenseNotFound = "ErridnInventory.Sys_UserLicense_CheckDB_UserLicenseNotFound"; //// //Sys_UserLicense_CheckDB_UserLicenseNotFound
		public const string Sys_UserLicense_CheckDB_UserLicenseExist = "ErridnInventory.Sys_UserLicense_CheckDB_UserLicenseExist"; //// //Sys_UserLicense_CheckDB_UserLicenseExist
		public const string Sys_UserLicense_CheckDB_FlagActiveNotMatched = "ErridnInventory.Sys_UserLicense_CheckDB_FlagActiveNotMatched"; //// //Sys_UserLicense_CheckDB_FlagActiveNotMatched


		// Sys_UserLicense_CheckDeny:
		public const string Sys_UserLicense_CheckDeny = "Sys_UserLicense_CheckDeny"; //// //Sys_UserLicense_CheckDeny

		// Sys_UserLicense_Get:
		public const string Sys_UserLicense_Get = "ErridnInventory.Sys_UserLicense_Get"; //// //Sys_UserLicense_Get

		// WAS_Sys_UserLicense_Get:
		public const string WAS_Sys_UserLicense_Get = "ErridnInventory.WAS_Sys_UserLicense_Get"; //// //WAS_Sys_UserLicense_Get

		// Sys_UserLicense_Save:
		public const string Sys_UserLicense_Save = "ErridnInventory.Sys_UserLicense_Save"; //// //Sys_UserLicense_Save
		public const string Sys_UserLicense_Save_InputTblDtlNotFound = "ErridnInventory.Sys_UserLicense_Save_InputTblDtlNotFound"; //// //Sys_UserLicense_Save_InputTblDtlNotFound

		// WAS_Sys_UserLicense_Save:
		public const string WAS_Sys_UserLicense_Save = "ErridnInventory.WAS_Sys_UserLicense_Save"; //// //WAS_Sys_UserLicense_Save
		#endregion
	}
}
