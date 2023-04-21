using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_InBrand_Inv_InventoryVerifiedID : WARQBase
    {
		public OS_InBrand_Inv_InventoryVerifiedID Inv_InventoryVerifiedID { get; set; }

		public List<OS_InBrand_Inv_InventoryVerifiedID> Lst_Inv_InventoryVerifiedID { get; set; }

        public string FlagExistToCheck { get; set; }

        public string FlagMapListToCheck { get; set; }

        public string FlagUsedListToCheck { get; set; }
    }
}
