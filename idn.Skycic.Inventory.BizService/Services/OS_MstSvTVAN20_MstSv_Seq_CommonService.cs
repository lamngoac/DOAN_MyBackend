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
    public class OS_MstSvTVAN20_MstSv_Seq_CommonService : ClientServiceBase<OS_MstSvTVAN20_MstSv_Seq_Common>
    {
        public static OS_MstSvTVAN20_MstSv_Seq_CommonService Instance
        {
            get
            {
                return GetInstance<OS_MstSvTVAN20_MstSv_Seq_CommonService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }

        public RT_OS_MstSvTVAN20_MstSv_Seq_Common WA_OS_MstSvTVAN20_Seq_Common(RQ_OS_MstSvTVAN20_MstSv_Seq_Common objRQ_OS_MstSvTVAN20_Seq_Common)
        {
            var result = PostData<RT_OS_MstSvTVAN20_MstSv_Seq_Common, RQ_OS_MstSvTVAN20_MstSv_Seq_Common>(TConst.UrlType.UrlMstSvSolution, "MasterServer", "WA_MstSv_Seq_Common_Get", new { }, objRQ_OS_MstSvTVAN20_Seq_Common);
            return result;
        }
    }
}
