using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CordobaServices.Interfaces_Layout;
using CordobaModels.Entities;
using System.Data.SqlClient;
using System.Data;
using CordobaModels;
using CordobaServices.Services;

namespace CordobaServices.Services_Layout
{
    public class LayoutDashboardServices : ILayoutDashboardServices
    {
        private GenericRepository<CategoryEntity> objGenericRepository = new GenericRepository<CategoryEntity>();

        public List<CategoryEntity> GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory, int customer_id)
        {
            try
            {
                List<CategoryEntity> CategoryList = new List<CategoryEntity>();
                var paramStoreId = new SqlParameter { ParameterName = "StoreId", DbType = DbType.Int32, Value = StoreID };
                var paramNeedToGetAllSubcategory = new SqlParameter { ParameterName = "NeedToGetAllSubcategory", DbType = DbType.Boolean, Value = NeedToGetAllSubcategory };
                var paramcustomer_id = new SqlParameter { ParameterName = "customer_id", DbType = DbType.Int32, Value = customer_id };
                CategoryList = objGenericRepository.ExecuteSQL<CategoryEntity>("GetCategoryListByStoreId", paramStoreId, paramNeedToGetAllSubcategory, paramcustomer_id).ToList();
                return CategoryList;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public StoreEntity GetStoreDetailByUrl(String URL)
        {
            try
            {
                StoreEntity storeEntity = new StoreEntity();
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("URL", URL) };
                storeEntity = objGenericRepository.ExecuteSQL<StoreEntity>("GetStoreDetailByUrl", sqlParameter).FirstOrDefault();
                return storeEntity;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProductEntity> GetLatestProductByStoreId(int StoreID, int Customer_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID),
                 new SqlParameter("Customer_Id", Customer_Id) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetLatestProductByStoreId", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<CategoryPopularEntity> GetPopularCategoryListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID), new SqlParameter("customer_id", customer_id) };
                var result = objGenericRepository.ExecuteSQL<CategoryPopularEntity>("GetPopularCategoryListByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProductEntity> GetHotDealsListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID), new SqlParameter("customer_id", customer_id) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetHotDealsListByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<ProductEntity> GetSpecialOfferListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID), new SqlParameter("customer_id", customer_id) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetSpecialOfferListByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public CustomerEntity CustomerLogin(CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("email", CustomerObj.email)
               ,new SqlParameter("password", CustomerObj.password)
               ,new SqlParameter("cartgroup_id", (CustomerObj.cartgroup_id!=null?CustomerObj.cartgroup_id:(object)DBNull.Value))
               ,new SqlParameter("store_id", CustomerObj.store_id)
               ,new SqlParameter("IsFromAdmin",CustomerObj.IsFromAdmin)
            };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("CustomerLogin", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public int? AddtoWishList(wishlistEntity WishlistObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("customer_id", WishlistObj.customer_id)
               ,new SqlParameter("store_id", WishlistObj.store_id)
               ,new SqlParameter("product_id", WishlistObj.product_id)               
            };
                var result = objGenericRepository.ExecuteSQL<int>("AddtoWishList", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int? RemoveFromWishList(int storeid, int product_id, int customer_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("customer_id",customer_Id)
               ,new SqlParameter("store_id",storeid)
               ,new SqlParameter("product_id",product_id)               
            };
                var result = objGenericRepository.ExecuteSQL<int>("RemoveFromWishList", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public CustomerEntity CustomerDetailLayout(int CustomerId, int StoreId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("CustomerId", CustomerId)
               ,new SqlParameter("StoreId",StoreId)              
            };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("CustomerDetail_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public int? SaveCustomerBasicDetails_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  

               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",CustomerObj.customer_id)  
             , new SqlParameter("firstname",CustomerObj.firstname)  
             , new SqlParameter("lastname",CustomerObj.lastname)     
             , new SqlParameter("email",CustomerObj.email)     
             , new SqlParameter("telephone",CustomerObj.telephone!=null ?CustomerObj.telephone:(object) DBNull.Value )     
            };
                var result = objGenericRepository.ExecuteSQL<int>("UpdateCustomerBasicDetails_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public int? SaveChangedPassword_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  

               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",CustomerObj.customer_id)  
             , new SqlParameter("password",CustomerObj.password)                 
            };
                var result = objGenericRepository.ExecuteSQL<int>("UpdateChangedPassword_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }


        }

        public List<AddressEntity> GetCustomerAddressList_Layout(int StoreId, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",customer_id)                            
            };
                var result = objGenericRepository.ExecuteSQL<AddressEntity>("GetCustomerAddressList_Layout", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<AddressEntity> AddOrUpdateAddressDetail_Layout(int StoreId, AddressEntity AddressObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("StoreId",StoreId) 
             , new SqlParameter("address_id",AddressObj.address_id)
             , new SqlParameter("customer_id",AddressObj.customer_id)  
             , new SqlParameter("company",AddressObj.company!=null ?AddressObj.company:(object)DBNull.Value)  
             , new SqlParameter("firstname",AddressObj.firstname!=null? AddressObj.firstname:(object)DBNull.Value)  
             , new SqlParameter("lastname",AddressObj.lastname!=null? AddressObj.lastname:(object)DBNull.Value)  
             , new SqlParameter("address_1",AddressObj.address_1!=null? AddressObj.address_1:(object)DBNull.Value)  
             , new SqlParameter("address_2",AddressObj.address_2!=null? AddressObj.address_2:(object)DBNull.Value)  
             , new SqlParameter("postcode",AddressObj.postcode!=null? AddressObj.postcode:(object)DBNull.Value)  
             , new SqlParameter("city",AddressObj.city!=null? AddressObj.city:(object)DBNull.Value)  
             , new SqlParameter("county",AddressObj.county!=null? AddressObj.county:(object)DBNull.Value)  
             , new SqlParameter("country_id",AddressObj.country_id!=null? AddressObj.country_id:(object)DBNull.Value) 
             , new SqlParameter("IsDefaultAddress",AddressObj.IsDefaultAddress!=null?AddressObj.IsDefaultAddress:false) 
            };
                var result = objGenericRepository.ExecuteSQL<AddressEntity>("AddOrUpdateAddressDetail_Layout", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }


        public int? DeleteCustomerAddress(int StoreId, int customer_id, int address_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  

               new SqlParameter("StoreId",StoreId)   
             , new SqlParameter("customer_id",customer_id)                            
             , new SqlParameter("address_id",address_id) 
            };
                var result = objGenericRepository.ExecuteSQL<int>("DeleteCustomerAddress_Layout", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<StoreImageEntity> GetStoreImageList(int Store_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("Store_Id",Store_Id)
            };
                var result = objGenericRepository.ExecuteSQL<StoreImageEntity>("GetStoreImageByStoreId_Dashboard", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<StoreTermsEntity> GetStoreTermsDetail(int Store_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {  
               new SqlParameter("Store_Id",Store_Id)
            };
                var result = objGenericRepository.ExecuteSQL<StoreTermsEntity>("GetStoreTermsDetail", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<ProductEntity> GetBestSellerListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreId", StoreID), new SqlParameter("customer_id", customer_id) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetBestSellerListByStoreId", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public List<ProductEntity> GetBestSellerListByStore(int storeId, int customer_id)
        {
            try
            {
                
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreId", storeId), new SqlParameter("Customer_id", customer_id) };
                var result = objGenericRepository.ExecuteSQL<ProductEntity>("GetBestSellerListByStore", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<OrderDetailCountEntity> GetOrderStatusCount(int StoreID)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreID) };
                var result = objGenericRepository.ExecuteSQL<OrderDetailCountEntity>("GetOrderCount", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }



        public CustomerEntity ForgotPassword(CustomerEntity CustomerObj)
        {
            Random rnd = new Random();
            string otp = rnd.Next(0, 1000000).ToString("D6");
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("email", CustomerObj.email)
               ,new SqlParameter("store_id", CustomerObj.store_id)
               ,new SqlParameter("otp", otp)
               
            };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("ForgotPasswordCustomerUser", sqlParameter).FirstOrDefault();
                if (result.errorcode > 0)
                {
                    CommonService customerService = new CommonService();
                    customerService.SendOTPEmail(CustomerObj.email, otp, CustomerObj.firstname, CustomerObj.store_name, CustomerObj.logo);
                }
                else
                {
                    return result;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public CustomerEntity VerifyOTP(CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { 
                new SqlParameter("email", CustomerObj.email)
               ,new SqlParameter("store_id", CustomerObj.store_id)
               ,new SqlParameter("otp", CustomerObj.otp)
                };

                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("Password_VerifyOTP", sqlParameter).FirstOrDefault();
                return result;

            }
            catch(Exception)
            {
                throw;
            }

        }

        public List<BannerAttributeEntity> GetBanner_Layout(int StoreId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] { new SqlParameter("StoreID", StoreId) };
                var result = objGenericRepository.ExecuteSQL<BannerAttributeEntity>("GetBanner_Layout", sqlParameter).ToList();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public CustomerEntity ChangePassword(CustomerEntity CustomerObj)
        //{
        //    try
        //    {
        //        try
        //        {
        //            SqlParameter[] sqlParameter = new SqlParameter[] { 
        //        new SqlParameter("email", CustomerObj.email)
        //       ,new SqlParameter("store_id", CustomerObj.store_id)
        //       ,new SqlParameter("otp", CustomerObj.otp)
        //        };

        //            var result = objGenericRepository.ExecuteSQL<CustomerEntity>("ChangePassword", sqlParameter).FirstOrDefault();
        //            return result;

        //        }
        //        catch (Exception)
        //        {
        //            throw;
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        throw;
        //    }
        //}

        public int? VerifyCustomerVisitedInfo(string CustomerEmail,int? store_id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("email", CustomerEmail)
                    ,new SqlParameter("store_id", store_id)
                };

                var result = objGenericRepository.ExecuteSQL<int>("Email_VerifyCustomerVisitedInfo", sqlParameter).FirstOrDefault();
                return result;

            }
            catch (Exception)
            {
                throw;
            }

        }

        public CustomerEntity ResetPasswordOTP(CustomerEntity CustomerObj)
        {
            Random rnd = new Random();
            string otp = rnd.Next(0, 1000000).ToString("D6");
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("email", CustomerObj.email)
                    ,new SqlParameter("store_id", CustomerObj.store_id)
                    ,new SqlParameter("otp", otp)

                };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("ResetPasswordOTPSend", sqlParameter).FirstOrDefault();
                if (result.errorcode > 0)
                {
                    CommonService customerService = new CommonService();
                    customerService.SendResetPassOTPEmail(CustomerObj.email, otp, CustomerObj.firstname, CustomerObj.store_name, CustomerObj.logo);
                }
                else
                {
                    return result;
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public CustomerEntity ResetPasswordAndVerifyOTP(CustomerEntity CustomerObj)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("email", CustomerObj.email)
                    ,new SqlParameter("store_id", CustomerObj.store_id)
                    ,new SqlParameter("otp", CustomerObj.otp)
                    ,new SqlParameter("new_pass",Security.Encrypt(CustomerObj.password) ??  DBNull.Value.ToString())

                };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("ResetPasswordAndVerifyOTP", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public CustomerEntity UpdateLanguageForCustomer(int customerid, int? languageid)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[] {
                    new SqlParameter("CustomerId", customerid)
                    ,new SqlParameter("LanguageId", languageid ?? (object)DBNull.Value )
                };
                var result = objGenericRepository.ExecuteSQL<CustomerEntity>("UpdateLanguageForCustomer", sqlParameter).FirstOrDefault();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
