using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_ColumnConfig : WARQBase
    {
        public string Rt_Cols_Mst_ColumnConfig { get; set; }

        public Mst_ColumnConfig Mst_ColumnConfig { get; set; }
    }
}
