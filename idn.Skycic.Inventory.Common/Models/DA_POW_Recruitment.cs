using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_POW_Recruitment : EntityBase
    {
        public string RecNo { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThemeImage { get; set; }
        public string Author { get; set; }
        public string PostDTime { get; set; }
        public string FlagActive { get; set; }
    }
}
