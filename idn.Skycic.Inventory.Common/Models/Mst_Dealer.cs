using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class Mst_Dealer
    {
		public object DLCode { get; set; }

		public object DLCodeParent { get; set; }

		public object NetworkID { get; set; }

		public object DLBUCode { get; set; }

		public object DLBUPattern { get; set; }

		public object DLLevel { get; set; }

		public object ProvinceCode { get; set; }

		public object DLName { get; set; }

		public object DLType { get; set; }

		public object DLAddress { get; set; }

		public object DLPresentBy { get; set; }

		public object DLGovIDNumber { get; set; }

		public object DLEmail { get; set; }

		public object DLPhoneNo { get; set; }

		public object FlagActive { get; set; }

		public object LogLUDTimeUTC { get; set; }

		public object LogLUBy { get; set; }

		public object count_MST { get; set; } //SLKH


		////
		public object mp_ProvinceCode { get; set; }

		public object mp_ProvinceName { get; set; }

		public object Remark { get; set; }

	}
}
