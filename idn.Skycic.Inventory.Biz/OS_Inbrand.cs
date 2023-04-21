using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Globalization;
using System.Net;
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
using idn.Skycic.Inventory.BizService.Services;

namespace idn.Skycic.Inventory.Biz
{
    public partial class BizidNInventory
    {

        #region // OS_MstSvDMS_MstSv_Mst_Network_GetByMST:
        public void OS_MstSvInBrand_MstSv_Mst_Network_GetByMST(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            //, string strWAUserPassword
            , string strNetworkID
            , string strOrgID
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strMST
            , out string strUrl_OS_SkycicInBrand_AllNetWork
            )
        {
            #region // Temp:
            string strFunctionName = "OS_MstSvInBrand_MstSv_Mst_Network_GetByMST";
            string strErrorCodeDefault = TError.ErridnInventory.OS_MstSvInBrand_MstSv_Mst_Network_GetByMST;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				//, "strWAUserPassword", strWAUserPassword
				, "strNetworkID", strNetworkID
                ////
                , "strMST", strMST
                });
            #endregion

            #region // Refine and check:
            string strOS_MasterServer_SkycicDMS_GwUserCode = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwUserCode"];
            string strOS_MasterServer_SkycicDMS_GwPassword = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwPassword"];
            string strOS_MasterServer_SkycicDMS_API_Url = _cf.nvcParams["OS_MasterServer_SkycicInBrand_API_Url"];
            /////
            alParamsCoupleError.AddRange(new object[]{
                "Check.strOS_MasterServer_SkycicDMS_API_Url", strOS_MasterServer_SkycicDMS_API_Url
                });
            #endregion

            #region // Call Func:
            RT_OS_MstSvInBrand_MstSv_Mst_Network objRT_OS_MstSvDMS_MstSv_Mst_Network = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                OS_MstSvInBrand_MstSv_Mst_Network objOS_MstSvInBrand_MstSv_Mst_Network = new OS_MstSvInBrand_MstSv_Mst_Network();
                objOS_MstSvInBrand_MstSv_Mst_Network.MST = strMST;
                /////
                RQ_OS_MstSvInBrand_MstSv_Mst_Network objRQ_OS_MstSvDMS_MstSv_Mst_Network = new RQ_OS_MstSvInBrand_MstSv_Mst_Network()
                {
                    MstSv_Mst_Network = objOS_MstSvInBrand_MstSv_Mst_Network,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = nNetworkID.ToString(),
                    OrgID = strOrgID,
                    AccessToken = strAccessToken,
                    GwUserCode = strOS_MasterServer_SkycicDMS_GwUserCode,
                    GwPassword = strOS_MasterServer_SkycicDMS_GwPassword,
                    WAUserCode = strOS_MasterServer_Solution_WAUserCode,
                    //WAUserPassword = strAccessToken
                };
                ////
                try
                {
                    objRT_OS_MstSvDMS_MstSv_Mst_Network = OS_MstSvInBrand_MstSv_Mst_NetworkService.Instance.WA_OS_MstSvInBrand_MstSv_Mst_Network_GetByMST(objRQ_OS_MstSvDMS_MstSv_Mst_Network, strOS_MasterServer_SkycicDMS_API_Url);
                    ////
                }
                catch (Exception cex)
                {
                    string strErrorCodeOS = null;

                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        , out strErrorCodeOS
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INBRAND" + "." + strErrorCodeOS
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion

            #region // Get Remark:
            strUrl_OS_SkycicInBrand_AllNetWork = objRT_OS_MstSvDMS_MstSv_Mst_Network.c_K_DT_Sys.Lst_c_K_DT_SysInfo[0].Remark;
            //CmUtils.CMyDataSet.SetRemark(ref mdsFinal, strResult);
            #endregion
        }
        #endregion

        #region // Inv_InventoryVerifiedID:
        public void OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            //, string strWAUserPassword
            , string strNetworkID
            , string strOrgID
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            ////
            , string strMST
            ////
            , object objIF_InvInNo
            /////
            , DataSet dsData
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn";
            string strErrorCodeDefault = TError.ErridnInventory.OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				//, "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                ////
                , "objIF_InvReturnNo", objIF_InvInNo
                //, "strUrl_OS_TVAN_AllNetWork", strUrl_OS_TVAN_AllNetWork
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and check Input:
            string strIF_InvInNo = TUtils.CUtils.StdParam(objIF_InvInNo);
            #endregion

