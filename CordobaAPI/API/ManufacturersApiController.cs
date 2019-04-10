using CordobaServices.Interfaces;
using CordobaServices.Services;
using CordobaModels.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class ManufacturersApiController : ApiController
    {
        public IManufacturerServices _ManufacturerServices;
        // GET: api/ManufacturersApi
        public ManufacturersApiController()
        {
            _ManufacturerServices = new ManufacturerServices();
        }

        [HttpGet]
        public HttpResponseMessage GetManufacturersList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _ManufacturerServices.GetManufacturersList(StoreId, LoggedInUserId);
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
        public HttpResponseMessage GetManufaturerDetail(int StoreId, int LoggedInUserId, int manufacturer_id)
        {
            try
            {
                var result = _ManufacturerServices.GetManufaturerDetail(StoreId, LoggedInUserId, manufacturer_id);
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
        public HttpResponseMessage InsertUpdateManufacture(int StoreId, int LoggedInUserId, ManufacturersEntity manufacturersEntity)
        {
            try
            {
                var result = _ManufacturerServices.InsertUpdateManufacture(StoreId, LoggedInUserId, manufacturersEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }


        [HttpGet]
        public HttpResponseMessage DeleteManufacturer(int StoreId, int LoggedInUserId, int manufacturer_id)
        {
            try
            {
                var result = _ManufacturerServices.DeleteManufacturer(StoreId, LoggedInUserId, manufacturer_id);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
