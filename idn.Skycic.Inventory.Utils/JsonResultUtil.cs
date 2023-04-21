using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Utils
{
    public class JsonResultUtil
    {
        public bool Success { get; set; }
        public string ErrDetail { get; set; } 
        public string ErrMessage { get; set; }
    }
}
