using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_CustomField : WARQBase
    {
        public string Rt_Cols_Invoice_CustomField { get; set; }

        public Invoice_CustomField Invoice_CustomField { get; set; }

        public List<Invoice_CustomField> Lst_Invoice_CustomField { get; set; }
        
    }
}
