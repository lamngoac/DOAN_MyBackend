using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_Inv_InventoryBalance_ExtendUI : Rpt_Inv_InventoryBalance_Extend
    {
        public object ListInvCode { get; set; }

        public List<Rpt_Inv_InventoryBalance_ExtendUI> ListRpt_Inv_InventoryBalanceExtendUIChild { get; set; }
    }
}
