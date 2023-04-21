using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_PrdCenter_Mst_Model
    {
        public object OrgID { get; set; }

        public object ModelCode { get; set; }

        public object NetworkID { get; set; }

        public object ModelName { get; set; }

        public object OrgModelCode { get; set; }

        public object BrandCode { get; set; }
	
		public object NetworkModelCode { get; set; }

		public object Remark { get; set; }

        public object FlagActive { get; set; }

        public object LogLUDTimeUTC { get; set; }

        public object LogLUBy { get; set; }

        ////
        public object mb_BrandCode { get; set; }

        public object mb_BrandName { get; set; }

    }
}
