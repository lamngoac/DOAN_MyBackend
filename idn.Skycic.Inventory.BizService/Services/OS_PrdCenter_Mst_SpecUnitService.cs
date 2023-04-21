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
    public class OS_PrdCenter_Mst_SpecUnitService : ClientServiceBase<OS_PrdCenter_Mst_SpecUnit>
    {
        public static OS_PrdCenter_Mst_SpecUnitService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecUnitService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecUnit";
            }
        }

        public RT_OS_PrdCenter_Mst_SpecUnit WA_OS_PrdCenter_Mst_SpecUnit_Get(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_Mst_SpecUnit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecUnit, RQ_OS_PrdCenter_Mst_SpecUnit>(TConst.UrlType.UrlPrdCenter, "MstSpecUnit", "WA_Mst_SpecUnit_Get", new { }, objRQ_Mst_SpecUnit);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecUnit WA_OS_PrdCenter_Mst_SpecUnit_Create(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_Mst_SpecUnit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecUnit, RQ_OS_PrdCenter_Mst_SpecUnit>(TConst.UrlType.UrlPrdCenter, "MstSpecUnit", "WA_Mst_SpecUnit_Create", new { }, objRQ_Mst_SpecUnit);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecUnit WA_OS_PrdCenter_Mst_SpecUnit_Update(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_Mst_SpecUnit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecUnit, RQ_OS_PrdCenter_Mst_SpecUnit>(TConst.UrlType.UrlPrdCenter, "MstSpecUnit", "WA_Mst_SpecUnit_Update", new { }, objRQ_Mst_SpecUnit);
            return result;
        }

        public RT_OS_PrdCenter_Mst_SpecUnit WA_OS_PrdCenter_Mst_SpecUnit_Delete(RQ_OS_PrdCenter_Mst_SpecUnit objRQ_Mst_SpecUnit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecUnit, RQ_OS_PrdCenter_Mst_SpecUnit>(TConst.UrlType.UrlPrdCenter, "MstSpecUnit", "WA_Mst_SpecUnit_Delete", new { }, objRQ_Mst_SpecUnit);
            return result;
        }
    }
}
