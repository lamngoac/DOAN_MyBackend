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
    public class OS_PrdCenter_Mst_CurrencyExService : ClientServiceBase<OS_PrdCenter_Mst_CurrencyExService>
    {
        public static OS_PrdCenter_Mst_CurrencyExService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_CurrencyExService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstCurrencyEx";
            }
        }

        public RT_OS_PrdCenter_Mst_CurrencyEx WA_OS_PrdCenter_Mst_CurrencyEx_Get(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_CurrencyEx, RQ_OS_PrdCenter_Mst_CurrencyEx>(TConst.UrlType.UrlPrdCenter, "MstCurrencyEx", "WA_Mst_CurrencyEx_Get", new { }, objRQ_OS_PrdCenter_Mst_CurrencyEx);
            return result;
        }

        public RT_OS_PrdCenter_Mst_CurrencyEx WA_OS_PrdCenter_Mst_CurrencyEx_Create(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_CurrencyEx, RQ_OS_PrdCenter_Mst_CurrencyEx>(TConst.UrlType.UrlPrdCenter, "MstCurrencyEx", "WA_Mst_CurrencyEx_Create", new { }, objRQ_OS_PrdCenter_Mst_CurrencyEx);
            return result;
        }

        public RT_OS_PrdCenter_Mst_CurrencyEx WA_OS_PrdCenter_Mst_CurrencyEx_Update(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_CurrencyEx, RQ_OS_PrdCenter_Mst_CurrencyEx>(TConst.UrlType.UrlPrdCenter, "MstCurrencyEx", "WA_Mst_CurrencyEx_Update", new { }, objRQ_OS_PrdCenter_Mst_CurrencyEx);
            return result;
        }

        public RT_OS_PrdCenter_Mst_CurrencyEx WA_OS_PrdCenter_Mst_CurrencyEx_Delete(RQ_OS_PrdCenter_Mst_CurrencyEx objRQ_OS_PrdCenter_Mst_CurrencyEx)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_CurrencyEx, RQ_OS_PrdCenter_Mst_CurrencyEx>(TConst.UrlType.UrlPrdCenter, "MstCurrencyEx", "WA_Mst_CurrencyEx_Delete", new { }, objRQ_OS_PrdCenter_Mst_CurrencyEx);
            return result;
        }
    }
}
