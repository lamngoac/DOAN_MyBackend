using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class SendMailGun
    {
        public string MailgateApiKey { get; set; }
        public string MailgateDomain { get; set; }
        public string MailgateApi { get; set; } // "http://mailgate.inos.vn/emailapi/Send"

        public string FromMail { get; set; }// mail: idn@idocnet.com
        public string From { get; set; } // ví dụ mail: idn@idocnet.com => From: idn; MailgateDomain: idocnet.com
        public List<string> ToMail { get; set; }
        public List<string> CcMail { get; set; }
        public List<string> BccMail { get; set; }
        public string DisplayNameMailFrom { get; set; }
        public string Subject { get; set; }
        public string HtmlBody { get; set; }
        public string SolutionCode { get; set; }
        public string OrgId { get; set; }
        public List<string> AttachmentFiles { get; set; }
    }
}
