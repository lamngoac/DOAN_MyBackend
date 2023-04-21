using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_CustomerInArea
	{
		public object CustomerCodeSys { get; set; }
		public object OrgID { get; set; }
		public object AreaCode { get; set; }
        public object ma_AreaName { get; set; }
        public object ma_AreaCodeParent { get; set; }
        public object NetworkID { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
	}
}
