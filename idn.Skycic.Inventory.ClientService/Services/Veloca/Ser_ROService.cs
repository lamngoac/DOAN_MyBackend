using idn.Skycic.Inventory.Common.Models.Veloca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services.Veloca
{
    public class Ser_ROService : ClientServiceBase<Ser_RO>
    {
        public static Ser_ROService Instance
        {
            get
            {
                return GetInstance<Ser_ROService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "SerRO";
            }
        }

        public RT_Ser_RO WA_Ser_RO_Get(RQ_Ser_RO objRQ_Ser_RO, string addressAPIs)
        {
            var result = PostData<RT_Ser_RO, RQ_Ser_RO>(ApiControllerName, "WA_Ser_RO_Get", new { }, objRQ_Ser_RO, addressAPIs);
            return result;
        }

        public RT_Ser_ROProductPart WA_Ser_ROProductPart_Get(RQ_Ser_ROProductPart objRQ_Ser_ROProductPart, string addressAPIs)
        {
            var result = PostData<RT_Ser_ROProductPart, RQ_Ser_ROProductPart>("SerROProductPart", "WA_Ser_ROProductPart_Get", new { }, objRQ_Ser_ROProductPart, addressAPIs);
            return result;
        }
    }
}
