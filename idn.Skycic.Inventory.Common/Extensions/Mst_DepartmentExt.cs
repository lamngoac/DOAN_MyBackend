using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public class Mst_DepartmentExt : Mst_Department
    {
        private Mst_DepartmentExt _parent;
        public Mst_DepartmentExt Parent
        {
            get
            {
                return _parent ?? (_parent = new Mst_DepartmentExt()
                {
                    DepartmentCode = DepartmentCodeParent
                });
            }
            set { _parent = value; }
        }

        public List<Mst_DepartmentExt> Children { get; set; }

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
