using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_POW_AboutUs : EntityBase
    {
        public string AUNo { get; set; }
        public string Title { get; set; }
        public string VideoURL { get; set; }
        public string PostDTime { get; set; }
        public string FlagActive { get; set; }
    }
}
