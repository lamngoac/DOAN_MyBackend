using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_MapDeliveryOrder_ByInvFIOut : WARQBase
    {
        public string DateFrom { get; set; } // Tìm kiếm từ ngày

        public string DateTo { get; set; } // Tìm kiếm đến ngày
    }
}
