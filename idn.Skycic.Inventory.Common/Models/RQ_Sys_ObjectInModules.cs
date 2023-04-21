using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_Sys_ObjectInModules : WARQBase
    {
        public string Rt_Cols_Sys_ObjectInModules { get; set; }

        public Sys_Modules Sys_Modules { get; set; }

        public List<Sys_ObjectInModules> Lst_Sys_ObjectInModules { get; set; }
    }
}
