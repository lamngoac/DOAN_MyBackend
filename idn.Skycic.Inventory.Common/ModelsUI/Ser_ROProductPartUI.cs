using idn.Skycic.Inventory.Common.Models.Veloca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Ser_ROProductPartUI : Ser_ROProductPart
    {
        public object ProductCodeBase { get; set; }
        public object ProductCodeRoot { get; set; }
        public object ProductCode { get; set; }
        public object ProductName { get; set; }
        public object ProductCodeUser { get; set; }
        public object ValConvert { get; set; }
        public object FlagSerial { get; set; }
        public object FlagLo { get; set; }
        public object FlagCombo { get; set; }
        public object UPSell { get; set; }
        public object OrderNoSys { get; set; }
        public object QtyAppr { get; set; }
        public object QtyInvOutAvail { get; set; }
        public object QtyTotalOK { get; set; }
        public object CustomerCodeSys { get; set; }
    }
}
