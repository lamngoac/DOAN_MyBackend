using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
    public class Mst_PrintOrderUI: Mst_PrintOrder
    {
        public object ProductCode { get; set; }
        public object ProductCodeUser { get; set; }
        public object ProductName { get; set; }
        public object UPIn { get; set; }
        public object UnitCode { get; set; }
        public object FlagLot { get; set; }
        public object FlagSerial { get; set; }
    }
}
