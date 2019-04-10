using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CordobaModels.Entities;
using CordobaServices.Helpers;
using System.Net.Http.Headers;
using CordobaCommon;
using Newtonsoft.Json;

namespace CordobaAPI.API
{ 
    public class ProductPurchasedReportApiController : ApiController
    {

        public IProductPurchasedReportServices _productPurchasedReportService;
        public ProductPurchasedReportApiController()
        {
            _productPurchasedReportService = new ProductPurchasedReportService();
        }
        [HttpGet]
        public HttpResponseMessage GetOrderStatus(int store_id, int LoggedInUserId, int language_id)
        {
            try
            {
                var result = _productPurchasedReportService.GetOrderStatus(store_id, LoggedInUserId, language_id);
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


        [HttpPost]
        public TableParameter<OrderProductEntity> GetProductPurchasedList(int PageIndex, int order_status_id, int store_id, int LoggedInUserId, DateTime? DateStart, DateTime? DateEnd, TableParameter<OrderProductEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _productPurchasedReportService.GetProductPurchasedList(sortColumn, order_status_id, store_id, LoggedInUserId, tableParameter, DateStart, DateEnd).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<OrderProductEntity>
                {
                    aaData = result.ToList(),
                    iTotalRecords = totalRecords,
                    iTotalDisplayRecords = totalRecords
                };
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }


        [HttpPost]
        public TableParameter<OrderProductEntity> GetProductViewedList(int store_id, int LoggedInUserId, int PageIndex, TableParameter<OrderProductEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _productPurchasedReportService.GetProductViewedList(store_id, LoggedInUserId, sortColumn, tableParameter).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<OrderProductEntity>
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
        public HttpResponseMessage ExportToExcelProductPurchasedList(int PageIndex, int order_status_id, int store_id, int LoggedInUserId, DateTime? DateStart, DateTime? DateEnd, object tableParameter)
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
                sortColumn = "name desc";
            }

           
           // string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";


            HttpResponseMessage httpResponseMessage;
            DateTime date = DateTime.Now.Date;
            string str = string.Concat("ProductPurchased", date.ToString("ddMMyyyy"), ".xls");

            DataSet ds = this._productPurchasedReportService.ExportToExcelProductPurchasedList(sortColumn, order_status_id, store_id, LoggedInUserId, DateStart, DateEnd);
            if (ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    if (!(ds.Tables[i].Rows.Count > 0))
                    {
                        ds.Tables.RemoveAt(i);
                    }
                }
            }
            if (ds != null && ds.Tables.Count > 0)
            {
            
                    List<ColumnConfiguration> selectedColumn = new List<ColumnConfiguration>();
                    selectedColumn.Add(new ColumnConfiguration("name", "Product Name"));
                    selectedColumn.Add(new ColumnConfiguration("model", "Model"));
                    selectedColumn.Add(new ColumnConfiguration("Categoryname", "Category"));
                    selectedColumn.Add(new ColumnConfiguration("storename", "Store"));
                    selectedColumn.Add(new ColumnConfiguration("quantity", "Quantity"));
                    selectedColumn.Add(new ColumnConfiguration("purchaseddatestr", "Purchased Date"));
                    selectedColumn.Add(new ColumnConfiguration("totalpoints", "total points"));

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
                byte[] asByteArray = GeneralMethods.ExportToExcel(ds, "Products Purchased");

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


    }
}