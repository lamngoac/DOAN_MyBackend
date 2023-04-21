using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Sys_Access
    {
        public string GroupCode { get; set; }

        public string ObjectCode { get; set; }

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }
        // //
        public string so_ObjectCode { get; set; }

        public string so_ObjectName { get; set; }

        public string so_ServiceCode { get; set; }

        public string so_ObjectType { get; set; }

        public string so_FlagExecModal { get; set; }

        public string so_FlagActive { get; set; }
    }
}
