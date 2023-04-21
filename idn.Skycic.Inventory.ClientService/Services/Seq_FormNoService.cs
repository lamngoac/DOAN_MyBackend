using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Seq_FormNoService : ClientServiceBase<Seq_FormNo>
    {
        public static Seq_FormNoService Instance
        {
            get
            {
                return GetInstance<Seq_FormNoService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Seq";
            }
        }

        public RT_Seq_FormNo WA_Seq_FormNo_Get(RQ_Seq_FormNo objRQ_Seq_FormNo, string addressAPIs)
        {
            var result = PostData<RT_Seq_FormNo, RQ_Seq_FormNo>("Seq", "WA_Seq_FormNo_Get", new { }, objRQ_Seq_FormNo, addressAPIs);
            return result;
        }        
    }
}
