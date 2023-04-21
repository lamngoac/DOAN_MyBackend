using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class SendMailUI
    {
        public string MST { get; set; }
        public string InvoiceNo { get; set; }
        public string FormNo { get; set; }
        public string Sign { get; set; }
        public string CustomerNNTName { get; set; }

        public string DealerName { get; set; }

        public string NNTFullName { get; set; }
        public string mp_ProvinceName { get; set; }       
        public string InvoiceDateUTC { get; set; }
        public string InvoiceCode { get; set; }
        public string ContactName { get; set; }
        public string ContactPhone { get; set; }
        public string ContactEmail { get; set; }        
    }
}
