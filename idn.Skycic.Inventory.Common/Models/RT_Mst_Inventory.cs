using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Mst_Inventory : WARTBase
    {
        public List<Mst_Inventory> Lst_Mst_Inventory { get; set; }
        public List<Mst_UserMapInventory> Lst_Mst_UserMapInventory { get; set; }
    }
}
