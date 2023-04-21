using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_Mst_TourGuide : WARQBase
    {
        public string Rt_Cols_Mst_TourGuide { get; set; }

        public DA_Mst_TourGuide Mst_TourGuide { get; set; }
    }
}
