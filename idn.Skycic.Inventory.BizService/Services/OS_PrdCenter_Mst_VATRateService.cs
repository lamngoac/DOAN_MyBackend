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
    public class OS_PrdCenter_Mst_VATRateService : ClientServiceBase<OS_PrdCenter_Mst_VATRateService>
    {
        public static OS_PrdCenter_Mst_VATRateService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_VATRateService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstVATRate";
            }
        }

        public RT_OS_PrdCenter_Mst_VATRate WA_OS_PrdCenter_Mst_VATRate_Get(RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_VATRate, RQ_OS_PrdCenter_Mst_VATRate>(TConst.UrlType.UrlPrdCenter, "MstVATRate", "WA_Mst_VATRate_Get", new { }, objRQ_OS_PrdCenter_Mst_VATRate);
            return result;
        }

        public RT_OS_PrdCenter_Mst_VATRate WA_OS_PrdCenter_Mst_VATRate_Create(RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_VATRate, RQ_OS_PrdCenter_Mst_VATRate>(TConst.UrlType.UrlPrdCenter, "MstVATRate", "WA_Mst_VATRate_Create", new { }, objRQ_OS_PrdCenter_Mst_VATRate);
            return result;
        }

        public RT_OS_PrdCenter_Mst_VATRate WA_OS_PrdCenter_Mst_VATRate_Update(RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_VATRate, RQ_OS_PrdCenter_Mst_VATRate>(TConst.UrlType.UrlPrdCenter, "MstVATRate", "WA_Mst_VATRate_Update", new { }, objRQ_OS_PrdCenter_Mst_VATRate);
            return result;
        }

        public RT_OS_PrdCenter_Mst_VATRate WA_OS_PrdCenter_Mst_VATRate_Delete(RQ_OS_PrdCenter_Mst_VATRate objRQ_OS_PrdCenter_Mst_VATRate)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_VATRate, RQ_OS_PrdCenter_Mst_VATRate>(TConst.UrlType.UrlPrdCenter, "MstVATRate", "WA_Mst_VATRate_Delete", new { }, objRQ_OS_PrdCenter_Mst_VATRate);
            return result;
        }
    }
}
