using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Common.ModelsUI
{
   public class Temp_PrintTempUI : Temp_PrintTemp
    {
        public object FilePath { get; set; } // đường dẫn file tem mẫu dùng để import vào hệ thống
        public object FromDate { get; set; }
        public object ToDate { get; set; }
    }
}
