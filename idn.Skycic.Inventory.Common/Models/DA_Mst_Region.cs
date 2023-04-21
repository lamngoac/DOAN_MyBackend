using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using idn.Skycic.Inventory.Common.Attributes;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_Region : EntityBase
    {
        public string RegionCode { get; set; }
        public string RegionCodeParent { get; set; }
        public string RegionBUCode { get; set; }
        public string RegionBUPattern { get; set; }
        public string RegionLevel { get; set; }
        public string RegionName { get; set; }
        public string RegionDesc { get; set; }
        public string ImageFilePath { get; set; }
        public string FlagActive { get; set; }
        public string LogLUDTime { get; set; }
        public string LogLUBy { get; set; }
    }
}
