using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_TourScheduleDetail : EntityBase
    {
        public string TourCode { get; set; }
        public int Idx { get; set; }
        public string Title { get; set; }
    }
}
