using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_Mst_Spec : WARQBase
    {
        public string Rt_Cols_Mst_Spec { get; set; }

        public string Rt_Cols_Mst_SpecImage { get; set; }

        public string Rt_Cols_Mst_SpecFiles { get; set; }

        public OS_PrdCenter_Mst_Spec Mst_Spec { get; set; }

        public List<OS_PrdCenter_Mst_Spec> Lst_Mst_Spec { get; set; }

        public List<OS_PrdCenter_Mst_SpecImage> Lst_Mst_SpecImage { get; set; }

        public List<OS_PrdCenter_Mst_SpecFiles> Lst_Mst_SpecFiles { get; set; }
    }
}
