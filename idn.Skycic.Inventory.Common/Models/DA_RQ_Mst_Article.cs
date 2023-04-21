using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_RQ_Mst_Article : WARQBase
    {
        public string Rt_Cols_Mst_Article { get; set; }
        public string Rt_Cols_Mst_ArticleDetail { get; set; }

        public DA_Mst_Article Mst_Article { get; set; }
        public DA_Mst_ArticleDetail Mst_ArticleDetail { get; set; }

        public List<DA_Mst_Article> Lst_Mst_Article { get; set; }
        public List<DA_Mst_ArticleDetail> Lst_Mst_ArticleDetail { get; set; }
    }
}
