using CordobaModels.Entities;
using CordobaServices.Interfaces_Layout;
using CordobaServices.Services_Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API_Layout
{
    public class CartApiController : ApiController
    {
        public ICartServices _CartServices;    
 
        public CartApiController()
        {
            _CartServices = new CartServices();
        }

        [HttpGet]
        public HttpResponseMessage GetCartDetailsByCustomerAndStoreId(int? StoreID, int CustomerId)
        {
            try
            {
                var result = _CartServices.GetCartDetailsByCustomerAndStoreId(StoreID, CustomerId);
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
        public HttpResponseMessage GetCartDetailsByCartGroupId(int? StoreID, int cartgroup_id)
        {
            try
            {
                var result = _CartServices.GetCartDetailsByCartGroupId(StoreID, cartgroup_id);
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
         public HttpResponseMessage RemoveProductFromCart(int? CartId)
         {
             try
             {
                 var result = _CartServices.RemoveProductFromCart(CartId);
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
         public HttpResponseMessage PlaceOrder(PlaceOrderEntity PlaceOrderObj)
         {
             try
             {
                 var result = _CartServices.PlaceOrder(PlaceOrderObj);
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

        [HttpGet]
         public HttpResponseMessage GetCustmoreAddressList(int? store_id, int customer_id)
         {
             try
             {
                 var result = _CartServices.GetCustmoreAddressList(store_id, customer_id);
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


        // GET: api/CartApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CartApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CartApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CartApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CartApi/5
        public void Delete(int id)
        {
        }
    }
}
