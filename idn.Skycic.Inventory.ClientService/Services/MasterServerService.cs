using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class MasterServerService : ClientServiceBase<MstSv_Sys_User>
    {
        public static MasterServerService Instance
        {
            get
            {
                return GetInstance<MasterServerService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }

        //Dùng link API Report Server 
        public RT_MstSv_Inos_User WA_MstSv_Inos_User_Activate(RQ_MstSv_Inos_User objRQ_MstSv_Inos_User, string apiaddresstype)
        {
            var result = PostData<RT_MstSv_Inos_User, RQ_MstSv_Inos_User>("MasterServer", "WA_MstSv_Inos_User_Activate", new { }, objRQ_MstSv_Inos_User, apiaddresstype);
            return result;
        }

        // 1
        public RT_MstSv_Sys_User WA_MstSv_Sys_User_GetAccessToken(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string apiaddresstype)
        {
            var result = PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>("MasterServer", "WA_MstSv_Sys_User_GetAccessToken", new { }, objRQ_MstSv_Sys_User, apiaddresstype);
            return result;
        }

        // 3
        public RT_MstSv_Sys_User WA_MstSv_Sys_User_Login(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string apiaddresstype)
        {
            var result = PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>("MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User, apiaddresstype);
            return result;
        }

        // Gen network

        public RT_MstSv_Inos_Org WA_MstSv_Inos_Org_BuildAndCreate(RQ_MstSv_Inos_Org objRQ_MstSv_Inos_Org, string apiaddresstype)
        {
            var result = PostData<RT_MstSv_Inos_Org, RQ_MstSv_Inos_Org>("MasterServer", "WA_MstSv_Inos_Org_BuildAndCreate", new { }, objRQ_MstSv_Inos_Org, apiaddresstype);
            return result;
        }

        public RT_MstSv_Mst_Network WA_MstSv_Mst_Network_Gen(RQ_MstSv_Mst_Network objRQ_MstSv_Mst_Network, string apiaddresstype)
        {
            var result = PostData<RT_MstSv_Mst_Network, RQ_MstSv_Mst_Network>("MstSvMstNetwork", "WA_MstSv_Mst_Network_Gen", new { }, objRQ_MstSv_Mst_Network, apiaddresstype);
            return result;
        }

        #region["Login MasterServer_ProductCentrer dựa vào NetworkID để lấy ra link APIs của ProductCentrer"]
        /// <summary>
        /// Chú ý trùng tên hàm WA_MstSv_Sys_User_Login của masterserver nhưng khác link APIs
        /// </summary>
        /// <param name="objRQ_MstSv_Sys_User"></param>
        /// <param name="apiAddress_MasterServer_PrdCenter"></param>
        /// <returns></returns>
        public RT_MstSv_Sys_User MasterServer_PrdCenter_WA_MstSv_Sys_User_Login(RQ_MstSv_Sys_User objRQ_MstSv_Sys_User, string apiAddress_MasterServer_PrdCenter)
        {
            var result = PostData<RT_MstSv_Sys_User, RQ_MstSv_Sys_User>("MasterServer", "WA_MstSv_Sys_User_Login", new { }, objRQ_MstSv_Sys_User, apiAddress_MasterServer_PrdCenter);
            return result;
        }
        #endregion


        ////////////
        public dynamic TestPustData(dynamic objRQ_MstSv_Sys_User, string apiaddresstype)
        {
            var result = PutData<dynamic, dynamic>("report", "run", new { }, objRQ_MstSv_Sys_User, apiaddresstype);
            return result;
        }
    }
}
