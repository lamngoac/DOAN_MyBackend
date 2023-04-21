using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_Sys_UserInGroup : WARQBase
    {
        public DA_Sys_Group Sys_Group { get; set; }

        public List<DA_Sys_UserInGroup> Lst_Sys_UserInGroup { get; set; }
    }
}
