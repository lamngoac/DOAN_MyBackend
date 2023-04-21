using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Inv_InventoryBalance
    {
        public object OrgID { get; set; }

        public object InvCode { get; set; }

        public object ProductCode { get; set; }

        public object NetworkID { get; set; }

        public object QtyTotalOK { get; set; }

        public object QtyBlockOK { get; set; }

        public object QtyAvailOK { get; set; }

        public object QtyTotalNG { get; set; }

        public object QtyBlockNG { get; set; }

        public object QtyAvailNG { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
        public object UPInv { get; set; } // Giá hàng tồn
        public object TotalValInv { get; set; } // Giá trị hàng tồn
    }
}
