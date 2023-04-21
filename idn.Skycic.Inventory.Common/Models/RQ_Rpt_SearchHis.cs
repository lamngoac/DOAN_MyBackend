using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Rpt_SearchHis : WARQBase
    {
        public string Rt_Cols_Rpt_SearchHis { get; set; }

        public Rpt_SearchHis Rpt_SearchHis { get; set; }
    }
}
