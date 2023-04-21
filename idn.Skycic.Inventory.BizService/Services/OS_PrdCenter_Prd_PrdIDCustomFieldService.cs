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
    public class OS_PrdCenter_Prd_PrdIDCustomFieldService : ClientServiceBase<OS_PrdCenter_Prd_PrdIDCustomFieldService>
    {
        public static OS_PrdCenter_Prd_PrdIDCustomFieldService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_Prd_PrdIDCustomFieldService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "PrdPrdIDCustomField";
            }
        }

        public RT_OS_PrdCenter_Prd_PrdIDCustomField WA_OS_PrdCenter_Prd_PrdIDCustomField_Get(RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_PrdIDCustomField, RQ_OS_PrdCenter_Prd_PrdIDCustomField>(TConst.UrlType.UrlPrdCenter, "PrdPrdIDCustomField", "WA_Prd_PrdIDCustomField_Get", new { }, objRQ_OS_PrdCenter_Prd_PrdIDCustomField);
            return result;
        }

        public RT_OS_PrdCenter_Prd_PrdIDCustomField WA_OS_PrdCenter_Prd_PrdIDCustomField_Create(RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_PrdIDCustomField, RQ_OS_PrdCenter_Prd_PrdIDCustomField>(TConst.UrlType.UrlPrdCenter, "PrdPrdIDCustomField", "WA_Prd_PrdIDCustomField_Create", new { }, objRQ_OS_PrdCenter_Prd_PrdIDCustomField);
            return result;
        }

        public RT_OS_PrdCenter_Prd_PrdIDCustomField WA_OS_PrdCenter_Prd_PrdIDCustomField_Update(RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_PrdIDCustomField, RQ_OS_PrdCenter_Prd_PrdIDCustomField>(TConst.UrlType.UrlPrdCenter, "PrdPrdIDCustomField", "WA_Prd_PrdIDCustomField_Update", new { }, objRQ_OS_PrdCenter_Prd_PrdIDCustomField);
            return result;
        }

        public RT_OS_PrdCenter_Prd_PrdIDCustomField WA_OS_PrdCenter_Prd_PrdIDCustomField_Delete(RQ_OS_PrdCenter_Prd_PrdIDCustomField objRQ_OS_PrdCenter_Prd_PrdIDCustomField)
        {
            var result = PostData<RT_OS_PrdCenter_Prd_PrdIDCustomField, RQ_OS_PrdCenter_Prd_PrdIDCustomField>(TConst.UrlType.UrlPrdCenter, "PrdPrdIDCustomField", "WA_Prd_PrdIDCustomField_Delete", new { }, objRQ_OS_PrdCenter_Prd_PrdIDCustomField);
            return result;
        }
    }
}
