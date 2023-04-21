using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class RQ_Mst_CustomerType : WARQBase
	{
        public List<Mst_CustomerType> Lst_Mst_CustomerType { get; set; }

        public string Rt_Cols_Mst_CustomerType { get; set; }

        public string Rt_Cols_Mst_CustomerTypeImages { get; set; }

        public Mst_CustomerType Mst_CustomerType { get; set; }

        public List<Mst_CustomerTypeImages> Lst_Mst_CustomerTypeImages { get; set; }

        //public List<Mst_CustomerTypeImages> Lst_Mst_SpecFiles { get; set; }
    }
}
