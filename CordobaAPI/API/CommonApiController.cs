using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CordobaAPI.API
{
    public class CommonApiController : ApiController
    {
        // GET: api/CommonApi
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CommonApi/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CommonApi
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CommonApi/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CommonApi/5
        public void Delete(int id)
        {
        }


    }
}
