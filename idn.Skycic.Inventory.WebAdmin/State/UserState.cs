using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.WebAdmin.State
{
    public class UserState
    {
        public string UserName { get; set; }
        public string AddressAPIs { get; set; }
        public bool IsSysAdmin { get; set; }
        public RptSv_Sys_User RptSv_Sys_User { get; set; }
        public List<RptSv_Sys_Access> ListRptSvSysAccess { get; set; }
        public string UtcOffset { get; set; }

        public UserState()
        {

        }

        public UserState(RptSv_Sys_User sysUser)
        {
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            RptSv_Sys_User = sysUser;
            UtcOffset = null;
        }

        public UserState(RptSv_Sys_User sysUser, List<RptSv_Sys_Access> listSysAccess)
        {
            UtcOffset = null;
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            RptSv_Sys_User = sysUser;
            ListRptSvSysAccess = new List<RptSv_Sys_Access>();
            if (listSysAccess != null && listSysAccess.Count > 0)
            {
                ListRptSvSysAccess.AddRange(listSysAccess);
            }
        }
    }
}