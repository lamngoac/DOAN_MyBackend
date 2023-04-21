using idn.Skycic.Inventory.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public class Mst_OrgExt : Mst_Org
    {
        private Mst_OrgExt _parent;
        public Mst_OrgExt Parent
        {
            get
            {
                return _parent ?? (_parent = new Mst_OrgExt()
                {
                    OrgID = OrgParent
                });
            }
            set { _parent = value; }
        }

        public List<Mst_OrgExt> Children { get; set; }

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
                    return string.Format("{0}{1}", l, OrgID);

                }

                return OrgID.ToString();
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
                    return string.Format("{0}{1}", l, OrgID);

                }

                return OrgID.ToString();
            }
        }
    }
}
