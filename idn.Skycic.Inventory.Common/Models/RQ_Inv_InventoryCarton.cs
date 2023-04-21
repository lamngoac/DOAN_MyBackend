using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventoryCarton : WARQBase
    {
        public string Rt_Cols_Inv_InventoryCarton { get; set; }

        public string nTop { get; set; }

        public Inv_InventoryCarton Inv_InventoryCarton { get; set; }

        public List<Inv_InventoryCarton> Lst_Inv_InventoryCarton { get; set; }
    }
}
