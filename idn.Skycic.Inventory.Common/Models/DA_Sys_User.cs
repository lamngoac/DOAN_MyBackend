using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class DA_Sys_User : EntityBase
    {
        public string UserCode { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public string UserPasswordNew { get; set; }

        public string UserPhoneNo { get; set; }

        public string UserEmail { get; set; }

        public string FlagSysAdmin { get; set; }

        public string FlagBG { get; set; }

        public string FlagActive { get; set; }

        public string Remark { get; set; }

        public string LogLUDTimeUTC { get; set; }

        public string LogLUBy { get; set; }

        ////
        public string mctm_CustomerCode { get; set; }

        public string mctm_CustomerTypeCode { get; set; }

        public string mctm_CustomerName { get; set; }

        public string mctm_CustomerGender { get; set; }

        public string mctm_CustomerPhoneNo { get; set; }

        public string mctm_CustomerMobileNo { get; set; }

        public string mctm_CustomerAddress { get; set; }

        public string mctm_CustomerEmail { get; set; }

        public string mctm_CustomerBOD { get; set; }

        public string mctm_CustomerAvatarPath { get; set; }

        public string mctm_CustomerIDCardNo { get; set; }
    }
}
