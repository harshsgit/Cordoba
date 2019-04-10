using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels.Entities;

namespace CordobaAPI.API
{
    public class CustomerDepartmentApiController : ApiController
    {
        public ICustomerDepartmentService _customerDepartmentService;
        public CustomerDepartmentApiController()
        {
            _customerDepartmentService = new CustomerDepartmentService();
        }


        [HttpGet]
        public HttpResponseMessage GetCustomerDepartmentList(int StoreId)
        {
            try
            {
                var result = _customerDepartmentService.GetCustomerDepartmentList(StoreId);
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
        public HttpResponseMessage GetCustomerDepartmentById(int CustomerDepartmentId)
        {
            try
            {
                var result = _customerDepartmentService.GetCustomerDepartmentById(CustomerDepartmentId);
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
        public HttpResponseMessage DeleteCustomerDepartment(int CustomerDepartmentId, int StoreId)
        {
            try
            {
                var result = _customerDepartmentService.DeleteCustomerDepartment(CustomerDepartmentId, StoreId);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public HttpResponseMessage InsertOrUpdateCustomerDepartment(CustomerDepartmentEntity objCustomerDepartmentEntity)
        {
            int result = _customerDepartmentService.InsertOrUpdateCustomerDepartment(objCustomerDepartmentEntity);
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
