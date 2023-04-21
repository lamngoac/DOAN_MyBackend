using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Invoice_license
    {
        public object MST { get; set; }

        public object NetworkID { get; set; }

        public object TotalQty { get; set; }

        public object TotalQtyIssued { get; set; }

        public object TotalQtyUsed { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object Qty { get; set; }
    }
}
