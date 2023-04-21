using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using idn.Skycic.Inventory.Common.Models;
using idn.Skycic.Inventory.Utils;
using idn.Skycic.Inventory.Web.Models;

namespace idn.Skycic.Inventory.Web.Extensions
{
    public static class Mst_DepartmentExtension
    {
        public static List<Mst_DepartmentUI> GetGroupBaseList(List<Mst_DepartmentUI> s)
        {
            var listPageBase = new List<Mst_DepartmentUI>();
            if (s != null)
            {
                listPageBase.AddRange(s.Cast<Mst_DepartmentUI>());
            }

            return listPageBase;
        }

        public static void BuildGroupBaseTree(this Mst_DepartmentUI groupBase, List<Mst_DepartmentUI> groupBaseList)
        {
            if (groupBase.Children == null) groupBase.Children = new List<Mst_DepartmentUI>();

            foreach (var c in groupBaseList)
            {
                if (c.Parent != null && !CUtils.IsNullOrEmpty(c.Parent.DepartmentCode))
                {
                    if (c.Parent.DepartmentCode.ToString().Trim().Equals(groupBase.DepartmentCode.ToString().Trim()))
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
        }

        public static List<Mst_DepartmentUI> ToGroupBaseTree(List<Mst_DepartmentUI> groupBaseList)
        {
            var list = new List<Mst_DepartmentUI>();

            #region["Common"]

            //var departmentCodeParent = System.Configuration.ConfigurationManager.AppSettings["DepartmentCodeParent"];
            //if (CUtils.IsNullOrEmpty(departmentCodeParent))
            //{
            //    departmentCodeParent = "O";
            //}

            //foreach (var groupBase in groupBaseList)
            //{
            //    if (!CUtils.IsNullOrEmpty(groupBase.DepartmentCodeParent) && groupBase.DepartmentCodeParent.ToString().Trim().ToUpper().Trim().Equals(departmentCodeParent))
            //    {
            //        if (groupBase.Children == null) groupBase.BuildGroupBaseTree(groupBaseList);

            //        list.Add(groupBase);
            //    }
            //}
            #endregion

            #region["Edit"]
            foreach (var groupBase in groupBaseList)
            {
                if (CUtils.IsNullOrEmpty(groupBase.DepartmentCodeParent))
                {
                    if (groupBase.Children == null) groupBase.BuildGroupBaseTree(groupBaseList);

                    list.Add(groupBase);
                }
            }
            #endregion
            return list;
        }

        public static List<Mst_DepartmentUI> ToFlatGroupBaseTree(this List<Mst_DepartmentUI> groupBaseList, int level = 0)
        {
            var list = new List<Mst_DepartmentUI>();

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