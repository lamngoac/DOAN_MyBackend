using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;


namespace idn.Skycic.Inventory.Errors
{
	public partial class ErridnInventoryBank
	{
		#region // Common:
		// Common:
		public const string NoError = Error.NoError; //// Thực hiện thành công.
		public const string ErridnInventoryBankPrefix = "Erridn.Skycic.Inventory."; //// Lỗi idn.Skycic.Inventory.
		public const string CmSys_ServiceInit = "Erridn.Skycic.Inventory.CmSys_ServiceInit"; //// Lỗi Hệ thống, khi khởi tạo SystemService.
		public const string CmSys_InvalidTid = "Erridn.Skycic.Inventory.CmSys_InvalidTid"; //// Lỗi Hệ thống, Mã giao dịch không hợp lệ.
		public const string CmSys_GatewayAuthenticateFailed = "Erridn.Skycic.Inventory.CmSys_GatewayAuthenticateFailed"; //// Lỗi Hệ thống, khi truy nhập trái phép vào Cổng giao dịch.
		public const string CmSys_SessionPreInitFailed = "Erridn.Skycic.Inventory.CmSys_SessionPreInitFailed"; //// Lỗi Hệ thống, khi khởi tạo Phiên làm việc.
		public const string CmSys_SessionNotFound = "Erridn.Skycic.Inventory.CmSys_SessionNotFound"; //// Lỗi Hệ thống, Phiên làm việc không hợp lệ.
		public const string CmSys_SessionExpired = "Erridn.Skycic.Inventory.CmSys_SessionExpired"; //// Lỗi Hệ thống, Phiên làm việc đã hết hạn.
		public const string CmSys_InvalidServiceCode = "Erridn.Skycic.Inventory.CmSys_InvalidServiceCode"; //// //CmSys_InvalidServiceCode
		public const string CmSys_InvalidBizSpecialPw = "Erridn.Skycic.Inventory.CmSys_InvalidBizSpecialPw"; //// //CmSys_InvalidBizSpecialPw
		public const string CmSys_InvalidStdCode = "Erridn.Skycic.Inventory.CmSys_InvalidStdCode"; //// //CmSys_InvalidStdCode

		// CmApp_Mst_Common:
		public const string CmApp_Mst_Common_TableNotSupported = "Erridn.Skycic.Inventory.CmApp_Mst_Common_TableNotSupported"; //// //CmApp_Mst_Common_TableNotSupported

		#endregion

		#region // T24.Common:
		// Common:
		public const string T24Error = "T24Error"; //// T24Error.
		#endregion

		#region // Scf_CallT24: 
		// Scf_CallT24:
		public const string Scf_CallT24_GetScfLcCollateralInfo_Error = "ErridnInventoryBank.Scf_CallT24_GetScfLcCollateralInfo_Error"; //// Scf_CallT24_GetScfLcCollateralInfo_Error.
		#endregion

		#region // MasterData:
		// Mst_Common_Get:
		public const string Mst_Common_Get = "Erridn.Skycic.Inventory.Mst_Common_Get"; //// //Mst_Common_Get
		public const string Mst_Common_Get_NotSupportTable = "Erridn.Skycic.Inventory.Mst_Common_Get_NotSupportTable"; //// //Mst_Common_Get_NotSupportTable

		// Cm_ExecSql:
		public const string Cm_ExecSql = "Erridn.Skycic.Inventory.Cm_ExecSql"; //// //Cm_ExecSql
		public const string Cm_ExecSql_ParamMissing = "Erridn.Skycic.Inventory.Cm_ExecSql_ParamMissing"; //// //Cm_ExecSql_ParamMissing

		#endregion

		#region // Test Service:
		// Test_Service_01:
		public const string Test_Service_01 = "ErridnInventoryBank.Test_Service_01"; //// //Test_Service_01
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

		#endregion

