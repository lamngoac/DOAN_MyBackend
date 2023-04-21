using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_TourGuide : EntityBase
    {
        public string TGNo { get; set; }
        public string TGName { get; set; }
        public string TGIDCardNo { get; set; }
        public string TGAddress { get; set; }
        public string TGMobileNo { get; set; }
        public string FlagActive { get; set; }
        public string CreateDTime { get; set; }
        public string CreateBy { get; set; }
        public string LogLUDTime { get; set; }
        public string LogLUBy { get; set; }
    }
}
