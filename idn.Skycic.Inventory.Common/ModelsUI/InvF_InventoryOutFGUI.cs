using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryOutFGUI : InvF_InventoryOutFG
    {
        public List<InvF_InventoryOutFGDtlUI> lstInvFInventoryOutFGDtlUIs { get; set; }
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public object ApprToDate { get; set; }
        public object ApprFromDate { get; set; }
        public object PartCode { get; set; }
    }
}
