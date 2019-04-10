using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Web;
using System.Data;
using System.Net;
using System.Drawing;
using OfficeOpenXml.Style;
using System.IO;
using System.Web.Mvc;
using System.Reflection;

namespace CordobaCommon
{
    /// <summary>
    /// General Methods
    /// </summary>
    public class GeneralMethods
    {
        /// <summary>
        /// Binds the logo for excel export.
        /// </summary>
        /// <param name="worksheetExport">The worksheet export.</param>
        public static void BindLogoForExcelExport(ref ExcelWorksheet worksheetExport)
        {
            System.Drawing.Image imglogo = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("~/Images/logo.png"));
            var picture = worksheetExport.Drawings.AddPicture("logo", imglogo);
            picture.From.Row = 0;
            picture.From.Column = 0;
            picture.To.Row = 0;
            picture.To.Column = 1;
            picture.SetSize(100, 49);
            picture.SetPosition(5, 20);
        }

        public static string ConvertDatatableToXML(DataTable dt)
        {
            MemoryStream str = new MemoryStream();
            dt.WriteXml(str, true);
            str.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(str);
            string xmlstr;
            xmlstr = sr.ReadToEnd();
            return (xmlstr);
        }

        public static byte[] ExportToExcel(DataSet ds, string Title)
        {

            DateTime date = DateTime.Now.Date;
            string str = string.Concat(Title, date.ToString("ddMMyyyy"), ".xls");
            try
            {
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    foreach (DataTable dtSrc in ds.Tables)
                    {
                        ExcelWorksheet wholeName = excelPackage.Workbook.Worksheets.Add(Title);
                        //Create the worksheet    

                        //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1    
                        wholeName.Cells["A1"].LoadFromDataTable(dtSrc, true);
                        wholeName.Cells.Style.Font.SetFromFont(new Font("Calibri", 10));
                        wholeName.Cells.AutoFitColumns();
                        //Format the header    
                        using (ExcelRange objRange = wholeName.Cells["A1:XFD1"])
                        {
                            objRange.Style.Font.Bold = true;
                            objRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            objRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                            objRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            objRange.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        }

                    }

                    byte[] asByteArray = excelPackage.GetAsByteArray();
                    return asByteArray;

                }
            }
            catch (Exception exception)
            {

            }
            return null;
        }

        public static void WriteLog(string logDetail)
        {
            try
            {
                //sLogFormat used to create log files format :
                // dd/mm/yyyy hh:mm:ss AM/PM ==> Log Message
                string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";




                //if (!Directory.Exists(@"C:\Log"))
                //{
                //    Directory.CreateDirectory(@"C:\Log");
                //}
                //if (!File.Exists(@"C:\Log\TISystemLog.txt"))
                //{
                //    File.Create(@"C:\Log\TISystemLog.txt");
                //}

                //if (File.Exists(@"C:\Log\TISystemLog.txt"))
                //{
                //    StreamWriter sw = new StreamWriter(@"C:\Log\TISystemLog.txt", true);
                //    sw.WriteLine(sLogFormat + logDetail);
                //    sw.Flush();
                //    sw.Close();

                //    //File.AppendAllText(@"C:\Log\TISystemLog.txt", string.Concat(DateTime.Now.ToString("MMM-dd-yyyy HH:MM:ss"), "---", logDetail, Environment.NewLine));
                //}

            }
            catch (Exception)
            {

            }
        }


        public static DataSet ChangeDataSetColumnTitleAndReorder(DataSet ds, List<ColumnConfiguration> selectedColumn)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                DataColumnCollection Dbcolumns = ds.Tables[0].Columns;
                if (selectedColumn.Count > 0)
                {
                    int columnIndex = 0;
                    foreach (var item in selectedColumn)
                    {
                        if (Dbcolumns.Contains(item.OriginalColumnName))
                        {
                            ds.Tables[0].Columns[item.OriginalColumnName].ColumnName = item.CustomColumnName;
                            ds.Tables[0].Columns[item.CustomColumnName].SetOrdinal(columnIndex);
                            columnIndex++;
                        }
                    }
                }

            }
            return ds;
        }

        
    }

    /// <summary>
    /// General Methods
    /// </summary>
    public class JsonCommonObject
    {
        public string ParameterValue { get; set; }
    }


    /// <summary>
    /// 
    /// </summary>
    public class ColumnConfiguration
    {
        public string OriginalColumnName { get; set; }
        public string CustomColumnName { get; set; }
        public bool IsSelectedForExport { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="originalColumnName">Original Model property Name </param>
        /// <param name="customColumnName">Custom Title for Column </param>
        public ColumnConfiguration(string originalColumnName, string customColumnName)
        {
            OriginalColumnName = originalColumnName;
            CustomColumnName = customColumnName;
        }

        public ColumnConfiguration() { }
    }
}
