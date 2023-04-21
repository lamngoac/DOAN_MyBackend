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
    public class OS_PrdCenter_Mst_SpecType1Service : ClientServiceBase<OS_PrdCenter_Mst_SpecType1Service>
    {
        public static OS_PrdCenter_Mst_SpecType1Service Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecType1Service>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecType1";
            }
        }

        public RT_OS_PrdCenter_Mst_SpecType1 WA_OS_PrdCenter_Mst_SpecType1_Get(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType1, RQ_OS_PrdCenter_Mst_SpecType1>(TConst.UrlType.UrlPrdCenter, "MstSpecType1", "WA_Mst_SpecType1_Get", new { }, objRQ_OS_PrdCenter_Mst_SpecType1);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecType1 WA_OS_PrdCenter_Mst_SpecType1_Create(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType1, RQ_OS_PrdCenter_Mst_SpecType1>(TConst.UrlType.UrlPrdCenter, "MstSpecType1", "WA_Mst_SpecType1_Create", new { }, objRQ_OS_PrdCenter_Mst_SpecType1);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecType1 WA_OS_PrdCenter_Mst_SpecType1_Update(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType1, RQ_OS_PrdCenter_Mst_SpecType1>(TConst.UrlType.UrlPrdCenter, "MstSpecType1", "WA_Mst_SpecType1_Update", new { }, objRQ_OS_PrdCenter_Mst_SpecType1);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecType1 WA_OS_PrdCenter_Mst_SpecType1_Delete(RQ_OS_PrdCenter_Mst_SpecType1 objRQ_OS_PrdCenter_Mst_SpecType1)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecType1, RQ_OS_PrdCenter_Mst_SpecType1>(TConst.UrlType.UrlPrdCenter, "MstSpecType1", "WA_Mst_SpecType1_Delete", new { }, objRQ_OS_PrdCenter_Mst_SpecType1);
            return result;
        }
    }
}
