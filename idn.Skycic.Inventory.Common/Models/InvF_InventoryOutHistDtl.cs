using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutHistDtl
    {
        public object IF_InvOutHistNo { get; set; }

        public object PartCode { get; set; }

        public object NetworkID { get; set; }

        public object Qty { get; set; }

        public object IF_InvOutHistStatusDtl { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}
