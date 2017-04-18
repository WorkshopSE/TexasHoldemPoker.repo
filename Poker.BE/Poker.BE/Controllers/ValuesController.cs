using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Poker.BE.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //[HttpGet]
        //public HttpResponseMessage idan(HttpRequestMessage request)
        //{
        //    request.
        //    HttpResponseMessage response = request.CreateErrorResponse(HttpStatusCode.OK, "idan is great!");
        //    return response;
        //}

        [HttpGet]
        public string IdanTwoGet()
        {
            return "idan was here";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
