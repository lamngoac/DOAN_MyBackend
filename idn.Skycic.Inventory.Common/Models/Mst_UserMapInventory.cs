using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_UserMapInventory
    {
        public string OrgID { get; set; }

        public string UserCode { get; set; }

        public string InvCode { get; set; }

        public string Remark { get; set; }

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }
    }
}
