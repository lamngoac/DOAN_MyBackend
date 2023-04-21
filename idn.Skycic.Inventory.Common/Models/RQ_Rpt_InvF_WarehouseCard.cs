using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvF_WarehouseCard : WARQBase
    {
        public string ProductCode { get; set; }
        public string ApprDTimeUTCFrom { get; set; }
        public string ApprDTimeUTCTo { get; set; }
        public Rpt_InvF_WarehouseCard Rpt_InvF_WarehouseCard { get; set; }
        public List<Mst_Inventory> Lst_Mst_Inventory { get; set; }
    }
}
