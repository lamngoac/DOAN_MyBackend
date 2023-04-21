using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_Inv_InventoryBalance_ByPeriod : WARQBase
    {
        //public List<Rpt_Inv_InventoryBalance_ByPeriod> Lst_Rpt_Inv_InventoryBalance_ByPeriod { get; set; }
        public List<Inv_InventoryBalance> Lst_Inv_InventoryBalance { get; set; }
    }
}
