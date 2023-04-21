using idn.Skycic.Inventory.Common.Models;
using System.Collections.Generic;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryInDtlUI : InvF_InventoryInDtl
    {
        public object ProductCodeBase { get; set; }
        public object ProductCodeRoot { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object ProductType { get; set; }
        public object PlanQty { get; set; }
        public object FlagSerial { get; set; }
        public object FlagLot { get; set; }
        public object SerialNo { get; set; } // Serial
        public object ProductLotNo { get; set; } // Số LOT
        public object ProductionDate { get; set; } // Ngày sản xuất
        public object ExpiredDate { get; set; } // Ngày hết hạn

        public object InvCodeSuggest { get; set; }

        public object Idx { get; set; }
        public object InvName { get; set; }
        
        public List<Mst_Product> Lst_Mst_ProductBase { get; set; }

        public List<InvF_InventoryInDtlUI> Lst_InvF_InventoryInDtlUI { get; set; } // phục vụ import excel hoặc màn hình chi tiết;

        public List<InvF_InventoryInDtlUI> Lst_Product_InvCodeInActual { get; set; } // phục vụ cache hàng hóa - vị trí;

        public List<InvF_InventoryInDtlUI> Lst_Product_InvSerial { get; set; } 

        public List<InvF_InventoryInDtlUI> Lst_Product_InvLot { get; set; } 
    }
}
