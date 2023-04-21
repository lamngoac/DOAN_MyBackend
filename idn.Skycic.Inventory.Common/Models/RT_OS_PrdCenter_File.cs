using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class RT_OS_PrdCenter_File : WARTBase
    {
        public object Status { get; set; }
        public object DescStatus { get; set; }
        public object Message { get; set; }
        public object AppPath { get; set; }
        public object UrlType { get; set; }
        public object UrlPath { get; set; }
    }
}
