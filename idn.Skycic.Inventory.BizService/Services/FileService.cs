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
    public class FileService : ClientServiceBaseFile
    {      
        public string[] WA_UploadFileNew(RQ_File objRQ_File)
        {
            var result = Up_Move_File(TConst.UrlType.UrlPrdCenter, "File", "WA_UploadFileNew", new { }, objRQ_File);            
            return result;
        }
        public string[] WA_MoveFileNew(RQ_File objRQ_File)
        {
            var result = Up_Move_File(TConst.UrlType.UrlPrdCenter,  "File", "WA_MoveFileNew", new { }, objRQ_File);
            return result;
        }
    }   
   
}
