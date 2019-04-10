using CordobaModels.Entities;
using CordobaServices.Interfaces;
using CordobaServices.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class ContactUsAPIController : ApiController
    {
        public IContactUsService _IContactUsService;
        public ContactUsAPIController()
        {
            _IContactUsService = new ContactUsService();
        }

        [HttpPost]
        public HttpResponseMessage SendContactUsDetails(string firstname, string lastname, string email, string phone, string description, StoreEntity storeEntity)
        {
            try
            {
                var result = _IContactUsService.sendContactUsDetails(firstname, lastname, email, phone, description, storeEntity);
                if (result)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong? Please try again later.");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}