		#region // T24_LC:
		// T24_LC_Get:
		public const string T24_LC_Get = "ErridnInventoryBank.T24_LC_Get"; //// //T24_LC_Get
		public const string T24_LC_GetByT24_LC_ID = "ErridnInventoryBank.T24_LC_GetByT24_LC_ID"; //// //T24_LC_GetByT24_LC_ID
		#endregion

		#region // T24_Collateral:
		// T24_Collateral_Get:
		public const string T24_Collateral_Get = "ErridnInventoryBank.T24_Collateral_Get"; //// //T24_Collateral_Get
		public const string T24_Collateral_GetByT24_Collateral_ID = "ErridnInventoryBank.T24_Collateral_GetByT24_Collateral_ID"; //// //T24_Collateral_GetByT24_Collateral_ID
		public const string T24_Collateral_GetByT24_CifInfo = "ErridnInventoryBank.T24_Collateral_GetByT24_CifInfo"; //// //T24_Collateral_GetByT24_CifInfo
		#endregion

		#region // T24_CollateralChange:
		// T24_CollateralChange_Get:
		public const string T24_CollateralChange_Get = "ErridnInventoryBank.T24_CollateralChange_Get"; //// //T24_CollateralChange_Get
		#endregion

		#region // T24_LD:
		// T24_LD_Get:
		public const string T24_LD_Get = "ErridnInventoryBank.T24_LD_Get"; //// //T24_LD_Get
		public const string T24_LD_GetByT24_LD_ID = "ErridnInventoryBank.T24_LD_GetByT24_LD_ID"; //// //T24_LD_GetByT24_LD_ID
		#endregion

		#region // T24_LDChange:
		// T24_LDChange_Get:
		public const string T24_LDChange_Get = "ErridnInventoryBank.T24_LDChange_Get"; //// //T24_LDChange_Get

		// T24_LDChange_GetByT24_Cif:
		public const string T24_LDChange_GetByT24_Cif = "ErridnInventoryBank.T24_LDChange_GetByT24_Cif"; //// //T24_LDChange_GetByT24_Cif
		#endregion

		#region // T24_LDCreate:
		// T24_LDCreate_Get:
		public const string T24_LDCreate_Get = "ErridnInventoryBank.T24_LDCreate_Get"; //// //T24_LDCreate_Get
		#endregion

		#region // T24_MD:
		// T24_MD_Get:
		public const string T24_MD_Get = "ErridnInventoryBank.T24_MD_Get"; //// //T24_MD_Get
		public const string T24_MD_Get_InvalidT24_Collateral_ID = "ErridnInventoryBank.T24_MD_Get_InvalidT24_Collateral_ID"; //// //T24_MD_Get_InvalidT24_Collateral_ID
		public const string T24_MD_GetByT24_MD_ID = "ErridnInventoryBank.T24_MD_GetByT24_MD_ID"; //// //T24_MD_GetByT24_MD_ID
		public const string T24_MD_GetByT24_MD_ID_InvalidT24_Collateral_ID = "ErridnInventoryBank.T24_MD_GetByT24_MD_ID_InvalidT24_Collateral_ID"; //// //T24_MD_GetByT24_MD_ID_InvalidT24_Collateral_ID
		#endregion

		#region // T24_MDChange:
		// T24_MDChange_Get:
		public const string T24_MDChange_Get = "ErridnInventoryBank.T24_MDChange_Get"; //// //T24_MDChange_Get

		// T24_MDChange_GetByT24_Cif:
		public const string T24_MDChange_GetByT24_Cif = "ErridnInventoryBank.T24_MDChange_GetByT24_Cif"; //// //T24_MDChange_GetByT24_Cif
		#endregion

		#region // T24_MDCreate:
		// T24_MDCreate_Get:
		public const string T24_MDCreate_Get = "ErridnInventoryBank.T24_MDCreate_Get"; //// //T24_MDCreate_Get
		#endregion

