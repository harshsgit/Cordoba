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
using System.Runtime.InteropServices;
using System.Management;
using System.Diagnostics;
using System.ServiceProcess;
//using Spire.Xls;

namespace CordobaAPI.API
{
    public class CatalogueApiController : ApiController
    {

        public ICatalogueServices _catalogueServices;
        public CatalogueApiController()
        {
            _catalogueServices = new CatalogueService();
        }
        [HttpGet]
        public HttpResponseMessage GetCatalogueList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _catalogueServices.GetCatalogueList(StoreId, LoggedInUserId);
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

        [HttpGet]
        public HttpResponseMessage GetCatalogueById(int catalogue_id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _catalogueServices.GetCatalogueById(StoreId, LoggedInUserId, catalogue_id);
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
        public HttpResponseMessage InsertUpdateCatalogue(int StoreId, int LoggedInUserId, CatalogueEntity catalogueEntity)
        {
            try
            {
                var result = _catalogueServices.InsertUpdateCatalogue(StoreId, LoggedInUserId, catalogueEntity);
                if (result >= -1)
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

        [HttpGet]
        public HttpResponseMessage DeleteCatalogue(int catalogue_id, int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _catalogueServices.DeleteCatalogue(catalogue_id, StoreId, LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage ImportCatalogue(int StoreId, int LoggedInUserId, int supplier_id, int language_id, int catalogue_id, bool IsConfirmToIgnore)
        {
            try
            {
                var directoryPath = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles");
                //DataTable dtXLS = null;
                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }
                //string filePath = directoryPath + @"\" + Guid.NewGuid().ToString() + ".xlsx";
                string filePath = directoryPath + @"\" + Guid.NewGuid().ToString() + ".xlsx";
                if (HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    // Get the uploaded image from the Files collection
                    var httpPostedFile = HttpContext.Current.Request.Files[0];

                    if (httpPostedFile != null)
                    {
                        string fileName = httpPostedFile.FileName;
                        string contentType = httpPostedFile.ContentType;
                        string extension = contentType.Substring(contentType.IndexOf('/') + 1, contentType.Length - contentType.IndexOf('/') - 1);
                        int fileSize = httpPostedFile.ContentLength;
                        byte[] fileData = null;

                        using (var binaryReader = new BinaryReader(HttpContext.Current.Request.Files[0].InputStream))
                        {
                            fileData = binaryReader.ReadBytes(HttpContext.Current.Request.Files[0].ContentLength);

                            File.WriteAllBytes(filePath, fileData);
                        }
                    }

                    //// Get the uploaded image from the Files collection
                    //var httpPostedFile = HttpContext.Current.Request.Files[0];
                    //httpPostedFile.SaveAs(filePath);
                    //Workbook workbook = new Workbook();
                    //workbook.LoadFromFile(filePath);
                    //Worksheet sheet = workbook.Worksheets[0];
                    //dtXLS = sheet.ExportDataTable();
                   
                }

                string excelfilepath = filePath;
                string strConnectionString = "";


                if (excelfilepath.ToLower().Trim().EndsWith(".xlsx") || excelfilepath.ToLower().Trim().EndsWith(".xls") || filePath.ToLower().Trim().EndsWith(".csv"))
                {
                    strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelfilepath);
                }

                //if (filePath.ToLower().Trim().EndsWith(".xlsx") || filePath.ToLower().Trim().EndsWith(".csv"))
                //{
                //    strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", filePath);
                //}


                OleDbConnection OleDbConn = new OleDbConnection(strConnectionString);

                OleDbConn.Open();
                DataTable dtSheets = OleDbConn.GetSchema("Tables");
                OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();

                string sql = "SELECT * FROM [" + dtSheets.Rows[0]["Table_name"] + "]";
                DataTable dtXLS = new DataTable(Convert.ToString(dtSheets.Rows[0]["Table_name"]).Replace("$", ""));
                dtXLS.TableName = "Sheet1";
                OleDbCommand oleDbcommand = new OleDbCommand(sql, OleDbConn);
                oleDbcommand.CommandTimeout = 120000000;
                OleDbAdapter.SelectCommand = oleDbcommand;
                OleDbAdapter.Fill(dtXLS);
                OleDbConn.Close();

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                List<ImportProductCatalogueEntity> result = _catalogueServices.ImportDatatoCatalogue(StoreId, LoggedInUserId, supplier_id, language_id, catalogue_id, dtXLS, IsConfirmToIgnore);
                if (!(result.Count > 0))
                {
                    StartCordobaInstallerService();
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void StartCordobaInstallerService()
        {
            try
            {
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/DownloadProductImage/CordobaProductImageDownloadService.exe");
                System.Diagnostics.Process.Start(path);

                //if (!System.Diagnostics.Debugger.IsAttached)
                //{
                //    System.Diagnostics.Debugger.Launch();
                //    System.Diagnostics.Debugger.Break();
                //}

                //var sc = new System.ServiceProcess.ServiceController();
                //sc.ServiceName = "CordobaInstaller";
                //sc.MachineName = System.Environment.MachineName;
                //if (Convert.ToString(sc.Status) == "Stopped")
                //{
                //    sc.Start();
                //}
            }
            catch (Exception)
            {
                
                throw;
            }
            

        }

        [HttpGet]
        public HttpResponseMessage GetCategoryListForStore(int StoreId)
        {
            try
            {
                var result = _catalogueServices.GetCategoryListForStore(StoreId);
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