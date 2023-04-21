using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RQ_OS_PrdCenter_RT_File : WARQBase
    {
        public string folderUpload { get; set; }
        public string fileName { get; set; }
        public string uploadFileAsBase64String { get; set; }
        public string sourceFileName { get; set; }
        public string destFileName { get; set; }
    }
}
