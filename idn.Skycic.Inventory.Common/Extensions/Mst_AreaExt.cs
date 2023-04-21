using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public class Mst_AreaExt : Mst_Area
    {
        private Mst_AreaExt _parent;
        public Mst_AreaExt Parent
        {
            get
            {
                return _parent ?? (_parent = new Mst_AreaExt()
                {
                    AreaCode = AreaCodeParent
                });
            }
            set { _parent = value; }
        }

        public List<Mst_AreaExt> Children { get; set; }

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
                    return string.Format("{0}{1}", l, AreaName);

                }

                var strAreaName = "";
                if (AreaName != null && AreaName.ToString().Trim().Length > 0)
                {
                    strAreaName = AreaName.ToString();
                }
                return strAreaName;
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
                    return string.Format("{0}{1}", l, AreaCode);

                }
                var strAreaCode = "";
                if (AreaCode != null && AreaCode.ToString().Trim().Length > 0)
                {
                    strAreaCode = AreaCode.ToString();
                }
                return strAreaCode;
            }
        }
    }
}
