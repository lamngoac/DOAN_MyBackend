using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_InBrand_Inv_InventoryGenID : WARQBase
    {
		public string Rt_Cols_Inv_InventoryGenID { get; set; }

		public OS_InBrand_Inv_InventoryGenID Inv_InventoryGenID { get; set; }

		public List<OS_InBrand_Inv_InventoryGenID> Lst_Inv_InventoryGenID { get; set; }

        public string FlagExistToCheck { get; set; }

        public string FlagMapListToCheck { get; set; }

        public string FlagUsedListToCheck { get; set; }

    }
}
