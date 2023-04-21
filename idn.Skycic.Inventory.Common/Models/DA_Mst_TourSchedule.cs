using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_TourSchedule : EntityBase
    {
        public string TourCode { get; set; }
        public int DateNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
