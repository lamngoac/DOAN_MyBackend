using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_PrdCenter_Mst_SpecImage
    {
        public object OrgID { get; set; }

        public object AutoID { get; set; }

        public object NetworkID { get; set; }

        public object SpecCode { get; set; }

        public object SpecImagePath { get; set; }

        public object SpecImageName { get; set; }

        public object SpecImageDesc { get; set; }

        public object FlagPrimaryImage { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }
    }
}