using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Mst_CustomerSourceImages
	{
		public object AutoID { get; set; }
		public object Idx { get; set; }
		public object OrgID { get; set; }
		public object CustomerSourceCode { get; set; }
		public object NetworkID { get; set; }
		public object ImagePath { get; set; }
		public object ImageName { get; set; }
		public object ImageDesc { get; set; }
		public object FlagPrimaryImage { get; set; }
		public object FlagActive { get; set; }
		public object LogLUDTimeUTC { get; set; }
		public object LogLUBy { get; set; }
        public object ImageSpec { get; set; }

    }
}
