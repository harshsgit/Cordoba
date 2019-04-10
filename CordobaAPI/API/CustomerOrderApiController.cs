using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class CustomerOrderApiController : ApiController
    {
        // GET: api/CustomerOrderApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CustomerOrderApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CustomerOrderApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CustomerOrderApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CustomerOrderApi/5
        public void Delete(int id)
        {
        }
    }
}
