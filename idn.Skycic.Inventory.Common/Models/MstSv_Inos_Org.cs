using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
	public class MstSv_Inos_Org
	{
		public object MST { get; set; }

		public object Id { get; set; }

		public object ParentId { get; set; }

		public object Name { get; set; }

		public object BizType { get; set; }

		public object BizField { get; set; }

		public object OrgSize { get; set; }

		public object ContactName { get; set; }

		public object Email { get; set; }

		public object PhoneNo { get; set; }

		public object Description { get; set; }

		public object Enable { get; set; }

		public object CurrentUserRole { get; set; }

		public object FlagActive { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

        public object OrderId { get; set; }

        public object imbt_BizType { get; set; }

        public object imbt_BizTypeName { get; set; }

        public object imbf_BizFieldCode { get; set; }

        public object imbf_BizFieldName { get; set; }

        public object imbs_BizSizeCode { get; set; }

        public object imbs_BizSizeName { get; set; }


    }
}
