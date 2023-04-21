using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Temp_PrintTemp : WARQBase
    {
        public string Rt_Cols_Temp_PrintTemp { get; set; }
        public Temp_PrintTemp Temp_PrintTemp { get; set; }
    }
}
