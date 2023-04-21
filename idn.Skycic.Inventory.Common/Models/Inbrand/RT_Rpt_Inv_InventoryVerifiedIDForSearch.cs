using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Inbrand
{
    public class RT_Rpt_Inv_InventoryVerifiedIDForSearch : WARTBase
    {
        public List<Rpt_Inv_InventoryVerifiedIDForSearch> Lst_Rpt_Inv_InventoryBalanceSerialForSearch { get; set; }

        public List<Rpt_InvF_InventoryOutHistInstSerialForSearch> Lst_Rpt_InvF_InventoryOutHistInstSerialForSearch { get; set; }
    }
}
