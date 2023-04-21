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
    public class OS_Inbrand_Inv_InventoryVerifiedIDService : ClientServiceBase<OS_Inbrand_Inv_InventoryVerifiedIDService>
    {
        public static OS_Inbrand_Inv_InventoryVerifiedIDService Instance
        {
            get
            {
                return GetInstance<OS_Inbrand_Inv_InventoryVerifiedIDService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvInventoryVerifiedID";
            }
        }

        public RT_OS_InBrand_Inv_InventoryVerifiedID WA_OS_InBrand_Inv_InventoryVerifiedID_AddMulti_ForGenSaveHistIn(string strNetWorkUrl, RQ_OS_InBrand_Inv_InventoryVerifiedID objRQ_OS_InBrand_Inv_InventoryVerifiedID)
        {
            var result = MstSvRoute_PostData<RT_OS_InBrand_Inv_InventoryVerifiedID, RQ_OS_InBrand_Inv_InventoryVerifiedID>(strNetWorkUrl, "Seq", "WA_Inv_InventoryVerifiedID_AddMulti_ForGenSaveHistIn", new { }, objRQ_OS_InBrand_Inv_InventoryVerifiedID);
            return result;
        }

        public RT_OS_InBrand_Inv_InventoryVerifiedID WA_OS_Inbrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn(string strNetWorkUrl, RQ_OS_InBrand_Inv_InventoryVerifiedID objRQ_OS_InBrand_Inv_InventoryVerifiedID)
        {

            //strNetWorkUrl = @"http://localhost:1800/";
            var result = MstSvRoute_PostData<RT_OS_InBrand_Inv_InventoryVerifiedID, RQ_OS_InBrand_Inv_InventoryVerifiedID>(strNetWorkUrl, "InvInventoryVerifiedID", "WA_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn", new { }, objRQ_OS_InBrand_Inv_InventoryVerifiedID);
            return result;
        }

        public RT_OS_InBrand_Inv_InventoryVerifiedID_OutInv WA_OS_InBrand_Inv_InventoryVerifiedID_OutInv(string strNetWorkUrl, RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv)
        {

            //strNetWorkUrl = @"http://localhost:1800/";
            var result = MstSvRoute_PostData<RT_OS_InBrand_Inv_InventoryVerifiedID_OutInv, RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv>(strNetWorkUrl, "InvInventoryVerifiedID", "WA_Inv_InventoryVerifiedID_OutInv", new { }, RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv);
            return result;
        }


        public RT_OS_InBrand_Inv_InventoryGenInv WA_OS_InBrand_Inv_InventoryGenInv_CheckListDB(string strNetWorkUrl, RQ_OS_InBrand_Inv_InventoryGenInv objRQ_OS_InBrand_Inv_InventoryGenInv)
        {

            //strNetWorkUrl = @"http://localhost:1800/";
            var result = MstSvRoute_PostData<RT_OS_InBrand_Inv_InventoryGenInv, RQ_OS_InBrand_Inv_InventoryGenInv>(strNetWorkUrl, "InvInventoryGenID", "WA_Inv_InventoryGenInv_CheckListDB", new { }, objRQ_OS_InBrand_Inv_InventoryGenInv);
            return result;
        }
    }
}
