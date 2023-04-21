using idn.Skycic.Inventory.Common.Models;
using inos.common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Website.State
{
    public class UserState
    {
        public string UserName { get; set; }
        public string UserType { get; set; }
        public string AddressAPIs { get; set; }
        public string AccessToken { get; set; }
        public string AddressAPIs_Product_Customer_Centrer { get; set; }
        public string AddressAPIs_SkycicVeloca { get; set; }
        public string AddressAPIs_SkycicDMS { get; set; }
        public string AddressAPIs_SkycicLQDMS { get; set; }
        public string NetworkID { get; set; }
        public string OrgID { get; set; }
        public string MST { get; set; }
        public bool IsSysAdmin { get; set; }
        public HttpSessionStateBase HttpSessionStateBase { get; set; }
        public Sys_User SysUser { get; set; }
        public InosUser InosUser { get; set; }
        public Mst_NNT Mst_NNT { get; set; }
        public List<Sys_Access> ListSysAccess { get; set; }
        public List<OrgSolution> ListOrgSolution { get; set; }
        public RT_Mst_Sys_Config RT_Mst_Sys_Config { get; set; }
        public double UtcOffset { get; set; }
        public UserState()
        {
        }
        public UserState(Sys_User sysUser)
        {
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            SysUser = sysUser;
        }

        public UserState(Sys_User sysUser, List<Sys_Access> listSysAccess)
        {
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

        public UserState(Sys_User sysUser, InosUser inosUser, Mst_NNT mstNNT, RT_Mst_Sys_Config objRT_Mst_Sys_Config, List<Sys_Access> listSysAccess)
        {
            UserName = sysUser.UserCode;
            IsSysAdmin = sysUser.FlagSysAdmin.Equals("1") ? true : false;
            SysUser = sysUser;
            InosUser = inosUser;
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

            if(objRT_Mst_Sys_Config != null)
            {
                RT_Mst_Sys_Config = objRT_Mst_Sys_Config;
            }
            else
            {
                RT_Mst_Sys_Config = new RT_Mst_Sys_Config()
                {
                    Lst_Mst_Sys_Config = new List<Mst_Sys_Config>()
                };
            }
        }
    }

    public class UserReportState
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AddressAPIs { get; set; }
        public string AccessToken { get; set; }
        public string NetworkID { get; set; }
        public double UtcOffset { get; set; }
        public UserReportState()
        {
        }

    }
}