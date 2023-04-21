using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.WebAdmin.Models
{
    public class PageInfo<T>
    {
        public PageInfo(int index, int pageSize)
        {
            this.PageSize = pageSize;
            this.PageIndex = index;
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int ItemCount { get; set; }
        public List<T> DataList { get; set; }
    }
}