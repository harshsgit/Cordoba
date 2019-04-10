using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;


namespace CordobaAPI.API
{
    public class BannerApiController : ApiController
    {
        public IBannerServices _BannerServices;
        public BannerApiController()
        {
            _BannerServices = new BannerService();
        }
        [HttpGet]
        public HttpResponseMessage GetBannerList()
        {
            try
            {
                var result = _BannerServices.GetBannerList();
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
        public HttpResponseMessage GetBannerById(int bannerId)
        {
            try
            {
                var result = _BannerServices.GetBannerById(bannerId);
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
        public HttpResponseMessage GetBannerImageList(int bannerId)
        {
            try
            {
                var result = _BannerServices.GetBannerImageById(bannerId);
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
        public HttpResponseMessage UploadBannerImage(int banner_id, int banner_image_id, string link, int sort_order)
        {
            //bool res = false;
            long res = 0;
            bool result = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    try
                    {
                        string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.BannerImage.ToString();
                        if (!string.IsNullOrWhiteSpace(folderPath))
                        {
                            if (!Directory.Exists(folderPath))
                            {
                                Directory.CreateDirectory(folderPath);
                            }

                            string childFolderPath = folderPath + "/" + banner_id;
                            if (!Directory.Exists(childFolderPath))
                            {
                                Directory.CreateDirectory(childFolderPath);
                            }

                            //childFolderPath += "/" + banner_image_id;

                            //string fileName = banner_id + "/" + banner_image_id + "/" + httpPostedFile.FileName;
                            
                            //res = _BannerServices.UploadBannerImage(banner_id, banner_image_id, link, sort_order, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.BannerImage.ToString() + "/" + fileName, 0);
                            res = _BannerServices.UploadBannerImage(banner_id, banner_image_id, link, sort_order, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.BannerImage.ToString(), httpPostedFile.FileName, 0);

                            string fileName = banner_id + "/" + res + "/" + httpPostedFile.FileName;
                            childFolderPath += "/" + res;
                            if (!Directory.Exists(childFolderPath))
                            {
                                Directory.CreateDirectory(childFolderPath);
                            }
                           
                            if (res > 0)
                            {
                                result = true;
                            }
                            else
                            {
                                result = false;
                            }

                            if (result == true)
                            {
                                httpPostedFile.SaveAs(folderPath + "\\" + fileName);
                                //System.IO.File.AppendAllText("//192.168.1.145//Amitp//cordoba - original images//File.txt", "folderPath + fileName:" + folderPath + "\\" + fileName);
                                //System.IO.File.AppendAllText("//192.168.1.145//Amitp//cordoba - original images//File.txt", "\n");
                                var directoryFiles = Directory.GetFiles(childFolderPath);
                                
                                foreach (var filepath in directoryFiles)
                                {
                                    if (Path.GetFileName(filepath) != httpPostedFile.FileName)
                                    {
                                        //System.IO.File.AppendAllText("//192.168.1.145//Amitp//cordoba - original images//File.txt", "\n");
                                        //System.IO.File.AppendAllText("//192.168.1.145//Amitp//cordoba - original images//File.txt", "filepath:" + filepath);
                                        //System.IO.File.AppendAllText("//192.168.1.145//Amitp//cordoba - original images//File.txt", "\n");
                                        //System.IO.File.AppendAllText("//192.168.1.145//Amitp//cordoba - original images//File.txt", "httpPostedFile.FileName:" + httpPostedFile.FileName);
                                        File.Delete(filepath);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                    return Request.CreateResponse(HttpStatusCode.OK);
                }

                return Request.CreateResponse(HttpStatusCode.OK);


            }
            else
            {
                res = _BannerServices.UploadBannerImage(banner_id, banner_image_id, link, sort_order, null, null, 0);
                if (res > 0)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            if (result == true)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new { data = true });
            }

            return Request.CreateResponse(HttpStatusCode.NotImplemented, new { data = false });
        }


        [HttpGet]
        public HttpResponseMessage DeleteBannerImage(int banner_image_id)
        {
            try
            {
                var result = _BannerServices.DeleteBannerImage(banner_image_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage DeleteBanner(int banner_id)
        {
            try
            {
                var result = _BannerServices.DeleteBanner(banner_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception e)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertUpdateBanner(int banner_id, string name, int status)
        {
            try
            {
                var result = _BannerServices.InsertUpdateBanner(banner_id, name, status);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }

}
