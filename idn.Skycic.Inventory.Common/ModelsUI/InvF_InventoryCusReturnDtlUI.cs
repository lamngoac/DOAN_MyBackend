using idn.Skycic.Inventory.Common.Models;
using System.Collections.Generic;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryCusReturnDtlUI : InvF_InventoryCusReturnDtl
    {
        public object ProductCodeUser { get; set; }
        public object FlagCombo { get; set; }
        public object UPBuy { get; set; }
        public object QtyRoot { get; set; } //SL gốc của hh thành phần trong combo
        public object ValConvert { get; set; } //Giá trị quy đổi
        public object QtyTotalOK { get; set; } //Tồn kho
        public object InvCodeMax { get; set; } // Vị trí tồn kho lớn nhất
        public object InvCodeSuggest { get; set; } //Vị trí gợi ý(VT tồn lớn nhất, gần nhất nếu tồn = 0)
        public object RefNo { get; set; } // Số RefNo
        public object RefNoSys { get; set; } //Số RefNo Sys
        public object RefType { get; set; } //
        public object QtyOrder { get; set; } // Số lượng đặt
        public object CustomerCodeSys { get; set; } //Mã KH Sys
        public object QtyOut { get; set; } // Số lượng đã xuất (Biz chưa trả)
        public object QtyReturnRemain { get; set; } // Số lượng còn lại có thể trả lại tiếp
        public object UPCusReturn { get; set; }

        public object SerialNo { get; set; } // Serial
        public object ProductLotNo { get; set; } // Số LOT
        public object ProductionDate { get; set; } // Ngày sản xuất
        public object ExpiredDate { get; set; } // Ngày hết hạn
        public List<Mst_Product> Lst_Mst_ProductBase { get; set; }

        public List<InvF_InventoryCusReturnDtlUI> Lst_InvF_InventoryCusreturnDtlUI { get; set; } // phục vụ import excel hoặc màn hình chi tiết;

        public List<InvF_InventoryCusReturnDtlUI> Lst_Product_InvCodeInActual { get; set; } // phục vụ cache hàng hóa - vị trí;

        public List<InvF_InventoryCusReturnDtlUI> Lst_Product_InvSerial { get; set; }

        public List<InvF_InventoryCusReturnDtlUI> Lst_Product_InvLot { get; set; }
    }
}
