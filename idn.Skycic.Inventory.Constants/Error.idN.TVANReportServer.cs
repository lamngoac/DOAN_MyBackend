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
        #region // RptSv_Sys_Solution:
        // RptSv_Sys_Solution_Get
        public const string RptSv_Sys_Solution_Get = "ErridnInventory.RptSv_Sys_Solution_Get"; //// //RptSv_Sys_Solution_Get
        // WAS_RptSv_Sys_Solution_Get
        public const string WAS_RptSv_Sys_Solution_Get = "ErridnInventory.WAS_RptSv_Sys_Solution_Get"; //// //
        #endregion

        #region // RptSv_Invoice_TempGroup:
        // RptSv_Invoice_TempGroup_Create:
        public const string RptSv_Invoice_TempGroup_Create = "ErridnInventory.RptSv_Invoice_TempGroup_Create"; //// //RptSv_Invoice_TempGroup_Create
		public const string RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupCode = "ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupCode"; //// //RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupCode
		public const string RptSv_Invoice_TempGroup_Create_InvalidVATType = "ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidVATType"; //// //RptSv_Invoice_TempGroup_Create_InvalidVATType
		public const string RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupName = "ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupName"; //// //RptSv_Invoice_TempGroup_Create_InvalidInvoiceTGroupName
		public const string RptSv_Invoice_TempGroup_Create_InvalidSpec_Prd_Type = "ErridnInventory.RptSv_Invoice_TempGroup_Create_InvalidSpec_Prd_Type"; //// //RptSv_Invoice_TempGroup_Create_InvalidSpec_Prd_Type
		public const string RptSv_Invoice_TempGroup_Create_TempGroupFieldNotFound = "ErridnInventory.RptSv_Invoice_TempGroup_Create_TempGroupFieldNotFound"; //// //RptSv_Invoice_TempGroup_Create_TempGroupFieldNotFound
		public const string RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName = "ErridnInventory.RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName"; //// //RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidDBFieldName
		public const string RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType = "ErridnInventory.RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType"; //// //RptSv_Invoice_TempGroup_Create_TempGroupFieldTbl_InvalidTCFType

		// WAS_RptSv_Invoice_TempGroup_Create:
		public const string WAS_RptSv_Invoice_TempGroup_Create = "ErridnInventory.WAS_RptSv_Invoice_TempGroup_Create"; //// //WAS_RptSv_Invoice_TempGroup_Create

		// RptSv_Invoice_TempGroup_Get:
		public const string RptSv_Invoice_TempGroup_Get = "ErridnInventory.RptSv_Invoice_TempGroup_Get"; //// //RptSv_Invoice_TempGroup_Get

		// WAS_RptSv_Invoice_TempGroup_Get:
		public const string WAS_RptSv_Invoice_TempGroup_Get = "ErridnInventory.WAS_RptSv_Invoice_TempGroup_Get"; //// //WAS_RptSv_Invoice_TempGroup_Get

		// WAS_RptSv_Invoice_TempGroup_Update:
		public const string WAS_RptSv_Invoice_TempGroup_Update = "ErridnInventory.WAS_RptSv_Invoice_TempGroup_Update"; //// //WAS_RptSv_Invoice_TempGroup_Update

		// WAS_RptSv_Invoice_TempGroup_Delete:
		public const string WAS_RptSv_Invoice_TempGroup_Delete = "ErridnInventory.WAS_RptSv_Invoice_TempGroup_Delete"; //// //WAS_RptSv_Invoice_TempGroup_Delete

		#endregion

		#region // RptSv_Mst_VATRate:
		// RptSv_Mst_VATRate_Get:
		public const string RptSv_Mst_VATRate_Get = "ErridnInventory.RptSv_Mst_VATRate_Get"; //// //RptSv_Mst_VATRate_Get

		// WAS_RptSv_Mst_VATRate_Get:
		public const string WAS_RptSv_Mst_VATRate_Get = "ErridnInventory.WAS_RptSv_Mst_VATRate_Get"; //// //WAS_RptSv_Mst_VATRate_Get

		#endregion

		#region // RptSv_Mst_PaymentMethods:
		// RptSv_Mst_PaymentMethods_Get:
		public const string RptSv_Mst_PaymentMethods_Get = "ErridnInventory.RptSv_Mst_PaymentMethods_Get"; //// //RptSv_Mst_PaymentMethods_Get

		// WAS_RptSv_Mst_PaymentMethods_Get:
		public const string WAS_RptSv_Mst_PaymentMethods_Get = "ErridnInventory.WAS_RptSv_Mst_PaymentMethods_Get"; //// //WAS_RptSv_Mst_PaymentMethods_Get

		#endregion

		#region // RptSv_Mst_InvoiceType:
		// RptSv_Mst_InvoiceType_Get:
		public const string RptSv_Mst_InvoiceType_Get = "ErridnInventory.RptSv_Mst_InvoiceType_Get"; //// //RptSv_Mst_InvoiceType_Get

		// WAS_RptSv_Mst_InvoiceType_Get:
		public const string WAS_RptSv_Mst_InvoiceType_Get = "ErridnInventory.WAS_RptSv_Mst_InvoiceType_Get"; //// //WAS_RptSv_Mst_InvoiceType_Get

		#endregion

		#region // Map_DealerDiscount:
		// RptSv_Map_DealerDiscount_Get:
		public const string RptSv_Map_DealerDiscount_Get = "ErridnInventory.RptSv_Map_DealerDiscount_Get"; //// //RptSv_Map_DealerDiscount_Get

		// WAS_RptSv_Map_DealerDiscount_Get:
		public const string WAS_RptSv_Map_DealerDiscount_Get = "ErridnInventory.WAS_RptSv_Map_DealerDiscount_Get"; //// //WAS_RptSv_Map_DealerDiscount_Get

		#endregion

		#region // RptSv_Sys_Access:
		// RptSv_Sys_Access_CheckDeny:
		public const string RptSv_Sys_Access_CheckDeny = "ErridnInventory.RptSv_Sys_Access_CheckDeny"; //// // RptSv_Sys_Access_CheckDeny

        // RptSv_Sys_Access_Get:
        public const string RptSv_Sys_Access_Get = "ErridnInventory.RptSv_Sys_Access_Get"; //// // RptSv_Sys_Access_Get

        // WAS_RptSv_Sys_Access_Get:
        public const string WAS_RptSv_Sys_Access_Get = "ErridnInventory.WAS_RptSv_Sys_Access_Get"; //// // WAS_RptSv_Sys_Access_Get

        // RptSv_Sys_Access_Save:
        public const string RptSv_Sys_Access_Save = "ErridnInventory.RptSv_Sys_Access_Save"; //// // RptSv_Sys_Access_Save
        public const string RptSv_Sys_Access_Save_InputTblDtlNotFound = "ErridnInventory.RptSv_Sys_Access_Save_InputTblDtlNotFound"; //// // RptSv_Sys_Access_Save_InputTblDtlNotFound

        //WAS_RptSv_Sys_Access_Save
        public const string WAS_RptSv_Sys_Access_Save = "ErridnInventory.WAS_RptSv_Sys_Access_Save"; //// // WAS_RptSv_Sys_Access_Save

        #endregion

        #region // RptSv_Sys_Group:
        // RptSv_Sys_Group_CheckDB:
        public const string RptSv_Sys_Group_CheckDB_GroupCodeNotFound = "ErridnInventory.RptSv_Sys_Group_CheckDB_GroupCodeNotFound"; //// // RptSv_Sys_Group_CheckDB_GroupCodeNotFound
        public const string RptSv_Sys_Group_CheckDB_GroupCodeExist = "ErridnInventory.RptSv_Sys_Group_CheckDB_GroupCodeExist"; //// // RptSv_Sys_Group_CheckDB_GroupCodeExist
        public const string RptSv_Sys_Group_CheckDB_FlagActiveNotMatched = "ErridnInventory.RptSv_Sys_Group_CheckDB_FlagActiveNotMatched"; //// // RptSv_Sys_Group_CheckDB_FlagActiveNotMatched

        // RptSv_Sys_Group_Get:
        public const string RptSv_Sys_Group_Get = "ErridnInventory.RptSv_Sys_Group_Get"; //// // RptSv_Sys_Group_Get

        // RptSv_Sys_Group_Create:
        public const string RptSv_Sys_Group_Create = "ErridnInventory.RptSv_Sys_Group_Create"; //// // RptSv_Sys_Group_Create
        public const string RptSv_Sys_Group_Create_InvalidGroupCode = "ErridnInventory.RptSv_Sys_Group_Create_InvalidGroupCode"; //// // RptSv_Sys_Group_Create_InvalidGroupCode
        public const string RptSv_Sys_Group_Create_InvalidGroupName = "ErridnInventory.RptSv_Sys_Group_Create_InvalidGroupName"; //// // RptSv_Sys_Group_Create_InvalidGroupName

        // RptSv_Sys_Group_Update:
        public const string RptSv_Sys_Group_Update = "ErridnInventory.RptSv_Sys_Group_Update"; //// // RptSv_Sys_Group_Update
        public const string RptSv_Sys_Group_Update_InvalidGroupName = "ErridnInventory.RptSv_Sys_Group_Update_InvalidGroupName"; //// // RptSv_Sys_Group_Update_InvalidGroupName
        
        // RptSv_Sys_Group_Delete:
        public const string RptSv_Sys_Group_Delete = "ErridnInventory.RptSv_Sys_Group_Delete"; //// // RptSv_Sys_Group_Delete

        // WAS_RptSv_Sys_Group_Get:
        public const string WAS_RptSv_Sys_Group_Get = "ErridnInventory.WAS_RptSv_Sys_Group_Get"; //// // WAS_RptSv_Sys_Group_Get

        // WAS_RptSv_Sys_Group_Create:
        public const string WAS_RptSv_Sys_Group_Create = "ErridnInventory.WAS_RptSv_Sys_Group_Create"; //// // WAS_RptSv_Sys_Group_Create

        // WAS_RptSv_Sys_Group_Update:
        public const string WAS_RptSv_Sys_Group_Update = "ErridnInventory.WAS_RptSv_Sys_Group_Update"; //// // WAS_RptSv_Sys_Group_Update

        // WAS_RptSv_Sys_Group_Delete: 
        public const string WAS_RptSv_Sys_Group_Delete = "ErridnInventory.WAS_RptSv_Sys_Group_Delete"; //// // WAS_RptSv_Sys_Group_Delete

        // RptSv_Sys_UserInGroup_Save:
        public const string RptSv_Sys_UserInGroup_Save = "ErridnInventory.RptSv_Sys_UserInGroup_Save"; //// // RptSv_Sys_UserInGroup_Save
        public const string RptSv_Sys_UserInGroup_Save_InputTblDtlNotFound = "ErridnInventory.RptSv_Sys_UserInGroup_Save_InputTblDtlNotFound"; //// // RptSv_Sys_UserInGroup_Save_InputTblDtlNotFound

        //WAS_RptSv_Sys_UserInGroup_Save:
        public const string WAS_RptSv_Sys_UserInGroup_Save = "ErridnInventory.WAS_RptSv_Sys_UserInGroup_Save"; //// // WAS_RptSv_Sys_UserInGroup_Save
        #endregion

        #region // RptSv_Sys_User:
        // RptSv_Sys_User_CheckDB:
        public const string RptSv_Sys_User_CheckDB_UserCodeNotFound = "ErridnInventory.RptSv_Sys_User_CheckDB_UserCodeNotFound"; //// //RptSv_Sys_User_CheckDB_UserCodeNotFound
		public const string RptSv_Sys_User_CheckDB_UserCodeExist = "ErridnInventory.RptSv_Sys_User_CheckDB_UserCodeExist"; //// //RptSv_Sys_User_CheckDB_UserCodeExist
		public const string RptSv_Sys_User_CheckDB_FlagActiveNotMatched = "ErridnInventory.RptSv_Sys_User_CheckDB_FlagActiveNotMatched"; //// //RptSv_Sys_User_CheckDB_FlagActiveNotMatched
		public const string RptSv_Sys_User_CheckDB_FlagSysAdminNotMatched = "ErridnInventory.RptSv_Sys_User_CheckDB_FlagSysAdminNotMatched"; //// //RptSv_Sys_User_CheckDB_FlagSysAdminNotMatched
		public const string RptSv_Sys_User_CheckDB_BizInvalidUserAbility = "ErridnInventory.RptSv_Sys_User_CheckDB_BizInvalidUserAbility"; //// //RptSv_Sys_User_CheckDB_BizInvalidUserAbility

		// RptSv_Sys_User_BizInvalidUserAbility:
		public const string RptSv_Sys_User_BizInvalidUserAbility = "ErridnInventory.RptSv_Sys_User_BizInvalidUserAbility"; //// //RptSv_Sys_User_BizInvalidUserAbility

		// RptSv_Sys_User_ChangePassword:
		public const string RptSv_Sys_User_ChangePassword = "ErridnInventory.RptSv_Sys_User_ChangePassword"; //// //RptSv_Sys_User_ChangePassword
		public const string RptSv_Sys_User_ChangePassword_InvalidPasswordOld = "ErridnInventory.RptSv_Sys_User_ChangePassword_InvalidPasswordOld"; //// //RptSv_Sys_User_ChangePassword_InvalidPasswordOld
		public const string RptSv_Sys_User_ChangePassword_InvalidPasswordNew = "ErridnInventory.RptSv_Sys_User_ChangePassword_InvalidPasswordNew"; //// //RptSv_Sys_User_ChangePassword_InvalidPasswordNew

		// RptSv_Sys_User_ResetPassword:
		public const string RptSv_Sys_User_ResetPassword = "ErridnInventory.RptSv_Sys_User_ResetPassword"; //// //RptSv_Sys_User_ResetPassword

		// RptSv_Sys_User_GetForCurrentUser:
		public const string RptSv_Sys_User_GetForCurrentUser = "ErridnInventory.RptSv_Sys_User_GetForCurrentUser"; //// //RptSv_Sys_User_GetForCurrentUser

		// RptSv_Sys_User_Login:
		public const string RptSv_Sys_User_Login = "ErridnInventory.RptSv_Sys_User_Login"; //// //RptSv_Sys_User_Login
		public const string RptSv_Sys_User_Login_InvalidLDAP = "ErridnInventory.RptSv_Sys_User_Login_InvalidLDAP"; //// //RptSv_Sys_User_Login_InvalidLDAP
		public const string RptSv_Sys_User_Login_Checking = "ErridnInventory.RptSv_Sys_User_Login_Checking"; //// //RptSv_Sys_User_Login_Checking
		public const string RptSv_Sys_User_Login_InvalidPassword = "ErridnInventory.RptSv_Sys_User_Login_InvalidPassword"; //// //RptSv_Sys_User_Login_InvalidPassword

		// RptSv_Sys_User_CheckAuthentication:
		public const string RptSv_Sys_User_CheckAuthentication = "ErridnInventory.RptSv_Sys_User_CheckAuthentication"; //// //RptSv_Sys_User_CheckAuthentication
		public const string RptSv_Sys_User_CheckAuthentication_InvalidPassword = "ErridnInventory.RptSv_Sys_User_CheckAuthentication_InvalidPassword"; //// //RptSv_Sys_User_CheckAuthentication_InvalidPassword

		// RptSv_Sys_User_Get:
		public const string RptSv_Sys_User_Get = "ErridnInventory.RptSv_Sys_User_Get"; //// //RptSv_Sys_User_Get
		public const string RptSv_Sys_User_Get_01 = "ErridnInventory.RptSv_Sys_User_Get_01"; //// //RptSv_Sys_User_Get_01

		// WA_RptSv_Sys_User_Get:
		public const string WA_RptSv_Sys_User_Get = "ErridnInventory.WA_RptSv_Sys_User_Get"; //// //WA_RptSv_Sys_User_Get

		// WAS_RptSv_Sys_User_Get:
		public const string WAS_RptSv_Sys_User_Get = "ErridnInventory.WAS_RptSv_Sys_User_Get"; //// //WAS_RptSv_Sys_User_Get

		// WAS_RptSv_Sys_User_GetForCurrentUser:
		public const string WAS_RptSv_Sys_User_GetForCurrentUser = "ErridnInventory.WAS_RptSv_Sys_User_GetForCurrentUser"; //// //WAS_RptSv_Sys_User_GetForCurrentUser

		// WAS_RptSv_Sys_User_Create:
		public const string WAS_RptSv_Sys_User_Create = "ErridnInventory.WAS_RptSv_Sys_User_Create"; //// //WAS_RptSv_Sys_User_Create

		// WAS_RptSv_Sys_User_ChangePassword:
		public const string WAS_RptSv_Sys_User_ChangePassword = "ErridnInventory.WAS_RptSv_Sys_User_ChangePassword"; //// //WAS_RptSv_Sys_User_ChangePassword

		// WAS_RptSv_Sys_User_Update:
		public const string WAS_RptSv_Sys_User_Update = "ErridnInventory.WAS_RptSv_Sys_User_Update"; //// //WAS_RptSv_Sys_User_Update

		// WAS_RptSv_Sys_User_Delete:
		public const string WAS_RptSv_Sys_User_Delete = "ErridnInventory.WAS_RptSv_Sys_User_Delete"; //// //WAS_RptSv_Sys_User_Delete

		// WAS_RptSv_Sys_User_Login:
		public const string WAS_RptSv_Sys_User_Login = "ErridnInventory.WAS_RptSv_Sys_User_Login"; //// //WAS_RptSv_Sys_User_Login

		// RptSv_Sys_User_Logout:
		public const string RptSv_Sys_User_Logout = "ErridnInventory.RptSv_Sys_User_Logout"; //// //RptSv_Sys_User_Logout

		// RptSv_Sys_User_GetByDB:
		public const string RptSv_Sys_User_GetByDB = "ErridnInventory.RptSv_Sys_User_GetByDB"; //// //RptSv_Sys_User_GetByDB

		// RptSv_Sys_User_Create:
		public const string RptSv_Sys_User_Create = "ErridnInventory.RptSv_Sys_User_Create"; //// //RptSv_Sys_User_Create
		public const string RptSv_Sys_User_Create_InvalidUserCode = "ErridnInventory.RptSv_Sys_User_Create_InvalidUserCode"; //// //RptSv_Sys_User_Create_InvalidUserCode
		public const string RptSv_Sys_User_Create_InvalidDBCode = "ErridnInventory.RptSv_Sys_User_Create_InvalidDBCode"; //// //RptSv_Sys_User_Create_InvalidDBCode
		public const string RptSv_Sys_User_Create_InvalidAreaCode = "ErridnInventory.RptSv_Sys_User_Create_InvalidAreaCode"; //// //RptSv_Sys_User_Create_InvalidAreaCode
		public const string RptSv_Sys_User_Create_InvalidUserNick = "ErridnInventory.RptSv_Sys_User_Create_InvalidUserNick"; //// //RptSv_Sys_User_Create_InvalidUserNick
		public const string RptSv_Sys_User_Create_InvalidUserName = "ErridnInventory.RptSv_Sys_User_Create_InvalidUserName"; //// //RptSv_Sys_User_Create_InvalidUserName
		public const string RptSv_Sys_User_Create_InvalidUserPassword = "ErridnInventory.RptSv_Sys_User_Create_InvalidUserPassword"; //// //RptSv_Sys_User_Create_InvalidUserPassword
		public const string RptSv_Sys_User_Create_InvalidFlagDLAdmin = "ErridnInventory.RptSv_Sys_User_Create_InvalidFlagDLAdmin"; //// //RptSv_Sys_User_Create_InvalidFlagDLAdmin
		public const string RptSv_Sys_User_Create_InvalidFlagSysAdmin = "ErridnInventory.RptSv_Sys_User_Create_InvalidFlagSysAdmin"; //// //RptSv_Sys_User_Create_InvalidFlagSysAdmin
		public const string RptSv_Sys_User_Create_InvalidFlagDBAdmin = "ErridnInventory.RptSv_Sys_User_Create_InvalidFlagDBAdmin"; //// //RptSv_Sys_User_Create_InvalidFlagDBAdmin

		// RptSv_Sys_User_Update:
		public const string RptSv_Sys_User_Update = "ErridnInventory.RptSv_Sys_User_Update"; //// //RptSv_Sys_User_Update
		public const string RptSv_Sys_User_Update_InvalidUserCode = "ErridnInventory.RptSv_Sys_User_Update_InvalidUserCode"; //// //RptSv_Sys_User_Update_InvalidUserCode
		public const string RptSv_Sys_User_Update_InvalidDBCode = "ErridnInventory.RptSv_Sys_User_Update_InvalidDBCode"; //// //RptSv_Sys_User_Update_InvalidDBCode
		public const string RptSv_Sys_User_Update_InvalidAreaCode = "ErridnInventory.RptSv_Sys_User_Update_InvalidAreaCode"; //// //RptSv_Sys_User_Update_InvalidAreaCode
		public const string RptSv_Sys_User_Update_InvalidUserName = "ErridnInventory.RptSv_Sys_User_Update_InvalidUserName"; //// //RptSv_Sys_User_Update_InvalidUserName
		public const string RptSv_Sys_User_Update_InvalidUserPassword = "ErridnInventory.RptSv_Sys_User_Update_InvalidUserPassword"; //// //RptSv_Sys_User_Update_InvalidUserPassword
		public const string RptSv_Sys_User_Update_InvalidFlagSysAdmin = "ErridnInventory.RptSv_Sys_User_Update_InvalidFlagSysAdmin"; //// //RptSv_Sys_User_Update_InvalidFlagSysAdmin
		public const string RptSv_Sys_User_Update_InvalidFlagDBAdmin = "ErridnInventory.RptSv_Sys_User_Update_InvalidFlagDBAdmin"; //// //RptSv_Sys_User_Update_InvalidFlagDBAdmin

		// RptSv_Sys_User_Delete:
		public const string RptSv_Sys_User_Delete = "ErridnInventory.RptSv_Sys_User_Delete"; //// //RptSv_Sys_User_Delete

        #endregion

        #region // RptSv_Sys_Object:
        //RptSv_Sys_Object_CheckDB
        public const string RptSv_Sys_Object_CheckDB_ObjectCodeNotFound = "ErridnInventory.RptSv_Sys_Object_CheckDB_ObjectCodeNotFound"; //// //RptSv_Sys_Object_CheckDB_ObjectCodeNotFound
        public const string RptSv_Sys_Object_CheckDB_ObjectCodeExist = "ErridnInventory.RptSv_Sys_Object_CheckDB_ObjectCodeExist"; //// // RptSv_Sys_Object_CheckDB_ObjectCodeExist
        public const string RptSv_Sys_Object_CheckDB_FlagActiveNotMatched = "ErridnInventory.RptSv_Sys_Object_CheckDB_FlagActiveNotMatched";//// // ErridnInventory.RptSv_Sys_Object_CheckDB_FlagActiveNotMatched
        //RptSv_Sys_Object_Get
        public const string RptSv_Sys_Object_Get = "ErridnInventory.RptSv_Sys_Object_Get";//// //RptSv_Sys_Object_Get
        #endregion

        #region // RptSv_OS_Inos_Package:
        // RptSv_OS_Inos_Package_Get:
        public const string RptSv_OS_Inos_Package_Get = "ErridnInventory.RptSv_OS_Inos_Package_Get"; //// //RptSv_OS_Inos_Package_Get

		// WAS_RptSv_OS_Inos_Package_Get:
		public const string WAS_RptSv_OS_Inos_Package_Get = "ErridnInventory.WAS_RptSv_OS_Inos_Package_Get"; //// //WAS_RptSv_OS_Inos_Package_Get
		#endregion

		#region // Inos:
		// Inos_OrgService_DeleteUser:
		public const string Inos_OrgService_DeleteUser = "ErridnInventory.Inos_OrgService_DeleteUser"; //// //Inos_OrgService_DeleteUser
		#endregion

		#region // Mst_NNT:
		// Mst_NNT_Calc:
		public const string Mst_NNT_Calc = "ErridnInventory.Mst_NNT_Calc"; //// //Mst_NNT_Calc
		public const string Mst_NNT_Calc_InvalidInosCreateUser = "ErridnInventory.Mst_NNT_Calc_InvalidInosCreateUser"; //// //Mst_NNT_Calc_InvalidInosCreateUser
		public const string Mst_NNT_Calc_InvalidInosCreateOrg = "ErridnInventory.Mst_NNT_Calc_InvalidInosCreateOrg"; //// //Mst_NNT_Calc_InvalidInosCreateOrg
		public const string Mst_NNT_Calc_InvalidRptSvMstNNtAdd = "ErridnInventory.Mst_NNT_Calc_InvalidRptSvMstNNtAdd"; //// //Mst_NNT_Calc_InvalidRptSvMstNNtAdd
		public const string Mst_NNT_Calc_InvalidInosCreateOrder = "ErridnInventory.Mst_NNT_Calc_InvalidInosCreateOrder"; //// //Mst_NNT_Calc_InvalidInosCreateOrder
		public const string Mst_NNT_Calc_InvalidInosCreateOrder_DiscountCodeNotFound = "ErridnInventory.Mst_NNT_Calc_InvalidInosCreateOrder_DiscountCodeNotFound"; //// //Mst_NNT_Calc_InvalidInosCreateOrder_DiscountCodeNotFound
		public const string Mst_NNT_Calc_InvalidInosCreateOrder_InvalidDiscountStatus = "ErridnInventory.Mst_NNT_Calc_InvalidInosCreateOrder_InvalidDiscountStatus"; //// //Mst_NNT_Calc_InvalidInosCreateOrder_InvalidDiscountStatus
		#endregion

		#region // Inv_InventoryBalanceSerial:
		// RptSv_Inv_InventoryBalanceSerial_Get:
		public const string RptSv_Inv_InventoryBalanceSerial_Get = "ErridnInventory.RptSv_Inv_InventoryBalanceSerial_Get"; //// //RptSv_Inv_InventoryBalanceSerial_Get

		// WAS_RptSv_Inv_InventoryBalanceSerial_Get:
		public const string WAS_RptSv_Inv_InventoryBalanceSerial_Get = "ErridnInventory.WAS_RptSv_Inv_InventoryBalanceSerial_Get"; //// //WAS_RptSv_Inv_InventoryBalanceSerial_Get
		#endregion


		#region // RptSv_Rpt_Inv_InventoryBalanceSerialForSearch:
		// RptSv_Rpt_Inv_InventoryBalanceSerialForSearch:
		public const string RptSv_Rpt_Inv_InventoryBalanceSerialForSearch = "ErridnInventory.RptSv_Rpt_Inv_InventoryBalanceSerialForSearch"; //// //RptSv_Rpt_Inv_InventoryBalanceSerialForSearch

		// WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearchForSearch:
		public const string WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearchForSearch = "ErridnInventory.WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearchForSearch"; //// //WAS_RptSv_Rpt_Inv_InventoryBalanceSerialForSearchForSearch
		#endregion
	}
}
