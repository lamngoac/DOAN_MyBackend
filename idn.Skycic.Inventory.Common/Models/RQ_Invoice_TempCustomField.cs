using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_TempCustomField : WARQBase
    {
        public string Rt_Cols_Invoice_TempCustomField { get; set; }

        public Invoice_TempCustomField Invoice_TempCustomField { get; set; }

        public List<Invoice_TempCustomField> Lst_Invoice_TempCustomField { get; set; }

    }
}
