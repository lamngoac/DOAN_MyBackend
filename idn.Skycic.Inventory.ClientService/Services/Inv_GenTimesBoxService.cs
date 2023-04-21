using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Inv_GenTimesBoxService : ClientServiceBase<Inv_GenTimesBox>
    {
        public static Inv_GenTimesBoxService Instance
        {
            get
            {
                return GetInstance<Inv_GenTimesBoxService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvGenTimesBox";
            }
        }
        public RT_Inv_GenTimesBox WA_Inv_GenTimesBox_Get(RQ_Inv_GenTimesBox objRQ_Inv_GenTimesBox, string addressAPIs)
        {
            var result = PostData<RT_Inv_GenTimesBox, RQ_Inv_GenTimesBox>("InvGenTimesBox", "WA_Inv_GenTimesBox_Get", new { }, objRQ_Inv_GenTimesBox, addressAPIs);
            return result;
        }
        public RT_Inv_GenTimesBox WA_Inv_GenTimesBox_Add(RQ_Inv_GenTimesBox objRQ_Inv_GenTimesBox, string addressAPIs)
        {
            var result = PostData<RT_Inv_GenTimesBox, RQ_Inv_GenTimesBox>("InvGenTimesBox", "WA_Inv_GenTimesBox_Add", new { }, objRQ_Inv_GenTimesBox, addressAPIs);
            return result;
        }
    }
}
