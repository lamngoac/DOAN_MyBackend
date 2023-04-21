using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_DtlCustomField : WARQBase
    {
        public string Rt_Cols_Invoice_DtlCustomField { get; set; }

        public Invoice_DtlCustomField Invoice_DtlCustomField { get; set; }

        public List<Invoice_DtlCustomField> Lst_Invoice_DtlCustomField { get; set; }
    }
}
