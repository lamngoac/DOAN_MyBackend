using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_InventoryInFG:WARQBase
    {
        public string Rt_Cols_InvF_InventoryInFG { get; set; }
        public string Rt_Cols_InvF_InventoryInFGDtl { get; set; }
        public string Rt_Cols_InvF_InventoryInFGInstSerial { get; set; }
        public InvF_InventoryInFG InvF_InventoryInFG { get; set; }
        public List<InvF_InventoryInFGDtl> Lst_InvF_InventoryInFGDtl { get; set; }
        public List<InvF_InventoryInFGInstSerial> Lst_InvF_InventoryInFGInstSerial { get; set; }
    }
}
