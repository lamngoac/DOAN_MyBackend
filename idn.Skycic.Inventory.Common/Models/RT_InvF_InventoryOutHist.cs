using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_InvF_InventoryOutHist : WARTBase
    {
        public List<InvF_InventoryOutHist> Lst_InvF_InventoryOutHist { get; set; }

        public List<InvF_InventoryOutHistDtl> Lst_InvF_InventoryOutHistDtl { get; set; }

        public List<InvF_InventoryOutHistInstSerial> Lst_InvF_InventoryOutHistInstSerial { get; set; }
    }
}
