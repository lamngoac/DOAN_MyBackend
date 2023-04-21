using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SearchInput
    {
        /// <summary>
        /// Điều kiện search
        /// Key: PropertyName + operator(=, in, like, >=, <=, >, <, in ) 
        /// operator==null=> dau =
        /// Value: Giá trị so sánh
        /// VD: {"CreateDTime >=", DateTime.Now}
        /// 
        /// </summary>
        public Dictionary<string, object> SearchDict { get; set; }


        public int PageIndex { get; set; }

        /// <summary>
        /// pagesize==0 => không giới hạn
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// vi du: "CreateDTime DESC"
        /// </summary>
        public string orderByColums { get; set; }

        /// <summary>
        /// những bảng trả về, mặc định=null => chỉ trả bảng chính
        /// key: tên bảng( classname)
        /// value: danh sách các trường trả về( *=> tất cả)
        /// </summary>
        public Dictionary<string, string> ReturnTableDict { get; set; }

        public string FlagIstranId { get; set; }
    }
}