		#region // T24_Customer:
		// T24_Customer_Get:
		public const string T24_Customer_Get = "ErridnInventoryBank.T24_Customer_Get"; //// //T24_Customer_Get
		#endregion

		#region // T24_CustomerAcc:
		// T24_CustomerAcc_Get:
		public const string T24_CustomerAcc_Get = "ErridnInventoryBank.T24_CustomerAcc_Get"; //// //T24_CustomerAcc_Get
		#endregion

		#region // T24_CurrencyConvert:
		// T24_CurrencyConvert_Get:
		public const string T24_CurrencyConvert_Get = "ErridnInventoryBank.T24_CurrencyConvert_Get"; //// //T24_CurrencyConvert_Get
		#endregion

		#region // Sys_Group:
		// Sys_Group_CheckDB:
		public const string Sys_Group_CheckDB_GroupCodeNotFound = "ErridnInventoryBank.Sys_Group_CheckDB_GroupCodeNotFound"; //// //Sys_Group_CheckDB_GroupCodeNotFound
		public const string Sys_Group_CheckDB_GroupCodeExist = "ErridnInventoryBank.Sys_Group_CheckDB_GroupCodeExist"; //// //Sys_Group_CheckDB_GroupCodeExist
		public const string Sys_Group_CheckDB_FlagActiveNotMatched = "ErridnInventoryBank.Sys_Group_CheckDB_FlagActiveNotMatched"; //// //Sys_Group_CheckDB_FlagActiveNotMatched
		public const string Sys_Group_CheckDB_FlagPublicNotMatched = "ErridnInventoryBank.Sys_Group_CheckDB_FlagPublicNotMatched"; //// //Sys_Group_CheckDB_FlagPublicNotMatched

		// Sys_Group_Get:
		public const string Sys_Group_Get = "ErridnInventoryBank.Sys_Group_Get"; //// //Sys_Group_Get

		// Sys_Group_Create:
		public const string Sys_Group_Create = "ErridnInventoryBank.Sys_Group_Create"; //// //Sys_Group_Create
		public const string Sys_Group_Create_InvalidGroupCode = "ErridnInventoryBank.Sys_Group_Create_InvalidGroupCode"; //// //Sys_Group_Create_InvalidGroupCode
		public const string Sys_Group_Create_InvalidGroupName = "ErridnInventoryBank.Sys_Group_Create_InvalidGroupName"; //// //Sys_Group_Create_InvalidGroupName

		// Sys_Group_Update:
		public const string Sys_Group_Update = "ErridnInventoryBank.Sys_Group_Update"; //// //Sys_Group_Update
		public const string Sys_Group_Update_InvalidGroupCode = "ErridnInventoryBank.Sys_Group_Update_InvalidGroupCode"; //// //Sys_Group_Update_InvalidGroupCode
		public const string Sys_Group_Update_InvalidGroupName = "ErridnInventoryBank.Sys_Group_Update_InvalidGroupName"; //// //Sys_Group_Update_InvalidGroupName

		// Sys_Group_Delete:
		public const string Sys_Group_Delete = "ErridnInventoryBank.Sys_Group_Delete"; //// //Sys_Group_Delete

		#endregion

		#region // Sys_User:
		// Sys_User_CheckDB:
		public const string Sys_User_CheckDB_UserCodeNotFound = "ErridnInventoryBank.Sys_User_CheckDB_UserCodeNotFound"; //// //Sys_User_CheckDB_UserCodeNotFound
		public const string Sys_User_CheckDB_UserCodeExist = "ErridnInventoryBank.Sys_User_CheckDB_UserCodeExist"; //// //Sys_User_CheckDB_UserCodeExist
		public const string Sys_User_CheckDB_FlagActiveNotMatched = "ErridnInventoryBank.Sys_User_CheckDB_FlagActiveNotMatched"; //// //Sys_User_CheckDB_FlagActiveNotMatched
		public const string Sys_User_CheckDB_FlagSysAdminNotMatched = "ErridnInventoryBank.Sys_User_CheckDB_FlagSysAdminNotMatched"; //// //Sys_User_CheckDB_FlagSysAdminNotMatched
		public const string Sys_User_CheckDB_BizInvalidUserAbility = "ErridnInventoryBank.Sys_User_CheckDB_BizInvalidUserAbility"; //// //Sys_User_CheckDB_BizInvalidUserAbility

