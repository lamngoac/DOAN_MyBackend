using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventoryBox : WARQBase
    {
        public string Rt_Cols_Inv_InventoryBox { get; set; }

        public string nTop { get; set; }

        public Inv_InventoryBox Inv_InventoryBox { get; set; }

        public List<Inv_InventoryBox> Lst_Inv_InventoryBox { get; set; }
    }
}
