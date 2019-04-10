using CordobaModels;
using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Services
{
    public class ProductService : IProductServices
    {
        private GenericRepository<HotSpecialProductEntity> hotSpecialProductEntityGenericRepository = new GenericRepository<HotSpecialProductEntity>();

        private GenericRepository<ProductEntity> objGenericRepository = new GenericRepository<ProductEntity>();
        public List<ProductEntity> GetProductList(int StoreId, int LoggedInUserId, string sortColumn, TableParameter<ProductEntity> filter, string name, decimal? Price, int? status, string Model, int? Quantity)
        {
            try
            {
                var ParameterStoreId = new SqlParameter
                {
                    ParameterName = "StoreId",
                    DbType = DbType.Int32,
                    Value = StoreId
                };
                var ParameterLoggedInUserId = new SqlParameter
                {
                    ParameterName = "LoggedInUserId",
                    DbType = DbType.Int32,
                    Value = LoggedInUserId
                };
                var paramOrderBy = new SqlParameter { ParameterName = "OrderBy", DbType = DbType.String, Value = sortColumn };
                var paramPageSize = new SqlParameter { ParameterName = "PageSize", DbType = DbType.Int32, Value = filter != null ? filter.iDisplayLength : 10 };
                var paramPageIndex = new SqlParameter { ParameterName = "PageIndex", DbType = DbType.Int32, Value = filter != null ? filter.PageIndex : 1 };
                //var paramPageFrom = new SqlParameter { ParameterName = "PageFrom", DbType = DbType.String, Value = PageFrom };

                var paramName = new SqlParameter { ParameterName = "name", DbType = DbType.String, Value = name ?? DBNull.Value.ToString() };
                var paramPrice = new SqlParameter { ParameterName = "price", DbType = DbType.Decimal, Value = Price ?? (object)DBNull.Value };
                var paramStatus = new SqlParameter { ParameterName = "status", DbType = DbType.Int32, Value = status ?? (object)DBNull.Value };
                var paramModel = new SqlParameter { ParameterName = "Model", DbType = DbType.String, Value = Model ?? DBNull.Value.ToString() };
                var paramQuantity = new SqlParameter { ParameterName = "Quantity", DbType = DbType.Int32, Value = Quantity ?? (object)DBNull.Value };

                var query = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductList", ParameterStoreId, ParameterLoggedInUserId, paramOrderBy, paramPageSize, paramPageIndex, paramName, paramPrice, paramStatus, paramModel, paramQuantity).ToList<ProductEntity>();
                return query;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductEntity GetProductById(int StoreId, int LoggedInUserId, int product_id)
        {
            ProductEntity ProductEntity = new ProductEntity();
            List<ProductDescriptionList> ProductDescriptionList = new List<ProductDescriptionList>();
            List<CatalogueEntity> CatalogueList = new List<CatalogueEntity>();

            var ParameterStoreId = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserId = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            if (product_id > 0)
            {
                var paramProductId = new SqlParameter { ParameterName = "product_id", DbType = DbType.Int32, Value = product_id };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductById", ParameterStoreId, ParameterLoggedInUserId, paramProductId).FirstOrDefault();
                ProductEntity = result;

                var paramProductIdForDesc = new SqlParameter { ParameterName = "product_id", DbType = DbType.Int32, Value = product_id };
                var DescResult = objGenericRepository.ExecuteSQL<ProductDescriptionList>("GetProductDescriptionList", paramProductIdForDesc).ToList<ProductDescriptionList>();
                if (DescResult != null)
                    ProductDescriptionList = DescResult.ToList();
            }
            else
            {
                ProductEntity = new ProductEntity();
                ProductEntity.ProductDescriptionList = ProductDescriptionList;
                ProductEntity.status = 1;
            }
            var ParameterStoreIdForProduct = new SqlParameter
            {
                ParameterName = "StoreId",
                DbType = DbType.Int32,
                Value = StoreId
            };
            var ParameterLoggedInUserIdForProduct = new SqlParameter
            {
                ParameterName = "LoggedInUserId",
                DbType = DbType.Int32,
                Value = LoggedInUserId
            };
            var paramProductIdForProduct = new SqlParameter
            {
                ParameterName = "product_id",
                DbType = DbType.Int32,
                Value = product_id
            };
            var catalogueResult = objGenericRepository.ExecuteSQL<CatalogueEntity>("GetProductToCatalogueList", ParameterStoreIdForProduct, ParameterLoggedInUserIdForProduct, paramProductIdForProduct).ToList<CatalogueEntity>();
            if (catalogueResult != null)
                CatalogueList = catalogueResult.ToList();

            ProductEntity.ProductDescriptionList = ProductDescriptionList;
            ProductEntity.CatalogueList = CatalogueList;
            return ProductEntity;
        }

        public CartEntity AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("store_id",store_id)
                                                 , new SqlParameter("customer_id", customer_id)
                                                 , new SqlParameter("product_id", product_id)
                                                 , new SqlParameter("qty", qty)
                                                 , new SqlParameter("cartgroup_id", cartgroup_id)};

            var result = objGenericRepository.ExecuteSQL<CartEntity>("AddProductToCart", sqlParameter).FirstOrDefault();
            return result;

        }

        public int DeleteProductFromCart(int store_id, int LoggedInUserId, int cart_id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("store_id",store_id)
                                                 , new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                 , new SqlParameter("cart_id", cart_id)
                                               };

            int result = objGenericRepository.ExecuteSQL<int>("DeleteProductFromCart", sqlParameter).FirstOrDefault();
            return result;

        }


        public int InsertUpdateProduct(int store_id, int LoggedInUserId, ProductEntity productEntity)
        {
            string ProductDescriptionXml = Helpers.ConvertToXml<ProductDescriptionList>.GetXMLString(productEntity.ProductDescriptionList);

            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("store_id",store_id)
                                                 , new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                 , new SqlParameter("product_id", productEntity.product_id)
                                                 , new SqlParameter("Image", productEntity.Image ??  (object)DBNull.Value)
                                                 , new SqlParameter("Model", productEntity.Model ??  (object)DBNull.Value)
                                                 , new SqlParameter("Price", productEntity.Price)
                                                 , new SqlParameter("Quantity", productEntity.Quantity ??  (object)DBNull.Value )
                                                 , new SqlParameter("minimum", productEntity.minimum ??  (object)DBNull.Value)
                                                 , new SqlParameter("subtract", productEntity.subtract ??  (object)DBNull.Value)
                                                 , new SqlParameter("stock_status_id", productEntity.stock_status_id ??  (object)DBNull.Value)
                                                 , new SqlParameter("shipping", productEntity.shipping ??  (object)DBNull.Value)
                                                 , new SqlParameter("date_available", productEntity.date_available  ??  (object)DBNull.Value)
                                                 , new SqlParameter("status", productEntity.status ??  (object)DBNull.Value)
                                                 , new SqlParameter("sort_order", productEntity.sort_order ??  (object)DBNull.Value)
                                                 , new SqlParameter("shipping_cost", productEntity.shipping_cost )
                                                 , new SqlParameter("country_id", productEntity.country_id ??  (object)DBNull.Value)
                                                 , new SqlParameter("manufacturer_id", productEntity.manufacturer_id ??  (object)DBNull.Value)
                                                 , new SqlParameter("category_id", productEntity.category_id ??  (object)DBNull.Value)
                                                 , new SqlParameter("supplier_id", productEntity.supplier_id ??  (object)DBNull.Value)
                                                 , new SqlParameter("CatalogueIdCSV", productEntity.CatalogueIdCSV ??  (object)DBNull.Value)
                                                 , new SqlParameter("ProductDescriptionXml", ProductDescriptionXml ??  (object)DBNull.Value)
                                                 ,new SqlParameter("StoreIds", productEntity.StoreIds ??   DBNull.Value.ToString())
                                                };
            int result = objGenericRepository.ExecuteSQL<int>("InsertUpdateProduct", sqlParameter).FirstOrDefault();
            return result;
        }

        public List<ProductEntity> GetProductListByCategoryAndStoreId(int StoreID, int CategoryId, int PageIndex, int Customer_Id = 0, string WhatAreYouLookingFor = "", string SearchByFilterId = "", int OrderById = 1)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                   new SqlParameter("StoreID", StoreID)
                                                   , new SqlParameter("CategoryId",CategoryId)
                                                   , new SqlParameter("PageIndex",PageIndex)
                                                    , new SqlParameter("Customer_Id",Customer_Id)
                                                       , new SqlParameter("WhatAreYouLookingFor",WhatAreYouLookingFor==null?"":WhatAreYouLookingFor)
                                                       , new SqlParameter("SearchByFilterId",SearchByFilterId==null?"":SearchByFilterId)
                                                       , new SqlParameter("OrderById",OrderById)
                                               };

            var ProductList = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductListByCategoryAndStoreId", sqlParameter).ToList();
            return ProductList;

        }



        public int DeleteProduct(int StoreId, int LoggedInUserId, int product_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                                new SqlParameter("StoreId",StoreId)
                                                                ,new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                                ,new SqlParameter("product_id", product_id)
                };
                int result = objGenericRepository.ExecuteSQL<int>("DeleteProduct", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ProductEntity GetProductDetailForLayout(int StoreId, int ProductId, int CustomerId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                            new SqlParameter("StoreId", StoreId)
                                                           ,new SqlParameter("ProductId", ProductId)
                                                           ,new SqlParameter("CustomerId", CustomerId)
                                                                };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductDetailForLayout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<ProductEntity> GetRelatedProductList(int StoreId, int SelectedProductId, int RelatedProductId, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                            new SqlParameter("StoreId",StoreId)
                                                           ,new SqlParameter("SelectedProductId", SelectedProductId)
                                                           ,new SqlParameter("RelatedProductId", RelatedProductId)
                                                           ,new SqlParameter("customer_id", customer_id)
                                                                };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetRelatedProductListForLayout", sqlParameter).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int InsertAsHotProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntityGenericRepository)
        {
            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                  new SqlParameter("hot_productid", hotSpecialProductEntityGenericRepository.hot_productid)
                                                , new SqlParameter("store_id", hotSpecialProductEntityGenericRepository.store_id)
                                                , new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                , new SqlParameter("product_id", hotSpecialProductEntityGenericRepository.product_id)
                                                , new SqlParameter("priority", hotSpecialProductEntityGenericRepository.priority)
                                                , new SqlParameter("startDate", hotSpecialProductEntityGenericRepository.startDate)
                                                , new SqlParameter("endDate", hotSpecialProductEntityGenericRepository.endDate)
                                                , new SqlParameter("status", hotSpecialProductEntityGenericRepository.status)
                                                , new SqlParameter("created_by", hotSpecialProductEntityGenericRepository.created_by)

                                                };
            int result = objGenericRepository.ExecuteSQL<int>("InsertAsHotProduct", sqlParameter).FirstOrDefault();
            return result;
        }

        public int InsertAsSpecialProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntityGenericRepository)
        {

            SqlParameter[] sqlParameter = new SqlParameter[] {
                                                  new SqlParameter("special_productid", hotSpecialProductEntityGenericRepository.special_productid)
                                                , new SqlParameter("store_id", hotSpecialProductEntityGenericRepository.store_id)
                                                , new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                , new SqlParameter("product_id", hotSpecialProductEntityGenericRepository.product_id)
                                                , new SqlParameter("priority", hotSpecialProductEntityGenericRepository.priority)
                                                , new SqlParameter("startDate", hotSpecialProductEntityGenericRepository.startDate)
                                                , new SqlParameter("endDate", hotSpecialProductEntityGenericRepository.endDate)
                                                , new SqlParameter("status", hotSpecialProductEntityGenericRepository.status)
                                                , new SqlParameter("created_by", hotSpecialProductEntityGenericRepository.created_by)

                                                };
            int result = objGenericRepository.ExecuteSQL<int>("InsertAsSpecialProduct", sqlParameter).FirstOrDefault();
            return result;
        }

        public List<HotSpecialProductEntity> GetHotOrSpecialProductById(int store_id, int LoggedInUserId, int product_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                           new SqlParameter("store_id", store_id)
                                                           ,new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                           ,new SqlParameter("product_id", product_id)
                                                                };
                var result = objGenericRepository.ExecuteSQL<HotSpecialProductEntity>("GetHotOrSpecialProductById", sqlParameter).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HotSpecialProductEntity> GetHotOrSpecialProductDetailById(int store_id, int LoggedInUserId, bool IsHotProduct, int product_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                            new SqlParameter("store_id",store_id)
                                                           ,new SqlParameter("LoggedInUserId",LoggedInUserId)
                                                           ,new SqlParameter("IsHotProduct",IsHotProduct)
                                                           ,new SqlParameter("product_id", product_id)
                                                                };
                var result = objGenericRepository.ExecuteSQL<HotSpecialProductEntity>("GetHotOrSpecialProductDetailById", sqlParameter).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UploadProductImage(int product_id, string image)
        {
            try
            {
                //System.IO.File.AppendAllText(@"C:\FileCheck.txt", image);

                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("product_id", product_id),
                    new SqlParameter("image",image),

                };
                int result = objGenericRepository.ExecuteSQL<int>("UploadProductImage", sqlParameter).FirstOrDefault();
                if (result > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ProductEntity> GetProductImageById(int product_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("product_id", product_id) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductImageById", sqlParameter).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<CategoryEntity> GetSubCategoryList(int StoreId, int LoggedInUserId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreId", StoreId), new SqlParameter("LoggedInUserId", LoggedInUserId) };
                var result = objGenericRepository.ExecuteSQL<CategoryEntity>("GetSubCategoryList", sqlParameter).ToList();
                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public List<ProductEntity> GetProductBycategoryForStore(int category_id, int store_id)
        {
            List<ProductEntity> product = new List<ProductEntity>();
            try
            {
                var paramCategoryId = new SqlParameter
                {
                    ParameterName = "category_id",
                    DbType = DbType.Int32,
                    Value = category_id
                };

                var paramStoreId = new SqlParameter
                {
                    ParameterName = "store_id",
                    DbType = DbType.Int32,
                    Value = store_id
                };

                var Result = objGenericRepository.ExecuteSQL<ProductEntity>("GetProductBycategoryForStore", paramCategoryId, paramStoreId).ToList<ProductEntity>();

                if (Result != null)
                    product = Result.ToList();

            }
            catch (Exception)
            {
                throw;
            }
            return product;
        }

        public int ExcludeProduct(int store_id, string product_id, string operation)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                                new SqlParameter("store_id",store_id)
                                                                ,new SqlParameter("product_id",product_id)
                                                                ,new SqlParameter("operation", operation)
                };
                int result = objGenericRepository.ExecuteSQL<int>("ExcludeProduct", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CountryEntity> GetShippingCostDetail(int product_id)
        {
            List<CountryEntity> product = new List<CountryEntity>();
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("product_id",product_id)
                };

                product = objGenericRepository.ExecuteSQL<CountryEntity>("GetShippingCostDetail", sqlParameter).ToList<CountryEntity>();

                return product;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public int updateShippingCost(int product_id, int country_id, decimal shipping_cost)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                                                                new SqlParameter("product_id",product_id)
                                                                ,new SqlParameter("country_id",country_id)
                                                                ,new SqlParameter("shipping_cost", shipping_cost)
                };
                int result = objGenericRepository.ExecuteSQL<int>("updateShippingCost", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BestSellerEntity> GetBestSellerByProductId(int productId)
        {
            var paramProductId = new SqlParameter { ParameterName = "ProductId", DbType = DbType.Int32, Value = productId };
            var result = objGenericRepository.ExecuteSQL<BestSellerEntity>("GetBestSellerByProductId", paramProductId).ToList();
            return result;
        }
    }


}
