using idn.Skycic.Inventory.Common.ModelsUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InventoryOutCover
    {
        public object IF_InvOutNo { get; set; } // Số phiếu xuất

        public object InvCodeOutActual { get; set; } // Mã kho xuất

        public object ProductCodeRoot { get; set; } // mã sản phẩm gốc

        public object NetworkID { get; set; } // NetworkID

        public object Qty { get; set; } // Số lượng

        public object UPInv { get; set; } // Giá vốn

        public object UPOut { get; set; } // đơn giá xuất

        public object UPOutDesc { get; set; } // giảm giá

        public object ValOut { get; set; } // Tổng tiền hàng detail

        public object ValOutDesc { get; set; } // Tổng tiền giảm detail

        public object ValOutAfterDesc { get; set; } // Thành tiền detail (Thành tiền)

        public object UnitCode { get; set; } // Đơn vị

        public object IF_InvOutStatusDtl { get; set; } // Trạng thái phiếu xuất

        public object Remark { get; set; } // ghi chu

        public object LogLUDTimeUTC { get; set; } // Thời điểm cập nhật cuối cùng

        public object LogLUBy { get; set; } // Người cập nhật cuối cùng



        public object FlagLot { get; set; } // Cờ LOT


        public object FlagSerial { get; set; } // Cờ Serial


        public object ValConvert { get; set; } // Cờ Serial


        public object ProductType { get; set; } // 


        public object mp_root_ProductCodeBase { get; set; } // 

        public object ifiod_ProductCode { get; set; } // Mã sản phẩm - 20211005

        public object mp_root_ProductName { get; set; } //


        public object mp_root_ProductCodeUser   { get; set; } // 


        public object ListInvCodeOutActual { get; set; } //  List kho 


        public object ListInvNameOutActual { get; set; } //  List kho 


        public object QtyInv { get; set; } // Số lượng tồn


        //Lan
        public List<Mst_ProductUI> lstUnitCodeUIByProduct { get; set; } // Danh sách đơn vị quy đổi

        public object QtyTotolOK { get; set; } // Tồn kho
        //
    }
}
