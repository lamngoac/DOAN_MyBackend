using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_MstSvTVANService : ClientServiceBase<OS_MstSvTVANService>
	{
		public static OS_MstSvTVANService Instance
		{
			get
			{
				return GetInstance<OS_MstSvTVANService>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "MasterServer";
			}
		}

		public RT_MstSv_Sys_User WA_OS_MstSvTVAN_MstSv_Sys_User_Login(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User)
		{
			var result = PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>(TConst.UrlType.UrlMstSvSolution, "MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User);
			return result;
		}

		public RT_MstSv_Sys_User WA_OS_MstSvTVAN_MstSv_Sys_User_Login(string strUrl, RQ_MstSv_Sys_User objRQ_MstSv_Sys_User)
		{
			var result = MstSvRoute_PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>(strUrl, "MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User);
			return result;
		}

		public RT_MstSv_OrgInNetwork WA_OS_MstSv_OrgInNetwork_GetOrgIDSln(string strUrl, RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
			var result =  MstSvRoute_PostData<RT_MstSv_OrgInNetwork, RQ_MstSv_OrgInNetwork>(strUrl, "MasterServer", "WA_MstSv_OrgInNetwork_GetOrgIDSln", new { }, objRQ_MstSv_OrgInNetwork);
			return result;
		}

		public RT_MstSv_OrgInNetwork WA_OS_MstSv_OrgInNetwork_GetByOrgIDSln(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
			var result = PostData<RT_MstSv_OrgInNetwork, RQ_MstSv_OrgInNetwork>(TConst.UrlType.UrlMstSvSolution, "MasterServer", "WA_MstSv_OrgInNetwork_GetByOrgIDSln", new { }, objRQ_MstSv_OrgInNetwork);
			return result;
		}

		public RT_MstSv_Mst_Network WA_OS_MstSv_Mst_Network_GetByMST(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network)
		{
			var result = PostData<RT_MstSv_Mst_Network, RQ_MstSv_Mst_Network>(TConst.UrlType.UrlMstSvSolution, "MstSvMstNetwork", "WA_MstSv_Mst_Network_GetByMST", new { }, objRQ_MstSv_Mst_Network);
			return result;
		}

        public RT_MstSv_OrgInNetwork WA_OS_MstSv_OrgInNetwork_Create(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
        {
            var result = PostData<RT_MstSv_OrgInNetwork, RQ_MstSv_OrgInNetwork>(TConst.UrlType.UrlMstSvSolution, "MstSvOrgInNetwork", "WA_MstSv_OrgInNetwork_Create", new { }, objRQ_MstSv_OrgInNetwork);
            return result;
        }

		public RT_MstSv_OrgInNetwork WA_OS_MstSv_OrgInNetwork_Create_MstSv(RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
			var result = PostData<RT_MstSv_OrgInNetwork, RQ_MstSv_OrgInNetwork>(TConst.UrlType.UrlMstSvSolution, "MstSvOrgInNetwork", "WA_MstSv_OrgInNetwork_Create_MstSv", new { }, objRQ_MstSv_OrgInNetwork);
			return result;
		}
	}
}
