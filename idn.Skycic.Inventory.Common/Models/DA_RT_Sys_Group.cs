using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RT_Sys_Group : WARTBase
    {
        public List<DA_Sys_Group> Lst_Sys_Group { get; set; }

        public List<DA_Sys_UserInGroup> Lst_Sys_UserInGroup { get; set; }
    }
}
