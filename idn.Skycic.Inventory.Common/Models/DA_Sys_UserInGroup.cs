using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Sys_UserInGroup
    {
        public string GroupCode { get; set; }

        public string UserCode { get; set; }

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }
        // //
        public string su_UserCode { get; set; }

        public string su_UserName { get; set; }
    }
}
