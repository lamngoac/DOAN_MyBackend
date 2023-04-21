using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.Veloca
{
    public  class RQ_Ser_ROProductPart : WARQBase
    {
        public string Rt_Cols_Ser_ROProductPart { get; set; }

        public Ser_ROProductPart Ser_ROProductPart { get; set; }
    }
}