		// Sys_User_BizInvalidUserAbility:
		public const string Sys_User_BizInvalidUserAbility = "ErridnInventoryBank.Sys_User_BizInvalidUserAbility"; //// //Sys_User_BizInvalidUserAbility

		// Sys_User_ChangePassword:
		public const string Sys_User_ChangePassword = "ErridnInventoryBank.Sys_User_ChangePassword"; //// //Sys_User_ChangePassword
		public const string Sys_User_ChangePassword_InvalidPasswordOld = "ErridnInventoryBank.Sys_User_ChangePassword_InvalidPasswordOld"; //// //Sys_User_ChangePassword_InvalidPasswordOld

		// Sys_User_GetForCurrentUser:
		public const string Sys_User_GetForCurrentUser = "ErridnInventoryBank.Sys_User_GetForCurrentUser"; //// //Sys_User_GetForCurrentUser

		// Sys_User_Login:
		public const string Sys_User_Login = "ErridnInventoryBank.Sys_User_Login"; //// //Sys_User_Login
		public const string Sys_User_Login_Checking = "ErridnInventoryBank.Sys_User_Login_Checking"; //// //Sys_User_Login_Checking
		public const string Sys_User_Login_InvalidPassword = "ErridnInventoryBank.Sys_User_Login_InvalidPassword"; //// //Sys_User_Login_InvalidPassword

		// Sys_User_Get:
		public const string Sys_User_Get = "ErridnInventoryBank.Sys_User_Get"; //// //Sys_User_Get
		public const string Sys_User_Get_01 = "ErridnInventoryBank.Sys_User_Get_01"; //// //Sys_User_Get_01

		// Sys_User_Logout:
		public const string Sys_User_Logout = "ErridnInventoryBank.Sys_User_Logout"; //// //Sys_User_Logout

		// Sys_User_GetByDB:
		public const string Sys_User_GetByDB = "ErridnInventoryBank.Sys_User_GetByDB"; //// //Sys_User_GetByDB

		// Sys_User_Create:
		public const string Sys_User_Create = "ErridnInventoryBank.Sys_User_Create"; //// //Sys_User_Create
		public const string Sys_User_Create_InvalidUserCode = "ErridnInventoryBank.Sys_User_Create_InvalidUserCode"; //// //Sys_User_Create_InvalidUserCode
		public const string Sys_User_Create_InvalidDBCode = "ErridnInventoryBank.Sys_User_Create_InvalidDBCode"; //// //Sys_User_Create_InvalidDBCode
		public const string Sys_User_Create_InvalidAreaCode = "ErridnInventoryBank.Sys_User_Create_InvalidAreaCode"; //// //Sys_User_Create_InvalidAreaCode
		public const string Sys_User_Create_InvalidUserName = "ErridnInventoryBank.Sys_User_Create_InvalidUserName"; //// //Sys_User_Create_InvalidUserName
		public const string Sys_User_Create_InvalidUserPassword = "ErridnInventoryBank.Sys_User_Create_InvalidUserPassword"; //// //Sys_User_Create_InvalidUserPassword
		public const string Sys_User_Create_InvalidFlagSysAdmin = "ErridnInventoryBank.Sys_User_Create_InvalidFlagSysAdmin"; //// //Sys_User_Create_InvalidFlagSysAdmin
		public const string Sys_User_Create_InvalidFlagDBAdmin = "ErridnInventoryBank.Sys_User_Create_InvalidFlagDBAdmin"; //// //Sys_User_Create_InvalidFlagDBAdmin

