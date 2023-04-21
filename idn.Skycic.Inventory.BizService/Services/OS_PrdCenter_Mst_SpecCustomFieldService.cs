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
    public class OS_PrdCenter_Mst_SpecCustomFieldService : ClientServiceBase<OS_PrdCenter_Mst_SpecCustomFieldService>
    {
        public static OS_PrdCenter_Mst_SpecCustomFieldService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Mst_SpecCustomFieldService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstSpecCustomField";
            }
        }

        public RT_OS_PrdCenter_Mst_SpecCustomField WA_OS_PrdCenter_Mst_SpecCustomField_Get(RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecCustomField, RQ_OS_PrdCenter_Mst_SpecCustomField>(TConst.UrlType.UrlPrdCenter, "MstSpecCustomField", "WA_Mst_SpecCustomField_Get", new { }, objRQ_OS_PrdCenter_Mst_SpecCustomField);
            return result;
        }
        
        public RT_OS_PrdCenter_Mst_SpecCustomField WA_OS_PrdCenter_Mst_SpecCustomField_Update(RQ_OS_PrdCenter_Mst_SpecCustomField objRQ_OS_PrdCenter_Mst_SpecCustomField)
        {
            var result = PostData<RT_OS_PrdCenter_Mst_SpecCustomField, RQ_OS_PrdCenter_Mst_SpecCustomField>(TConst.UrlType.UrlPrdCenter, "MstSpecCustomField", "WA_Mst_SpecCustomField_Update", new { }, objRQ_OS_PrdCenter_Mst_SpecCustomField);
            return result;
        }
        
    }
}