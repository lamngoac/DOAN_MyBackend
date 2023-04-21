using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace idn.Skycic.Inventory.Common.Models
{
    [DataContract]
    public class SysObjectFunction
    {
        [DataMember(Order = 1)]
        public string ObjectCode { get; set; }


        [DataMember(Order = 10)]
        public List<string> FunctionCodes { get; set; }
    }

    [DataContract]
    public class SysObjectSetting
    {
        [DataMember]
        public List<SysObjectFunction> List { get; set; }
    }
}
