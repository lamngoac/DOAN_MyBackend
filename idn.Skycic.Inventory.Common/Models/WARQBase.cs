using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class WARQBase
	{
		public string Tid { get; set; }

        public string NetworkID { get; set; }

		public string OrgID { get; set; }

		public string TokenID { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string UtcOffset { get; set; }

		public string GwUserCode { get; set; }

		public string GwPassword { get; set; }

		public string WAUserCode { get; set; }

		public string WAUserPassword { get; set; }

		public string FlagIsDelete { get; set; } // 0: tạo, sửa; 1: xóa

        public string FlagAppr { get; set; } // 1: Duyệt luôn; 0: ra ngoài màn hình quản lý duyệt

		public string FlagIsEndUser { get; set; } 

		public string FuncType { get; set; }

		public string Ft_RecordStart { get; set; }

		public string Ft_RecordCount { get; set; }

		public string Ft_WhereClause { get; set; }

		public string Ft_Cols_Upd { get; set; }
	}
}
