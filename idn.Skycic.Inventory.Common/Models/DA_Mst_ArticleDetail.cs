using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_ArticleDetail : EntityBase
    {
        public string ArticleNo { get; set; }
        public int Idx { get; set; }
        public string ArticleContent { get; set; }
        public string ArticleImageName { get; set; }
        public string ArticleImagePath { get; set; }
        public string Title { get; set; }
    }
}
