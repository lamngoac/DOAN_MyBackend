using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public static class Mst_ProductGroupExtension
    {
        public static List<Mst_ProductGroupExt> GetGroupBaseList(List<Mst_ProductGroupExt> s)
        {
            var listPageBase = new List<Mst_ProductGroupExt>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<Mst_ProductGroupExt>());
            }

            return listPageBase;
        }

        public static void BuildGroupBaseTree(this Mst_ProductGroupExt groupBase, List<Mst_ProductGroupExt> groupBaseList)
        {
            if (groupBase.Children == null) groupBase.Children = new List<Mst_ProductGroupExt>();

            foreach (var c in groupBaseList)
            {
                if (c.Parent.ProductGrpCode != null && c.Parent.ProductGrpCode.Equals(groupBase.ProductGrpCode))
                {

                    if (!groupBase.Children.Contains(c))
                    {
                        groupBase.Children.Add(c);
                        c.Parent = groupBase;

                        BuildGroupBaseTree(c, groupBaseList);
                    }
                }
            }
        }

        public static List<Mst_ProductGroupExt> ToGroupBaseTree(List<Mst_ProductGroupExt> groupBaseList)
        {
            var list = new List<Mst_ProductGroupExt>();


            #region["Edit"]
            foreach (var groupBase in groupBaseList)
            {
                if (groupBase.ProductGrpCodeParent == null || groupBase.ProductGrpCodeParent.ToString().Trim().Length == 0)
                {
                    if (groupBase.Children == null) groupBase.BuildGroupBaseTree(groupBaseList);

                    list.Add(groupBase);
                }
            }
            #endregion
            return list;
        }

        public static List<Mst_ProductGroupExt> ToFlatGroupBaseTree(this List<Mst_ProductGroupExt> groupBaseList, int level = 0)
        {
            var list = new List<Mst_ProductGroupExt>();

            foreach (var groupBase in groupBaseList)
            {
                groupBase.HLevel = level;
                list.Add(groupBase);
                list.AddRange(groupBase.Children.ToFlatGroupBaseTree(level + 1));
            }

            return list;
        }
    }
}
