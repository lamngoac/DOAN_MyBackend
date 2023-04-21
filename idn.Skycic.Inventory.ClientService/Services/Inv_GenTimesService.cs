using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_GenTimesService : ClientServiceBase<Inv_GenTimes>
    {
        public static Inv_GenTimesService Instance
        {
            get
            {
                return GetInstance<Inv_GenTimesService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvGenTimes";
            }
        }

        public RT_Inv_GenTimes WA_Inv_GenTimes_Get(RQ_Inv_GenTimes objRQ_Inv_GenTimes, string addressAPIs)
        {
            var result = PostData<RT_Inv_GenTimes, RQ_Inv_GenTimes>("InvGenTimes", "WA_Inv_GenTimes_Get", new { }, objRQ_Inv_GenTimes, addressAPIs);
            return result;
        }

        public RT_Inv_GenTimes WA_Inv_GenTimes_Add(RQ_Inv_GenTimes objRQ_Inv_GenTimes, string addressAPIs)
        {
            var result = PostData<RT_Inv_GenTimes, RQ_Inv_GenTimes>("InvGenTimes", "WA_Inv_GenTimes_Add", new { }, objRQ_Inv_GenTimes, addressAPIs);
            return result;
        }
    }
}
