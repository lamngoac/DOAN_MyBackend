using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Rpt_MapDeliveryOrder_ByInvFIOutUI : Rpt_MapDeliveryOrder_ByInvFIOut
    {
        public List<Rpt_MapDeliveryOrder_ByInvFIOut_Date> ListRpt_Date { get; set; }
        public List<string> ListMst_Area { get; set; }
    }
    public class Rpt_MapDeliveryOrder_ByInvFIOut_Date
    {
        public object Rpt_Date { get; set; }
        public object Qty { get; set; }
    }
}
