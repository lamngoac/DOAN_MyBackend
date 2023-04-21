using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace idn.Skycic.Inventory.Common.Models
{
    public class SysUser
    {
        public string Code { get; set; }

        public string Email { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Password { get; set; }

        public bool SysAdmin { get; set; }

        public bool Enable { get; set; }

        public bool Lockout { get; set; }

        public DateTime LockoutDate { get; set; }

        public string Language { get; set; }


		
	}
}
