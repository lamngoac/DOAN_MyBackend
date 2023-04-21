using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public static class Mst_OrgExtension
    {
        public static List<Mst_OrgExt> GetGroupBaseList(List<Mst_OrgExt> s)
        {
            var listPageBase = new List<Mst_OrgExt>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<Mst_OrgExt>());
            }

            return listPageBase;
        }

        public static void BuildGroupBaseTree(this Mst_OrgExt groupBase, List<Mst_OrgExt> groupBaseList)
        {
            if (groupBase.Children == null) groupBase.Children = new List<Mst_OrgExt>();

            foreach (var c in groupBaseList)
            {
                if (c.Parent.OrgID != null && c.Parent.OrgID.Equals(groupBase.OrgID))
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

        public static List<Mst_OrgExt> ToGroupBaseTree(List<Mst_OrgExt> groupBaseList)
        {
            var list = new List<Mst_OrgExt>();


            #region["Edit"]
            foreach (var groupBase in groupBaseList)
            {
                if (groupBase.OrgParent == null || groupBase.OrgParent.ToString().Trim().Length == 0)
                {
                    if (groupBase.Children == null) groupBase.BuildGroupBaseTree(groupBaseList);

                    list.Add(groupBase);
                }
            }
            #endregion
            return list;
        }

        public static List<Mst_OrgExt> ToFlatGroupBaseTree(this List<Mst_OrgExt> groupBaseList, int level = 0)
        {
            var list = new List<Mst_OrgExt>();

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
