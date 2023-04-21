using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_PowNewsType : EntityBase
    {
        public string NewsType { get; set; }
        public string NewsName { get; set; }
        public string FlagActive { get; set; }
    }
}
