using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RT_Mst_TourDetail : WARTBase
    {
        public List<DA_Mst_TourDetail> Lst_Mst_TourDetail { get; set; }
        public List<DA_Mst_TourDetailDate> Lst_Mst_TourDetailDate { get; set; }
        public List<DA_Mst_TourSchedule> Lst_Mst_TourSchedule { get; set; }
        public List<DA_Mst_TourScheduleDetail> Lst_Mst_TourScheduleDetail { get; set; }
        public List<DA_Mst_TourDestImages> Lst_Mst_TourDestImages { get; set; }
    }
}
