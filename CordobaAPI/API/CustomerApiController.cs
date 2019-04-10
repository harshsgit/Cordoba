using CordobaModels.Entities;
using CordobaServices.Helpers;
using CordobaServices.Interfaces;
using CordobaServices.Services;
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
using System.Configuration;
using Newtonsoft.Json;
using CordobaCommon;
using System.Net.Http.Headers;
using CordobaModels;


namespace CordobaAPI.API
{
    public class CustomerApiController : ApiController
    {
        public ICustomerService _CustomerService;

        public CustomerApiController()
        {
            _CustomerService = new CustomerService();
        }


        [HttpPost]
        public TableParameter<CustomerEntity> GetCustomerList(int PageIndex, string customerName, string email, int? customer_group_id, int? status, int? approved, string ip, DateTime? date_added, int storeId, TableParameter<CustomerEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _CustomerService.GetCustomerList(sortColumn, tableParameter, customerName, email, customer_group_id, status, approved, ip, date_added, storeId).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<CustomerEntity>
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
        public HttpResponseMessage CustomerExportToExcel(int PageIndex, string customerName, string email, int? customer_group_id, int? status, int? approved, string ip, DateTime? date_added, int storeId, object tableParameter)
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
                sortColumn = "customerName desc";
            }

            HttpResponseMessage httpResponseMessage;
            DateTime date = DateTime.Now.Date;
            string str = string.Concat("Customers_export", date.ToString("ddMMyyyy"), ".xls");

            DataSet ds = _CustomerService.CustomerExportToExcel(sortColumn, tableParameter, customerName, email, customer_group_id, status, approved, ip, date_added, storeId);

