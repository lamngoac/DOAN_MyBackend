using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_RptSv_Sys_Group : WARQBase
    {
        public string Rt_Cols_RptSv_Sys_Group { get; set; }

        public string Rt_Cols_RptSv_Sys_UserInGroup { get; set; }

        public RptSv_Sys_Group RptSv_Sys_Group { get; set; }
    }
}
