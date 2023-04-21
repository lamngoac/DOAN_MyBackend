using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class SeqService : ClientServiceBase<RT_Seq_ObjCode>
    {
        public static SeqService Instance
        {
            get
            {
                return GetInstance<SeqService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "Seq";
            }
        }

        public RT_Seq_ObjCode WA_Seq_GenObjCode_Get(RQ_Seq_ObjCode objRQ_Seq_ObjCode, string addressAPIs)
        {
            var result = PostData<RT_Seq_ObjCode, RQ_Seq_ObjCode>(ApiControllerName, "WA_Seq_GenObjCode_Get", new { }, objRQ_Seq_ObjCode, addressAPIs);
            return result;
        }

        public RT_Seq_Common WA_Seq_Common_Get(RQ_Seq_Common objRQ_Seq_Common, string addressAPIs)
        {
            var result = PostData<RT_Seq_Common, RQ_Seq_Common>(ApiControllerName, "WA_Seq_Common_Get", new { }, objRQ_Seq_Common, addressAPIs);
            return result;
        }
    }
}