		// Sys_User_Update:
		public const string Sys_User_Update = "ErridnInventoryBank.Sys_User_Update"; //// //Sys_User_Update
		public const string Sys_User_Update_InvalidUserCode = "ErridnInventoryBank.Sys_User_Update_InvalidUserCode"; //// //Sys_User_Update_InvalidUserCode
		public const string Sys_User_Update_InvalidDBCode = "ErridnInventoryBank.Sys_User_Update_InvalidDBCode"; //// //Sys_User_Update_InvalidDBCode
		public const string Sys_User_Update_InvalidAreaCode = "ErridnInventoryBank.Sys_User_Update_InvalidAreaCode"; //// //Sys_User_Update_InvalidAreaCode
		public const string Sys_User_Update_InvalidUserName = "ErridnInventoryBank.Sys_User_Update_InvalidUserName"; //// //Sys_User_Update_InvalidUserName
		public const string Sys_User_Update_InvalidUserPassword = "ErridnInventoryBank.Sys_User_Update_InvalidUserPassword"; //// //Sys_User_Update_InvalidUserPassword
		public const string Sys_User_Update_InvalidFlagSysAdmin = "ErridnInventoryBank.Sys_User_Update_InvalidFlagSysAdmin"; //// //Sys_User_Update_InvalidFlagSysAdmin
		public const string Sys_User_Update_InvalidFlagDBAdmin = "ErridnInventoryBank.Sys_User_Update_InvalidFlagDBAdmin"; //// //Sys_User_Update_InvalidFlagDBAdmin

		// Sys_User_Delete:
		public const string Sys_User_Delete = "ErridnInventoryBank.Sys_User_Delete"; //// //Sys_User_Delete

		#endregion

		#region // Sys_UserInGroup:
		// Sys_UserInGroup_Save:
		public const string Sys_UserInGroup_Save = "ErridnInventoryBank.Sys_UserInGroup_Save"; //// //Sys_UserInGroup_Save
		public const string Sys_UserInGroup_Save_InputTblDtlNotFound = "ErridnInventoryBank.Sys_UserInGroup_Save_InputTblDtlNotFound"; //// //Sys_UserInGroup_Save_InputTblDtlNotFound

		#endregion

		#region // Acc_AccMapUser:
		// Acc_AccMapUser_Save:
		public const string Acc_AccMapUser_Save = "ErridnInventoryBank.Acc_AccMapUser_Save"; //// //Acc_AccMapUser_Save
		#endregion

		#region // Sys_Access:
		// Sys_Access_CheckDeny:
		public const string Sys_Access_CheckDeny = "Sys_Access_CheckDeny"; //// //Sys_Access_CheckDeny
		// Sys_ViewAbility_Deny:
		public const string Sys_ViewAbility_Deny = "Sys_ViewAbility_Deny"; //// //Sys_ViewAbility_Deny
		public const string Sys_ViewAbility_NotExactUser = "Sys_ViewAbility_NotExactUser"; //// //Sys_ViewAbility_NotExactUser

		// Sys_Access_Get:
		public const string Sys_Access_Get = "ErridnInventoryBank.Sys_Access_Get"; //// //Sys_Access_Get

		// Sys_Access_Save:
		public const string Sys_Access_Save = "ErridnInventoryBank.Sys_Access_Save"; //// //Sys_Access_Save
		public const string Sys_Access_Save_InputTblDtlNotFound = "ErridnInventoryBank.Sys_Access_Save_InputTblDtlNotFound"; //// //Sys_Access_Save_InputTblDtlNotFound

		#endregion

		#region // Sys_Object:
		// Sys_Object_Get:
		public const string Sys_Object_Get = "ErridnInventoryBank.Sys_Object_Get"; //// //Sys_Object_Get
		#endregion

	}
}
