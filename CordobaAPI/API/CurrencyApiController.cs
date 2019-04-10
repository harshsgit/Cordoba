using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class CurrencyApiController : ApiController
    {
        public  ICurrencyService _CurrencyService;
        // GET: CurrencyApi
        public CurrencyApiController()
        {
            _CurrencyService = new CurrencyService();
        }

        [HttpGet]
        public HttpResponseMessage GetCurrencyList(int StoreId, int LoggedInUserId)
        {
            try
            {
                var result = _CurrencyService.GetCurrencyList( StoreId, LoggedInUserId);
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
        public HttpResponseMessage GetCurrencyDetail(int StoreId, int LoggedInUserId, int CurrencyId = 0)
        {
            try
            {
                var result = _CurrencyService.GetCurrencyDetail( StoreId,  LoggedInUserId, CurrencyId);
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
        public HttpResponseMessage CreateOrUpdateCurrency(int StoreId, int LoggedInUserId, CurrencyEntity CurrencyEntity)
        {
            try
            {
                var result = _CurrencyService.CreateOrUpdateCurrency(StoreId, LoggedInUserId, CurrencyEntity);
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public HttpResponseMessage DeleteCurrency(int StoreId, int LoggedInUserId, int CurrencyId = 0)
        {
            try
            {
                var result = _CurrencyService.DeleteCurrency(StoreId, LoggedInUserId, CurrencyId);
                return Request.CreateResponse(HttpStatusCode.OK, result);

            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}