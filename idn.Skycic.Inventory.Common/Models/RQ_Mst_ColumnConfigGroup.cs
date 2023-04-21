using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_ColumnConfigGroup : WARQBase
    {
        public string Rt_Cols_Mst_ColumnConfigGroup { get; set; }

        public Mst_ColumnConfigGroup Mst_ColumnConfigGroup { get; set; }
    }
}
