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
    public class OS_PrdCenter_Mst_BrandService : ClientServiceBase<OS_PrdCenter_Mst_BrandService>
    {
        public static OS_PrdCenter_Mst_BrandService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_BrandService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstBrand";
            }
        }

        public RT_OS_PrdCenter_Mst_Brand WA_OS_PrdCenter_Mst_Brand_Get(RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Brand, RQ_OS_PrdCenter_Mst_Brand>(TConst.UrlType.UrlPrdCenter, "MstBrand", "WA_Mst_Brand_Get", new { }, objRQ_OS_PrdCenter_Mst_Brand);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Brand WA_OS_PrdCenter_Mst_Brand_Create(RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Brand, RQ_OS_PrdCenter_Mst_Brand>(TConst.UrlType.UrlPrdCenter, "MstBrand", "WA_Mst_Brand_Create", new { }, objRQ_OS_PrdCenter_Mst_Brand);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Brand WA_OS_PrdCenter_Mst_Brand_Update(RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Brand, RQ_OS_PrdCenter_Mst_Brand>(TConst.UrlType.UrlPrdCenter, "MstBrand", "WA_Mst_Brand_Update", new { }, objRQ_OS_PrdCenter_Mst_Brand);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Brand WA_OS_PrdCenter_Mst_Brand_Delete(RQ_OS_PrdCenter_Mst_Brand objRQ_OS_PrdCenter_Mst_Brand)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Brand, RQ_OS_PrdCenter_Mst_Brand>(TConst.UrlType.UrlPrdCenter, "MstBrand", "WA_Mst_Brand_Delete", new { }, objRQ_OS_PrdCenter_Mst_Brand);
            return result;
        }
    }
}
