using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.WebAdmin.Models;
using idn.Skycic.Inventory.WebAdmin.Utils;

namespace idn.Skycic.Inventory.WebAdmin.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var userState = this.UserState;
            Session["UserState"] = userState;
            var userName = "";
            DateTimeServerBizUTC();
            if (userState != null && !CUtils.IsNullOrEmpty(userState.UserName))
            {
                userName = userState.UserName.Trim();
            }
            ViewBag.UserName = userName;
            return View();
        }

        #region["Import Excel"]
        [HttpPost]
        public ActionResult ShowPopupImportExcel()
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                return JsonView("_ImportExcel", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("_ImportExcel", null, resultEntry);
        }
        #endregion

        #region["Upload File Server"]
        [HttpPost]
        public ActionResult UploadFileServer(HttpPostedFileWrapper fileupload, string filetype, string filefolder = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                var fileType = "";
                var filePath = "";
                var folderPart = filefolder;
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
                        if (!FileUtils.listFileExt().Contains(ext))
                        {
                            resultEntry.Success = true;
                            resultEntry.AddMessage("File đính kèm không thuộc các định dạng: \"doc | docx | xls | xlsx | ppt | ppts | pps | ppsx | pptx | mdb | pdf | psd | gif | jpg | jpeg | png | bmp | rar | zip | html | htm | xml\"");
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

                    return Json(new { Success = true, FileName = fileName, FileType = fileType, FilePath = filePathCur, UrlFilePath = SubPath + filePathCur });
                }

                #endregion
                return JsonView("UploadFileServer", null);
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("UploadFileServer", null, resultEntry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFile(string filepath = "")
        {
            var resultEntry = new JsonResultEntry() { Success = false };
            try
            {
                if (!CUtils.IsNullOrEmpty(filepath))
                {
                    var filePathCur = "";
                    filePathCur = filepath;
                    filePathCur = filePathCur.Replace(@"/", @"\");
                    if (Hethong.Equals("TEST"))
                    {
                        filePathCur = FolderUploadTest + filepath;
                    }
                    else
                    {
                        filePathCur = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(filepath)).Replace(@"\Home\", @"\");
                    }

                    if (System.IO.File.Exists(filePathCur))
                    {
                        System.IO.File.Delete(filePathCur);
                    }
                    resultEntry.Success = true;
                    return Json(resultEntry);
                }
            }
            catch (Exception ex)
            {
                resultEntry.SetFailed().AddException(this, ex);
            }
            resultEntry.AddModelState(ViewData.ModelState);
            return JsonViewError("UploadFileServer", null, resultEntry);
        }
        #endregion

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}