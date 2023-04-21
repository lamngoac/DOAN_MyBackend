using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;
using TConst = idn.Skycic.Inventory.Constants;

using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class OS_FileController : ApiControllerBase
    {
        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_File> WA_OS_UploadFile(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_File.Tid);
            RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_UploadFile";
            string strErrorCodeDefault = "WA_OS_PrdCenter_UploadFile";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_UploadFile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File // objRQ_OS_PrdCenter_File
                                              // //
                    , out objRT_OS_PrdCenter_File // objRT_OS_PrdCenter_File
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_PrdCenter_File.AppPath = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsReturn));
                objRT_OS_PrdCenter_File.Status = "true";
                objRT_OS_PrdCenter_File.DescStatus = "Import file thành công!";
                return Success<RT_OS_PrdCenter_File>(objRT_OS_PrdCenter_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_PrdCenter_File == null) objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
                objRT_OS_PrdCenter_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_File>(ex, objRT_OS_PrdCenter_File);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_File> WA_OS_MoveFile(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_File.Tid);
            RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_MoveFile";
            string strErrorCodeDefault = "WA_OS_PrdCenter_MoveFile";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_MoveFile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File // objRQ_OS_PrdCenter_File
                                              // //
                    , out objRT_OS_PrdCenter_File // objRT_OS_PrdCenter_File
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_PrdCenter_File.AppPath = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsReturn));
                objRT_OS_PrdCenter_File.Status = "true";
                objRT_OS_PrdCenter_File.DescStatus = "Move file thành công!";
                return Success<RT_OS_PrdCenter_File>(objRT_OS_PrdCenter_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_PrdCenter_File == null) objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
                objRT_OS_PrdCenter_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_File>(ex, objRT_OS_PrdCenter_File);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_File> WA_OS_DeleteFile(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_File.Tid);
            RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_PrdCenter_DeleteFile";
            string strErrorCodeDefault = "WA_OS_PrdCenter_DeleteFile";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_PrdCenter_DeleteFile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File // objRQ_OS_PrdCenter_File
                                              // //
                    , out objRT_OS_PrdCenter_File // objRT_OS_PrdCenter_File
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_OS_PrdCenter_File.AppPath = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsReturn));
                objRT_OS_PrdCenter_File.Status = "true";
                objRT_OS_PrdCenter_File.DescStatus = "Delete file thành công!";
                return Success<RT_OS_PrdCenter_File>(objRT_OS_PrdCenter_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_PrdCenter_File == null) objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
                objRT_OS_PrdCenter_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_File>(ex, objRT_OS_PrdCenter_File);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_OS_PrdCenter_File> WA_OS_GetFileFromPath(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_OS_PrdCenter_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_OS_PrdCenter_File.Tid);
            RT_OS_PrdCenter_File objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_GetFileFromPath";
            string strErrorCodeDefault = "WA_OS_GetFileFromPath";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_OS_PrdCenter_File.GwUserCode // strGwUserCode
                    , objRQ_OS_PrdCenter_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                string strAddressUrl = "";
                if(CmUtils.StringUtils.StringEqualIgnoreCase(objRQ_OS_PrdCenter_File.UrlType, TConst.UrlType.UrlPrdCenter))
                {
                   // strAddressUrl = TConst.BizidNInventoryAPIAddress.BaseBizidNInventoryAPIAddress;
                }
                else if (CmUtils.StringUtils.StringEqualIgnoreCase(objRQ_OS_PrdCenter_File.UrlType, TConst.UrlType.UrlMstSvPrdCenter))
                {
                    strAddressUrl = TConst.BizMasterServerPrdCenterAPIAddress.BaseBizMasterServerAPIAddress;
                }
                else if (CmUtils.StringUtils.StringEqualIgnoreCase(objRQ_OS_PrdCenter_File.UrlType, TConst.UrlType.UrlMstSvSolution))
                {
                    strAddressUrl = TConst.BizMasterServerSolutionAPIAddress.BaseBizMasterServerSolutionAPIAddress;
                }
                #endregion

                // Return Good:
                objRT_OS_PrdCenter_File.UrlPath = string.Format("{0}{1}/{2}", strAddressUrl.Trim(), "api", "File");
                objRT_OS_PrdCenter_File.UrlType = objRQ_OS_PrdCenter_File.UrlType;
                objRT_OS_PrdCenter_File.Status = "true";
                return Success<RT_OS_PrdCenter_File>(objRT_OS_PrdCenter_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_OS_PrdCenter_File == null) objRT_OS_PrdCenter_File = new RT_OS_PrdCenter_File();
                objRT_OS_PrdCenter_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_OS_PrdCenter_File>(ex, objRT_OS_PrdCenter_File);
                #endregion
            }
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_File> WA_OS_DMS_UploadFile(RQ_File objRQ_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_File.Tid);
            RT_File objRT_File = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_DMS_UploadFile";
            string strErrorCodeDefault = "WA_OS_DMS_UploadFile";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_File.GwUserCode // strGwUserCode
                    , objRQ_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_DMS_UploadFile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_File // objRQ_File
                                 // //
                    , out objRT_File // objRT_File
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_File.AppPath = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsReturn));
                objRT_File.Status = "true";
                objRT_File.DescStatus = "Import file thành công!";
                return Success<RT_File>(objRT_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_File == null) objRT_File = new RT_File();
                objRT_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_File>(ex, objRT_File);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_File> WA_OS_DMS_MoveFile(RQ_File objRQ_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_File.Tid);
            RT_File objRT_File = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_DMS_MoveFile";
            string strErrorCodeDefault = "WA_OS_DMS_MoveFile";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_File.GwUserCode // strGwUserCode
                    , objRQ_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_DMS_MoveFile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_File // objRQ_File
                                 // //
                    , out objRT_File // objRT_File
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_File.AppPath = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsReturn));
                objRT_File.Status = "true";
                objRT_File.DescStatus = "Move file thành công!";
                return Success<RT_File>(objRT_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_File == null) objRT_File = new RT_File();
                objRT_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_File>(ex, objRT_File);
                #endregion
            }
        }

        [AcceptVerbs("POST")]
        public ServiceResult<RT_File> WA_OS_DMS_DeleteFile(RQ_File objRQ_File)
        {
            #region // Temp:
            if (_mdsInitError != null) return ProcessMyDSInitError<RT_File>();
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_File.Tid);
            RT_File objRT_File = null;
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_OS_DMS_DeleteFile";
            string strErrorCodeDefault = "WA_OS_DMS_DeleteFile";

            ArrayList alParamsCoupleError = new ArrayList();
            alParamsCoupleError.AddRange(new object[] {
                "strControllerName", strControllerName
                , "dtimeSys", dtimeSys.ToString("yyyy-MM-dd HH:mm:ss")
				//, "_biz._cf.nvcParams", TJson.JsonConvert.SerializeObject(_biz._cf.nvcParams)
				//, "_biz.RQ_Mst_Brand", TJson.JsonConvert.SerializeObject(objRQ_Mst_Brand)
				////
				});
            #endregion

            try
            {
                #region // CheckGatewayAuthentication:
                TUtils.CConnectionManager.CheckGatewayAuthentication(
                    _biz._cf.nvcParams // nvcParams
                    , ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_File.GwUserCode // strGwUserCode
                    , objRQ_File.GwPassword // strGwPassword
                    );
                #endregion

                #region // Process:
                //
                mdsReturn = _biz.WAS_OS_DMS_DeleteFile(
                    ref alParamsCoupleError // alParamsCoupleError
                    , objRQ_File // objRQ_File
                                 // //
                    , out objRT_File // objRT_File
                    );

                if (CmUtils.CMyDataSet.HasError(mdsReturn))
                {
                    throw CmUtils.CMyException.Raise(
                        (string)CmUtils.CMyDataSet.GetErrorCode(mdsReturn)
                        , null
                        , null
                        );
                }
                #endregion

                // Return Good:
                objRT_File.AppPath = Convert.ToString(CmUtils.CMyDataSet.GetRemark(mdsReturn));
                objRT_File.Status = "true";
                objRT_File.DescStatus = "Delete file thành công!";
                return Success<RT_File>(objRT_File);
            }
            catch (Exception ex)
            {
                #region // Catch of try:
                ////
                TUtils.CProcessExc.Process(
                    ref mdsReturn // mdsFinal
                    , ex // exc
                    , strErrorCodeDefault // strErrorCode
                    , alParamsCoupleError.ToArray() // arrobjErrorParams
                    );

                // Return Bad:
                if (objRT_File == null) objRT_File = new RT_File();
                objRT_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
                return Error<RT_File>(ex, objRT_File);
                #endregion
            }
        }
    }
}