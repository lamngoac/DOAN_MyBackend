using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_InvF_InventoryOutFG : WARTBase
    {
        public List<InvF_InventoryOutFG> Lst_InvF_InventoryOutFG { get; set; }

        public List<InvF_InventoryOutFGDtl> Lst_InvF_InventoryOutFGDtl { get; set; }

        public List<InvF_InventoryOutFGInstSerial> Lst_InvF_InventoryOutFGInstSerial { get; set; }
    }
}
