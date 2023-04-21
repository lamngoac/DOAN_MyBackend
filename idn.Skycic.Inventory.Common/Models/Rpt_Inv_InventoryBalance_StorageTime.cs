using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_Inv_InventoryBalance_StorageTime
    {
        public object OrgID { get; set; }

        public object InvCode { get; set; }

        public object ProductCode { get; set; }

        public object mp_ProductCodeBase { get; set; }

        public object mp_ProductCodeRoot { get; set; }

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductName { get; set; }

        public object mp_ProductNameEN { get; set; }

        public object mp_ProductGrpCode { get; set; }

        public object mpc_ProductGrpName { get; set; }

        public object mp_FlagLot { get; set; }

        public object mp_FlagSerial { get; set; }

        public object mp_ProductType { get; set; }

        public object mp_UnitCode { get; set; }

        public object mp_ValConvert { get; set; }

        public object Qty { get; set; }

        public object ValMixBase { get; set; }

        public object ValMixBaseDesc { get; set; }

        public object ValMixBaseAfterDesc { get; set; }

        public object TotalValMixBase { get; set; }

        public object TotalValMixBaseDesc { get; set; }

        public object TotalValMixBaseAfterDesc { get; set; }

        public object QtyTotalOK { get; set; }  // Số lượng tổng

        public object QtyAvailOK { get; set; }  // Số lượng Avail

        public object QtyBlockOK { get; set; }  // Số lượng Avail

        public object UPInv { get; set; }  // Đơn giá hàng tồn kho

        public object TotalValInv { get; set; }  // Giá trị hàng tồn kho

        // StorageTime:
        public object QtyMinSt { get; set; }  // Tồn kho tối thiểu

        public object QtyMaxSt { get; set; }  // Tồn kho tối đa

        public object TotalInvValue { get; set; }

        public object LastDTimeInvIn { get; set; }

        public object StorageTime { get; set; }
    }
}
