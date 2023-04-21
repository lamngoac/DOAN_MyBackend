using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_Province : EntityBase
    {
        public string ProvinceCode { get; set; }
        public string ProvinceName { get; set; }
        public string FlagActive { get; set; }
    }
}
