using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Ord_OrderPDUI : OS_Ord_OrderPD
    {
        public string CreateDTimeUTCFrom { get; set; }
        public string CreateDTimeUTCTo { get; set; }
        public string CustomerNameKH { get; set; }
        public string CustomerCodeKH { get; set; }
        public string CustomerCodeSysKH { get; set; }
        public string ProductName { get; set; }
        public string ProductCodeUser { get; set; }
        public string OrderSRNoSys { get; set; }
        public string ProductCode { get; set; }
        public string ProductCodeBase { get; set; }
        public string ProductCodeRoot { get; set; }
        public string PlanQty { get; set; }
        public string UPIn { get; set; }
        public string UnitCode { get; set; }
        public string FlagLot { get; set; }
        public string FlagSerial { get; set; }
        public string FlagCombo { get; set; }
        public string ValConvert { get; set; }
        public string QtyAppr { get; set; }
        public string QtyInvOutAvail { get; set; }
        public string QtyTotalOK { get; set; }
    }
}
