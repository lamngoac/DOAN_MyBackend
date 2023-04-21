using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class InvF_InvAuditService : ClientServiceBase<InvF_InvAudit>
    {
        public static InvF_InvAuditService Instance
        {
            get
            {
                return GetInstance<InvF_InvAuditService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "InvFInvAudit";
            }
        }

        public RT_InvF_InvAudit InvF_InvAudit_Get(RQ_InvF_InvAudit objRQ_InvF_InvAudit, string addressAPIs)
        {
            var result = PostData<RT_InvF_InvAudit, RQ_InvF_InvAudit>("InvFInvAudit", "WA_InvF_InvAudit_Get", new { }, objRQ_InvF_InvAudit, addressAPIs);
            return result;
        }
        public RT_InvF_InvAudit InvF_InvAudit_Save(RQ_InvF_InvAudit objRQ_InvF_InvAudit, string addressAPIs)
        {
            var result = PostData<RT_InvF_InvAudit, RQ_InvF_InvAudit>("InvFInvAudit", "WA_InvF_InvAudit_Save", new { }, objRQ_InvF_InvAudit, addressAPIs);
            return result;
        }
        public RT_InvF_InvAudit InvF_InvAudit_Appr(RQ_InvF_InvAudit objRQ_InvF_InvAudit, string addressAPIs)
        {
            var result = PostData<RT_InvF_InvAudit, RQ_InvF_InvAudit>("InvFInvAudit", "WA_InvF_InvAudit_Appr", new { }, objRQ_InvF_InvAudit, addressAPIs);
            return result;
        }
        public RT_InvF_InvAudit InvF_InvAudit_Cancel(RQ_InvF_InvAudit objRQ_InvF_InvAudit, string addressAPIs)
        {
            var result = PostData<RT_InvF_InvAudit, RQ_InvF_InvAudit>("InvFInvAudit", "WA_InvF_InvAudit_Cancel", new { }, objRQ_InvF_InvAudit, addressAPIs);
            return result;
        }
        public RT_InvF_InvAudit InvF_InvAudit_Finish(RQ_InvF_InvAudit objRQ_InvF_InvAudit, string addressAPIs)
        {
            var result = PostData<RT_InvF_InvAudit, RQ_InvF_InvAudit>("InvFInvAudit", "WA_InvF_InvAudit_Finish", new { }, objRQ_InvF_InvAudit, addressAPIs);
            return result;
        }
        public RT_InvF_InvAudit InvF_InvAuditDtl_FlagAudit(RQ_InvF_InvAudit objRQ_InvF_InvAudit, string addressAPIs) // Kiểm kê
        {
            var result = PostData<RT_InvF_InvAudit, RQ_InvF_InvAudit>("InvFInvAudit", "WA_InvF_InvAuditDtl_FlagAudit", new { }, objRQ_InvF_InvAudit, addressAPIs);
            return result;
        }
    }
}
