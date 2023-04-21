using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    // Check SerialNumber và MST có trong hệ thống
    public class CertificateInfo
    {
        public string NetworkID { get; set; }
        public string MST { get; set; }
        
    }
    public class Certificate
    {
        public string SerialNumber { get; set; }
        public Issuer Issuer { get; set; }

        public Subject Subject { get; set; }

        public string NotBefore { get; set; }
        public string NotAfter { get; set; }
    }

    public class Issuer
    {
        public string CN { get; set; }
        public string O { get; set; }
        public string C { get; set; }
    }

    public class Subject
    {
        public string C { get; set; }
        public string CN { get; set; }
        //public string MST { get; set; }
    }
}
