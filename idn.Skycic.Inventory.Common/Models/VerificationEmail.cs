using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace idn.Skycic.Inventory.Common.Models
{
    public class VerificationEmail
    {
        public string email { get; set; }

        public string emailSubject { get; set; }

        public string emailTemplate { get; set; }

       public string code { get; set; }
    }
}
