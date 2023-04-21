using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_InvBalLot_MaxExpiredDateByInv
    {
        public object OrgID { get; set; }

        public object ProductCode { get; set; } // Mã sản phẩm hệ thống sinh

        public object ProductCodeUser { get; set; } // Mã sản phẩm

        public object ProductName { get; set; } // Tên sản phẩm

        public object UnitCode { get; set; } //đơn vị

        public object ProductLotNo { get; set; } // Số Lot

        public object InvCode { get; set; } // Mã kho

        public object ProductGrpCode { get; set; } // Mã nhóm

        public object ProductGrpName { get; set; } //. Tên nhóm

        public object LastInInvDate { get; set; } // Ngày nhập kho

        public object MaxExpiredDate { get; set; }  // Ngày hết hạn

        public object QtyDayInv { get; set; } // Số ngày tồn

        public object TotalQtyTotalOK { get; set; } // Số lượng

        public object TotalQtyBlockOK { get; set; }

        public object TotalQtyAvailOK { get; set; }
    }
}
