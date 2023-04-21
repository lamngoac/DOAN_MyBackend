using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Seq_Common : WARQBase
	{
		public Seq_Common Seq_Common { get; set; }
        public object Qty { get; set; }
	}
}