            #region // OS_MstSvDMS_MstSv_Mst_Network_GetByMST:
            string strUrl_OS_Inv_AllNetWork = "";
            {
                OS_MstSvInBrand_MstSv_Mst_Network_GetByMST(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                                    //, strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                    , strOrgID // strOrgID
                    , strAccessToken // strAccessToken
                                     ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strMST // strMST
                    , out strUrl_OS_Inv_AllNetWork // strUrl_OS_SkycicDMS_AllNetWork
                    );
                ////
                if (string.IsNullOrEmpty(strUrl_OS_Inv_AllNetWork))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.MST", strMST
                        , "Check.strUrl_OS_Inv_AllNetWork", strUrl_OS_Inv_AllNetWork
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.OSInBrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn_InvalidUrl_OS_Inv_AllNetWork
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }

            }
            #endregion

            #region // Refine and Check Input:
            OS_InBrand_Inv_InventoryVerifiedID objOS_InBrand_Inv_InventoryVerifiedID = new OS_InBrand_Inv_InventoryVerifiedID();
            //List<OSDMS_InvF_InventoryCusReturn> Lst_InvF_InventoryCusReturn = new List<OSDMS_InvF_InventoryCusReturn>();
            List<OS_InBrand_Inv_InventoryVerifiedID> Lst_OS_InBrand_Inv_InventoryVerifiedID = new List<OS_InBrand_Inv_InventoryVerifiedID>();
            ////
            {
                ////
                objOS_InBrand_Inv_InventoryVerifiedID.IF_InvInNo = strIF_InvInNo;
                objOS_InBrand_Inv_InventoryVerifiedID.OrgID = strOrgID;
                /////
                DataTable dtInput_Inv_InventoryVerifiedID = dsData.Tables["Inv_InventoryVerifiedID"];
                Lst_OS_InBrand_Inv_InventoryVerifiedID = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryVerifiedID>(dtInput_Inv_InventoryVerifiedID);
                ////

            }
            #endregion

            #region // Call Func:
            RT_OS_InBrand_Inv_InventoryVerifiedID objRT_OS_InBrand_Inv_InventoryVerifiedID = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                /////
                RQ_OS_InBrand_Inv_InventoryVerifiedID objRQ_OS_InBrand_Inv_InventoryVerifiedID = new RQ_OS_InBrand_Inv_InventoryVerifiedID()
                {
                    Inv_InventoryVerifiedID = objOS_InBrand_Inv_InventoryVerifiedID,
                    Lst_Inv_InventoryVerifiedID = Lst_OS_InBrand_Inv_InventoryVerifiedID,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = strNetworkID,
                    OrgID = strOrgID,
                    AccessToken = strAccessToken,
                    GwUserCode = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwUserCode"],
                    GwPassword = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwPassword"],
                    WAUserCode = strWAUserCode,
                    //WAUserPassword = strWAUserPassword
                };
                ////
                try
                {
                    string Json = TJson.JsonConvert.SerializeObject(objRQ_OS_InBrand_Inv_InventoryVerifiedID);
                    objRT_OS_InBrand_Inv_InventoryVerifiedID = OS_Inbrand_Inv_InventoryVerifiedIDService.Instance.WA_OS_Inbrand_Inv_InventoryVerifiedID_AddMulti_NoGenInvFIn(strUrl_OS_Inv_AllNetWork, objRQ_OS_InBrand_Inv_InventoryVerifiedID);

                }
                catch (Exception cex)
                {
                    string strErrorCodeOS = null;

                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        , out strErrorCodeOS
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INBRAND" + "." + strErrorCodeOS
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion
        }

        public void OSInBrand_Inv_InventoryVerifiedID_OutInv(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            //, string strWAUserPassword
            , string strNetworkID
            , string strOrgID
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            /////
            , string strMST
            ////
            , object objIF_InvOutNo
            , object objFormOutType
            , object objPlateNo
            , object objMoocNo
            , object objDriverName
            , object objDriverPhoneNo
            , object objCustomerCode
            , object objOrgID_Customer
            , object objCustomerName
            , object objCustomerAddress
            , object objRemark
            /////
            , DataSet dsData
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "OSInBrand_Inv_InventoryVerifiedID_OutInv";
            string strErrorCodeDefault = TError.ErridnInventory.OSInBrand_Inv_InventoryVerifiedID_OutInv;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				//, "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                /////
                , "strMST", strMST
                ////
                , "objIF_InvOutNo", objIF_InvOutNo
                , "objFormOutType", objFormOutType
                , "objPlateNo", objPlateNo
                , "objMoocNo", objMoocNo
                , "objDriverName", objDriverName
                , "objDriverPhoneNo", objDriverPhoneNo
                , "objCustomerCode", objCustomerCode
                , "objOrgID_Customer", objOrgID_Customer
                , "objCustomerName", objCustomerName
                , "objCustomerAddress", objCustomerAddress
                , "objRemark", objRemark
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and check Input:
            string strIF_InvOutNo = TUtils.CUtils.StdParam(objIF_InvOutNo);
            string strFormOutType = TUtils.CUtils.StdParam(objFormOutType);
            string strPlateNo = string.Format("{0}", objPlateNo);
            string strMoocNo = string.Format("{0}", objMoocNo);
            string strDriverName = string.Format("{0}", objDriverName);
            string strDriverPhoneNo = string.Format("{0}", objDriverPhoneNo);
            string strCustomerCode = TUtils.CUtils.StdParam(objCustomerCode);
            string strOrgID_Customer = TUtils.CUtils.StdParam(objOrgID_Customer);
            string strCustomerName = string.Format("{0}", objCustomerName);
            string strCustomerAddress = string.Format("{0}", objCustomerAddress);
            string strRemark = string.Format("{0}", objRemark);
            #endregion

            #region // OS_MstSvDMS_MstSv_Mst_Network_GetByMST:
            string strUrl_OS_Inv_AllNetWork = "";
            {
                OS_MstSvInBrand_MstSv_Mst_Network_GetByMST(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                                    //, strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                    , strOrgID // strOrgID
                    , strAccessToken // strAccessToken
                                     ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strMST // strMST
                    , out strUrl_OS_Inv_AllNetWork // strUrl_OS_SkycicDMS_AllNetWork
                    );
                ////
                if (string.IsNullOrEmpty(strUrl_OS_Inv_AllNetWork))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.MST", strMST
                        , "Check.strUrl_OS_Inv_AllNetWork", strUrl_OS_Inv_AllNetWork
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.OSInBrand_Inv_InventoryVerifiedID_OutInv_InvalidUrl_OS_Inv_AllNetWork
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }

            }
            #endregion

            #region // Refine and Check Input:
            OS_InBrand_Inv_InventoryVerifiedID objOS_InBrand_Inv_InventoryVerifiedID = new OS_InBrand_Inv_InventoryVerifiedID();
            //List<OSDMS_InvF_InventoryCusReturn> Lst_InvF_InventoryCusReturn = new List<OSDMS_InvF_InventoryCusReturn>();
            List<OS_InBrand_Inv_InventoryVerifiedID> Lst_OS_InBrand_Inv_InventoryVerifiedID = new List<OS_InBrand_Inv_InventoryVerifiedID>();
            List<OS_InBrand_Inv_InventoryGenBox> Lst_OS_InBrand_Inv_InventoryGenBox = new List<OS_InBrand_Inv_InventoryGenBox>();
            List<OS_InBrand_Inv_InventoryGenCarton> Lst_OS_InBrand_Inv_InventoryGenCarton = new List<OS_InBrand_Inv_InventoryGenCarton>();
            ////
            {
                ////
                DataTable dtInput_Inv_InventoryVerifiedID = dsData.Tables["Inv_InventoryVerifiedID"];
                Lst_OS_InBrand_Inv_InventoryVerifiedID = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryVerifiedID>(dtInput_Inv_InventoryVerifiedID);
                ////
                DataTable dtInput_Inv_InventoryGenBox = dsData.Tables["Inv_InventoryGenBox"];
                Lst_OS_InBrand_Inv_InventoryGenBox = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryGenBox>(dtInput_Inv_InventoryGenBox);
                ////
                DataTable dtInput_Inv_InventoryGenCarton = dsData.Tables["Inv_InventoryGenCan"];
                Lst_OS_InBrand_Inv_InventoryGenCarton = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryGenCarton>(dtInput_Inv_InventoryGenCarton);
                ////

            }
            #endregion

            #region // Call Func:
            RT_OS_InBrand_Inv_InventoryVerifiedID_OutInv objRT_OS_InBrand_Inv_InventoryVerifiedID_OutInv = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                /////
                RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv objRQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv = new RQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv()
                {
                    IF_InvOutNo = strIF_InvOutNo,
                    InvFOutType = strFormOutType,
                    PlateNo = strPlateNo,
                    MoocNo = strMoocNo,
                    DriverName = strDriverName,
                    DriverPhoneNo = strDriverPhoneNo,
                    CustomerCode = strCustomerCode,
                    OrgID_Customer = strOrgID_Customer,
                    CustomerName = strCustomerName,
                    Remark = strRemark,
                    CustomerAddress = strCustomerAddress, 
                    Lst_Inv_InventoryVerifiedID = Lst_OS_InBrand_Inv_InventoryVerifiedID,
                    Lst_Inv_InventoryGenBox = Lst_OS_InBrand_Inv_InventoryGenBox,
                    Lst_Inv_InventoryGenCarton = Lst_OS_InBrand_Inv_InventoryGenCarton,
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = strNetworkID,
                    OrgID = strOrgID,
                    AccessToken = strAccessToken,
                    GwUserCode = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwUserCode"],
                    GwPassword = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwPassword"],
                    WAUserCode = strWAUserCode,
                    //WAUserPassword = strWAUserPassword
                };
                ////
                try
                {

                    objRT_OS_InBrand_Inv_InventoryVerifiedID_OutInv = OS_Inbrand_Inv_InventoryVerifiedIDService.Instance.WA_OS_InBrand_Inv_InventoryVerifiedID_OutInv(strUrl_OS_Inv_AllNetWork, objRQ_OS_InBrand_Inv_InventoryVerifiedID_OutInv);

                }
                catch (Exception cex)
                {
                    string strErrorCodeOS = null;

                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        , out strErrorCodeOS
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INBRAND" + "." + strErrorCodeOS
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion
        }


        public void OSInBrand_Inv_InventoryGenID_CheckListDB(
            string strTid
            , string strGwUserCode
            , string strGwPassword
            , string strWAUserCode
            //, string strWAUserPassword
            , string strNetworkID
            , string strOrgID
            , string strAccessToken
            , ref ArrayList alParamsCoupleError
            ////
            , DateTime dtimeSys
            , object objMST
            /////
            , object objFlagExistToCheck
            , object objFlagMapListToCheck
            , object objFlagUsedListToCheck
            /////
            , DataSet dsData
            ////
            )
        {
            #region // Temp:
            string strFunctionName = "OSInBrand_Inv_InventoryGenID_CheckListDB";
            string strErrorCodeDefault = TError.ErridnInventory.OSInBrand_Inv_InventoryGenID_CheckListDB;
            alParamsCoupleError.AddRange(new object[]{
                "strFunctionName", strFunctionName
                , "strErrorCodeDefault", strErrorCodeDefault
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				////
				, "strWAUserCode", strWAUserCode
				//, "strWAUserPassword", strWAUserPassword
                , "strNetworkID", strNetworkID
                /////
                , "objFlagExistToCheck", objFlagExistToCheck
                , "objFlagMapListToCheck", objFlagMapListToCheck
                , "objFlagUsedListToCheck", objFlagUsedListToCheck
                ////
                });
            #endregion

            #region // Convert Input:
            //DataSet dsData = TUtils.CUtils.StdDS(arrobjDSData);
            //if (dsData == null) dsData = new DataSet("dsData");
            //dsData.AcceptChanges();
            alParamsCoupleError.AddRange(new object[]{
                "Check.dsData", CmUtils.XmlUtils.DataSet2XmlSimple(dsData)
                });
            #endregion

            #region // Refine and check Input:
            string strFlagExistToCheck = TUtils.CUtils.StdFlag(objFlagExistToCheck);
            string strFlagMapListToCheck = TUtils.CUtils.StdFlag(objFlagMapListToCheck);
            string strFlagUsedListToCheck = TUtils.CUtils.StdFlag(objFlagUsedListToCheck);
            string strMST = TUtils.CUtils.StdParam(objMST);
            #endregion

            #region // OS_MstSvDMS_MstSv_Mst_Network_GetByMST:
            string strUrl_OS_Inv_AllNetWork = "";
            {
                OS_MstSvInBrand_MstSv_Mst_Network_GetByMST(
                    strTid // strTid
                    , strGwUserCode // strGwUserCode
                    , strGwPassword // strGwPassword
                    , strWAUserCode // strWAUserCode
                                    //, strWAUserPassword // strWAUserPassword
                    , strNetworkID // strNetworkID
                    , strOrgID // strOrgID
                    , strAccessToken // strAccessToken
                                     ////
                    , ref alParamsCoupleError // alParamsCoupleError
                    , dtimeSys // dtimeSys
                    , strMST // strMST
                    , out strUrl_OS_Inv_AllNetWork // strUrl_OS_SkycicDMS_AllNetWork
                    );
                ////
                if (string.IsNullOrEmpty(strUrl_OS_Inv_AllNetWork))
                {
                    alParamsCoupleError.AddRange(new object[]{
                        "Check.MST", strMST
                        , "Check.strUrl_OS_Inv_AllNetWork", strUrl_OS_Inv_AllNetWork
                        });
                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.OSInBrand_Inv_InventoryGenID_CheckListDB_InvalidUrl_OS_Inv_AllNetWork
                        , null
                        , alParamsCoupleError.ToArray()
                        );

                }
                /////
                alParamsCoupleError.AddRange(new object[]{
                    "Check.MST", strMST
                    , "Check.strUrl_OS_Inv_AllNetWork", strUrl_OS_Inv_AllNetWork
                    });
                /////
            }
            #endregion

            #region // Refine and Check Input:
            OS_InBrand_Inv_InventoryGenID objOS_InBrand_Inv_InventoryGenID = new OS_InBrand_Inv_InventoryGenID();
            //List<OSDMS_InvF_InventoryCusReturn> Lst_InvF_InventoryCusReturn = new List<OSDMS_InvF_InventoryCusReturn>();
            List<OS_InBrand_Inv_InventoryGenID> Lst_Inv_InventoryGenID = new List<OS_InBrand_Inv_InventoryGenID>();
            List<OS_InBrand_Inv_InventoryGenBox> Lst_Inv_InventoryGenBox = new List<OS_InBrand_Inv_InventoryGenBox>();
            List<OS_InBrand_Inv_InventoryGenCarton> Lst_Inv_InventoryGenCarton = new List<OS_InBrand_Inv_InventoryGenCarton>();
            ////
            {
                ////
                DataTable dtInput_Inv_InventoryGenID = dsData.Tables["Inv_InventoryGenID"];
                Lst_Inv_InventoryGenID = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryGenID>(dtInput_Inv_InventoryGenID);
                /////
                DataTable dtInput_Inv_InventoryGenBox = dsData.Tables["Inv_InventoryGenBox"];
                Lst_Inv_InventoryGenBox = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryGenBox>(dtInput_Inv_InventoryGenBox);
                ////
                DataTable dtInput_Inv_InventoryGenCarton = dsData.Tables["Inv_InventoryGenCarton"];
                Lst_Inv_InventoryGenCarton = TUtils.DataTableCmUtils.ToListof<OS_InBrand_Inv_InventoryGenCarton>(dtInput_Inv_InventoryGenCarton);
                ////

            }
            #endregion

            #region // Call Func:
            RT_OS_InBrand_Inv_InventoryGenInv objRT_OS_InBrand_Inv_InventoryVerifiedID = null;
            {
                #region // WA_MstSv_Mst_Network_GetByMST:
                /////
                RQ_OS_InBrand_Inv_InventoryGenInv objRQ_OS_InBrand_Inv_InventoryVerifiedID = new RQ_OS_InBrand_Inv_InventoryGenInv()
                {
                    FlagExistToCheck = strFlagExistToCheck,
                    FlagMapListToCheck = strFlagMapListToCheck,
                    FlagUsedListToCheck = strFlagUsedListToCheck,
                    ////
                    Lst_Inv_InventoryGenID = Lst_Inv_InventoryGenID,
                    Lst_Inv_InventoryGenBox = Lst_Inv_InventoryGenBox,
                    Lst_Inv_InventoryGenCarton = Lst_Inv_InventoryGenCarton,
                    ////
                    Tid = strTid,
                    TokenID = strOS_MasterServer_Solution_TokenID,
                    NetworkID = strNetworkID,
                    OrgID = strOrgID,
                    AccessToken = strAccessToken,
                    GwUserCode = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwUserCode"],
                    GwPassword = _cf.nvcParams["OS_MasterServer_SkycicInBrand_GwPassword"],
                    WAUserCode = strWAUserCode,
                    //WAUserPassword = strWAUserPassword
                };
                ////
                try
                {

                    objRT_OS_InBrand_Inv_InventoryVerifiedID = OS_Inbrand_Inv_InventoryVerifiedIDService.Instance.WA_OS_InBrand_Inv_InventoryGenInv_CheckListDB(strUrl_OS_Inv_AllNetWork, objRQ_OS_InBrand_Inv_InventoryVerifiedID);

                }
                catch (Exception cex)
                {
                    string strErrorCodeOS = null;

                    TUtils.CProcessExc.BizShowException(
                        ref alParamsCoupleError // alParamsCoupleError
                        , cex // cex
                        , out strErrorCodeOS
                        );

                    throw CmUtils.CMyException.Raise(
                        TError.ErridnInventory.CmSys_InvalidOutSite + "." + "INBRAND" + "." + strErrorCodeOS
                        , null
                        , alParamsCoupleError.ToArray()
                        );
                }
                ////
                #endregion
            }
            #endregion
        }
        #endregion
    }
}
