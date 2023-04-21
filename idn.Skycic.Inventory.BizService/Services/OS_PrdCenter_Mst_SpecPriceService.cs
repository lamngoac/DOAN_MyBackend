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
    public class OS_PrdCenter_Mst_SpecPriceService : ClientServiceBase<OS_PrdCenter_Mst_SpecPrice>
    {
        public static OS_PrdCenter_Mst_SpecPriceService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecPriceService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecPrice";
            }
        }

        public RT_OS_PrdCenter_Mst_SpecPrice WA_OS_PrdCenter_Mst_SpecPrice_Get(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecPrice, RQ_OS_PrdCenter_Mst_SpecPrice>(TConst.UrlType.UrlPrdCenter, "MstSpecPrice", "WA_Mst_SpecPrice_Get", new { }, objRQ_OS_PrdCenter_Mst_SpecPrice);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecPrice WA_OS_PrdCenter_Mst_SpecPrice_Create(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecPrice, RQ_OS_PrdCenter_Mst_SpecPrice>(TConst.UrlType.UrlPrdCenter, "MstSpecPrice", "WA_Mst_SpecPrice_Create", new { }, objRQ_OS_PrdCenter_Mst_SpecPrice);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecPrice WA_OS_PrdCenter_Mst_SpecPrice_Update(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecPrice, RQ_OS_PrdCenter_Mst_SpecPrice>(TConst.UrlType.UrlPrdCenter, "MstSpecPrice", "WA_Mst_SpecPrice_Update", new { }, objRQ_OS_PrdCenter_Mst_SpecPrice);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecPrice WA_OS_PrdCenter_Mst_SpecPrice_Delete(RQ_OS_PrdCenter_Mst_SpecPrice objRQ_OS_PrdCenter_Mst_SpecPrice)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecPrice, RQ_OS_PrdCenter_Mst_SpecPrice>(TConst.UrlType.UrlPrdCenter, "MstSpecPrice", "WA_Mst_SpecPrice_Delete", new { }, objRQ_OS_PrdCenter_Mst_SpecPrice);
            return result;
        }
    }
}
