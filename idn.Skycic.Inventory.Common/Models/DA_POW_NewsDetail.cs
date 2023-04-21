using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_POW_NewsDetail : EntityBase
    {
        public string NewsNo { get; set; }
        public int Idx { get; set; }
        public string ImagePath { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }
    }
}
