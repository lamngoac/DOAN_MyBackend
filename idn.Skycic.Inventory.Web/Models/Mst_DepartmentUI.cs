using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace idn.Skycic.Inventory.Web.Models
{
    public class Mst_DepartmentUI : Mst_Department
    {
        private Mst_DepartmentUI _parent;
        public Mst_DepartmentUI Parent
        {
            get
            {
                return _parent ?? (_parent = new Mst_DepartmentUI()
                {
                    DepartmentCode = DepartmentCodeParent
                });
            }
            set { _parent = value; }
        }

        public List<Mst_DepartmentUI> Children { get; set; }

        public int HLevel
        {
            get;
            set;
        }

        public string HlevelTitle
        {
            get
            {
                if (HLevel > 0)
                {
                    var l = "";
                    for (var i = 1; i <= HLevel; ++i)
                    {
                        l += "|--";
                    }
                    return string.Format("{0}{1}", l, DepartmentName);

                }

                return DepartmentName.ToString();
            }
        }

        public string HlevelCode
        {
            get
            {
                if (HLevel > 0)
                {
                    var l = "";
                    for (var i = 1; i <= HLevel; ++i)
                    {
                        l += "|--";
                    }
                    return string.Format("{0}{1}", l, DepartmentCode);

                }

                return DepartmentCode.ToString();
            }
        }
    }
}