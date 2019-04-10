using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Helpers;
using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Net.Http.Headers;
using CordobaCommon;

namespace CordobaAPI.API
{
    public class ReportApiController : ApiController
    {
        public IReportService _reportServices;

        public ReportApiController()
        {
            _reportServices = new ReportService();
        }

        [HttpPost]
        public TableParameter<ReportEntity> GetReturnList(int PageIndex, DateTime? DateStart, DateTime? DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetReturnList(sortColumn, DateStart, DateEnd, GroupById, StatusId, StoreId, LoggedInUserId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public TableParameter<ReportEntity> GetOrderReportList(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int? GroupById, int? StatusId, int? StoreId, int LoggedInUserId, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetOrderReportList(sortColumn, DateStart, DateEnd, GroupById, StatusId, StoreId, LoggedInUserId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public TableParameter<ReportEntity> GetTransactionReportList(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetTransactionReportList(sortColumn, DateStart, DateEnd, StoreId, LoggedInUserId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public TableParameter<ReportEntity> GetCustomerBalanceReportList(int PageIndex, string StoreIDs, Nullable<DateTime> Date, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetCustomerBalanceReportList(sortColumn, StoreIDs,Date, tableParameter).ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage TransactionReportExportToExcel(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, object tableParameter)
        {
            SortColumn sr;
            string sortColumn;
            if (tableParameter != null)
            {
                sr = (SortColumn)JsonConvert.DeserializeObject(tableParameter.ToString(), typeof(SortColumn));
                sortColumn = sr.Desc ? sr.Column + " desc" : sr.Column + " asc";
            }
            else
            {
                sortColumn = "Date desc";
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            DateTime date = DateTime.Now.Date;
            string str = string.Concat("TransactionReport_export", date.ToString("ddMMyyyy"), ".xls");

            DataSet ds = _reportServices.TransactionReportExportToExcel(sortColumn, tableParameter, DateStart, DateEnd, StoreId);

            if (ds != null && ds.Tables.Count > 0)
            {
                List<ColumnConfiguration> selectedColumn = new List<ColumnConfiguration>();
                selectedColumn.Add(new ColumnConfiguration("DateFormat", "Date"));
                selectedColumn.Add(new ColumnConfiguration("firstname", "First Name"));
                selectedColumn.Add(new ColumnConfiguration("lastname", "Last Name"));
                selectedColumn.Add(new ColumnConfiguration("email", "Email"));
                selectedColumn.Add(new ColumnConfiguration("store", "Store"));
                selectedColumn.Add(new ColumnConfiguration("adjustment", "Adjustment"));
                selectedColumn.Add(new ColumnConfiguration("type_of_points", "Type Of Points"));
                selectedColumn.Add(new ColumnConfiguration("comments", "Comments"));

                if (!string.IsNullOrEmpty(sortColumn))
                {
                    ds.Tables[0].DefaultView.Sort = sortColumn;
                }
                DataTable dt = ds.Tables[0].DefaultView.ToTable(false, selectedColumn.Select(s => s.OriginalColumnName).ToArray());

                ds.Tables.RemoveAt(0); // Delete Existing Table 
                ds.Tables.Add(dt); // Add Modified Table
                ds = CordobaCommon.GeneralMethods.ChangeDataSetColumnTitleAndReorder(ds, selectedColumn); // Rename Selected Columns

            }

            try
            {
                if (ds == null)
                {
                    return base.Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
                }
                byte[] asByteArray = GeneralMethods.ExportToExcel(ds, "Customers");

                HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
                Stream @null = Stream.Null;
                streamContent.Content = new StreamContent(new MemoryStream(asByteArray));
                streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                streamContent.Content.Headers.Add("content-disposition", string.Concat("attachment;  filename=\"", str, "\""));
                httpResponseMessage = streamContent;

            }
            catch (Exception)
            {
                httpResponseMessage = base.Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
            return httpResponseMessage;
        }

        [HttpPost]
        public TableParameter<ReportEntity> GetTransactionItemReportList(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetTransactionItemReportList(sortColumn, DateStart, DateEnd, StoreId, LoggedInUserId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage TransactionItemReportExportToExcel(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, object tableParameter)
        {
            SortColumn sr;
            string sortColumn;
            if (tableParameter != null)
            {
                sr = (SortColumn)JsonConvert.DeserializeObject(tableParameter.ToString(), typeof(SortColumn));
                sortColumn = sr.Desc ? sr.Column + " desc" : sr.Column + " asc";
            }
            else
            {
                sortColumn = "Date desc";
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            DateTime date = DateTime.Now.Date;
            string str = string.Concat("TransactionItemReport_export", date.ToString("ddMMyyyy"), ".xls");

            DataSet ds = _reportServices.TransactionItemReportExportToExcel(sortColumn, tableParameter, DateStart, DateEnd, StoreId);

            if (ds != null && ds.Tables.Count > 0)
            {
                List<ColumnConfiguration> selectedColumn = new List<ColumnConfiguration>();
                selectedColumn.Add(new ColumnConfiguration("DateFormat", "Date"));
                selectedColumn.Add(new ColumnConfiguration("firstname", "First Name"));
                selectedColumn.Add(new ColumnConfiguration("lastname", "Last Name"));
                selectedColumn.Add(new ColumnConfiguration("email", "Email"));
                selectedColumn.Add(new ColumnConfiguration("store", "Store"));
                selectedColumn.Add(new ColumnConfiguration("adjustment", "Adjustment"));
                selectedColumn.Add(new ColumnConfiguration("type_of_points", "Type Of Points"));
                selectedColumn.Add(new ColumnConfiguration("comment", "Comments"));
                selectedColumn.Add(new ColumnConfiguration("model", "Model"));
                selectedColumn.Add(new ColumnConfiguration("product_name", "Item Name"));
                selectedColumn.Add(new ColumnConfiguration("quantity", "Quantity"));

                if (!string.IsNullOrEmpty(sortColumn))
                {
                    ds.Tables[0].DefaultView.Sort = sortColumn;
                }
                DataTable dt = ds.Tables[0].DefaultView.ToTable(false, selectedColumn.Select(s => s.OriginalColumnName).ToArray());

                ds.Tables.RemoveAt(0); // Delete Existing Table 
                ds.Tables.Add(dt); // Add Modified Table
                ds = CordobaCommon.GeneralMethods.ChangeDataSetColumnTitleAndReorder(ds, selectedColumn); // Rename Selected Columns

            }

            try
            {
                if (ds == null)
                {
                    return base.Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
                }
                byte[] asByteArray = GeneralMethods.ExportToExcel(ds, "Customers");

                HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
                Stream @null = Stream.Null;
                streamContent.Content = new StreamContent(new MemoryStream(asByteArray));
                streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                streamContent.Content.Headers.Add("content-disposition", string.Concat("attachment;  filename=\"", str, "\""));
                httpResponseMessage = streamContent;

            }
            catch (Exception)
            {
                httpResponseMessage = base.Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
            return httpResponseMessage;
        }

        [HttpPost]
        public HttpResponseMessage CustomerBalanceReportExportToExcel(int PageIndex, string StoreIDs, Nullable<DateTime> Date, object tableParameter)
        {
            SortColumn sr;
            string sortColumn;
            if (tableParameter != null)
            {
                sr = (SortColumn)JsonConvert.DeserializeObject(tableParameter.ToString(), typeof(SortColumn));
                sortColumn = sr.Desc ? sr.Column + " desc" : sr.Column + " asc";
            }
            else
            {
                sortColumn = "StoreName desc";
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            DateTime date = DateTime.Now.Date;
            string str = string.Concat("CustomerBalanceReport_export", date.ToString("ddMMyyyy"), ".xls");

            DataSet ds = _reportServices.CustomerBalanceReportExportToExcel(sortColumn, tableParameter, StoreIDs, Date );

            if (ds != null && ds.Tables.Count > 0)
            {
                List<ColumnConfiguration> selectedColumn = new List<ColumnConfiguration>();
                selectedColumn.Add(new ColumnConfiguration("StoreName", "Store Name"));
                selectedColumn.Add(new ColumnConfiguration("email", "Email"));
                selectedColumn.Add(new ColumnConfiguration("Balance", "Balance"));

                if (!string.IsNullOrEmpty(sortColumn))
                {
                    ds.Tables[0].DefaultView.Sort = sortColumn;
                }
                DataTable dt = ds.Tables[0].DefaultView.ToTable(false, selectedColumn.Select(s => s.OriginalColumnName).ToArray());

                ds.Tables.RemoveAt(0); // Delete Existing Table 
                ds.Tables.Add(dt); // Add Modified Table
                ds = CordobaCommon.GeneralMethods.ChangeDataSetColumnTitleAndReorder(ds, selectedColumn); // Rename Selected Columns

            }

            try
            {
                if (ds == null)
                {
                    return base.Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
                }
                byte[] asByteArray = GeneralMethods.ExportToExcel(ds, "Customers");

                HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
                Stream @null = Stream.Null;
                streamContent.Content = new StreamContent(new MemoryStream(asByteArray));
                streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                streamContent.Content.Headers.Add("content-disposition", string.Concat("attachment;  filename=\"", str, "\""));
                httpResponseMessage = streamContent;

            }
            catch (Exception)
            {
                httpResponseMessage = base.Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
            return httpResponseMessage;
        }
               

        [HttpPost]
        public TableParameter<ReportEntity> GetTransactionItemCategoryReportList(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, int LoggedInUserId, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetTransactionItemCategoryReportList(sortColumn, DateStart, DateEnd, StoreId, LoggedInUserId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage TransactionItemCategoryReportExportToExcel(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, object tableParameter)
        {
            SortColumn sr;
            string sortColumn;
            if (tableParameter != null)
            {
                sr = (SortColumn)JsonConvert.DeserializeObject(tableParameter.ToString(), typeof(SortColumn));
                sortColumn = sr.Desc ? sr.Column + " desc" : sr.Column + " asc";
            }
            else
            {
                sortColumn = "Date desc";
            }

            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            DateTime date = DateTime.Now.Date;
            string str = string.Concat("TransactionItemCategoryReport_export", date.ToString("ddMMyyyy"), ".xls");

            DataSet ds = _reportServices.TransactionItemCategoryReportExportToExcel(sortColumn, tableParameter, DateStart, DateEnd, StoreId);

            if (ds != null && ds.Tables.Count > 0)
            {
                List<ColumnConfiguration> selectedColumn = new List<ColumnConfiguration>();
                selectedColumn.Add(new ColumnConfiguration("DateFormat", "Date"));
                selectedColumn.Add(new ColumnConfiguration("firstname", "First Name"));
                selectedColumn.Add(new ColumnConfiguration("lastname", "Last Name"));
                selectedColumn.Add(new ColumnConfiguration("email", "Email"));
                selectedColumn.Add(new ColumnConfiguration("store", "Store"));
                selectedColumn.Add(new ColumnConfiguration("adjustment", "Adjustment"));
                selectedColumn.Add(new ColumnConfiguration("comment", "Comments"));
                selectedColumn.Add(new ColumnConfiguration("model", "Model"));
                selectedColumn.Add(new ColumnConfiguration("product_name", "Item Name"));
                selectedColumn.Add(new ColumnConfiguration("category", "Category"));
                selectedColumn.Add(new ColumnConfiguration("quantity", "Quantity"));

                if (!string.IsNullOrEmpty(sortColumn))
                {
                    ds.Tables[0].DefaultView.Sort = sortColumn;
                }
                DataTable dt = ds.Tables[0].DefaultView.ToTable(false, selectedColumn.Select(s => s.OriginalColumnName).ToArray());

                ds.Tables.RemoveAt(0); // Delete Existing Table 
                ds.Tables.Add(dt); // Add Modified Table
                ds = CordobaCommon.GeneralMethods.ChangeDataSetColumnTitleAndReorder(ds, selectedColumn); // Rename Selected Columns

            }

            try
            {
                if (ds == null)
                {
                    return base.Request.CreateErrorResponse(HttpStatusCode.NotFound, "No records found.");
                }
                byte[] asByteArray = GeneralMethods.ExportToExcel(ds, "Customers");

                HttpResponseMessage streamContent = new HttpResponseMessage(HttpStatusCode.OK);
                Stream @null = Stream.Null;
                streamContent.Content = new StreamContent(new MemoryStream(asByteArray));
                streamContent.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                streamContent.Content.Headers.Add("content-disposition", string.Concat("attachment;  filename=\"", str, "\""));
                httpResponseMessage = streamContent;

            }
            catch (Exception)
            {
                httpResponseMessage = base.Request.CreateResponse<bool>(HttpStatusCode.OK, false);
            }
            return httpResponseMessage;
        }

        [HttpPost]
        public TableParameter<ReportEntity> GetStoreReportList(int PageIndex, Nullable<DateTime> DateStart, Nullable<DateTime> DateEnd, int StoreId, TableParameter<ReportEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _reportServices.GetStoreReportList(sortColumn, DateStart, DateEnd, StoreId, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<ReportEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCustomerByStoreForChart(int store_id, int ChartFilterType)
        {
            try
            {
                var result = _reportServices.GetCustomerByStoreForChart(store_id, ChartFilterType);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}