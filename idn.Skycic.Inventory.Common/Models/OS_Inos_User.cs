using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Models
{
    public class OS_Inos_User
    {
        public object Id { get; set; }
        public object Name { get; set; }
        public object Email { get; set; }
        public object Language { get; set; }
        public object TimeZone { get; set; }
        public object Avatar { get; set; }
        public object LogLUDTimeUTC { get; set; }
        public object LogLUBy { get; set; }

        ////

        public object Password { get; set; }
        public object VerificationCode { get; set; }
    }
}
