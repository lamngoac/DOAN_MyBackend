using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Common.Models.ProductCentrer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public class Mst_ProductGroupExt : Mst_ProductGroup
    {
        private Mst_ProductGroupExt _parent;
        public Mst_ProductGroupExt Parent
        {
            get
            {
                return _parent ?? (_parent = new Mst_ProductGroupExt()
                {
                    ProductGrpCode = ProductGrpCodeParent
                });
            }
            set { _parent = value; }
        }

        public List<Mst_ProductGroupExt> Children { get; set; }

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
                        l += "&emsp;";
                    }
                    return string.Format("{0}{1}", l, ProductGrpName);

                }

                return ProductGrpName.ToString();
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
                    return string.Format("{0}{1}", l, ProductGrpCode);

                }

                return ProductGrpCode.ToString();
            }
        }
    }
}
