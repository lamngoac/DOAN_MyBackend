using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using idn.Skycic.Inventory.Utils;

namespace idn.Skycic.Inventory.Website.Utils
{
    public class FileUtils
    {
        public static string GetTempDir()
        {
            return "TempFiles";
        }

        public static string GetUploadDir()
        {
            return "Uploads";
        }

        public static string GetTempDirPhysicalPath()
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/{0}", GetUploadDir()));
        }
        public static string GetTempDirPhysicalPath(string folder)
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/{0}", folder));
        }
        public static string GetUploadDirPhysicalPath()
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/{0}", GetUploadDir()));
        }

        public static string GetTempFilePhysicalPath(string fileId, string extension)
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", GetTempDir(), fileId, extension));
        }

        public static string GetUploadFilePhysicalPath(string fileId, string extension)
        {
            return HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", GetUploadDir(), fileId, extension));
        }

        public static string GetTempFileVirtualPath(string fileId, string extension)
        {
            return string.Format("~/{0}/{1}{2}", GetTempDir(), fileId, extension);
            //return HttpContext.Current.Server.MapPath(string.Format("~/{0}/{1}{2}", GetTempDir(), fileId, extension));
        }

        public static string GetUploadFileVirtualPath(string fileId, string extension)
        {
            return string.Format("~/{0}/{1}{2}", GetUploadDir(), fileId, extension);
        }

        public static string SaveTempFile(HttpPostedFileWrapper file, string fileId, string extension)
        {
            var getTempDirPhysicalPath = FileUtils.GetTempDirPhysicalPath();
            CUtils.EnsureDirectoryExists(getTempDirPhysicalPath);

            var filePath = GetTempFilePhysicalPath(fileId, extension);
            file.SaveAs(filePath);
            return filePath;
        }

        public static string SaveTempFile(HttpPostedFileBase file, string fileId, string extension)
        {
            var getTempDirPhysicalPath = FileUtils.GetTempDirPhysicalPath();
            CUtils.EnsureDirectoryExists(getTempDirPhysicalPath);

            var filePath = GetTempFilePhysicalPath(fileId, extension);
            file.SaveAs(filePath);
            return filePath;
        }

        public static string SaveUploadFile(HttpPostedFileWrapper file, string fileId, string extension)
        {
            var getTempDirPhysicalPath = FileUtils.GetTempDirPhysicalPath();
            CUtils.EnsureDirectoryExists(getTempDirPhysicalPath);

            var filePath = GetUploadFilePhysicalPath(fileId, extension);
            file.SaveAs(filePath);
            return filePath;
        }

        public static string SaveFile(HttpPostedFileWrapper file, string filePath)
        {
            var strPath = HttpContext.Current.Server.MapPath(filePath);
            file.SaveAs(strPath);
            return strPath;
        }

        public static string SaveFileUpload(HttpPostedFileWrapper file, string fileId, string extension)
        {
            var getTempDirPhysicalPath = FileUtils.GetTempDirPhysicalPath();
            CUtils.EnsureDirectoryExists(getTempDirPhysicalPath);

            var filePath = GetTempFilePhysicalPath(fileId, extension);
            file.SaveAs(filePath);
            return filePath;
        }

        #region["Delete File and folder"]

        public static List<string> listFileImageExt()
        {
            var listExt = new List<string> { "gif", "jpg", "jpeg", "png", "bmp" };
            return listExt;
        }

        public static List<string> listFileExt()
        {
            //var listExt = new List<string> { "rar", "zip", "gif", "jpg", "jpeg", "png", "bmp", "doc", "docx", "xls", "xlsx", "ppt", "ppts", "pps", "ppsx", "pptx", "mdb", "pdf", "psd", "html", "htm", "xml", };
            var listExt = new List<string> { "rar", "zip", "7z", "gif", "jpg", "jpeg", "png", "bmp", "doc", "docx", "xls", "xlsx", "pdf", "ppt", "pptx", };
            return listExt;
        }

        /// <summary>
        /// - folder: 20170209-160854-0353947df-49ac-4968-8f49-1046ad978ab6
        /// + file 1: abc.doc
        /// + file 2: acd.doc
        /// + file 3: acb.doc
        /// strFolder = @"/Uploads/idocNet/MstPart/20170209-160854-0353947df-49ac-4968-8f49-1046ad978ab6";
        /// </summary>
        /// <param name="strFolder"></param>
        /// <returns></returns>
        public static bool DeleteFile(string strFolder)
        {
            var checkDelete = false;
            try
            {
                if (strFolder != null && strFolder.Trim().Length > 0)
                {
                    var strPath = HttpContext.Current.Server.MapPath(strFolder);
                    var di = new DirectoryInfo(strPath);
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    checkDelete = true;
                }
            }
            catch
            {
                checkDelete = false;
            }
            return checkDelete;
        }

        /// <summary>
        /// strFile = @"\Uploads\idocNet\MstPart\20170209-184214-0ba8e752b-c30c-4958-a459-c2ca9e4b3d0e\ProHRM _ Dashboard.pdf";
        /// </summary>
        /// <param name="strFile"></param>
        /// <returns></returns>
        public static bool DeleteFileCurrent(string strFile)
        {
            var checkDelete = false;
            if (strFile != null && strFile.Trim().Length > 0)
            {
                try
                {
                    var strPath = HttpContext.Current.Server.MapPath(strFile);
                    if (File.Exists(strPath))
                    {
                        File.Delete(strPath);
                    }
                    checkDelete = true;
                }
                catch (Exception)
                {
                    checkDelete = false;
                }
            }

            return checkDelete;
        }

        /// <summary>
        /// - Xóa folder
        /// - /Uploads/idocNet/MstPart
        /// + /Uploads/idocNet/MstPart/20170209-160854-0353947df-49ac-4968-8f49-1046ad978ab6
        /// + /Uploads/idocNet/MstPart/20170209-160854-0353948df-49ac-4968-8f49-1046ad978ab6
        /// + /Uploads/idocNet/MstPart/20170209-160854-0353949df-49ac-4968-8f49-1046ad978ab6
        /// strFolder = @"D:\Allprjs\idocNet\2017.1.OdinasCRM\Dev\V10\idn.Odinas.CRM\idn.InBrand.web\Uploads\idocNet\MstPart"
        /// </summary>
        /// <param name="strFolder"></param>
        /// <returns></returns>
        public static bool DeleteFolder(string strFolder)
        {
            var checkDelete = false;
            try
            {
                if (strFolder != null && strFolder.Trim().Length > 0)
                {
                    var strPath = HttpContext.Current.Server.MapPath(strFolder);
                    var di = new DirectoryInfo(strPath);
                    foreach (DirectoryInfo dir in di.GetDirectories())
                    {
                        dir.Delete(true);
                    }
                    checkDelete = true;
                }

            }
            catch
            {
                checkDelete = false;
            }

            return checkDelete;
        }

        /// <summary>
        /// strFolder = @"/Uploads/idocNet/MstPart/20170209-173243-04c105d9f-3daf-4e58-b62d-f605e495aa78";
        /// </summary>
        /// <param name="strFolder"></param>
        /// <returns></returns>
        public static bool DeleteFolderCurrent(string strFolder)
        {
            var checkDelete = false;
            try
            {
                var strPath = HttpContext.Current.Server.MapPath(strFolder);
                bool exists = System.IO.Directory.Exists(strPath);
                if (exists)
                {
                    Directory.Delete(strPath, true);
                    checkDelete = true;
                }
            }
            catch (Exception)
            {
                checkDelete = false;
            }
            return checkDelete;
        }
        #endregion

    }
}