using idn.Skycic.Inventory.Common.ModelsUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_MoveOrdInstSerial
    {
        public object IF_MONo { get; set; } // Số phiếu điều chuyển

        public object InvCodeOut { get; set; } //

        public object InvCodeIn { get; set; } //

        public object ProductCode { get; set; } //

        public object SerialNo { get; set; } //

        public object ProductLotNo { get; set; } //

        public object NetworkID { get; set; } //

        public object IF_MOISStatus { get; set; } //

        public object LogLUDTimeUTC { get; set; } //

        public object LogLUBy { get; set; } //

        public object mp_ProductCode { get; set; } // mp_ProductCode

        public object mp_ProductCodeUser { get; set; } // mp_ProductCode

        public object mp_ProductName { get; set; } // mp_ProductName

        public object mp_ProductNameEN { get; set; } // mp_ProductNameEN

        public object mp_FlagLot { get; set; } // mp_FlagLot

        public object mp_FlagSerial { get; set; } // mp_FlagSerial

        public object mp_ValConvert { get; set; } // mp_ValConvert
    }

    public class InvF_MoveOrdInstSerialUI : InvF_MoveOrdInstSerial
    {
        public object UnitCode { get; set; }

        public object ProductCodeBase { get; set; }

        public object prd_QtyTotalOK { get; set; }
        public object QtyTotalOK { get; set; } // Tồn kho
        public List<Mst_ProductUI> Lst_Mst_ProductBase { get; set; }//Danh sách hàng cùng base
    }
}
