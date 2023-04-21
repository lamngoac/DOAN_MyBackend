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
    public class OS_PrdCenter_Mst_SpecService : ClientServiceBase<OS_PrdCenter_Mst_Spec>
    {
        public static OS_PrdCenter_Mst_SpecService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpec";
            }
        }

        public RT_OS_PrdCenter_Mst_Spec WA_OS_PrdCenter_Mst_Spec_Get(RQ_OS_PrdCenter_Mst_Spec objRQ_Mst_Spec)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Spec, RQ_OS_PrdCenter_Mst_Spec>(TConst.UrlType.UrlPrdCenter, "MstSpec", "WA_Mst_Spec_Get", new { }, objRQ_Mst_Spec);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Spec WA_OS_PrdCenter_Mst_Spec_Add(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Spec, RQ_OS_PrdCenter_Mst_Spec>(TConst.UrlType.UrlPrdCenter, "MstSpec", "WA_Mst_Spec_Add", new { }, objRQ_OS_PrdCenter_Mst_Spec);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Spec WA_OS_PrdCenter_Mst_Spec_Upd(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Spec, RQ_OS_PrdCenter_Mst_Spec>(TConst.UrlType.UrlPrdCenter, "MstSpec", "WA_Mst_Spec_Upd", new { }, objRQ_OS_PrdCenter_Mst_Spec);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Spec WA_OS_PrdCenter_Mst_Spec_Del(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Spec, RQ_OS_PrdCenter_Mst_Spec>(TConst.UrlType.UrlPrdCenter, "MstSpec", "WA_Mst_Spec_Del", new { }, objRQ_OS_PrdCenter_Mst_Spec);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Spec WA_OS_PrdCenter_Mst_Spec_CheckListDB(RQ_OS_PrdCenter_Mst_Spec objRQ_OS_PrdCenter_Mst_Spec)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Spec, RQ_OS_PrdCenter_Mst_Spec>(TConst.UrlType.UrlPrdCenter, "MstSpec", "WA_Mst_Spec_CheckListDB", new { }, objRQ_OS_PrdCenter_Mst_Spec);
            return result;
        }

    }
}
