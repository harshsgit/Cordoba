﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Interfaces_Layout;
using CordobaServices.Services_Layout;
using System.Web.Http.Hosting;
using System.Web.Mvc;
using CordobaModels.Entities;
using CordobaModels;
using CordobaAPI.Auth;

namespace CordobaAPI.API_Layout
{

    public class LayoutDashboardAPIController : ApiController
    {
        public ILayoutDashboardServices _LayoutDashboardServices;

        public LayoutDashboardAPIController()
        {
            _LayoutDashboardServices = new LayoutDashboardServices();
        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetCategoryListByStoreId(int? StoreID, bool NeedToGetAllSubcategory, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetCategoryListByStoreId(StoreID, NeedToGetAllSubcategory, customer_id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetStoreDetailByUrl(String URL)
        {
            try
            {
                var result = _LayoutDashboardServices.GetStoreDetailByUrl(URL);
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
        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetLatestProductByStoreId(int StoreID, int Customer_Id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetLatestProductByStoreId(StoreID, Customer_Id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetPopularCategoryListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetPopularCategoryListByStoreId(StoreID, customer_id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetHotDealsListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetHotDealsListByStoreId(StoreID, customer_id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetSpecialOfferListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetSpecialOfferListByStoreId(StoreID, customer_id);
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


        [System.Web.Http.HttpPost]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage CustomerLogin(CustomerEntity CustomerObj)
        {
            try
            {
                CustomerObj.password = Security.Encrypt(CustomerObj.password);
                var result = _LayoutDashboardServices.CustomerLogin(CustomerObj);
                if (result != null && result.customer_id > 0)
                {
                    result.JWTToken = JwtAuthManager.GenerateJWTToken(Convert.ToString(result.customer_id));
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetBestSellerListByStoreId(int StoreID, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetBestSellerListByStoreId(StoreID, customer_id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetBestSellerListByStore(int StoreID, int customer_id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetBestSellerListByStore(StoreID, customer_id);
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

        [System.Web.Http.HttpPost]
        public HttpResponseMessage AddtoWishList(wishlistEntity WishObj)
        {
            try
            {
                var result = _LayoutDashboardServices.AddtoWishList(WishObj);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage RemoveFromWishList(int StoreID, int product_id, int Customer_Id)
        {
            try
            {
                var result = _LayoutDashboardServices.RemoveFromWishList(StoreID, product_id, Customer_Id);
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

        [System.Web.Http.HttpGet]
        [JwtAuthentication]
        public HttpResponseMessage CustomerDetailLayout(string CustomerId, string StoreId)
        {
            try
            {
                int CustID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(CustomerId));
                int storeID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(StoreId));
                if (Convert.ToInt32(User.Identity.Name) != CustID)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }

                var result = _LayoutDashboardServices.CustomerDetailLayout(CustID, storeID);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [System.Web.Http.HttpPost]
        [JwtAuthentication]
        public HttpResponseMessage SaveCustomerBasicDetails_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                if (Convert.ToInt32(User.Identity.Name) != CustomerObj.customer_id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }

                var result = _LayoutDashboardServices.SaveCustomerBasicDetails_Layout(StoreId, CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ForgotPasswordChange_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                CustomerObj.password = Security.Encrypt(CustomerObj.password);
                var result = _LayoutDashboardServices.SaveChangedPassword_Layout(StoreId, CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [System.Web.Http.HttpPost]
        [JwtAuthentication]
        public HttpResponseMessage SaveChangedPassword_Layout(int StoreId, CustomerEntity CustomerObj)
        {
            try
            {
                if (Convert.ToInt32(User.Identity.Name) != CustomerObj.customer_id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }

                CustomerObj.password = Security.Encrypt(CustomerObj.password);
                var result = _LayoutDashboardServices.SaveChangedPassword_Layout(StoreId, CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [System.Web.Http.HttpGet]
        [JwtAuthentication]
        public HttpResponseMessage GetCustomerAddressList_Layout(string StoreId, string customer_id)
        {
            try
            {
                int CustID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(customer_id));
                int storeID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(StoreId));

                if (Convert.ToInt32(User.Identity.Name) != CustID)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }

                var result = _LayoutDashboardServices.GetCustomerAddressList_Layout(storeID, CustID);
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

        [System.Web.Http.HttpPost]
        [JwtAuthentication]
        public HttpResponseMessage AddOrUpdateAddressDetail_Layout(int StoreId, AddressEntity AddressObj)
        {
            try
            {
                if (Convert.ToInt32(User.Identity.Name) != AddressObj.customer_id)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }
                var result = _LayoutDashboardServices.AddOrUpdateAddressDetail_Layout(StoreId, AddressObj);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [System.Web.Http.HttpGet]
        public HttpResponseMessage DeleteCustomerAddress(int StoreId, int customer_id, int address_id)
        {
            try
            {
                var result = _LayoutDashboardServices.DeleteCustomerAddress(StoreId, customer_id, address_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetStoreImageList(int Store_Id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetStoreImageList(Store_Id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetStoreTermsDetail(int Store_Id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetStoreTermsDetail(Store_Id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetOrderDetailCount(int Store_Id)
        {
            try
            {
                var result = _LayoutDashboardServices.GetOrderStatusCount(Store_Id);
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

        [System.Web.Http.HttpGet]
        public HttpResponseMessage GetBanner_Layout(int StoreID)
        {
            try
            {
                var result = _LayoutDashboardServices.GetBanner_Layout(StoreID);
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

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ForgotPassword(CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.ForgotPassword(CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch (Exception)
            {

                throw;
            }

        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage VerifyOTP(CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.VerifyOTP(CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage VisitedCustomerInfo(CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.VerifyCustomerVisitedInfo(CustomerObj.email, CustomerObj.store_id);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage SendResetPassEmail(CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.ResetPasswordOTP(CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                throw;
            }
        }

        [System.Web.Http.HttpPost]
        public HttpResponseMessage ResetPasswordAndOtpVerify(CustomerEntity CustomerObj)
        {
            try
            {
                var result = _LayoutDashboardServices.ResetPasswordAndVerifyOTP(CustomerObj);

                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                throw;
            }
        }


        [System.Web.Http.HttpPost]
        public HttpResponseMessage UpdateLanguageForCustomer(int customerid, int? languageid)
        {
            try
            {
                var result = _LayoutDashboardServices.UpdateLanguageForCustomer(customerid, languageid);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception e)
            {
                throw;
            }
        }


        //[HttpPost]
        //public HttpResponseMessage ChangePassword(CustomerEntity CustomerObj)
        //{
        //    try
        //    {
        //        var result = _LayoutDashboardServices.ChangePassword(CustomerObj);
        //        if (result != null)
        //        {
        //            return Request.CreateResponse(HttpStatusCode.OK, result);
        //        }
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
        //    }
        //    catch (Exception e)
        //    {
        //        throw;
        //    }
        //}

    }
}
