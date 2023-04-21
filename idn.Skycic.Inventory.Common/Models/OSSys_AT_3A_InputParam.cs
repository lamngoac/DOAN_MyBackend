using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class OSSys_AT_3A_InputParam<T>
	{
		public string Action { get; set; }
		public T Data { get; set; }
	}
}
