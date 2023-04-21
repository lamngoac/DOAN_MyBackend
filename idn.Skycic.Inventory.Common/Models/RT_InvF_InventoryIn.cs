using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_InvF_InventoryIn : WARTBase
    {
        public List<InvF_InventoryIn> Lst_InvF_InventoryIn { get; set; }

        public List<InvF_InventoryInDtl> Lst_InvF_InventoryInDtl { get; set; }

        public List<InvF_InventoryInInstLot> Lst_InvF_InventoryInInstLot { get; set; }

        public List<InvF_InventoryInInstSerial> Lst_InvF_InventoryInInstSerial { get; set; }

        public List<InvF_InventoryInQR> Lst_InvF_InventoryInQR { get; set; }
    }
}
