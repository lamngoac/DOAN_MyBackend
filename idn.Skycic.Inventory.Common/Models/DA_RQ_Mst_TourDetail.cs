using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_Mst_TourDetail : WARQBase
    {
        public string Rt_Cols_Mst_TourDetail { get; set; }
        public DA_Mst_TourDetail Mst_TourDetail { get; set; }
        public DA_Mst_TourDetailDate Mst_TourDetailDate { get; set; }
        public DA_Mst_TourSchedule Mst_TourSchedule { get; set; }
        public DA_Mst_TourScheduleDetail Mst_TourScheduleDetail { get; set; }
        public DA_Mst_TourDestImages Mst_TourDestImages { get; set; }
    }
}
