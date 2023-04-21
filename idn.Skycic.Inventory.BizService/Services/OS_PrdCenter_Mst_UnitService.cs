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
    public class OS_PrdCenter_Mst_UnitService : ClientServiceBase<OS_PrdCenter_Mst_UnitService>
    {
        public static OS_PrdCenter_Mst_UnitService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_UnitService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstUnit";
            }
        }

        public RT_OS_PrdCenter_Mst_Unit WA_OS_PrdCenter_Mst_Unit_Get(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Unit, RQ_OS_PrdCenter_Mst_Unit>(TConst.UrlType.UrlPrdCenter, "MstUnit", "WA_Mst_Unit_Get", new { }, objRQ_OS_PrdCenter_Mst_Unit);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Unit WA_OS_PrdCenter_Mst_Unit_Create(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Unit, RQ_OS_PrdCenter_Mst_Unit>(TConst.UrlType.UrlPrdCenter, "MstUnit", "WA_Mst_Unit_Create", new { }, objRQ_OS_PrdCenter_Mst_Unit);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Unit WA_OS_PrdCenter_Mst_Unit_Update(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Unit, RQ_OS_PrdCenter_Mst_Unit>(TConst.UrlType.UrlPrdCenter, "MstUnit", "WA_Mst_Unit_Update", new { }, objRQ_OS_PrdCenter_Mst_Unit);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Unit WA_OS_PrdCenter_Mst_Unit_Delete(RQ_OS_PrdCenter_Mst_Unit objRQ_OS_PrdCenter_Mst_Unit)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Unit, RQ_OS_PrdCenter_Mst_Unit>(TConst.UrlType.UrlPrdCenter, "MstUnit", "WA_Mst_Unit_Delete", new { }, objRQ_OS_PrdCenter_Mst_Unit);
            return result;
        }
    }
}
