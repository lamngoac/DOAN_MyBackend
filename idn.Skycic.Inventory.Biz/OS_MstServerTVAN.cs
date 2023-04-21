using idn.Skycic.Inventory.BizService.Services;
using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
//using System.Xml.Linq;

using CmUtils = CommonUtils;
using TConst = idn.Skycic.Inventory.Constants;
using TDALUtils = EzDAL.Utils;
using TError = idn.Skycic.Inventory.Errors;
using TJson = Newtonsoft.Json;
using TUtils = idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {
        #region // MstSv_Mst_Network_GetByMST:
        public void OS_MstSvTVAN_MstSv_Mst_Network_GetByMST(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strMST
            , out string strUrl_OS_TVAN_AllNetWork
            )
        {
            #region // Temp:
            string strFunctionName = "OS_MstSvTVAN_MstSv_Mst_Network_GetByMST";
            string strErrorCodeDefault = TError.ErridnInventory.OS_MstSvTVAN_MstSv_Mst_Network_GetByMST;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "strMST", strMST
                });
            #endregion

            #region // Call Func:
            RT_OS_MstSvTVAN_MstSv_Mst_Network objRT_OS_MstSvTVAN_MstSv_Mst_Network = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                OS_MstSvTVAN_MstSv_Mst_Network objOS_MstSvTVAN_MstSv_Mst_Network = new OS_MstSvTVAN_MstSv_Mst_Network();
                objOS_MstSvTVAN_MstSv_Mst_Network.MST = strMST;
                /////
                RQ_OS_MstSvTVAN_MstSv_Mst_Network objRQ_OS_MstSvTVAN_MstSv_Mst_Network = new RQ_OS_MstSvTVAN_MstSv_Mst_Network()
                {
                    MstSv_Mst_Network = objOS_MstSvTVAN_MstSv_Mst_Network,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = nNetworkID.ToString(),
                    GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                    GwPassword = strOS_MasterServer_Solution_GwPassword,
                    WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                    WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
                };
                ////
                try
                {
                    objRT_OS_MstSvTVAN_MstSv_Mst_Network = OS_MstSvTVAN_MstSv_Mst_NetworkService.Instance.WA_OS_MstSvTVAN_MstSv_Mst_Network_GetByMST(objRQ_OS_MstSvTVAN_MstSv_Mst_Network);
                    ////
                }
                catch (Exception cex)
                {
                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion

            #region // Get Remark:
            strUrl_OS_TVAN_AllNetWork = objRT_OS_MstSvTVAN_MstSv_Mst_Network.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
            #endregion
        }
        #endregion

        #region // Map_Network_SysOutSide_GetBySysOS:
        public void OS_MstSvTVAN_Map_Network_SysOutSide_GetBySysOS(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            , string strWAUserPassword
            , string strNetworkID
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strOrgID
            , out string strUrl_OS_TVAN_3A
            )
        {
            #region // Temp:
            string strFunctionName = "OS_MstSvTVAN_Map_Network_SysOutSide_GetBySysOS";
            string strErrorCodeDefault = TError.ErridnInventory.OS_MstSvTVAN_Map_Network_SysOutSide_GetBySysOS;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
                , "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "strOrgID", strOrgID
                });
            #endregion

            #region // Call Func:
            RT_OS_MstSvTVAN_Map_Network_SysOutSide objRT_OS_MstSvTVAN_Map_Network_SysOutSide = null;
            {
                #region // Add Param:
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "strOS_MasterServer_Solution_TokenID", strOS_MasterServer_Solution_TokenID
                        });
                }
                #endregion

                #region // WA_MstSv_Mst_Network_GetByMST:
                OS_MstSvTVAN_Map_Network_SysOutSide objOS_MstSvTVAN_Map_Network_SysOutSide = new OS_MstSvTVAN_Map_Network_SysOutSide();
                objOS_MstSvTVAN_Map_Network_SysOutSide.SysOSCode = strOS_MasterServer_Solution_SysOSCode3A;
                /////
                RQ_OS_MstSvTVAN_Map_Network_SysOutSide objRQ_OS_MstSvTVAN_Map_Network_SysOutSide = new RQ_OS_MstSvTVAN_Map_Network_SysOutSide()
                {
                    Map_Network_SysOutSide = objOS_MstSvTVAN_Map_Network_SysOutSide,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = nNetworkID.ToString(),
                    OrgID = strOrgID,
                    GwUserCode = strOS_MasterServer_Solution_GwUserCode,
                    GwPassword = strOS_MasterServer_Solution_GwPassword,
                    WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                    WAUserPassword = strOS_MasterServer_Solution_WAUserPassword
                };
                ////
                try
                {
                    objRT_OS_MstSvTVAN_Map_Network_SysOutSide = OS_MstSvTVAN_Map_Network_SysOutSideService.Instance.WA_Map_Network_SysOutSide_GetBySysOS(objRQ_OS_MstSvTVAN_Map_Network_SysOutSide);
                    ////
                }
                catch (Exception cex)
                {
                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion

            #region // Get Remark:
            strUrl_OS_TVAN_3A = objRT_OS_MstSvTVAN_Map_Network_SysOutSide.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
            #endregion
        }
        #endregion
    }
}
