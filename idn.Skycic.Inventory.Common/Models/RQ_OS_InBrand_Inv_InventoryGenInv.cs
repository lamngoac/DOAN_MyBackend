using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_InBrand_Inv_InventoryGenInv : WARQBase
    {
        public List<OS_InBrand_Inv_InventoryGenID> Lst_Inv_InventoryGenID { get; set; }

        public List<OS_InBrand_Inv_InventoryGenBox> Lst_Inv_InventoryGenBox { get; set; }

        public List<OS_InBrand_Inv_InventoryGenCarton> Lst_Inv_InventoryGenCarton { get; set; }

        public string FlagExistToCheck { get; set; }

        public string FlagMapListToCheck { get; set; }

        public string FlagUsedListToCheck { get; set; }
    }
}
