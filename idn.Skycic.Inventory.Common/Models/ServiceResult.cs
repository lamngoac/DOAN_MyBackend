using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        
        public Exception Exception { get; set; }
        //public ServiceException Exception { get; set; }
    }
}
