using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CordobaProductImageDownloadService
{
    static class Program
    {
        static void Main(string[] args)
        {
            GetImportCatalogImageURLs(); 
        }
        static string fileimageyrl = string.Empty;
        public static List<ProductCatalogue> GetImportCatalogImageURLs()
        {
            List<ProductCatalogue> objProductCatalogue = new List<ProductCatalogue>();
            SqlConnection con = new SqlConnection();
            con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CordobaEntities1"].ConnectionString;

            con.Open();

            SqlCommand cmd = new SqlCommand("GetImportCatalogImageURLs", con);
            cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@Supplier_Id", supplierId);
            //cmd.Parameters.AddWithValue("@Catalogue_Id", catalogueId);

            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    ProductCatalogue recordProductCatalog = new ProductCatalogue();
                    recordProductCatalog.code = Convert.ToString(dr["code"]);
                    recordProductCatalog.image_full = Convert.ToString(dr["image_full"]);
                    recordProductCatalog.image_full_url = Convert.ToString(dr["image_full_url"]);
                    recordProductCatalog.IsOperationCompleted = Convert.ToBoolean(dr["IsOperationCompleted"] == null ? false : true);
                    recordProductCatalog.product_id = Convert.ToInt32(dr["product_id"]);//Convert.ToString(dr["product_id"]);
                    recordProductCatalog.CatalogueName = Convert.ToString(dr["CatalogueName"]);
                    objProductCatalogue.Add(recordProductCatalog);
                }
            }
            DownloadCatalogueImages(objProductCatalogue);
            ProductActive_CatalogImport();
            return objProductCatalogue;
            //return lstOfImageUrls;
        }



        public static void DownloadCatalogueImages(List<ProductCatalogue> productCatalogue)
        {
            List<ProductCatalogue> notdownloadableproducts = new List<ProductCatalogue>();
            ProductCatalogue objproductCatalogue = new ProductCatalogue();
            try
            {
                for (int i = 0; i < productCatalogue.Count; i++)
                {
                    objproductCatalogue = productCatalogue[i];
                    //For issue: Can not create SSL/TLS secure channel
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    //For issue: Can not create SSL/TLS secure channel

                    WebClient request = new WebClient();
                    if (productCatalogue[i].CatalogueName == null || productCatalogue[i].CatalogueName == "")
                    {
                        productCatalogue[i].CatalogueName = "Procurement Image Files";
                    }
                    string DirectoryPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ImportProductImagePath"]) + "data//" + productCatalogue[i].CatalogueName;

                    if (!System.IO.Directory.Exists(DirectoryPath))
                    {
                        System.IO.Directory.CreateDirectory(DirectoryPath);
                    }
                    try
                    {
                        fileimageyrl = productCatalogue[i].image_full;
                        //request.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                        request.DownloadFile(new Uri(productCatalogue[i].image_full_url), DirectoryPath + "//" + productCatalogue[i].image_full);
                        bool fileexists = System.IO.File.Exists(DirectoryPath + "//" + productCatalogue[i].image_full);
                        if (fileexists)
                        {
                            UpdateStatusDownloadedImage(productCatalogue[i].product_id);
                        }
                    }
                    catch (Exception)
                    {
                        notdownloadableproducts.Add(objproductCatalogue);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            try
            {
                for (int i = 0; i < notdownloadableproducts.Count; i++)
                {
                //added code
                //For issue: Can not create SSL/TLS secure channel
                out2: ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                //For issue: Can not create SSL/TLS secure channel
                //end added code
                out1: System.Drawing.Image image = null;
                    System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(notdownloadableproducts[i].image_full_url);

                    //webRequest.ProtocolVersion = HttpVersion.Version10;

                    webRequest.CookieContainer = new System.Net.CookieContainer();
                    webRequest.MaximumAutomaticRedirections = 999999999;

                    webRequest.AllowWriteStreamBuffering = true;
                    webRequest.Timeout = 999999999;
                    webRequest.AllowAutoRedirect = true;
                    webRequest.UserAgent = "Foo";
                    webRequest.Accept = "*/*";
                    //System.Net.WebResponse webResponse = webRequest.GetResponse();
                    System.Net.WebResponse webResponse = null;

                    try
                    {
                        webResponse = webRequest.GetResponse();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message == "The request was aborted: Could not create SSL/TLS secure channel.")
                        {
                            ServicePointManager.Expect100Continue = false;
                            ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3 | System.Net.SecurityProtocolType.Tls;
                            ServicePointManager.ServerCertificateValidationCallback = null;
                            goto out1;
                        }
                        else if (ex.Message == "The remote server returned an error: (403) Forbidden." ||
                                 ex.Message == "Unable to connect to the remote server." ||
                                 ex.Message == "The remote server returned an error: (404) Not Found." ||
                                 ex.Message == "The remote server returned an error: (400) Bad Request.")
                        {
                            i++;
                            if (i < notdownloadableproducts.Count)
                            {
                                goto out2;
                            }
                            else
                            {
                                break;
                            }
                        }
                        throw;
                    }

                    System.IO.Stream stream = webResponse.GetResponseStream();
                    try
                    {
                        image = System.Drawing.Image.FromStream(stream);
                    }
                    catch (Exception) 
                    {
                        continue;
                    }
                    webResponse.Close();
                    string DirectoryPath = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["ImportProductImagePath"]) + "data//" + notdownloadableproducts[i].CatalogueName;
                    if (!System.IO.Directory.Exists(DirectoryPath))
                    {
                        System.IO.Directory.CreateDirectory(DirectoryPath);
                    }
                    string rootPath = DirectoryPath;
                    string fileName = System.IO.Path.Combine(rootPath, notdownloadableproducts[i].image_full);
                    fileimageyrl = notdownloadableproducts[i].image_full;
                    image.Save(fileName);
                    bool fileexists = System.IO.File.Exists(DirectoryPath + "//" + notdownloadableproducts[i].image_full);
                    if (fileexists)
                    {
                        UpdateStatusDownloadedImage(notdownloadableproducts[i].product_id);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void UpdateStatusDownloadedImage(int ProductId)
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CordobaEntities1"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand("UpdateStatusDownloadedImage", con);
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.CommandType = CommandType.StoredProcedure;
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static void ProductActive_CatalogImport()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                con.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["CordobaEntities1"].ConnectionString;
                con.Open();
                SqlCommand cmd = new SqlCommand("ProductActive_CatalogImport", con);
                cmd.CommandType = CommandType.StoredProcedure;
                int result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
