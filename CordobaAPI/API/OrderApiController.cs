using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices;
using CordobaServices.Helpers;
using CordobaAPI.Auth;

namespace CordobaAPI.API
{
    
    public class OrderApiController : ApiController
    {
        public IOrderService _orderService;
        public ICountryServices _countryServices;
        public OrderApiController()
        {
            _orderService = new OrderService();
            _countryServices = new CountryServices();
        }


        [HttpGet]
        [JwtAuthentication]
        public HttpResponseMessage GetOrderHistory(string StoreId, string LoggedInUserId, string customer_id)
        {
            try
            {

                int storeID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(StoreId));
                int LoggedUserID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(LoggedInUserId));
                int CustID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(customer_id));

                if (Convert.ToInt32(User.Identity.Name) != CustID)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }
                var result = _orderService.GetOrderHistory(storeID, LoggedUserID, CustID);
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
        public HttpResponseMessage GetOrderDetails(int StoreId, int LoggedInUserId, int orderId)
        {
            try
            {
                var result = _orderService.GetOrderDetails(StoreId, LoggedInUserId, orderId);
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
        public HttpResponseMessage InsertOrderHistory(int StoreId, int LoggedInUserId, OrderHistoryEntity objHistoryEntity)
        {
            try
            {
                var result = _orderService.InsertOrderHistory(StoreId, LoggedInUserId, objHistoryEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public TableParameter<OrderEntity> GetOrderList(int StoreId, int LoggedInUserId, int PageIndex, int? orderId, int? order_status_id, string CustomerName, Nullable<DateTime> DateAdded, Nullable<DateTime> DateModified, TableParameter<OrderEntity> tableParameter)
        {
            try
            {
                tableParameter.PageIndex = PageIndex;
                string sortColumn = tableParameter.SortColumn.Desc ? tableParameter.SortColumn.Column + " desc" : tableParameter.SortColumn.Column + " asc";
                var result = _orderService.GetOrderList(StoreId, LoggedInUserId, sortColumn, orderId, order_status_id, CustomerName, DateAdded, DateModified, tableParameter, "").ToList();

                int totalRecords = 0;
                if (result != null && result.Count > 0)
                {
                    totalRecords = result.FirstOrDefault().TotalRecords;
                }

                return new TableParameter<OrderEntity>
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
        public HttpResponseMessage GetCustomerGroupList(int StoreId, int LoggedInUserId)
        {
            var customerGroup = _orderService.GetCustomerGroupList(StoreId, LoggedInUserId);
            if (customerGroup != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, customerGroup);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
        }

        [HttpGet]
        public HttpResponseMessage GetCurrencyList(int StoreId, int LoggedInUserId)
        {
            var currency = _orderService.GetCurrencyList(StoreId, LoggedInUserId);
            if (currency != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, currency);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
        }


        [HttpGet]
        public HttpResponseMessage GetCountryList(int StoreId, int LoggedInUserId, int countryId)
        {
            try
            {
                var result = _countryServices.GetCountryList(StoreId, LoggedInUserId, countryId).OrderBy(m => m.name).ToList();
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
        public HttpResponseMessage GetZoneListByCountry(int StoreId, int LoggedInUserId, int countryId)
        {
            try
            {
                var result = _orderService.GetZoneListByCountry(StoreId, LoggedInUserId, countryId).ToList();
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

        public HttpResponseMessage GetCustomerAddress(int StoreId, int LoggedInUserId, int orderId)
        {
            try
            {
                var result = _orderService.GetCustomerAddress(StoreId, LoggedInUserId, orderId);
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
        public HttpResponseMessage UpdateOrder_CutomerDetails(int LoggedInUserId, OrderEntity objOrderEntity)
        {
            try
            {
                var result = _orderService.UpdateOrder_CutomerDetails(LoggedInUserId, objOrderEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateOrder_PaymentDetails(int StoreId, int LoggedInUserId, OrderEntity objOrderEntity)
        {
            try
            {
                var result = _orderService.UpdateOrder_PaymentDetails(StoreId, LoggedInUserId, objOrderEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateOrder_ShippingDetails(int StoreId, int LoggedInUserId, OrderEntity objOrderEntity)
        {
            try
            {
                var result = _orderService.UpdateOrder_ShippingDetails(StoreId, LoggedInUserId, objOrderEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage GetCustomersByStore(int storeId, int LoggedInUserId)
        {
            try
            {
                var result = _orderService.GetCustomersByStore(storeId, LoggedInUserId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public HttpResponseMessage UpdateOrder_TotalDetails(int StoreId, int LoggedInUserId, int order_id, int order_status_id, string comment)
        {
            try
            {
                var result = _orderService.UpdateOrder_TotalDetails(StoreId, LoggedInUserId, order_id, order_status_id, comment);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        [JwtAuthentication]
        public HttpResponseMessage GetOrderDetail_Layout(string order_id, string store_id)
        {
            try
            {
                int OrderID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(order_id));
                int storeID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(store_id));

                var result = _orderService.GetOrderDetail_Layout(OrderID, storeID);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage UpdateOrderStatus(int OrderId, int OrderStatusId, string Comment)
        {
            try
            {
                var result = _orderService.UpdateOrderStatus(OrderId, OrderStatusId, Comment);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpPost]
        public HttpResponseMessage UpdateOrderDate(int OrderId, DateTime OrderDate)
        {
            try
            {
                var result = _orderService.UpdateOrderDate(OrderId, OrderDate);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
