using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_InvoiceType2 : WARQBase
    {
        public string Rt_Cols_Mst_InvoiceType2 { get; set; }

        public Mst_InvoiceType2 Mst_InvoiceType2 { get; set; }
    }
}
