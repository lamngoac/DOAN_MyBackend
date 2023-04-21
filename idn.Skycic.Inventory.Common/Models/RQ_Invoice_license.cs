using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_license : WARQBase
    {
        public string Rt_Cols_Invoice_license { get; set; }

        public string Rt_Cols_Invoice_licenseCreHist { get; set; }

        public Invoice_license Invoice_license { get; set; }

        public Invoice_licenseCreHist Invoice_licenseCreHist { get; set; }

        public List<Invoice_licenseCreHist> Lst_Invoice_licenseCreHist { get; set; }
    }
}
