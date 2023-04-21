using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.IO;

using CmUtils = CommonUtils;
using TUtils = idn.Skycic.Inventory.Utils;
using TJson = Newtonsoft.Json;

using idn.Skycic.Inventory.Common.Models;
using System.Collections;
namespace idn.Skycic.Inventory.Biz.Web.Controllers
{
    public class FileController : ApiControllerBase
    {
        public string CheckFolderExists(string path)
        {
            var strFolder = "";
            //var _path = "";
            if (!string.IsNullOrEmpty(path))
            {
                bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                }
                strFolder = path.Trim();
            }
            else
            {
                var strpath = "UploadedFiles";
                bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(strpath));
                if (!exists)
                {
                    System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(strpath));
                }
                strFolder = strpath.Trim();
            }
            return strFolder;
        }

        [AcceptVerbs("POST")]
        public string[] WA_UploadFileNew(RQ_File objRQ_File)
        {
            var array = new string[3];
            var str = "";

            try
            {
                var subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                var strFolder = objRQ_File.folderUpload + @"\" + subFolder;
                var strSubFolder = CheckFolderExists(strFolder);
                byte[] fileContent = Convert.FromBase64String(objRQ_File.uploadFileAsBase64String);
                var appPath = strSubFolder + "\\" + objRQ_File.fileName;
                str = appPath;
                appPath = HttpContext.Current.Server.MapPath(appPath);
                System.IO.File.WriteAllBytes(appPath, fileContent);
                array[0] = "true";
                array[1] = "Import file thành công!";
                array[2] = str;

            }
            catch (Exception ex)
            {
                array[0] = "false";
                array[1] = "Import file không thành công!";
                array[2] = ex.Message;
            }
            return array;
        }

        [AcceptVerbs("POST")]
        public string[] WA_MoveFileNew(RQ_File objRQ_File)
        {
            var array = new string[3];
            var str = "";

            try
            {
                //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                //WindowsIdentity idnt = new WindowsIdentity(username, password);
                //WindowsImpersonationContext context = idnt.Impersonate();

                var strSubFolder = CheckFolderExists(objRQ_File.folderUpload);
                objRQ_File.sourceFileName = HttpContext.Current.Server.MapPath(objRQ_File.sourceFileName);
                objRQ_File.sourceFileName.Replace(@"\10.Common\MyWebsites\", @"\");
                objRQ_File.destFileName = HttpContext.Current.Server.MapPath(objRQ_File.destFileName);
                objRQ_File.destFileName.Replace(@"\10.Common\MyWebsites\", @"\");
                str = strSubFolder + "ZZZZZ" + objRQ_File.sourceFileName + "ZZZZZ" + objRQ_File.destFileName;
                // Ensure that the target does not exist.
                if (File.Exists(objRQ_File.destFileName))
                    File.Delete(objRQ_File.destFileName);

                // Move the file.

                File.Move(objRQ_File.sourceFileName, objRQ_File.destFileName);
                //str = destFileName;
                array[0] = "true";
                array[1] = "Move file thành công!";
                array[2] = str;

            }
            catch (Exception ex)
            {
                array[0] = "false";
                array[1] = "Move file không thành công! AAA" + str;
                array[2] = ex.Message;
            }
            return array;
        }

        [AcceptVerbs("POST")]
        public string[] WA_DeleteFile(RQ_File objRQ_File)
        {
            var array = new string[3];
            var str = "";

            try
            {
                // Ensure that the target does not exist.
                if (File.Exists(objRQ_File.sourceFileName))
                    File.Delete(objRQ_File.sourceFileName);

                array[0] = "true";
                array[1] = "Delete file thành công!";
                array[2] = str;

            }
            catch (Exception ex)
            {
                array[0] = "false";
                array[1] = "Delete file không thành công! AAA" + str;
                array[2] = ex.Message;
            }
            return array;
        }


        [AcceptVerbs("POST")]
        public ServiceResult<RT_File> WA_UploadFile(RQ_File objRQ_File)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_File.Tid);
            RT_File objRT_File = new RT_File();
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_UploadFile";
            string strErrorCodeDefault = "WA_UploadFile";

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

                #region // Upload File:
                string str = "";
                var subFolder = DateTime.Now.ToString("yyyy-MM-dd");
                var strFolder = objRQ_File.folderUpload + @"\" + subFolder;
                var strSubFolder = CheckFolderExists(strFolder);
                byte[] fileContent = Convert.FromBase64String(objRQ_File.uploadFileAsBase64String);
                var appPath = strSubFolder + "\\" + objRQ_File.fileName;
                str = appPath;
                appPath = HttpContext.Current.Server.MapPath(appPath);
                System.IO.File.WriteAllBytes(appPath, fileContent);
                objRT_File.Status = "true";
                objRT_File.DescStatus = "Import file thành công!";
                objRT_File.AppPath = str;
                #endregion

                // Return Good:
                objRT_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
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
        public ServiceResult<RT_File> WA_MoveFile(RQ_File objRQ_File)
        {
            #region // Temp:
            DataSet mdsReturn = CmUtils.CMyDataSet.NewMyDataSet(objRQ_File.Tid);
            RT_File objRT_File = new RT_File();
            DateTime dtimeSys = DateTime.Now;
            string strControllerName = "WA_UploadFile";
            string strErrorCodeDefault = "WA_UploadFile";

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

                #region // Move:
                //AppDomain.CurrentDomain.SetPrincipalPolicy(PrincipalPolicy.WindowsPrincipal);
                //WindowsIdentity idnt = new WindowsIdentity(username, password);
                //WindowsImpersonationContext context = idnt.Impersonate();

                var strSubFolder = CheckFolderExists(objRQ_File.folderUpload);
                objRQ_File.sourceFileName = HttpContext.Current.Server.MapPath(objRQ_File.sourceFileName);
                objRQ_File.sourceFileName.Replace(@"\10.Common\MyWebsites\", @"\");
                objRQ_File.destFileName = HttpContext.Current.Server.MapPath(objRQ_File.destFileName);
                objRQ_File.destFileName.Replace(@"\10.Common\MyWebsites\", @"\");
                string str = strSubFolder + "ZZZZZ" + objRQ_File.sourceFileName + "ZZZZZ" + objRQ_File.destFileName;
                // objRT_File.AppPath = strSubFolder + "ZZZZZ" + objRQ_File.sourceFileName + "ZZZZZ" + objRQ_File.destFileName;
                // Ensure that the target does not exist.
                if (File.Exists(objRQ_File.destFileName))
                    File.Delete(objRQ_File.destFileName);

                // Move the file.

                File.Move(objRQ_File.sourceFileName, objRQ_File.destFileName);
                //str = destFileName;
                objRT_File.Status = "true";
                objRT_File.DescStatus = "Move file thành công!";
                objRT_File.AppPath = objRT_File.AppPath;
                #endregion

                // Return Good:
                objRT_File.c_K_DT_Sys = TUtils.CMyDataList.CProcessMyDS(mdsReturn);
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