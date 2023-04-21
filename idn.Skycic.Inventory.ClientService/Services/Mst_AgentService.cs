using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class Mst_AgentService : ClientServiceBase<Mst_Agent>
    {
        public static Mst_AgentService Instance
        {
            get
            {
                return GetInstance<Mst_AgentService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "MstAgent";
            }
        }

        public RT_Mst_Agent WA_Mst_Agent_Get(RQ_Mst_Agent objRQ_Mst_Agent, string addressAPIs)
        {
            var result = PostData<RT_Mst_Agent, RQ_Mst_Agent>("MstAgent", "WA_Mst_Agent_Get", new { }, objRQ_Mst_Agent, addressAPIs);
            return result;
        }

        public RT_Mst_Agent WA_Mst_Agent_Create(RQ_Mst_Agent objRQ_Mst_Agent, string addressAPIs)
        {
            var result = PostData<RT_Mst_Agent, RQ_Mst_Agent>("MstAgent", "WA_Mst_Agent_Create", new { }, objRQ_Mst_Agent, addressAPIs);
            return result;
        }

        public RT_Mst_Agent WA_Mst_Agent_Update(RQ_Mst_Agent objRQ_Mst_Agent, string addressAPIs)
        {
            var result = PostData<RT_Mst_Agent, RQ_Mst_Agent>("MstAgent", "WA_Mst_Agent_Update", new { }, objRQ_Mst_Agent, addressAPIs);
            return result;
        }

        public RT_Mst_Agent WA_Mst_Agent_Delete(RQ_Mst_Agent objRQ_Mst_Agent, string addressAPIs)
        {
            var result = PostData<RT_Mst_Agent, RQ_Mst_Agent>("MstAgent", "WA_Mst_Agent_Delete", new { }, objRQ_Mst_Agent, addressAPIs);
            return result;
        }
    }
}
