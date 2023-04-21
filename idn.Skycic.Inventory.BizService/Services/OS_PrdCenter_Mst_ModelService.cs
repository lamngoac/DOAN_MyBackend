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
    public class OS_PrdCenter_Mst_ModelService : ClientServiceBase<OS_PrdCenter_Mst_ModelService>
    {
        public static OS_PrdCenter_Mst_ModelService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_ModelService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstModel";
            }
        }

        public RT_OS_PrdCenter_Mst_Model WA_OS_PrdCenter_Mst_Model_Get(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Model, RQ_OS_PrdCenter_Mst_Model>(TConst.UrlType.UrlPrdCenter, "MstModel", "WA_Mst_Model_Get", new { }, objRQ_OS_PrdCenter_Mst_Model);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Model WA_OS_PrdCenter_Mst_Model_Create(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Model, RQ_OS_PrdCenter_Mst_Model>(TConst.UrlType.UrlPrdCenter, "MstModel", "WA_Mst_Model_Create", new { }, objRQ_OS_PrdCenter_Mst_Model);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Model WA_OS_PrdCenter_Mst_Model_Update(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Model, RQ_OS_PrdCenter_Mst_Model>(TConst.UrlType.UrlPrdCenter, "MstModel", "WA_Mst_Model_Update", new { }, objRQ_OS_PrdCenter_Mst_Model);
            return result;
        }

        public RT_OS_PrdCenter_Mst_Model WA_OS_PrdCenter_Mst_Model_Delete(RQ_OS_PrdCenter_Mst_Model objRQ_OS_PrdCenter_Mst_Model)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_Model, RQ_OS_PrdCenter_Mst_Model>(TConst.UrlType.UrlPrdCenter, "MstModel", "WA_Mst_Model_Delete", new { }, objRQ_OS_PrdCenter_Mst_Model);
            return result;
        }
    }
}
