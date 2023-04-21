using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Collections;
using System.Xml;
using System.Linq;
using System.Threading;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TDAL = EzDAL.MyDB;
using TDALUtils = EzDAL.Utils;
using TConst = idn.Skycic.Inventory.Constants;
using TUtils = idn.Skycic.Inventory.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Diagnostics;
using inos.common.Model;
using System.Globalization;

namespace idn.Skycic.Inventory.Biz
{
	public partial class BizidNInventory
    {
        #region // Sys Mix:
        private DataRow Sys_User_GetAbilityViewOfUser(
            object strUserCode
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select
						su.UserCode
						, su.MST
                        , mnnt.OrgID
						, su.DepartmentCode
						, su.UserName
						, su.FlagSysAdmin
						, su.FlagActive
						, su.FlagBG
						---
						, mnnt.MST MNNTMST
						, mnnt.MSTBUPattern MNNTMSTBUPattern
						, mnnt.OrgID MNNTOrgID
						, mnnt.DealerType MNNTDealerType
                        ---
                        , mo.OrgID mo_OrgID
						, mo.OrgBUCode mo_OrgBUCode
						, mo.OrgBUPattern mo_OrgBUPattern
						, mo.OrgLevel mo_OrgLevel
						, mo.OrgParent mo_OrgParent
					from Sys_User su --//[mylock]
						left join Mst_NNT mnnt --//[mylock]
							on su.MST = mnnt.MST
                        left join Mst_Org mo --//[mylock]
							on mnnt.OrgID = mo.OrgID
					where
						su.UserCode = @strUserCode
					;
				"
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strUserCode", strUserCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                throw new Exception(TError.ErridnInventory.Sys_User_BizInvalidUserAbility);
            }

            // Return Good:
            return dt.Rows[0];
        }

        private DataRow Sys_User_GetAbilityWriteOfUser(
            object strUserCode
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select
						su.UserCode
						, su.MST
						, su.DepartmentCode
						, su.UserName
						, su.FlagSysAdmin
						, su.FlagActive
						, su.FlagBG
						, su.FlagSysAdmin
						---
						, mnnt.MST MNNTMST
						, mnnt.MSTBUPattern MNNTMSTBUPattern
						, mnnt.OrgID MNNTOrgID
					from Sys_User su --//[mylock]
						left join Mst_NNT mnnt --//[mylock]
							on su.MST = mnnt.MST
					where
						su.UserCode = @strUserCode
					;
				"
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strUserCode", strUserCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                throw new Exception(TError.ErridnInventory.Sys_User_BizInvalidUserAbility);
            }

            // Return Good:
            return dt.Rows[0];
        }

        private void zzzzClauseSelect_Mst_Org_ViewAbility_Get(
            string strOrgID
            , ref ArrayList alParamsCoupleError
            )
        {
            //// Check:
            strOrgID = TUtils.CUtils.StdParam(strOrgID);
            if (strOrgID == null || strOrgID == "")
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strOrgID", strOrgID
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Mst_Org_CheckDB_OrgIDNotFound
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            ////
            // Init:
            string strSql = "--- Nothing.";
            {
                strSql = CmUtils.StringUtils.Replace(@"
						---- #tbl_Mst_Org_Filter:
						select 
							mo.OrgID
							, mo.OrgBUPattern
						into #tbl_Mst_Org_Filter
						from Mst_Org mo --//[mylock]
						where (1=1)
							and (mo.OrgID = '@strOrgID')
						;


						select distinct
							mo.OrgID
						into #tbl_Mst_Org_ViewAbility
						from Mst_Org mo --//[mylock]
							inner join #tbl_Mst_Org_Filter f --//[mylock]
								on (1=1)
						where (1=1)
							and mo.OrgBUCode like f.OrgBUPattern
						;

                        --select t.* from #tbl_Mst_Org_Filter t
                        select t.* from #tbl_Mst_Org_ViewAbility t
					"
                    , "@strOrgID", strOrgID
                    );
                //alParamsCoupleSql.AddRange(new object[]{
                //    "@p_MSTBUPattern", drAbilityOfUser["MNNTMSTBUPattern"]
                //    });
                DataSet ds = _cf.db.ExecQuery(strSql);
            }

            // Return Good:
        }
        private void Sys_User_CheckAuthentication(
            ref ArrayList alParamsCoupleError
            , object strWAUserCode
            , object strWAUserPassword
            )
        {
            // Sys_User_CheckDB:
            DataTable dt_Sys_User = null;

            Sys_User_CheckDB(
                ref alParamsCoupleError // alParamsCoupleError
                , strWAUserCode // strUserCode
                , TConst.Flag.Active // strFlagExistToCheck
                , TConst.Flag.Active // strFlagActiveListToCheck
                , out dt_Sys_User // dt_Sys_User
                );

            // CheckPassword:
            if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash((string)strWAUserPassword), dt_Sys_User.Rows[0]["UserPassword"]))
            {
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_User_CheckAuthentication_InvalidPassword // strErrorCode
                    , null // excInner
                    , alParamsCoupleError.ToArray() // arrobjParamsCouple
                    );
            }
        }

        private void RptSv_Sys_User_CheckAuthentication(
            ref ArrayList alParamsCoupleError
            , object strWAUserCode
            , object strWAUserPassword
            )
        {
            // RptSv_Sys_User_CheckDB:
            DataTable dt_RptSv_Sys_User = null;

            RptSv_Sys_User_CheckDB(
                ref alParamsCoupleError // alParamsCoupleError
                , strWAUserCode // strUserCode
                , TConst.Flag.Active // strFlagExistToCheck
                , TConst.Flag.Active // strFlagActiveListToCheck
                , out dt_RptSv_Sys_User // dt_RptSv_Sys_User
                );

            // CheckPassword:
            if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash((string)strWAUserPassword), dt_RptSv_Sys_User.Rows[0]["UserPassword"]))
            {
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.RptSv_Sys_User_CheckAuthentication_InvalidPassword // strErrorCode
                    , null // excInner
                    , alParamsCoupleError.ToArray() // arrobjParamsCouple
                    );
            }
        }

        private void MstSv_Sys_User_CheckAuthentication(
            ref ArrayList alParamsCoupleError
            , object strWAUserCode
            , object strWAUserPassword
            )
        {
            // MstSv_Sys_User_CheckDB:
            DataTable dt_MstSv_Sys_User = null;

            MstSv_Sys_User_CheckDB(
                ref alParamsCoupleError // alParamsCoupleError
                , strWAUserCode // strUserCode
                , TConst.Flag.Active // strFlagExistToCheck
                , TConst.Flag.Active // strFlagActiveListToCheck
                , out dt_MstSv_Sys_User // dt_MstSv_Sys_User
                );

            // CheckPassword:
            if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash((string)strWAUserPassword), dt_MstSv_Sys_User.Rows[0]["UserPassword"]))
            {
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.MstSv_Sys_User_CheckAuthentication_InvalidPassword // strErrorCode
                    , null // excInner
                    , alParamsCoupleError.ToArray() // arrobjParamsCouple
                    );
            }
        }

        private void myCache_Mst_AreaMarket_ViewAbility_Get(
            DataRow drAbilityOfUser
            , string zzB_tbl_Mst_AreaMarket_ViewAbility_zzE = "#tbl_Mst_AreaMarket_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select 
							Cast(null as nvarchar(400)) AreaCode
						into zzB_tbl_Mst_AreaMarket_ViewAbility_zzE
						where (0=1)
						;
				"
                , "zzB_tbl_Mst_AreaMarket_ViewAbility_zzE", zzB_tbl_Mst_AreaMarket_ViewAbility_zzE
                );
            _cf.db.ExecQuery(strSql);

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_AreaMarket_ViewAbility_zzE(AreaCode)
						select distinct
							mam.AreaCode 
						from Mst_AreaMarket mam --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_AreaMarket_ViewAbility_zzE", zzB_tbl_Mst_AreaMarket_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    );
            }
            else if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select 
							mam.AreaBUPattern
						into #tbl_Sys_User_AreaBUPattern
						from Sys_User su --//[mylock]
							left join Mst_AreaMarket mam --//[mylock]
								on su.AreaCode = mam.AreaCode
						where (1=1)
							and su.UserCode = @strUserCode
						;

						insert into zzB_tbl_Mst_AreaMarket_ViewAbility_zzE(AreaCode)
						select distinct
							mam.AreaCode 
						from Mst_AreaMarket mam --//[mylock]
							left join #tbl_Sys_User_AreaBUPattern f --//[mylock]
								on (1=1)
						where (1=1)
							and (mam.AreaBUCode like f.AreaBUPattern)
						;
					"
                    , "zzB_tbl_Mst_AreaMarket_ViewAbility_zzE", zzB_tbl_Mst_AreaMarket_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@strUserCode", drAbilityOfUser["UserCode"]
                    );
            }
        }
        private DataRow myCache_Mst_AreaMarket_ViewAbility_CheckAccessAreaMarket(
            ref ArrayList alParamsCoupleError
            , DataRow drAbilityOfUser
            , object strAreaCode
            , string zzB_tbl_Mst_AreaMarket_ViewAbility_zzE = "#tbl_Mst_AreaMarket_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select 
						*
					from zzB_tbl_Mst_AreaMarket_ViewAbility_zzE t --//[mylock]
					where 
						t.AreaCode = @strAreaCode
					;
				"
                , "zzB_tbl_Mst_AreaMarket_ViewAbility_zzE", zzB_tbl_Mst_AreaMarket_ViewAbility_zzE
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strAreaCode", strAreaCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CurrUser.AreaCode", drAbilityOfUser["MAMAreaCode"]
                    , "Check.CurrUser.AreaBUCode", drAbilityOfUser["MAMAreaBUCode"]
                    , "Check.CurrUser.AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                    , "Check.strAreaCode", strAreaCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_ViewAbility_Deny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            // Return Good:
            return dt.Rows[0];
        }
        private void myCache_Mst_Distributor_ViewAbility_Get(
            DataRow drAbilityOfUser
            , string zzB_tbl_Mst_Distributor_ViewAbility_zzE = "#tbl_Mst_Distributor_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select 
							Cast(null as nvarchar(400)) DBCode
						into zzB_tbl_Mst_Distributor_ViewAbility_zzE
						where (0=1)
						;
				"
                , "zzB_tbl_Mst_Distributor_ViewAbility_zzE", zzB_tbl_Mst_Distributor_ViewAbility_zzE
                );
            _cf.db.ExecQuery(strSql);

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Distributor_ViewAbility_zzE(DBCode)
						select distinct
							md.DBCode
						from Mst_Distributor md --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_Distributor_ViewAbility_zzE", zzB_tbl_Mst_Distributor_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    );
            }
            else if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Distributor_ViewAbility_zzE(DBCode)
						select distinct
							md.DBCode
						from Mst_AreaMarket mam --//[mylock]
							inner join Mst_Distributor md --//[mylock]
								on mam.AreaCode = md.AreaCode
						where (1=1)
							and (mam.AreaBUCode like @p_va_Distributor_AreaBUPattern)
						;
					"
                    , "zzB_tbl_Mst_Distributor_ViewAbility_zzE", zzB_tbl_Mst_Distributor_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@p_va_Distributor_AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                    );
            }
            else if (!CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Distributor_ViewAbility_zzE(DBCode)
						select distinct
							md.DBCode
						from Mst_Distributor md --//[mylock]
						where (1=1)
							and (md.DBBUCode = @p_va_Distributor_BUCode)
						;
					"
                    , "zzB_tbl_Mst_Distributor_ViewAbility_zzE", zzB_tbl_Mst_Distributor_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@p_va_Distributor_BUCode", drAbilityOfUser["MBDBBUCode"]
                    );
            }

            // Return Good:
            //return strSql;
        }
        private DataRow myCache_ViewAbility_CheckAccessDistributor(
            ref ArrayList alParamsCoupleError
            , DataRow drAbilityOfUser
            , object strDBCode
            , string zzB_tbl_Mst_Distributor_ViewAbility_zzE = "#tbl_Mst_Distributor_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select
						*
					from zzB_tbl_Mst_Distributor_ViewAbility_zzE t --//[mylock]
					where
						t.DBCode = @strDBCode
					;
				"
                , "zzB_tbl_Mst_Distributor_ViewAbility_zzE", zzB_tbl_Mst_Distributor_ViewAbility_zzE
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strDBCode", strDBCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CurrUser.DBCode", drAbilityOfUser["DBCode"]
                    , "Check.CurrUser.MBDBBUCode", drAbilityOfUser["MBDBBUCode"]
                    , "Check.CurrUser.DBBUPattern", drAbilityOfUser["MBDBBUPattern"]
                    , "Check.CurrUser.AreaCode", drAbilityOfUser["MAMAreaCode"]
                    , "Check.CurrUser.AreaBUCode", drAbilityOfUser["MAMAreaBUCode"]
                    , "Check.CurrUser.AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                    , "Check.strDBCode", strDBCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_ViewAbility_Deny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            // Return Good:
            return dt.Rows[0];
        }
        private void myCache_ViewAbility_CheckExactUser(
            ref ArrayList alParamsCoupleError
            , DataRow drAbilityOfUser
            , object objUserCodeExact
            )
        {
            if (!CmUtils.StringUtils.StringEqualIgnoreCase(objUserCodeExact, _cf.sinf.strUserCode))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CurrUser.UserCode", _cf.sinf.strUserCode
                    , "Check.objUserCodeExact", objUserCodeExact
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_ViewAbility_NotExactUser
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }
        private void myCache_ViewAbility_GetOutletInfo(
            DataRow drAbilityOfUser
            , string zzB_tbl_Mst_Outlet_ViewAbility_zzE = "#tbl_Mst_Outlet_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select 
							Cast(null as nvarchar(400)) OLCode
						into zzB_tbl_Mst_Outlet_ViewAbility_zzE
						where (0=1)
						;
				"
                , "zzB_tbl_Mst_Outlet_ViewAbility_zzE", zzB_tbl_Mst_Outlet_ViewAbility_zzE
                );
            _cf.db.ExecQuery(strSql);

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Outlet_ViewAbility_zzE(OLCode)
						select distinct
							mo.OLCode
						from Mst_Outlet mo --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_Outlet_ViewAbility_zzE", zzB_tbl_Mst_Outlet_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    );
            }
            else if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Outlet_ViewAbility_zzE(OLCode)
						select distinct
							mo.OLCode
						from Mst_AreaMarket mam --//[mylock]
							inner join Mst_Distributor md --//[mylock]
								on mam.AreaCode = md.AreaCode
							inner join Mst_Outlet mo --//[mylock]
								on md.DBCode = mo.DBCode
						where (1=1)
							and (mam.AreaBUCode like @p_va_Outlet_AreaBUPattern)
						;
					"
                    , "zzB_tbl_Mst_Outlet_ViewAbility_zzE", zzB_tbl_Mst_Outlet_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@p_va_Outlet_AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                );
            }
            else if (!CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Outlet_ViewAbility_zzE(OLCode)
						select distinct
							mo.OLCode
						from Mst_Outlet mo --//[mylock]
							left join Mst_Distributor md --//[mylock]
								on mo.DBCode = md.DBCode
						where (1=1)
							and (md.DBBUCode = @p_va_Dealer_BUCode)
						;
					"
                    , "zzB_tbl_Mst_Outlet_ViewAbility_zzE", zzB_tbl_Mst_Outlet_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@p_va_Dealer_BUCode", drAbilityOfUser["MBDBBUCode"]
                    );
            }
        }
        private DataRow myCache_ViewAbility_CheckAccessOutlet(
            ref ArrayList alParamsCoupleError
            , DataRow drAbilityOfUser
            , object strOLCode
            , string zzB_tbl_Mst_Outlet_ViewAbility_zzE = "#tbl_Mst_Outlet_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
				select 
					*
				from zzB_tbl_Mst_Outlet_ViewAbility_zzE
				where
					t.OLCode = @strOLCode
				;
				"
                , "zzB_tbl_Mst_Outlet_ViewAbility_zzE", zzB_tbl_Mst_Outlet_ViewAbility_zzE
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strOLCode", strOLCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CurrUser.DBCode", drAbilityOfUser["DBCode"]
                    , "Check.CurrUser.MBDBBUCode", drAbilityOfUser["MBDBBUCode"]
                    , "Check.CurrUser.DBBUPattern", drAbilityOfUser["MBDBBUPattern"]
                    , "Check.CurrUser.AreaCode", drAbilityOfUser["MAMAreaCode"]
                    , "Check.CurrUser.AreaBUCode", drAbilityOfUser["MAMAreaBUCode"]
                    , "Check.CurrUser.AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                    , "Check.strOLCode", strOLCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_ViewAbility_Deny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            // Return Good:
            return dt.Rows[0];
        }
        private void myCache_Mst_Organ_ViewAbility_Get(
            DataRow drAbilityOfUser
            , string zzB_tbl_Mst_Organ_ViewAbility_zzE = "#tbl_Mst_Organ_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select 
							Cast(null as nvarchar(400)) OrganCode
						into zzB_tbl_Mst_Organ_ViewAbility_zzE
						where (0=1)
						;
				"
                , "zzB_tbl_Mst_Organ_ViewAbility_zzE", zzB_tbl_Mst_Organ_ViewAbility_zzE
                );
            _cf.db.ExecQuery(strSql);

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Organ_ViewAbility_zzE(OrganCode)
						select distinct
							mo.OrganCode
						from Mst_Organ mo --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_Organ_ViewAbility_zzE", zzB_tbl_Mst_Organ_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    );
            }
            else
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Organ_ViewAbility_zzE(OrganCode)
						select 
							muio.OrganCode
						from Map_UserInOrgan muio --//[mylock]
						where (1=1)
							and muio.UserCode = @strUserCode
						;
					"
                    , "zzB_tbl_Mst_Organ_ViewAbility_zzE", zzB_tbl_Mst_Organ_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@strUserCode", drAbilityOfUser["UserCode"]
                    );
            }

            // Return Good:
            //return strSql;
        }
        private DataRow myCache_ViewAbility_CheckAccessOrgan(
            ref ArrayList alParamsCoupleError
            , DataRow drAbilityOfUser
            , object strOrganCode
            , string zzB_tbl_Mst_Organ_ViewAbility_zzE = "#tbl_Mst_Organ_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select
						*
					from zzB_tbl_Mst_Organ_ViewAbility_zzE t --//[mylock]
					where
						t.OrganCode = @strOrganCode
					;
				"
                , "zzB_tbl_Mst_Organ_ViewAbility_zzE", zzB_tbl_Mst_Organ_ViewAbility_zzE
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strOrganCode", strOrganCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CurrUser.UserCode", drAbilityOfUser["UserCode"]
                    , "Check.CurrUser.OrganCode", drAbilityOfUser["OrganCode"]
                    , "Check.strOrganCode", strOrganCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_ViewAbility_Deny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            // Return Good:
            return dt.Rows[0];
        }
        private string zzzzClauseSelect_Mst_Outlet_ViewAbility_Get(
            DataRow drAbilityOfUser
            , ref ArrayList alParamsCoupleSql
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
						where (0=1)
						;
				");

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
						where (1=1)
						;
					");
            }
            else if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_AreaMarket mam --//[mylock]
							inner join Mst_Distributor md --//[mylock]
								on mam.AreaCode = md.AreaCode
							inner join Mst_Outlet mo --//[mylock]
								on md.DBCode = mo.DBCode
						where (1=1)
							and (mam.AreaBUCode like @p_va_Outlet_AreaBUPattern)
						;
					");
                alParamsCoupleSql.AddRange(new object[]{
                    "@p_va_Outlet_AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                    });
            }
            else if (!CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot)
                )
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
						where (1=1)
							and (mo.DBCode = @p_va_Outlet_DBCode)
						;
					");
                alParamsCoupleSql.AddRange(new object[]{
                    "@p_va_Outlet_DBCode", drAbilityOfUser["DBCode"]
                    });
            }

            // Return Good:
            return strSql;
        }

        private void myCache_Sys_User_ViewAbilityUser_Get(
            DataRow drAbilityOfUser
            , string zzB_tbl_Sys_User_ViewAbility_Read_zzE = "#tbl_Sys_User_ViewAbility_Read"
            , string zzB_tbl_Sys_User_ViewAbility_Write_zzE = "#tbl_Sys_User_ViewAbility_Write"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
                        select
	                        t.OrgID
                        into #tbl_Mst_Org
                        from Mst_Org t --//[mylock]
                        where (1=1)
                            and t.OrgBUCode like '@strOrgID'
                        ;

                        select 
							Cast(null as nvarchar(400)) UserCode
						into zzB_tbl_Sys_User_ViewAbility_Read_zzE
						where (0=1)
						;

						select 
							Cast(null as nvarchar(400)) UserCode
						into zzB_tbl_Sys_User_ViewAbility_Write_zzE
						where (0=1)
						;
				"
               , "zzB_tbl_Sys_User_ViewAbility_Read_zzE", zzB_tbl_Sys_User_ViewAbility_Read_zzE
               , "zzB_tbl_Sys_User_ViewAbility_Write_zzE", zzB_tbl_Sys_User_ViewAbility_Write_zzE
               , "@strOrgID", drAbilityOfUser["mo_OrgBUPattern"]
               );
            _cf.db.ExecQuery(strSql);

            //string strSqlCheck = CmUtils.StringUtils.Replace(@"select * from #tbl_Mst_Org");
            //DataTable dtCheck = _cf.db.ExecQuery(strSqlCheck).Tables[0];

            // Cases: UserBG.
            if ((CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagBG"], TConst.Flag.Active)))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						-------- Case: UserBackGround --------:
						insert into zzB_tbl_Sys_User_ViewAbility_Read_zzE(UserCode)
						select distinct
							t.UserCode
						from Sys_User t --//[mylock]
						where (1=1)
						;


						insert into zzB_tbl_Sys_User_ViewAbility_Write_zzE(UserCode)
						select distinct
							t.UserCode
						from Sys_User t --//[mylock]
						where (1=1)
						;

					"
                    , "zzB_tbl_Sys_User_ViewAbility_Read_zzE", zzB_tbl_Sys_User_ViewAbility_Read_zzE
                    , "zzB_tbl_Sys_User_ViewAbility_Write_zzE", zzB_tbl_Sys_User_ViewAbility_Write_zzE
                    );

                DataSet ds = _cf.db.ExecQuery(
                    strSql
                    );
            }
            // Cases: FlagViewOrgCreate = 1.
            else
            {
                strSql = CmUtils.StringUtils.Replace(@"
						-------- Case: UserBackGround --------:
						insert into zzB_tbl_Sys_User_ViewAbility_Read_zzE(UserCode)
						select
	                        su.UserCode
                        from #tbl_Mst_Org mo --//[mylock]
                            inner join Mst_NNT mnnt --//[mylock]
	                           on mo.OrgID = mnnt.OrgID
                            inner join Sys_User  su --//[mylock]
	                           on su.MST = mnnt.MST
                        where (1=1)
                        ;


						insert into zzB_tbl_Sys_User_ViewAbility_Write_zzE(UserCode)
						select
	                        su.UserCode
                        from #tbl_Mst_Org mo --//[mylock]
                            inner join Mst_NNT mnnt --//[mylock]
	                           on mo.OrgID = mnnt.OrgID
                            inner join Sys_User  su --//[mylock]
	                           on su.MST = mnnt.MST
                        where (1=1)
                        ;

					"
                    , "zzB_tbl_Sys_User_ViewAbility_Read_zzE", zzB_tbl_Sys_User_ViewAbility_Read_zzE
                    , "zzB_tbl_Sys_User_ViewAbility_Write_zzE", zzB_tbl_Sys_User_ViewAbility_Write_zzE
                    );

                DataSet ds = _cf.db.ExecQuery(
                    strSql
                    );
            }
        }

        private string zzzzClauseSelect_Mst_Outlet_ByRouting_ViewAbility_Get(
            DataRow drAbilityOfUser
            , ref ArrayList alParamsCoupleSql
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
						where (0=1)
						;
				");

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
						where (1=1)
						;
					");
            }
            else if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Distributor_ViewAbility
						from Mst_AreaMarket mam --//[mylock]
							inner join Mst_Distributor md --//[mylock]
								on mam.AreaCode = md.AreaCode
							inner join Mst_Outlet mo --//[mylock]
								on md.DBCode = mo.DBCode
						where (1=1)
							and (mam.AreaBUCode like @p_va_Outlet_AreaBUPattern)
						;
					");
                alParamsCoupleSql.AddRange(new object[]{
                    "@p_va_Outlet_AreaBUPattern", drAbilityOfUser["MAMAreaBUPattern"]
                    });
            }
            else if (!CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot)
                        && CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagDBAdmin"], TConst.Flag.Yes)
                )
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
						where (1=1)
							and (mo.DBCode = @p_va_Outlet_DBCode)
						;
					");
                alParamsCoupleSql.AddRange(new object[]{
                    "@p_va_Outlet_DBCode", drAbilityOfUser["DBCode"]
                    });
            }
            else if (!CmUtils.StringUtils.StringEqual(drAbilityOfUser["DBCode"], TConst.BizMix.DBCodeRoot)
                        && !CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagDBAdmin"], TConst.Flag.Yes)
                )
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select distinct
							mo.OLCode
						into #tbl_Mst_Outlet_ViewAbility
						from Mst_Outlet mo --//[mylock]
							inner join DB_Routing dbr --//[mylock]
								on mo.DBCode = dbr.DBCode
							inner join DB_RoutingDetail dbrd --//[mylock]
								on dbr.RTCode = dbrd.RTCode
						where (1=1)
							and (mo.DBCode = @p_va_Outlet_DBCode)
							and (mo.OLStatus = '1')
							and (dbr.UserCodeSM = @p_va_Outlet_UserCode)
							and (dbr.RTStatus = '1')
						;
					");
                alParamsCoupleSql.AddRange(new object[]{
                    "@p_va_Outlet_DBCode", drAbilityOfUser["DBCode"]
                    , "@p_va_Outlet_UserCode", drAbilityOfUser["UserCode"]
                    });
            }

            // Return Good:
            return strSql;
        }
        private string zzzzClauseSelect_DB_LimitPrdScope_Latest_Get10(
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
							---- #tbl_DB_LimitPrdScope_Filter_Draft01:
							select distinct
								dblps.LPCode
								, dblps.DBCode
								, dblp.ProductCode
								, dblp.CreateDTime
							into #tbl_DB_LimitPrdScope_Filter_Draft01
							from #tbl_Mst_Distributor_ViewAbility va_md --//[mylock]
								inner join DB_LimitPrdScope dblps --//[mylock]
									on va_md.DBCode  = dblps.DBCode -- Filter Ability
								inner join DB_LimitPrd dblp --//[mylock]
									on dblp.LPCode  = dblps.LPCode
							where (1=1)
								and (dblp.EffDateStart <= @strRefDate and @strRefDate <= dblp.EffDateEnd)
							;
						
							---- #tbl_DB_LimitPrdScope_Filter_Draft02:
							select
								t.DBCode
								, t.ProductCode
								, Max(t.CreateDTime) CreateDTime
							into #tbl_DB_LimitPrdScope_Filter_Draft02
							from #tbl_DB_LimitPrdScope_Filter_Draft01 t --//[mylock]
							group by
								t.DBCode
								, t.ProductCode
							;

							---- #tbl_DB_LimitPrdScope_Filter_Draft:
							select
								t.LPCode
								, t.DBCode
								, t.ProductCode
								, t.CreateDTime
							into #tbl_DB_LimitPrdScope_Filter_Draft
							from #tbl_DB_LimitPrdScope_Filter_Draft01 t --//[mylock]
								inner join #tbl_DB_LimitPrdScope_Filter_Draft02 t2 --//[mylock]
									on t.DBCode = t2.DBCode and t.CreateDTime = t2.CreateDTime
							;
				");

            // Return Good:
            return strSql;
        }
        private string zzzzClauseSelect_DB_LimitPrdScope_Latest_Get20_GetOODBDFilter(
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
							---- #tbl_OODBD_Filter:
							select distinct
								oodbd.ProductCode
								, oodb.DBCode
							into #tbl_OODBD_Filter
							from Ord_OrderDBDetail oodbd --//[mylock]
								inner join Ord_OrderDB oodb --//[mylock]
									on oodbd.OrderDBNo = oodb.OrderDBNo
							where (1=1)
								and (oodbd.OrderDBNo = @strOrderDBNo)
							;
				");

            // Return Good:
            return strSql;
        }
        private string zzzzClauseSelect_DB_LimitPrdScope_Latest_Get30_GetOverItems(
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
							---- Get OverItems:
							select
								s.*
								, pp.ProductDesc pp_ProductDesc
								, f.CreateDTime dblps_CreateDTime
								, dblps.LPCode dblps_LPCode
								, dblps.DBCode dblps_DBCode
								, dblps.QtyLimit dblps_QtyLimit
							from #tbl_OODBD_Sum s --//[mylock]
								inner join #tbl_DB_LimitPrdScope_Filter_Draft f --//[mylock]
									on s.ProductCode = f.ProductCode and s.DBCode = f.DBCode
								inner join DB_LimitPrdScope dblps --//[mylock]
									on f.LPCode = dblps.LPCode and f.DBCode = dblps.DBCode
								inner join Prd_Product pp --//[mylock]
									on s.ProductCode = pp.ProductCode
							where (1=1)
								and s.SumQty > dblps.QtyLimit
							;
				");

            // Return Good:
            return strSql;
        }

        // // 20181113.DũngND:
        private void myCache_Mst_Organ_RW_ViewAbility_Get(
            DataRow drAbilityOfUser
            , string zzB_tbl_Mst_Organ_ViewAbility_Read_zzE = "#tbl_Mst_Organ_ViewAbility_Read"
            , string zzB_tbl_Mst_Organ_ViewAbility_Write_zzE = "#tbl_Mst_Organ_ViewAbility_Write"
            )
        {
            //Init
            string strSql = CmUtils.StringUtils.Replace(@"
                        select 
							Cast(null as nvarchar(400)) OrganCode
						into zzB_tbl_Mst_Organ_ViewAbility_Read_zzE
						where (0=1)
						;
						
						select 
							Cast(null as nvarchar(400)) OrganCode
						into zzB_tbl_Mst_Organ_ViewAbility_Write_zzE
						where (0=1)
						;
				"
                , "zzB_tbl_Mst_Organ_ViewAbility_Read_zzE", zzB_tbl_Mst_Organ_ViewAbility_Read_zzE
                , "zzB_tbl_Mst_Organ_ViewAbility_Write_zzE", zzB_tbl_Mst_Organ_ViewAbility_Write_zzE
                );
            _cf.db.ExecQuery(strSql);

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Organ_ViewAbility_Read_zzE(OrganCode)
						select distinct
							mo.OrganCode
						from Mst_Organ mo --//[mylock]
						where (1=1)
						;

						insert into zzB_tbl_Mst_Organ_ViewAbility_Write_zzE(OrganCode)
						select distinct
							mo.OrganCode
						from Mst_Organ mo --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_Organ_ViewAbility_Read_zzE", zzB_tbl_Mst_Organ_ViewAbility_Read_zzE
                    , "zzB_tbl_Mst_Organ_ViewAbility_Write_zzE", zzB_tbl_Mst_Organ_ViewAbility_Write_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    );
            }
            else
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Organ_ViewAbility_Read_zzE(OrganCode)
						select 
							muio.OrganCode
						from Map_UserInOrgan muio --//[mylock]
						where (1=1)
							and muio.UserCode = @strUserCode
						;

						insert into zzB_tbl_Mst_Organ_ViewAbility_Write_zzE(OrganCode)
						select distinct
							mo.OrganCode
						from Mst_Organ mo --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_Organ_ViewAbility_Read_zzE", zzB_tbl_Mst_Organ_ViewAbility_Read_zzE
                    , "zzB_tbl_Mst_Organ_ViewAbility_Write_zzE", zzB_tbl_Mst_Organ_ViewAbility_Write_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@strUserCode", drAbilityOfUser["UserCode"]
                    );
            }

            // Return Good:
            //return strSql;
        }
        private void zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
            DataRow drAbilityOfUser
            , ref ArrayList alParamsCoupleSql
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_NNT_ViewAbility') is not null
                        drop table #tbl_Mst_NNT_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Mst_NNT_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (0=1)
						;
				");

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_NNT_ViewAbility') is not null
                        drop table #tbl_Mst_NNT_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Mst_NNT_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (1=1)
						;
					");
                _cf.db.ExecQuery(strSql);
            }
            ////
     //       else if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["MNNTDealerType"], "NPP"))
     //       {
     //           strSql = CmUtils.StringUtils.Replace(@"
     //                   if object_id('tempdb..#tbl_Mst_NNT_ViewAbility') is not null
     //                   drop table #tbl_Mst_NNT_ViewAbility;

					//	select distinct
					//		mnnt.MST
					//		, mnnt.OrgID
					//	into #tbl_Mst_NNT_ViewAbility
					//	from Mst_NNT mnnt --//[mylock]
					//	where (1=1)
     //                       and mnnt.DealerType = 'NPP'
					//	;
					//");
     //           _cf.db.ExecQuery(strSql);
     //       }
            else
            {
                strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_NNT_ViewAbility') is not null
                        drop table #tbl_Mst_NNT_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Mst_NNT_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (1=1)
							and (mnnt.MSTBUPattern like '@p_MSTBUPattern')
						;
					"
                    , "@p_MSTBUPattern", drAbilityOfUser["MNNTMSTBUPattern"]);
                //alParamsCoupleSql.AddRange(new object[]{
                //    "@p_MSTBUPattern", drAbilityOfUser["MNNTMSTBUPattern"]
                //    });
                _cf.db.ExecQuery(strSql);
            }

            // Return Good:
        }

        /// <summary>
        /// 20210927. Nâng cấp riêng cho tầm nhìn mh phân quyền kho: Org nào chỉ nhìn được kho của Org đó
        /// </summary>
        private void zzzzClauseSelect_Mst_NNT_ViewAbility_GetForInventory(
            DataRow drAbilityOfUser
            , ref ArrayList alParamsCoupleSql
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Mst_NNT_ViewAbility') is not null
                        drop table #tbl_Mst_NNT_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Mst_NNT_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (1=1)
							and (mnnt.OrgID = '@strOrgID')
						;
					"
                    , "@strOrgID", drAbilityOfUser["mo_OrgID"]
                );

            _cf.db.ExecQuery(strSql);
            // Return Good:
        }

        private void zzzzClauseSelect_Sys_User_ViewAbility_Get(
            DataRow drAbilityOfUser
            , ref ArrayList alParamsCoupleSql
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Sys_User_ViewAbility') is not null
                        drop table #tbl_Sys_User_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Sys_User_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (0=1)
						;
				");

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagBG"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Sys_User_ViewAbility') is not null
                        drop table #tbl_Sys_User_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Sys_User_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (1=1)
						;
					");
                _cf.db.ExecQuery(strSql);
            }
            ////
            else
            {
                strSql = CmUtils.StringUtils.Replace(@"
                        if object_id('tempdb..#tbl_Sys_User_ViewAbility') is not null
                        drop table #tbl_Sys_User_ViewAbility;

						select distinct
							mnnt.MST
							, mnnt.OrgID
						into #tbl_Sys_User_ViewAbility
						from Mst_NNT mnnt --//[mylock]
						where (1=1)
							and (mnnt.MSTBUPattern like '@p_MSTBUPattern')
						;
					"
                    , "@p_MSTBUPattern", drAbilityOfUser["MNNTMSTBUPattern"]);
                //alParamsCoupleSql.AddRange(new object[]{
                //    "@p_MSTBUPattern", drAbilityOfUser["MNNTMSTBUPattern"]
                //    });
                _cf.db.ExecQuery(strSql);
            }

            // Return Good:
        }

        private void zzzzClauseSelect_Mst_Inventory_ViewAbility_Get(
            string strWAUserCode
            , ref ArrayList alParamsCoupleSql
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"                        
                        if object_id('tempdb..#tbl_Mst_UserMapInventory_Filter') is not null
                        drop table #tbl_Mst_UserMapInventory_Filter;

						---- #tbl_Mst_UserMapInventory_Filter:
						select distinct
							mnnt.OrgID
							, mnnt.InvCode
							, mi.InvBUCode
							, mi.InvBUPattern
						into #tbl_Mst_UserMapInventory_Filter
						from Mst_UserMapInventory mnnt --//[mylock]
							inner join Mst_Inventory mi --//[mylock]
								on mnnt.OrgID = mi.OrgID
									and mnnt.InvCode = mi.InvCode
						where (1=1)
							and mnnt.UserCode = '@strWAUserCode'
						;
						
                        if object_id('tempdb..#tbl_Mst_Inventory_ViewAbility') is not null
                        drop table #tbl_Mst_Inventory_ViewAbility;
						select distinct
							t.OrgID
							, t.InvCode
						into #tbl_Mst_Inventory_ViewAbility
						from Mst_Inventory t --//[mylock]
							inner join #tbl_Mst_UserMapInventory_Filter f --//[mylock]
								on t.OrgID = f.OrgID
						where(1=1)
							and t.InvBUCode like f.InvBUPattern
						;

						--- Clear For Debug:
						drop table #tbl_Mst_UserMapInventory_Filter;
				"
                , "@strWAUserCode", strWAUserCode
                );

            _cf.db.ExecQuery(strSql);

            // Return Good:
        }

        private void zzzzClauseSelect_Mst_Inventory_ViewAbility_Get_New20200827(
            string strWAUserCode
            , string strFlagIsUserBG
            , ref ArrayList alParamsCoupleSql
            )
        {
            // //
            string strSql = null;

            if (CmUtils.StringUtils.StringEqual(strFlagIsUserBG, TConst.Flag.Active))
            {
                // Init:
                strSql = CmUtils.StringUtils.Replace(@"                       
                        if object_id('tempdb..#tbl_Mst_Inventory_ViewAbility') is not null
                        drop table #tbl_Mst_Inventory_ViewAbility;
						select distinct
							t.OrgID
							, t.InvCode
						into #tbl_Mst_Inventory_ViewAbility
						from Mst_Inventory t --//[mylock]						
						where(1=1)
						;
				    "
                    , "@strWAUserCode", strWAUserCode
                    );
            }
            else
            {
                // Init:
                strSql = CmUtils.StringUtils.Replace(@"                        
                        if object_id('tempdb..#tbl_Mst_UserMapInventory_Filter') is not null
                        drop table #tbl_Mst_UserMapInventory_Filter;

						---- #tbl_Mst_UserMapInventory_Filter:
						select distinct
							mnnt.OrgID
							, mnnt.InvCode
							, mi.InvBUCode
							, mi.InvBUPattern
						into #tbl_Mst_UserMapInventory_Filter
						from Mst_UserMapInventory mnnt --//[mylock]
							inner join Mst_Inventory mi --//[mylock]
								on mnnt.OrgID = mi.OrgID
									and mnnt.InvCode = mi.InvCode
						where (1=1)
							and mnnt.UserCode = '@strWAUserCode'
						;
						
                        if object_id('tempdb..#tbl_Mst_Inventory_ViewAbility') is not null
                        drop table #tbl_Mst_Inventory_ViewAbility;
						select distinct
							t.OrgID
							, t.InvCode
						into #tbl_Mst_Inventory_ViewAbility
						from Mst_Inventory t --//[mylock]
							inner join #tbl_Mst_UserMapInventory_Filter f --//[mylock]
								on t.OrgID = f.OrgID
						where(1=1)
							and t.InvBUCode like f.InvBUPattern
						;

						--- Clear For Debug:
						drop table #tbl_Mst_UserMapInventory_Filter;
				    "
                    , "@strWAUserCode", strWAUserCode
                    );
            }
            

            _cf.db.ExecQuery(strSql);

            // Return Good:
        }

        private DataRow myCache_ViewAbility_CheckAccessOrganWrite(
            ref ArrayList alParamsCoupleError
            , DataRow drAbilityOfUser
            , object strOrganCode
            , string zzB_tbl_Mst_Organ_ViewAbility_Write_zzE = "#tbl_Mst_Organ_ViewAbility_Write"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select
						*
					from zzB_tbl_Mst_Organ_ViewAbility_Write_zzE t --//[mylock]
					where
						t.OrganCode = @strOrganCode
					;
				"
                , "zzB_tbl_Mst_Organ_ViewAbility_Write_zzE", zzB_tbl_Mst_Organ_ViewAbility_Write_zzE
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strOrganCode", strOrganCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.CurrUser.UserCode", drAbilityOfUser["UserCode"]
                    , "Check.CurrUser.OrganCode", drAbilityOfUser["OrganCode"]
                    , "Check.strOrganCode", strOrganCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_ViewAbility_Deny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

            // Return Good:
            return dt.Rows[0];
        }

        private void myCache_Mst_Dealer_ViewAbility_Get(
            DataRow drAbilityOfUser
            , string zzB_tbl_Mst_Dealer_ViewAbility_zzE = "#tbl_Mst_Dealer_ViewAbility"
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
						select 
							Cast(null as nvarchar(400)) DLCode
						into zzB_tbl_Mst_Dealer_ViewAbility_zzE
						where (0=1)
						;
				"
                , "zzB_tbl_Mst_Dealer_ViewAbility_zzE", zzB_tbl_Mst_Dealer_ViewAbility_zzE
                );
            _cf.db.ExecQuery(strSql);

            // Cases:
            if (CmUtils.StringUtils.StringEqual(drAbilityOfUser["FlagSysAdmin"], TConst.Flag.Active))
            {
                strSql = CmUtils.StringUtils.Replace(@"
						insert into zzB_tbl_Mst_Dealer_ViewAbility_zzE(DLCode)
						select distinct
							mdl.DLCode 
						from Mst_Dealer mdl --//[mylock]
						where (1=1)
						;
					"
                    , "zzB_tbl_Mst_Dealer_ViewAbility_zzE", zzB_tbl_Mst_Dealer_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    );
            }
            else
            {
                strSql = CmUtils.StringUtils.Replace(@"
						select 
							mdl.DLBUPattern
						into #tbl_RptSv_Sys_User_DLBUPattern
						from RptSv_Sys_User su --//[mylock]
							left join Mst_Dealer mdl --//[mylock]
								on su.DLCode = mdl.DLCode
						where (1=1)
							and su.UserCode = @strUserCode
						;

						insert into zzB_tbl_Mst_Dealer_ViewAbility_zzE(DLCode)
						select distinct
							mdl.DLCode 
						from Mst_Dealer mdl --//[mylock]
							left join #tbl_RptSv_Sys_User_DLBUPattern f --//[mylock]
								on (1=1)
						where (1=1)
							and (mdl.DLBUCode like f.DLBUPattern)
						;
					"
                    , "zzB_tbl_Mst_Dealer_ViewAbility_zzE", zzB_tbl_Mst_Dealer_ViewAbility_zzE
                    );
                _cf.db.ExecQuery(
                    strSql
                    , "@strUserCode", drAbilityOfUser["UserCode"]
                    );
            }
        }
        private DataRow RptSv_Sys_User_GetAbilityViewOfUser(
            object strUserCode
            )
        {
            // Init:
            string strSql = CmUtils.StringUtils.Replace(@"
					select
	                    su.UserCode
	                    , su.DLCode
	                    , su.UserName
	                    , su.FlagSysAdmin
	                    , su.FlagActive
	                    ---
	                    , mdl.DLCode MDLDLCODE
	                    , mdl.DLBUPattern MDLDLBUPattern
                    from RptSv_Sys_User su --//[mylock]
	                    left join dbo.Mst_Dealer mdl --//[mylock]
		                    on su.DLCode = mdl.DLCode
					where
						su.UserCode = @strUserCode
					;
				"
                );
            DataTable dt = _cf.db.ExecQuery(
                strSql // strSqlQuery
                , "@strUserCode", strUserCode // arrParams couple items
                ).Tables[0];
            if (dt.Rows.Count < 1)
            {
                throw new Exception(TError.ErridnInventory.Sys_User_BizInvalidUserAbility);
            }

            // Return Good:
            return dt.Rows[0];
        }
        #endregion

        #region // Sys_Access:
        private void Sys_Access_CheckDB(
            ref ArrayList alParamsCoupleError
            , string strFunctionName
            )
        {

        }
        private void Sys_Access_CheckDeny(
            ref ArrayList alParamsCoupleError
            , object strObjectCode
            )
        {
            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                "@strUserCode", _cf.sinf.strUserCode
                , "@strObjectCode", strObjectCode
                });
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- Sys_Access:
						select distinct
							so.ObjectCode
						from Sys_User su --//[mylock]
							inner join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							inner join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode and sg.FlagActive = '1'
							inner join Sys_Access sa --//[mylock]
								on sg.GroupCode = sa.GroupCode
							inner join Sys_Object so --//[mylock]
								on sa.ObjectCode = so.ObjectCode and so.FlagActive = '1'
						where (1=1)
							and su.UserCode = @strUserCode
							and su.FlagActive = '1'
							and so.ObjectCode = @strObjectCode
							and so.FlagActive = '1'
						union -- distinct
						select distinct
							so.ObjectCode
						from Sys_User su --//[mylock]
							inner join Sys_Object so --//[mylock]
								on (1=1)
									and su.FlagSysAdmin = '1' 
									and su.UserCode = @strUserCode
									and su.FlagActive = '1'
									and so.ObjectCode = @strObjectCode
									and so.FlagActive = '1'
						;
					"
                );
            #endregion

            #region // Get Data and Check:
            DataTable dtGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                ).Tables[0];
            if (dtGetData.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strObjectCode", strObjectCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_Access_CheckDeny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion

        }

        private void Sys_Access_CheckDenyV30(
            ref ArrayList alParamsCoupleError
            , object strUserCode
            , object strObjectCode
            )
        {
            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                "@strUserCode", strUserCode
                , "@strObjectCode", strObjectCode
                });
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- Sys_Access:
						select 
							soim.ObjectCode
						from Sys_User su --//[mylock] 
							inner join Sys_UserLicenseModules sulm --//[mylock] 
								on su.UserCode = sulm.UserCode
							inner join Sys_ObjectInModules soim --//[mylock]
								on sulm.ModuleCode = soim.ModuleCode
							inner join Sys_Object so --//[mylock]
								on soim.ObjectCode = so.ObjectCode and so.FlagActive = '1' 
						where (1=1)
							and su.UserCode = @strUserCode
							and su.FlagActive = '1'
							and so.ObjectCode = @strObjectCode
							and so.FlagActive = '1'
						union -- distinct
						select distinct
							so.ObjectCode
						from Sys_User su --//[mylock]
							inner join Sys_Object so --//[mylock]
								on (1=1)
									and (su.FlagSysAdmin = '1' or su.FlagBG = '1')
									and su.UserCode = @strUserCode
									and su.FlagActive = '1'
									and so.ObjectCode = @strObjectCode
									and so.FlagActive = '1'
						;
					"
                );
            #endregion

            #region // Get Data and Check:
            DataTable dtGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                ).Tables[0];
            if (dtGetData.Rows.Count < 1)
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.strObjectCode", strObjectCode
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_Access_CheckDeny
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
            #endregion

        }

        //private void RptSv_Sys_Access_CheckDeny(
        //	ref ArrayList alParamsCoupleError
        //	, object strUserCode
        //	, object strObjectCode
        //	)
        //{
        //	#region // Build Sql:
        //	ArrayList alParamsCoupleSql = new ArrayList();
        //	alParamsCoupleSql.AddRange(new object[] {
        //		"@strUserCode", strUserCode
        //		, "@strObjectCode", strObjectCode
        //		});

        //	////
        //	DataTable dt_RptSv_Sys_User = null;

        //	RptSv_Sys_User_CheckDB(
        //		ref alParamsCoupleError // alParamsCoupleError
        //		, strUserCode // strUserCode
        //		, TConst.Flag.Yes // strFlagExistToCheck
        //		, TConst.Flag.Active // strFlagActiveListToCheck
        //		, out dt_RptSv_Sys_User // dt_RptSv_Sys_User
        //		);
        //	#endregion

        //	#region // Get Data and Check:
        //	if (!CmUtils.StringUtils.StringEqual(dt_RptSv_Sys_User.Rows[0]["FlagSysAdmin"], TConst.Flag.Active))
        //	{
        //		alParamsCoupleError.AddRange(new object[]{
        //			"Check.strObjectCode", strObjectCode
        //			});
        //		throw CmUtils.CMyException.Raise(
        //			TError.ErridnInventory.RptSv_Sys_Access_CheckDeny
        //			, null
        //			, alParamsCoupleError.ToArray()
        //			);
        //	}
        //	#endregion

        //}

        public DataSet Sys_Access_Get(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Access
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Access_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Access_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_Access", strRt_Cols_Sys_Access
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Access = (strRt_Cols_Sys_Access != null && strRt_Cols_Sys_Access.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = mySys_Access_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Access_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sa.GroupCode
                            , sa.ObjectCode
						into #tbl_Sys_Access_Filter_Draft
						from Sys_Access sa --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by sa.GroupCode asc
                                , sa.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Access_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Access_Filter:
						select
							t.*
						into #tbl_Sys_Access_Filter
						from #tbl_Sys_Access_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Access --------:
						zzzzClauseSelect_Sys_Access_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Access_Filter_Draft;
						--drop table #tbl_Sys_Access_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Access_zOut = "-- Nothing.";
                if (bGet_Sys_Access)
                {
                    #region // bGet_Sys_Access:
                    zzzzClauseSelect_Sys_Access_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Access:
							select
								t.MyIdxSeq
								, sa.*
								, so.ObjectCode so_ObjectCode
                                , so.ObjectName so_ObjectName 
                                , so.ServiceCode so_ServiceCode 
                                , so.ObjectType so_ObjectType 
                                , so.FlagExecModal so_FlagExecModal 
                                , so.FlagActive so_FlagActive 
							from #tbl_Sys_Access_Filter t --//[mylock]
								inner join Sys_Access sa --//[mylock]
									on t.GroupCode = sa.GroupCode
                                        and t.ObjectCode = sa.ObjectCode
                                left join Sys_Object so --//[mylock]
                                    on t.ObjectCode = so.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Access" // strTableNameDB
                            , "Sys_Access." // strPrefixStd
                            , "sa." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Access_zOut", zzzzClauseSelect_Sys_Access_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Access)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Access";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Access_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Access
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Access_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Access_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
			//// Return
				, "strRt_Cols_Sys_Access", strRt_Cols_Sys_Access
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Access = (strRt_Cols_Sys_Access != null && strRt_Cols_Sys_Access.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = mySys_Access_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Access_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sa.GroupCode
                            , sa.ObjectCode
						into #tbl_Sys_Access_Filter_Draft
						from Sys_Access sa --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by sa.GroupCode asc
                                , sa.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Access_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Access_Filter:
						select
							t.*
						into #tbl_Sys_Access_Filter
						from #tbl_Sys_Access_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Access --------:
						zzzzClauseSelect_Sys_Access_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Access_Filter_Draft;
						--drop table #tbl_Sys_Access_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Access_zOut = "-- Nothing.";
                if (bGet_Sys_Access)
                {
                    #region // bGet_Sys_Access:
                    zzzzClauseSelect_Sys_Access_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Access:
							select
								t.MyIdxSeq
								, sa.*
								, so.ObjectCode so_ObjectCode
                                , so.ObjectName so_ObjectName 
                                , so.ServiceCode so_ServiceCode 
                                , so.ObjectType so_ObjectType 
                                , so.FlagExecModal so_FlagExecModal 
                                , so.FlagActive so_FlagActive 
							from #tbl_Sys_Access_Filter t --//[mylock]
								inner join Sys_Access sa --//[mylock]
									on t.GroupCode = sa.GroupCode
                                        and t.ObjectCode = sa.ObjectCode
                                left join Sys_Object so --//[mylock]
                                    on t.ObjectCode = so.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Access" // strTableNameDB
                            , "Sys_Access." // strPrefixStd
                            , "sa." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Access_zOut", zzzzClauseSelect_Sys_Access_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Access)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Access";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Access_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Access objRQ_Sys_Access
            ////
            , out RT_Sys_Access objRT_Sys_Access
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Access.Tid;
            objRT_Sys_Access = new RT_Sys_Access();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Access.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Access_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Access_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Access> lst_Sys_Access = new List<Sys_Access>();
                #endregion

                #region // WS_Sys_Access_Get:
                mdsResult = Sys_Access_Get(
                    objRQ_Sys_Access.Tid // strTid
                    , objRQ_Sys_Access.GwUserCode // strGwUserCode
                    , objRQ_Sys_Access.GwPassword // strGwPassword
                    , objRQ_Sys_Access.WAUserCode // strUserCode
                    , objRQ_Sys_Access.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_Access.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Access.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Access.Ft_WhereClause // strFt_WhereClause
                                                      //// Return:
                    , objRQ_Sys_Access.Rt_Cols_Sys_Access // strRt_Cols_Sys_Access
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Access.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    DataTable dt_Sys_Access = mdsResult.Tables["Sys_Access"].Copy();
                    lst_Sys_Access = TUtils.DataTableCmUtils.ToListof<Sys_Access>(dt_Sys_Access);
                    objRT_Sys_Access.Lst_Sys_Access = lst_Sys_Access;
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_Access_Save(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object[] arrobjDSData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Access_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Access_Save;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                if (dsData == null) dsData = new DataSet("dsData");
                dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                    });
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                ////
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Group.Rows[0]["LogLUBy"] = _cf.sinf.strUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_Sys_Access = null;
                ////
                {
                    ////
                    string strTableCheck = "Sys_Access";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Access_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Sys_Access = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Sys_Access // dtData
                        , "StdParam", "ObjectCode" // arrstrCouple
                        );
                    ////
                    for (int nScan = 0; nScan < dtInput_Sys_Access.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dtInput_Sys_Access.Rows[nScan];

                        ////
                        DataTable dtDB_Sys_Object = null;

                        Sys_Object_CheckDB(
                            ref alParamsCoupleError // alParamsCoupleError
                            , drScan["ObjectCode"] // strObjectCode
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.Flag.Active // strFlagActiveListToCheck
                            , out dtDB_Sys_Object // dtDB_Sys_Object
                            );
                    }

                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Access" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ObjectCode" } // arrSingleStructure
                        , dtInput_Sys_Access // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_Access:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from Sys_Access t --//[mylock]
							inner join #tbl_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into Sys_Access(
							GroupCode
							, ObjectCode
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_sa.ObjectCode
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_Sys_Group t_sg --//[mylock]
							inner join #tbl_Sys_Access t_sa --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Access_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Access_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Access_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                //if (dsData == null) dsData = new DataSet("dsData");
                //dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                ////
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Group.Rows[0]["LogLUBy"] = strWAUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "NetworkID", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_Sys_Access = null;
                ////
                {
                    ////
                    string strTableCheck = "Sys_Access";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Access_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Sys_Access = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Sys_Access // dtData
                        , "StdParam", "ObjectCode" // arrstrCouple
                        );

                    ////
                    for (int nScan = 0; nScan < dtInput_Sys_Access.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dtInput_Sys_Access.Rows[nScan];

                        ////
                        DataTable dtDB_Sys_Object = null;

                        Sys_Object_CheckDB(
                            ref alParamsCoupleError // alParamsCoupleError
                            , drScan["ObjectCode"] // strObjectCode
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.Flag.Active // strFlagActiveListToCheck
                            , out dtDB_Sys_Object // dtDB_Sys_Object
                            );
                    }

                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Access" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ObjectCode" } // arrSingleStructure
                        , dtInput_Sys_Access // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_Access:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from Sys_Access t --//[mylock]
							inner join #tbl_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into Sys_Access(
							GroupCode
							, ObjectCode
							, NetworkID
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_sa.ObjectCode
							, t_sg.NetworkID
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_Sys_Group t_sg --//[mylock]
							inner join #tbl_Sys_Access t_sa --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Access_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Access objRQ_Sys_Access
            ////
            , out RT_Sys_Access objRT_Sys_Access
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Access.Tid;
            objRT_Sys_Access = new RT_Sys_Access();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_UserInGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Access_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Access_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_Sys_Access.Sys_Group)
                , "Lst_Sys_Access", TJson.JsonConvert.SerializeObject(objRQ_Sys_Access.Lst_Sys_Access)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Sys_Access = TUtils.DataTableCmUtils.ToDataTable<Sys_Access>(objRQ_Sys_Access.Lst_Sys_Access, "Sys_Access");
                    dsData.Tables.Add(dt_Sys_Access);
                }
                #endregion

                #region // Sys_Access_Delete:
                mdsResult = Sys_Access_Save(
                    objRQ_Sys_Access.Tid // strTid
                    , objRQ_Sys_Access.GwUserCode // strGwUserCode
                    , objRQ_Sys_Access.GwPassword // strGwPassword
                    , objRQ_Sys_Access.WAUserCode // strUserCode
                    , objRQ_Sys_Access.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Access.Sys_Group.GroupCode // objGroupCode
                    , dsData // dsData
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        #endregion

        #region // Sys_Group:
        private void Sys_Group_CheckDB(
            ref ArrayList alParamsCoupleError
            , object strGroupCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Sys_Group
            )
        {
            // GetInfo:
            dtDB_Sys_Group = TDALUtils.DBUtils.GetTableContents(
                _cf.db // db
                , "Sys_Group" // strTableName
                , "top 1 *" // strColumnList
                , "" // strClauseOrderBy
                , "GroupCode", "=", strGroupCode // arrobjParamsTriple item
                );

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Sys_Group.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GroupCodeNotFound", strGroupCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Group_CheckDB_GroupCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Sys_Group.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.GroupCodeExist", strGroupCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Group_CheckDB_GroupCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Sys_Group.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.GroupCode", strGroupCode
                    , "Check.FlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Sys_Group.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_Group_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

        }

        public DataSet Sys_Group_Get(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Group
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_Group", strRt_Cols_Sys_Group
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Group = (strRt_Cols_Sys_Group != null && strRt_Cols_Sys_Group.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                // drAbilityOfGroup:
                //DataRow drAbilityOfGroup = mySys_Group_GetAbilityViewBankOfGroup(_cf.sinf.strGroupCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfGroup", drAbilityOfGroup["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Group_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sg.GroupCode
						into #tbl_Sys_Group_Filter_Draft
						from Sys_Group sg --//[mylock]
							left join Sys_UserInGroup suig --//[mylock]
								on sg.GroupCode = suig.GroupCode
							left join Sys_User su --//[mylock]
								on suig.UserCode = su.UserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfGroup
							zzzzClauseWhere_strFilterWhereClause
						order by sg.GroupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Group_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Group_Filter:
						select
							t.*
						into #tbl_Sys_Group_Filter
						from #tbl_Sys_Group_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Group --------:
						zzzzClauseSelect_Sys_Group_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Group_Filter_Draft;
						--drop table #tbl_Sys_Group_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Group_zOut = "-- Nothing.";
                if (bGet_Sys_Group)
                {
                    #region // bGet_Sys_Group:
                    zzzzClauseSelect_Sys_Group_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Group:
							select
								t.MyIdxSeq
								, sg.*
							from #tbl_Sys_Group_Filter t --//[mylock]
								inner join Sys_Group sg --//[mylock]
									on t.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.BankCode su_BankCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_Group_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.GroupCode = suig.GroupCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Group_zOut", zzzzClauseSelect_Sys_Group_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Group)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Group";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_Group_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Group
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Sys_Group", strRt_Cols_Sys_Group
                , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Group = (strRt_Cols_Sys_Group != null && strRt_Cols_Sys_Group.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                // drAbilityOfGroup:
                //DataRow drAbilityOfGroup = mySys_Group_GetAbilityViewBankOfGroup(_cf.sinf.strGroupCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfGroup", drAbilityOfGroup["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Group_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sg.GroupCode
						into #tbl_Sys_Group_Filter_Draft
						from Sys_Group sg --//[mylock]
							left join Sys_UserInGroup suig --//[mylock]
								on sg.GroupCode = suig.GroupCode
							left join Sys_User su --//[mylock]
								on suig.UserCode = su.UserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfGroup
							zzzzClauseWhere_strFilterWhereClause
						order by sg.GroupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Group_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Group_Filter:
						select
							t.*
						into #tbl_Sys_Group_Filter
						from #tbl_Sys_Group_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Group --------:
						zzzzClauseSelect_Sys_Group_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Group_Filter_Draft;
						--drop table #tbl_Sys_Group_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Group_zOut = "-- Nothing.";
                if (bGet_Sys_Group)
                {
                    #region // bGet_Sys_Group:
                    zzzzClauseSelect_Sys_Group_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Group:
							select
								t.MyIdxSeq
								, sg.*
							from #tbl_Sys_Group_Filter t --//[mylock]
								inner join Sys_Group sg --//[mylock]
									on t.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_Group_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.GroupCode = suig.GroupCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Group_zOut", zzzzClauseSelect_Sys_Group_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Group)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Group";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Group_Get_New20191102(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Group
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Sys_Group", strRt_Cols_Sys_Group
                , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                //Sys_User_CheckAuthorize(
                //    strTid // strTid
                //    , strGwUserCode // strGwUserCode
                //    , strGwPassword // strGwPassword
                //    , strWAUserCode // strWAUserCode
                //                    //, strWAUserPassword // strWAUserPassword
                //    , ref mdsFinal // mdsFinal
                //    , ref alParamsCoupleError // alParamsCoupleError
                //    , dtimeSys // dtimeSys
                //    , strAccessToken // strAccessToken
                //    , strNetworkID // strNetworkID
                //    , strOrgID // strOrgID
                //    , TConst.Flag.Active // strFlagUserCodeToCheck
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Group = (strRt_Cols_Sys_Group != null && strRt_Cols_Sys_Group.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                // drAbilityOfGroup:
                //DataRow drAbilityOfGroup = mySys_Group_GetAbilityViewBankOfGroup(_cf.sinf.strGroupCode);

                #endregion

                #region // Build Sql:
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );

                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfGroup", drAbilityOfGroup["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Group_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sg.GroupCode
						into #tbl_Sys_Group_Filter_Draft
						from Sys_Group sg --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on sg.MST = t_MstNNT_View.MST
							left join Sys_UserInGroup suig --//[mylock]
								on sg.GroupCode = suig.GroupCode
							left join Sys_User su --//[mylock]
								on suig.UserCode = su.UserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfGroup
							zzzzClauseWhere_strFilterWhereClause
						order by sg.GroupCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Group_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Group_Filter:
						select
							t.*
						into #tbl_Sys_Group_Filter
						from #tbl_Sys_Group_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Group --------:
						zzzzClauseSelect_Sys_Group_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Group_Filter_Draft;
						--drop table #tbl_Sys_Group_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Group_zOut = "-- Nothing.";
                if (bGet_Sys_Group)
                {
                    #region // bGet_Sys_Group:
                    zzzzClauseSelect_Sys_Group_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Group:
							select
								t.MyIdxSeq
								, sg.*
							from #tbl_Sys_Group_Filter t --//[mylock]
								inner join Sys_Group sg --//[mylock]
									on t.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_Group_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.GroupCode = suig.GroupCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Group_zOut", zzzzClauseSelect_Sys_Group_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Group)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Group";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Group_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Group objRQ_Sys_Group
            ////
            , out RT_Sys_Group objRT_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Group.Tid;
            objRT_Sys_Group = new RT_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Group_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Group_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Group> lst_Sys_Group = new List<Sys_Group>();
                List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                bool bGet_Sys_Group = (objRQ_Sys_Group.Rt_Cols_Sys_Group != null && objRQ_Sys_Group.Rt_Cols_Sys_Group.Length > 0);
                bool bGet_Sys_UserInGroup = (objRQ_Sys_Group.Rt_Cols_Sys_UserInGroup != null && objRQ_Sys_Group.Rt_Cols_Sys_UserInGroup.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = Sys_Group_Get_New20191102(
                    objRQ_Sys_Group.Tid // strTid
                    , objRQ_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_Sys_Group.GwPassword // strGwPassword
                    , objRQ_Sys_Group.WAUserCode // strUserCode
                    , objRQ_Sys_Group.WAUserPassword // strUserPassword
                    , objRQ_Sys_Group.AccessToken
                    , objRQ_Sys_Group.NetworkID
                    , objRQ_Sys_Group.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Sys_Group.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_Group.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Group.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Group.Ft_WhereClause // strFt_WhereClause
                                                     //// Return:
                    , objRQ_Sys_Group.Rt_Cols_Sys_Group // strRt_Cols_Sys_Group
                    , objRQ_Sys_Group.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {

                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Group.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_Group)
                    {
                        ////
                        DataTable dt_Sys_User = mdsResult.Tables["Sys_Group"].Copy();
                        lst_Sys_Group = TUtils.DataTableCmUtils.ToListof<Sys_Group>(dt_Sys_User);
                        objRT_Sys_Group.Lst_Sys_Group = lst_Sys_Group;
                    }
                    // //
                    if (bGet_Sys_UserInGroup)
                    {
                        ////
                        DataTable dt_Sys_UserInGroup = mdsResult.Tables["Sys_UserInGroup"].Copy();
                        lst_Sys_UserInGroup = TUtils.DataTableCmUtils.ToListof<Sys_UserInGroup>(dt_Sys_UserInGroup);
                        objRT_Sys_Group.Lst_Sys_UserInGroup = lst_Sys_UserInGroup;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_Group_Create(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object objGroupName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Create;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    , "objGroupName", objGroupName
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                ////
                DataTable dtDB_Sys_Group = null;
                {
                    ////
                    if (strGroupCode == null || strGroupCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupCode", strGroupCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Create_InvalidGroupCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagPublicListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    if (strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Create_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Group.NewRow();
                    strFN = "GroupCode"; drDB[strFN] = strGroupCode;
                    strFN = "GroupName"; drDB[strFN] = strGroupName;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode;
                    dtDB_Sys_Group.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        //, alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_Group_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , object objGroupName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                , "objGroupName", objGroupName
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                ////
                DataTable dtDB_Sys_Group = null;
                {
                    ////
                    if (strGroupCode == null || strGroupCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupCode", strGroupCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Create_InvalidGroupCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagPublicListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    if (strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Create_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Group.NewRow();
                    strFN = "GroupCode"; drDB[strFN] = strGroupCode;
                    strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                    strFN = "GroupName"; drDB[strFN] = strGroupName;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_Sys_Group.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        //, alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Group_Create_New20191102(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , object objGroupName
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                , "objGroupName", objGroupName
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                //Sys_User_CheckAuthorize(
                //    strTid // strTid
                //    , strGwUserCode // strGwUserCode
                //    , strGwPassword // strGwPassword
                //    , strWAUserCode // strWAUserCode
                //                    //, strWAUserPassword // strWAUserPassword
                //    , ref mdsFinal // mdsFinal
                //    , ref alParamsCoupleError // alParamsCoupleError
                //    , dtimeSys // dtimeSys
                //    , strAccessToken // strAccessToken
                //    , strNetworkID // strNetworkID
                //    , strOrgID // strOrgID
                //    , TConst.Flag.Active // strFlagUserCodeToCheck
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                string strMST = null;
                ////
                DataTable dtDB_Sys_Group = null;
                {
                    ////
                    if (strGroupCode == null || strGroupCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupCode", strGroupCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Create_InvalidGroupCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagPublicListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    if (strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Create_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    DataTable dtDB_Sys_User = null;

                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strWAUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );

                    strMST = TUtils.CUtils.StdParam(dtDB_Sys_User.Rows[0]["MST"]);
                    ////

                }
                #endregion

                #region // SaveDB Sys_Group:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Group.NewRow();
                    strFN = "GroupCode"; drDB[strFN] = strGroupCode;
                    strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                    strFN = "MST"; drDB[strFN] = strMST;
                    strFN = "GroupName"; drDB[strFN] = strGroupName;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_Sys_Group.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        //, alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet WAS_Sys_Group_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Group objRQ_Sys_Group
            ////
            , out RT_Sys_Group objRT_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Group.Tid;
            objRT_Sys_Group = new RT_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Group_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Group_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_Sys_Group.Sys_Group)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Group> lst_Sys_Group = new List<Sys_Group>();
                //List<Sys_GroupInGroup> lst_Sys_GroupInGroup = new List<Sys_GroupInGroup>();
                #endregion

                #region // Sys_Group_Create:
                mdsResult = Sys_Group_Create_New20191102(
                    objRQ_Sys_Group.Tid // strTid
                    , objRQ_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_Sys_Group.GwPassword // strGwPassword
                    , objRQ_Sys_Group.WAUserCode // strUserCode
                    , objRQ_Sys_Group.WAUserPassword // strUserPassword
                    , objRQ_Sys_Group.AccessToken
                    , objRQ_Sys_Group.NetworkID
                    , objRQ_Sys_Group.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Sys_Group.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Group.Sys_Group.GroupCode // objGroupCode
                    , objRQ_Sys_Group.Sys_Group.GroupName // objGroupName
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        public DataSet Sys_Group_Update(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object objGroupName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Group_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Update;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    , "objGroupName", objGroupName
                    , "objFlagActive", objFlagActive
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                DataTable dtDB_Sys_Group = null;
                bool bUpd_GroupName = strFt_Cols_Upd.Contains("Sys_Group.GroupName".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_Group.FlagActive".ToUpper());
                ////
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    if (bUpd_GroupName && strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Update_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB Sys_Group:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Group.Rows[0];
                    if (bUpd_GroupName) { strFN = "GroupName"; drDB[strFN] = strGroupName; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Group_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , object objGroupName
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Group_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                , "objGroupName", objGroupName
                , "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                //Sys_User_CheckAuthorize(
                //    strTid // strTid
                //    , strGwUserCode // strGwUserCode
                //    , strGwPassword // strGwPassword
                //    , strWAUserCode // strWAUserCode
                //                    //, strWAUserPassword // strWAUserPassword
                //    , ref mdsFinal // mdsFinal
                //    , ref alParamsCoupleError // alParamsCoupleError
                //    , dtimeSys // dtimeSys
                //    , strAccessToken // strAccessToken
                //    , strNetworkID // strNetworkID
                //    , strOrgID // strOrgID
                //    , TConst.Flag.Active // strFlagUserCodeToCheck
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                string strGroupName = string.Format("{0}", objGroupName).Trim();
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                DataTable dtDB_Sys_Group = null;
                bool bUpd_GroupName = strFt_Cols_Upd.Contains("Sys_Group.GroupName".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_Group.FlagActive".ToUpper());
                ////
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    if (bUpd_GroupName && strGroupName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strGroupName", strGroupName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Group_Update_InvalidGroupName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB Sys_Group:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Group.Rows[0];
                    if (bUpd_GroupName) { strFN = "GroupName"; drDB[strFN] = strGroupName; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Group_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Group objRQ_Sys_Group
            ////
            , out RT_Sys_Group objRT_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Group.Tid;
            objRT_Sys_Group = new RT_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Group_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Group_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_Sys_Group.Sys_Group)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Group> lst_Sys_Group = new List<Sys_Group>();
                //List<Sys_GroupInGroup> lst_Sys_GroupInGroup = new List<Sys_GroupInGroup>();
                #endregion

                #region // Sys_Group_Update:
                mdsResult = Sys_Group_Update(
                    objRQ_Sys_Group.Tid // strTid
                    , objRQ_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_Sys_Group.GwPassword // strGwPassword
                    , objRQ_Sys_Group.WAUserCode // strUserCode
                    , objRQ_Sys_Group.WAUserPassword // strUserPassword
                    , objRQ_Sys_Group.AccessToken
                    , objRQ_Sys_Group.NetworkID
                    , objRQ_Sys_Group.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Sys_Group.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Group.Sys_Group.GroupCode // objGroupCode
                    , objRQ_Sys_Group.Sys_Group.GroupName // objGroupName
                    , objRQ_Sys_Group.Sys_Group.FlagActive // objFlagActive
                                                           ////
                    , objRQ_Sys_Group.Ft_Cols_Upd // objFt_Cols_Upd
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_Group_Delete(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Group_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Delete;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    //// Delete Sys_GroupInGroup:
                    Sys_UserInGroup_Delete_ByGroup(
                        strGroupCode // strGroupCode
                        );
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    dtDB_Sys_Group.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Group_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Group_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Group_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    //// Delete Sys_GroupInGroup:
                    Sys_UserInGroup_Delete_ByGroup(
                        strGroupCode // strGroupCode
                        );
                    ////
                }
                #endregion

                #region // SaveDB GroupCode:
                {
                    // Init:
                    dtDB_Sys_Group.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Group"
                        , dtDB_Sys_Group
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Group_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Group objRQ_Sys_Group
            ////
            , out RT_Sys_Group objRT_Sys_Group
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Group.Tid;
            objRT_Sys_Group = new RT_Sys_Group();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Group.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Group_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Group_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_Sys_Group.Sys_Group)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Group> lst_Sys_Group = new List<Sys_Group>();
                //List<Sys_GroupInGroup> lst_Sys_GroupInGroup = new List<Sys_GroupInGroup>();
                #endregion

                #region // Sys_Group_Delete:
                mdsResult = Sys_Group_Delete(
                    objRQ_Sys_Group.Tid // strTid
                    , objRQ_Sys_Group.GwUserCode // strGwUserCode
                    , objRQ_Sys_Group.GwPassword // strGwPassword
                    , objRQ_Sys_Group.WAUserCode // strUserCode
                    , objRQ_Sys_Group.WAUserPassword // strUserPassword
                    , objRQ_Sys_Group.AccessToken
                    , objRQ_Sys_Group.NetworkID
                    , objRQ_Sys_Group.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Sys_Group.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Group.Sys_Group.GroupCode // objGroupCode
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        #endregion

        #region // Sys_User:
        private void Sys_User_CheckDB(
            ref ArrayList alParamsCoupleError
            , object strUserCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dt_Sys_User
            )
        {
            // GetInfo:
            dt_Sys_User = TDALUtils.DBUtils.GetTableContents(
                _cf.db // db
                , "Sys_User" // strTableName
                , "top 1 *" // strColumnList
                , "" // strClauseOrderBy
                , "UserCode", "=", strUserCode // arrobjParamsTriple item
                );

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dt_Sys_User.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCodeNotFound", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_CheckDB_UserCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dt_Sys_User.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCodeExist", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_CheckDB_UserCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dt_Sys_User.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.UserCodeError", strUserCode
                    , "Check.FlagActiveListToCheck", strFlagActiveListToCheck
                    , "Check.FlagActiveCurrent", dt_Sys_User.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_User_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

        }
        public DataSet Sys_User_Login(
            string strTid
            , string strRootSvCode
            , string strRootUserCode
            , string strServiceCode
            , string strUserCode
            , string strLanguageCode
            , string strUserPassword
            , string strOtherInfo
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = false;
            //string strErrorCode = null;
            string strFunctionName = "Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Login;
            strUserCode = TUtils.CUtils.StdParam(strUserCode);
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
                , "strTid", strTid
                , "strRootSvCode", strRootSvCode
                , "strRootUserCode", strRootUserCode
                , "strServiceCode", strServiceCode
                , "strUserCode", strUserCode
                , "strLanguageCode", strLanguageCode
                , "strOtherInfo", strOtherInfo
                });

            // Manual SessionInfo:
            DataRow drSessionInfo = TSession.Core.CSession.s_myGetSchema_Lic_Session().NewRow();
            drSessionInfo["RootSvCode"] = strRootSvCode;
            drSessionInfo["RootUserCode"] = strRootUserCode;
            drSessionInfo["ServiceCode"] = strServiceCode;
            drSessionInfo["UserCode"] = strUserCode;
            drSessionInfo["LanguageCode"] = strLanguageCode;
            drSessionInfo["InfoExternal"] = strOtherInfo;
            _cf.sinf = new CSessionInfo(drSessionInfo);
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                string strChainCode = TUtils.CUtils.StdParam(strOtherInfo);
                bool bTest = Convert.ToBoolean(_cf.nvcParams["Biz_TestLocal"]);
                #endregion

                #region // Process:
                // Sys_User_CheckDB:
                DataTable dt_Sys_User = null;
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strUserCode // strUserCode
                    , TConst.Flag.Active // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dt_Sys_User // dt_Sys_User
                    );

                if (!bTest)
                {
                    //////
                    //WSLDAP.ldap wsLDAP = new WSLDAP.ldap();
                    //wsLDAP.Url = _cf.nvcParams["WSLDAP_Url"];

                    //string strUserNick = Convert.ToString(dt_Sys_User.Rows[0]["UserNick"]);

                    //////
                    //string strResult = wsLDAP.ldapLogin(
                    //	strUserNick // username
                    //	, strUserPassword // password
                    //	);

                    //if (!string.IsNullOrEmpty(strResult))
                    //{
                    //	alParamsCoupleError.AddRange(new object[]{
                    //		"Check.strUserNick", strUserNick
                    //		, "Check.LDAP.Url", wsLDAP.Url
                    //		, "Check.LDAP.UserName", strUserNick
                    //		, "Check.LDAP.ErrorMsg", strResult
                    //		});
                    //	throw CmUtils.CMyException.Raise(
                    //		TError.ErridnInventory.Sys_User_Login_InvalidLDAP
                    //		, null
                    //		, alParamsCoupleError.ToArray()
                    //		);
                    //}
                }
                else
                {
                    // CheckPassword:
                    string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPassword);
                    if (!CmUtils.StringUtils.StringEqual(strUserPwCheck, dt_Sys_User.Rows[0]["UserPassword"]))
                    {
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidPassword // strErrorCode
                            , null // excInner
                            , alParamsCoupleError.ToArray() // arrobjParamsCouple
                            );
                    }
                }

                ////

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dt_Sys_User.Rows[0]["UserCode"]);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Login(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , string strUserCode
            , string strUserPassword
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = false;
            //string strErrorCode = null;
            string strFunctionName = "Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Login;
            strUserCode = TUtils.CUtils.StdParam(strUserCode);
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strTid", strTid
				////
				, "strUserCode", strUserCode
                });

            // Manual SessionInfo:
            //DataRow drSessionInfo = TSession.Core.CSession.s_myGetSchema_Lic_Session().NewRow();
            //drSessionInfo["RootSvCode"] = strRootSvCode;
            //drSessionInfo["RootUserCode"] = strRootUserCode;
            //drSessionInfo["ServiceCode"] = strServiceCode;
            //drSessionInfo["UserCode"] = strUserCode;
            //drSessionInfo["LanguageCode"] = strLanguageCode;
            //drSessionInfo["InfoExternal"] = strOtherInfo;
            //_cf.sinf = new CSessionInfo(drSessionInfo);
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                //string strChainCode = TUtils.CUtils.StdParam(strOtherInfo);
                bool bTest = true;
                #endregion

                #region // Process:
                // Sys_User_CheckDB:
                DataTable dt_Sys_User = null;
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strUserCode // strUserCode
                    , TConst.Flag.Active // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dt_Sys_User // dt_Sys_User
                    );

                if (!bTest)
                {
                    //////
                    //WSLDAP.ldap wsLDAP = new WSLDAP.ldap();
                    //wsLDAP.Url = _cf.nvcParams["WSLDAP_Url"];

                    //string strUserNick = Convert.ToString(dt_Sys_User.Rows[0]["UserNick"]);

                    //////
                    //string strResult = wsLDAP.ldapLogin(
                    //	strUserNick // username
                    //	, strUserPassword // password
                    //	);

                    //if (!string.IsNullOrEmpty(strResult))
                    //{
                    //	alParamsCoupleError.AddRange(new object[]{
                    //		"Check.strUserNick", strUserNick
                    //		, "Check.LDAP.Url", wsLDAP.Url
                    //		, "Check.LDAP.UserName", strUserNick
                    //		, "Check.LDAP.ErrorMsg", strResult
                    //		});
                    //	throw CmUtils.CMyException.Raise(
                    //		TError.ErridnInventory.Sys_User_Login_InvalidLDAP
                    //		, null
                    //		, alParamsCoupleError.ToArray()
                    //		);
                    //}
                }
                else
                {
                    // CheckPassword:
                    //string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPassword);
                    if (!CmUtils.StringUtils.StringEqual(strWAUserPassword, dt_Sys_User.Rows[0]["UserPassword"]))
                    {
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidPassword // strErrorCode
                            , null // excInner
                            , alParamsCoupleError.ToArray() // arrobjParamsCouple
                            );
                    }
                }

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dt_Sys_User.Rows[0]["UserCode"]);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Login(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , string strUserCode
            , string strUserPassword
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            //string strErrorCode = null;
            string strFunctionName = "Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Login;
            strUserCode = TUtils.CUtils.StdParam(strUserCode);
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strTid", strTid
				////
				, "strAccessToken", strAccessToken
                , "strUserCode", strUserCode
                });

            // Manual SessionInfo:
            //DataRow drSessionInfo = TSession.Core.CSession.s_myGetSchema_Lic_Session().NewRow();
            //drSessionInfo["RootSvCode"] = strRootSvCode;
            //drSessionInfo["RootUserCode"] = strRootUserCode;
            //drSessionInfo["ServiceCode"] = strServiceCode;
            //drSessionInfo["UserCode"] = strUserCode;
            //drSessionInfo["LanguageCode"] = strLanguageCode;
            //drSessionInfo["InfoExternal"] = strOtherInfo;
            //_cf.sinf = new CSessionInfo(drSessionInfo);
            #endregion

            try
            {
                #region // SW:				
                stopWatchFunc.Start();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                //string strChainCode = TUtils.CUtils.StdParam(strOtherInfo);
                //bool bTest = true;
                #endregion

                #region // Refine and Check Input:
                ////
                string strOrgID = null;

                ////
                DataTable dt_Sys_User = null;
                {
                    // Sys_User_CheckDB:
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strUserCode // strUserCode
                        , TConst.Flag.Active // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dt_Sys_User // dt_Sys_User
                        );

                    string strFlagBG = TUtils.CUtils.StdParam(dt_Sys_User.Rows[0]["FlagBG"]);

                    if (CmUtils.StringUtils.StringEqual(strFlagBG, TConst.Flag.Active))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserCode", dt_Sys_User.Rows[0]["UserCode"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidFlagBG
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    string strCheckPass = TUtils.CUtils.GetEncodedHash(strWAUserPassword);
                    if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash(strWAUserPassword), dt_Sys_User.Rows[0]["UserPassword"]))
                    {
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidPassword // strErrorCode
                            , null // excInner
                            , alParamsCoupleError.ToArray() // arrobjParamsCouple
                            );
                    }

                    ////
                    string strSqlGetDB_Sys_UserInOrgID = CmUtils.StringUtils.Replace(@"
							---- Sys_User
							select
								t.UserCode
								, f.OrgID 
							from Sys_User t --//[mylock]
								left join Mst_NNT f --//[mylock]
									on t.MST = f.MST
							where (1=1)
								and t.UserCode = '@strUserCode'
							;
						"
                        , "@strUserCode", strUserCode
                        );

                    DataTable dtDB_Sys_UserInOrgID = _cf.db.ExecQuery(strSqlGetDB_Sys_UserInOrgID).Tables[0];
                    strOrgID = TUtils.CUtils.StdParam(dtDB_Sys_UserInOrgID.Rows[0]["OrgID"]);

                    ////
                    DataTable dtDB_Mst_Org = null;

                    Mst_Org_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strOrgID // objOrgID
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_Org // dtDB_Mst_Org
                        );
                }
                #endregion

                #region // Inos_Sys_User:
                {
                    ////
                    DataTable dtInos_Sys_User = null;

                    DataSet dsGetData = null;

                    Inos_AccountService_GetCurrentUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////

                        , out dsGetData // dsData
                        );

                    dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
                    dtInos_Sys_User.TableName = "Sys_User";

                    ////
                    if (!CmUtils.StringUtils.StringEqualIgnoreCase(strWAUserCode, dtInos_Sys_User.Rows[0]["UserCode"]))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserCode", strUserCode
                            , "Check.Inos.UserCode", dtInos_Sys_User.Rows[0]["UserCode"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidInosUser
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // SaveDB:
                {
                    ////
                    string strSqlUpd_Sys_User = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							update t
							set
								t.InosAccessToken = @strInosAccessToken
								, t.LogLUDTimeUTC = @strLogLUDTimeUTC
								, t.LogLUBy = @strLogLUBy
							from Sys_User t --//[mylock]
							where (1=1)
								and t.UserCode = @strUserCode
							; 
						"
                        //, "@strInosAccessToken", strAccessToken
                        //, "@strUserCode", strUserCode
                        //, "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        //, "@strLogLUBy", strWAUserCode
                        );

                    _cf.db.ExecQuery(
                        strSqlUpd_Sys_User
                        , "@strInosAccessToken", strAccessToken
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        , "@strUserCode", strUserCode
                        );

                    ////
                    Sys_UserLicense_SaveX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError 
                        , _cf.db // dbAction
                        );

                    ////
                    Sys_OrgLicenseModules_InsertOrUpdOrDelX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , _cf.db // dbAction
                                 ////
                        , strOrgID // objOrgID
                        );

                    //// Invoice_license:
                    string strSqlUpd_Invoice_license = CmUtils.StringUtils.Replace(@"
							---- #tbl_Invoice_license_Modules_Filter:
							select
								mnnt.MST
								, t.OrgID
								, t.NetworkID
								, t.ModuleCode
								, t.Qty
								, f.QtyInvoice
								, (t.Qty * f.QtyInvoice) TotalQty 
							into #tbl_Invoice_license_Modules_Filter
							from Sys_OrgLicenseModules t --//[mylock]
								left join Sys_Modules f --//[mylock]
									on t.ModuleCode = f.ModuleCode
								left join Mst_NNT mnnt --//[mylock]
									on t.OrgID = mnnt.OrgID
							;

							select null tbl_Invoice_license_Modules_Filter, * from #tbl_Invoice_license_Modules_Filter t --//[mylock];
							--drop table #tbl_Invoice_license_Modules_Filter;

							---- #tbl_Invoice_license_Filter:
							select
								t.MST
								, t.NetworkID
								, Sum(t.TotalQty) TotalQty
							into #tbl_Invoice_license_Filter
							from #tbl_Invoice_license_Modules_Filter t --//[mylock]
							where (1=1)
							group by
								t.MST
								, t.NetworkID
							;

							select null tbl_Invoice_license_Filter, * from #tbl_Invoice_license_Filter t --//[mylock];
							--drop table #tbl_Invoice_license_Filter;

							---- Invoice_license:
							insert into Invoice_license
							(
								MST
								, NetworkID
								, TotalQty
								, TotalQtyIssued
								, TotalQtyUsed
								, FlagActive
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.MST
								, t.NetworkID
								, 0.0 TotalQty
								, 0.0 TotalQtyIssued
								, 0.0 TotalQtyUsed
								, '1' FlagActive
								, '@strLogLUDTimeUTC' LogLUDTimeUTC
								, '@strLogLUBy' LogLUBy
							from #tbl_Invoice_license_Filter t --//[mylock]
								left join Invoice_license f --//[mylock]
									on t.MST = f.MST
							where (1=1)
								and f.MST is null
							;


							---- Invoice_license:
							update t
							set
								t.TotalQty = IsNull(f.TotalQty, 0.0)
								, t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
								, t.LogLUBy = '@strLogLUBy'
							from Invoice_license t --//[mylock]
								inner join #tbl_Invoice_license_Filter f --//[mylock]
									on t.MST = f.MST
							where (1=1)
							;
						"
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );

                    DataSet dsExec = _cf.db.ExecQuery(
                        strSqlUpd_Invoice_license
                        );
                }
                #endregion

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, dt_Sys_User.Rows[0]["UserCode"]);

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                stopWatchFunc.Stop();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc.ElapsedMilliseconds
                    });

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleSW // alParamsCoupleSW
                    );
                #endregion
            }
        }

        public DataSet Sys_User_RefreshToken(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , string strUserCode
            , string strUserPassword
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            //string strErrorCode = null;
            string strFunctionName = "Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Login;
            strUserCode = TUtils.CUtils.StdParam(strUserCode);
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strTid", strTid
				////
				, "strUserCode", strUserCode
                });

            // Manual SessionInfo:
            //DataRow drSessionInfo = TSession.Core.CSession.s_myGetSchema_Lic_Session().NewRow();
            //drSessionInfo["RootSvCode"] = strRootSvCode;
            //drSessionInfo["RootUserCode"] = strRootUserCode;
            //drSessionInfo["ServiceCode"] = strServiceCode;
            //drSessionInfo["UserCode"] = strUserCode;
            //drSessionInfo["LanguageCode"] = strLanguageCode;
            //drSessionInfo["InfoExternal"] = strOtherInfo;
            //_cf.sinf = new CSessionInfo(drSessionInfo);
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                //string strChainCode = TUtils.CUtils.StdParam(strOtherInfo);
                //bool bTest = true;
                #endregion

                #region // Refine and Check Input:
                ////
                DataTable dt_Sys_User = null;
                {
                    // Sys_User_CheckDB:
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strUserCode // strUserCode
                        , TConst.Flag.Active // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dt_Sys_User // dt_Sys_User
                        );

                    string strFlagBG = TUtils.CUtils.StdParam(dt_Sys_User.Rows[0]["FlagBG"]);

                    if (CmUtils.StringUtils.StringEqual(strFlagBG, TConst.Flag.Active))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserCode", dt_Sys_User.Rows[0]["UserCode"]
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidFlagBG
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash(strWAUserPassword), dt_Sys_User.Rows[0]["UserPassword"]))
                    {
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidPassword // strErrorCode
                            , null // excInner
                            , alParamsCoupleError.ToArray() // arrobjParamsCouple
                            );
                    }
                    ////
                    if (!CmUtils.StringUtils.StringEqual(strAccessToken, dt_Sys_User.Rows[0]["InosAccessToken"]))
                    {
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Login_InvalidAccessToken // strErrorCode
                            , null // excInner
                            , alParamsCoupleError.ToArray() // arrobjParamsCouple
                            );
                    }

                }
                #endregion

                #region // Inos_AccountService_GetAccessTokenX:
                {
                    ////
                    object objAccessToken = null;

                    Inos_AccountService_GetAccessTokenX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strUserCode // objEmail
                        , strUserPassword // objPassword
                                          ////
                        , out objAccessToken // objAccessToken
                        );

                    strAccessToken = Convert.ToString(objAccessToken);
                }
                #endregion

                #region // SaveDB:
                {
                    ////
                    string strSqlUpd_Sys_User = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							update t
							set
								t.InosAccessToken = '@strInosAccessToken'
								, t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
								, t.LogLUBy = '@strLogLUBy'
							from Sys_User t --//[mylock]
							where (1=1)
								and t.UserCode = '@strUserCode'
							; 
						"
                        , "@strInosAccessToken", strAccessToken
                        , "@strUserCode", strUserCode
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );

                    _cf.db.ExecQuery(
                        strSqlUpd_Sys_User
                        );
                }
                #endregion

                // Assign:
                CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strAccessToken);

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Activate(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , object objUUID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Activate;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUUID", objUUID
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Sys_User_CreateX:
                {
                    Sys_User_ActivateX_New20191118(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objUUID // UUID
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Sys_User_ActivateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objUUID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_ActivateX";
            //string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Activate;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objUUID", objUUID
                    });
            #endregion

            #region // Refine and Check Input:
            string strUUID = TUtils.CUtils.StdParam(objUUID);

            DataTable dtDB_Sys_User = null;
            {
                ////
                string strSqlGetDB_Sys_User = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
						select top 1
							t.*
						from Sys_User t --//[mylock]
						where (1=1)
							and t.UUID = '@strUUID'
						;
					"
                    , "@strUUID", strUUID
                    );

                dtDB_Sys_User = _cf.db.ExecQuery(strSqlGetDB_Sys_User).Tables[0];
                ////
            }
            #endregion

            #region // MasterServer:
            {
                // //
                DataSet dsData = new DataSet();

                Inos_AccountService_ActivateX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                                        //, strAccessToken // strAccessToken
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               // //
                    , dtDB_Sys_User.Rows[0]["InosUUID"] // objUUID
                                                        // //
                    , out dsData // dsData
                    );
            }
            #endregion

            // Return Good:
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            mdsFinal.AcceptChanges();
            //return mdsFinal;
        }

        private void Sys_User_ActivateX_New20191118(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            //, string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            ////
            , object objUUID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            //DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_ActivateX";
            //string strErrorCodeDefault = TError.ErridNTVAN.Sys_User_Activate;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			        ////
					, "objUUID", objUUID
                    });
            #endregion

            #region // Refine and Check Input:
            string strUUID = TUtils.CUtils.StdParam(objUUID);

            DataTable dtDB_Sys_User = null;
            DataTable dtDB_Mst_NNT = null;
            {
                ////
                string strSqlGetDB_Sys_User = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
						select top 1
							t.*
						from Sys_User t --//[mylock]
						where (1=1)
							and t.UUID = '@strUUID'
						;
					"
                    , "@strUUID", strUUID
                    );

                dtDB_Sys_User = _cf.db.ExecQuery(strSqlGetDB_Sys_User).Tables[0];
                ////
                Mst_NNT_CheckDB(
                    ref alParamsCoupleError
                    , dtDB_Sys_User.Rows[0]["MST"]
                    , TConst.Flag.Yes
                    , TConst.Flag.Active
                    , ""
                    , out dtDB_Mst_NNT
                    );
            }
            #endregion

            #region // MasterServer:
            {
                // //
                DataSet dsData = new DataSet();

                Inos_AccountService_ActivateX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                                        //, strAccessToken // strAccessToken
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               // //
                    , dtDB_Sys_User.Rows[0]["InosUUID"] // objUUID
                                                        // //
                    , out dsData // dsData
                    );
            }
            #endregion

            #region // Save Lic:
            //{
            //    ////
            //    DataTable dtDB_Mst_Param = null;

            //    Mst_Param_CheckDB(
            //        ref alParamsCoupleError
            //        , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
            //        , TConst.Flag.Yes
            //        , out dtDB_Mst_Param //dtDB_Mst_Param
            //        );

            //    string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
            //    ////
            //    Mst_Param_CheckDB(
            //        ref alParamsCoupleError
            //        , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
            //        , TConst.Flag.Yes
            //        , out dtDB_Mst_Param //dtDB_Mst_Param
            //        );

            //    string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
            //    ////
            //    object objAccessToken = null;
            //    ////
            //    {
            //        Inos_AccountService_GetAccessTokenX(
            //            strTid // strTid
            //            , strGwUserCode // strGwUserCode
            //            , strGwPassword // strGwPassword
            //            , strWAUserCode // strWAUserCode
            //            , strWAUserPassword // strWAUserPassword
            //            , ref mdsFinal // mdsFinal
            //            , ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //                       ////
            //            , UserCode // objEmail
            //            , UserPassword // objPassword
            //                           ////
            //            , out objAccessToken // objAccessToken
            //            );
            //    }

            //    DataTable dtInos_Sys_User = null;
            //    {
            //        ////
            //        DataSet dsGetUser = null;

            //        Inos_AccountService_GetUserX(
            //            strTid // strTid
            //            , strGwUserCode // strGwUserCode
            //            , strGwPassword // strGwPassword
            //            , strWAUserCode // strWAUserCode
            //            , strWAUserPassword // strWAUserPassword
            //            , (string)objAccessToken // strAccessToken
            //            , ref alParamsCoupleError // alParamsCoupleError
            //            , dtimeSys // dtimeSys
            //                       ////
            //            , dtDB_Sys_User.Rows[0]["EMail"] // objEMail
            //                                             ////
            //            , out dsGetUser // dsData
            //            );

            //        dtInos_Sys_User = dsGetUser.Tables["Sys_User"].Copy();
            //        dtInos_Sys_User.TableName = "Sys_User";
            //    }
            //    ////
            //    DataSet dsGetData = null;

            //    List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
            //         strTid // strTid
            //        , strGwUserCode // strGwUserCode
            //        , strGwPassword // strGwPassword
            //        , strWAUserCode // strWAUserCode
            //        , strWAUserPassword // strWAUserPassword
            //        , (string)objAccessToken // strAccessToken
            //        , ref mdsFinal // mdsFinal
            //        , ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //                   ////
            //        , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
            //                                        ////
            //        , out dsGetData // dsData
            //        );
            //    ////

            //    #region // Sys_Solution: Get.
            //    ////
            //    DataTable dtDB_Sys_Solution = null;
            //    {
            //        // GetInfo:
            //        dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
            //            _cf.db // db
            //            , "Sys_Solution" // strTableName
            //            , "top 1 *" // strColumnList
            //            , "" // strClauseOrderBy
            //            , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
            //            );
            //    }
            //    #endregion

            //    ////
            //    List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

            //    foreach (var item in lstOrgLicense)
            //    {
            //        OrgSolutionUser obj = new OrgSolutionUser();
            //        obj.LicId = item.Id;
            //        obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
            //        lstOrgSolutionUser.Add(obj);
            //    }

            //    DataSet dsData = null;
            //    Int32 result = Inos_LicService_AddOrgSolutionUsersX(
            //         strTid // strTid
            //        , strGwUserCode // strGwUserCode
            //        , strGwPassword // strGwPassword
            //        , strWAUserCode // strWAUserCode
            //        , strWAUserPassword // strWAUserPassword
            //        , (string)objAccessToken // strAccessToken
            //        , ref mdsFinal // mdsFinal
            //        , ref alParamsCoupleError // alParamsCoupleError
            //        , dtimeSys // dtimeSys
            //                   ////
            //        , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
            //        , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
            //        , lstOrgSolutionUser // lstOrgSolutionUser
            //                             ////
            //        , out dsData
            //        );
            //}
            #endregion

            #region // Save Lic:
            {
                ////
                DataTable dtDB_Mst_Param = null;

                Mst_Param_CheckDB(
                    ref alParamsCoupleError
                    , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
                    , TConst.Flag.Yes
                    , out dtDB_Mst_Param //dtDB_Mst_Param
                    );

                string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
                ////
                Mst_Param_CheckDB(
                    ref alParamsCoupleError
                    , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
                    , TConst.Flag.Yes
                    , out dtDB_Mst_Param //dtDB_Mst_Param
                    );

                string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
                ////
                object objAccessToken = null;
                ////
                {
                    Inos_AccountService_GetAccessTokenX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , UserCode // objEmail
                        , UserPassword // objPassword
                                       ////
                        , out objAccessToken // objAccessToken
                        );
                }

                DataTable dtInos_Sys_User = null;
                {
                    ////
                    DataSet dsGetUser = null;

                    Inos_AccountService_GetUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , (string)objAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Sys_User.Rows[0]["EMail"] // objEMail
                                                         ////
                        , out dsGetUser // dsData
                        );

                    dtInos_Sys_User = dsGetUser.Tables["Sys_User"].Copy();
                    dtInos_Sys_User.TableName = "Sys_User";
                }
                ////

                ////
                DataSet dsGetData = null;

                List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
                     strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , (string)objAccessToken // strAccessToken
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
                                                    ////
                    , out dsGetData // dsData
                    );
                ////

                #region // Sys_Solution: Get.
                ////
                DataTable dtDB_Sys_Solution = null;
                {
                    // GetInfo:
                    dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                        _cf.db // db
                        , "Sys_Solution" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                        );
                }
                #endregion

                ////
                DataSet dsData_Package = null;
                Inos_LicService_GetAllPackagesX_New20191113(
                    strTid
                    , strGwUserCode
                    , strGwPassword
                    , strWAUserCode
                    , strWAUserPassword
                    , (string)objAccessToken
                    , ref mdsFinal
                    , ref alParamsCoupleError
                    , dtimeSys
                    , dtDB_Sys_Solution.Rows[0]["SolutionCode"]
                    , out dsData_Package
                    );
                ////
                List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

                //foreach (var item in lstOrgLicense)
                //{
                //    OrgSolutionUser obj = new OrgSolutionUser();
                //    obj.LicId = item.Id;
                //    obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
                //    lstOrgSolutionUser.Add(obj);
                //}

                for (int nScan = 0; nScan < dsData_Package.Tables[0].Rows.Count; nScan++)
                {
                    DataRow drScan = dsData_Package.Tables[0].Rows[nScan];
                    long PackageID = Convert.ToInt64(drScan["id"]);
                    foreach (var item in lstOrgLicense)
                    {
                        if (item.PackageId == PackageID)
                        {
                            OrgSolutionUser obj = new OrgSolutionUser();
                            obj.LicId = item.Id;
                            obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
                            lstOrgSolutionUser.Add(obj);
                        }
                    }
                }

                DataSet dsData = null;
                Int32 result = Inos_LicService_AddOrgSolutionUsersX(
                     strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , (string)objAccessToken // strAccessToken
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                    , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
                    , lstOrgSolutionUser // lstOrgSolutionUser
                                         ////
                    , out dsData
                    );
            }
            #endregion

            // Return Good:
            //TDALUtils.DBUtils.CommitSafety(_cf.db);
            mdsFinal.AcceptChanges();
            //return mdsFinal;
        }

        public DataSet WAS_Sys_User_Activate(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_Activate";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_Activate;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Sys_User_Login:
                mdsResult = Sys_User_Activate(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UUID // objUUID
                    );
                #endregion

                #region // GetData:
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_Sys_User_Login(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_Login";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_Login;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Sys_User_Login:
                mdsResult = Sys_User_Login(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    );
                #endregion

                #region // GetData:
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_Sys_User_RefreshToken(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_RefreshToken";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_RefreshToken;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Sys_User_Login:
                mdsResult = Sys_User_RefreshToken(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    );
                #endregion

                #region // GetData:
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }


        public DataSet Sys_User_Logout(
            string strTid
            , DataRow drSession
            ////
            , object strSessionId
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = false;
            string strFunctionName = "Sys_User_Logout";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Logout;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
                , "strTid", strTid
				////
                , "strSessionId", strSessionId
                });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                #endregion

                #region // Logout:
                _cf.sess.Remove(false, strSessionId);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_User_GetForCurrentUser(
            string strTid
            , DataRow drSession
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_GetForCurrentUser;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@strUserCode", _cf.sinf.strUserCode
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
						select
							su.UserCode
							--, su.DBCode
                            --, su.AreaCode
							, su.UserName
							, su.MST
							, su.FlagDLAdmin
							, su.FlagSysAdmin
							--, su.FlagDBAdmin
							, su.FlagActive
							--, md.DBCode md_DBCode 
							--, md.DBCodeParent md_DBCodeParent 
							--, md.DBName md_DBName 
							--, md.DBLevel md_DBLevel 
							--, md.DBStatus md_DBStatus 
                            --, mam.AreaCode mam_AreaCode
	                        --, mam.AreaCodeParent mam_AreaCodeParent
	                        --, mam.AreaDesc mam_AreaDesc
	                        --, mam.AreaLevel mam_AreaLevel
	                        --, mam.AreaStatus mam_AreaStatus
						into #tbl_Sys_User
						from Sys_User su --//[mylock]
							--left join Mst_Distributor md --//[mylock]
								--on su.DBCode = md.DBCode
                            --left join Mst_AreaMarket mam --//[mylock] 
		                        --on md.AreaCode = mam.AreaCode
						where
							su.UserCode = @strUserCode
						;
						select * from #tbl_Sys_User t --//[mylock]
						;

						---- Sys_Access:
						select distinct
							sa.ObjectCode
						into #tbl_Sys_Access
						from Sys_User su --//[mylock]
							inner join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							inner join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode and sg.FlagActive = '1'
							inner join Sys_Access sa --//[mylock]
								on sg.GroupCode = sa.GroupCode
							inner join Sys_Object so --//[mylock]
								on sa.ObjectCode = so.ObjectCode and so.FlagActive = '1'
						where (1=1)
							and su.UserCode = @strUserCode
							and su.FlagActive = '1'
						union -- distinct
						select distinct
							so.ObjectCode
						from #tbl_Sys_User f --//[mylock]
							inner join Sys_Object so --//[mylock]
								on f.FlagSysAdmin = '1' and f.FlagActive = '1' and so.FlagActive = '1'
						;
						select 
							so.*
						from #tbl_Sys_Access f --//[mylock]
							inner join Sys_Object so --//[mylock]
								on f.ObjectCode = so.ObjectCode
						;
					"
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                dsGetData.Tables[0].TableName = "Sys_User";
                dsGetData.Tables[1].TableName = "Sys_Access";
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_GetForCurrentUser(
            string strTid
            , string strServiceCode
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID_RQ
            , ref ArrayList alParamsCoupleError
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            Stopwatch stopWatchFunc = new Stopwatch();
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_GetForCurrentUser;
            ArrayList alParamsCoupleSW = new ArrayList();
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // SW:				
                stopWatchFunc.Start();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "stopWatchFunc.Start", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );
                Sys_User_CheckAuthorize(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                                    //, strWAUserPassword // strWAUserPassword
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strAccessToken // strAccessToken
                    , strNetworkID // strNetworkID
                    , strOrgID_RQ // strOrgID
                    , TConst.Flag.Active // strFlagUserCodeToCheck
                    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                #endregion

                #region // Build Sql:
                bool bIsHasServicesCodeNull = false;
                if (!string.IsNullOrEmpty(strServiceCode))
                {
                    bIsHasServicesCodeNull = true;
                }
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@strUserCode", strWAUserCode
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
                        select
	                        su.UserCode
	                        , su.NetworkID
	                        , mnnt.OrgID
	                        --, su.DBCode
                            --, su.AreaCode
	                        , su.UserName
	                        , su.MST
	                        , su.CustomerCodeSys
	                        , su.DepartmentCode
	                        , su.FlagDLAdmin
	                        , su.FlagSysAdmin
	                        --, su.FlagDBAdmin
	                        , su.FlagActive
	                        , su.Avatar
	                        ----
	                        , mdept.DepartmentCode mdept_DepartmentCode
	                        , mdept.DepartmentName mdept_DepartmentName
	                        ----
	                        , mnnt.DealerType mnnt_DealerType
	                        ----
	                        , mo.OrgID mo_OrgID
	                        , mo.OrgBUCode mo_OrgBUCode
	                        , mo.OrgBUPattern mo_OrgBUPattern
	                        , mo.OrgLevel mo_OrgLevel
	                        , mo.OrgParent mo_OrgParent
                        into #tbl_Sys_User
                        from Sys_User su --//[mylock]
	                        left join Mst_Department mdept --//[mylock]
		                        on su.DepartmentCode = mdept.DepartmentCode
	                        left join Mst_NNT mnnt --//[mylock]
		                        on su.MST = mnnt.MST
	                        left join Mst_Org mo --//[mylock]
		                        on mnnt.OrgID = mo.OrgID
                        where
	                        su.UserCode = @strUserCode
                        ;
						select * from #tbl_Sys_User t --//[mylock]
						;

						---- Sys_Access:
						select distinct
							sa.ObjectCode
						into #tbl_Sys_Access
						from Sys_User su --//[mylock]
							inner join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							inner join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode and sg.FlagActive = '1'
							inner join Sys_Access sa --//[mylock]
								on sg.GroupCode = sa.GroupCode
							inner join Sys_Object so --//[mylock]
								on sa.ObjectCode = so.ObjectCode and so.FlagActive = '1'
						where (1=1)
							and su.UserCode = @strUserCode
							and su.FlagActive = '1'
						union -- distinct
						select distinct
							so.ObjectCode
						from #tbl_Sys_User f --//[mylock]
							inner join Sys_Object so --//[mylock]
								on f.FlagSysAdmin = '1' and f.FlagActive = '1' and so.FlagActive = '1'
						;
						select 
							f.*
							, so.ObjectCode so_ObjectCode
                            , so.ObjectName so_ObjectName 
                            , so.ServiceCode so_ServiceCode 
                            , so.ObjectType so_ObjectType 
                            , so.FlagExecModal so_FlagExecModal 
                            , so.FlagActive so_FlagActive
						from #tbl_Sys_Access f --//[mylock]
							inner join Sys_Object so --//[mylock]
								on f.ObjectCode = so.ObjectCode
                        where (1=1)
                            @strServicesCode
						;
					"
                    , "@strServicesCode", bIsHasServicesCodeNull ? "and so.ServiceCode = '@strFt_ServiceCode'".Replace("@strFt_ServiceCode", strServiceCode) : "" //  -- MOBILEAPP
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                dsGetData.Tables[0].TableName = "Sys_User";
                dsGetData.Tables[1].TableName = "Sys_Access";
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                stopWatchFunc.Stop();
                alParamsCoupleSW.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "stopWatchFunc.Stop", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "stopWatchFunc.ElapsedMilliseconds", stopWatchFunc.ElapsedMilliseconds
                    });

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleSW // alParamsCoupleSW
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_User_GetForCurrentUser(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_GetForCurrentUser";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_GetForCurrentUser;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Sys_User> lst_Sys_User = new List<Sys_User>();
                List<Sys_Access> lst_Sys_Access = new List<Sys_Access>();
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = Sys_User_GetForCurrentUser(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.ServiceCode
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , objRQ_Sys_User.NetworkID // strNetworkID
                    , objRQ_Sys_User.OrgID // strOrgID
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                    lst_Sys_User = TUtils.DataTableCmUtils.ToListof<Sys_User>(dt_Sys_User);
                    objRT_Sys_User.Lst_Sys_User = lst_Sys_User;

                    ////
                    DataTable dt_Sys_Access = mdsResult.Tables["Sys_Access"].Copy();
                    lst_Sys_Access = TUtils.DataTableCmUtils.ToListof<Sys_Access>(dt_Sys_Access);
                    objRT_Sys_User.Lst_Sys_Access = lst_Sys_Access;
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        public DataSet Sys_User_ChangePassword(
            string strTid
            , DataRow drSession
            , string strUserPasswordOld
            , string strUserPasswordNew
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_ChangePassword";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_ChangePassword;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                // Sys_User_CheckDB:
                DataTable dt_Sys_User = null;
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , _cf.sinf.strUserCode // strUserCode
                    , TConst.Flag.Active // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dt_Sys_User
                    );

                // CheckPassword:
                string strUserPwCheck = TUtils.CUtils.GetSimpleHash(strUserPasswordOld);
                if (!CmUtils.StringUtils.StringEqual(strUserPwCheck, dt_Sys_User.Rows[0]["UserPassword"]))
                {
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_ChangePassword_InvalidPasswordOld // strErrorCode
                        , null // excInner
                        , alParamsCoupleError.ToArray() // arrobjParamsCouple
                        );
                }
                ////
                if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPasswordNew)))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserPasswordNew", strUserPasswordNew
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_ChangePassword_InvalidPasswordNew
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                #endregion

                #region // dt_Sys_User:
                ArrayList alColumnEffective = new ArrayList();
                dt_Sys_User.Rows[0]["UserPassword"] = TUtils.CUtils.GetSimpleHash(strUserPasswordNew); alColumnEffective.Add("UserPassword");
                _cf.db.SaveData("Sys_User", dt_Sys_User, alColumnEffective.ToArray());
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_ChangePassword(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , string strUserPasswordOld
            , string strUserPasswordNew
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_ChangePassword";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_ChangePassword;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                //// Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:

                // Sys_User_CheckDB:
                DataTable dt_Sys_User = null;
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strWAUserCode // strUserCode
                    , TConst.Flag.Active // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dt_Sys_User
                    );

                // CheckPassword:
                //strUserPasswordOld = TUtils.CUtils.GetEncodedHash(strUserPasswordOld);
                if (!CmUtils.StringUtils.StringEqual(TUtils.CUtils.GetEncodedHash(strUserPasswordOld), dt_Sys_User.Rows[0]["UserPassword"]))
                {
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_ChangePassword_InvalidPasswordOld // strErrorCode
                        , null // excInner
                        , alParamsCoupleError.ToArray() // arrobjParamsCouple
                        );
                }
                ////
                //if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPasswordNew)))
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.strUserPasswordNew", strUserPasswordNew
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErridnInventory.Sys_User_ChangePassword_InvalidPasswordNew
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                #endregion

                #region // dt_Sys_User:
                ArrayList alColumnEffective = new ArrayList();
                dt_Sys_User.Rows[0]["UserPassword"] = TUtils.CUtils.GetEncodedHash(strUserPasswordNew); alColumnEffective.Add("UserPassword");
                _cf.db.SaveData("Sys_User", dt_Sys_User, alColumnEffective.ToArray());
                #endregion

                #region // MasterServer:
                {
                    // //
                    DataSet dsData = new DataSet();

                    Inos_AccountService_EditProfileX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dt_Sys_User.Rows[0]["UserName"] // objName
                        , strUserPasswordOld // objOldPassword
                        , strUserPasswordNew // objNewPassword
                        , true // objChangePassword
                        , false // objChangeAvatar
                        , false // objChangeName
                        , null // objAvatarBase64
                        , out dsData // dsData
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_User_ChangePassword(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_ChangePassword";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_ChangePassword;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_User> lst_Sys_User = new List<Sys_User>();
                //List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                #endregion

                #region // Sys_User_Create:
                mdsResult = Sys_User_ChangePassword(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.WAUserPassword // strUserPasswordOld
                    , objRQ_Sys_User.Sys_User.UserPasswordNew // strUserPasswordNew
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_User_Get(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                //myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

                //myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Bank mb --//[mylock]
								on su.BankCode = mb.BankCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode
								, su.UserNick
                                , su.BankCode                             
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                                , su.FlagDLAdmin
								, su.FlagSysAdmin                       
								, su.FlagActive
								, mb.BankCode mb_BankCode
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Bank mb --//[mylock]
									on su.BankCode = mb.BankCode
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.BankCode su_BankCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Bank" // strTableNameDB
                            , "Mst_Bank." // strPrefixStd
                            , "mb." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Sys_User_CheckAuthorize(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            //, string strWAUserPassword
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagUserCodeToCheck
            )
        {
			////
			TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
			string strErrorCodeDefault = TError.ErridnInventory.Sys_User_CheckAuthorize;
			string strWAUserPassword = "idocNet";

			try
			{
				dbLocal.BeginTransaction();

				//  //
				string strSqlCheck = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
						select top 1
							t.UserCode
							, t.FlagBG
							, t.FlagSysAdmin
							, t.AuthorizeDTimeStart
						from Sys_User t --//[mylock]
						where (1=1)
							and t.InosAccessToken = @strAccessToken
						;
					"
					);

				DataTable dt_Sys_User = dbLocal.ExecQuery(
					strSqlCheck
					, "@strAccessToken", strAccessToken
					).Tables[0];
				////
				string strFlagBG = null;
				string strUserCode = null;
				string strAuthorizeDTimeStart = null;
				InosUser InosUser = null;

				// Nếu Tồn tại AccessToken thì check vượt qua ExpireDTime chưa ?
				if (dt_Sys_User.Rows.Count > 0)
				{
					// //
					strFlagBG = TUtils.CUtils.StdFlag(dt_Sys_User.Rows[0]["FlagBG"]);
					strUserCode = TUtils.CUtils.StdParam(dt_Sys_User.Rows[0]["UserCode"]);
					strAuthorizeDTimeStart = TUtils.CUtils.StdDTime(dt_Sys_User.Rows[0]["AuthorizeDTimeStart"]);

					DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
					dtfi.ShortDatePattern = "yyyy-MM-dd HH:mm:ss";
					dtfi.DateSeparator = "-";
					DateTime dtimeEffDateStart = Convert.ToDateTime(strAuthorizeDTimeStart, dtfi);
					DateTime dtimeEffDateEnd = Convert.ToDateTime(dtimeSys, dtfi);
					TimeSpan timeSpan = (dtimeEffDateEnd - dtimeEffDateStart);
					double dblTimeSpan = timeSpan.TotalMinutes;

					// Check UserBG
					if (CmUtils.StringUtils.StringEqual(strFlagBG, TConst.Flag.Active))
                    {
                        //return;
                        goto BG_Return;
                    }
					else
					{
						// // Nếu chưa bị Expire thì sử dụng UserCode.
						if (dblTimeSpan <= TConst.BizMix.Default_AuthMinutes)
						{

						}
						// // Nếu bị Expire thì thực hiện truy vấn Inos để xin thông tin tương ứng với AccessToken.
						else
						{
							// //
							var accService = new inos.common.Service.AccountService(null);
							accService.AccessToken = strAccessToken;
							InosUser = accService.GetCurrentUser();

							if (CmUtils.StringUtils.StringEqual(strFlagUserCodeToCheck, TConst.Flag.Active))
							{
								if (!CmUtils.StringUtils.StringEqualIgnoreCase(strWAUserCode, InosUser.Email))
								{
									alParamsCoupleError.AddRange(new object[]{
										"Check.strWAUserCode", strWAUserCode
										, "Check.strWAUserCode.DB", InosUser.Email
										});
									throw CmUtils.CMyException.Raise(
										TError.ErridnInventory.Sys_User_CheckAuthorize_InvalidUserCode
										, null
										, alParamsCoupleError.ToArray()
										);
								}
							}

							////
							string strSqlUpd_Sys_User = CmUtils.StringUtils.Replace(@"
									---- Sys_User:
									update t
									set
										t.InosAccessToken = @strInosAccessToken
										, t.AuthorizeDTimeStart = @strAuthorizeDTimeStart
										, t.LogLUDTimeUTC = @strLogLUDTimeUTC
										, t.LogLUBy = @strLogLUBy
										----
										, t.ACId = @strACId
										, t.ACAvatar = @strACAvatar
										, t.ACEmail = @strACEmail
										, t.ACLanguage = @strACLanguage
										, t.ACName = @strACName
										, t.ACPhone = @strACPhone
										, t.ACTimeZone = @strACTimeZone
									from Sys_User t --//[mylock]
									where (1=1)
										and t.UserCode = @strUserCode
									; 
								"
								//, "@strInosAccessToken", strAccessToken
								//, "@strUserCode", strUserCode
								//, "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
								//, "@strLogLUBy", strWAUserCode
								);

							dbLocal.ExecQuery(
								strSqlUpd_Sys_User
								, "@strInosAccessToken", strAccessToken
								, "@strAuthorizeDTimeStart", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
								, "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
								, "@strLogLUBy", strWAUserCode
								, "@strUserCode", strUserCode
								// //
								, "@strACId", InosUser.Id
								, "@strACAvatar", InosUser.Avatar
								, "@strACEmail", InosUser.Email
								, "@strACLanguage", InosUser.Language
								, "@strACName", InosUser.Name
								, "@strACPhone", InosUser.Phone
								, "@strACTimeZone", InosUser.TimeZone
								);

							////
							Sys_UserLicense_SaveX(
								strTid // strTid
								, strGwUserCode // strGwUserCode
								, strGwPassword // strGwPassword
								, strWAUserCode // strWAUserCode
								, strWAUserPassword // strWAUserPassword
								, strAccessToken // strAccessToken
								, ref alParamsCoupleError // alParamsCoupleError 
								, dbLocal // dbAction
								);

							////
							Sys_OrgLicenseModules_InsertOrUpdOrDelX(
								strTid // strTid
								, strGwUserCode // strGwUserCode
								, strGwPassword // strGwPassword
								, strWAUserCode // strWAUserCode
								, strWAUserPassword // strWAUserPassword
								, strAccessToken // strAccessToken
								, ref alParamsCoupleError // alParamsCoupleError 
								, dbLocal // dbAction
										  ////
								, strOrgID // objOrgID
								);

						}
					}
				}
				// Nếu Chưa Tồn tại AccessToken thì truy vấn thông tin Inos để lấy thông tin tương ứng với AccessToken
				else
				{
					// Sys_User_CheckDB:
					DataTable dt_Sys_User_CheckWA = null;

					Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, strWAUserCode // strUserCode
						, TConst.Flag.Active // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dt_Sys_User_CheckWA // dt_Sys_User
						);

					strFlagBG = TUtils.CUtils.StdFlag(dt_Sys_User_CheckWA.Rows[0]["FlagBG"]);
					// //
					if (CmUtils.StringUtils.StringEqual(strFlagBG, TConst.Flag.Active))
                    {
                        //return;
                        goto BG_Return;
                    }

					// //
					var accService = new inos.common.Service.AccountService(null);
					accService.AccessToken = strAccessToken;
					InosUser = accService.GetCurrentUser();

					// Sys_User_CheckDB:
					DataTable dt_Sys_User_Check = null;

					Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, InosUser.Email // strUserCode
						, TConst.Flag.Active // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, out dt_Sys_User_Check // dt_Sys_User
						);

					////
					string strSqlUpd_Sys_User = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							update t
							set
								t.InosAccessToken = @strInosAccessToken
								, t.AuthorizeDTimeStart = @strAuthorizeDTimeStart
								, t.LogLUDTimeUTC = @strLogLUDTimeUTC
								, t.LogLUBy = @strLogLUBy
								----
								, t.ACId = @strACId
								, t.ACAvatar = @strACAvatar
								, t.ACEmail = @strACEmail
								, t.ACLanguage = @strACLanguage
								, t.ACName = @strACName
								, t.ACPhone = @strACPhone
								, t.ACTimeZone = @strACTimeZone
							from Sys_User t --//[mylock]
							where (1=1)
								and t.UserCode = @strUserCode
							; 
						"
						//, "@strInosAccessToken", strAccessToken
						//, "@strAuthorizeDTimeStart", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						//, "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						//, "@strLogLUBy", InosUser.Email
						//, "@strUserCode", InosUser.Email
						);

					dbLocal.ExecQuery(
						strSqlUpd_Sys_User
						, "@strInosAccessToken", strAccessToken
						, "@strAuthorizeDTimeStart", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						, "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
						, "@strLogLUBy", InosUser.Email
						, "@strUserCode", InosUser.Email
						// //
						, "@strACId", InosUser.Id
						, "@strACAvatar", InosUser.Avatar
						, "@strACEmail", InosUser.Email
						, "@strACLanguage", InosUser.Language
						, "@strACName", InosUser.Name
						, "@strACPhone", InosUser.Phone
						, "@strACTimeZone", InosUser.TimeZone
						);

					////
					Sys_UserLicense_SaveX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError 
						, dbLocal // dbAction
						);

					////
					Sys_OrgLicenseModules_InsertOrUpdOrDelX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError 
						, dbLocal // dbAction
								  ////
						, strOrgID // objOrgID
						);
				}

                BG_Return:
                dbLocal.Commit();
			}
			catch (Exception exc)
			{
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(dbLocal);

				// Return Bad:
				TUtils.CProcessExc.Process(
					ref mdsFinal
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);

				throw exc;
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(dbLocal);
                //TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                //_cf.ProcessBizReturn_OutSide(
                //    ref mdsFinal // mdsFinal
                //    , strTid // strTid
                //    , strGwUserCode // strGwUserCode
                //    , strGwPassword // strGwPassword
                //    , strWAUserCode // objUserCode
                //    , strFunctionName // strFunctionName
                //    );
                #endregion
            }




        }

        public DataSet Sys_User_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                //myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

                //myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on su.MST = t_MstNNT_View.MST
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Department mdept --//[mylock]
								on su.DepartmentCode = mdept.DepartmentCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode                         
								, su.UserName                      
								, su.MST
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
								, su.PhoneNo
								, su.EMail
								, su.DepartmentCode
								, su.Position
                                , su.FlagDLAdmin 
								, su.FlagSysAdmin 
								, su.FlagNNTAdmin                         
								, su.FlagActive
								, mdept.DepartmentCode mdept_DepartmentCode
								, mdept.DepartmentName mdept_DepartmentName
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Department mdept --//[mylock]
									on su.DepartmentCode = mdept.DepartmentCode
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Department" // strTableNameDB
                            , "Mst_Department." // strPrefixStd
                            , "mdept." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Get_New20191104(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                ////		
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                //myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

                //myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on su.MST = t_MstNNT_View.MST
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Department mdept --//[mylock]
								on su.DepartmentCode = mdept.DepartmentCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode                         
								, su.UserName                      
								, su.MST
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
								, su.PhoneNo
								, su.EMail
								, su.DepartmentCode
								, su.Position
                                , su.FlagDLAdmin 
								, su.FlagSysAdmin 
								, su.FlagNNTAdmin                         
								, su.FlagActive
                                , su.ACId
                                , su.ACAvatar
                                , su.ACEmail
                                , su.ACLanguage
                                , su.ACName
                                , su.ACPhone
                                , su.ACTimeZone
                                , su.CustomerCodeSys
                                , mc.CustomerCode
                                , mc.CustomerName
								, mdept.DepartmentCode mdept_DepartmentCode
								, mdept.DepartmentName mdept_DepartmentName
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Department mdept --//[mylock]
									on su.DepartmentCode = mdept.DepartmentCode
                                left join Mst_Customer mc --//[mylock]
                                    on su.CustomerCodeSys = mc.CustomerCodeSys
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Department" // strTableNameDB
                            , "Mst_Department." // strPrefixStd
                            , "mdept." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_User_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_User> lst_Sys_User = new List<Sys_User>();
                List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                bool bGet_Sys_User = (objRQ_Sys_User.Rt_Cols_Sys_User != null && objRQ_Sys_User.Rt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (objRQ_Sys_User.Rt_Cols_Sys_UserInGroup != null && objRQ_Sys_User.Rt_Cols_Sys_UserInGroup.Length > 0);
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = Sys_User_Get_New20191104(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken
                    , objRQ_Sys_User.NetworkID
                    , objRQ_Sys_User.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_User.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_User.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_User.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_Sys_User.Rt_Cols_Sys_User // strRt_Cols_Sys_User
                    , objRQ_Sys_User.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_User.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_User)
                    {
                        ////
                        DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                        lst_Sys_User = TUtils.DataTableCmUtils.ToListof<Sys_User>(dt_Sys_User);
                        objRT_Sys_User.Lst_Sys_User = lst_Sys_User;
                    }
                    ////
                    if (bGet_Sys_UserInGroup)
                    {
                        DataTable dt_Sys_UserInGroup = mdsResult.Tables["Sys_UserInGroup"].Copy();
                        lst_Sys_UserInGroup = TUtils.DataTableCmUtils.ToListof<Sys_UserInGroup>(dt_Sys_UserInGroup);
                        objRT_Sys_User.Lst_Sys_UserInGroup = lst_Sys_UserInGroup;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_User_Get_01(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get_01";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Get_01;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                //// drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
                            left join Mst_Distributor md --//[mylock]
								on su.DBCode = md.DBCode
                            inner join #tbl_Mst_Distributor_ViewAbility va_md --//[mylock]
								on md.DBCode = va_md.DBCode
							left join Mst_AreaMarket mam --//[mylock]
								on md.AreaCode = mam.AreaCode 
							left join Aud_CampaignOLDtl acoldt --//[mylock] 
								on su.UserCode = acoldt.AuditUserCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select distinct
								t.MyIdxSeq
								, su.UserCode
                                , su.DBCode
                                , su.AreaCode
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                                , su.FlagDLAdmin
								, su.FlagSysAdmin
                                , su.FlagDBAdmin
								, su.FlagActive
								, md.DBCode md_DBCode
								, mam.AreaCode mam_AreaCode
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								inner join Aud_CampaignOLDtl acoldt --//[mylock] 
									on su.UserCode = acoldt.AuditUserCode
								left join Mst_Distributor md --//[mylock]
									on su.DBCode = md.DBCode
								left join Mst_AreaMarket mam --//[mylock]
									on su.AreaCode = mam.AreaCode
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName
                                , su.FlagDLAdmin su_FlagDLAdmin 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Distributor" // strTableNameDB
                            , "Mst_Distributor." // strPrefixStd
                            , "md." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_AreaMarket" // strTableNameDB
                            , "Mst_AreaMarket." // strPrefixStd
                            , "mam." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Aud_CampaignOLDtl" // strTableNameDB
                            , "Aud_CampaignOLDtl." // strPrefixStd
                            , "acoldt." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Create_Old(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strUserName = string.Format("{0}", objUserName).Trim();
                string strUserPassword = string.Format("{0}", objUserPassword);
                string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
                string strEMail = string.Format("{0}", objEMail).Trim();
                string strMST = TUtils.CUtils.StdParam(objMST);
                string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
                string strPosition = string.Format("{0}", objPosition);
                string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
                string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    if (strUserCode == null || strUserCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserCode", strUserCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    ////
                    if (strUserName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserName", strUserName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strUserPassword.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserPassword", strUserPassword
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    //// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                    //if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                    //{
                    //	alParamsCoupleError.AddRange(new object[]{
                    //		"Check.strUserPassword", strUserPassword
                    //		});
                    //	throw CmUtils.CMyException.Raise(
                    //		TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                    //		, null
                    //		, alParamsCoupleError.ToArray()
                    //		);
                    //}
                    ////
                    DataTable dtDB_Mst_NNT = null;

                    Mst_NNT_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strMST // objMST
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , "" // strTCTStatusListToCheck
                        , out dtDB_Mst_NNT // dtDB_Mst_NNT
                        );
                    ////
                    DataTable dtDB_Mst_Department = null;

                    Mst_Department_CheckDB(
                         ref alParamsCoupleError // alParamsCoupleError
                         , strDepartmentCode // strDepartmentCode 
                         , TConst.Flag.Yes // strFlagExistToCheck
                         , TConst.Flag.Active // strFlagActiveListToCheck
                         , out dtDB_Mst_Department // dtDB_Mst_Organ
                        );
                    ////
                    if (strFlagDLAdmin.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strFlagDLAdmin", strFlagDLAdmin
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strFlagSysAdmin.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strFlagSysAdmin", strFlagSysAdmin
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_User.NewRow();
                    strFN = "UserCode"; drDB[strFN] = strUserCode;
                    strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                    strFN = "UserName"; drDB[strFN] = strUserName;
                    strFN = "UserPassword"; drDB[strFN] = strUserPassword;
                    strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
                    strFN = "EMail"; drDB[strFN] = strEMail;
                    strFN = "MST"; drDB[strFN] = strMST;
                    strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
                    strFN = "Position"; drDB[strFN] = strPosition;
                    strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                    strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_Sys_User.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        //, alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagNTTAdmin
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Sys_User_CreateX:
                {
                    Sys_User_CreateForNetworkX_New20191104(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objUserCode // objUserCode
                        , objUserName // objUserName
                        , objUserPassword // objUserPassword
                        , objPhoneNo // objPhoneNo
                        , objEMail // objEMail
                        , objMST // objMST
                        , objDepartmentCode // objDepartmentCode
                        , objPosition // objPosition
                        , objFlagDLAdmin // objFlagDLAdmin
                        , objFlagSysAdmin // objFlagSysAdmin
                        , objFlagNTTAdmin // objFlagNTTAdmin
                                          ////
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
		public DataSet Sys_User_Create_New20200110(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, string strFlagIsEndUser
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			, object objUserName
			, object objUserPassword
			, object objPhoneNo
			, object objEMail
			, object objMST
			, object objDepartmentCode
			, object objPosition
			, object objFlagDLAdmin
			, object objFlagSysAdmin
			, object objFlagNTTAdmin
			, object objCustomerCodeSys
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Sys_User_Create";
			string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Create;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				, "objUserName", objUserName
				, "objPhoneNo", objPhoneNo
				, "objEMail", objEMail
				, "objMST", objMST
				, "objDepartmentCode", objDepartmentCode
				, "objPosition", objPosition
				, "objFlagDLAdmin", objFlagDLAdmin
				, "objFlagSysAdmin", objFlagSysAdmin
				, "objCustomerCodeSys", objCustomerCodeSys
				});
			#endregion

			try
			{
				#region // Convert Input:
				#endregion

				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
				if (!bFlagIsEndUser)
				{
					Sys_User_CheckAuthorize(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
										//, strWAUserPassword // strWAUserPassword
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						, strAccessToken // strAccessToken
						, strNetworkID // strNetworkID
						, strOrgID_RQ // strOrgID
						, TConst.Flag.Active // strFlagUserCodeToCheck
						);

					//Check Access/ Deny:
					Sys_Access_CheckDenyV30(
						ref alParamsCoupleError
						, strWAUserCode
						, strFunctionName
						);
				}
				#endregion

				#region // Sys_User_CreateX:
				{
					// Sys_User_CreateForNetworkX_New20200110
					Sys_User_CreateForNetworkX_New20200110(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objUserCode // objUserCode
						, objUserName // objUserName
						, objUserPassword // objUserPassword
						, objPhoneNo // objPhoneNo
						, objEMail // objEMail
						, objMST // objMST
						, objDepartmentCode // objDepartmentCode
						, objPosition // objPosition
						, objFlagDLAdmin // objFlagDLAdmin
						, objFlagSysAdmin // objFlagSysAdmin
						, objFlagNTTAdmin // objFlagNTTAdmin
						, objCustomerCodeSys
						////
						);
				}
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);
				mdsFinal.AcceptChanges();
				return mdsFinal;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);

				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsFinal
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Rollback and Release resources:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
				TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

				// Write ReturnLog:
				_cf.ProcessBizReturn_OutSide(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}

		private void Sys_User_CreateForNetworkX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagNNTAdmin
            ////
            )
        {
            #region // Temp:
            //mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strUserName = string.Format("{0}", objUserName).Trim();
            string strUserPassword = string.Format("{0}", objUserPassword);
            string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
            string strEMail = string.Format("{0}", objEMail).Trim();
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
            string strPosition = string.Format("{0}", objPosition);
            string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
            string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
            string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);

            string strOrgID = null;
            string strFlagUserExist = TConst.Flag.Inactive;

            ////
            DataTable dtDB_Sys_User = null;
            {
                ////
                if (strUserCode == null || strUserCode.Length <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objUserCode // objUserCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );
                ////
                if (strUserName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserName", strUserName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strUserPassword.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserPassword", strUserPassword
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DataTable dtDB_Mst_NNT = null;

                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // objMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );

                strOrgID = TUtils.CUtils.StdParam(dtDB_Mst_NNT.Rows[0]["OrgID"]);
                ////
                DataTable dtDB_Mst_Department = null;

                Mst_Department_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strDepartmentCode // strDepartmentCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , TConst.Flag.Active // strFlagActiveListToCheck
                     , out dtDB_Mst_Department // dtDB_Mst_Organ
                    );
                ////
                if (strFlagDLAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagDLAdmin", strFlagDLAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strFlagSysAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagSysAdmin", strFlagSysAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.NewRow();
                strFN = "UserCode"; drDB[strFN] = strUserCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "UserName"; drDB[strFN] = strUserName;
                strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetEncodedHash(strUserPassword);
                strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
                strFN = "EMail"; drDB[strFN] = strEMail;
                strFN = "MST"; drDB[strFN] = strMST;
                strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
                strFN = "Position"; drDB[strFN] = strPosition;
                strFN = "UUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
                strFN = "InosUUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
                strFN = "InosAccessToken"; drDB[strFN] = null;
                strFN = "InosRefreshToken"; drDB[strFN] = null;
                strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin;
                strFN = "FlagBG"; drDB[strFN] = TConst.Flag.Inactive;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Sys_User.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion

            #region // Inos_AccountService_GetUserX:
            ////
            DataTable dtInos_Sys_User = null;
            {
                ////
                DataSet dsGetData = null;

                Inos_AccountService_GetUserX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strAccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , strEMail // objEMail
                               ////
                    , out dsGetData // dsData
                    );

                dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
                dtInos_Sys_User.TableName = "Sys_User";
            }
            #endregion

            #region // Process:
            {
                ////
                if (dtInos_Sys_User.Rows.Count > 0)
                {
                    strFlagUserExist = TConst.Flag.Active;

                    ////
                    DataSet dsGetData = null;

                    Inos_OrgService_AddInviteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID // objOrgID
                        , strEMail // objEMail
                                   ////
                        , out dsGetData // dsData
                        );
                }
                ////
                else
                {
                    ////
                    string strInosUUID = null;
                    string strUUID = null;

                    ////
                    DataSet dsData = null;

                    Inos_AccountService_RegisterForNetworkX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                                            //, strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objMST // objMST
                        , strEMail // objEmail
                        , strUserName // objName
                        , strUserPassword // objPassword
                        , TConst.InosMix.Default_Language // objLanguage
                        , TConst.InosMix.Default_TimeZone // objTimeZone
                                                          ////
                        , out dsData // dsData
                        );

                    ////
                    DataSet dsGetData = null;

                    Inos_OrgService_AddInviteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID // objOrgID
                        , strEMail // objEMail
                                   ////
                        , out dsGetData // dsData
                        );

                    // //
                    DataTable dtUpd_Sys_User = dsData.Tables["Sys_User"];

                    if (dtUpd_Sys_User.Rows.Count > 0)
                    {
                        strInosUUID = Convert.ToString(dtUpd_Sys_User.Rows[0]["UUID"]);
                    }

                    ////
                    DataSet dsSeq = null;

                    dsSeq = Seq_UUID_Get(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , Convert.ToString(nNetworkID) // strNetworkID
                        , strOrgID // strOrgID
                        , ref alParamsCoupleError // alParamsCoupleError
                        );

                    strUUID = Convert.ToString(CmUtils.CMyDataSet.GetRemark(dsSeq));

                    ////
                    string strSqlUpd_Sys_User = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							update t
							set
								t.UUID = '@strUUID'
								, t.InosUUID = '@strInosUUID'
								, t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
								, t.LogLUBy = '@strLogLUBy'
							from Sys_User t --//[mylock]
							where (1=1)
								and t.UserCode = '@strUserCode'
							;						
						"
                        , "@strUserCode", strUserCode
                        , "@strUUID", strUUID
                        , "@strInosUUID", strInosUUID
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );

                    _cf.db.ExecQuery(
                        strSqlUpd_Sys_User
                        );

                    Email_BatchSendEmail_Sys_User_SendX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                                            //, strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , strUUID // objUUID
                        , strEMail // objEMail
                        );
                }
            }
            #endregion

            // Assign:
            CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strFlagUserExist);
        }

        private void Sys_User_CreateForNetworkX_New20191104(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagNNTAdmin
            ////
            )
        {
            #region // Temp:
            //mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strUserName = string.Format("{0}", objUserName).Trim();
            string strUserPassword = string.Format("{0}", objUserPassword);
            string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
            string strEMail = string.Format("{0}", objEMail).Trim();
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
            string strPosition = string.Format("{0}", objPosition);
            string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
            string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
            string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);

            string strOrgID = null;
            string strFlagUserExist = TConst.Flag.Inactive;

            ////
            DataTable dtDB_Sys_User = null;
            {
                ////
                if (strUserCode == null || strUserCode.Length <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objUserCode // objUserCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );
                ////
                if (strUserName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserName", strUserName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strUserPassword.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserPassword", strUserPassword
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DataTable dtDB_Mst_NNT = null;

                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // objMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );

                strOrgID = TUtils.CUtils.StdParam(dtDB_Mst_NNT.Rows[0]["OrgID"]);
                ////
                DataTable dtDB_Mst_Department = null;

                Mst_Department_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strDepartmentCode // strDepartmentCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , TConst.Flag.Active // strFlagActiveListToCheck
                     , out dtDB_Mst_Department // dtDB_Mst_Organ
                    );
                ////
                if (strFlagDLAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagDLAdmin", strFlagDLAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strFlagSysAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagSysAdmin", strFlagSysAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DataTable dtDB_Sys_UserRoot = null;

                Sys_User_CheckDB(
                    ref alParamsCoupleError //alParamsCoupleError
                    , strWAUserCode // objUserCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Sys_UserRoot // dtDB_Sys_UserRoot
                    );

                Int32 nFlagSysAdminRoot = Convert.ToInt32(dtDB_Sys_UserRoot.Rows[0]["FlagSysAdmin"]);
                //// Nếu người Đăng nhập không phải là Admin => không cho bật cờ FlagSysAdmin
                if (Convert.ToInt32(strFlagSysAdmin) == 1)
                {
                    if (nFlagSysAdminRoot != 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                        "Check.strWAUserCode", strWAUserCode
                        ,"Check.nFlagSysAdmin", nFlagSysAdminRoot
                        });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_NNTNotAdmin
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }

                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.NewRow();
                strFN = "UserCode"; drDB[strFN] = strUserCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "UserName"; drDB[strFN] = strUserName;
                strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetEncodedHash(strUserPassword);
                strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
                strFN = "EMail"; drDB[strFN] = strEMail;
                strFN = "MST"; drDB[strFN] = strMST;
                strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
                strFN = "Position"; drDB[strFN] = strPosition;
                strFN = "UUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
                strFN = "InosUUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
                strFN = "InosAccessToken"; drDB[strFN] = null;
                strFN = "InosRefreshToken"; drDB[strFN] = null;
                strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin;
                strFN = "FlagBG"; drDB[strFN] = TConst.Flag.Inactive;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Sys_User.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion

            #region // Inos_AccountService_GetUserX:
            ////
            DataTable dtInos_Sys_User = null;
            {
                ////
                DataSet dsGetData = null;

                Inos_AccountService_GetUserX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strAccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , strEMail // objEMail
                               ////
                    , out dsGetData // dsData
                    );

                dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
                dtInos_Sys_User.TableName = "Sys_User";
            }
            #endregion

            #region // Process:
            {
                ////
                if (dtInos_Sys_User.Rows.Count > 0)
                {
                    strFlagUserExist = TConst.Flag.Active;

                    ////
                    DataSet dsGetData = null;

                    Inos_OrgService_AddInviteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID // objOrgID
                        , strEMail // objEMail
                                   ////
                        , out dsGetData // dsData
                        );
                }
                ////
                else
                {
                    ////
                    string strInosUUID = null;
                    string strUUID = null;

                    ////
                    DataSet dsData = null;

                    Inos_AccountService_RegisterForNetworkX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                                            //, strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objMST // objMST
                        , strEMail // objEmail
                        , strUserName // objName
                        , strUserPassword // objPassword
                        , TConst.InosMix.Default_Language // objLanguage
                        , TConst.InosMix.Default_TimeZone // objTimeZone
                                                          ////
                        , out dsData // dsData
                        );

                    ////
                    DataSet dsGetData = null;

                    Inos_OrgService_AddInviteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID // objOrgID
                        , strEMail // objEMail
                                   ////
                        , out dsGetData // dsData
                        );

                    // //
                    DataTable dtUpd_Sys_User = dsData.Tables["Sys_User"];

                    if (dtUpd_Sys_User.Rows.Count > 0)
                    {
                        strInosUUID = Convert.ToString(dtUpd_Sys_User.Rows[0]["UUID"]);
                    }

                    ////
                    DataSet dsSeq = null;

                    dsSeq = Seq_UUID_Get(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , Convert.ToString(nNetworkID) // strNetworkID
                        , strOrgID // strOrgID
                        , ref alParamsCoupleError // alParamsCoupleError
                        );

                    strUUID = Convert.ToString(CmUtils.CMyDataSet.GetRemark(dsSeq));

                    ////
                    string strSqlUpd_Sys_User = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							update t
							set
								t.UUID = '@strUUID'
								, t.InosUUID = '@strInosUUID'
								, t.LogLUDTimeUTC = '@strLogLUDTimeUTC'
								, t.LogLUBy = '@strLogLUBy'
							from Sys_User t --//[mylock]
							where (1=1)
								and t.UserCode = '@strUserCode'
							;						
						"
                        , "@strUserCode", strUserCode
                        , "@strUUID", strUUID
                        , "@strInosUUID", strInosUUID
                        , "@strLogLUDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                        , "@strLogLUBy", strWAUserCode
                        );

                    _cf.db.ExecQuery(
                        strSqlUpd_Sys_User
                        );

                    Email_BatchSendEmail_Sys_User_SendX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                                            //, strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                                                  ////
                        , strUUID // objUUID
                        , strEMail // objEMail
                        );
                }
            }
            #endregion

            // Assign:
            CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strFlagUserExist);
        }

        private void Sys_User_CreateForNetworkX_New20200110_Old(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref DataSet mdsFinal
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagNNTAdmin
            //, object objOrgID
            , object objCustomerCodeSys
            ////
            )
        {
            #region // Temp:
            //mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strUserName = string.Format("{0}", objUserName).Trim();
            string strUserPassword = string.Format("{0}", objUserPassword);
            string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
            string strEMail = string.Format("{0}", objEMail).Trim();
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
            string strPosition = string.Format("{0}", objPosition);
            string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
            string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
            string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);
            //string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            string strCustomerCodeSys = TUtils.CUtils.StdParam(objCustomerCodeSys);

            string strOrgID = null;
            string strFlagUserExist = TConst.Flag.Inactive;

            ////
            DataTable dtDB_Sys_User = null;
            {
                ////
                if (strUserCode == null || strUserCode.Length <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objUserCode // objUserCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );
                ////
                if (strUserName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserName", strUserName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strUserPassword.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserPassword", strUserPassword
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DataTable dtDB_Mst_NNT = null;

                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // objMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );

                strOrgID = TUtils.CUtils.StdParam(dtDB_Mst_NNT.Rows[0]["OrgID"]);
                //// 20200420. Yêu cầu từ chị Đông không bắt buộc nhập DepartmentCode
                DataTable dtDB_Mst_Department = null;

                if(!string.IsNullOrEmpty(strDepartmentCode))
                {
                    Mst_Department_CheckDB(
                         ref alParamsCoupleError // alParamsCoupleError
                         , strDepartmentCode // strDepartmentCode 
                         , TConst.Flag.Yes // strFlagExistToCheck
                         , TConst.Flag.Active // strFlagActiveListToCheck
                         , out dtDB_Mst_Department // dtDB_Mst_Organ
                        );

                }
                ////
                if (strFlagDLAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagDLAdmin", strFlagDLAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strFlagSysAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagSysAdmin", strFlagSysAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                DataTable dtDB_Sys_UserRoot = null;

                Sys_User_CheckDB(
                    ref alParamsCoupleError //alParamsCoupleError
                    , strWAUserCode // objUserCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , out dtDB_Sys_UserRoot // dtDB_Sys_UserRoot
                    );

                Int32 nFlagSysAdminRoot = Convert.ToInt32(dtDB_Sys_UserRoot.Rows[0]["FlagSysAdmin"]);
                //// Nếu người Đăng nhập không phải là Admin => không cho bật cờ FlagSysAdmin
                if (Convert.ToInt32(strFlagSysAdmin) == 1)
                {
                    if (nFlagSysAdminRoot != 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                        "Check.strWAUserCode", strWAUserCode
                        ,"Check.nFlagSysAdmin", nFlagSysAdminRoot
                        });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_NNTNotAdmin
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }

                ////
                if (!string.Equals(strCustomerCodeSys, TConst.BizMix.CustomerCodeSysRoot))
                {
                    DataTable dtDB_Mst_Customer = null;

                    Mst_Customer_CheckDB(
                        ref alParamsCoupleError //alParamsCoupleError
                        , strOrgID // strOrgID
                        , strCustomerCodeSys // strCustomerCodeSys
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Mst_Customer // dtDB_Mst_Customer
                        );
                }
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.NewRow();
                strFN = "UserCode"; drDB[strFN] = strUserCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "UserName"; drDB[strFN] = strUserName;
                strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetEncodedHash(strUserPassword);
                strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
                strFN = "EMail"; drDB[strFN] = strEMail;
                strFN = "MST"; drDB[strFN] = strMST;
                strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
                strFN = "Position"; drDB[strFN] = strPosition;
                strFN = "UUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
                strFN = "InosUUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
                strFN = "InosAccessToken"; drDB[strFN] = null;
                strFN = "InosRefreshToken"; drDB[strFN] = null;
                strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin;
                strFN = "OrgID"; drDB[strFN] = strOrgID;
                strFN = "CustomerCodeSys"; drDB[strFN] = strCustomerCodeSys;
                strFN = "FlagBG"; drDB[strFN] = TConst.Flag.Inactive;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Sys_User.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion

            #region // Inos_AccountService_GetUserX:
            ////
            DataTable dtInos_Sys_User = null;
            {
                ////
                DataSet dsGetData = null;

                Inos_AccountService_GetUserX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strAccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , strEMail // objEMail
                               ////
                    , out dsGetData // dsData
                    );

                dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
                dtInos_Sys_User.TableName = "Sys_User";
            }
            #endregion

            #region // Process:
            {
                ////
                if (dtInos_Sys_User.Rows.Count > 0)
                {
                    strFlagUserExist = TConst.Flag.Active;

                    ////
                    DataSet dsGetData = null;

                    Inos_OrgService_AddInviteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID // objOrgID
                        , strEMail // objEMail
                                   ////
                        , out dsGetData // dsData
                        );

                    ////
                    #region // Save Lic:
                    {
                        ////
                        DataTable dtDB_Mst_Param = null;

                        Mst_Param_CheckDB(
                            ref alParamsCoupleError
                            , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
                            , TConst.Flag.Yes
                            , out dtDB_Mst_Param //dtDB_Mst_Param
                            );

                        string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
                        ////
                        Mst_Param_CheckDB(
                            ref alParamsCoupleError
                            , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
                            , TConst.Flag.Yes
                            , out dtDB_Mst_Param //dtDB_Mst_Param
                            );

                        string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
                        ////
                        object objAccessToken = null;
                        ////
                        {
                            Inos_AccountService_GetAccessTokenX(
                                strTid // strTid
                                , strGwUserCode // strGwUserCode
                                , strGwPassword // strGwPassword
                                , strWAUserCode // strWAUserCode
                                , strWAUserPassword // strWAUserPassword
                                , ref mdsFinal // mdsFinal
                                , ref alParamsCoupleError // alParamsCoupleError
                                , dtimeSys // dtimeSys
                                           ////
                                , UserCode // objEmail
                                , UserPassword // objPassword
                                               ////
                                , out objAccessToken // objAccessToken
                                );
                        }

                        ////
                        dsGetData = null;

                        List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
                             strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                            , strWAUserPassword // strWAUserPassword
                            , (string)objAccessToken // strAccessToken
                            , ref mdsFinal // mdsFinal
                            , ref alParamsCoupleError // alParamsCoupleError
                            , dtimeSys // dtimeSys
                                       ////
                            , strOrgID // objOrgID
                                       ////
                            , out dsGetData // dsData
                            );
                        ////

                        #region // Sys_Solution: Get.
                        ////
                        DataTable dtDB_Sys_Solution = null;
                        {
                            // GetInfo:
                            dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                                _cf.db // db
                                , "Sys_Solution" // strTableName
                                , "top 1 *" // strColumnList
                                , "" // strClauseOrderBy
                                , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                                );
                        }
                        #endregion
                        ////
                        DataSet dsData_Package = null;
                        Inos_LicService_GetAllPackagesX_New20191113(
                            strTid
                            , strGwUserCode
                            , strGwPassword
                            , strWAUserCode
                            , strWAUserPassword
                            , (string)objAccessToken
                            , ref mdsFinal
                            , ref alParamsCoupleError
                            , dtimeSys
                            , dtDB_Sys_Solution.Rows[0]["SolutionCode"]
                            , out dsData_Package
                            );
                        ////
                        List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

                        for (int nScan = 0; nScan < dsData_Package.Tables[0].Rows.Count; nScan++)
                        {
                            DataRow drScan = dsData_Package.Tables[0].Rows[nScan];
                            long PackageID = Convert.ToInt64(drScan["id"]);
                            foreach (var item in lstOrgLicense)
                            {
                                if (item.PackageId == PackageID)
                                {
                                    OrgSolutionUser obj = new OrgSolutionUser();
                                    obj.LicId = item.Id;
                                    obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
                                    lstOrgSolutionUser.Add(obj);
                                }
                            }
                        }

                        DataSet dsData = null;
                        Int32 result = Inos_LicService_AddOrgSolutionUsersX(
                             strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                            , strWAUserPassword // strWAUserPassword
                            , (string)objAccessToken // strAccessToken
                            , ref mdsFinal // mdsFinal
                            , ref alParamsCoupleError // alParamsCoupleError
                            , dtimeSys // dtimeSys
                                       ////
                            , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                            , strOrgID // objOrgID
                            , lstOrgSolutionUser // lstOrgSolutionUser
                                                 ////
                            , out dsData
                            );
                    }

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "Sys_User";

                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref ds);
                    #endregion
                }
                ////
                else
                {
                    ////
                    //string strInosUUID = null;
                    //string strUUID = null;

                    ////
                    DataSet dsData = null;

                    Inos_User_CreateUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , objMST // objMST
                        , strEMail // objEmail
                        , strUserName // objName
                        , strUserPassword // objPassword
                        , TConst.InosMix.Default_Language // objLanguage
                        , TConst.InosMix.Default_TimeZone // objTimeZone
                                                          ////
                        , out dsData // dsData
                        );

                    DataSet dsGetData = null;

                    Inos_OrgService_AddInviteX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strOrgID // objOrgID
                        , strEMail // objEMail
                                   ////
                        , out dsGetData // dsData
                        );

                    #region // Save Lic:
                    {
                        dsGetData = null;

                        Inos_AccountService_GetUserX(
                            strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                            , strWAUserPassword // strWAUserPassword
                            , strAccessToken // strAccessToken
                            , ref alParamsCoupleError // alParamsCoupleError
                            , dtimeSys // dtimeSys
                                       ////
                            , strEMail // objEMail
                                       ////
                            , out dsGetData // dsData
                            );

                        dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
                        dtInos_Sys_User.TableName = "Sys_User";
                    }
                    {
                        ////
                        DataTable dtDB_Mst_Param = null;

                        Mst_Param_CheckDB(
                            ref alParamsCoupleError
                            , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
                            , TConst.Flag.Yes
                            , out dtDB_Mst_Param //dtDB_Mst_Param
                            );

                        string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
                        ////
                        Mst_Param_CheckDB(
                            ref alParamsCoupleError
                            , TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
                            , TConst.Flag.Yes
                            , out dtDB_Mst_Param //dtDB_Mst_Param
                            );

                        string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
                        ////
                        object objAccessToken = null;
                        ////
                        {
                            Inos_AccountService_GetAccessTokenX(
                                strTid // strTid
                                , strGwUserCode // strGwUserCode
                                , strGwPassword // strGwPassword
                                , strWAUserCode // strWAUserCode
                                , strWAUserPassword // strWAUserPassword
                                , ref mdsFinal // mdsFinal
                                , ref alParamsCoupleError // alParamsCoupleError
                                , dtimeSys // dtimeSys
                                           ////
                                , UserCode // objEmail
                                , UserPassword // objPassword
                                               ////
                                , out objAccessToken // objAccessToken
                                );
                        }

                        ////
                        dsGetData = null;

                        List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
                             strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                            , strWAUserPassword // strWAUserPassword
                            , (string)objAccessToken // strAccessToken
                            , ref mdsFinal // mdsFinal
                            , ref alParamsCoupleError // alParamsCoupleError
                            , dtimeSys // dtimeSys
                                       ////
                            , strOrgID // objOrgID
                                       ////
                            , out dsGetData // dsData
                            );
                        ////

                        #region // Sys_Solution: Get.
                        ////
                        DataTable dtDB_Sys_Solution = null;
                        {
                            // GetInfo:
                            dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                                _cf.db // db
                                , "Sys_Solution" // strTableName
                                , "top 1 *" // strColumnList
                                , "" // strClauseOrderBy
                                , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                                );
                        }
                        #endregion
                        ////
                        DataSet dsData_Package = null;
                        Inos_LicService_GetAllPackagesX_New20191113(
                            strTid
                            , strGwUserCode
                            , strGwPassword
                            , strWAUserCode
                            , strWAUserPassword
                            , (string)objAccessToken
                            , ref mdsFinal
                            , ref alParamsCoupleError
                            , dtimeSys
                            , dtDB_Sys_Solution.Rows[0]["SolutionCode"]
                            , out dsData_Package
                            );
                        ////
                        List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

                        for (int nScan = 0; nScan < dsData_Package.Tables[0].Rows.Count; nScan++)
                        {
                            DataRow drScan = dsData_Package.Tables[0].Rows[nScan];
                            long PackageID = Convert.ToInt64(drScan["id"]);
                            foreach (var item in lstOrgLicense)
                            {
                                if (item.PackageId == PackageID)
                                {
                                    OrgSolutionUser obj = new OrgSolutionUser();
                                    obj.LicId = item.Id;
                                    obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
                                    lstOrgSolutionUser.Add(obj);
                                }
                            }
                        }

                        dsData = null;
                        Int32 result = Inos_LicService_AddOrgSolutionUsersX(
                             strTid // strTid
                            , strGwUserCode // strGwUserCode
                            , strGwPassword // strGwPassword
                            , strWAUserCode // strWAUserCode
                            , strWAUserPassword // strWAUserPassword
                            , (string)objAccessToken // strAccessToken
                            , ref mdsFinal // mdsFinal
                            , ref alParamsCoupleError // alParamsCoupleError
                            , dtimeSys // dtimeSys
                                       ////
                            , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                            , strOrgID // objOrgID
                            , lstOrgSolutionUser // lstOrgSolutionUser
                                                 ////
                            , out dsData
                            );
                    }

                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    ds.Tables.Add(dt);
                    ds.Tables[0].TableName = "Sys_User";

                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref ds);
                    #endregion
                }
            }
			#endregion

			#region // Save Mst_UserMapInventory:
			//// NC.HTTT.20200512: Map user voi Ma kho cha I cua OrgID:
			{
				string sqlSaveMst_UserMapInventory = CmUtils.StringUtils.Replace(@"
						---- Mst_UserMapInventory:
						insert into Mst_UserMapInventory
						(
							OrgID
							, UserCode
							, InvCode
							, NetworkID
							, Remark
							, LogLUDTimeUTC
							, LogLUBy
						)
						select 
							t.OrgID
							, t.UserCode
							, 'I' InvCode
							, t.NetworkID
							, null Remark
							, '2000-01-01 00:00:00' LogLUDTimeUTC
							, 'SYS' LogLUBy
						from Sys_User t --//[mylock]
						where(1=1)
							and t.UserCode = '@strUserCode'
						;
                    "
					, "@strUserCode", strUserCode
				);
				DataSet dtDB = _cf.db.ExecQuery(
						sqlSaveMst_UserMapInventory
						);
				////
			}
			#endregion

			// Assign:
			CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strFlagUserExist);
        }
		private void Sys_User_CreateForNetworkX_New20200110(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, ref DataSet mdsFinal
			, ref ArrayList alParamsCoupleError
			, DateTime dtimeSys
			//// 
			, object objUserCode
			, object objUserName
			, object objUserPassword
			, object objPhoneNo
			, object objEMail
			, object objMST
			, object objDepartmentCode
			, object objPosition
			, object objFlagDLAdmin
			, object objFlagSysAdmin
			, object objFlagNNTAdmin
			, object objCustomerCodeSys
			////
			)
		{
			#region // Temp:
			//mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Sys_User_CreateX";
			//string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				, "objUserName", objUserName
				, "objPhoneNo", objPhoneNo
				, "objEMail", objEMail
				, "objMST", objMST
				, "objDepartmentCode", objDepartmentCode
				, "objPosition", objPosition
				, "objFlagDLAdmin", objFlagDLAdmin
				, "objFlagSysAdmin", objFlagSysAdmin
				, "objCustomerCodeSys", objCustomerCodeSys
				});
			#endregion

			#region // Refine and Check Input:
			////
			string strUserCode = TUtils.CUtils.StdParam(objUserCode);
			string strUserName = string.Format("{0}", objUserName).Trim();
			string strUserPassword = string.Format("{0}", objUserPassword);
			string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
			string strEMail = string.Format("{0}", objEMail).Trim();
			string strMST = TUtils.CUtils.StdParam(objMST);
			string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
			string strPosition = string.Format("{0}", objPosition);
			string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
			string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
			string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);
			string strCustomerCodeSys = TUtils.CUtils.StdParam(objCustomerCodeSys);
			string strOrgID = null;
			string strFlagUserExist = TConst.Flag.Inactive;

			////
			DataTable dtDB_Sys_User = null;
			{
				////
				if (strUserCode == null || strUserCode.Length <= 0)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strUserCode", strUserCode
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Sys_User_Create_InvalidUserCode
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				Sys_User_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, objUserCode // objUserCode
					, TConst.Flag.No // strFlagExistToCheck
					, "" // strFlagActiveListToCheck
					, out dtDB_Sys_User // dtDB_Sys_User
					);
				////
				if (strUserName.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strUserName", strUserName
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Sys_User_Create_InvalidUserName
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strUserPassword.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strUserPassword", strUserPassword
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtDB_Mst_NNT = null;

				Mst_NNT_CheckDB(
					ref alParamsCoupleError // alParamsCoupleError
					, strMST // objMST
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, "" // strTCTStatusListToCheck
					, out dtDB_Mst_NNT // dtDB_Mst_NNT
					);

				strOrgID = TUtils.CUtils.StdParam(dtDB_Mst_NNT.Rows[0]["OrgID"]);
				////
				//DataTable dtDB_Mst_Department = null;

				//Mst_Department_CheckDB(
				//	 ref alParamsCoupleError // alParamsCoupleError
				//	 , strDepartmentCode // strDepartmentCode 
				//	 , TConst.Flag.Yes // strFlagExistToCheck
				//	 , TConst.Flag.Active // strFlagActiveListToCheck
				//	 , out dtDB_Mst_Department // dtDB_Mst_Organ
				//	);
				////
				if (strFlagDLAdmin.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strFlagDLAdmin", strFlagDLAdmin
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				if (strFlagSysAdmin.Length < 1)
				{
					alParamsCoupleError.AddRange(new object[]{
						"Check.strFlagSysAdmin", strFlagSysAdmin
						});
					throw CmUtils.CMyException.Raise(
						TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
						, null
						, alParamsCoupleError.ToArray()
						);
				}
				////
				DataTable dtDB_Sys_UserRoot = null;

				Sys_User_CheckDB(
					ref alParamsCoupleError //alParamsCoupleError
					, strWAUserCode // objUserCode
					, TConst.Flag.Yes // strFlagExistToCheck
					, TConst.Flag.Active // strFlagActiveListToCheck
					, out dtDB_Sys_UserRoot // dtDB_Sys_UserRoot
					);

				Int32 nFlagSysAdminRoot = Convert.ToInt32(dtDB_Sys_UserRoot.Rows[0]["FlagSysAdmin"]);
				//// Nếu người Đăng nhập không phải là Admin => không cho bật cờ FlagSysAdmin
				if (Convert.ToInt32(strFlagSysAdmin) == 1)
				{
					if (nFlagSysAdminRoot != 1)
					{
						alParamsCoupleError.AddRange(new object[]{
						"Check.strWAUserCode", strWAUserCode
						,"Check.nFlagSysAdmin", nFlagSysAdminRoot
						});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Sys_User_Create_NNTNotAdmin
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
                ////
                if (string.IsNullOrEmpty(strCustomerCodeSys))
                {
                    strCustomerCodeSys = TConst.BizMix.CustomerCodeSysRoot;
                }
                else
                {
                    ////
                    if (!string.Equals(strCustomerCodeSys, TConst.BizMix.CustomerCodeSysRoot))
                    {
                        DataTable dtDB_Mst_Customer = null;

                        Mst_Customer_CheckDB(
                            ref alParamsCoupleError //alParamsCoupleError
                            , strOrgID // strOrgID
                            , strCustomerCodeSys // strCustomerCodeSys
                            , TConst.Flag.Yes // strFlagExistToCheck
                            , TConst.Flag.Active // strFlagActiveListToCheck
                            , out dtDB_Mst_Customer // dtDB_Mst_Customer
                            );
                    }
                }
				////
			}
			#endregion

			#region // SaveDB:
			{
				// Init:
				//ArrayList alColumnEffective = new ArrayList();
				string strFN = "";
				DataRow drDB = dtDB_Sys_User.NewRow();
				strFN = "UserCode"; drDB[strFN] = strUserCode;
				strFN = "NetworkID"; drDB[strFN] = nNetworkID;
				strFN = "UserName"; drDB[strFN] = strUserName;
				strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetEncodedHash(strUserPassword);
				strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
				strFN = "EMail"; drDB[strFN] = strEMail;
				strFN = "MST"; drDB[strFN] = strMST;
				strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
				strFN = "Position"; drDB[strFN] = strPosition;
				strFN = "UUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
				strFN = "InosUUID"; drDB[strFN] = TConst.InosMix.Default_Anonymous;
				strFN = "InosAccessToken"; drDB[strFN] = null;
				strFN = "InosRefreshToken"; drDB[strFN] = null;
				strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
				strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
				strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin;
				strFN = "CustomerCodeSys"; drDB[strFN] = strCustomerCodeSys;
				strFN = "OrgID"; drDB[strFN] = strOrgID;
				strFN = "FlagBG"; drDB[strFN] = TConst.Flag.Inactive;
				strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
				strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
				strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
				dtDB_Sys_User.Rows.Add(drDB);

				// Save:
				_cf.db.SaveData(
					"Sys_User"
					, dtDB_Sys_User
					//, alColumnEffective.ToArray()
					);

                // Save vào NotifyManager
                Mst_ManageNotify_CreateX(
                    strTid
                    , strGwUserCode
                    , strGwPassword
                    , strWAUserCode
                    , strWAUserPassword
                    , ref alParamsCoupleError
                    , dtimeSys
                    , strUserCode
                    , strUserName
                    );
            }
			#endregion

			#region // Inos_AccountService_GetUserX:
			// Check Quyền Tạo User Trên Inos
			{
				OrgUser objOrgUser = new OrgUser();
				objOrgUser = Inos_OrgServices_GetMyOrgUserList(
					strTid
					, strGwUserCode
					, strGwPassword
					, strWAUserCode
					, strWAUserPassword
					, strOrgID
					, strAccessToken
					, ref mdsFinal
					, ref alParamsCoupleError
					, dtimeSys
					);

				if (objOrgUser != null)
				{
					if (objOrgUser.Role != OrgUserRoles.Admin)
					{
						alParamsCoupleError.AddRange(new object[]{
						"Check.strUserCode", strWAUserCode
						, "Check.Role", objOrgUser.Role
						});
						throw CmUtils.CMyException.Raise(
							TError.ErridnInventory.Sys_User_Create_InosRole_Invalid
							, null
							, alParamsCoupleError.ToArray()
							);
					}
				}
			}
			////
			DataTable dtInos_Sys_User = null;
			{
				////
				DataSet dsGetData = null;

				Inos_AccountService_GetUserX(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // strWAUserCode
					, strWAUserPassword // strWAUserPassword
					, strAccessToken // strAccessToken
					, ref alParamsCoupleError // alParamsCoupleError
					, dtimeSys // dtimeSys
							   ////
					, strEMail // objEMail
							   ////
					, out dsGetData // dsData
					);

				dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
				dtInos_Sys_User.TableName = "Sys_User";
			}
			#endregion

			#region // Process:
			{
				////
				if (dtInos_Sys_User.Rows.Count > 0)
				{
					#region // Check User Đã thuộc Org Chưa, Nếu đã thuộc Org Chỉ cần Add license:
					OrgUser objOrgUser = new OrgUser();
					{
						objOrgUser = Inos_OrgServices_GetAllOrgUser(
							strTid
							, strGwUserCode
							, strGwPassword
							, strWAUserCode
							, strWAUserPassword
							, strOrgID
							, strAccessToken
							, ref mdsFinal
							, ref alParamsCoupleError
							, dtimeSys
							, (long)dtInos_Sys_User.Rows[0]["id"]
							);
					}
					#endregion

					strFlagUserExist = TConst.Flag.Active;

					////
					DataSet dsGetData = null;
					if (objOrgUser == null)
					{
						Inos_OrgService_AddInviteX(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, strAccessToken // strAccessToken
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, strOrgID // objOrgID
							, strEMail // objEMail
									   ////
							, out dsGetData // dsData
							);
					}

					////
					#region // Save Lic:
					{
						////
						DataTable dtDB_Mst_Param = null;

						Mst_Param_CheckDB(
							ref alParamsCoupleError
							, TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
							, TConst.Flag.Yes
							, out dtDB_Mst_Param //dtDB_Mst_Param
							);

						string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
						////
						Mst_Param_CheckDB(
							ref alParamsCoupleError
							, TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
							, TConst.Flag.Yes
							, out dtDB_Mst_Param //dtDB_Mst_Param
							);

						string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
						////
						object objAccessToken = null;
						////
						{
							Inos_AccountService_GetAccessTokenX(
								strTid // strTid
								, strGwUserCode // strGwUserCode
								, strGwPassword // strGwPassword
								, strWAUserCode // strWAUserCode
								, strWAUserPassword // strWAUserPassword
								, ref mdsFinal // mdsFinal
								, ref alParamsCoupleError // alParamsCoupleError
								, dtimeSys // dtimeSys
										   ////
								, UserCode // objEmail
								, UserPassword // objPassword
											   ////
								, out objAccessToken // objAccessToken
								);
						}

						////
						dsGetData = null;

						List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
							 strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, strOrgID // objOrgID
									   ////
							, out dsGetData // dsData
							);
						////

						#region // Sys_Solution: Get.
						////
						DataTable dtDB_Sys_Solution = null;
						{
							// GetInfo:
							dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
								_cf.db // db
								, "Sys_Solution" // strTableName
								, "top 1 *" // strColumnList
								, "" // strClauseOrderBy
								, "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
								);
						}
						#endregion
						////
						DataSet dsData_Package = null;
						Inos_LicService_GetAllPackagesX_New20191113(
							strTid
							, strGwUserCode
							, strGwPassword
							, strWAUserCode
							, strWAUserPassword
							, (string)objAccessToken
							, ref mdsFinal
							, ref alParamsCoupleError
							, dtimeSys
							, dtDB_Sys_Solution.Rows[0]["SolutionCode"]
							, out dsData_Package
							);
						////
						List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

						for (int nScan = 0; nScan < dsData_Package.Tables[0].Rows.Count; nScan++)
						{
							DataRow drScan = dsData_Package.Tables[0].Rows[nScan];
							long PackageID = Convert.ToInt64(drScan["id"]);
							foreach (var item in lstOrgLicense)
							{
								if (item.PackageId == PackageID)
								{
									OrgSolutionUser obj = new OrgSolutionUser();
									obj.LicId = item.Id;
									obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
									lstOrgSolutionUser.Add(obj);
								}
							}
						}

						DataSet dsData = null;
						Int32 result = Inos_LicService_AddOrgSolutionUsersX(
							 strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
							, strOrgID // objOrgID
							, lstOrgSolutionUser // lstOrgSolutionUser
												 ////
							, out dsData
							);
					}

					DataSet ds = new DataSet();
					DataTable dt = new DataTable();
					ds.Tables.Add(dt);
					ds.Tables[0].TableName = "Sys_User";

					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref ds);
					#endregion
				}
				////
				else
				{
					////
					//string strInosUUID = null;
					//string strUUID = null;

					////
					DataSet dsData = null;

					Inos_User_CreateUserX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, objMST // objMST
						, strEMail // objEmail
						, strUserName // objName
						, strUserPassword // objPassword
						, TConst.InosMix.Default_Language // objLanguage
						, TConst.InosMix.Default_TimeZone // objTimeZone
														  ////
						, out dsData // dsData
						);

					DataSet dsGetData = null;

					Inos_OrgService_AddInviteX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strOrgID // objOrgID
						, strEMail // objEMail
								   ////
						, out dsGetData // dsData
						);

					#region // Save Lic:
					{
						dsGetData = null;

						Inos_AccountService_GetUserX(
							strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, strAccessToken // strAccessToken
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, strEMail // objEMail
									   ////
							, out dsGetData // dsData
							);

						dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
						dtInos_Sys_User.TableName = "Sys_User";
					}
					{
						////
						DataTable dtDB_Mst_Param = null;

						Mst_Param_CheckDB(
							ref alParamsCoupleError
							, TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
							, TConst.Flag.Yes
							, out dtDB_Mst_Param //dtDB_Mst_Param
							);

						string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
						////
						Mst_Param_CheckDB(
							ref alParamsCoupleError
							, TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
							, TConst.Flag.Yes
							, out dtDB_Mst_Param //dtDB_Mst_Param
							);

						string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
						////
						object objAccessToken = null;
						////
						{
							Inos_AccountService_GetAccessTokenX(
								strTid // strTid
								, strGwUserCode // strGwUserCode
								, strGwPassword // strGwPassword
								, strWAUserCode // strWAUserCode
								, strWAUserPassword // strWAUserPassword
								, ref mdsFinal // mdsFinal
								, ref alParamsCoupleError // alParamsCoupleError
								, dtimeSys // dtimeSys
										   ////
								, UserCode // objEmail
								, UserPassword // objPassword
											   ////
								, out objAccessToken // objAccessToken
								);
						}

						////
						dsGetData = null;

						List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
							 strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, strOrgID // objOrgID
									   ////
							, out dsGetData // dsData
							);
						////

						#region // Sys_Solution: Get.
						////
						DataTable dtDB_Sys_Solution = null;
						{
							// GetInfo:
							dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
								_cf.db // db
								, "Sys_Solution" // strTableName
								, "top 1 *" // strColumnList
								, "" // strClauseOrderBy
								, "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
								);
						}
						#endregion
						////
						DataSet dsData_Package = null;
						Inos_LicService_GetAllPackagesX_New20191113(
							strTid
							, strGwUserCode
							, strGwPassword
							, strWAUserCode
							, strWAUserPassword
							, (string)objAccessToken
							, ref mdsFinal
							, ref alParamsCoupleError
							, dtimeSys
							, dtDB_Sys_Solution.Rows[0]["SolutionCode"]
							, out dsData_Package
							);
						////
						List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

						for (int nScan = 0; nScan < dsData_Package.Tables[0].Rows.Count; nScan++)
						{
							DataRow drScan = dsData_Package.Tables[0].Rows[nScan];
							long PackageID = Convert.ToInt64(drScan["id"]);
							foreach (var item in lstOrgLicense)
							{
								if (item.PackageId == PackageID)
								{
									OrgSolutionUser obj = new OrgSolutionUser();
									obj.LicId = item.Id;
									obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
									lstOrgSolutionUser.Add(obj);
								}
							}
						}

						dsData = null;
						Int32 result = Inos_LicService_AddOrgSolutionUsersX(
							 strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
							, strOrgID // objOrgID
							, lstOrgSolutionUser // lstOrgSolutionUser
												 ////
							, out dsData
							);
					}

					DataSet ds = new DataSet();
					DataTable dt = new DataTable();
					ds.Tables.Add(dt);
					ds.Tables[0].TableName = "Sys_User";

					CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref ds);
					#endregion
				}
			}
            #endregion

            #region // Save Mst_UserMapInventory:
            //// NC.HTTT.20200512: Map user voi Ma kho cha I cua OrgID:
            {
      //          string sqlSaveMst_UserMapInventory = CmUtils.StringUtils.Replace(@"
						//---- Mst_UserMapInventory:
						//insert into Mst_UserMapInventory
						//(
						//	OrgID
						//	, UserCode
						//	, InvCode
						//	, NetworkID
						//	, Remark
						//	, LogLUDTimeUTC
						//	, LogLUBy
						//)
						//select 
						//	t.OrgID
						//	, t.UserCode
						//	, 'I' InvCode
						//	, t.NetworkID
						//	, null Remark
						//	, '2000-01-01 00:00:00' LogLUDTimeUTC
						//	, 'SYS' LogLUBy
						//from Sys_User t --//[mylock]
						//where(1=1)
						//	and t.UserCode = '@strUserCode'
						//;
      //              "
      //              , "@strUserCode", strUserCode
      //          );
      //          DataSet dtDB = _cf.db.ExecQuery(
      //                  sqlSaveMst_UserMapInventory
      //                  );
                ////
            }
            #endregion

            // Assign:
            CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strFlagUserExist);
		}

        private void Sys_User_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagNNTAdmin
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Sys_User_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strUserName = string.Format("{0}", objUserName).Trim();
            string strUserPassword = string.Format("{0}", objUserPassword);
            string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
            string strEMail = string.Format("{0}", objEMail).Trim();
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
            string strPosition = string.Format("{0}", objPosition);
            string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
            string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
            string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);
            ////
            DataTable dtDB_Sys_User = null;
            {
                ////
                if (strUserCode == null || strUserCode.Length <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objUserCode // objUserCode
                    , TConst.Flag.No // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dtDB_Sys_User
                    );
                ////
                if (strUserName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserName", strUserName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strUserPassword.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserPassword", strUserPassword
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                //// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                //if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.strUserPassword", strUserPassword
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                DataTable dtDB_Mst_NNT = null;

                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // objMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );
                ////
                DataTable dtDB_Mst_Department = null;

                Mst_Department_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strDepartmentCode // strDepartmentCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , TConst.Flag.Active // strFlagActiveListToCheck
                     , out dtDB_Mst_Department // dtDB_Mst_Organ
                    );
                ////
                if (strFlagDLAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagDLAdmin", strFlagDLAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strFlagSysAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagSysAdmin", strFlagSysAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.NewRow();
                strFN = "UserCode"; drDB[strFN] = strUserCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "UserName"; drDB[strFN] = strUserName;
                strFN = "UserPassword"; drDB[strFN] = strUserPassword;
                strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
                strFN = "EMail"; drDB[strFN] = strEMail;
                strFN = "MST"; drDB[strFN] = strMST;
                strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
                strFN = "Position"; drDB[strFN] = strPosition;
                strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Sys_User.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        private void RptSv_Sys_User_CreateX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// 
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagNNTAdmin
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "Sys_User_CreateX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                });
            #endregion

            #region // Refine and Check Input:
            ////
            string strUserCode = TUtils.CUtils.StdParam(objUserCode);
            string strUserName = string.Format("{0}", objUserName).Trim();
            string strUserPassword = string.Format("{0}", objUserPassword);
            string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
            string strEMail = string.Format("{0}", objEMail).Trim();
            string strMST = TUtils.CUtils.StdParam(objMST);
            string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
            string strPosition = string.Format("{0}", objPosition);
            string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
            string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
            string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);
            ////
            DataTable dtDB_Sys_User = null;
            {
                ////
                if (strUserCode == null || strUserCode.Length <= 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }

                dtDB_Sys_User = TDALUtils.DBUtils.GetSchema(_cf.db, "Sys_User").Tables[0];
                //Sys_User_CheckDB(
                //	ref alParamsCoupleError // alParamsCoupleError
                //	, objUserCode // objUserCode
                //	, TConst.Flag.No // strFlagExistToCheck
                //	, "" // strFlagActiveListToCheck
                //	, out dtDB_Sys_User // dtDB_Sys_User
                //	);
                ////
                if (strUserName.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserName", strUserName
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserName
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strUserPassword.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserPassword", strUserPassword
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                //// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                //if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                //{
                //	alParamsCoupleError.AddRange(new object[]{
                //		"Check.strUserPassword", strUserPassword
                //		});
                //	throw CmUtils.CMyException.Raise(
                //		TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                //		, null
                //		, alParamsCoupleError.ToArray()
                //		);
                //}
                ////
                DataTable dtDB_Mst_NNT = null;

                Mst_NNT_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strMST // objMST
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , TConst.Flag.Active // strFlagActiveListToCheck
                    , "" // strTCTStatusListToCheck
                    , out dtDB_Mst_NNT // dtDB_Mst_NNT
                    );
                ////
                DataTable dtDB_Mst_Department = null;

                Mst_Department_CheckDB(
                     ref alParamsCoupleError // alParamsCoupleError
                     , strDepartmentCode // strDepartmentCode 
                     , TConst.Flag.Yes // strFlagExistToCheck
                     , TConst.Flag.Active // strFlagActiveListToCheck
                     , out dtDB_Mst_Department // dtDB_Mst_Organ
                    );
                ////
                if (strFlagDLAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagDLAdmin", strFlagDLAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                if (strFlagSysAdmin.Length < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strFlagSysAdmin", strFlagSysAdmin
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
            }
            #endregion

            #region // SaveDB:
            {
                // Init:
                //ArrayList alColumnEffective = new ArrayList();
                string strFN = "";
                DataRow drDB = dtDB_Sys_User.NewRow();
                strFN = "UserCode"; drDB[strFN] = strUserCode;
                strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                strFN = "UserName"; drDB[strFN] = strUserName;
                strFN = "UserPassword"; drDB[strFN] = strUserPassword;
                strFN = "PhoneNo"; drDB[strFN] = strPhoneNo;
                strFN = "EMail"; drDB[strFN] = strEMail;
                strFN = "MST"; drDB[strFN] = strMST;
                strFN = "DepartmentCode"; drDB[strFN] = strDepartmentCode;
                strFN = "Position"; drDB[strFN] = strPosition;
                strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin;
                strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                dtDB_Sys_User.Rows.Add(drDB);

                // Save:
                _cf.db.SaveData(
                    "Sys_User"
                    , dtDB_Sys_User
                    //, alColumnEffective.ToArray()
                    );
            }
            #endregion
        }

        public DataSet WAS_Sys_User_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<Sys_User> lst_Sys_User = new List<Sys_User>();
                //List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                #endregion

                #region // Sys_User_Create:
                mdsResult = Sys_User_Create_New20200110(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , objRQ_Sys_User.NetworkID // strAccessToken
                    , objRQ_Sys_User.OrgID // strAccessToken
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UserCode // objUserCode
                    , objRQ_Sys_User.Sys_User.UserName // objUserName
                    , objRQ_Sys_User.Sys_User.UserPassword // objUserPassword
                    , objRQ_Sys_User.Sys_User.PhoneNo //object objPhoneNo
                    , objRQ_Sys_User.Sys_User.EMail //object objEMail
                    , objRQ_Sys_User.Sys_User.MST //object objMST
                    , objRQ_Sys_User.Sys_User.DepartmentCode //object objDepartmentCode
                    , objRQ_Sys_User.Sys_User.Position //object objPosition
                    , objRQ_Sys_User.Sys_User.FlagDLAdmin //object objFlagDLAdmin
                    , objRQ_Sys_User.Sys_User.FlagSysAdmin // objFlagSysAdmin
                    , objRQ_Sys_User.Sys_User.FlagNNTAdmin // objFlagNNTAdmin
                    , objRQ_Sys_User.Sys_User.CustomerCodeSys // CustomerCodeSys
                    );
                #endregion

                #region // GetData:
                //if (!CmUtils.CMyDataSet.HasError(mdsResult))
                //{
                //	//DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                //	//lst_Sys_User = TUtils.DataTableCmUtils.ToListof<Sys_User>(dt_Sys_User);
                //	//objRT_Sys_User.Lst_Sys_User = lst_Sys_User;
                //}
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        public DataSet Sys_User_Create(
            string strTid
            , DataRow drSession
            ////
            , object objUserCode
            , object objUserNick
            , object objBankCode
            , object objUserName
            , object objUserPassword
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Create;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
					////
					, "objUserCode", objUserCode
                    , "objUserNick", objUserNick
                    , "objBankCode", objBankCode
                    , "objUserName", objUserName
					//, "objUserPassword", objUserPassword
                    , "objFlagDLAdmin", objFlagDLAdmin
                    , "objFlagSysAdmin", objFlagSysAdmin
                    });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strUserNick = Convert.ToString(objUserNick);
                string strBankCode = TUtils.CUtils.StdParam(objBankCode);
                string strUserName = string.Format("{0}", objUserName).Trim();
                string strUserPassword = string.Format("{0}", objUserPassword);
                string strFlagDLAdmin = TUtils.CUtils.StdParam(objFlagDLAdmin);
                string strFlagSysAdmin = TUtils.CUtils.StdParam(objFlagSysAdmin);
                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    if (strUserCode == null || strUserCode.Length <= 0)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserCode", strUserCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    ////
                    if (string.IsNullOrEmpty(strUserNick))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserNick", strUserNick
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserNick
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strUserName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserName", strUserName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strUserPassword.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserPassword", strUserPassword
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    //// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                    //if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                    //{
                    //	alParamsCoupleError.AddRange(new object[]{
                    //		"Check.strUserPassword", strUserPassword
                    //		});
                    //	throw CmUtils.CMyException.Raise(
                    //		TError.ErridnInventory.Sys_User_Create_InvalidUserPassword
                    //		, null
                    //		, alParamsCoupleError.ToArray()
                    //		);
                    //}
                    ////
                    if (strFlagDLAdmin.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strFlagDLAdmin", strFlagDLAdmin
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidFlagDLAdmin
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (strFlagSysAdmin.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strFlagSysAdmin", strFlagSysAdmin
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Create_InvalidFlagSysAdmin
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                }
                #endregion

                #region // SaveDB UserCode:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_User.NewRow();
                    strFN = "UserCode"; drDB[strFN] = strUserCode;
                    strFN = "UserNick"; drDB[strFN] = strUserNick;
                    strFN = "BankCode"; drDB[strFN] = strBankCode;
                    strFN = "UserName"; drDB[strFN] = strUserName;
                    strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetSimpleHash(strUserPassword);
                    strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin;
                    strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Yes;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode;
                    dtDB_Sys_User.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        //, alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_User_Update(
            string strTid
            , DataRow drSession
            ////
            , object objUserCode
            , object objUserNick
            , object objBankCode
            , object objUserName
            , object objUserPassword
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Update;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
					////
					, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    , "objUserCode", objUserCode
                    , "objBankCode", objBankCode
                    , "objUserName", objUserName
                    , "objUserPassword", objUserPassword
                    , "objFlagDLAdmin", objFlagDLAdmin
                    , "objFlagSysAdmin", objFlagSysAdmin
                    , "objFlagActive", objFlagActive
					////
					, "objFt_Cols_Upd", objFt_Cols_Upd
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strUserNick = Convert.ToString(objUserNick);
                string strBankCode = TUtils.CUtils.StdParam(objBankCode);
                string strUserName = string.Format("{0}", objUserName).Trim();
                string strUserPassword = string.Format("{0}", objUserPassword);
                string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
                string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

                ////
                //bool bUpd_UserNick = strFt_Cols_Upd.Contains("Sys_User.UserNick".ToUpper());
                bool bUpd_UserName = strFt_Cols_Upd.Contains("Sys_User.UserName".ToUpper());
                bool bUpd_UserPassword = strFt_Cols_Upd.Contains("Sys_User.UserPassword".ToUpper());
                bool bUpd_FlagDLAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagDLAdmin".ToUpper());
                bool bUpd_FlagSysAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagSysAdmin".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_User.FlagActive".ToUpper());

                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    ////
                    if (bUpd_UserName && strUserName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserName", strUserName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Update_InvalidUserName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    if (bUpd_UserPassword)
                    {
                        //// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                        //if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                        //{
                        //	alParamsCoupleError.AddRange(new object[]{
                        //		"Check.strUserPassword", strUserPassword
                        //		});
                        //	throw CmUtils.CMyException.Raise(
                        //		TError.ErridnInventory.Sys_User_Update_InvalidUserPassword
                        //		, null
                        //		, alParamsCoupleError.ToArray()
                        //		);
                        //}
                    }
                    ////
                }
                #endregion

                #region // SaveDB Sys_User:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_User.Rows[0];
                    //if (bUpd_UserNick) { strFN = "UserNick"; drDB[strFN] = strUserNick; alColumnEffective.Add(strFN); }
                    if (bUpd_UserName) { strFN = "UserName"; drDB[strFN] = strUserName; alColumnEffective.Add(strFN); }
                    if (bUpd_UserPassword) { strFN = "UserPassword"; drDB[strFN] = TUtils.CUtils.GetSimpleHash(strUserPassword); alColumnEffective.Add(strFN); }
                    if (bUpd_FlagDLAdmin) { strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagSysAdmin) { strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = _cf.sinf.strUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_User_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagSysAdmin
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                , "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strUserName = string.Format("{0}", objUserName).Trim();
                string strUserPassword = string.Format("{0}", objUserPassword);
                string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
                string strEMail = string.Format("{0}", objEMail).Trim();
                string strMST = TUtils.CUtils.StdParam(objMST);
                string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
                string strPosition = string.Format("{0}", objPosition);
                string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
                string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

                ////
                //bool bUpd_UserNick = strFt_Cols_Upd.Contains("Sys_User.UserNick".ToUpper());
                bool bUpd_UserName = strFt_Cols_Upd.Contains("Sys_User.UserName".ToUpper());
                //bool bUpd_UserPassword = strFt_Cols_Upd.Contains("Sys_User.UserPassword".ToUpper());
                bool bUpd_PhoneNo = strFt_Cols_Upd.Contains("Sys_User.PhoneNo".ToUpper());
                bool bUpd_EMail = strFt_Cols_Upd.Contains("Sys_User.EMail".ToUpper());
                bool bUpd_Position = strFt_Cols_Upd.Contains("Sys_User.Position".ToUpper());
                bool bUpd_FlagDLAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagDLAdmin".ToUpper());
                bool bUpd_FlagSysAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagSysAdmin".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_User.FlagActive".ToUpper());

                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    ////
                    if (bUpd_UserName && strUserName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserName", strUserName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Update_InvalidUserName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    //               if (bUpd_UserPassword)
                    //               {
                    //	// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                    //	if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                    //	{
                    //		alParamsCoupleError.AddRange(new object[]{
                    //			"Check.strUserPassword", strUserPassword
                    //			});
                    //		throw CmUtils.CMyException.Raise(
                    //			TError.ErridnInventory.Sys_User_Update_InvalidUserPassword
                    //			, null
                    //			, alParamsCoupleError.ToArray()
                    //			);
                    //	}
                    //}
                    ////
                }
                #endregion

                #region // SaveDB Sys_User:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_User.Rows[0];
                    //if (bUpd_UserNick) { strFN = "UserNick"; drDB[strFN] = strUserNick; alColumnEffective.Add(strFN); }
                    if (bUpd_UserName) { strFN = "UserName"; drDB[strFN] = strUserName; alColumnEffective.Add(strFN); }
                    //if (bUpd_UserPassword) { strFN = "UserPassword"; drDB[strFN] = strUserPassword; alColumnEffective.Add(strFN); }
                    if (bUpd_PhoneNo) { strFN = "PhoneNo"; drDB[strFN] = strPhoneNo; alColumnEffective.Add(strFN); }
                    if (bUpd_EMail) { strFN = "EMail"; drDB[strFN] = strEMail; alColumnEffective.Add(strFN); }
                    if (bUpd_Position) { strFN = "Position"; drDB[strFN] = strPosition; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagDLAdmin) { strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagSysAdmin) { strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                #region // MasterServer:
                {
                    // //
                    DataSet dsData = new DataSet();

                    Inos_AccountService_EditProfileX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strUserName // objName
                        , strUserPassword // objOldPassword
                        , strUserPassword // objNewPassword
                        , false // objChangePassword
                        , false // objChangeAvatar
                        , bUpd_UserName // objChangeName
                        , null // objAvatarBase64
                        , out dsData // dsData
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_User_Update_New20191109(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objFlagDLAdmin
            , object objFlagNNTAdmin
            , object objFlagSysAdmin
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagNNTAdmin", objFlagNNTAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                , "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strUserName = string.Format("{0}", objUserName).Trim();
                string strUserPassword = string.Format("{0}", objUserPassword);
                string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
                string strEMail = string.Format("{0}", objEMail).Trim();
                string strMST = TUtils.CUtils.StdParam(objMST);
                string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
                string strPosition = string.Format("{0}", objPosition);
                string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
                string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);
                string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

                ////
                //bool bUpd_UserNick = strFt_Cols_Upd.Contains("Sys_User.UserNick".ToUpper());
                bool bUpd_UserName = strFt_Cols_Upd.Contains("Sys_User.UserName".ToUpper());
                //bool bUpd_UserPassword = strFt_Cols_Upd.Contains("Sys_User.UserPassword".ToUpper());
                bool bUpd_PhoneNo = strFt_Cols_Upd.Contains("Sys_User.PhoneNo".ToUpper());
                bool bUpd_EMail = strFt_Cols_Upd.Contains("Sys_User.EMail".ToUpper());
                bool bUpd_Position = strFt_Cols_Upd.Contains("Sys_User.Position".ToUpper());
                bool bUpd_FlagDLAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagDLAdmin".ToUpper());
                bool bUpd_FlagNNTAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagNNTAdmin".ToUpper());
                bool bUpd_FlagSysAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagSysAdmin".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_User.FlagActive".ToUpper());

                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    ////
                    if (bUpd_UserName && strUserName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserName", strUserName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Update_InvalidUserName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    //               if (bUpd_UserPassword)
                    //               {
                    //	// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                    //	if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                    //	{
                    //		alParamsCoupleError.AddRange(new object[]{
                    //			"Check.strUserPassword", strUserPassword
                    //			});
                    //		throw CmUtils.CMyException.Raise(
                    //			TError.ErridnInventory.Sys_User_Update_InvalidUserPassword
                    //			, null
                    //			, alParamsCoupleError.ToArray()
                    //			);
                    //	}
                    //}
                    ////
                }
                #endregion

                #region // SaveDB Sys_User:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_User.Rows[0];
                    //if (bUpd_UserNick) { strFN = "UserNick"; drDB[strFN] = strUserNick; alColumnEffective.Add(strFN); }
                    if (bUpd_UserName) { strFN = "UserName"; drDB[strFN] = strUserName; alColumnEffective.Add(strFN); }
                    //if (bUpd_UserPassword) { strFN = "UserPassword"; drDB[strFN] = strUserPassword; alColumnEffective.Add(strFN); }
                    if (bUpd_PhoneNo) { strFN = "PhoneNo"; drDB[strFN] = strPhoneNo; alColumnEffective.Add(strFN); }
                    if (bUpd_EMail) { strFN = "EMail"; drDB[strFN] = strEMail; alColumnEffective.Add(strFN); }
                    if (bUpd_Position) { strFN = "Position"; drDB[strFN] = strPosition; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagDLAdmin) { strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagNNTAdmin) { strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagSysAdmin) { strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                #region // MasterServer:
                {
                    // //
                    DataSet dsData = new DataSet();

                    Inos_AccountService_EditProfileX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strUserName // objName
                        , strUserPassword // objOldPassword
                        , strUserPassword // objNewPassword
                        , false // objChangePassword
                        , false // objChangeAvatar
                        , bUpd_UserName // objChangeName
                        , null // objAvatarBase64
                        , out dsData // dsData
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }


        public DataSet Sys_User_Update_New20191226(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            , object objUserName
            , object objUserPassword
            , object objPhoneNo
            , object objEMail
            , object objMST
            , object objDepartmentCode
            , object objPosition
            , object objAvatar
            , object objFlagDLAdmin
            , object objFlagNNTAdmin
            , object objFlagSysAdmin
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                , "objUserName", objUserName
                , "objPhoneNo", objPhoneNo
                , "objEMail", objEMail
                , "objMST", objMST
                , "objDepartmentCode", objDepartmentCode
                , "objPosition", objPosition
                , "objFlagDLAdmin", objFlagDLAdmin
                , "objFlagNNTAdmin", objFlagNNTAdmin
                , "objFlagSysAdmin", objFlagSysAdmin
                , "objFlagActive", objFlagActive
                , "objAvatar", objAvatar
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                string strUserName = string.Format("{0}", objUserName).Trim();
                string strUserPassword = string.Format("{0}", objUserPassword);
                string strPhoneNo = string.Format("{0}", objPhoneNo).Trim();
                string strEMail = string.Format("{0}", objEMail).Trim();
                string strMST = TUtils.CUtils.StdParam(objMST);
                string strDepartmentCode = TUtils.CUtils.StdParam(objDepartmentCode);
                string strPosition = string.Format("{0}", objPosition);
                string strAvatar = string.Format("{0}", objAvatar);
                string strFlagDLAdmin = TUtils.CUtils.StdFlag(objFlagDLAdmin);
                string strFlagNNTAdmin = TUtils.CUtils.StdFlag(objFlagNNTAdmin);
                string strFlagSysAdmin = TUtils.CUtils.StdFlag(objFlagSysAdmin);
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);

                ////
                //bool bUpd_UserNick = strFt_Cols_Upd.Contains("Sys_User.UserNick".ToUpper());
                bool bUpd_UserName = strFt_Cols_Upd.Contains("Sys_User.UserName".ToUpper());
                //bool bUpd_UserPassword = strFt_Cols_Upd.Contains("Sys_User.UserPassword".ToUpper());
                bool bUpd_PhoneNo = strFt_Cols_Upd.Contains("Sys_User.PhoneNo".ToUpper());
                bool bUpd_EMail = strFt_Cols_Upd.Contains("Sys_User.EMail".ToUpper());
                bool bUpd_Position = strFt_Cols_Upd.Contains("Sys_User.Position".ToUpper());
                bool bUpd_Avatar = strFt_Cols_Upd.Contains("Sys_User.Avatar".ToUpper());
                bool bUpd_FlagDLAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagDLAdmin".ToUpper());
                bool bUpd_FlagNNTAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagNNTAdmin".ToUpper());
                bool bUpd_FlagSysAdmin = strFt_Cols_Upd.Contains("Sys_User.FlagSysAdmin".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_User.FlagActive".ToUpper());

                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    ////
                    if (bUpd_UserName && strUserName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strUserName", strUserName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_User_Update_InvalidUserName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    ////
                    //               if (bUpd_UserPassword)
                    //               {
                    //	// 20180403. Anh Hải bảo bỏ Verify Mật Khẩu.
                    //	if (!TUtils.PasswordPolicy.IsValid(TUtils.CUtils.StdParam(strUserPassword)))
                    //	{
                    //		alParamsCoupleError.AddRange(new object[]{
                    //			"Check.strUserPassword", strUserPassword
                    //			});
                    //		throw CmUtils.CMyException.Raise(
                    //			TError.ErridnInventory.Sys_User_Update_InvalidUserPassword
                    //			, null
                    //			, alParamsCoupleError.ToArray()
                    //			);
                    //	}
                    //}
                    ////
                }
                #endregion

                #region // SaveDB Sys_User:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_User.Rows[0];
                    //if (bUpd_UserNick) { strFN = "UserNick"; drDB[strFN] = strUserNick; alColumnEffective.Add(strFN); }
                    if (bUpd_UserName) { strFN = "UserName"; drDB[strFN] = strUserName; alColumnEffective.Add(strFN); }
                    //if (bUpd_UserPassword) { strFN = "UserPassword"; drDB[strFN] = strUserPassword; alColumnEffective.Add(strFN); }
                    if (bUpd_PhoneNo) { strFN = "PhoneNo"; drDB[strFN] = strPhoneNo; alColumnEffective.Add(strFN); }
                    if (bUpd_EMail) { strFN = "EMail"; drDB[strFN] = strEMail; alColumnEffective.Add(strFN); }
                    if (bUpd_Position) { strFN = "Position"; drDB[strFN] = strPosition; alColumnEffective.Add(strFN); }
                    if (bUpd_Avatar) { strFN = "Avatar"; drDB[strFN] = strAvatar; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagDLAdmin) { strFN = "FlagDLAdmin"; drDB[strFN] = strFlagDLAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagNNTAdmin) { strFN = "FlagNNTAdmin"; drDB[strFN] = strFlagNNTAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagSysAdmin) { strFN = "FlagSysAdmin"; drDB[strFN] = strFlagSysAdmin; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                #region // MasterServer:
                {
                    // //
                    DataSet dsData = new DataSet();

                    Inos_AccountService_EditProfileX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strUserName // objName
                        , strUserPassword // objOldPassword
                        , strUserPassword // objNewPassword
                        , false // objChangePassword
                        , false // objChangeAvatar
                        , bUpd_UserName // objChangeName
                        , null // objAvatarBase64
                        , out dsData // dsData
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet WAS_Sys_User_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_User> lst_Sys_User = new List<Sys_User>();
                //List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                #endregion

                #region // Sys_User_Create:
                mdsResult = Sys_User_Update_New20191226(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , objRQ_Sys_User.NetworkID // strAccessToken
                    , objRQ_Sys_User.OrgID // strAccessToken
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UserCode // objUserCode
                    , objRQ_Sys_User.Sys_User.UserName // objUserName
                    , objRQ_Sys_User.Sys_User.UserPassword // objUserPassword
                    , objRQ_Sys_User.Sys_User.PhoneNo //object objPhoneNo
                    , objRQ_Sys_User.Sys_User.EMail //object objEMail
                    , objRQ_Sys_User.Sys_User.MST //object objMST
                    , objRQ_Sys_User.Sys_User.DepartmentCode //object objDepartmentCode
                    , objRQ_Sys_User.Sys_User.Position //object objPosition
                    , objRQ_Sys_User.Sys_User.Avatar //object objAvatar
                    , objRQ_Sys_User.Sys_User.FlagDLAdmin // objFlagDLAdmin
                    , objRQ_Sys_User.Sys_User.FlagNNTAdmin // objFlagNNTAdmin
                    , objRQ_Sys_User.Sys_User.FlagSysAdmin // objFlagSysAdmin
                    , objRQ_Sys_User.Sys_User.FlagActive // objFlagActive
                                                         ////
                    , objRQ_Sys_User.Ft_Cols_Upd// objFt_Cols_Upd
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_User_Delete(
            string strTid
            , DataRow drSession
            ////
            , object objUserCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Delete;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objUserCode", objUserCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                ////
                DataTable dtDB_Sys_User = null;
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    //// Delete Sys_UserInGroup:
                    Sys_UserInGroup_Delete_ByUser(
                        strUserCode // strUserCode
                        );
                    ////
                }
                #endregion

                #region // SaveDB UserCode:
                {
                    // Init:
                    dtDB_Sys_User.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_User_Delete_Old(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            ////
            , object objUserCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_User_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strUserCode = TUtils.CUtils.StdParam(objUserCode);
                ////
                DataTable dtDB_Sys_User = null;
                DataTable dtDB_Mst_NNT = null;
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dtDB_Sys_User
                        );
                    //// Delete Sys_UserInGroup:
                    Sys_UserInGroup_Delete_ByUser(
                        strUserCode // strUserCode
                        );
                    ////
                    Mst_NNT_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , dtDB_Sys_User.Rows[0]["MST"] // objMST
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , "" // strTCTStatusListToCheck
                        , out dtDB_Mst_NNT // dtDB_Mst_NNT
                        );
                }
                #endregion

                #region // SaveDB UserCode:
                {
                    // Init:
                    dtDB_Sys_User.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "Sys_User"
                        , dtDB_Sys_User
                        );
                }
                #endregion

                #region // Inos_AccountService_GetUserX:
                ////
                DataTable dtInos_Sys_User = null;
                {
                    ////
                    DataSet dsGetData = null;

                    Inos_AccountService_GetUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strUserCode // objEMail
                                      ////
                        , out dsGetData // dsData
                        );

                    dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
                    dtInos_Sys_User.TableName = "Sys_User";
                }
                #endregion

                #region // Inos_AccountService_GetUserX:
                ////
                {
                    ////
                    DataSet dsGetData = null;

                    Inos_OrgService_DeleteUserX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
                        , dtInos_Sys_User.Rows[0]["Id"] // objUserID
                                                        ////
                        , out dsGetData // dsData
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
		public DataSet Sys_User_Delete(
			string strTid
			, string strGwUserCode
			, string strGwPassword
			, string strWAUserCode
			, string strWAUserPassword
			, string strAccessToken
			, string strNetworkID
			, string strOrgID_RQ
			, string strFlagIsEndUser
			, ref ArrayList alParamsCoupleError
			////
			, object objUserCode
			)
		{
			#region // Temp:
			DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
			//int nTidSeq = 0;
			DateTime dtimeSys = DateTime.UtcNow;
			string strFunctionName = "Sys_User_Delete";
			string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Delete;
			alParamsCoupleError.AddRange(new object[]{
				"strFunctionName", strFunctionName
				, "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", objUserCode
				});
			#endregion

			try
			{
				#region // Convert Input:
				DateTime dtimeTDate = DateTime.UtcNow;
				#endregion

				#region // Init:
				//_cf.db.LogUserId = _cf.sinf.strUserCode;
				_cf.db.BeginTransaction();

				// Write RequestLog:
				_cf.ProcessBizReq_OutSide(
					strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					, alParamsCoupleError // alParamsCoupleError
					);

				bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
				if (!bFlagIsEndUser)
				{
					Sys_User_CheckAuthorize(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
										//, strWAUserPassword // strWAUserPassword
						, ref mdsFinal // mdsFinal
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
						, strAccessToken // strAccessToken
						, strNetworkID // strNetworkID
						, strOrgID_RQ // strOrgID
						, TConst.Flag.Active // strFlagUserCodeToCheck
						);

					//Check Access/ Deny:
					Sys_Access_CheckDenyV30(
						ref alParamsCoupleError
						, strWAUserCode
						, strFunctionName
						);
				}
				#endregion

				#region // Refine and Check Input:
				////
				string strUserCode = TUtils.CUtils.StdParam(objUserCode);
				////
				DataTable dtDB_Sys_User = null;
				DataTable dtDB_Mst_NNT = null;
				{
					////
					Sys_User_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, objUserCode // objUserCode
						, TConst.Flag.Yes // strFlagExistToCheck
						, "" // strFlagActiveListToCheck
						, out dtDB_Sys_User // dtDB_Sys_User
						);
					//// Delete Sys_UserInGroup:
					Sys_UserInGroup_Delete_ByUser(
						strUserCode // strUserCode
						);
                    //// Delete User in Mst_UserMapInventory:
                    Mst_UserMapInventory_Delete_ByUser(
                        strUserCode // strUserCode
                        );
                    ////
                    Mst_NNT_CheckDB(
						ref alParamsCoupleError // alParamsCoupleError
						, dtDB_Sys_User.Rows[0]["MST"] // objMST
						, TConst.Flag.Yes // strFlagExistToCheck
						, TConst.Flag.Active // strFlagActiveListToCheck
						, "" // strTCTStatusListToCheck
						, out dtDB_Mst_NNT // dtDB_Mst_NNT
						);
				}
				#endregion

				#region // SaveDB UserCode:
				{
                    //// Xóa trong Quản lý thông báo
                    Mst_ManageNotify_DeleteX(
                        strTid
                        , strGwUserCode
                        , strGwPassword
                        , strWAUserCode
                        , strWAUserPassword
                        , ref alParamsCoupleError
                        , dtimeSys
                        , strUserCode
                        );

                    // Init:
                    dtDB_Sys_User.Rows[0].Delete();

					// Save:
					_cf.db.SaveData(
						"Sys_User"
						, dtDB_Sys_User
						);
				}
				#endregion

				#region // Inos_AccountService_GetUserX:
				////
				DataTable dtInos_Sys_User = null;
				{
					////
					DataSet dsGetData = null;

					Inos_AccountService_GetUserX(
						strTid // strTid
						, strGwUserCode // strGwUserCode
						, strGwPassword // strGwPassword
						, strWAUserCode // strWAUserCode
						, strWAUserPassword // strWAUserPassword
						, strAccessToken // strAccessToken
						, ref alParamsCoupleError // alParamsCoupleError
						, dtimeSys // dtimeSys
								   ////
						, strUserCode // objEMail
									  ////
						, out dsGetData // dsData
						);

					dtInos_Sys_User = dsGetData.Tables["Sys_User"].Copy();
					dtInos_Sys_User.TableName = "Sys_User";
				}
				#endregion

				#region // Inos_AccountService_GetUserX:
				////
				//{
				//	////
				//	DataSet dsGetData = null;

				//	Inos_OrgService_DeleteUserX(
				//		strTid // strTid
				//		, strGwUserCode // strGwUserCode
				//		, strGwPassword // strGwPassword
				//		, strWAUserCode // strWAUserCode
				//		, strWAUserPassword // strWAUserPassword
				//		, strAccessToken // strAccessToken
				//		, ref mdsFinal // mdsFinal
				//		, ref alParamsCoupleError // alParamsCoupleError
				//		, dtimeSys // dtimeSys
				//		////
				//		, dtDB_Mst_NNT.Rows[0]["OrgID"] // objOrgID
				//		, dtInos_Sys_User.Rows[0]["Id"] // objUserID
				//		////
				//		, out dsGetData // dsData
				//		);
				//}
				#endregion

				#region // Delete User
				{
					#region // Save Lic:
					{
						////
						DataTable dtDB_Mst_Param = null;
						string strOrgID = dtDB_Mst_NNT.Rows[0]["OrgID"].ToString();
						Mst_Param_CheckDB(
							ref alParamsCoupleError
							, TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERCODE
							, TConst.Flag.Yes
							, out dtDB_Mst_Param //dtDB_Mst_Param
							);

						string UserCode = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
						////
						Mst_Param_CheckDB(
							ref alParamsCoupleError
							, TConst.Mst_Param.PARAM_QINVOICEORDADMIN_USERPASSWORD
							, TConst.Flag.Yes
							, out dtDB_Mst_Param //dtDB_Mst_Param
							);

						string UserPassword = dtDB_Mst_Param.Rows[0]["ParamValue"].ToString();
						////
						object objAccessToken = null;
						////
						{
							Inos_AccountService_GetAccessTokenX(
								strTid // strTid
								, strGwUserCode // strGwUserCode
								, strGwPassword // strGwPassword
								, strWAUserCode // strWAUserCode
								, strWAUserPassword // strWAUserPassword
								, ref mdsFinal // mdsFinal
								, ref alParamsCoupleError // alParamsCoupleError
								, dtimeSys // dtimeSys
										   ////
								, UserCode // objEmail
								, UserPassword // objPassword
											   ////
								, out objAccessToken // objAccessToken
								);
						}

						////
						DataSet dsGetData = null;

						List<OrgLicense> lstOrgLicense = Inos_LicService_GetOrgLicenseX(
							 strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, strOrgID // objOrgID
									   ////
							, out dsGetData // dsData
							);
						////

						#region // Sys_Solution: Get.
						////
						DataTable dtDB_Sys_Solution = null;
						{
							// GetInfo:
							dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
								_cf.db // db
								, "Sys_Solution" // strTableName
								, "top 1 *" // strColumnList
								, "" // strClauseOrderBy
								, "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
								);
						}
						#endregion
						////
						DataSet dsData_Package = null;
						Inos_LicService_GetAllPackagesX_New20191113(
							strTid
							, strGwUserCode
							, strGwPassword
							, strWAUserCode
							, strWAUserPassword
							, (string)objAccessToken
							, ref mdsFinal
							, ref alParamsCoupleError
							, dtimeSys
							, dtDB_Sys_Solution.Rows[0]["SolutionCode"]
							, out dsData_Package
							);
						////
						List<OrgSolutionUser> lstOrgSolutionUser = new List<OrgSolutionUser>();

						for (int nScan = 0; nScan < dsData_Package.Tables[0].Rows.Count; nScan++)
						{
							DataRow drScan = dsData_Package.Tables[0].Rows[nScan];
							long PackageID = Convert.ToInt64(drScan["id"]);
							foreach (var item in lstOrgLicense)
							{
								if (item.PackageId == PackageID)
								{
									OrgSolutionUser obj = new OrgSolutionUser();
									obj.LicId = item.Id;
									obj.UserId = (long)dtInos_Sys_User.Rows[0]["id"];
									lstOrgSolutionUser.Add(obj);
								}
							}
						}

						DataSet dsData = null;
						Int32 result = Inos_LicService_DeleteOrgSolutionUsersX(
							 strTid // strTid
							, strGwUserCode // strGwUserCode
							, strGwPassword // strGwPassword
							, strWAUserCode // strWAUserCode
							, strWAUserPassword // strWAUserPassword
							, (string)objAccessToken // strAccessToken
							, ref mdsFinal // mdsFinal
							, ref alParamsCoupleError // alParamsCoupleError
							, dtimeSys // dtimeSys
									   ////
							, dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
							, strOrgID // objOrgID
							, lstOrgSolutionUser // lstOrgSolutionUser
												 ////
							, out dsData
							);
					}
					#endregion
				}
				#endregion

				// Return Good:
				TDALUtils.DBUtils.CommitSafety(_cf.db);
				mdsFinal.AcceptChanges();
				return mdsFinal;
			}
			catch (Exception exc)
			{
				#region // Catch of try:
				// Rollback:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);

				// Return Bad:
				return TUtils.CProcessExc.Process(
					ref mdsFinal
					, exc
					, strErrorCodeDefault
					, alParamsCoupleError.ToArray()
					);
				#endregion
			}
			finally
			{
				#region // Finally of try:
				// Rollback and Release resources:
				TDALUtils.DBUtils.RollbackSafety(_cf.db);
				TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

				// Write ReturnLog:
				_cf.ProcessBizReturn_OutSide(
					ref mdsFinal // mdsFinal
					, strTid // strTid
					, strGwUserCode // strGwUserCode
					, strGwPassword // strGwPassword
					, strWAUserCode // objUserCode
					, strFunctionName // strFunctionName
					);
				#endregion
			}
		}
		public DataSet WAS_Sys_User_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                //List<Sys_User> lst_Sys_User = new List<Sys_User>();
                //List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                #endregion

                #region // Sys_User_Create:
                mdsResult = Sys_User_Delete(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken // strAccessToken
                    , objRQ_Sys_User.NetworkID // strAccessToken
                    , objRQ_Sys_User.OrgID // strAccessToken
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_User.Sys_User.UserCode // objUserCode
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WebAPI_Sys_User_GetX(
            string strTid
            //, DataRow drSession
            , string strUserCode
            , string strUserPassword
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();



                // Check Access/Deny:
                //Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                //myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

                //myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Bank mb --//[mylock]
								on su.BankCode = mb.BankCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode
								, su.UserNick
                                , su.BankCode                             
								, su.UserName
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                                , su.FlagDLAdmin
								, su.FlagSysAdmin                       
								, su.FlagActive
								, mb.BankCode mb_BankCode
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Bank mb --//[mylock]
									on su.BankCode = mb.BankCode
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.BankCode su_BankCode
								, su.UserName su_UserName
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Bank" // strTableNameDB
                            , "Mst_Bank." // strPrefixStd
                            , "mb." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                //// Write ReturnLog:
                //_cf.ProcessBizReturn_OutSide(
                //	ref mdsFinal // mdsFinal
                //	, strTid // strTid
                //	, strGwUserCode // strGwUserCode
                //	, strGwPassword // strGwPassword
                //	, strUserCode // objUserCode
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WS_Sys_User_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strUserCode
            , string strUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = null;
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			////// Filter
			//		, "strFt_RecordStart", strFt_RecordStart
			//		, "strFt_RecordCount", strFt_RecordCount
			//		, "strFt_WhereClause", strFt_WhereClause
			////// Return
			//		, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
			//		, "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
					});
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strUserCode
                    , strFunctionName
                    );
                #endregion

                #region // WS_Sys_User_GetX:
                WS_Sys_User_GetX(
                    strTid // strTid
                    , dtimeSys // dtimeSys
                    , ref alParamsCoupleError
                    //// Filter:
                    , strFt_RecordStart // strFt_RecordStart
                    , strFt_RecordCount // strFt_RecordCount
                    , strFt_WhereClause // strFt_WhereClause
                                        //// Return:
                    , strRt_Cols_Sys_User // strRt_Cols_Sys_User
                    , strRt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                                                 ////
                    , out mdsFinal // mdsFinal
                    );
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void WS_Sys_User_GetX(
            string strTid
            , DateTime dtimeSys
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            ////
            , out DataSet mdsFinal
            )
        {
            #region // Temp:
            mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            string strFunctionName = "WS_Sys_User_GetX";
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                });
            #endregion

            #region // Init:
            //_cf.db.LogUserId = _cf.sinf.strUserCode;
            //if (bNeedTransaction) _cf.db.BeginTransaction();

            //// Write RequestLog:
            //_cf.ProcessBizReq(
            //	strTid // strTid
            //	, strFunctionName // strFunctionName
            //	, alParamsCoupleError // alParamsCoupleError
            //	);

            //// Check Access/Deny:
            //Sys_Access_CheckDeny(
            //	ref alParamsCoupleError
            //	, strFunctionName
            //	);
            #endregion

            #region // Check:
            // Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
            bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ArrayList alParamsCoupleSql = new ArrayList();
            //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
            alParamsCoupleSql.AddRange(new object[] {
                "@nFilterRecordStart", nFilterRecordStart
                , "@nFilterRecordEnd", nFilterRecordEnd
                , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                });
            ////
            //myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

            //myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
					---- #tbl_Sys_User_Filter_Draft:
					--select distinct
						identity(bigint, 0, 1) MyIdxSeq
						, su.UserCode
					into #tbl_Sys_User_Filter_Draft
					from Sys_User su --//[mylock]
						left join Sys_UserInGroup suig --//[mylock]
							on su.UserCode = suig.UserCode
						left join Sys_Group sg --//[mylock]
							on suig.GroupCode = sg.GroupCode
						left join Mst_Bank mb --//[mylock]
							on su.BankCode = mb.BankCode
					where (1=1)
						zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
						zzzzClauseWhere_strFilterWhereClause
					order by su.UserCode asc
					;

					---- Summary:
					select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
					;

					---- #tbl_Sys_User_Filter:
					select
						t.*
					into #tbl_Sys_User_Filter
					from #tbl_Sys_User_Filter_Draft t --//[mylock]
					where
						(t.MyIdxSeq >= @nFilterRecordStart)
						and (t.MyIdxSeq <= @nFilterRecordEnd)
					;

					-------- Sys_User --------:
					zzzzClauseSelect_Sys_User_zOut
					----------------------------------------

					-------- Sys_UserInGroup --------:
					zzzzClauseSelect_Sys_UserInGroup_zOut
					----------------------------------------

					---- Clear for debug:
					--drop table #tbl_Sys_User_Filter_Draft;
					--drop table #tbl_Sys_User_Filter;
				"
                , "zzzzClauseWhere_FilterAbilityOfUser", ""
                );
            ////
            string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
            if (bGet_Sys_User)
            {
                #region // bGet_Sys_User:
                zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
						---- Sys_User:
						select
							t.MyIdxSeq
							, su.UserCode
							, su.UserNick
							, su.BankCode                             
							, su.UserName
							, 'zzzzClausePVal_Default_PasswordMask' UserPassword
                            , su.FlagDLAdmin
							, su.FlagSysAdmin                       
							, su.FlagActive
							, mb.BankCode mb_BankCode
						from #tbl_Sys_User_Filter t --//[mylock]
							inner join Sys_User su --//[mylock]
								on t.UserCode = su.UserCode
							left join Mst_Bank mb --//[mylock]
								on su.BankCode = mb.BankCode
						order by t.MyIdxSeq asc
						;
					"
                    , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                    );
                #endregion
            }
            ////
            string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
            if (bGet_Sys_UserInGroup)
            {
                #region // bGet_Sys_UserInGroup:
                zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
						---- Sys_UserInGroup:
						select
							t.MyIdxSeq
							, suig.*
							, su.UserCode su_UserCode
							, su.BankCode su_BankCode
							, su.UserName su_UserName 
							, su.FlagSysAdmin su_FlagSysAdmin 
							, su.FlagActive su_FlagActive 
							, sg.GroupCode sg_GroupCode
							, sg.GroupName sg_GroupName 
							, sg.FlagActive sg_FlagActive 
						from #tbl_Sys_User_Filter t --//[mylock]
							inner join Sys_UserInGroup suig --//[mylock]
								on t.UserCode = suig.UserCode
							left join Sys_User su --//[mylock]
								on suig.UserCode = su.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
						order by t.MyIdxSeq asc
						;
					"
                    );
                #endregion
            }
            ////
            string zzzzClauseWhere_strFilterWhereClause = "";
            {
                Hashtable htSpCols = new Hashtable();
                {
                    #region // htSpCols:
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Sys_User" // strTableNameDB
                        , "Sys_User." // strPrefixStd
                        , "su." // strPrefixAlias
                        );
                    htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Sys_UserInGroup" // strTableNameDB
                        , "Sys_UserInGroup." // strPrefixStd
                        , "suig." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Sys_Group" // strTableNameDB
                        , "Sys_Group." // strPrefixStd
                        , "sg." // strPrefixAlias
                        );
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Mst_Bank" // strTableNameDB
                        , "Mst_Bank." // strPrefixStd
                        , "mb." // strPrefixAlias
                        );
                    ////
                    #endregion
                }
                zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                    htSpCols // htSpCols
                    , strFt_WhereClause // strClause
                    , "@p_" // strParamPrefix
                    , ref alParamsCoupleSql // alParamsCoupleSql
                    );
                zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                alParamsCoupleError.AddRange(new object[]{
                    "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    });
            }
            ////
            strSqlGetData = CmUtils.StringUtils.Replace(
                strSqlGetData
                , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                );
            #endregion

            #region // Get Data:
            DataSet dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Sys_User)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
            }
            if (bGet_Sys_UserInGroup)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
            }
            CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
            #endregion
        }

        public RT_Sys_User WA_Sys_User_Get(
            RQ_Sys_User objRQ_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            RT_Sys_User objRT_Sys_User = new RT_Sys_User();
            objRT_Sys_User.c_K_DT_Sys = new c_K_DT_Sys();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WA_Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WA_Sys_User_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                "strFunctionName", strFunctionName
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                objRT_Sys_User.Lst_Sys_User = new List<Sys_User>();
                objRT_Sys_User.c_K_DT_Sys = new c_K_DT_Sys();
                #endregion

                #region // WS_Sys_User_Get:
                mdsExec = WS_Sys_User_Get(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                                                ////
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserCode // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_User.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_User.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_User.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_Sys_User.Rt_Cols_Sys_User // strRt_Cols_Sys_User
                    , objRQ_Sys_User.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                ////
                if (CmUtils.CMyDataSet.HasError(mdsExec))
                {
                    ////
                    TUtils.CUtils.ProcessMyDSError(ref mdsResult, mdsExec);

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.WA_Sys_User_Get
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                #endregion

                #region // GetData:
                {
                    ////
                    DataTable dt_Sys_User = mdsExec.Tables["Sys_User"].Copy();
                    List<Sys_User> lst_Sys_User = new List<Sys_User>();
                    lst_Sys_User = TUtils.DataTableCmUtils.ToListof<Sys_User>(dt_Sys_User);
                    objRT_Sys_User.Lst_Sys_User = lst_Sys_User;
                }
                #endregion

                // Return Good:
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsResult);
                return objRT_Sys_User;
            }
            catch (Exception exc)
            {
                ////
                TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                // Return Bad:
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsResult);
                return objRT_Sys_User;
            }
            finally
            {
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
            }
        }

        public DataSet WS_Sys_User_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WS_Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WA_Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = WS_Sys_User_Get(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserCode // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_User.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_User.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_User.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_Sys_User.Rt_Cols_Sys_User // strRt_Cols_Sys_User
                    , objRQ_Sys_User.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                #endregion

                return mdsResult;
            }
            catch (Exception exc)
            {
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
            }
            finally
            {
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
            }
        }

        public DataSet WS_Mix_Sys_User_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WS_Sys_User_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WA_Sys_User_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_User", TJson.JsonConvert.SerializeObject(objRQ_Sys_User.Sys_User)
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = WS_Sys_User_Get(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserCode // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_User.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_User.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_User.Ft_WhereClause // strFt_WhereClause
                                                    //// Return:
                    , objRQ_Sys_User.Rt_Cols_Sys_User // strRt_Cols_Sys_User
                    , objRQ_Sys_User.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                #endregion

                #region // GetData:
                ////
                DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                List<Sys_User> lst_Sys_User = new List<Sys_User>();
                lst_Sys_User = TUtils.DataTableCmUtils.ToListof<Sys_User>(dt_Sys_User);
                objRT_Sys_User.Lst_Sys_User = lst_Sys_User;
                objRT_Sys_User.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsResult);
                #endregion

                return mdsResult;
            }
            catch (Exception exc)
            {
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
            }
            finally
            {
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
            }
        }

        public DataSet WAS_Sys_User_GetForUserMapInv(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_User objRQ_Sys_User
            ////
            , out RT_Sys_User objRT_Sys_User
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_User.Tid;
            objRT_Sys_User = new RT_Sys_User();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_User.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_User_GetForUserMapInv";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_User_GetForUserMapInv;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_User> lst_Sys_User = new List<Sys_User>();
                List<Sys_UserInGroup> lst_Sys_UserInGroup = new List<Sys_UserInGroup>();
                bool bGet_Sys_User = (objRQ_Sys_User.Rt_Cols_Sys_User != null && objRQ_Sys_User.Rt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (objRQ_Sys_User.Rt_Cols_Sys_UserInGroup != null && objRQ_Sys_User.Rt_Cols_Sys_UserInGroup.Length > 0);
                #endregion

                #region // WS_Sys_User_GetForUserMapInv:
                mdsResult = Sys_User_GetForUserMapInv(
                    objRQ_Sys_User.Tid // strTid
                    , objRQ_Sys_User.GwUserCode // strGwUserCode
                    , objRQ_Sys_User.GwPassword // strGwPassword
                    , objRQ_Sys_User.WAUserCode // strUserCode
                    , objRQ_Sys_User.WAUserPassword // strUserPassword
                    , objRQ_Sys_User.AccessToken
                    , objRQ_Sys_User.NetworkID
                    , objRQ_Sys_User.OrgID
                    , TUtils.CUtils.StdFlag(objRQ_Sys_User.FlagIsEndUser) // objFlagIsEndUser
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_User.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_User.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_User.Ft_WhereClause // strFt_WhereClause
                                             //// Return:
                    , objRQ_Sys_User.Rt_Cols_Sys_User // strRt_Cols_Sys_User
                    , objRQ_Sys_User.Rt_Cols_Sys_UserInGroup // strRt_Cols_Sys_UserInGroup
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_User.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_User)
                    {
                        ////
                        DataTable dt_Sys_User = mdsResult.Tables["Sys_User"].Copy();
                        lst_Sys_User = TUtils.DataTableCmUtils.ToListof<Sys_User>(dt_Sys_User);
                        objRT_Sys_User.Lst_Sys_User = lst_Sys_User;
                    }
                    ////
                    if (bGet_Sys_UserInGroup)
                    {
                        DataTable dt_Sys_UserInGroup = mdsResult.Tables["Sys_UserInGroup"].Copy();
                        lst_Sys_UserInGroup = TUtils.DataTableCmUtils.ToListof<Sys_UserInGroup>(dt_Sys_UserInGroup);
                        objRT_Sys_User.Lst_Sys_UserInGroup = lst_Sys_UserInGroup;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_User_GetForUserMapInv(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , string strNetworkID
            , string strOrgID
            , string strFlagIsEndUser
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_User
            , string strRt_Cols_Sys_UserInGroup
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_User_GetForUserMapInv";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_User_GetForUserMapInv;
            alParamsCoupleError.AddRange(new object[]{
                    "strFunctionName", strFunctionName
                    , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_User", strRt_Cols_Sys_User
                    , "strRt_Cols_Sys_UserInGroup", strRt_Cols_Sys_UserInGroup
                    });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                bool bFlagIsEndUser = CmUtils.StringUtils.StringEqual(strFlagIsEndUser, TConst.Flag.Yes);
                if (!bFlagIsEndUser)
                {
                    Sys_User_CheckAuthorize(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                                        //, strWAUserPassword // strWAUserPassword
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                        , strAccessToken // strAccessToken
                        , strNetworkID // strNetworkID
                        , strOrgID // strOrgID
                        , TConst.Flag.Active // strFlagUserCodeToCheck
                        );
                }

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_User = (strRt_Cols_Sys_User != null && strRt_Cols_Sys_User.Length > 0);
                bool bGet_Sys_UserInGroup = (strRt_Cols_Sys_UserInGroup != null && strRt_Cols_Sys_UserInGroup.Length > 0);
                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(_cf.sinf.strUserCode);

                #endregion

                #region // Build Sql:
                ////		
                // drAbilityOfUser:
                DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                zzzzClauseSelect_Mst_NNT_ViewAbility_Get(
                    drAbilityOfUser // drAbilityOfUser
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfUser", drAbilityOfUser["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                ////
                //myCache_Mst_Distributor_ViewAbility_Get(drAbilityOfUser);

                //myCache_Mst_AreaMarket_ViewAbility_Get(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_User_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, su.UserCode
						into #tbl_Sys_User_Filter_Draft
						from Sys_User su --//[mylock]
                            inner join #tbl_Mst_NNT_ViewAbility t_MstNNT_View --//[mylock]
                                on su.MST = t_MstNNT_View.MST
							left join Sys_UserInGroup suig --//[mylock]
								on su.UserCode = suig.UserCode
							left join Sys_Group sg --//[mylock]
								on suig.GroupCode = sg.GroupCode
							left join Mst_Department mdept --//[mylock]
								on su.DepartmentCode = mdept.DepartmentCode
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfUser
							zzzzClauseWhere_strFilterWhereClause
                            and su.OrgID = @strOrgID
						order by su.UserCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_User_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_User_Filter:
						select
							t.*
						into #tbl_Sys_User_Filter
						from #tbl_Sys_User_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_User --------:
						zzzzClauseSelect_Sys_User_zOut
						----------------------------------------

						-------- Sys_UserInGroup --------:
						zzzzClauseSelect_Sys_UserInGroup_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_User_Filter_Draft;
						--drop table #tbl_Sys_User_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_User_zOut = "-- Nothing.";
                if (bGet_Sys_User)
                {
                    #region // bGet_Sys_User:
                    zzzzClauseSelect_Sys_User_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_User:
							select
								t.MyIdxSeq
								, su.UserCode                         
								, su.UserName                      
								, su.MST
								, 'zzzzClausePVal_Default_PasswordMask' UserPassword
								, su.PhoneNo
								, su.EMail
								, su.DepartmentCode
								, su.Position
                                , su.FlagDLAdmin 
								, su.FlagSysAdmin 
								, su.FlagNNTAdmin                         
								, su.FlagActive
                                , su.ACId
                                , su.ACAvatar
                                , su.ACEmail
                                , su.ACLanguage
                                , su.ACName
                                , su.ACPhone
                                , su.ACTimeZone
                                , su.CustomerCodeSys
                                , mc.CustomerCode
                                , mc.CustomerName
								, mdept.DepartmentCode mdept_DepartmentCode
								, mdept.DepartmentName mdept_DepartmentName
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_User su --//[mylock]
									on t.UserCode = su.UserCode
								left join Mst_Department mdept --//[mylock]
									on su.DepartmentCode = mdept.DepartmentCode
                                left join Mst_Customer mc --//[mylock]
                                    on su.CustomerCodeSys = mc.CustomerCodeSys
							order by t.MyIdxSeq asc
							;
						"
                        , "zzzzClausePVal_Default_PasswordMask", TConst.BizMix.Default_PasswordMask
                        );
                    #endregion
                }
                ////
                string zzzzClauseSelect_Sys_UserInGroup_zOut = "-- Nothing.";
                if (bGet_Sys_UserInGroup)
                {
                    #region // bGet_Sys_UserInGroup:
                    zzzzClauseSelect_Sys_UserInGroup_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_UserInGroup:
							select
								t.MyIdxSeq
								, suig.*
								, su.UserCode su_UserCode
								, su.UserName su_UserName 
								, su.FlagSysAdmin su_FlagSysAdmin 
								, su.FlagActive su_FlagActive 
								, sg.GroupCode sg_GroupCode
								, sg.GroupName sg_GroupName 
								, sg.FlagActive sg_FlagActive 
							from #tbl_Sys_User_Filter t --//[mylock]
								inner join Sys_UserInGroup suig --//[mylock]
									on t.UserCode = suig.UserCode
								left join Sys_User su --//[mylock]
									on suig.UserCode = su.UserCode
								left join Sys_Group sg --//[mylock]
									on suig.GroupCode = sg.GroupCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_User" // strTableNameDB
                            , "Sys_User." // strPrefixStd
                            , "su." // strPrefixAlias
                            );
                        htSpCols.Remove("Sys_User.UserPassword".ToUpper());
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_UserInGroup" // strTableNameDB
                            , "Sys_UserInGroup." // strPrefixStd
                            , "suig." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Group" // strTableNameDB
                            , "Sys_Group." // strPrefixStd
                            , "sg." // strPrefixAlias
                            );
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Mst_Department" // strTableNameDB
                            , "Mst_Department." // strPrefixStd
                            , "mdept." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_User_zOut", zzzzClauseSelect_Sys_User_zOut
                    , "zzzzClauseSelect_Sys_UserInGroup_zOut", zzzzClauseSelect_Sys_UserInGroup_zOut
                    , "@strOrgID", strOrgID
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_User)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_User";
                }
                if (bGet_Sys_UserInGroup)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_UserInGroup";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        #endregion

        #region // Sys_UserInGroup:
        private void Sys_UserInGroup_Delete_ByUser(
            object strUserCode
            )
        {
            string strSql_Exec = CmUtils.StringUtils.Replace(@"
					delete t
					from Sys_UserInGroup t --//[mylock]
					where (1=1)
						and t.UserCode = @strUserCode
					;
				");
            DataSet dsDB_Check = _cf.db.ExecQuery(
                strSql_Exec
                , "@strUserCode", strUserCode
                );
        }
        private void Sys_UserInGroup_Delete_ByGroup(
            object strGroupCode
            )
        {
            string strSql_Exec = CmUtils.StringUtils.Replace(@"
					delete t
					from Sys_UserInGroup t --//[mylock]
					where (1=1)
						and t.GroupCode = @strGroupCode
					;
				");
            DataSet dsDB_Check = _cf.db.ExecQuery(
                strSql_Exec
                , "@strGroupCode", strGroupCode
                );
        }

        public DataSet Sys_UserInGroup_Save(
            string strTid
            , DataRow drSession
            ////
            , object objGroupCode
            , object[] arrobjDSData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_UserInGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_UserInGroup_Save;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objGroupCode", objGroupCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                if (dsData == null) dsData = new DataSet("dsData");
                dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                    });
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                ////
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Group.Rows[0]["LogLUBy"] = _cf.sinf.strUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_Sys_UserInGroup = null;
                ////
                {
                    ////
                    string strTableCheck = "Sys_UserInGroup";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_UserInGroup_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Sys_UserInGroup = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Sys_UserInGroup // dtData
                        , "StdParam", "UserCode" // arrstrCouple
                        );
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_UserInGroup" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "UserCode" } // arrSingleStructure
                        , dtInput_Sys_UserInGroup // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_UserInGroup:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from Sys_UserInGroup t --//[mylock]
							inner join #tbl_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into Sys_UserInGroup(
							GroupCode
							, UserCode
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_suig.UserCode
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_Sys_Group t_sg --//[mylock]
							inner join #tbl_Sys_UserInGroup t_suig --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_UserInGroup_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objGroupCode
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_UserInGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_UserInGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objGroupCode", objGroupCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                //if (dsData == null) dsData = new DataSet("dsData");
                //dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
                ////
                DataTable dtDB_Sys_Group = null;
                ////
                {
                    ////
                    Sys_Group_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objGroupCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Group // dtDB_Sys_Group
                        );
                    ////
                    dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Group.Rows[0]["LogLUBy"] = strWAUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Group" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "GroupCode", "NetworkID", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Group // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_Sys_UserInGroup = null;
                ////
                {
                    ////
                    string strTableCheck = "Sys_UserInGroup";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_UserInGroup_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Sys_UserInGroup = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Sys_UserInGroup // dtData
                        , "StdParam", "UserCode" // arrstrCouple
                        );
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_UserInGroup" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "UserCode" } // arrSingleStructure
                        , dtInput_Sys_UserInGroup // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_UserInGroup:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from Sys_UserInGroup t --//[mylock]
							inner join #tbl_Sys_Group t_sg --//[mylock]
								on t.GroupCode = t_sg.GroupCode
						where (1=1)
						;

						---- Insert All:
						insert into Sys_UserInGroup(
							GroupCode
							, UserCode
							, NetworkID
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sg.GroupCode
							, t_suig.UserCode
							, t_sg.NetworkID
							, t_sg.LogLUDTimeUTC
							, t_sg.LogLUBy
						from #tbl_Sys_Group t_sg --//[mylock]
							inner join #tbl_Sys_UserInGroup t_suig --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        //    public DataSet Sys_UserInGroup_Save_New20191102(
        //        string strTid
        //        , string strGwUserCode
        //        , string strGwPassword
        //        , string strWAUserCode
        //        , string strWAUserPassword
        //        , ref ArrayList alParamsCoupleError
        //        ////
        //        , object objGroupCode
        //        , DataSet dsData
        //        )
        //    {
        //        #region // Temp:
        //        DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
        //        //int nTidSeq = 0;
        //        DateTime dtimeSys = DateTime.UtcNow;
        //        string strFunctionName = "Sys_UserInGroup_Save";
        //        string strErrorCodeDefault = TError.ErridnInventory.Sys_UserInGroup_Save;
        //        alParamsCoupleError.AddRange(new object[]{
        //            "strFunctionName", strFunctionName
        //            , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
        //////
        //, "objGroupCode", objGroupCode
        //            });
        //        #endregion

        //        try
        //        {
        //            #region // Convert Input:
        //            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
        //            //if (dsData == null) dsData = new DataSet("dsData");
        //            //dsData.AcceptChanges();
        //            alParamsCoupleError.AddRange(new object[]{
        //                "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
        //                });
        //            #endregion

        //            #region // Init:
        //            //_cf.db.LogUserId = _cf.sinf.strUserCode;
        //            _cf.db.BeginTransaction();

        //            // Write RequestLog:
        //            _cf.ProcessBizReq_OutSide(
        //                strTid // strTid
        //                , strGwUserCode // strGwUserCode
        //                , strGwPassword // strGwPassword
        //                , strWAUserCode // objUserCode
        //                , strFunctionName // strFunctionName
        //                , alParamsCoupleError // alParamsCoupleError
        //                );

        //            // Sys_User_CheckAuthentication:
        //            Sys_User_CheckAuthentication(
        //                ref alParamsCoupleError
        //                , strWAUserCode
        //                , strWAUserPassword
        //                );

        //            // Check Access/Deny:
        //            Sys_Access_CheckDenyV30(
        //                ref alParamsCoupleError
        //                , strWAUserCode
        //                , strFunctionName
        //                );
        //            #endregion

        //            #region // Refine and Check Input Master:
        //            ////
        //            string strGroupCode = TUtils.CUtils.StdParam(objGroupCode);
        //            ////
        //            DataTable dtDB_Sys_Group = null;
        //            ////
        //            {
        //                ////
        //                Sys_Group_CheckDB(
        //                    ref alParamsCoupleError // alParamsCoupleError
        //                    , objGroupCode // objGroupCode
        //                    , TConst.Flag.Yes // strFlagExistToCheck
        //                    , "" // strFlagActiveListToCheck
        //                    , out dtDB_Sys_Group // dtDB_Sys_Group
        //                    );
        //                ////
        //                dtDB_Sys_Group.Rows[0]["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
        //                dtDB_Sys_Group.Rows[0]["LogLUBy"] = strWAUserCode;
        //                //// Upload:
        //                TUtils.CUtils.MyBuildDBDT_Common(
        //                    _cf.db // db
        //                    , "#tbl_Sys_Group" // strTableName
        //                    , TConst.BizMix.Default_DBColType // strDefaultType
        //                    , new object[] { "GroupCode", "NetworkID", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
        //                    , dtDB_Sys_Group // dtData
        //                    );
        //                ////
        //            }
        //            #endregion

        //            #region // Refine and Check Input Detail:
        //            ////
        //            DataTable dtInput_Sys_UserInGroup = null;
        //            ////
        //            {
        //                ////
        //                string strTableCheck = "Sys_UserInGroup";
        //                ////
        //                if (!dsData.Tables.Contains(strTableCheck))
        //                {
        //                    alParamsCoupleError.AddRange(new object[]{
        //                        "Check.TableName", strTableCheck
        //                        });
        //                    throw CmUtils.CMyException.Raise(
        //                        TError.ErridnInventory.Sys_UserInGroup_Save_InputTblDtlNotFound
        //                        , null
        //                        , alParamsCoupleError.ToArray()
        //                        );
        //                }
        //                dtInput_Sys_UserInGroup = dsData.Tables[strTableCheck];
        //                TUtils.CUtils.StdDataInTable(
        //                    dtInput_Sys_UserInGroup // dtData
        //                    , "StdParam", "UserCode" // arrstrCouple
        //                    );
        //                //// Upload:
        //                TUtils.CUtils.MyBuildDBDT_Common(
        //                    _cf.db // db
        //                    , "#tbl_Sys_UserInGroup" // strTableName
        //                    , TConst.BizMix.Default_DBColType // strDefaultType
        //                    , new object[] { "UserCode" } // arrSingleStructure
        //                    , dtInput_Sys_UserInGroup // dtData
        //                    );
        //                ////
        //            }
        //            #endregion

        //            #region // SaveDB Sys_UserInGroup:
        //            {
        //                string strSql_Exec = CmUtils.StringUtils.Replace(@"
        //		---- Clear All:
        //		delete t
        //		from Sys_UserInGroup t --//[mylock]
        //			inner join #tbl_Sys_Group t_sg --//[mylock]
        //				on t.GroupCode = t_sg.GroupCode
        //		where (1=1)
        //		;

        //		---- Insert All:
        //		insert into Sys_UserInGroup(
        //			GroupCode
        //			, UserCode
        //			, NetworkID
        //			, LogLUDTimeUTC
        //			, LogLUBy
        //			)
        //		select
        //			t_sg.GroupCode
        //			, t_suig.UserCode
        //			, t_sg.NetworkID
        //			, t_sg.LogLUDTimeUTC
        //			, t_sg.LogLUBy
        //		from #tbl_Sys_Group t_sg --//[mylock]
        //			inner join #tbl_Sys_UserInGroup t_suig --//[mylock]
        //				on (1=1)
        //		;
        //	");
        //                DataSet dsDB_Check = _cf.db.ExecQuery(
        //                    strSql_Exec
        //                    );
        //            }
        //            #endregion

        //            // Return Good:
        //            TDALUtils.DBUtils.CommitSafety(_cf.db);
        //            mdsFinal.AcceptChanges();
        //            return mdsFinal;
        //        }
        //        catch (Exception exc)
        //        {
        //            #region // Catch of try:
        //            // Rollback:
        //            TDALUtils.DBUtils.RollbackSafety(_cf.db);

        //            // Return Bad:
        //            return TUtils.CProcessExc.Process(
        //                ref mdsFinal
        //                , exc
        //                , strErrorCodeDefault
        //                , alParamsCoupleError.ToArray()
        //                );
        //            #endregion
        //        }
        //        finally
        //        {
        //            #region // Finally of try:
        //            // Rollback and Release resources:
        //            TDALUtils.DBUtils.RollbackSafety(_cf.db);
        //            TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

        //            // Write ReturnLog:
        //            _cf.ProcessBizReturn_OutSide(
        //                ref mdsFinal // mdsFinal
        //                , strTid // strTid
        //                , strGwUserCode // strGwUserCode
        //                , strGwPassword // strGwPassword
        //                , strWAUserCode // objUserCode
        //                , strFunctionName // strFunctionName
        //                );
        //            #endregion
        //        }
        //    }

        public DataSet WAS_Sys_UserInGroup_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_UserInGroup objRQ_Sys_UserInGroup
            ////
            , out RT_Sys_UserInGroup objRT_Sys_UserInGroup
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_UserInGroup.Tid;
            objRT_Sys_UserInGroup = new RT_Sys_UserInGroup();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_UserInGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_UserInGroup_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_UserInGroup_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Group", TJson.JsonConvert.SerializeObject(objRQ_Sys_UserInGroup.Sys_Group)
                , "Lst_Sys_UserInGroup", TJson.JsonConvert.SerializeObject(objRQ_Sys_UserInGroup.Lst_Sys_UserInGroup)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Sys_UserInGroup = TUtils.DataTableCmUtils.ToDataTable<Sys_UserInGroup>(objRQ_Sys_UserInGroup.Lst_Sys_UserInGroup, "Sys_UserInGroup");
                    dsData.Tables.Add(dt_Sys_UserInGroup);
                }
                #endregion

                #region // Sys_UserInGroup_Save:
                mdsResult = Sys_UserInGroup_Save_New20191102(
                    objRQ_Sys_UserInGroup.Tid // strTid
                    , objRQ_Sys_UserInGroup.GwUserCode // strGwUserCode
                    , objRQ_Sys_UserInGroup.GwPassword // strGwPassword
                    , objRQ_Sys_UserInGroup.WAUserCode // strUserCode
                    , objRQ_Sys_UserInGroup.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_UserInGroup.Sys_Group.GroupCode // objGroupCode
                    , dsData // dsData
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        #endregion

        #region // Sys_Object:
        private void Sys_Object_CheckDB(
            ref ArrayList alParamsCoupleError
            , object strObjectCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Sys_Object
            )
        {
            // GetInfo:
            dtDB_Sys_Object = TDALUtils.DBUtils.GetTableContents(
                _cf.db // db
                , "Sys_Object" // strTableName
                , "top 1 *" // strColumnList
                , "" // strClauseOrderBy
                , "ObjectCode", "=", strObjectCode // arrobjParamsTriple item
                );

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Sys_Object.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ObjectCodeNotFound", strObjectCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Object_CheckDB_ObjectCodeNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Sys_Object.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ObjectCodeExist", strObjectCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Object_CheckDB_ObjectCodeExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Sys_Object.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.ObjectCode", strObjectCode
                    , "Check.FlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Sys_Object.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_Object_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }

        }
        public DataSet Sys_Object_Get(
            string strTid
            , DataRow drSession
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Object
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_Object_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Object_Get;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
			//// Filter
					, "strFt_RecordStart", strFt_RecordStart
                    , "strFt_RecordCount", strFt_RecordCount
                    , "strFt_WhereClause", strFt_WhereClause
			//// Return
					, "strRt_Cols_Sys_Object", strRt_Cols_Sys_Object
                    });
            #endregion

            try
            {
                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Object = (strRt_Cols_Sys_Object != null && strRt_Cols_Sys_Object.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = mySys_Object_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Object_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
                            , so.ObjectCode
						into #tbl_Sys_Object_Filter_Draft
						from Sys_Object so --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by so.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Object_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Object_Filter:
						select
							t.*
						into #tbl_Sys_Object_Filter
						from #tbl_Sys_Object_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Object --------:
						zzzzClauseSelect_Sys_Object_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Object_Filter_Draft;
						--drop table #tbl_Sys_Object_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Object_zOut = "-- Nothing.";
                if (bGet_Sys_Object)
                {
                    #region // bGet_Sys_Object:
                    zzzzClauseSelect_Sys_Object_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Object:
							select
								t.MyIdxSeq
								, so.*
							from #tbl_Sys_Object_Filter t --//[mylock]
								inner join Sys_Object so --//[mylock]
                                     on  t.ObjectCode = so.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Object" // strTableNameDB
                            , "Sys_Object." // strPrefixStd
                            , "so." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Object_zOut", zzzzClauseSelect_Sys_Object_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Object)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Object";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_Object_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Object
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Object_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Object_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Sys_Object", strRt_Cols_Sys_Object
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Check:
                // Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_Object = (strRt_Cols_Sys_Object != null && strRt_Cols_Sys_Object.Length > 0);

                // drAbilityOfAccess:
                //DataRow drAbilityOfAccess = mySys_Object_GetAbilityViewBankOfAccess(_cf.sinf.strAccessCode);

                #endregion

                #region // Build Sql:
                ArrayList alParamsCoupleSql = new ArrayList();
                //alParamsCoupleSql.AddRange(new object[] { "@strAbilityOfAccess", drAbilityOfAccess["MBBankBUPattern"] });
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    , "@Today", DateTime.Today.ToString("yyyy-MM-dd")
                    });
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Object_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
                            , so.ObjectCode
						into #tbl_Sys_Object_Filter_Draft
						from Sys_Object so --//[mylock]
						where (1=1)
							zzzzClauseWhere_FilterAbilityOfUser -- Filter the AbilityOfAccess
							zzzzClauseWhere_strFilterWhereClause
						order by so.ObjectCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Object_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Object_Filter:
						select
							t.*
						into #tbl_Sys_Object_Filter
						from #tbl_Sys_Object_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Object --------:
						zzzzClauseSelect_Sys_Object_zOut
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Object_Filter_Draft;
						--drop table #tbl_Sys_Object_Filter;
					"
                    , "zzzzClauseWhere_FilterAbilityOfUser", ""
                    );
                ////
                string zzzzClauseSelect_Sys_Object_zOut = "-- Nothing.";
                if (bGet_Sys_Object)
                {
                    #region // bGet_Sys_Object:
                    zzzzClauseSelect_Sys_Object_zOut = CmUtils.StringUtils.Replace(@"
							---- Sys_Object:
							select
								t.MyIdxSeq
								, so.*
							from #tbl_Sys_Object_Filter t --//[mylock]
								inner join Sys_Object so --//[mylock]
                                     on  t.ObjectCode = so.ObjectCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }

                ////
                string zzzzClauseWhere_strFilterWhereClause = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_Object" // strTableNameDB
                            , "Sys_Object." // strPrefixStd
                            , "so." // strPrefixAlias
                            );

                        ////
                        #endregion
                    }
                    zzzzClauseWhere_strFilterWhereClause = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzzzClauseWhere_strFilterWhereClause = (zzzzClauseWhere_strFilterWhereClause.Length <= 0 ? "" : string.Format(" and ({0})", zzzzClauseWhere_strFilterWhereClause));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzzzClauseWhere_strFilterWhereClause", zzzzClauseWhere_strFilterWhereClause
                    , "zzzzClauseSelect_Sys_Object_zOut", zzzzClauseSelect_Sys_Object_zOut
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_Object)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_Object";
                }

                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Object_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Object objRQ_Sys_Object
            ////
            , out RT_Sys_Object objRT_Sys_Object
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Object.Tid;
            objRT_Sys_Object = new RT_Sys_Object();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Object.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Object_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Object_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Object.WAUserCode
                //    , objRQ_Sys_Object.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Object> lst_Sys_Object = new List<Sys_Object>();
                #endregion

                #region // WS_Sys_User_Get:
                mdsResult = Sys_Object_Get(
                    objRQ_Sys_Object.Tid // strTid
                    , objRQ_Sys_Object.GwUserCode // strGwUserCode
                    , objRQ_Sys_Object.GwPassword // strGwPassword
                    , objRQ_Sys_Object.WAUserCode // strUserCode
                    , objRQ_Sys_Object.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              // // Filter:
                    , objRQ_Sys_Object.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Object.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Object.Ft_WhereClause // strFt_WhereClause
                                                      // // Return:
                    , objRQ_Sys_Object.Rt_Cols_Sys_Object // strRt_Cols_Sys_Object
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Object.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    DataTable dt_Sys_User = mdsResult.Tables["Sys_Object"].Copy();
                    lst_Sys_Object = TUtils.DataTableCmUtils.ToListof<Sys_Object>(dt_Sys_User);
                    objRT_Sys_Object.Lst_Sys_Object = lst_Sys_Object;
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        #endregion

        #region // Sys_Solution:
        private void Sys_Solution_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objSolutionCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Sys_Solution
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Sys_Solution t --//[mylock]
					where (1=1)
						and t.SolutionCode = @objSolutionCode
					;
				");
            dtDB_Sys_Solution = _cf.db.ExecQuery(
                strSqlExec
                , "@objSolutionCode", objSolutionCode
                ).Tables[0];
            dtDB_Sys_Solution.TableName = "Sys_Solution";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Sys_Solution.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.SolutionCode", objSolutionCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Solution_CheckDB_SolutionNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Sys_Solution.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.SolutionCode", objSolutionCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Solution_CheckDB_SolutionExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Sys_Solution.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                    "Check.SolutionCode", objSolutionCode
                    , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                    , "DB.FlagActive", dtDB_Sys_Solution.Rows[0]["FlagActive"]
                    });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_Solution_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet WAS_Sys_Solution_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Solution objRQ_Sys_Solution
            ////
            , out RT_Sys_Solution objRT_Sys_Solution
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Solution.Tid;
            objRT_Sys_Solution = new RT_Sys_Solution();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Solution.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Solution_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Solution_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
        });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Solution> lst_Sys_Solution = new List<Sys_Solution>();
                #endregion

                #region // WS_Sys_Solution_Get:
                mdsResult = Sys_Solution_Get(
                    objRQ_Sys_Solution.Tid // strTid
                    , objRQ_Sys_Solution.GwUserCode // strGwUserCode
                    , objRQ_Sys_Solution.GwPassword // strGwPassword
                    , objRQ_Sys_Solution.WAUserCode // strUserCode
                    , objRQ_Sys_Solution.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_Solution.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Solution.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Solution.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Sys_Solution.Rt_Cols_Sys_Solution // strRt_Cols_Sys_Solution
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Solution.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    DataTable dt_Sys_Solution = mdsResult.Tables["Sys_Solution"].Copy();
                    lst_Sys_Solution = TUtils.DataTableCmUtils.ToListof<Sys_Solution>(dt_Sys_Solution);
                    objRT_Sys_Solution.Lst_Sys_Solution = lst_Sys_Solution;
                    /////
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_Solution_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Solution objRQ_Sys_Solution
            ////
            , out RT_Sys_Solution objRT_Sys_Solution
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Solution.Tid;
            objRT_Sys_Solution = new RT_Sys_Solution();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Solution.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Solution_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Solution_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Solution> lst_Sys_Solution = new List<Sys_Solution>();
                #endregion

                #region // WS_Sys_Solution_Get:
                mdsResult = RptSv_Sys_Solution_Get(
                    objRQ_Sys_Solution.Tid // strTid
                    , objRQ_Sys_Solution.GwUserCode // strGwUserCode
                    , objRQ_Sys_Solution.GwPassword // strGwPassword
                    , objRQ_Sys_Solution.WAUserCode // strUserCode
                    , objRQ_Sys_Solution.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_Solution.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Solution.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Solution.Ft_WhereClause // strFt_WhereClause
                                                        //// Return:
                    , objRQ_Sys_Solution.Rt_Cols_Sys_Solution // strRt_Cols_Sys_Solution
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Solution.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    DataTable dt_Sys_Solution = mdsResult.Tables["Sys_Solution"].Copy();
                    lst_Sys_Solution = TUtils.DataTableCmUtils.ToListof<Sys_Solution>(dt_Sys_Solution);
                    objRT_Sys_Solution.Lst_Sys_Solution = lst_Sys_Solution;
                    /////
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet Sys_Solution_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Solution
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Solution_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Solution_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
               //// Filter
        	    , "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
               //// Return
        	    , "strRt_Cols_Sys_Solution", strRt_Cols_Sys_Solution
                        });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // RptSv_Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //                ref alParamsCoupleError
                //                , strWAUserCode
                //                , strWAUserPassword
                //                );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Sys_Solution_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Sys_Solution_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Sys_Solution // strRt_Cols_Sys_Solution
                                                  ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet RptSv_Sys_Solution_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Solution
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "RptSv_Sys_Solution_Get";
            string strErrorCodeDefault = TError.ErridnInventory.RptSv_Sys_Solution_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
               //// Filter
        	    , "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
               //// Return
        	    , "strRt_Cols_Sys_Solution", strRt_Cols_Sys_Solution
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // RptSv_Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // RptSv_Sys_Solution_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Sys_Solution_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Sys_Solution // strRt_Cols_Sys_Solution
                                                  ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Sys_Solution_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Solution
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Sys_Solution_GetX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Sys_Solution = (strRt_Cols_Sys_Solution != null && strRt_Cols_Sys_Solution.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
            ////
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
        	        ---- #tbl_Sys_Solution_Filter_Draft:
        	        select distinct
        		        identity(bigint, 0, 1) MyIdxSeq
        		        , ss.SolutionCode
        	        into #tbl_Sys_Solution_Filter_Draft
        	        from Sys_Solution ss --//[mylock]
        	        where (1=1)
        		        zzB_Where_strFilter_zzE
        	        order by ss.SolutionCode asc
        	        ;

        	        ---- Summary:
        	        select Count(0) MyCount from #tbl_Sys_Solution_Filter_Draft t --//[mylock]
        	        ;

        	        ---- #tbl_Sys_Solution_Filter:
        	        select
        		        t.*
        	        into #tbl_Sys_Solution_Filter
        	        from #tbl_Sys_Solution_Filter_Draft t --//[mylock]
        	        where (1=1)
        		        and (t.MyIdxSeq >= @nFilterRecordStart)
        		        and (t.MyIdxSeq <= @nFilterRecordEnd)
        	        ;

        	        -------- Sys_Solution -----:
        	        zzB_Select_Sys_Solution_zzE
        	        ------------------------

        	        ---- Clear for debug:
        	        --drop table #tbl_Sys_Solution_Filter_Draft;
        	        --drop table #tbl_Sys_Solution_Filter;
                "
                );
            ////
            string zzB_Select_Sys_Solution_zzE = "-- Nothing.";
            if (bGet_Sys_Solution)
            {
                #region // bGet_Sys_Solution:
                zzB_Select_Sys_Solution_zzE = CmUtils.StringUtils.Replace(@"
        	            ---- Sys_Solution:
        	            select
        		            t.MyIdxSeq
        		            , ss.*
        	            from #tbl_Sys_Solution_Filter t --//[mylock]
        		            inner join Sys_Solution ss --//[mylock]
        			            on t.SolutionCode = ss.SolutionCode
        	            order by t.MyIdxSeq asc
        	            ;
                    "
                    );
                #endregion
            }
            ////
            string zzB_Where_strFilter_zzE = "";
            {
                Hashtable htSpCols = new Hashtable();
                {
                    #region // htSpCols:
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Sys_Solution" // strTableNameDB
                        , "Sys_Solution." // strPrefixStd
                        , "ss." // strPrefixAlias
                        );
                    ////
                    #endregion
                }
                zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                    htSpCols // htSpCols
                    , strFt_WhereClause // strClause
                    , "@p_" // strParamPrefix
                    , ref alParamsCoupleSql // alParamsCoupleSql
                    );
                zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
                alParamsCoupleError.AddRange(new object[]{
                        "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                        });
            }
            ////
            strSqlGetData = CmUtils.StringUtils.Replace(
                strSqlGetData
                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                , "zzB_Select_Sys_Solution_zzE", zzB_Select_Sys_Solution_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Sys_Solution)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Sys_Solution";
            }
            #endregion
        }
        #endregion

        #region // Sys_Modules:
        private void Sys_Modules_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objModuleCode
            , string strFlagExistToCheck
            , string strFlagActiveListToCheck
            , out DataTable dtDB_Sys_Modules
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Sys_Modules t --//[mylock]
					where (1=1)
						and t.ModuleCode = @objModuleCode
					;
				");
            dtDB_Sys_Modules = _cf.db.ExecQuery(
                strSqlExec
                , "@objModuleCode", objModuleCode
                ).Tables[0];
            dtDB_Sys_Modules.TableName = "Sys_Modules";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Sys_Modules.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.ModuleCode", objModuleCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Modules_CheckDB_ModulesNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Sys_Modules.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                    "Check.ModuleCode", objModuleCode
                    });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_Modules_CheckDB_ModulesExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }

            // strFlagActiveListToCheck:
            if (strFlagActiveListToCheck.Length > 0 && !strFlagActiveListToCheck.Contains(Convert.ToString(dtDB_Sys_Modules.Rows[0]["FlagActive"])))
            {
                alParamsCoupleError.AddRange(new object[]{
                "Check.ModuleCode", objModuleCode
                , "Check.strFlagActiveListToCheck", strFlagActiveListToCheck
                , "DB.FlagActive", dtDB_Sys_Modules.Rows[0]["FlagActive"]
                });
                throw CmUtils.CMyException.Raise(
                    TError.ErridnInventory.Sys_Modules_CheckDB_FlagActiveNotMatched
                    , null
                    , alParamsCoupleError.ToArray()
                    );
            }
        }

        public DataSet Sys_Modules_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Modules
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Modules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Modules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
               //// Filter
        	    , "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
               //// Return
        	    , "strRt_Cols_Sys_Modules", strRt_Cols_Sys_Modules
                        });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                Sys_Access_CheckDenyV30(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Sys_Modules_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Sys_Modules_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Sys_Modules // strRt_Cols_Sys_Modules
                                                 ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Sys_Modules_GetX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Modules
            ////
            , out DataSet dsGetData
            )
        {
            #region // Temp:
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //DateTime dtimeSys = DateTime.UtcNow;
            //bool bNeedTransaction = true;
            string strFunctionName = "Sys_Modules_GetX";
            //string strErrorCodeDefault = TError.ErrHTCNM.Mnf_VIN_Get_QCX;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                    });
            #endregion

            #region // Check:
            //// Refine:
            long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
            long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
            bool bGet_Sys_Modules = (strRt_Cols_Sys_Modules != null && strRt_Cols_Sys_Modules.Length > 0);

            //// drAbilityOfUser:
            //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(_cf.sinf.strUserCode);

            #endregion

            #region // Build Sql:
            ////
            ArrayList alParamsCoupleSql = new ArrayList();
            alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
            ////
            //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
            ////
            string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Modules_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, sm.ModuleCode
						into #tbl_Sys_Modules_Filter_Draft
						from Sys_Modules sm --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by sm.ModuleCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_Modules_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_Modules_Filter:
						select
							t.*
						into #tbl_Sys_Modules_Filter
						from #tbl_Sys_Modules_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_Modules --------:
						zzB_Select_Sys_Modules_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_Modules_Filter_Draft;
						--drop table #tbl_Sys_Modules_Filter;
					"
                );
            ////
            string zzB_Select_Sys_Modules_zzE = "-- Nothing.";
            if (bGet_Sys_Modules)
            {
                #region // bGet_Sys_Modules:
                zzB_Select_Sys_Modules_zzE = CmUtils.StringUtils.Replace(@"
        	            ---- Sys_Modules:
						select
							t.MyIdxSeq
							, sm.*
                            , ss.SolutionCode ss_SolutionCode
                            , ss.SolutionName ss_SolutionName
						from #tbl_Sys_Modules_Filter t --//[mylock]
							inner join Sys_Modules sm --//[mylock]
								on t.ModuleCode = sm.ModuleCode
                            left join Sys_Solution ss --//[mylock]
                                on sm.SolutionCode = ss.SolutionCode 
						order by t.MyIdxSeq asc
						;
                    "
                    );
                #endregion
            }
            ////
            string zzB_Where_strFilter_zzE = "";
            {
                Hashtable htSpCols = new Hashtable();
                {
                    #region // htSpCols:
                    ////
                    TUtils.CUtils.MyBuildHTSupportedColumns(
                        _cf.db // db
                        , ref htSpCols // htSupportedColumns
                        , "Sys_Modules" // strTableNameDB
                        , "Sys_Modules." // strPrefixStd
                        , "sm." // strPrefixAlias
                        );
                    ////
                    #endregion
                }
                zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                    htSpCols // htSpCols
                    , strFt_WhereClause // strClause
                    , "@p_" // strParamPrefix
                    , ref alParamsCoupleSql // alParamsCoupleSql
                    );
                zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
                alParamsCoupleError.AddRange(new object[]{
                        "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                        });
            }
            ////
            strSqlGetData = CmUtils.StringUtils.Replace(
                strSqlGetData
                , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                , "zzB_Select_Sys_Modules_zzE", zzB_Select_Sys_Modules_zzE
                );
            #endregion

            #region // Get Data:
            dsGetData = _cf.db.ExecQuery(
                strSqlGetData
                , alParamsCoupleSql.ToArray()
                );
            int nIdxTable = 0;
            dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
            if (bGet_Sys_Modules)
            {
                dsGetData.Tables[nIdxTable++].TableName = "Sys_Modules";
            }
            #endregion
        }
        public DataSet Sys_Modules_Create(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objModuleCode
            , object objSolutionCode
            , object objModuleName
            , object objDescription
            , object objQtyInvoice
            , object objValCapacity
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Modules_Create";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Modules_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                ////
				, "objModuleCode", objModuleCode
                , "objSolutionCode", objSolutionCode
                , "objModuleName", objModuleName
                , "objDescription", objDescription
                , "objQtyInvoice", objQtyInvoice
                , "objValCapacity", objValCapacity
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strModuleCode = TUtils.CUtils.StdParam(objModuleCode);
                string strSolutionCode = TUtils.CUtils.StdParam(objSolutionCode);
                string strModuleName = string.Format("{0}", objModuleName).Trim();
                string strDescription = string.Format("{0}", objDescription).Trim();
                double dblQtyInvoice = Convert.ToDouble(objQtyInvoice);
                double dblValCapacity = Convert.ToDouble(objValCapacity);

                // drAbilityOfUser:
                //DataRow drAbilityOfUser = Sys_User_GetAbilityViewOfUser(strWAUserCode);
                ////
                DataTable dtDB_Sys_Modules = null;
                {
                    ////
                    if (strModuleCode == null || strModuleCode.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strModuleCode", strModuleCode
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Modules_Create_InvalidModuleCode
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    Sys_Modules_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strModuleCode // strModuleCode
                        , TConst.Flag.No // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Modules // dtDB_Sys_Modules
                        );
                    ////
                    DataTable dtDB_Sys_Solution = null;

                    Sys_Solution_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strSolutionCode // strSolutionCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Sys_Solution // dtDB_Sys_Solution
                        );
                    ////
                    if (strModuleName == null || strModuleName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strModuleName", strModuleName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Modules_Create_InvalidModuleName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // SaveDB Sys_Modules:
                {
                    // Init:
                    //ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Modules.NewRow();
                    strFN = "ModuleCode"; drDB[strFN] = strModuleCode;
                    strFN = "NetworkID"; drDB[strFN] = nNetworkID;
                    strFN = "SolutionCode"; drDB[strFN] = strSolutionCode;
                    strFN = "ModuleName"; drDB[strFN] = strModuleName;
                    strFN = "Description"; drDB[strFN] = strDescription;
                    strFN = "QtyInvoice"; drDB[strFN] = dblQtyInvoice;
                    strFN = "ValCapacity"; drDB[strFN] = dblValCapacity;
                    strFN = "FlagActive"; drDB[strFN] = TConst.Flag.Active;
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode;
                    dtDB_Sys_Modules.Rows.Add(drDB);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Modules"
                        , dtDB_Sys_Modules
                        //, alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_Modules_Update(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objModuleCode
            , object objSolutionCode
            , object objModuleName
            , object objDescription
            , object objQtyInvoice
            , object objValCapacity
            , object objFlagActive
            ////
            , object objFt_Cols_Upd
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Modules_Update";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Modules_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objModuleCode", objModuleCode
                , "objSolutionCode", objSolutionCode
                , "objModuleName", objModuleName
                , "objDescription", objDescription
                , "objQtyInvoice", objQtyInvoice
                , "objValCapacity", objValCapacity
                , "objFlagActive", objFlagActive
				////
				, "objFt_Cols_Upd", objFt_Cols_Upd
                });
            #endregion

            try
            {
                #region // Convert Input:
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strFt_Cols_Upd = TUtils.CUtils.StdParam(objFt_Cols_Upd);
                strFt_Cols_Upd = (strFt_Cols_Upd == null ? "" : strFt_Cols_Upd);
                ////
                string strModuleCode = TUtils.CUtils.StdParam(objModuleCode);
                string strSolutionCode = TUtils.CUtils.StdParam(objSolutionCode);
                string strModuleName = string.Format("{0}", objModuleName).Trim();
                string strDescription = string.Format("{0}", objDescription).Trim();
                double dblQtyInvoice = Convert.ToDouble(objQtyInvoice);
                double dblValCapacity = Convert.ToDouble(objValCapacity);
                string strFlagActive = TUtils.CUtils.StdFlag(objFlagActive);
                ////
                bool bUpd_ModuleName = strFt_Cols_Upd.Contains("Sys_Modules.ModuleName".ToUpper());
                bool bUpd_Description = strFt_Cols_Upd.Contains("Sys_Modules.Description".ToUpper());
                bool bUpd_QtyInvoice = strFt_Cols_Upd.Contains("Sys_Modules.QtyInvoice".ToUpper());
                bool bUpd_ValCapacity = strFt_Cols_Upd.Contains("Sys_Modules.ValCapacity".ToUpper());
                bool bUpd_FlagActive = strFt_Cols_Upd.Contains("Sys_Modules.FlagActive".ToUpper());

                ////
                DataTable dtDB_Sys_Modules = null;
                {
                    ////
                    Sys_Modules_CheckDB(
                         ref alParamsCoupleError // alParamsCoupleError
                         , strModuleCode // strModuleCode 
                         , TConst.Flag.Yes // strFlagExistToCheck
                         , "" // strFlagActiveListToCheck
                         , out dtDB_Sys_Modules // dtDB_Sys_Modules
                        );
                    ////
                    DataTable dtDB_Sys_Solution = null;
                    Sys_Solution_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strSolutionCode // strSolutionCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , TConst.Flag.Active // strFlagActiveListToCheck
                        , out dtDB_Sys_Solution // dtDB_Sys_Solution
                        );
                    ////
                    if (strModuleName == null || strModuleName.Length < 1)
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.strModuleName", strModuleName
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_Modules_Update_InvalidModuleName
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                }
                #endregion

                #region // SaveDB Sys_Modules:
                {
                    // Init:
                    ArrayList alColumnEffective = new ArrayList();
                    string strFN = "";
                    DataRow drDB = dtDB_Sys_Modules.Rows[0];
                    if (bUpd_ModuleName) { strFN = "ModuleName"; drDB[strFN] = strModuleName; alColumnEffective.Add(strFN); }
                    if (bUpd_Description) { strFN = "Description"; drDB[strFN] = strDescription; alColumnEffective.Add(strFN); }
                    if (bUpd_QtyInvoice) { strFN = "QtyInvoice"; drDB[strFN] = dblQtyInvoice; alColumnEffective.Add(strFN); }
                    if (bUpd_ValCapacity) { strFN = "ValCapacity"; drDB[strFN] = dblValCapacity; alColumnEffective.Add(strFN); }
                    if (bUpd_FlagActive) { strFN = "FlagActive"; drDB[strFN] = strFlagActive; alColumnEffective.Add(strFN); }
                    strFN = "LogLUDTimeUTC"; drDB[strFN] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"); alColumnEffective.Add(strFN);
                    strFN = "LogLUBy"; drDB[strFN] = strWAUserCode; alColumnEffective.Add(strFN);

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Modules"
                        , dtDB_Sys_Modules
                        , alColumnEffective.ToArray()
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        public DataSet Sys_Modules_Delete(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            /////
            , object objModuleCode
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Modules_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Modules_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objModuleCode", objModuleCode
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                #region // Refine and Check Input:
                ////
                string strTaxId = TUtils.CUtils.StdParam(objModuleCode);
                ////
                DataTable dtDB_Sys_Modules = null;
                {
                    ////
                    Sys_Modules_CheckDB(
                         ref alParamsCoupleError // alParamsCoupleError
                         , objModuleCode // objModuleCode
                         , TConst.Flag.Yes // strFlagExistToCheck
                         , "" // strFlagActiveListToCheck
                         , out dtDB_Sys_Modules // dtDB_Sys_Modules
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_Modules:
                {
                    // Init:
                    dtDB_Sys_Modules.Rows[0].Delete();

                    // Save:
                    _cf.db.SaveData(
                        "Sys_Modules"
                        , dtDB_Sys_Modules
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_Modules_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Modules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Modules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                bool bGet_Sys_Modules = (objRQ_Sys_Modules.Rt_Cols_Sys_Modules != null && objRQ_Sys_Modules.Rt_Cols_Sys_Modules.Length > 0);
                #endregion

                #region // WS_Sys_Modules_Get:
                mdsResult = Sys_Modules_Get(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_Modules.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Modules.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Modules.Ft_WhereClause // strFt_WhereClause
                                                       //// Return:
                    , objRQ_Sys_Modules.Rt_Cols_Sys_Modules // strRt_Cols_Sys_Modules
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Modules.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_Modules)
                    {
                        ////
                        DataTable dt_Sys_Modules = mdsResult.Tables["Sys_Modules"].Copy();
                        lst_Sys_Modules = TUtils.DataTableCmUtils.ToListof<Sys_Modules>(dt_Sys_Modules);
                        objRT_Sys_Modules.Lst_Sys_Modules = lst_Sys_Modules;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        public DataSet WAS_Sys_Modules_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Modules_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Modules_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules.Sys_Modules)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Modules.WAUserCode
                //    , objRQ_Sys_Modules.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                //List<Sys_ModulesInGroup> lst_Sys_ModulesInGroup = new List<Sys_ModulesInGroup>();
                #endregion

                #region // Sys_Modules_Create:
                mdsResult = Sys_Modules_Create(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Modules.Sys_Modules.ModuleCode // ModuleCode
                    , objRQ_Sys_Modules.Sys_Modules.SolutionCode // SolutionCode
                    , objRQ_Sys_Modules.Sys_Modules.ModuleName // ModuleName
                    , objRQ_Sys_Modules.Sys_Modules.Description // Description
                    , objRQ_Sys_Modules.Sys_Modules.QtyInvoice // QtyInvoice
                    , objRQ_Sys_Modules.Sys_Modules.ValCapacity // ValCapacity
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_Sys_Modules_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Modules_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Modules_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules.Sys_Modules)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Modules.WAUserCode
                //    , objRQ_Sys_Modules.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                //List<Sys_ModulesInGroup> lst_Sys_ModulesInGroup = new List<Sys_ModulesInGroup>();
                #endregion

                #region // Sys_Modules_Update:
                mdsResult = Sys_Modules_Update(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Modules.Sys_Modules.ModuleCode // ModuleCode
                    , objRQ_Sys_Modules.Sys_Modules.SolutionCode // SolutionCode
                    , objRQ_Sys_Modules.Sys_Modules.ModuleName // ModuleName
                    , objRQ_Sys_Modules.Sys_Modules.Description // Description
                    , objRQ_Sys_Modules.Sys_Modules.QtyInvoice // QtyInvoice
                    , objRQ_Sys_Modules.Sys_Modules.ValCapacity // ValCapacity
                    , objRQ_Sys_Modules.Sys_Modules.FlagActive // objFlagActive
                                                               ////
                    , objRQ_Sys_Modules.Ft_Cols_Upd// objFt_Cols_Upd
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_Sys_Modules_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Modules_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Modules_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules.Sys_Modules)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Modules.WAUserCode
                //    , objRQ_Sys_Modules.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                //List<Sys_ModulesInGroup> lst_Sys_ModulesInGroup = new List<Sys_ModulesInGroup>();
                #endregion

                #region // Sys_Modules_Delete:
                mdsResult = Sys_Modules_Delete(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Modules.Sys_Modules.ModuleCode // objTaxId
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_Modules_Create(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Modules_Create";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Modules_Create;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules.Sys_Modules)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Modules.WAUserCode
                //    , objRQ_Sys_Modules.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                //List<Sys_ModulesInGroup> lst_Sys_ModulesInGroup = new List<Sys_ModulesInGroup>();
                #endregion

                #region // Sys_Modules_Create:
                mdsResult = Sys_Modules_Create(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Modules.Sys_Modules.ModuleCode // ModuleCode
                    , objRQ_Sys_Modules.Sys_Modules.SolutionCode // SolutionCode
                    , objRQ_Sys_Modules.Sys_Modules.ModuleName // ModuleName
                    , objRQ_Sys_Modules.Sys_Modules.Description // Description
                    , objRQ_Sys_Modules.Sys_Modules.QtyInvoice // QtyInvoice
                    , objRQ_Sys_Modules.Sys_Modules.ValCapacity // ValCapacity
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_Modules_Update(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Modules_Update";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Modules_Update;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                , "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules.Sys_Modules)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Modules.WAUserCode
                //    , objRQ_Sys_Modules.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                //List<Sys_ModulesInGroup> lst_Sys_ModulesInGroup = new List<Sys_ModulesInGroup>();
                #endregion

                #region // Sys_Modules_Update:
                mdsResult = Sys_Modules_Update(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Modules.Sys_Modules.ModuleCode // ModuleCode
                    , objRQ_Sys_Modules.Sys_Modules.SolutionCode // SolutionCode
                    , objRQ_Sys_Modules.Sys_Modules.ModuleName // ModuleName
                    , objRQ_Sys_Modules.Sys_Modules.Description // Description
                    , objRQ_Sys_Modules.Sys_Modules.QtyInvoice // QtyInvoice
                    , objRQ_Sys_Modules.Sys_Modules.ValCapacity // ValCapacity
                    , objRQ_Sys_Modules.Sys_Modules.FlagActive // objFlagActive
                                                               ////
                    , objRQ_Sys_Modules.Ft_Cols_Upd// objFt_Cols_Upd
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_Modules_Delete(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_Modules_Delete";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_Modules_Delete;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_Modules.Sys_Modules)
				////
				});
            #endregion

            try
            {
                #region // Init:
                // Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , objRQ_Sys_Modules.WAUserCode
                //    , objRQ_Sys_Modules.WAUserPassword
                //    );
                #endregion

                #region // Refine and Check Input:
                //List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                //List<Sys_ModulesInGroup> lst_Sys_ModulesInGroup = new List<Sys_ModulesInGroup>();
                #endregion

                #region // Sys_Modules_Delete:
                mdsResult = Sys_Modules_Delete(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_Modules.Sys_Modules.ModuleCode // objTaxId
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_Modules_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_Modules objRQ_Sys_Modules
            ////
            , out RT_Sys_Modules objRT_Sys_Modules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_Modules.Tid;
            objRT_Sys_Modules = new RT_Sys_Modules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_Modules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_Modules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_Modules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_Modules> lst_Sys_Modules = new List<Sys_Modules>();
                bool bGet_Sys_Modules = (objRQ_Sys_Modules.Rt_Cols_Sys_Modules != null && objRQ_Sys_Modules.Rt_Cols_Sys_Modules.Length > 0);
                #endregion

                #region // WS_Sys_Modules_Get:
                mdsResult = RptSv_Sys_Modules_Get(
                    objRQ_Sys_Modules.Tid // strTid
                    , objRQ_Sys_Modules.GwUserCode // strGwUserCode
                    , objRQ_Sys_Modules.GwPassword // strGwPassword
                    , objRQ_Sys_Modules.WAUserCode // strUserCode
                    , objRQ_Sys_Modules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_Modules.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_Modules.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_Modules.Ft_WhereClause // strFt_WhereClause
                                                       //// Return:
                    , objRQ_Sys_Modules.Rt_Cols_Sys_Modules // strRt_Cols_Sys_Modules
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_Modules.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_Modules)
                    {
                        ////
                        DataTable dt_Sys_Modules = mdsResult.Tables["Sys_Modules"].Copy();
                        lst_Sys_Modules = TUtils.DataTableCmUtils.ToListof<Sys_Modules>(dt_Sys_Modules);
                        objRT_Sys_Modules.Lst_Sys_Modules = lst_Sys_Modules;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        public DataSet RptSv_Sys_Modules_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_Modules
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_Modules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_Modules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
               //// Filter
        	    , "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
               //// Return
        	    , "strRt_Cols_Sys_Modules", strRt_Cols_Sys_Modules
                        });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // RptSv_Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strWAUserPassword
                //    );

                // Check Access/Deny:
                //Sys_Access_CheckDenyV30(
                //    ref alParamsCoupleError
                //    , strWAUserCode
                //    , strFunctionName
                //    );
                #endregion

                #region // Sys_Modules_GetX:
                DataSet dsGetData = null;
                {
                    ////
                    Sys_Modules_GetX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , strFt_RecordStart // strFt_RecordStart
                        , strFt_RecordCount // strFt_RecordCount
                        , strFt_WhereClause // strFt_WhereClause
                                            ////
                        , strRt_Cols_Sys_Modules // strRt_Cols_Sys_Modules
                                                 ////
                        , out dsGetData // dsGetData
                        );
                    ////
                    CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }
        #endregion

        #region // Sys_ObjectInModules:
        private void Sys_ObjectInModules_Delete_ByObject(
            object strObjectCode
            )
        {
            string strSql_Exec = CmUtils.StringUtils.Replace(@"
					delete t
					from Sys_ObjectInModules t --//[mylock]
					where (1=1)
						and t.ObjectCode = @strObjectCode
					;
				");
            DataSet dsDB_Check = _cf.db.ExecQuery(
                strSql_Exec
                , "@strObjectCode", strObjectCode
                );
        }
        private void Sys_ObjectInModules_Delete_ByModules(
            object strModuleCode
            )
        {
            string strSql_Exec = CmUtils.StringUtils.Replace(@"
					delete t
					from Sys_ObjectInModules t --//[mylock]
					where (1=1)
						and t.ModuleCode = @strModuleCode
					;
				");
            DataSet dsDB_Check = _cf.db.ExecQuery(
                strSql_Exec
                , "@strModuleCode", strModuleCode
                );
        }
        public DataSet Sys_ObjectInModules_Get(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            //// Filter:
            , string strFt_RecordStart
            , string strFt_RecordCount
            , string strFt_WhereClause
            //// Return:
            , string strRt_Cols_Sys_ObjectInModules
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_ObjectInModules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_ObjectInModules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//// Filter
				, "strFt_RecordStart", strFt_RecordStart
                , "strFt_RecordCount", strFt_RecordCount
                , "strFt_WhereClause", strFt_WhereClause
				//// Return
				, "strRt_Cols_Sys_ObjectInModules", strRt_Cols_Sys_ObjectInModules
                });
            #endregion

            try
            {
                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Check:
                //// Refine:
                long nFilterRecordStart = Convert.ToInt64(strFt_RecordStart);
                long nFilterRecordEnd = nFilterRecordStart + Convert.ToInt64(strFt_RecordCount) - 1;
                bool bGet_Sys_ObjectInModules = (strRt_Cols_Sys_ObjectInModules != null && strRt_Cols_Sys_ObjectInModules.Length > 0);

                //// drAbilityOfUser:
                //DataRow drAbilityOfUser = myCache_ViewAbility_GetUserInfo(strWAUserCode);

                #endregion

                #region // Build Sql:
                ////
                ArrayList alParamsCoupleSql = new ArrayList();
                alParamsCoupleSql.AddRange(new object[] {
                    "@nFilterRecordStart", nFilterRecordStart
                    , "@nFilterRecordEnd", nFilterRecordEnd
                    });
                ////
                //myCache_ViewAbility_GetDealerInfo(drAbilityOfUser);
                ////
                string strSqlGetData = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_ObjectInModules_Filter_Draft:
						select distinct
							identity(bigint, 0, 1) MyIdxSeq
							, soim.ObjectCode
							, soim.ModuleCode
						into #tbl_Sys_ObjectInModules_Filter_Draft
						from Sys_ObjectInModules soim --//[mylock]
						where (1=1)
							zzB_Where_strFilter_zzE
						order by 
							soim.ObjectCode
							, soim.ModuleCode asc
						;

						---- Summary:
						select Count(0) MyCount from #tbl_Sys_ObjectInModules_Filter_Draft t --//[mylock]
						;

						---- #tbl_Sys_ObjectInModules_Filter:
						select
							t.*
						into #tbl_Sys_ObjectInModules_Filter
						from #tbl_Sys_ObjectInModules_Filter_Draft t --//[mylock]
						where
							(t.MyIdxSeq >= @nFilterRecordStart)
							and (t.MyIdxSeq <= @nFilterRecordEnd)
						;

						-------- Sys_ObjectInModules --------:
						zzB_Select_Sys_ObjectInModules_zzE
						----------------------------------------

						---- Clear for debug:
						--drop table #tbl_Sys_ObjectInModules_Filter_Draft;
						--drop table #tbl_Sys_ObjectInModules_Filter;
					"
                    );
                ////
                string zzB_Select_Sys_ObjectInModules_zzE = "-- Nothing.";
                if (bGet_Sys_ObjectInModules)
                {
                    #region // bGet_Sys_ObjectInModules:
                    zzB_Select_Sys_ObjectInModules_zzE = CmUtils.StringUtils.Replace(@"
							---- Sys_ObjectInModules:
							select distinct
								t.MyIdxSeq
								, soim.*
							from #tbl_Sys_ObjectInModules_Filter t --//[mylock]
								inner join Sys_ObjectInModules soim --//[mylock]
									on t.ObjectCode = soim.ObjectCode
										and t.ModuleCode = soim.ModuleCode
							order by t.MyIdxSeq asc
							;
						"
                        );
                    #endregion
                }
                ////
                string zzB_Where_strFilter_zzE = "";
                {
                    Hashtable htSpCols = new Hashtable();
                    {
                        #region // htSpCols:
                        ////
                        TUtils.CUtils.MyBuildHTSupportedColumns(
                            _cf.db // db
                            , ref htSpCols // htSupportedColumns
                            , "Sys_ObjectInModules" // strTableNameDB
                            , "Sys_ObjectInModules." // strPrefixStd
                            , "soim." // strPrefixAlias
                            );
                        ////
                        #endregion
                    }
                    zzB_Where_strFilter_zzE = CmUtils.SqlUtils.BuildWhere(
                        htSpCols // htSpCols
                        , strFt_WhereClause // strClause
                        , "@p_" // strParamPrefix
                        , ref alParamsCoupleSql // alParamsCoupleSql
                        );
                    zzB_Where_strFilter_zzE = (zzB_Where_strFilter_zzE.Length <= 0 ? "" : string.Format(" and ({0})", zzB_Where_strFilter_zzE));
                    alParamsCoupleError.AddRange(new object[]{
                        "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                        });
                }
                ////
                strSqlGetData = CmUtils.StringUtils.Replace(
                    strSqlGetData
                    , "zzB_Where_strFilter_zzE", zzB_Where_strFilter_zzE
                    , "zzB_Select_Sys_ObjectInModules_zzE", zzB_Select_Sys_ObjectInModules_zzE
                    );
                #endregion

                #region // Get Data:
                DataSet dsGetData = _cf.db.ExecQuery(
                    strSqlGetData
                    , alParamsCoupleSql.ToArray()
                    );
                int nIdxTable = 0;
                dsGetData.Tables[nIdxTable++].TableName = "MySummaryTable";
                if (bGet_Sys_ObjectInModules)
                {
                    dsGetData.Tables[nIdxTable++].TableName = "Sys_ObjectInModules";
                }
                CmUtils.DataUtils.MoveDataTable(ref mdsFinal, ref dsGetData);
                #endregion

                // Return Good:
                TDALUtils.DBUtils.RollbackSafety(_cf.db); // Always Rollback.
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_ObjectInModules_Save(
            string strTid
            , DataRow drSession
            ////
            , object objModuleCode
            , object[] arrobjDSData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            bool bNeedTransaction = true;
            string strFunctionName = "Sys_ObjectInModules_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_ObjectInModules_Save;
            ArrayList alParamsCoupleError = new ArrayList(new object[]{
                    "strFunctionName", strFunctionName
                    , "objModuleCode", objModuleCode
                    });
            #endregion

            try
            {
                #region // Convert Input:
                DateTime dtimeTDate = DateTime.UtcNow;
                DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                if (dsData == null) dsData = new DataSet("dsData");
                dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                    });
                #endregion

                #region // Init:
                _cf.db.LogUserId = _cf.sinf.strUserCode;
                if (bNeedTransaction) _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq(
                    strTid // strTid
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Check Access/Deny:
                //RptSv_Sys_Access_CheckDeny(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strFunctionName
                //	);
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strModuleCode = TUtils.CUtils.StdParam(objModuleCode);
                ////
                DataTable dtDB_Sys_Modules = null;
                ////
                {
                    ////
                    Sys_Modules_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objModuleCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Modules // dtDB_Sys_Modules
                        );
                    ////
                    dtDB_Sys_Modules.Rows[0]["LogLUDTimeUTC"] = dtimeTDate.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Modules.Rows[0]["LogLUBy"] = _cf.sinf.strUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Modules" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ModuleCode", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Modules // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_Sys_ObjectInModules = null;
                ////
                {
                    ////
                    string strTableCheck = "Sys_ObjectInModules";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_ObjectInModules_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Sys_ObjectInModules = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Sys_ObjectInModules // dtData
                        , "StdParam", "ObjectCode" // arrstrCouple
                        );
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_ObjectInModules" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ObjectCode" } // arrSingleStructure
                        , dtInput_Sys_ObjectInModules // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_UserInGroup:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from Sys_ObjectInModules t --//[mylock]
							inner join #tbl_Sys_Modules t_sm --//[mylock]
								on t.ModuleCode = t_sm.ModuleCode
						where (1=1)
						;

						---- Insert All:
						insert into Sys_ObjectInModules(
							ModuleCode
							, ObjectCode
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sm.ModuleCode
							, t_soim.ObjectCode
							, t_sm.LogLUDTimeUTC
							, t_sm.LogLUBy
						from #tbl_Sys_Modules t_sm --//[mylock]
							inner join #Sys_ObjectInModules t_soim --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet Sys_ObjectInModules_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , ref ArrayList alParamsCoupleError
            ////
            , object objModuleCode
            , DataSet dsData
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_ObjectInModules_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_ObjectInModules_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objModuleCode", objModuleCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                //if (dsData == null) dsData = new DataSet("dsData");
                //dsData.AcceptChanges();
                alParamsCoupleError.AddRange(new object[]{
                    "Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                    });
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // RptSv_Sys_User_CheckAuthentication:
                //RptSv_Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);

                // Check Access/Deny:
                RptSv_Sys_Access_CheckDeny(
                    ref alParamsCoupleError
                    , strWAUserCode
                    , strFunctionName
                    );
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strModuleCode = TUtils.CUtils.StdParam(objModuleCode);
                ////
                DataTable dtDB_Sys_Modules = null;
                ////
                {
                    ////
                    Sys_Modules_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , objModuleCode // objGroupCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_Modules // dtDB_Sys_Modules
                        );
                    ////
                    dtDB_Sys_Modules.Rows[0]["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    dtDB_Sys_Modules.Rows[0]["LogLUBy"] = strWAUserCode;
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_Modules" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ModuleCode", "NetworkID", "LogLUDTimeUTC", "LogLUBy" } // arrSingleStructure
                        , dtDB_Sys_Modules // dtData
                        );
                    ////
                }
                #endregion

                #region // Refine and Check Input Detail:
                ////
                DataTable dtInput_Sys_ObjectInModules = null;
                ////
                {
                    ////
                    string strTableCheck = "Sys_ObjectInModules";
                    ////
                    if (!dsData.Tables.Contains(strTableCheck))
                    {
                        alParamsCoupleError.AddRange(new object[]{
                            "Check.TableName", strTableCheck
                            });
                        throw CmUtils.CMyException.Raise(
                            TError.ErridnInventory.Sys_ObjectInModules_Save_InputTblDtlNotFound
                            , null
                            , alParamsCoupleError.ToArray()
                            );
                    }
                    dtInput_Sys_ObjectInModules = dsData.Tables[strTableCheck];
                    TUtils.CUtils.StdDataInTable(
                        dtInput_Sys_ObjectInModules // dtData
                        , "StdParam", "ObjectCode" // arrstrCouple
                        );
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db // db
                        , "#tbl_Sys_ObjectInModules" // strTableName
                        , TConst.BizMix.Default_DBColType // strDefaultType
                        , new object[] { "ObjectCode" } // arrSingleStructure
                        , dtInput_Sys_ObjectInModules // dtData
                        );
                    ////
                }
                #endregion

                #region // SaveDB Sys_ObjectInModules:
                {
                    string strSql_Exec = CmUtils.StringUtils.Replace(@"
						---- Clear All:
						delete t
						from Sys_ObjectInModules t --//[mylock]
							inner join #tbl_Sys_Modules t_sm --//[mylock]
								on t.ModuleCode = t_sm.ModuleCode
						where (1=1)
						;

						---- Insert All:
						insert into Sys_ObjectInModules(
							ModuleCode
							, ObjectCode
							, NetworkID
							, LogLUDTimeUTC
							, LogLUBy
							)
						select
							t_sm.ModuleCode
							, t_soim.ObjectCode
							, t_sm.NetworkID
							, t_sm.LogLUDTimeUTC
							, t_sm.LogLUBy
						from #tbl_Sys_Modules t_sm --//[mylock]
							inner join #tbl_Sys_ObjectInModules t_soim --//[mylock]
								on (1=1)
						;
					");
                    DataSet dsDB_Check = _cf.db.ExecQuery(
                        strSql_Exec
                        );
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        public DataSet WAS_Sys_ObjectInModules_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules
            ////
            , out RT_Sys_ObjectInModules objRT_Sys_ObjectInModules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_ObjectInModules.Tid;
            objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_ObjectInModules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_ObjectInModules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_ObjectInModules> lst_Sys_ObjectInModules = new List<Sys_ObjectInModules>();
                bool bGet_Sys_ObjectInModules = (objRQ_Sys_ObjectInModules.Rt_Cols_Sys_ObjectInModules != null && objRQ_Sys_ObjectInModules.Rt_Cols_Sys_ObjectInModules.Length > 0);
                #endregion

                #region // WS_Sys_ObjectInModules_Get:
                mdsResult = Sys_ObjectInModules_Get(
                    objRQ_Sys_ObjectInModules.Tid // strTid
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    , objRQ_Sys_ObjectInModules.WAUserCode // strUserCode
                    , objRQ_Sys_ObjectInModules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_ObjectInModules.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_ObjectInModules.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_ObjectInModules.Ft_WhereClause // strFt_WhereClause
                                                               //// Return:
                    , objRQ_Sys_ObjectInModules.Rt_Cols_Sys_ObjectInModules // strRt_Cols_Sys_ObjectInModules
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_ObjectInModules.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_ObjectInModules)
                    {
                        ////
                        DataTable dt_Sys_ObjectInModules = mdsResult.Tables["Sys_ObjectInModules"].Copy();
                        lst_Sys_ObjectInModules = TUtils.DataTableCmUtils.ToListof<Sys_ObjectInModules>(dt_Sys_ObjectInModules);
                        objRT_Sys_ObjectInModules.Lst_Sys_ObjectInModules = lst_Sys_ObjectInModules;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_Sys_ObjectInModules_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules
            ////
            , out RT_Sys_ObjectInModules objRT_Sys_ObjectInModules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_ObjectInModules.Tid;
            objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_ObjectInModules_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_ObjectInModules_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules.Sys_Modules)
                , "Lst_Sys_ObjectInModules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Sys_ObjectInModules = TUtils.DataTableCmUtils.ToDataTable<Sys_ObjectInModules>(objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules, "Sys_ObjectInModules");
                    dsData.Tables.Add(dt_Sys_ObjectInModules);
                }
                #endregion

                #region // Sys_ObjectInModules_Delete:
                mdsResult = Sys_ObjectInModules_Save(
                    objRQ_Sys_ObjectInModules.Tid // strTid
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    , objRQ_Sys_ObjectInModules.WAUserCode // strUserCode
                    , objRQ_Sys_ObjectInModules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_ObjectInModules.Sys_Modules.ModuleCode // objModuleCode
                    , dsData // dsData
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_ObjectInModules_Get(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules
            ////
            , out RT_Sys_ObjectInModules objRT_Sys_ObjectInModules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_ObjectInModules.Tid;
            objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_ObjectInModules_Get";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_ObjectInModules_Get;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                List<MySummaryTable> lst_MySummaryTable = new List<MySummaryTable>();
                List<Sys_ObjectInModules> lst_Sys_ObjectInModules = new List<Sys_ObjectInModules>();
                bool bGet_Sys_ObjectInModules = (objRQ_Sys_ObjectInModules.Rt_Cols_Sys_ObjectInModules != null && objRQ_Sys_ObjectInModules.Rt_Cols_Sys_ObjectInModules.Length > 0);
                #endregion

                #region // WS_Sys_ObjectInModules_Get:
                mdsResult = Sys_ObjectInModules_Get(
                    objRQ_Sys_ObjectInModules.Tid // strTid
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    , objRQ_Sys_ObjectInModules.WAUserCode // strUserCode
                    , objRQ_Sys_ObjectInModules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              //// Filter:
                    , objRQ_Sys_ObjectInModules.Ft_RecordStart // strFt_RecordStart
                    , objRQ_Sys_ObjectInModules.Ft_RecordCount // strFt_RecordCount
                    , objRQ_Sys_ObjectInModules.Ft_WhereClause // strFt_WhereClause
                                                               //// Return:
                    , objRQ_Sys_ObjectInModules.Rt_Cols_Sys_ObjectInModules // strRt_Cols_Sys_ObjectInModules
                    );
                #endregion

                #region // GetData:
                if (!CmUtils.CMyDataSet.HasError(mdsResult))
                {
                    ////
                    DataTable dt_MySummaryTable = mdsResult.Tables["MySummaryTable"].Copy();
                    lst_MySummaryTable = TUtils.DataTableCmUtils.ToListof<MySummaryTable>(dt_MySummaryTable);
                    objRT_Sys_ObjectInModules.MySummaryTable = lst_MySummaryTable[0];

                    ////
                    if (bGet_Sys_ObjectInModules)
                    {
                        ////
                        DataTable dt_Sys_ObjectInModules = mdsResult.Tables["Sys_ObjectInModules"].Copy();
                        lst_Sys_ObjectInModules = TUtils.DataTableCmUtils.ToListof<Sys_ObjectInModules>(dt_Sys_ObjectInModules);
                        objRT_Sys_ObjectInModules.Lst_Sys_ObjectInModules = lst_Sys_ObjectInModules;
                    }
                }
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }

        public DataSet WAS_RptSv_Sys_ObjectInModules_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_ObjectInModules objRQ_Sys_ObjectInModules
            ////
            , out RT_Sys_ObjectInModules objRT_Sys_ObjectInModules
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_ObjectInModules.Tid;
            objRT_Sys_ObjectInModules = new RT_Sys_ObjectInModules();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_ObjectInModules.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_RptSv_Sys_ObjectInModules_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_RptSv_Sys_ObjectInModules_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "Sys_Modules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules.Sys_Modules)
                , "Lst_Sys_ObjectInModules", TJson.JsonConvert.SerializeObject(objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules)
                });
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                ////
                DataSet dsData = new DataSet();
                {
                    ////
                    DataTable dt_Sys_ObjectInModules = TUtils.DataTableCmUtils.ToDataTable<Sys_ObjectInModules>(objRQ_Sys_ObjectInModules.Lst_Sys_ObjectInModules, "Sys_ObjectInModules");
                    dsData.Tables.Add(dt_Sys_ObjectInModules);
                }
                #endregion

                #region // Sys_ObjectInModules_Delete:
                mdsResult = Sys_ObjectInModules_Save(
                    objRQ_Sys_ObjectInModules.Tid // strTid
                    , objRQ_Sys_ObjectInModules.GwUserCode // strGwUserCode
                    , objRQ_Sys_ObjectInModules.GwPassword // strGwPassword
                    , objRQ_Sys_ObjectInModules.WAUserCode // strUserCode
                    , objRQ_Sys_ObjectInModules.WAUserPassword // strUserPassword
                    , ref alParamsCoupleError // alParamsCoupleError
                                              ////
                    , objRQ_Sys_ObjectInModules.Sys_Modules.ModuleCode // objModuleCode
                    , dsData // dsData
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        #endregion

        #region // Sys_UserLicense:
        private void Sys_UserLicense_CheckDB(
            ref ArrayList alParamsCoupleError
            , object objUserCode
            , string strFlagExistToCheck
            , out DataTable dtDB_Sys_UserLicense
            )
        {
            // GetInfo:
            string strSqlExec = CmUtils.StringUtils.Replace(@"
					select top 1
						t.*
					from Sys_UserLicense t --//[mylock]
					where (1=1)
						and t.UserCode = @objUserCode
					;
				");
            dtDB_Sys_UserLicense = _cf.db.ExecQuery(
                strSqlExec
                , "@objUserCode", objUserCode
                ).Tables[0];
            dtDB_Sys_UserLicense.TableName = "Sys_UserLicense";

            // strFlagExistToCheck:
            if (strFlagExistToCheck.Length > 0)
            {
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Active) && dtDB_Sys_UserLicense.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCode", objUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_UserLicense_CheckDB_UserLicenseNotFound
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                if (CmUtils.StringUtils.StringEqual(strFlagExistToCheck, TConst.Flag.Inactive) && dtDB_Sys_UserLicense.Rows.Count > 0)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.UserCode", objUserCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_UserLicense_CheckDB_UserLicenseExist
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
        }
        private void Sys_UserLicense_CheckDeny(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , DateTime dtimeSys
            , ref DataSet mdsFinal
            ////
            , object strUserCode
            , object strObjectCode
            )
        {
            #region // Refine and Check Input:
            //bool bTest = Convert.ToBoolean(_cf.nvcParams["Biz_TestLocal"]);
            #endregion

            #region // Check Sys_Modules:
            {
                ////
                string strSqlCheck_Sys_Modules = CmUtils.StringUtils.Replace(@"
						---- #tbl_Sys_Modules_Mix_Filter:
						select distinct
							t.ModuleCode
						into #tbl_Sys_Modules_Mix_Filter
						from Sys_ObjectInModules t --//[mylock]
						where (1=1)
							and t.ObjectCode = '@strObjectCode'
						;

						--select null tbl_Sys_Modules_Mix_Filter, * from #tbl_Sys_Modules_Mix_Filter t --//[mylock];

						---- #tbl_Sys_Modules_User_Filter:
						select distinct
							t.ModuleCode 
						into #tbl_Sys_Modules_User_Filter
						from Sys_UserLicenseModules t --//[mylock]
						where (1=1)
							and t.UserCode = '@strUserCode'
						;

						--select null tbl_Sys_Modules_User_Filter, * from #tbl_Sys_Modules_User_Filter t --//[mylock];

						---- Check:
						select
							t.*
						from #tbl_Sys_Modules_Mix_Filter t --//[mylock]
							inner join #tbl_Sys_Modules_User_Filter f --//[mylock]
								on t.ModuleCode = f.ModuleCode
						where (1=1)
						;
					"
                    , "@strObjectCode", strObjectCode
                    , "@strUserCode", strUserCode
                    );

                DataTable dtCheck = _cf.db.ExecQuery(strSqlCheck_Sys_Modules).Tables[0];
                ////
                if (dtCheck.Rows.Count < 1)
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.strUserCode", strUserCode
                        , "Check.strObjectCode", strObjectCode
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.Sys_UserLicense_CheckDeny
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
            }
            #endregion

            #region // Check Token Expire:
            {
                ////
                DataTable dtDB_Sys_UserLicense = null;

                Sys_UserLicense_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strUserCode // objUserCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , out dtDB_Sys_UserLicense // dtDB_Sys_UserLicense
                    );

                DateTime LastAccessDTimeUTC = Convert.ToDateTime(dtDB_Sys_UserLicense.Rows[0]["LastAccessDTimeUTC"]);
                double dbleTimeSpan = (dtimeSys - LastAccessDTimeUTC).TotalMinutes;

                ////
                if (dbleTimeSpan >= TConst.BizMix.Default_MaxMinutesToCheckToken)
                {
                    #region // Check Token Expire:
                    ////
                    DataTable dtDB_Sys_Solution = null;
                    {
                        // GetInfo:
                        dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                            _cf.db // db
                            , "Sys_Solution" // strTableName
                            , "top 1 *" // strColumnList
                            , "" // strClauseOrderBy
                            , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                            );
                    }
                    ////
                    string strSqlGetDB_Sys_UserInOrgID = CmUtils.StringUtils.Replace(@"
							---- Sys_User
							select
								t.UserCode
								, f.OrgID 
							from Sys_User t --//[mylock]
								left join Mst_NNT f --//[mylock]
									on t.MST = f.MST
							where (1=1)
								and t.UserCode = '@strUserCode'
							;
						"
                        , "@strUserCode", strUserCode
                        );

                    DataTable dtDB_Sys_UserInOrgID = _cf.db.ExecQuery(strSqlGetDB_Sys_UserInOrgID).Tables[0];
                    ////
                    DataSet dsGetData = null;

                    Inos_LicService_GetCurrentUserLicenseX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                        , dtDB_Sys_UserInOrgID.Rows[0]["OrgID"] // objOrgID
                                                                ////
                        , out dsGetData // dsData
                        );
                    #endregion

                    #region // Sys_UserLicense: Upd.
                    {
                        TDAL.IEzDAL dbLocal = (TDAL.IEzDAL)_cf.db.Clone();
                        dbLocal.BeginTransaction();

                        try
                        {
                            ////
                            string strSqlUpd_Sys_UserLicense = CmUtils.StringUtils.Replace(@"
									---- Sys_UserLicense:
									update t
									set
										t.LastAccessDTimeUTC = '@strLastAccessDTimeUTC' 
									from Sys_UserLicense t --//[mylock]
									where (1=1)
										and t.UserCode = '@strUserCode'
									;
								"
                                , "@strUserCode", strUserCode
                                , "@strLastAccessDTimeUTC", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
                                );

                            dbLocal.ExecQuery(
                                strSqlUpd_Sys_UserLicense
                                );

                            dbLocal.Commit();
                        }
                        catch (Exception ex)
                        {
                            dbLocal.Rollback();
                            throw ex;
                        }

                    }
                    #endregion

                }

            }
            #endregion

        }

        public DataSet Sys_UserLicense_Save(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_UserLicense_Save";
            string strErrorCodeDefault = TError.ErridnInventory.Sys_UserLicense_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", strWAUserCode
                });
            #endregion

            try
            {
                #region // Convert Input:
                //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
                //if (dsData == null) dsData = new DataSet("dsData");
                //dsData.AcceptChanges();
                //alParamsCoupleError.AddRange(new object[]{
                //	"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
                //	});
                #endregion

                #region // Init:
                //_cf.db.LogUserId = _cf.sinf.strUserCode;
                _cf.db.BeginTransaction();

                // Write RequestLog:
                _cf.ProcessBizReq_OutSide(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    , alParamsCoupleError // alParamsCoupleError
                    );

                // Sys_User_CheckAuthentication:
                //Sys_User_CheckAuthentication(
                //	ref alParamsCoupleError
                //	, strWAUserCode
                //	, strWAUserPassword
                //	);
                #endregion

                #region // Refine and Check Input Master:
                ////
                string strUserCode = TUtils.CUtils.StdParam(strWAUserCode);
                string strUserPasswordEncode = TUtils.CUtils.Base64_Encode(strWAUserPassword);
                string strUserPasswordHash = TUtils.CUtils.GetEncodedHash(strWAUserPassword);

                ////
                DataTable dtDB_Sys_User = null;
                ////
                {
                    ////
                    Sys_User_CheckDB(
                        ref alParamsCoupleError // alParamsCoupleError
                        , strUserCode // objUserCode
                        , TConst.Flag.Yes // strFlagExistToCheck
                        , "" // strFlagActiveListToCheck
                        , out dtDB_Sys_User // dt_Sys_User
                        );
                    ////
                }
                #endregion

                #region //// SaveTemp Sys_UserLicense:
                {
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db
                        , "#input_Sys_UserLicense"
                        , new object[]{
                            "UserCode", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "UserPasswordEncode", TConst.BizMix.Default_DBColType,
                            "UserPasswordHash", TConst.BizMix.Default_DBColType,
                            "LastAccessDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                            }
                        , new object[]{
                            new object[]{
                                strUserCode, // LCID
								nNetworkID, // NetworkID
								strUserPasswordEncode , // UserPasswordEncode								
								strUserPasswordHash , // UserPasswordHash
								dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LastAccessDTimeUTC
								dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
								strWAUserCode, // LogLUBy
								}
                            }
                        );
                }
                #endregion

                #region // Build Sys_UserLicenseModules:
                ////
                bool bTest = Convert.ToBoolean(_cf.nvcParams["Biz_TestLocal"]);
                DataTable dt_Sys_UserLicenseModules = null;
                ////
                if (!bTest)
                {
                    ////
                    DataTable dtDB_Sys_Solution = null;
                    {
                        // GetInfo:
                        dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                            _cf.db // db
                            , "Sys_Solution" // strTableName
                            , "top 1 *" // strColumnList
                            , "" // strClauseOrderBy
                            , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                            );
                    }
                    ////
                    string strSqlGetDB_Sys_UserInOrgID = CmUtils.StringUtils.Replace(@"
							---- Sys_User
							select
								t.UserCode
								, f.OrgID 
							from Sys_User t --//[mylock]
								left join Mst_NNT f --//[mylock]
									on t.MST = f.MST
							where (1=1)
								and t.UserCode = '@strUserCode'
							;
						"
                        , "@strUserCode", strUserCode
                        );

                    DataTable dtDB_Sys_UserInOrgID = _cf.db.ExecQuery(strSqlGetDB_Sys_UserInOrgID).Tables[0];
                    ////
                    DataSet dsGetData = null;

                    Inos_LicService_GetCurrentUserLicenseX(
                        strTid // strTid
                        , strGwUserCode // strGwUserCode
                        , strGwPassword // strGwPassword
                        , strWAUserCode // strWAUserCode
                        , strWAUserPassword // strWAUserPassword
                        , strAccessToken // strAccessToken
                        , ref mdsFinal // mdsFinal
                        , ref alParamsCoupleError // alParamsCoupleError
                        , dtimeSys // dtimeSys
                                   ////
                        , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                        , dtDB_Sys_UserInOrgID.Rows[0]["OrgID"] // objOrgID
                                                                ////
                        , out dsGetData // dsData
                        );

                    dt_Sys_UserLicenseModules = dsGetData.Tables["Sys_Modules"].Copy();
                    dt_Sys_UserLicenseModules.TableName = "Sys_UserLicenseModules";
                }
                else
                {
                    string strSqlGetDB_Sys_Modules = CmUtils.StringUtils.Replace(@"
							---- Sys_Modules:
							select distinct
								t.ModuleCode
							from Sys_Modules t
							where (1=1)
							;
						");

                    dt_Sys_UserLicenseModules = _cf.db.ExecQuery(strSqlGetDB_Sys_Modules).Tables[0];
                    dt_Sys_UserLicenseModules.TableName = "Sys_UserLicenseModules";
                }

                ////
                if (dt_Sys_UserLicenseModules != null)
                {
                    ////
                    TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "UserCode", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "NetworkID", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "LogLUDTimeUTC", typeof(object));
                    TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "LogLUBy", typeof(object));
                    ////
                    for (int nScan = 0; nScan < dt_Sys_UserLicenseModules.Rows.Count; nScan++)
                    {
                        ////
                        DataRow drScan = dt_Sys_UserLicenseModules.Rows[nScan];

                        ////
                        drScan["UserCode"] = strUserCode;
                        drScan["NetworkID"] = nNetworkID;
                        drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                        drScan["LogLUBy"] = strWAUserCode;
                    }
                }
                #endregion

                #region // SaveTemp Sys_UserLicenseModules:
                {
                    //// Upload:
                    TUtils.CUtils.MyBuildDBDT_Common(
                        _cf.db
                        , "#input_Sys_UserLicenseModules"
                        , new object[]{
                            "UserCode", TConst.BizMix.Default_DBColType,
                            "ModuleCode", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                            }
                        , dt_Sys_UserLicenseModules
                        );
                    ////
                }
                #endregion

                #region //// Save:
                //// Clear All:
                {
                    string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- Sys_UserLicenseModules:
								delete t
								from Sys_UserLicenseModules t
								where (1=1)
									and t.UserCode = @strUserCode
								;

								---- Sys_UserLicense:
								delete t
								from Sys_UserLicense t
								where (1=1)
									and t.UserCode = @strUserCode
								;

							");
                    _cf.db.ExecQuery(
                        strSqlDelete
                        , "@strUserCode", strUserCode
                        );
                }

                //// Insert All:
                {
                    ////
                    string zzzzClauseInsert_Sys_UserLicense_zSave = CmUtils.StringUtils.Replace(@"
							---- Sys_UserLicense:
							insert into Sys_UserLicense
							(
								UserCode
								, NetworkID
								, UserPasswordEncode
								, UserPasswordHash
								, LastAccessDTimeUTC
								, LogLUDTimeUTC
								, LogLUBy	
							)
							select 
								t.UserCode
								, t.NetworkID
								, t.UserPasswordEncode
								, t.UserPasswordHash
								, t.LastAccessDTimeUTC
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Sys_UserLicense t --//[mylock]
							;
						");
                    ////
                    string zzzzClauseInsert_Sys_UserLicenseModules_zSave = CmUtils.StringUtils.Replace(@"
							---- Sys_UserLicenseModules:
							insert into Sys_UserLicenseModules
							(
								UserCode
								, ModuleCode
								, NetworkID
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.UserCode
								, t.ModuleCode
								, t.NetworkID
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Sys_UserLicenseModules t --//[mylock]
							;
						");
                    ////
                    string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_Sys_UserLicense_zSave
			
							----
							zzzzClauseInsert_Sys_UserLicenseModules_zSave
						"
                        , "zzzzClauseInsert_Sys_UserLicense_zSave", zzzzClauseInsert_Sys_UserLicense_zSave
                        , "zzzzClauseInsert_Sys_UserLicenseModules_zSave", zzzzClauseInsert_Sys_UserLicenseModules_zSave
                        );
                    ////
                    DataSet dsExec = _cf.db.ExecQuery(strSqlExec);
                }
                #endregion

                // Return Good:
                TDALUtils.DBUtils.CommitSafety(_cf.db);
                mdsFinal.AcceptChanges();
                return mdsFinal;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Rollback:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);

                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsFinal
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Rollback and Release resources:
                TDALUtils.DBUtils.RollbackSafety(_cf.db);
                TDALUtils.DBUtils.ReleaseAllSemaphore(_cf.db_Sys, true);

                // Write ReturnLog:
                _cf.ProcessBizReturn_OutSide(
                    ref mdsFinal // mdsFinal
                    , strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // objUserCode
                    , strFunctionName // strFunctionName
                    );
                #endregion
            }
        }

        private void Sys_UserLicense_SaveX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , TDAL.IEzDAL dbAction
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_UserLicense_Save";
            //string strErrorCodeDefault = TError.ErridInBrand.Sys_UserLicense_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objUserCode", strWAUserCode
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            //alParamsCoupleError.AddRange(new object[]{
            //	"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
            //	});
            #endregion

            #region // Refine and Check Input Master:
            ////
            string strUserCode = TUtils.CUtils.StdParam(strWAUserCode);
            string strUserPasswordEncode = TUtils.CUtils.Base64_Encode(strWAUserPassword);
            string strUserPasswordHash = TUtils.CUtils.GetEncodedHash(strWAUserPassword);

            ////
            DataTable dtDB_Sys_User = null;
            ////
            {
                ////
                Sys_User_CheckDB(
                    ref alParamsCoupleError // alParamsCoupleError
                    , strUserCode // objUserCode
                    , TConst.Flag.Yes // strFlagExistToCheck
                    , "" // strFlagActiveListToCheck
                    , out dtDB_Sys_User // dt_Sys_User
                    );
                ////
            }
            #endregion

            #region //// SaveTemp Sys_UserLicense:
            {
                TUtils.CUtils.MyBuildDBDT_Common(
                    dbAction
                    , "#input_Sys_UserLicense"
                    , new object[]{
                            "UserCode", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "UserPasswordEncode", TConst.BizMix.Default_DBColType,
                            "UserPasswordHash", TConst.BizMix.Default_DBColType,
                            "LastAccessDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , new object[]{
                            new object[]{
                                strUserCode, // LCID
								nNetworkID, // NetworkID
								strUserPasswordEncode , // UserPasswordEncode								
								strUserPasswordHash , // UserPasswordHash
								dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LastAccessDTimeUTC
								dtimeSys.ToString("yyyy-MM-dd HH:mm:ss"), // LogLUDTimeUTC
								strWAUserCode, // LogLUBy
								}
                        }
                    );
            }
            #endregion

            #region // Build Sys_UserLicenseModules:
            ////
            bool bTest = Convert.ToBoolean(_cf.nvcParams["Biz_TestLocal"]);
            DataTable dt_Sys_UserLicenseModules = null;
            ////
            if (!bTest)
            {
                ////
                DataTable dtDB_Sys_Solution = null;
                {
                    // GetInfo:
                    dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                        dbAction // db
                        , "Sys_Solution" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                        );
                }
                ////
                string strSqlGetDB_Sys_UserInOrgID = CmUtils.StringUtils.Replace(@"
							---- Sys_User
							select
								t.UserCode
								, f.OrgID 
							from Sys_User t --//[mylock]
								left join Mst_NNT f --//[mylock]
									on t.MST = f.MST
							where (1=1)
								and t.UserCode = '@strUserCode'
							;
						"
                    , "@strUserCode", strUserCode
                    );

                DataTable dtDB_Sys_UserInOrgID = dbAction.ExecQuery(strSqlGetDB_Sys_UserInOrgID).Tables[0];
                ////
                DataSet dsGetData = null;

                Inos_LicService_GetCurrentUserLicenseX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strAccessToken // strAccessToken
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                    , dtDB_Sys_UserInOrgID.Rows[0]["OrgID"] // objOrgID
                                                            ////
                    , out dsGetData // dsData
                    );

                dt_Sys_UserLicenseModules = dsGetData.Tables["Sys_Modules"].Copy();
                dt_Sys_UserLicenseModules.TableName = "Sys_UserLicenseModules";
            }
            else
            {
                string strSqlGetDB_Sys_Modules = CmUtils.StringUtils.Replace(@"
							---- Sys_Modules:
							select distinct
								t.ModuleCode
							from Sys_Modules t
							where (1=1)
							;
						");

                dt_Sys_UserLicenseModules = dbAction.ExecQuery(strSqlGetDB_Sys_Modules).Tables[0];
                dt_Sys_UserLicenseModules.TableName = "Sys_UserLicenseModules";
            }

            ////
            if (dt_Sys_UserLicenseModules != null)
            {
                ////
                TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "UserCode", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "NetworkID", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "LogLUDTimeUTC", typeof(object));
                TUtils.CUtils.MyForceNewColumn(ref dt_Sys_UserLicenseModules, "LogLUBy", typeof(object));
                ////
                for (int nScan = 0; nScan < dt_Sys_UserLicenseModules.Rows.Count; nScan++)
                {
                    ////
                    DataRow drScan = dt_Sys_UserLicenseModules.Rows[nScan];

                    ////
                    drScan["UserCode"] = strUserCode;
                    drScan["NetworkID"] = nNetworkID;
                    drScan["LogLUDTimeUTC"] = dtimeSys.ToString("yyyy-MM-dd HH:mm:ss");
                    drScan["LogLUBy"] = strWAUserCode;
                }
            }
            #endregion

            #region // SaveTemp Sys_UserLicenseModules:
            {
                //// Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    dbAction
                    , "#input_Sys_UserLicenseModules"
                    , new object[]{
                            "UserCode", TConst.BizMix.Default_DBColType,
                            "ModuleCode", TConst.BizMix.Default_DBColType,
                            "NetworkID", TConst.BizMix.Default_DBColType,
                            "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                            "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dt_Sys_UserLicenseModules
                    );
                ////
            }
            #endregion

            #region //// Save:
            //// Clear All:
            {
                string strSqlDelete = CmUtils.StringUtils.Replace(@"
								---- Sys_UserLicenseModules:
								delete t
								from Sys_UserLicenseModules t
								where (1=1)
									and t.UserCode = @strUserCode
								;

								---- Sys_UserLicense:
								delete t
								from Sys_UserLicense t
								where (1=1)
									and t.UserCode = @strUserCode
								;

							");
                dbAction.ExecQuery(
                    strSqlDelete
                    , "@strUserCode", strUserCode
                    );
            }

            //// Insert All:
            {
                ////
                string zzzzClauseInsert_Sys_UserLicense_zSave = CmUtils.StringUtils.Replace(@"
							---- Sys_UserLicense:
							insert into Sys_UserLicense
							(
								UserCode
								, NetworkID
								, UserPasswordEncode
								, UserPasswordHash
								, LastAccessDTimeUTC
								, LogLUDTimeUTC
								, LogLUBy	
							)
							select 
								t.UserCode
								, t.NetworkID
								, t.UserPasswordEncode
								, t.UserPasswordHash
								, t.LastAccessDTimeUTC
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Sys_UserLicense t --//[mylock]
							;
						");
                ////
                string zzzzClauseInsert_Sys_UserLicenseModules_zSave = CmUtils.StringUtils.Replace(@"
							---- Sys_UserLicenseModules:
							insert into Sys_UserLicenseModules
							(
								UserCode
								, ModuleCode
								, NetworkID
								, LogLUDTimeUTC
								, LogLUBy
							)
							select 
								t.UserCode
								, t.ModuleCode
								, t.NetworkID
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Sys_UserLicenseModules t --//[mylock]
							;
						");
                ////
                string strSqlExec = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_Sys_UserLicense_zSave
			
							----
							zzzzClauseInsert_Sys_UserLicenseModules_zSave
						"
                    , "zzzzClauseInsert_Sys_UserLicense_zSave", zzzzClauseInsert_Sys_UserLicense_zSave
                    , "zzzzClauseInsert_Sys_UserLicenseModules_zSave", zzzzClauseInsert_Sys_UserLicenseModules_zSave
                    );
                ////
                DataSet dsExec = dbAction.ExecQuery(strSqlExec);

                //{
                //    string strSql_Check = CmUtils.StringUtils.Replace(@"
                //            select
                //                t.*
                //            from Sys_UserLicenseModules t with(nolock)
                //            ;

                //            select
                //                t.*
                //            from Sys_UserLicense t with(nolock)
                //            ;
                //        ");
                //    DataSet dsData = dbAction.ExecQuery(strSql_Check);
                //}
            }

            // Return Good:
            //TDALUtils.DBUtils.CommitSafety(dbAction);
            mdsFinal.AcceptChanges();
            //return mdsFinal;
            #endregion
        }

        private void Sys_OrgLicenseModules_InsertOrUpdOrDelX(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            , TDAL.IEzDAL dbAction
            ////
            , object objOrgID
            )
        {
            #region // Temp:
            DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            //int nTidSeq = 0;
            DateTime dtimeSys = DateTime.UtcNow;
            string strFunctionName = "Sys_OrgLicenseModules_Save";
            //string strErrorCodeDefault = TError.ErridInBrand.Sys_OrgLicenseModules_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "objOrgID", objOrgID
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            //alParamsCoupleError.AddRange(new object[]{
            //	"Check.dsData", TJson.JsonConvert.SerializeObject(dsData)
            //	});
            #endregion

            #region // Refine and Check Input:
            string strOrgID = TUtils.CUtils.StdParam(objOrgID);
            #endregion

            #region // Build Sys_OrgLicenseModules:
            ////
            DataTable dt_Sys_OrgLicenseModules = null;
            {
                DataTable dtDB_Sys_Solution = null;
                {
                    // GetInfo:
                    dtDB_Sys_Solution = TDALUtils.DBUtils.GetTableContents(
                        dbAction // db
                        , "Sys_Solution" // strTableName
                        , "top 1 *" // strColumnList
                        , "" // strClauseOrderBy
                        , "FlagActive", "=", TConst.Flag.Active // arrobjParamsTriple item
                        );
                }
                ////
                DataSet dsGetData = null;

                Inos_LicService_GetOrgSolutionModulesX(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                    , strWAUserPassword // strWAUserPassword
                    , strAccessToken // strAccessToken
                    , ref mdsFinal // mdsFinal
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                               ////
                    , dtDB_Sys_Solution.Rows[0]["SolutionCode"] // objSolutionCode
                    , strOrgID // objOrgID
                               ////
                    , out dsGetData // dsData
                    );

                dt_Sys_OrgLicenseModules = dsGetData.Tables["Mst_Org"].Copy();
                dt_Sys_OrgLicenseModules.TableName = "Sys_UserLicenseModules";
            }

            #endregion

            #region // SaveTemp Sys_UserLicenseModules:
            {
                //// Upload:
                TUtils.CUtils.MyBuildDBDT_Common(
                    dbAction
                    , "#input_Sys_OrgLicenseModules"
                    , new object[]{
                        "OrgID", TConst.BizMix.Default_DBColType,
                        "ModuleCode", TConst.BizMix.Default_DBColType,
                        "NetworkID", TConst.BizMix.Default_DBColType,
                        "Qty", "float",
                        "LogLUDTimeUTC", TConst.BizMix.Default_DBColType,
                        "LogLUBy", TConst.BizMix.Default_DBColType,
                        }
                    , dt_Sys_OrgLicenseModules
                    );
                ////
            }
            #endregion

            #region // SaveDB:
            {
                ////
                string zzzzClauseInsert_Sys_OrgLicenseModules_zSave = CmUtils.StringUtils.Replace(@"
							---- Sys_OrgLicenseModules:
							insert into Sys_OrgLicenseModules
							(
								OrgID
								, ModuleCode
								, NetworkID
								, Qty
								, LogLUDTimeUTC
								, LogLUBy
							)
							select
								t.OrgID
								, t.ModuleCode
								, t.NetworkID
								, t.Qty
								, t.LogLUDTimeUTC
								, t.LogLUBy
							from #input_Sys_OrgLicenseModules t --//[mylock]
								left join Sys_OrgLicenseModules f --//[mylock]
									on t.OrgID = f.OrgID
										and t.ModuleCode = f.ModuleCode
							where (1=1)
								and f.ModuleCode is null
							;
						");

                ////
                string zzB_Update_Sys_OrgLicenseModules_ClauseSet_zzE = @"
								t.LogLUDTimeUTC = f.LogLUDTimeUTC
								, t.LogLUBy = f.LogLUBy
								, t.Qty = f.Qty
								";

                ////
                string zzB_Update_Sys_OrgLicenseModules_zzE = CmUtils.StringUtils.Replace(@"
							---- Sys_OrgLicenseModules:
							update t
							set 
								zzB_Update_Sys_OrgLicenseModules_ClauseSet_zzE
							from Sys_OrgLicenseModules t --//[mylock]
								inner join #input_Sys_OrgLicenseModules f --//[mylock]
									on t.OrgID = f.OrgID
										and t.ModuleCode = f.ModuleCode
							where (1=1)
							;
						"
                    , "zzB_Update_Sys_OrgLicenseModules_ClauseSet_zzE", zzB_Update_Sys_OrgLicenseModules_ClauseSet_zzE
                    );

                ////
                string zzzzClauseDel_Sys_OrgLicenseModules_zSave = CmUtils.StringUtils.Replace(@"
							---- Sys_OrgLicenseModules:
							delete f
							from #input_Sys_OrgLicenseModules t --//[mylock]
								full join Sys_OrgLicenseModules f --//[mylock]
									on t.OrgID = f.OrgID
										and t.ModuleCode = f.ModuleCode
							where (1=1)
								and t.ModuleCode is null
							;
						");
                ////
                string strSql_SaveOnDB = CmUtils.StringUtils.Replace(@"
							----
							zzzzClauseInsert_Sys_OrgLicenseModules_zSave
							----
							zzB_Update_Sys_OrgLicenseModules_zzE
							----
							zzzzClauseDel_Sys_OrgLicenseModules_zSave
							----
						"
                    , "zzzzClauseInsert_Sys_OrgLicenseModules_zSave", zzzzClauseInsert_Sys_OrgLicenseModules_zSave
                    , "zzB_Update_Sys_OrgLicenseModules_zzE", zzB_Update_Sys_OrgLicenseModules_zzE
                    , "zzzzClauseDel_Sys_OrgLicenseModules_zSave", zzzzClauseDel_Sys_OrgLicenseModules_zSave
                    );

                DataSet dsDB_Check = dbAction.ExecQuery(
                    strSql_SaveOnDB
                    );
            }
            #endregion
        }

        public DataSet WAS_Sys_UserLicense_Save(
            ref ArrayList alParamsCoupleError
            , RQ_Sys_UserLicense objRQ_Sys_UserLicense
            ////
            , out RT_Sys_UserLicense objRT_Sys_UserLicense
            )
        {
            #region // Temp:
            string strTid = objRQ_Sys_UserLicense.Tid;
            objRT_Sys_UserLicense = new RT_Sys_UserLicense();
            DataSet mdsResult = CmUtils.CMyDataSet.NewMyDataSet(strTid);
            DateTime dtimeSys = DateTime.UtcNow;
            //DataSet mdsExec = null;
            //DataSet mdsFinal = CmUtils.CMyDataSet.NewMyDataSet(objRQ_Sys_UserInGroup.Tid);
            //int nTidSeq = 0;
            //bool bNeedTransaction = true;
            string strFunctionName = "WAS_Sys_UserLicense_Save";
            string strErrorCodeDefault = TError.ErridnInventory.WAS_Sys_UserLicense_Save;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				});
            #endregion

            try
            {
                #region // Init:
                #endregion

                #region // Refine and Check Input:
                #endregion

                #region // Sys_UserLicense_Delete:
                mdsResult = Sys_UserLicense_Save(
                    objRQ_Sys_UserLicense.Tid // strTid
                    , objRQ_Sys_UserLicense.GwUserCode // strGwUserCode
                    , objRQ_Sys_UserLicense.GwPassword // strGwPassword
                    , objRQ_Sys_UserLicense.WAUserCode // strUserCode
                    , objRQ_Sys_UserLicense.WAUserPassword // strUserPassword
                    , objRQ_Sys_UserLicense.AccessToken // strAccessToken
                    , ref alParamsCoupleError // alParamsCoupleError
                    );
                #endregion

                // Return Good:
                return mdsResult;
            }
            catch (Exception exc)
            {
                #region // Catch of try:
                // Return Bad:
                return TUtils.CProcessExc.Process(
                    ref mdsResult
                    , exc
                    , strErrorCodeDefault
                    , alParamsCoupleError.ToArray()
                    );
                #endregion
            }
            finally
            {
                #region // Finally of try:
                // Write ReturnLog:
                //_cf.ProcessBizReturn(
                //	ref mdsResult // mdsFinal
                //	, strTid // strTid
                //	, strFunctionName // strFunctionName
                //	);
                #endregion
            }
        }
        #endregion

    }
}
