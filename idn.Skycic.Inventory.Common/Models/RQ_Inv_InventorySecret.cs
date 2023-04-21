using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Inv_InventorySecret : WARQBase
    {
        public string Rt_Cols_Inv_InventorySecret { get; set; }

        public Inv_InventorySecret Inv_InventorySecret { get; set; }

        public List<Inv_InventorySecret> Lst_Inv_InventorySecret { get; set; }
    }
}
