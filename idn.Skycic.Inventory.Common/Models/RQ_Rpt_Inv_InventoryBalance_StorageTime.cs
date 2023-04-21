using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_Inv_InventoryBalance_StorageTime : WARQBase
    {
        public string ProductCode { get; set; }

        public string ProductGrpCode { get; set; }

        public string ReportDateUTC { get; set; } // Ngày báo cáo
        public List<Mst_ProductGroup> Lst_Mst_ProductGroup { get; set; }
        public Rpt_Inv_InventoryBalance_StorageTime Rpt_Inv_InventoryBalance_StorageTime { get; set; }
    }
}
