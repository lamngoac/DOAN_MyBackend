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
    public class SerRO_InvF_TransactionService : ClientServiceBase<SerRO_InvF_TransactionService>
    {
        public static SerRO_InvF_TransactionService Instance
        {
            get
            {
                return GetInstance<SerRO_InvF_TransactionService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MasterServer";
            }
        }

        public RT_SerRO_InvF_Transaction OSVeloca_WA_SerRO_InvF_Transaction_AddQtyMix(string strUrl, RQ_SerRO_InvF_Transaction objRQ_SerRO_InvF_Transaction)
        {
            var result = MstSvRoute_PostData<RT_SerRO_InvF_Transaction, RQ_SerRO_InvF_Transaction>(strUrl, "SerROInvFTransaction", "WA_SerRO_InvF_Transaction_AddQtyMix", new { }, objRQ_SerRO_InvF_Transaction);
            return result;
        }
    }
}
