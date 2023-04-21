using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RT_Sys_User : WARTBase
    {
        public List<DA_Sys_User> Lst_Sys_User { get; set; }

        public List<DA_Sys_UserInGroup> Lst_Sys_UserInGroup { get; set; }

        public List<DA_Sys_Access> Lst_Sys_Access { get; set; }
    }
}
