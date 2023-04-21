using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_CustomerInCustomerGroup
	{
		public object CustomerGrpCode { get; set; }
		public object CustomerCodeSys { get; set; }
		public object OrgID { get; set; }
		public object NetworkID { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
        public object mc_CustomerCodeSys { get; set; }
        public object mc_CustomerCode { get; set; }
        public object mc_CustomerName { get; set; }
        public object mcg_CustomerGrpCode { get; set; }
        public object mcg_CustomerGrpName { get; set; }
        public object mcg_FlagActive { get; set; }
        public object SolutionCode { get; set; }
        public object FunctionActionType { get; set; }
    }
}
