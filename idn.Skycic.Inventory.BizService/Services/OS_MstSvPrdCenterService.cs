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
    public class OS_MstSvPrdCenterService : ClientServiceBase<OS_MstSvPrdCenter_MstSv_Sys_User>
    {
        public static OS_MstSvPrdCenterService Instance
        {
            get
            {
                return GetInstance<OS_MstSvPrdCenterService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }

        public RT_OS_MstSvPrdCentrer_MstSv_Sys_User WA_OS_MstPrdCenter_MstSv_Sys_User_Login(RQ_OS_MstSvPrdCenter_MstSv_Sys_User objRQ_MstSv_Sys_User)
        {
            var result = PostData<RT_OS_MstSvPrdCentrer_MstSv_Sys_User, RQ_OS_MstSvPrdCenter_MstSv_Sys_User>(TConst.UrlType.UrlMstSvPrdCenter, "MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User);
            return result;
        }
		public RT_MstSv_Sys_User WA_OS_MstPrdCenter_MstSv_Sys_User_Login(string strUrl, RQ_MstSv_Sys_User objRQ_MstSv_Sys_User)
		{
			var result = MstSvRoute_PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>(strUrl, "MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User);
			return result;
		}

		public RT_Mst_Org WA_OS_MstPrdCenter_Mst_Org_Create(string strUrl, RQ_Mst_Org objRQ_Mst_Org)
		{
			var result = MstSvRoute_PostData<RT_Mst_Org, RQ_Mst_Org>(strUrl, "MstOrg", "WA_Mst_Org_Create", new { }, objRQ_Mst_Org);
			return result;
		}

        public RT_Mst_Product WA_OS_MstPrdCenter_Mst_Product_UpdateMaster(string strUrl, RQ_Mst_Product objRQ_Mst_Product)
        {
            var result = MstSvRoute_PostData<RT_Mst_Product, RQ_Mst_Product>(strUrl, "MstProduct", "WA_Mst_Product_UpdateMaster", new { }, objRQ_Mst_Product);
            return result;
        }

        public RT_Mst_Customer WA_OS_MstPrdCenter_Mst_Customer_UpdateDTimeUsed(string strUrl, RQ_Mst_Customer objRQ_Mst_Customer)
        {
            var result = MstSvRoute_PostData<RT_Mst_Customer, RQ_Mst_Customer>(strUrl, "MstCustomer", "WA_Mst_Customer_UpdateDtimeUsed", new { }, objRQ_Mst_Customer);
            return result;
        }
    }
}
