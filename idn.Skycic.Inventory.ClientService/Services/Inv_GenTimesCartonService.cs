using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_GenTimesCartonService : ClientServiceBase<Inv_GenTimesCarton>
    {
        public static Inv_GenTimesCartonService Instance
        {
            get
            {
                return GetInstance<Inv_GenTimesCartonService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvGenTimesCarton";
            }
        }
        public RT_Inv_GenTimesCarton WA_Inv_GenTimesCarton_Get(RQ_Inv_GenTimesCarton objRQ_Inv_GenTimesCarton, string addressAPIs)
        {
            var result = PostData<RT_Inv_GenTimesCarton, RQ_Inv_GenTimesCarton>("InvGenTimesCarton", "WA_Inv_GenTimesCarton_Get", new { }, objRQ_Inv_GenTimesCarton, addressAPIs);
            return result;
        }
        public RT_Inv_GenTimesCarton WA_Inv_GenTimesCarton_Add(RQ_Inv_GenTimesCarton objRQ_Inv_GenTimesCarton, string addressAPIs)
        {
            var result = PostData<RT_Inv_GenTimesCarton, RQ_Inv_GenTimesCarton>("InvGenTimesCarton", "WA_Inv_GenTimesCarton_Add", new { }, objRQ_Inv_GenTimesCarton, addressAPIs);
            return result;
        }
    }
}
