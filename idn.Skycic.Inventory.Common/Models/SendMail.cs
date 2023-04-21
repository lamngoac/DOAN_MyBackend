using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SendMail
    {
        public string MG_Domain { get; set; }

        public string MG_From { get; set; }

        public string MG_HttpBasicAuthUserPwd { get; set; }

        public string MG_HttpBasicAuthUserName { get; set; }

        // //
        public string FromMail { get; set; }

        public List<string> ToMail { get; set; }
        public List<string> CcMail { get; set; }
        public List<string> BccMail { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> AttachmentFile { get; set; }

        // Client
        public string ApiKeySendMail { get; set; }
        public string ApiSendMail { get; set; }
        public string DisplayNameMailFrom { get; set; }
        public string MailFrom { get; set; }
        public string HtmlBody { get; set; }

        public string SolutionCode { get; set; }
        public string OrgId { get; set; }
    }
}
