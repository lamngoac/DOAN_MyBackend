using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_InvF_InventoryInFG : WARTBase
    {
        public List<InvF_InventoryInFG> Lst_InvF_InventoryInFG { get; set; }

        public List<InvF_InventoryInFGDtl> Lst_InvF_InventoryInFGDtl { get; set; }

        public List<InvF_InventoryInFGInstSerial> Lst_InvF_InventoryInFGInstSerial { get; set; }
    }
}
