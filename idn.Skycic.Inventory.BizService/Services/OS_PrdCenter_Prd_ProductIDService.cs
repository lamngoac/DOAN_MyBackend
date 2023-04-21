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
    public class OS_PrdCenter_Prd_ProductIDService : ClientServiceBase<OS_PrdCenter_Prd_ProductIDService>
    {
        public static OS_PrdCenter_Prd_ProductIDService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Prd_ProductIDService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "PrdProductID";
            }
        }

        public RT_OS_PrdCenter_Prd_ProductID WA_OS_PrdCenter_Prd_ProductID_Get(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_ProductID, RQ_OS_PrdCenter_Prd_ProductID>(TConst.UrlType.UrlPrdCenter, "PrdProductID", "WA_Prd_ProductID_Get", new { }, objRQ_OS_PrdCenter_Prd_ProductID);
            return result;
        }

        public RT_OS_PrdCenter_Prd_ProductID WA_OS_PrdCenter_Prd_ProductID_Create(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_ProductID, RQ_OS_PrdCenter_Prd_ProductID>(TConst.UrlType.UrlPrdCenter, "PrdProductID", "WA_Prd_ProductID_Create", new { }, objRQ_OS_PrdCenter_Prd_ProductID);
            return result;
        }

        public RT_OS_PrdCenter_Prd_ProductID WA_OS_PrdCenter_Prd_ProductID_Update(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_ProductID, RQ_OS_PrdCenter_Prd_ProductID>(TConst.UrlType.UrlPrdCenter, "PrdProductID", "WA_Prd_ProductID_Update", new { }, objRQ_OS_PrdCenter_Prd_ProductID);
            return result;
        }

        public RT_OS_PrdCenter_Prd_ProductID WA_OS_PrdCenter_Prd_ProductID_Delete(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_ProductID, RQ_OS_PrdCenter_Prd_ProductID>(TConst.UrlType.UrlPrdCenter, "PrdProductID", "WA_Prd_ProductID_Delete", new { }, objRQ_OS_PrdCenter_Prd_ProductID);
            return result;
        }

        public RT_OS_PrdCenter_Prd_ProductID WA_OS_PrdCenter_Prd_ProductID_CheckListDB(RQ_OS_PrdCenter_Prd_ProductID objRQ_OS_PrdCenter_Prd_ProductID)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_ProductID, RQ_OS_PrdCenter_Prd_ProductID>(TConst.UrlType.UrlPrdCenter, "PrdProductID", "WA_Prd_ProductID_CheckListDB", new { }, objRQ_OS_PrdCenter_Prd_ProductID);
            return result;
        }
    }
}
