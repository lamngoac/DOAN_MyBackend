using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Rpt_Inv_InventoryBalance_ByPeriod
    {
        public object OrgID { get; set; }
        public object NetworkID { get; set; }
        public object ProductCode { get; set; } // Mã hàng hóa HT sinh
        public object QtyTotalOK { get; set; }  // Số lượng tồn kho
        public object ReportDTimeUTC { get; set; }  // Thời gian lấy báo cáo
    }
}
