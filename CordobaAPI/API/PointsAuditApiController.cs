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
    public class PointsAuditApiController : ApiController
    {
        public IPointsAudit _PointsAuditServices;
        public PointsAuditApiController()
        {
            _PointsAuditServices = new PointsAuditService();
        }


        [HttpGet]
        public HttpResponseMessage GetPointsAuditList(int customer_id)
        {
            try
            {
                var result = _PointsAuditServices.GetPointsAuditList(customer_id);
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
    }
}
