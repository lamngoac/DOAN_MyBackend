using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Invoice_TempGroup : WARQBase
    {
        public string Rt_Cols_Invoice_TempGroup { get; set; }
        public string Rt_Cols_Invoice_TempGroupField { get; set; }

        public Invoice_TempGroup Invoice_TempGroup { get; set; }

        public List<Invoice_TempGroupField> Lst_Invoice_TempGroupField { get; set; }
    }
}
