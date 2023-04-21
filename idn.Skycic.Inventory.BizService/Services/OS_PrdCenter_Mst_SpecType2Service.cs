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
    public class OS_PrdCenter_Mst_SpecType2Service : ClientServiceBase<OS_PrdCenter_Mst_SpecType2Service>
    {
        public static OS_PrdCenter_Mst_SpecType2Service Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecType2Service>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecType2";
            }
        }

        public RT_OS_PrdCenter_Mst_SpecType2 WA_OS_PrdCenter_Mst_SpecType2_Get(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType2, RQ_OS_PrdCenter_Mst_SpecType2>(TConst.UrlType.UrlPrdCenter, "MstSpecType2", "WA_Mst_SpecType2_Get", new { }, objRQ_OS_PrdCenter_Mst_SpecType2);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecType2 WA_OS_PrdCenter_Mst_SpecType2_Create(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType2, RQ_OS_PrdCenter_Mst_SpecType2>(TConst.UrlType.UrlPrdCenter, "MstSpecType2", "WA_Mst_SpecType2_Create", new { }, objRQ_OS_PrdCenter_Mst_SpecType2);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecType2 WA_OS_PrdCenter_Mst_SpecType2_Update(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType2, RQ_OS_PrdCenter_Mst_SpecType2>(TConst.UrlType.UrlPrdCenter, "MstSpecType2", "WA_Mst_SpecType2_Update", new { }, objRQ_OS_PrdCenter_Mst_SpecType2);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecType2 WA_OS_PrdCenter_Mst_SpecType2_Delete(RQ_OS_PrdCenter_Mst_SpecType2 objRQ_OS_PrdCenter_Mst_SpecType2)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType2, RQ_OS_PrdCenter_Mst_SpecType2>(TConst.UrlType.UrlPrdCenter, "MstSpecType2", "WA_Mst_SpecType2_Delete", new { }, objRQ_OS_PrdCenter_Mst_SpecType2);
            return result;
        }
    }
}
