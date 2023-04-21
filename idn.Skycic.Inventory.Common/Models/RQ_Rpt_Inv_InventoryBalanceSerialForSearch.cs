using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_Inv_InventoryBalanceSerialForSearch : WARQBase
    {
        public string ObjectQRMix { get; set; }

        public Rpt_Inv_InventoryBalanceSerialForSearch Rpt_Inv_InventoryBalanceSerialForSearch { get; set; }
    }
}
