using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RT_POW_NewsNews : WARTBase
    {
        public List<DA_POW_NewsNews> Lst_POW_NewsNews { get; set; }
        public List<DA_POW_NewsDetail> Lst_POW_NewsDetail { get; set; }
    }
}
