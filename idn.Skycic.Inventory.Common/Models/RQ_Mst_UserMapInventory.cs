using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_UserMapInventory : WARQBase
    {
        public string Rt_Cols_Mst_UserMapInventory { get; set; }
        public object InvCode { get; set; }

        public Mst_UserMapInventory Mst_UserMapInventory { get; set; }

        public List<Mst_UserMapInventory> Lst_Mst_UserMapInventory { get; set; }

    }
}
