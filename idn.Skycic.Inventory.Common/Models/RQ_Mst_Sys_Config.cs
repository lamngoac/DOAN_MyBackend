using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Mst_Sys_Config : WARQBase
    {
        public string Rt_Cols_Mst_Sys_Config { get; set; }

        public Mst_Sys_Config Mst_Sys_Config { get; set; }
        public List<Mst_Sys_Config> Lst_Mst_Sys_Config { get; set; }
    }
}
