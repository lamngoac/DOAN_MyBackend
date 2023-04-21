using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OS_Inos_UserLicense
	{
		public object OrgId { get; set; }
		public object UserId { get; set; }
		public string SolutionCode { get; set; }
		public List<string> Modules { get; set; }
	}
}
