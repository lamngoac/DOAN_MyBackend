using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvBalanceValuationPeriodMonth : WARQBase
    {
        public string ReportMonthFrom { get; set; } // Tháng báo cáo từ

        public string ReportMonthTo { get; set; } // Tháng báo cáo đến
        public List<Mst_ProductGroup> Lst_Mst_ProductGroup { get; set; } // Nhóm hàng hoá 
        public Rpt_InvBalanceValuationPeriodMonth Rpt_InvBalanceValuationPeriodMonth { get; set; }
    }
}
