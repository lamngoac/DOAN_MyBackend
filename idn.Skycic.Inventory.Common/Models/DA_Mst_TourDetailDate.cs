using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_TourDetailDate : EntityBase
    {
        public string TourCode { get; set; }
        public string IDNo { get; set; }
        public int Idx { get; set; }
        public int DateNo { get; set; }
        public string ExactDate { get; set; }
    }
}
