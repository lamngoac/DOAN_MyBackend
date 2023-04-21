using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Mst_Article : EntityBase
    {
        public string ArticleNo { get; set; }
        public string ArticleTitle { get; set; }
        public string ArticleDesc { get; set; }
        public string ArticleThemePath { get; set; }
        public string DestCode { get; set; }
        public string ProvinceCode { get; set; }
        public string Author { get; set; }
        public string PostDTime { get; set; }
        public string FlagShow { get; set; }
        public string FlagActive { get; set; }
        ////
        public DA_Mst_ArticleDetail Mst_ArticleDetail { get; set; }
    }
}
