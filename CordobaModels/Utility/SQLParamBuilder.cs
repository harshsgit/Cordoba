using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace CordobaModels
{
    public class SQLParamBuilder
    {

        public static SqlParameter GetSQLParam(string paramName, SqlDbType type, dynamic value)
        {
            SqlParameter sqlParam = new SqlParameter(paramName, type);
            sqlParam.Value = value;
            return sqlParam;
        }

        public static SqlParameter GetSQLParam(string paramName, SqlDbType type, dynamic value, [Optional] string typeName)
        {
            SqlParameter sqlParam = new SqlParameter(paramName, type);
            sqlParam.Value = value;
            if (typeName != null)
            {
                sqlParam.TypeName = typeName;
            }
            return sqlParam;
        }

        public static string GetSortOrder(string SortBy, string SortDirection)
        {
            return SortBy + " " + (SortDirection.ToLower() == "descending" ? "DESC" : "");
        }
    }
}