using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_Sys_User : WARQBase
    {
        public string Rt_Cols_Sys_User { get; set; }

        public string Rt_Cols_Sys_UserInGroup { get; set; }

        public DA_Sys_User Sys_User { get; set; }

        public string ServiceCode { get; set; }
    }
}
