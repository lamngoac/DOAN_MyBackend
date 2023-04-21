using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class AccountController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<SysUser> Login(string username, string password)
        {
            try
            {
                var sysUser = new SysUser() { 
                    Code = username,
                    Password = password,
                };
                return Success<SysUser>(sysUser);
            }
            catch (Exception ex)
            {
                return Error<SysUser>(ex);
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<List<SysUser>> MyLogin(SysUser objsysuser)
        {
            try
            {
                //var lstSysUserCur = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUser>>(objsysuser);
                var lstSysUserCur = new List<SysUser>() { objsysuser };
                var lstSysUser = new List<SysUser>() {
                    new SysUser()
                    {
                        Code = "cso3",
                        Password = "123456",
                        FullName = "CSO3",
                    },
                };
                return Success<List<SysUser>>(lstSysUser);
            }
            catch (Exception ex)
            {
                return Error<List<SysUser>>(ex);
            }
        }

		[AcceptVerbs("POST")]
		public ServiceResult<List<SysUser>> MyLogin1(string objsysuer)
		{
			try
			{
				var lstSysUserInput = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUser>>(objsysuer);

				var lstSysUser = new List<SysUser>()
				{
					new SysUser(){
						Code = "cso3",
						Password="123456",
						FullName = "CSO3",
					},
				};
				return Success<List<SysUser>>(lstSysUser);
			}
			catch (Exception ex)
			{
				return Error<List<SysUser>>(ex);
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<SysUserRT> WebAPILogin(SysUser objsysuser)
		{
			try
			{
				//var lstSysUserCur = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUser>>(objsysuser);
				var objSysUserRT = new SysUserRT();

				objSysUserRT.SysUserLst = new List<SysUser>();
				objSysUserRT.c_K_DT_SysInfoLst = new List<c_K_DT_SysInfo>();

				//var lstSysUser = new List<SysUser>() {
				//	new SysUser()
				//	{
				//		Code = "cso3",
				//		Password = "123456",
				//		FullName = "CSO3",
				//	},
				//};

				var objSysUser = new SysUser() {
					Code = objsysuser.Code,
					Password = objsysuser.Password,
					FullName = objsysuser.FullName
				};


				objSysUserRT.SysUserLst.Add(objSysUser);

				////
				var objc_K_DT_SysInfo = new c_K_DT_SysInfo()
				{
					ErrorCode = "Invalid Error"
				};

				objSysUserRT.c_K_DT_SysInfoLst.Add(objc_K_DT_SysInfo);

				return Success<SysUserRT>(objSysUserRT);
			}
			catch (Exception ex)
			{
				return Error<SysUserRT>(ex);
			}
		}

		[AcceptVerbs("POST")]
		public ServiceResult<SysUserRT> WebAPILogin1(SysUserRT objsysuser)
		{
			try
			{
				//var lstSysUserCur = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUser>>(objsysuser);
				var objSysUserRT = new SysUserRT();

				objSysUserRT.SysUserLst = new List<SysUser>();

				//var lstSysUser = new List<SysUser>() {
				//	new SysUser()
				//	{
				//		Code = "cso3",
				//		Password = "123456",
				//		FullName = "CSO3",
				//	},
				//};

				//var objSysUser = new SysUser()
				//{
				//	Code = objsysuser.Code,
				//	Password = objsysuser.Password,
				//	FullName = objsysuser.FullName
				//};


				//objSysUserRT.SysUserLst.Add(objSysUser);

				return Success<SysUserRT>(objSysUserRT);
			}
			catch (Exception ex)
			{
				return Error<SysUserRT>(ex);
			}
		}


		//[AcceptVerbs("POST")]
		//public ServiceResult<SysUserRT> WebAPI_Sys_User_Get(SysUser objsysuser)
		//{
		//	try
		//	{
		//		//var lstSysUserCur = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SysUser>>(objsysuser);
		//		var objSysUserRT = new SysUserRT();

		//		var objSysUserRQ = new SysUserRQ();

		//		objSysUserRQ.Tid = DateTime.Now.ToString("yyyyMMdd.HHmmss.ffffff");
		//		objSysUserRQ.UserCode = "SYSADMIN";
		//		objSysUserRQ.UserPassword = "1";
		//		objSysUserRQ.Ft_RecordStart = "0";
		//		objSysUserRQ.Ft_RecordCount = "12345600";
		//		objSysUserRQ.Ft_WhereClause = "";
		//		objSysUserRQ.Rt_Cols_Sys_User = "*";
		//		objSysUserRQ.Rt_Cols_Sys_UserInGroup = "*";

		//		objSysUserRT = _biz.WebAPI_Sys_User_Get(objSysUserRQ);

		//		return Success<SysUserRT>(objSysUserRT);
		//	}
		//	catch (Exception ex)
		//	{
		//		return Error<SysUserRT>(ex);
		//	}
		//}
	}
}
