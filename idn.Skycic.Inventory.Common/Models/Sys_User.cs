using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using idn.Skycic.Inventory.Common.Attributes;

namespace idn.Skycic.Inventory.Common.Models
{
	public class Sys_User : EntityBase
	{
        [DataColum]
        public string UserCode { get; set; }

		public string NetworkID { get; set; }

		public string UserName { get; set; }

		public string UserPassword { get; set; }

		public string UserPasswordNew { get; set; }

		public string PhoneNo { get; set; }

		public string EMail { get; set; }

        public object MST { get; set; }

        public string OrganCode { get; set; }

		public string DepartmentCode { get; set; }

		public string Position { get; set; }

        public string VerificationCode { get; set; }

        public string Avatar { get; set; }

        public string UUID { get; set; }

		public string FlagDLAdmin { get; set; }

        public string FlagSysAdmin { get; set; }

        public string FlagNNTAdmin { get; set; }
        public string OrgID { get; set; }
        public string CustomerCodeSys { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string FlagActive { get; set; }

		public string LogLUDTimeUTC { get; set; }

		public string LogLUBy { get; set; }

        public string ACId { get; set; }

        public string ACAvatar { get; set; }

        public string ACEmail { get; set; }

        public string ACLanguage { get; set; }

        public string ACName { get; set; }

        public string ACPhone { get; set; }

        public string ACTimeZone { get; set; }

        // //
        public string mo_OrganCode { get; set; }

		public string mo_OrganName { get; set; }

		public string mdept_DepartmentCode { get; set; }

		public string mdept_DepartmentName { get; set; }

        public string mnnt_DealerType { get; set; }
    }
}
