using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_InventoryOutFG : WARQBase
    {
        public string Rt_Cols_InvF_InventoryOutFG { get; set; }
        public string Rt_Cols_InvF_InventoryOutFGDtl { get; set; }
        public string Rt_Cols_InvF_InventoryOutFGInstSerial { get; set; }
        public InvF_InventoryOutFG InvF_InventoryOutFG { get; set; }
        public List<InvF_InventoryOutFGDtl> Lst_InvF_InventoryOutFGDtl { get; set; }
        public List<InvF_InventoryOutFGInstSerial> Lst_InvF_InventoryOutFGInstSerial { get; set; }
    }
}
