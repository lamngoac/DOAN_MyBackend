using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Utils
{
	public class CMyDataList
	{
		#region // Constructors and Destructors:
		private const string c_strKey_DataSetName_CMyDataSet = "c_K_DS";
		private const string c_strKey_DataTableName_SysInfo = "c_K_DT_SysInfo";
		private const string c_strKey_DataTableName_SysError = "c_K_DT_SysError";
		private const string c_strKey_DataTableName_SysWarning = "c_K_DT_SysWarning";
		private const string c_strKey_ColumnName_Tid = "Tid";
		private const string c_strKey_ColumnName_DigitalSignature = "DigitalSignature";
		private const string c_strKey_ColumnName_ErrorCode = "ErrorCode";
		private const string c_strKey_ColumnName_FlagWarning = "FlagWarning";
		private const string c_strKey_ColumnName_Remark = "Remark";
		private const string c_strKey_ColumnName_FlagCompress = "FlagCompress";
		private const string c_strKey_ColumnName_FlagEncrypt = "FlagEncrypt";
		private const string c_strKey_ColumnName_ParamCode = "PCode";
		private const string c_strKey_ColumnName_ParamValue = "PVal";
		private const string c_strKey_ColumnName_WarningCode = "WCode";
		private const string c_strKey_ColumnName_WarningValue = "WVal";
		#endregion

		#region // Public members:
		public static c_K_DT_Sys CProcessMyDS(
			DataSet mdsFinal
			)
		{
			#region // Init:
			////
			c_K_DT_Sys objc_K_DT_Sys = new c_K_DT_Sys();
			objc_K_DT_Sys.Lst_c_K_DT_SysInfo = new List<c_K_DT_SysInfo>();
			objc_K_DT_Sys.Lst_c_K_DT_SysError = new List<c_K_DT_SysError>();
			objc_K_DT_Sys.Lst_c_K_DT_SysWarning = new List<c_K_DT_SysWarning>();
			DataTable dt_c_K_DT_SysInfo = mdsFinal.Tables["c_K_DT_SysInfo"].Copy();
			DataTable dt_c_K_DT_SysError = mdsFinal.Tables["c_K_DT_SysError"].Copy();
			DataTable dt_c_K_DT_SysWarning = mdsFinal.Tables["c_K_DT_SysWarning"].Copy();
			List<c_K_DT_SysInfo> lst_c_K_DT_SysInfo = new List<c_K_DT_SysInfo>();			
			List<c_K_DT_SysError> lst_c_K_DT_SysError = new List<c_K_DT_SysError>();			
			List<c_K_DT_SysWarning> lst_c_K_DT_SysWarning = new List<c_K_DT_SysWarning>();
			#endregion

			#region // Process:
			////
			lst_c_K_DT_SysInfo = DataTableCmUtils.DtToList<c_K_DT_SysInfo>(dt_c_K_DT_SysInfo);
			lst_c_K_DT_SysError = DataTableCmUtils.DtToList<c_K_DT_SysError>(dt_c_K_DT_SysError);
			lst_c_K_DT_SysWarning = DataTableCmUtils.DtToList<c_K_DT_SysWarning>(dt_c_K_DT_SysWarning);

			////
			objc_K_DT_Sys.Lst_c_K_DT_SysInfo = lst_c_K_DT_SysInfo;
			objc_K_DT_Sys.Lst_c_K_DT_SysError = lst_c_K_DT_SysError;
			objc_K_DT_Sys.Lst_c_K_DT_SysWarning = lst_c_K_DT_SysWarning;

			return objc_K_DT_Sys;
			#endregion
		}

		//public static void SetErrorCode(c_)
		//{

		//}
		#endregion
	}

	public class CDataListProcessExc
	{
		public static void Process(
			ref c_K_DT_Sys objc_K_DT_Sys
			, Exception exc
			, string strErrorCode
			, params object[] arrobjErrorParams
			)
		{

		}
	}

	public class CMyDataListException
	{
		#region // Constructors and Destructors:
		private const string c_strKeyID = "CMyException";
		private const string c_strKeyErrorCode = "MyErrorCode";
		private const string c_strKeyData = "MyData";
		#endregion

		#region // Public members:
		public static Exception Raise(
			string strErrorCode
			, Exception excInner
			, params object[] arrobjParamsCouple
			)
		{
			// Init:
			Exception exc = new Exception(strErrorCode, excInner);

			// Return Good:
			return exc;
		}
		public static bool IsMyException(Exception exc)
		{
			if (exc == null) return false;
			return (string.Equals(exc.Data[c_strKeyID], c_strKeyID));
		}
		#endregion
	}
}
