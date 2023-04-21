using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_InvBalLot_MaxExpiredDateByInv : WARQBase
    {
        public Rpt_Summary_In_Out_Pivot Rpt_Summary_In_Out_Pivot { get; set; }
        public List<Mst_Product> Lst_Mst_Product { get; set; }
        public List<Mst_Inventory> Lst_Mst_Inventory { get; set; }
        public List<Mst_ProductGroup> Lst_Mst_ProductGroup { get; set; }
    }
}
