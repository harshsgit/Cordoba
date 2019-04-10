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
    public class ActivityApiController : ApiController
    {
        public ActivityService activityService;
        public ActivityApiController()
        {
            activityService = new ActivityService();
        }

        [HttpGet]
        public HttpResponseMessage GetActivityList(int store_id)
        {
            try
            {
                var result = activityService.GetActivityList(store_id);
                if (result != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
            }
            catch(Exception e)
            {
                throw;
            }
        }
    }
}