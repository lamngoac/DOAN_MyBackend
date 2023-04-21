using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_RptSv_Sys_Group : WARTBase
    {
        public List<RptSv_Sys_Group> Lst_RptSv_Sys_Group { get; set; }

        public List<RptSv_Sys_UserInGroup> Lst_RptSv_Sys_UserInGroup { get; set; }
    }
}
