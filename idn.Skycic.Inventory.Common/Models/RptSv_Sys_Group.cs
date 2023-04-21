using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RptSv_Sys_Group
    {
        public string GroupCode { get; set; }

        public string GroupName { get; set; }

        public string FlagActive { get; set; }

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }
    }
}
