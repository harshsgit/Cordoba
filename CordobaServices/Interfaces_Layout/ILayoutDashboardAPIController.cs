using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CordobaServices.Interfaces_Layout
{
    public interface ILayoutDashboardServices
    {
        List<CategoryEntity> GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory, int customer_id);
        StoreEntity GetStoreDetailByUrl(String URL);
        List<ProductEntity> GetLatestProductByStoreId(int StoreID, int Customer_Id);
        List<CategoryPopularEntity> GetPopularCategoryListByStoreId(int StoreID, int customer_id);
        List<ProductEntity> GetHotDealsListByStoreId(int StoreID, int customer_id);
        CustomerEntity CustomerLogin(CustomerEntity CustomerObj);
        int? AddtoWishList(wishlistEntity WishlistObj);
        int? RemoveFromWishList(int storeid, int product_id, int customer_Id);
        List<ProductEntity> GetSpecialOfferListByStoreId(int StoreID, int customer_id);
        CustomerEntity CustomerDetailLayout(int CustomerId, int StoreId);
        int? SaveCustomerBasicDetails_Layout(int StoreId, CustomerEntity CustomerObj);
        int? SaveChangedPassword_Layout(int StoreId, CustomerEntity CustomerObj);
        List<AddressEntity> GetCustomerAddressList_Layout(int StoreId, int customer_id);
        List<AddressEntity> AddOrUpdateAddressDetail_Layout(int StoreId, AddressEntity AddressObj);
        int? DeleteCustomerAddress(int StoreId, int customer_id, int address_id);
        List<StoreImageEntity> GetStoreImageList(int Store_Id);
        List<ProductEntity> GetBestSellerListByStoreId(int StoreID, int customer_id);
        List<StoreTermsEntity> GetStoreTermsDetail(int Store_Id);
        CustomerEntity ForgotPassword(CustomerEntity CustomerObj);
        List<OrderDetailCountEntity> GetOrderStatusCount(int StoreID);
        List<BannerAttributeEntity> GetBanner_Layout(int StoreId);
        CustomerEntity VerifyOTP(CustomerEntity CustomerObj);

        int? VerifyCustomerVisitedInfo(string CustomerEmail, int? store_id);
        CustomerEntity ResetPasswordOTP(CustomerEntity CustomerObj);

        CustomerEntity ResetPasswordAndVerifyOTP(CustomerEntity CustomerObj);
        //CustomerEntity ChangePassword(CustomerEntity CustomerObj);
        CustomerEntity UpdateLanguageForCustomer(int customerid, int? languageid);
        List<ProductEntity> GetBestSellerListByStore(int StoreID,int customerId);
    }
}
