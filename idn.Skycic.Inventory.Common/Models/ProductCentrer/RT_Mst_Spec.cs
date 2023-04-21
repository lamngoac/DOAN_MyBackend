using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models.ProductCentrer
{
    public class RT_Mst_Spec : WARTBase
    {
        
        public List<Mst_Spec> Lst_Mst_Spec { get; set; }

        public List<Mst_SpecImage> Lst_Mst_SpecImage { get; set; }

        public List<Mst_SpecFiles> Lst_Mst_SpecFiles { get; set; }

		public List<dt_Mst_Spec_Exist> Lst_dt_Mst_Spec_Exist { get; set; }

		public List<dt_Mst_Spec_Active> Lst_dt_Mst_Spec_Active { get; set; }
	}
}