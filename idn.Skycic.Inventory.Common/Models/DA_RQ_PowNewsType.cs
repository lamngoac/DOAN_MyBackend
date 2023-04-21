using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_PowNewsType : WARQBase
    {
        public string Rt_Cols_POW_NewsType { get; set; }

        public DA_PowNewsType POW_NewsType { get; set; }
    }
}
