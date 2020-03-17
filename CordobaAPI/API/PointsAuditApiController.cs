using CordobaAPI.Auth;
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
        [JwtAuthentication]
        public HttpResponseMessage GetPointsAuditList(string customer_id)
        {
            try
            {
                int CustID = Convert.ToInt32(AESEncrytDecry.DecryptStringAES(customer_id));


                if (Convert.ToInt32(User.Identity.Name) != CustID)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
                }

                var result = _PointsAuditServices.GetPointsAuditList(CustID);
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
