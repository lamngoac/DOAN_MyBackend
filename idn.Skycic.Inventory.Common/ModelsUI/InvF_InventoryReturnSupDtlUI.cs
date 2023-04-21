using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryReturnSupDtlUI : InvF_InventoryReturnSupDtl
    {
        public object QtyTotalOK { get; set; } // Tồn kho

        public object Idx { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public List<Mst_ProductUI> lstUnitCodeUIByProduct { get; set; }


        public object ProductCodeRoot { get; set; }
        public object ProductCodeBase { get; set; }

        public object ValConvert { get; set; }
        public object ProductLotNo { get; set; }
        public object SerialNo { get; set; }


        public List<InvF_InventoryReturnSupDtlUI> Lst_Product_InvLot { get; set; }

    }
}
