using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RT_OS_Inos_Org : WARTBase
	{
		public List<OS_Inos_Org> Lst_OS_Inos_Org { get; set; }
        public List<iNOS_Mst_BizType> Lst_iNOS_Mst_BizType { get; set; }
        public List<iNOS_Mst_BizField> Lst_iNOS_Mst_BizField { get; set; }
    }
}
