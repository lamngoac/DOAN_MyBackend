using idn.Skycic.Inventory.Website.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace idn.Skycic.Inventory.Website.Controllers
{
    public class FileController : BaseController
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileWrapper file)
        {


            try
            {
                string fileId = Guid.NewGuid().ToString("D");


                string oldFileName = file.FileName;
                var ext = System.IO.Path.GetExtension(oldFileName).ToLower();


                FileUtils.SaveTempFile(file, fileId, ext);


                var url = GetRequestBaseAddress() + Url.Content(FileUtils.GetTempFileVirtualPath(fileId, ext));

                return Json(new
                {
                    Success = true,
                    FileName = oldFileName,
                    Url = url
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Success = false,
                    Url = e.Message
                });
            }
        }


        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileWrapper file)
        {
            try
            {
                string fileId = Guid.NewGuid().ToString("D");
                string fileName = file.FileName;

                var ext = System.IO.Path.GetExtension(fileName).ToLower();

                FileUtils.SaveTempFile(file, fileId, ext);

                var url = Url.Content(FileUtils.GetTempFileVirtualPath(fileId, ext));


                double size = file.ContentLength;
                string filesize = size.ToString();

                return Json(new
                {
                    success = true,
                    data = new
                    {
                        url = url,

                        fileId = fileId + ext,
                        fileName = fileName,
                        fileSize = filesize,
                    }
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    Success = false,
                    Url = e.Message
                });
            }
        }



        #region pdf
        

        private string RefineHtml(string html)
        {
            var idx = html.IndexOf("<head>") + "<head>".Length;
            string css = @"<link type='text/css' rel='stylesheet' href='_BASE_/Content/tinymce/skins/ui/oxide/content.min.css'><link type='text/css' rel='stylesheet' href='_BASE_/Content/tinymce/skins/content/default/content.min.css'>";

            css = css.Replace("_BASE_", GetRequestBaseAddress());
            html = html.Insert(idx, css);
            return html;
        }
        #endregion

    }
}