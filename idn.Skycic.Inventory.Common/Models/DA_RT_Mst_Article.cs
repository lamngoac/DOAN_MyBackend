using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RT_Mst_Article : WARTBase
    {
        public List<DA_Mst_Article> Lst_Mst_Article { get; set; }

        public List<DA_Mst_ArticleDetail> Lst_Mst_ArticleDetail { get; set; }
    }
}
