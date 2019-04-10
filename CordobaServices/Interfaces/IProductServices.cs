using CordobaModels.Entities;
using CordobaServices.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces
{
    public interface IProductServices
    {
        List<ProductEntity> GetProductList(int StoreId, int LoggedInUserId, string sortColumn, TableParameter<ProductEntity> filter, string name, decimal? Price, int? status, string Model, int? Quantity);
        ProductEntity GetProductById(int StoreId, int LoggedInUserId, int product_id);
        CartEntity AddProductToCart(int store_id, int customer_id, int product_id, int qty, int cartgroup_id);
        int DeleteProductFromCart(int StoreId, int LoggedInUserId, int cart_id);

        int InsertUpdateProduct(int StoreId, int LoggedInUserId, ProductEntity productEntity);
        List<ProductEntity> GetProductListByCategoryAndStoreId(int StoreID, int CategoryId, int PageIndex, int Customer_Id, string WhatAreYouLookingFor, string SearchByFilterId, int OrderById);

        int DeleteProduct(int StoreId, int LoggedInUserId, int product_id);
        ProductEntity GetProductDetailForLayout(int StoreId, int ProductId, int CustomerId);
        List<ProductEntity> GetRelatedProductList(int StoreId, int SelectedProductId, int RelatedProductId, int customer_id);

        //InsertAsHotProduct
        int InsertAsHotProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity);
        int InsertAsSpecialProduct(int LoggedInUserId, HotSpecialProductEntity hotSpecialProductEntity);
        List<HotSpecialProductEntity> GetHotOrSpecialProductById(int store_id, int LoggedInUserId, int product_id);
        List<HotSpecialProductEntity> GetHotOrSpecialProductDetailById(int store_id, int LoggedInUserId, bool IsHotProduct, int product_id);
        bool UploadProductImage(int product_id, string image);

        List<ProductEntity> GetProductImageById(int product_id);
        List<CategoryEntity> GetSubCategoryList(int StoreId, int LoggedInUserId);
        List<ProductEntity> GetProductBycategoryForStore(int category_id, int store_id);
        int ExcludeProduct(int store_id, string product_id, string operation);

        List<CountryEntity> GetShippingCostDetail(int product_id);
        int updateShippingCost(int product_id, int country_id, decimal shipping_cost);

        List<BestSellerEntity> GetBestSellerByProductId(int productId);

    }
}
