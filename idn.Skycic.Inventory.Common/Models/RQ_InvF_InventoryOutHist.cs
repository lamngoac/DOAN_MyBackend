using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_InvF_InventoryOutHist : WARQBase
    {
        public string Rt_Cols_InvF_InventoryOutHist { get; set; }
        public string Rt_Cols_InvF_InventoryOutHistDtl { get; set; }
        public string Rt_Cols_InvF_InventoryOutHistInstSerial { get; set; }
        public InvF_InventoryOutHist InvF_InventoryOutHist { get; set; }
        public List<InvF_InventoryOutHistDtl> Lst_InvF_InventoryOutHistDtl { get; set; }
        public List<InvF_InventoryOutHistInstSerial> Lst_InvF_InventoryOutHistInstSerial { get; set; }
    }
}
