using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_SerRO_InvF_Transaction : WARQBase
    {
        public string Rt_Cols_SerRO_InvF_Transaction { get; set; }

        public SerRO_InvF_Transaction SerRO_InvF_Transaction { get; set; }

        public List<SerRO_InvF_Transaction> Lst_SerRO_InvF_Transaction { get; set; }
    }
}
