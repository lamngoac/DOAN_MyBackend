using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idn.Skycic.Inventory.Utils
{
    public static class SqlStringExtensions
    {

        public static string GenWhereClause(string colName, object value, string op = "=")
        {
            if (value is string)
            {
                if (!string.IsNullOrEmpty((string)value))
                {
                    return string.Format("{0} {1} '{2}'", colName, op, value);
                }
            }

            else if (value != null)
            {
                return string.Format("{0} {1} {2}", colName, op, value);
            }
            else if(value == null)
            {
                return string.Format("{0} {1}", colName, "ISNULL");
            }


            return "";
        }


        public static string AddWhereClause(this StringBuilder sql, string colName, object value, string op = "=", string rel = "and")
        {
            string cl = GenWhereClause(colName, value, op);


            if (string.IsNullOrEmpty(cl)) return sql.ToString();
            if (sql.Length > 0)
            {
                sql.Append(string.Format(" {0} {1}", rel, cl));
            }
            else
            {
                sql.Append(cl);
            }

            return sql.ToString();
        }


    }
}
