using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_Inos_User : WARQBase
    {
        public string Rt_Cols_OS_Inos_User { get; set; }
        //public string Rt_Cols_iNOS_Mst_BizType { get; set; }
        //public string Rt_Cols_iNOS_Mst_BizField { get; set; }
        public OS_Inos_User OS_Inos_User { get; set; }
        //public iNOS_Mst_BizType iNOS_Mst_BizType { get; set; }
        //public iNOS_Mst_BizField iNOS_Mst_BizField { get; set; }
        //public List<OS_Inos_Org> Lst_OS_Inos_Org { get; set; }
    }
}
