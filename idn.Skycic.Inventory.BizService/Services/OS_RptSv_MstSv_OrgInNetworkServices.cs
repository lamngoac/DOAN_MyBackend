using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.BizService.Services
{
	public class OS_RptSv_MstSv_OrgInNetworkServices : ClientServiceBase<OS_RptSv_MstSv_OrgInNetworkServices>
	{
		public static OS_RptSv_MstSv_OrgInNetworkServices Instance
		{
			get
			{
				return GetInstance<OS_RptSv_MstSv_OrgInNetworkServices>();
			}
		}

		public override string ApiControllerName
		{
			get
			{
				return "MstSvOrgInNetwork";
			}
		}

		public RT_MstSv_OrgInNetwork WA_OS_MstSv_OrgInNetwork_Create(string strUrl, RQ_MstSv_OrgInNetwork objRQ_MstSv_OrgInNetwork)
		{
			var result = MstSvRoute_PostData<RT_MstSv_OrgInNetwork, RQ_MstSv_OrgInNetwork>(strUrl, "MstSvOrgInNetwork", "WA_MstSv_OrgInNetwork_Create", new { }, objRQ_MstSv_OrgInNetwork);
			return result;
		}
	}
}
