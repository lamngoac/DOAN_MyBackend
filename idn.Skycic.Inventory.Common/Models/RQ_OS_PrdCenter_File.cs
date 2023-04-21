using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_File : WARQBase
    {
        public object folderUpload { get; set; }
        public object fileName { get; set; }
        public object uploadFileAsBase64String { get; set; }
        public object sourceFileName { get; set; }
        public object destFileName { get; set; }
        public object UrlType { get; set; }
    }
}
