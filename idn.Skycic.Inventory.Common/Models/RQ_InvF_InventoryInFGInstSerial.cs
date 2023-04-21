using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_InventoryInFGInstSerial : WARQBase
    {
        public string Rt_Cols_InvF_InventoryInFGInstSerial { get; set; }
        public InvF_InventoryInFGInstSerial InvF_InventoryInFGInstSerial { get; set; }
    }
}
