using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUtils = idn.Skycic.Inventory.Utils;
using TConst = idn.Skycic.Inventory.Constants;

namespace idn.Skycic.Inventory.BizService.Services
{
    public class OS_PrdCenter_FileService : ClientServiceBase<OS_PrdCenter_FileService>
    {
        public static OS_PrdCenter_FileService Instance
        {
            get
            {
                return GetInstance<OS_PrdCenter_FileService>();
            }
        }

        public override string ApiControllerName
        {
            get
            {
                return "File";
            }
        }

        public RT_OS_PrdCenter_File WA_OS_PrdCenter_UploadFile(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            var result = PostData<RT_OS_PrdCenter_File, RQ_OS_PrdCenter_File>(TConst.UrlType.UrlPrdCenter, "File",  "WA_UploadFile", new { }, objRQ_OS_PrdCenter_File);
            return result;
        }

        public RT_OS_PrdCenter_File WA_OS_PrdCenter_MoveFile(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            var result = PostData<RT_OS_PrdCenter_File, RQ_OS_PrdCenter_File>(TConst.UrlType.UrlPrdCenter, "File", "WA_MoveFile", new { }, objRQ_OS_PrdCenter_File);
            return result;
        }

        public RT_OS_PrdCenter_File WA_OS_PrdCenter_DeleteFile(RQ_OS_PrdCenter_File objRQ_OS_PrdCenter_File)
        {
            var result = PostData<RT_OS_PrdCenter_File, RQ_OS_PrdCenter_File>(TConst.UrlType.UrlPrdCenter, "File", "WA_DeleteFile", new { }, objRQ_OS_PrdCenter_File);
            return result;
        }

    }
}
