
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
    public class TestApiController : ApiController
    {

        public ITestServices _TestServices;

        public TestApiController()
        {
            _TestServices = new TestServices();
        }

        [HttpPost]
        public HttpResponseMessage InsertOrUpdateCompanyLayout(TestEntity model)
        {
            var result = _TestServices.InsertOrUpdateCompanyLayout(model);
            if (result != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }

            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Something wrong! Please try again later.");
        }

        // GET: api/TestApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/TestApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TestApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TestApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TestApi/5
        public void Delete(int id)
        {
        }

       
    }
}
