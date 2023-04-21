using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Sys_Object
    {
        public string ObjectCode { get; set; }

        public string ObjectName { get; set; }

        public string ServiceCode { get; set; }

        public string ObjectType { get; set; }

        public string FlagExecModal { get; set; }

        public string FlagActive { get; set; }

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }
    }
}
