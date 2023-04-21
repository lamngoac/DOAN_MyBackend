using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.State
{
    public class UserState
    {
        public string UserName { get; set; }
        public string AddressAPIs { get; set; }

        public string AddressAPIs_ProductCentrer { get; set; }
        public string TokenID { get; set; }
        public string MST { get; set; }
        public bool IsSysAdmin { get; set; }
        public Sys_User SysUser { get; set; }
        public Mst_NNT Mst_NNT { get; set; }
        public List<Sys_Access> ListSysAccess { get; set; }
        public string UtcOffset { get; set; }
        public UserState()
        {
        }
        public UserState(Sys_User sysUser)
        {
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            SysUser = sysUser;
            UtcOffset = null;
        }

        public UserState(Sys_User sysUser, List<Sys_Access> listSysAccess)
        {
            UtcOffset = null;
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            SysUser = sysUser;
            ListSysAccess = new List<Sys_Access>();
            if (listSysAccess != null && listSysAccess.Count > 0)
            {
                ListSysAccess.AddRange(listSysAccess);
            }
        }

        public UserState(Sys_User sysUser, Mst_NNT mstNNT, List<Sys_Access> listSysAccess)
        {
            UtcOffset = null;
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            SysUser = sysUser;
            ListSysAccess = new List<Sys_Access>();
            if (listSysAccess != null && listSysAccess.Count > 0)
            {
                ListSysAccess.AddRange(listSysAccess);
            }

            if (mstNNT != null)
            {
                Mst_NNT = mstNNT;
            }
            else
            {
                Mst_NNT = new Mst_NNT()
                {
                    NetworkID = "",
                    OrgID = "",
                };
            }
        }
    }
}