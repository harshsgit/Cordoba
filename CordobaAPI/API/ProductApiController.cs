using CordobaServices.Interfaces;
using CordobaServices.Helpers;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaModels.Entities;
using System.Web;
using System.Configuration;
using System.IO;



namespace CordobaAPI.API
{
    public class ProductApiController : ApiController
    {
        public IProductServices _ProductServices;
        public ProductApiController()
        {
            _ProductServices = new ProductService();
        }
        [HttpPost]
        public TableParameter<ProductEntity> GetProductList(int StoreId, int LoggedInUserId, int PageIndex, string name, decimal? Price, int? status, string Model, int? Quantity, TableParameter<ProductEntity> tableParameter)
        {
            try
            {

                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _ProductServices.GetProductList(StoreId, LoggedInUserId, sortColumn, tableParameter, name, Price, status, Model, Quantity).ToList();
                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }
                return new TableParameter<ProductEntity>
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
        public HttpResponseMessage GetProductById(int StoreId, int LoggedInUserId, int product_id)
        {
            try
            {
                var result = _ProductServices.GetProductById(StoreId, LoggedInUserId, product_id);
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
        public HttpResponseMessage AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id)
        {
            try
            {
                var result = _ProductServices.AddProductToCart(store_id, customer_id, product_id, qty, cartgroup_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage DeleteProductFromCart(int StoreId, int LoggedInUserId, int cart_id)
        {
            try
            {
                var result = _ProductServices.DeleteProductFromCart(StoreId, LoggedInUserId, cart_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public HttpResponseMessage InsertUpdateProduct(int StoreId, int LoggedInUserId, ProductEntity productEntity)
        {
            try
            {
                var result = _ProductServices.InsertUpdateProduct(StoreId, LoggedInUserId, productEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetProductListByCategoryAndStoreId(int StoreID, int CategoryId, int PageIndex, int Customer_Id = 0, string WhatAreYouLookingFor = "", string SearchByFilterId = "", int OrderById = 1)
        {
            try
            {
                var result = _ProductServices.GetProductListByCategoryAndStoreId(StoreID, CategoryId, PageIndex, Customer_Id, WhatAreYouLookingFor, SearchByFilterId, OrderById);
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
        public HttpResponseMessage DeleteProduct(int StoreId, int LoggedInUserId, int product_id)
        {
            try
            {
                var result = _ProductServices.DeleteProduct(StoreId, LoggedInUserId, product_id);
                if (result > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetProductDetailForLayout(int StoreID, int ProductId, int CustomerId)
        {
            try
            {
                var result = _ProductServices.GetProductDetailForLayout(StoreID, ProductId, CustomerId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetRelatedProductList(int StoreID, int SelectedProductId, int RelatedProductId, int customer_id)
        {
            try
            {
                var result = _ProductServices.GetRelatedProductList(StoreID, SelectedProductId, RelatedProductId, customer_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertAsHotProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity)
        {
            try
            {
                var result = _ProductServices.InsertAsHotProduct(LoggedInUserId, hotSpecialProductEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertAsSpecialProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity)
        {
            try
            {
                var result = _ProductServices.InsertAsSpecialProduct(LoggedInUserId, hotSpecialProductEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public HttpResponseMessage GetHotOrSpecialProductById(int store_id, int LoggedInUserId, int product_id)
        {
            try
            {
                var result = _ProductServices.GetHotOrSpecialProductById(store_id, LoggedInUserId, product_id);
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
        public HttpResponseMessage GetHotOrSpecialProductDetailById(int store_id, int LoggedInUserId, bool IsHotProduct, int product_id)
        {
            try
            {
                var result = _ProductServices.GetHotOrSpecialProductDetailById(store_id, LoggedInUserId, IsHotProduct, product_id);
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
        public HttpResponseMessage UploadProductImage(int product_id)
        {
            bool res = false;
            if (HttpContext.Current.Request.Files.AllKeys.Any())
            {
                // Get the uploaded image from the Files collection
                var httpPostedFile = HttpContext.Current.Request.Files[0];

                if (httpPostedFile != null)
                {
                    string folderPath = ConfigurationManager.AppSettings["FileUploadPath"].ToString() + "data//" + CordobaCommon.Enum.CommonEnums.FolderName.productImage.ToString();
                    if (!string.IsNullOrWhiteSpace(folderPath))
                    {
                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        string childFolderPath = folderPath + "/" + product_id;
                        if (!Directory.Exists(childFolderPath))
                        {
                            Directory.CreateDirectory(childFolderPath);
                        }

                        string fileName = product_id + "/" + httpPostedFile.FileName;

                        //System.IO.File.AppendAllText(@"C:\FileCheck.txt", folderPath + Environment.NewLine );
                        //System.IO.File.AppendAllText(@"C:\FileCheck.txt", childFolderPath + Environment.NewLine);

                        res = _ProductServices.UploadProductImage(product_id, "data/" + CordobaCommon.Enum.CommonEnums.FolderName.productImage.ToString() + "/" + fileName);

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
        public HttpResponseMessage GetProductImageById(int product_id)
        {
            try
            {
                var result = _ProductServices.GetProductImageById(product_id);
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


        [HttpGet]
        public HttpResponseMessage GetSubCategoryList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _ProductServices.GetSubCategoryList(StoreId, LoggedInUserId);
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

        [HttpGet]
        public HttpResponseMessage GetProductBycategoryForStore(int category_id, int store_id)
        {
            try
            {
                var result = _ProductServices.GetProductBycategoryForStore(category_id, store_id);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public HttpResponseMessage ExcludeProduct(int store_id, string product_id, string operation)
        {
            try
            {
                var result = _ProductServices.ExcludeProduct(store_id, product_id, operation);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetShippingCostDetail(int product_id)
        {
            try
            {
                var result = _ProductServices.GetShippingCostDetail(product_id);
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
        public HttpResponseMessage updateShippingCost(int product_id, int country_id, decimal shipping_cost)
        {
            try
            {
                var result = _ProductServices.updateShippingCost(product_id, country_id, shipping_cost);

                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetBestSellerByProductId(int productId)
        {
            try
            {
                var result = _ProductServices.GetBestSellerByProductId(productId);
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