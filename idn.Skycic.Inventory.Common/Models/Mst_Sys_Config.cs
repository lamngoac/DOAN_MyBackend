using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_Sys_Config
    {
        public object OrgID { get; set; }
        public object SysConfigID { get; set; }
        public object NetworkID { get; set; }
        public object SysConfigName { get; set; }
        public object SysConfigDesc { get; set; }
        public object Remark { get; set; }
        public object FlagActive { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }
    }
}
