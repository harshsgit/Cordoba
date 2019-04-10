using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Helpers
{
    /// <summary>
    /// Helpers Class
    /// </summary>
    public static class Helpers
    {
       
        /// <summary>
        /// Commas the separated to integer.
        /// </summary>
        /// <param name="commaSeparatedsString">The comma separated string.</param>
        /// <returns>List</returns>
        /// <exception cref="System.Exception">none</exception>
        public static List<int> CommaSeparatedToInt(this string commaSeparatedsString)
        {
            List<int> result = new List<int>();
            var lstIds = commaSeparatedsString.Split(',').ToList();
            try
            {
                foreach (var item in lstIds)
                {
                    result.Add(Convert.ToInt32(item));
                }
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }

            return result;
        }
    }

    /// <summary>
    /// XML serializer helper class. Serializes and deserializes objects from/to XML
    /// </summary>
    /// <typeparam name="T">The type of the object to serialize/deserialize.
    /// Must have a parameterless constructor and implement <see cref="Serializable"/></typeparam>
    public class ConvertToXml<T> where T : class, new()
    {
        static ConvertToXml()
        {
            
            
        }

        public static string GetXMLString(List<T> sourceList, string csvSelectedProperties= "")
        {

            //All numeric values in created xml was with dot symbol instead of comma
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            if (sourceList != null)
            {
                

                StringBuilder xmlString = new StringBuilder();
                xmlString.Append(@"<table>");

                Type sourceType;
                foreach (T item in sourceList)
                {
                    xmlString.Append("<row>");
                    sourceType = item.GetType();

                    foreach (PropertyInfo p in sourceType.GetProperties().Where(p => string.IsNullOrEmpty(csvSelectedProperties) || csvSelectedProperties.Split(',').Contains(p.Name)))
                    {
                        xmlString.Append("<" + p.Name + ">");
                        xmlString.Append(EncodeSpecialCharacter(p.GetValue(item, null)));
                        xmlString.Append("</" + p.Name + ">");
                    }
                    xmlString.Append("</row>");
                }
                xmlString.Append("</table>");

                return xmlString.ToString();
            }
            return string.Empty;
        }

        /// <summary>
        /// Replace Special Character as following 
        /// 1) &  =   &amp;
        /// 2) <  =   &lt;
        /// 3) >  =   &gt;
        /// 4) "  =   &quot;
        /// 5) '  =   &#39;
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static object EncodeSpecialCharacter(object value)
        {
            if (value != null)
            {
                string strValue = value as string;

                if (!string.IsNullOrEmpty(strValue))
                {
                    strValue = strValue.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace(@"""","&quot;").Replace("'", "&#39;");
                    return strValue;
                       
                }
            }
            return value;
        }
    }
}
