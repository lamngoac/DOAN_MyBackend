using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_POW_FAQ : EntityBase
    {
        public string FAQNo { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string PostDTime { get; set; }
        public string FlagActive { get; set; }
    }
}
