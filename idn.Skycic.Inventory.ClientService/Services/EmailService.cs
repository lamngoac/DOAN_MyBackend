using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class EmailService : ClientServiceBase<Email_BatchSendEmail>
    {
        public static EmailService Instance
        {
            get
            {
                return GetInstance<EmailService>();
            }
        }
        public override string ApiControllerName
        {
            get
            {
                return "Email";
            }
        }

        public RT_Email_BatchSendEmail WA_Email_BatchSendEmail_SaveAndSend(RQ_Email_BatchSendEmail objRQ_Email_BatchSendEmail, string addressAPIs)
        {
            var result = PostData<RT_Email_BatchSendEmail, RQ_Email_BatchSendEmail>("Email", "WA_Email_BatchSendEmail_SaveAndSend", new { }, objRQ_Email_BatchSendEmail, addressAPIs);
            return result;
        }
    }
}
