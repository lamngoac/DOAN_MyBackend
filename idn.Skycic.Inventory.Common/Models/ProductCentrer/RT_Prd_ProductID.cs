using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_Prd_ProductID : WARTBase
    {
        public List<Prd_ProductID> Lst_Prd_ProductID { get; set; }


        public List<dt_Prd_ProductID_Exist> Lst_dt_Prd_ProductID_Exist { get; set; }

        public List<dt_Prd_ProductID_Active> Lst_dt_Prd_ProductID_Active { get; set; }
    }
}
