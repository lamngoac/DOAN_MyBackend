using idn.Skycic.Inventory.Common.ModelsUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class InvF_InvAuditDtl
    {
        public object IF_InvAudNo { get; set; } // Mã phiếu kiểm kê

        public object InvCodeInit { get; set; } // Vị trí lý thuyết

        public object InvCodeActual { get; set; } // Vị trí thực tế

        public object ProductCode { get; set; } // Mã hàng hóa

        public object QtyInit { get; set; } // Số lượng lý thuyết

        public object QtyActual { get; set; } // Số lượng thực tế

        public object UPAudit { get; set; } // Đơn giá

        public object ValAudit { get; set; } // Thanh tiền

        public object UnitCode { get; set; } // Đơn vị

        public object InventoryAction { get; set; } // 

        public object FlagExist { get; set; } // 

        public object FlagAudit { get; set; } // Cờ kiểm kê

        public object IF_InvAuditStatusDtl { get; set; }

        public object Remark { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        public object mp_ProductCode { get; set; }

        public object mp_ProductName { get; set; }

        public object mp_ProductCodeUser { get; set; }

        public object mp_ProductCodeBase { get; set; }

        public object mp_FlagLot { get; set; }

        public object mp_FlagSerial { get; set; }

        public object mp_ProductType { get; set; }

        public List<Mst_ProductUI> lstUnitCodeUIByProduct { get; set; }
    }
}
