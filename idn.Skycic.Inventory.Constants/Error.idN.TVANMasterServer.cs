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
		#region // MstSv_Sys_Administrator:
		// MstSv_Sys_Administrator_CheckDB:
		public const string MstSv_Sys_Administrator_CheckDB_AdministratorNotFound = "ErridnInventory.MstSv_Sys_Administrator_CheckDB_AdministratorNotFound"; //// //MstSv_Sys_Administrator_CheckDB_AdministratorNotFound
		public const string MstSv_Sys_Administrator_CheckDB_AdministratorExist = "ErridnInventory.MstSv_Sys_Administrator_CheckDB_AdministratorExist"; //// //MstSv_Sys_Administrator_CheckDB_AdministratorExist
		public const string MstSv_Sys_Administrator_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_Sys_Administrator_CheckDB_FlagActiveNotMatched"; //// //MstSv_Sys_Administrator_CheckDB_FlagActiveNotMatched

		// MstSv_Sys_Administrator_CheckDeny:
		public const string MstSv_Sys_Administrator_CheckDeny = "ErridnInventory.MstSv_Sys_Administrator_CheckDeny"; //// //MstSv_Sys_Administrator_CheckDeny

		#endregion

		#region // MstSv_Sys_User:
		// MstSv_Sys_User_CheckDB:
		public const string MstSv_Sys_User_CheckDB_UserNotFound = "ErridnInventory.MstSv_Sys_User_CheckDB_UserNotFound"; //// //MstSv_Sys_User_CheckDB_UserNotFound
		public const string MstSv_Sys_User_CheckDB_UserExist = "ErridnInventory.MstSv_Sys_User_CheckDB_UserExist"; //// //MstSv_Sys_User_CheckDB_UserExist
		public const string MstSv_Sys_User_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_Sys_User_CheckDB_FlagActiveNotMatched"; //// //MstSv_Sys_User_CheckDB_FlagActiveNotMatched

		// MstSv_Sys_User_Login:
		public const string MstSv_Sys_User_Login = "ErridnInventory.MstSv_Sys_User_Login"; //// //MstSv_Sys_User_Login
		public const string MstSv_Sys_User_Login_InvalidPassword = "ErridnInventory.MstSv_Sys_User_Login_InvalidPassword"; //// //MstSv_Sys_User_Login_InvalidPassword

		// MstSv_Sys_User_CheckAuthentication_InvalidPassword:
		public const string MstSv_Sys_User_CheckAuthentication_InvalidPassword = "ErridnInventory.MstSv_Sys_User_CheckAuthentication_InvalidPassword"; //// //MstSv_Sys_User_CheckAuthentication_InvalidPassword

		// MstSv_Sys_User_GetAccessToken:
		public const string MstSv_Sys_User_GetAccessToken = "ErridnInventory.MstSv_Sys_User_GetAccessToken"; //// //MstSv_Sys_User_GetAccessToken

		// MstSv_Sys_User_Get:
		public const string MstSv_Sys_User_Get = "ErridnInventory.MstSv_Sys_User_Get"; //// //MstSv_Sys_User_Get

		// MstSv_Sys_User_Add:
		public const string MstSv_Sys_User_Add = "ErridnInventory.MstSv_Sys_User_Add"; //// //MstSv_Sys_User_Add
		public const string MstSv_Sys_User_Add_InvalidUserCode = "ErridnInventory.MstSv_Sys_User_Add_InvalidUserCode"; //// //MstSv_Sys_User_Add_InvalidUserCode
		public const string MstSv_Sys_User_Add_InvalidUserName = "ErridnInventory.MstSv_Sys_User_Add_InvalidUserName"; //// //MstSv_Sys_User_Add_InvalidUserName

		// MstSv_Sys_User_Update:
		public const string MstSv_Sys_User_Update = "ErridnInventory.MstSv_Sys_User_Update"; //// //MstSv_Sys_User_Update
		public const string MstSv_Sys_User_Update_InvalidBrandName = "ErridnInventory.MstSv_Sys_User_Update_InvalidBrandName"; //// //MstSv_Sys_User_Update_InvalidBrandName

		// MstSv_Sys_User_Delete:
		public const string MstSv_Sys_User_Delete = "ErridnInventory.MstSv_Sys_User_Delete"; //// //MstSv_Sys_User_Delete

		// WAS_MstSv_Sys_User_Get:
		public const string WAS_MstSv_Sys_User_Get = "ErridnInventory.WAS_MstSv_Sys_User_Get"; //// //WAS_MstSv_Sys_User_Get WAS_MstSv_Sys_User_Add

		// WAS_MstSv_Sys_User_Login:
		public const string WAS_MstSv_Sys_User_Login = "ErridnInventory.WAS_MstSv_Sys_User_Login"; //// //WAS_MstSv_Sys_User_Login

		// WAS_MstSv_Sys_User_GetAccessToken:
		public const string WAS_MstSv_Sys_User_GetAccessToken = "ErridnInventory.WAS_MstSv_Sys_User_GetAccessToken"; //// //WAS_MstSv_Sys_User_GetAccessToken

		// WAS_MstSv_Sys_User_Add:
		public const string WAS_MstSv_Sys_User_Add = "ErridnInventory.WAS_MstSv_Sys_User_Add"; //// //WAS_MstSv_Sys_User_Add

		// WAS_MstSv_Sys_User_Update:
		public const string WAS_MstSv_Sys_User_Update = "ErridnInventory.WAS_MstSv_Sys_User_Update"; //// //WAS_MstSv_Sys_User_Update

		// WAS_MstSv_Sys_User_Delete:
		public const string WAS_MstSv_Sys_User_Delete = "ErridnInventory.WAS_MstSv_Sys_User_Delete"; //// //WAS_MstSv_Sys_User_Delete
		#endregion

		#region // MstSv_Mst_Network:
		// MstSv_Mst_Network_CheckDB:
		public const string MstSv_Mst_Network_CheckDB_NetworkNotFound = "ErridnInventory.MstSv_Mst_Network_CheckDB_NetworkNotFound"; //// //MstSv_Mst_Network_CheckDB_NetworkNotFound
		public const string MstSv_Mst_Network_CheckDB_NetworkExist = "ErridnInventory.MstSv_Mst_Network_CheckDB_NetworkExist"; //// //MstSv_Mst_Network_CheckDB_NetworkExist
		public const string MstSv_Mst_Network_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_Mst_Network_CheckDB_FlagActiveNotMatched"; //// //MstSv_Mst_Network_CheckDB_FlagActiveNotMatched

		// MstSv_Mst_Network_Get:
		public const string MstSv_Mst_Network_Get = "ErridnInventory.MstSv_Mst_Network_Get"; //// //MstSv_Mst_Network_Get

		// MstSv_Mst_Network_Get:
		public const string MstSv_Mst_Network_GetByMST = "ErridnInventory.MstSv_Mst_Network_GetByMST"; //// //MstSv_Mst_Network_GetByMST
		public const string MstSv_Mst_Network_GetByMST_NetworkNotFound = "ErridnInventory.MstSv_Mst_Network_GetByMST_NetworkNotFound"; //// //MstSv_Mst_Network_GetByMST_NetworkNotFound

		// WAS_MstSv_Mst_Network_Get:
		public const string WAS_MstSv_Mst_Network_Get = "ErridnInventory.WAS_MstSv_Mst_Network_Get"; //// //WAS_MstSv_Mst_Network_Get

		// WAS_MstSv_Mst_Network_GetByMST:
		public const string WAS_MstSv_Mst_Network_GetByMST = "ErridnInventory.WAS_MstSv_Mst_Network_GetByMST"; //// //WAS_MstSv_Mst_Network_GetByMST

		// MstSv_Mst_Network_Create:
		public const string MstSv_Mst_Network_Create = "ErridnInventory.MstSv_Mst_Network_Create"; //// //MstSv_Mst_Network_Create
		public const string MstSv_Mst_Network_Create_InvalidNetworkID = "ErridnInventory.MstSv_Mst_Network_Create_InvalidNetworkID"; //// //MstSv_Mst_Network_Create_InvalidNetworkID
		public const string MstSv_Mst_Network_Create_InvalidNetworkName = "ErridnInventory.MstSv_Mst_Network_Create_InvalidNetworkName"; //// //MstSv_Mst_Network_Create_InvalidNetworkName

		// WAS_MstSv_Mst_Network_Create:
		public const string WAS_MstSv_Mst_Network_Create = "ErridnInventory.WAS_MstSv_Mst_Network_Create"; //// //WAS_MstSv_Mst_Network_Create

		// MstSv_Mst_Network_Update:
		public const string MstSv_Mst_Network_Update = "ErridnInventory.MstSv_Mst_Network_Update"; //// //MstSv_Mst_Network_Update
		public const string MstSv_Mst_Network_Update_InvalidNetworkName = "ErridnInventory.MstSv_Mst_Network_Update_InvalidNetworkName"; //// //MstSv_Mst_Network_Update_InvalidNetworkName

		// WAS_MstSv_Mst_Network_Update:
		public const string WAS_MstSv_Mst_Network_Update = "ErridnInventory.WAS_MstSv_Mst_Network_Update"; //// //WAS_MstSv_Mst_Network_Update

		// MstSv_Mst_Network_Delete:
		public const string MstSv_Mst_Network_Delete = "ErridnInventory.MstSv_Mst_Network_Delete"; //// //MstSv_Mst_Network_Delete

		// WAS_MstSv_Mst_Network_Delete:
		public const string WAS_MstSv_Mst_Network_Delete = "ErridnInventory.WAS_MstSv_Mst_Network_Delete"; //// //WAS_MstSv_Mst_Network_Delete

		// MstSv_Mst_Network_Gen:
		public const string MstSv_Mst_Network_Gen = "ErridnInventory.MstSv_Mst_Network_Gen"; //// //MstSv_Mst_Network_Gen
		public const string MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User = "ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User"; //// //MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_User
		public const string MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org = "ErridnInventory.MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org"; //// //MstSv_Mst_Network_Gen_Invalid_MstSv_Inos_Org
		public const string MstSv_Mst_Network_Gen_NetworkGenFinish = "ErridnInventory.MstSv_Mst_Network_Gen_NetworkGenFinish"; //// //MstSv_Mst_Network_Gen_NetworkGenFinish
		public const string MstSv_Mst_Network_Gen_InitFail = "ErridnInventory.MstSv_Mst_Network_Gen_InitFail"; //// //MstSv_Mst_Network_Gen_InitFail

		// RptSv_Mst_Network_InsertMQ:
		public const string RptSv_Mst_Network_InsertMQ = "ErridnInventory.RptSv_Mst_Network_InsertMQ"; //// //RptSv_Mst_Network_InsertMQ
		public const string RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_User = "ErridnInventory.RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_User"; //// //RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_User
		public const string RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_Org = "ErridnInventory.RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_Org"; //// //RptSv_Mst_Network_InsertMQ_Invalid_MstSv_Inos_Org
		public const string RptSv_Mst_Network_InsertMQ_InitFail = "ErridnInventory.RptSv_Mst_Network_InsertMQ_InitFail"; //// //RptSv_Mst_Network_InsertMQ_InitFail

		// WAS_MstSv_Mst_Network_Gen:
		public const string WAS_MstSv_Mst_Network_Gen = "ErridnInventory.WAS_MstSv_Mst_Network_Gen"; //// //WAS_MstSv_Mst_Network_Gen

		// WAS_RptSv_Mst_Network_InsertMQ:
		public const string WAS_RptSv_Mst_Network_InsertMQ = "ErridnInventory.WAS_RptSv_Mst_Network_InsertMQ"; //// //WAS_RptSv_Mst_Network_InsertMQ

		// WAS_RptSvLocal_Mst_Network_InsertMQ:
		public const string WAS_RptSvLocal_Mst_Network_InsertMQ = "ErridnInventory.WAS_RptSvLocal_Mst_Network_InsertMQ"; //// //WAS_RptSvLocal_Mst_Network_InsertMQ

		// WAS_MstSv_Mst_Network_Add:
		public const string WAS_MstSv_Mst_Network_Add = "ErridnInventory.WAS_MstSv_Mst_Network_Add"; //// //WAS_MstSv_Mst_Network_Add

		#endregion

		#region // MQ_Mst_Network:
		// MQ_Mst_Network_CheckDB:
		public const string MQ_Mst_Network_CheckDB_NetworkNotFound = "ErridnInventory.MQ_Mst_Network_CheckDB_NetworkNotFound"; //// //MQ_Mst_Network_CheckDB_NetworkNotFound
		public const string MQ_Mst_Network_CheckDB_NetworkExist = "ErridnInventory.MQ_Mst_Network_CheckDB_NetworkExist"; //// //MQ_Mst_Network_CheckDB_NetworkExist
		public const string MQ_Mst_Network_CheckDB_FlagActiveNotMatched = "ErridnInventory.MQ_Mst_Network_CheckDB_FlagActiveNotMatched"; //// //MQ_Mst_Network_CheckDB_FlagActiveNotMatched

		// MQ_Mst_Network_Get:
		public const string MQ_Mst_Network_Get = "ErridnInventory.MQ_Mst_Network_Get"; //// //MQ_Mst_Network_Get

		// MQ_Mst_Network_Get:
		public const string MQ_Mst_Network_GetByMST = "ErridnInventory.MQ_Mst_Network_GetByMST"; //// //MQ_Mst_Network_GetByMST

		// WAS_MQ_Mst_Network_Get:
		public const string WAS_MQ_Mst_Network_Get = "ErridnInventory.WAS_MQ_Mst_Network_Get"; //// //WAS_MQ_Mst_Network_Get

		// WAS_MQ_Mst_Network_GetByMST:
		public const string WAS_MQ_Mst_Network_GetByMST = "ErridnInventory.WAS_MQ_Mst_Network_GetByMST"; //// //WAS_MQ_Mst_Network_GetByMST

		// MQ_Mst_Network_Create:
		public const string MQ_Mst_Network_Create = "ErridnInventory.MQ_Mst_Network_Create"; //// //MQ_Mst_Network_Create
		public const string MQ_Mst_Network_Create_InvalidNetworkID = "ErridnInventory.MQ_Mst_Network_Create_InvalidNetworkID"; //// //MQ_Mst_Network_Create_InvalidNetworkID
		public const string MQ_Mst_Network_Create_InvalidNetworkName = "ErridnInventory.MQ_Mst_Network_Create_InvalidNetworkName"; //// //MQ_Mst_Network_Create_InvalidNetworkName

		// WAS_MQ_Mst_Network_Create:
		public const string WAS_MQ_Mst_Network_Create = "ErridnInventory.WAS_MQ_Mst_Network_Create"; //// //WAS_MQ_Mst_Network_Create

		// MQ_Mst_Network_Update:
		public const string MQ_Mst_Network_Update = "ErridnInventory.MQ_Mst_Network_Update"; //// //MQ_Mst_Network_Update
		public const string MQ_Mst_Network_Update_InvalidNetworkName = "ErridnInventory.MQ_Mst_Network_Update_InvalidNetworkName"; //// //MQ_Mst_Network_Update_InvalidNetworkName

		// WAS_MQ_Mst_Network_Update:
		public const string WAS_MQ_Mst_Network_Update = "ErridnInventory.WAS_MQ_Mst_Network_Update"; //// //WAS_MQ_Mst_Network_Update

		// MQ_Mst_Network_Delete:
		public const string MQ_Mst_Network_Delete = "ErridnInventory.MQ_Mst_Network_Delete"; //// //MQ_Mst_Network_Delete

		// WAS_MQ_Mst_Network_Delete:
		public const string WAS_MQ_Mst_Network_Delete = "ErridnInventory.WAS_MQ_Mst_Network_Delete"; //// //WAS_MQ_Mst_Network_Delete

		// MQ_Mst_Network_Gen:
		public const string MQ_Mst_Network_Gen = "ErridnInventory.MQ_Mst_Network_Gen"; //// //MQ_Mst_Network_Gen
		public const string MQ_Mst_Network_Gen_Invalid_MstSv_Inos_User = "ErridnInventory.MQ_Mst_Network_Gen_Invalid_MstSv_Inos_User"; //// //MQ_Mst_Network_Gen_Invalid_MstSv_Inos_User
		public const string MQ_Mst_Network_Gen_Invalid_MstSv_Inos_Org = "ErridnInventory.MQ_Mst_Network_Gen_Invalid_MstSv_Inos_Org"; //// //MQ_Mst_Network_Gen_Invalid_MstSv_Inos_Org
		public const string MQ_Mst_Network_Gen_InitFail = "ErridnInventory.MQ_Mst_Network_Gen_InitFail"; //// //MQ_Mst_Network_Gen_InitFail

		// WAS_MQ_Mst_Network_Gen:
		public const string WAS_MQ_Mst_Network_Gen = "ErridnInventory.WAS_MQ_Mst_Network_Gen"; //// //WAS_MQ_Mst_Network_Gen
		#endregion

		#region // MstSv_Sys_UserInNetWork:
		// MstSv_Sys_UserInNetWork_CheckDB:
		public const string MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkNotFound = "ErridnInventory.MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkNotFound"; //// //MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkNotFound
		public const string MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkExist = "ErridnInventory.MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkExist"; //// //MstSv_Sys_UserInNetWork_CheckDB_UserInNetWorkExist
		public const string MstSv_Sys_UserInNetWork_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_Sys_UserInNetWork_CheckDB_FlagActiveNotMatched"; //// //MstSv_Sys_UserInNetWork_CheckDB_FlagActiveNotMatched

		#endregion

		#region // MstSv_Inos_User:
		// MstSv_Inos_User_CheckDB:
		public const string MstSv_Inos_User_CheckDB_UserNotFound = "ErridnInventory.MstSv_Inos_User_CheckDB_UserNotFound"; //// //MstSv_Inos_User_CheckDB_UserNotFound
		public const string MstSv_Inos_User_CheckDB_UserExist = "ErridnInventory.MstSv_Inos_User_CheckDB_UserExist"; //// //MstSv_Inos_User_CheckDB_UserExist
		public const string MstSv_Inos_User_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_Inos_User_CheckDB_FlagActiveNotMatched"; //// //MstSv_Inos_User_CheckDB_FlagActiveNotMatched

		// MstSv_Inos_User_Get:
		public const string MstSv_Inos_User_Get = "ErridnInventory.MstSv_Inos_User_Get"; //// //MstSv_Inos_User_Get

		// WAS_MstSv_Inos_User_Get:
		public const string WAS_MstSv_Inos_User_Get = "ErridnInventory.WAS_MstSv_Inos_User_Get"; //// //WAS_MstSv_Inos_User_Get

		// MstSv_Inos_User_Add:
		public const string MstSv_Inos_User_Add = "ErridnInventory.MstSv_Inos_User_Add"; //// //MstSv_Inos_User_Add
		public const string MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblNotFound = "ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblNotFound"; //// //MstSv_Inos_User_Add_Input_MstSv_Inos_OrgTblNotFound
		public const string MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblNotFound = "ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblNotFound"; //// //MstSv_Inos_User_Add_Input_MstSv_Inos_OrgUserTblNotFound
		public const string MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblNotFound = "ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblNotFound"; //// //MstSv_Inos_User_Add_Input_MstSv_Inos_PackageTblNotFound
		public const string MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblNotFound = "ErridnInventory.MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblNotFound"; //// //MstSv_Inos_User_Add_Input_MstSv_Inos_OrgInviteTblNotFound

		// MstSv_Inos_User_Activate:
		public const string MstSv_Inos_User_Activate = "ErridnInventory.MstSv_Inos_User_Activate"; //// //MstSv_Inos_User_Activate

		// WAS_MstSv_Inos_User_Activate:
		public const string WAS_MstSv_Inos_User_Activate = "ErridnInventory.WAS_MstSv_Inos_User_Activate"; //// //WAS_MstSv_Inos_User_Activate

		// MstSv_Inos_User_Build:
		public const string MstSv_Inos_User_Build = "ErridnInventory.MstSv_Inos_User_Build"; //// //MstSv_Inos_User_Build
		public const string MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblNotFound = "ErridnInventory.MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblNotFound"; //// //MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblNotFound
		public const string MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblInvalid = "ErridnInventory.MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblInvalid"; //// //MstSv_Inos_User_Build_Input_MstSv_Inos_PackageTblInvalid

		// MstSv_Inos_User_Create:
		public const string MstSv_Inos_User_Create = "ErridnInventory.MstSv_Inos_User_Create"; //// //MstSv_Inos_User_Create
		public const string MstSv_Inos_User_Create_InvalidNetworkID = "ErridnInventory.MstSv_Inos_User_Create_InvalidNetworkID"; //// //MstSv_Inos_User_Create_InvalidNetworkID
		public const string MstSv_Inos_User_Create_InvalidNetworkName = "ErridnInventory.MstSv_Inos_User_Create_InvalidNetworkName"; //// //MstSv_Inos_User_Create_InvalidNetworkName

		// WAS_MstSv_Inos_User_Create:
		public const string WAS_MstSv_Inos_User_Create = "ErridnInventory.WAS_MstSv_Inos_User_Create"; //// //WAS_MstSv_Inos_User_Create

		// MstSv_Inos_User_Update:
		public const string MstSv_Inos_User_Update = "ErridnInventory.MstSv_Inos_User_Update"; //// //MstSv_Inos_User_Update
		public const string MstSv_Inos_User_Update_InvalidNetworkName = "ErridnInventory.MstSv_Inos_User_Update_InvalidNetworkName"; //// //MstSv_Inos_User_Update_InvalidNetworkName

		// WAS_MstSv_Inos_User_Update:
		public const string WAS_MstSv_Inos_User_Update = "ErridnInventory.WAS_MstSv_Inos_User_Update"; //// //WAS_MstSv_Inos_User_Update

		// MstSv_Inos_User_Delete:
		public const string MstSv_Inos_User_Delete = "ErridnInventory.MstSv_Inos_User_Delete"; //// //MstSv_Inos_User_Delete

		// WAS_MstSv_Inos_User_Delete:
		public const string WAS_MstSv_Inos_User_Delete = "ErridnInventory.WAS_MstSv_Inos_User_Delete"; //// //WAS_MstSv_Inos_User_Delete
		#endregion

		#region // MstSv_Inos_Org:
		// MstSv_Inos_Org_CheckDB:
		public const string MstSv_Inos_Org_CheckDB_OrgNotFound = "ErridnInventory.MstSv_Inos_Org_CheckDB_OrgNotFound"; //// //MstSv_Inos_Org_CheckDB_OrgNotFound
		public const string MstSv_Inos_Org_CheckDB_OrgExist = "ErridnInventory.MstSv_Inos_Org_CheckDB_OrgExist"; //// //MstSv_Inos_Org_CheckDB_OrgExist
		public const string MstSv_Inos_Org_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_Inos_Org_CheckDB_FlagActiveNotMatched"; //// //MstSv_Inos_Org_CheckDB_FlagActiveNotMatched

		// MstSv_Inos_Org_Get:
		public const string MstSv_Inos_Org_Get = "ErridnInventory.MstSv_Inos_Org_Get"; //// //MstSv_Inos_Org_Get

		// WAS_MstSv_Inos_Org_Get:
		public const string WAS_MstSv_Inos_Org_Get = "ErridnInventory.WAS_MstSv_Inos_Org_Get"; //// //WAS_MstSv_Inos_Org_Get

		// MstSv_Inos_Org_Add:
		public const string MstSv_Inos_Org_Add = "ErridnInventory.MstSv_Inos_Org_Add"; //// //MstSv_Inos_Org_Add
		public const string MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgTblNotFound = "ErridnInventory.MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgTblNotFound"; //// //MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgTblNotFound
		public const string MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgUserTblNotFound = "ErridnInventory.MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgUserTblNotFound"; //// //MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgUserTblNotFound
		public const string MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgInviteTblNotFound = "ErridnInventory.MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgInviteTblNotFound"; //// //MstSv_Inos_Org_Add_Input_MstSv_Inos_OrgInviteTblNotFound

		// MstSv_Inos_Org_Activate:
		public const string MstSv_Inos_Org_Activate = "ErridnInventory.MstSv_Inos_Org_Activate"; //// //MstSv_Inos_Org_Activate

		// WAS_MstSv_Inos_Org_Activate:
		public const string WAS_MstSv_Inos_Org_Activate = "ErridnInventory.WAS_MstSv_Inos_Org_Activate"; //// //WAS_MstSv_Inos_Org_Activate

		// MstSv_Inos_Org_BuildAndCreate:
		public const string MstSv_Inos_Org_BuildAndCreate = "ErridnInventory.MstSv_Inos_Org_BuildAndCreate"; //// //MstSv_Inos_Org_BuildAndCreate

		// WAS_MstSv_Inos_Org_BuildAndCreate:
		public const string WAS_MstSv_Inos_Org_BuildAndCreate = "ErridnInventory.WAS_MstSv_Inos_Org_BuildAndCreate"; //// //WAS_MstSv_Inos_Org_BuildAndCreate

		// MstSv_Inos_Org_Create:
		public const string MstSv_Inos_Org_Create = "ErridnInventory.MstSv_Inos_Org_Create"; //// //MstSv_Inos_Org_Create
		public const string MstSv_Inos_Org_Create_InvalidNetworkID = "ErridnInventory.MstSv_Inos_Org_Create_InvalidNetworkID"; //// //MstSv_Inos_Org_Create_InvalidNetworkID
		public const string MstSv_Inos_Org_Create_InvalidNetworkName = "ErridnInventory.MstSv_Inos_Org_Create_InvalidNetworkName"; //// //MstSv_Inos_Org_Create_InvalidNetworkName

		// WAS_MstSv_Inos_Org_Create:
		public const string WAS_MstSv_Inos_Org_Create = "ErridnInventory.WAS_MstSv_Inos_Org_Create"; //// //WAS_MstSv_Inos_Org_Create

		// MstSv_Inos_Org_Update:
		public const string MstSv_Inos_Org_Update = "ErridnInventory.MstSv_Inos_Org_Update"; //// //MstSv_Inos_Org_Update
		public const string MstSv_Inos_Org_Update_InvalidNetworkName = "ErridnInventory.MstSv_Inos_Org_Update_InvalidNetworkName"; //// //MstSv_Inos_Org_Update_InvalidNetworkName

		// WAS_MstSv_Inos_Org_Update:
		public const string WAS_MstSv_Inos_Org_Update = "ErridnInventory.WAS_MstSv_Inos_Org_Update"; //// //WAS_MstSv_Inos_Org_Update

		// MstSv_Inos_Org_Delete:
		public const string MstSv_Inos_Org_Delete = "ErridnInventory.MstSv_Inos_Org_Delete"; //// //MstSv_Inos_Org_Delete

		// WAS_MstSv_Inos_Org_Delete:
		public const string WAS_MstSv_Inos_Org_Delete = "ErridnInventory.WAS_MstSv_Inos_Org_Delete"; //// //WAS_MstSv_Inos_Org_Delete
        #endregion

        #region // Inos_AccountService: 
        // Inos_OrgService_GetAllBizField:
        public const string Inos_OrgService_GetAllBizField = "ErridnInventory.Inos_OrgService_GetAllBizField"; //// //Inos_OrgService_GetAllBizField

        // Inos_AccountService_Register:
        public const string Inos_AccountService_Register = "ErridnInventory.Inos_AccountService_Register"; //// //Inos_AccountService_Register

		// Inos_AccountService_Activate:
		public const string Inos_AccountService_Activate = "ErridnInventory.Inos_AccountService_Activate"; //// //Inos_AccountService_Activate

		// Inos_AccountService_EditProfile:
		public const string Inos_AccountService_EditProfile = "ErridnInventory.Inos_AccountService_EditProfile"; //// //Inos_AccountService_EditProfile

		
		// Inos_OrgService_GetAllBizType:
		public const string Inos_OrgService_GetAllBizType = "ErridnInventory.Inos_OrgService_GetAllBizType"; //// //Inos_OrgService_GetAllBizTypeInos_OrgService_GetAllBizType

        // Inos_LicService_GetOrgSolutionModules:
        public const string Inos_LicService_GetOrgSolutionModules = "ErridnInventory.Inos_LicService_GetOrgSolutionModules"; //// //Inos_LicService_GetOrgSolutionModules

		// Inos_LicService_GetOrgSolutionModules:
		public const string Inos_LicService_GetCurrentUserLicense = "ErridnInventory.Inos_LicService_GetCurrentUserLicense"; //// //Inos_LicService_GetCurrentUserLicense

		// Inos_OrgService_GetMyOrgList:
		public const string Inos_OrgService_GetMyOrgList = "ErridnInventory.Inos_OrgService_GetMyOrgList"; //// //Inos_OrgService_GetMyOrgList

		// Inos_OrgService_AddInvite:
		public const string Inos_OrgService_AddInvite = "ErridnInventory.Inos_OrgService_AddInvite"; //// //Inos_OrgService_AddInvite

		// Inos_AccountService_GetUser:
		public const string Inos_AccountService_GetUser = "ErridnInventory.Inos_AccountService_GetUser"; //// //Inos_AccountService_GetUser

		// Inos_AccountService_RequestToken:
		public const string Inos_AccountService_RequestToken = "ErridnInventory.Inos_AccountService_RequestToken"; //// //Inos_AccountService_RequestToken

		// Inos_LicService_GetAllPackages:
		public const string Inos_LicService_GetAllPackages = "ErridnInventory.Inos_LicService_GetAllPackages"; //// //Inos_LicService_GetAllPackages

		// Inos_LicService_GetOrgLicense:
		public const string Inos_LicService_GetOrgLicense = "ErridnInventory.Inos_LicService_GetOrgLicense"; //// //Inos_LicService_GetOrgLicense

		// Inos_LicService_AddOrgSolutionUsers:
		public const string Inos_LicService_AddOrgSolutionUsers = "ErridnInventory.Inos_LicService_AddOrgSolutionUsers"; //// //Inos_LicService_AddOrgSolutionUsers

		// Inos_LicService_RegisterPackages:
		public const string Inos_LicService_RegisterPackages = "ErridnInventory.Inos_LicService_RegisterPackages"; //// //Inos_LicService_RegisterPackages

		// Inos_AccountService_GetCurrentUser:
		public const string Inos_AccountService_GetCurrentUser = "ErridnInventory.Inos_AccountService_GetCurrentUser"; //// //Inos_AccountService_GetCurrentUser
		#endregion

		#region // OS_Inos_Package:
		// OS_Inos_Package_Get:
		public const string OS_Inos_Package_Get = "ErridnInventory.OS_Inos_Package_Get"; //// //OS_Inos_Package_Get

		// WAS_OS_Inos_Package_Get:
		public const string WAS_OS_Inos_Package_Get = "ErridnInventory.WAS_OS_Inos_Package_Get"; //// //WAS_OS_Inos_Package_Get

		// Inos_OrgService_GetOrg:
		public const string Inos_OrgService_GetOrg = "ErridnInventory.Inos_OrgService_GetOrg"; //// //Inos_OrgService_GetOrg

		#endregion

		#region // OS_Inos_OrgLicense:
		// OS_Inos_OrgLicense_GetAndSave:
		public const string OS_Inos_OrgLicense_GetAndSave = "ErridnInventory.OS_Inos_OrgLicense_GetAndSave"; //// //OS_Inos_OrgLicense_GetAndSave

        // WAS_OS_Inos_OrgLicense_GetAndSave:
        public const string WAS_OS_Inos_OrgLicense_GetAndSave = "ErridnInventory.WAS_OS_Inos_OrgLicense_GetAndSave"; //// //WAS_OS_Inos_OrgLicense_GetAndSave
        #endregion

        #region // OS_Inos: 
        // Inos_OrderService_CheckOrderStatus:
        public const string Inos_OrderService_CheckOrderStatus = "ErridnInventory.Inos_OrderService_CheckOrderStatus"; //// //Inos_OrderService_CheckOrderStatus

        // WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus:
        public const string WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus = "ErridnInventory.WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus"; //// //WAS_RptSv_OS_Inos_OrderService_CheckOrderStatus

        // WAS_OS_Inos_OrderService_CheckOrderStatus:
        public const string WAS_OS_Inos_OrderService_CheckOrderStatus = "ErridnInventory.WAS_OS_Inos_OrderService_CheckOrderStatus"; //// //WAS_OS_Inos_OrderService_CheckOrderStatus

        // Inos_OrderService_CreateOrder:
        public const string Inos_OrderService_CreateOrder = "ErridnInventory.Inos_OrderService_CreateOrder"; //// //Inos_OrderService_CreateOrder

        // WAS_OS_Inos_OrderService_CreateOrder:
        public const string WAS_OS_Inos_OrderService_CreateOrder = "ErridnInventory.WAS_OS_Inos_OrderService_CreateOrder"; //// //WAS_OS_Inos_OrderService_CreateOrder

        // WAS_RptSv_OS_Inos_OrderService_CreateOrder: 
        public const string WAS_RptSv_OS_Inos_OrderService_CreateOrder = "ErridnInventory.WAS_RptSv_OS_Inos_OrderService_CreateOrder"; //// //WAS_RptSv_OS_Inos_OrderService_CreateOrder

        // Inos_OrderService_GetDiscountCode:
        public const string Inos_OrderService_GetDiscountCode = "ErridnInventory.Inos_OrderService_GetDiscountCode"; //// //Inos_OrderService_GetDiscountCode

        // WAS_OS_Inos_OrderService_GetDiscountCode: 
        public const string WAS_OS_Inos_OrderService_GetDiscountCode = "ErridnInventory.WAS_OS_Inos_OrderService_GetDiscountCode"; //// //WAS_OS_Inos_OrderService_GetDiscountCode

        // WAS_RptSv_OS_Inos_OrderService_GetDiscountCode: 
        public const string WAS_RptSv_OS_Inos_OrderService_GetDiscountCode = "ErridnInventory.WAS_RptSv_OS_Inos_OrderService_GetDiscountCode"; //// //WAS_RptSv_OS_Inos_OrderService_GetDiscountCode

        // Inos_OrgService_CreateOrg:
        public const string Inos_OrgService_CreateOrg = "ErridnInventory.Inos_OrgService_CreateOrg"; //// //Inos_OrgService_CreateOrg

        // Inos_AccountService_CreateUser:
        public const string Inos_AccountService_CreateUser = "ErridnInventory.Inos_AccountService_CreateUser"; //// //Inos_AccountService_CreateUser

        // OS_Inos_Org_Create:
        public const string OS_Inos_Org_Create = "ErridnInventory.OS_Inos_Org_Create"; //// //OS_Inos_Org_Create

        // WAS_OS_Inos_Org_Create:
        public const string WAS_OS_Inos_Org_Create = "ErridnInventory.WAS_OS_Inos_Org_Create"; //// //WAS_OS_Inos_Org_Create

        // Inos_AccountService_SendEmailVerificationEmail:
        public const string Inos_AccountService_SendEmailVerificationEmail = "ErridnInventory.Inos_AccountService_SendEmailVerificationEmail"; //// //Inos_AccountService_SendEmailVerificationEmail

        // WAS_OS_Inos_SendEmailVerificationEmail:
        public const string WAS_OS_Inos_SendEmailVerificationEmail = "ErridnInventory.WAS_OS_Inos_SendEmailVerificationEmail"; //// //WAS_OS_Inos_SendEmailVerificationEmail

        // WAS_OS_Inos_VerifyEmail:
        public const string WAS_OS_Inos_VerifyEmail = "ErridnInventory.WAS_OS_Inos_VerifyEmail"; //// //WAS_OS_Inos_VerifyEmail

        // Inos_AccountService_VerifyEmail:
        public const string Inos_AccountService_VerifyEmail = "ErridnInventory.Inos_AccountService_VerifyEmail"; //// //Inos_AccountService_VerifyEmail

        // WAS_OS_Inos_AccountService_VerifyEmail:
        public const string WAS_OS_Inos_AccountService_VerifyEmail = "ErridnInventory.WAS_OS_Inos_AccountService_VerifyEmail"; //// //WAS_OS_Inos_AccountService_VerifyEmail

        // OS_Inos_User_Create:
        public const string OS_Inos_User_Create = "ErridnInventory.OS_Inos_User_Create"; //// //OS_Inos_User_Create

        // WAS_OS_Inos_User_Create:
        public const string WAS_OS_Inos_User_Create = "ErridnInventory.WAS_OS_Inos_User_Create"; //// //WAS_OS_Inos_User_Create

        #endregion

        #region // OS_Inos_OrgSolution:
        // OS_Inos_OrgSolution_GetAndSave:
        public const string OS_Inos_OrgSolution_GetAndSave = "ErridnInventory.OS_Inos_OrgSolution_GetAndSave"; //// //OS_Inos_OrgSolution_GetAndSave

        // WAS_OS_Inos_OrgSolution_GetAndSave:
        public const string WAS_OS_Inos_OrgSolution_GetAndSave = "ErridnInventory.WAS_OS_Inos_OrgSolution_GetAndSave"; //// //WAS_OS_Inos_OrgSolution_GetAndSave
        #endregion

        #region // OS_Inos_Org:
        // OS_Inos_Org_GetMyOrgList:
        public const string OS_Inos_Org_GetMyOrgList = "ErridnInventory.OS_Inos_Org_GetMyOrgList"; //// //OS_Inos_Org_GetMyOrgList

		// WAS_OS_Inos_Org_GetMyOrgList:
		public const string WAS_OS_Inos_Org_GetMyOrgList = "ErridnInventory.WAS_OS_Inos_Org_GetMyOrgList"; //// //WAS_OS_Inos_Org_GetMyOrgList
        
        #endregion

        #region // MstSv_OrgInNetwork:
        // MstSv_OrgInNetwork_CheckDB:
        public const string MstSv_OrgInNetwork_CheckDB_OrgInNetworkNotFound = "ErridnInventory.MstSv_OrgInNetwork_CheckDB_OrgInNetworkNotFound"; //// //MstSv_OrgInNetwork_CheckDB_OrgInNetworkNotFound
		public const string MstSv_OrgInNetwork_CheckDB_OrgInNetworkExist = "ErridnInventory.MstSv_OrgInNetwork_CheckDB_OrgInNetworkExist"; //// //MstSv_OrgInNetwork_CheckDB_OrgInNetworkExist
		public const string MstSv_OrgInNetwork_CheckDB_FlagActiveNotMatched = "ErridnInventory.MstSv_OrgInNetwork_CheckDB_FlagActiveNotMatched"; //// //MstSv_OrgInNetwork_CheckDB_FlagActiveNotMatched

		// MstSv_OrgInNetwork_GetOrgIDSln:
		public const string MstSv_OrgInNetwork_GetOrgIDSln = "ErridnInventory.MstSv_OrgInNetwork_GetOrgIDSln"; //// //MstSv_OrgInNetwork_GetOrgIDSln

        // MstSv_OrgInNetwork_Create
        public const string MstSv_OrgInNetwork_Create = "ErridnInventory.MstSv_OrgInNetwork_Create"; //// //MstSv_OrgInNetwork_Create
        public const string MstSv_OrgInNetwork_Create_InvalidNetworkID = "ErridnInventory.MstSv_OrgInNetwork_Create_InvalidNetworkID"; //// //MstSv_OrgInNetwork_Create_InvalidNetworkID
        public const string MstSv_OrgInNetwork_Create_InvalidOrgIDSln = "ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgIDSln"; //// //MstSv_OrgInNetwork_Create_InvalidOrgIDSln
        public const string MstSv_OrgInNetwork_Create_InvalidMST = "ErridnInventory.MstSv_OrgInNetwork_Create_InvalidMST"; //// //MstSv_OrgInNetwork_Create_InvalidMST
        public const string MstSv_OrgInNetwork_Create_InvalidOrgID = "ErridnInventory.MstSv_OrgInNetwork_Create_InvalidOrgID"; //// //MstSv_OrgInNetwork_Create_InvalidOrgID

        // ErridnInventory.OS_MstSv_OrgInNetwork_Create
        public const string OS_MstSv_OrgInNetwork_Create = "ErridnInventory.OS_MstSv_OrgInNetwork_Create";//// //OS_MstSv_OrgInNetwork_Create

        // WAS_MstSv_OrgInNetwork_Create:
        public const string WAS_MstSv_OrgInNetwork_Create = "ErridnInventory.WAS_MstSv_OrgInNetwork_Create"; //// //WAS_MstSv_OrgInNetwork_Create

        // WAS_MstSv_OrgInNetwork_GetOrgIDSln:
        public const string WAS_MstSv_OrgInNetwork_GetOrgIDSln = "ErridnInventory.WAS_MstSv_OrgInNetwork_GetOrgIDSln"; //// //WAS_MstSv_OrgInNetwork_GetOrgIDSln

		// MstSv_OrgInNetwork_GetByOrgIDSln:
		public const string MstSv_OrgInNetwork_GetByOrgIDSln = "ErridnInventory.MstSv_OrgInNetwork_GetByOrgIDSln"; //// //MstSv_OrgInNetwork_GetByOrgIDSln

		// WAS_MstSv_OrgInNetwork_GetByOrgIDSln:
		public const string WAS_MstSv_OrgInNetwork_GetByOrgIDSln = "ErridnInventory.WAS_MstSv_OrgInNetwork_GetByOrgIDSln"; //// //WAS_MstSv_OrgInNetwork_GetByOrgIDSln
		#endregion

		#region // Map_Network_SysOutSide:
		// Map_Network_SysOutSide_CheckDB:
		public const string Map_Network_SysOutSide_CheckDB_NetworkSysOutSideNotFound = "ErridnInventory.Map_Network_SysOutSide_CheckDB_NetworkSysOutSideNotFound"; //// //Map_Network_SysOutSide_CheckDB_NetworkSysOutSideNotFound
		public const string Map_Network_SysOutSide_CheckDB_NetworkSysOutSideExist = "ErridnInventory.Map_Network_SysOutSide_CheckDB_NetworkSysOutSideExist"; //// //Map_Network_SysOutSide_CheckDB_NetworkSysOutSideExist
		public const string Map_Network_SysOutSide_CheckDB_FlagActiveNotMatched = "ErridnInventory.Map_Network_SysOutSide_CheckDB_FlagActiveNotMatched"; //// //Map_Network_SysOutSide_CheckDB_FlagActiveNotMatched

		// Map_Network_SysOutSide_GetBySysOS:
		public const string Map_Network_SysOutSide_GetBySysOS = "ErridnInventory.Map_Network_SysOutSide_GetBySysOS"; //// //Map_Network_SysOutSide_GetBySysOS

		// WAS_Map_Network_SysOutSide_GetBySysOS:
		public const string WAS_Map_Network_SysOutSide_GetBySysOS = "ErridnInventory.WAS_Map_Network_SysOutSide_GetBySysOS"; //// //WAS_Map_Network_SysOutSide_GetBySysOS

		// WAS_Map_Network_SysOutSide_GetOrgIDSln:
		public const string WAS_Map_Network_SysOutSide_GetOrgIDSln = "ErridnInventory.WAS_Map_Network_SysOutSide_GetOrgIDSln"; //// //WAS_Map_Network_SysOutSide_GetOrgIDSln

		// Map_Network_SysOutSide_GetByOrgIDSln:
		public const string Map_Network_SysOutSide_GetByOrgIDSln = "ErridnInventory.Map_Network_SysOutSide_GetByOrgIDSln"; //// //Map_Network_SysOutSide_GetByOrgIDSln

		// WAS_Map_Network_SysOutSide_GetByOrgIDSln:
		public const string WAS_Map_Network_SysOutSide_GetByOrgIDSln = "ErridnInventory.WAS_Map_Network_SysOutSide_GetByOrgIDSln"; //// //WAS_Map_Network_SysOutSide_GetByOrgIDSln
		#endregion

		#region // Report Server:
		// RptSv_Invoice_Invoice_Get:
		public const string RptSv_Invoice_Invoice_Get = "ErridnInventory.RptSv_Invoice_Invoice_Get"; //// //RptSv_Invoice_Invoice_Get

		// WAS_RptSv_Invoice_Invoice_Get:
		public const string WAS_RptSv_Invoice_Invoice_Get = "ErridnInventory.WAS_RptSv_Invoice_Invoice_Get"; //// //WAS_RptSv_Invoice_Invoice_Get
		#endregion

		#region // OS_Sys:
		// OS_Sys_AT_3A_Invoice_Delete:
		public const string OS_Sys_AT_3A_Invoice_Delete = "ErridnInventory.OS_Sys_AT_3A_Invoice_Delete"; //// //OS_Sys_AT_3A_Invoice_Delete

		// WAS_OS_Sys_AT_3A_Invoice_Delete:
		public const string WAS_OS_Sys_AT_3A_Invoice_Delete = "ErridnInventory.WAS_OS_Sys_AT_3A_Invoice_Delete"; //// //WAS_OS_Sys_AT_3A_Invoice_Delete
		#endregion
	}
}
