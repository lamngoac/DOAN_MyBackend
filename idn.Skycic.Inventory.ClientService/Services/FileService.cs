using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.ClientService.Services
{
    public class FileService : ClientServiceBase1
    {      
        public string[] WA_UploadFileNew(RQ_File objRQ_File, string addressAPIs)
        {
            var result = Up_Move_File("File", "WA_UploadFileNew", new { }, objRQ_File, addressAPIs);            
            return result;
        }
        public string[] WA_MoveFileNew(RQ_File objRQ_File, string addressAPIs)
        {
            var result = Up_Move_File("File", "WA_MoveFileNew", new { }, objRQ_File, addressAPIs);
            return result;
        }
    }   
   
}
