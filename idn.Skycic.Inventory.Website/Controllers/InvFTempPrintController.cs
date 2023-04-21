using idn.Skycic.Inventory.ClientService;
using idn.Skycic.Inventory.ClientService.Services;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.ModelsUI;
using idn.Skycic.Inventory.Constants;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Website.Models;
using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class InvFTempPrintController : AdminBaseController
    {
        // GET: InvFTempPrint
        public ActionResult Index()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            
            #region ["UserState Common Info"]
            var waUserCode = CUtils.StrValueOrNull(UserState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValueOrNull(UserState.SysUser.UserPassword);
            var networkID = CUtils.StrValueOrNull(UserState.SysUser.NetworkID);
            var orgId = UserState.Mst_NNT.OrgID;
            var addressAPIs = CUtils.StrValueOrNull(UserState.AddressAPIs);
            #endregion
            try
            {
                #region Lấy loại mẫu in
                var lst_Mst_TempPrintType = new List<Mst_TempPrintType>();
                var objMst_TempPrintType = new RQ_Mst_TempPrintType()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    Rt_Cols_Mst_TempPrintType = "*",
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount
                };
                var rtMst_TempPrintType = MstTempPrintTypeService.Instance.WA_Mst_TempPrintType_Get(objMst_TempPrintType, addressAPIs);
                if (rtMst_TempPrintType != null && rtMst_TempPrintType.Lst_Mst_TempPrintType != null)
                {
                    lst_Mst_TempPrintType = rtMst_TempPrintType.Lst_Mst_TempPrintType;
                }
                ViewBag.Lst_Mst_TempPrintType = lst_Mst_TempPrintType;
                #endregion

                return View();
            }
            catch (ClientException ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return View(resultEntry);
        }

        [HttpPost]
        public ActionResult Save(string model)
        {
            var addressAPIs = UserState.AddressAPIs;
            var waUserCode = UserState.SysUser.UserCode;
            var waUserPassword = UserState.SysUser.UserPassword;
            var serverUrl = ReportBro_ServerUrl;
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            try
            {                
                var nnname = "";
                var objInvF_TempPrintUIInput = Newtonsoft.Json.JsonConvert.DeserializeObject<InvF_TempPrint>(model);
                
                objInvF_TempPrintUIInput.OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID);
                objInvF_TempPrintUIInput.NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID);
                objInvF_TempPrintUIInput.NNTName = nnname;

                
                //objInvF_TempPrintUIInput.IF_TempPrintNo = "PN.A4G.21613";//printTempCode;

                var objRQ_InvF_TempPrint = new RQ_InvF_TempPrint()
                {
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                    WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    FlagIsDelete = "0",
                    // RQ_InvF_TempPrint
                    Rt_Cols_InvF_TempPrint = null,
                    InvF_TempPrint = objInvF_TempPrintUIInput
                };
                //var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_InvF_TempPrint);
                InvFTempPrintService.Instance.WA_InvF_TempPrint_Save(objRQ_InvF_TempPrint, addressAPIs);
                exitsData = "Lưu mẫu in thành công!";
                resultEntry.Success = true;
                resultEntry.AddMessage(exitsData);
                return Json(resultEntry);
                #region

                #endregion

            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        [HttpPost]
        public ActionResult GetTempPrint(string tempprinttype)
        {
            var serverUrl = ReportBro_ServerUrl;
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);
            //ViewBag.Offset = Offset;
            if (!CUtils.IsNullOrEmpty(tempprinttype))
            {
                string linkPdf = "";
                string iF_TempPrintNo = "";
                string printTempBody = "";

                var listInvF_TempPrint = this.ListInvF_TempPrint(tempprinttype);
                if (listInvF_TempPrint != null && listInvF_TempPrint.Count > 0)
                {
                    var objInvF_TempPrint = listInvF_TempPrint[0];
                    iF_TempPrintNo = CUtils.StrValue(objInvF_TempPrint.IF_TempPrintNo);
                    printTempBody = CUtils.StrValue(objInvF_TempPrint.TempPrintBody);
                    dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                    var watermark = "";

                    var listMst_Part = new List<Mst_Part>();

                    var objMst_Part = new Mst_Part()
                    {
                        PartCode = "",
                        PartName = "",
                        PartNameFS = "",
                    };
                    listMst_Part.Add(objMst_Part);

                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.LogoFilePath))
                    {
                        string logofilepath = addressAPI + "api/File" + objInvF_TempPrint.LogoFilePath.ToString().Replace("\\", "/");
                        objInvF_TempPrint.LogoFilePath = logofilepath;
                    }
                    if (!CUtils.IsNullOrEmpty(objInvF_TempPrint.BackgroundFilePath))
                    {
                        string backgroundfilepath = addressAPI + "api/File" + objInvF_TempPrint.BackgroundFilePath.ToString().Replace("\\", "/");
                        objInvF_TempPrint.BackgroundFilePath = backgroundfilepath;
                    }
                    //objInvF_TempPrint.LogoFilePath = null;
                    data.data = CreateDataObjectReportServerTemp(objInvF_TempPrint, listMst_Part, ref watermark);
                    data.watermark = watermark;
                    var outputFormat = data.outputFormat;
                    if (CUtils.IsNullOrEmpty(outputFormat))
                    {
                        outputFormat = "pdf";
                    }
                    var content = PostReport(data);

                    if (!CUtils.IsNullOrEmpty(content))
                    {
                        content = CUtils.StrReplace(content, "\"", "");
                        if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                        {
                            var iReportBro_Key = ReportBro_Key.Length;
                            var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                            var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                            linkPdf = linkpdf + "#toolbar=0";
                        }
                    }

                }
                else
                {
                    iF_TempPrintNo = SeqNo(new Seq_Common()
                    {
                        SequenceType = Client_SequenceType.IFTEMPPRINTNO,
                        Param_Postfix = "",
                        Param_Prefix = ""
                    });

                    var listMst_TempPrintGroup = this.ListMst_TempPrintGroup(tempprinttype);

                    if (listMst_TempPrintGroup != null && listMst_TempPrintGroup.Count > 0)
                    {
                        var objMst_TempPrintGroup = listMst_TempPrintGroup[0];
                        printTempBody = CUtils.StrValue(objMst_TempPrintGroup.TempGrpPrintBody);
                        dynamic data = System.Web.Helpers.Json.Decode(printTempBody);
                        var watermark = "";

                        var listMst_Part = new List<Mst_Part>();

                        var objMst_Part = new Mst_Part()
                        {
                            PartCode = "",
                            PartName = "",
                            PartNameFS = "",
                        };
                        listMst_Part.Add(objMst_Part);

                        data.data = CreateDataObjectReportServerTemp(null, listMst_Part, ref watermark);
                        data.watermark = watermark;
                        var outputFormat = data.outputFormat;
                        if (CUtils.IsNullOrEmpty(outputFormat))
                        {
                            outputFormat = "pdf";
                        }
                        var content = PostReport(data);

                        if (!CUtils.IsNullOrEmpty(content))
                        {
                            content = CUtils.StrReplace(content, "\"", "");
                            if (content.IndexOf(ReportBro_Key, StringComparison.Ordinal) == 0)
                            {
                                var iReportBro_Key = ReportBro_Key.Length;
                                var key = content.Substring(iReportBro_Key, content.Length - iReportBro_Key);
                                var linkpdf = LinkFilePdf(serverUrl, key, outputFormat);
                                linkPdf = linkpdf + "#toolbar=0";
                            }
                        }
                    }
                }

                return Json(new { Success = true, LinkPDF = linkPdf, TempPrintBody = printTempBody, IF_TempPrintNo = iF_TempPrintNo });
            }
            return Json(new { Success = true, LinkPDF = "", TempPrintBody = "", IF_TempPrintNo="" });
        }

        [HttpPost]
        public ActionResult UploadFileServer(HttpPostedFileWrapper fileupload, string filetype, string filefolder = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            string url = System.Web.HttpContext.Current.Request.Url.AbsoluteUri;
            try
            {
                var fileType = "";
                var filePath = "";
                var folderPart = filefolder;
                var TenantIdName = "idocnet";
                string SubPath = "";
                if (CUtils.IsNullOrEmpty(folderPart))
                {
                    folderPart = "UploadFile";
                }
                var strFolder = @"~/" + TempFiles + "/" + TenantIdName + "/" + folderPart + "/" + GetNextTId();
                #region["Tạo folder và save file trong folder vừa tạo"]
                if (fileupload != null)
                {
                    var strFolderFile = strFolder + "-f";
                    CheckFolderExists(strFolderFile);
                    var fileName = fileupload.FileName;
                    var extension = System.IO.Path.GetExtension(fileName);
                    if (extension != null)
                    {
                        var ext = extension.Substring(1, extension.Trim().Length - 1).ToLower();
                        fileType = ext;
                        if (!FileUtils.listFileImageExt().Contains(ext))
                        {
                            resultEntry.Success = true;
                            resultEntry.AddMessage("File đính kèm không thuộc các định dạng: \" mdb | gif | jpg | jpeg | png\"");
                            return Json(resultEntry);
                        }
                    }
                    if (fileupload.ContentLength > FileUploadSize)
                    {
                        resultEntry.Success = true;
                        resultEntry.AddMessage("File đính kèm có dung lượng vượt quá 25MB!");
                        return Json(resultEntry);
                    }
                    filePath = strFolderFile.Replace("/", "\\") + "\\" + fileName;
                    FileUtils.SaveFile(fileupload, filePath);
                    var filePathCur = filePath.Replace("\\", "/");
                    filePathCur = filePathCur.Substring(1);
                    filePathCur = filePathCur.Replace("\\", "/");
                    //return Json(new { Success = true, FileName = fileName, FileType = fileType, FilePath = filePathCur.Replace("\\", "/") });
                    return Json(new { Success = true, FileName = fileName, FileType = fileType, FilePath = filePathCur, UrlFilePath = SubPath + filePathCur });
                }

                #endregion
                return JsonView("Index", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Index", null, resultEntry);
        }

        #region Save Mst_TempPrintGroup

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult CreateTempPrintGroup(HttpPostedFileWrapper file, string tempPrintType)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var addressAPIs = userState.AddressAPIs;
            var waUserCode = userState.SysUser.UserCode;
            var waUserPassword = userState.SysUser.UserPassword;
            var resultEntry = new JsonResultEntry() { Success = false };
            var exitsData = "";
            try
            {
                var filePath = "";                
                string fileId = Guid.NewGuid().ToString("D");
                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();
                if (ext.Equals(".rtmpl"))
                {
                    filePath = FileUtils.SaveTempFile(file, fileId, ext);                    
                }
                else
                {
                    exitsData = "File import không hợp lệ!";
                    resultEntry.AddMessage(exitsData);
                    return Json(resultEntry);
                }

                if (!CUtils.IsNullOrEmpty(filePath))
                {
                    using (StreamReader r = new StreamReader(filePath))
                    {
                        string json = r.ReadToEnd();
                        var contentFileDecrypt = CUtils.Decrypt(json);
                        dynamic data = System.Web.Helpers.Json.Decode(contentFileDecrypt.ToString());

                        var uiid = CUtils.StrValue(data.UserId);
                        var title = CUtils.StrValue(data.Title);
                        
                        var detail = data.Detail;
                        var watermark = "";
                        var objJson = new
                        {
                            outputFormat = "pdf",
                            isTestData = false,
                            data = CreateDataObjectReportServer(new List<Mst_Part>(), ref watermark),
                            watermark = watermark,
                            report = detail,
                        };
                        string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(objJson);               

                        var objMst_TempPrintGroupInput = new Mst_TempPrintGroup()
                        {
                            TempPrintType = CUtils.StrValue(tempPrintType),
                            NetworkID = CUtils.StrValue(userState.Mst_NNT.NetworkID),
                            TempGrpPrintBody = serializedObject,                            
                            FlagUpdloadLogoFilePathBase64 = "0",
                            FlagUpdloadBackgroundFilePathBase64 = "0"
                        };

                        var objRQ_Mst_TempPrintGroup = new RQ_Mst_TempPrintGroup()
                        {
                            Tid = GetNextTId(),
                            GwUserCode = GwUserCode,
                            GwPassword = GwPassword,
                            WAUserCode = CUtils.StrValue(UserState.SysUser.UserCode),
                            WAUserPassword = CUtils.StrValue(UserState.SysUser.UserPassword),
                            AccessToken = CUtils.StrValue(UserState.AccessToken),
                            NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                            OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                            Ft_RecordStart = Ft_RecordStart,
                            Ft_RecordCount = Ft_RecordCount,
                            FlagIsDelete = "0",

                            // RQ_Mst_TempPrintGroup
                            Rt_Cols_Mst_TempPrintGroup = null,
                            Mst_TempPrintGroup = objMst_TempPrintGroupInput
                        };
                        var dejson = Newtonsoft.Json.JsonConvert.SerializeObject(objRQ_Mst_TempPrintGroup);
                        Mst_TempPrintGroupService.Instance.WA_Mst_TempPrintGroup_Save(objRQ_Mst_TempPrintGroup, addressAPIs);
                        exitsData = "Tạo mẫu tem thành công!";
                        resultEntry.RedirectUrl = Url.Action("Index");
                    }
                }
                else
                {
                    exitsData = "Đường dẫn file mẫu tem trống!";
                }
                resultEntry.Success = true;

                resultEntry.AddMessage(exitsData);
                return Json(resultEntry);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("Create", null, resultEntry);
        }

        #endregion

        #region Common
        private List<Mst_TempPrintGroup> ListMst_TempPrintGroup(string tempprinttype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);

            var listMst_TempPrintGroup = new List<Mst_TempPrintGroup>();
            if (!CUtils.IsNullOrEmpty(tempprinttype))
            {
                var strWhereClauseMst_TempPrintGroup = "Mst_TempPrintGroup.TempPrintType = '" + tempprinttype + "'";

                var objRQ_Mst_TempPrintGroup = new RQ_Mst_TempPrintGroup()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseMst_TempPrintGroup,
                    Ft_Cols_Upd = null,
                    
                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    // RQ_Mst_TempPrintGroup
                    Rt_Cols_Mst_TempPrintGroup = "*",
                };

                var objRT_Mst_TempPrintGroupCur = Mst_TempPrintGroupService.Instance.WA_Mst_TempPrintGroup_Get(objRQ_Mst_TempPrintGroup, addressAPI);
                if (objRT_Mst_TempPrintGroupCur.Lst_Mst_TempPrintGroup != null && objRT_Mst_TempPrintGroupCur.Lst_Mst_TempPrintGroup.Count > 0)
                {
                    listMst_TempPrintGroup.AddRange(objRT_Mst_TempPrintGroupCur.Lst_Mst_TempPrintGroup);
                }
            }
            return listMst_TempPrintGroup;
        }

        private List<InvF_TempPrint> ListInvF_TempPrint(string tempprinttype)
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var waUserCode = CUtils.StrValue(userState.SysUser.UserCode);
            var waUserPassword = CUtils.StrValue(userState.SysUser.UserPassword);
            var addressAPI = CUtils.StrValue(userState.AddressAPIs);

            var listInvF_TempPrint = new List<InvF_TempPrint>();
            if (!CUtils.IsNullOrEmpty(tempprinttype))
            {
                var strWhereClauseInvF_TempPrint = "InvF_TempPrint.TempPrintType = '" + tempprinttype + "'";

                var objRQ_InvF_TempPrint = new RQ_InvF_TempPrint()
                {
                    // WARQBase
                    Tid = GetNextTId(),
                    GwUserCode = GwUserCode,
                    GwPassword = GwPassword,
                    WAUserCode = waUserCode,
                    WAUserPassword = waUserPassword,
                    AccessToken = CUtils.StrValue(UserState.AccessToken),
                    NetworkID = CUtils.StrValue(UserState.Mst_NNT.NetworkID),
                    OrgID = CUtils.StrValue(UserState.Mst_NNT.OrgID),
                    FuncType = null,
                    Ft_RecordStart = Ft_RecordStart,
                    Ft_RecordCount = Ft_RecordCount,
                    Ft_WhereClause = strWhereClauseInvF_TempPrint,
                    Ft_Cols_Upd = null,

                    UtcOffset = CUtils.StrValue(UserState.UtcOffset),
                    // RQ_InvF_TempPrint
                    Rt_Cols_InvF_TempPrint = "*",
                };

                var objRT_InvF_TempPrintCur = InvFTempPrintService.Instance.WA_InvF_TempPrint_Get(objRQ_InvF_TempPrint, addressAPI);
                if (objRT_InvF_TempPrintCur.Lst_InvF_TempPrint != null && objRT_InvF_TempPrintCur.Lst_InvF_TempPrint.Count > 0)
                {
                    listInvF_TempPrint.AddRange(objRT_InvF_TempPrintCur.Lst_InvF_TempPrint);
                }
            }
            return listInvF_TempPrint;
        }

        public string PostReport(dynamic data)
        {
            string url = ReportBro_ServerUrl + "rp/preview";
            string serializedObject = Newtonsoft.Json.JsonConvert.SerializeObject(data);
            Object myObject = new
            {
                json = serializedObject
            };
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(myObject);
            HttpWebRequest request = WebRequest.CreateHttp(url);
            request.Method = "Post";
            request.AllowWriteStreamBuffering = false;
            request.ContentType = "application/json";
            request.Accept = "Accept=application/json";
            request.SendChunked = true;
            request.ContentLength = serializedObject.Length;
            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(json);
            }
            var response = request.GetResponse() as HttpWebResponse;

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Now you have your response.
                //or false depending on information in the response
                return responseText;
            }
            return null;
        }

        public string LinkFilePdf(string serverurl, string key, string outputFormat)
        {
            var linkPdf = String.Format("{0}{1}/{2}?key={3}&outputFormat={4}", serverurl, "rp", "preview", key, outputFormat);
            return linkPdf;
        }

        #endregion
        #region[""]
        public dynamic CreateDataObjectReportServer(List<Mst_Part> listMst_Part, ref string watermark)
        {
            var MyDynamic = new ObjectReportServer()
            {
                DataTable = new List<Mst_Part_ReportServer>()
            };

            if (listMst_Part != null && listMst_Part.Count > 0)
            {
                var listMst_Part_ReportServer = new List<Mst_Part_ReportServer>();
                foreach (var item in listMst_Part)
                {
                    var objMst_Part_ReportServer = new Mst_Part_ReportServer();
                    objMst_Part_ReportServer.PartCode = CUtils.StrValue(item.PartCode);
                    objMst_Part_ReportServer.PartBarCode = CUtils.StrValue(item.PartBarCode);
                    objMst_Part_ReportServer.PartName = CUtils.StrValue(item.PartName);
                    objMst_Part_ReportServer.PartNameFS = CUtils.StrValue(item.PartNameFS);
                    objMst_Part_ReportServer.PartDesc = CUtils.StrValue(item.PartDesc);
                    objMst_Part_ReportServer.PartType = CUtils.StrValue(item.PartType);
                    objMst_Part_ReportServer.PMType = CUtils.StrValue(item.PMType);
                    objMst_Part_ReportServer.PartUnitCodeStd = CUtils.StrValue(item.PartUnitCodeStd);
                    objMst_Part_ReportServer.PartUnitCodeDefault = CUtils.StrValue(item.PartUnitCodeDefault);
                    objMst_Part_ReportServer.UPIn = CUtils.StrValue(item.UPIn);
                    objMst_Part_ReportServer.UPOut = CUtils.StrValue(item.UPOut);
                    objMst_Part_ReportServer.QtyEffMonth = CUtils.StrValue(item.QtyEffMonth);
                    objMst_Part_ReportServer.PartOrigin = CUtils.StrValue(item.PartOrigin);
                    objMst_Part_ReportServer.PartComponents = CUtils.StrValue(item.PartComponents);
                    objMst_Part_ReportServer.InstructionForUse = CUtils.StrValue(item.InstructionForUse);
                    objMst_Part_ReportServer.PartStorage = CUtils.StrValue(item.PartStorage);
                    objMst_Part_ReportServer.UrlMnfSequence = CUtils.StrValue(item.UrlMnfSequence);
                    objMst_Part_ReportServer.MnfStandard = CUtils.StrValue(item.MnfStandard);
                    objMst_Part_ReportServer.PartStyle = CUtils.StrValue(item.PartStyle);
                    objMst_Part_ReportServer.PartIntroduction = CUtils.StrValue(item.PartIntroduction);
                    //objMst_Part_ReportServer.LogLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT_TVAN_VN, offset);
                    objMst_Part_ReportServer.LogLUBy = CUtils.StrValue(item.LogLUBy);

                    listMst_Part_ReportServer.Add(objMst_Part_ReportServer);
                }

                MyDynamic.DataTable.AddRange(listMst_Part_ReportServer);
            }
            return MyDynamic;
        }

        public dynamic CreateDataObjectReportServerTemp(InvF_TempPrint objTempPrint, List<Mst_Part> listMst_Part, ref string watermark)
        {
            var MyDynamic = new ObjectReportServer();
            if(objTempPrint != null)
            {
                MyDynamic.LogoFilePath = CUtils.StrValue(objTempPrint.LogoFilePath);
                MyDynamic.BackgroundFilePath = CUtils.StrValue(objTempPrint.BackgroundFilePath);
            }
            
            MyDynamic.DataTable = new List<Mst_Part_ReportServer>();
            //var MyDynamic = new ObjectReportServer()
            //{
            //    DataTable = new List<Mst_Part_ReportServer>()
            //};
            if (listMst_Part != null && listMst_Part.Count > 0)
            {
                var listMst_Part_ReportServer = new List<Mst_Part_ReportServer>();
                foreach (var item in listMst_Part)
                {
                    var objMst_Part_ReportServer = new Mst_Part_ReportServer();
                    objMst_Part_ReportServer.PartCode = CUtils.StrValue(item.PartCode);
                    objMst_Part_ReportServer.PartBarCode = CUtils.StrValue(item.PartBarCode);
                    objMst_Part_ReportServer.PartName = CUtils.StrValue(item.PartName);
                    objMst_Part_ReportServer.PartNameFS = CUtils.StrValue(item.PartNameFS);
                    objMst_Part_ReportServer.PartDesc = CUtils.StrValue(item.PartDesc);
                    objMst_Part_ReportServer.PartType = CUtils.StrValue(item.PartType);
                    objMst_Part_ReportServer.PMType = CUtils.StrValue(item.PMType);
                    objMst_Part_ReportServer.PartUnitCodeStd = CUtils.StrValue(item.PartUnitCodeStd);
                    objMst_Part_ReportServer.PartUnitCodeDefault = CUtils.StrValue(item.PartUnitCodeDefault);
                    objMst_Part_ReportServer.UPIn = CUtils.StrValue(item.UPIn);
                    objMst_Part_ReportServer.UPOut = CUtils.StrValue(item.UPOut);
                    objMst_Part_ReportServer.QtyEffMonth = CUtils.StrValue(item.QtyEffMonth);
                    objMst_Part_ReportServer.PartOrigin = CUtils.StrValue(item.PartOrigin);
                    objMst_Part_ReportServer.PartComponents = CUtils.StrValue(item.PartComponents);
                    objMst_Part_ReportServer.InstructionForUse = CUtils.StrValue(item.InstructionForUse);
                    objMst_Part_ReportServer.PartStorage = CUtils.StrValue(item.PartStorage);
                    objMst_Part_ReportServer.UrlMnfSequence = CUtils.StrValue(item.UrlMnfSequence);
                    objMst_Part_ReportServer.MnfStandard = CUtils.StrValue(item.MnfStandard);
                    objMst_Part_ReportServer.PartStyle = CUtils.StrValue(item.PartStyle);
                    objMst_Part_ReportServer.PartIntroduction = CUtils.StrValue(item.PartIntroduction);
                    //objMst_Part_ReportServer.LogLUDTimeUTC = CUtils.ReturnDateTimeUtcToLocalTime(CUtils.StrValue(item.LogLUDTimeUTC), Nonsense.DATE_TIME_FORMAT_TVAN_VN, offset);
                    objMst_Part_ReportServer.LogLUBy = CUtils.StrValue(item.LogLUBy);
                    
                    listMst_Part_ReportServer.Add(objMst_Part_ReportServer);
                }

                MyDynamic.DataTable.AddRange(listMst_Part_ReportServer);
                
            }
            return MyDynamic;
        }
        #endregion
    }
}