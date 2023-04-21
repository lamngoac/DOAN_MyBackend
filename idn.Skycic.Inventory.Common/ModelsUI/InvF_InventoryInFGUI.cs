using idn.Skycic.Inventory.Common.Attributes;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class InvF_InventoryInFGUI : InvF_InventoryInFG
    {
        public List<InvF_InventoryInFGDtlUI> lstInvFInventoryInFGDtlUIs { get; set; }
        public object FromDate { get; set; }
        public object ToDate { get; set; }
        public object ApprToDate { get; set; }
        public object ApprFromDate { get; set; }
        public object PartCode { get; set; }

    }
}