            if (ds != null && ds.Tables.Count > 0)
            {
                List<ColumnConfiguration> selectedColumn = new List<ColumnConfiguration>();
                selectedColumn.Add(new ColumnConfiguration("customer_id", "ID"));
                selectedColumn.Add(new ColumnConfiguration("firstname", "First Name"));
                selectedColumn.Add(new ColumnConfiguration("lastname", "Last Name"));
                //selectedColumn.Add(new ColumnConfiguration("customerName", "Name"));
                selectedColumn.Add(new ColumnConfiguration("email", "Email"));
                selectedColumn.Add(new ColumnConfiguration("StatusName", "Status"));
                selectedColumn.Add(new ColumnConfiguration("ip", "IP"));
                selectedColumn.Add(new ColumnConfiguration("date_added", "Date Added"));
                selectedColumn.Add(new ColumnConfiguration("approved", "Approved"));
                selectedColumn.Add(new ColumnConfiguration("Points", "Points"));
                selectedColumn.Add(new ColumnConfiguration("storeId", "StoreID"));
                selectedColumn.Add(new ColumnConfiguration("store_name", "Store Name"));
                selectedColumn.Add(new ColumnConfiguration("store_url", "Store Url"));
                selectedColumn.Add(new ColumnConfiguration("activated", "Activated"));
                selectedColumn.Add(new ColumnConfiguration("telephone", "Telephone"));
                selectedColumn.Add(new ColumnConfiguration("company", "Company"));
                selectedColumn.Add(new ColumnConfiguration("address_1", "Address 1"));
                selectedColumn.Add(new ColumnConfiguration("address_2", "Address 2"));
                selectedColumn.Add(new ColumnConfiguration("postcode", "Postcode"));
                selectedColumn.Add(new ColumnConfiguration("city", "City"));
                selectedColumn.Add(new ColumnConfiguration("country", "Country"));
                selectedColumn.Add(new ColumnConfiguration("customer_group_name", "Customer Group"));

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


        [HttpGet]
        public HttpResponseMessage GetCustomerById(int StoreId, int LoggedInUserId, int customer_id)
        {
            try
            {
                var result = _CustomerService.GetCustomerById(StoreId, LoggedInUserId, customer_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception e)
            {
                throw;
            }

        }


        [HttpPost]
        public HttpResponseMessage InsertUpdateCustomer(int LoggedInUserId, CustomerEntity customerEntity)
        {
            try
            {
                var result = _CustomerService.InsertUpdateCustomer(LoggedInUserId, customerEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public HttpResponseMessage DeleteCustomer(int StoreId, int LoggedInUserId, int customer_id)
        {
            try
            {
                var result = _CustomerService.DeleteCustomer(StoreId, LoggedInUserId, customer_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }
        }




        [HttpPost]
        public HttpResponseMessage PointsImporter(int store_id, int LoggedInUserId, bool IsSendEmail)
        {
            try
            {

                var directoryPath = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles");

                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }
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
                }

                string excelfilepath = filePath;
                string strConnectionString = "";
                if (excelfilepath.ToLower().Trim().EndsWith(".xlsx") || excelfilepath.ToLower().Trim().EndsWith(".xls") || excelfilepath.ToLower().Trim().EndsWith(".csv"))
                {
                    strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelfilepath);
                }
                //strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelfilepath);

                OleDbConnection OleDbConn = new OleDbConnection(strConnectionString);

                OleDbConn.Open();
                DataTable dtSheets = OleDbConn.GetSchema("Tables");

                OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();

                string sql = "SELECT * FROM [" + dtSheets.Rows[0]["Table_name"] + "]";
                DataTable dtXLS = new DataTable(Convert.ToString(dtSheets.Rows[0]["Table_name"]).Replace("$", ""));
                dtXLS.TableName = "Sheet1";
                OleDbCommand oleDbcommand = new OleDbCommand(sql, OleDbConn);

                OleDbAdapter.SelectCommand = oleDbcommand;
                OleDbAdapter.Fill(dtXLS);
                OleDbConn.Close();

                if (File.Exists(excelfilepath))
                {
                    File.Delete(excelfilepath);
                }

                if (dtXLS != null && dtXLS.Rows.Count > 0)
                {
                    try
                    {
                        dtXLS.Columns["Customer Email Address"].ColumnName = "email";
                        dtXLS.Columns["Points Adjustment"].ColumnName = "points";
                        dtXLS.Columns["Comment"].ColumnName = "comment";

                        var result = _CustomerService.PointsImporter(store_id, LoggedInUserId, IsSendEmail, dtXLS);
                        Console.Write(result);
                        return Request.CreateResponse(HttpStatusCode.OK, result);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }


                return Request.CreateResponse(HttpStatusCode.OK, 0);
            }
            catch (Exception)
            {
                throw;
            }

        }




        //[HttpPost]
        //public HttpResponseMessage CustomerImport(int store_id, int LoggedInUserId, int customer_group_id)
        //{
        //    try
        //    {

        //        var directoryPath = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles");

        //        if (!System.IO.Directory.Exists(directoryPath))
        //        {
        //            System.IO.Directory.CreateDirectory(directoryPath);
        //        }
        //        string filePath = directoryPath + @"\" + Guid.NewGuid().ToString() + ".xlsx";
        //        if (HttpContext.Current.Request.Files.AllKeys.Any())
        //        {
        //            // Get the uploaded image from the Files collection
        //            var httpPostedFile = HttpContext.Current.Request.Files[0];

        //            if (httpPostedFile != null)
        //            {
        //                string fileName = httpPostedFile.FileName;
        //                string contentType = httpPostedFile.ContentType;
        //                string extension = contentType.Substring(contentType.IndexOf('/') + 1, contentType.Length - contentType.IndexOf('/') - 1);
        //                int fileSize = httpPostedFile.ContentLength;
        //                byte[] fileData = null;

        //                using (var binaryReader = new BinaryReader(HttpContext.Current.Request.Files[0].InputStream))
        //                {
        //                    fileData = binaryReader.ReadBytes(HttpContext.Current.Request.Files[0].ContentLength);

        //                    File.WriteAllBytes(filePath, fileData);
        //                }


        //            }
        //        }

        //        string excelfilepath = filePath;
        //        string strConnectionString = "";
        //        if (excelfilepath.ToLower().Trim().EndsWith(".xlsx") || excelfilepath.ToLower().Trim().EndsWith(".xls") || excelfilepath.ToLower().Trim().EndsWith(".csv"))
        //        {
        //            strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelfilepath);
        //        }


        //        OleDbConnection OleDbConn = new OleDbConnection(strConnectionString);

        //        OleDbConn.Open();
        //        DataTable dtSheets = OleDbConn.GetSchema("Tables");

        //        OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();

        //        string sql = "SELECT * FROM [" + dtSheets.Rows[0]["Table_name"] + "]";
        //        DataTable dtXLS = new DataTable(Convert.ToString(dtSheets.Rows[0]["Table_name"]).Replace("$", ""));
        //        dtXLS.TableName = "Sheet1";
        //        OleDbCommand oleDbcommand = new OleDbCommand(sql, OleDbConn);

        //        OleDbAdapter.SelectCommand = oleDbcommand;
        //        OleDbAdapter.Fill(dtXLS);
        //        OleDbConn.Close();

        //        if (File.Exists(filePath))
        //        {
        //            File.Delete(filePath);
        //        }

        //        if (dtXLS != null && dtXLS.Rows.Count > 0)
        //        {
        //            try
        //            {
        //                dtXLS.Columns["First Name"].ColumnName = "firstname";
        //                dtXLS.Columns["Last Name"].ColumnName = "lastname";
        //                dtXLS.Columns["Email Address"].ColumnName = "email";
        //                dtXLS.Columns["Initial Points"].ColumnName = "points";
        //                dtXLS.Columns["Company_Address"].ColumnName = "company";
        //                dtXLS.Columns["Address 1"].ColumnName = "address_1";
        //                dtXLS.Columns["Address 2"].ColumnName = "address_2";
        //                dtXLS.Columns["City"].ColumnName = "city";
        //                dtXLS.Columns["County"].ColumnName = "county";
        //                dtXLS.Columns["Post Code"].ColumnName = "postcode";

        //                var result = _CustomerService.CustomerImport(store_id, LoggedInUserId, customer_group_id, dtXLS);
        //                return Request.CreateResponse(HttpStatusCode.OK, result);
        //            }
        //            catch (Exception)
        //            {

        //                throw;
        //            }
        //        }


        //        return Request.CreateResponse(HttpStatusCode.OK, 0);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        [HttpPost]
        public string CustomerImport(int store_id, int LoggedInUserId, int customer_group_id)
        {
            try
            {

                var directoryPath = System.Web.HttpContext.Current.Server.MapPath("~/TempFiles");

                if (!System.IO.Directory.Exists(directoryPath))
                {
                    System.IO.Directory.CreateDirectory(directoryPath);
                }
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
                }

                string excelfilepath = filePath;
                string strConnectionString = "";
                if (excelfilepath.ToLower().Trim().EndsWith(".xlsx") || excelfilepath.ToLower().Trim().EndsWith(".xls") || excelfilepath.ToLower().Trim().EndsWith(".csv"))
                {
                    strConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\";", excelfilepath);
                }


                OleDbConnection OleDbConn = new OleDbConnection(strConnectionString);

                OleDbConn.Open();
                DataTable dtSheets = OleDbConn.GetSchema("Tables");

                OleDbDataAdapter OleDbAdapter = new OleDbDataAdapter();

                string sql = "SELECT * FROM [" + dtSheets.Rows[0]["Table_name"] + "]";
                DataTable dtXLS = new DataTable(Convert.ToString(dtSheets.Rows[0]["Table_name"]).Replace("$", ""));
                dtXLS.TableName = "Sheet1";
                OleDbCommand oleDbcommand = new OleDbCommand(sql, OleDbConn);

                OleDbAdapter.SelectCommand = oleDbcommand;
                OleDbAdapter.Fill(dtXLS);
                OleDbConn.Close();

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                if (dtXLS != null && dtXLS.Rows.Count > 0)
                {
                    try
                    {
                        dtXLS.Columns["First Name"].ColumnName = "firstname";
                        dtXLS.Columns["Last Name"].ColumnName = "lastname";
                        dtXLS.Columns["Email Address"].ColumnName = "email";
                        dtXLS.Columns["Initial Points"].ColumnName = "points";
                        //dtXLS.Columns["Company (Address)"].ColumnName = "company";
                        //dtXLS.Columns["Address 1"].ColumnName = "address_1";
                        //dtXLS.Columns["Address 2"].ColumnName = "address_2";
                        //dtXLS.Columns["City"].ColumnName = "city";
                        //dtXLS.Columns["County"].ColumnName = "county";
                        //dtXLS.Columns["Post Code"].ColumnName = "postcode";

                        for (int i = 0; i < dtXLS.Rows.Count; i++)
                        {
                            if (!(IsValidEmail(Convert.ToString(dtXLS.Rows[i]["email"]))))
                            {
                                var str = "Email is not valid : " + Convert.ToString(dtXLS.Rows[i]["email"]);
                                return str;
                            }
                            // do something with dr
                        }

                        dtXLS.Columns.Add(new DataColumn("password", typeof(string)));

                        Random r = new Random();
                        string userPassword = Convert.ToString(r.Next(1, 100000));

                        foreach (DataRow item in dtXLS.Rows)
                        {
                            item["password"] = Security.Encrypt(userPassword);
                        }

                        var result = _CustomerService.CustomerImport(store_id, LoggedInUserId, customer_group_id, dtXLS, userPassword);
                        return result;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    var errorMsg = "Records Not found!";
                    return errorMsg;
                }


                return "";
            }
            catch (Exception ex)
            {
                throw;
            }

        }



        [HttpPost]
        public HttpResponseMessage UploadUserImage(int customerImage_id, int customer_id)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.CustomerImage.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string childFolderPath = folderPath + "/" + customer_id;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        //childFolderPath += "/" + banner_image_id;
                        //if (!Directory.Exists(childFolderPath))
                        //{
                        //    Directory.CreateDirectory(childFolderPath);
                        //}

                        string fileName = customer_id + "/" + httpPostedFile.FileName;
                        res = _CustomerService.UploadUserImage(customerImage_id, customer_id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.CustomerImage.ToString() + "/" + fileName);

                        if (res == true)
                        {
                            httpPostedFile.SaveAs(folderPath + "\\" + fileName);

                            var directoryFiles = Directory.GetFiles(childFolderPath);
                            foreach (var filepath in directoryFiles)
                            {
                                if (Path.GetFileName(filepath) != httpPostedFile.FileName)
                                {
                                    File.Delete(filepath);
                                }
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                return Request.CreateResponse(HttpStatusCode.OK);


            }
            if (res == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { data = true });
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented, new { data = false });
        }

        [HttpGet]
        public HttpResponseMessage GetUserImage(int customer_id)
        {
            try
            {
                var result = _CustomerService.getUserImage(customer_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteCustomerImage(int customer_id)
        {
            try
            {
                var result = _CustomerService.deleteCustomerImage(customer_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertPointAudit(int customer_id, string description, int points)
        {
            try
            {
                var result = _CustomerService.InsertPointAudit(customer_id, description, points);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage SendCustomerPassword(int customer_id, string NewPassword)
        {
            try
            {
                //Random rnd = new Random();
                //string password = rnd.Next(0, 1000000).ToString("D6");
                //string encryptedpasssword = Security.Encrypt(rnd.Next(0, 1000000).ToString("D6"));

                var result = _CustomerService.SendCustomerPassword(customer_id, NewPassword);
               
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        

        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


    }
}
