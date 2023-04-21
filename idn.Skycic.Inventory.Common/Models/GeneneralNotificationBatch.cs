using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class GeneneralNotificationBatch
	{
		public long OrgId { get; set; }
		public string SolutionCode { get; set; }
		public List<GeneneralNotification> List { get; set; }
	}
}
