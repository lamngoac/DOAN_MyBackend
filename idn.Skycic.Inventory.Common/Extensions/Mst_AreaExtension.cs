using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public static class Mst_AreaExtension
    {
        public static List<Mst_AreaExt> GetGroupBaseList(List<Mst_AreaExt> s)
        {
            var listPageBase = new List<Mst_AreaExt>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<Mst_AreaExt>());
            }

            return listPageBase;
        }

        public static void BuildGroupBaseTree(this Mst_AreaExt groupBase, List<Mst_AreaExt> groupBaseList)
        {
            if (groupBase.Children == null) groupBase.Children = new List<Mst_AreaExt>();

            foreach (var c in groupBaseList)
            {
                if (c.Parent.AreaCode != null && c.Parent.AreaCode.Equals(groupBase.AreaCode))
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

        public static List<Mst_AreaExt> ToGroupBaseTree(List<Mst_AreaExt> groupBaseList)
        {
            var list = new List<Mst_AreaExt>();


            #region["Edit"]
            foreach (var groupBase in groupBaseList)
            {
                if (groupBase.AreaCodeParent == null || groupBase.AreaCodeParent.ToString().Trim().Length == 0)
                {
                    if (groupBase.Children == null) groupBase.BuildGroupBaseTree(groupBaseList);

                    list.Add(groupBase);
                }
            }
            #endregion
            return list;
        }

        public static List<Mst_AreaExt> ToFlatGroupBaseTree(this List<Mst_AreaExt> groupBaseList, int level = 0)
        {
            var list = new List<Mst_AreaExt>();

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
