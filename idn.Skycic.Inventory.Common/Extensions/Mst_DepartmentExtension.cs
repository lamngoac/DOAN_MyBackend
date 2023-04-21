using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Common.Extensions
{
    public static class Mst_DepartmentExtension
    {
        public static List<Mst_DepartmentExt> GetGroupBaseList(List<Mst_DepartmentExt> s)
        {
            var listPageBase = new List<Mst_DepartmentExt>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<Mst_DepartmentExt>());
            }

            return listPageBase;
        }

        public static void BuildGroupBaseTree(this Mst_DepartmentExt groupBase, List<Mst_DepartmentExt> groupBaseList)
        {
            if (groupBase.Children == null) groupBase.Children = new List<Mst_DepartmentExt>();

            foreach (var c in groupBaseList)
            {
                if (c.Parent.DepartmentCode != null && c.Parent.DepartmentCode.Equals(groupBase.DepartmentCode))
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

        public static List<Mst_DepartmentExt> ToGroupBaseTree(List<Mst_DepartmentExt> groupBaseList)
        {
            var list = new List<Mst_DepartmentExt>();


            #region["Edit"]
            foreach (var groupBase in groupBaseList)
            {
                if (groupBase.DepartmentCodeParent == null || groupBase.DepartmentCodeParent.ToString().Trim().Length == 0)
                {
                    if (groupBase.Children == null) groupBase.BuildGroupBaseTree(groupBaseList);

                    list.Add(groupBase);
                }
            }
            #endregion
            return list;
        }

        public static List<Mst_DepartmentExt> ToFlatGroupBaseTree(this List<Mst_DepartmentExt> groupBaseList, int level = 0)
        {
            var list = new List<Mst_DepartmentExt>();

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
