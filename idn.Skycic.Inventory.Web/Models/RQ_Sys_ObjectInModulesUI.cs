using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Common.Models;

namespace idn.Skycic.Inventory.Web.Models
{
    public class RQ_Sys_ObjectInModulesUI : RQ_Sys_ObjectInModules
    {
        public List<Sys_ObjectInModulesUI> Lst_Sys_ObjectInModulesUI { get; set; }
    }

    public class Sys_ObjectInModulesUI : Sys_ObjectInModules
    {
        public string so_ObjectType { get; set; }
    }
}